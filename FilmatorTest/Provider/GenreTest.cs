using Filmator.Model.Manager;
using Filmator.Model.Provider;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilmatorTest.Provider {
    [TestClass]
    public class GenreTest {
        [TestMethod]
        public void GenreGetAllTest() {
            var genreProvider = new GenreProvider();
            Assert.IsNotNull(genreProvider.GetAll());
        }

        [TestMethod]
        public void GenreGetSearchContainerByGenreId() {
            var movieManager = new SearchMovieProvider();
            var searchCon = movieManager.GetByGenreId(14, 1);
        }
    }
}
