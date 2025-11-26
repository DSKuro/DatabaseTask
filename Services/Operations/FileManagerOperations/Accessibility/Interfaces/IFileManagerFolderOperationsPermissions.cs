using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Collections.Generic;

namespace DatabaseTask.Services.Operations.FileManagerOperations.Accessibility.Interfaces
{
    public interface IFileManagerFolderOperationsPermissions
    {
        public void CanDoOperationOnFolder();
        public void CanDeleteFolder();
        public void CanCopyCatalog(List<INode> nodes);
    }
}
