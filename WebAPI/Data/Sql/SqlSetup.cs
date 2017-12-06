using System;
using System.Data.SqlClient;

namespace WebAPI.Data.Sql
{
    public class SqlSetup
    {
        public const string ConnectionStringFormat =
            "Server=" + SqlServer + ";Database={0};Trusted_Connection=yes;Connection Timeout=60";

        public const string PetInsuranceDatabaseName = "PetInsuranceDb";

        private const string CreateDatabaseTemplate = "CREATE DATABASE {0}";

        /// <summary>Database exists query template</summary>
        private const string DatabaseExistsTemplate = "SELECT database_id FROM sys.databases WHERE Name='{0}'";

        private const string LocalServerConnectionStringTemplate =
            "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog={0};Integrated Security=SSPI;Connection Timeout=60";

        private const string SqlServer = "(LocalDB)\\MSSQLLocalDB";


        static SqlSetup()
        {
            try
            {
                CreateMetadataSqlDatabase(PetInsuranceDatabaseName);
            }
            catch (Exception ex)
            {
                // Note: If these tests fail due to JSON_VALUE not being valid, you may need to upgrade (LocalDb)\MsSqlLocalDb to 2016. You can do this by going to
                // c:\Program Files\Microsoft SQL Server\130\Tools\Binn and running the following commands:
                //      sqllocaldb stop mssqllocaldb
                //      sqllocaldb delete mssqllocaldb
                //      sqllocaldb start "MSSQLLocalDB"
                ////
                throw new Exception($"TestDb was not created successfully; Exception: {ex}");
            }
        }

        public static void CreateLocalTestDatabase(string databaseName)
        {
            var serverConnectionstring = GetLocalServerConnectionString();
            CreateTestDatabase(serverConnectionstring, databaseName);
        }


        public static void SetupBaseTables(string connectionString)
        {
        }

        private static bool CreateDatabaseIfNotExists(string connectionString, string databaseName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = string.Format(DatabaseExistsTemplate, databaseName);
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        return true;
                    }
                }
            }
            var commandText = string.Format(CreateDatabaseTemplate, databaseName);
            ExecuteNonQuery(connectionString, commandText);
            return false;
        }

        private static void CreateMetadataSqlDatabase(string databaseName)
        {
            CreateLocalTestDatabase(databaseName);
        }

        private static void CreateTestDatabase(string serverConnectionString, string databaseName)
        {
            CreateDatabaseIfNotExists(serverConnectionString, databaseName);
        }

        private static void ExecuteNonQuery(string connectionString, string commandText)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = commandText;
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception($"ExecuteNonQuery failed for Query {commandText}; Exception: {e}");
            }
        }

        private static string GetLocalServerConnectionString()
        {
            return string.Format(LocalServerConnectionStringTemplate, "master");
        }
    }
}