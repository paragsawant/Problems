using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ClassLibrary.Common
{
    public abstract class SqlClient<T>
    {
        private readonly string _connectionString;

        protected SqlClient()
        {
            this._connectionString =
                $"Data Source = (LocalDB)\\MSSQLLocalDB; Initial Catalog = {CommonConstant.DBName}; Integrated Security = SSPI; Connection Timeout = 60";
        }

        private T Result { get; set; }

        public virtual async Task<T> ExecuteReaderAsync()
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(this._connectionString);
            using (var conn = new SqlConnection(connectionStringBuilder.ToString()))
            {
                using (var command = conn.CreateCommand())
                {
                    command.CommandTimeout = 60;
                    this.SetCommandDetails(command);
                    await conn.OpenAsync();

                    try
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            this.Result = await this.ReadAsync(reader);
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

        protected abstract Task<T> ReadAsync(DbDataReader reader);

        protected abstract void SetCommandDetails(DbCommand command);

        public SqlParameter BigIntParameter(string parameterName, long? value)
        {
            return new SqlParameter
            {
                ParameterName = parameterName,
                SqlDbType = SqlDbType.BigInt,
                Value = value.HasValue ? (object)value.Value : DBNull.Value
            };
        }

        public SqlParameter BoolParameter(string parameterName, bool value)
        {
            return new SqlParameter { ParameterName = parameterName, SqlDbType = SqlDbType.Bit, Value = value };
        }

        public SqlParameter DatetimeParameter(string parameterName, DateTime? value)
        {
            return new SqlParameter
            {
                ParameterName = parameterName,
                SqlDbType = SqlDbType.DateTime,
                Value = value.HasValue ? (object)value.Value : DBNull.Value
            };
        }

        public SqlParameter TableValuedParameter(string parameterName, DataTable value)
        {
            return new SqlParameter { ParameterName = parameterName, SqlDbType = SqlDbType.Structured, Value = value };
        }

        public SqlParameter NVarCharParameter(string parameterName, string value)
        {
            return new SqlParameter { ParameterName = parameterName, SqlDbType = SqlDbType.NVarChar, Value = value };
        }
    }
}