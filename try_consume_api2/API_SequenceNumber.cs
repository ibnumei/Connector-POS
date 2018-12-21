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
    class API_SequenceNumber
    {
        LinkSwagger ls = new LinkSwagger();
        String code_store;

        Connection ckon = new Connection();

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
        //==========================GET NUMBER LAST SEQUENCE TRANSACTION=========
        public async Task GetSeqNumTrans()
        {
            String response = "";
            String uri="";
            var credentials = new NetworkCredential("username", "password");
            var handler = new HttpClientHandler { Credentials = credentials };
            using (var client = new HttpClient(handler))
            {
                // Make your request...
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage message = client.GetAsync(ls.link+"/api/AppVersions?app=POS").Result;

                    /*uri = @"http://mpos.biensicore.co.id/api/SequenceNumber?storeCode=AAS&transType=Transaction";
                   HttpResponseMessage message =
                       client.GetAsync(uri).Result;*/


                    //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/StoreMaster?condition=2&paramValue=" + city + "&storeCode=" + store_code).Result;

                    if (message.IsSuccessStatusCode)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(List<api_version>));
                        var result = message.Content.ReadAsStringAsync().Result;
                        byte[] byteArray = Encoding.UTF8.GetBytes(result);
                        MemoryStream stream = new MemoryStream(byteArray);
                        api_version resultData = serializer.ReadObject(stream) as api_version;
                        //List<SequenceNumber> resultData = serializer.ReadObject(stream) as List<SequenceNumber>;
                        //for (int i = 0; i < resultData.Count; i++)
                        //{
                        //    String a = resultData[i].LastNumberSequence;
                        //}
                        String num_seq = resultData.Version;
                        MessageBox.Show(num_seq);
                        //String date = resultData.Date;
                        //String month = date.Substring(5, 2);
                        //if (num_seq != "null")
                        //{
                        //    //MessageBox.Show(num_seq);
                        //    String query = "UPDATE auto_number SET Number= '" + num_seq + "', Month = '" + month + "' WHERE Type_trans= '1'";
                        //    Crud update = new Crud();
                        //    update.NonReturn2(query);
                        //}
                        //else
                        //{
                        //    MessageBox.Show("Data null");
                        //}
                    }
                    else
                    {
                        response = "Fail";
                        MessageBox.Show("Error API Sequence Number Transaction");
                    }

                }
                catch (Exception ex)
                {
                    response = ex.ToString();
                    MessageBox.Show(response);
                }
            }
        }
        //==============================================================================================================================

        //==========================GET NUMBER LAST SEQUENCE REQUEST ORDER=========
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
                    HttpResponseMessage message = client.GetAsync(ls.link+"/api/SequenceNumber?storeCode=" + code_store + "&transType=Request%20Order").Result;

                    if (message.IsSuccessStatusCode)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(List<SequenceNumber>));
                        var result = message.Content.ReadAsStringAsync().Result;
                        byte[] byteArray = Encoding.UTF8.GetBytes(result);
                        MemoryStream stream = new MemoryStream(byteArray);
                        SequenceNumber resultData = serializer.ReadObject(stream) as SequenceNumber;

                        String num_seq = resultData.LastNumberSequence;
                        if (num_seq != "null")
                        {
                            //MessageBox.Show(num_seq);
                            String query = "UPDATE auto_number SET Number= '" + num_seq + "' WHERE Type_trans= '2'";
                            Crud update = new Crud();
                            update.NonReturn2(query);
                        }
                        else
                        {
                            MessageBox.Show("Data null");
                        }
                    }
                    else
                    {
                        response = "Fail";
                        MessageBox.Show("Error API Sequence Number Request Order");
                    }

                }
                catch (Exception ex)
                {
                    response = ex.ToString();
                    MessageBox.Show(response);
                }
            }
        }
        //==============================================================================================================================

        //==========================GET NUMBER LAST SEQUENCE MUTASI ORDER=========
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
                    HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/SequenceNumber?storeCode=" + code_store + "&transType=Mutasi%20Order").Result;

                    if (message.IsSuccessStatusCode)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(List<SequenceNumber>));
                        var result = message.Content.ReadAsStringAsync().Result;
                        byte[] byteArray = Encoding.UTF8.GetBytes(result);
                        MemoryStream stream = new MemoryStream(byteArray);
                        SequenceNumber resultData = serializer.ReadObject(stream) as SequenceNumber;

                        String num_seq = resultData.LastNumberSequence;
                        if (num_seq != "null")
                        {
                            //MessageBox.Show(num_seq);
                            String query = "UPDATE auto_number SET Number= '" + num_seq + "' WHERE Type_trans= '3'";
                            Crud update = new Crud();
                            update.NonReturn2(query);
                        }
                        else
                        {
                            MessageBox.Show("Data null");
                        }
                    }
                    else
                    {
                        response = "Fail";
                        MessageBox.Show("Error API Sequence Number Mutasi Order");
                    }

                }
                catch (Exception ex)
                {
                    response = ex.ToString();
                    MessageBox.Show("Make Sure You Are Connected To The Internet");
                }
            }
        }
        //==============================================================================================================================

        //==========================GET NUMBER LAST SEQUENCE RETURN ORDER=========
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
                    HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/SequenceNumber?storeCode=" + code_store + "&transType=Return%20Order").Result;

                    if (message.IsSuccessStatusCode)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(List<SequenceNumber>));
                        var result = message.Content.ReadAsStringAsync().Result;
                        byte[] byteArray = Encoding.UTF8.GetBytes(result);
                        MemoryStream stream = new MemoryStream(byteArray);
                        SequenceNumber resultData = serializer.ReadObject(stream) as SequenceNumber;

                        String num_seq = resultData.LastNumberSequence;
                        if (num_seq != "null")
                        {
                            //MessageBox.Show(num_seq);
                            String query = "UPDATE auto_number SET Number= '" + num_seq + "' WHERE Type_trans= '4'";
                            Crud update = new Crud();
                            update.NonReturn2(query);
                        }
                        else
                        {
                            MessageBox.Show("Data null");
                        }
                    }
                    else
                    {
                        response = "Fail";
                        MessageBox.Show("Error API Sequence Number Return Order");
                    }

                }
                catch (Exception ex)
                {
                    response = ex.ToString();
                    MessageBox.Show("Make Sure You Are Connected To The Internet");
                }
            }
        }
        //==============================================================================================================================

        //==========================GET NUMBER LAST SEQUENCE PETTY CASH=========
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
                    HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/SequenceNumber?storeCode=" + code_store + "&transType=Petty%20Cash").Result;

                    if (message.IsSuccessStatusCode)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(List<SequenceNumber>));
                        var result = message.Content.ReadAsStringAsync().Result;
                        byte[] byteArray = Encoding.UTF8.GetBytes(result);
                        MemoryStream stream = new MemoryStream(byteArray);
                        SequenceNumber resultData = serializer.ReadObject(stream) as SequenceNumber;

                        String num_seq = resultData.LastNumberSequence;
                        if (num_seq != "null")
                        {
                            //MessageBox.Show(num_seq);
                            String query = "UPDATE auto_number SET Number= '" + num_seq + "' WHERE Type_trans= '6'";
                            Crud update = new Crud();
                            update.NonReturn2(query);
                        }
                        else
                        {
                            MessageBox.Show("Data null");
                        }
                    }
                    else
                    {
                        response = "Fail";
                        MessageBox.Show("Error API Sequence Number Petty Cash");
                    }

                }
                catch (Exception ex)
                {
                    response = ex.ToString();
                    MessageBox.Show("Make Sure You Are Connected To The Internet");
                }
            }
        }
        //==============================================================================================================================

        //==========================GET NUMBER LAST SEQUENCE CLOSING SHIFT=========
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
                    HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/SequenceNumber?storeCode=" + code_store + "&transType=Closing%20Shift").Result;

                    if (message.IsSuccessStatusCode)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(List<SequenceNumber>));
                        var result = message.Content.ReadAsStringAsync().Result;
                        byte[] byteArray = Encoding.UTF8.GetBytes(result);
                        MemoryStream stream = new MemoryStream(byteArray);
                        SequenceNumber resultData = serializer.ReadObject(stream) as SequenceNumber;

                        String num_seq = resultData.LastNumberSequence;
                        if (num_seq != "null")
                        {
                            //MessageBox.Show(num_seq);
                            String query = "UPDATE auto_number SET Number= '" + num_seq + "' WHERE Type_trans= '7'";
                            Crud update = new Crud();
                            update.NonReturn2(query);
                        }
                        else
                        {
                            MessageBox.Show("Data null");
                        }
                    }
                    else
                    {
                        response = "Fail";
                        MessageBox.Show("Error API Sequence Number Closing Shift");
                    }

                }
                catch (Exception ex)
                {
                    response = ex.ToString();
                    MessageBox.Show("Make Sure You Are Connected To The Internet");
                }
            }
        }
        //==============================================================================================================================

        //==========================GET NUMBER LAST SEQUENCE CLOSING STORE=========
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
                    HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/SequenceNumber?storeCode=" + code_store + "&transType=Closing%20Store").Result;

                    if (message.IsSuccessStatusCode)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(List<SequenceNumber>));
                        var result = message.Content.ReadAsStringAsync().Result;
                        byte[] byteArray = Encoding.UTF8.GetBytes(result);
                        MemoryStream stream = new MemoryStream(byteArray);
                        SequenceNumber resultData = serializer.ReadObject(stream) as SequenceNumber;

                        String num_seq = resultData.LastNumberSequence;
                        if (num_seq != "null")
                        {
                            //MessageBox.Show(num_seq);
                            String query = "UPDATE auto_number SET Number= '" + num_seq + "' WHERE Type_trans= '8'";
                            Crud update = new Crud();
                            update.NonReturn2(query);
                        }
                        else
                        {
                            MessageBox.Show("Data null");
                        }
                    }
                    else
                    {
                        response = "Fail";
                        MessageBox.Show("Error API Sequence Number Closing Store");
                    }

                }
                catch (Exception ex)
                {
                    response = ex.ToString();
                    MessageBox.Show("Make Sure You Are Connected To The Internet");
                }
            }
        }
        //==============================================================================================================================
    }
}
