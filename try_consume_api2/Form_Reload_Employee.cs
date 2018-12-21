using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace try_consume_api2
{
    public partial class Form_Reload_Employee : Form
    {
        public Form_Reload_Employee()
        {
            InitializeComponent();
        }

        private void b_reload_Click(object sender, EventArgs e)
        {
            API_Employee employee = new API_Employee();
            employee.get_cust_id();
            employee.delete();
            employee.get_data_employee().Wait();
        }
    }
}
