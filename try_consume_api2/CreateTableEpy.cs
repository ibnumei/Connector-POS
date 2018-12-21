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
    class CreateTableEpy
    {
        Connection ckon = new Connection();


        public void del_tabel()
        {
            String sql = "DROP TABLE Employee";
            Crud INPUT = new Crud();
            INPUT.NonReturn2(sql);
        }
        public void create_tabel()
        {
            String query = "CREATE TABLE Employee (_id Bigint (20) NOT NULL DEFAULT 0,EMPLOYEE_ID varchar(50) NOT NULL DEFAULT '-',NAME varchar(50) NOT NULL DEFAULT '-',POSITION_ID Bigint(20) NOT NULL DEFAULT 0,Pass TEXT)";
            Crud INPUT = new Crud();
            INPUT.NonReturn2(query);
        }

        public void insert_table()
        { 
            String sql = "Insert into Employee2 (_id,EMPLOYEE_ID,NAME,POSITION_ID,Pass) values (123,'a123432','ibnu fm',67,'paswordibnubaru')";
            Crud INPUT = new Crud();
            INPUT.NonReturn2(sql);
        }
    }
}
