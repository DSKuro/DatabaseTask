using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DatabaseTask.Models;
using DatabaseTask.Services.Messages;
using DatabaseTask.ViewModels.Analyses.Models;
using DatabaseTask.ViewModels.Base;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseTask.ViewModels.Analyses
{
    public abstract partial class FilesSelectionViewModel : ViewModelBase
    {
        public string WindowTitle 
        { 
            get;
        }

        public string FilesTabTitle 
        { 
            get;
        }

        public string AdditionalFilesTabTitle 
        { 
            get; 
        }

        public bool ShowAdditionalFiles 
        { 
            get; 
        }

        public bool ShowExport 
        { 
            get; 
        }

        public SmartCollection<FileSelectionItemViewModel> Files 
        { 
            get; 
        }

        public SmartCollection<string> AdditionalFiles 
        { 
            get; 
        }

        protected FilesSelectionViewModel(
            string windowTitle,
            string filesTabTitle,
            string additionalFilesTabTitle = "",
            bool showAdditionalFiles = false,
            bool showExport = true)
        {
            WindowTitle = windowTitle;
            FilesTabTitle = filesTabTitle;
            AdditionalFilesTabTitle = additionalFilesTabTitle;
            ShowAdditionalFiles = showAdditionalFiles;
            ShowExport = showExport;
            Files = new SmartCollection<FileSelectionItemViewModel>();
            AdditionalFiles = new SmartCollection<string>();
        }

        protected void SetFiles(IEnumerable<string> files)
        {
            Files.AddRange(files.Select(path => new FileSelectionItemViewModel(false, path)));
        }

        protected void SetAdditionalFiles(IEnumerable<string> files)
        {
            AdditionalFiles.AddRange(files);
        }

        [RelayCommand]
        public void CheckAll()
        {
            SetChecked(true);
        }

        [RelayCommand]
        public void UncheckAll()
        {
            SetChecked(false);
        }

        private void SetChecked(bool value)
        {
            foreach (var item in Files)
            {
                item.IsDelete = value;
            }
        }

        [RelayCommand]
        public void Export()
        {
            ExportCore();
        }

        protected virtual void ExportCore()
        {
        }

        [RelayCommand]
        public void Apply()
        {
            List<string> paths = Files
                .Where(item => item.IsDelete)
                .Select(item => item.Path)
                .ToList();

            WeakReferenceMessenger.Default.Send(new AnalyseFilesDialogueCloseMessage(paths));
        }

        [RelayCommand]
        public void Cancel()
        {
            WeakReferenceMessenger.Default.Send(
                new AnalyseFilesDialogueCloseMessage(new List<string>()));
        }
    }
}
