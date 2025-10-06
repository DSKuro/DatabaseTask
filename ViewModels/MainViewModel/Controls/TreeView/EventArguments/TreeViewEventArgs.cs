using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System;

namespace DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.EventArguments
{
    public class TreeViewEventArgs : EventArgs
    {
        public INode Node { get; set; }

        public TreeViewEventArgs(INode node) 
        {
            Node = node;
        }
    }
}
