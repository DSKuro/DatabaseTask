using Avalonia;
using Avalonia.Input;
using DatabaseTask.ViewModels.Nodes;
using System;

namespace DatabaseTask.Services.TreeView
{
    public interface IDragDrop
    {
        public string DataFormat { get; set; }
        public int DragThreshold { get; set; }

        public bool IsDragging { get; set; }
        public Point DragStartPosition { get; set; }
        public INode DraggedNode { get; set; }

        public EventHandler<DragEventArgs> DragEnterEvent { get; }
        public EventHandler<DragEventArgs> DragLeaveEvent { get; }
        public EventHandler<DragEventArgs> DragOverEvent { get; }
        public EventHandler<DragEventArgs> DropEvent { get; }

        public void OnDragEnter(object? sender, DragEventArgs e);
        public void OnDragLeave(object? sender, DragEventArgs e);
        public void OnDragOver(object? sender, DragEventArgs e);
        public void OnDrop(object? sender, DragEventArgs e);
    }
}
