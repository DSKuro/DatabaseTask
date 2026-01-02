using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;

namespace DatabaseTask.Services.Operations.FilesOperations
{
    public class FullPath : IFullPath
    {
        private readonly ITreeViewFunctionality _treeViewFunctionality;

        public string? PathToCoreFolder { get; set; }

        public FullPath(ITreeViewFunctionality treeViewFunctionality)
        {
            _treeViewFunctionality = treeViewFunctionality;
        }

        public string GetFullpath(string pathToItem)
        {
            string fullPath = PathToCoreFolder ?? "";
            NodeViewModel? node = _treeViewFunctionality.GetFirstSelectedNode() as NodeViewModel;
            INode? coreNode = _treeViewFunctionality.GetCoreNode();

            if (coreNode == null || node == null)
            {
                return fullPath;
            }

            while (node != coreNode)
            {
                fullPath += "/" + node!.Name;
                node = node.Parent as NodeViewModel;
            }
            return fullPath;
        }
    }
}
