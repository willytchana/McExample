using McExample.BLL;
using McExample.BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace McExample.WinForms
{
    public partial class FrmCompanyEdit : Form
    {
        private CompanyBLO companyBLO;
        private Company oldCompany;
        public FrmCompanyEdit()
        {
            InitializeComponent();
            companyBLO = new CompanyBLO(ConfigurationManager.AppSettings["DbFolder"]);
            oldCompany = companyBLO.GetCompany();
            if(oldCompany != null)
            {
                txtEmail.Text = oldCompany.Email;
                txtName.Text = oldCompany.Name;
                txtPhone.Text = oldCompany.PhoneNumber.ToString();
                txtPostalCode.Text = oldCompany.PostalCode;
                pictureBox1.ImageLocation = oldCompany.Logo;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                checkForm();

                Company newCompany = new Company
                (
                    txtName.Text.ToUpper(),
                    txtPostalCode.Text,
                    long.Parse(txtPhone.Text),
                    txtEmail.Text,
                    pictureBox1.ImageLocation
                );

                companyBLO.CreateCompany(oldCompany, newCompany);

                MessageBox.Show
                (
                    "Save done !",
                    "Confirmation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                Close();

            }
            catch (TypingException ex)
            {
                MessageBox.Show
               (
                   ex.Message,
                   "Typing error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning
               );
            }
            catch (Exception ex)
            {
                ex.WriteToFile();
                MessageBox.Show
               (
                   "An error occurred! Please try again later.",
                   "Erreur",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error
               );
            }
        }

        private void checkForm()
        {
            string text = string.Empty;
            txtName.BackColor = Color.White;
            txtEmail.BackColor = Color.White;
            if (!long.TryParse(txtPhone.Text, out _))
            {
                text += "- Please enter a good phone number ! \n";
                txtName.BackColor = Color.Pink;
            }
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                text += "- Please enter the name ! \n";
                txtEmail.BackColor = Color.Pink;
            }

            if (!string.IsNullOrEmpty(text))
                throw new TypingException(text);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Choose a picture";
            ofd.Filter = "Image files|*.jpg;*.jpeg;*.png;*.gif";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = ofd.FileName;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pictureBox1.ImageLocation = null;
        }
    }
}
