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
    public partial class frmEmployee : Form
    {
        private List<ClsEmployee> Employees = new List<ClsEmployee>();
        private IOManager iOManager = new IOManager();
        private string fileName = "employee";
        public frmEmployee()
        {
            InitializeComponent();
        }
        private void frmEmployee_Load(object sender, EventArgs e)
        {
            LoadEmployee();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtAge.Text) || string.IsNullOrEmpty(txtSalary.Text) || string.IsNullOrEmpty(txtAddress.Text) || string.IsNullOrEmpty(txtUsername.Text) ||string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("please input all field");
                return;
            }

            int id = int.Parse(txtId.Text);
            string name = txtName.Text;
            int age = int.Parse(txtAge.Text);
            double salary = double.Parse(txtSalary.Text);
            string phone = txtPhone.Text;
            string address = txtAddress.Text;
            string username = txtUsername.Text;
            int password = int.Parse(txtPassword.Text);

            ClsEmployee employee = new ClsEmployee(id,name,age,phone,salary,address,username,password);
            ClsEmployee temp = Employees.Where( t => t.Name == name).FirstOrDefault();
            
            if( temp == null )
            {
                Employees.Add(employee);
                iOManager.Save(Employees, fileName);
                MessageBox.Show("added");
                Clear();
            }
            else
            {
                MessageBox.Show("employee name already exist");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int employeeId = int.Parse(txtId.Text);
            ClsEmployee employeeUpdate = Employees.Where(c => c.Id == employeeId).FirstOrDefault();
            if (employeeUpdate != null)
            {
                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtAge.Text) || string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(txtSalary.Text) || string.IsNullOrEmpty(txtAddress.Text))
                {
                    MessageBox.Show("please input all field");
                    return;
                }
                employeeUpdate.Name = txtName.Text;
                employeeUpdate.Age = int.Parse(txtAge.Text);
                employeeUpdate.Phone = txtPhone.Text;
                employeeUpdate.Salary = double.Parse(txtSalary.Text);
                employeeUpdate.Address = txtAddress.Text;
                iOManager.Save(Employees, fileName);
                Clear();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int employeeId = int.Parse(txtId.Text);
            ClsEmployee employeeRemove = Employees.Where(c => c.Id == employeeId).FirstOrDefault();
            if (employeeRemove != null)
            {
                Employees.Remove(employeeRemove);
                iOManager.Save(Employees, fileName);
                MessageBox.Show("removed");
                Clear();
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            frmEmployeeView view = new frmEmployeeView();
            view.frmEmployee= this;
            view.ShowDialog();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        //method

        private void LoadEmployee()
        {
            Employees = iOManager.Load<List<ClsEmployee>>(fileName);
            if (Employees == null)
            {
                Employees = new List<ClsEmployee>();
            }
            Clear();
        }
        //getId method
        public int getId()
        {
            ClsEmployee employee = Employees.OrderByDescending(c => c.Id).FirstOrDefault();
            if (employee != null)
            {
                return (employee.Id + 1);
            }
            return 1;
        }

        //refresh employee by id method
        public void RefreshEmployeeById(int employeeId)
        {
            ClsEmployee temp = Employees.Where(c => c.Id == employeeId).FirstOrDefault();
            if (temp != null)
            {
                txtId.Text = temp.Id.ToString();
                txtName.Text = temp.Name;
                txtAge.Text = temp.Age.ToString();
                txtPhone.Text = temp.Phone;
                txtSalary.Text = temp.Salary.ToString();
                txtAddress.Text = temp.Address;
            }
        }
        //clear method
        private void Clear()
        {
            txtId.Text = getId().ToString();
            txtName.Text = "";
            txtAge.Text = "";
            txtPhone.Text = "";
            txtSalary.Text = "";
            txtAddress.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
        }

        public string GetEmployeeUser(string username)
        {
            ClsEmployee temp = Employees.Where(p => p.Username == username).FirstOrDefault();
            if (temp != null)
                return temp.Username;
            return "";
        }
        public int GetEmployeePassword(int password)
        {
            ClsEmployee temp = Employees.Where(p => p.Password == password).FirstOrDefault();
            if (temp != null)
                return temp.Password;
            return -1;
        }
    }
}
