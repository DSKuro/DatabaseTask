using Avalonia.Platform.Storage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseTask.Services.TreeViewLogic.TreeViewManager.Interfaces
{
    public interface ITreeViewManager
    {
        public Task LoadFoldersAsync(IEnumerable<IStorageFolder> folders);
    }
}
