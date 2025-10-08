using Avalonia.Controls;

namespace DatabaseTask.Services.TreeViewItemLogic.Interfaces
{
    public interface ITreeViewInitializer
    {
        public void Initialize(TreeView treeView, Window window);
    }
}
