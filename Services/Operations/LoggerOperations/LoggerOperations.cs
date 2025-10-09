using DatabaseTask.Models;
using DatabaseTask.Models.Categories;
using DatabaseTask.Services.Operations.LoggerOperations.Interfaces;
using DatabaseTask.ViewModels.Logger.Interfaces;
using System;

namespace DatabaseTask.Services.Operations.LoggerOperations
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
            _logger.LogOperations.Add(new LogData
            (
                category.Value.GetStringWithParams(parameters),
                TimeToString()
            ));
        }

        private string TimeToString()
        {
            return DateTime.Now.ToString("HH:mm") ?? "";
        }
    }
}
