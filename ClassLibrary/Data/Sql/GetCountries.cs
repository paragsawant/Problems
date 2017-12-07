// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetCountries.cs" company="Microsoft">
// Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using ClassLibrary.Common;
using ClassLibrary.Model;

namespace ClassLibrary.Data.Sql
{
    public class GetCountries : SqlClient<List<Country>>
    {
        protected override async Task<List<Country>> ReadAsync(DbDataReader reader)
        {
            var countries = new List<Country>();
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
            return new Country
                   {
                       Id = (int) reader.GetValue(0),
                       Name = (string) reader.GetValue(1),
                       IsoCode = (string) reader.GetValue(2)
                   };
        }
    }
}