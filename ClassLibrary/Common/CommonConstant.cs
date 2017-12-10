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

        public const string GetPoliciesStoredProcedure = "[dbo].[GetPolicies]";

        public const string EnrollPolicyStoredProcedure = "[dbo].[EnrollPolicy]";

        public const string CancelPolicyStoredProcedure = "[dbo].[CancelPolicy]";

        public const string DBName = "PetInsuranceDb";

        public const string ParamPolicyNumber = "@policyNumber";

        public const string ParamCountryId = "@countryId";

        public const string ParamPetDetails = "@petDetails";

        public const string ParamPetOwnerName = "@petOwnerName";

        public const string ParamPetId = "@petId";
    }
}
