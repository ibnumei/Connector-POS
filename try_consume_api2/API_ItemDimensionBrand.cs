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
    class API_ItemDimensionBrand
    {
        LinkSwagger ls = new LinkSwagger();
        //=======================================================================================================
        public async Task getItemBrand()
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
                    //HttpResponseMessage message = client.GetAsync("http://retailbiensi.azurewebsites.net/api/DPItemDimensionBrand").Result;
                    //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/DPItemDimensionBrand").Result;
                    HttpResponseMessage message = client.GetAsync(ls.link+"/api/DPItemDimensionBrand").Result;

                    string ConnectionString = "Server='" + try_consume_api2.Properties.Settings.Default.mServer + "';Database='" + try_consume_api2.Properties.Settings.Default.mDBName + "';Uid='" + try_consume_api2.Properties.Settings.Default.mUserDB + "';Pwd='" + try_consume_api2.Properties.Settings.Default.mPassDB + "';";
                    StringBuilder sCommand = new StringBuilder("INSERT INTO itemdimensionbrand (Id, Code, Description) VALUES");

                    if (message.IsSuccessStatusCode)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(List<DPItemDimensionBrand>));
                        var result = message.Content.ReadAsStringAsync().Result;
                        byte[] byteArray = Encoding.UTF8.GetBytes(result);
                        MemoryStream stream = new MemoryStream(byteArray);
                        List<DPItemDimensionBrand> resultData = serializer.ReadObject(stream) as List<DPItemDimensionBrand>;

                        try
                        {
                            using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                            {
                                if(resultData.Count > 0)
                                {
                                    List<string> Rows = new List<string>();
                                    for (int i = 0; i < resultData.Count; i++)
                                    {
                                        Rows.Add(string.Format("('{0}','{1}','{2}')", MySqlHelper.EscapeString(resultData[i].Id.ToString()), MySqlHelper.EscapeString(resultData[i].Code), MySqlHelper.EscapeString(resultData[i].Description)));
                                    }
                                    sCommand.Append(string.Join(",", Rows));
                                    sCommand.Append(";");
                                    mConnection.Open();
                                    using (MySqlCommand myCmd = new MySqlCommand(sCommand.ToString(), mConnection))
                                    {
                                        myCmd.CommandType = CommandType.Text;
                                        myCmd.ExecuteNonQuery();

                                        String query = "UPDATE log_msg SET Status='Success' WHERE Data = 'Discount Item Dimension Brand' ";
                                        Crud update = new Crud();
                                        update.NonReturn2(query);

                                        MessageBox.Show("Successful Update Data Item Dimension Brand", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {

                                }
                            }
                        }
                        catch
                        {

                        }
                        
                        
                    }
                    else
                    {
                        String query = "UPDATE log_msg SET Status='Failed' WHERE Data = 'Discount Item Dimension Brand' ";
                        Crud update = new Crud();
                        update.NonReturn2(query);

                        response = "Fail";
                        MessageBox.Show("Error API Item Dimension Brand", "Error API", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                catch (Exception ex)
                {
                    String query = "UPDATE log_msg SET Status='Failed' WHERE Data = 'Discount Item Dimension Brand' ";
                    Crud update = new Crud();
                    update.NonReturn2(query);

                    response = ex.ToString();
                    MessageBox.Show("Make Sure You Are Connected To The Internet", "No Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        //==============================================================================================================================
    }
}
