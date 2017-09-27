using ArgumentParser.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArgumentParser
{
    [TestClass]
    [TestCategory("Argument.Parse")]
    public class CopyFileCommand
    {
        [TestMethod]
        public void Parse_CopyFilenameToFolderWithForce()
        {
            const string commandLine = "copy readme.txt c:\\temp -f -abc- /S";
            var args = CommandLine.Split(commandLine);
            Assert.AreEqual(6, args.Length);
            // copy
            var copyCommand = Argument.Parse(args[0]);
            Assert.AreEqual(null, copyCommand.OptionPrefix);
            Assert.AreEqual(null, copyCommand.Name);
            Assert.AreEqual(null, copyCommand.OptionSuffix);
            Assert.AreEqual(null, copyCommand.AssignmentOperator);
            Assert.AreEqual("copy", copyCommand.Value);
            // readme.txt
            var readmetxt = Argument.Parse(args[1]);
            Assert.AreEqual(null, readmetxt.OptionPrefix);
            Assert.AreEqual(null, readmetxt.Name);
            Assert.AreEqual(null, readmetxt.OptionSuffix);
            Assert.AreEqual(null, readmetxt.AssignmentOperator);
            Assert.AreEqual("readme.txt", readmetxt.Value);
            // c:\temp
            var cTemp = Argument.Parse(args[2]);
            Assert.AreEqual(null, cTemp.OptionPrefix);
            Assert.AreEqual(null, cTemp.Name);
            Assert.AreEqual(null, cTemp.OptionSuffix);
            Assert.AreEqual(null, cTemp.AssignmentOperator);
            Assert.AreEqual("c:\\temp", cTemp.Value);
            // -f
            var dashF = Argument.Parse(args[3]);
            Assert.AreEqual(OptionPrefix.SingleHyphen, dashF.OptionPrefix);
            Assert.AreEqual("f", dashF.Name);
            Assert.AreEqual(null, dashF.OptionSuffix);
            Assert.AreEqual(null, dashF.AssignmentOperator);
            Assert.AreEqual(null, dashF.Value);            
            // -abc-
            var dashAbcDash = Argument.Parse(args[4]);
            Assert.AreEqual(OptionPrefix.SingleHyphen, dashAbcDash.OptionPrefix);
            Assert.AreEqual("abc", dashAbcDash.Name);
            Assert.AreEqual(OptionSuffix.Minus, dashAbcDash.OptionSuffix);
            Assert.AreEqual(null, dashAbcDash.AssignmentOperator);
            Assert.AreEqual(null, dashAbcDash.Value);            
            // /S
            var slashS = Argument.Parse(args[5]);
            Assert.AreEqual(OptionPrefix.ForwardSlash, slashS.OptionPrefix);
            Assert.AreEqual("S", slashS.Name);
            Assert.AreEqual(null, slashS.OptionSuffix);
            Assert.AreEqual(null, slashS.AssignmentOperator);
            Assert.AreEqual(null, slashS.Value);
        }
    }
}