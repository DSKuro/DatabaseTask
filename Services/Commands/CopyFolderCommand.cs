using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.FileManagerOperations.FoldersOperations.Interfaces;

namespace DatabaseTask.Services.Commands
{
    internal class CopyFolderCommand : ICommand
    {
        private readonly ICopyFolderOperation _folderOperation;

        public CopyFolderCommand(ICopyFolderOperation folderOperation)
        {
            _folderOperation = folderOperation;
        }

        public void Execute()
        {
            if (_folderOperation != null)
            {
                _folderOperation.CopyFolder();
            }
        }

        public void Undo()
        {


        }
    }
}
