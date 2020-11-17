using McExample.BO;
using McExample.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McExample.BLL
{
    public class CompanyBLO
    {
        private CompanyDAO companyRepo;
        private string dbFolder;
        public CompanyBLO(string dbFolder)
        {
            this.dbFolder = dbFolder;
            companyRepo = new CompanyDAO(dbFolder);
        }
        public  void CreateCompany(Company oldCompany, Company newCompany)
        {
            string filename = null;
            if (!string.IsNullOrEmpty(newCompany.Logo))
            {
                string ext = Path.GetExtension(newCompany.Logo);
                filename = Guid.NewGuid().ToString() + ext;
                FileInfo fileSource = new FileInfo(newCompany.Logo);
                string filePath = Path.Combine(dbFolder, "logo", filename);
                FileInfo fileDest = new FileInfo(filePath);
                if (!fileDest.Directory.Exists)
                    fileDest.Directory.Create();
                fileSource.CopyTo(fileDest.FullName);
            }
            newCompany.Logo = filename;
            companyRepo.Add(newCompany);

            if (!string.IsNullOrEmpty(oldCompany.Logo))
                File.Delete(oldCompany.Logo);
        }

        public Company GetCompany()
        {
            Company company =  companyRepo.Get();
            if (company != null)
                if (!string.IsNullOrEmpty(company.Logo))
                    company.Logo = Path.Combine(dbFolder, "logo", company.Logo);
            return company;
        }
    }
}
