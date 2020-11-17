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
    public class CompanyDAO
    {
        private Company company;
        private const string FILE_NAME = @"company.json";
        private readonly string dbFolder;
        private FileInfo file;
        public CompanyDAO(string dbFolder)
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
                    company = JsonConvert.DeserializeObject<Company>(json);
                }
            }
        }

        public void Add(Company company)
        {
            using (StreamWriter sw = new StreamWriter(file.FullName, false))
            {
                string json = JsonConvert.SerializeObject(company);
                sw.WriteLine(json);
            }
        }

        public Company Get()
        {
            return company;
        }

    }
}
