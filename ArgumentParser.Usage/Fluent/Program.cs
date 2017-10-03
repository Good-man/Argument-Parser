using System.Diagnostics;
using ArgumentParser;

namespace ConsoleApp.Usage.Fluent
{
    class Options
    {
        public string StringOption { get; set; }
        public bool BooleanOption { get; set; }
        public string StringValue { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
                // cmd.exe --option "test option"
                args = CommandLineUtil.Split("--option \"test option\"");

            var parser = new Parser<Options>();

            parser.SetupOption(o => o.StringOption)
                .As('o', "option")
                .IsRequired();

            parser.SetupOption(o => o.BooleanOption)
                .As('f', "flag")
                .SetDefault(true);

            parser.SetupValue(v => v.StringValue)
                .As(0)
                .SetDefault("test value");

            var result = parser.Parse(args);

            var options = result.Options;
            Debug.Assert(options.StringOption == "test option");
            Debug.Assert(options.BooleanOption == true);
            Debug.Assert(options.StringValue == "test value");
        }
    }
}
