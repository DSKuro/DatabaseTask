using DatabaseTask.Services.TreeViewLogic.TreeViewManager.Interfaces;
using System.IO;

namespace DatabaseTask.Services.TreeViewLogic.TreeViewManager
{
    public class TreeViewManagerHelper : ITreeViewManagerHelper
    {
        public bool HasFlag(string path, FileAttributes flag)
        {
            try
            {
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
