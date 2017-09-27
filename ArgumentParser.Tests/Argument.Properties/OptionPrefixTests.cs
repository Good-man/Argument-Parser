using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArgumentParser
{
    [TestClass]
    [TestCategory("Argument.OptionPrefix")]
    public class OptionPrefixTests
    {
        [TestMethod]
        public void EqualsDoubleHyphen()
        {
            var doubleHyphen = OptionPrefix.DoubleHyphen;
            Assert.IsTrue(doubleHyphen.Equals("--"));
            Assert.IsTrue(doubleHyphen.Name.Equals("DoubleHyphen"));
        }

        [TestMethod]
        public void EqualsSingleHyphen()
        {
            var singleHyphen = OptionPrefix.SingleHyphen;
            Assert.IsTrue(singleHyphen.Equals("-"));
            Assert.IsTrue(singleHyphen.Name.Equals("SingleHyphen"));
        }

        [TestMethod]
        public void EqualsForwardSlash()
        {
            var forwardSlash = OptionPrefix.ForwardSlash;
            Assert.IsTrue(forwardSlash.Equals("/"));
            Assert.IsTrue(forwardSlash.Name.Equals("ForwardSlash"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ParseInvalidPrefix()
        {
            OptionPrefix.Parse("x");
        }
    }
}