using McExample.BO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McExample.DAL
{
    public class ProductDAO
    {
        private static List<Product> products;
        private const string FILE_NAME = @"products.json";
        private readonly string dbFolder;
        private FileInfo file;
        public ProductDAO(string dbFolder)
        {
            this.dbFolder = dbFolder;
            file = new FileInfo(Path.Combine(this.dbFolder, FILE_NAME));
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }
            if(!file.Exists)
            {
                file.Create().Close();
                file.Refresh();
            }
            if(file.Length > 0)
            {
                using(StreamReader sr = new StreamReader(file.FullName))
                {
                    string json = sr.ReadToEnd();
                    products = JsonConvert.DeserializeObject<List<Product>>(json);
                }
            }
            if(products == null)
            {
                products = new List<Product>();
            }
        }

        public void Set(Product oldProduct, Product newProduct)
        {
            var oldIndex = products.IndexOf(oldProduct);
            var newIndex = products.IndexOf(newProduct);
            if (oldIndex < 0)
                throw new KeyNotFoundException("The product doesn't exists !");
            if(newIndex >= 0  && oldIndex != newIndex)
                throw new DuplicateNameException("This product reference already exists !");
            products[oldIndex] = newProduct;
            Save();
        }

        public void Add(Product product)
        {
            var index =  products.IndexOf(product);
            if (index >= 0)
                throw new DuplicateNameException("This product reference already exists !");
            products.Add(product);
            Save();
        }

        private void Save()
        {
            using (StreamWriter sw = new StreamWriter(file.FullName, false))
            {
                string json = JsonConvert.SerializeObject(products);
                sw.WriteLine(json);
            }
        }

        public void Remove(Product product)
        {
            products.Remove(product);//base sur Product.Equals redefini
            Save();
        }

        public IEnumerable<Product> Find()
        {
            return new List<Product>(products);
        }

        public IEnumerable<Product> Find(Func<Product, bool> predicate)
        {
            return new List<Product>(products.Where(predicate).ToArray());
        }
    }
}
