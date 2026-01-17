using DatabaseTask.Models.DTO;
using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Operations.LoggerOperations.Interfaces;
using System.Threading.Tasks;

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

        public Task Execute()
        {
            if (_loggerOperations != null)
            {
                _loggerOperations.AddLog(_data);
            }
            return Task.CompletedTask;
        }

        public void Undo()
        {
            if ( _loggerOperations is not null)
            {
                _loggerOperations.RemoveLog();
            }
        }
    }
}
