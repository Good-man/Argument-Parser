using ArgumentParser.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArgumentParser
{
    [TestClass]
    [TestCategory("Parser.Parse.Git")]
    public class GitConfigProxy : ParserParseBase<GitConfigProxy.GitConfigOptions>
    {
        [TestInitialize]
        public void TestInit()
        {
            Parser = new Parser<GitConfigOptions>();

            Parser.SetupCommand("config")
                .WithDescription("Get and set repository or global options");

            Parser.SetupValue(arg => arg.FileOptions).As(0)
                .SetDefault(FileOptions.Global)
                .IsRequired()
                .WithDescription("Config file location");

            Parser.SetupOption(option => option.GetTheSetting)
                .As("get")
                .WithDescription("Get the setting's value");

            Parser.SetupValue(arg => arg.SettingName)
                .As(0)
                .IsRequired()
                .WithDescription("The name of the setting");

            Parser.SetupValue(arg => arg.SettingValue).As(1)
                .WithDescription("The value of the setting");
        }

        [TestMethod]
        public void Parse_GitClone_ReturnsNull()
        {
            var args = CommandLine.Split("clone http://gitrepo");
            var options = Parser.Parse(args);
            Assert.IsNull(options);
        }

        [TestMethod]
        public void Parse_GitConfigGlobalProxy_SetsGlobalFileOption()
        {
            var args = CommandLine.Split("config --global http.proxy http://proxyuser:proxypwd@proxy.server.com:8080");
            var result = Parser.Parse(args);
            var options = result.Options;
            Assert.AreEqual(FileOptions.Global, options.FileOptions);
        }

        [TestMethod]
        public void Parse_GitConfigGetProxy_SetsGetAndSettingName()
        {
            var args = CommandLine.Split("config --get http.proxy");
            var result = Parser.Parse(args);
            var options = result.Options;
            Assert.IsTrue(options.GetTheSetting);
            Assert.AreEqual("http.proxy", options.SettingName);
        }

        [TestMethod]
        public void Parse_GitConfigProxy_SetsDefaultFileOption()
        {
            var args = CommandLine.Split("config http.proxy http://proxyuser:proxypwd@proxy.server.com:8080");
            var result = Parser.Parse(args);
            var options = result.Options;
            Assert.AreEqual(FileOptions.Global, options.FileOptions);
        }

        public class GitConfigOptions
        {
            public FileOptions FileOptions = FileOptions.Global;  // Default

            public bool GetTheSetting { get; set; }

            public string SettingName { get; set; }

            public string SettingValue { get; set; }
        }

        public enum FileOptions
        {
            System,
            Global,
            Local
        }
    }
}