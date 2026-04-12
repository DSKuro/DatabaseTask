using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Linq;

namespace DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations
{
    public class CopyItemOperation : ICopyItemOperation
    {
        private readonly ITreeViewFunctionality _treeViewFunctionality;

        public CopyItemOperation(
            ITreeViewFunctionality treeViewFunctionality)
        {
            _treeViewFunctionality = treeViewFunctionality;
        }

        public void CopyItem(INode copied, INode target, string newItemName)
        {
            INode? newNode = _treeViewFunctionality.CreateNode(copied, target);
            if (newNode != null)
            {
                target.IsExpanded = true;
                newNode.Name = newItemName;
                newNode.FullPath = string.Empty;
                bool isInsert = _treeViewFunctionality.TryInsertNode(target, newNode, out _);
                if (isInsert)
                {
                    _treeViewFunctionality.RecursiveCopyChildren(copied, newNode);
                    _treeViewFunctionality.UpdateSelectedNodes(newNode);
                    _treeViewFunctionality.BringIntoView(newNode);
                    newNode.IsOperationHighlighted = true;
                }
            }
        }

        public void UndoCopyItem(INode copied, INode target, string newItemName)
        {
            var nodeToDelete = target.Children.FirstOrDefault(item => item.Name.Equals(newItemName));

            if (nodeToDelete is not null)
            {
                _treeViewFunctionality.RemoveNode(nodeToDelete);
                _treeViewFunctionality.UpdateSelectedNodes(target);
                nodeToDelete.IsOperationHighlighted = false;
            }
        }

        public void CommitCopyItem(INode copied, INode target, string newItemName)
        {
            var nodeToDelete = target.Children.FirstOrDefault(item => item.Name.Equals(newItemName));

            if (nodeToDelete is not null)
            {
                nodeToDelete.IsOperationHighlighted = false;
            }
        }
    }
}
