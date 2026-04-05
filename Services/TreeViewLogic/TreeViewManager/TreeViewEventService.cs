using Avalonia.Controls;
using Avalonia.Platform.Storage;
using DatabaseTask.Models.Categories;
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
        private readonly ITreeViewFunctionality _treeViewFunctionality;
        private readonly IDataGridFunctionality _dataGridFunctionality;
        private readonly ITreeView _treeView;
        private readonly IDataGrid _dataGrid;
        private readonly ITreeViewManagerHelper _treeViewManagerHelper;

        public TreeViewEventService(ITreeView treeView, IDataGrid dataGrid,
                                   ITreeViewFunctionality treeViewFunctionality,
                                   IDataGridFunctionality dataGridFunctionality,
                                   ITreeViewManagerHelper treeViewManagerHelper)
        {
            _treeView = treeView;
            _treeView.SelectionChanged += HandleSelectionChanged;
            _dataGrid = dataGrid;
            _treeViewFunctionality = treeViewFunctionality;
            _dataGridFunctionality = dataGridFunctionality;
            _treeViewManagerHelper = treeViewManagerHelper;
        }

        public void HandleSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            _dataGridFunctionality.ClearFilesProperties();
            if (_treeViewFunctionality.GetAllSelectedNodes().Count is 1)
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
                _treeViewFunctionality.GetAllSelectedNodes().Clear();
                _treeViewFunctionality.GetAllSelectedNodes().Add(node);
            }
        }

        private async void UpdatePropertiesOnSelection()
        {
            INode? node = _treeViewFunctionality.GetFirstSelectedNode();

            if (node is not NodeViewModel selectedNode)
            {
                return;
            }

            _dataGrid.FilesProperties.Clear();

            var items = new List<FileProperties>();

            if (selectedNode.StorageItem is IStorageFolder folder)
            {
                try
                {
                    await foreach (var item in folder.GetItemsAsync())
                    {
                        if (_treeViewManagerHelper.HasFlag(item, FileAttributes.Hidden))
                        {
                            continue;
                        }

                        var properties = await item.GetBasicPropertiesAsync();

                        items.Add(new FileProperties(
                            item.Name,
                            item is IStorageFolder
                                ? ""
                                : _dataGridFunctionality.SizeToString(properties.Size),
                            _dataGridFunctionality.TimeToString(properties.DateModified),
                            item is IStorageFolder
                                ? IconCategory.Folder.Value
                                : IconCategory.File.Value,
                            selectedNode
                        ));
                    }
                }
                catch (UnauthorizedAccessException)
                {
                }
            }

            var virtualItems = selectedNode.Children
                .OfType<NodeViewModel>()
                .Where(x => x.StorageItem is null && x.Name != "Loading...")
                .Select(x => new FileProperties(
                    x.Name,
                    x.IsFolder ? "" : "",
                    _dataGridFunctionality.TimeToString(DateTime.Now),
                    x.IsFolder
                        ? IconCategory.Folder.Value
                        : IconCategory.File.Value,
                    x
                ));

            items.AddRange(virtualItems);

            var sorted = items
                .OrderByDescending(x => string.IsNullOrEmpty(x.Size))
                .ThenBy(x => x.Name, StringComparer.OrdinalIgnoreCase)
                .ToList();

            _dataGrid.FilesProperties.AddRange(sorted);
        }
    }
}
