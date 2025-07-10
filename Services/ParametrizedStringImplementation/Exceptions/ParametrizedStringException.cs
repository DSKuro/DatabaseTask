using System;

namespace DatabaseTask.Services.ParametrizedStringImplementation.Exceptions
{
    public class ParametrizedStringException : Exception
    {
        public ParametrizedStringException(string message)
            :base(message) { }
    }
}
