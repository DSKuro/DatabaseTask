using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Commands.FilesCommands.Commands
{
    public class DeleteFolderCommand : IResultCommand
    {
        private readonly string _path;
        private readonly IFilesOperations _filesOperations;

        private bool _isSuccess;

        public bool IsSuccess => _isSuccess;

        public DeleteFolderCommand(string path, IFilesOperations filesOperations)
        {
            _path = path;
            _filesOperations = filesOperations;
            _isSuccess = false;
        }

        public Task Execute()
        {
            if (_filesOperations.DeleteFolder(_path))
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
