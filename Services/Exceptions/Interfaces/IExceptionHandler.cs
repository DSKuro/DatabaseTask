using System;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Exceptions.Interfaces
{
    public interface IExceptionHandler
    {
        public Task<T> ExecuteWithHandlingAsync<T>(Func<Task<T>> action, T defaultValue = default);
    }
}
