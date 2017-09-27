using System;
using System.Text;

namespace ArgumentParser
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class OptionAttribute : ArgumentAttribute
    {
        public OptionAttribute()
        {
        }
            
        public OptionAttribute(string longName) : this(default(char), longName)
        {
            if (longName == null)
                throw new ArgumentNullException(nameof(longName));
        }

        public OptionAttribute(char shortName) : this(shortName, default(string))
        {
            if (shortName == default(char))
                throw new ArgumentOutOfRangeException(nameof(shortName));
        }

        public OptionAttribute(char shortName, string longName)
        {
            ShortName = shortName;
            LongName = longName;
        }

        public string LongName { get; }

        public char ShortName { get; }

        public bool Required { get; set; }

        public object DefaultValue { get; set; }
    }
}