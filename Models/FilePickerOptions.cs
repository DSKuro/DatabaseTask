using Avalonia.Platform.Storage;

namespace DatabaseTask.Models
{
    public class FilePickerOptions
    {
        public bool AllowMultiple
        {
            get;
        }

        public string Title
        {
            get;
        }
        public FilePickerFileType Filter
        {
            get;
        }

        public FilePickerOptions(string title, FilePickerFileType filter,
            bool allowMultiple = false)
        {
            Title = title;
            Filter = filter;
            AllowMultiple = allowMultiple;
        }
    }
}
