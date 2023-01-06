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
    public partial class frmPurchaseView : Form
    {
        //load sale summaries
        private string saleSummariesFileName = "sale";
        private List<ClsSaleSummaries> SaleSummaries = new List<ClsSaleSummaries>();

        //customer
        private string customerFileName = "customer";
        private List<ClsCustomer> Customers = new List<ClsCustomer>();

        //employee
        private string employeeFileName = "employee";
        private List<ClsEmployee> Employees = new List<ClsEmployee>();

        public frmPurchase purchaseView;

        private PurchaseUtil purchaseUtil = new PurchaseUtil();
        public frmPurchaseView()
        {
            InitializeComponent();
        }

        private void frmPurchaseView_Load(object sender, EventArgs e)
        {
            SaleSummaries = purchaseUtil.LoadSaleSummaries(saleSummariesFileName);
            Customers = purchaseUtil.LoadCustomer(customerFileName);
            Employees = purchaseUtil.LoadEmployee(employeeFileName);

            foreach (ClsSaleSummaries s in SaleSummaries)
            {
                int invoiceId = s.InvoiceId;
                string customerName = GetCustomerName(s.CustomerId);
                string employeeName = GetEmployeeName(s.EmployeeId);
                double totalPrice = s.TotalPrice;

                dgvSaleHistory.Rows.Add(invoiceId, customerFileName, employeeFileName, totalPrice);
            }
        }
        private string GetCustomerName(int id)
        {
            ClsCustomer cus = Customers.Where(c => c.Id == id).FirstOrDefault();
            if (cus != null)
                return cus.Name;
            return "";
        }

        private string GetEmployeeName(int id)
        {
            ClsEmployee emp = Employees.Where(e => e.Id == id).FirstOrDefault();
            if (emp != null)
                return emp.Name;
            return "";
        }

        private void dgvSaleHistory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvSaleHistory.SelectedRows.Count > 0)
            {
                int invoiceId = (int)dgvSaleHistory.SelectedRows[0].Cells[0].Value;
                purchaseView.RefreshPosUI(invoiceId);
                this.Close();
            }
        }
    }
}
