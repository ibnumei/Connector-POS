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
    class API_Expense
    {
        Connection ckon = new Connection();
        Connection2 ckon2 = new Connection2();
        LinkSwagger ls = new LinkSwagger();
        String del_id_cost;
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
                    //HttpResponseMessage message = client.GetAsync("http://retailbiensi.azurewebsites.net/api/CostCategoryMaster").Result;
                    //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/CostCategoryMaster").Result;
                    HttpResponseMessage message = client.GetAsync(ls.link+"/api/CostCategoryMaster").Result;
                    if (message.IsSuccessStatusCode)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(List<CostCategory>));
                        var result = message.Content.ReadAsStringAsync().Result;
                        byte[] byteArray = Encoding.UTF8.GetBytes(result);
                        MemoryStream stream = new MemoryStream(byteArray);
                        List<CostCategory> resultData = serializer.ReadObject(stream) as List<CostCategory>;

                        for (int i = 0; i < resultData.Count; i++)
                        {

                            try
                            {
                                String sql = "INSERT INTO costcategory (_id, COST_CATEGORY_ID, NAME, COA) VALUES ('" + resultData[i].Id + "', '" + resultData[i].CostCategoryId + "', '" + resultData[i].Name + "', '" + resultData[i].Coa + "')";
                                Crud input = new Crud();
                                input.NonReturn2(sql);
                                //MessageBox.Show("Successful Update Data Article");
                            }
                            catch(Exception ex)
                            {
                                //MessageBox.Show(ex.ToString());
                            }
                        }
                        String query = "UPDATE log_msg SET Status='Success' WHERE Data = 'Expense' ";
                        Crud update = new Crud();
                        update.NonReturn2(query);
                        MessageBox.Show("Successful Update Data Expense", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //===========================END FOR GET DATA===============================================================
                    }
                    else
                    {
                        String query = "UPDATE log_msg SET Status='Failed' WHERE Data = 'Expense' ";
                        Crud update = new Crud();
                        update.NonReturn2(query);

                        response = "Fail";
                        MessageBox.Show("Error API Expense", "Error API", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                catch (Exception ex)
                {
                    String query = "UPDATE log_msg SET Status='Failed' WHERE Data = 'Expense' ";
                    Crud update = new Crud();
                    update.NonReturn2(query);

                    MessageBox.Show("Make Sure You Are Connected To The Internet", "No Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        //==============================================================================================================================
        public void delete()
        {
            ckon.con.Close();
            String sql = "DELETE FROM costcategory";
            Crud delete = new Crud();
            delete.NonReturn2(sql);
        }
    }
}
