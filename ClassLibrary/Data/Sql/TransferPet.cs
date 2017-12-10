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
    public class TransferPet : SqlClient<bool>
    {
        private readonly int _oldPetOwnerId;
        private readonly int _petId;
        private readonly int _newPetOwnerId;
        public TransferPet(int oldPetOwnerId, int petId, int newPetOwnerId)
        {
            this._oldPetOwnerId = oldPetOwnerId;
            this._newPetOwnerId = newPetOwnerId;
            this._petId = petId;
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
            command.CommandText = CommonConstant.TransferPetStoredProcedure;
            command.Parameters.Add(this.BigIntParameter(CommonConstant.ParamOldPetOwnerId, this._oldPetOwnerId));
            command.Parameters.Add(this.BigIntParameter(CommonConstant.ParamPetId, this._petId));
            command.Parameters.Add(this.BigIntParameter(CommonConstant.ParamNewPetOwnerId, this._newPetOwnerId));
        }
    }
}
