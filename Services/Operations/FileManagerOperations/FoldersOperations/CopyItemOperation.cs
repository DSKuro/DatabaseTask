using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.IO;
using System.Linq;

namespace DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations
{
    public class CopyItemOperation : ICopyItemOperation
    {
        private readonly ITreeViewFunctionality _treeViewFunctionality;

        public CopyItemOperation(ITreeViewFunctionality treeViewFunctionality)
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
                UpdatePathRecursive(newNode, target);
                bool isInsert = _treeViewFunctionality.TryInsertNode(target, newNode, out _);
                if (isInsert)
                {
                    RecursiveCopyChildren(copied, newNode);
                    _treeViewFunctionality.AddNodeToSelected(newNode);
                    _treeViewFunctionality.BringIntoView(newNode);
                    newNode.IsOperationHighlighted = true;
                }
            }
        }

        private void RecursiveCopyChildren(INode sourceParent, INode targetParent)
        {
            foreach (var child in sourceParent.Children.Where(x => x.Name != "Loading..."))
            {
                var newChildNode = _treeViewFunctionality.CreateNode(child, targetParent);

                if (newChildNode == null)
                {
                    continue;
                }

                _treeViewFunctionality.TryInsertNode(targetParent, newChildNode, out _);

                if (child is NodeViewModel nodeChild && nodeChild.IsFolder && nodeChild.Children.Any())
                {
                    RecursiveCopyChildren(child, newChildNode);
                }
            }
        }

        private static void UpdatePathRecursive(INode node, INode parent)
        {
            node.FullPath = string.IsNullOrWhiteSpace(parent.FullPath)
                ? null
                : Path.Combine(parent.FullPath, node.Name);

            foreach (INode child in node.Children)
            {
                UpdatePathRecursive(child, node);
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
