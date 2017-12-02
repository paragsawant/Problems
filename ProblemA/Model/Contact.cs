using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemA.Model
{
    public class Contact
    {
        public Contact(string firstName, string lastName, List<Address> addresses) : this(firstName, lastName, new DateTime(1987, 01, 13), addresses)
        { }

        public Contact(string firstName, string lastName, DateTime birthDate, List<Address> addresses)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = birthDate;
            Addresses = addresses;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Address> Addresses { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
