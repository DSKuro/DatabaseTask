using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Collections.Generic;

namespace DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Decorator
{
    public class MoveOperationDecorator : AbstractCopyOperationDecorator
    {
        private readonly IDeleteItemOperation _deleteItemOperation;

        public MoveOperationDecorator(
            ICopyItemOperation itemOperation,
            IDeleteItemOperation deleteItemOperation)
            : base(itemOperation) 
        {
            _deleteItemOperation = deleteItemOperation;
        }

        public override void CopyItem(INode copied, INode target)
        {
            base.CopyItem(copied, target);
            _deleteItemOperation.DeleteItem(new List<INode> { copied });
        }
    }
}
