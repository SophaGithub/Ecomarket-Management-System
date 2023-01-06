using PharmacyManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyManagementSystem.Forms
{

    public partial class frmProductView : Form
    {
        private List<ClsCategory> Categories = new List<ClsCategory>();
        private List<ClsProduct> Products = new List<ClsProduct>();
        private IOManager iOManager = new IOManager();
        private string fileName = "category";
        private string fileProductName = "product";
        public frmProduct frmProduct;
        public frmProductView()
        {
            InitializeComponent();
        }

        private void frmProductView_Load(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void dgProductView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgProductView.SelectedRows.Count > 0)
            {
                int productId = (int)dgProductView.SelectedRows[0].Cells[0].Value;
                frmProduct.RefreshProductById(productId);
                this.Close();
            }
        }

        //method

        private void LoadProduct()
        {
            Categories = iOManager.Load<List<ClsCategory>>(fileName);
            if (Categories == null)
            {
                Categories = new List<ClsCategory>();
            }
            Products = iOManager.Load<List<ClsProduct>>(fileProductName);
            if (Products != null)
            {
                //dgProductView.DataSource = Products;
                foreach (ClsProduct p in Products)
                {
                    string categoryName = "No name";
                    ClsCategory cate = Categories.Where(c => c.Id == p.Id).FirstOrDefault();
                    if (cate != null)
                    {
                        categoryName = cate.Name;
                    }
                    dgProductView.Rows.Add(p.Id, p.Name, categoryName, p.CostPrice, p.SellingPrice, p.Unit);
                }
            }
            else
            {
                Products = new List<ClsProduct>();
            }
        }
    }
}
