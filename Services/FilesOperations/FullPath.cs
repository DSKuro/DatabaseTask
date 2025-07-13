using DatabaseTask.Services.FilesOperations.Interfaces;
using DatabaseTask.ViewModels.Nodes;
using DatabaseTask.ViewModels.TreeView.Interfaces;

namespace DatabaseTask.Services.FilesOperations
{
    public class FullPath : IFullPath
    {
        private readonly ITreeView _treeView;

        public FullPath(ITreeView treeView)
        {
            _treeView = treeView;
        }

        public string PathToCoreFolder { get; set; }

        public string GetFullpath(string pathToItem)
        {
            string fullPath = PathToCoreFolder;
            NodeViewModel node = _treeView.SelectedNodes[0] as NodeViewModel;
            while (node != null)
            {
                fullPath += "/" + node.Name;
                node = node.Parent as NodeViewModel;
            }
            return fullPath;
        }
    }
}
