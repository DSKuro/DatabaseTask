using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.Interfaces
{
    public interface IFileManagerFolderCommandsViewModel
    {
        public Task CreateFolderImpl();
        public Task RenameFolderImpl();
        public Task DeleteFolderImpl();
    }
}
