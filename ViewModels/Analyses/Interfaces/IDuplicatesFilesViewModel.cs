using DatabaseTask.Models;
using DatabaseTask.ViewModels.Analyses.Models;

namespace DatabaseTask.ViewModels.Analyses.Interfaces
{
    public interface IDuplicatesFilesViewModel
    {
        public SmartCollection<DuplicatesFilesItemViewModel> DuplicatesFiles
        {
            get; set;
        }
    }
}
