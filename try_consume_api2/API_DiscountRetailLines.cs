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
    class API_DiscountRetailLines
    {

        String id_diskon, status, store_code;
        Connection ckon = new Connection();
        LinkSwagger ls = new LinkSwagger();
        public void cek()
        {
            ckon.con.Close();
            String sql_storeCode = "Select * from store";
            ckon.cmd = new MySqlCommand(sql_storeCode, ckon.con);
            ckon.con.Open();
            ckon.myReader = ckon.cmd.ExecuteReader();
            while(ckon.myReader.Read())
            {
                store_code = ckon.myReader.GetString("CODE");
            }
            ckon.con.Close();

            ckon.con.Close();
            String sql_cek = "Select * from log_msg where Data = 'Discount Retail'";
            ckon.cmd = new MySqlCommand(sql_cek, ckon.con);
            ckon.con.Open();
            ckon.myReader = ckon.cmd.ExecuteReader();
            while (ckon.myReader.Read())
            {
                status = ckon.myReader.GetString("Status");
            }
            ckon.con.Close();
            if (status == "Success")
            {
                get_DiscountRetailLines().Wait();
            }
            else
            {
                String query = "UPDATE log_msg SET Status='Failed' WHERE Data = 'Discount Retail Lines' ";
                Crud update = new Crud();
                update.NonReturn2(query);

                MessageBox.Show("Please Get Retail Discount Data First", "Warning Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //=======================================================================================================

        public async Task get_DiscountRetailLines()
        {
            int count_id = 0; int count_sukses = 0, count_noConnection = 0;
            ckon.con.Close();
            String sql = "Select * from discountretail";
            ckon.cmd = new MySqlCommand(sql, ckon.con);
            ckon.con.Open();
            ckon.myReader = ckon.cmd.ExecuteReader();
            while (ckon.myReader.Read())
            {
                count_id = count_id + 1;
                id_diskon = ckon.myReader.GetString("Id");
                //********************************************************************************************************
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
                        //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/DiscountRetailLinesApi").Result;
                        //HttpResponseMessage message = client.GetAsync(ls.link + "/api/DiscountRetailStoreLine?discountID=" + id_diskon).Result;
                        HttpResponseMessage message = client.GetAsync(ls.link + "/api/NewDiscountRetailLinesAPI?discountID=" + id_diskon+"&storecode="+store_code).Result;

                        string ConnectionString = "Server='" + try_consume_api2.Properties.Settings.Default.mServer + "';Database='" + try_consume_api2.Properties.Settings.Default.mDBName + "';Uid='" + try_consume_api2.Properties.Settings.Default.mUserDB + "';Pwd='" + try_consume_api2.Properties.Settings.Default.mPassDB + "';";
                        StringBuilder sCommand = new StringBuilder("INSERT INTO discountretaillines (Id, BrandCode, Department, DepartmentType,Gender, ArticleId, Color, Size, DiscountRetailId, DiscountPrecentage, CashDiscount, DiscountPrice, Qty, AmountTransaction, ArticleIdDiscount) VALUES");

                        if (message.IsSuccessStatusCode)
                        {
                            var serializer = new DataContractJsonSerializer(typeof(List<DiscountRetailLinesApi>));
                            var result = message.Content.ReadAsStringAsync().Result;
                            byte[] byteArray = Encoding.UTF8.GetBytes(result);
                            MemoryStream stream = new MemoryStream(byteArray);
                            List<DiscountRetailLinesApi> resultData = serializer.ReadObject(stream) as List<DiscountRetailLinesApi>;

                            using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                            {
                                //** CEK APAKAH RESULTDATA MEMPUNYAI ISI LEBIH DARI 0, UNTUK CEK RETAIL LINES ADA ISINYA ATAU TIDAK**
                                if(resultData.Count > 0)
                                {
                                    List<string> Rows = new List<string>();
                                    for (int i = 0; i < resultData.Count; i++)
                                    {
                                        Rows.Add(string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')", MySqlHelper.EscapeString(resultData[i].Id.ToString()), MySqlHelper.EscapeString(resultData[i].BrandCode.ToString()), MySqlHelper.EscapeString(resultData[i].Department.ToString()), MySqlHelper.EscapeString(resultData[i].DepartmentType.ToString()), MySqlHelper.EscapeString(resultData[i].Gender.ToString()), MySqlHelper.EscapeString(resultData[i].ArticleId.ToString()), MySqlHelper.EscapeString(resultData[i].Color.ToString()), MySqlHelper.EscapeString(resultData[i].Size.ToString()), MySqlHelper.EscapeString(resultData[i].DiscountRetailId.ToString()), MySqlHelper.EscapeString(resultData[i].DiscountPrecentage.ToString()), MySqlHelper.EscapeString(resultData[i].CashDiscount.ToString()), MySqlHelper.EscapeString(resultData[i].DiscountPrice.ToString()), MySqlHelper.EscapeString(resultData[i].Qty.ToString()), MySqlHelper.EscapeString(resultData[i].AmountTransaction.ToString()), MySqlHelper.EscapeString(resultData[i].ArticleIdDiscount.ToString())));
                                    }
                                    sCommand.Append(string.Join(",", Rows));
                                    sCommand.Append(";");
                                    mConnection.Open();
                                    using (MySqlCommand myCmd = new MySqlCommand(sCommand.ToString(), mConnection))
                                    {
                                        myCmd.CommandType = CommandType.Text;
                                        myCmd.ExecuteNonQuery();

                                        /*
                                        String query = "UPDATE log_msg SET Status='Success' WHERE Data = 'Discount Retail Lines' ";
                                        Crud update = new Crud();
                                        update.NonReturn2(query);
                                        MessageBox.Show("Successful Update Data Discount Retail Lines", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        */
                                        count_sukses = count_sukses + 1;
                                        //MessageBox.Show("Sukses"+count_sukses.ToString());
                                    }
                                }
                                else
                                {
                                    count_sukses = count_sukses + 1;
                                }
                                

                            }

                        }
                        else
                        {
                            /*
                            String query = "UPDATE log_msg SET Status='Failed' WHERE Data = 'Discount Retail Lines' ";
                            Crud update = new Crud();
                            update.NonReturn2(query);
                            
                            response = "Fail";
                            */
                            //MessageBox.Show(response, "Error API", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            
                            count_noConnection = count_noConnection + 1;
                        }

                    }
                    catch (Exception ex)
                    {
                        /*
                        String query = "UPDATE log_msg SET Status='Failed' WHERE Data = 'Discount Retail Lines' ";
                        Crud update = new Crud();
                        update.NonReturn2(query);
                        */
                        //MessageBox.Show(ex.ToString(), "No Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        count_noConnection = count_noConnection + 1;
                        //MessageBox.Show("Gagal di id = " + id_diskon);
                        //MessageBox.Show(ex.ToString(), "No Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                //*****************************END OF USING ****************************************************
            }
            //---------------------------------END OF WHILE ----------------------------------------------------
            ckon.con.Close();
            if (count_noConnection > 0)
            {
                String query = "UPDATE log_msg SET Status='Failed' WHERE Data = 'Discount Retail Lines' ";
                Crud update = new Crud();
                update.NonReturn2(query);

                MessageBox.Show("Make Sure You Are Connected To The Internet", "No Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                    String query = "UPDATE log_msg SET Status='Success' WHERE Data = 'Discount Retail Lines' ";
                    Crud update = new Crud();
                    update.NonReturn2(query);
                    MessageBox.Show("Successful Update Data Discount Retail Lines", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //===================================END OF ASYNC TASK===================================
        public async Task get_DiscountRetailLines2()
        {
            //********************************************************************************************************
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
                    //HttpResponseMessage message = client.GetAsync("http://retailbiensi.azurewebsites.net/api/DiscountRetailLinesApi").Result;
                    //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/DiscountRetailLinesApi").Result;
                    HttpResponseMessage message = client.GetAsync(ls.link + "/api/DiscountRetailStoreLine?discountID=2094").Result;

                    string ConnectionString = "Server='" + try_consume_api2.Properties.Settings.Default.mServer + "';Database='" + try_consume_api2.Properties.Settings.Default.mDBName + "';Uid='" + try_consume_api2.Properties.Settings.Default.mUserDB + "';Pwd='" + try_consume_api2.Properties.Settings.Default.mPassDB + "';";
                    StringBuilder sCommand = new StringBuilder("INSERT INTO discountretaillines (Id, BrandCode, Department, DepartmentType,Gender, ArticleId, Color, Size, DiscountRetailId, DiscountPrecentage, CashDiscount, DiscountPrice, Qty, AmountTransaction, ArticleIdDiscount) VALUES");

                    if (message.IsSuccessStatusCode)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(List<DiscountRetailLinesApi>));
                        var result = message.Content.ReadAsStringAsync().Result;
                        byte[] byteArray = Encoding.UTF8.GetBytes(result);
                        MemoryStream stream = new MemoryStream(byteArray);
                        List<DiscountRetailLinesApi> resultData = serializer.ReadObject(stream) as List<DiscountRetailLinesApi>;

                        using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                        {
                            if(resultData.Count > 0)
                            {
                                List<string> Rows = new List<string>();
                                for (int i = 0; i < resultData.Count; i++)
                                {
                                    Rows.Add(string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')", MySqlHelper.EscapeString(resultData[i].Id.ToString()), MySqlHelper.EscapeString(resultData[i].BrandCode.ToString()), MySqlHelper.EscapeString(resultData[i].Department.ToString()), MySqlHelper.EscapeString(resultData[i].DepartmentType.ToString()), MySqlHelper.EscapeString(resultData[i].Gender.ToString()), MySqlHelper.EscapeString(resultData[i].ArticleId.ToString()), MySqlHelper.EscapeString(resultData[i].Color.ToString()), MySqlHelper.EscapeString(resultData[i].Size.ToString()), MySqlHelper.EscapeString(resultData[i].DiscountRetailId.ToString()), MySqlHelper.EscapeString(resultData[i].DiscountPrecentage.ToString()), MySqlHelper.EscapeString(resultData[i].CashDiscount.ToString()), MySqlHelper.EscapeString(resultData[i].DiscountPrice.ToString()), MySqlHelper.EscapeString(resultData[i].Qty.ToString()), MySqlHelper.EscapeString(resultData[i].AmountTransaction.ToString()), MySqlHelper.EscapeString(resultData[i].ArticleIdDiscount.ToString())));
                                }
                                sCommand.Append(string.Join(",", Rows));
                                sCommand.Append(";");
                                mConnection.Open();
                                using (MySqlCommand myCmd = new MySqlCommand(sCommand.ToString(), mConnection))
                                {
                                    myCmd.CommandType = CommandType.Text;
                                    myCmd.ExecuteNonQuery();

                                    /*
                                    String query = "UPDATE log_msg SET Status='Success' WHERE Data = 'Discount Retail Lines' ";
                                    Crud update = new Crud();
                                    update.NonReturn2(query);
                                    MessageBox.Show("Successful Update Data Discount Retail Lines", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    */
                                    //MessageBox.Show("Sukses"+count_sukses.ToString());
                                }

                            }
                            else
                            {

                            }
                            
                        }

                    }
                    else
                    {
                        /*
                        String query = "UPDATE log_msg SET Status='Failed' WHERE Data = 'Discount Retail Lines' ";
                        Crud update = new Crud();
                        update.NonReturn2(query);
                        */
                        response = "Fail";
                        MessageBox.Show(response, "Error API", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        
                    }

                }
                catch (Exception ex)
                {
                    /*
                    String query = "UPDATE log_msg SET Status='Failed' WHERE Data = 'Discount Retail Lines' ";
                    Crud update = new Crud();
                    update.NonReturn2(query);
                    */
                    //MessageBox.Show(ex.ToString(), "No Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show(ex.ToString());
                }
            }
            //*****************************END OF USING ****************************************************


        }
    }
}
