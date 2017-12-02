using ProblemA.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProblemA
{
    public class ModelService<T, U> : IModelService<T, U>
    {
        public string GetHash()
        {
            throw new NotImplementedException();
        }

        public string GetHash(T input1)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, object> ValidateObject(T input1, U input2)
        {
            Dictionary<string, object> returnObject = new Dictionary<string, object>();
            if (input1 != null && input2 != null)
            {
                Type typeOne = typeof(T);
                Type typeTwo = typeof(U);
                Dictionary<string, object> objectInfo = new Dictionary<string, object>();
                foreach (PropertyInfo pi in typeOne.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    objectInfo.Add(typeOne.GetProperty(pi.Name).Name, typeOne.GetProperty(pi.Name).GetValue(input1, null));

                }

                foreach (PropertyInfo pi in typeTwo.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (objectInfo.Any())
                    {
                        if (objectInfo.ContainsKey(typeTwo.GetProperty(pi.Name).Name))
                        {
                            if (!objectInfo[typeTwo.GetProperty(pi.Name).Name].Equals(typeTwo.GetProperty(pi.Name).GetValue(input2, null)))
                            {
                                returnObject.Add(pi.Name, new List<object> { objectInfo[typeTwo.GetProperty(pi.Name).Name], typeTwo.GetProperty(pi.Name).GetValue(input2, null) });
                            }
                        }
                    }
                }
            }

            return returnObject;
        }

        public List<string> ValidateObjects(T input1, U input2)
        {
            throw new NotImplementedException();
        }
    }
}
