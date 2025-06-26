namespace DatabaseTask.Models
{
    public class FolderPickerOptions
    {
        public bool AllowMultiple
        {
            get;
        }

        public FolderPickerOptions(bool allowMultiple)
        {
            AllowMultiple = allowMultiple;
        }
    }
}
