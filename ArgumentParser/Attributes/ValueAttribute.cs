using System;

namespace ArgumentParser
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ValueAttribute : ArgumentAttribute
    {
        public ValueAttribute()
        {
        }

        public ValueAttribute(int index)
        {
            Index = index;
        }

        public int Index { get; }

        public bool Required { get; set; }

        public object DefaultValue { get; set; }
    }
}