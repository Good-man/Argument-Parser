using System;
using System.Diagnostics;
using ArgumentParser.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArgumentParser
{
    [TestClass]
    [TestCategory("Exceptions")]
    public class TestInvalidOptionNameException
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOptionNameException))]
        [TestCategory("SyntaxBuilder.ValidateName")]
        public void SyntaxBuilder_InvalidName_ExpectInvalidOptionNameException()
        {
            Debug.WriteLine(Argument.NamePattern);
            var syntaxBuilder = new ArgumentSpecifications<Options>();
            try
            {
                ArgumentSpecification.ValidateName("name@");
            }
            catch (InvalidOptionNameException e)
            {
                Assert.AreEqual("name@", e.Name);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOptionNameException))]
        [TestCategory("SyntaxBuilder.ValidateName")]
        public void Parser_InvalidName_ExpectInvalidOptionNameException()
        {
            var parser = new Parser<Options>();

            try
            {
                parser.SetupOption(o => o.Value).
                    As("name@");
            }
            catch (InvalidOptionNameException e)
            {
                Assert.AreEqual("name@", e.Name);
                throw;
            }
        }

        public class Options
        {
            public string Value { get; set; }
        }
    }
}