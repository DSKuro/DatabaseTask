using DatabaseTask.Models;

namespace DatabaseTask.ViewModels.Analyses.Models.Interfaces
{
    public interface IUnusedFilesItemViewModel
    {
        public bool IsDelete { get; set; }
        public string Path { get; set; }
    }
}
