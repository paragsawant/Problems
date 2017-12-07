// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlSetup.cs" company="Microsoft">
// Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebAPI.Data.Sql
{
    public class SqlSetup
    {
        public const string PetInsuranceDatabaseName = "PetInsuranceDb";

        private const string CreateDatabaseTemplate = "CREATE DATABASE {0}";

        /// <summary>Database exists query template</summary>
        private const string DatabaseExistsTemplate = "SELECT database_id FROM sys.databases WHERE Name='{0}'";

        private const string DropDatabaseTemplate = "Drop DATABASE {0}";

        private const string LocalServerConnectionStringTemplate =
            "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog={0};Integrated Security=SSPI;Connection Timeout=60";


        static SqlSetup()
        {
            try
            {
                CreateMetadataSqlDatabase(PetInsuranceDatabaseName);
                RunTestSqlScript(string.Format(LocalServerConnectionStringTemplate, PetInsuranceDatabaseName),
                    "Schema.sql");
                RunTestSqlScript(string.Format(LocalServerConnectionStringTemplate, PetInsuranceDatabaseName),
                    "InsertDataScript.sql");
                RunTestSqlScript(string.Format(LocalServerConnectionStringTemplate, PetInsuranceDatabaseName),
                    "SqlLogic.sql");
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

        public static void ExecuteScript(string connectionString, string script)
        {
            var commands = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var currentlyExecutingStatement = "";


            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (var command in commands.Where(command => !string.IsNullOrWhiteSpace(command)))
                        {
                            using (var sqlCommand = new SqlCommand(command, conn, transaction))
                            {
                                currentlyExecutingStatement = command;
                                sqlCommand.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public static void RunTestSqlScript(string connectionString, string scriptName)
        {
            var path = HttpContext.Current.Server.MapPath("~/bin/Data/Script/" + scriptName);
            var script = File.ReadAllText(path);
            ExecuteScript(connectionString, script);
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

            var commandText = string.Format(DropDatabaseTemplate, databaseName);
            ExecuteNonQuery(connectionString, commandText);

            commandText = string.Format(CreateDatabaseTemplate, databaseName);
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