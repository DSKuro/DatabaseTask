using Avalonia;
using DatabaseTask.Services.Interactions.Interfaces.InteractionData;

namespace DatabaseTask.Services.TreeViewItemLogic.InteractionData.Interfaces
{
    public interface ITreeViewData : IScrolledInteractionData
    {
        public bool IsDragging { get; set; }
        public bool IsPressed { get; set; }
        public int DragThreshold { get; set; }
        public double TreeViewItemHeight { get; set; }
        public string DataFormat { get; set; }
        public Point DragStartPosition { get; set; }
        public Visual DraggedItemView { get; set; }
    }
}
