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
    public class GetPolicy : SqlClient<Policy>
    {
        private string _policyNumber;
        public GetPolicy(string policyNumber)
        {
            this._policyNumber = policyNumber;
        }

        protected override async Task<Policy> ReadAsync(DbDataReader reader)
        {
            var policy = new Policy();
            while (await reader.ReadAsync())
            {
                policy = ReadPolicy(reader);
            }
            await reader.NextResultAsync();
            List<Pet> pets = new List<Pet>();
            while (await reader.ReadAsync())
            {
                var pet = ReadPet(reader);
                pets.Add(pet);
            }
            if (pets.Any() && policy != null)
            {
                policy.Pets = pets;
            }

            return policy;
        }

        protected override void SetCommandDetails(DbCommand command)
        {
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = CommonConstant.GetPoliciesStoredProcedure;
            command.Parameters.Add(this.NVarCharParameter(CommonConstant.ParamPolicyNumber, this._policyNumber));
        }

        private static Policy ReadPolicy(IDataRecord reader)
        {
            return new Policy
            {
                PolicyId = (int)reader.GetValue(0),
                PolicyDate = (DateTime)reader.GetValue(1),
                PolicyNumber = (string)reader.GetValue(2),
                PetOwnerName = (string)reader.GetValue(3),
                CountryId = (int)reader.GetValue(4)
            };
        }

        private static Pet ReadPet(IDataRecord reader)
        {
            return new Pet
            {
                PetId = (int)reader.GetValue(0),
                PetName = (string)reader.GetValue(1),
                PetType = EnumUtil.ConvertToEnum<PetType>((string)reader.GetValue(2)),
                DateOfBirth = (DateTime)reader.GetValue(3),
                PetOwnerId = (int)reader.GetValue(4)
            };
        }
    }
}
