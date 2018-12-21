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
    public partial class W_BudgetPettyCash : Form
    {
        Connection ckon = new Connection();
        String id_store;
        int budget;
        public W_BudgetPettyCash()
        {
            InitializeComponent();
        }

        private void b_reload_Click(object sender, EventArgs e)
        {
            get_cust_id();
            Post_Get_StoreData().Wait();
        }
        public void get_cust_id()
        {
            ckon.con.Close();
            String sql = "SELECT * FROM store";
            ckon.cmd = new MySqlCommand(sql, ckon.con);
            ckon.con.Open();
            ckon.myReader = ckon.cmd.ExecuteReader();
            while (ckon.myReader.Read())
            {
                id_store = ckon.myReader.GetString("CODE");
            }
            ckon.con.Close();
        }
        public async Task Post_Get_StoreData()
        {
            LinkSwagger ls = new LinkSwagger();
            
            String response = "";
            var credentials = new NetworkCredential("username", "password");
            var handler = new HttpClientHandler { Credentials = credentials };
            //var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            using (var client = new HttpClient(handler))
            {
                try
                {
                    HttpResponseMessage message = client.GetAsync(ls.link+"/api/StoreData?storeCode=" + id_store).Result;
                    //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id:8082/api/StoreData?storeCode=aab").Result;
                    if (message.Content != null)
                    {
                        // GET RETURN VALUE FROM POST API
                        var serializer = new DataContractJsonSerializer(typeof(StoreMaster_respone));
                        var responseContent = message.Content.ReadAsStringAsync().Result;
                        byte[] byteArray = Encoding.UTF8.GetBytes(responseContent);
                        MemoryStream stream = new MemoryStream(byteArray);
                        StoreMaster_respone resultData = serializer.ReadObject(stream) as StoreMaster_respone;

                        //================================INSERT STORE=====================================================
                         budget = resultData.budgetStore.remaining;
                        //int id = resultData.store.Id;
                        String sql = "UPDATE store SET BUDGET_TO_STORE = " + budget + "";
                        Crud input2 = new Crud();
                        input2.NonReturn(sql);

                        MessageBox.Show("Successful Update Data Budget", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error API Strore", "Error API", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    //=================END IF RETURN HAVE VALUE==========
                }
                catch (Exception ex )
                {
                    MessageBox.Show(ex.ToString());
                }

            }
            //=====================END POST VALUE FOR API============
        }
    }
}
