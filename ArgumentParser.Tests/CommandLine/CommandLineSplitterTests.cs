using ArgumentParser.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArgumentParser
{
    [TestClass]
    [TestCategory("CommandLine.Split")]
    public class CommandLineSplitterTests 
    {
        /// <summary>
        ///     clone https://good-man.visualstudio.com/DefaultCollection/_git/ArgumentParser ArgumentParser
        /// </summary>
        [TestMethod]
        public void Split_CommandLine_ReturnsArgsArray()
        {
            const string commandLine = "clone https://good-man.visualstudio.com/DefaultCollection/_git/ArgumentParser ArgumentParser";
            var args = CommandLine.Split(commandLine);
            Assert.AreEqual("clone", args[0]);
            Assert.AreEqual("https://good-man.visualstudio.com/DefaultCollection/_git/ArgumentParser", args[1]);
            Assert.AreEqual("ArgumentParser", args[2]);
        }

        /// <summary>
        ///     argument1 "argument 2" "the third argument"
        /// </summary>
        [TestMethod]
        public void Split_ArgumentsWithQuotesAndSpaces_ReturnsArgsArray()
        {
            const string commandLine = @"argument1 ""argument 2"" ""the third argument""";
            var args = CommandLine.Split(commandLine);
            Assert.AreEqual("argument1", args[0]);
            Assert.AreEqual("argument 2", args[1]);
            Assert.AreEqual("the third argument", args[2]);
        }
    }
}
