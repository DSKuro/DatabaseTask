using DatabaseTask.Models;
using DatabaseTask.Services.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.ViewModels.DataGrid.Interfaces;
using DatabaseTask.ViewModels.Nodes;
using DatabaseTask.ViewModels.TreeView.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseTask.Services.FileManagerOperations.FoldersOperations
{
    public class DeleteFolderOperation : IDeleteFolderOperation
    {
        private readonly ITreeView _treeView;
        private readonly IDataGrid _dataGrid;

        public DeleteFolderOperation(ITreeView treeView, IDataGrid dataGrid)
        {
            _treeView = treeView;
            _dataGrid = dataGrid;
        }

        public async Task DeleteFolder()
        {
            INode node = RemoveNode();
            RemoveProperties(node);
            _treeView.SelectedNodes.Clear();
        }

        private INode RemoveNode()
        {
            INode node = _treeView.SelectedNodes.First();
            _treeView.Nodes.Remove(node);
            if (node.Parent != null)
            {
                node.Parent.Children.Remove(node);
            }
            return node;
        }

        private void RemoveProperties(INode node)
        {
            FileProperties? properties = _dataGrid.SavedFilesProperties.Find(x => x.Node == node);
            if (properties != null)
            {
                _dataGrid.SavedFilesProperties.Remove(properties);
            }
        }
    }
}
