using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.VisualTree;
using DatabaseTask.Services.Interactions;
using DatabaseTask.Services.TreeViewItemLogic.Interfaces;
using DatabaseTask.ViewModels;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System;

namespace DatabaseTask.Services.TreeViewItemLogic
{
    public class TreeViewItemDragDrop : BaseDragDropHandlers, ITreeViewItemDragDrop
    {
        private readonly ITreeViewData _data;
        private readonly ITreeViewControlsHelper _controlsHelper;
        private readonly ITreeNodeOperations _nodeOperations;
        private readonly ITreeViewVisualOperations _visualOperations;
        private readonly MainWindowViewModel _viewModel;

        public EventHandler<DragEventArgs> DragEnterEvent { get; }
        public EventHandler<DragEventArgs> DragLeaveEvent { get; }
        public EventHandler<DragEventArgs> DragOverEvent { get; }
        public EventHandler<DragEventArgs> DropEvent { get; }

        public TreeViewItemDragDrop(ITreeViewData data, 
            ITreeViewControlsHelper controlsHelper, 
            ITreeNodeOperations nodeOperations,
            ITreeViewVisualOperations visualOperations,
            MainWindowViewModel viewModel)
        {
            _data = data;
            _controlsHelper = controlsHelper;
            _nodeOperations = nodeOperations;
            _visualOperations = visualOperations;
            _viewModel = viewModel;
            DragEnterEvent += OnDragEnter;
            DragLeaveEvent += OnDragLeave;
            DragOverEvent += OnDragOver;
            DropEvent += OnDrop;
        }
        protected override void OnDragEnter(object? sender, DragEventArgs e)
        {
            Control control = e.Source as Control;
            if (control != null)
            {
                _data.DraggedItemView = control.FindAncestorOfType<TreeViewItem>();
            }
            if (_controlsHelper.GetDataFromRoutedControl(e) is INode targetNode &&
                e.Data.Contains(_data.DataFormat))
            {
                OnDragEnterImpl(sender, control, targetNode, e);
            }
        }

        private void OnDragEnterImpl(object? sender,
            Control control,
            INode targetNode,
            DragEventArgs e)
        {
            if (control != null)
            {
                OnCanDropImpl(targetNode, e);
                e.Handled = true;
            }
        }

        private void OnCanDropImpl(INode targetNode,
            DragEventArgs e)
        {
            if (_nodeOperations.CanDrop(targetNode))
            {
                e.DragEffects = DragDropEffects.Move;
                _visualOperations.SetDropHighlight(e, "drop");
            }
            else
            {
                e.DragEffects = DragDropEffects.None;
            }
        }

        protected override void OnDragLeave(object? sender, DragEventArgs e)
        {
            if (_data.DraggedItemView != null)
            {
                _visualOperations.ClearDropHighlight("drop");
                _data.DraggedItemView = null;
            }

            e.Handled = true;
        }

        protected override void OnDragOver(object? sender, DragEventArgs e)
        {
            _data.IsDragging = true;
            if (_controlsHelper.GetDataFromRoutedControl(e) is INode targetNode &&
                e.Data.Contains(_data.DataFormat))
            {
                if (_nodeOperations.CanDrop(targetNode))
                {
                    OnDragOverImpl(e, DragDropEffects.Move);
                    _nodeOperations.ScrollToDroppedItem(e);
                    return;
                }
            }
            e.DragEffects = DragDropEffects.None;
            _nodeOperations.ScrollToDroppedItem(e);
            e.Handled = true;
        }

        private void OnDragOverImpl(DragEventArgs e, DragDropEffects effect)
        {
            e.DragEffects = effect;
            _visualOperations.SetDropHighlight(e, "drop");
            e.Handled = true;
        }

        protected override void OnDrop(object? sender, DragEventArgs e)
        {
            INode targetNode = _controlsHelper.GetDataFromRoutedControl(e);
            if (targetNode == null ||
                !e.Data.Contains(_data.DataFormat))
            {
                OnImpossibleDrop(e);
                return;
            }

            if (!_nodeOperations.CanDrop(targetNode))
            {
                e.DragEffects = DragDropEffects.None;
                OnImpossibleDrop(e);
                return;
            }

            OnDropImpl(targetNode, e);
        }

        private void OnImpossibleDrop(DragEventArgs e)
        {
            ResetData();
            e.Handled = true;
        }

        private void ResetData()
        {
            _data.IsDragging = false;
            _data.IsPressed = false;
        }

        private void OnDropImpl(INode targetNode, DragEventArgs e)
        {
            _nodeOperations.DragItem(targetNode, e);
            e.DragEffects = DragDropEffects.Move;
            SetData();
            e.Handled = true;
        }

        private void SetData()
        {
            ResetData();
            _data.DraggedItemView.Classes.Remove("drop");
        }
    }
}

