using Avalonia.Platform.Storage;
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

namespace DatabaseTask.Services.TreeViewLogic.Functionality.SubFunctionality
{
    public class TreeViewNodeService : ITreeViewNodeService
    {
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
                return node.Parent.Children
                    .Any(x => x.Name == name);
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
                INode? nextNode = currentNode.Children
                    .FirstOrDefault(node => node.Name.Equals(part));

                if (nextNode is null)
                {
                    return null;
                }

                currentNode = nextNode;
            }

            return currentNode;
        }

        public INode? GetCoreNode()
        {
            return _treeView.Nodes.FirstOrDefault();
        }

        public INode? CreateNode(INode template, INode parent)
        {
            if (template is NodeViewModel nodeTemplate)
            {
                var node = new NodeViewModel()
                {
                    Name = nodeTemplate.Name,
                    IsExpanded = nodeTemplate.IsExpanded,
                    IsFolder = nodeTemplate.IsFolder,
                    IconPath = nodeTemplate.IconPath,
                    Parent = parent,
                    StorageItem = template.StorageItem,
                    Children = new SmartCollection<INode>()
                };
                node.Expanded += _eventService.ExpandHandler;
                node.Expanded += ExpandNodeAsync;
                node.Collapsed += _eventService.CollapsedHandler;

                return node;
            }


            return null;
        }

        private NodeViewModel CreateNode(IStorageItem item, INode? parent)
        {
            var node = new NodeViewModel
            {
                Name = item.Name,
                IsFolder = item is IStorageFolder,
                IconPath = item is IStorageFolder
                    ? IconCategory.Folder.Value
                    : IconCategory.File.Value,
                Parent = parent,
                StorageItem = item
            };

            node.Expanded += _eventService.ExpandHandler;
            node.Expanded += ExpandNodeAsync;
            node.Collapsed += _eventService.CollapsedHandler;

            return node;
        }

        private async void AddPlaceholder(NodeViewModel node, IStorageItem item)
        {
            if (!node.IsFolder || item is not IStorageFolder folder)
            {
                return;
            }

            await foreach (var child in folder.GetItemsAsync())
            {
                if (_managerHelper.HasFlag(child, FileAttributes.Hidden))
                {
                    continue;
                }

                node.Children.Add(new NodeViewModel
                {
                    Name = "Loading..."
                });

                return;
            }
        }

        private async void ExpandNodeAsync(INode expandedNode)
        {
            if (expandedNode is not NodeViewModel node || !node.IsFolder || node.IsLoaded)
            {
                return;
            }

            try
            {
                var realNodes = new List<INode>();
                if (node.StorageItem is IStorageFolder folder)
                {
                    await foreach (var item in folder.GetItemsAsync())
                    {
                        if (_managerHelper.HasFlag(item, FileAttributes.Hidden))
                        {
                            continue;
                        }

                        AddNode(realNodes, node, item);
                    }
                }

                var virtualNodes = node.Children.Where(c => c.StorageItem is null && !c.Name.Equals("Loading...")).ToList();

                var combined = realNodes.Concat(virtualNodes)
                                        .ToList();

                combined.Sort(_nodeComparer);

                node.Children.Clear();

                if (combined is not null && combined.Any())
                {
                    node.Children.AddRange(combined!);
                }

                node.IsLoaded = true;
            }
            catch (UnauthorizedAccessException)
            {
            }
        }

        private void AddNode(List<INode> nodes, INode? parent, IStorageItem item)
        {
            var child = CreateNode(item, parent);

            AddPlaceholder(child, item);

            nodes.Add(child);
        }

        public void RemoveNode(INode node)
        {
            node.Parent?.Children.Remove(node);
        }

        public void BringIntoView(INode node)
        {
            _treeView.ScrollChanged?.Invoke(this, new TreeViewEventArgs(node));
        }
    }
}
