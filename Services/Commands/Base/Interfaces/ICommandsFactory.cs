using DatabaseTask.Models.DTO;
using DatabaseTask.Services.Commands.Utility.Info;

namespace DatabaseTask.Services.Commands.Base.Interfaces
{
    public interface ICommandsFactory
    {
        public ICommand CreateCommand(CommandInfo info, LoggerDTO data);
    }
}
