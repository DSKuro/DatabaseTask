using Avalonia.Platform.Storage;
using DatabaseTask.Models.Categories;
using DatabaseTask.Services.Comparer.Interfaces;
using DatabaseTask.Services.DataGrid.DataGridFunctionality.Interfaces;
using DatabaseTask.Services.TreeViewLogic.TreeViewManager.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DatabaseTask.Services.TreeViewLogic.TreeViewManager
{
    public class TreeViewManager : ITreeViewManager
    {
        private readonly ITreeView _treeView;
        private readonly IDataGridFunctionality _dataGridFunctionality;
        private readonly ITreeViewEventService _eventService;
        private readonly ITreeViewManagerHelper _managerHelper;
        private readonly INodeComparer _nodeComparer;

        public TreeViewManager(ITreeView treeView,
                              IDataGridFunctionality dataGridFunctionality,
                              ITreeViewEventService eventService,
                              ITreeViewManagerHelper managerHelper,
                              INodeComparer nodeComparer)
        {
            _treeView = treeView;
            _dataGridFunctionality = dataGridFunctionality;
            _eventService = eventService;
            _managerHelper = managerHelper;
            _nodeComparer = nodeComparer;
        }

        public void LoadFoldersAsync(IEnumerable<IStorageFolder> folders)
        {
            _treeView.Nodes.Clear();
            _dataGridFunctionality.ClearSavedProperties();
            _dataGridFunctionality.ClearFilesProperties();
            GetCollection(folders);
        }

        private void GetCollection(IEnumerable<IStorageFolder> folders)
        {
            var nodes = new List<INode>();
            foreach (var folder in folders)
            {
                AddNode(nodes, null, folder);
            }
            _treeView.Nodes.AddRange(nodes);
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
    }
}
