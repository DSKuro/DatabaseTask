﻿using DatabaseTask.Services.Collection;
using System;
using System.Collections.ObjectModel;

namespace DatabaseTask.ViewModels.Nodes
{
    public class NodeViewModel : ViewModelBase, INode
    {
        private bool _isExpanded;
        private string _name;
        private string _iconPath;

        public event Action<INode> Expanded;
        public event Action<INode> Collapsed;

        public bool IsFolder { get; set; }
        public INode? Parent { get; set; }

        public SmartCollection<INode> Children { get; set; } = new();

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
                    else
                    {
                        OnCollapsed(this);
                    }
                }
            }
        }

        public string IconPath
        {
            get => _iconPath;
            set
            {
                if (_iconPath != value)
                {
                    _iconPath = value;
                    OnPropertyChanged();
                }
            }
        }

        private void OnExpanded(INode expandedNode)
        {
            Expanded?.Invoke(expandedNode);
        }

        private void OnCollapsed(INode expandedNode)
        {
            Collapsed?.Invoke(expandedNode);
        }
    }
}
