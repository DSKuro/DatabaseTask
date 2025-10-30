using Avalonia.Platform.Storage;
using DatabaseTask.Models.Categories;
using DatabaseTask.Services.DataGrid.DataGridFunctionality.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.Services.TreeViewLogic.TreeViewManager.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DatabaseTask.Services.TreeViewLogic.TreeViewManager
{
    public class TreeViewManager : ITreeViewManager
    {
        private readonly ITreeView _treeView;
        private readonly ITreeViewFunctionality _treeViewFunctionality;
        private readonly IDataGridFunctionality _dataGridFunctionality;
        private readonly ITreeViewEventService _eventService;

        public TreeViewManager(ITreeView treeView, ITreeViewFunctionality treeViewFunctionality,
                              IDataGridFunctionality dataGridFunctionality,
                              ITreeViewEventService eventService)
        {
            _treeView = treeView;
            _treeViewFunctionality = treeViewFunctionality;
            _dataGridFunctionality = dataGridFunctionality;
            _eventService = eventService;
        }

        public async Task LoadFoldersAsync(IEnumerable<IStorageFolder> folders)
        {
            _treeView.Nodes.Clear();
            _dataGridFunctionality.ClearSavedProperties();
            _dataGridFunctionality.ClearFilesProperties();
            await GetCollectionByRecursion(folders);
        }

        private async Task GetCollectionByRecursion(IEnumerable<IStorageFolder> folders)
        {
            foreach (IStorageFolder folder in folders)
            {
                NodeViewModel rootNode = await CreateNodeRecursive(folder);
                _treeView.Nodes.Add(rootNode);
            }
        }

        private async Task<NodeViewModel> CreateNodeRecursive(IStorageItem item, INode? parent = null)
        {
            StorageItemProperties properties = await item.GetBasicPropertiesAsync();
            NodeViewModel node = CreateMainNode(item, parent);
            await CreateNode(item, properties, node);
            return node;
        }

        private NodeViewModel CreateMainNode(IStorageItem item, INode? parent)
        {
            return new NodeViewModel
            {
                Name = item.Name,
                IsFolder = item is IStorageFolder,
                IconPath = item is IStorageFolder
                    ? IconCategory.Folder.Value
                    : IconCategory.File.Value,
                Parent = parent
            };
        }

        private async Task CreateNode(IStorageItem item, StorageItemProperties properties,
            INode node)
        {
            AddFileProperties(item, properties, node);
            await CreateChildren(item, node);
            node.Expanded += _eventService.ExpandHandler;
            node.Collapsed += _eventService.CollapsedHandler;
        }

        public void AddFileProperties(IStorageItem item, StorageItemProperties properties, INode node)
        {
            string modifiedString = _dataGridFunctionality.TimeToString(properties.DateModified);

            var newProperties = new FileProperties(
                item.Name,
                item is IStorageFolder ? "" : _dataGridFunctionality.SizeToString(properties.Size),
                modifiedString,
                item is IStorageFolder ? IconCategory.Folder.Value : IconCategory.File.Value,
                node
            );

            _dataGridFunctionality.AddProperties(newProperties);
        }

        private async Task CreateChildren(IStorageItem item, INode node)
        {
            if (item is IStorageFolder folder)
            {
                try
                {
                    IAsyncEnumerable<IStorageItem> items = folder.GetItemsAsync();
                    await foreach (IStorageItem? childItem in items)
                    {
                        if (!HasFlag(childItem, FileAttributes.Hidden))
                        {
                            NodeViewModel childNode = await CreateNodeRecursive(childItem, node);
                            _treeViewFunctionality.TryInsertNode(node, childNode, out _);
                        }
                    }
                }
                catch (UnauthorizedAccessException)
                {
                }
            }
        }

        private bool HasFlag(IStorageItem item, FileAttributes flag)
        {
            try
            {
                string? path = item.TryGetLocalPath();
                if (string.IsNullOrEmpty(path))
                    return false;

                FileAttributes attributes = File.GetAttributes(path);
                return attributes.HasFlag(flag);
            }
            catch
            {
                return false;
            }
        }
    }
}
