using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Commands.ItemCommands.Commands
{
    public class DeleteItemCommand : ICommand
    {
        private readonly List<INode> _nodes;
        private readonly IDeleteItemOperation _folderOperation;

        public DeleteItemCommand(
            IDeleteItemOperation folderOperation,
            List<INode> nodes)
        {
            _nodes = nodes;
            _folderOperation = folderOperation;
        }

        public Task Execute()
        {
            if (_folderOperation != null)
            {
                _folderOperation.DeleteItem(_nodes);
            }
            return Task.CompletedTask;
        }

        public void Undo()
        {

        }
    }
}
