using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Commands.FilesCommands.Commands
{
    public class DeleteFolderCommand : IResultCommand
    {
        private readonly string _path;
        private readonly IFilesOperations _filesOperations;
        private string? _tempBackup;

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
            _tempBackup = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            _filesOperations.CopyFolder(_path, _tempBackup);
            if (_filesOperations.DeleteFolder(_path))
            {
                _isSuccess = true;
            }
            else
            {
                _filesOperations.DeleteFolder(_tempBackup);
            }
            return Task.CompletedTask;
        }

        public void Undo()
        {
            if (!string.IsNullOrEmpty(_tempBackup))
            {
                _filesOperations.CopyFolder(_tempBackup, _path);
                _filesOperations.DeleteFolder(_tempBackup);
            }
        }

        public void Commit()
        {
            if (!string.IsNullOrEmpty(_tempBackup))
            {
                _filesOperations.DeleteFolder(_tempBackup);
            }
        }
    }
}
