using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace McExample.WinForms
{
    public partial class FrmPreview : Form
    {

        private object items { get; set; }
        private object reportPath { get; set; }
        public FrmPreview()
        {
            InitializeComponent();
        }

        public FrmPreview(string reportPath, object items):this()
        {
            this.reportPath = reportPath;
            this.items = items;
        }

        private void FrmPreview_Load(object sender, EventArgs e)
        {
            this.reportViewer1.LocalReport.ReportPath = "ProductListRpt.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add
            (
                new Microsoft.Reporting.WinForms.ReportDataSource
                (
                    "DataSet1",
                    items
                )
            );
            this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
            this.reportViewer1.ZoomPercent = 100;
            this.reportViewer1.RefreshReport();
        }
    }
}
