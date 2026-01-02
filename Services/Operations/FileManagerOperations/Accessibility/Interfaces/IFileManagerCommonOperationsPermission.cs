using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Collections.Generic;

namespace DatabaseTask.Services.Operations.FileManagerOperations.Accessibility.Interfaces
{
    public interface IFileManagerCommonOperationsPermission
    {
        public void CanDeleteItems(List<INode> nodes);
        public void CanMoveItems(List<INode> nodes);
    }
}
