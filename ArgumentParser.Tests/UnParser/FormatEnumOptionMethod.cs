using System;
using ArgumentParser.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArgumentParser
{
    [TestClass]
    [TestCategory("UnParser.FormatEnumOption")]
    public class FormatEnumMethod : UnParserTestBase
    {
        [TestMethod]
        public void FormatEnumWithMatchingLongName()
        {
            var unParser = new UnParser();
            var argumentSpecification = new OptionSpecification<MyEnum>(null) { LongName = "OpTiOnA" };
            var enumOptionString = unParser.FormatEnum(argumentSpecification, MyEnum.OptionA);
            Assert.AreEqual("--OpTiOnA", enumOptionString);
        }

        [TestMethod]
        public void FormatEnumWithoutMatchingLongName()
        {
            var unParser = new UnParser();
            var argumentSpecification = new OptionSpecification<MyEnum>(null);
            var enumOptionString = unParser.FormatEnum(argumentSpecification, MyEnum.OptionA);
            Assert.AreEqual("OptionA", enumOptionString);
        }
    }
}