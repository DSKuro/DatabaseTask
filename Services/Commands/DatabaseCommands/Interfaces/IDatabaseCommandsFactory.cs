using DatabaseTask.Models.DTO;

namespace DatabaseTask.Services.Commands.DatabaseCommands.Interfaces
{
    public interface IDatabaseCommandsFactory
    {
        public IDatabaseCommand CreateCommand(CommandInfo info);
    }
}
