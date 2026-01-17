using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;

namespace DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces
{
    public interface ICopyItemOperation
    {
        public void CopyItem(INode copied, INode target, string newItemName);
        public void UndoCopyItem(INode target, string newItemName);
    }
}
