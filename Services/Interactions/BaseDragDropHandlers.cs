using Avalonia.Input;

namespace DatabaseTask.Services.Interactions
{
    public abstract class BaseDragDropHandlers
    {
        protected abstract void OnDragEnter(object? sender, DragEventArgs e);
        protected abstract void OnDragOver(object? sender, DragEventArgs e);
        protected abstract void OnDragLeave(object? sender, DragEventArgs e);
        protected abstract void OnDrop(object? sender, DragEventArgs e);
    }
}
