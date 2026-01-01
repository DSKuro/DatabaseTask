using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Collections.Generic;

namespace DatabaseTask.Services.Operations.FileManagerOperations.Accessibility.Interfaces
{
    public interface IFileManagerFileOperationsPermissions
    {
        public void CanDeleteFile(List<INode> nodes);
        public void CanCopyFile(List<INode> nodes);
    }
}
