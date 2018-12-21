using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Xml;

namespace try_consume_api2
{
    public partial class form_connector : Form
    {
        Connection ckon = new Connection();
        public form_connector()
        {
            InitializeComponent();
        }

        private void form_connector_Load(object sender, EventArgs e)
        {
            //txtServer.Text = "localhost";
            //txtNmDb.Text = "biensi_pos_db";
            //txtUser.Text = "root";

            
            txtPass.Text = Properties.Settings.Default.mPassDB;
            //txtOff.Text = Properties.Settings.Default.mLinkApi;
        }

        private void b_save_Click(object sender, EventArgs e)
        {
            String link = "http://mpos.biensicore.co.id";
            //=====change pass database========================
            try
            {
                Properties.Settings.Default.mServer = "localhost";
                Properties.Settings.Default.mDBName = "biensi_pos_db";
                Properties.Settings.Default.mUserDB = "root";
                Properties.Settings.Default.mPassDB = txtPass.Text;
                Properties.Settings.Default.mLinkApi = "http://mpos.biensicore.co.id";
                Properties.Settings.Default.Save();

                XmlDocument doc = new XmlDocument();
                doc.Load("xmlConn.xml");

                XmlNode node = doc.SelectSingleNode("Table/Product/pass_db[1]"); // [index of user node]
                node.InnerText = txtPass.Text;
                XmlNode node2 = doc.SelectSingleNode("Table/Product/link[1]"); // [index of user node]
                node2.InnerText = link;
                doc.Save("xmlConn.xml");

                MessageBox.Show("Connection Successfully Saved. Application Will Be Closed, Please Re-Open", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Restart();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Please Open The Aplication With Right Click, Run As Administrator","Run As Administratror", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Application.Exit();
            }
          

            
        }
        /*
        private void bSaveOff_Click(object sender, EventArgs e)
        {
            String link = txtOff.Text;
            //=====change pass database========================
            try
            {
                Properties.Settings.Default.mLinkApi = link;
                Properties.Settings.Default.Save();

                XmlDocument doc = new XmlDocument();
                doc.Load("xmlConn.xml");

                
                XmlNode node2 = doc.SelectSingleNode("Table/Product/link[1]"); // [index of user node]
                node2.InnerText = link;
                doc.Save("xmlConn.xml");

                MessageBox.Show("Connection Successfully Saved. Application Will Be Closed, Please Re-Open", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Restart();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Please Open The Aplication With Right Click, Run As Administrator", "Run As Administratror", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(ex.ToString());
                Application.Exit();
            }
        }
        //=============================================================
        */
    }
}
