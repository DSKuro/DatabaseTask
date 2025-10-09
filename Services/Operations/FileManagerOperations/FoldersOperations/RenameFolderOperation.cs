using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using System.Linq;

namespace DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations
{
    public class RenameFolderOperation : IRenameFolderOperation
    {
        private readonly ITreeView _treeView;
        private readonly IDataGrid _dataGrid;

        public RenameFolderOperation(ITreeView treeView,
            IDataGrid dataGrid)
        {
            _treeView = treeView;
            _dataGrid = dataGrid;
        }

        public void RenameFolder(string newName)
        {
            if (_treeView.SelectedNodes[0] is NodeViewModel node)
            {
                RenameFolderImpl(newName, node);
            }
        }

        private void RenameFolderImpl(string newName, NodeViewModel node)
        {
            node.Name = newName;
            FileProperties? item = _dataGrid.SavedFilesProperties.FirstOrDefault(x => x.Node == node);
            if (item != null)
            {
                item.Name = newName;
            }
        }
    }
}
