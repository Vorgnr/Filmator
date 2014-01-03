using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TMDbLib.Objects.General;

namespace Filmator.Model.Cache {
    class SearchContainerCacheHandler : ICache<SearchContainer<MovieResult>> {
        public const string _path = "Cache/SearchContainer.json";
        public string CachePath { get; set; }
        private CacheList _currentList;

        public SearchContainerCacheHandler() {
            CachePath = Path.GetFullPath(_path);
            Directory.CreateDirectory(Path.GetDirectoryName(CachePath));
            if (!File.Exists(CachePath))
                File.Create(CachePath);
            _currentList = GetCurrentList();
        }

        private CacheList GetCurrentList() {
            using (var fs = File.Open(CachePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
            using (var sr = new StreamReader(fs))
            using (var jr = new JsonTextReader(sr)) {
                var serializer = new JsonSerializer();
                return serializer.Deserialize<CacheList>(jr) ?? new CacheList();
            }
        }

        public SearchContainer<MovieResult> Get(object options) {
            if(options.GetType() != typeof(Hashtable))
                throw new ArgumentException("arg must be an Hashtable");
            var searchContainerData = options as Hashtable;
            if (searchContainerData == null || !searchContainerData.ContainsKey("Page") || !searchContainerData.ContainsKey("State"))
                throw new ArgumentException("arg must contains Page and State as key");
            if (searchContainerData["State"] == null || searchContainerData["State"].GetType() != typeof(string))
                throw new ArgumentException("Value of Page must be a String");

            var cacheItem = (from item in _currentList.Items
                             where (item.SearchContainer.Page == (int)searchContainerData["Page"]) && (item.State == (string)searchContainerData["State"])
                             select item).FirstOrDefault();
            if (cacheItem == null)
                return null;
            if (cacheItem.IsExpired()) {
                _currentList.Items.Remove(cacheItem);
                return null;
            }
            return cacheItem.SearchContainer;
        }

        public void Add(SearchContainer<MovieResult> obj, DateTime expirationDate, string state) {
            var serializer = new JsonSerializer();
            _currentList.Items.Add(new CacheItem {
                State = state,
                SearchContainer = obj,
                ExpirationDate = expirationDate,
            });
            using (var fs = File.Open(CachePath, FileMode.OpenOrCreate))
            using (var sw = new StreamWriter(fs))
            using (var jw = new JsonTextWriter(sw)) {
                jw.Formatting = Formatting.Indented;
                serializer.Serialize(jw, _currentList);
            }
        }

        class CacheList {
            public List<CacheItem> Items { get; set; }

            public CacheList() {
                Items = new List<CacheItem>();
            }
        }

        class CacheItem {
            public SearchContainer<MovieResult> SearchContainer { get; set; }
            public string State { get; set; }
            public DateTime ExpirationDate { get; set; }
            public bool IsExpired() {
                return DateTime.Now > ExpirationDate;
            }
        }
    }
}
