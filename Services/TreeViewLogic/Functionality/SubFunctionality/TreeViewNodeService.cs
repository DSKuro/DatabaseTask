using DatabaseTask.Models;
using DatabaseTask.Models.Categories;
using DatabaseTask.Services.Comparer.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.SubFunctionality.Interfaces;
using DatabaseTask.Services.TreeViewLogic.TreeViewManager.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.EventArguments;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseTask.Services.TreeViewLogic.Functionality.SubFunctionality
{
    public class TreeViewNodeService : ITreeViewNodeService
    {
        private const string LoadingPlaceholderName = "Loading...";

        private readonly ITreeView _treeView;
        private readonly ITreeViewManagerHelper _managerHelper;
        private readonly INodeComparer _nodeComparer;
        private readonly ITreeViewEventService _eventService;

        public TreeViewNodeService(ITreeView treeView,
            ITreeViewEventService eventService,
            INodeComparer nodeComparer,
            ITreeViewManagerHelper managerHelper)
        {
            _treeView = treeView;
            _managerHelper = managerHelper;
            _eventService = eventService;
            _nodeComparer = nodeComparer;
        }

        public bool IsNodeExist(INode parent, string name)
        {
            if (parent == null)
            {
                return false;
            }

            return parent.Children.Any(x => x.Name == name);
        }

        public bool IsParentHasNodeWithName(INode node, string name)
        {
            if (node != null && node.Parent != null)
            {
                return node.Parent.Children.Any(x => x.Name == name);
            }

            return false;
        }

        public INode? GetChildrenByName(INode node, string name)
        {
            return node.Children.FirstOrDefault(x => x.Name == name);
        }

        public INode? GetNodeByPath(string path)
        {
            INode? coreNode = GetCoreNode();
            if (coreNode is null)
            {
                return null;
            }

            var parts = path.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries)
                .Where(part => !part.Equals("."));

            INode currentNode = coreNode;

            foreach (var part in parts)
            {
                INode? nextNode = currentNode.Children.FirstOrDefault(node => node.Name.Equals(part));

                if (nextNode is null)
                {
                    return null;
                }

                currentNode = nextNode;
            }

            return currentNode;
        }

        public INode? FindVirtualNode(string name)
        {
            INode? coreNode = GetCoreNode();
            return coreNode is null ? null : FindVirtualNodeRecursive(coreNode, name);
        }

        public INode? GetCoreNode()
        {
            return _treeView.Nodes.FirstOrDefault();
        }

        public INode? CreateNode(INode template, INode parent)
        {
            if (template is not NodeViewModel nodeTemplate)
            {
                return null;
            }

            var node = new NodeViewModel()
            {
                Name = nodeTemplate.Name,
                IsExpanded = nodeTemplate.IsExpanded,
                IsFolder = nodeTemplate.IsFolder,
                IconPath = nodeTemplate.IconPath,
                Parent = parent,
                FullPath = parent is null || string.IsNullOrWhiteSpace(parent.FullPath)
                    ? nodeTemplate.FullPath
                    : Path.Combine(parent.FullPath, nodeTemplate.Name),
                Children = new SmartCollection<INode>(),
                CreatedAt = nodeTemplate.CreatedAt
            };

            AttachNodeEvents(node);
            return node;
        }

        public async Task<List<INode>> CreateNodesFromPathsAsync(IEnumerable<string> paths, INode? parent = null)
        {
            var nodes = new List<INode>();

            foreach (var path in paths)
            {
                if (string.IsNullOrWhiteSpace(path))
                {
                    continue;
                }

                NodeViewModel child = await CreateNodeFromPathAsync(path, parent);
                nodes.Add(child);
            }

            return nodes;
        }

        public async Task<List<INode>> GetChildNodesAsync(INode node)
        {
            if (node is not NodeViewModel parentNode || !parentNode.IsFolder || string.IsNullOrWhiteSpace(parentNode.FullPath))
            {
                return GetExistingChildren(node);
            }

            var realNodes = new List<INode>();

            try
            {
                foreach (string childPath in Directory.EnumerateFileSystemEntries(parentNode.FullPath))
                {
                    if (_managerHelper.HasFlag(childPath, FileAttributes.Hidden))
                    {
                        continue;
                    }

                    realNodes.Add(await CreateNodeFromPathAsync(childPath, parentNode));
                }
            }
            catch (UnauthorizedAccessException)
            {
            }
            catch (DirectoryNotFoundException)
            {
            }
            catch (IOException)
            {
            }

            var combined = realNodes
                .Concat(GetExistingChildren(node))
                .GroupBy(GetNodeIdentity)
                .Select(x => x.First())
                .ToList();
            combined.Sort(_nodeComparer);
            return combined;
        }

        public void RemoveNode(INode node)
        {
            node.Parent?.Children.Remove(node);
        }

        public void BringIntoView(INode node)
        {
            _treeView.ScrollChanged?.Invoke(this, new TreeViewEventArgs(node));
        }

        private async Task<NodeViewModel> CreateNodeFromPathAsync(string path, INode? parent)
        {
            bool isFolder = Directory.Exists(path);
            DateTime createdAt = GetCreatedAt(path);

            var node = new NodeViewModel
            {
                Name = Path.GetFileName(path),
                IsFolder = isFolder,
                IconPath = isFolder ? IconCategory.Folder.Value : IconCategory.File.Value,
                Parent = parent,
                FullPath = path,
                CreatedAt = createdAt
            };

            AttachNodeEvents(node);
            AddPlaceholder(node);

            await Task.CompletedTask;
            return node;
        }

        private void AttachNodeEvents(NodeViewModel node)
        {
            node.Expanded += _eventService.ExpandHandler;
            node.Expanded += async n => await ExpandNodeAsync(n);
            node.Collapsed += _eventService.CollapsedHandler;
        }

        private void AddPlaceholder(NodeViewModel node)
        {
            if (!node.IsFolder || string.IsNullOrWhiteSpace(node.FullPath))
            {
                return;
            }

            try
            {
                foreach (string childPath in Directory.EnumerateFileSystemEntries(node.FullPath))
                {
                    if (_managerHelper.HasFlag(childPath, FileAttributes.Hidden))
                    {
                        continue;
                    }

                    node.Children.Add(new NodeViewModel
                    {
                        Name = LoadingPlaceholderName
                    });

                    return;
                }
            }
            catch (UnauthorizedAccessException)
            {
            }
            catch (DirectoryNotFoundException)
            {
            }
            catch (IOException)
            {
            }
        }

        private async Task ExpandNodeAsync(INode expandedNode)
        {
            if (expandedNode is not NodeViewModel node || !node.IsFolder || node.IsLoaded)
            {
                return;
            }

            List<INode> combined = await GetChildNodesAsync(node);

            node.Children.Clear();

            if (combined.Any())
            {
                node.Children.AddRange(combined);
            }

            node.IsLoaded = true;
        }

        private static DateTime GetCreatedAt(string path)
        {
            try
            {
                return Directory.Exists(path)
                    ? Directory.GetCreationTime(path)
                    : File.GetCreationTime(path);
            }
            catch
            {
                return DateTime.Now;
            }
        }

        private List<INode> GetExistingChildren(INode node)
        {
            return node.Children
                .Where(c => !c.Name.Equals(LoadingPlaceholderName))
                .ToList();
        }

        private string GetNodeIdentity(INode node)
        {
            return $"{node.FullPath ?? string.Empty}|{node.Name}";
        }

        private static INode? FindVirtualNodeRecursive(INode node, string name)
        {
            if (string.Equals(node.Name, name, StringComparison.OrdinalIgnoreCase)
                && string.IsNullOrWhiteSpace(node.FullPath))
            {
                return node;
            }

            foreach (INode child in node.Children)
            {
                INode? found = FindVirtualNodeRecursive(child, name);
                if (found is not null)
                {
                    return found;
                }
            }

            return null;
        }
    }
}
