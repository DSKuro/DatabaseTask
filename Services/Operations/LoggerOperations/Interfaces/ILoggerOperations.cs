using DatabaseTask.Models.DTO;

namespace DatabaseTask.Services.Operations.LoggerOperations.Interfaces
{
    public interface ILoggerOperations
    {
        public void AddLog(LoggerDTO dto);
    }
}
