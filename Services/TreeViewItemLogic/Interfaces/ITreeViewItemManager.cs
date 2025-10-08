using Avalonia.Controls;
using System;

namespace DatabaseTask.Services.TreeViewItemLogic.Interfaces
{
    public interface ITreeViewItemManager
    {
        public EventHandler<ContainerPreparedEventArgs> ContainerPreparedEvent { get; }
        public void Initialize(TreeView control, Window window);
        public void InitializeScrollViewer(ScrollViewer scroll);
    }
}
