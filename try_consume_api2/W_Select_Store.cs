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
    public partial class W_Select_Store : Form
    {
        Connection ckon = new Connection();
        String code_store, name_store, get_id, get_id2, local_store_code, status_sukses, cust_id_Store, string_count;
        String id, pass, token, device;
        //=======CURRENCY==============
        String cur_name, cur_sign;
        //=======DENOMINATION==========
        int de_cur_id_fk, de_nom;
        //=======STORE=================
        int store_id, store_typeId;
        String store_code, store_name, store_loc, store_add, store_city, store_reg, store_add2, store_add3, store_add4, store_warId, cust_id_store;
        //========WAREHOUSE============
        int war_id;
        String war_code, war_name, war_type, war_add, war_add2, war_add3, war_add4, war_city, war_reg, war_div;
        //===============BANK==========
        String id_bank, nm_bank;
        //=================EMPLOYEE=================
        int e_id, p_id, e_pos;
        String e_plyId, e_name, p_posId, p_posName, e_pass;
        //=======================================================
        public W_Select_Store()
        {
            InitializeComponent();
        }
        //=================================BUTTON SELECT====================================
        private void b_select_Click(object sender, EventArgs e)
        {
            get_id_Store();
            dgv_status.Visible = true;
            //cek();
            compare();
        }

        //===========================LOAD FORM===================================================
        private void W_Select_Store_Load(object sender, EventArgs e)
        {
            dgv_status.Visible = false;
            textBox1.Visible = false;
            get_Store().Wait();
        }

        //=========MEMILIH SEMUA STORE DARI STORE MASTER GET CONDITION 3, DITAMPILKAN KE DROPDOWN==========
        public async Task get_Store()
        {
            LinkSwagger ls = new LinkSwagger();
            String response = "";
            var credentials = new NetworkCredential("username", "password");
            var handler = new HttpClientHandler { Credentials = credentials };
            //var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            using (var client = new HttpClient(handler))
            {
                // Make your request...
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    //HttpResponseMessage message = client.GetAsync("http://retailbiensi.azurewebsites.net/api/StoreMaster?condition=3").Result;
                    HttpResponseMessage message = client.GetAsync(ls.link+"/api/StoreMaster?condition=3").Result;
                    if (message.IsSuccessStatusCode)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(List<StoreRelasi>));
                        var result = message.Content.ReadAsStringAsync().Result;
                        byte[] byteArray = Encoding.UTF8.GetBytes(result);
                        MemoryStream stream = new MemoryStream(byteArray);
                        List<StoreRelasi> resultData = serializer.ReadObject(stream) as List<StoreRelasi>;
                        for (int i = 0; i < resultData.Count; i++)
                        {
                            try
                            {
                                code_store = resultData[i].Code;
                                name_store = resultData[i].Name;
                                //combo_store.Items.Add(code_store+"--"+name_store);
                                combo_store.Items.Add(name_store + "--" + code_store);
                            }
                            catch
                            {

                            }
                        }
                    }
                    else
                    {
                        response = "Fail";
                    }

                }
                catch (Exception ex)
                {
                    //response = ex.ToString();
                    MessageBox.Show("Make Sure You Are Connected To Internet");
                }
            }
        }
        //======================GET DATA ID COMBO================================================
        public void get_id_Store()
        {
            try
            {
                get_id = combo_store.Text;
                //get_id2 = get_id.Substring(0, 3);
                get_id2 = get_id.Substring(get_id.Length - 3);
                //MessageBox.Show(get_id2);
            }
            catch
            { MessageBox.Show("Make Sure You're Connected To The Internet"); }
        }
        //=====================CEK GET ID STORE WITH STORE CODE IN LOCAL DB======================
        public void cek()
        {
           
            ckon.con.Close();
            String sql2 = "SELECT * FROM store";
            ckon.cmd = new MySqlCommand(sql2, ckon.con);
            try
            {
                ckon.con.Open();
                ckon.myReader = ckon.cmd.ExecuteReader();
                if (ckon.myReader.HasRows)
                {
                    while (ckon.myReader.Read())
                    {
                        local_store_code = ckon.myReader.GetString("CODE");
                    }
                }
                else
                { local_store_code = ""; }
                ckon.con.Close();
            }
            catch
            { MessageBox.Show("Make Sure You're Connected To The Local DB"); }
        }
        //==================COMPARE STORE CODE FROM API AND FROM LOCAL DB===========================
        public void compare()
        {

                delete();

                //INSERT DATA FROM API STORE DATA MASTER
                Post_Get_StoreData().Wait();

            
                if(status_sukses=="1")
                {
                    //INSERT INVENTORY===
                    API_Inventory inv = new API_Inventory();
                    inv.get_cust_id();
                    inv.getArticle().Wait();
                    update_tabel();
                    //INSERT PROMOTION====
                    API_Promotion promo = new API_Promotion();
                    promo.get_cust_id();
                    promo.getArticle().Wait();
                    update_tabel();
                    //====INSERT COST CATEGORY===
                    API_Expense expn = new API_Expense();
                    expn.getArticle().Wait();
                    update_tabel();
                    //==INSERT ITEM DIEMNSION==
                    API_Item_Dimension item = new API_Item_Dimension();
                    item.get_Item().Wait();
                    update_tabel();
                    //=====INSERT INTO STORE RELASI===
                    API_StoreRelasi1 relasi = new API_StoreRelasi1();
                    relasi.get_cust_id();
                    relasi.get_Store_relasi().Wait();
                    update_tabel();
                    //====INSERT INTO DISKON SELECTED ITEM
                    API_DiscountSelectedItem sel_item = new API_DiscountSelectedItem();
                    sel_item.get_DiscountSelectedItem().Wait();
                    update_tabel();
                    //====INSERT INTO DISKON RETAIL 
                    API_DiscountRetail disc_retail = new API_DiscountRetail();
                    disc_retail.get_cust_id();
                    disc_retail.get_DiscountRetail().Wait();
                    update_tabel();
                    //====INSERT INTO DISKON RETAIL LINES
                    API_DiscountRetailLines disc_retail_line = new API_DiscountRetailLines();
                    disc_retail_line.cek();
                    //disc_retail_line.get_DiscountRetailLines().Wait();
                    update_tabel();
                    //===========================================
                    API_CustGroup cust_group = new API_CustGroup();
                    cust_group.cek_data_log();
                    cust_group.del_tabel();
                    cust_group.get_cust_group().Wait();
                    //===============================================
                    API_Customer cust = new API_Customer();
                    cust.getCustomer().Wait();
                    update_tabel();
                    //===============================================
                    API_ItemDimensionBrand item_brang = new API_ItemDimensionBrand();
                    item_brang.getItemBrand().Wait();
                    update_tabel();
                    //===============================================
                    API_ItemDimensionColor item_color = new API_ItemDimensionColor();
                    item_color.getItemColor().Wait();
                    update_tabel();
                    //===============================================
                    API_ItemDimensionDepartment item_dept = new API_ItemDimensionDepartment();
                    item_dept.getItemDepartment().Wait();
                    update_tabel();
                    //===============================================
                    API_ItemDimensionDepartmentType item_dept_type = new API_ItemDimensionDepartmentType();
                    item_dept_type.getItemDepartmentType().Wait();
                    update_tabel();
                    //===============================================
                    API_DiscountStoreApi store_api = new API_DiscountStoreApi();
                    store_api.getStore().Wait();
                    update_tabel();
                    //===================================================
                    API_SeqNum2 seq_num = new API_SeqNum2();
                    seq_num.get_cust_id();
                    seq_num.GetSeqNumTrans().Wait();
                    seq_num.GetSeqNumReqOrder().Wait();
                    seq_num.GetSeqNumMutOrder().Wait();
                    seq_num.GetSeqNumRetOrder().Wait();
                    seq_num.GetSeqNumPetCash().Wait();
                    seq_num.GetSeqNumClosingShift().Wait();
                    seq_num.GetSeqNumClosingStore().Wait();
                    update_tabel();
                    //===INSERT ARTICLE====
                    //get_cust_id();
                    //getArticle().Wait();
                    //update_tabel();
                    //INSERT ARTICLE======================
                    API_Article art = new API_Article();
                    art.delete();
                    art.get_cust_id();
                    art.getArticle().Wait();
                    update_tabel();
            }
                else
                {
                    MessageBox.Show("Make Sure You Are Connected To Internet");
                }
               

        }
        //=======================INSERT LOCAL DB FROM API(STORE MASTER)==============================
        public async Task Post_Get_StoreData()
        {
            LinkSwagger ls = new LinkSwagger();
            //id = get_id2;
            //StoreData store = new StoreData()
            //{
            //    userId = id,
            //    password = pass,
            //    token = token,
            //    deviceId = device
            //};
            //var stringPayload = JsonConvert.SerializeObject(store);
            String response = "";
            var credentials = new NetworkCredential("username", "password");
            var handler = new HttpClientHandler { Credentials = credentials };
            //var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            using (var client = new HttpClient(handler))
            {
                //HttpResponseMessage message = client.GetAsync("http://retailbiensi.azurewebsites.net/api/StoreData?storeCode="+get_id2).Result;
                //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/StoreData?storeCode=" + get_id2).Result;
                HttpResponseMessage message = client.GetAsync(ls.link+"/api/StoreData?storeCode=" + get_id2).Result;
                if (message.Content != null)
                {
                    // GET RETURN VALUE FROM POST API
                    var serializer = new DataContractJsonSerializer(typeof(StoreMaster_respone));
                    var responseContent = message.Content.ReadAsStringAsync().Result;
                    byte[] byteArray = Encoding.UTF8.GetBytes(responseContent);
                    MemoryStream stream = new MemoryStream(byteArray);
                    StoreMaster_respone resultData = serializer.ReadObject(stream) as StoreMaster_respone;
                    //====================================INSERT CURRENCY===============================================
                    try
                    {
                        cur_name = resultData.currency.name;
                        cur_sign = resultData.currency.sign;
                        String sql = "INSERT INTO currency (SIGN, NAME) VALUES ('" + cur_sign + "', '" + cur_name + "')";
                        Crud input = new Crud();
                        input.NonReturn(sql);
                        //==================================INSERT DENOMINATION=============================================
                        foreach (var a in resultData.currency.denominations)
                        {
                            //MessageBox.Show(a.nominal.ToString());
                            de_cur_id_fk = a.currencyIdFk;
                            de_nom = a.nominal;
                            String sql1 = "INSERT INTO denomination (CURRENCY_ID_FK, NOMINAL) VALUES ('" + de_cur_id_fk + "', '" + de_nom + "')";
                            Crud input1 = new Crud();
                            input1.NonReturn(sql1);
                        }
                        //================================INSERT STORE=====================================================
                        store_id = resultData.store.Id;
                        store_code = resultData.store.Code;
                        store_name = resultData.store.Name;
                        store_loc = resultData.store.Location;
                        store_add = resultData.store.Address;
                        store_city = resultData.store.City;
                        store_reg = resultData.store.Regional;
                        store_typeId = resultData.store.StoreTypeId;
                        store_add2 = resultData.store.Address2;
                        store_add3 = resultData.store.Address3;
                        store_add4 = resultData.store.Address4;
                        store_warId = resultData.store.WarehouseId;
                        cust_id_store = resultData.store.CustomerIdStore;
                        int budget = resultData.budgetStore.remaining;
                        //String sql2 = "INSERT INTO store (_id, CODE, NAME, LOCATION, ADDRESS, CITY, REGIONAL, STORE_TYPE_ID, ADDRESS2, ADDRESS3, ADDRESS4, WAREHOUSE_ID, CUST_ID_STORE) VALUES ('" + store_id + "', '" + store_code + "', '" + store_name + "', '" + store_loc + "', '" + store_add + "', '" + store_city + "', '" + store_reg + "', '" + store_typeId + "', '" + store_add2 + "', '" + store_add3 + "', '" + store_add4 + "', '" + store_warId + "','" + cust_id_store + "')";
                        String sql2 = "INSERT INTO store (_id, CODE, NAME, LOCATION, ADDRESS, CITY, REGIONAL, STORE_TYPE_ID, ADDRESS2, ADDRESS3, ADDRESS4, WAREHOUSE_ID, CUST_ID_STORE,BUDGET_TO_STORE) VALUES ('" + store_id + "', '" + store_code + "', '" + store_name + "', '" + store_loc + "', '" + store_add + "', '" + store_city + "', '" + store_reg + "', '" + store_typeId + "', '" + store_add2 + "', '" + store_add3 + "', '" + store_add4 + "', '" + store_warId + "','" + cust_id_store + "','"+ budget +"')";
                        Crud input2 = new Crud();
                        input2.NonReturn(sql2);
                        //=================================================INSERT WAREHOUSE=========================================
                        war_id = resultData.warehouse.Id;
                        war_code = resultData.warehouse.Code;
                        war_name = resultData.warehouse.Name;
                        war_type = resultData.warehouse.Type;
                        war_add = resultData.warehouse.Address;
                        war_add2 = resultData.warehouse.Address2;
                        war_add3 = resultData.warehouse.Address3;
                        war_add4 = resultData.warehouse.Address4;
                        war_city = resultData.warehouse.City;
                        war_reg = resultData.warehouse.Regional;
                        war_div = resultData.warehouse.Division;
                        String sql3 = "INSERT INTO warehouse (_id, CODE, NAME, TYPE, ADDRESS, ADDRESS2, ADDRESS3, ADDRESS4, CITY, REGIONAL, DIVISION) VALUES ('" + war_id + "', '" + war_code + "', '" + war_name + "', '" + war_type + "', '" + war_add + "', '" + war_add2 + "', '" + war_add3 + "', '" + war_add4 + "', '" + war_city + "', '" + war_reg + "', '" + war_div + "')";
                        Crud input3 = new Crud();
                        input3.NonReturn(sql3);
                        //==================================BANK
                        foreach (var B in resultData.banks)
                        {
                            //MessageBox.Show(B.bankId + " , " + B.bankName);
                            id_bank = B.bankId;
                            nm_bank = B.bankName;
                            String sql4 = "INSERT INTO bank (BANK_ID, BANK_NAME) VALUES ('" + id_bank + "', '" + nm_bank + "')";
                            Crud input4 = new Crud();
                            input4.NonReturn(sql4);
                        }
                        //MENJALANKAN METHOD MENGGANTI TABEL EPY DENGAN YANG BARU
                        CreateTableEpy epy = new CreateTableEpy();
                        epy.del_tabel();
                        epy.create_tabel();
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

                            String sql6 = "INSERT INTO employee (_id, EMPLOYEE_ID, NAME, POSITION_ID,Pass) VALUES ('" + e_id + "', '" + e_plyId + "', '" + e_name + "', '" + e_pos + "', '"+ e_pass +"')";
                            Crud input6 = new Crud();
                            input6.NonReturn(sql6);
                        }
                        status_sukses = "1";
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        status_sukses = "0";
                        
                    }
                    
                }
                else
                {

                }
                //=================END IF RETURN HAVE VALUE==========
            }
            //=====================END POST VALUE FOR API============
        }
        //===========DELETE DATA DARI LOKAL DB, YG GET DATA NYA MENGGUNAKAN STORE CODE ATAU CUSTOMER ID=====
        public void delete()
        {
            String query_log = "UPDATE log_msg SET Status='0'";
            Crud new_del = new Crud();
            new_del.NonReturn2(query_log);
            //=========DELETE AUTO NUMBER=============
            String query_number = "DELETE FROM auto_number";
            Crud del = new Crud();
            del.NonReturn2(query_number);
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
            //String sql8 = "DELETE FROM employee";
            //Crud input8 = new Crud();
            //input8.NonReturn2(sql8);
            //=========ARTICLE======================
            String sql9 = "DELETE FROM article";
            Crud input9 = new Crud();
            input9.NonReturn2(sql9);
            //==========DO HEADER===================
            String sqla = "DELETE FROM deliveryorder";
            Crud inputa = new Crud();
            inputa.NonReturn2(sqla);
            //==========DO LINE=====================
            String sqlb = "DELETE FROM deliveryorder_line";
            Crud inputb = new Crud();
            inputb.NonReturn2(sqlb);
            //====================PROMOTION===========
            String sqlC = "DELETE FROM promotion";
            Crud inputC = new Crud();
            inputC.NonReturn2(sqlC);
            //=================PROMOTION_LINE==========
            String sqlD = "DELETE FROM promotion_line";
            Crud inputD = new Crud();
            inputD.NonReturn2(sqlD);
            //=============STORE RELASI================
            String sqlE = "DELETE FROM store_relasi";
            Crud inputE = new Crud();
            inputE.NonReturn2(sqlE);
            //=========DISCOUNT ITEM============
            String sqlF = "DELETE FROM discount_item";
            Crud inputF = new Crud();
            inputF.NonReturn2(sqlF);
            //====closing shift==
            String sql00 = "DELETE FROM closing_shift";
            Crud input00 = new Crud();
            input00.NonReturn2(sql00);
            //====closing store
            String sql01 = "DELETE FROM closing_store";
            Crud input01 = new Crud();
            input01.NonReturn2(sql01);
            //====inventory_line====
            String sql02 = "delete from inventory_line";
            Crud input02 = new Crud();
            input02.NonReturn2(sql02);
            //===mutasi order===
            String sql03 = "delete from mutasiorder";
            Crud input03 = new Crud();
            input03.NonReturn2(sql03);
            //===mutasi order line
            String sql04 = "delete from mutasiorder_line";
            Crud input04 = new Crud();
            input04.NonReturn2(sql04);
            //===mutasi order line
            String sql05 = "delete from pettycash";
            Crud input05 = new Crud();
            input05.NonReturn2(sql05);
            //===mutasi order line
            String sql06 = "delete from pettycash_line";
            Crud input06 = new Crud();
            input06.NonReturn2(sql06);
            //===mutasi order line
            String sql07 = "delete from requestorder";
            Crud input07 = new Crud();
            input07.NonReturn2(sql07);
            //===mutasi order line
            String sql08 = "delete from requestorder_line";
            Crud input08 = new Crud();
            input08.NonReturn2(sql08);
            //===mutasi order line
            String sql09 = "delete from returnorder";
            Crud input09 = new Crud();
            input09.NonReturn2(sql09);
            //===mutasi order line
            String sql10 = "delete from returnorder_line";
            Crud input10 = new Crud();
            input10.NonReturn2(sql10);
            //===mutasi order line
            String sql11 = "delete from stock_take";
            Crud input11 = new Crud();
            input11.NonReturn2(sql11);
            //===mutasi order line
            String sql12 = "delete from transaction";
            Crud input12 = new Crud();
            input12.NonReturn2(sql12);
            //===mutasi order line
            String sql13 = "delete from transaction_line";
            Crud input13 = new Crud();
            input13.NonReturn2(sql13);


            //====DELETE COST CATEGORY===
            String sqlG = "DELETE FROM costcategory";
            Crud inputG = new Crud();
            inputG.NonReturn2(sqlG);
            //=========DELETE BRAND===============
            String sqlH = "DELETE FROM brand";
            Crud inputH = new Crud();
            inputH.NonReturn2(sqlH);
            //========DELETE COLOR==================
            String sqlI = "DELETE FROM color";
            Crud inputI = new Crud();
            inputI.NonReturn2(sqlI);
            //=======DELETE DEPARTMENT===============
            String sqlJ = "DELETE FROM departement";
            Crud inputJ = new Crud();
            inputJ.NonReturn2(sqlJ);
            //=========DELETE DEPARTMENT TYPE====================
            String sqlK = "DELETE FROM departementtype";
            Crud inputK = new Crud();
            inputK.NonReturn2(sqlK);
            //=========DELETE GENDER=================
            String sqlL = "DELETE FROM gender";
            Crud inputL = new Crud();
            inputL.NonReturn2(sqlL);
            //==========BANK=============================
            String sqlM = "DELETE FROM size";
            Crud inputM = new Crud();
            inputM.NonReturn2(sqlM);

            String sqlN = "DELETE FROM discountitemselected";
            Crud inpunN = new Crud();
            inpunN.NonReturn2(sqlN);

            String sqlO = "DELETE FROM discountretail";
            Crud inputO = new Crud();
            inputO.NonReturn2(sqlO);

            String sqlP = "DELETE FROM discountretaillines";
            Crud inputP = new Crud();
            inputP.NonReturn2(sqlP);

            String sqlQ = "DELETE FROM customer";
            Crud delQ = new Crud();
            delQ.NonReturn2(sqlQ);

            String sqlR = "DELETE FROM itemdimensionbrand";
            Crud delR = new Crud();
            delR.NonReturn2(sqlR);

            String sqlT = "DELETE FROM itemdimensioncolor";
            Crud delT = new Crud();
            delT.NonReturn2(sqlT);

            String sqlU = "DELETE FROM itemdimensiondepartment";
            Crud delU = new Crud();
            delU.NonReturn2(sqlU);

            String sqlV = "DELETE FROM itemdimensiondepartmenttype";
            Crud delV = new Crud();
            delV.NonReturn2(sqlV);


            String sqlW = "DELETE FROM discountstore";
            Crud delW = new Crud();
            delW.NonReturn2(sqlW);
        }
        //=======================================================================================================
        public void update_tabel()
        {
            ckon.con.Close();
            dgv_status.Rows.Clear();
            String sql = "select * from log_msg";
            ckon.cmd = new MySqlCommand(sql, ckon.con);
            ckon.con.Open();
            ckon.myReader = ckon.cmd.ExecuteReader();
            while(ckon.myReader.Read())
            {
                int n = dgv_status.Rows.Add();
                dgv_status.Rows[n].Cells[0].Value = ckon.myReader.GetString("Data");
                dgv_status.Rows[n].Cells[1].Value = ckon.myReader.GetString("Status");
            }
            ckon.dt.Rows.Clear();
            ckon.con.Close();
        }
        //================GET CUSTOMER ID STORE
        public void get_cust_id()
        {
            ckon.con.Close();
            String sql = "SELECT * FROM store";
            ckon.cmd = new MySqlCommand(sql, ckon.con);
            ckon.con.Open();
            ckon.myReader = ckon.cmd.ExecuteReader();
            while (ckon.myReader.Read())
            {
                cust_id_Store = ckon.myReader.GetString("CUST_ID_STORE");
            }
            ckon.con.Close();
        }
        public async Task getArticle()
        {
            LinkSwagger ls = new LinkSwagger();
            textBox1.Visible = true;
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
                    //HttpResponseMessage message = client.GetAsync("http://retailbiensi.azurewebsites.net/api/Article?customerCode=" + cust_id_Store).Result;
                    //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/Article?customerCode=" + cust_id_Store).Result;
                    HttpResponseMessage message = client.GetAsync(ls.link+"/api/Article?customerCode=" + cust_id_Store).Result;
                    if (message.IsSuccessStatusCode)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(List<Article>));
                        var result = message.Content.ReadAsStringAsync().Result;
                        byte[] byteArray = Encoding.UTF8.GetBytes(result);
                        MemoryStream stream = new MemoryStream(byteArray);
                        List<Article> resultData = serializer.ReadObject(stream) as List<Article>;
                        int count = 0;
                        int a = resultData.Count();
                        //MessageBox.Show(a.ToString());
                        string_count = a.ToString();
                        for (int i = 0; i < resultData.Count; i++)
                        {
                            try
                            {
                                String sql = "INSERT INTO article (_id ,ARTICLE_ID, ARTICLE_NAME, BRAND, GENDER, DEPARTMENT, DEPARTMENT_TYPE, SIZE, COLOR, UNIT, PRICE, ARTICLE_ID_ALIAS) VALUES ('" + resultData[i].id + "','" + resultData[i].articleId + "', '" + resultData[i].articleName + "', '" + resultData[i].brand + "', '" + resultData[i].gender + "', '" + resultData[i].department + "', '" + resultData[i].departmentType + "', '" + resultData[i].size + "', '" + resultData[i].color + "', '" + resultData[i].unit + "', '" + resultData[i].price + "', '" + resultData[i].articleIdAlias + "')";
                                Crud input = new Crud();
                                input.NonReturn2(sql);
                                count = count + 1;
                                //label1.Text = count.ToString();
                                //label1.Text = string.Format("Processing...{0}/{1}", count,string_count);
                                // MessageBox.Show("Jumlah Data = "+ count.ToString());
                                textBox1.AppendText("Data Article - " + count.ToString() + "/ " + string_count + "\n");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }
                        String query = "UPDATE log_msg SET Status='Success' WHERE Data = 'Article' ";
                        Crud update = new Crud();
                        update.NonReturn2(query);
                        MessageBox.Show("Successful Update Data Article");
                    }
                    else
                    {
                        response = "Fail";
                        MessageBox.Show("Error API Article");
                    }

                }
                catch (Exception ex)
                {
                    String query = "UPDATE log_msg SET Status='Failed' WHERE Data = 'Article' ";
                    Crud update = new Crud();
                    update.NonReturn2(query);

                    response = ex.ToString();
                    MessageBox.Show("Make Sure You Are Connected To The Internet");
                }
            }
        }
        //===============================================================================================================
    }
}
