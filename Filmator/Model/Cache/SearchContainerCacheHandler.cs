using System;
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
        private string _type;
        private int _page;

        public SearchContainerCacheHandler(string type, int page) {
            _type = type;
            _page = page;
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

        public bool Exist(SearchContainer<MovieResult> obj) {
            throw new NotImplementedException();
        }

        public List<SearchContainer<MovieResult>> GetAll() {
            throw new NotImplementedException();
        }

        public SearchContainer<MovieResult> Get(object arg) {
            var cacheItem = (from item in _currentList.Items 
                             where item.Type == _type && item.SearchContainer.Page == _page 
                             select item).FirstOrDefault();
            if (cacheItem == null)
                return null;
            if (cacheItem.IsExpired()) {
                _currentList.Items.Remove(cacheItem);
                return null;
            }
            return cacheItem.SearchContainer;
        }

        public void Add(SearchContainer<MovieResult> obj, DateTime expirationDate) {
            var serializer = new JsonSerializer();
            _currentList.Items.Add(new CacheItem {
                SearchContainer = obj,
                ExpirationDate = expirationDate,
                Type = _type
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
            public string Type { get; set; }
            public DateTime ExpirationDate { get; set; }
            public bool IsExpired() {
                return DateTime.Now > ExpirationDate;
            }
        }
    }
}
