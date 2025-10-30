using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.VisualTree;
using DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic.InteractionData.Interfaces;
using DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic.Interactions.Interfaces;
using DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic.Interfaces;
using DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic.TreeDragDrop.Interfaces;
using System;

namespace DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic
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
            item.PointerPressed += (sender, e) =>
            { _treeViewInteractions.PressedEvent?.Invoke(sender, e); e.Handled = false; };
            item.PointerMoved += (sender, e) =>
            { _treeViewInteractions.PointerMovedEvent?.Invoke(sender, e); e.Handled = false; };
            item.PointerReleased += (sender, e) =>
            { _treeViewInteractions.ReleasedEvent?.Invoke(sender, e); e.Handled = false; };
            item.AddHandler(DragDrop.DragEnterEvent, (sender, e) =>
            { _treeViewDragDrop.DragEnterEvent?.Invoke(sender, e); e.Handled = false; });
            item.AddHandler(DragDrop.DragLeaveEvent, (sender, e) =>
            { _treeViewDragDrop.DragLeaveEvent?.Invoke(sender, e); e.Handled = false; });
            item.AddHandler(DragDrop.DragOverEvent, (sender, e) =>
            { _treeViewDragDrop.DragOverEvent?.Invoke(sender, e); e.Handled = false; });
            item.AddHandler(DragDrop.DropEvent, (sender, e) =>
            { _treeViewDragDrop.DropEvent?.Invoke(sender, e); e.Handled = false; });
        }
    }
}
