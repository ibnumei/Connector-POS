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
    class API_Customer
    {
        LinkSwagger ls = new LinkSwagger();
        //=======================================================================================================
        public async Task getCustomer()
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
                    //HttpResponseMessage message = client.GetAsync("http://retailbiensi.azurewebsites.net/api/Customer").Result;
                    //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/Customer").Result;
                    HttpResponseMessage message = client.GetAsync(ls.link+"/api/Customer").Result;

                    string ConnectionString = "Server='" + try_consume_api2.Properties.Settings.Default.mServer + "';Database='" + try_consume_api2.Properties.Settings.Default.mDBName + "';Uid='" + try_consume_api2.Properties.Settings.Default.mUserDB + "';Pwd='" + try_consume_api2.Properties.Settings.Default.mPassDB + "';";
                    StringBuilder sCommand = new StringBuilder("INSERT INTO customer (Id, CustId, Name, CustGroupId, Address, Address2, Address3, Address4, Email, PhoneNumber, StoreId, DefaultCurr) VALUES");

                    if (message.IsSuccessStatusCode)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(List<Customer>));
                        var result = message.Content.ReadAsStringAsync().Result;
                        byte[] byteArray = Encoding.UTF8.GetBytes(result);
                        MemoryStream stream = new MemoryStream(byteArray);
                        List<Customer> resultData = serializer.ReadObject(stream) as List<Customer>;

                        try
                        {
                            using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                            {
                                if(resultData.Count > 0)
                                {
                                    List<string> Rows = new List<string>();
                                    for (int i = 0; i < resultData.Count; i++)
                                    {
                                        Rows.Add(string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')", MySqlHelper.EscapeString(resultData[i].Id.ToString()), MySqlHelper.EscapeString(resultData[i].CustId), MySqlHelper.EscapeString(resultData[i].Name), MySqlHelper.EscapeString(resultData[i].CustGroupId.ToString()), MySqlHelper.EscapeString(resultData[i].Address), MySqlHelper.EscapeString(resultData[i].Address2), MySqlHelper.EscapeString(resultData[i].Address3), MySqlHelper.EscapeString(resultData[i].Address4), MySqlHelper.EscapeString(resultData[i].Email), MySqlHelper.EscapeString(resultData[i].PhoneNumber), MySqlHelper.EscapeString(resultData[i].StoreId.ToString()), MySqlHelper.EscapeString(resultData[i].DefaultCurr)));
                                    }
                                    sCommand.Append(string.Join(",", Rows));
                                    sCommand.Append(";");
                                    mConnection.Open();
                                    using (MySqlCommand myCmd = new MySqlCommand(sCommand.ToString(), mConnection))
                                    {
                                        myCmd.CommandType = CommandType.Text;
                                        myCmd.ExecuteNonQuery();

                                        String query = "UPDATE log_msg SET Status='Success' WHERE Data = 'Discount Customer Store' ";
                                        Crud update = new Crud();
                                        update.NonReturn2(query);

                                        MessageBox.Show("Successful Update Data Customer", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {

                                }
                               
                            }
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.ToString());
                        }
                        
                        /*
                        for (int i = 0; i < resultData.Count; i++)
                        {
                            try
                            {
                                String sql = "INSERT INTO customer (Id, CustId, Name, CustGroupId, Address, Address2, Address3, Address4, Email, PhoneNumber, StoreId, DefaultCurr) VALUES ('" + resultData[i].Id + "','" + resultData[i].CustId + "','" + resultData[i].Name + "','" + resultData[i].CustGroupId + "','" + resultData[i].Address + "','" + resultData[i].Address2 + "','" + resultData[i].Address3 + "','" + resultData[i].Address4 + "','" + resultData[i].Email + "','" + resultData[i].PhoneNumber + "','" + resultData[i].StoreId + "','" + resultData[i].DefaultCurr + "')";
                                Crud input = new Crud();
                                input.NonReturn2(sql);
                            }
                            catch (Exception ex)
                            {
                                //MessageBox.Show(ex.ToString());
                            }
                        }
                        */
                        
                    }
                    else
                    {
                        String query = "UPDATE log_msg SET Status='Failed' WHERE Data = 'Discount Customer Store' ";
                        Crud update = new Crud();
                        update.NonReturn2(query);

                        response = "Fail";
                        MessageBox.Show("Error API Customer","Error API", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                catch (Exception ex)
                {
                    String query = "UPDATE log_msg SET Status='Failed' WHERE Data = 'Discount Customer Store' ";
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
