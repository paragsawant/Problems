using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Model;
using ClassLibrary.Data.Sql;

namespace ClassLibrary
{
    public class DataLayerAccess : IDataLayerAccess
    {
        public DataLayerAccess()
        {

        }

        public Task<bool> AddPetToPolicy(string policyNumber, IList<Pet> pets)
        {
            var obj = new AddPetToPolicy(policyNumber, pets);
            return obj.ExecuteReaderAsync();
        }

        public Task<bool> CancelPolicy(string PolicyNumber)
        {
            var obj = new CancelPolicy(PolicyNumber);
            return obj.ExecuteReaderAsync();
        }

        public Task<bool> EnrollPolicy(Policy policy)
        {
            var obj = new EnrollPolicy(policy);
            return obj.ExecuteReaderAsync();
        }

        public Task<List<Country>> GetCounties()
        {
            var obj = new GetCountries();
            return obj.ExecuteReaderAsync();
        }

        public Task<List<Policy>> GetPolicies()
        {
            var obj = new GetPolicies();
            return obj.ExecuteReaderAsync();
        }

        public Task<Policy> GetPolicy(string PolicyNumber)
        {
            var obj = new GetPolicy(PolicyNumber);
            return obj.ExecuteReaderAsync();
        }

        public Task<bool> RemovePetFromPolicy(string policyNumber, int petId)
        {
            var obj = new RemovePetFromPolicy(policyNumber, petId);
            return obj.ExecuteReaderAsync();
        }

        public Task<bool> TransferPet(int oldPetOwnerId, int petId, int newPetOwnerId)
        {
            var obj = new TransferPet(oldPetOwnerId, petId,newPetOwnerId);
            return obj.ExecuteReaderAsync();
        }
    }
}
