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
    public partial class Orders : Form
    {
        string SqlConnection = @"Data Source = DESKTOP-262LE0I\MSSQLSERVER01; Initial Catalog = name_database; Integrated Security = True";
        public Orders()
        {
            InitializeComponent();
            LoadOrders();
        }

        private void LoadOrders()
        {
            string query = $"SELECT * from order";

            using (SqlConnection connection = new SqlConnection(SqlConnection))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dgv_orders.DataSource = dataTable;
            }
        }


        private void Orders_Load(object sender, EventArgs e)
        {

        }
    }
}
