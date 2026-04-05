namespace DatabaseTask.Services.Database.Utils.Interfaces
{
    public interface IDatabasePath
    {
        public string NormalizeDatabasePath(string path);
        public string DenormalizePath(string path);
    }
}
