using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using DatabaseTask.Services.Interactions;
using DatabaseTask.Services.TreeViewItemLogic.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System;

namespace DatabaseTask.Services.TreeViewItemLogic
{
    internal class TreeViewItemInteractions : BaseElementInteractionHandlers, ITreeViewItemInteractions
    {

        private readonly ITreeViewData _data;

        public EventHandler<PointerPressedEventArgs> PressedEvent { get; }

        public EventHandler<PointerEventArgs> PointerMovedEvent { get; }

        public EventHandler<PointerReleasedEventArgs> ReleasedEvent { get; }

        public TreeViewItemInteractions(ITreeViewData data)
        {
            _data = data;
            PressedEvent += OnPointerPressed;
            PointerMovedEvent += OnPointerMoved;
            ReleasedEvent += OnPointerReleased;
        }

        protected override void OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if (e.Source is Control control &&
                control.DataContext is INode node
                && e.GetCurrentPoint(_data.Window).Properties.IsLeftButtonPressed)
            {
                OnPointerPressedImpl(node, e);
                return;
            }
            e.Handled = true;
        }

        private void OnPointerPressedImpl(INode node, PointerPressedEventArgs e)
        {
            _data.IsPressed = true;
            _data.DragStartPosition = e.GetPosition(_data.Window);
            e.Handled = true;
        }

        protected override void OnPointerMoved(object? sender, PointerEventArgs e)
        {
            if (_data.IsDragging || !_data.IsPressed || e.Source is not Control control)
            {
                e.Handled = true;
                return;
            }

            OnPointerMovedImpl(e);
        }

        private void OnPointerMovedImpl(PointerEventArgs e)
        {
            Point currentPosition = e.GetPosition(_data.Window);
            Point diff = _data.DragStartPosition - currentPosition;

            if (Math.Abs(diff.X) > _data.DragThreshold || Math.Abs(diff.Y) > _data.DragThreshold)
            {
                OnPointerMovedOverThreshold(e);
            }
            e.Handled = true;
        }

        private void OnPointerMovedOverThreshold(PointerEventArgs e)
        {
            DataObject data = new DataObject();
            data.Set(_data.DataFormat, null);
            DragDrop.DoDragDrop(e, data, DragDropEffects.Move);
            _data.IsDragging = false;
            _data.IsPressed = false;
        }

        protected override void OnPointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            OnPointerReleasedImpl();
        }

        private void OnPointerReleasedImpl()
        {
            _data.IsPressed = false;
            _data.IsDragging = false;
            _data.DragStartPosition = new Point();
        }
    }
}
