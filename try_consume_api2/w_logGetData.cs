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
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace try_consume_api2
{
    public partial class w_logGetData : Form
    {
        Connection ckon = new Connection();
        public w_logGetData()
        {
            InitializeComponent();
        }
        public void update_tabel()
        {
            ckon.con.Close();
            dgv_status.Rows.Clear();
            String sql = "select * from log_msg";
            ckon.cmd = new MySqlCommand(sql, ckon.con);
            ckon.con.Open();
            ckon.myReader = ckon.cmd.ExecuteReader();
            while (ckon.myReader.Read())
            {
                int n = dgv_status.Rows.Add();
                dgv_status.Rows[n].Cells[0].Value = ckon.myReader.GetString("Data");
                dgv_status.Rows[n].Cells[1].Value = ckon.myReader.GetString("Status");
            }
            ckon.dt.Rows.Clear();
            ckon.con.Close();
        }

        private void w_logGetData_Load(object sender, EventArgs e)
        {
            update_tabel();
        }
    }
}
