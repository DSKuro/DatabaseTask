using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;

namespace DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces
{
    public interface IDeleteItemOperation
    {
        public void DeleteItem(INode node);
        public void UndoDeleteItem(INode node);
    }
}
