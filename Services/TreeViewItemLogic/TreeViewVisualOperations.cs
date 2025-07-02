using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.VisualTree;
using DatabaseTask.Services.TreeViewItemLogic.Interfaces;
using DatabaseTask.ViewModels.Nodes;
using System.Collections.Generic;

namespace DatabaseTask.Services.TreeViewItemLogic
{
    public class TreeViewVisualOperations : ITreeViewVisualOperations
    {
        private string _style;
        private DragEventArgs _args;

        private ITreeViewData _treeViewData;

        public TreeViewVisualOperations(ITreeViewData treeViewData)
        {
            _treeViewData = treeViewData;
        }

        public void SetDropHighlight(DragEventArgs args, string style)
        {
            SetVariables(args, style);
            Visual container = GetContainer();

            if (container == null || container.DataContext is not INode targetNode)
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

        private Visual GetContainer()
        {
            Point positionInTree = _args.GetPosition(_treeViewData.Control);
            return GetVisualAtPosition(positionInTree);
        }

        private TreeViewItem GetVisualAtPosition(Point point)
        {
            IEnumerable<Visual> visuals = _treeViewData.Control.GetVisualsAt(point);
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
            _treeViewData.DraggedItemView.Classes.Remove(style);
        }
    }
}
