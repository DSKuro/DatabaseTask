using Avalonia.Platform.Storage;
using DatabaseTask.Services.FileManagerOperations.Accessibility.Interfaces;
using DatabaseTask.ViewModels.DataGrid.Interfaces;
using DatabaseTask.ViewModels.TreeView.Interfaces;
using System.Collections.Generic;
using System.Security;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.FileManager.Interfaces
{
    public interface IFileManager
    {
        public ITreeView TreeView { get; }
        public IDataGrid DataGrid { get; }
        public IFileManagerOperationsPermissions Permissions { get; }

        public Task GetCollectionFromFolders
            (IEnumerable<IStorageFolder> folders);
    }
}
