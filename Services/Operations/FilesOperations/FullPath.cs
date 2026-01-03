using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using System.Collections.Generic;

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
            INode? coreNode = _treeViewFunctionality.GetCoreNode();
            INode? selectedNode = _treeViewFunctionality.GetFirstSelectedNode();
            List<string> pathParts = new List<string>();

            if (coreNode == null || selectedNode == null)
            {
                return PathToCoreFolder ?? string.Empty;
            }

            pathParts.Add(pathToItem);

            NodeViewModel? parentNode = selectedNode.Parent as NodeViewModel;

            while (parentNode != null && parentNode != coreNode)
            {
                pathParts.Add(parentNode.Name);
                parentNode = parentNode.Parent as NodeViewModel;
            }

            pathParts.Reverse();

            string relativePath = string.Join("/", pathParts);

            return $"{PathToCoreFolder}/{relativePath}";
        }
    }
}
