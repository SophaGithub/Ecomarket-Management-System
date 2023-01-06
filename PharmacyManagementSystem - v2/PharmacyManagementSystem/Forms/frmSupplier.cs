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
    public partial class frmSupplier : Form
    {
        private List<ClsSupplier> Suppliers = new List<ClsSupplier>();
        private IOManager iOManager = new IOManager();
        private string fileName = "supplier";
        public frmSupplier()
        {
            InitializeComponent();
        }
        private void frmSupplier_Load(object sender, EventArgs e)
        {
            LoadSupplier();
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
                ClsSupplier supplier = new ClsSupplier(id, name, phone, address);
                ClsSupplier temp = Suppliers.Where(c => c.Name == supplier.Name).FirstOrDefault();
                if (temp == null)
                {
                    Suppliers.Add(supplier);
                    iOManager.Save(Suppliers, fileName);
                    MessageBox.Show("added");
                    Clear();
                }
                else
                {
                    MessageBox.Show("supplier name already exist");
                }
            }catch(FormatException)
            {
                MessageBox.Show("invalid");
            }
            finally { Clear(); }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int supplierId = int.Parse(txtId.Text);
            ClsSupplier temp = Suppliers.Where(c => c.Id == supplierId).FirstOrDefault();
            if (temp != null)
            {
                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(txtAddress.Text))
                {
                    MessageBox.Show("please input all field");
                    return;
                }
                temp.Name = txtName.Text;
                temp.Phone = txtPhone.Text;
                temp.Address = txtAddress.Text;
                iOManager.Save(Suppliers, fileName);
                Clear();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int supplierId = int.Parse(txtId.Text);
            ClsSupplier temp = Suppliers.Where(c => c.Id == supplierId).FirstOrDefault();
            if (temp != null)
            {
                Suppliers.Remove(temp);
                iOManager.Save(Suppliers, fileName);
                MessageBox.Show("removed");
                Clear();
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            frmSupplierView view = new frmSupplierView();
            view.frmSupplier = this;
            view.Show();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        //method

        private void LoadSupplier()
        {
            Suppliers = iOManager.Load<List<ClsSupplier>>(fileName);
            if (Suppliers == null)
            {
                Suppliers = new List<ClsSupplier>();
            }
            Clear();
        }
        public int getId()
        {
            ClsSupplier temp = Suppliers.OrderByDescending(c => c.Id).FirstOrDefault();
            if (temp != null)
            {
                return (temp.Id + 1);
            }
            return 1;
        }

        //refresh supplier by id method
        public void RefreshSupplierById(int supplierId)
        {
            ClsSupplier temp = Suppliers.Where(c => c.Id == supplierId).FirstOrDefault();
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
