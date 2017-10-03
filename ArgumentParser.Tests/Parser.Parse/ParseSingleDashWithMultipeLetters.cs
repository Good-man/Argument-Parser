using ArgumentParser.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArgumentParser
{
    [TestClass]
    [TestCategory("Parser.Parse")]
    public class ParseSingleDashWithMultipeLetters
    {
        private Parser<Options> _parser;

        private class Options
        {
            public bool OptionA { get; set; }
            public bool OptionB { get; set; }
            public bool OptionC { get; set; }
        }

        [TestInitialize]
        public void TestInit()
        {
            _parser = new Parser<Options>();

            _parser.SetupOption(o => o.OptionA)
                .As('a');
            _parser.SetupOption(o => o.OptionB)
                .As('b');
            _parser.SetupOption(o => o.OptionC)
                .As('c');
        }

        [TestMethod]
        public void Parse_DashABC_SetsOptionAOptionBOptionCtoTrue()
        {
            const string commandLine = "-abc";
            var args = CommandLine.Split(commandLine);
            var result = _parser.Parse(args);
            var options = result.Options;

            Assert.IsTrue(options.OptionA);
            Assert.IsTrue(options.OptionB);
            Assert.IsTrue(options.OptionC);
        }

    }
}