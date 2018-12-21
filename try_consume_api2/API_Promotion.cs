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
    class API_Promotion
    {
        Connection ckon = new Connection();
        String store_code;
        LinkSwagger ls = new LinkSwagger();
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
            String sql = "DELETE FROM promotion";
            Crud delete = new Crud();
            delete.NonReturn2(sql);
            //=====================DELETE DO LINE=================
            String sql2 = "DELETE FROM promotion_line";
            Crud delete2 = new Crud();
            delete2.NonReturn2(sql2);
            //========DELETE ARTICLE DISCOUNT===============
            String sql3 = "DELETE FROM discount_item";
            Crud delete3 = new Crud();
            delete3.NonReturn2(sql3);

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
                    //HttpResponseMessage message = client.GetAsync("http://retailbiensi.azurewebsites.net/api/Promotion?StoreCode=" + store_code).Result;
                    //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/Promotion?StoreCode=" + store_code).Result;
                    HttpResponseMessage message = client.GetAsync(ls.link+"/api/Promotion?StoreCode=" + store_code).Result;

                    string ConnectionString = "Server='" + try_consume_api2.Properties.Settings.Default.mServer + "';Database='" + try_consume_api2.Properties.Settings.Default.mDBName + "';Uid='" + try_consume_api2.Properties.Settings.Default.mUserDB + "';Pwd='" + try_consume_api2.Properties.Settings.Default.mPassDB + "';";
                    StringBuilder sCommand = new StringBuilder("INSERT INTO promotion_line (_id, PROMOTION_ID_FK, DISCOUNT_CODE, ARTICLE_ID,ARTICLE_NAME,BRAND,SIZE,COLOR,GENDER,DEPARTMENT,DEPARTMENT_TYPE,CUSTOMER_GROUP,QTA,AMOUNT,BANK,DISCOUNT_PERCENT,DISCOUNT_PRICE,SPESIAL_PRICE,ARTICLE_ID_DISCOUNT,ARTICLE_NAME_DISCOUNT) VALUES");

                    if (message.IsSuccessStatusCode)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(List<Promotion>));
                        var result = message.Content.ReadAsStringAsync().Result;
                        byte[] byteArray = Encoding.UTF8.GetBytes(result);
                        MemoryStream stream = new MemoryStream(byteArray);
                        List<Promotion> resultData = serializer.ReadObject(stream) as List<Promotion>;

                        //**************************************************************************************
                        /*
                        using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                        {

                            List<string> Rows = new List<string>();
                            for (int i = 0; i < resultData.Count(); i++)
                            {
                                foreach (var c in resultData[i].promotionLines)
                                {
                                    Rows.Add(string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}')", MySqlHelper.EscapeString(c.id.ToString()), MySqlHelper.EscapeString(c.promotionIdFk.ToString()), MySqlHelper.EscapeString(c.discountCode), MySqlHelper.EscapeString(c.articleId), MySqlHelper.EscapeString(c.articleName), MySqlHelper.EscapeString(c.brand), MySqlHelper.EscapeString(c.size), MySqlHelper.EscapeString(c.color), MySqlHelper.EscapeString(c.gender), MySqlHelper.EscapeString(c.department), MySqlHelper.EscapeString(c.departmentType), MySqlHelper.EscapeString(c.customerGroup), MySqlHelper.EscapeString(c.qta.ToString()), MySqlHelper.EscapeString(c.amount.ToString()), MySqlHelper.EscapeString(c.bank), MySqlHelper.EscapeString(c.discountPercent.ToString()), MySqlHelper.EscapeString(c.discountPrice.ToString()), MySqlHelper.EscapeString(c.specialPrice.ToString()), MySqlHelper.EscapeString(c.articleIdDiscount), MySqlHelper.EscapeString(c.articleNameDiscount)));
                                }
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
                        */
                        //**************************************************************************************

                        
                    for (int i = 0; i < resultData.Count; i++)
                    {

                        //=================================================================
                        try
                        {
                            foreach (var c in resultData[i].promotionLines)
                            {
                                String sql = "INSERT INTO promotion_line (_id, PROMOTION_ID_FK, DISCOUNT_CODE, ARTICLE_ID, ARTICLE_NAME,BRAND,SIZE,COLOR,GENDER,DEPARTMENT,DEPARTMENT_TYPE,CUSTOMER_GROUP,QTA,AMOUNT,BANK,DISCOUNT_PERCENT,DISCOUNT_PRICE,SPESIAL_PRICE,ARTICLE_ID_DISCOUNT,ARTICLE_NAME_DISCOUNT) VALUES ('" + c.id + "','" + c.promotionIdFk + "', '" + c.discountCode + "', '" + c.articleId + "', '" + c.articleName + "','" + c.brand + "','" + c.size + "','" + c.color + "','" + c.gender + "','" + c.department + "','" + c.departmentType + "','" + c.customerGroup + "','" + c.qta + "','" + c.amount + "','" + c.bank + "','" + c.discountPercent + "', '" + c.discountPrice + "','" + c.specialPrice + "','" + c.articleIdDiscount + "','" + c.articleNameDiscount + "')";
                                Crud input = new Crud();
                                input.NonReturn2(sql);
                            }
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.ToString());
                        }
                        //=========================================================
                        try
                        {
                            foreach (var b in resultData[i].discountItems)
                            {
                                String sql2 = "INSERT INTO discount_item (_id, ARTICLE_ID, ARTICLE_NAME,BRAND,GENDER,DEPARTMENT,DEPARTMENT_TYPE,SIZE,COLOR,UNIT,PRICE,DISCOUNT_CODE) VALUES ('" + b.id + "', '" + b.articleId + "', '" + b.articleName + "','" + b.brand + "','" + b.gender + "','" + b.department + "', '" + b.departmentType + "','" + b.size + "','" + b.color + "', '" + b.unit + "','" + b.price + "','" + resultData[i].discountCode + "')";
                                Crud inputA = new Crud();
                                inputA.NonReturn2(sql2);
                            }
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.ToString());
                        }
                        //=========================================================
                        try
                        {

                            String sql1 = "INSERT INTO promotion (_id,DISCOUNT_CODE,DISCOUNT_NAME,DISCOUNT_CATEGORY,DESCRIPTION,START_DATE,END_DATE,STATUS) VALUES ('" + resultData[i].id + "', '" + resultData[i].discountCode + "', '" + resultData[i].discountName + "', '" + resultData[i].discountCategory + "', '" + resultData[i].description + "', '" + resultData[i].startDate + "', '" + resultData[i].endDate + "', '" + resultData[i].status + "')";
                            Crud input2 = new Crud();
                            input2.NonReturn2(sql1);
                        }
                        catch(Exception ex)
                        {
                            //MessageBox.Show(ex.ToString());
                        }

                    }
                    
                        //======================END FOR GET DO DATA======================================
                        String query = "UPDATE log_msg SET Status='Success' WHERE Data = 'Promotion' ";
                        Crud update = new Crud();
                        update.NonReturn2(query);
                        MessageBox.Show("Successful Update Data Promotion", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        String query = "UPDATE log_msg SET Status='Failed' WHERE Data = 'Promotion' ";
                        Crud update = new Crud();
                        update.NonReturn2(query);

                        response = "Fail";
                        MessageBox.Show("Error API Promotion","Error API", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                catch (Exception ex)
                {
                    String query = "UPDATE log_msg SET Status='Failed' WHERE Data = 'Promotion' ";
                    Crud update = new Crud();
                    update.NonReturn2(query);

                    //MessageBox.Show("Make Sure You Are Connected To The Internet", "No Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
