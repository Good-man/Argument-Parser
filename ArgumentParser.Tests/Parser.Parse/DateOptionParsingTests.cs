using System;
using ArgumentParser.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArgumentParser
{
    [TestClass]
    [TestCategory("Parser.Parse.DateTime")]
    public class DateOptionParsingTests : ParserParseBase<DateOptionParsingTests.TerrorAttack>
    {
        [TestInitialize]
        public void TestInit()
        {
            Parser = new Parser<TerrorAttack>();

            Parser.SetupOption(f => f.Flight11)
                .As("flight11");

            Parser.SetupOption(f => f.Flight175)
                .As("flight175")
                .SetDefault(DateTime.Parse("9/11/2001 9:03 am"));

            Parser.SetupValue(o => o.Flight77)
                .As(0);

            Parser.SetupValue(o => o.Flight93)
                .As(1)
                .SetDefault(DateTime.Parse("9/11/2001 10:07 am"));
        }

        [TestMethod]
        public void Parse_DateTimeOptionArgument_ShouldSetDateProperty()
        {
            var args = CommandLine.Split("--flight11 \"9/11/2001 8:40 am\"");
            var result = Parser.Parse(args);
            var options = result.Options;
            Assert.AreEqual(DateTime.Parse("9/11/2001 8:40 am"), options.Flight11);
        }

        [TestMethod]
        public void Parse_DateTimeOptionDefault_ShouldSetDateProperty()
        {
            var args = CommandLine.Split("");
            var result = Parser.Parse(args);
            var options = result.Options;
            Assert.AreEqual(DateTime.Parse("9/11/2001 9:03 am"), options.Flight175);
        }

        [TestMethod]
        public void Parse_DateTimeValueArgument_ShouldSetDateProperty()
        {
            var args = CommandLine.Split("\"9/11/2001 9:37 am\"");
            var result = Parser.Parse(args);
            var options = result.Options;
            Assert.AreEqual(DateTime.Parse("9/11/2001 9:37 am"), options.Flight77);
        }

        [TestMethod]
        public void Parse_DateTimeValueDefault_ShouldSetDateProperty()
        {
            var args = CommandLine.Split("");
            var result = Parser.Parse(args);
            var options = result.Options;
            Assert.AreEqual(DateTime.Parse("9/11/2001 10:07 am"), options.Flight93);
        }

        public class TerrorAttack
        {
            public DateTime Flight11 { get; set; }
            public DateTime Flight175 { get; set; }
            public DateTime Flight77 { get; set; }
            public DateTime Flight93 { get; set; }
        }
    }
}