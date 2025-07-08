using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.FileManagerOperations.FoldersOperations.Interfaces;

namespace DatabaseTask.Services.Commands
{
    internal class CopyFolderCommand : ICommand
    {
        private readonly bool _isCopy;
        private readonly ICopyFolderOperation _folderOperation;

        public CopyFolderCommand(ICopyFolderOperation folderOperation, bool isCopy)
        {
            _folderOperation = folderOperation;
            _isCopy = isCopy;
        }

        public void Execute()
        {
            if (_folderOperation != null)
            {
                _folderOperation.CopyFolder(_isCopy);
            }
        }

        public void Undo()
        {


        }
    }
}
