using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant.UC
{
    public partial class ManagePegawaiUC : UserControl
    {
        Engine engine = new Engine();
        String kode_user;
        public ManagePegawaiUC()
        {
            InitializeComponent();
        }

        public void ManagePegawaiUC_Load(object sender, EventArgs e)
        {
            DataSet data = engine.GetData("select * from users");
            guna2DataGridView1.DataSource = data.Tables[0];
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if(txtNamaPegawai.Text != "" && txtAlamatPegawai.Text != "" && txtTelfonPegawai.Text != "" && txtUsername.Text != "" && txtPassword.Text != "" && txtJabatanPegawai.Text != "")
            {
                if (MessageBox.Show("Simpan pegawai baru?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    kode_user = engine.GenerateCode("kode_user", "users", "USR", 4);
                    engine.SetData("insert into users(kode_user, nama_user, alamat_user, telfon_user, username, pass, level_user) values('" + kode_user + "', '" + txtNamaPegawai.Text + "', '" + txtAlamatPegawai.Text + "', '" + txtTelfonPegawai.Text + "', '" + txtUsername.Text + "', '" + txtPassword.Text + "', '"+txtJabatanPegawai.Text+"')", "Berhasil menambahkan pegawai baru.");
                    ManagePegawaiUC_Load(this, null);
                    engine.LogActivity("menambahkan pegawai");
                    Clear();
                }
            } else
            {
                engine.FillAllFields();
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (txtNamaPegawai.Text != "" && txtAlamatPegawai.Text != "" && txtTelfonPegawai.Text != "" && txtUsername.Text != "" && txtPassword.Text != "" && txtJabatanPegawai.Text != "")
            {
                if (MessageBox.Show("Simpan perubahan pegawai?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    engine.SetData("update users set kode_user='" + kode_user + "', nama_user='" + txtNamaPegawai.Text + "', alamat_user='" + txtAlamatPegawai.Text + "', telfon_user='" + txtTelfonPegawai.Text + "', username='" + txtUsername.Text + "', pass='" + txtPassword.Text + "', level_user='" + txtJabatanPegawai.Text + "' where kode_user='"+kode_user+"'", "Berhasil mengedit pegawai.");
                    ManagePegawaiUC_Load(this, null);
                    engine.LogActivity("mengedit pegawai");
                    Clear();
                }
            }
            else
            {
                engine.SelectDataToEdit();
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (txtNamaPegawai.Text != "" && txtAlamatPegawai.Text != "" && txtTelfonPegawai.Text != "" && txtUsername.Text != "" && txtPassword.Text != "" && txtJabatanPegawai.Text != "")
            {
                if (MessageBox.Show("Simpan perubahan pegawai?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    engine.SetData("delete from users where kode_user='" + kode_user + "'", "Berhasil menghapus pegawai.");
                    ManagePegawaiUC_Load(this, null);
                    engine.LogActivity("menghapus pegawai");
                    Clear();
                }
            }
            else
            {
                engine.SelectDataToDelete();
            }
        }

        private void Clear()
        {
            txtNamaPegawai.Clear();
            txtAlamatPegawai.Clear();
            txtTelfonPegawai.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtJabatanPegawai.Text = "";
            txtJabatanPegawai.SelectedIndex = -1;
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void txtTelfonPegawai_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtTelfonPegawai.Text, "[^0-9]"))
            {
                MessageBox.Show("Data telfon harus diisi dengan nomor.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelfonPegawai.Clear();
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                kode_user = guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtNamaPegawai.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtAlamatPegawai.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtTelfonPegawai.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtUsername.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtPassword.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtJabatanPegawai.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DataSet data = engine.GetData("select * from users where kode_user like '%"+txtSearch.Text+"%' or nama_user like '%"+txtSearch.Text+"%' or alamat_user like '%"+txtSearch.Text+"%'");
            guna2DataGridView1.DataSource = data.Tables[0];
        }
    }
}
