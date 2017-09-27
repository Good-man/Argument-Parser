using System;
using System.IO;

namespace ArgumentParser
{
    public class HelpSettings
    {
        public HelpSettings()
        {
            HelpWriter = Console.Out;
            HelpStyle = HelpStyle.GnuStyle;
        }

        public TextWriter HelpWriter { get; set; }

        public HelpStyle HelpStyle { get; set; }

        public static HelpSettings Default => new HelpSettings();
        public static HelpSettings Current { get; } = Default;
    }
}