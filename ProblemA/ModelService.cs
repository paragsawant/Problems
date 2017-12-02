using ProblemA.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProblemA
{
    public class ModelService<T, U> : IModelService<T, U>
    {

        public string GetHashCode(HashAlgorithmType cryptoServiceProvider, T input1)
        {

            switch (cryptoServiceProvider)
            {
                case HashAlgorithmType.Md5:
                    return GetKeyedHash<HMACMD5>(input1, ObjectToByteArray(input1));
                case HashAlgorithmType.Sha1:
                    return GetKeyedHash<HMACSHA1>(input1, ObjectToByteArray(input1));
                default:
                    break;
            }

            return string.Empty;
        }

        public IDictionary<string, object> ValidateObject(T input1, U input2)
        {
            Dictionary<string, object> returnObject = new Dictionary<string, object>();
            if (input1 != null && input2 != null)
            {
                Type tColl = typeof(ICollection<>);
                Type typeOne = typeof(T);
                int typeOnePropertyCount = typeOne.GetProperties(BindingFlags.Public | BindingFlags.Instance).Count();
                Type typeTwo = typeof(U);
                int typeTwoPropertyCount = typeTwo.GetProperties(BindingFlags.Public | BindingFlags.Instance).Count();
                Dictionary<string, PropertyInfo> objectInfo = new Dictionary<string, PropertyInfo>();
                foreach (PropertyInfo pi in typeOne.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    objectInfo.Add(typeOne.GetProperty(pi.Name).Name, typeOne.GetProperty(pi.Name));
                }

                foreach (PropertyInfo pi in typeTwo.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (objectInfo.Any())
                    {
                        if (objectInfo.ContainsKey(typeTwo.GetProperty(pi.Name).Name))
                        {
                            PropertyInfo propertyInfoInput1 = objectInfo[typeOne.GetProperty(pi.Name).Name];
                            PropertyInfo propertyInfoInput2 = pi;
                            Type t = propertyInfoInput1.PropertyType;
                            if (t.IsGenericType && tColl.IsAssignableFrom(t.GetGenericTypeDefinition()) ||
                                t.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == tColl))
                            {
                                IList listInput1 = propertyInfoInput1.GetValue(input1, null) as IList;
                                IList listInput2 = propertyInfoInput2.GetValue(input2, null) as IList;
                                if (listInput1.Count == listInput2.Count)
                                {
                                    for (int i = 0; i < listInput1.Count; i++)
                                    {
                                        List<object> notEqualObject = new List<object>();
                                        if (!GetKeyedHash<HMACMD5>(listInput1[i], ObjectToByteArray(listInput1[i])).Equals(GetKeyedHash<HMACMD5>(listInput2[i], ObjectToByteArray(listInput2[i]))))
                                        {
                                            if (returnObject.ContainsKey(pi.Name))
                                            {
                                                var data = returnObject[pi.Name] as IList;
                                                data.Add(listInput1[i]);
                                                data.Add(listInput2[i]);
                                                returnObject[pi.Name] = data;
                                            }
                                            else
                                            {
                                                returnObject.Add(pi.Name, new List<object> { listInput1[i], listInput2[i] });
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    returnObject.Add(pi.Name, new List<object> { propertyInfoInput1.GetValue(input1, null), typeTwo.GetProperty(pi.Name).GetValue(input2, null) });
                                }
                                Console.WriteLine(pi.Name + " IS an ICollection<>");

                            }
                            else if (!propertyInfoInput1.GetValue(input1, null).Equals(typeTwo.GetProperty(pi.Name).GetValue(input2, null)))
                            {
                                returnObject.Add(pi.Name, new List<object> { propertyInfoInput1.GetValue(input1, null), typeTwo.GetProperty(pi.Name).GetValue(input2, null) });
                            }
                        }
                    }
                }
            }

            return returnObject;
        }

        public List<string> ListOfCommonProperties(T input1, U input2)
        {
            List<string> properties = new List<string>();

            if (input1 != null && input2 != null)
            {
                Type typeOne = typeof(T);
                PropertyInfo[] typeOneProperties = typeOne.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                Type typeTwo = typeof(U);
                PropertyInfo[] typeTwoProperties = typeTwo.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                var lookup1 = typeOneProperties.ToLookup(x => x.Name);
                var lookup2 = typeTwoProperties.ToLookup(x => x.Name);

                properties = lookup1.SelectMany(l1s => lookup2[l1s.Key].Zip(l1s, (l2, l1) => l1)).Select(p => p.Name).ToList();
            }

            return properties;
        }

        private static string GetHash<T>(object instance) where T : HashAlgorithm, new()
        {
            T cryptoServiceProvider = new T();
            return computeHash(instance, cryptoServiceProvider);
        }

        private static string GetKeyedHash<T>(object instance, byte[] key) where T : KeyedHashAlgorithm, new()
        {
            T cryptoServiceProvider = new T { Key = key };
            return computeHash(instance, cryptoServiceProvider);
        }

        private static string computeHash<C>(object instance, C cryptoServiceProvider) where C : HashAlgorithm, new()
        {
            DataContractSerializer serializer = new DataContractSerializer(instance.GetType());
            using (MemoryStream memoryStream = new MemoryStream())
            {
                serializer.WriteObject(memoryStream, instance);
                cryptoServiceProvider.ComputeHash(memoryStream.ToArray());
                return Convert.ToBase64String(cryptoServiceProvider.Hash);
            }
        }

        private static byte[] ObjectToByteArray(Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
    }
}
