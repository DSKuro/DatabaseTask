using DatabaseTask.Services.Exceptions.Interfaces;
using System;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Exceptions
{
    public class ExceptionHandler : IExceptionHandler
    {
        public async Task<T> ExecuteWithHandlingAsync<T>(Func<Task<T>> action, T defaultValue = default)
        {
            try
            {
                return await action();
            }
            catch (UnauthorizedAccessException ex)
            {
                return defaultValue;
            }
            catch (Exception ex)
            {
                return defaultValue;
            }
        }
    }
}
