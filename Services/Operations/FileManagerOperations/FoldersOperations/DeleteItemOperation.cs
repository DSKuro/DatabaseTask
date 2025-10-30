using DatabaseTask.Services.DataGrid.DataGridFunctionality.Interfaces;
using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;

namespace DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations
{
    public class DeleteItemOperation : IDeleteItemOperation
    {
        private readonly ITreeViewFunctionality _treeViewFunctionality;
        private readonly IDataGridFunctionality _dataGridFunctionality;

        public DeleteItemOperation(ITreeViewFunctionality treeViewFunctionality,
            IDataGridFunctionality dataGridFunctionality)
        {
            _treeViewFunctionality = treeViewFunctionality;
            _dataGridFunctionality = dataGridFunctionality;
        }

        public void DeleteItem(INode node)
        {
            _treeViewFunctionality.RemoveNode(node);
            _dataGridFunctionality.RemoveProperties(node);
            _treeViewFunctionality.RemoveSelectedNodes(node);
        }
    }
}
