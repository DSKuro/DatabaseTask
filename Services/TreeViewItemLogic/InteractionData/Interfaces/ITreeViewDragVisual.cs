using Avalonia;

namespace DatabaseTask.Services.TreeViewItemLogic.InteractionData.Interfaces
{
    public interface ITreeViewDragVisual
    {
        public Visual? DraggedItemView { get; set; }
    }
}
