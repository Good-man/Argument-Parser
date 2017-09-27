using System;

namespace ArgumentParser
{
    public class OptionPrefix
    {
        public string Prefix { get; }
        public string Name { get; }

        private OptionPrefix(string prefix, string name)
        {
            Prefix = prefix;
            Name = name;
        }

        public static OptionPrefix ForwardSlash { get; } = new OptionPrefix("/", "ForwardSlash");
        public static OptionPrefix SingleHyphen { get; } = new OptionPrefix("-", "SingleHyphen");
        public static OptionPrefix DoubleHyphen { get; } = new OptionPrefix("--", "DoubleHyphen");

        public override string ToString()
        {
            return Prefix;
        }

        public bool Equals(string prefix)
        {
            return Prefix == prefix;
        }

        internal static OptionPrefix Parse(string optionPrefix)
        {
            switch (optionPrefix)
            {
                case "-":
                    return SingleHyphen;
                case "--":
                    return DoubleHyphen;
                case "/":
                    return ForwardSlash;
                case "":
                case null:
                    return null;
                default:
                    throw new ArgumentOutOfRangeException(nameof(optionPrefix), optionPrefix,
                        "Prefix not supported.");
            }
        }
    }
}