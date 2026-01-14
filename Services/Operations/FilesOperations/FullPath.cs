using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Collections.Generic;
using System.IO;

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

        public string GetPathForNewItem(INode node, string newItemName = "")
        {
            string basePath = GetNodePath(node);
            if (!string.IsNullOrEmpty(newItemName))
            {
                return Path.Combine(basePath, newItemName);
            }
            return basePath;
        }

        public string GetRelativePath(INode node, string newItemName)
        {
            string result = ".";
            string basePath = GetRelativeNodePath(node);
            if (!string.IsNullOrEmpty(basePath))
            {
                result = Path.Combine(result, basePath);
            }

            if (!string.IsNullOrEmpty(newItemName))
            {
                result = Path.Combine(result, newItemName);
            }

            return result;
        }

        private string GetNodePath(INode? node)
        {
            string relativePath = GetRelativeNodePath(node);
            if (string.IsNullOrEmpty(relativePath))
            {
                return PathToCoreFolder ?? string.Empty;
            }

            if (!string.IsNullOrEmpty(PathToCoreFolder))
            {
                return Path.Combine(PathToCoreFolder, relativePath);
            }

            return relativePath;
        }

        private string GetRelativeNodePath(INode? node)
        {
            INode? coreNode = _treeViewFunctionality.GetCoreNode();
            List<string> pathParts = new List<string>();

            if (coreNode == null || node == null)
            {
                return PathToCoreFolder ?? string.Empty;
            }

            while (node != coreNode)
            {
                pathParts.Add(node!.Name);
                node = node.Parent as NodeViewModel;
            }

            pathParts.Reverse();
            string relativePath = Path.Combine(pathParts.ToArray());
            return relativePath;
        }
    }
}
