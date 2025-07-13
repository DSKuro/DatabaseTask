using DatabaseTask.Services.Commands.Info;

namespace DatabaseTask.Services.Commands.Interfaces
{
    public interface IFileCommandsFactory
    {
        public ICommand CreateCommand(CommandInfo info);
    }
}
