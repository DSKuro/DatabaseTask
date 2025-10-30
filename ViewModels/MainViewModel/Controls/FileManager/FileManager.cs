using Avalonia.Controls;
using Avalonia.Platform.Storage;
using DatabaseTask.Models.Categories;
using DatabaseTask.ViewModels.Base;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.DataGridFunctionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.FileManager.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Functionality;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Functionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.Controls.FileManager
{
    public partial class FileManager : ViewModelBase, IFileManager
    {
        private readonly ITreeView _treeView;
        private readonly ITreeViewFunctionality _treeViewFunctionality;
        private readonly IDataGrid _dataGrid;
        private readonly IDataGridFunctionality _dataGridFunctionality;

        public IDataGrid DataGrid { get => _dataGrid; }
        public ITreeView TreeView { get => _treeView; }
        public ITreeViewFunctionality TreeViewFunctionality { get => _treeViewFunctionality; }

        public FileManager(ITreeView treeView,
            ITreeViewFunctionality treeViewFunctionality,
            IDataGrid dataGrid,
            IDataGridFunctionality dataGridFunctionality)
        {
            _treeView = treeView;
            _treeView.SelectionChanged += OnSelectionChanged;
            _treeViewFunctionality = treeViewFunctionality;
            _dataGrid = dataGrid;
            _dataGridFunctionality = dataGridFunctionality;
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
            node.Expanded += ExpandHandler;
            node.Collapsed += CollapsedHandler;
        }

        private void AddFileProperties(
    IStorageItem item,
    StorageItemProperties properties,
    INode node)
        {
            string modifiedString = _dataGridFunctionality.TimeToString(properties.DateModified);

            var newProperties = new FileProperties(
                item.Name,
                item is IStorageFolder ? "" : _dataGridFunctionality.SizeToString(properties.Size),
                modifiedString,
                item is IStorageFolder
                    ? IconCategory.Folder.Value
                    : IconCategory.File.Value,
                node
            );

            NodeViewModel newNode = node as NodeViewModel;
            // Вставка в SavedFilesProperties с сортировкой
            var comparer = new AdvancedExplorerComparer();

            var sameTypeProps = _dataGrid.SavedFilesProperties
                .Where(x => (x.Node as NodeViewModel).IsFolder == newNode.IsFolder)
                .ToList();

            int insertIndex = sameTypeProps.FindIndex(x => comparer.Compare(newProperties.Name, x.Node.Name) < 0);
            int finalIndex;

            if (insertIndex == -1)
            {
                var last = sameTypeProps.LastOrDefault();
                finalIndex = last != null ? _dataGrid.SavedFilesProperties.IndexOf(last) + 1 : 0;
            }
            else
            {
                var targetProp = sameTypeProps[insertIndex];
                finalIndex = _dataGrid.SavedFilesProperties.IndexOf(targetProp);
            }

            _dataGrid.SavedFilesProperties.Insert(finalIndex, newProperties);
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
                            // Вставляем childNode в node.Children в правильное место
                            var comparer = new AdvancedExplorerComparer();
                            int insertIndex = node.Children
                                .OfType<NodeViewModel>()
                                .ToList()
                                .FindIndex(x => x.IsFolder == childNode.IsFolder && comparer.Compare(childNode.Name, x.Name) < 0);

                            if (insertIndex == -1)
                                node.Children.Add(childNode); // в конец
                            else
                                node.Children.Insert(insertIndex, childNode);
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
                .Where(x => ((NodeViewModel)x.Node).IsFolder);
            IEnumerable<FileProperties> files = selectedNodeChilds
                .Where(x => !((NodeViewModel)x.Node).IsFolder);
            return (folders, files);
        }
    }
}
