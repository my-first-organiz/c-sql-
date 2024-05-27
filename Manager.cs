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
    public partial class Manager : Form
    {
        string SqlConnection = @"Data Source = DESKTOP-262LE0I\MSSQLSERVER01; Initial Catalog = name_database; Integrated Security = True";

        public Manager()
        {
            InitializeComponent();
            LoadOrders();
        }

        private void LoadOrders()
        {
            string query = $"SELECT * from orders";

            using (SqlConnection connection = new SqlConnection(SqlConnection))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dgv_order.DataSource = dataTable;
            }
        }

        private void Manager_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string language = tbx_language.Text;
            string format = radiobt_txt.Checked ? "Тест" : "Аудио";
            int amountTranslation = (int)numeric.Value;

            if (string.IsNullOrWhiteSpace(language) || string.IsNullOrWhiteSpace(format))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            string SqlConnection = @"Data Source = DESKTOP-262LE0I\MSSQLSERVER01; Initial Catalog = name_database; Integrated Security = True";

            string query = "INSERT INTO orders (datecreation, orderstatus, paymentstatus, language, format, amounttranslation) " +
                           $"VALUES ('{DateTime.Now}', 'готовится', 'принят', '{language}', '{format}', '{amountTranslation}')";

            using (SqlConnection connection = new SqlConnection(SqlConnection))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Заказ успешно создан.");
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при создании заказа: " + ex.Message);
                }
            }
        }
    }
}