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
    public partial class frmEmployeeView : Form
    {
        private List<ClsEmployee> Employees = new List<ClsEmployee>();
        private IOManager iOManager = new IOManager();
        private string fileName = "employee";
        public frmEmployee frmEmployee;
        public frmEmployeeView()
        {
            InitializeComponent();
        }

        private void frmEmployeeView_Load(object sender, EventArgs e)
        {
            LoadEmployee();
        }

        private void dgEmployeeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgEmployeeView.SelectedRows.Count > 0)
            {
                int employeeId = (int)dgEmployeeView.SelectedRows[0].Cells[0].Value;
                frmEmployee.RefreshEmployeeById(employeeId);
                this.Close();
            }
        }

        private void LoadEmployee()
        {
            Employees = iOManager.Load<List<ClsEmployee>>(fileName);
            if (Employees != null)
            {
                foreach (ClsEmployee e in Employees)
                {
                    dgEmployeeView.Rows.Add(e.Id, e.Name, e.Age, e.Phone, e.Salary, e.Address);
                }
            }
            else
            {
                Employees = new List<ClsEmployee>();
            }
        }
    }
}
