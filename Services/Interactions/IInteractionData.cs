using Avalonia;

namespace DatabaseTask.Services.Interactions
{
    public interface IInteractionData
    {
        public Visual Window { get; set; }
        public Visual Control { get; set; }
    }
}
