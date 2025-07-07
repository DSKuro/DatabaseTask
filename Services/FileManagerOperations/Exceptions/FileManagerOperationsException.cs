using System;

namespace DatabaseTask.Services.FileManagerOperations.Exceptions
{
    public class FileManagerOperationsException : Exception
    {
        public FileManagerOperationsException(string message)
            :base(message) { }
    }
}
