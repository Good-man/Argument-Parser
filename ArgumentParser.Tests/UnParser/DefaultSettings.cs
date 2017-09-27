using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArgumentParser
{
    [TestClass]
    [TestCategory("UnParser.FormatCommandLine")]
    public class DefaultSettings : UnParserTestBase
    {
        [TestMethod]
        public void ExpectDefaultCommandLine()
        {
            var unParser = new UnParser<Options>();
            unParser.Configure().Setup("command").WithDescription("The command");
            unParser.Configure().Setup(o => o.StringProperty).As('p').WithDescription("StringProperty");
            unParser.Configure().Setup(o => o.StringField).As("field");
            var commandLine = unParser.FormatCommandLine(new Options { StringField = "myField", StringProperty = "myProperty" });
            Assert.AreEqual("command -p:myProperty --field:myField", commandLine);
        }
    }
}