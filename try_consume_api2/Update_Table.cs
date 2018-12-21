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
    class Update_Table
    {
        Connection ckon = new Connection();

        public void alter_table_ro()
        {
            try
            {
                ckon.con.Close();
                String sql = "ALTER TABLE returnorder ADD NO_SJ Varchar(50) NOT NULL DEFAULT '-'";
                Crud update = new Crud();
                update.NonReturn2(sql);
            }
            catch (Exception ex )
            {
                MessageBox.Show("SJ field already exists in Return Order", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void alter_table_rt()
        {
            try
            {
                ckon.con.Close();
                String sql = "ALTER TABLE requestorder ADD NO_SJ Varchar(50) NOT NULL DEFAULT '-'";
                Crud update = new Crud();
                update.NonReturn2(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("SJ field already exists in Request Order", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void alter_table_mo()
        {
            try
            {
                ckon.con.Close();
                String sql = "ALTER TABLE mutasiorder ADD NO_SJ Varchar(50) NOT NULL DEFAULT '-'";
                Crud update = new Crud();
                update.NonReturn2(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("SJ field already exists in Mutation Order", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void UpdateAllTable()
        {
            ckon.con.Close();
            String sql1 = "ALTER TABLE returnorder MODIFY COLUMN REMARK Varchar(200) NOT NULL DEFAULT '-'";
            Crud update = new Crud();
            update.NonReturn2(sql1);
        }
    }
}
