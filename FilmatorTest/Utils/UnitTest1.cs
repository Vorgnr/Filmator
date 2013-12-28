using Filmator.Model.Enums;
using Filmator.Model.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilmatorTest.Utils {
    [TestClass]
    public class UtilsTest {
        [TestMethod]
        public void EnumByDescriptionMethod() {
            Assert.IsTrue(SearchState.Popular == EnumsHelper.GetEnumByDescription<SearchState>("Popular"));
        }

        [TestMethod]
        public void DescriptionByEnumethod() {
            Assert.IsTrue("Popular" == EnumsHelper.GetDescriptionByEnum(SearchState.Popular));
        }
    }
}
