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
    class API_DiscountRetail
    {
        Connection ckon = new Connection();
        LinkSwagger ls = new LinkSwagger();

        String store_code;

        public void get_cust_id()
        {
            ckon.con.Close();
            String sql = "SELECT * FROM store";
            ckon.cmd = new MySqlCommand(sql, ckon.con);
            ckon.con.Open();
            ckon.myReader = ckon.cmd.ExecuteReader();
            while (ckon.myReader.Read())
            {
                store_code = ckon.myReader.GetString("CODE");
            }
            ckon.con.Close();
        }

        
        //=======================================================================================================
        public async Task get_DiscountRetail()
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
                    //HttpResponseMessage message = client.GetAsync("http://retailbiensi.azurewebsites.net/api/DiscountRetailApi").Result;
                    //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/DiscountRetailApi").Result;
                    HttpResponseMessage message = client.GetAsync(ls.link+"/api/DiscountRetailStore?storeCode=" + store_code).Result;

                    string ConnectionString = "Server='" + try_consume_api2.Properties.Settings.Default.mServer + "';Database='" + try_consume_api2.Properties.Settings.Default.mDBName + "';Uid='" + try_consume_api2.Properties.Settings.Default.mUserDB + "';Pwd='" + try_consume_api2.Properties.Settings.Default.mPassDB + "';";
                    StringBuilder sCommand = new StringBuilder("INSERT INTO discountretail (Id, DiscountCategory, DiscountCode, DiscountName, CustomerGroupId, DiscountPartner, Description, DiscountType, StartDate, EndDate, Status, DiscountPercent) VALUES");

                    if (message.IsSuccessStatusCode)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(List<DiscountRetailApi>));
                        var result = message.Content.ReadAsStringAsync().Result;
                        byte[] byteArray = Encoding.UTF8.GetBytes(result);
                        MemoryStream stream = new MemoryStream(byteArray);
                        List<DiscountRetailApi> resultData = serializer.ReadObject(stream) as List<DiscountRetailApi>;

                            using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                            {

                                List<string> Rows = new List<string>();
                                for (int i = 0; i < resultData.Count; i++)
                                {
                                    Rows.Add(string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')", MySqlHelper.EscapeString(resultData[i].Id.ToString()), MySqlHelper.EscapeString(resultData[i].DiscountCategory.ToString()), MySqlHelper.EscapeString(resultData[i].DiscountCode), MySqlHelper.EscapeString(resultData[i].DiscountName), MySqlHelper.EscapeString(resultData[i].CustomerGroupId.ToString()), MySqlHelper.EscapeString(resultData[i].DiscountPartner), MySqlHelper.EscapeString(resultData[i].Description), MySqlHelper.EscapeString(resultData[i].DiscountType.ToString()), MySqlHelper.EscapeString(resultData[i].StartDate), MySqlHelper.EscapeString(resultData[i].EndDate), MySqlHelper.EscapeString(resultData[i].Status), MySqlHelper.EscapeString(resultData[i].DiscountPercent.ToString())));
                                }
                                sCommand.Append(string.Join(",", Rows));
                                sCommand.Append(";");
                                mConnection.Open();
                                using (MySqlCommand myCmd = new MySqlCommand(sCommand.ToString(), mConnection))
                                {
                                    myCmd.CommandType = CommandType.Text;
                                    myCmd.ExecuteNonQuery();

                                    String query = "UPDATE log_msg SET Status='Success' WHERE Data = 'Discount Retail' ";
                                    Crud update = new Crud();
                                    update.NonReturn2(query);
                                    MessageBox.Show("Successful Update Data Discount Retail", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                            }
                       
                       
                    }
                    else
                    {
                        String query = "UPDATE log_msg SET Status='Failed' WHERE Data = 'Discount Retail' ";
                        Crud update = new Crud();
                        update.NonReturn2(query);

                        response = "Fail";
                        MessageBox.Show("Error API Discount Retail ", "Error API", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                catch (Exception ex)
                {
                    String query = "UPDATE log_msg SET Status='Failed' WHERE Data = 'Discount Retail' ";
                    Crud update = new Crud();
                    update.NonReturn2(query);

                    //response = ex.ToString();
                    MessageBox.Show(ex.ToString());
                    //MessageBox.Show("Make Sure You Are Connected To The Internet","No Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        //=========================================================================================
    }
}
