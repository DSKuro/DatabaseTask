using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DatabaseTask.Models;
using DatabaseTask.Models.Duplicates;
using DatabaseTask.Services.AnalyseServices.Interfaces;
using DatabaseTask.Services.AnalyseServices.Utils.Interfaces;
using DatabaseTask.Services.Excel.DuplicatesFiles.Interfaces;
using DatabaseTask.Services.Messages;
using DatabaseTask.ViewModels.Analyses.Interfaces;
using DatabaseTask.ViewModels.Analyses.Models;
using DatabaseTask.ViewModels.Base;
using System.Linq;

namespace DatabaseTask.ViewModels.Analyses
{
    public partial class DuplicatesFilesViewModel : ViewModelBase, IDuplicatesFilesViewModel
    {
        private readonly IFindDuplicatesService _findDuplicatesService;
        private readonly IExcelDuplicatesFiles _excelDuplicatesFiles;
        private readonly IAnalyseUtils _analyseUtils;

        public SmartCollection<DuplicatesFilesItemViewModel> DuplicatesFiles
        {
            get; set;
        }

        public DuplicatesFilesViewModel(IFindDuplicatesService findDuplicatesService,
                                        IExcelDuplicatesFiles excelDuplicatesFiles,
                                        IAnalyseUtils analyseUtils)
        {
            _findDuplicatesService = findDuplicatesService;
            _excelDuplicatesFiles = excelDuplicatesFiles;
            _analyseUtils = analyseUtils;
            DuplicatesFiles = new SmartCollection<DuplicatesFilesItemViewModel>();
            _analyseUtils.ClearTempFiles();
            LoadDuplicatesFiles();
        }

        private void LoadDuplicatesFiles()
        {
            var duplicates = _findDuplicatesService.FindDuplicatesByHash();
            var individualViewModels = duplicates.SelectMany(group =>
                group.files.Select((file, index) =>
                    new DuplicatesFilesItemViewModel(
                        false,
                        group.key,
                        file.path
                    )
                    {
                        IsFirstInGroup = index is 0,
                        IsDB = index is 0
                    }
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
                if (!item.IsDB)
                {
                    item.IsDelete = value;
                }
            }
        }

        [RelayCommand]
        public void ExcelDuplicateFiles()
        {
            _excelDuplicatesFiles.WriteDuplicatesToExcel(DuplicatesFiles.ToList());
        }

        [RelayCommand]
        public void Apply()
        {
            var pathsToDelete = DuplicatesFiles
                                .Where(item => item.IsDelete)
                                .Select(item => item.Path)
                                .ToList();

            var pathsToUpdate = DuplicatesFiles
                .GroupBy(item => item.FileName)
                .Where(group => group.Any(item => item.IsDelete))
                .SelectMany(group =>
                {
                    var selectedItem = group.FirstOrDefault(item => item.IsDB) ?? group.First();

                    return group
                        .Where(item => item.IsDelete)
                        .Where(item => item.Path != selectedItem.Path)
                        .Select(item => new DuplicatePathUpdate(item.Path, selectedItem.Path));
                })
                .ToList();

            var result = new DuplicatesFilesDialogResult(pathsToDelete, pathsToUpdate);

            WeakReferenceMessenger.Default
                .Send(new DuplicatesFilesDialogueCloseMessage(result));
        }

        [RelayCommand]
        public void Cancel()
        {
            WeakReferenceMessenger.Default
                .Send(new DuplicatesFilesDialogueCloseMessage(new DuplicatesFilesDialogResult()));
        }
    }
}
