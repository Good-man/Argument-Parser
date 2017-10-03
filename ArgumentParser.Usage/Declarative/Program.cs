using System.Diagnostics;

namespace ConsoleApp.Usage.Declarative
{
    using ArgumentParser;

    class Options
    {
        [Option('o', "option", Required = true)]
        public string StringOption { get; set; }

        [Option('f', "flag", DefaultValue = true)]
        public bool BooleanOption { get; set; }

        [Value(0, DefaultValue = "test value")]
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

            var result = parser.Parse(args);

            var options = result.Options;
            Debug.Assert(options.StringOption == "test option");
            Debug.Assert(options.BooleanOption == true);
            Debug.Assert(options.StringValue == "test value");
        }
    }
}
