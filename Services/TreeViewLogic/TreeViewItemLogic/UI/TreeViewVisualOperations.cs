using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.VisualTree;
using DatabaseTask.Services.Interactions.Interfaces.InteractionData;
using DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic.InteractionData.Interfaces;
using DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic.UI.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Collections.Generic;

namespace DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic.UI
{
    public class TreeViewVisualOperations : ITreeViewVisualOperations
    {
        private string _style;
        private DragEventArgs _args;

        private IControlData _data;
        private ITreeViewDragVisual _visual;

        public TreeViewVisualOperations(ITreeViewData treeViewData)
        {
            _data = treeViewData;
            _visual = treeViewData;
            _style = "";
            _args = null!;
        }

        public void SetDropHighlight(DragEventArgs args, string style)
        {
            SetVariables(args, style);
            Visual? container = GetContainer();

            if (container == null || container.DataContext is not INode)
            {
                return;
            }

            SetDropHighLightImpl(container);
        }

        private void SetVariables(DragEventArgs args, string style)
        {
            _args = args;
            _style = style;
        }

        private Visual? GetContainer()
        {
            Point positionInTree = _args.GetPosition(_data.Control);
            return GetVisualAtPosition(positionInTree);
        }

        private TreeViewItem? GetVisualAtPosition(Point point)
        {
            IEnumerable<Visual> visuals = _data.Control.GetVisualsAt(point);
            return GetVisualImpl(visuals);
        }

        private TreeViewItem? GetVisualImpl(IEnumerable<Visual> visuals)
        {
            foreach (Visual visual in visuals)
            {
                if (visual is TreeViewItem item)
                {
                    return item;
                }

                TreeViewItem? parent = visual.FindAncestorOfType<TreeViewItem>();
                if (parent != null)
                {
                    return parent;
                }
            }

            return null;
        }

        private void SetDropHighLightImpl(Visual container)
        {
            Point positionInContainer = _args.GetPosition(container);
            Rect bounds = container.Bounds;
            if (!container.Classes.Contains(_style))
            {
                container.Classes.Add(_style);
            }
        }

        public void ClearDropHighlight(string style)
        {
            Visual? itemView = _visual.DraggedItemView;
            if (itemView == null)
            {
                return;
            }

            itemView.Classes.Remove(style);
        }
    }
}
