using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArgumentParser
{
    [TestClass]
    [TestCategory("UnParser.FormatCommandLine")]
    public class OptionPrefixTestCase : UnParserTestBase
    {
        [TestMethod]
        public void WithDoubleHyphen_ReturnsUnParsedCommandLine()
        {
            var unParser = new UnParser<Options>(settings => { settings.OptionPrefix = OptionPrefix.DoubleHyphen; });
            unParser.Configure().SetupOption(o => o.StringProperty).As("property");
            unParser.Configure().SetupOption(o => o.StringField).As("field");
            var commandLine = unParser.FormatCommandLine(new Options { StringField = "myField", StringProperty = "myProperty" });
            Assert.AreEqual("--property:myProperty --field:myField", commandLine);
        }

        [TestMethod]
        public void WithSpaceAssignment_ReturnsUnParsedCommandLine()
        {
            var unParser = new UnParser<Options>(settings => { settings.OptionPrefix = OptionPrefix.ForwardSlash; });
            unParser.Configure().SetupOption(o => o.StringProperty).As('p');
            unParser.Configure().SetupOption(o => o.StringField).As('f');
            var commandLine = unParser.FormatCommandLine(new Options { StringField = "myField", StringProperty = "myProperty" });
            Assert.AreEqual("/p:myProperty /f:myField", commandLine);
        }
    }
}