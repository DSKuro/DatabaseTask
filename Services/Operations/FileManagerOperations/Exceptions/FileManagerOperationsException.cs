using System;

namespace DatabaseTask.Services.Operations.FileManagerOperations.Exceptions
{
    public class FileManagerOperationsException : Exception
    {
        public FileManagerOperationsException(string message)
            :base(message) { }
    }
}
