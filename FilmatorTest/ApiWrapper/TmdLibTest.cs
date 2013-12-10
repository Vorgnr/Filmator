using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;

namespace FilmatorTest.ApiWrapper {
    [TestClass]
    public class TmdLibTest {

        [TestMethod]
        public void TestMovieId() {
            var client = new TMDbClient(ConfigurationManager.AppSettings["ApiKey"]);
            var movie = client.GetMovie(47964);
            Assert.AreEqual(movie.Id, 47964);
        }
            
        [TestMethod]
        public void TestMovieTitle() {
            var client = new TMDbClient(ConfigurationManager.AppSettings["ApiKey"]);
            var movie = client.GetMovie(47964);
            Assert.AreEqual(movie.Title, "A Good Day to Die Hard");
        }
    }
}
