using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using DatabaseTask.Services.Operations.FileManagerOperations.Accessibility.Interfaces;
using DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic.ControlsHelpers.Interfaces;
using DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic.InteractionData.Interfaces;
using DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic.Operations.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic.Operations
{
    public class TreeNodeOperations : ITreeNodeOperations
    {
        private readonly ITreeViewData _treeViewData;
        private readonly ITreeView _treeView;
        private readonly IDataGrid _dataGrid;
        private readonly ITreeViewControlsHelper _helper;
        private readonly INodeEvents _nodeEvents;
        private readonly IFileManagerFileOperationsPermissions _filePermission;
        private readonly IFileManagerFolderOperationsPermissions _folderPermission;

        private List<NodeViewModel> _sortedFolders;
        private List<NodeViewModel> _sortedFiles;

        public TreeNodeOperations(
            ITreeViewData treeViewData,
            ITreeView treeView,
            IDataGrid dataGrid,
            ITreeViewControlsHelper helper,
            INodeEvents nodeEvents,
            IFileManagerFileOperationsPermissions filePermission,
            IFileManagerFolderOperationsPermissions folderPermission)
        {
            _treeViewData = treeViewData;
            _treeView = treeView;
            _dataGrid = dataGrid;
            _helper = helper;
            _sortedFiles = new List<NodeViewModel>();
            _sortedFolders = new List<NodeViewModel>();
            _nodeEvents = nodeEvents;
            _filePermission = filePermission;
            _folderPermission = folderPermission;
        }

        public bool CanDrop(INode target)
        {
            List<INode> selectedNodes = _treeView.SelectedNodes.ToList();
  
            if (selectedNodes.Count <= 0)
            {
                return false;
            }

            if (target is not NodeViewModel targetNode || !targetNode.IsFolder)
            {
                return false;
            }

            List<NodeViewModel> nodeViewModels = selectedNodes.OfType<NodeViewModel>().ToList();

            if (nodeViewModels.Count != selectedNodes.Count)
            {
                return false;
            }

            List<INode> files = nodeViewModels.Where(item => !item.IsFolder).Cast<INode>().ToList();
            List<INode> folders = nodeViewModels.Where(item => item.IsFolder).Cast<INode>().ToList();

            try
            {
                if (files.Count > 0)
                {
                    files.Add(target);
                    _filePermission.CanCopyFile(files);
                }

                if (folders.Count > 0)
                {
                    folders.Add(target);
                    _folderPermission.CanCopyCatalog(folders);
                }
            }
            catch
            {
                return false;
            }

            return selectedNodes.All(item =>
                item != target &&
                IsTargetAboveSource(item, target) &&
                item.Parent != target
            );
        }

        private bool IsTargetAboveSource(INode source, INode target)
        {
            INode? parent = target.Parent;
            while (parent != null)
            {
                if (parent == source)
                {
                    return false;
                }
                parent = parent.Parent;
            }

            return true;
        }

        public void ScrollToDroppedItem(DragEventArgs e)
        {
            _treeViewData.DragStartPosition = e.GetPosition(_treeViewData.Control);
            (bool isInBottomZone, bool isInTopZone) = IsOverrideTopOrDownZone();
            if (isInBottomZone || isInTopZone)
            {
                Scroll(isInTopZone);
            }
        }

        private (bool, bool) IsOverrideTopOrDownZone()
        {
            Visual? itemView = _treeViewData.DraggedItemView;
            if (itemView == null)
            {
                return (false, false);
            }

            double startPosition = _treeViewData.DragStartPosition.Y;
            double itemHeight = itemView.Bounds.Height;
            double controlHeight = _treeViewData.Control.Bounds.Height;
            bool isInBottomZone = startPosition > controlHeight - itemHeight - 5;
            bool isInTopZone = startPosition < itemHeight - 5;
            return (isInBottomZone, isInTopZone);
        }

        private void Scroll(bool isInTopZone)
        {
            Visual? itemView = _treeViewData.DraggedItemView;
            if (itemView == null)
            {
                return;
            }

            double newOffset = _treeViewData.ScrollViewer.Offset.Y +
                itemView.Bounds.Height * (isInTopZone ? -1 : 1);
            newOffset = Math.Clamp(newOffset, 0, _treeViewData.ScrollViewer.ScrollBarMaximum.Y);
            _treeViewData.ScrollViewer.Offset = new Vector(_treeViewData.ScrollViewer.Offset.X, newOffset);
        }

        public void BringIntoView(INode item)
        {
            TreeViewItem? treeItem = _helper.GetVisualForData(item);
            if (treeItem != null)
            {
                treeItem.BringIntoView();
            }
        }

        public async void DragItem(INode item, DragEventArgs args)
        {
            List<INode> nodes = _treeView.SelectedNodes.ToList();
            nodes.Add(item);
            //PlaceNode(nodes, item);
            //GetSortedCategories(item);
            //FillNodes(item, nodes);
            //await Task.Delay(100);
            //BringIntoView(nodes[0]);
            _nodeEvents.OnDrop?.Invoke(nodes);
        }

        private void PlaceNode(List<INode> nodes, INode targetNode)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                INode? parent = nodes[i].Parent;
                if (parent != null)
                {
                    parent.Children.Remove(nodes[i]);
                }
                else
                {
                    _treeView.Nodes.Remove(nodes[i]);
                }
                targetNode.Children.Add(nodes[i]);
                FileProperties? elem = _dataGrid.SavedFilesProperties
                    .Find(x => x.Node == nodes[i]);
                if (elem != null)
                {
                    elem.Node.Parent = targetNode;
                    parent = targetNode;
                    if (parent != null)
                    {
                        parent.IsExpanded = true;
                    }
                }

            }
        }

        private void GetSortedCategories(INode targetNode)
        {
            IEnumerable<NodeViewModel> sorted = targetNode.Children.Select(x => (NodeViewModel)x);
            _sortedFolders = sorted.Where(x => x.IsFolder).OrderBy(x => x.Name).ToList();
            _sortedFiles = sorted.Where(x => !x.IsFolder).OrderBy(x => x.Name).ToList();
        }

        private void FillNodes(INode targetNode,
            List<INode> nodes)
        {
            targetNode.Children.Clear();
            targetNode.Children.AddRange(_sortedFolders);
            targetNode.Children.AddRange(_sortedFiles);
            _treeView.SelectedNodes.AddRange(nodes);
        }
    }
}
