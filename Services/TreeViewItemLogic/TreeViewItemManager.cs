using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.VisualTree;
using DatabaseTask.Services.TreeViewItemLogic.InteractionData.Interfaces;
using DatabaseTask.Services.TreeViewItemLogic.Interactions.Interfaces;
using DatabaseTask.Services.TreeViewItemLogic.Interfaces;
using DatabaseTask.Services.TreeViewItemLogic.TreeDragDrop.Interfaces;
using System;

namespace DatabaseTask.Services.TreeViewItemLogic
{
    public class TreeViewItemManager : ITreeViewItemManager
    {
        private readonly ITreeViewItemInteractions _treeViewInteractions;
        private readonly ITreeViewItemDragDrop _treeViewDragDrop;
        private readonly ITreeViewData _treeViewItemInteractionData;

        public EventHandler<ContainerPreparedEventArgs> ContainerPreparedEvent { get; }

        public TreeViewItemManager(
            ITreeViewItemInteractions treeViewInteractions,
            ITreeViewItemDragDrop treeViewDragDrop,
            ITreeViewData treeViewItemInteractionData)
        {
            _treeViewInteractions = treeViewInteractions;
            _treeViewDragDrop = treeViewDragDrop;
            _treeViewItemInteractionData = treeViewItemInteractionData;
            ContainerPreparedEvent += OnContainerPrepared;
        }

        public void Initialize(TreeView control, Window window)
        {
            _treeViewItemInteractionData.Control = control;
            _treeViewItemInteractionData.Window = window;
        }

        public void InitializeScrollViewer(ScrollViewer scroll)
        {
            _treeViewItemInteractionData.ScrollViewer = scroll;
        }

        private void OnContainerPrepared(object? sender, ContainerPreparedEventArgs e)
        {
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

        private void SetSettingsForChild(Control control)
        {
            DragDrop.SetAllowDrop(control, false);
            control.IsHitTestVisible = false;
            DisableDragDropForChildren(control);
        }

        private void AddHandlers(TreeViewItem item)
        {
            item.PointerPressed += (object? sender, PointerPressedEventArgs e) =>
            { _treeViewInteractions.PressedEvent?.Invoke(sender, e); e.Handled = false; };
            item.PointerMoved += (object? sender, PointerEventArgs e) =>
            { _treeViewInteractions.PointerMovedEvent?.Invoke(sender, e); e.Handled = false; };
            item.PointerReleased += (object? sender, PointerReleasedEventArgs e) =>
            { _treeViewInteractions.ReleasedEvent?.Invoke(sender, e); e.Handled = false; };
            item.AddHandler(DragDrop.DragEnterEvent, (object? sender, DragEventArgs e) =>
            { _treeViewDragDrop.DragEnterEvent?.Invoke(sender, e); e.Handled = false; });
            item.AddHandler(DragDrop.DragLeaveEvent, (object? sender, DragEventArgs e) =>
            { _treeViewDragDrop.DragLeaveEvent?.Invoke(sender, e); e.Handled = false; });
            item.AddHandler(DragDrop.DragOverEvent, (object? sender, DragEventArgs e) =>
            { _treeViewDragDrop.DragOverEvent?.Invoke(sender, e); e.Handled = false; });
            item.AddHandler(DragDrop.DropEvent, (object? sender, DragEventArgs e) =>
            { _treeViewDragDrop.DropEvent?.Invoke(sender, e); e.Handled = false; });
        }
    }
}
