using DatabaseTask.ViewModels.Analyses.Models.Interfaces;
using DatabaseTask.ViewModels.Base;
using System.ComponentModel;

namespace DatabaseTask.ViewModels.Analyses.Models
{
    public class UnusedFilesItemViewModel : ViewModelBase, IUnusedFilesItemViewModel, INotifyPropertyChanged
    {
        private bool _isDelete;

        public bool IsDelete
        {
            get => _isDelete;
            set
            {
                if (_isDelete != value)
                {
                    _isDelete = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Path { get; set; }

        public UnusedFilesItemViewModel(bool isDelete, string path)
        {
            IsDelete = isDelete;
            Path = path;
        }
    }
}
