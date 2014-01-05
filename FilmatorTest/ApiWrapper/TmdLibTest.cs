using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.General;
using System.Diagnostics;
using System.Linq;

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

        [TestMethod]
        public void TestMovieList() {
            var client = new TMDbClient(ConfigurationManager.AppSettings["ApiKey"]);
            var movies = client.GetMovieList(MovieListType.Popular);
            Assert.AreEqual(movies.Results.Count, 20);
        }

        [TestMethod]
        public void TestSearchOneMovie()
        {
            var client = new TMDbClient(ConfigurationManager.AppSettings["ApiKey"]);
            SearchContainer<SearchMovie> result = client.SearchMovie("snatch");

            var movie = client.GetMovie(result.Results.First().Id);
            Assert.AreEqual(movie.Title, "Snatch");
        }


    }
}
