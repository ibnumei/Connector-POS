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
    class API_Bank
    {
        Connection ckon = new Connection();
        LinkSwagger ls = new LinkSwagger();
        //===============BANK==========
        String id_bank, nm_bank, store;

        //======================================DELETE DATA BEFORE GET FROM API==================================
        public void delete()
        {
            ckon.con.Close();
            String sql = "DELETE FROM bank";
            Crud INPUT = new Crud();
            INPUT.NonReturn2(sql);
        }
        //==========AMBIL KODE STORE
        public void get_cust_id()
        {
            ckon.con.Close();
            String sql = "SELECT * FROM store";
            ckon.cmd = new MySqlCommand(sql, ckon.con);
            ckon.con.Open();
            ckon.myReader = ckon.cmd.ExecuteReader();
            while (ckon.myReader.Read())
            {
                store = ckon.myReader.GetString("CODE");
            }
            ckon.con.Close();
        }
        //======AMBIL DATA DARI API
        public async Task get_data_bank()
        {
            String response = "";
            var credentials = new NetworkCredential("username", "password");
            var handler = new HttpClientHandler { Credentials = credentials };
            //var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            using (var client = new HttpClient(handler))
            {
                //HttpResponseMessage message = client.GetAsync("http://retailbiensi.azurewebsites.net/api/StoreData?storeCode=" + store).Result;
                //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/StoreData?storeCode=" + store).Result;
                HttpResponseMessage message = client.GetAsync(ls.link+"/api/StoreData?storeCode=" + store).Result;

                if (message.Content != null)
                {

                    // GET RETURN VALUE FROM POST API
                    var serializer = new DataContractJsonSerializer(typeof(StoreMaster_respone));
                    var responseContent = message.Content.ReadAsStringAsync().Result;
                    byte[] byteArray = Encoding.UTF8.GetBytes(responseContent);
                    MemoryStream stream = new MemoryStream(byteArray);
                    StoreMaster_respone resultData = serializer.ReadObject(stream) as StoreMaster_respone;
                    try
                    {
                        //==================================BANK
                        foreach (var B in resultData.banks)
                        {
                            //MessageBox.Show(B.bankId + " , " + B.bankName);
                            id_bank = B.bankId;
                            nm_bank = B.bankName;
                            String sql4 = "INSERT INTO bank (BANK_ID, BANK_NAME) VALUES ('" + id_bank + "', '" + nm_bank + "')";
                            Crud input4 = new Crud();
                            input4.NonReturn(sql4);
                        }
                        MessageBox.Show("Successful Update Data Bank", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("Make Sure You Are Connected To Internet", "Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {

                }
            }
        }
        //=========================================================================================
    }
}
