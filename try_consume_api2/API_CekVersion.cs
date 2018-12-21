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
    class API_CekVersion
    {
        LinkSwagger ls = new LinkSwagger();
        String version_web, version_apk, message, tgl_update, url_donlod;
        //======AMBIL DATA DARI API
        public async Task GetVoucher()
        {
            String response = "";
            var credentials = new NetworkCredential("username", "password");
            var handler = new HttpClientHandler { Credentials = credentials };
            using (var client = new HttpClient(handler))
            {
                // Make your request...
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    //HttpResponseMessage message = client.GetAsync("http://mpos.biensicore.co.id/api/AppVersions?app=conn").Result;
                    HttpResponseMessage message = client.GetAsync(ls.link+"/api/AppVersions?app=conn").Result;
                    try
                    {
                        if (message.IsSuccessStatusCode)
                        {
                            var serializer = new DataContractJsonSerializer(typeof(api_version));
                            var result = message.Content.ReadAsStringAsync().Result;
                            byte[] byteArray = Encoding.UTF8.GetBytes(result);
                            MemoryStream stream = new MemoryStream(byteArray);
                            api_version resultData = serializer.ReadObject(stream) as api_version;
                            //==masukan daat ke dalam variable
                            version_web = resultData.Version;
                            tgl_update = resultData.TanggalUpdate;
                            url_donlod = resultData.UrlDownload;
                            //MessageBox.Show(code.ToString());

                        }
                        else
                        {
                            response = "Fail";
                        }
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.ToString());
                    }

                }
                catch (Exception ex)
                {
                    response = ex.ToString();
                }
            }
        }
        //=========================================================================================

        //=========METHOD CEK VERSION DARI CLASS API_CEKVERSION==============================
        public string cek_ver()
        {
            version_apk = Properties.Settings.Default.mVersion;


            if (version_apk == version_web)
            {
                message = "Same";
                //message = "The Application Version Is up to date";
            }
            else
            {
                message = "NotSame";
                //message = "Application Version Needs To Be Updated";

            }



            return message;
        }
        //===================================================================================
        public Tuple<String, String> cek_ver2(String version_apk3, String version_web3)
        {
            version_apk = Properties.Settings.Default.mVersion;


            if (version_apk == version_web)
            {
                message = "Same";
                //message = "The Application Version Is up to date";
            }
            else
            {
                message = "NotSame";
                //message = "Application Version Needs To Be Updated";

            }

            return new Tuple<string, string>(tgl_update, url_donlod);
        }
    }
}
