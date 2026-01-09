using DatabaseTask.Services.Database.Utils.Interfaces;
using Microsoft.Data.SqlClient;
using System;

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

        public bool IsDatabaseExist()
        {
            string connection = BuildMainConnectionString();
            using var sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();
            string query = "SELECT CASE WHEN DB_ID(@dbName) IS NOT NULL THEN 1 ELSE 0 END";
            var command = new SqlCommand(query, sqlConnection);
            command.Parameters.AddWithValue("@dbName", _initialCatalog);
            int result = (int)command.ExecuteScalar();
            return result == 1;
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
