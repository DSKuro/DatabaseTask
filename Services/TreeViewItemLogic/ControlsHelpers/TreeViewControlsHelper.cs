using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using DatabaseTask.Services.Interactions.Interfaces.InteractionData;
using DatabaseTask.Services.TreeViewItemLogic.ControlsHelpers.Interfaces;
using DatabaseTask.Services.TreeViewItemLogic.InteractionData.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;  
using System.Collections.Generic;

namespace DatabaseTask.Services.TreeViewItemLogic
{
    public class TreeViewControlsHelper : ITreeViewControlsHelper
    {
        private readonly IInteractionData _data;

        public TreeViewControlsHelper(ITreeViewData data)
        {
            _data = data;
        }

        public INode? GetDataFromRoutedControl(RoutedEventArgs e)
        {
            if (e.Source is Control control)
            {
                TreeViewItem? item = control.FindAncestorOfType<TreeViewItem>();
                if (item != null)
                {
                    return item.DataContext as INode;
                }
            }
            return null;
        }

        public TreeViewItem? GetVisualForData(INode data)
        {
            return FindTreeViewItemRecursive(_data.Control, data);
        }

        private TreeViewItem? FindTreeViewItemRecursive(Visual parent, INode node)
        {
            if (parent is TreeViewItem item && item.DataContext == node)
            {
                return item;
            }

            return FindItemInChildren(parent, node);
        }

        private TreeViewItem? FindItemInChildren(Visual parent, INode node)
        {
            IEnumerable<Visual> children = parent.GetVisualChildren();
            foreach (Visual child in children)
            {
                if (child is Visual visualChild)
                {
                    TreeViewItem? result = FindTreeViewItemRecursive(visualChild, node);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }
    }
}
