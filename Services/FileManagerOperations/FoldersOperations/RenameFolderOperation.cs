using DatabaseTask.Models;
using DatabaseTask.Services.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.ViewModels.DataGrid.Interfaces;
using DatabaseTask.ViewModels.Nodes;
using DatabaseTask.ViewModels.TreeView.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseTask.Services.FileManagerOperations.FoldersOperations
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

        public async Task RenameFolder(string newName)
        {
            if (_treeView.SelectedNodes[0] is NodeViewModel node)
            {
                RenameFolderImpl(newName, node);
            }
        }

        private void RenameFolderImpl(string newName, NodeViewModel node)
        {
            node.Name = newName;
            FileProperties item = _dataGrid.SavedFilesProperties.FirstOrDefault(x => x.Node == node);
            if (item != null)
            {
                item.Name = newName;
            }
        }
    }
}
