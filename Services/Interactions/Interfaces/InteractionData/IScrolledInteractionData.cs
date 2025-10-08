using Avalonia.Controls;

namespace DatabaseTask.Services.Interactions.Interfaces.InteractionData
{
    public interface IScrolledInteractionData : IControlData, IWindowData    
    {
        public ScrollViewer ScrollViewer { get; set; }
    }
}
