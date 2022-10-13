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
    public partial class FormKasir : Form
    {
        Engine engine = new Engine();
        public FormKasir()
        {
            InitializeComponent();
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            kasirPanel.Visible = true;
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Anda akan keluar. Lanjutkan?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                FormLogin fl = new FormLogin();
                this.Hide();
                fl.Show();
                engine.LogActivity("logout");
            }
        }

        private void FormKasir_Load(object sender, EventArgs e)
        {
            kasirPanel.Visible = false;
            pesanUC1.Visible = true;
            pesanUC1.BringToFront();
            pesanUC1.PesanUC_Load(this, null);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            pesanUC1.Visible = true;
            pesanUC1.BringToFront();
            pesanUC1.PesanUC_Load(this, null);
            kasirPanel.Visible = false;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            bayarUC1.Visible = true;
            bayarUC1.BringToFront();
            bayarUC1.BayarUC_Load(this, null);
            kasirPanel.Visible = false;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            pemasukanUC1.Visible = true;
            pemasukanUC1.BringToFront();
            pemasukanUC1.PemasukanUC_Load(this, null);
            kasirPanel.Visible = false;
        }
    }
}
