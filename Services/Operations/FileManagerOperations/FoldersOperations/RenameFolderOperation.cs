using DatabaseTask.Services.DataGrid.DataGridFunctionality.Interfaces;
using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations
{
    public class RenameFolderOperation : IRenameFolderOperation
    {
        private readonly ITreeViewFunctionality _treeViewFunctionality;
        private readonly IDataGridFunctionality _dataGridFunctionality;

        private string? _oldName;

        public RenameFolderOperation(
            ITreeViewFunctionality treeViewFunctionality,
            IDataGridFunctionality dataGridFunctionality)
        {
            _treeViewFunctionality = treeViewFunctionality;
            _dataGridFunctionality = dataGridFunctionality;
        }

        public async Task RenameFolder(INode node, string newName)
        {
            if (node is NodeViewModel nodeViewModel)
            {
                _oldName = nodeViewModel.Name;
                await RenameFolderImpl(newName, nodeViewModel, true);
            }
        }

        private async Task RenameFolderImpl(string newName, NodeViewModel node, bool isScroll)
        {
            node.Name = newName;
            FileProperties? item = _dataGridFunctionality.GetPropertiesForNode(node);
            if (item != null)
            {
                item.Name = newName;
                await UpdatePlacement(node, item, true);
            }
        }

        private async Task UpdatePlacement(INode node, FileProperties properties, bool isScroll)
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
                    if (isScroll)
                    {
                        _treeViewFunctionality.UpdateSelectedNodes(node);
                        await Task.Delay(50);
                        _treeViewFunctionality.BringIntoView(node);
                    }
                }
            }
        }

        public async Task UndoRenameFolder(INode node)
        {
            if (node is NodeViewModel nodeViewModel && _oldName is not null)
            {
                await RenameFolderImpl(_oldName, nodeViewModel, false);
            }
        }
    }
}
