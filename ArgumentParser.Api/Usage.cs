using System;

namespace ArgumentParser.Api
{
    public class Usage
    {
        public void Main()
        {
            var builder = new SyntaxBuilder();

            builder.AddCommand("command")
                .WithDescription("Subcommand");

            builder.AddOption("option")
                .SetDefault("default value")
                .WithDescription("Option description");

            builder.AddOption("optiona")
                .SetDefault(MyEnum.OptionA)
                .WithDescription("OptionA description");

            builder.AddOption("num").SetDefault(3).WithDescription("number description");

            builder.AddValue().WithDescription("Some value without a name");
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