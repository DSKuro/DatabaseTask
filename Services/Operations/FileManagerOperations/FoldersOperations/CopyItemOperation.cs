using DatabaseTask.Services.DataGrid.DataGridFunctionality.Interfaces;
using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Linq;

namespace DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations
{
    public class CopyItemOperation : ICopyItemOperation
    {
        private readonly IDataGridFunctionality _dataGridFunctionality;
        private readonly ITreeViewFunctionality _treeViewFunctionality;

        public CopyItemOperation(
            IDataGridFunctionality dataGridFunctionality,
            ITreeViewFunctionality treeViewFunctionality)
        {
            _dataGridFunctionality = dataGridFunctionality;
            _treeViewFunctionality = treeViewFunctionality;
        }

        public void CopyItem(INode copied, INode target, string newItemName)
        {
            INode? newNode = _treeViewFunctionality.CreateNode(copied, target);
            if (newNode != null)
            {
                newNode.Name = newItemName;
                bool isInsert = _treeViewFunctionality.TryInsertNode(target, newNode, out _);
                if (isInsert)
                {
                    _dataGridFunctionality.CopyProperties(copied, newNode, target);
                    RecursiveCopyChildren(copied, newNode);
                }
            }
        }

        private void RecursiveCopyChildren(INode sourceParent, INode targetParent)
        {
            foreach (INode child in sourceParent.Children)
            {
                INode? newChildNode = _treeViewFunctionality.CreateNode(child, targetParent);
                if (newChildNode != null)
                {
                    bool isInsert = _treeViewFunctionality.TryInsertNode(targetParent, newChildNode, out _);
                    _dataGridFunctionality.CopyProperties(child, newChildNode, targetParent);
                    if (child.Children.Any())
                    {
                        RecursiveCopyChildren(child, newChildNode);
                    }
                }
            }
        }

        public void UndoCopyItem(INode target, string newItemName)
        {
            var nodeToDelete = target.Children.FirstOrDefault(item => item.Name.Equals(newItemName));

            if (nodeToDelete is not null)
            {
                _treeViewFunctionality.RemoveNode(nodeToDelete);
                _dataGridFunctionality.RemoveProperties(nodeToDelete);
            }
        }
    }
}
