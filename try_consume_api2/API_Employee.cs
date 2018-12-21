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
    class API_Employee
    {
        Connection ckon = new Connection();
        LinkSwagger ls = new LinkSwagger();
        //=================EMPLOYEE=================
        int e_id, p_id, e_pos;
        String e_plyId, e_name, p_posId, p_posName, store, e_pass;
        //======================================DELETE DATA BEFORE GET FROM API==================================
        public void delete()
        {
            ckon.con.Close();
            
            String sql2 = "DELETE FROM position";
            Crud INPUT2 = new Crud();
            INPUT2.NonReturn2(sql2);
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
        public async Task get_data_employee()
        {
            String response = "";
            var credentials = new NetworkCredential("username", "password");
            var handler = new HttpClientHandler { Credentials = credentials };
            //var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            using (var client = new HttpClient(handler))
            {
                try
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
                            //=============EMPLOYEE================
                            foreach (var c in resultData.employees)
                            {
                                //MessageBox.Show(c.id + " ," + c.name + " ," + c.employeeId+" ,"+c.possition.possitionName+" ,"+c.possition.id+" ,"+c.possition.possitionId);
                                e_id = c.id;
                                e_name = c.name;
                                e_plyId = c.employeeId;
                                e_pos = c.possition.id;
                                e_pass = c.passwordaja;
                                p_id = c.possition.id;
                                p_posName = c.possition.possitionName;
                                p_posId = c.possition.possitionId;
                                String sql5 = "INSERT INTO position (_id, POSITION_ID, POSITION_NAME) VALUES ('" + p_id + "', '" + p_posId + "', '" + p_posName + "')";
                                Crud input5 = new Crud();
                                input5.NonReturn(sql5);

                                String sql6 = "INSERT INTO employee (_id, EMPLOYEE_ID, NAME, POSITION_ID,Pass) VALUES ('" + e_id + "', '" + e_plyId + "', '" + e_name + "', '" + e_pos + "', '" + e_pass + "')";
                                Crud input6 = new Crud();
                                input6.NonReturn(sql6);
                            }
                            MessageBox.Show("Successfully Retrieving Employee Data", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            //MessageBox.Show("Make Sure You Are Connected To Internet");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error API Employee", "Error API", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch
                {
                    MessageBox.Show("Make Sure You Are Connected To The Internet", "No Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
        //=========================================================================================
    }
}
