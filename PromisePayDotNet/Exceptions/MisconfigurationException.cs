using System;

namespace PromisePayDotNet.Exceptions
{
    public class MisconfigurationException : Exception
    {
        public MisconfigurationException(string message) : base(message)
        {
        }
    }
}
