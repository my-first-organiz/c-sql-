using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Employees employ = new Employees();  
            employ.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Orders orders = new Orders();
            orders.Show();
        }

        private void bt_shifts_Click(object sender, EventArgs e)
        {
            this.Hide();
            Shifts shifts = new Shifts();
            shifts.Show();
        }
    }
}
