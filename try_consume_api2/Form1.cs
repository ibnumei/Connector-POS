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
    public partial class Form1 : Form
    {
        String s_id, s_artcileid, s_articlename, s_brand, s_gender, s_department, s_departmenttype, s_size, s_color, s_unit, s_price, cust_id, cust_id_Store, string_count;

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Visible = false;
        }

        Connection ckon = new Connection();
        Connection2 ckon2 = new Connection2();
        //=============================VARIABLE FOR DELETE ARTICLE===============================
        String del_id_art;
        public Form1()
        {
            InitializeComponent();
        }

        private void b_reload_Click(object sender, EventArgs e)
        {
            API_Article art = new API_Article();
            art.delete();
            art.get_cust_id();
            art.getArticle().Wait();

            //textBox1.Visible = true;
            //delete();
            //get_cust_id();
            //getArticle().Wait();
        }
        //======================================DELETE DATA BEFORE GET FROM API==================================
        public void delete()
        {
            ckon.con.Close();
            String sql = "DELETE FROM article";
            Crud INPUT = new Crud();
            INPUT.NonReturn2(sql);
        }
        //================GET CUSTOMER ID STORE
        public void get_cust_id()
        {
            ckon.con.Close();
            String sql = "SELECT * FROM store";
            ckon.cmd = new MySqlCommand(sql, ckon.con);
            ckon.con.Open();
            ckon.myReader = ckon.cmd.ExecuteReader();
            while (ckon.myReader.Read())
            {
                cust_id_Store = ckon.myReader.GetString("CUST_ID_STORE");
            }
            ckon.con.Close();
        }
        //=======================================================================================================

        /*
        public async Task getArticle()
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
                    //HttpResponseMessage message = client.GetAsync("http://retailbiensi.azurewebsites.net/api/Article?customerCode=" + cust_id_Store).Result;
                    HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/Article?customerCode=" + cust_id_Store).Result;

                    if (message.IsSuccessStatusCode)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(List<Article>));
                        var result = message.Content.ReadAsStringAsync().Result;
                        byte[] byteArray = Encoding.UTF8.GetBytes(result);
                        MemoryStream stream = new MemoryStream(byteArray);
                        List<Article> resultData = serializer.ReadObject(stream) as List<Article>;
                        int count = 0;
                        int a = resultData.Count();
                        //MessageBox.Show(a.ToString());
                        string_count = a.ToString();
                        for (int i = 0; i < resultData.Count; i++)
                        {
                            try
                            {
                                String sql = "INSERT INTO article (_id ,ARTICLE_ID, ARTICLE_NAME, BRAND, GENDER, DEPARTMENT, DEPARTMENT_TYPE, SIZE, COLOR, UNIT, PRICE, ARTICLE_ID_ALIAS) VALUES ('" + resultData[i].id + "','" + resultData[i].articleId + "', '" + resultData[i].articleName + "', '" + resultData[i].brand + "', '" + resultData[i].gender + "', '" + resultData[i].department + "', '" + resultData[i].departmentType + "', '" + resultData[i].size + "', '" + resultData[i].color + "', '" + resultData[i].unit + "', '" + resultData[i].price + "', '" + resultData[i].articleIdAlias + "')";
                                Crud input = new Crud();
                                input.NonReturn2(sql);
                                count = count + 1;
                                //label1.Text = count.ToString();
                                //label1.Text = string.Format("Processing...{0}/{1}", count,string_count);
                                // MessageBox.Show("Jumlah Data = "+ count.ToString());
                                textBox1.AppendText("Data Article - " + count.ToString() + "/ "+string_count+"\n");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }
                        String query = "UPDATE log_msg SET Status='Success' WHERE Data = 'Article' ";
                        Crud update = new Crud();
                        update.NonReturn2(query);
                        MessageBox.Show("Successful Update Data Article");
                    }
                    else
                    {
                        response = "Fail";
                        MessageBox.Show("Error API Article");
                    }

                }
                catch (Exception ex)
                {
                    String query = "UPDATE log_msg SET Status='Failed' WHERE Data = 'Article' ";
                    Crud update = new Crud();
                    update.NonReturn2(query);

                    response = ex.ToString();
                    MessageBox.Show("Make Sure You Are Connected To The Internet");
                }
            }
        }
        */
        //===============================================================================================================

        public async Task getArticle()
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
                    //HttpResponseMessage message = client.GetAsync("http://retailbiensi.azurewebsites.net/api/Article?customerCode=" + cust_id_Store).Result;
                    HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/Article?customerCode=" + cust_id_Store).Result;

                    string ConnectionString = "Server='localhost';Database='biensi_pos_db';Uid='root';Pwd='ibnumei'";
                    StringBuilder sCommand = new StringBuilder("INSERT INTO article (_id ,ARTICLE_ID, ARTICLE_NAME, BRAND, GENDER, DEPARTMENT, DEPARTMENT_TYPE, SIZE, COLOR, UNIT, PRICE, ARTICLE_ID_ALIAS) VALUES");

                    if (message.IsSuccessStatusCode)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(List<Article>));
                        var result = message.Content.ReadAsStringAsync().Result;
                        byte[] byteArray = Encoding.UTF8.GetBytes(result);
                        MemoryStream stream = new MemoryStream(byteArray);
                        List<Article> resultData = serializer.ReadObject(stream) as List<Article>;

                        using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                        {

                            List<string> Rows = new List<string>();
                            for (int i = 0; i < resultData.Count; i++)
                            {
                                Rows.Add(string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')", MySqlHelper.EscapeString(resultData[i].id.ToString()), MySqlHelper.EscapeString(resultData[i].articleId), MySqlHelper.EscapeString(resultData[i].articleName), MySqlHelper.EscapeString(resultData[i].brand), MySqlHelper.EscapeString(resultData[i].gender), MySqlHelper.EscapeString(resultData[i].department), MySqlHelper.EscapeString(resultData[i].departmentType), MySqlHelper.EscapeString(resultData[i].size), MySqlHelper.EscapeString(resultData[i].color), MySqlHelper.EscapeString(resultData[i].unit), MySqlHelper.EscapeString(resultData[i].price.ToString()), MySqlHelper.EscapeString(resultData[i].articleIdAlias)));
                            }
                            sCommand.Append(string.Join(",", Rows));
                            sCommand.Append(";");
                            mConnection.Open();
                            using (MySqlCommand myCmd = new MySqlCommand(sCommand.ToString(), mConnection))
                            {
                                myCmd.CommandType = CommandType.Text;
                                myCmd.ExecuteNonQuery();
                            }
                        }

                        String query = "UPDATE log_msg SET Status='Success' WHERE Data = 'Article' ";
                        Crud update = new Crud();
                        update.NonReturn2(query);
                        MessageBox.Show("Successful Update Data Article");
                    }
                    else
                    {
                        response = "Fail";
                        MessageBox.Show("Error API Article");
                    }

                }
                catch (Exception ex)
                {
                    String query = "UPDATE log_msg SET Status='Failed' WHERE Data = 'Article' ";
                    Crud update = new Crud();
                    update.NonReturn2(query);

                    response = ex.ToString();
                    MessageBox.Show("Make Sure You Are Connected To The Internet");
                }
            }
        }
    }
}
