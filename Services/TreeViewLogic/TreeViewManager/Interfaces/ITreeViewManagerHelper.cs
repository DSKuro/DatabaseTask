using Avalonia.Platform.Storage;
using System.IO;

namespace DatabaseTask.Services.TreeViewLogic.TreeViewManager.Interfaces
{
    public interface ITreeViewManagerHelper
    {
        public bool HasFlag(IStorageItem item, FileAttributes flag);
    }
}
