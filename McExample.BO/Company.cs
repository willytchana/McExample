using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McExample.BO
{
    public class Company
    {
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public long PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }

        public Company()
        {
                
        }

        public Company(string name, string postalCode, 
            long phoneNumber, string email, string logo)
        {
            Name = name;
            PostalCode = postalCode;
            PhoneNumber = phoneNumber;
            Email = email;
            Logo = logo;
        }
    }
}
