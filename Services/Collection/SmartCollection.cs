using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace DatabaseTask.Services._serviceCollection
{
    public class Smart_serviceCollection<T> : ObservableCollection<T>
    {
        public Smart_serviceCollection() : base() { }

        public Smart_serviceCollection(IEnumerable<T> _serviceCollection)
            : base(_serviceCollection)
        {
        }

        public Smart_serviceCollection(List<T> list)
            : base(list)
        {
        }

        public void AddRange(IEnumerable<T> range)
        {
            foreach (T item in range)
            {
                Items.Add(item);
            }

            this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void Reset(IEnumerable<T> range)
        {
            this.Items.Clear();
            AddRange(range);
        }
    }
}
