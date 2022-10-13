using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Restaurant
{
    internal class Engine
    {
        public string blankImage = @"C:\Users\rplne\Documents\XII-RPL\Krisna-Purnama\PBO\Re-Restaurant\Restaurant\Restaurant\Assets\blank-image.jpeg";

        public SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @"Data Source=DESKTOP-QF9S06Q;Initial Catalog=restaurant;Integrated Security=True";

            return connection;
        }

        public DataSet GetData(String query)
        {
            SqlConnection connection = GetConnection();
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataSet data = new DataSet();
            sqlDataAdapter.Fill(data);

            return data;
        }

        public DataTable GetOneData(String query)
        {
            SqlConnection connection = GetConnection();
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable data = new DataTable();
            sqlDataAdapter.Fill(data);

            return data;
        }

        public void SetData(String query, String message)
        {
            SqlConnection connection = GetConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            connection.Open();
            command.CommandType = CommandType.Text;
            command.CommandText = query;
            command.ExecuteNonQuery();
            connection.Close();

            MessageBox.Show("" + message + "", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void SetDataWithOutMessageBox(String query)
        {
            SqlConnection connection = GetConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            connection.Open();
            command.CommandType = CommandType.Text;
            command.CommandText = query;
            command.ExecuteNonQuery();
            connection.Close();
        }

        public DataTable Login(String username, String password)
        {
            SqlConnection connection = GetConnection();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select kode_user, nama_user, level_user from users where username='" + username + "' and pass='" + password + "'", connection);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);

            return dt;
        }

        public void LogActivity(String logActivity)
        {
            SqlConnection connection = GetConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            connection.Open();
            command.CommandType = CommandType.Text;
            command.CommandText = "insert into log_activity (kode_user, user_activity) values('" + FormLogin.kode_user + "', '" + logActivity + "')";
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void FillAllFields()
        {
            MessageBox.Show("Isi Semua Data.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void SelectDataToEdit()
        {
            MessageBox.Show("Pilih data untuk diedit.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void SelectDataToDelete()
        {
            MessageBox.Show("Pilih data untuk dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void ValidateCash()
        {
            MessageBox.Show("Uang tidak boleh kurang dari total tagihan.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public String GenerateCode(String column, String table, String serial, int deleteIndexSerial)
        {
            DataTable countDB = GetOneData("select count("+column+") from "+table+"");
            int count = int.Parse(countDB.Rows[0][0].ToString());
            int initialValue = 000;
            
            if(countDB.Rows.Count != 0)
            {
                int lastValueCount = initialValue + count + 1;
                String codeValue = serial + "-" + lastValueCount.ToString("D5");
                return codeValue.Remove(deleteIndexSerial, 1);
            } else
            {
                int lastValueCount = initialValue + 1;
                String codeValue = serial + "-" + lastValueCount.ToString("D5");
                return codeValue.Remove(deleteIndexSerial, 1);
            }
        }
    }
}
