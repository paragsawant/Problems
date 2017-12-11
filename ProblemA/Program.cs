using Newtonsoft.Json;
using ProblemA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace ProblemA
{
    class Program
    {
        static void Main(string[] args)
        {
            Person personObj = new Person("sneha", "sawant", new List<Address> { new Address("Add1", "Add3", 98007, "Bellevue", "WA", "USA"), new Address("Add2", "Add1", 98005, "Bellevue", "WA", "USA") });
            Console.WriteLine("Above is the Person Object");
            Console.WriteLine(JsonConvert.SerializeObject(personObj));
            Contact contactObj = new Contact("parag", "sawant", new List<Address> { new Address("Add1", "Add2", 98007, "Bellevue", "WA", "USA"), new Address("Add2", "Add1", 98005, "Bellevue", "WA", "USA") });

            Console.WriteLine(JsonConvert.SerializeObject(contactObj));
            Console.WriteLine("Above is the Contact Object");
            Employee employeeObj = new Employee(1, "sneha", "sawant", "Redmond");
            Console.WriteLine(JsonConvert.SerializeObject(employeeObj));
            Console.WriteLine("Above is the Employee Object");

            ModelService<Person, Contact> modelService1 = new ModelService<Person, Contact>();
            var returnType = modelService1.ValidateObject(personObj, contactObj);
            Console.WriteLine("Below is the example of problem 1: Difference between Person and contact");
            Console.WriteLine(JsonConvert.SerializeObject(returnType));
            ModelService<Person, Employee> modelService2 = new ModelService<Person, Employee>();
            var listReturnType = modelService2.ListOfCommonProperties(personObj, employeeObj);
            Console.WriteLine("Below is the example of problem 3: Common between person and employee");
            Console.WriteLine(JsonConvert.SerializeObject(listReturnType));
            
            ModelService<Person,Contact > modelService3 = new ModelService<Person, Contact>();

            var result = modelService3.GetHashCode(HashAlgorithmType.Md5, personObj);
            Console.WriteLine("Below is the example of problem 4: Md5 of Person Object");
            Console.WriteLine(result);
            result = modelService3.GetHashCode(HashAlgorithmType.Sha1, personObj);
            Console.WriteLine("Below is the example of problem 4: Sha1 of Person Object");
            Console.WriteLine(result);
            Console.WriteLine("Press Any Key to Exist");
            Console.ReadLine();
        }
    }
}
