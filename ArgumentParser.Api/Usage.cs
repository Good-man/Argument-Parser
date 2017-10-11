using System;

namespace ArgumentParser.Api
{
    public class Usage
    {
        public void Main()
        {
            var builder = new SyntaxBuilder();

            builder.Add<string>("option")
                .SetDefault("default value")
                .WithDescription("Option description");

            builder.Add<MyEnum>("optiona")
                .SetDefault(MyEnum.OptionA)
                .WithDescription("OptionA description");

            builder.Add<int>("num").SetDefault(3).WithDescription("number description");
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