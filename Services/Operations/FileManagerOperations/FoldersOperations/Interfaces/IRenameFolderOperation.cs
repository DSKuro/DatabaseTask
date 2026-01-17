using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces
{
    public interface IRenameFolderOperation
    {
        public Task RenameFolder(INode node, string newName);
        public Task UndoRenameFolder(INode node);
    }
}
