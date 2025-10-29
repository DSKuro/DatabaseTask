using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.DataGridFunctionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Functionality.Interfaces;

namespace DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations
{
    public class RenameFolderOperation : IRenameFolderOperation
    {
        private readonly ITreeViewFunctionality _treeViewFunctionality;
        private readonly IDataGridFunctionality _dataGridFunctionality;

        public RenameFolderOperation(
            ITreeViewFunctionality treeViewFunctionality,
            IDataGridFunctionality dataGridFunctionality)
        {
            _treeViewFunctionality = treeViewFunctionality;
            _dataGridFunctionality = dataGridFunctionality;
        }

        public void RenameFolder(string newName)
        {
            if (_treeViewFunctionality.GetFirstSelectedNode() is NodeViewModel node)
            {
                RenameFolderImpl(newName, node);
            }
        }

        private void RenameFolderImpl(string newName, NodeViewModel node)
        {
            node.Name = newName;
            FileProperties? item = _dataGridFunctionality.GetPropertiesForNode(node);
            if (item != null)
            {
                item.Name = newName;
            }
        }
    }
}
