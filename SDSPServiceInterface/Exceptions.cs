using System;
namespace SDSPServiceImplementation.Exceptions
{
    public class LoadException : Exception
    {
        public LoadException(string message)
            : base(message)
        {
        }
    }
}
