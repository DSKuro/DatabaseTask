using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Commands.FilesCommands.Commands
{
    public class RenameFolderCommand : IResultCommand
    {
        private readonly string _oldPath;
        private readonly string _newPath;
        private readonly IFilesOperations _filesOperations;

        private bool _isSuccess;

        public bool IsSuccess => _isSuccess;

        public RenameFolderCommand(string oldPath, string newPath, IFilesOperations filesOperations)
        {
            _oldPath = oldPath;
            _newPath = newPath;
            _filesOperations = filesOperations;
            _isSuccess = false;
        }

        public Task Execute()
        {
            if (_filesOperations.RenameFolder(_oldPath, _newPath))
            {
                _isSuccess = true;
            }
            return Task.CompletedTask;
        }

        public void Undo()
        {

        }
    }
}
