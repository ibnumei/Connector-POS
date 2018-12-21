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
    public partial class W_Coba_MEthod : Form
    {
        public W_Coba_MEthod()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Try_API_Inventory api_inv = new Try_API_Inventory();

            MessageBox.Show("Sukses");
        }
    }
}
