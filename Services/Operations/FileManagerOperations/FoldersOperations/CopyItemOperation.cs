using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.SubFunctionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations
{
    public class CopyItemOperation : ICopyItemOperation
    {
        private readonly ITreeViewFunctionality _treeViewFunctionality;
        private readonly ITreeViewNodeService _treeViewNodeService;

        public CopyItemOperation(
            ITreeViewFunctionality treeViewFunctionality,
            ITreeViewNodeService treeViewNodeService)
        {
            _treeViewFunctionality = treeViewFunctionality;
            _treeViewNodeService = treeViewNodeService;
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
                    RecursiveCopyChildren(copied, newNode);
                    _treeViewFunctionality.UpdateSelectedNodes(newNode);
                    _treeViewFunctionality.BringIntoView(newNode);
                    newNode.IsOperationHighlighted = true;
                }
            }
        }

        private void RecursiveCopyChildren(INode sourceParent, INode targetParent)
        {
            foreach (INode child in GetSourceChildren(sourceParent))
            {
                var newChildNode = _treeViewFunctionality.CreateNode(child, targetParent);

                if (newChildNode == null)
                {
                    continue;
                }

                newChildNode.FullPath = string.Empty;

                _treeViewFunctionality.TryInsertNode(targetParent, newChildNode, out _);

                if (child is NodeViewModel nodeChild && nodeChild.IsFolder)
                {
                    RecursiveCopyChildren(child, newChildNode);
                }
            }
        }

        private IEnumerable<INode> GetSourceChildren(INode sourceParent)
        {
            return _treeViewNodeService
                .GetChildNodesAsync(sourceParent)
                .GetAwaiter()
                .GetResult()
                .Where(x => x.Name != "Loading...");
        }

        private void UpdatePathRecursive(INode node, INode parent)
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
