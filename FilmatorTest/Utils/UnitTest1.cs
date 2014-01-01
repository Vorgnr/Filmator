using Filmator.Model.Enums;
using Filmator.Model.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilmatorTest.Utils {
    [TestClass]
    public class UtilsTest {
        [TestMethod]
        public void EnumByDescriptionMethodTest() {
            Assert.IsTrue(SearchState.Popular == EnumsHelper.GetEnumByDescription<SearchState>("Popular"));
        }

        [TestMethod]
        public void DescriptionByEnumethodTest() {
            Assert.IsTrue("Popular" == EnumsHelper.GetDescriptionByEnum(SearchState.Popular));
        }

        [TestMethod]
        public void EucliTest() {
            Assert.AreEqual(2, 44 / 20);
        }
    }
}
