using Avalonia;
using Avalonia.Controls;
using DatabaseTask.Services.TreeViewItemLogic.Interfaces;

namespace DatabaseTask.Services.TreeViewItemLogic
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
        public Visual DraggedItemView { get; set; }
        public Visual Window { get; set; }
        public Visual Control { get; set; }
        public ScrollViewer ScrollViewer { get; set; }
    }
}
