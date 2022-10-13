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
    public partial class PesanUC : UserControl
    {
        Engine engine = new Engine();
        String kode_menu, kode_meja, kode_transaksi;
        DataTable dataTable = new DataTable();
        DataRow dataRow;
        public PesanUC()
        {
            InitializeComponent();
            dataTable.Columns.Add("kode menu");
            dataTable.Columns.Add("Nama Menu");
            dataTable.Columns.Add("Nomor Meja");
            dataTable.Columns.Add("Jumlah Menu");
            dataTable.Columns.Add("Total Harga");
        }

        public void PesanUC_Load(object sender, EventArgs e)
        {
            DataSet data = engine.GetData("select * from menu");
            guna2DataGridView1.DataSource = data.Tables[0];
            DataSet data2 = engine.GetData("select * from meja where status_meja = 'Kosong'");
            guna2DataGridView2.DataSource = data2.Tables[0];
        }

        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {
            DataSet data = engine.GetData("select * from menu where kode_menu like '%" + txtSearch.Text + "%' or nama_menu like '%" + txtSearch.Text + "%' or harga_menu like '%" + txtSearch.Text + "%' or kategori_menu like '%" + txtSearch.Text + "%'");
            guna2DataGridView1.DataSource = data.Tables[0];
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if(txtNamaMenu.Text != "" && txtNomorMeja.Text != "" && txtJumlahMenu.Text != "")
            {
                if(MessageBox.Show("Masukkan ke dalam keranjang?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    dataRow = dataTable.NewRow();
                    dataRow["kode menu"] = kode_menu;
                    dataRow["Nama Menu"] = txtNamaMenu.Text;
                    dataRow["Nomor Meja"] = txtNomorMeja.Text;
                    dataRow["Jumlah Menu"] = txtJumlahMenu.Text;
                    dataRow["Total Harga"] = int.Parse(txtHargaMenu.Text) * int.Parse(txtJumlahMenu.Text);
                    dataTable.Rows.Add(dataRow);
                    guna2DataGridView3.DataSource = dataTable;
                    guna2DataGridView3.Columns[0].Visible = false;
                    guna2DataGridView3.Columns[1].Visible = false;

                    int sum = 0;
                    for (int i = 0; i < guna2DataGridView3.Rows.Count; ++i)
                    {
                        sum += Convert.ToInt32(guna2DataGridView3.Rows[i].Cells[4].Value);
                    }
                    txtTotalBayar.Text = sum.ToString();

                    Clear();
                }
            } else
            {
                MessageBox.Show("Pilih menu, meja, dan isi jumlah menu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Clear()
        {
            txtNamaMenu.Text = "";
            txtHargaMenu.Text = "";
            txtJumlahMenu.Clear();
        }

        private void txtJumlahMenu_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtJumlahMenu.Text, "[^0-9]"))
            {
                MessageBox.Show("Data jumlah harus diisi dengan nomor.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtJumlahMenu.Clear();
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            dataTable.Clear();
            Clear();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if(dataTable.Rows.Count != 0)
            {
                if(MessageBox.Show("Pesan sekarang?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    kode_transaksi = engine.GenerateCode("kode_transaksi", "transaksi", "TRX", 4);
                    engine.SetDataWithOutMessageBox("insert into transaksi(kode_transaksi, total_bayar, kode_user, kode_meja) values('"+kode_transaksi+"', '"+txtTotalBayar.Text+"', '"+FormLogin.kode_user+"', '"+kode_meja+"')");

                    foreach (DataGridViewRow row in guna2DataGridView3.Rows)
                    {
                        engine.SetDataWithOutMessageBox("insert into detail_transaksi(jumlah_menu, kode_transaksi, kode_menu) values('" + row.Cells["Jumlah Menu"].Value + "', '" + kode_transaksi + "', '" + row.Cells["kode menu"].Value + "')");
                    }

                    engine.SetDataWithOutMessageBox("update meja set status_meja='Diisi' where kode_meja='" + kode_meja + "'");
                    dataTable.Clear();
                    Clear();
                    txtNomorMeja.Text = "";
                    PesanUC_Load(this, null);
                    engine.LogActivity("memesan menu dan meja");

                    MessageBox.Show("Berhasil memesan menu.", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            } else
            {
                MessageBox.Show("Keranjang tidak boleh kosong.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            DataSet data = engine.GetData("select * from meja where kode_meja like '%" + guna2TextBox1.Text + "%' or nomor_meja like '%" + guna2TextBox1.Text + "%' or status_meja like '%" + guna2TextBox1.Text + "%'");
            guna2DataGridView2.DataSource = data.Tables[0];
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                kode_menu = guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtNamaMenu.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtHargaMenu.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
        }

        private void guna2DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(txtNomorMeja.Text == "")
            {
                if (e.RowIndex != -1)
                {
                    kode_meja = guna2DataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtNomorMeja.Text = guna2DataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                }
            } else
            {
                MessageBox.Show("Hanya dapat memilih satu meja.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
