namespace DatabaseTask.Services.Messages
{ 
    public class CreateFolderWindowCloseMessage
    {
        public string FolderName { get; }

        public CreateFolderWindowCloseMessage(string folderName)
        {
            FolderName = folderName;
        }
    }
}
