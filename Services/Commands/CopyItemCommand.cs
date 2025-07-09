using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.FileManagerOperations.FoldersOperations.Interfaces;

namespace DatabaseTask.Services.Commands
{
    public class CopyItemCommand : ICommand
    {
        private readonly ICopyItemOperation _folderOperation;

        public CopyItemCommand(ICopyItemOperation folderOperation)
        {
            _folderOperation = folderOperation;
        }

        public void Execute()
        {
            if (_folderOperation != null)
            {
                _folderOperation.CopyItem();
            }
        }

        public void Undo()
        {


        }
    }
}
