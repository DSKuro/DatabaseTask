using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Commands.FilesCommands.Commands
{
    public class RenameFolderCommand : ICommand
    {
        private readonly string _oldPath;
        private readonly string _newPath;
        private readonly IFilesOperations _filesOperations;

        public RenameFolderCommand(string oldPath, string newPath, IFilesOperations filesOperations)
        {
            _oldPath = oldPath;
            _newPath = newPath;
            _filesOperations = filesOperations;
        }

        public Task Execute()
        {
            _filesOperations.RenameFolder(_oldPath, _newPath);
            return Task.CompletedTask;
        }

        public void Undo()
        {

        }
    }
}
