using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;

namespace DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations
{
    public class DeleteItemOperation : IDeleteItemOperation
    {
        private readonly ITreeViewFunctionality _treeViewFunctionality;

        public DeleteItemOperation(ITreeViewFunctionality treeViewFunctionality)
        {
            _treeViewFunctionality = treeViewFunctionality;
        }

        public void DeleteItem(INode node, bool isUpdateSelection = true)
        {
            _treeViewFunctionality.RemoveNode(node);
            if (node.Parent is not null && isUpdateSelection)
            {
                node.Parent.IsOperationHighlighted = true;
            }
        }

        public void UndoDeleteItem(INode node)
        {
            if (node.Parent is not null)
            {
                if (_treeViewFunctionality.TryInsertNode(node.Parent, node, out int index))
                {
                    _treeViewFunctionality.AddNodeToSelected(node);
                    node.Parent.IsOperationHighlighted = false;
                }
            }
        }

        public void CommitDeleteItem(INode node)
        {
            if (node.Parent is not null)
            {
                node.Parent.IsOperationHighlighted = false;
            }
        }
    }
}
