using ClassLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using ClassLibrary.Model;
using System.Data;

namespace ClassLibrary.Data.Sql
{
    public class AddPetToPolicy : SqlClient<bool>
    {
        private readonly string _policyNumber;
        private readonly IList<Pet> _pets;

        public AddPetToPolicy(string policyNumber, IList<Pet> pets)
        {
            this._pets = pets;
            this._policyNumber = policyNumber;
        }

        protected override async Task<bool> ReadAsync(DbDataReader reader)
        {
            await reader.ReadAsync();
            int result = (int)reader.GetValue(0);
            if (result == 1)
            {
                return true;
            }
            else
            {
                await reader.NextResultAsync();
                await reader.ReadAsync();
                string exception = (string)reader.GetValue(0);
                return false;
            }
        }

        protected override void SetCommandDetails(DbCommand command)
        {
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = CommonConstant.AddPetToPolicyStoredProcedure;
            command.Parameters.Add(this.NVarCharParameter(CommonConstant.ParamPolicyNumber, this._policyNumber));
            command.Parameters.Add(this.TableValuedParameter(CommonConstant.ParamPetDetails, Pet.ToDataTable(this._pets)));
        }
    }
}
