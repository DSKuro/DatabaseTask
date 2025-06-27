using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using DatabaseTask.ViewModels;
using System;

namespace DatabaseTask.Views
{
    public partial class MainWindow : Window
    {
        private NodeViewModel? _draggedNode;
        private Point _dragStartPosition;
        private const int DragThreshold = 3;

        public MainWindow()
        {
            InitializeComponent();
            // �������� �� ������� ��� ����� TreeVie
            TreeViewControl.AddHandler(PointerPressedEvent, OnPointerPressed, RoutingStrategies.Tunnel);
            TreeViewControl.AddHandler(PointerMovedEvent, OnPointerMoved, RoutingStrategies.Tunnel);
            TreeViewControl.AddHandler(PointerReleasedEvent, OnPointerReleased, RoutingStrategies.Tunnel);
            AddHandler(DragDrop.DragOverEvent, OnDragOver, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
            AddHandler(DragDrop.DropEvent, OnDrop, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
            AddHandler(DragDrop.DragLeaveEvent, OnDragLeave, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        }

        private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if (e.Source is Control control &&
                control.DataContext is NodeViewModel node)
            {
                _draggedNode = node;
                _dragStartPosition = e.GetPosition(this);
                e.Handled = false;
            }
        }

        private void OnPointerMoved(object? sender, PointerEventArgs e)
        {
            if (_draggedNode == null || e.Source is not Control control)
            {
                e.Handled = true;
                return;
            }


            var currentPosition = e.GetPosition(this);
            var diff = _dragStartPosition - currentPosition;

            if (Math.Abs(diff.X) > DragThreshold || Math.Abs(diff.Y) > DragThreshold)
            {
                var data = new DataObject();
                data.Set("NODE", _draggedNode);

                if (DataContext is MainWindowViewModel vm)
                {
                    vm.DraggedItem = _draggedNode;
                }

                // ���������� ��������������
                var result = DragDrop.DoDragDrop(e, data, DragDropEffects.Move);

                if (result.Result != DragDropEffects.None)
                {
                    // �������� ����������
                    _draggedNode = null;
                }
                e.Handled = true;
            }
            e.Handled = false;
        }

        private void OnPointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            _draggedNode = null;
        }

        private void OnDragOver(object? sender, DragEventArgs e)
        {
            if (GetNodeFromEvent(e) is NodeViewModel targetNode &&
                e.Data.Contains("NODE") &&
                e.Data.Get("NODE") is NodeViewModel draggedNode)
            {
                if (CanDrop(draggedNode, targetNode))
                {
                    e.DragEffects = DragDropEffects.Move;
                    e.Handled = true;

                    // ���������� ���������
                    SetDropHighlight(targetNode, e);
                    return;
                }
            }

            e.DragEffects = DragDropEffects.None;
            e.Handled = true;
        }

        private void OnDrop(object? sender, DragEventArgs e)
        {
            var targetNode = GetNodeFromEvent(e);
            if (targetNode == null ||
                !e.Data.Contains("NODE") ||
                e.Data.Get("NODE") is not NodeViewModel draggedNode ||
                DataContext is not MainWindowViewModel vm)
            {
                return;
            }

            // ��������� ���������� ��������
            if (!CanDrop(draggedNode, targetNode))
            {
                e.DragEffects = DragDropEffects.None;
                e.Handled = true;
                return;
            }

            // ������� �� ������� �����
            if (draggedNode.Parent != null)
            {
                draggedNode.Parent.Children.Remove(draggedNode);
            }
            else
            {
                vm.Nodes.Remove(draggedNode);
            }

            // ��������� � ����� �����
            targetNode.Children.Add(draggedNode);
            draggedNode.Parent = targetNode;
            targetNode.IsExpanded = true;

            // ����� ���������
            vm.DraggedItem = null;
            _draggedNode = null;
            e.DragEffects = DragDropEffects.Move;
            e.Handled = true;

            // ������� ���������
            ClearDropHighlight();
        }

        private void OnDragLeave(object? sender, DragEventArgs e)
        {
            ClearDropHighlight();
            e.Handled = true;
        }

        private NodeViewModel? GetNodeFromEvent(RoutedEventArgs e)
        {
            if (e.Source is Control control)
            {
                // ���� ��������� TreeViewItem
                var item = control.FindAncestorOfType<TreeViewItem>();
                return item?.DataContext as NodeViewModel;
            }
            return null;
        }

        private bool CanDrop(NodeViewModel source, NodeViewModel target)
        {
            // ������ ���������� �� ����
            if (source == target) return false;

            // ������ ���������� �� �������
            var parent = target.Parent;
            while (parent != null)
            {
                if (parent == source) return false;
                parent = parent.Parent;
            }

            return true;
        }

        private void SetDropHighlight(NodeViewModel target, DragEventArgs e)
        {
            // ������� ���������� �������
            var container = TreeViewControl.ContainerFromItem(target);
            if (container is TreeViewItem item)
            {
                var position = e.GetPosition(item);
                var height = item.Bounds.Height;

                // ���������� ������� ������
                if (position.Y < height * 0.3)
                {
                    // ������� ����
                    item.Classes.Add("drop-above");
                    item.Classes.Remove("drop-below");
                    item.Classes.Remove("drop-inside");
                }
                else if (position.Y > height * 0.7)
                {
                    // ������� ����
                    item.Classes.Add("drop-below");
                    item.Classes.Remove("drop-above");
                    item.Classes.Remove("drop-inside");
                }
                else
                {
                    // ������� ������
                    item.Classes.Add("drop-inside");
                    item.Classes.Remove("drop-above");
                    item.Classes.Remove("drop-below");
                }
            }
        }

        private void ClearDropHighlight()
        {
            foreach (var item in TreeViewControl.GetRealizedContainers())
            {
                if (item is TreeViewItem tvi)
                {
                    tvi.Classes.Remove("drop-above");
                    tvi.Classes.Remove("drop-below");
                    tvi.Classes.Remove("drop-inside");
                }
            }
        }
    }
}
