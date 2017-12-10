using ClassLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace ClassLibrary.Data.Sql
{
    public class CancelPolicy: SqlClient<bool>
    {
        private readonly string _policyNumber;
        public CancelPolicy(string policyNumber)
        {
            this._policyNumber = policyNumber;
        }

        protected override Task<bool> ReadAsync(DbDataReader reader)
        {
            throw new NotImplementedException();
        }

        protected override void SetCommandDetails(DbCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
