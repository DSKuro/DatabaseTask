using Avalonia.Controls;
using DatabaseTask.Models;
using DatabaseTask.Services.Operations.FileManagerOperations.Exceptions;
using DatabaseTask.ViewModels.Base;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.EventArguments;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using System;
using System.Linq;

namespace DatabaseTask.ViewModels.MainViewModel.Controls.TreeView
{
    public class TreeViewService : ViewModelBase, ITreeView
    {
        private SmartCollection<INode> _nodes = new SmartCollection<INode>();

        public INode? DraggedItem { get; set; }

        public SmartCollection<INode> SelectedNodes { get; } = new();

        public SmartCollection<INode> Nodes
        {
            get => _nodes;
            set => SetProperty(ref _nodes, value);
        }

        public EventHandler<SelectionChangedEventArgs> SelectionChanged { get; set; } = null!;
        public EventHandler<TreeViewEventArgs> ScrollChanged { get; set; } = null!;

        public bool IsNodeExist(int selectedItemIndex, string name)
        {
            if (selectedItemIndex < 0 || selectedItemIndex > SelectedNodes.Count)
            {
                throw new FileManagerOperationsException("Индекс выбранного элемента за границами коллекции");
            }

            return SelectedNodes[selectedItemIndex]
                .Children
                .Where(x => x != null)
                .Any(x => x!.Name == name);
        }

        public bool IsParentHasNodeWithName(INode node, string name)
        {
            if (node != null && node.Parent != null)
            {
                return node.Parent.Children
                    .Any(x => x.Name == name);
            }

            return false;
        }

        public void AddSelectedNodeByIndex(int index)
        {
            if (index < 0 || index > Nodes.Count)
            {
                return;
            }

            SelectedNodes.Add(Nodes[index]);
        }
    }
}
