using DatabaseTask.Models.DTO;

namespace DatabaseTask.Services.Commands.Base.Interfaces
{
    public interface IResultCommandsFactory
    {
        public IResultCommand CreateCommand(CommandInfo info);
    }
}
