using System;

namespace ArgumentParser
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class CommandAttribute : ArgumentAttribute
    {
        public CommandAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}