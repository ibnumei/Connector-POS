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
    public partial class W_Inventorycs : Form
    {
        Connection ckon = new Connection();
        Connection2 ckon2 = new Connection2();
        String store_code, del_id_art;
        public W_Inventorycs()
        {
            InitializeComponent();
        }
        //==============================================================================================================================
        private void b_reload_Click(object sender, EventArgs e)
        {
            //store_code = t_store.Text;
            API_Inventory INV = new API_Inventory();
            INV.get_cust_id();
            INV.delete();
            INV.getArticle().Wait();
            //retreive();
        }
        //==============================================================================================================================

        //======================================DELETE DATA BEFORE GET FROM API==================================
        public void delete()
        {
            ckon.con.Close();
            String sql = "DELETE FROM inventory";
            Crud delete = new Crud();
            delete.NonReturn2(sql);
            
        }
        //=======================================================================================================
    }
}
