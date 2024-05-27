using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApp1
{
    public partial class LoginForm : Form
    {
      
        string SqlConnection = @"Data Source = DESKTOP-262LE0I\MSSQLSERVER01; Initial Catalog = name_database; Integrated Security = True";
        
        
        
        public LoginForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void butLogin_Click(object sender, EventArgs e)
        {
            string login = textBox_login.Text;
            string password = textBox_password.Text;

            string query = $"SELECT userroleid from users WHERE login = '{login}' AND password = '{password}'";
            using (SqlConnection connection = new SqlConnection(SqlConnection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int role = reader.GetInt32(0);
                        if (role == 1)
                        {
                            this.Hide();
                            Admin admin = new Admin();
                            admin.Show();
                        }
                        else if (role == 2)
                        {
                            Manager manager = new Manager();
                            manager.Show();
                            this.Hide();
                        }
                        else if (role == 3)
                        {
                            Translator trans = new Translator();
                            trans.Show();
                            this.Hide();
                        }
                        else
                            MessageBox.Show("У вас нет роли.");
                    }

                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                }
                
                reader.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
