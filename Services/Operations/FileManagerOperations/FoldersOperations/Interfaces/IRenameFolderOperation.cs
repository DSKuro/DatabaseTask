using System.Threading.Tasks;

namespace DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces
{
    public interface IRenameFolderOperation
    {
        public Task RenameFolder(string newName);
    }
}
