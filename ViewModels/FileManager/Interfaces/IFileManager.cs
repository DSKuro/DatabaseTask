using Avalonia.Platform.Storage;
using DatabaseTask.Services.FileManagerOperations.Accessibility.Interfaces;
using DatabaseTask.ViewModels.DataGrid.Interfaces;
using DatabaseTask.ViewModels.TreeView.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.FileManager.Interfaces
{
    public interface IFileManager
    {
        public ITreeView TreeView { get; }
        public IDataGrid DataGrid { get; }
        public IFileManagerFolderOperationsPermissions FolderPermissions { get; }
        public IFileManagerFileOperationsPermissions FilePermissions { get; }


        public Task GetCollectionFromFolders
            (IEnumerable<IStorageFolder> folders);
    }
}
