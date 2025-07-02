using Avalonia.Controls;
using Avalonia.Interactivity;

namespace DatabaseTask.Services.Interactions.Interfaces
{
    public interface IControlsHelper<T, V> where T : Control where V : class
    {
        public V GetDataFromRoutedControl(RoutedEventArgs e);
        public T GetVisualForData(V data); 
    }
}
