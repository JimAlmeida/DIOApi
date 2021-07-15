using System;

namespace DIOApi.Exceptions
{
    public class NullQueryParameters: Exception
    {
        public NullQueryParameters(string message)
        : base(message)
        {
        }
    }
}
