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
    public partial class frmProduct : Form
    {
        private List<ClsCategory> Categories = new List<ClsCategory>();
        private List<ClsProduct> Products = new List<ClsProduct>();
        private IOManager iOManager = new IOManager();
        private string categoryFile = "category";
        private string fileName = "product";
        public frmProduct()
        {
            InitializeComponent();
        }

        private void frmProduct_Load(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtCostPrice.Text) || string.IsNullOrEmpty(txtSellingPrice.Text) || string.IsNullOrEmpty(txtUnit.Text))
            {
                MessageBox.Show("please input all field");
                return;
            }
            int id = int.Parse(txtId.Text);
            string name = txtName.Text;
            int categoryId = -1;
            string categoryName = cmbCategory.Text;
            ClsCategory category = Categories.Where(c => c.Name == categoryName).FirstOrDefault();
            if (category != null)
            {
                categoryId = category.Id;
            }
            double costPrice = double.Parse(txtCostPrice.Text);
            double sellingPrice = double.Parse(txtSellingPrice.Text);
            string unit = txtUnit.Text;
            
            ClsProduct product = new ClsProduct(id, name, categoryId, costPrice, sellingPrice, unit);
            ClsProduct temp = Products.Where(p => p.Name == product.Name).FirstOrDefault();
            if (temp == null)
            {
                Products.Add(product);
                iOManager.Save(Products, fileName);
                Clear();
            }
            else
            {
                MessageBox.Show("product name already exist");
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int prodId = int.Parse(txtId.Text);
            ClsProduct prodUpdate = Products.Where(c => c.Id == prodId).FirstOrDefault();
            if (prodUpdate != null)
            {
                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtCostPrice.Text) || string.IsNullOrEmpty(txtSellingPrice.Text) || string.IsNullOrEmpty(txtUnit.Text))
                {
                    MessageBox.Show("please input all field");
                    return;
                }
                prodUpdate.Name = txtName.Text;
                prodUpdate.CategoryId = cmbCategory.SelectedIndex;
                prodUpdate.CostPrice = double.Parse(txtCostPrice.Text);
                prodUpdate.SellingPrice = double.Parse(txtSellingPrice.Text);
                prodUpdate.Unit = txtUnit.Text;
                iOManager.Save(Products, fileName);
                MessageBox.Show("updated");
                Clear();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int productId = int.Parse(txtId.Text);
            ClsProduct productRemove = Products.Where(c => c.Id == productId).FirstOrDefault();
            if (productRemove != null)
            {
                Products.Remove(productRemove);
                iOManager.Save(Products, fileName);
                MessageBox.Show("removed");
                Clear();
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            frmProductView productView = new frmProductView();
            productView.frmProduct = this;
            productView.Show();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        //method

        private void LoadProduct()
        {
            Categories = iOManager.Load<List<ClsCategory>>(categoryFile);
            if (Categories == null)
            {
                Categories = new List<ClsCategory>();
            }

            foreach (ClsCategory cat in Categories)
            {
                cmbCategory.Items.Add(cat.Name);
            }

            Products = iOManager.Load<List<ClsProduct>>(fileName);
            if (Products == null)
            {
                Products = new List<ClsProduct>();
            }
            Clear();
        }
        private int getId()
        {
            ClsProduct temp = Products.OrderByDescending(p => p.Id).FirstOrDefault();
            if (temp != null)
            {
                return (temp.Id + 1);
            }
            return 1;
        }
        public void RefreshProductById(int productId)
        {
            ClsProduct temp = Products.Where(p => p.Id == productId).FirstOrDefault();
            if (temp != null)
            {
                txtId.Text = temp.Id.ToString();
                txtName.Text = temp.Name;
                cmbCategory.Text = temp.CategoryId.ToString();
                txtCostPrice.Text = temp.CostPrice.ToString();
                txtSellingPrice.Text = temp.SellingPrice.ToString();
                txtUnit.Text = temp.Unit;
            }
        }
        private void Clear()
        {
            txtId.Text = getId().ToString();
            txtName.Text = "";
            cmbCategory.Text = "";
            txtCostPrice.Text = "0";
            txtSellingPrice.Text = "0";
            txtUnit.Text = "";
        }
    }
}
