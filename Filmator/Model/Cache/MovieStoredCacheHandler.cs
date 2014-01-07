using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TMDbLib.Objects.Movies;

namespace Filmator.Model.Cache {
    class MovieStoredCacheHandler : ICache<Movie> {
        public const string _path = "Cache/Movie.json";
        public string CachePath { get; set; }
        private CacheList _currentList;

        public MovieStoredCacheHandler() {
            CachePath = Path.GetFullPath(_path);
            Directory.CreateDirectory(Path.GetDirectoryName(CachePath));
            _currentList = GetCurrentList();
        }

        public bool Exist(Movie obj) {
            return _currentList.Items.Any(item => item.Movie.Id == obj.Id);
        }

        public Movie Get(object id) {
            var item = _currentList.Items.FirstOrDefault(i => i.Movie.Id == (int) id);
            if (item == null)
                return null;
            if (item.IsExpired()) {
                _currentList.Items.Remove(item);
                return null;
            }
            return item.Movie;
        }

        public void Add(Movie obj, DateTime expirationDate, string state = "") {
            var serializer = new JsonSerializer();
            _currentList.Items.Add(new CacheItem {
                Movie = obj,
                ExpirationDate = expirationDate
            });
            using (var fs = File.Open(CachePath, FileMode.OpenOrCreate))
            using (var sw = new StreamWriter(fs))
            using (var jw = new JsonTextWriter(sw)) {
                jw.Formatting = Formatting.Indented;
                serializer.Serialize(jw, _currentList);
            }
        }

        private CacheList GetCurrentList() {
            using (var fs = File.Open(CachePath, FileMode.OpenOrCreate, FileAccess.Read))
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
        public Movie Movie { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsExpired() {
            return DateTime.Now > ExpirationDate;
        }
    }
}
