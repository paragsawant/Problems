﻿using ClassLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;

namespace ClassLibrary.Data.Sql
{
    public class RemovePetFromPolicy : SqlClient<bool>
    {
        private readonly string _policyNumber;
        private readonly int _petId;
        public RemovePetFromPolicy(string policyNumber, int petId)
        {
            this._petId = petId;
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
            command.CommandText = CommonConstant.RemovePetFromPolicyStoredProcedure;
            command.Parameters.Add(this.NVarCharParameter(CommonConstant.ParamPolicyNumber, this._policyNumber));
            command.Parameters.Add(this.BigIntParameter(CommonConstant.ParamPetId, this._petId));
        }
    }
}
