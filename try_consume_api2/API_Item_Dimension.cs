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
    class API_Item_Dimension
    {
        LinkSwagger ls = new LinkSwagger();

        //===============VARIABLE BRANDS=============================================
        int b_id;
        String b_code, b_desc;
        //================VARIABLE COLOR=======================================
        int c_id;
        String c_code, c_desc;
        //================VARIABLE DEPARTMENT==================================
        int d_id;
        String d_code, d_desc;
        //===============VARIABLE DEPARTMENT TYPE=============================
        int d_t_id;
        String d_t_code, d_t_desc;
        //================VARIABLE GENDER====================================
        int g_id;
        String g_code, g_desc;
        //================VARIABLE SIZE=====================================
        int s_id;
        String s_code, s_desc;
        //============================DELETE============================
        public void delete()
        {
            //=========DELETE INVENTORY===============
            String sql = "DELETE FROM brand";
            Crud input = new Crud();
            input.NonReturn2(sql);
            //========DELETE CURRENCY==================
            String sql2 = "DELETE FROM color";
            Crud input2 = new Crud();
            input2.NonReturn2(sql2);
            //=======DELETE DENOMINATION===============
            String sql3 = "DELETE FROM departement";
            Crud input3 = new Crud();
            input3.NonReturn2(sql3);
            //=========DELETE STORE====================
            String sql4 = "DELETE FROM departementtype";
            Crud input4 = new Crud();
            input4.NonReturn2(sql4);
            //=========DELETE WAREHOUSE=================
            String sql5 = "DELETE FROM gender";
            Crud input5 = new Crud();
            input5.NonReturn2(sql5);
            //==========BANK=============================
            String sql6 = "DELETE FROM size";
            Crud input6 = new Crud();
            input6.NonReturn2(sql6);
        }
        //=======================================================================================================

        //===================================METHOD GET================================================
        public async Task get_Item()
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
                    //HttpResponseMessage message = client.GetAsync("http://retailbiensi.azurewebsites.net/api/ItemDimension").Result;
                    //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/ItemDimension").Result;
                    HttpResponseMessage message = client.GetAsync(ls.link+"/api/ItemDimension").Result;
                    if (message.IsSuccessStatusCode)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(ItemDimension));
                        var result = message.Content.ReadAsStringAsync().Result;
                        byte[] byteArray = Encoding.UTF8.GetBytes(result);
                        MemoryStream stream = new MemoryStream(byteArray);
                        ItemDimension resultData = serializer.ReadObject(stream) as ItemDimension;
                        //==========================INSERT BRANDS===================================
                        try
                        {
                            foreach (var a in resultData.brands)
                            {
                                //MessageBox.Show(a.Code + " , " + a.Description);
                                b_id = a.Id;
                                b_code = a.Code;
                                b_desc = a.Description;
                                String sql = "INSERT INTO brand (_id, CODE, DESCRIPTION) VALUES ('" + b_id + "', '" + b_code.Replace("'", "''") + "', '" + b_desc.Replace("'", "''") + "')";
                                Crud input = new Crud();
                                input.NonReturn(sql);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }

                        //========================COLOR====================================
                        try
                        {
                            foreach (var b in resultData.colors)
                            {
                                c_id = b.Id;
                                c_code = b.Code;
                                c_desc = b.Description;
                                String sql2 = "INSERT INTO color (_id, CODE, DESCRIPTION) VALUES ('" + c_id + "', '" + c_code.Replace("'", "''") + "', '" + c_desc.Replace("'", "''") + "')";
                                Crud input2 = new Crud();
                                input2.NonReturn(sql2);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }

                        //========================DEPARTMENTS====================================
                        try
                        {
                            foreach (var c in resultData.departments)
                            {
                                d_id = c.Id;
                                d_code = c.Code;
                                d_desc = c.Description;
                                String sql3 = "INSERT INTO departement (_id, CODE, DESCRIPTION) VALUES ('" + d_id + "', '" + d_code.Replace("'", "''") + "', '" + d_desc.Replace("'", "''") + "')";
                                Crud input3 = new Crud();
                                input3.NonReturn(sql3);
                            }
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }

                        //========================DEPARTMENTS TYPE====================================
                        try
                        {
                            foreach (var d in resultData.departmentTypes)
                            {
                                d_t_id = d.Id;
                                d_t_code = d.Code;
                                d_t_desc = d.Description;
                                String sql4 = "INSERT INTO departementtype (_id, CODE, DESCRIPTION) VALUES ('" + d_t_id + "', '" + d_t_code.Replace("'", "''") + "', '" + d_t_desc.Replace("'", "''") + "')";
                                Crud input4 = new Crud();
                                input4.NonReturn(sql4);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }

                        //========================GENDER================================================
                        try
                        {
                            foreach (var e in resultData.genders)
                            {
                                g_id = e.Id;
                                g_code = e.Code;
                                g_desc = e.Description;
                                String sql5 = "INSERT INTO gender (_id, CODE, DESCRIPTION) VALUES ('" + g_id + "', '" + g_code.Replace("'", "''") + "', '" + g_desc.Replace("'", "''") + "')";
                                Crud input5 = new Crud();
                                input5.NonReturn(sql5);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }

                        //========================SIZE===================================================
                        try
                        {
                            foreach (var f in resultData.sizes)
                            {
                                s_id = f.Id;
                                s_code = f.Code;
                                s_desc = f.Description;
                                String sql6 = "INSERT INTO size (_id, CODE, DESCRIPTION) VALUES ('" + s_id + "', '" + s_code.Replace("'", "''") + "', '" + s_desc.Replace("'", "''") + "')";
                                Crud input6 = new Crud();
                                input6.NonReturn(sql6);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }

                        //=======================================================================================
                        String query = "UPDATE log_msg SET Status='Success' WHERE Data = 'Item Dimension' ";
                        Crud update = new Crud();
                        update.NonReturn2(query);
                        MessageBox.Show("Successful Update Data Item Dimension");
                    }
                    else
                    {
                        String query = "UPDATE log_msg SET Status='Failed' WHERE Data = 'Item Dimension' ";
                        Crud update = new Crud();
                        update.NonReturn2(query);

                        response = "Fail";
                        MessageBox.Show("Error API Item Dimension");
                    }

                }
                catch (Exception ex)
                {
                    String query = "UPDATE log_msg SET Status='Failed' WHERE Data = 'Item Dimension' ";
                    Crud update = new Crud();
                    update.NonReturn2(query);

                    //response = ex.ToString();
                    //MessageBox.Show("Make Sure You Are Connected To The Internet");

                    MessageBox.Show(ex.ToString());
                }
            }
        }
        //=============================================================================================
    }
}
