using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemA.Model
{
    public class Employee
    {
        public Employee(int id, string firstName, string lastName, string address) : this(id, firstName, lastName, DateTime.Now, DateTime.Now.AddYears(-10), address)
        { }

        public Employee(int id, string firstName, string lastName, DateTime birthDate, DateTime dateOfJoining, string address)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = birthDate;
            DateOfJoining = dateOfJoining;
            EmployeeAddress = address;
        }

        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string EmployeeAddress { get; set; }
    }
}
