using DatabaseTask.Models;
using DatabaseTask.Services.AnalyseServices.Interfaces;
using DatabaseTask.ViewModels.Analyses.Interfaces;
using DatabaseTask.ViewModels.Base;

namespace DatabaseTask.ViewModels.Analyses
{
    public class DuplicatesFilesViewModel : ViewModelBase, IDuplicatesFilesViewModel
    {
        private readonly IFindDuplicatesService _findDuplicatesService;

        public SmartCollection<DuplicatesFilesViewModel> DuplicatesFiles
        {
            get; set;
        }

        public DuplicatesFilesViewModel(IFindDuplicatesService findDuplicatesService)
        {
            _findDuplicatesService = findDuplicatesService;
            DuplicatesFiles = new SmartCollection<DuplicatesFilesViewModel>();
            LoadDuplicatesFiles();
        }

        private void LoadDuplicatesFiles()
        {
            var duplicates = _findDuplicatesService.FindDuplicatesByNameAndSize();
        }
    }
}
