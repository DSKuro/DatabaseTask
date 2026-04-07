using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations
{
    public class RenameFolderOperation : IRenameFolderOperation
    {
        private readonly ITreeViewFunctionality _treeViewFunctionality;

        public RenameFolderOperation(ITreeViewFunctionality treeViewFunctionality)
        {
            _treeViewFunctionality = treeViewFunctionality;
        }

        public async Task RenameFolder(INode node, string newName)
        {
            if (node is NodeViewModel nodeViewModel)
            {
                await RenameFolderImpl(newName, nodeViewModel, true, true);
            }
        }

        private async Task RenameFolderImpl(string newName, NodeViewModel node, bool isScroll, bool isHighlight)
        {
            node.Name = newName;
            RefreshNodePaths(node);
            node.IsOperationHighlighted = isHighlight;
            RefreshNodePosition(node);
            _treeViewFunctionality.UpdateSelectedNodes(node);

            if (isScroll)
            {
                await Task.Delay(10);
                _treeViewFunctionality.BringIntoView(node);
            }
        }

        private void RefreshNodePosition(INode node)
        {
            if (node.Parent is not null)
            {
                var sorted = node.Parent.Children
                    .OfType<NodeViewModel>()
                    .OrderByDescending(x => x.IsFolder)
                    .ThenBy(x => x.Name, StringComparer.OrdinalIgnoreCase)
                    .ToList();

                node.Parent.Children.Clear();
                node.Parent.Children.AddRange(sorted);
            }
        }

        private void RefreshNodePaths(INode node)
        {
            if (node.Parent is not null)
            {
                node.FullPath = string.Empty;
            }

            foreach (INode child in node.Children)
            {
                RefreshNodePaths(child);
            }
        }

        public async Task UndoRenameFolder(INode node, string oldName)
        {
            if (node is NodeViewModel nodeViewModel)
            {
                await RenameFolderImpl(oldName, nodeViewModel, false, false);
            }
        }

        public void CommitRenameFolder(INode node)
        {
            node.IsOperationHighlighted = false;
        }
    }
}
