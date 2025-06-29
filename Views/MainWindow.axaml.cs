using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using DatabaseTask.ViewModels;
using DatabaseTask.ViewModels.Nodes;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace DatabaseTask.Views
{
    public partial class MainWindow : Window
    {
        private NodeViewModel? _draggedNode;
        private Point _dragStartPosition;
        private const int DragThreshold = 3;
        private bool _isDragging;
        private IDisposable? _pointerCapture;
        private TreeViewItem _lastDragItem;

        public MainWindow()
        {
            InitializeComponent();
            //TreeViewControl.AddHandler(PointerPressedEvent, OnPointerPressed, RoutingStrategies.Tunnel);
            //TreeViewControl.AddHandler(PointerMovedEvent, OnPointerMoved);
            //TreeViewControl.AddHandler(PointerReleasedEvent, OnPointerReleased);
            //AddHandler(DragDrop.DragOverEvent, OnDragOver, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
            //AddHandler(DragDrop.DropEvent, OnDrop, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
            //AddHandler(DragDrop.DragLeaveEvent, OnDragLeave, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
            TreeViewControl.ContainerPrepared += Test;
        }

        private void Test(object sender, ContainerPreparedEventArgs e)
        {
            if (e.Container is TreeViewItem item)
            {
                DisableDragDropForChildren(item);

                DragDrop.SetAllowDrop(item, true);
                item.AddHandler(PointerPressedEvent, OnPointerPressed);
                item.AddHandler(DragDrop.DragOverEvent, OnDragOver);
                item.AddHandler(DragDrop.DropEvent, OnDrop);
                item.AddHandler(DragDrop.DragLeaveEvent, OnDragLeave);
                item.AddHandler(DragDrop.DragEnterEvent, OnItemDragEnter);
                item.AddHandler(PointerReleasedEvent, OnPointerReleased);
                item.AddHandler(PointerMovedEvent, OnPointerMoved);
            }
        }

        private void OnItemDragEnter(object? sender, DragEventArgs e)
        {
            if (GetNodeFromEvent(e) is NodeViewModel targetNode &&
                e.Data.Contains("NODE") &&
                e.Data.Get("NODE") is NodeViewModel draggedNode)
            {
                if (sender is TreeViewItem item)
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

                    _lastDragItem = item;
                    e.Handled = true;
                }
            }
        }

        private void DisableDragDropForChildren(Control parent)
        {
            foreach (var child in parent.GetVisualChildren())
            {
                if (child is Control control)
                {
                    DragDrop.SetAllowDrop(control, false);
                    control.IsHitTestVisible = false;
                    //control.AddHandler(DragDrop.DragOverEvent, (s, e) => e.Handled = true);
                    //control.AddHandler(DragDrop.DragEnterEvent, (s, e) => e.Handled = true);
                    DisableDragDropForChildren(control);
                }
            }
        }

        private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if (e.Source is Control control &&
                control.DataContext is NodeViewModel node && e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            {
                _draggedNode = node;
                System.Diagnostics.Debug.WriteLine("Pressed");
                _dragStartPosition = e.GetPosition(this);

                e.Pointer.Capture(sender as TreeViewItem);
                e.Handled = true;

            }
            e.Handled = false;
        }

        private void OnPointerMoved(object? sender, PointerEventArgs e)
        {
            if (_draggedNode == null || _isDragging || e.Source is not Control control)
            {
                e.Handled = false;
                return;
            }


            var currentPosition = e.GetPosition(this);
            var diff = _dragStartPosition - currentPosition;

            if (Math.Abs(diff.X) > DragThreshold || Math.Abs(diff.Y) > DragThreshold)
            {
                var data = new DataObject();
                data.Set("NODE", _draggedNode);
                System.Diagnostics.Debug.WriteLine("Do Drag Move");
                e.Pointer.Capture(null);
                var result = DragDrop.DoDragDrop(e, data, DragDropEffects.Move);

                if (result.Result != DragDropEffects.None)
                {
                    
                }
                e.Handled = true;
            }
            e.Handled = true;
        }

        private void OnPointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            _draggedNode = null;
            System.Diagnostics.Debug.WriteLine("Released");
            //ClearDropHighlight();
        }

        private void OnDragOver(object? sender, DragEventArgs e)
        {
            _isDragging = true;
            TreeViewItem item = null;
            if (e.Source is Control control)
            {
                _lastDragItem = control.FindAncestorOfType<TreeViewItem>();
            }
            if (GetNodeFromEvent(e) is NodeViewModel targetNode &&
                e.Data.Contains("NODE") &&
                e.Data.Get("NODE") is NodeViewModel draggedNode)
            {

                if (CanDrop(draggedNode, targetNode))
                {
                    System.Diagnostics.Debug.WriteLine("Do Highlight");
                    e.DragEffects = DragDropEffects.Move;
                    //if (e.Source is Control control)
                    //{
                    //    var item = control.FindAncestorOfType<TreeViewItem>();
                    //    DragDrop.SetAllowDrop(item, true);
                    //}
                    SetDropHighlight(e);
                    e.Handled = true;
                    return;
                }
                System.Diagnostics.Debug.WriteLine($"Can not Drop {draggedNode.Name}, {targetNode.Name}");
            }
            System.Diagnostics.Debug.WriteLine("Forbidden Highlight");
            e.DragEffects = DragDropEffects.None;
            var t = GetVisualAtPosition(e.GetPosition(this));
            //DragDrop.SetAllowDrop(e.Source as Control, false);
            e.Handled = true;
        }

        private void OnDrop(object? sender, DragEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Drop");
            var targetNode = GetNodeFromEvent(e);
            if (targetNode == null ||
                !e.Data.Contains("NODE") ||
                e.Data.Get("NODE") is not NodeViewModel draggedNode ||
                DataContext is not MainWindowViewModel vm)
            {
                return;
            }

            if (!CanDrop(draggedNode, targetNode))
            {
                e.DragEffects = DragDropEffects.None;
                e.Handled = true;
                return;
            }

            if (draggedNode.Parent != null)
            {
                draggedNode.Parent.Children.Remove(draggedNode);
            }
            else
            {
                vm._getTreeNodes.TreeView.Nodes.Remove(draggedNode);
            }
            targetNode.Children.Add(draggedNode);
            draggedNode.Parent = targetNode;
            targetNode.IsExpanded = true;

            _draggedNode = null;
            e.DragEffects = DragDropEffects.Move;
 
            _isDragging = false;

            TreeViewItem item = null;
            if (e.Source is Control control)
            {
                item = control.FindAncestorOfType<TreeViewItem>();
            }
            ClearDropHighlight(item);
            e.Handled = true;
        }

        private void OnDragLeave(object? sender, DragEventArgs e)
        {
            ClearDropHighlight(_lastDragItem);

            _lastDragItem = null;
            e.Handled = true;
        }

        private NodeViewModel? GetNodeFromEvent(RoutedEventArgs e)
        {
            if (e.Source is Control control)
            {
                var item = control.FindAncestorOfType<TreeViewItem>();
                return item?.DataContext as NodeViewModel;
            }
            return null;
        }

        private bool CanDrop(NodeViewModel source, NodeViewModel target)
        {
            if (source == target)
            {
                return false;
            }

            var parent = target.Parent;
            while (parent != null)
            {
                if (parent == source) return false;
                parent = parent.Parent;
            }

            return true;
        }

        private TreeViewItem GetVisualAtPosition(Point point)
        {
            var visuals = TreeViewControl.GetVisualsAt(point);

            foreach (var visual in visuals)
            {
                if (visual is TreeViewItem item)
                {
                    return item;
                }

                var parent = visual.FindAncestorOfType<TreeViewItem>();
                if (parent != null)
                {
                    return parent;
                }
            }

            return null;
        }

        private void SetDropHighlight(DragEventArgs e)
        {
            var positionInTree = e.GetPosition(TreeViewControl);
            var container = GetVisualAtPosition(positionInTree) as TreeViewItem;

            if (container == null || container.DataContext is not NodeViewModel targetNode)
            {
                return;
            }

            var positionInContainer = e.GetPosition(container);
            var bounds = container.Bounds;

            if (bounds.Height <= 0)
            {
                bounds = new Rect(bounds.Position, container.DesiredSize);

                if (bounds.Height <= 0)
                {
                    return;
                }
            }

            if (positionInContainer.Y < bounds.Height * 0.3)
            {
                container.Classes.Add("drop-above");
                container.Classes.Remove("drop-below");
                container.Classes.Remove("drop-inside");
            }
            else if (positionInContainer.Y > bounds.Height * 0.7)
            {
                container.Classes.Remove("drop-above");
                container.Classes.Add("drop-below");
                container.Classes.Remove("drop-inside");
            }
            else
            {
                //_dropPosition = DropPosition.None;
            }
        }
        

        private void ClearDropHighlight(TreeViewItem container)
        {
            container.Classes.Remove("drop-above");
            container.Classes.Remove("drop-below");
            container.Classes.Remove("drop-inside");
        }
    }
}
