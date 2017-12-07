using System.Collections.Generic;
using System.Threading.Tasks;
using ClassLibrary.Model;

namespace ClassLibrary
{
    public interface IDataLayerAccess
    {
        Task<List<Country>> GetCounties();
    }
}