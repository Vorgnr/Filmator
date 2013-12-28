using System.Reflection;
using Filmator.Model.Enums;
using Filmator.Model.Provider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Filmator.Model.Manager;

namespace FilmatorTest.Manager {
    [TestClass]
    public class SearchTest {
        [TestMethod]
        public void SearchBestMethod() {
            var sm = new SearchManager();
            Assert.IsNotNull(sm.GetSearchByState(SearchState.Popular));
        }
    }
}
