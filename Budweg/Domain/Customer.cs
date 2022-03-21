using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budweg.Domain
{
    public class Customer
    {

        public int CustumerId { get; set; }
        public string Name { get; set; }
        public string TelephoneNumber { get; set; }
        public string CVRNumber { get; set; }
        public string Workshop { get; set; }

        public Customer(int custumerId, string name, string telephoneNumber, string cVRNumber, string workshop)
        {
            CustumerId = custumerId;
            Name = name;
            TelephoneNumber = telephoneNumber;
            CVRNumber = cVRNumber;
            Workshop = workshop;
        }

    }
}
