using Avalonia.Input;
using DatabaseTask.Services.Interactions.Interfaces;
using DatabaseTask.ViewModels.Nodes;

namespace DatabaseTask.Services.TreeViewItemLogic.Interfaces
{
    public interface ITreeNodeOperations : IItemOperations<INode, DragEventArgs>
    {
        
    }
}
