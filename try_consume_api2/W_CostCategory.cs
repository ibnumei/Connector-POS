using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Runtime.Serialization.Json;

namespace try_consume_api2
{
    public partial class W_CostCategory : Form
    {
        Connection ckon = new Connection();
        Connection2 ckon2 = new Connection2();
        String del_id_cost;
        public W_CostCategory()
        {
            InitializeComponent();
        }
        //=======================================================================================================
        private void b_reload_Click(object sender, EventArgs e)
        {

            API_Expense exp = new API_Expense();
            exp.delete();
            exp.getArticle().Wait();
            //retreive();
            //MessageBox.Show("Data has been sent to local database ");
        }
        //=======================================================================================================
    }
}
