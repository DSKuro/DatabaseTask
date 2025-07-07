namespace DatabaseTask.Services._serviceCollection
{
    public class TextBoxWatermark
    {
        public string Value { get; set; }

        public TextBoxWatermark(string value)
        {
            Value = value;
        }

        public static TextBoxWatermark CreateFolderWatermark
        {
            get
            {
                return new TextBoxWatermark("Введите имя каталога");
            }
        }
        public static TextBoxWatermark RenameFolderWatermark
        {
            get
            {
                return new TextBoxWatermark("Введите новое имя каталога");
            }
        }
    }
}
