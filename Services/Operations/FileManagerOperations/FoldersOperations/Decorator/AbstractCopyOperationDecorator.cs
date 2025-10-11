using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;

namespace DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Decorator
{
    public abstract class AbstractCopyOperationDecorator : ICopyItemOperation
    {
        protected ICopyItemOperation _copyOperation;

        public AbstractCopyOperationDecorator(ICopyItemOperation copyOperation)
        {
            _copyOperation = copyOperation;
        }

        public virtual void CopyItem(INode copied,INode target)
        {
            _copyOperation.CopyItem(copied, target);
        } 
    }
}
