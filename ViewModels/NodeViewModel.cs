using DatabaseTask.Services.Collection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels
{
    public class NodeViewModel : ViewModelBase
    {
        private string _name;
        private bool _isExpanded;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                if (_isExpanded != value)
                {
                    _isExpanded = value;
                    OnPropertyChanged();

                    if (value)
                    {
                        OnExpanded(this);
                    }
                }
            }
        }

        public string IconPath { get; set; }

        public SmartCollection<NodeViewModel> Children { get; } = new();

        public event Action<NodeViewModel> Expanded;
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnExpanded(NodeViewModel expandedNode)
        {
            Expanded?.Invoke(expandedNode);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public NodeViewModel? Parent { get; set; }
    }
}
