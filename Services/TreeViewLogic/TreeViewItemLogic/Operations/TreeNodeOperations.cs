using Avalonia.Controls;
using Avalonia.Input;
using DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic.ControlsHelpers.Interfaces;
using DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic.Operations.Interfaces;
using DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic.Operations.SubOperations.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic.Operations
{
    public class TreeNodeOperations : ITreeNodeOperations
    {
        private readonly ITreeView _treeView;
        private readonly ITreeViewControlsHelper _helper;
        private readonly INodeEvents _nodeEvents;
        private readonly IDropValidationService _dropValidationService;
        private readonly IScrollService _scrollService;

        public TreeNodeOperations(
            ITreeView treeView,
            IDataGrid dataGrid,
            ITreeViewControlsHelper helper,
            INodeEvents nodeEvents,
            IDropValidationService dropValidationService,
            IScrollService scrollService)
        {
            _treeView = treeView;
            _helper = helper;
            _nodeEvents = nodeEvents;
            _dropValidationService = dropValidationService;
            _scrollService = scrollService;
        }

        public bool CanDrop(INode target)
        {
            return _dropValidationService.CanDrop(target);
        }

        public void ScrollToDroppedItem(DragEventArgs e)
        {
            _scrollService.ScrollToDroppedItem(e);
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
