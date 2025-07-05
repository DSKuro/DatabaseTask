using Avalonia.Platform.Storage;
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

        public Task GetCollectionFromFolders
            (IEnumerable<IStorageFolder> folders);
    }
}
