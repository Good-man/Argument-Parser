using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArgumentParser
{
    [TestClass]
    [TestCategory("UnParser.FormatCommandLine")]
    public class AssignmentOperatorTestCase : UnParserTestBase
    {
        [TestMethod]
        public void WithColonAssignment_ExpectColonAssignment()
        {
            var unParser = new UnParser<Options>(setup => { setup.AssignmentOperator = AssignmentOperator.Colon; });
            unParser.Configure().SetupOption(o => o.StringProperty).As('p');
            unParser.Configure().SetupOption(o => o.StringField).As('f');
            var commandLine = unParser.FormatCommandLine(new Options { StringField = "myField", StringProperty = "myProperty" });
            Assert.AreEqual("-p:myProperty -f:myField", commandLine);
        }
        [TestMethod]
        public void WithEqualAssignment_ExpectEqaulAssignment()
        {
            var unParser = new UnParser<Options>(setup => { setup.AssignmentOperator = AssignmentOperator.Equal; });
            unParser.Configure().SetupOption(o => o.StringProperty).As('p');
            unParser.Configure().SetupOption(o => o.StringField).As('f');
            var commandLine = unParser.FormatCommandLine(new Options { StringField = "myField", StringProperty = "myProperty" });
            Assert.AreEqual("-p=myProperty -f=myField", commandLine);
        }

        [TestMethod]
        public void WithSpaceAssignment_ExpectSpace()
        {
            var unParser = new UnParser<Options>(setup => { setup.AssignmentOperator = AssignmentOperator.Space; });
            unParser.Configure().SetupOption(o => o.StringProperty).As('p');
            unParser.Configure().SetupOption(o => o.StringField).As('f');
            var commandLine = unParser.FormatCommandLine(new Options { StringField = "myField", StringProperty = "myProperty" });
            Assert.AreEqual("-p myProperty -f myField", commandLine);
        }

    }
}