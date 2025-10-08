using Avalonia.Input;
using DatabaseTask.Services.Interactions.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;

namespace DatabaseTask.Services.TreeViewItemLogic.Operations.Interfaces
{
    public interface ITreeNodeOperations : IItemOperations<INode, DragEventArgs>
    {
        
    }
}
