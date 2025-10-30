using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.DataGridFunctionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.EventArguments;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Functionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations
{
    public class RenameFolderOperation : IRenameFolderOperation
    {
        private readonly ITreeView _treeView;
        private readonly ITreeViewFunctionality _treeViewFunctionality;
        private readonly IDataGridFunctionality _dataGridFunctionality;

        public RenameFolderOperation(
            ITreeView treeView,
            ITreeViewFunctionality treeViewFunctionality,
            IDataGridFunctionality dataGridFunctionality)
        {
            _treeView = treeView;
            _treeViewFunctionality = treeViewFunctionality;
            _dataGridFunctionality = dataGridFunctionality;
        }

        public async Task RenameFolder(string newName)
        {
            if (_treeViewFunctionality.GetFirstSelectedNode() is NodeViewModel node)
            {
                await RenameFolderImpl(newName, node);
            }
        }

        private async Task RenameFolderImpl(string newName, NodeViewModel node)
        {
            node.Name = newName;
            FileProperties? item = _dataGridFunctionality.GetPropertiesForNode(node);
            if (item != null)
            {
                item.Name = newName;
                await UpdatePlacement(node);
            }
        }

        private async Task UpdatePlacement(INode node)
        {
            _treeViewFunctionality.RemoveNode(node);
            if (node.Parent != null)
            {
                bool isInsert = _treeViewFunctionality.TryInsertNode(node.Parent, node, out _);
                if (isInsert)
                {
                    _treeViewFunctionality.UpdateSelectedNodes(node);
                    await Task.Delay(50);
                    _treeView.ScrollChanged?.Invoke(this, new TreeViewEventArgs(node));
                }
            }
        }
    }
}
