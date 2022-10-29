using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Restaurant;

namespace Restaurant
{
    public partial class FormLogin : Form
    {
        Engine engine = new Engine();
        public static string kode_user;
        public static string nama_user;
        public static string level_user;
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            wrong.Visible = false;
            guna2TextBox2.Clear();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text != "" && guna2TextBox2.Text != "")
            {
                DataTable data = engine.Login(guna2TextBox1.Text, guna2TextBox2.Text);

                if (data.Rows.Count == 1)
                {
                    kode_user = data.Rows[0][0].ToString();
                    nama_user = data.Rows[0][1].ToString();
                    level_user = data.Rows[0][2].ToString();
                    engine.LogActivity("login");

                    if(level_user == "Admin")
                    {
                        FormAdmin fa = new FormAdmin();
                        this.Hide();
                        fa.Show();
                    } else if (level_user == "Kasir")
                    {
                        FormKasir fk = new FormKasir();
                        this.Hide();
                        fk.Show();
                    } else { }
                }
                else
                {
                    wrong.Visible = true;
                    guna2TextBox1.Clear();
                    guna2TextBox2.Clear();

                    Timer myTimer = new Timer();
                    myTimer.Interval = (3 * 1000);
                    myTimer.Tick += new EventHandler(timer1_Tick);
                    myTimer.Start();
                }
            }
            else
            {
                engine.FillAllFields();
            }
        }

        private void guna2TextBox2_MouseHover(object sender, EventArgs e)
        {
            guna2TextBox2.PasswordChar = '\0';
        }

        private void guna2TextBox2_MouseLeave(object sender, EventArgs e)
        {
            guna2TextBox2.PasswordChar = '•';
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            wrong.Visible = false;
        }
    }
}
