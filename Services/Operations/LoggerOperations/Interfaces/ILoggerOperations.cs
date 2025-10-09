using DatabaseTask.Models.Categories;

namespace DatabaseTask.Services.Operations.LoggerOperations.Interfaces
{
    public interface ILoggerOperations
    {
        public void AddLog(LogCategory category, params string[] parameters);
    }
}
