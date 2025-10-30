using DatabaseTask.Models;
using DatabaseTask.Services.TreeViewLogic.Functionality.SubFunctionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Linq;

namespace DatabaseTask.Services.TreeViewLogic.Functionality.SubFunctionality
{
    public class TreeViewNodeService : ITreeViewNodeService
    {
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
    }
}
