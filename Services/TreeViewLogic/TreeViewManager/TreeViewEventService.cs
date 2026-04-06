using Avalonia.Controls;
using DatabaseTask.Models.Categories;
using DatabaseTask.Services.Comparer.Interfaces;
using DatabaseTask.Services.DataGrid.DataGridFunctionality.Interfaces;
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
            INode? node = _treeView.SelectedNodes.FirstOrDefault();
            if (node is not NodeViewModel selectedNode)
            {
                return;
            }

            _dataGrid.FilesProperties.Clear();

            var realNodes = new List<NodeViewModel>();

            if (selectedNode.IsFolder && !string.IsNullOrWhiteSpace(selectedNode.FullPath))
            {
                try
                {
                    foreach (string childPath in Directory.EnumerateFileSystemEntries(selectedNode.FullPath))
                    {
                        if (_treeViewManagerHelper.HasFlag(childPath, FileAttributes.Hidden))
                        {
                            continue;
                        }

                        bool isFolder = Directory.Exists(childPath);
                        realNodes.Add(new NodeViewModel
                        {
                            Name = Path.GetFileName(childPath),
                            IsFolder = isFolder,
                            FullPath = childPath,
                            Parent = selectedNode,
                            IconPath = isFolder ? IconCategory.Folder.Value : IconCategory.File.Value,
                            CreatedAt = GetCreatedAt(childPath)
                        });
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

            var virtualNodes = selectedNode.Children
                .OfType<NodeViewModel>()
                .Where(x => string.IsNullOrWhiteSpace(x.FullPath) && x.Name != "Loading...")
                .ToList();

            var combinedNodes = realNodes.Concat(virtualNodes).ToList();
            combinedNodes.Sort(_comparer);

            var filePropertiesList = new List<FileProperties>();

            foreach (NodeViewModel item in combinedNodes)
            {
                filePropertiesList.Add(CreateFileProperties(item));
            }

            _dataGrid.FilesProperties.AddRange(filePropertiesList);
            await System.Threading.Tasks.Task.CompletedTask;
        }

        private FileProperties CreateFileProperties(NodeViewModel node)
        {
            if (!string.IsNullOrWhiteSpace(node.FullPath))
            {
                return new FileProperties(
                    node.Name,
                    _dataGridFunctionality.SizeToString(node.IsFolder ? null : GetFileSize(node.FullPath)),
                    _dataGridFunctionality.TimeToString(GetModifiedAt(node.FullPath)),
                    node.IsFolder ? IconCategory.Folder.Value : IconCategory.File.Value,
                    node);
            }

            return new FileProperties(
                node.Name,
                "",
                _dataGridFunctionality.TimeToString(new DateTimeOffset(node.CreatedAt)),
                node.IsFolder ? IconCategory.Folder.Value : IconCategory.File.Value,
                node);
        }

        private DateTime GetCreatedAt(string path)
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

        private DateTimeOffset GetModifiedAt(string path)
        {
            try
            {
                DateTime modifiedAt = Directory.Exists(path)
                    ? Directory.GetLastWriteTime(path)
                    : File.GetLastWriteTime(path);

                return new DateTimeOffset(modifiedAt);
            }
            catch
            {
                return DateTimeOffset.MinValue;
            }
        }

        private ulong? GetFileSize(string path)
        {
            try
            {
                return (ulong)new FileInfo(path).Length;
            }
            catch
            {
                return null;
            }
        }
    }
}
