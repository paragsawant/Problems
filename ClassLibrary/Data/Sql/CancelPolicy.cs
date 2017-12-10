using ClassLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;

namespace ClassLibrary.Data.Sql
{
    public class CancelPolicy: SqlClient<bool>
    {
        private readonly string _policyNumber;
        public CancelPolicy(string policyNumber)
        {
            this._policyNumber = policyNumber;
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
            command.CommandText = CommonConstant.CancelPolicyStoredProcedure;
            command.Parameters.Add(this.NVarCharParameter(CommonConstant.ParamPolicyNumber, this._policyNumber));
        }
    }
}
