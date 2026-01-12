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
    public partial class UnusedFilesViewModel : ViewModelBase, IUnusedFilesViewModel
    {
        private readonly IFindUnusedFilesServices _findUnusedFilesServices;

        public SmartCollection<UnusedFilesItemViewModel> UnusedFiles
        {
            get;
            set;
        }

        public UnusedFilesViewModel(IFindUnusedFilesServices findUnusedFilesServices)
        {
            _findUnusedFilesServices = findUnusedFilesServices;
            UnusedFiles = new SmartCollection<UnusedFilesItemViewModel>();
            LoadUnusedFilesAsync();
        }

        private void LoadUnusedFilesAsync()
        {
            var files = _findUnusedFilesServices.FindUnusedFiles(); ;
            UnusedFiles.AddRange(files.Select(f => new UnusedFilesItemViewModel(false, f)));
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
        public void Apply()
        {
            List<string> paths = UnusedFiles
                .Where(item => item.IsDelete)
                .Select(item => item.Path)
                .ToList();
            WeakReferenceMessenger.Default.Send(new UnusedFilesDialogueCloseMessage(paths));
        }

        [RelayCommand]
        public void Cancel()
        {
            WeakReferenceMessenger.Default.Send(new UnusedFilesDialogueCloseMessage(new List<string>()));
        }
    }
}
