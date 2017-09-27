using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArgumentParser
{
    [TestClass]
    [TestCategory("Argument.OptionSuffix")]
    public class OptionSuffixTests
    {
        [TestMethod]
        public void EqualsPlusn()
        {
            var doubleHyphen = OptionSuffix.Plus;
            Assert.IsTrue(doubleHyphen.Equals("+"));
            Assert.IsTrue(doubleHyphen.Name.Equals("Plus"));
        }

        [TestMethod]
        public void EqualsMinus()
        {
            var singleHyphen = OptionSuffix.Minus;
            Assert.IsTrue(singleHyphen.Equals("-"));
            Assert.IsTrue(singleHyphen.Name.Equals("Minus"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ParseInvalidPrefix()
        {
            OptionSuffix.Parse("x");
        }
    }
}