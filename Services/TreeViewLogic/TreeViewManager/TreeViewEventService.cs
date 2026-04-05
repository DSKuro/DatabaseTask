using Avalonia.Controls;
using Avalonia.Platform.Storage;
using DatabaseTask.Models.Categories;
using DatabaseTask.Services.Comparer.Interfaces;
using DatabaseTask.Services.DataGrid.DataGridFunctionality.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.Services.TreeViewLogic.TreeViewManager.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DatabaseTask.Services.TreeViewLogic.TreeViewManager
{
    public class TreeViewEventService : ITreeViewEventService
    {
        private readonly IDataGridFunctionality _dataGridFunctionality;
        private readonly ITreeView _treeView;
        private readonly IDataGrid _dataGrid;
        private readonly ITreeViewManagerHelper _treeViewManagerHelper;
        private readonly INodeComparer _comparer;

        public TreeViewEventService(ITreeView treeView, IDataGrid dataGrid,
                                   IDataGridFunctionality dataGridFunctionality,
                                   ITreeViewManagerHelper treeViewManagerHelper,
                                   INodeComparer comparer)
        {
            _treeView = treeView;
            _treeView.SelectionChanged += HandleSelectionChanged;
            _dataGrid = dataGrid;
            _dataGridFunctionality = dataGridFunctionality;
            _treeViewManagerHelper = treeViewManagerHelper;
            _comparer = comparer;
        }

        public void HandleSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            _dataGridFunctionality.ClearFilesProperties();
            if (_treeView.Nodes.Count is 1)
            {
                UpdatePropertiesOnSelection();
            }
        }

        public void ExpandHandler(INode model) => ExpandCollapsedImpl(model, IconCategory.OpenedFolder);
        public void CollapsedHandler(INode model) => ExpandCollapsedImpl(model, IconCategory.Folder);

        private void ExpandCollapsedImpl(INode model, IconCategory iconPath)
        {
            if (model is NodeViewModel node)
            {
                node.IconPath = iconPath.Value;
                _treeView.SelectedNodes.Clear();
                _treeView.SelectedNodes.Add(node);
            }
        }

        private async void UpdatePropertiesOnSelection()
        {
            INode? node = _treeView.Nodes.FirstOrDefault();
            if (node is not NodeViewModel selectedNode)
            {
                return;
            }

            _dataGrid.FilesProperties.Clear();

            var realNodes = new List<NodeViewModel>();

            if (selectedNode.StorageItem is IStorageFolder folder)
            {
                try
                {
                    await foreach (var item in folder.GetItemsAsync())
                    {
                        if (_treeViewManagerHelper.HasFlag(item, FileAttributes.Hidden))
                            continue;

                        var nvm = new NodeViewModel
                        {
                            Name = item.Name,
                            IsFolder = item is IStorageFolder,
                            StorageItem = item,
                            Parent = selectedNode,
                            IconPath = item is IStorageFolder ? IconCategory.Folder.Value : IconCategory.File.Value
                        };

                        realNodes.Add(nvm);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                }
            }

            var virtualNodes = selectedNode.Children
                .OfType<NodeViewModel>()
                .Where(x => x.StorageItem is null && x.Name != "Loading...")
                .ToList();

            var combinedNodes = realNodes.Concat(virtualNodes).ToList();
            combinedNodes.Sort(_comparer);

            var filePropertiesList = new List<FileProperties>();
            foreach (var n in combinedNodes)
            {
                if (n.StorageItem is not null)
                {
                    var basicProperties = await n.StorageItem.GetBasicPropertiesAsync();

                    filePropertiesList.Add(new FileProperties(
                        n.StorageItem.Name,
                        n.StorageItem is IStorageFolder ? "" : _dataGridFunctionality.SizeToString(basicProperties.Size),
                        _dataGridFunctionality.TimeToString(basicProperties.DateModified),
                        n.IsFolder ? IconCategory.Folder.Value : IconCategory.File.Value,
                        n
                    ));
                }
                else
                {
                    filePropertiesList.Add(new FileProperties(
                        n.Name,
                        "",
                        _dataGridFunctionality.TimeToString(DateTime.Now),
                        n.IsFolder ? IconCategory.Folder.Value : IconCategory.File.Value,
                        n
                    ));
                }
            }

            _dataGrid.FilesProperties.AddRange(filePropertiesList);
        }
    }
}
