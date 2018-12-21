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
using System.Security;

namespace try_consume_api2
{
    public partial class w_employee : Form
    {
        Connection ckon = new Connection();
        String passwordHash2 = "0x69727387F4A1F61EFA8D667059C2F53355377B98A93A4ADEEFC4981FAA84D417F299E4DEDA046CDA553B7F5FFF94235FD17B04C423415A264FB805DD9F1435D8";
        String passwordSalt2 = "0x5FA6914AFB44C684CA2653DDC9BA64B7679FE52419E034CB915965948CE39E82818AE5622FF6C1E7850437F6DB70942D65E444E0B5D27DAAA4725885B1C4F2E800FA126663AE601ECB7A3EBC7DAC434AC3D8434FEACB2F3AA8EAE8D430C97D3D116966B2C63885B3AADFF7BC548F7DE642AEC622B4B5D53719DC26D33B7AC4F7";
        public w_employee()
        {
            InitializeComponent();
        }
        byte[] passwordHash, passwordSalt;

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
         

        private void b_reload_Click(object sender, EventArgs e)
        {
            //CreateTableEpy epy = new CreateTableEpy();
            //epy.del_tabel();
            //epy.create_tabel();

            API_Employee epy2 = new API_Employee();
            epy2.delete();
            epy2.get_cust_id();
            epy2.get_data_employee().Wait();

        }

        public void get_info()
        {
            ckon.con.Close();
            String sql = "Select * from employee";
            ckon.cmd = new MySqlCommand(sql, ckon.con);
            ckon.con.Open();
            ckon.myReader = ckon.cmd.ExecuteReader();

            if (ckon.myReader.Read())
            {
                try
                {
                    String a = ckon.myReader.GetString("PassHash");
                    MessageBox.Show(a);
                    String query = " ALTER TABLE employee ADD PassHash Text Not Null default '-', PassSalt Text Not Null default '-'";
                }
                catch
                {
                    MessageBox.Show("Field belum ada");
                }
                //store = ckon.myReader.GetString("CODE");
            }
            ckon.con.Close();

        }


        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            // if (password == null) return false;
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }
    }
}
