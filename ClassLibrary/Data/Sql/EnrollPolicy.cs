using ClassLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;
using ClassLibrary.Model;

namespace ClassLibrary.Data.Sql
{
    public class EnrollPolicy : SqlClient<bool>
    {
        Policy _policy;
        public EnrollPolicy(Policy policy)
        {
            this._policy = policy;
        }

        protected override async Task<bool> ReadAsync(DbDataReader reader)
        {
            await reader.ReadAsync();
            int result = (int)reader.GetValue(0);
            return result == 1 ? true : false;
        }

        protected override void SetCommandDetails(DbCommand command)
        {
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = CommonConstant.EnrollPolicyStoredProcedure;
            command.Parameters.Add(this.NVarCharParameter(CommonConstant.ParamPetOwnerName, this._policy.PetOwnerName));
            command.Parameters.Add(this.BigIntParameter(CommonConstant.ParamCountryId, this._policy.CountryId));
            command.Parameters.Add(this.TableValuedParameter(CommonConstant.ParamPetDetails, Pet.ToDataTable(this._policy.Pets)));
        }
    }
}
