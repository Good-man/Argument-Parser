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
        [ExpectedException(typeof(DuplicateKeyException))]
        [TestCategory("DuplicateKey")]
        public void SyntaxBuilder_DuplicateName_ThrowsException()
        {
            var syntaxBuilder = new ArgumentSpecifications<Options>();
            syntaxBuilder.SetupOption(o => o.Value1).As('o');
            syntaxBuilder.SetupOption(o => o.Value2).As('o');
            try
            {
                syntaxBuilder.Validate();
            }
            catch (DuplicateKeyException e)
            {
                Assert.AreEqual('o', e.Key);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateKeyException))]
        [TestCategory("DuplicateKey")]
        public void Parser_DuplicateName_ThrowsException()
        {
            var parser = new Parser<Options>();
            parser.SetupOption(o => o.Value1).As('o');
            parser.SetupOption(o => o.Value2).As('o');
            try
            {
                parser.Validate();
            }
            catch (DuplicateKeyException e)
            {
                Assert.AreEqual('o', e.Key);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateKeyException))]
        [TestCategory("DuplicateKey")]
        public void SyntaxBuilder_DuplicateIndex_ThrowsException()
        {
            var syntaxBuilder = new ArgumentSpecifications<Options>();
            syntaxBuilder.SetupValue(o => o.Value1).As(0);
            syntaxBuilder.SetupValue(o => o.Value2).As(0);
            try
            {
                syntaxBuilder.Validate();
            }
            catch (DuplicateKeyException e)
            {
                Assert.AreEqual(0, e.Key);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateKeyException))]
        [TestCategory("DuplicateKey")]
        public void Parser_DuplicateIndex_ThrowsException()
        {
            var parser = new Parser<Options>();
            parser.SetupValue(o => o.Value1).As(0);
            parser.SetupValue(o => o.Value2).As(0);
            try
            {
                parser.Validate();
            }
            catch (DuplicateKeyException e)
            {
                Assert.AreEqual(0, e.Key);
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