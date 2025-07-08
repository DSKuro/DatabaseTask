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
    }
}
