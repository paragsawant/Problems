using ClassLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    interface IDataLayerAccess
    {
        Task<List<Country>> GetCounties();
    }
}
