using DatabaseTask.Services.Commands.Info;

namespace DatabaseTask.Services.Commands.Interfaces
{
    public interface IItemCommandsFactory
    {
        public ICommand CreateCommand(CommandInfo info);
    }
}
