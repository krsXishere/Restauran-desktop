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
    public partial class ManageMejaUC : UserControl
    {
        Engine engine = new Engine();
        String kode_meja;
        int dataNomorMejaAwal, dataNomorMejaBaru;
        public ManageMejaUC()
        {
            InitializeComponent();
        }

        public void ManageMejaUC_Load(object sender, EventArgs e)
        {
            DataSet data = engine.GetData("select * from meja");
            guna2DataGridView1.DataSource = data.Tables[0];
            txtNomorMeja.Enabled = false;
            DataTable dataNomorMejaDB = engine.GetOneData("select nomor_meja from meja order by nomor_meja desc");
            dataNomorMejaAwal = int.Parse(dataNomorMejaDB.Rows[0][0].ToString());
            dataNomorMejaBaru = dataNomorMejaAwal + 1;
            txtNomorMeja.Text = dataNomorMejaBaru.ToString();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if(txtNomorMeja.Text != "" && txtStatusMeja.Text != "")
            {
                if (MessageBox.Show("Simpan meja baru?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    kode_meja = engine.GenerateCode("kode_meja", "meja", "MJ", 3);
                    engine.SetData("insert into meja(kode_meja, nomor_meja, status_meja) values('"+kode_meja+"', '"+txtNomorMeja.Text+"', '"+txtStatusMeja.Text+"')", "Berhasil menyimpan meja baru.");
                    ManageMejaUC_Load(this, null);
                    engine.LogActivity("menambahkan meja");
                    Clear();
                }
            } else
            {
                engine.FillAllFields();
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                txtNomorMeja.Enabled = true;
                kode_meja = guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtNomorMeja.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtStatusMeja.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (txtNomorMeja.Text != "" && txtStatusMeja.Text != "")
            {
                if (MessageBox.Show("Simpan perubahan meja?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    engine.SetData("update meja set nomor_meja='" + txtNomorMeja.Text + "', status_meja='" + txtStatusMeja.Text + "' where kode_meja='"+kode_meja+"'", "Berhasil mengedit meja.");
                    ManageMejaUC_Load(this, null);
                    engine.LogActivity("mengedit meja");
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
            if (txtNomorMeja.Text != "" && txtStatusMeja.Text != "")
            {
                if (MessageBox.Show("Hapus meja?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    engine.SetData("delete from meja where kode_meja='" + kode_meja + "'", "Berhasil menghapus meja.");
                    ManageMejaUC_Load(this, null);
                    engine.LogActivity("menghapus meja");
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
            txtNomorMeja.Clear();
            txtStatusMeja.Text = "";
            txtStatusMeja.SelectedIndex = -1;
            ManageMejaUC_Load(this, null);
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DataSet data = engine.GetData("select * from meja where kode_meja like '%"+txtSearch.Text+"%' or nomor_meja like '%"+txtSearch.Text+"%' or status_meja like '%"+txtSearch.Text+"%'");
            guna2DataGridView1.DataSource = data.Tables[0];
        }

        private void txtNomorMeja_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtNomorMeja.Text, "[^0-9]"))
            {
                MessageBox.Show("Data nomor meja harus diisi dengan nomor.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNomorMeja.Clear();
            }
        }
    }
}
