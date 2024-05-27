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
    public partial class NewEmploye : Form
    {
        string SqlConnection = @"Data Source = DESKTOP-262LE0I\MSSQLSERVER01; Initial Catalog = name_database; Integrated Security = True";

        public NewEmploye()
        {
            InitializeComponent();
            LoadRoles();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoadRoles()
        {
            string query = "SELECT userroleid, namerole FROM userrole";
            using (SqlConnection connection = new SqlConnection(SqlConnection))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                cbx_role.DataSource = dataTable;
                cbx_role.DisplayMember = "namerole";
                cbx_role.ValueMember = "userroleid";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = tbx_login.Text;
            string password = tbx_password.Text;
            string lastname = tbx_lastname.Text;
            string firstname = tbx_firstname.Text;
            string middlenme = tbx_middlename.Text;
            int roleid = Convert.ToInt32(cbx_role.SelectedValue);

            string query = $"INSERT INTO users (login, password, lastname, firstname, middlename, userroleid) VALUES ('{login}', '{password}', '{lastname}', '{firstname}', '{middlenme}', '{roleid}')'";
            using (SqlConnection connection = new SqlConnection(SqlConnection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
            this.Close();
        }
    }
}
