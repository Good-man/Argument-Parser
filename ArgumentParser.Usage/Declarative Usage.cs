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
    }

    class Program
    {
        static void Main(string[] args)
        {
            if (args == null)
                // cmd.exe --option "test"
                args = CommandLine.Split("--option \"test\"");

            var parser = new Parser<Options>();

            var result = parser.Parse(args);

            var options = result.Options;
            Debug.Assert(options.StringOption == "test");
            Debug.Assert(options.BooleanOption == true);
        }
    }
}
