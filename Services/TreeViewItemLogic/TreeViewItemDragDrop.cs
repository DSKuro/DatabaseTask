using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using DatabaseTask.Services.Interactions;
using DatabaseTask.Services.TreeViewItemLogic.Interfaces;
using DatabaseTask.ViewModels;
using DatabaseTask.ViewModels.Nodes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseTask.Services.TreeViewItemLogic
{
    public class TreeViewItemDragDrop : BaseDragDropHandlers, ITreeViewItemDragDrop
    {
        private readonly ITreeViewData _data;
        private readonly MainWindowViewModel _viewModel;

        public EventHandler<DragEventArgs> DragEnterEvent { get; }
        public EventHandler<DragEventArgs> DragLeaveEvent { get; }
        public EventHandler<DragEventArgs> DragOverEvent { get; }
        public EventHandler<DragEventArgs> DropEvent { get; }

        public TreeViewItemDragDrop(ITreeViewData data, MainWindowViewModel viewModel)
        {
            _data = data;
            _viewModel = viewModel;
            DragEnterEvent += OnDragEnter;
            DragLeaveEvent += OnDragLeave;
            DragOverEvent += OnDragOver;
            DropEvent += OnDrop;
        }

        protected override void OnDragEnter(object? sender, DragEventArgs e)
        {
            if (GetNodeFromEvent(e) is INode targetNode &&
                e.Data.Contains(_data.DataFormat))
            {

                OnDragEnterImpl(sender, targetNode, e);
            }
        }

        private INode? GetNodeFromEvent(RoutedEventArgs e)
        {
            if (e.Source is Control control)
            {
                TreeViewItem item = control.FindAncestorOfType<TreeViewItem>();
                _data.DraggedItemView = item;
                return item?.DataContext as INode;
            }
            return null;
        }

        private void OnDragEnterImpl(object? sender, INode targetNode,
            DragEventArgs e)
        {
            if (e.Source is Control control)
            {
                Debug.WriteLine($"Enter {((NodeViewModel) targetNode).Name}, {_data.DraggedItemView}");
                OnCanDropImpl(targetNode, e);
                e.Handled = true;
            }
        }

        private void OnCanDropImpl(INode targetNode,
            DragEventArgs e)
        {
            if (CanDrop(targetNode))
            {
                e.DragEffects = DragDropEffects.Move;
                Debug.WriteLine($"{((NodeViewModel)targetNode).Name}");
                SetDropHighlight(e);
            }
            else
            {
                e.DragEffects = DragDropEffects.None;
            }
        }

        private bool CanDrop(INode target)
        {
            foreach (INode item in _viewModel.GetTreeNodes.TreeView.SelectedNodes)
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

        private void SetDropHighlight(DragEventArgs e)
        {
            Point positionInTree = e.GetPosition(_data.Control);
            Visual container = GetVisualAtPosition(positionInTree);

            if (container == null || container.DataContext is not INode targetNode)
            {
                return;
            }

            SetDropHighLightImpl(e, container);
        }

        private TreeViewItem GetVisualAtPosition(Point point)
        {
            IEnumerable<Visual> visuals = _data.Control.GetVisualsAt(point);
            return GetVisualImpl(visuals);
        }

        private TreeViewItem GetVisualImpl(IEnumerable<Visual> visuals)
        {
            foreach (Visual visual in visuals)
            {
                if (visual is TreeViewItem item)
                {
                    return item;
                }

                TreeViewItem parent = visual.FindAncestorOfType<TreeViewItem>();
                if (parent != null)
                {
                    return parent;
                }
            }

            return null;
        }

        private void SetDropHighLightImpl(DragEventArgs e, Visual container)
        {
            Point positionInContainer = e.GetPosition(container);
            Rect bounds = container.Bounds;
            if (!container.Classes.Contains("drop")) 
            {
                container.Classes.Add("drop");
            }

        }

        protected override void OnDragLeave(object? sender, DragEventArgs e)
        {
            Debug.WriteLine($"Leave {_data.DraggedItemView}");

            if (_data.DraggedItemView != null)
            {
                _data.DraggedItemView.Classes.Remove("drop");
                _data.DraggedItemView = null;
            }

            e.Handled = true;
        }

        protected override void OnDragOver(object? sender, DragEventArgs e)
        {
            _data.IsDragging = true;
            if (GetNodeFromEvent(e) is INode targetNode &&
                e.Data.Contains(_data.DataFormat))
            {
                if (CanDrop(targetNode))
                {
                    //Debug.WriteLine("Drag over");
                    OnDragOverImpl(e, DragDropEffects.Move);
                    ScrollToDropItem(e);
                    return;
                }
            }
            e.DragEffects = DragDropEffects.None;
            ScrollToDropItem(e);
            e.Handled = true;
        }

        private void ScrollToDropItem(DragEventArgs e)
        {
            _data.DragStartPosition = e.GetPosition(_data.Control);
            bool isInBottomZone = _data.DragStartPosition.Y > (_data.Control.Bounds.Height - _data.DraggedItemView.Bounds.Height - 5);
            bool isInTopZone = _data.DragStartPosition.Y < _data.DraggedItemView.Bounds.Height - 5;
            if (isInBottomZone || isInTopZone)
            {
                double newOffset = _data.ScrollViewer.Offset.Y + (_data.DraggedItemView.Bounds.Height * ((isInTopZone) ? -1 : 1));
                newOffset = Math.Clamp(newOffset, 0, _data.ScrollViewer.ScrollBarMaximum.Y);
                _data.ScrollViewer.Offset = new Vector(_data.ScrollViewer.Offset.X, newOffset);
            }
        }

        private void OnDragOverImpl(DragEventArgs e, DragDropEffects effect)
        {
            e.DragEffects = effect;
            SetDropHighlight(e);
            e.Handled = true;
        }

        protected override void OnDrop(object? sender, DragEventArgs e)
        {
            Debug.WriteLine("Drop");
            INode targetNode = GetNodeFromEvent(e);
            if (targetNode == null ||
                !e.Data.Contains(_data.DataFormat))
            {
                //DraggedNode = null;
                OnImpossibleDrop(e);
                return;
            }

            if (!CanDrop(targetNode))
            {
                //DraggedNode = null;
                e.DragEffects = DragDropEffects.None;
                OnImpossibleDrop(e);
                return;
            }

            OnDropImpl(targetNode, e);
        }

        private void OnDropImpl(INode targetNode, DragEventArgs e)
        {
            Drag(targetNode, e);

            //DraggedNode = null;
            e.DragEffects = DragDropEffects.Move;
            SetData();
            e.Handled = true;
        }

        private void SetData()
        {
            _data.IsDragging = false;
            _data.DraggedItemView.Classes.Remove("drop");
            _data.IsPressed = false;
        }

        private void OnImpossibleDrop(DragEventArgs e)
        {
            _data.IsDragging = false;
            _data.IsPressed = false;
            e.Handled = true;
        }

        private async void Drag(INode targetNode, DragEventArgs e)
        {
            List<INode> nodes = _viewModel.GetTreeNodes.TreeView.SelectedNodes.ToList();
            PlaceNode(nodes, targetNode);
            (List<NodeViewModel?> sortedFolders, List<NodeViewModel?> sortedFiles) =
                GetSortedCategories(targetNode);
            FillNodes(targetNode, sortedFolders, sortedFiles, nodes);
            BringIntoView(nodes[0]);
        }

        private async void BringIntoView(INode node)
        {
            await Task.Delay(100);
            TreeViewItem? treeItem = GetTreeViewItemForNode(node);
            treeItem.BringIntoView();
        }
        
        private (List<NodeViewModel?>, List<NodeViewModel?>) GetSortedCategories(INode targetNode)
        {
            IEnumerable<NodeViewModel> sorted = targetNode.Children.Select(x => (NodeViewModel)x);
            List<NodeViewModel?> sortedFolders = sorted.Where(x => x.IsFolder).OrderBy(x => x.Name).ToList();
            List<NodeViewModel?> sortedFiles = sorted.Where(x => !x.IsFolder).OrderBy(x => x.Name).ToList();
            return (sortedFolders, sortedFiles);
        }

        private void FillNodes(INode targetNode,
            List<NodeViewModel?> sortFolders,
            List<NodeViewModel?> sortFiles,
            List<INode> nodes)
        {
            targetNode.Children.Clear();
            targetNode.Children.AddRange(sortFolders);
            targetNode.Children.AddRange(sortFiles);
            _viewModel.GetTreeNodes.TreeView.SelectedNodes.AddRange(nodes);
        }

        private void PlaceNode(List<INode> nodes, INode targetNode)
        {
            foreach (INode item in nodes)
            {
                if (item.Parent != null)
                {
                    item.Parent.Children.Remove(item);
                }
                else
                {
                    _viewModel.GetTreeNodes.TreeView.Nodes.Remove(item);
                }
                targetNode.Children.Add(item);
                item.Parent = targetNode;
                item.Parent.IsExpanded = true;
            }
        }

        public TreeViewItem? GetTreeViewItemForNode(INode node)
        {
            var treeView = _viewModel.GetTreeNodes.TreeView;
            return FindTreeViewItemRecursive(_data.Control, node);
        }

        private TreeViewItem? FindTreeViewItemRecursive(Visual parent, INode node)
        {
            if (parent is TreeViewItem item && item.DataContext == node)
            {
                return item;
            }

            return FindItemInChildren(parent, node);
        }

        private TreeViewItem? FindItemInChildren(Visual parent, INode node)
        {
            IEnumerable<Visual> children = parent.GetVisualChildren();
            foreach (Visual child in children)
            {
                if (child is Visual visualChild)
                {
                    TreeViewItem? result = FindTreeViewItemRecursive(visualChild, node);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }
    }
}

