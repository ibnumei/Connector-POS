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
    class API_CustGroup
    {
        Connection ckon = new Connection();
        LinkSwagger ls = new LinkSwagger();
        String value;
        public void cek_data_log()
        {
            ckon.con.Close();
            String sql = "SELECT * FROM log_msg where Data = 'Customer Group'";
            ckon.cmd = new MySqlCommand(sql, ckon.con);
            ckon.con.Open();
            ckon.myReader = ckon.cmd.ExecuteReader();
            if(ckon.myReader.HasRows)
            {
                while (ckon.myReader.Read())
                {
                    value = ckon.myReader.GetString("STATUS");
                }
            }
            else
            {
                String input = "INSERT INTO log_msg (DATA,STATUS) VALUES ('Customer Group','-')";
                Crud input_query = new Crud();
                input_query.NonReturn2(input);
            }
           
            ckon.con.Close();
        }
        public void del_tabel()
        {
            try
            {
                String sql = "DROP TABLE customer_group";
                Crud INPUT = new Crud();
                INPUT.NonReturn2(sql);

                String query = "CREATE TABLE customer_group (_id Bigint (20) NOT NULL DEFAULT 0,CODE varchar(50) NOT NULL DEFAULT '-',DESCRIPTION varchar(50) NOT NULL DEFAULT '-')";
                Crud INPUT2 = new Crud();
                INPUT2.NonReturn2(query);
            }
            catch
            {
                String query = "CREATE TABLE customer_group (_id Bigint (20) NOT NULL DEFAULT 0,CODE varchar(50) NOT NULL DEFAULT '-',DESCRIPTION varchar(50) NOT NULL DEFAULT '-')";
                Crud INPUT2 = new Crud();
                INPUT2.NonReturn2(query); 
            }

        }
        //==============================================================================
        public async Task get_cust_group()
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
                    //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id:8082/api/CustomerGroupAPI").Result;
                    HttpResponseMessage message = client.GetAsync(ls.link + "/api/CustomerGroupAPI").Result;


                    if (message.IsSuccessStatusCode)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(List<CustomerGroup>));
                        var result = message.Content.ReadAsStringAsync().Result;
                        byte[] byteArray = Encoding.UTF8.GetBytes(result);
                        MemoryStream stream = new MemoryStream(byteArray);
                        List<CustomerGroup> resultData = serializer.ReadObject(stream) as List<CustomerGroup>;

                        for (int i = 0; i < resultData.Count; i++)
                        {
                            try
                            {

                                String sql = "INSERT INTO customer_group (_id ,CODE, DESCRIPTION) VALUES('" + resultData[i].Id + "' ,'" + resultData[i].Code + "', '" + resultData[i].Description + "')";
                                Crud input = new Crud();
                                input.NonReturn2(sql);
                                //MessageBox.Show(resultData[i].Id + "," + "," + resultData[i].Code + "," + resultData[i].Name);
                            }
                            catch (Exception ex)
                            {
                                //MessageBox.Show(ex.ToString());

                            }

                        }

                        String query = "UPDATE log_msg SET Status='Success' WHERE Data = 'Customer Group' ";
                        Crud update = new Crud();
                        update.NonReturn2(query);
                        MessageBox.Show("Successful Update Data Customer Group", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        String query = "UPDATE log_msg SET Status='Failed' WHERE Data = 'Customer Group' ";
                        Crud update = new Crud();
                        update.NonReturn2(query);

                        response = "Fail";
                        MessageBox.Show("Error API Customer Group", "Error API", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                catch (Exception ex)
                {
                    String query = "UPDATE log_msg SET Status='Failed' WHERE Data = 'Customer Group' ";
                    Crud update = new Crud();
                    update.NonReturn2(query);

                    response = ex.ToString();
                    MessageBox.Show("Make Sure You Are Connected To The Internet", "No Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //MessageBox.Show(ex.ToString());
                }
            }
        }
        //====================================================================================================
    }
}
