using ClassLibrary.Common;
using ClassLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;

namespace ClassLibrary.Data.Sql
{
    public class GetCountries : SqlClient<List<Country>>
    {
        public GetCountries(Func<Task<string>> connectionStringFunc) : base(connectionStringFunc)
        {
        }

        public async Task<List<Country>> ExecuteReaderAsync()
        {
            return await this.ExecuteReaderAsync();
        }

        protected override async Task<List<Country>> ReadAsync(DbDataReader reader)
        {
            List<Country> countries = new List<Country>();
            while (await reader.ReadAsync())
            {
                countries.Add(ReadCountry(reader));
            }

            return countries;
        }

        protected override void SetCommandDetails(DbCommand command)
        {
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = CommonConstant.GetCountriesStoredProcedure;
        }

        private static Country ReadCountry(IDataRecord reader)
        {
            return new Country() { Id = (int)reader.GetValue(0), Name = (string)reader.GetValue(1), IsoCode = (string)reader.GetValue(2) };
        }
    }
}
