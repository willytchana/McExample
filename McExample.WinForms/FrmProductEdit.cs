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
    public partial class FrmProductEdit : Form
    {
        private Action callBack;
        private Product oldProduct;
        public FrmProductEdit()
        {
            InitializeComponent();
        }

        public FrmProductEdit(Action callBack):this()
        {
            this.callBack = callBack;            
        }

        public FrmProductEdit(Product product, Action callBack) : this(callBack)
        {
            this.oldProduct = product;
            txtName.Text = product.Name;
            txtPrice.Text = product.UnitPrice.ToString();
            txtReference.Text = product.Reference;
            txtTax.Text = product.Tax.ToString();
            if (product.Picture != null)
                pictureBox1.Image = Image.FromStream(new MemoryStream(product.Picture));
            if (!string.IsNullOrEmpty(product.Logo))
                pictureBox2.ImageLocation = Path.Combine(ConfigurationManager.AppSettings["DbFolder"], "logo", product.Logo);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                checkForm();
                string filename = null;
                if (!string.IsNullOrEmpty(pictureBox2.ImageLocation))
                {

                    string ext = Path.GetExtension(pictureBox2.ImageLocation);
                    filename = Guid.NewGuid().ToString() + ext;
                    FileInfo fileSource = new FileInfo(pictureBox2.ImageLocation);
                    string filePath = Path.Combine(ConfigurationManager.AppSettings["DbFolder"], "logo", filename);
                    FileInfo fileDest = new FileInfo(filePath);
                    if (!fileDest.Directory.Exists)
                        fileDest.Directory.Create();
                    fileSource.CopyTo(fileDest.FullName);
                }

                Product newProduct = new Product
                (
                    txtReference.Text.ToUpper(),
                    txtName.Text,
                    double.Parse(txtPrice.Text),
                    float.Parse(txtTax.Text),
                    !string.IsNullOrEmpty(pictureBox1.ImageLocation) ? File.ReadAllBytes(pictureBox1.ImageLocation) : this.oldProduct?.Picture,
                    filename
                );
                ProductBLO productBLO = new ProductBLO(ConfigurationManager.AppSettings["DbFolder"]);

                if (this.oldProduct == null)
                    productBLO.CreateProduct(newProduct);
                else
                    productBLO.EditProduct(oldProduct, newProduct);

                MessageBox.Show
                (
                    "Save done !",
                    "Confirmation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                if (callBack != null)
                    callBack();

                if (oldProduct != null)
                    Close();

                txtReference.Clear();
                txtName.Clear();
                txtPrice.Clear();
                txtTax.Clear();
                txtReference.Focus();

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
            catch (DuplicateNameException ex)
            {
                MessageBox.Show
               (
                   ex.Message,
                   "Duplicate error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning
               );
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show
               (
                   ex.Message,
                   "Not found error",
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
            txtReference.BackColor = Color.White;
            txtName.BackColor = Color.White;
            if (string.IsNullOrWhiteSpace(txtReference.Text))
            {
                text += "- Please enter the reference ! \n";
                txtReference.BackColor = Color.Pink;
            }
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                text += "- Please enter the name ! \n";
                txtName.BackColor = Color.Pink;
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Choose a picture";
            ofd.Filter = "Image files|*.jpg;*.jpeg;*.png;*.gif";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.ImageLocation = ofd.FileName;
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pictureBox2.ImageLocation = null;
        }
    }
}
