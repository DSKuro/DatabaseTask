using DatabaseTask.Models.DTO;

namespace DatabaseTask.Services.Commands.Base.Interfaces
{
    public interface ICommandsFactory
    {
        public ICommand CreateCommand(CommandInfo info, LoggerDTO data);
    }
}
