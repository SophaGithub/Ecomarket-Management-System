using PharmacyManagementSystem.Theme;
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
    public partial class frmMenu : Form
    {
        private Button currentButton;
        private Form activateForm;
        private ClsThemeColor themeColor = new ClsThemeColor();
        public frmMenu()
        {
            InitializeComponent();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.frmCategory(), sender);
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.frmProduct(), sender);
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.frmCustomer(), sender);
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.frmSupplier(), sender);
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.frmEmployee(), sender);
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.frmStock(), sender);
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.frmPurchase(), sender);
        }


        //method
        private Color SelectThemeColor()
        {
            string color = "#3F51B5";
            return ColorTranslator.FromHtml(color);
        }
        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = SelectThemeColor();
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    panelTitleBar.BackColor = color;
                    panelLogo.BackColor = themeColor.ChangeColorBrightness(color, -0.3);
                    themeColor.PrimaryColor = color;
                    themeColor.SecondaryColor = themeColor.ChangeColorBrightness(color, -0.3);
                }
            }
        }
        private void DisableButton()
        {
            foreach (Control previousBtn in panelMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(51, 51, 76);
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }
        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activateForm != null)
            {
                activateForm.Close();
            }
            ActivateButton(btnSender);
            activateForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.pnLeaderboard.Controls.Add(childForm);
            this.pnLeaderboard.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitleBar.Text = childForm.Text;
        }
    }
}
