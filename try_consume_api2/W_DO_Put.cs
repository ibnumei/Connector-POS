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
    public partial class W_DO_Put : Form
    {
        Connection ckon1 = new Connection();
        Connection2 ckon2 = new Connection2();
        Connection3 ckon3 = new Connection3();
        //========================VARIABLE FOR ARTICLE ======== =========================================
        String id_from_article2, articleName2, brand2, color2, department2, dept_type2, gender2, size2, unit2;
        int id_article2, price_article2;
        //========================VARIABLE FOR DELIVERY ORDER LINE ======== =============================
        int id_article_Fk2, dev_orderid2_Fk, id_DO_Line2, qty_dev2, qty_rev2;
        String dev_orderid2;
        //========================VARIABLE FOR DELIVERY ORDER HEADER======= =============================
        String date2, dev_date2, dev_orderId2, dev_time2, time2, timestamp2, store_code2, war_from2, war_to2;
        int id_Do2, status2, totalqty2;
        //===============================================================================================
        public W_DO_Put()
        {
            InitializeComponent();
        }
        //============================================================================================================
        public async Task Put_Dev_Order()
        {
            DeliveryOrder do2 = new DeliveryOrder();
            do2.deliveryOrderLines = new List<DeliveryOrderLine>();
            String sql = "SELECT * FROM deliveryorder WHERE STATUS_API=0";
            ckon1.cmd = new MySqlCommand(sql, ckon1.con);
            ckon1.con.Open();
            ckon1.myReader = ckon1.cmd.ExecuteReader();
            if(ckon1.myReader.HasRows)
            {
                while(ckon1.myReader.Read())
                {
                    //=================AMBIL NILAI DO HEADER====================
                    dev_orderId2 = ckon1.myReader.GetString("DELIVERY_ORDER_ID");
                    dev_orderid2_Fk = ckon1.myReader.GetInt32("_id");
                    date2 = ckon1.myReader.GetString("DATE");
                    dev_date2 = ckon1.myReader.GetString("DELIVERY_DATE");
                    dev_time2 = ckon1.myReader.GetString("TIME");
                    id_Do2 = ckon1.myReader.GetInt32("_id");
                    status2 = ckon1.myReader.GetInt32("STATUS");
                    time2 = ckon1.myReader.GetString("TIME");
                    timestamp2 = ckon1.myReader.GetString("TIME_STAMP");
                    totalqty2 = ckon1.myReader.GetInt32("TOTAL_QTY");
                    store_code2 = ckon1.myReader.GetString("STORE_CODE");
                    war_from2 = ckon1.myReader.GetString("WAREHOUSE_FROM");
                    war_to2 = ckon1.myReader.GetString("WAREHOUSE_TO");
                    //===========SEARCH DO LINE BY DO ID========================
                    String sql2 = "SELECT * FROM deliveryorder_line WHERE DELIVERY_ORDER_ID = '" + dev_orderId2 + "'";
                    ckon2.cmd2 = new MySqlCommand(sql2, ckon2.con2);
                    ckon2.con2.Open();
                    ckon2.myReader2 = ckon2.cmd2.ExecuteReader();
                    while(ckon2.myReader2.Read())
                    {
                        //====================GET VALUE FROM DO LINE======================================
                        id_article_Fk2 = ckon2.myReader2.GetInt32("ARTICLE_ID");
                        //=============SEARCH DATA ARTICLE BY ARTICLE ID================================
                        String sql3 = "SELECT * FROM article WHERE ARTICLE_ID='" + id_article_Fk2 + "'";
                        ckon3.cmd3 = new MySqlCommand(sql3, ckon3.con3);
                        ckon3.con3.Open();
                        ckon3.myReader3 = ckon3.cmd3.ExecuteReader();
                        while (ckon3.myReader3.Read())
                        {
                            id_article2 = ckon3.myReader3.GetInt32("_id");
                            id_from_article2 = ckon3.myReader3.GetString("ARTICLE_ID");
                            articleName2 = ckon3.myReader3.GetString("ARTICLE_NAME");
                            brand2 = ckon3.myReader3.GetString("BRAND");
                            gender2 = ckon3.myReader3.GetString("GENDER");
                            department2 = ckon3.myReader3.GetString("DEPARTMENT");
                            dept_type2 = ckon3.myReader3.GetString("DEPARTMENT_TYPE");
                            size2 = ckon3.myReader3.GetString("SIZE");
                            color2 = ckon3.myReader3.GetString("COLOR");
                            unit2 = ckon3.myReader3.GetString("UNIT");
                            price_article2 = ckon3.myReader3.GetInt32("PRICE");
                        }
                        ckon3.con3.Close();
                        //===============================END OF ARTICLE DATA============================
                        dev_orderid2 = ckon2.myReader2.GetString("DELIVERY_ORDER_ID");
                        id_DO_Line2 = ckon2.myReader2.GetInt32("_id");
                        qty_dev2 = ckon2.myReader2.GetInt32("QTY_DELIVER");
                        qty_rev2 = ckon2.myReader2.GetInt32("QTY_RECEIVE");

                        DeliveryOrderLine do_line = new DeliveryOrderLine()
                        {
                            article = new Article
                            {
                                articleId = id_from_article2,
                                articleName = articleName2,
                                brand = brand2,
                                color = color2,
                                department = department2,
                                departmentType = dept_type2,
                                gender = gender2,
                                size = size2,
                                unit = unit2,
                                id = id_article2,
                                price = price_article2
                            },
                            articleIdFk = id_article_Fk2,
                            deliveryOrderId = dev_orderid2,
                            deliveryOrderIdFk = dev_orderid2_Fk,
                            id = id_DO_Line2,
                            qtyDeliver = qty_dev2,
                            qtyReceive = qty_rev2
                        };

                        do2.deliveryOrderLines.Add(do_line);
                    }
                    ckon2.con2.Close();
                    //======================END WHILE CKON2 GET DO LINE DATA===============================

                    DeliveryOrder dev_order = new DeliveryOrder()
                    {
                        date = date2,
                        deliveryDate = dev_date2,
                        deliveryOrderId = dev_orderId2,
                        deliveryOrderLines = do2.deliveryOrderLines,
                        deliveryTime = dev_time2,
                        id = id_Do2,
                        status = status2,
                        time = time2,
                        timeStamp = timestamp2,
                        totalQty = totalqty2,
                        storeCode = store_code2,
                        warehouseFrom = war_from2,
                        warehouseTo = war_to2
                    };
                    var json2 = new JavaScriptSerializer().Serialize(dev_order);
                    var stringPayload = JsonConvert.SerializeObject(dev_order);
                    //String response = "";
                    var credentials = new NetworkCredential("username", "password");
                    var handler = new HttpClientHandler { Credentials = credentials };
                    var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                    using (var client = new HttpClient(handler))
                    {
                        //HttpResponseMessage message = client.PostAsync("http://retailbiensi.azurewebsites.net/api/DeliveryOrder", httpContent).Result;
                        HttpResponseMessage message = client.PutAsync("http://mpos.biensicore.co.id/api/DeliveryOrder", httpContent).Result;

                    }
                }
                //=========================END WHILE CKON1 GET DO HEADER DATA==============================
            }
            ckon1.con.Close();
            //=============================END IF HAS ROWS DATA=============================================


            //DeliveryOrderLine do_line = new DeliveryOrderLine()
            //{
            //    article = new Article
            //    {
            //        articleId = id_from_article2,
            //        articleName = articleName2,
            //        brand = brand2,
            //        color = color2,
            //        department = department2,
            //        departmentType = dept_type2,
            //        gender = gender2,
            //        size = size2,
            //        unit = unit2,
            //        id = id_article2,
            //        price = price_article2
            //    },
            //    articleIdFk = id_article_Fk2,
            //    deliveryOrderId = dev_orderid2,
            //    deliveryOrderIdFk = dev_orderid2_Fk,
            //    id = id_DO_Line2,
            //    qtyDeliver = qty_dev2,
            //    qtyReceive = qty_rev2
            //};
            //DeliveryOrder do2 = new DeliveryOrder();
            //do2.deliveryOrderLines = new List<DeliveryOrderLine>();
            //do2.deliveryOrderLines.Add(do_line);

            //DeliveryOrder dev_order = new DeliveryOrder()
            //{
            //    date = date2,
            //    deliveryDate = dev_date2,
            //    deliveryOrderId = dev_orderId2,
            //    deliveryOrderLines = do2.deliveryOrderLines,
            //    deliveryTime = dev_time2,
            //    id= id_Do2,
            //    status = status2,
            //    time = time2,
            //    timeStamp = timestamp2,
            //    totalQty = totalqty2,
            //    storeCode = store_code2,
            //    warehouseFrom = war_from2,
            //    warehouseTo = war_to2
            //};
            //var json2 = new JavaScriptSerializer().Serialize(dev_order);
            //var stringPayload = JsonConvert.SerializeObject(dev_order);
            ////String response = "";
            //var credentials = new NetworkCredential("username", "password");
            //var handler = new HttpClientHandler { Credentials = credentials };
            //var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            //using (var client = new HttpClient(handler))
            //{
            //    HttpResponseMessage message = client.PostAsync("http://retailbiensi.azurewebsites.net/api/DeliveryOrder", httpContent).Result;

            //}
        }
        //============================================================================================================
        private void W_DO_Put_Load(object sender, EventArgs e)
        {
            //===== ARTICLE
            //id_from_article2 = "2212121212";
            //articleName2 = "Greenlight Pants Jeans 34 Blue Men";
            //brand2 = "Greenlight";
            //color2 = "Blue";
            //department2 = "Pants";
            //dept_type2 = "Jeans";
            //gender2 = "Men";
            //size2 = "34";
            //unit2 = "Pcs";
            //id_article2 = 0;
            //price_article2 = 550000;
            //=========== DO LINE
            // id_article_Fk2 = 3;
            //dev_orderid2 = "DO-009";
            //dev_orderid2_Fk = 288;
            //id_DO_Line2 = 257;
            //qty_dev2 = 100;
            //qty_rev2 = 100;
            //========== DO HEADER
            // date2 = "2018-01-08";
            //dev_date2 = "2018-01-08";
            //dev_orderId2 = "DO-009";
            //dev_time2 = "2018-01-08";
            //id_Do2 = 288;
            //status2 = 1;
            //time2 = "04:38:08";
            //timestamp2 = "04:38:08";
            //totalqty2 = 1;
            //store_code2 = "";
            //war_from2 = "";
            //war_to2 = "AAA";
        }

        //======================================================================================================
        private void b_reload_Click(object sender, EventArgs e)
        {
            //Put_Dev_Order().Wait();
            API_DO_PUT do_put = new API_DO_PUT();
            do_put.Put_Dev_Order().Wait();

        }
    }
}
