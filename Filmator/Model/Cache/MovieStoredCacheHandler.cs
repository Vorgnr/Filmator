using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Filmator.Model.Entities;
using Newtonsoft.Json;

namespace Filmator.Model.Cache {
    class MovieStoredCacheHandler : ICache<MovieStored> {
        public const string _path = "Cache/MovieStored.json";
        public string CachePath { get; set; }
        private CacheList _currentList;

        public MovieStoredCacheHandler() {
            CachePath = Path.GetFullPath(_path);
            Directory.CreateDirectory(Path.GetDirectoryName(CachePath));
            if (!File.Exists(CachePath))
                File.Create(CachePath);
            _currentList = GetCurrentList();
        }

        public bool Exist(MovieStored obj) {
            return _currentList.Items.Any(item => item.MovieStored.RemoteID == obj.RemoteID);
        }

        public MovieStored Get(object id) {
            var item = _currentList.Items.FirstOrDefault(i => i.MovieStored.RemoteID == (decimal) id);
            if (item == null)
                return null;
            if (item.IsExpired()) {
                _currentList.Items.Remove(item);
                return null;
            }
            return item.MovieStored;
        }

        public void Add(MovieStored obj, DateTime expirationDate) {
            var serializer = new JsonSerializer();
            _currentList.Items.Add(new CacheItem {
                MovieStored = obj,
                ExpirationDate = expirationDate
            });
            using (var fs = File.Open(CachePath, FileMode.OpenOrCreate))
            using (var sw = new StreamWriter(fs))
            using (var jw = new JsonTextWriter(sw)) {
                jw.Formatting = Formatting.Indented;
                serializer.Serialize(jw, _currentList);
            }
        }

        public List<MovieStored> GetAll() {
            throw new NotImplementedException();
        }

        private CacheList GetCurrentList() {
            using (var fs = File.Open(CachePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
            using (var sr = new StreamReader(fs))
            using (var jr = new JsonTextReader(sr)) {
                var serializer = new JsonSerializer();
                return serializer.Deserialize<CacheList>(jr) ?? new CacheList();
            }
        }

    }

    class CacheList {
        public List<CacheItem> Items { get; set; }

        public CacheList() {
            Items = new List<CacheItem>();
        }
    }

    class CacheItem {
        public MovieStored MovieStored { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsExpired() {
            return DateTime.Now > ExpirationDate;
        }
    }
}
