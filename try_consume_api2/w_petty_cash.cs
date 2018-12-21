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
    public partial class w_petty_cash : Form
    {
        public w_petty_cash()
        {
            InitializeComponent();
        }



        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            API_petty_cash PETTY = new API_petty_cash();
            PETTY.post_pettyCAsh().Wait();
        }
    }
}
