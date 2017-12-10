using System.Collections.Generic;
using System.Threading.Tasks;
using ClassLibrary.Model;

namespace ClassLibrary
{
    public interface IDataLayerAccess
    {
        Task<List<Country>> GetCounties();

        Task<List<Policy>> GetPolicies();

        Task<Policy> GetPolicy(string PolicyNumber);

        Task<bool> EnrollPolicy(Policy policy);

        Task<bool> CancelPolicy(string PolicyNumber);
    }
}