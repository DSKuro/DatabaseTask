using Avalonia.Controls;

namespace DatabaseTask.Services.Interactions.Interfaces
{
    public interface IScrolledInteractionData : IInteractionData    
    {
        public ScrollViewer ScrollViewer { get; set; }
    }
}
