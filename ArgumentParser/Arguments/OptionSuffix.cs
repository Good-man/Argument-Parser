using System;

namespace ArgumentParser
{
    public class OptionSuffix
    {
        public string Name { get; }
        public string Character { get; }

        private OptionSuffix(string character, string name)
        {
            Character = character;
            Name = name;
        }

        public static OptionSuffix Plus { get; } = new OptionSuffix("+", "Plus");
        public static OptionSuffix Minus { get; } = new OptionSuffix("-", "Minus");

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(string character)
        {
            return Character == character;
        }

        internal static OptionSuffix Parse(string toggleCharacter)
        {
            switch (toggleCharacter)
            {
                case "+":
                    return Plus;
                case "-":
                    return Minus;
                case "":
                case null:
                    return null;
                default:
                    throw new ArgumentOutOfRangeException(nameof(toggleCharacter), toggleCharacter,
                        "Toggle character not supported.");
            }
        }
    }
}