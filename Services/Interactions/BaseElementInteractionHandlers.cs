using Avalonia.Input;

namespace DatabaseTask.Services.Interactions
{
    public abstract class BaseElementInteractionHandlers
    {
        protected abstract void OnPointerPressed(object? sender, PointerPressedEventArgs e);
        protected abstract void OnPointerMoved(object? sender, PointerEventArgs e);
        protected abstract void OnPointerReleased(object? sender, PointerReleasedEventArgs e);
    }
}
