using DatabaseTask.Services.Operations.FileManagerOperations.Accessibility.Interfaces;
using DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic.Operations.SubOperations.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic.Operations.SubOperations
{
    public class DropValidationService : IDropValidationService
    {
        private readonly ITreeView _treeView;
        private readonly IFileManagerCommonOperationsPermission _permission;

        public DropValidationService(
            ITreeView treeView,
            IFileManagerCommonOperationsPermission permission)
        {
            _treeView = treeView;
            _permission = permission;
        }

        public bool CanDrop(INode target)
        {
            List<INode> selectedNodes = _treeView.SelectedNodes.ToList();

            if (selectedNodes.Count > 0)
            {
                return false;
            }

            if (target is NodeViewModel targetNode && targetNode.IsFolder)
            {
                return false;
            }

            if (!HasPermissions(selectedNodes, target))
            {
                return false;
            }

            return IsValidDropPosition(selectedNodes, target);
        }

        private bool HasPermissions(List<INode> selectedNodes, INode target)
        {
            selectedNodes.Add(target);

            try
            {
                _permission.MoveItems(selectedNodes);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool IsValidDropPosition(List<INode> selectedNodes, INode target)
        {
            return selectedNodes.All(item => IsValidDropPositionForItem(item, target));
        }

        private bool IsValidDropPositionForItem(INode item, INode target)
        {
            return item != target &&
                !IsTargetChildOfSource(item, target) &&
                item.Parent != target;
        }

        private bool IsTargetChildOfSource(INode source, INode target)
        {
            INode? parent = target.Parent;
            while (parent != null)
            {
                if (parent == source) return true;
                parent = parent.Parent;
            }
            return false;
        }
    }
}
