using DatabaseTask.Services.FilesOperations.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;

namespace DatabaseTask.Services.FilesOperations
{
    public class FullPath : IFullPath
    {
        private readonly ITreeView _treeView;

        public string? PathToCoreFolder { get; set; }

        public FullPath(ITreeView treeView)
        {
            _treeView = treeView;
        }

        public string GetFullpath(string pathToItem)
        {
            string fullPath = PathToCoreFolder ?? "";
            NodeViewModel? node = _treeView.SelectedNodes[0] as NodeViewModel;
            while (node != null)
            {
                fullPath += "/" + node.Name;
                node = node.Parent as NodeViewModel;
            }
            return fullPath;
        }
    }
}
