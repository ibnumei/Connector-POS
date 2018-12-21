using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace try_consume_api2
{
    //PASSWORD UNTUK DATABASE BIENSI ="qwerty"

    //class LinkSwagger
    //{
    //    //public string link = "http://mpos.biensicore.co.id";
    //    public string link = Properties.Settings.Default.mLinkApi;
    //}

    class Connection
    {
        //public static string conString = "Server='"+ dynamic_koneksi.Properties.Settings.Default.mServer +"';Database='"+ dynamic_koneksi.Properties.Settings.Default.mDBName +"';Uid='"+ dynamic_koneksi.Properties.Settings.Default.mUserDB +"';Pwd='"+ dynamic_koneksi.Properties.Settings.Default.mPassDB +"';";
        //public static string conString = "Server=localhost;Database=biensi_pos_db;Uid=root;Pwd=;";
        public static string conString = "Server='" + try_consume_api2.Properties.Settings.Default.mServer + "';Database='" + try_consume_api2.Properties.Settings.Default.mDBName + "';Uid='" + try_consume_api2.Properties.Settings.Default.mUserDB + "';Pwd='" + try_consume_api2.Properties.Settings.Default.mPassDB + "';";
        public MySqlConnection con = new MySqlConnection(conString);
        public MySqlCommand cmd;
        public MySqlDataAdapter adapter;
        public DataTable dt = new DataTable();
        public DataSet ds = new DataSet();
        public MySqlDataReader myReader;
    }

    class Connection2
    {
        public static string conString2 = "Server='" + try_consume_api2.Properties.Settings.Default.mServer + "';Database='" + try_consume_api2.Properties.Settings.Default.mDBName + "';Uid='" + try_consume_api2.Properties.Settings.Default.mUserDB + "';Pwd='" + try_consume_api2.Properties.Settings.Default.mPassDB + "';";
        public MySqlConnection con2 = new MySqlConnection(conString2);
        public MySqlCommand cmd2;
        public MySqlDataAdapter adapter2;
        public DataTable dt2 = new DataTable();
        public DataSet ds2 = new DataSet();
        public MySqlDataReader myReader2;
    }

    class Connection3
    {
        public static string conString3 = "Server='" + try_consume_api2.Properties.Settings.Default.mServer + "';Database='" + try_consume_api2.Properties.Settings.Default.mDBName + "';Uid='" + try_consume_api2.Properties.Settings.Default.mUserDB + "';Pwd='" + try_consume_api2.Properties.Settings.Default.mPassDB + "';";
        public MySqlConnection con3 = new MySqlConnection(conString3);
        public MySqlCommand cmd3;
        public MySqlDataAdapter adapter3;
        public DataTable dt3 = new DataTable();
        public DataSet ds3 = new DataSet();
        public MySqlDataReader myReader3;
    }

    class Connection4
    {
        public static string conString4 = "Server='" + try_consume_api2.Properties.Settings.Default.mServer + "';Database='" + try_consume_api2.Properties.Settings.Default.mDBName + "';Uid='" + try_consume_api2.Properties.Settings.Default.mUserDB + "';Pwd='" + try_consume_api2.Properties.Settings.Default.mPassDB + "';";
        public MySqlConnection con4 = new MySqlConnection(conString4);
        public MySqlCommand cmd4;
        public MySqlDataAdapter adapter4;
        public DataTable dt4 = new DataTable();
        public DataSet ds4 = new DataSet();
        public MySqlDataReader myReader4;
    }

}
