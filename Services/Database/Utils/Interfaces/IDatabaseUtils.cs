namespace DatabaseTask.Services.Database.Utils.Interfaces
{
    public interface IDatabaseUtils
    {
        public string BuildConnectionString(string connectionString);
        public void DetachDatabase();
    }
}
