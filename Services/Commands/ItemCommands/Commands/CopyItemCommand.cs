using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;

namespace DatabaseTask.Services.Commands.ItemCommands.Commands
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
