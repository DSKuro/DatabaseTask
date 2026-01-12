using DatabaseTask.Models;
using DatabaseTask.ViewModels.Analyses.Models;

namespace DatabaseTask.ViewModels.Analyses.Interfaces
{
    public interface IUnusedFilesViewModel
    {
        public SmartCollection<UnusedFilesItemViewModel> UnusedFiles { get; set; }
    }
}
