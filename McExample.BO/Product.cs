using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McExample.BO
{
    [Serializable]
    public class Product
    {
        public string Reference { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public float Tax { get; set; }
        public byte[] Picture { get; set; }
        public string Logo { get; set; }

        public Product()//pour les sérialisations
        {

        }

        public Product(string reference, string name, double unitPrice,
            float tax, byte[] picture, string logo)
        {
            Reference = reference;
            Name = name;
            UnitPrice = unitPrice;
            Tax = tax;
            Picture = picture;
            Logo = logo;
        }

        public override bool Equals(object obj)
        {
            return obj is Product product &&
                   Reference.Equals(product.Reference, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return -1304721846 + EqualityComparer<string>.Default.GetHashCode(Reference);
        }
    }
}
