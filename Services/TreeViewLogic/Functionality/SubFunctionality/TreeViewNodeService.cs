using DatabaseTask.Models;
using DatabaseTask.Services.TreeViewLogic.Functionality.SubFunctionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.EventArguments;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using System;
using System.Linq;

namespace DatabaseTask.Services.TreeViewLogic.Functionality.SubFunctionality
{
    public class TreeViewNodeService : ITreeViewNodeService
    {
        private readonly ITreeView _treeView;

        public TreeViewNodeService(ITreeView treeView)
        {
            _treeView = treeView;
        }

        public bool IsNodeExist(INode parent, string name)
        {
            if (parent == null)
            {
                return false;
            }

            return parent.Children.Any(x => x.Name == name);
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

        public INode? GetChildrenByName(INode node, string name)
        {
            return node.Children.FirstOrDefault(x => x.Name == name);
        }

        public INode? GetNodeByPath(string path)
        {
            INode? coreNode = GetCoreNode();
            if (coreNode is null)
            {
                return null;
            }

            var parts = path.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries)
                .Where(part => !part.Equals("."));

            INode currentNode = coreNode;

            foreach (var part in parts)
            {
                INode? nextNode = currentNode.Children
                    .FirstOrDefault(node => node.Name.Equals(part));

                if (nextNode is null)
                {
                    return null;
                }

                currentNode = nextNode;
            }

            return currentNode;
        }

        public INode? GetCoreNode()
        {
            return _treeView.Nodes.FirstOrDefault();
        }

        public INode? CreateNode(INode template, INode parent)
        {
            if (template is NodeViewModel nodeTemplate)
            {
                return new NodeViewModel()
                {
                    Name = nodeTemplate.Name,
                    IsExpanded = nodeTemplate.IsExpanded,
                    IsFolder = nodeTemplate.IsFolder,
                    IconPath = nodeTemplate.IconPath,
                    Parent = parent,
                    Children = new SmartCollection<INode>()
                };
            }
            return null;
        }

        public void RemoveNode(INode node)
        {
            node.Parent?.Children.Remove(node);
        }

        public void BringIntoView(INode node)
        {
            _treeView.ScrollChanged?.Invoke(this, new TreeViewEventArgs(node));
        }
    }
}
