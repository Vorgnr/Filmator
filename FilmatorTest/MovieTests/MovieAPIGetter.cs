using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Filmator.Model.Provider;
using System.Linq;

namespace FilmatorTest.MovieTests
{
    [TestClass]
    public class MovieAPIGetter
    {
        private SearchMovieProvider _apiProvider;

        [TestInitialize]
        public void Initiator()
        {
            _apiProvider = new SearchMovieProvider();
        }

    }
}
