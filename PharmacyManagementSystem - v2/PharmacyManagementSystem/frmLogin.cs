using PharmacyManagementSystem.Forms;
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

namespace PharmacyManagementSystem
{
    public partial class frmLogin : Form
    {
        private List<ClsEmployee> Employees = new List<ClsEmployee>();
        private PurchaseUtil PurchaseUtil = new PurchaseUtil();
        private string fileName = "employee";
        public frmLogin()
        {
            InitializeComponent();
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            Employees = PurchaseUtil.LoadEmployee(fileName);
        }
        //close button
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("please input all field");
                return;
            }

            string username = txtUsername.Text;
            int password = int.Parse(txtPassword.Text);

            ClsEmployee temp = Employees.Where(c => c.Username== username && c.Password == password).FirstOrDefault();
            if(temp != null)
            {
                frmMenu menu = new frmMenu();
                menu.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("invalid user!");
            }
        }

        private void Login()
        {
            
        }

        
    }
}
