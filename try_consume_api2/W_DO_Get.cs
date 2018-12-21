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
    public partial class W_DO_Get : Form
    {
        Connection ckon = new Connection();
        String store_code;
        public W_DO_Get()
        {
            InitializeComponent();
        }

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
                    //HttpResponseMessage message = client.GetAsync("http://retailbiensi.azurewebsites.net/api/DeliveryOrder?StoreCode="+store_code).Result;
                    HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/DeliveryOrder?StoreCode=" + store_code).Result;
                    if (message.IsSuccessStatusCode)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(List<DeliveryOrder>));
                        var result = message.Content.ReadAsStringAsync().Result;
                        byte[] byteArray = Encoding.UTF8.GetBytes(result);
                        MemoryStream stream = new MemoryStream(byteArray);
                        List<DeliveryOrder> resultData = serializer.ReadObject(stream) as List<DeliveryOrder>;
                        for (int i = 0; i < resultData.Count; i++)
                        {

                            //=================================FOR LOOPING DO_LINE AND INSERT DATABASE=======================================
                            foreach(var c in resultData[i].deliveryOrderLines)
                            {

                                String sql = "INSERT INTO deliveryorder_line (_id, DELIVERY_ORDER_ID, ARTICLE_ID, QTY_DELIVER, QTY_RECEIVE) VALUES ('"+ c.id +"','"+ c.deliveryOrderId +"', '"+ c.articleIdFk +"', '"+ c.qtyDeliver +"', '"+ c.qtyReceive +"')";
                                Crud input = new Crud();
                                input.NonReturn2(sql);
                            }
                            //=====================END GET AND INSERT DO_LINE INTO DATABASE=========================================
                            String sql1 = "INSERT INTO deliveryorder (_id, DELIVERY_ORDER_ID, STORE_CODE, WAREHOUSE_FROM, WAREHOUSE_TO, DELIVERY_DATE, DELIVERY_TIME, TOTAL_QTY, STATUS, DATE, TIME, TIME_STAMP) VALUES ('" + resultData[i].id + "', '" + resultData[i].deliveryOrderId + "', '" + resultData[i].storeCode + "', '" + resultData[i].warehouseFrom + "', '" + resultData[i].warehouseTo + "', '" + resultData[i].deliveryDate + "', '" + resultData[i].deliveryTime + "', '" + resultData[i].totalQty + "', '" + resultData[i].status + "', '" + resultData[i].date + "', '" + resultData[i].time + "', '" + resultData[i].timeStamp + "')";
                            Crud input2 = new Crud();
                            input2.NonReturn2(sql1);
                        }
                        //======================END FOR GET DO DATA======================================
                    }
                    else
                    {
                        response = "Fail";
                    }

                }
                catch (Exception ex)
                {
                    response = ex.ToString();
                }
            }
        }
        //=============================================================================================================================
        private void W_DO_Get_Load(object sender, EventArgs e)
        {
            //API_DO_Get DO_GET = new API_DO_Get();
           // DO_GET.get_cust_id();
           // DO_GET.getArticle().Wait();
            //MessageBox.Show("Data has been sent to local database");
        }
        //==============================================================================================================================
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

        private void b_reload_Click(object sender, EventArgs e)
        {
            API_DO_Get DO_GET = new API_DO_Get();
            DO_GET.get_cust_id();
            DO_GET.getArticle().Wait();
            //MessageBox.Show("Data has been sent to local database");
        }
        //=======================================================================================================

    }
}
