using Avalonia.Controls;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;

namespace DatabaseTask.Services.TreeViewLogic.TreeViewManager.Interfaces
{
    public interface ITreeViewEventService
    {
        public void SubscribeToNodeEvents(INode node);
        public void UnsubscribeFromNodeEvents(INode node);
        public void HandleSelectionChanged(object? sender, SelectionChangedEventArgs e);
        public void ExpandHandler(INode model);
        public void CollapsedHandler(INode model);
    }
}
