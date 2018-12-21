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
    public partial class W_TransHistory : Form
    {
        public W_TransHistory()
        {
            InitializeComponent();
        }

        private void b_reload_Click(object sender, EventArgs e)
        {
            API_TransHistory his = new API_TransHistory();
            his.get_cust_id();
            his.getTransHistory().Wait();
        }
    }
}
