using System.Diagnostics;

namespace ConsoleApp.Usage
{
    using ArgumentParser;

    class Options
    {
        public string StringOption { get; set; }
        public bool BooleanOption { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // cmd.exe --option "test"
            args = CommandLine.Split("--option \"test\"");
            var parser = new Parser<Options>();

            parser.SetupOption(o => o.StringOption)
                .As('o', "option")
                .IsRequired();

            parser.SetupOption(o => o.BooleanOption)
                .As('f', "flag")
                .SetDefault(true);

            var result = parser.Parse(args);

            var options = result.Options;
            Debug.Assert(options.StringOption == "test");
            Debug.Assert(options.BooleanOption == true);
        }
    }
}
