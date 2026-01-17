using DatabaseTask.Services.DataGrid.DataGridFunctionality.Interfaces;
using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.EventArguments;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations
{
    public class RenameFolderOperation : IRenameFolderOperation
    {
        private readonly ITreeView _treeView;
        private readonly ITreeViewFunctionality _treeViewFunctionality;
        private readonly IDataGridFunctionality _dataGridFunctionality;

        private string? _oldName;

        public RenameFolderOperation(
            ITreeView treeView,
            ITreeViewFunctionality treeViewFunctionality,
            IDataGridFunctionality dataGridFunctionality)
        {
            _treeView = treeView;
            _treeViewFunctionality = treeViewFunctionality;
            _dataGridFunctionality = dataGridFunctionality;
        }

        public async Task RenameFolder(INode node, string newName)
        {
            if (node is NodeViewModel nodeViewModel)
            {
                _oldName = nodeViewModel.Name;
                await RenameFolderImpl(newName, nodeViewModel);
            }
        }

        private async Task RenameFolderImpl(string newName, NodeViewModel node)
        {
            node.Name = newName;
            FileProperties? item = _dataGridFunctionality.GetPropertiesForNode(node);
            if (item != null)
            {
                item.Name = newName;
                await UpdatePlacement(node, item);
            }
        }

        private async Task UpdatePlacement(INode node, FileProperties properties)
        {
            _treeViewFunctionality.RemoveNode(node);
            if (node.Parent != null)
            {
                int index = 0;
                bool isInsert = _treeViewFunctionality.TryInsertNode(node.Parent, node, out index);
                if (isInsert)
                {
                    _dataGridFunctionality.RemoveProperties(node);
                    _dataGridFunctionality.AddProperties(properties);
                    _treeViewFunctionality.UpdateSelectedNodes(node);
                    await Task.Delay(50);
                    _treeView.ScrollChanged?.Invoke(this, new TreeViewEventArgs(node));
                }
            }
        }

        public async Task UndoRenameFolder(INode node)
        {
            if (node is NodeViewModel nodeViewModel && _oldName is not null)
            {
                await RenameFolderImpl(_oldName, nodeViewModel);
            }
        }
    }
}
