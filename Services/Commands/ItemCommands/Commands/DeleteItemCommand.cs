using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Commands.ItemCommands.Commands
{
    public class DeleteItemCommand : ICommand
    {
        private readonly INode _node;
        private readonly IDeleteItemOperation _folderOperation;

        public DeleteItemCommand(
            IDeleteItemOperation folderOperation,
            INode node)
        {
            _node = node;
            _folderOperation = folderOperation;
        }

        public Task Execute()
        {
            if (_folderOperation != null)
            {
                _folderOperation.DeleteItem(_node);
            }
            return Task.CompletedTask;
        }

        public void Undo()
        {
            if (_folderOperation is not null)
            {
                _folderOperation.UndoDeleteItem(_node);
            }
        }
    }
}
