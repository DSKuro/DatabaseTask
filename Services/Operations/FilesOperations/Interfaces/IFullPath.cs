namespace DatabaseTask.Services.Operations.FilesOperations.Interfaces
{
    public interface IFullPath
    {
        public string? PathToCoreFolder { get; set; }

        public string GetFullpath(string pathToItem);
    }
}
