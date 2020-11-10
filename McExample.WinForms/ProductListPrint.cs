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
        public byte[] Logo { get; set; }

        public ProductListPrint(string reference, string name, 
            double price, byte[] picture, byte[] logo)
        {
            Reference = reference;
            Name = name;
            Price = price;
            Picture = picture;
            Logo = logo;
        }
    }
}
