using Avalonia.Platform.Storage;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.Controls.FileManager.Interfaces
{
    public interface IFileManager
    {
        public IDataGrid DataGrid { get; }
        public ITreeView TreeView { get; }
        public ITreeViewFunctionality TreeViewFunctionality { get; }

        public Task GetCollectionFromFolders
            (IEnumerable<IStorageFolder> folders);
    }
}
