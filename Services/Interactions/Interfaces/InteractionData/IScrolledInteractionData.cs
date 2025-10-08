using Avalonia.Controls;

namespace DatabaseTask.Services.Interactions.Interfaces.InteractionData
{
    public interface IScrolledInteractionData : IInteractionData    
    {
        public ScrollViewer ScrollViewer { get; set; }
    }
}
