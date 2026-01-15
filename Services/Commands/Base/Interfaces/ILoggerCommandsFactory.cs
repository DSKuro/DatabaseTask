using DatabaseTask.Models.DTO;

namespace DatabaseTask.Services.Commands.Base.Interfaces
{
    public interface ILoggerCommandsFactory
    {
        public ICommand CreateCommand(CommandInfo info, LoggerDTO data);
    }
}
