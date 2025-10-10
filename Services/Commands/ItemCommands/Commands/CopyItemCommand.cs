using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Commands.ItemCommands.Commands
{
    public class CopyItemCommand : ICommand
    {
        private readonly ICopyItemOperation _folderOperation;

        public CopyItemCommand(ICopyItemOperation folderOperation)
        {
            _folderOperation = folderOperation;
        }

        public Task Execute()
        {
            if (_folderOperation != null)
            {
                _folderOperation.CopyItem();
            }
            return Task.CompletedTask;
        }

        public void Undo()
        {


        }
    }
}
