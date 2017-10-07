using System;
using System.Collections.Generic;
using System.Linq;
using ArgumentParser.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArgumentParser.Enumerables
{
    [TestClass]
    [TestCategory("Parser.Parse.Enumerables")]
    public class BasicEnumerableTests
    {
        [TestMethod]
        public void EnumerableOfStringsTest()
        {
            var args = CommandLine.Split(@"""one"" ""two"" ""three""");
            var parser = new Parser<Options>();
            var results = parser.Parse(args);
            var strings = results.Options.Strings;
            Assert.AreEqual(3, strings.Count());
            Assert.AreEqual("one", strings[0]);
            Assert.AreEqual("two", strings[1]);
            Assert.AreEqual("three", strings[2]);
        }

        class Options
        {
            [Value(0)]
            public IList<string> Strings { get; set; }
        }
    }
}
