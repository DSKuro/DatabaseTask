using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
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
            await _treeViewFunctionality.EnsureTreeLoadedRecursive(node);
            node.Name = newName;
            _treeViewFunctionality.UpdatePathRecursive(node, string.Empty);
            node.IsOperationHighlighted = isHighlight;
            _treeViewFunctionality.UpdateSelectedNodes(node);

            if (isScroll)
            {
                await Task.Delay(10);
                _treeViewFunctionality.BringIntoView(node);
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
