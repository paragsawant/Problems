using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Common
{
    public class CommonConstant
    {
        public const int DefaultSqlCommandTimeoutInSecs = 60;

        public const string GetCountriesStoredProcedure = "[dbo].[GetCountries]";

        public const string DBName = "PetInsuranceDb";
    }
}
