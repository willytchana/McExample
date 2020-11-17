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
        public  void CreateCompany(Company company, string logoFileName)
        {
            string filename = !string.IsNullOrEmpty(company.Logo) ? Path.GetFileName(company.Logo): null;
            if (!string.IsNullOrEmpty(logoFileName))
            {
                string ext = Path.GetExtension(logoFileName);
                filename = Guid.NewGuid().ToString() + ext;
                FileInfo fileSource = new FileInfo(logoFileName);
                string filePath = Path.Combine(dbFolder, "logo", filename);
                FileInfo fileDest = new FileInfo(filePath);
                if (!fileDest.Directory.Exists)
                    fileDest.Directory.Create();
                fileSource.CopyTo(fileDest.FullName);
            }
            company.Logo = filename;
            companyRepo.Add(company);
        }

        public Company GetCompany()
        {
            Company company =  companyRepo.Get();
            if (!string.IsNullOrEmpty(company.Logo))
                company.Logo = Path.Combine(dbFolder, "logo", company.Logo);
            return company;
        }
    }
}
