using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemA.Model
{
    [Serializable]
    public class Employee
    {
        public Employee(int id, string firstName, string lastName, string address) : this(id, firstName, lastName, DateTime.Now, DateTime.Now.AddYears(-10), address,911)
        { }

        public Employee(int id, string firstName, string lastName, DateTime birthDate, DateTime dateOfJoining, string address, int contactNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = birthDate;
            DateOfJoining = dateOfJoining;
            Address = address;
            ContactNumber = contactNumber;
        }

        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string Address { get; set; }
        public int ContactNumber { get; set; }
    }
}
