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
    public partial class W_Transaction : Form
    {
        Connection ckon1 = new Connection();
        Connection2 ckon2 = new Connection2();
        Connection3 ckon3 = new Connection3();
        
        //========================VARIABLE FOR TRANSACTION HEADER========================================
        public String st_code2, custid2, date2, employe2, recepid2, spgid2, time2, timestamp2, transid2, cust_id_store2, nm_cur2, bank1, bank2, ref1, ref2;
        public int cash2, change2, diskon2, edc2a, edc2b, id2, pay_type2, status2, total2, articleid2;
        //========================VARIABLE FOR TRANSACTION LINE =========================================
        public String trans_line2, dis_code;
        public int id_line2, price_line2, qty_line2, sub_line2, trans_lineFk2, dis_type;
        //========================VARIABLE FOR ARTICLE ======== =========================================
        String id_from_article2, articleName2, brand2, color2, department2, dept_type2, gender2, size2, unit2;
        int id_article2, price_article2;
        //===============================================================================================
        String real_article_id;
        public W_Transaction()
        {
            InitializeComponent();
        }
        public async Task Post_article()
        {
            Transaction trans_new2 = new Transaction();
            trans_new2.transactionLines = new List<TransactionLine>();
            String sql = "SELECT * FROM transaction WHERE STATUS_API=0";
            ckon1.cmd = new MySqlCommand(sql, ckon1.con);
            ckon1.con.Open();
            ckon1.myReader = ckon1.cmd.ExecuteReader();
            if (ckon1.myReader.HasRows)
            {
                while (ckon1.myReader.Read())
                {
                    //AMBIL NILAI DARI TRANSACTION HEADER DISINI
                    transid2 = ckon1.myReader.GetString("TRANSACTION_ID");
                    trans_lineFk2 = ckon1.myReader.GetInt32("_id");
                    st_code2 = ckon1.myReader.GetString("STORE_CODE");
                    cash2 = ckon1.myReader.GetInt32("CASH");
                    change2 = ckon1.myReader.GetInt32("CHANGEE");
                    custid2 = ckon1.myReader.GetString("CUSTOMER_ID");
                    date2 = ckon1.myReader.GetString("DATE");
                    diskon2 = ckon1.myReader.GetInt32("DISCOUNT");
                    edc2a = ckon1.myReader.GetInt32("EDC");
                    edc2b = ckon1.myReader.GetInt32("EDC2");
                    bank1 = ckon1.myReader.GetString("BANK_NAME");
                    bank2 = ckon1.myReader.GetString("BANK_NAME2");
                    ref1 = ckon1.myReader.GetString("NO_REF");
                    ref2 = ckon1.myReader.GetString("NO_REF2");
                    //employe2 = ckon1.myReader.GetString("EMPLOYEE_ID");
                    id2 = ckon1.myReader.GetInt32("_id");
                    pay_type2 = ckon1.myReader.GetInt32("PAYMENT_TYPE");
                    recepid2 = ckon1.myReader.GetString("RECEIPT_ID");
                    spgid2 = ckon1.myReader.GetString("SPG_ID");
                    status2 = ckon1.myReader.GetInt32("STATUS");
                    time2 = ckon1.myReader.GetString("TIME");
                    timestamp2 = ckon1.myReader.GetString("TIME_STAMP");
                    total2 = ckon1.myReader.GetInt32("TOTAL");
                    transid2 = ckon1.myReader.GetString("TRANSACTION_ID");
                    cust_id_store2 = ckon1.myReader.GetString("CUST_ID_STORE");
                    nm_cur2 = ckon1.myReader.GetString("CURRENCY");
                    //=====================SEARCH BY TRANSACTION_ID======================================
                    String sql2 = "SELECT * FROM transaction_line WHERE TRANSACTION_ID='" + transid2 + "'";
                    ckon2.cmd2 = new MySqlCommand(sql2, ckon2.con2);
                    ckon2.con2.Open();
                    ckon2.myReader2 = ckon2.cmd2.ExecuteReader();
                    while (ckon2.myReader2.Read())
                    {
                        //AMBIL NILAI DARI TRANSACTION LINE DISINI BY ID TRANSACTION
                        real_article_id = ckon2.myReader2.GetString("ARTICLE_ID");

                        //=============SEARCH DATA ARTICLE BY ARTICLE ID================================
                        String sql3 = "SELECT * FROM article WHERE ARTICLE_ID='" + real_article_id + "'";
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
                        //===============================END OF ARTICLE DATA============================
                        ckon3.con3.Close();
                        articleid2 = id_article2;
                        id_line2 = ckon2.myReader2.GetInt32("_id");
                        price_line2 = ckon2.myReader2.GetInt32("PRICE");
                        qty_line2 = ckon2.myReader2.GetInt32("QUANTITY");
                        sub_line2 = ckon2.myReader2.GetInt32("SUBTOTAL");
                        trans_line2 = ckon2.myReader2.GetString("TRANSACTION_ID");
                        dis_code = ckon2.myReader2.GetString("DISCOUNT_CODE");
                        dis_type = ckon2.myReader2.GetInt32("DISCOUNT_TYPE");

                        TransactionLine trans_linee = new TransactionLine()
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
                            articleIdFk = articleid2,
                            id = id_line2,
                            price = price_line2,
                            quantity = qty_line2,
                            subtotal = sub_line2,
                            transactionId = trans_line2,
                            transactionIdFk = trans_lineFk2,
                            discountCode = dis_code,
                            discountType = dis_type
                        };

                        //trans_new2.transactionLines = new List<TransactionLine>();
                        trans_new2.transactionLines.Add(trans_linee);
                    }
                    ckon2.con2.Close();

                    //=====================END OF TRANSACTION LINE    ===================================
                    Transaction trans_new = new Transaction()
                    {
                        storeCode = st_code2,
                        cash = cash2,
                        change = change2,
                        customerIdStore = cust_id_store2,
                        currency = nm_cur2,
                        date = date2,
                        discount = diskon2,
                        id = id2,
                        paymentType = pay_type2,
                        receiptId = recepid2,
                        spgId = spgid2,
                        status = status2,
                        time = time2,
                        timeStamp = timestamp2,
                        total = total2,
                        Edc1 = edc2a,
                        Edc2 = edc2b,
                        Bank1 = bank1,
                        Bank2 = bank2,
                        NoRef1 = ref1,
                        NoRef2 = ref2,
                        transactionType = pay_type2,                        
                        //employeeId = employe2,
                        transactionId = transid2,
                        transactionLines = trans_new2.transactionLines
                    };
                    var stringPayload = JsonConvert.SerializeObject(trans_new);
                    String response = "";
                    var credentials = new NetworkCredential("username", "password");
                    var handler = new HttpClientHandler { Credentials = credentials };
                    var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                    using (var client = new HttpClient(handler))
                    {
                        try
                        {
                            HttpResponseMessage message = client.PostAsync("http://retailbiensi.azurewebsites.net/api/Transaction", httpContent).Result;
                            String query = "UPDATE transaction SET STATUS_API='1' WHERE TRANSACTION_ID='" + transid2 + "'";
                            Crud input = new Crud();
                            input.NonReturn2(query);
                        }
                        catch 
                        {

                        }
                        
                    }

                }
                //==============================END OF TRANSACTION HEADER================================
            }
            ckon1.con.Close(); 
            //====================================END OF IF QUERY HAS DATA=================================================

        }
        //============================================================================================================================
        private void button1_Click(object sender, EventArgs e)
        {
            API_Transaction trans = new API_Transaction();
            trans.Post_article().Wait();
            //MessageBox.Show("Data Has Been Sent to Backend Database");
        }
        //============================================================================================================================
        private void W_Transaction_Load(object sender, EventArgs e)
        {


        }
        //============================================================================================================================
        private void b_reload_Click(object sender, EventArgs e)
        {
            API_Transaction trans = new API_Transaction();
            trans.Post_article().Wait();
        }
    }
}
