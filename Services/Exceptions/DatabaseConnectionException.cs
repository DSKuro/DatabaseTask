using System;

namespace DatabaseTask.Services.Exceptions
{
    public class DatabaseUnavailableException : Exception
    {
        public DatabaseUnavailableException(string message) : base(message) { }
    }
}
