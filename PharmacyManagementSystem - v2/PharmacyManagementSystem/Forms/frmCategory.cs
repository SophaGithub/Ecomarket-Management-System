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
    public partial class frmCategory : Form
    {
        private List<ClsCategory> Categories = new List<ClsCategory>();
        private IOManager iOManager = new IOManager();
        private string fileName = "category";
        public frmCategory()
        {
            InitializeComponent();
        }
        private void frmCategory_Load(object sender, EventArgs e)
        {
            LoadCategory();
        }
        
        //add button
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    MessageBox.Show("please input category name");
                    return;
                }
                int id = int.Parse(txtId.Text);
                string name = txtName.Text;
                ClsCategory category = new ClsCategory(id, name);
                ClsCategory temp = Categories.Where(c => c.Name == category.Name).FirstOrDefault();
                if (temp == null)
                {
                    Categories.Add(category);
                    iOManager.Save(Categories, fileName);
                    MessageBox.Show("added");
                    Clear();
                }
                else
                {
                    MessageBox.Show("category name already exist");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("invalid");
            }
            finally
            {
                Clear();
            }
        }

        //update button
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int categoryId = int.Parse(txtId.Text);
            ClsCategory categoryUpdate = Categories.Where(c => c.Id == categoryId).FirstOrDefault();
            if (categoryUpdate != null)
            {
                categoryUpdate.Name = txtName.Text;
                iOManager.Save(Categories, fileName);
                MessageBox.Show("updated");
                Clear();
            }
        }

        //remove button
        private void btnRemove_Click(object sender, EventArgs e)
        {
            int categoryId = int.Parse(txtId.Text);
            ClsCategory categoryRemove = Categories.Where(c => c.Id == categoryId).FirstOrDefault();
            if (categoryRemove != null)
            {
                Categories.Remove(categoryRemove);
                iOManager.Save(Categories, fileName);
                MessageBox.Show("removed");
                Clear();
            }
        }

        //view button
        private void btnView_Click(object sender, EventArgs e)
        {
            frmCategoryView view = new frmCategoryView();
            view.frmCategory = this;
            view.Show();
        }

        //clear button
        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        //method 

        //load category 
        private void LoadCategory()
        {
            Categories = iOManager.Load<List<ClsCategory>>(fileName);
            if (Categories == null)
            {
                Categories = new List<ClsCategory>();
            }
            Clear();
        }

        //get id
        private int getId()
        {
            ClsCategory category = Categories.OrderByDescending(c => c.Id).FirstOrDefault();
            if (category != null)
            {
                return (category.Id + 1);
            }
            return 1;
        }

        //refresh
        public void RefreshCategoryById(int categoryId)
        {
            ClsCategory temp = Categories.Where(c => c.Id == categoryId).FirstOrDefault();
            if (temp != null)
            {
                txtId.Text = temp.Id.ToString();
                txtName.Text = temp.Name;
            }
        }

        //clear
        private void Clear()
        {
            txtId.Text = getId().ToString();
            txtName.Text = "";
        }
    }
}
