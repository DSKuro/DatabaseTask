using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.Utility.Info;

namespace DatabaseTask.Services.Commands.FilesCommands.Interfaces
{
    public interface IFileCommandsFactory
    {
        public ICommand CreateCommand(CommandInfo info);
    }
}
