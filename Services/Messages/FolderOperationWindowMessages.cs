namespace DatabaseTask.Services.Messages
{ 
    public class FolderOperationWindowCloseMessage
    {
        public string FolderName { get; }

        public FolderOperationWindowCloseMessage(string folderName)
        {
            FolderName = folderName;
        }
    }
}
