using System;

namespace ArgumentParser.Api
{
    public class Usage
    {
        public void Main()
        {
            var builder = new SyntaxBuilder();

            builder.Add("option")
                .SetDefault("default value")
                .WithDescription("Option description");

            builder.Add("optiona")
                .SetDefault(MyEnum.OptionA)
                .WithDescription("OptionA description");

            builder.Add("num").SetDefault(3).WithDescription("number description");
        }
    }

    public class Options
    {
        public string Option1 { get; set; }
        public MyEnum MyEnum { get; set; }
    }

    public enum MyEnum
    {
        OptionA,
        OptionB
    }
}