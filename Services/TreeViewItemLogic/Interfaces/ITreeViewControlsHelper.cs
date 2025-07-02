using Avalonia.Controls;
using DatabaseTask.Services.Interactions.Interfaces;
using DatabaseTask.ViewModels.Nodes;

namespace DatabaseTask.Services.TreeViewItemLogic.Interfaces
{
    public interface ITreeViewControlsHelper : IControlsHelper<TreeViewItem, INode>
    {
    }
}
