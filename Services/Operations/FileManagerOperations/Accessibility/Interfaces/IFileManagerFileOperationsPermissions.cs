using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Collections.Generic;

namespace DatabaseTask.Services.Operations.FileManagerOperations.Accessibility.Interfaces
{
    public interface IFileManagerFileOperationsPermissions
    {
        public void CanDeleteFile();
        public void CanCopyFile(List<INode> nodes);
    }
}
