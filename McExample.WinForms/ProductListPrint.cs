using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McExample.WinForms
{
    public class ProductListPrint
    {
        public string Reference { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public byte[] Picture { get; set; }
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyPhoneNumber { get; set; }
        public string CompanyPostalCode { get; set; }
        public byte[] CompanyLogo { get; set; }

        public ProductListPrint(string reference, string name, double price, byte[] picture, 
            string companyName, string companyEmail, 
            string companyPhoneNumber, string companyPostalCode, byte[] companyLogo)
        {
            Reference = reference;
            Name = name;
            Price = price;
            Picture = picture;
            CompanyName = companyName;
            CompanyEmail = companyEmail;
            CompanyPhoneNumber = companyPhoneNumber;
            CompanyPostalCode = companyPostalCode;
            CompanyLogo = companyLogo;
        }
    }
}
