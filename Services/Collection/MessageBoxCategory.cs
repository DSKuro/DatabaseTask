namespace DatabaseTask.Services.Collection
{
    public class MessageBoxCategory
    {
        public string Title { get; private set; }
        public string Content { get; private set; }

        public MessageBoxCategory(string title, string content)
        {
            Title = title;
            Content = content;
        }

        public static MessageBoxCategory DeleteFolderMessageBox
        {
            get
            {
                return new MessageBoxCategory("Удаление каталога", "Вы точно хотите удалить выбранные каталоги?");
            }
        }
        public static MessageBoxCategory DeleteFileMessageBox
        {
            get
            {
                return new MessageBoxCategory("Удаление файла", "Вы точно хотите удалить выбранные файлы?");
            }
        }
        public static MessageBoxCategory CopyFolderMessageBox
        {
            get
            {
                return new MessageBoxCategory("Копировать каталог",
                    "Вы точно хотите копировать выделенный каталог?");
            }
        }
        public static MessageBoxCategory CopyFileMessageBox
        {
            get
            {
                return new MessageBoxCategory("Копировать файл",
                    "Вы точно хотите копировать выделенный файл?");
            }
        }
    }
}
