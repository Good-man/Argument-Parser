using System;

namespace ArgumentParser
{
    public class DuplicateKeyException : Exception
    {
        public object Key { get; }

        public DuplicateKeyException(object key)
        {
            Key = key;
        }
    }
}
