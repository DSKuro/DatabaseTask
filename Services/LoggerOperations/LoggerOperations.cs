using DatabaseTask.Models;
using DatabaseTask.Services.Collection;
using DatabaseTask.Services.LoggerOperations.Interfaces;
using DatabaseTask.ViewModels.Logger.Interfaces;
using System;

namespace DatabaseTask.Services.LoggerOperations
{
    public class LoggerOperations : ILoggerOperations
    {
        private readonly ILogger _logger;

        public LoggerOperations(ILogger logger)
        {
            _logger = logger;
        }

        public void AddLog(LogCategory category, params string[] parameters)
        {
            _logger.LogOperations.Add(new LogData()
            {
                Operation = category.Value.GetStringWithParams(parameters),
                Time = TimeToString()
            });
        }

        private string TimeToString()
        {
            return DateTime.Now.ToString("HH:mm") ?? "";
        }
    }
}
