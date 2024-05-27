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
    public partial class Translator : Form
    {
        string SqlConnection = @"Data Source = DESKTOP-262LE0I\MSSQLSERVER01; Initial Catalog = name_database; Integrated Security = True";

        public Translator()
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
                dgv_orders.DataSource = dataTable;
            }
        }

        private void bt__Click(object sender, EventArgs e)
        {
            int orderID = Convert.ToInt32(dgv_orders.SelectedRows[0].Cells["orderid"].Value);
            string query = $"UPDATE orders SET orderstatus = 'готов' WHERE orderid = '{orderID}'";
            using (SqlConnection connection = new SqlConnection(SqlConnection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
            LoadOrders();
        }
    }
}
