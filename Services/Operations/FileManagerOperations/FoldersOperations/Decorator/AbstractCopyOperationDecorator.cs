using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;

namespace DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Decorator
{
    public abstract class AbstractCopyOperationDecorator : ICopyItemOperation
    {
        protected ICopyItemOperation _copyOperation;

        public AbstractCopyOperationDecorator(ICopyItemOperation copyOperation)
        {
            _copyOperation = copyOperation;
        }

        public virtual void CopyItem()
        {
            _copyOperation.CopyItem();
        } 
    }
}
