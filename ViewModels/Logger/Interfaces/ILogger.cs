using DatabaseTask.Models;
using DatabaseTask.Services._serviceCollection;

namespace DatabaseTask.ViewModels.Logger.Interfaces
{
    public interface ILogger
    {
        public SmartCollection<LogData> LogOperations { get; set; }
    }
}
