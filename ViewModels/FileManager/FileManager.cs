using Avalonia.Controls;
using Avalonia.Platform.Storage;
using DatabaseTask.Models;
using DatabaseTask.Services._serviceCollection;
using DatabaseTask.Services.FileManagerOperations.Accessibility.Interfaces;
using DatabaseTask.ViewModels.DataGrid.Interfaces;
using DatabaseTask.ViewModels.FileManager.Interfaces;
using DatabaseTask.ViewModels.Nodes;
using DatabaseTask.ViewModels.TreeView.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.FileManager
{
    public partial class FileManager : ViewModelBase, IFileManager
    {
        private readonly ITreeView _treeView;
        private readonly IDataGrid _dataGrid;
        private readonly IFileManagerOperationsPermissions _permissions;

        public ITreeView TreeView { get => _treeView; }
        public IDataGrid DataGrid { get => _dataGrid; }
        public IFileManagerOperationsPermissions Permissions { get => _permissions; }

        public FileManager(ITreeView treeView, IDataGrid dataGrid, IFileManagerOperationsPermissions permissions)
        {
            _treeView = treeView;
            _treeView.SelectionChanged += OnSelectionChanged;
            _dataGrid = dataGrid;
            _permissions = permissions;
        }

        public async Task GetCollectionFromFolders(IEnumerable<IStorageFolder> folders)
        {
            _treeView.Nodes.Clear();
            _dataGrid.SavedFilesProperties.Clear();
            _dataGrid.FilesProperties.Clear();
            await GetCollectionByRecursion(folders);
        }

        private async Task GetCollectionByRecursion(IEnumerable<IStorageFolder> folders)
        {
            foreach (IStorageFolder folder in folders)
            {
                   
                NodeViewModel rootNode = await CreateNodeRecursive(folder, null);
                _treeView.Nodes.Add(rootNode);
            }
        }

        private async Task<NodeViewModel> CreateNodeRecursive(IStorageItem item, INode parent)
        {
            StorageItemProperties properties = await item.GetBasicPropertiesAsync();
            NodeViewModel node = CreateMainNode(item, parent);
            await CreateNode(item, properties, parent, node);
            return node;
        }

        private async Task CreateNode(IStorageItem item, StorageItemProperties properties,
            INode parent, INode node)
        {
            AddFileProperties(item, properties, parent, node);
            await CreateChildren(item, node);
            node.Expanded += ExpandHandler;
            node.Collapsed += CollapsedHandler;
        }

        private NodeViewModel CreateMainNode(IStorageItem item, INode parent)
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

        private void AddFileProperties(
            IStorageItem item,
            StorageItemProperties properties,
            INode parent,
            INode node)
        {
            string modifiedString = _dataGrid.TimeToString(properties.DateModified);
            
            _dataGrid.SavedFilesProperties.Add(new FileProperties(
                item.Name,
                item is IStorageFolder ? "" : _dataGrid.SizeToString(properties.Size),
                modifiedString,
                item is IStorageFolder
                    ? IconCategory.Folder.Value
                    : IconCategory.File.Value,
                node
            ));
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
                            node.Children.Add(childNode);
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

                FileAttributes attributes = System.IO.File.GetAttributes(path);
                return attributes.HasFlag(flag);
            }
            catch
            {
                return false;
            }
        }

        private void ExpandHandler(INode model)
        {
            ExpandCollapsedImpl(model, IconCategory.OpenedFolder);
        }

        private void CollapsedHandler(INode model)
        {
            ExpandCollapsedImpl(model, IconCategory.Folder);
        }

        private void ExpandCollapsedImpl(INode model, IconCategory iconPath)
        {
            if (model is NodeViewModel node)
            {
                node.IconPath = iconPath.Value;
                _treeView.SelectedNodes.Clear();
                _treeView.SelectedNodes.Add(node);
            }
        }

        private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            _dataGrid.FilesProperties.Clear();
            if (_treeView.SelectedNodes.Count == 1)
            {
                UpdatePropertiesOnSelection();
            }
        }

        private void UpdatePropertiesOnSelection()
        {
            (IEnumerable<FileProperties> folders,
               IEnumerable<FileProperties> files) = GetChildFoldersAndFilesProperties();
            _dataGrid.FilesProperties.AddRange(folders);
            _dataGrid.FilesProperties.AddRange(files);
        }

        private (IEnumerable<FileProperties>, IEnumerable<FileProperties>) GetChildFoldersAndFilesProperties()
        {
            IEnumerable<FileProperties> selectedNodeChilds = _dataGrid.SavedFilesProperties
                .Where(x => x.Node.Parent == _treeView.SelectedNodes[0])
                .Where(x => x.Node is NodeViewModel);
            IEnumerable<FileProperties> folders = selectedNodeChilds
                .Where(x => ((NodeViewModel)x.Node).IsFolder).OrderBy(x => x.Name);
            IEnumerable<FileProperties> files = selectedNodeChilds
                .Where(x => !((NodeViewModel)x.Node).IsFolder).OrderBy(x => x.Name);
            return (folders, files);
        }
    }
}
