namespace DatabaseTask.Services.Operations.FilesOperations.Interfaces
{
    public interface IFilesOperations
    {
        public bool CreateFolder(string path);
        public bool RenameFolder(string path, string newName);
        public bool DeleteFolder(string path);
        public bool DeleteFile(string path);
    }
}
