using Avalonia.Input;
using System;

namespace DatabaseTask.Services.Interactions.Interfaces
{
    public interface IElementInteractions
    {
        public EventHandler<PointerPressedEventArgs> PressedEvent { get; }
        public EventHandler<PointerEventArgs> PointerMovedEvent { get; }
        public EventHandler<PointerReleasedEventArgs> ReleasedEvent { get; }
    }
}
