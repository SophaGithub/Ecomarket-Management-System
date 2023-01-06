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
    public partial class frmCustomer : Form
    {
        private List<ClsCustomer> Customers = new List<ClsCustomer>();
        private IOManager iOManager = new IOManager();
        private string fileName = "customer";
        public frmCustomer()
        {
            InitializeComponent();
        }
        private void frmCustomer_Load(object sender, EventArgs e)
        {
            LoadCustomer();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(txtAddress.Text))
                {
                    MessageBox.Show("please input all field");
                    return;
                }
                int id = int.Parse(txtId.Text);
                string name = txtName.Text;
                string phone = txtPhone.Text;
                string address = txtAddress.Text;
                ClsCustomer customer = new ClsCustomer(id, name, phone, address);
                ClsCustomer temp = Customers.Where(c => c.Name == customer.Name).FirstOrDefault();
                if (temp == null)
                {
                    Customers.Add(customer);
                    iOManager.Save(Customers, fileName);
                    MessageBox.Show("added");
                    Clear();
                }
                else
                {
                    MessageBox.Show("customer name already exist");
                }
            }catch(FormatException)
            {
                MessageBox.Show("invalid");
            }
            finally { Clear(); }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int customerId = int.Parse(txtId.Text);
                ClsCustomer customerUpdate = Customers.Where(c => c.Id == customerId).FirstOrDefault();
                if (customerUpdate != null)
                {
                    if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(txtAddress.Text))
                    {
                        MessageBox.Show("please input all field");
                        return;
                    }
                    customerUpdate.Name = txtName.Text;
                    customerUpdate.Phone = txtPhone.Text;
                    customerUpdate.Address = txtAddress.Text;
                    iOManager.Save(Customers, fileName);
                    Clear();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("invalid");
            }
            finally { Clear(); }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int customerId = int.Parse(txtId.Text);
            ClsCustomer customerRemove = Customers.Where(c => c.Id == customerId).FirstOrDefault();
            if (customerRemove != null)
            {
                Customers.Remove(customerRemove);
                iOManager.Save(Customers, fileName);
                MessageBox.Show("removed");
                Clear();
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            frmCustomerView customerView = new frmCustomerView();
            customerView.frmCustomer = this;
            customerView.Show();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        //method

        private void LoadCustomer()
        {
            Customers = iOManager.Load<List<ClsCustomer>>(fileName);
            if (Customers == null)
            {
                Customers = new List<ClsCustomer>();
            }
            Clear();
        }
        //getId method
        public int getId()
        {
            ClsCustomer customer = Customers.OrderByDescending(c => c.Id).FirstOrDefault();
            if (customer != null)
            {
                return (customer.Id + 1);
            }
            return 1;
        }

        //refresh customer by id method
        public void RefreshCustomerById(int customerId)
        {
            ClsCustomer temp = Customers.Where(c => c.Id == customerId).FirstOrDefault();
            if (temp != null)
            {
                txtId.Text = temp.Id.ToString();
                txtName.Text = temp.Name;
                txtPhone.Text = temp.Phone;
                txtAddress.Text = temp.Address;
            }
        }
        //clear method
        private void Clear()
        {
            txtId.Text = getId().ToString();
            txtName.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
        }
    }
}
