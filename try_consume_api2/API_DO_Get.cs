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
    class API_DO_Get
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
        //======================================DELETE DATA BEFORE GET FROM API==================================
        public void delete()
        {
            ckon.con.Close();
            String sql = "DELETE FROM deliveryorder";
            Crud delete = new Crud();
            delete.NonReturn2(sql);
            //=====================DELETE DO LINE=================
            String sql2 = "DELETE FROM deliveryorder_line";
            Crud delete2 = new Crud();
            delete2.NonReturn2(sql2);

        }
        //=======================================================================================================
        //==============================================================================================================================
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
                    //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/DeliveryOrder?StoreCode=" + store_code).Result;
                    HttpResponseMessage message = client.GetAsync(ls.link+"/api/DeliveryOrder?StoreCode=" + store_code).Result;
                    //HttpResponseMessage message = client.GetAsync("http://retailbiensi.azurewebsites.net/api/DeliveryOrder?StoreCode=" + store_code).Result;
                    if (message.IsSuccessStatusCode)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(List<DeliveryOrder>));
                        var result = message.Content.ReadAsStringAsync().Result;
                        byte[] byteArray = Encoding.UTF8.GetBytes(result);
                        MemoryStream stream = new MemoryStream(byteArray);
                        List<DeliveryOrder> resultData = serializer.ReadObject(stream) as List<DeliveryOrder>;
                        for (int i = 0; i < resultData.Count; i++)
                        {
                            try
                            {
                                foreach (var c in resultData[i].deliveryOrderLines)
                                {
                                    try
                                    {
                                        String sql = "INSERT INTO deliveryorder_line (_id, DELIVERY_ORDER_ID, ARTICLE_ID, QTY_DELIVER, QTY_RECEIVE, AMOUNT, PACKING_NUMBER) VALUES ('" + c.id + "','" + c.deliveryOrderId + "', '" + c.articleIdFk + "', '" + c.qtyDeliver + "', '" + c.qtyDeliver + "','"+ c.amount +"', '"+ c.packingNumber +"')";
                                        Crud input = new Crud();
                                        input.NonReturn2(sql);
                                    }
                                    catch (Exception ex)
                                    {
                                        //MessageBox.Show(ex.ToString());
                                    }

                                }
                                //===========END GET AND INSERT DO_LINE INTO DATABASE=========================================
                                try
                                {
                                    String sql1 = "INSERT INTO deliveryorder (_id, DELIVERY_ORDER_ID, STORE_CODE, WAREHOUSE_FROM, WAREHOUSE_TO, DELIVERY_DATE, DELIVERY_TIME, TOTAL_QTY, STATUS, DATE, TIME, CUST_ID_STORE,TOTAL_AMOUNT) VALUES ('" + resultData[i].id + "', '" + resultData[i].deliveryOrderId + "', '" + resultData[i].storeCode + "', '" + resultData[i].warehouseFrom + "', '" + resultData[i].warehouseTo + "', '" + resultData[i].deliveryDate + "', '" + resultData[i].deliveryTime + "', '" + resultData[i].totalQty + "', '" + resultData[i].status + "', '" + resultData[i].date + "', '" + resultData[i].time + "', '" + resultData[i].CustomerIdStore + "','"+ resultData[i].totalAmount +"')";
                                    Crud input2 = new Crud();
                                    input2.NonReturn2(sql1);
                                }
                                catch(Exception ex)
                                {
                                    //MessageBox.Show(ex.ToString());
                                }

                            }
                            catch
                            { }
                            //===========FOR LOOPING DO_LINE AND INSERT DATABASE=======================================
                           
                        }
                        MessageBox.Show("Data has been sent to local database", "Successful", MessageBoxButtons.OK,MessageBoxIcon.Information);
                        //======================END FOR GET DO DATA======================================
                    }
                    else
                    {
                        response = "Fail";
                        MessageBox.Show("Error API DO", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                catch (Exception ex)
                {
                    response = ex.ToString();
                    MessageBox.Show("Make Sure You Are Connected To The Internet", "No Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        //==============================================================================================================================
    }
}
