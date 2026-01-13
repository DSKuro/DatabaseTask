namespace DatabaseTask.ViewModels.Analyses.Models.Interfaces
{
    public interface IDuplicatesFilesItemViewModel
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
    }
}
