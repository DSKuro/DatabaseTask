namespace DatabaseTask.Services.FileManagerOperations.Accessibility.Interfaces
{
    public interface IFileManagerFolderOperationsPermissions
    {
        public void CanDoOperationOnFolder();
        public void CanDeleteFolder();
        public void CanCopyCatalog();
    }
}
