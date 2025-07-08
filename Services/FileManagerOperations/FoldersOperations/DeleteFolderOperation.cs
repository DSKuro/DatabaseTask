using DatabaseTask.Models;
using DatabaseTask.Services.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.ViewModels.DataGrid.Interfaces;
using DatabaseTask.ViewModels.Nodes;
using DatabaseTask.ViewModels.TreeView.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

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
