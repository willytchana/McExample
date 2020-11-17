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
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace McExample.WinForms
{
    public partial class FrmProductList : Form
    {
        private ProductBLO productBLO;
        private CompanyBLO companyBLO;
        public FrmProductList()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            productBLO = new ProductBLO(ConfigurationManager.AppSettings["DbFolder"]);
            companyBLO = new CompanyBLO(ConfigurationManager.AppSettings["DbFolder"]);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void loadData()
        {
            string value = txtSearch.Text.ToLower();
            var products = productBLO.GetBy
            (
                x =>
                x.Reference.ToLower().Contains(value) ||
                x.Name.ToLower().Contains(value)
            ).OrderBy(x => x.Reference).ToArray();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = products;
            lblRowCount.Text = $"{dataGridView1.RowCount} rows";
            dataGridView1.ClearSelection();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form f = new FrmProductEdit(loadData);
            f.Show();
        }

        private void FrmProductList_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtSearch.Text))
                loadData();
            else
                txtSearch.Clear();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)
            {
                for(int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    Form f = new FrmProductEdit
                    (
                        dataGridView1.SelectedRows[i].DataBoundItem as Product,
                        loadData
                    );
                    f.ShowDialog();
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEdit_Click(sender, e);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (
                    MessageBox.Show
                    (
                        "Do you really want to delete this product(s)?",
                        "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                    ) == DialogResult.Yes
                )
                {
                    for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                    {
                        productBLO.DeleteProduct(dataGridView1.SelectedRows[i].DataBoundItem as Product);
                    }
                    loadData();
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            List<ProductListPrint> items = new List<ProductListPrint>();
            Company company = companyBLO.GetCompany();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                Product p = dataGridView1.Rows[i].DataBoundItem as Product;
                items.Add
                (
                   new ProductListPrint
                   (
                       p.Reference,
                       p.Name,
                       p.UnitPrice,
                       p.Picture,
                       company?.Name,
                       company?.Email,
                       company?.PhoneNumber.ToString(),
                       company?.PostalCode,
                       !string.IsNullOrEmpty(company?.Logo) ? File.ReadAllBytes(company?.Logo) : null
                    )
                );
            }
            Form f = new FrmPreview("ProductListRpt.rdlc", items);
            f.Show();
        }
    }
}
