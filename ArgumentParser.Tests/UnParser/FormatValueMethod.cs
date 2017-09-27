using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArgumentParser
{
    [TestClass]
    [TestCategory("UnParser.FormatValue")]
    public class FormatValueMethod : UnParserTestBase
    {
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void UnexpectedType()
        {
            UnParser.FormatValue(5, false, null);
        }

        [TestMethod]
        public void FormatDateTimeValue()
        {
            var now = (object)DateTime.Now;
            var str = UnParser.FormatValue(now, false, null);
            Assert.AreEqual(now.ToString(), str);
        }

        [TestMethod]
        public void FormatNullValueWhenNotRequired()
        {
            var str = UnParser.FormatValue(null, false, null);
            Assert.AreEqual(string.Empty, str);
        }

        [TestMethod]
        public void FormatStringValueWithSpaces()
        {
            var str = UnParser.FormatValue("string with spaces", false, null);
            Assert.AreEqual("\"string with spaces\"", str);
        }

        [TestMethod]
        public void FormatNullStringValueWithDefault()
        {
            var str = UnParser.FormatValue(null, true, "null string with default");
            Assert.AreEqual("\"null string with default\"", str);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FormatRequiredArgumentWithoutDefault()
        {
            var str = UnParser.FormatValue(null, true, null);
        }

        [TestMethod]
        public void FormatBooleanValueTrue()
        {
            var boolean = UnParser.FormatValue(true, false, null);
            Assert.AreEqual("true", boolean);
        }
    }
}