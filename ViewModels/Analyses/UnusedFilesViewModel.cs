using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DatabaseTask.Models;
using DatabaseTask.Services.AnalyseServices.Interfaces;
using DatabaseTask.Services.AnalyseServices.Utils.Interfaces;
using DatabaseTask.Services.Excel.UnusedFiles.Interfaces;
using DatabaseTask.Services.Messages;
using DatabaseTask.ViewModels.Analyses.Interfaces;
using DatabaseTask.ViewModels.Analyses.Models;
using DatabaseTask.ViewModels.Base;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace DatabaseTask.ViewModels.Analyses
{
    public partial class UnusedFilesViewModel : ViewModelBase, IUnusedFilesViewModel
    {
        private readonly IFindUnusedFilesServices _findUnusedFilesServices;
        private readonly IExcelUnusedPaths _excelUnusedPaths;
        private readonly IAnalyseUtils _analyseUtils;

        public SmartCollection<UnusedFilesItemViewModel> UnusedFiles
        {
            get;
            set;
        }

        public SmartCollection<string> ExceptFiles
        {
            get;
            set;
        }

        public UnusedFilesViewModel(IFindUnusedFilesServices findUnusedFilesServices,
                                    IExcelUnusedPaths excelUnusedPaths,
                                    IAnalyseUtils analyseUtils)
        {
            _findUnusedFilesServices = findUnusedFilesServices;
            _excelUnusedPaths = excelUnusedPaths;
            _analyseUtils = analyseUtils;
            UnusedFiles = new SmartCollection<UnusedFilesItemViewModel>();
            ExceptFiles = new SmartCollection<string>();
            _analyseUtils.ClearTempFiles();
            LoadUnusedFilesAsync();
        }

        private void LoadUnusedFilesAsync()
        {
            (var unusedFiles, var exceptFiles)  = _findUnusedFilesServices.FindUnusedFiles();
            UnusedFiles.AddRange(unusedFiles.Select(f => new UnusedFilesItemViewModel(false, f)));
            ExceptFiles.AddRange(exceptFiles);
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
            foreach(var item in UnusedFiles)
            {
                item.IsDelete = value;
            }
        }

        [RelayCommand]
        public void WriteUnusedExcelPaths()
        {
            _excelUnusedPaths.WriteUnusedPathsToExcel(UnusedFiles.Select(x => x.Path).ToList(),
                ExceptFiles.ToList());
        }

        [RelayCommand]
        public void Apply()
        {
            List<string> paths = UnusedFiles
                .Where(item => item.IsDelete)
                .Select(item => item.Path)
                .ToList();
            WeakReferenceMessenger.Default.Send(new AnalyseFilesDialogueCloseMessage(paths));
        }

        [RelayCommand]
        public void Cancel()
        {
            WeakReferenceMessenger.Default.Send(new AnalyseFilesDialogueCloseMessage(new List<string>()));
        }
    }
}
