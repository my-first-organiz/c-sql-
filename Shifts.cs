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
    public partial class Shifts : Form
    {
        string SqlConnection = @"Data Source = DESKTOP-262LE0I\MSSQLSERVER01; Initial Catalog = name_database; Integrated Security = True";

        private int idshifts;

        public Shifts()
        {
            InitializeComponent();
            LoadShifts();
            LoadUsers();
            LoadUsersANDShifts();
        }

        private void LoadShifts()
        {
            string query = $"SELECT * from shift";

            using (SqlConnection connection = new SqlConnection(SqlConnection))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dgv_shifts.DataSource = dataTable;
            }
        }

        

        private void LoadUsers()
        {
            string query = "SELECT userid, lastname FROM users";
            using (SqlConnection connection = new SqlConnection(SqlConnection))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                cbx_addusershift.DataSource = dataTable;
                cbx_addusershift.DisplayMember = "lastname";
                cbx_addusershift.ValueMember = "userid";
            }
        }

        private void LoadUsersANDShifts()
        {
            string query = "SELECT ul.userlistid, u.login, u.firstname, u.lastname FROM [userlist] ul " +
                           "JOIN [users] u ON ul.userid = u.userid " +
                           $"WHERE ul.shiftid = '{idshifts}'";
            using (SqlConnection connection = new SqlConnection(SqlConnection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dgv_usershift.DataSource = dataTable;
            }
        }

        private void bt_addshift_Click(object sender, EventArgs e)
        {
            DateTime dateStart = dt_start.Value;
            DateTime dateEnd = dt_end.Value;

            string query = $"INSERT INTO [shift] (datestart, dateend) VALUES ('{dateStart}', '{dateEnd}')";
            using (SqlConnection connection = new SqlConnection(SqlConnection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }

            LoadShifts();
        }

        private void bt_adduserShift_Click(object sender, EventArgs e)
        {
            int iduser = Convert.ToInt32(cbx_addusershift.SelectedValue);

            string query = $"INSERT INTO userlist (userid, shiftid) VALUES ('{iduser}', '{idshifts}')";
            using (SqlConnection connection = new SqlConnection(SqlConnection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
           
        }

        private void dgv_shifts_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dgv_shifts.SelectedRows.Count > 0)
            {
                idshifts = Convert.ToInt32(dgv_shifts.SelectedRows[0].Cells[0].Value.ToString());  
            }
            LoadUsersANDShifts();
        }
    }
}
