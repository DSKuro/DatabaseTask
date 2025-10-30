using Avalonia;

namespace DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic.InteractionData.Interfaces
{
    public interface ITreeViewDragVisual
    {
        public Visual? DraggedItemView { get; set; }
    }
}
