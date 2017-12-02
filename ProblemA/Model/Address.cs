using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemA.Model
{
    [Serializable]
    public class Address
    {
        public Address(string address1, string address2, int zipCode, string city, string state, string country)
        {
            Address1 = address1;
            Address2 = address2;
            ZipCode = zipCode;
            City = city;
            State = state;
            Country = country;
        }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
