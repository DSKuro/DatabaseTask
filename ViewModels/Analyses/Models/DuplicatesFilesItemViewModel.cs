using DatabaseTask.ViewModels.Analyses.Models.Interfaces;
using DatabaseTask.ViewModels.Base;

namespace DatabaseTask.ViewModels.Analyses.Models
{
    public class DuplicatesFilesItemViewModel : ViewModelBase, IDuplicatesFilesItemViewModel
    {
        public bool IsDB
        {
            get; set;
        }

        public bool IsDelete
        {
            get; set;
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
