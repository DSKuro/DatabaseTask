using Avalonia.Input;
using System;

namespace DatabaseTask.Services.Interactions
{
    public interface IDragDrop
    {
        public EventHandler<DragEventArgs> DragEnterEvent { get; }
        public EventHandler<DragEventArgs> DragLeaveEvent { get; }
        public EventHandler<DragEventArgs> DragOverEvent { get; }
        public EventHandler<DragEventArgs> DropEvent { get; }
    }
}
