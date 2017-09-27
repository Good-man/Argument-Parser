using System;
using System.Globalization;

namespace ArgumentParser
{
    public class UnParserTestBase
    {
        internal class Options
        {
            public string StringField;
            public string StringProperty { get; set; }

            public DateTime DateTimeField;
            public DateTime DateTimeProperty { get; set; }

            public MyEnum EnumField = MyEnum.OptionA;
            public MyEnum EnumProperty { get; set; } = MyEnum.OptionB;


        }

        internal enum MyEnum
        {
            OptionA,
            OptionB
        }
    }
}

