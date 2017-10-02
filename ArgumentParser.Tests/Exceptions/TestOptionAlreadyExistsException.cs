using System;
using System.Diagnostics;
using ArgumentParser.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArgumentParser
{
    [TestClass]
    [TestCategory("Exceptions")]
    public class TestOptionAlreadyExistsException
    {
        [TestMethod]
        [ExpectedException(typeof(OptionAlreadyExistsException))]
        [TestCategory("DuplicateName")]
        public void SyntaxBuilder_DuplicateName_ExpectOptionAlreadyExistsException()
        {
            var syntaxBuilder = new ArgumentSpecifications<Options>();
            syntaxBuilder.SetupOption(o => o.Value1).As('o');
            try
            {
                syntaxBuilder.SetupOption(o => o.Value2).As('o');
            }
            catch (OptionAlreadyExistsException e)
            {
                Assert.AreEqual('o', e.Key);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(OptionAlreadyExistsException))]
        [TestCategory("DuplicateName")]
        public void Parser_DuplicateName_ExpectOptionAlreadyExistsException()
        {
            var parser = new Parser<Options>();
            parser.SetupOption(o => o.Value1)
                .As('o');
            try
            {
                // add duplicate option named 'o'
                parser.SetupOption(o => o.Value2)
                    .As('o');
            }
            catch (OptionAlreadyExistsException e)
            {
                Assert.AreEqual('o', e.Key);
                throw;
            }
        }

        public class Options
        {
            public string Value1 { get; private set; }
            public string Value2 { get; private set; }
        }
    }
}