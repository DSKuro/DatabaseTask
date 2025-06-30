using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;

namespace DatabaseTask.Services.TreeView
{
    public interface ITreeViewItemLogic
    {
        public Visual PressedItem { get; set; }
        public void OnContainerPrepared(object sender, ContainerPreparedEventArgs e);
        public void OnPointerPressed(object? sender, PointerPressedEventArgs e);
        public void OnPointerMoved(object? sender, PointerEventArgs e);
        public void OnPointerReleased(object? sender, PointerReleasedEventArgs e);
    }
}
