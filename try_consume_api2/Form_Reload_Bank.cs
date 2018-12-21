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
    public partial class Form_Reload_Bank : Form
    {
        public Form_Reload_Bank()
        {
            InitializeComponent();
        }

        private void b_reload_Click(object sender, EventArgs e)
        {
            API_Bank BANK = new API_Bank();
            BANK.get_cust_id();
            BANK.delete();
            BANK.get_data_bank().Wait();
            this.Close();
        }
    }
}
