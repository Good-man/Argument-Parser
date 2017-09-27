using System.Collections.Generic;
using System.Linq;
using ArgumentParser.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArgumentParser
{
    [TestClass]
    public class SyntaxBuilderTest
    {
        //[TestMethod]
        public void TestMethod1()
        {
            var sb = new SyntaxBuilder<Options1>();
            sb.Setup(o => o.StringProperty).As("string");
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TestMethod2()
        {
            var sb = new SyntaxBuilder<Options2>();
            Assert.Inconclusive();
        }

        public class Options1
        {
            public string StringProperty { get; set; }
            public List<string> EnumerableProperty { get; set; }
            public MyEnum MyEnum { get; set; }
        }

        public class Options2
        {
            [Option("string")]
            public string StringProperty { get; set; }
            [Value(0)]
            public List<string> EnumerableProperty { get; set; }
            [Value(1)]
            public MyEnum MyEnum { get; set; }
        }

        public enum MyEnum
        {
            OptionA,
            OptionB
        }
    }
}
