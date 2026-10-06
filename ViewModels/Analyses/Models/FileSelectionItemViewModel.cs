using DatabaseTask.ViewModels.Base;

namespace DatabaseTask.ViewModels.Analyses.Models
{
    public class FileSelectionItemViewModel : ViewModelBase
    {
        private bool _isDelete;

        public bool IsDelete
        {
            get => _isDelete;
            set => SetProperty(ref _isDelete, value);
        }

        public string Path 
        { 
            get;
            set;
        }

        public FileSelectionItemViewModel(bool isDelete, string path)
        {
            IsDelete = isDelete;
            Path = path;
        }
    }
}
