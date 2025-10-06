using DatabaseTask.Models.DTO;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.LoggerOperations.Interfaces;
using System;

namespace DatabaseTask.Services.Commands.LogCommands
{
    public class LogAddCommand : ICommand
    {
        private ILoggerOperations _loggerOperations;
        private LoggerDTO _data;

        public LogAddCommand(ILoggerOperations loggerOperations,
            LoggerDTO data)
        {
            _loggerOperations = loggerOperations;
            _data = data;
        }

        public void Execute()
        {
            if (_loggerOperations != null)
            {
                _loggerOperations.AddLog(_data.LogCategory,
                    Array.ConvertAll(_data.Parameters, x => x?.ToString() ?? string.Empty));
            }
        }

        public void Undo()
        {
        }
    }
}
