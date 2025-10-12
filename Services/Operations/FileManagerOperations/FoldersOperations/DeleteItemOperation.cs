using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;

namespace DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations
{
    public class DeleteItemOperation : IDeleteItemOperation
    {
        private readonly ITreeView _treeView;
        private readonly IDataGrid _dataGrid;

        public DeleteItemOperation(ITreeView treeView, IDataGrid dataGrid)
        {
            _treeView = treeView;
            _dataGrid = dataGrid;
        }

        public void DeleteItem(INode node)
        {
            RemoveNode(node);
            RemoveProperties(node);
            _treeView.SelectedNodes.Remove(node);
        }

        private void RemoveNode(INode node)
        {
            if (node.Parent != null)
            {
                node.Parent.Children.Remove(node);
            }
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
