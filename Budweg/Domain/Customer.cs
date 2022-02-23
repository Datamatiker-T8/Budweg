using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budweg.Domain
{
    public class Customer
    {
        public string Name { get; set; }
        public string TelephoneNumber { get; set; }
        public string CVRNumber { get; set; }
        public string Workshop { get; set; }

        public Customer(string name, string telephoneNumber, string cVRNumber, string workshop)
        {
            Name = name;
            TelephoneNumber = telephoneNumber;
            CVRNumber = cVRNumber;
            Workshop = workshop;
        }

    }
}
