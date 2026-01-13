using DatabaseTask.Models;

namespace DatabaseTask.ViewModels.Analyses.Interfaces
{
    public interface IDuplicatesFilesViewModel
    {
        public SmartCollection<DuplicatesFilesViewModel> DuplicatesFiles
        {
            get; set;
        }
    }
}
