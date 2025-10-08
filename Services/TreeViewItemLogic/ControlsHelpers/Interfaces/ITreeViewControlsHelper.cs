using Avalonia.Controls;
using DatabaseTask.Services.Interactions.Interfaces.ControlsHelpers;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;

namespace DatabaseTask.Services.TreeViewItemLogic.ControlsHelpers.Interfaces
{
    public interface ITreeViewControlsHelper : IControlsHelper<TreeViewItem, INode>
    {
    }
}
