using PharmacyManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyManagementSystem.Forms
{

    public partial class frmSupplierView : Form
    {
        private List<ClsSupplier> Suppliers = new List<ClsSupplier>();
        private IOManager iOManager = new IOManager();
        private string fileName = "supplier";
        public frmSupplier frmSupplier;
        public frmSupplierView()
        {
            InitializeComponent();
        }

        private void frmSupplierView_Load(object sender, EventArgs e)
        {
            LoadSupplier();
        }
        private void LoadSupplier()
        {
            Suppliers = iOManager.Load<List<ClsSupplier>>(fileName);
            if (Suppliers != null)
            {
                foreach (ClsSupplier s in Suppliers)
                {
                    dgSupplierView.Rows.Add(s.Id, s.Name, s.Phone, s.Address);
                }
            }
            else
            {
                Suppliers = new List<ClsSupplier>();
            }
        }

        private void dgSupplierView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgSupplierView.SelectedRows.Count > 0)
            {
                int supplierId = (int)dgSupplierView.SelectedRows[0].Cells[0].Value;
                frmSupplier.RefreshSupplierById(supplierId);
                this.Close();
            }
        }
    }
}
