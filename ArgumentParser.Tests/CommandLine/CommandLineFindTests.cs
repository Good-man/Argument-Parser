using ArgumentParser.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArgumentParser
{
    [TestClass]
    [TestCategory("CommandLine.Find")]
    public class CommandLineFindTests
    {
        /// <summary>
        ///     clone https://good-man.visualstudio.com/DefaultCollection/_git/ArgumentParser ArgumentParser
        /// </summary>
        [TestMethod]
        public void Find_CloneCommand_ReturnsCommandArgument()
        {
            var commandLine = CommandLine.Parse("clone https://good-man.visualstudio.com/DefaultCollection/_git/ArgumentParser ArgumentParser");
            var argument = commandLine.FindFirst(new CommandSpecification("clone"));
            Assert.AreEqual("clone", argument.Text);
        }

        /// <summary>
        ///     argument1 "argument 2" "the third argument"
        /// </summary>
        [TestMethod]
        public void Find_SecondValueArgument_ReturnsValueArgument()
        {
            var commandLine = CommandLine.Parse(@"argument1 ""argument 2"" ""the third argument""");
            var argument = commandLine.FindFirst(new OptionSpecification<string>(null) { Index = 1 });
            Assert.AreEqual("argument 2", argument.Value);
        }
    }
}