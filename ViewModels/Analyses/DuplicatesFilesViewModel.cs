using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DatabaseTask.Models;
using DatabaseTask.Services.AnalyseServices.Interfaces;
using DatabaseTask.Services.Messages;
using DatabaseTask.ViewModels.Analyses.Interfaces;
using DatabaseTask.ViewModels.Analyses.Models;
using DatabaseTask.ViewModels.Base;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseTask.ViewModels.Analyses
{
    public partial class DuplicatesFilesViewModel : ViewModelBase, IDuplicatesFilesViewModel
    {
        private readonly IFindDuplicatesService _findDuplicatesService;

        public SmartCollection<DuplicatesFilesItemViewModel> DuplicatesFiles
        {
            get; set;
        }

        public DuplicatesFilesViewModel(IFindDuplicatesService findDuplicatesService)
        {
            _findDuplicatesService = findDuplicatesService;
            DuplicatesFiles = new SmartCollection<DuplicatesFilesItemViewModel>();
            LoadDuplicatesFiles();
        }

        private void LoadDuplicatesFiles()
        {
            var duplicates = _findDuplicatesService.FindDuplicatesByNameAndSize();
            var individualViewModels = duplicates.SelectMany(group =>
                group.files.Select(file =>
                    new DuplicatesFilesItemViewModel(
                        file.isInDatabase,
                        false,
                        group.key,
                        file.path
                    )
                )
            );

            DuplicatesFiles.AddRange(individualViewModels);
        }

        [RelayCommand]
        public void CheckAll()
        {
            Check(true);
        }

        [RelayCommand]
        public void UncheckAll()
        {
            Check(false);
        }

        private void Check(bool value)
        {
            foreach (var item in DuplicatesFiles)
            {
                item.IsDelete = value;
            }
        }

        [RelayCommand]
        public void Apply()
        {
            var resultItems = DuplicatesFiles
                .Where(item => item.IsDelete)
                .Select(item => item.Path)
                .ToList();
            WeakReferenceMessenger.Default
                .Send(new AnalyseFilesDialogueCloseMessage(resultItems));
        }

        [RelayCommand]
        public void Cancel()
        {
            WeakReferenceMessenger.Default
                .Send(new AnalyseFilesDialogueCloseMessage(new List<string>()));
        }
    }
}
