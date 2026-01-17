using DatabaseTask.Models.Categories;
using DatabaseTask.Models.DTO;
using DatabaseTask.Services.Operations.LoggerOperations.Interfaces;
using DatabaseTask.ViewModels.Logger;
using DatabaseTask.ViewModels.Logger.Interfaces;
using System;
using System.Collections.Generic;

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
            if (dto.Parameters is not null)
            {
                _logger.LogOperations.Add(new LogData
                (
                    dto.LogCategory.Value.GetStringWithParams(dto.Parameters),
                    TimeToString()
                ));
            }
        }

        private string TimeToString()
        {
            return DateTime.Now.ToString("HH:mm") ?? "";
        }

        public void UpdateStatus(List<bool> results)
        {
            var logs = _logger.LogOperations;
            for (int i = 0; i < results.Count; i++)
            {
                string path = results[i] == true ? StatusCategory.CorrectStatus.Path :
                    StatusCategory.WrongStatus.Path;
                logs[i].ImagePath = path;
            }
        }

        public void RemoveLog()
        {
            _logger.LogOperations.RemoveAt(0);
        }

        public void ClearAll()
        {
            _logger.LogOperations.Clear();
        }
    }
}
