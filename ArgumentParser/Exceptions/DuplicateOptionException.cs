using System;

namespace ArgumentParser
{
    public class DuplicateOptionException : Exception
    {
        public object Key { get; }

        public DuplicateOptionException(object key)
        {
            Key = key;
        }
    }
}
