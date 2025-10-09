using DatabaseTask.Models;

namespace DatabaseTask.ViewModels.Logger.Interfaces
{
    public interface ILogger
    {
        public SmartCollection<LogData> LogOperations { get; set; }
    }
}
