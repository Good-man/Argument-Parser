using System;

namespace ArgumentParser
{
    internal class OptionNameTooShortException : Exception
    {
        public OptionNameTooShortException(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}