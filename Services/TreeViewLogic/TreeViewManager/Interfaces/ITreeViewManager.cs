using Avalonia.Platform.Storage;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Collections.Generic;

namespace DatabaseTask.Services.TreeViewLogic.TreeViewManager.Interfaces
{
    public interface ITreeViewManager
    {
        public void LoadFoldersAsync(IEnumerable<IStorageFolder> folders);
    }
}
