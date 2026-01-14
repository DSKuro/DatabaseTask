using DatabaseTask.Models.DTO;
using DatabaseTask.Services.Commands.Base.Interfaces;

namespace DatabaseTask.Services.Commands.DatabaseCommands.Interfaces
{
    public interface IDatabaseCommandsFactory
    {
        public IResultCommand CreateCommand(CommandInfo info);
    }
}
