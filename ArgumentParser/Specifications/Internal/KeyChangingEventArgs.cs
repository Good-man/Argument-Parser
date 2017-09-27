using System;
using System.ComponentModel;

namespace ArgumentParser.Internal
{
    internal class KeyChangingEventArgs : CancelEventArgs
    {
        public object Key { get; }
        public Exception Exception { get; set; }

        public KeyChangingEventArgs(object key)
        {
            Key = key;
        }
    }
}
