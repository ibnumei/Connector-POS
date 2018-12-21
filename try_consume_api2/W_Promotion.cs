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
    public partial class W_Promotion : Form
    {
        Connection ckon = new Connection();
        String store_code;
        public W_Promotion()
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
                    //HttpResponseMessage message = client.GetAsync("http://retailbiensi.azurewebsites.net/api/Promotion?storeId=" + store_code).Result;
                    HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/Promotion?StoreCode=" + store_code).Result;
                    if (message.IsSuccessStatusCode)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(List<Promotion>));
                        var result = message.Content.ReadAsStringAsync().Result;
                        byte[] byteArray = Encoding.UTF8.GetBytes(result);
                        MemoryStream stream = new MemoryStream(byteArray);
                        List<Promotion> resultData = serializer.ReadObject(stream) as List<Promotion>;
                        for (int i = 0; i < resultData.Count; i++)
                        {

                            //=================================FOR LOOPING DO_LINE AND INSERT DATABASE=======================================
                            foreach (var c in resultData[i].promotionLines)
                            {

                                String sql = "INSERT INTO promotion_line (_id, PROMOTION_ID_FK, DISCOUNT_CODE, ARTICLE_ID, ARTICLE_NAME,BRAND,SIZE,COLOR,GENDER,DEPARTMENT,DEPARTMENT_TYPE,CUSTOMER_GROUP,QTA,AMOUNT,BANK,DISCOUNT_PERCENT,DISCOUNT_PRICE,SPESIAL_PRICE,ARTICLE_ID_DISCOUNT,ARTICLE_NAME_DISCOUNT) VALUES ('" + c.id + "','" + c.promotionIdFk + "', '" + c.discountCode + "', '" + c.articleId + "', '" + c.articleName + "','"+ c.brand +"','"+ c.size +"','"+ c.color +"','"+ c.gender +"','"+ c.department +"','"+ c.departmentType +"','"+ c.customerGroup +"','"+ c.qta +"','"+ c.amount +"','"+ c.bank +"','"+ c.discountPercent +"', '"+ c.discountPrice +"','"+ c.specialPrice +"','"+ c.articleIdDiscount +"','"+ c.articleNameDiscount +"')";
                                Crud input = new Crud();
                                input.NonReturn2(sql);
                            }
                            //=====================END GET AND INSERT DO_LINE INTO DATABASE=========================================
                            String sql1 = "INSERT INTO promotion (_id,DISCOUNT_CODE,DISCOUNT_NAME,DISCOUNT_CATEGORY,DESCRIPTION,START_DATE,END_DATE,STATUS) VALUES ('" + resultData[i].id + "', '" + resultData[i].discountCode + "', '" + resultData[i].discountName + "', '" + resultData[i].discountCategory + "', '" + resultData[i].description + "', '" + resultData[i].startDate + "', '" + resultData[i].endDate + "', '" + resultData[i].status + "')";
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
        //=======================================================================================================
        //======================================DELETE DATA BEFORE GET FROM API==================================
        public void delete()
        {
            ckon.con.Close();
            String sql = "DELETE FROM promotion";
            Crud delete = new Crud();
            delete.NonReturn2(sql);
            //=====================DELETE DO LINE=================
            String sql2 = "DELETE FROM promotion_line";
            Crud delete2 = new Crud();
            delete2.NonReturn2(sql2);

        }

        private void b_reload2_Click(object sender, EventArgs e)
        {
            API_Promotion promo = new API_Promotion();
            promo.delete();
            promo.get_cust_id();
            promo.getArticle().Wait();
        }
        //=======================================================================================================
    }
}
