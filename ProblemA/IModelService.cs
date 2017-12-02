using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemA
{
    public interface IModelService<in T, in U>
    {
        IDictionary<string, object> ValidateObject(T input1, U input2);

        List<string> ValidateObjects(T input1, U input2);

        string GetHash(T input1);

        string GetHash();
    }
}
