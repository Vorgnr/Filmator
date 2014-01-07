//using Filmator.Model.Entities;
using Filmator.Model.Provider;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilmatorTest.Provider {
    [TestClass]
    public class MovieStoredProviderTest {
        [TestMethod]
        public void TestCreate() {
            //var movie = new MovieStored {
            //    Title = "prout",
            //    RemoteID = 25
            //};
            var movieProvider = new MovieStoredProvider();
            //Assert.AreEqual(movie, movieProvider.Create(movie));
        }

        [TestMethod]
        public void TestGetAll() {
            var movieProvider = new MovieStoredProvider();
            //var movies = movieProvider.GetAll(1);
            //Assert.IsNotNull(movies);
        }

        [TestMethod]
        public void TestGetByRemoteId() {
            var movieProvider = new MovieStoredProvider();
            //var movie = movieProvider.GetByRemoteId(25);
            //Assert.IsNotNull(movie);
            //Assert.AreEqual(25, movie.RemoteID);

        }

        [TestMethod]
        public void TestUpdate() {
            var movieProvider = new MovieStoredProvider();
            //var movie = movieProvider.GetByRemoteId(25);
            //Assert.IsNotNull(movie);
            const string newTitle = "Okeyyy";
            //movie.Title = newTitle;
            //Assert.AreEqual(newTitle, movieProvider.Update(movie).Title);

        }
    }
}
