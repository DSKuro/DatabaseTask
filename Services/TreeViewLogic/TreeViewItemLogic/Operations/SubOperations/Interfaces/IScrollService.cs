using Avalonia.Input;

namespace DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic.Operations.SubOperations.Interfaces
{
    public interface IScrollService
    {
        public void ScrollToDroppedItem(DragEventArgs e);
        public void StopScrolling();
    }
}
