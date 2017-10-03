using ArgumentParser.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArgumentParser
{
    [TestClass]
    [TestCategory("CommandLine.Parse")]
    public class ParseSingleDashWithMultipeLetters2
    {
        [TestMethod]
        public void Parse_DashABC_ReturnsThreeArguments()
        {
            var commandLine = CommandLine.Parse("-abc");
            Assert.AreEqual(3, commandLine.Count);
            var first = commandLine.First;
            Assert.AreEqual("a", first.Value.Name);
            var second = first.Next;
            Assert.AreEqual("b", second.Value.Name);
            var third = second.Next;
            Assert.AreEqual("c", third.Value.Name);
        }
    }
}