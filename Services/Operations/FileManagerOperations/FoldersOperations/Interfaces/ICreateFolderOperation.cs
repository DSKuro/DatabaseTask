using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces
{
    public interface ICreateFolderOperation
    {
        public Task CreateFolder(INode parent, string folderName);
        public void UndoCreateFolder(INode parent, string folderName);
    }
}
