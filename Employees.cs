using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Employees : Form
    {
        string SqlConnection = @"Data Source = DESKTOP-262LE0I\MSSQLSERVER01; Initial Catalog = name_database; Integrated Security = True";
        public Employees()
        {
            InitializeComponent();
            LoadEmployees();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadEmployees()
        {
            string query = $"SELECT * from users";

            using (SqlConnection connection = new SqlConnection(SqlConnection))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dgv_employ.DataSource = dataTable;
            }
        }

        private void Dismiss()
        {
            int userID = Convert.ToInt32(dgv_employ.SelectedRows[0].Cells["userid"].Value);
            string query = $"UPDATE users SET status = 'уволен' WHERE userid = '{userID}'";
            using (SqlConnection connection = new SqlConnection(SqlConnection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
            LoadEmployees();
        }

        private void bt_newemploye_Click(object sender, EventArgs e)
        {
            this.Hide();
            NewEmploye newEmploye = new NewEmploye();
            newEmploye.Show();
        }

        private void bt_dismiss_Click(object sender, EventArgs e)
        {
            Dismiss();
        }
    }

}
