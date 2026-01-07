using DatabaseTask.Services.Database.Utils.Interfaces;
using Microsoft.Data.SqlClient;

namespace DatabaseTask.Services.Database.Utils
{
    public class DatabaseUtils : IDatabaseUtils
    {
        private const string _mainCatalog = "master";
        private const string _initialCatalog = "Data";
        private const string _source = ".\\SQLEXPRESS";
        private const string _detachCommand = 
            @"ALTER DATABASE [Data] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; " +
            @"EXEC sp_detach_db 'Data', 'true'";

        public string BuildConnectionString(string filePath)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = _source,
                InitialCatalog = _initialCatalog,
                AttachDBFilename = filePath,
                IntegratedSecurity = true,
                TrustServerCertificate = true,
                ConnectTimeout = 50
            };

            return builder.ConnectionString;
        }

        public void DetachDatabase()
        {
            string connection = BuildMainConnectionString();
            using var sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();
            var command = new SqlCommand(_detachCommand, sqlConnection);
            command.ExecuteNonQuery();
        }

        private string BuildMainConnectionString()
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = _source,
                InitialCatalog = _mainCatalog,
                IntegratedSecurity = true,
                TrustServerCertificate = true,
                ConnectTimeout = 50
            };

            return builder.ConnectionString;
        }
    }
}
