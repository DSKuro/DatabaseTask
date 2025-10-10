using DatabaseTask.Models.DTO;
using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Operations.LoggerOperations.Interfaces;

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
                _loggerOperations.AddLog(_data);
            }
        }

        public void Undo()
        {
        }
    }
}
