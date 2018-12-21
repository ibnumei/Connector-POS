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
    public partial class w_bankcs : Form
    {
        public w_bankcs()
        {
            InitializeComponent();
        }

        private void b_reload_Click(object sender, EventArgs e)
        {
            API_Bank bank = new API_Bank();
            bank.delete();
            bank.get_cust_id();
            bank.get_data_bank().Wait();
        }
    }
}
