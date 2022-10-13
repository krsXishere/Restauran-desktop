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
    public partial class LogAcitvityUC : UserControl
    {
        Engine engine = new Engine();
        public LogAcitvityUC()
        {
            InitializeComponent();
        }

        public void LogAcitvity_Load(object sender, EventArgs e)
        {
            String theDate = DateTime.Now.ToString("yyyy-MM-dd");
            DataSet data = engine.GetData("select users.kode_user, users.nama_user, log_activity.user_activity, log_activity.tanggal_activity, users.level_user from log_activity, users where log_activity.kode_user = users.kode_user and (tanggal_filter = '" + theDate + "')");
            guna2DataGridView1.DataSource = data.Tables[0];
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            String theDate = guna2DateTimePicker1.Value.ToString("yyyy-MM-dd");
            DataSet data = engine.GetData("select users.kode_user, users.nama_user, log_activity.user_activity, log_activity.tanggal_activity, users.level_user from log_activity, users where log_activity.kode_user = users.kode_user and tanggal_filter = '" + theDate + "' and (users.kode_user like '%" + txtSearch.Text+ "%' or users.nama_user like '%"+txtSearch.Text+ "%' or users.level_user like '%" + txtSearch.Text+ "%' or log_activity.user_activity like '%"+txtSearch.Text+"%')");
            guna2DataGridView1.DataSource = data.Tables[0];
        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            String theDate = guna2DateTimePicker1.Value.ToString("yyyy-MM-dd");
            DataSet data = engine.GetData("select users.kode_user, users.nama_user, log_activity.user_activity, log_activity.tanggal_activity, users.level_user from log_activity, users where log_activity.kode_user = users.kode_user and (tanggal_filter = '" + theDate + "')");
            guna2DataGridView1.DataSource = data.Tables[0];
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            FormAboutApp fab = new FormAboutApp();
            fab.Show();
        }
    }
}
