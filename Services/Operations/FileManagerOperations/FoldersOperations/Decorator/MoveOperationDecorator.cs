using DatabaseTask.Models;
using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using System.Linq;

namespace DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Decorator
{
    public class MoveOperationDecorator : AbstractCopyOperationDecorator
    {
        private readonly ITreeView _treeView;
        private readonly IDataGrid _dataGrid;

        public MoveOperationDecorator(
            ICopyItemOperation itemOperation,
            ITreeView treeView,
            IDataGrid dataGrid)
            : base(itemOperation) 
        {
            _treeView = treeView;
            _dataGrid = dataGrid;
        }

        public override void CopyItem()
        {
            base.CopyItem();
            RemoveItem();
        }

        private void RemoveItem()
        {
            INode? node = _treeView.SelectedNodes.FirstOrDefault();
            if (node != null)
            {
                node.Parent?.Children.Remove(node);
                FileProperties? properties = _dataGrid.SavedFilesProperties.Find(x => x.Node == node);
                if (properties != null)
                {
                    _dataGrid.SavedFilesProperties.Remove(properties);
                }
            }
        }
    }
}
