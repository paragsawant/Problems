using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebAPI.Data.Sql
{
    public class SqlSetup
    {

        private const string SqlServer = "(LocalDB)\\MSSQLLocalDB";

        public const string ConnectionStringFormat = "Server=" + SqlServer + ";Database={0};Trusted_Connection=yes;Connection Timeout=60";

        public const string PetInsuranceDatabaseName = "PetInsuranceDb";

        private const string CreateDatabaseTemplate = "CREATE DATABASE {0}";

        /// <summary>Database exists query template</summary>
        private const string DatabaseExistsTemplate = "SELECT database_id FROM sys.databases WHERE Name='{0}'";

        /// <summary>Drop database query template</summary>
        private const string DropDatabaseTemplate = "ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE {0}";

        /// <summary>The connection string format. TODO: Move this to common app config so other tests can also access</summary>
        private const string LocalServerConnectionStringTemplate = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog={0};Integrated Security=SSPI;Connection Timeout=60";


        static SqlSetup()
        {
            try
            {
                CreateMetadataSqlDatabase(PetInsuranceDatabaseName).ConfigureAwait(false).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                // Note: If these tests fail due to JSON_VALUE not being valid, you may need to upgrade (LocalDb)\MsSqlLocalDb to 2016. You can do this by going to
                // c:\Program Files\Microsoft SQL Server\130\Tools\Binn and running the following commands:
                //      sqllocaldb stop mssqllocaldb
                //      sqllocaldb delete mssqllocaldb
                //      sqllocaldb start "MSSQLLocalDB"
                ////
                throw new Exception($"Metadata Cit Db was not created successfully; Exception: {ex}");
            }
        }


        public static void SetupBaseTables(string connectionString)
        {
        }
        private static async Task CreateMetadataSqlDatabase(string databaseName)
        {
            await CreateLocalTestDatabaseAsync(databaseName);

        }

        public static Task CreateLocalTestDatabaseAsync(string databaseName)
        {
            var serverConnectionstring = GetLocalServerConnectionString();
            return CreateTestDatabaseAsync(serverConnectionstring, databaseName);
        }

        private static string GetLocalServerConnectionString()
        {
            return string.Format(LocalServerConnectionStringTemplate, "master");
        }

        private static async Task CreateTestDatabaseAsync(string serverConnectionString, string databaseName)
        {
            var dbExists = await DatabaseExistsAsync(serverConnectionString, databaseName);

            if (dbExists)
            {
                await DeleteDatabaseAsync(serverConnectionString, databaseName);
            }

            await CreateDatabaseAsync(serverConnectionString, databaseName);
        }

        private static async Task<bool> DatabaseExistsAsync(string connectionString, string databaseName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = string.Format(DatabaseExistsTemplate, databaseName);
                using (var command = new SqlCommand(query, connection))
                {
                    await connection.OpenAsync();
                    var result = await command.ExecuteScalarAsync();
                    if (result != null)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static async Task CreateDatabaseAsync(string connectionString, string databaseName)
        {
            var commandText = string.Format(CreateDatabaseTemplate, databaseName);
            await ExecuteNonQueryAsync(connectionString, commandText);
        }

        private static async Task DeleteDatabaseAsync(string connectionString, string databaseName)
        {
            var commandText = string.Format(DropDatabaseTemplate, databaseName);
            await ExecuteNonQueryAsync(connectionString, commandText);
        }

        private static async Task ExecuteNonQueryAsync(string connectionString, string commandText)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = commandText;
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}