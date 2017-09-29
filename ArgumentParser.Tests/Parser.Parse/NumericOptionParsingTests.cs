using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable BuiltInTypeReferenceStyle

namespace ArgumentParser
{
    [TestClass]
    [TestCategory("Parser.Parse.Numbers")]
    public class NumericOptionParsingTests
    {
        [TestMethod]
        public void Parse_Int16Option_ShouldSetMinMaxProperty()
        {
            var parser = SetupParser<Int16>();

            var result = parser.Parse(new[] { $"--min:{Int16.MinValue}" });
            var options = result.Options;
            Assert.AreEqual(Int16.MinValue, options.Min);
            
            result = parser.Parse(new[] { $"--max:{Int16.MaxValue}" });
            options = result.Options;
            Assert.AreEqual(Int16.MaxValue, options.Max);
        }

        [TestMethod]
        public void Parse_Int32Option_ShouldSetMinMaxProperty()
        {
            var parser = SetupParser<Int32>();

            var result = parser.Parse(new[] { $"--min:{Int32.MinValue}" });
            var options = result.Options;
            Assert.AreEqual(Int32.MinValue, options.Min);

            result = parser.Parse(new[] { $"--max:{Int32.MaxValue}" });
            options = result.Options;
            Assert.AreEqual(Int32.MaxValue, options.Max);
        }

        [TestMethod]
        public void Parse_Int64Option_ShouldSetMinMaxProperty()
        {
            var parser = SetupParser<Int64>();

            var result = parser.Parse(new[] { $"--min:{Int64.MinValue}" });
            var options = result.Options;
            Assert.AreEqual(Int64.MinValue, options.Min);

            result = parser.Parse(new[] { $"--max:{Int64.MaxValue}" });
            options = result.Options;
            Assert.AreEqual(Int64.MaxValue, options.Max);           
        }

        [TestMethod]
        public void Parse_DecimalOption_ShouldSetMinMaxProperty()
        {
            var parser = SetupParser<Decimal>();

            var result = parser.Parse(new[] { $"--min:{Decimal.MinValue}" });
            var options = result.Options;
            Assert.AreEqual(Decimal.MinValue, options.Min);

            result = parser.Parse(new[] { $"--max:{Decimal.MaxValue}" });
            options = result.Options;
            Assert.AreEqual(Decimal.MaxValue, options.Max);           
        }

        private static Parser<IntegerRange<TValue>> SetupParser<TValue>() where TValue : IComparable
        {
            var parser = new Parser<IntegerRange<TValue>>();

            parser.Setup(o => o.Min)
                .As("min");

            parser.Setup(o => o.Max)
                .As("max");

            return parser;
        }

        public class IntegerRange<TNumber> where TNumber : IComparable
        {
            public TNumber Min { get; internal set; }
            public TNumber Max { get; internal set; }
        }
    }
}