using System;

namespace ArgumentParser
{
    public abstract class ArgumentAttribute : Attribute
    {
        public string Description { get; set; }
    }
}