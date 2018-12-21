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
    public partial class W_TransToday : Form
    {
        public W_TransToday()
        {
            InitializeComponent();
        }

        private void b_reload_Click(object sender, EventArgs e)
        {
            API_TransToday today = new API_TransToday();
            today.get_cust_id();
            today.getTransToday().Wait();
        }
    }
}
