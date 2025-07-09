using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.Interfaces
{
    public interface IFolderCommandsViewModel
    {
        public Task CreateFolderImpl();
        public Task RenameFolderImpl();
    }
}
