using Avalonia.Platform.Storage;

namespace DatabaseTask.Models
{
    public class FilePickerOptions : FolderPickerOptions
    {
        public string Title
        {
            get;
        }
        public FilePickerFileType Filter
        {
            get;
        }

        public FilePickerOptions(string title, FilePickerFileType filter,
            bool allowMultiple = false) : base(allowMultiple)
        {
            Title = title;
            Filter = filter;
        }
    }
}
