using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Filmator.Model.Provider;
using System.Linq;

namespace FilmatorTest.MovieTests
{
    [TestClass]
    public class MovieAPIGetter
    {
        private MovieRemoteProvider _apiProvider;

        [TestInitialize]
        public void Initiator()
        {
            _apiProvider = new MovieRemoteProvider();
        }

        [TestMethod]
        public void TestGetMovieFromId()
        {
            var movie = _apiProvider.GetById(107);
            Assert.AreEqual(movie.OriginalTitle, "Snatch");
        }

        [TestMethod]
        public void TestSearchMoviesFromName()
        {
            var movie = _apiProvider.GetById(107);
            var movies = _apiProvider.SearchByName("snatch");
            Assert.AreEqual(movies.IndexOf(movie), -1);
        }

        [TestMethod]
        public void TestGetMostPopularMovies()
        {
            var movies = _apiProvider.GetPopular(1);
            Assert.AreEqual(movies.Count, 20);
        }

        [TestMethod]
        public void TestGetNowPlayingMovies()
        {
            var movies = _apiProvider.GetNowPlaying(1);
            Assert.AreEqual(movies.Count, 20);
        }

        [TestMethod]
        public void TestGetTopRatedMovies()
        {
            var movies = _apiProvider.GetTopRated(1);
            Assert.AreEqual(movies.Count, 20);
        }
    }
}
