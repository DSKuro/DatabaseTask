using Avalonia;
using Avalonia.Controls;
using DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic.InteractionData.Interfaces;

namespace DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic.InteractionData
{
    public class TreeViewItemInteractionData : ITreeViewData
    {
        private static readonly int Threshold = 3;

        public bool IsDragging { get; set; }
        public bool IsPressed { get; set; }
        public int DragThreshold { get; set; } = Threshold;
        public double TreeViewItemHeight { get; set; }
        public string DataFormat { get; set; } = "NODE";
        public Point DragStartPosition { get; set; }
        public Visual? DraggedItemView { get; set; }
        public Visual Window { get; set; } = null!;
        public Visual Control { get; set; } = null!;
        public ScrollViewer ScrollViewer { get; set; } = null!;
    }
}
