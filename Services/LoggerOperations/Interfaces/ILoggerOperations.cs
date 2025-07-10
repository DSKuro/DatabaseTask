using DatabaseTask.Services.Collection;

namespace DatabaseTask.Services.LoggerOperations.Interfaces
{
    public interface ILoggerOperations
    {
        public void AddLog(LogCategory category, params string[] parameters);
    }
}
