namespace DatabaseTask.Models.Categories
{
    public class WindowCategory
    {
        public string Value { get; private set; }

        public WindowCategory(string value) 
        {
            Value = value;
        }

        public static WindowCategory CreateFolderCategory
        {
            get
            {
                return new WindowCategory("Создать каталог");
            }
        }
        public static WindowCategory RenameFolderCategory
        {
            get
            {
                return new WindowCategory("Переименовать каталог");
            }
        }
        public static WindowCategory DeleteFolderCategory
        {
            get
            {
                return new WindowCategory("Удалить каталог");
            }
        }
    }
}
