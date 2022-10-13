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
    public partial class DashboardUC : UserControl
    {
        Engine engine = new Engine();
        public DashboardUC()
        {
            InitializeComponent();
        }

        public void DashboardUC_Load(object sender, EventArgs e)
        {
            DataTable jumlahMejaDB = engine.GetOneData("select count(kode_meja) from meja");
            jumlahMeja.Text = jumlahMejaDB.Rows[0][0].ToString();

            DataTable jumlahMenuDB = engine.GetOneData("select count(kode_menu) from menu");
            jumlahMenu.Text = jumlahMenuDB.Rows[0][0].ToString();

            string theDate = DateTime.Now.ToString("yyyy-MM-dd");
            DataTable totalHariIniDB = engine.GetOneData("select sum(menu.harga_menu * detail_transaksi.jumlah_menu) from menu, detail_transaksi, transaksi where detail_transaksi.kode_transaksi = transaksi.kode_transaksi and detail_transaksi.kode_menu = menu.kode_menu and (transaksi.tanggal_filter = '"+theDate+"')");
            if (totalHariIniDB.Rows[0][0].ToString() == "")
            {
                label6.Text = "Belum ada pemasukan";
                incomeToday.Visible = false;
            } else
            {
                incomeToday.Text = totalHariIniDB.Rows[0][0].ToString();
            }

            DataTable makananTerlarisDB = engine.GetOneData("select menu.nama_menu, count(detail_transaksi.id_detail_transaksi) as total from menu, detail_transaksi where detail_transaksi.kode_menu = menu.kode_menu and (menu.kategori_menu = 'Makanan') group by menu.nama_menu order by total desc");
            if(makananTerlarisDB.Rows.Count != 0)
            {
                makananTerlaris.Text = makananTerlarisDB.Rows[0][0].ToString();
            } else
            {
                makananTerlaris.Text = "Belum tersedia";
            }

            DataTable minumanTerlarisDB = engine.GetOneData("select menu.nama_menu, count(detail_transaksi.id_detail_transaksi) as total from menu, detail_transaksi where detail_transaksi.kode_menu = menu.kode_menu and (menu.kategori_menu = 'Minuman') group by menu.nama_menu order by total desc");
            if(minumanTerlarisDB.Rows.Count != 0)
            {
                minumanTerlaris.Text = minumanTerlarisDB.Rows[0][0].ToString();
            } else
            {
                minumanTerlaris.Text = "Belum tersedia";
            }

            DataTable jumlahPegawaiDB = engine.GetOneData("select count(kode_user) from users");
            jumlahPegawai.Text = jumlahPegawaiDB.Rows[0][0].ToString();
        }
    }
}
