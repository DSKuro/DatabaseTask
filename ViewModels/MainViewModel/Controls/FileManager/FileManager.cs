using Avalonia.Platform.Storage;
using DatabaseTask.Services.DataGrid.DataGridFunctionality.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.Services.TreeViewLogic.TreeViewManager.Interfaces;
using DatabaseTask.ViewModels.Base;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.FileManager.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.Controls.FileManager
{
    public partial class FileManager : ViewModelBase, IFileManager
    {
        private readonly ITreeView _treeView;
        private readonly ITreeViewFunctionality _treeViewFunctionality;
        private readonly ITreeViewManager _treeViewManager;
        private readonly IDataGrid _dataGrid;

        public IDataGrid DataGrid { get => _dataGrid; }
        public ITreeView TreeView { get => _treeView; }
        public ITreeViewFunctionality TreeViewFunctionality { get => _treeViewFunctionality; }

        public FileManager(ITreeView treeView,
            ITreeViewFunctionality treeViewFunctionality,
            ITreeViewManager treeViewManager,
            IDataGrid dataGrid,
            IDataGridFunctionality dataGridFunctionality)
        {
            _treeView = treeView;
            _treeViewFunctionality = treeViewFunctionality;
            _treeViewManager = treeViewManager;
            _dataGrid = dataGrid;
        }

        public async Task GetCollectionFromFolders(IEnumerable<IStorageFolder> folders)
        {
            await _treeViewManager.LoadFoldersAsync(folders);
        }
    }
}
