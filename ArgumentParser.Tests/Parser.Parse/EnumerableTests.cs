using System;
using System.Collections.Generic;
using System.Linq;
using ArgumentParser.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArgumentParser
{
    [TestClass]
    public class EnumerableTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var args = CommandLine.Split(@"""one"" ""two"" ""three""");
            var parser = new Parser<Options>();
            var results = parser.Parse(args);
            var strings = results.Options.Strings;
            Assert.AreEqual(3, strings.Count());
        }

        class Options
        {
            [Option]
            public IEnumerable<string> Strings { get; set; }
        }
    }
}
