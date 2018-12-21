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
    class API_TransToday
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
        public async Task getTransToday()
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
                    HttpResponseMessage message = client.GetAsync(ls.link + "/api/GetTransaksiSehari?Store=" + store_code).Result;
                    //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id:8082/api/GetTransactionAPI?store=ABQ").Result;
                    if (message.IsSuccessStatusCode)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(List<Trans_Today>));
                        var result = message.Content.ReadAsStringAsync().Result;
                        byte[] byteArray = Encoding.UTF8.GetBytes(result);
                        MemoryStream stream = new MemoryStream(byteArray);
                        List<Trans_Today> resultData = serializer.ReadObject(stream) as List<Trans_Today>;
                        for (int i = 0; i < resultData.Count; i++)
                        {
                            try
                            {
                               
                                DateTime date = DateTime.Parse(resultData[i].date);
                                //date = resultData[i].date;
                                String date_format = date.ToString("yyyy-MM-dd");
                                String sql = "INSERT INTO transaction (TRANSACTION_ID, CUSTOMER_ID, EMPLOYEE_ID, RECEIPT_ID, SPG_ID, DISCOUNT, TOTAL, STATUS,PAYMENT_TYPE, CASH, EDC, EDC2, CHANGEE, BANK_NAME, BANK_NAME2, NO_REF, NO_REF2, DATE, STATUS_API,CURRENCY) VALUES ('" + resultData[i].transactionId + "','" + resultData[i].customerId + "','" + resultData[i].employeeId + "','" + resultData[i].receiptId + "','" + resultData[i].spgId + "','" + resultData[i].discount + "','" + resultData[i].total + "','1','" + resultData[i].paymentType + "','" + resultData[i].cash + "','" + resultData[i].Edc1 + "','" + resultData[i].Edc2 + "','" + resultData[i].change + "','" + resultData[i].Bank1 + "','" + resultData[i].Bank2 + "','" + resultData[i].NoRef1 + "','" + resultData[i].NoRef2 + "','" + date_format + "','"+ resultData[i].status +"','" + resultData[i].currency + "')";
                                Crud input = new Crud();
                                input.NonReturn2(sql);

                                var b = resultData[i].transactionLines;
                                foreach (var c in b)
                                {
                                    var d = c.transactionId;
                                    var e = c.articleId;
                                    var f = e;
                                    String sql2 = "INSERT INTO transaction_line (TRANSACTION_ID,ARTICLE_ID,QUANTITY,PRICE,DISCOUNT,SUBTOTAL,SPG_ID,DISCOUNT_CODE,DISCOUNT_TYPE) values ('" + c.transactionId + "','" + c.articleId + "','" + c.qty + "','" + c.unitPrice + "','" + c.discount + "','" + c.amount + "','" + c.spgid + "','" + c.discountCode + "','" + c.discountType + "')";
                                    Crud input2 = new Crud();
                                    input2.NonReturn2(sql2);
                                }

                            }
                            catch (Exception ex)
                            { MessageBox.Show(ex.ToString()); }
                            //===========FOR LOOPING DO_LINE AND INSERT DATABASE=======================================

                        }
                        MessageBox.Show("Data has been sent to local database", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show(ex.ToString());
                    //MessageBox.Show("Make Sure You Are Connected To The Internet", "No Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
