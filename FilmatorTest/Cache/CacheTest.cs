using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Filmator.Model.Cache;
using Filmator.Model.Entities;
using Filmator.Model.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMDbLib.Objects.General;

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
        public void CacheAddSearchContainer() {
            var cache = CacheHandlerFactory.GetCacheHandler<SearchContainer<MovieResult>>();
            var search = new SearchContainer<MovieResult> {
                Page = 2,
                TotalPages = 25,
                TotalResults = 750,
                Results = new List<MovieResult> {
                    new MovieResult {
                        Title = "ok",
                        Id = 25
                    },
                    new MovieResult {
                        Title = "ok1",
                        Id = 251
                    },
                    new MovieResult {
                        Title = "ok2",
                        Id = 252
                    },
                    new MovieResult {
                        Title = "ok3",
                        Id = 253
                    },
                }
            };
            cache.Add(search, DateTime.Now.AddMonths(2), "testState");
        }

        [TestMethod]
        public void CacheGetSearchContainer() {
            var cache = CacheHandlerFactory.GetCacheHandler<SearchContainer<MovieResult>>();
            var search = new SearchContainer<MovieResult> {
                Page = 2,
                TotalPages = 25,
                TotalResults = 750,
                Results = new List<MovieResult> {
                    new MovieResult {
                        Title = "ok",
                        Id = 25
                    },
                    new MovieResult {
                        Title = "ok1",
                        Id = 251
                    },
                    new MovieResult {
                        Title = "ok2",
                        Id = 252
                    },
                    new MovieResult {
                        Title = "ok3",
                        Id = 253
                    },
                }
            };
            cache.Add(search, DateTime.Now.AddMonths(2), "newTest");
            var options = new Hashtable { { "Page", 2 }, { "State", "newTest" } };
            var res = cache.Get(options);
            Assert.AreEqual(2, res.Page);
            Assert.AreEqual(4, res.Results.Count);
        }
    }
}
