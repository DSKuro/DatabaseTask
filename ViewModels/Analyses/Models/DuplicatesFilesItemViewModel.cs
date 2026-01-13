using DatabaseTask.ViewModels.Analyses.Models.Interfaces;
using DatabaseTask.ViewModels.Base;

namespace DatabaseTask.ViewModels.Analyses.Models
{
    public class DuplicatesFilesItemViewModel : ViewModelBase, IDuplicatesFilesItemViewModel
    {
        private bool _isDelete;

        public bool IsDB
        {
            get; set;
        }

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

        public string FileName
        {
            get; set;
        }

        public string Path
        {
            get; set;
        }

        public DuplicatesFilesItemViewModel(bool isDB, bool isDelete, string fileName, string path)
        {
            IsDB = isDB;
            IsDelete = isDelete;
            FileName = fileName;
            Path = path;
        }
    }
}
