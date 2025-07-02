using Avalonia.Controls;

namespace DatabaseTask.Services.Interactions
{
    public interface IScrolledInteractionData : IInteractionData    
    {
        public ScrollViewer ScrollViewer { get; set; }
    }
}
