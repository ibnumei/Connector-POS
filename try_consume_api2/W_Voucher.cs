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
    public partial class W_Voucher : Form
    {
        public W_Voucher()
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
                    HttpResponseMessage message = client.GetAsync("http://retailbiensi.azurewebsites.net/api/Voucher?VoucherCode=aaa").Result;

                    if (message.IsSuccessStatusCode)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(Voucher));
                        var result = message.Content.ReadAsStringAsync().Result;
                        byte[] byteArray = Encoding.UTF8.GetBytes(result);
                        MemoryStream stream = new MemoryStream(byteArray);
                        Voucher resultData = serializer.ReadObject(stream) as Voucher;

                        String code = resultData.VoucherCode;
                        MessageBox.Show(code + "");
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

        private void button1_Click(object sender, EventArgs e)
        {
            getArticle().Wait();
        }
    }
}
