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
    class API_SeqNum2
    {
        Connection ckon = new Connection();
        LinkSwagger ls = new LinkSwagger();
        String  code_store;
        public void get_cust_id()
        {
            ckon.con.Close();
            String sql = "SELECT * FROM store";
            ckon.cmd = new MySqlCommand(sql, ckon.con);
            ckon.con.Open();
            ckon.myReader = ckon.cmd.ExecuteReader();
            while (ckon.myReader.Read())
            {
                code_store = ckon.myReader.GetString("CODE");
            }
            ckon.con.Close();
        }
        public async Task GetSeqNumTrans()
        {
            String response = "";
            var credentials = new NetworkCredential("username", "password");
            var handler = new HttpClientHandler { Credentials = credentials };
            using (var client = new HttpClient(handler))
            {
                // Make your request...
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/AppVersions?app=conn").Result;
                    //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/SequenceNumber?storeCode="+code_store+"&transType=transaction").Result;
                    HttpResponseMessage message = client.GetAsync(ls.link+"/api/SequenceNumber?storeCode=" + code_store + "&transType=transaction").Result;
                    try
                    {
                        if (message.IsSuccessStatusCode)
                        {
                            var serializer = new DataContractJsonSerializer(typeof(SequenceNumber));
                            var result = message.Content.ReadAsStringAsync().Result;
                            byte[] byteArray = Encoding.UTF8.GetBytes(result);
                            MemoryStream stream = new MemoryStream(byteArray);
                            SequenceNumber resultData = serializer.ReadObject(stream) as SequenceNumber;
                            //==masukan daat ke dalam variable
                            String num_seq = resultData.LastNumberSequence;
                            String date = resultData.Date;
                            String month = date.Substring(0,2);

                            //MessageBox.Show(month);
                            if (num_seq != "null")
                            {
                                try
                                {
                                    String query = "UPDATE auto_number SET Number= '" + num_seq + "', Month = '" + month + "' WHERE Type_trans= '1'";
                                    Crud update = new Crud();
                                    update.NonReturn2(query);
                                }
                                catch
                                {
                                    String query = "INSERT into auto_number (Store_Code, Month, Number, Type_trans) values ('"+ code_store +"','"+month+"','"+num_seq+"','1')";
                                    Crud update = new Crud();
                                    update.NonReturn2(query);
                                }
                                //String query2 = "UPDATE log_msg SET Status='Success' WHERE Data = 'Seq Number Transaction' ";
                                //Crud update2 = new Crud();
                                //update2.NonReturn2(query2);
                                //MessageBox.Show("Successful Update Data Seq Number Transaction");
                            }
                            else
                            {
                                //MessageBox.Show("Data null");
                            }

                        }
                        else
                        {
                            response = "Fail";
                        }
                    }
                    catch (Exception ex)
                    {
                       //MessageBox.Show("pertama, "+ex.ToString());
                    }

                }
                catch (Exception ex)
                {
                    response = ex.ToString();
                    //MessageBox.Show("kedua, "+ex.ToString());
                }
            }
        }
        //=========================================================================================
        public async Task GetSeqNumReqOrder()
        {
            String response = "";
            var credentials = new NetworkCredential("username", "password");
            var handler = new HttpClientHandler { Credentials = credentials };
            using (var client = new HttpClient(handler))
            {
                // Make your request...
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/AppVersions?app=conn").Result;
                    //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/SequenceNumber?storeCode=" + code_store + "&transType=request order").Result;
                    HttpResponseMessage message = client.GetAsync(ls.link+"/api/SequenceNumber?storeCode=" + code_store + "&transType=request order").Result;
                    try
                    {
                        if (message.IsSuccessStatusCode)
                        {
                            var serializer = new DataContractJsonSerializer(typeof(SequenceNumber));
                            var result = message.Content.ReadAsStringAsync().Result;
                            byte[] byteArray = Encoding.UTF8.GetBytes(result);
                            MemoryStream stream = new MemoryStream(byteArray);
                            SequenceNumber resultData = serializer.ReadObject(stream) as SequenceNumber;
                            //==masukan daat ke dalam variable
                            String num_seq = resultData.LastNumberSequence;
                            String date = resultData.Date;
                            String month = date.Substring(0, 2);

                            //MessageBox.Show(month);
                            if (num_seq != "null")
                            {
                                try
                                {
                                    //MessageBox.Show(num_seq);
                                    String query = "UPDATE auto_number SET Number= '" + num_seq + "', Month = '" + month + "' WHERE Type_trans= '2'";
                                    Crud update = new Crud();
                                    update.NonReturn2(query);
                                }
                                catch
                                {
                                    String query = "INSERT into auto_number (Store_Code, Month, Number, Type_trans) values ('" + code_store + "','" + month + "','" + num_seq + "','2')";
                                    Crud update = new Crud();
                                    update.NonReturn2(query);
                                }
                                //String query2 = "UPDATE log_msg SET Status='Success' WHERE Data = 'Seq Number Request Order' ";
                                //Crud update2 = new Crud();
                                //update2.NonReturn2(query2);
                                //MessageBox.Show("Successful Update Data Seq Number Request Order");
                            }
                            else
                            {
                                MessageBox.Show("Data null");
                            }

                        }
                        else
                        {
                            response = "Fail";
                        }
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("pertama, "+ex.ToString());
                    }

                }
                catch (Exception ex)
                {
                    response = ex.ToString();
                    //MessageBox.Show("kedua, "+ex.ToString());
                }
            }
        }
        //=========================================================================================
        public async Task GetSeqNumMutOrder()
        {
            String response = "";
            var credentials = new NetworkCredential("username", "password");
            var handler = new HttpClientHandler { Credentials = credentials };
            using (var client = new HttpClient(handler))
            {
                // Make your request...
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/AppVersions?app=conn").Result;
                    //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/SequenceNumber?storeCode=" + code_store + "&transType=mutasi order").Result;
                    HttpResponseMessage message = client.GetAsync(ls.link+"/api/SequenceNumber?storeCode=" + code_store + "&transType=mutasi order").Result;
                    try
                    {
                        if (message.IsSuccessStatusCode)
                        {
                            var serializer = new DataContractJsonSerializer(typeof(SequenceNumber));
                            var result = message.Content.ReadAsStringAsync().Result;
                            byte[] byteArray = Encoding.UTF8.GetBytes(result);
                            MemoryStream stream = new MemoryStream(byteArray);
                            SequenceNumber resultData = serializer.ReadObject(stream) as SequenceNumber;
                            //==masukan daat ke dalam variable
                            String num_seq = resultData.LastNumberSequence;
                            String date = resultData.Date;
                            String month = date.Substring(0, 2);

                            //MessageBox.Show(month);
                            if (num_seq != "null")
                            {
                                try
                                {
                                    String query = "UPDATE auto_number SET Number= '" + num_seq + "', Month = '" + month + "' WHERE Type_trans= '3'";
                                    Crud update = new Crud();
                                    update.NonReturn2(query);
                                }
                                catch
                                {
                                    String query = "INSERT into auto_number (Store_Code, Month, Number, Type_trans) values ('" + code_store + "','" + month + "','" + num_seq + "','3')";
                                    Crud update = new Crud();
                                    update.NonReturn2(query);
                                }
                                //MessageBox.Show(num_seq);

                                //String query2 = "UPDATE log_msg SET Status='Success' WHERE Data = 'Seq Number Mutasi Order' ";
                                //Crud update2 = new Crud();
                                //update2.NonReturn2(query2);
                                //MessageBox.Show("Successful Update Data Seq Number Mutasi Order");

                            }
                            else
                            {
                                MessageBox.Show("Data null");
                            }

                        }
                        else
                        {
                            response = "Fail";
                        }
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("pertama, "+ex.ToString());
                    }

                }
                catch (Exception ex)
                {
                    response = ex.ToString();
                    //MessageBox.Show("kedua, "+ex.ToString());
                }
            }
        }
        //=========================================================================================
        public async Task GetSeqNumRetOrder()
        {
            String response = "";
            var credentials = new NetworkCredential("username", "password");
            var handler = new HttpClientHandler { Credentials = credentials };
            using (var client = new HttpClient(handler))
            {
                // Make your request...
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/AppVersions?app=conn").Result;
                    //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/SequenceNumber?storeCode=" + code_store + "&transType=return order").Result;
                    HttpResponseMessage message = client.GetAsync(ls.link+"/api/SequenceNumber?storeCode=" + code_store + "&transType=return order").Result;
                    try
                    {
                        if (message.IsSuccessStatusCode)
                        {
                            var serializer = new DataContractJsonSerializer(typeof(SequenceNumber));
                            var result = message.Content.ReadAsStringAsync().Result;
                            byte[] byteArray = Encoding.UTF8.GetBytes(result);
                            MemoryStream stream = new MemoryStream(byteArray);
                            SequenceNumber resultData = serializer.ReadObject(stream) as SequenceNumber;
                            //==masukan daat ke dalam variable
                            String num_seq = resultData.LastNumberSequence;
                            String date = resultData.Date;
                            String month = date.Substring(0, 2);

                            //MessageBox.Show(month);
                            if (num_seq != "null")
                            {
                                try
                                {
                                    String query = "UPDATE auto_number SET Number= '" + num_seq + "', Month = '" + month + "' WHERE Type_trans= '4'";
                                    Crud update = new Crud();
                                    update.NonReturn2(query);
                                }
                                catch
                                {
                                    String query = "INSERT into auto_number (Store_Code, Month, Number, Type_trans) values ('" + code_store + "','" + month + "','" + num_seq + "','4')";
                                    Crud update = new Crud();
                                    update.NonReturn2(query);
                                }
                                //MessageBox.Show(num_seq);
                                //String query2 = "UPDATE log_msg SET Status='Success' WHERE Data = 'Seq Number Return Order' ";
                                //Crud update2 = new Crud();
                                //update2.NonReturn2(query2);
                                //MessageBox.Show("Successful Update Data Seq Number Return Order");

                            }
                            else
                            {
                                MessageBox.Show("Data null");
                            }

                        }
                        else
                        {
                            response = "Fail";
                        }
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("pertama, "+ex.ToString());
                    }

                }
                catch (Exception ex)
                {
                    response = ex.ToString();
                    //MessageBox.Show("kedua, "+ex.ToString());
                }
            }
        }
        //=========================================================================================
        public async Task GetSeqNumPetCash()
        {
            String response = "";
            var credentials = new NetworkCredential("username", "password");
            var handler = new HttpClientHandler { Credentials = credentials };
            using (var client = new HttpClient(handler))
            {
                // Make your request...
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/AppVersions?app=conn").Result;
                    //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/SequenceNumber?storeCode=" + code_store + "&transType=petty cash").Result;
                    HttpResponseMessage message = client.GetAsync(ls.link+"/api/SequenceNumber?storeCode=" + code_store + "&transType=petty cash").Result;
                    try
                    {
                        if (message.IsSuccessStatusCode)
                        {
                            var serializer = new DataContractJsonSerializer(typeof(SequenceNumber));
                            var result = message.Content.ReadAsStringAsync().Result;
                            byte[] byteArray = Encoding.UTF8.GetBytes(result);
                            MemoryStream stream = new MemoryStream(byteArray);
                            SequenceNumber resultData = serializer.ReadObject(stream) as SequenceNumber;
                            //==masukan daat ke dalam variable
                            String num_seq = resultData.LastNumberSequence;
                            String date = resultData.Date;
                            String month = date.Substring(0, 2);

                            //MessageBox.Show(month);
                            if (num_seq != "null")
                            {
                                try
                                {
                                    String query = "UPDATE auto_number SET Number= '" + num_seq + "', Month = '" + month + "' WHERE Type_trans= '6'";
                                    Crud update = new Crud();
                                    update.NonReturn2(query);
                                }
                                catch
                                {
                                    String query = "INSERT into auto_number (Store_Code, Month, Number, Type_trans) values ('" + code_store + "','" + month + "','" + num_seq + "','6')";
                                    Crud update = new Crud();
                                    update.NonReturn2(query);
                                }
                                //MessageBox.Show(num_seq);
                                //String query2 = "UPDATE log_msg SET Status='Success' WHERE Data = 'Seq Number Petty Cash' ";
                                //Crud update2 = new Crud();
                                //update2.NonReturn2(query2);
                                //MessageBox.Show("Successful Update Data Seq Number Petty Cash");
                            }
                            else
                            {
                                MessageBox.Show("Data null");
                            }

                        }
                        else
                        {
                            response = "Fail";
                        }
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("pertama, "+ex.ToString());
                    }

                }
                catch (Exception ex)
                {
                    response = ex.ToString();
                    //MessageBox.Show("kedua, "+ex.ToString());
                }
            }
        }
        //=========================================================================================
        public async Task GetSeqNumClosingShift()
        {
            String response = "";
            var credentials = new NetworkCredential("username", "password");
            var handler = new HttpClientHandler { Credentials = credentials };
            using (var client = new HttpClient(handler))
            {
                // Make your request...
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/AppVersions?app=conn").Result;
                    //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/SequenceNumber?storeCode=" + code_store + "&transType=closing shift").Result;
                    HttpResponseMessage message = client.GetAsync(ls.link+"/api/SequenceNumber?storeCode=" + code_store + "&transType=closing shift").Result;
                    try
                    {
                        if (message.IsSuccessStatusCode)
                        {
                            var serializer = new DataContractJsonSerializer(typeof(SequenceNumber));
                            var result = message.Content.ReadAsStringAsync().Result;
                            byte[] byteArray = Encoding.UTF8.GetBytes(result);
                            MemoryStream stream = new MemoryStream(byteArray);
                            SequenceNumber resultData = serializer.ReadObject(stream) as SequenceNumber;
                            //==masukan daat ke dalam variable
                            String num_seq = resultData.LastNumberSequence;
                            String date = resultData.Date;
                            String month = date.Substring(0, 2);

                            //MessageBox.Show(month);
                            if (num_seq != "null")
                            {
                                try
                                {
                                    String query = "UPDATE auto_number SET Number= '" + num_seq + "', Month = '" + month + "' WHERE Type_trans= '7'";
                                    Crud update = new Crud();
                                    update.NonReturn2(query);
                                }
                                catch
                                {
                                    String query = "INSERT into auto_number (Store_Code, Month, Number, Type_trans) values ('" + code_store + "','" + month + "','" + num_seq + "','7')";
                                    Crud update = new Crud();
                                    update.NonReturn2(query);
                                }
                                //MessageBox.Show(num_seq);
                                //String query2 = "UPDATE log_msg SET Status='Success' WHERE Data = 'Seq Number Closing Shift' ";
                                //Crud update2 = new Crud();
                                //update2.NonReturn2(query2);
                                //MessageBox.Show("Successful Update Data Seq Number Closing Shift");
                            }
                            else
                            {
                                MessageBox.Show("Data null");
                            }

                        }
                        else
                        {
                            response = "Fail";
                        }
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("pertama, "+ex.ToString());
                    }

                }
                catch (Exception ex)
                {
                    response = ex.ToString();
                    //MessageBox.Show("kedua, "+ex.ToString());
                }
            }
        }
        //=========================================================================================
        public async Task GetSeqNumClosingStore()
        {
            String response = "";
            var credentials = new NetworkCredential("username", "password");
            var handler = new HttpClientHandler { Credentials = credentials };
            using (var client = new HttpClient(handler))
            {
                // Make your request...
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/AppVersions?app=conn").Result;
                    //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/SequenceNumber?storeCode=" + code_store + "&transType=closing store").Result;
                    HttpResponseMessage message = client.GetAsync(ls.link+"/api/SequenceNumber?storeCode=" + code_store + "&transType=closing store").Result;
                    try
                    {
                        if (message.IsSuccessStatusCode)
                        {
                            var serializer = new DataContractJsonSerializer(typeof(SequenceNumber));
                            var result = message.Content.ReadAsStringAsync().Result;
                            byte[] byteArray = Encoding.UTF8.GetBytes(result);
                            MemoryStream stream = new MemoryStream(byteArray);
                            SequenceNumber resultData = serializer.ReadObject(stream) as SequenceNumber;
                            //==masukan daat ke dalam variable
                            String num_seq = resultData.LastNumberSequence;
                            String date = resultData.Date;
                            String month = date.Substring(0, 2);

                            //MessageBox.Show(month);
                            if (num_seq != "null")
                            {
                                try
                                {
                                    String query = "UPDATE auto_number SET Number= '" + num_seq + "', Month = '" + month + "' WHERE Type_trans= '8'";
                                    Crud update = new Crud();
                                    update.NonReturn2(query);
                                }
                                catch
                                {
                                    String query = "INSERT into auto_number (Store_Code, Month, Number, Type_trans) values ('" + code_store + "','" + month + "','" + num_seq + "','8')";
                                    Crud update = new Crud();
                                    update.NonReturn2(query);
                                }
                                //MessageBox.Show(num_seq);
                                //String query2 = "UPDATE log_msgl SET Status='Success' WHERE Data = 'Seq Number Closing Store' ";
                                //Crud update2 = new Crud();
                                //update2.NonReturn2(query2);
                                //MessageBox.Show("Successful Update Data Seq Number Closing Store");
                            }
                            else
                            {
                                MessageBox.Show("Data null");
                            }

                        }
                        else
                        {
                            response = "Fail";
                        }
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("pertama, "+ex.ToString());
                    }

                }
                catch (Exception ex)
                {
                    response = ex.ToString();
                    //MessageBox.Show("kedua, "+ex.ToString());
                }
            }
        }
        //=========================================================================================
    }
}
