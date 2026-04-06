using System.IO;

namespace DatabaseTask.Services.TreeViewLogic.TreeViewManager.Interfaces
{
    public interface ITreeViewManagerHelper
    {
        public bool HasFlag(string path, FileAttributes flag);
    }
}
