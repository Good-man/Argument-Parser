using ArgumentParser.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArgumentParser
{
    [TestClass]
    [TestCategory("Parser.Parse")]
    public class TestParseOptionAssignmentValue
    {
        private Parser<Options> _parser;

        private class Options
        {
            public string SomeOption { get; set; }
        }

        [TestInitialize]
        public void TestInit()
        {
            _parser = new Parser<Options>();
            _parser.Setup("command");

            _parser.Setup(o => o.SomeOption)
                .As("option");
        }

        [TestMethod]
        public void Parse_OptionWithColonAssignment()
        {
            const string commandLine = "command --option:\"some value that goes with option\"";
            var args = CommandLine.Split(commandLine);
            var result = _parser.Parse(args);
            var options = result.Options;

            Assert.AreEqual("some value that goes with option", options.SomeOption);
        }

        [TestMethod]
        public void Parse_OptionWithEqualsAssignment()
        {
            const string commandLine = "command --option=\"some value that goes with option\"";
            var args = CommandLine.Split(commandLine);
            var result = _parser.Parse(args);
            var options = result.Options;

            Assert.AreEqual("some value that goes with option", options.SomeOption);
        }

        [TestMethod]
        public void Parse_OptionWithSpaceAssignment()
        {
            const string commandLine = "command --option \"some value that goes with option\"";
            var args = CommandLine.Split(commandLine);
            var result = _parser.Parse(args);
            var options = result.Options;

            Assert.AreEqual("some value that goes with option", options.SomeOption);
        }

    }
}