using System.Threading.Tasks;

namespace DatabaseTask.Services.FileManagerOperations.FoldersOperations.Interfaces
{
    public interface ICreateFolderOperation
    {
        public Task CreateFolder(string FolderName);
    }
}
