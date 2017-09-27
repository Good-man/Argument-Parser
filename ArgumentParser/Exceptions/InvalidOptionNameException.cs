using System;

namespace ArgumentParser
{
    internal class InvalidOptionNameException : Exception
    {
        public InvalidOptionNameException(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}