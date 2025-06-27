using Avalonia.Platform.Storage;

namespace DatabaseTask.Services.Dialogues.Storage
{
    public class StorageConstants
    {
        public string Value { get; private set; }
        public FilePickerFileType Type { get; private set; }

        private StorageConstants(string value, FilePickerFileType type)
        {
            Value = value;
            Type = type;
        }

        public static StorageConstants BaseStorage 
        { 
            get
            {
                return new StorageConstants("Выберите базу данных:",
                    new("Database Files")
                    {
                        Patterns = new[] { "*.mdf" },
                        AppleUniformTypeIdentifiers = new[] { "*.mdf" },
                        MimeTypes = null
                    });
            } 
        }
    }
}
