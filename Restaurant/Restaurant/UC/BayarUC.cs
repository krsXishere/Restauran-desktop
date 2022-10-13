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
    public partial class BayarUC : UserControl
    {
        Engine engine = new Engine();
        String kode_transaksi, kode_meja;
        public BayarUC()
        {
            InitializeComponent();
        }

        public void BayarUC_Load(object sender, EventArgs e)
        {
            DataSet data = engine.GetData("select transaksi.kode_transaksi, transaksi.kode_meja, users.kode_user, users.nama_user, transaksi.tanggal_transaksi, transaksi.total_bayar, transaksi.status_transaksi from users, transaksi, meja where transaksi.kode_meja = meja.kode_meja and transaksi.kode_user = users.kode_user and (status_transaksi='Belum dibayar')");
            guna2DataGridView1.DataSource = data.Tables[0];
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                kode_transaksi = guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                kode_meja = guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtTotalTagihan.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
        }

        private void Clear()
        {
            txtTotalTagihan.Text = "";
            txtTotalBayar.Clear();
            txtKembalian.Text = "";
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void txtTotalBayar_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtTotalBayar.Text, "[^0-9]"))
            {
                MessageBox.Show("Data total bayar harus diisi dengan nomor.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTotalBayar.Clear();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (txtTotalTagihan.Text != "")
            {
                if(int.Parse(txtTotalBayar.Text) >= int.Parse(txtTotalTagihan.Text)) {
                    int kembalian = int.Parse(txtTotalBayar.Text) - int.Parse(txtTotalTagihan.Text);
                    txtKembalian.Text = kembalian.ToString();
                    engine.SetDataWithOutMessageBox("update meja set status_meja='Kosong' where kode_meja='"+kode_meja+"'");
                    engine.SetData("update transaksi set status_transaksi='Sudah dibayar'", "Berhasil membayar.");
                    BayarUC_Load(this, null);
                    engine.LogActivity("membayar transaksi");
                } else
                {
                    engine.ValidateCash();
                }
            }
            else
            {
                engine.FillAllFields();
            }
        }
    }
}
