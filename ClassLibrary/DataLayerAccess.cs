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
    }
}
