using DatabaseTask.Models;
using DatabaseTask.Services.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseTask.Services.FileManagerOperations.FoldersOperations
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

        public async Task DeleteItem()
        {
            RemoveNodes();
            _treeView.SelectedNodes.Clear();
        }

        private void RemoveNodes()
        {
            List<INode> selectedNodes = _treeView.SelectedNodes.ToList();
            foreach (INode node in selectedNodes)
            {
                RemoveNode(node);
                RemoveProperties(node);
            }
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
