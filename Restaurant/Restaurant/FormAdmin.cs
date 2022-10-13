using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant
{
    public partial class FormAdmin : Form
    {
        Engine engine = new Engine();
        public FormAdmin()
        {
            InitializeComponent();
        }
        private void FormUser_Load(object sender, EventArgs e)
        {
            adminPanel.Visible = false;
            dashboardUC1.Visible = true;
            dashboardUC1.BringToFront();
            dashboardUC1.DashboardUC_Load(this, null);
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            adminPanel.Visible = true;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            dashboardUC1.Visible = true;
            dashboardUC1.BringToFront();
            dashboardUC1.DashboardUC_Load(this, null);
            adminPanel.Visible = false;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            manageMenuUC1.Visible = true;
            manageMenuUC1.BringToFront();
            manageMenuUC1.ManageMenuUC_Load(this, null);
            adminPanel.Visible = false;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            manageMejaUC1.Visible = true;
            manageMejaUC1.BringToFront();
            manageMejaUC1.ManageMejaUC_Load(this, null);
            adminPanel.Visible = false;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            managePegawaiUC1.Visible = true;
            managePegawaiUC1.BringToFront();
            managePegawaiUC1.ManagePegawaiUC_Load(this, null);
            adminPanel.Visible = false;
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            logAcitvityUC1.Visible = true;
            logAcitvityUC1.BringToFront();
            logAcitvityUC1.LogAcitvity_Load(this, null);
            adminPanel.Visible = false;
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Anda akan keluar. Lanjutkan?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                FormLogin fl = new FormLogin();
                this.Hide();
                fl.Show();
                engine.LogActivity("logout");
            }
        }
    }
}
