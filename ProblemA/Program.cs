using Newtonsoft.Json;
using ProblemA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemA
{
    class Program
    {
        static void Main(string[] args)
        {
            Person personObj = new Person("sneha", "sawant", new List<Address> { new Address("Add1", "Add3", 98007, "Bellevue", "WA", "USA"), new Address("Add2", "Add1", 98005, "Bellevue", "WA", "USA") });
            Contact contactObj = new Contact("parag", "sawant", new List<Address> { new Address("Add1", "Add3", 98007, "Bellevue", "WA", "USA"), new Address("Add2", "Add1", 98005, "Bellevue", "WA", "USA") });
            Employee employeeObj = new Employee(1, "sneha", "sawant", "Redmond");
            ModelService<Person, Contact> modelService = new ModelService<Person, Contact>();
            var returnType = modelService.ValidateObject(personObj, contactObj);

            Console.WriteLine(JsonConvert.SerializeObject(returnType));
            Console.ReadLine();
        }
    }
}
