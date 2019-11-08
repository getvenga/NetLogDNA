using System;

namespace NetLogDNA.Exceptions
{
    [Serializable]
    public class InvalidLogDnaConfigException : Exception
    {
        internal InvalidLogDnaConfigException(string message) : base(message) { }
    }
}