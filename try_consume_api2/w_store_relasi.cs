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
    public partial class w_store_relasi : Form
    {
        public w_store_relasi()
        {
            InitializeComponent();
        }


        private void b_reload_Click(object sender, EventArgs e)
        {
            API_StoreRelasi1 relasi = new API_StoreRelasi1();
            relasi.delete();
            relasi.get_cust_id();
            relasi.get_Store_relasi().Wait();
        }
    }
}
