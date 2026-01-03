namespace DatabaseTask.Services.Operations.FilesOperations.Interfaces
{
    public interface IFilesOperations
    {
        public bool CreateFolder(string path);
        public bool RenameFolder(string oldPath, string newPath);
        public bool DeleteFolder(string path);
        public bool DeleteFile(string path);
        public bool CopyFolder(string oldPath, string newPath);
        public bool CopyFile(string oldPath, string newPath);
        public bool MoveFile(string oldPath, string newPath);
    }
}
