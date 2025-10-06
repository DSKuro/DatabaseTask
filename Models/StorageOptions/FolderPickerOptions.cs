namespace DatabaseTask.Models.StorageOptions
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
