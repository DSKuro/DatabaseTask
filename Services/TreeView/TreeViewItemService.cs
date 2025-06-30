using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Remote.Protocol.Input;
using Avalonia.VisualTree;
using DatabaseTask.ViewModels;
using DatabaseTask.ViewModels.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace DatabaseTask.Services.TreeView
{
    public class TreeViewItemService : ITreeViewItem
    {
        private bool Pressed = false;

        public ScrollViewer _scrollViewer { get; set; }
        public Visual _mainWindow { get; set; }
        public Visual _treeViewControl { get; set; }

        public Visual PressedItem { get; set; }
        public string DataFormat { get; set; } = "NODE";
        public bool IsDragging { get; set; }
        public int DragThreshold { get; set; } = 3;
        public Point DragStartPosition { get; set; }
        public INode DraggedNode { get; set; }

        public EventHandler<PointerPressedEventArgs> PressedEvent { get; }
        public EventHandler<PointerEventArgs> PointerMovedEvent { get; }
        public EventHandler<PointerReleasedEventArgs> ReleasedEvent { get; }

        public EventHandler<DragEventArgs> DragEnterEvent { get; }
        public EventHandler<DragEventArgs> DragLeaveEvent { get; }
        public EventHandler<DragEventArgs> DragOverEvent { get; }
        public EventHandler<DragEventArgs> DropEvent { get; }
        public MainWindowViewModel MainWindowViewModel { get; set; }

        public TreeViewItemService()
        {
            PressedEvent += OnPointerPressed;
            PointerMovedEvent += OnPointerMoved;
            ReleasedEvent += OnPointerReleased;
            DragEnterEvent += OnDragEnter;
            DragLeaveEvent += OnDragLeave;
            DragOverEvent += OnDragOver;
            DropEvent += OnDrop;
        }

        public void OnContainerPrepared(object sender, ContainerPreparedEventArgs e)
        {
            _scrollViewer = _treeViewControl.FindDescendantOfType<ScrollViewer>();
            if (e.Container is TreeViewItem item)
            {
                SetSettingForContainer(item);
            }
        }

        private void SetSettingForContainer(TreeViewItem item)
        {
            DisableDragDropForChildren(item);
            DragDrop.SetAllowDrop(item, true);
            AddHandlers(item);
        }

        private void DisableDragDropForChildren(Control parent)
        {
            foreach (Visual child in parent.GetVisualChildren())
            {
                if (child is Control control)
                {
                    SetSettingsForChild(control);
                }
            }
        }

        private void AddHandlers(TreeViewItem item)
        {
            item.PointerPressed += (object? sender, PointerPressedEventArgs e)
                =>
            { PressedEvent?.Invoke(sender, e); e.Handled = false; };
            item.PointerMoved += (object? sender, PointerEventArgs e)
                =>
            { PointerMovedEvent?.Invoke(sender, e); e.Handled = false; };
            item.PointerReleased += (object? sender, PointerReleasedEventArgs e)
                =>
            { ReleasedEvent?.Invoke(sender, e); e.Handled = false; };
            item.AddHandler(DragDrop.DragEnterEvent, (object? sender, DragEventArgs e) =>
            { DragEnterEvent?.Invoke(sender, e); e.Handled = false; });
            item.AddHandler(DragDrop.DragLeaveEvent, (object? sender, DragEventArgs e) =>
            { DragLeaveEvent?.Invoke(sender, e); e.Handled = false; });
            item.AddHandler(DragDrop.DragOverEvent, (object? sender, DragEventArgs e) =>
            { DragOverEvent?.Invoke(sender, e); e.Handled = false; });
            item.AddHandler(DragDrop.DropEvent, (object? sender, DragEventArgs e) =>
            { DropEvent?.Invoke(sender, e); e.Handled = false; });
        }

        private void SetSettingsForChild(Control control)
        {
            DragDrop.SetAllowDrop(control, false);
            control.IsHitTestVisible = false;
            DisableDragDropForChildren(control);
        }

        public void OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            Debug.WriteLine($"Pressed");
            if (e.Source is Control control &&
                control.DataContext is INode node 
                && e.GetCurrentPoint(_mainWindow).Properties.IsLeftButtonPressed)
            {
                //Debug.WriteLine($"Pressed");
                OnPointerPressedImpl(node, e);
                return;
            }
            Pressed = true;
            e.Handled = true;
        }

        private void OnPointerPressedImpl(INode node, PointerPressedEventArgs e)
        {
            DraggedNode = node;
            DragStartPosition = e.GetPosition(_mainWindow);
            e.Handled = true;
        }

        public void OnPointerMoved(object? sender, PointerEventArgs e)
        {
            //Debug.WriteLine($"{DraggedNode}, {IsDragging}");
            if (DraggedNode == null || IsDragging || e.Source is not Control control)
            {
                e.Handled = true;
                return;
            }

            OnPointerMovedImpl(e);

            e.Handled = true;
        }

        private void OnPointerMovedImpl(PointerEventArgs e)
        {
            Point currentPosition = e.GetPosition(_mainWindow);
            Point diff = DragStartPosition - currentPosition;

            if (Math.Abs(diff.X) > DragThreshold || Math.Abs(diff.Y) > DragThreshold)
            {
                OnPointerMovedOverThreshold(e);
            }
        }

        private void OnPointerMovedOverThreshold(PointerEventArgs e)
        {
            DataObject data = new DataObject();
            data.Set(DataFormat, DraggedNode);
            DragDrop.DoDragDrop(e, data, DragDropEffects.Move);
            IsDragging = false;
            DraggedNode = null;
            Console.WriteLine();
        }

        public void OnPointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            Debug.WriteLine($"Released");
            OnPointerReleasedImpl();
        }

        private void OnPointerReleasedImpl()
        {
            DraggedNode = null;
            IsDragging = false;
            DragStartPosition = new Point();
        }

        public void OnDragEnter(object? sender, DragEventArgs e)
        {
            if (GetNodeFromEvent(e) is INode targetNode &&
                e.Data.Contains(DataFormat) &&
                e.Data.Get(DataFormat) is INode draggedNode)
            {
                OnDragEnterImpl(sender, targetNode, draggedNode, e);
            }
        }

        private void OnDragEnterImpl(object? sender, INode targetNode, INode draggedNode,
            DragEventArgs e)
        {
            if (e.Source is Control control)
            {
                OnCanDropImpl(targetNode, draggedNode, e);
                e.Handled = true;
            }
        }

        private void OnCanDropImpl(INode targetNode, INode draggedNode,
            DragEventArgs e)
        {
            if (CanDrop(draggedNode, targetNode))
            {
                e.DragEffects = DragDropEffects.Move;
                SetDropHighlight(e);
            }
            else
            {
                e.DragEffects = DragDropEffects.None;
            }
        }

        private INode? GetNodeFromEvent(RoutedEventArgs e)
        {
            if (e.Source is Control control)
            {
                TreeViewItem item = control.FindAncestorOfType<TreeViewItem>();
                return item?.DataContext as INode;
            }
            return null;
        }

        private bool CanDrop(INode source, INode target)
        {
            foreach (var item in MainWindowViewModel._getTreeNodes.TreeView.SelectedNodes)
            {
                if (item == target)
                {
                    return false;
                }

                NodeViewModel node = target as NodeViewModel;

                if ( !(IsTargetAboveSource(item, target) && node.IsFolder && item.Parent != target))
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
            Point positionInTree = e.GetPosition(_treeViewControl);
            Visual container = GetVisualAtPosition(positionInTree);

            if (container == null || container.DataContext is not INode targetNode)
            {
                return;
            }

            SetDropHighLightImpl(e, container);
        }

        private void SetDropHighLightImpl(DragEventArgs e, Visual container)
        {
            Point positionInContainer = e.GetPosition(container);
            Rect bounds = container.Bounds;

            AddClassesAbove(container);
        }

        private void AddClassesAbove(Visual container)
        {
            container.Classes.Add("drop-above");
            container.Classes.Remove("drop-below");
            container.Classes.Remove("drop-inside");
        }
        
        private TreeViewItem GetVisualAtPosition(Point point)
        {
            IEnumerable<Visual> visuals = _treeViewControl.GetVisualsAt(point);
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

        public void OnDragLeave(object? sender, DragEventArgs e)
        {
            Debug.WriteLine($"Leave");
            //Debug.WriteLine("Leave");
            if (Pressed)
            {
                Pressed = false;
            }
            
            if (PressedItem != null)
            {
                ClearDropHighlight(PressedItem);
                PressedItem = null;
            }
  

            e.Handled = true;
        }

        private void ClearDropHighlight(Visual container)
        {
            container.Classes.Remove("drop-above");
            container.Classes.Remove("drop-below");
            container.Classes.Remove("drop-inside");
        }

        public void OnDragOver(object? sender, DragEventArgs e)
        {
            IsDragging = true;
            Debug.WriteLine($"Over");
            if (e.Source is Control control)
            {
                PressedItem = control.FindAncestorOfType<TreeViewItem>();
            }

            bool isInBottomZone;
            bool isInTopZone;
            if (GetNodeFromEvent(e) is INode targetNode &&
                e.Data.Contains("NODE") &&
                e.Data.Get("NODE") is INode draggedNode)
            {
                if (CanDrop(draggedNode, targetNode))
                {
                    OnDragOverImpl(e);
                    DragStartPosition = e.GetPosition(_treeViewControl);
                    isInBottomZone = DragStartPosition.Y > _treeViewControl.Bounds.Height - 5;
                    isInTopZone = DragStartPosition.Y < 5;
                    Debug.WriteLine($"Scroll: {DragStartPosition.Y}: {_treeViewControl.Bounds.Height}");
                    if (isInBottomZone || isInTopZone)
                    {
                        _scrollViewer.Offset = new Vector(_scrollViewer.Offset.X, _scrollViewer.Offset.Y + (30 * ((isInTopZone) ? -1 : 1)));
                    }
                    return;
                }
            }
            e.DragEffects = DragDropEffects.None;
            e.Handled = true;
            DragStartPosition = e.GetPosition(_treeViewControl);
             isInBottomZone = DragStartPosition.Y > _treeViewControl.Bounds.Height -5;
            isInTopZone = DragStartPosition.Y < 5;
            Debug.WriteLine($"Scroll: {DragStartPosition.Y}: {_treeViewControl.Bounds.Height}");
            if (isInBottomZone || isInTopZone)
            {
                _scrollViewer.Offset = new Vector(_scrollViewer.Offset.X,_scrollViewer.Offset.Y + (30 * ((isInTopZone) ? -1 : 1)));
            }

        }

        private void OnDragOverImpl(DragEventArgs e)
        {
            e.DragEffects = DragDropEffects.Move;
            SetDropHighlight(e);
            e.Handled = true;
        }

        public void OnDrop(object? sender, DragEventArgs e)
        {
            Debug.WriteLine($"Drop");
            INode targetNode = GetNodeFromEvent(e);
            if (targetNode == null ||
                !e.Data.Contains("NODE") ||
                e.Data.Get("NODE") is not INode draggedNode)
            {
                DraggedNode = null;
                IsDragging = false;
                e.Handled = true;
                return;
            }

            if (!CanDrop(draggedNode, targetNode))
            {
                DraggedNode = null;
                IsDragging = false;
                e.DragEffects = DragDropEffects.None;
                e.Handled = true;
                return;
            }

            Drag(targetNode, draggedNode);

            DraggedNode = null;
            e.DragEffects = DragDropEffects.Move;
            IsDragging = false;
            ClearDropHighlight(PressedItem);
            Pressed = false;
            e.Handled = true;
        }

        private void Drag(INode targetNode, INode draggedNode)
        {
            List<INode> nodes = MainWindowViewModel._getTreeNodes.TreeView.SelectedNodes.ToList();
            foreach (var item in nodes)
            {
                if (item.Parent != null)
                {
                    item.Parent.Children.Remove(item);
                }
                else
                {
                    MainWindowViewModel._getTreeNodes.TreeView.Nodes.Remove(item);
                }
                targetNode.Children.Add(item);
                item.Parent = targetNode;
                item.Parent.IsExpanded = true;
                MainWindowViewModel._getTreeNodes.TreeView.SelectedNodes.Add(item);
            }
          

        }


    }
}
