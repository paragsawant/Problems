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

        public Task<List<Country>> GetCounties()
        {
            var obj = new GetCountries();
            return obj.ExecuteReaderAsync();
        }
    }
}
