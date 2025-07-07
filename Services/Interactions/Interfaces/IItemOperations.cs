using System;

namespace DatabaseTask.Services.Interactions.Interfaces
{
    public interface IItemOperations<T, V> where V : EventArgs
    {
        public bool CanDrop(T target);
        public void ScrollToDroppedItem(V args);
        public void BringIntoView(T item);
        public void DragItem(T item, V args);
    }
}
