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
    class API_StoreRelasi1
    {
        Connection ckon = new Connection();
        Connection2 ckon2 = new Connection2();
        String store_code, city;
        LinkSwagger ls = new LinkSwagger();

        public void get_cust_id()
        {
            ckon.con.Close();
            String sql = "SELECT * FROM store";
            ckon.cmd = new MySqlCommand(sql, ckon.con);
            ckon.con.Open();
            ckon.myReader = ckon.cmd.ExecuteReader();
            while (ckon.myReader.Read())
            {
                string ANGKA = "2";
                store_code = ckon.myReader.GetString("CODE");
                city = ckon.myReader.GetString("REGIONAL");//MENGAMBIL DATA REGIONAL UNTUK MENDAPATKAN SEMUA DATA
                //MessageBox.Show(ANGKA + " , " + store_code +", " + city);
            }
            ckon.con.Close();
        }
        //======================================DELETE DATA BEFORE GET FROM API==================================
        public void delete()
        {
            ckon.con.Close();
            String sql = "DELETE FROM store_relasi";
            Crud delete = new Crud();
            delete.NonReturn2(sql);

        }
        //==============================================================================
        public async Task get_Store_relasi()
        {
            String response = "";
            var credentials = new NetworkCredential("username", "password");
            var handler = new HttpClientHandler { Credentials = credentials }; // for validation
                                                                               //    handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };// allow domain checker
            using (var client = new HttpClient(handler))
            {
                // Make your request...
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    //HttpResponseMessage message = client.GetAsync("http://retailbiensi.azurewebsites.net/api/StoreMaster?condition=2&paramValue="+ city +"&storeCode="+store_code).Result;
                    //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/StoreMaster?condition=2&paramValue=" + city + "&storeCode=" + store_code).Result;
                    HttpResponseMessage message = client.GetAsync(ls.link+"/api/StoreMaster?condition=2&paramValue=" + city + "&storeCode=" + store_code).Result;

                    string ConnectionString = "Server='" + try_consume_api2.Properties.Settings.Default.mServer + "';Database='" + try_consume_api2.Properties.Settings.Default.mDBName + "';Uid='" + try_consume_api2.Properties.Settings.Default.mUserDB + "';Pwd='" + try_consume_api2.Properties.Settings.Default.mPassDB + "';";
                    StringBuilder sCommand = new StringBuilder("INSERT INTO store_relasi (_id ,CODE, NAME, LOCATION, ADDRESS, CITY, REGIONAL,STORE_TYPE_ID,ADDRESS2,ADDRESS3,ADDRESS4,WAREHOUSE_ID,CUST_ID_STORE) VALUES");

                    if (message.IsSuccessStatusCode)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(List<StoreRelasi>));
                        var result = message.Content.ReadAsStringAsync().Result;
                        byte[] byteArray = Encoding.UTF8.GetBytes(result);
                        MemoryStream stream = new MemoryStream(byteArray);
                        List<StoreRelasi> resultData = serializer.ReadObject(stream) as List<StoreRelasi>;

                        for (int i = 0; i < resultData.Count; i++)
                        {
                            try
                            {

                                String sql = "INSERT INTO store_relasi (_id ,CODE, NAME, LOCATION, ADDRESS, CITY, REGIONAL,STORE_TYPE_ID,ADDRESS2,ADDRESS3,ADDRESS4,WAREHOUSE_ID,CUST_ID_STORE) VALUES('" + resultData[i].Id + "' ,'" + resultData[i].Code + "', '" + resultData[i].Name + "', '" + resultData[i].Location + "', '" + resultData[i].Address + "', '" + resultData[i].City + "', '" + resultData[i].Regional + "','" + resultData[i].StoreTypeId + "','" + resultData[i].Address2 + "','" + resultData[i].Address3 + "','" + resultData[i].Address4 + "','" + resultData[i].WarehouseId + "','" + resultData[i].CustomerIdStore + "')";
                                Crud input = new Crud();
                                input.NonReturn2(sql);
                                //MessageBox.Show(resultData[i].Id + "," + "," + resultData[i].Code + "," + resultData[i].Name);
                            }
                            catch (Exception ex)
                            {
                                //MessageBox.Show(ex.ToString());

                            }

                        }

                        String query = "UPDATE log_msg SET Status='Success' WHERE Data = 'Store Relasi' ";
                        Crud update = new Crud();
                        update.NonReturn2(query);
                        MessageBox.Show("Successful Update Data Store Relasi", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        String query = "UPDATE log_msg SET Status='Failed' WHERE Data = 'Store Relasi' ";
                        Crud update = new Crud();
                        update.NonReturn2(query);

                        response = "Fail";
                        MessageBox.Show("Error API Store Relasi", "Error API", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                catch (Exception ex)
                {
                    String query = "UPDATE log_msg SET Status='Failed' WHERE Data = 'Store Relasi' ";
                    Crud update = new Crud();
                    update.NonReturn2(query);

                    response = ex.ToString();
                    MessageBox.Show("Make Sure You Are Connected To The Internet", "No Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        //====================================================================================================
    }
}
