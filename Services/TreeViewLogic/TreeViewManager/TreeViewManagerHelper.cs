using Avalonia.Platform.Storage;
using DatabaseTask.Services.TreeViewLogic.TreeViewManager.Interfaces;
using System.IO;

namespace DatabaseTask.Services.TreeViewLogic.TreeViewManager
{
    public class TreeViewManagerHelper : ITreeViewManagerHelper
    {
        public bool HasFlag(IStorageItem item, FileAttributes flag)
        {
            try
            {
                string? path = item.TryGetLocalPath();
                if (string.IsNullOrEmpty(path))
                {
                    return false;
                }

                FileAttributes attributes = File.GetAttributes(path);
                return attributes.HasFlag(flag);
            }
            catch
            {
                return false;
            }
        }
    }
}
