using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Common
{
    public abstract class SqlClient<T>
    {
        private readonly string applicationName = ".Net SqlClient Data Provider";

        /// <summary>Initializes a new instance of the <see cref="SqlClient{T}" /> class.</summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="diagnosticsFactory">The diagnostics factory</param>
        protected SqlClient()
        {
            this.ConnectionStringFunc = () => Task.FromResult($"Data Source = (LocalDB)\\MSSQLLocalDB; Initial Catalog = { CommonConstant.DBName }; Integrated Security = SSPI; Connection Timeout = 60");
        }

        /// <summary>The connection string.</summary>
        protected Func<Task<string>> ConnectionStringFunc { get; set; }

        /// <summary>The result.</summary>
        protected T Result { get; set; }

        /// <summary>The execute non query.</summary>
        /// <returns> The <see cref="Task" />. </returns>
        public async Task<int> ExecuteNonQueryAsync(int commandTimeout = CommonConstant.DefaultSqlCommandTimeoutInSecs)
        {
            var correlationId = Guid.NewGuid().ToString();

            var connectionString = await this.ConnectionStringFunc().ConfigureAwait(false);
            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            connectionStringBuilder.ApplicationName = this.applicationName;

            using (var conn = new SqlConnection(connectionStringBuilder.ToString()))
            {
                using (var command = conn.CreateCommand())
                {
                    command.CommandTimeout = commandTimeout;
                    this.SetCommandDetails(command);
                    var dbName = connectionStringBuilder.InitialCatalog;

                    await this.TryOpenConnection(conn, dbName);

                    try
                    {
                        return await command.ExecuteNonQueryAsync();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        /// <summary>The execute reader.</summary>
        /// <returns> The <see cref="Task" />. </returns>
        public virtual async Task<T> ExecuteReaderAsync(int retryCount = 0, int commandTimeout = CommonConstant.DefaultSqlCommandTimeoutInSecs)
        {
            var correlationId = Guid.NewGuid().ToString();
            var connectionString = await this.ConnectionStringFunc().ConfigureAwait(false);
            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            connectionStringBuilder.ApplicationName = this.applicationName;

            using (var conn = new SqlConnection(connectionStringBuilder.ToString()))
            {
                using (var command = conn.CreateCommand())
                {
                    command.CommandTimeout = commandTimeout;
                    this.SetCommandDetails(command);
                    var dbName = connectionStringBuilder.InitialCatalog;

                    await this.TryOpenConnection(conn, dbName);

                    try
                    {
                        using (var reader = await this.GetExecuteReaderAsync(command))
                        {
                            this.Result = await this.ReadAsync(reader);

                            this.ExecuteFinished();
                            return this.Result;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        /// <summary>The bigint parameter.</summary>
        /// <param name="parameterName"> The parameter name. </param>
        /// <param name="value"> The value. </param>
        /// <returns> The <see cref="SqlParameter" />. </returns>
        protected SqlParameter BigIntParameter(string parameterName, long? value)
        {
            return new SqlParameter { ParameterName = parameterName, SqlDbType = SqlDbType.BigInt, Value = (object)value ?? DBNull.Value };
        }

        /// <summary>The bool parameter.</summary>
        /// <param name="parameterName"> The parameter name. </param>
        /// <param name="value"> The value. </param>
        /// <returns> The <see cref="SqlParameter" />. </returns>
        protected SqlParameter BoolParameter(string parameterName, bool? value)
        {
            return new SqlParameter { ParameterName = parameterName, SqlDbType = SqlDbType.Bit, Value = (object)value ?? DBNull.Value };
        }

        /// <summary>The datetime parameter.</summary>
        /// <param name="parameterName"> The parameter name. </param>
        /// <param name="value"> The value. </param>
        /// <returns> The <see cref="SqlParameter" />. </returns>
        protected SqlParameter DatetimeParameter(string parameterName, DateTime? value)
        {
            return new SqlParameter { ParameterName = parameterName, SqlDbType = SqlDbType.DateTime2, Value = value };
        }
        
        /// <summary>The execute finished.</summary>
        protected virtual void ExecuteFinished()
        {
        }

        /// <summary>The int parameter.</summary>
        /// <param name="parameterName"> The parameter name. </param>
        /// <param name="value"> The value. </param>
        /// <returns> The <see cref="SqlParameter" />. </returns>
        protected SqlParameter IntParameter(string parameterName, int value)
        {
            return new SqlParameter { ParameterName = parameterName, SqlDbType = SqlDbType.Int, Value = value };
        }

        /// <summary>The n var char parameter.</summary>
        /// <param name="parameterName"> The parameter name. </param>
        /// <param name="value"> The value. </param>
        /// <returns> The <see cref="SqlParameter" />. </returns>
        protected SqlParameter NVarCharParameter(string parameterName, string value)
        {
            return new SqlParameter { ParameterName = parameterName, SqlDbType = SqlDbType.NVarChar, Value = (object)value ?? DBNull.Value };
        }

        /// <summary>The read method.</summary>
        /// <param name="reader"> The reader. </param>
        /// <returns> The <see cref="Task" />. </returns>
        protected abstract Task<T> ReadAsync(DbDataReader reader);

        /// <summary> The set command details. </summary>
        /// <param name="command"> The command. </param>
        protected abstract void SetCommandDetails(DbCommand command);

        /// <summary>Retries ExecuteReaderAsync based on retry value and returns SqlDataReader</summary>
        /// <param name="command">Sql command</param>
        /// <param name="retryCount">retry count</param>
        /// <returns>SqlDataReader</returns>
        protected async Task<SqlDataReader> GetExecuteReaderAsync(SqlCommand command)
        {
                return await command.ExecuteReaderAsync();

        }

        private async Task TryOpenConnection(DbConnection conn, string dbName)
        {
            var timer = Stopwatch.StartNew();
            try
            {
                await conn.OpenAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
