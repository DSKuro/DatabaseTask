using DatabaseTask.Models;
using DatabaseTask.Services._serviceCollection;
using DatabaseTask.ViewModels.Logger.Interfaces;

namespace DatabaseTask.ViewModels.Logger
{
    public class Logger : ILogger
    {
        public SmartCollection<LogData> LogOperations 
        { get ; set ; } = new SmartCollection<LogData>();
    }
}
