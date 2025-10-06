using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using DatabaseTask.Services.TreeViewItemLogic.Interfaces;
using DatabaseTask.ViewModels.MainViewModel;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseTask.Services.TreeViewItemLogic
{
    public class TreeNodeOperations : ITreeNodeOperations
    {
        private readonly ITreeViewData _treeViewData;
        private readonly ITreeViewControlsHelper _helper;
        private readonly MainWindowViewModel _viewModel;

        private List<NodeViewModel?> _sortedFolders;
        private List<NodeViewModel?> _sortedFiles;

        public TreeNodeOperations(
            ITreeViewData treeViewData,
            ITreeViewControlsHelper helper,
            MainWindowViewModel viewModel)
        {
            _treeViewData = treeViewData;
            _helper = helper;
            _viewModel = viewModel;
        }

        public bool CanDrop(INode target)
        {
            foreach (INode item in _viewModel.FileManager.TreeView.SelectedNodes)
            {
                if (item == target)
                {
                    return false;
                }

                NodeViewModel node = target as NodeViewModel;

                if (!(IsTargetAboveSource(item, target) && node.IsFolder && item.Parent != target))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsTargetAboveSource(INode source, INode target)
        {
            INode parent = target.Parent;
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
            bool isInBottomZone = _treeViewData.DragStartPosition.Y >
                (_treeViewData.Control.Bounds.Height - _treeViewData.DraggedItemView.Bounds.Height - 5);
            bool isInTopZone = _treeViewData.DragStartPosition.Y <
                _treeViewData.DraggedItemView.Bounds.Height - 5;
            return (isInBottomZone, isInTopZone);
        }

        private void Scroll(bool isInTopZone)
        {
            double newOffset = _treeViewData.ScrollViewer.Offset.Y +
                (_treeViewData.DraggedItemView.Bounds.Height * (isInTopZone ? -1 : 1));
            newOffset = Math.Clamp(newOffset, 0, _treeViewData.ScrollViewer.ScrollBarMaximum.Y);
            _treeViewData.ScrollViewer.Offset = new Vector(_treeViewData.ScrollViewer.Offset.X, newOffset);
        }

        public void BringIntoView(INode item)
        {
            TreeViewItem? treeItem = _helper.GetVisualForData(item);
            treeItem.BringIntoView();
        }

        public async void DragItem(INode item, DragEventArgs args)
        {
            List<INode> nodes = _viewModel.FileManager.TreeView.SelectedNodes.ToList();
            PlaceNode(nodes, item);
            GetSortedCategories(item);
            FillNodes(item, nodes);
            await Task.Delay(100);
            BringIntoView(nodes[0]);
        }

        private void PlaceNode(List<INode> nodes, INode targetNode)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].Parent != null)
                {
                    nodes[i].Parent.Children.Remove(nodes[i]);
                }
                else
                {
                    _viewModel.FileManager.TreeView.Nodes.Remove(nodes[i]);
                }
                targetNode.Children.Add(nodes[i]);
                _viewModel.FileManager.DataGrid.SavedFilesProperties
                    .Find(x => x.Node == nodes[i]).Node.Parent = targetNode;
                nodes[i].Parent = targetNode;
                nodes[i].Parent.IsExpanded = true;
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
            _viewModel.FileManager.TreeView.SelectedNodes.AddRange(nodes);
        }
    }
}
