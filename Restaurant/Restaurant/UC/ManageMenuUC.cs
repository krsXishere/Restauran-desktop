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
    public partial class ManageMenuUC : UserControl
    {
        Engine engine = new Engine();
        String imageLocation, kode_menu;
        public ManageMenuUC()
        {
            InitializeComponent();
        }

        private void txtHargaMenu_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtHargaMenu.Text, "[^0-9]"))
            {
                MessageBox.Show("Data harga harus diisi dengan nomor.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHargaMenu.Clear();
            }
        }

        public void ManageMenuUC_Load(object sender, EventArgs e)
        {
            DataSet data = engine.GetData("select * from menu");
            guna2DataGridView1.DataSource = data.Tables[0];
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if(txtNamaMenu.Text != "" && txtDeskripsiMenu.Text != "" && txtKategoriMenu.Text != "" && txtHargaMenu.Text != "" && txtFotoMenu.Text != "")
            {
                if(MessageBox.Show("Simpan menu baru?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    kode_menu = engine.GenerateCode("kode_menu", "menu", "MN", 3);
                    Console.WriteLine(kode_menu);
                    engine.SetData("insert into menu(kode_menu, nama_menu, deskripsi_menu, kategori_menu, harga_menu, foto_menu) values('" + kode_menu + "', '" + txtNamaMenu.Text + "', '" + txtDeskripsiMenu.Text + "', '"+txtKategoriMenu.Text+"', '" + txtHargaMenu.Text + "', '" + txtFotoMenu.Text + "')", "Berhasil menambahkan menu baru.");
                    ManageMenuUC_Load(this, null);
                    engine.LogActivity("menambahkan menu");
                    Clear();
                }
            } else
            {
                engine.FillAllFields();
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (txtNamaMenu.Text != "" && txtDeskripsiMenu.Text != "" && txtKategoriMenu.Text != "" && txtHargaMenu.Text != "" && txtFotoMenu.Text != "")
            {
                if (MessageBox.Show("Simpan perubahan menu?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    engine.SetData("update menu set nama_menu='" + txtNamaMenu.Text + "', deskripsi_menu='" + txtDeskripsiMenu.Text + "', kategori_menu='"+txtKategoriMenu.Text+"', harga_menu='" + txtHargaMenu.Text + "', foto_menu='" + txtFotoMenu.Text + "' where kode_menu='"+kode_menu+"'", "Berhasil mengedit menu.");
                    ManageMenuUC_Load(this, null);
                    engine.LogActivity("mengedit menu");
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
            if (txtNamaMenu.Text != "" && txtDeskripsiMenu.Text != "" && txtKategoriMenu.Text != "" && txtHargaMenu.Text != "" && txtFotoMenu.Text != "")
            {
                if (MessageBox.Show("Hapus menu?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    engine.SetData("delete from menu where kode_menu='" + kode_menu + "'", "Berhasil menghapus menu.");
                    ManageMenuUC_Load(this, null);
                    engine.LogActivity("menghapus menu");
                    Clear();
                }
            }
            else
            {
                engine.SelectDataToDelete();
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                kode_menu = guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtNamaMenu.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtDeskripsiMenu.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtKategoriMenu.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtHargaMenu.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtFotoMenu.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                guna2PictureBox1.ImageLocation = guna2DataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
        }

        private void Clear()
        {
            txtNamaMenu.Clear();
            txtDeskripsiMenu.Clear();
            txtHargaMenu.Clear();
            txtFotoMenu.Text = "";
            txtKategoriMenu.Text = "";
            txtKategoriMenu.SelectedIndex = -1;
            guna2PictureBox1.ImageLocation = engine.blankImage;
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DataSet data = engine.GetData("select * from menu where kode_menu like '%"+txtSearch.Text+"%' or nama_menu like '%"+txtSearch.Text+"%' or harga_menu like '%"+txtSearch.Text+"%' or kategori_menu like '%"+txtSearch.Text+"%'");
            guna2DataGridView1.DataSource = data.Tables[0];
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Image files(*.jpg;*.jpeg;)|*.jpg;*.jpeg;", Multiselect = false })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imageLocation = openFileDialog.FileName;
                    txtFotoMenu.Text = openFileDialog.FileName;
                    guna2PictureBox1.ImageLocation = imageLocation;
                }
            }
        }
    }
}
