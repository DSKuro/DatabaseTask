using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Commands.ItemCommands.Commands
{
    public class DeleteItemCommand : ICommand
    {
        private readonly IDeleteItemOperation _folderOperation;

        public DeleteItemCommand(IDeleteItemOperation folderOperation)
        {
            _folderOperation = folderOperation;
        }

        public Task Execute()
        {
            if (_folderOperation != null)
            {
                _folderOperation.DeleteItem();
            }
            return Task.CompletedTask;
        }

        public void Undo()
        {

        }
    }
}
