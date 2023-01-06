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
    public partial class frmCustomerView : Form
    {
        private List<ClsCustomer> Customers = new List<ClsCustomer>();
        private IOManager iOManager = new IOManager();
        private string fileName = "customer";
        public frmCustomer frmCustomer;
        public frmCustomerView()
        {
            InitializeComponent();
        }

        private void frmCustomerView_Load(object sender, EventArgs e)
        {
            LoadCustomer();
        }

        private void LoadCustomer()
        {
            Customers = iOManager.Load<List<ClsCustomer>>(fileName);
            if (Customers != null)
            {
                foreach (ClsCustomer c in Customers)
                {
                    dgCustomerView.Rows.Add(c.Id, c.Name, c.Phone, c.Address);
                }
            }
            else
            {
                Customers = new List<ClsCustomer>();
            }
        }
        private void dgCustomerView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgCustomerView.SelectedRows.Count > 0)
            {
                int customerId = (int)dgCustomerView.SelectedRows[0].Cells[0].Value;
                frmCustomer.RefreshCustomerById(customerId);
                this.Close();
            }
        }


    }

        
}
