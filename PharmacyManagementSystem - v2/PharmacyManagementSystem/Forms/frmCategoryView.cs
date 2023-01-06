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
    public partial class frmCategoryView : Form
    {
        //field
        private List<ClsCategory> Categories = new List<ClsCategory>();
        private IOManager iOManager = new IOManager();
        private string fileName = "category";
        public frmCategory frmCategory;

        public frmCategoryView()
        {
            InitializeComponent();
        }

        private void frmCategoryView_Load(object sender, EventArgs e)
        {
            LoadCategory();
        }


        //method
        private void LoadCategory()
        {
            Categories = iOManager.Load<List<ClsCategory>>(fileName);
            if (Categories != null)
            {
                foreach (ClsCategory c in Categories)
                {
                    dgCategoryView.Rows.Add(c.Id, c.Name);
                }
            }
            else
            {
                Categories = new List<ClsCategory>();
            }
        }

        //event
        private void dgCategoryView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgCategoryView.SelectedRows.Count > 0)
            {
                int categoryId = (int)dgCategoryView.SelectedRows[0].Cells[0].Value;
                frmCategory.RefreshCategoryById(categoryId);
                this.Close();
            }
        }
    }
}
