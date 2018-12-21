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
    public partial class W_StoreMaster : Form
    {
        Connection4 ckon4 = new Connection4();
        String id, pass, token, device, id_store;
        //=======CURRENCY==============
        String cur_name, cur_sign;
        //=======DENOMINATION==========
        int de_cur_id_fk, de_nom;
        //=======STORE=================
        int store_id, store_typeId;
        String store_code, store_name, store_loc, store_add, store_city, store_reg, store_add2, store_add3, store_add4, store_warId;
        //========WAREHOUSE============
        int war_id;
        String war_code, war_name, war_type, war_add, war_add2, war_add3, war_add4, war_city, war_reg, war_div;
        //===============BANK==========
        String id_bank, nm_bank;
        //=================EMPLOYEE=================
        int e_id, p_id, e_pos;
        String e_plyId, e_name, p_posId, p_posName;
        //=======================================================
        private void button1_Click(object sender, EventArgs e)
        {
            //delete();
            API_StoreData store = new API_StoreData();
            
            store.id = textBox1.Text;
            store.store_id_master = textBox2.Text;
            store.delete();
            store.Post_Get_StoreData().Wait();
            //===============GET DATA ARTICLE================
            API_Article article = new API_Article();
            article.get_cust_id();
            article.getArticle().Wait();
            //===============DO GET=========================
            API_DO_Get DO_GET = new API_DO_Get();
            DO_GET.get_cust_id();
            DO_GET.getArticle().Wait();
            //===============INVENTORY=======================
            API_Inventory INV = new API_Inventory();
            INV.get_cust_id();
            INV.getArticle().Wait();
            //====================PROMOTION================
            API_Promotion promo = new API_Promotion();
            promo.get_cust_id();
            promo.getArticle().Wait();
            //================STORE RELASI==================
            API_StoreRelasi1 relasi = new API_StoreRelasi1();
            relasi.get_cust_id();
            relasi.get_Store_relasi().Wait();
            MessageBox.Show("Data has been sent to local database");
            this.Close();
        }

        public W_StoreMaster()
        {
            InitializeComponent();
        }

        //public async Task Post_Get_StoreData()
        //{
        //    //id = textBox1.Text;
        //    id = "EM-001";
        //    StoreData store = new StoreData()
        //    {
        //        userId = id,
        //        password = pass,
        //        token = token,
        //        deviceId = device,
        //        storeId = id_store
        //    };
        //    var stringPayload = JsonConvert.SerializeObject(store);
        //    String response = "";
        //    var credentials = new NetworkCredential("username", "password");
        //    var handler = new HttpClientHandler { Credentials = credentials };
        //    var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
        //    using (var client = new HttpClient(handler))
        //    {
        //        HttpResponseMessage message = client.PostAsync("http://retailbiensi.azurewebsites.net/api/StoreData", httpContent).Result;
        //        if (message.Content != null)
        //        {
        //            // GET RETURN VALUE FROM POST API
        //            var serializer = new DataContractJsonSerializer(typeof(StoreMaster_respone));
        //            var responseContent = message.Content.ReadAsStringAsync().Result;
        //            byte[] byteArray = Encoding.UTF8.GetBytes(responseContent);
        //            MemoryStream stream = new MemoryStream(byteArray);
        //            StoreMaster_respone resultData = serializer.ReadObject(stream) as StoreMaster_respone;
        //            //====================================INSERT CURRENCY===============================================
        //            cur_name = resultData.currency.name;
        //            cur_sign = resultData.currency.sign;
        //            String sql = "INSERT INTO currency (SIGN, NAME) VALUES ('" + cur_sign + "', '" + cur_name + "')";
        //            Crud input = new Crud();
        //            input.NonReturn(sql);
        //            //==================================INSERT DENOMINATION=============================================
        //            foreach (var a in resultData.currency.denominations)
        //            {
        //                //MessageBox.Show(a.nominal.ToString());
        //                de_cur_id_fk = a.currencyIdFk;
        //                de_nom = a.nominal;
        //                String sql1 = "INSERT INTO denomination (CURRENCY_ID_FK, NOMINAL) VALUES ('" + de_cur_id_fk + "', '" + de_nom + "')";
        //                Crud input1 = new Crud();
        //                input1.NonReturn(sql1);
        //            }
        //            //================================INSERT STORE=====================================================
        //            store_id = resultData.store.Id;
        //            store_code = resultData.store.Code;
        //            store_name = resultData.store.Name;
        //            store_loc = resultData.store.Location;
        //            store_add = resultData.store.Address;
        //            store_city = resultData.store.City;
        //            store_reg = resultData.store.Regional;
        //            store_typeId = resultData.store.StoreTypeId;
        //            store_add2 = resultData.store.Address2;
        //            store_add3 = resultData.store.Address3;
        //            store_add4 = resultData.store.Address4;
        //            store_warId = resultData.store.WarehouseId;
        //            String sql2 = "INSERT INTO store (_id, CODE, NAME, LOCATION, ADDRESS, CITY, REGIONAL, STORE_TYPE_ID, ADDRESS2, ADDRESS3, ADDRESS4, WAREHOUSE_ID) VALUES ('" + store_id + "', '" + store_code + "', '" + store_name + "', '" + store_loc + "', '" + store_add + "', '" + store_city + "', '" + store_reg + "', '" + store_typeId + "', '" + store_add2 + "', '" + store_add3 + "', '" + store_add4 + "', '" + store_warId + "')";
        //            Crud input2 = new Crud();
        //            input2.NonReturn(sql2);
        //            //=================================================INSERT WAREHOUSE=========================================
        //            war_id = resultData.warehouse.Id;
        //            war_code = resultData.warehouse.Code;
        //            war_name = resultData.warehouse.Name;
        //            war_type = resultData.warehouse.Type;
        //            war_add = resultData.warehouse.Address;
        //            war_add2 = resultData.warehouse.Address2;
        //            war_add3 = resultData.warehouse.Address3;
        //            war_add4 = resultData.warehouse.Address4;
        //            war_city = resultData.warehouse.City;
        //            war_reg = resultData.warehouse.Regional;
        //            war_div = resultData.warehouse.Division;
        //            String sql3 = "INSERT INTO warehouse (_id, CODE, NAME, TYPE, ADDRESS, ADDRESS2, ADDRESS3, ADDRESS4, CITY, REGIONAL, DIVISION) VALUES ('" + war_id + "', '" + war_code + "', '" + war_name + "', '" + war_type + "', '" + war_add + "', '" + war_add2 + "', '" + war_add3 + "', '" + war_add4 + "', '" + war_city + "', '" + war_reg + "', '" + war_div + "')";
        //            Crud input3 = new Crud();
        //            input3.NonReturn(sql3);
        //            //==================================BANK
        //            foreach (var B in resultData.banks)
        //            {
        //                //MessageBox.Show(B.bankId + " , " + B.bankName);
        //                id_bank = B.bankId;
        //                nm_bank = B.bankName;
        //                String sql4 = "INSERT INTO bank (BANK_ID, BANK_NAME) VALUES ('" + id_bank + "', '" + nm_bank + "')";
        //                Crud input4 = new Crud();
        //                input4.NonReturn(sql4);
        //            }
        //            //=============EMPLOYEE================
        //            foreach (var c in resultData.employees)
        //            {
        //                //MessageBox.Show(c.id + " ," + c.name + " ," + c.employeeId+" ,"+c.possition.possitionName+" ,"+c.possition.id+" ,"+c.possition.possitionId);
        //                e_id = c.id;
        //                e_name = c.name;
        //                e_plyId = c.employeeId;
        //                e_pos = c.possition.id;
        //                p_id = c.possition.id;
        //                p_posName = c.possition.possitionName;
        //                p_posId = c.possition.possitionId;
        //                String sql5 = "INSERT INTO position (_id, POSITION_ID, POSITION_NAME) VALUES ('" + p_id + "', '" + p_posId + "', '" + p_posName + "')";
        //                Crud input5 = new Crud();
        //                input5.NonReturn(sql5);

        //                String sql6 = "INSERT INTO employee (_id, EMPLOYEE_ID, NAME, POSITION_ID) VALUES ('" + e_id + "', '" + e_plyId + "', '" + e_name + "', '" + e_pos + "')";
        //                Crud input6 = new Crud();
        //                input6.NonReturn(sql6);
        //            }

        //        }
        //        //=================END IF RETURN HAVE VALUE==========
        //    }
        //    //=====================END POST VALUE FOR API============
        //}

        //==============================================================================================

        //======================================DELETE DATA BEFORE GET FROM API==================================
        public void delete()
        {
            //=========DELETE INVENTORY===============
            String sql = "DELETE FROM inventory";
            Crud input = new Crud();
            input.NonReturn2(sql);
            //========DELETE CURRENCY==================
            String sql2 = "DELETE FROM currency";
            Crud input2 = new Crud();
            input2.NonReturn2(sql2);
            //=======DELETE DENOMINATION===============
            String sql3 = "DELETE FROM denomination";
            Crud input3 = new Crud();
            input3.NonReturn2(sql3);
            //=========DELETE STORE====================
            String sql4 = "DELETE FROM store";
            Crud input4 = new Crud();
            input4.NonReturn2(sql4);
            //=========DELETE WAREHOUSE=================
            String sql5 = "DELETE FROM warehouse";
            Crud input5 = new Crud();
            input5.NonReturn2(sql5);
            //==========BANK=============================
            String sql6 = "DELETE FROM bank";
            Crud input6 = new Crud();
            input6.NonReturn2(sql6);
            //==============POSITION=====================
            String sql7 = "DELETE FROM position";
            Crud input7 = new Crud();
            input7.NonReturn2(sql7);
            //=========EMPLOYEE====================
            String sql8 = "DELETE FROM employee";
            Crud input8 = new Crud();
            input8.NonReturn2(sql8);
        }
        //=======================================================================================================
    }
}
