using DatabaseTask.Models.DTO;
using DatabaseTask.Services.Commands.Info;

namespace DatabaseTask.Services.Commands.Interfaces
{
    public interface ICommandsFactory
    {
        public ICommand CreateCommand(CommandInfo info, LoggerDTO data);
    }
}
