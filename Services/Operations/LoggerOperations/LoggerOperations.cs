using DatabaseTask.Models.DTO;
using DatabaseTask.Services.Operations.LoggerOperations.Interfaces;
using DatabaseTask.ViewModels.Logger;
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

        public void AddLog(LoggerDTO dto)
        {
            _logger.LogOperations.Add(new LogData
            (
                dto.LogCategory.Value.GetStringWithParams(dto.Parameters),
                TimeToString()
            ));
        }

        private string TimeToString()
        {
            return DateTime.Now.ToString("HH:mm") ?? "";
        }
    }
}
