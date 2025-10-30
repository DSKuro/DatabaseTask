using DatabaseTask.Services.Comparer.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.SubFunctionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using ExCSS;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseTask.Services.TreeViewLogic.Functionality.SubFunctionality
{
    public class TreeViewSortService : ITreeViewSortService
    {
        private readonly INodeComparer _comparer;

        public TreeViewSortService(INodeComparer comparer)
        {
            _comparer = comparer;
        }

        public int GetNodePositionIndex(INode target, INode node)
        {
            List<INode> children = target.Children.ToList();

            if (children.Count == 0)
                return 0;

            int index = children.FindIndex(x => _comparer.Compare(node, x) < 0);

            if (index == -1)
                return children.Count;

            return index;
        }
    }
}
