using DatabaseTask.ViewModels.Nodes;
using System;

namespace DatabaseTask.ViewModels.TreeView.EventArguments
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
