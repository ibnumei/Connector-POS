using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace try_consume_api2
{
    class DeleteData
    {
        //====CLASS KONEKSI===
        Connection ckon = new Connection();
        Connection2 ckon2 = new Connection2();
        //=====VARIABLE FOR TRANSACTION======

        //=====METHOD FOR DELETE TRANSACTION======
        public void delete_trans(String id_date)
        {
            String sql = "SELECT * FROM transaction WHERE DATE LIKE '_____'" + id_date + "'%'";
            ckon.cmd = new MySqlCommand(sql, ckon.con);
            ckon.con.Open();

        }
    }
}
