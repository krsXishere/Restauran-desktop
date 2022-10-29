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

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(Setruk(), new Font("Century Gothic", 18, FontStyle.Bold), Brushes.Black, new Point(10, 10));
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (txtTotalTagihan.Text != "")
            {
                if (int.Parse(txtTotalBayar.Text) >= int.Parse(txtTotalTagihan.Text)) {
                    int kembalian = int.Parse(txtTotalBayar.Text) - int.Parse(txtTotalTagihan.Text);
                    txtKembalian.Text = kembalian.ToString();
                    engine.SetDataWithOutMessageBox("update meja set status_meja='Kosong' where kode_meja='" + kode_meja + "'");
                    engine.SetData("update transaksi set status_transaksi='Sudah dibayar'", "Berhasil membayar.");
                    BayarUC_Load(this, null);
                    engine.LogActivity("membayar transaksi");

                    printPreviewDialog1.Document = printDocument1;
                    printPreviewDialog1.ShowDialog();
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

        public String Setruk()
        {
            String setruk;
            DataSet barangDB = engine.GetData("select menu.nama_menu, detail_transaksi.jumlah_menu, menu.harga_menu from menu, transaksi, detail_transaksi, users where transaksi.kode_user = users.kode_user and transaksi.kode_transaksi = detail_transaksi.kode_transaksi and detail_transaksi.kode_menu = menu.kode_menu and (transaksi.kode_transaksi = '"+kode_transaksi+"')");
            DataTable infoTransaksi = engine.GetOneData("select transaksi.kode_transaksi, users.nama_user from transaksi, users where transaksi.kode_user = users.kode_user and (transaksi.kode_transaksi = '"+kode_transaksi+"')");

            setruk = "                                                     Restoran     \n";
            setruk += "                                                 Kelompok 2     \n";
            setruk += "                                 Jl. Angkrek No. 35, Sumedang     \n";
            setruk += "                                         Transaksi No. #" + infoTransaksi.Rows[0][0].ToString() +"     \n";
            setruk += "************************************************************************\n";
            setruk += "Tanggal transaksi:                                               " + DateTime.Now + "\n";
            setruk += "Kasir:                                                                      " + infoTransaksi.Rows[0][1].ToString() + "\n";
            setruk += "************************************************************************\n";
            setruk += "Menu yang dibeli: \n";
            foreach (DataRow row in barangDB.Tables[0].Rows)
            {
                setruk += row["jumlah_menu"].ToString() + " " + row["nama_menu"].ToString() + "                                                                         " + int.Parse(row["harga_menu"].ToString()) * int.Parse(row["jumlah_menu"].ToString()) + "\n";
            }
            setruk += "************************************************************************\n";
            setruk += "Total harga:                                                                                 Rp" + txtTotalTagihan.Text + "\n";
            setruk += "Total bayar:                                                                                 Rp" + txtTotalBayar.Text + "\n";
            setruk += "Kembali:                                                                                      Rp" + txtKembalian.Text + "\n";
            return setruk;
        }
    }
}
