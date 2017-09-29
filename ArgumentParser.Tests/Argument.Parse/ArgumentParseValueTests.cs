using System;
using ArgumentParser.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable BuiltInTypeReferenceStyle

namespace ArgumentParser
{
    [TestClass]
    [TestCategory("Argument.ParseValue")]
    public class ArgumentParseValueTests
    {
        private readonly TestData[] _testData =
        {
            new TestData("-d 9/11/2001", typeof(DateTime), DateTime.Parse("9/11/2001")),
            new TestData("-d 8:40 am", typeof(DateTime), DateTime.Parse("8:40 am")),
            new TestData("-d 9/11/2001 8:40 am", typeof(DateTime), DateTime.Parse("9/11/2001 8:40 am")),
            new TestData("-b true", typeof(bool), true),
            new TestData("-b false", typeof(bool), false),
            new TestData("-b True", typeof(bool), true),
            new TestData("-b False", typeof(bool), false),
            new TestData("-b", typeof(bool), true),
            new TestData("-i -32768", typeof(Int16), Int16.MinValue),
            new TestData("-i 32767", typeof(Int16), Int16.MaxValue),
            new TestData("--optiona", typeof(MyEnum), MyEnum.OptionA),
            new TestData("--optionb", typeof(MyEnum), MyEnum.OptionB)
        };

        private enum MyEnum
        {
            OptionA,
            OptionB
        }
        [TestMethod]
        public void ParseValue_Arg_ReturnsExpectedValue()
        {
            foreach (var arg in _testData)
            {
                var argument = Argument.Parse(arg.Text);
                var value = argument.ParseValue(arg.Type);
                Assert.IsInstanceOfType(value, arg.Type);
                Assert.AreEqual(arg.ExpectedValue, value);
            }
        }

        private class TestData
        {
            public TestData(string text, Type type, object value)
            {
                Text = text;
                Type = type;
                ExpectedValue = value;
            }

            public string Text { get; }
            public Type Type { get; }
            public object ExpectedValue { get; }
        }
    }
}