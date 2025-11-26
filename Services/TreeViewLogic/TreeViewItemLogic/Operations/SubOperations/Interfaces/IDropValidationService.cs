using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;

namespace DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic.Operations.SubOperations.Interfaces
{
    public interface IDropValidationService
    {
        public bool CanDrop(INode target);
    }
}
