using Avalonia.Platform.Storage;

namespace DatabaseTask.Services.Dialogues.FilePicker
{
    public class FilePickerConstants
    {
        public static readonly string DbChose = "Выберите базу данных:";
        public static readonly FilePickerFileType DatabaseFilter = new("Database Files")
        {
            Patterns = new[] { "*.mdf" },
            AppleUniformTypeIdentifiers = new[] { "*.mdf" },
            MimeTypes = null
        };
    }
}
