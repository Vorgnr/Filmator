using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilmatorTest.Config {
    [TestClass]
    public class ConfigTest {
        [TestMethod]
        public void TestApiKey() {
            string key = ConfigurationManager.AppSettings["ApiKey"];
            Assert.IsInstanceOfType(key, typeof(string));
        }
    }
}
