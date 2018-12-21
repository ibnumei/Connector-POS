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

namespace try_consume_api2
{
    public partial class Form_First_Opened : Form
    {
        Connection ckon = new Connection();
        String pesan;
        public Form_First_Opened()
        {
            InitializeComponent();
        }

        //pilih conenctor POS to backend
        private void b_connector_Click(object sender, EventArgs e)
        {
            String coba_a = "coba a"; String coba_b = "coba_b";

            API_CekVersion cek = new API_CekVersion();
            cek.GetVoucher().Wait();
            pesan = cek.cek_ver();
            var pesan2 = cek.cek_ver2(coba_a, coba_b);
            //mengecel apakah jenis aplikasi yang di instal sudah sesuai dengan versi terbaru dari backend, jika tidak maka tidak bisa dibuka

            if (pesan == "Same")
            {
                this.Hide();
                Form_utama fu = new Form_utama();
                fu.ShowDialog();
                this.Close();
            }
            else if (pesan == "NotSame")
            {
                DialogResult dialog = MessageBox.Show("Application Version Needs To Be Updated, the Date of latest version is " + pesan2.Item1 + ".  Please download the latest version. Click OK to open link " + pesan2.Item2, "Version Sistem", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                //System.Diagnostics.Process.Start("www.google.com");
                try
                {
                    if (dialog == DialogResult.OK)
                    {
                        System.Diagnostics.Process.Start(pesan2.Item2);
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                    MessageBox.Show("Please Connect To Internet", "Connect To Internet", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Please Select First Store In POS Connector Application", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //====pilih menu konfigurasi koneksi
        private void b_connection_Click(object sender, EventArgs e)
        {
            this.Hide();
            form_connector con = new form_connector();
            con.ShowDialog();
            this.Close();
        }

        //cek koneksi 
        public void tes_koneksi()
        {
            ckon.con.Close();
            String sql = "SELECT * FROM ARTICLE";
            ckon.cmd = new MySqlCommand(sql, ckon.con);
            try
            {
                ckon.con.Open();
                ckon.myReader = ckon.cmd.ExecuteReader();
                if (ckon.myReader.HasRows)
                {
                    MessageBox.Show("Connection Is Connected");
                }
                else
                {
                    MessageBox.Show("Connection Is Connected");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            ckon.con.Close();

        }

        //form ke load, cek koneksi
        private void Form_First_Opened_Load(object sender, EventArgs e)
        {
            tes_koneksi();
        }


    }
}
