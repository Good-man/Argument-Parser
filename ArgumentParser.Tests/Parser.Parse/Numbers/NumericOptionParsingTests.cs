using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable BuiltInTypeReferenceStyle

namespace ArgumentParser.Numbers
{
    [TestClass]
    [TestCategory("Parser.Parse.Numbers")]
    public class NumericOptionParsingTests
    {
        [TestMethod]
        public void Parse_Int16Option_ShouldSetExpectedValue()
        {
            TestParser(Int16.MinValue.ToString(), Int16.MinValue);
            TestParser(Int16.MaxValue.ToString(), Int16.MaxValue);
        }

        [TestMethod]
        public void Parse_Int32Option_ShouldSetExpectedValue()
        {
            TestParser(Int32.MinValue.ToString(), Int32.MinValue);
            TestParser(Int32.MaxValue.ToString(), Int32.MaxValue);
        }

        [TestMethod]
        public void Parse_Int64Option_ShouldSetExpectedValue()
        {
            TestParser(Int64.MinValue.ToString(), Int64.MinValue);
            TestParser(Int64.MaxValue.ToString(), Int64.MaxValue);
        }

        [TestMethod]
        public void Parse_DecimalOption_ShouldSetExpectedValue()
        {
            TestParser(Decimal.MinValue.ToString(), Decimal.MinValue);
            TestParser(Decimal.MaxValue.ToString(), Decimal.MaxValue);
        }

        [TestMethod]
        public void Parse_DoubleOption_ShouldSetExpectedValue()
        {
            TestParser(Double.MinValue.ToString("G17"), Double.MinValue);
            TestParser(Double.Epsilon.ToString("G17"), Double.Epsilon);
            TestParser(Double.MaxValue.ToString("G17"), Double.MaxValue);
        }

        [TestMethod]
        public void Parse_SingleOption_ShouldSetExpectedValue()
        {
            TestParser(Single.MinValue.ToString("G17"), Single.MinValue);
            TestParser(Single.Epsilon.ToString("G17"), Single.Epsilon);
            TestParser(Single.MaxValue.ToString("G17"), Single.MaxValue);
        }

        public void TestParser<TNum>(string stringValue, TNum expectedValue) where TNum : IComparable
        {
            var parser = SetupParser<TNum>();

            var result = parser.Parse(new[] {$"--value:{stringValue}"});
            var options = result.Options;
            Assert.AreEqual(expectedValue, options.Value);
        }

        private static Parser<NumberOptions<TValue>> SetupParser<TValue>() where TValue : IComparable
        {
            var parser = new Parser<NumberOptions<TValue>>();

            parser.SetupOption(o => o.Value)
                .As("value");

            return parser;
        }
    }
}