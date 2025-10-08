using Avalonia.Controls;
using DatabaseTask.Services.TreeViewItemLogic.InteractionData.Interfaces;
using DatabaseTask.Services.TreeViewItemLogic.Operations.Interfaces;
using System;

namespace DatabaseTask.Services.TreeViewItemLogic.Interfaces
{
    public interface ITreeViewItemManager
    {
        public EventHandler<ContainerPreparedEventArgs> ContainerPreparedEvent { get; }
        public ITreeViewData TreeViewItemInteractionData { get; }
        public ITreeNodeOperations TreeNodeOperations { get; }
    }
}
