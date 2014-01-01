using System;
using System.IO;
using Filmator.Model.Cache;
using Filmator.Model.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilmatorTest.Cache {
    [TestClass]
    public class CacheTest {
        [TestMethod]
        public void TestFileExists() {
            Assert.IsFalse(File.Exists(Path.GetFullPath("test/file")));
        }

        [TestMethod]
        public void TestFileCreate() {
            var path = Path.GetFullPath("Cache/data.json");
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.Create(path);
            Assert.IsTrue(File.Exists(path));
        }

        [TestMethod]
        public void CacheAdd() {
            var cache = CacheHandlerFactory.GetCacheHandler<MovieStored>();
            var movie = new MovieStored {
                ID = 5,
                ImdbId = "2545",
                Title = "Jean Jean",
                RemoteID = 25,
                Status = "cool",
                Adult = false,
                Overview = "bla bla bla bla bla bla",
                Seen = true
            };
            cache.Add(movie, DateTime.Now.AddMonths(2));
        }

        [TestMethod]
        public void CacheExist() {
            var cache = CacheHandlerFactory.GetCacheHandler<MovieStored>();
            var movie = new MovieStored {
                ID = 5,
                ImdbId = "2545",
                Title = "Jean Jean",
                RemoteID = 27,
                Status = "cool",
                Adult = false,
                Overview = "bla bla bla bla bla bla",
                Seen = true
            };
            cache.Add(movie, DateTime.Now.AddMonths(2));
            Assert.IsTrue(cache.Exist(movie));
        }
    }
}
