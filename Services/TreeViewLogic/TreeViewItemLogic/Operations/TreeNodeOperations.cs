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
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic.Operations
{
    public class TreeNodeOperations : ITreeNodeOperations
    {
        private readonly ITreeViewData _treeViewData;
        private readonly ITreeView _treeView;
        private readonly ITreeViewControlsHelper _helper;
        private readonly INodeEvents _nodeEvents;
        private readonly IFileManagerFileOperationsPermissions _filePermission;
        private readonly IFileManagerFolderOperationsPermissions _folderPermission;

        private DateTime _lastScrollTime = DateTime.MinValue;
        private readonly TimeSpan _scrollInterval = TimeSpan.FromMilliseconds(50);
        private CancellationTokenSource? _currentScrollCancellation;

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
            _helper = helper;
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
            else
            {
                StopScrolling();
            }
        }

        private void StopScrolling()
        {
            _currentScrollCancellation?.Cancel();
            _currentScrollCancellation = null;
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

        private async void Scroll(bool isInTopZone)
        {
            _currentScrollCancellation?.Cancel();
            _currentScrollCancellation = new CancellationTokenSource();
            CancellationToken token = _currentScrollCancellation.Token;

            if (DateTime.Now - _lastScrollTime < _scrollInterval)
            {
                return;
            }

            Visual? itemView = _treeViewData.DraggedItemView;
            if (itemView == null)
            {
                return;
            }

            _lastScrollTime = DateTime.Now;

            double itemHeight = itemView.Bounds.Height;
            double currentOffset = _treeViewData.ScrollViewer.Offset.Y;

            double scrollStep = itemHeight * 0.7;
            double targetOffset = currentOffset + scrollStep * (isInTopZone ? -1 : 1);
            targetOffset = Math.Clamp(targetOffset, 0, _treeViewData.ScrollViewer.ScrollBarMaximum.Y);

            if (Math.Abs(targetOffset - currentOffset) < 1.0)
            {
                return;
            }

            try
            {
                double startOffset = currentOffset;
                double duration = 250;
                DateTime startTime = DateTime.Now;

                while ((DateTime.Now - startTime).TotalMilliseconds < duration)
                {
                    if (token.IsCancellationRequested)
                    {
                        return;
                    }

                    double progress = (DateTime.Now - startTime).TotalMilliseconds / duration;
                    progress = Math.Min(1.0, progress);

                    double easedProgress = EaseOutCubic(progress);

                    double currentScrollOffset = startOffset + (targetOffset - startOffset) * easedProgress;
                    _treeViewData.ScrollViewer.Offset = new Vector(_treeViewData.ScrollViewer.Offset.X, currentScrollOffset);

                    await Task.Delay(16, token);
                }

                if (!token.IsCancellationRequested)
                {
                    _treeViewData.ScrollViewer.Offset = new Vector(_treeViewData.ScrollViewer.Offset.X, targetOffset);
                }
            }
            catch (TaskCanceledException)
            {
            }
        }

        private double EaseOutCubic(double progress)
        {
            return 1 - Math.Pow(1 - progress, 3);
        }

        public void BringIntoView(INode item)
        {
            TreeViewItem? treeItem = _helper.GetVisualForData(item);
            if (treeItem != null)
            {
                treeItem.BringIntoView();
            }
        }

        public void DragItem(INode item, DragEventArgs args)
        {
            List<INode> nodes = _treeView.SelectedNodes.ToList();
            nodes.Add(item);
            _nodeEvents.OnDrop?.Invoke(nodes, true, true);
        }
    }
}
