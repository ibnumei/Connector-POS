using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace try_consume_api2
{
    class Delete_Data
    {
        Connection ckon = new Connection();
        Connection2 ckon2 = new Connection2();
        Connection3 ckon3 = new Connection3();
        String month_now, month_trans;
        //---variable untuk transaksi
        String id_trans;
        //variabel untuk req_order
        String id_ro;
        //variabel untuk mut-order
        String id_mut;
        //VARIABEL UNTUK RET_ORDER
        String id_rt;
        //VARIABEL UNTUK DEL DO
        String id_do;
        //VARIABLE UNTUK PETTY_CASH
        String id_pc;
        //VARIABLE CLOSING SHIFT
        String id_shift;
        //VARIABLE CLOSING STORE
        String id_store;
        public void set_bulan_sekarang()
        {
            DateTime mydate = DateTime.Now;
            month_now = mydate.ToString("MM");
            //MELAKUKAN PERCABANGAN UNTUK MENENTUKAN BULAN DIHAPUS BERDASARKAN BULAN SEKARANG

            if(month_now=="01")
            { month_trans = "11"; }
            else if(month_now=="02")
            { month_trans = "12"; }
            else if(month_now=="03")
            { month_trans = "01"; }
            else if(month_now=="04")
            { month_trans = "02"; }
            else if(month_now=="05")
            { month_trans = "03"; }
            else if (month_now=="06")
            { month_trans = "04"; }
            else if (month_now=="07")
            { month_trans = "05"; }
            else if (month_now=="08")
            { month_trans = "06"; }
            else if (month_now=="09")
            { month_trans = "07"; }
            else if (month_now=="10")
            { month_trans = "08"; }
            else if(month_now=="11")
            { month_trans = "09"; }
            else
            { month_trans = "10"; }
        }

        //PILIH DARI TRANSAKSI DAN HAPUS SESUAI DATA YANG BERHUBUNGAN DENGAN ID TERSEBUT BERDASARKAN month_trans
        public void del_trans()
        {
            String sql = "SELECT * FROM transaction WHERE DATE LIKE '_____"+ month_trans +"%'";
            ckon.con.Open();
            ckon.cmd = new MySqlCommand(sql, ckon.con);
            ckon.myReader = ckon.cmd.ExecuteReader();
            if(ckon.myReader.HasRows)
            {
                while (ckon.myReader.Read())
                {
                    id_trans = ckon.myReader.GetString("TRANSACTION_ID");

                    //FUNGSI DELETE DATA TRANSACTION_LINE SESUAI id_trans
                    String del_trans_line = "DELETE FROM transaction_line WHERE TRANSACTION_ID='" + id_trans + "'";
                    Crud new_del0 = new Crud();
                    new_del0.NonReturn2(del_trans_line);

                    //---FUNGSI DELETE DATA TRANSACTION SESUAI id_trans
                    String del_trans_header = "DELETE FROM transaction WHERE TRANSACTION_ID='" + id_trans + "'";
                    Crud new_del = new Crud();
                    new_del.NonReturn2(del_trans_header);

                    //==FUNGSI DELETE DATA DI inv_line SESUAI id_trans
                    String del_inv_line = "DELETE FROM inventory_line WHERE TRANS_REF_ID= '" + id_trans + "'";
                    Crud new_del1 = new Crud();
                    new_del1.NonReturn2(del_inv_line);
                }
                ckon.con.Close();
            }
            else
            {

            }
        }

        //PILIH DARI REQ_ORDER DAN HAPUS SESUAI DATA YANG BERHUBUNGAN DENGAN ID TERSEBUT BERDASARKAN month_trans
        public void del_ro()
        {
            String sql = "SELECT * FROM requestorder WHERE DATE LIKE '_____" + month_trans + "%'";
            ckon.con.Open();
            ckon.cmd = new MySqlCommand(sql, ckon.con);
            ckon.myReader = ckon.cmd.ExecuteReader();
            if (ckon.myReader.HasRows)
            {
                while (ckon.myReader.Read())
                {
                    id_ro = ckon.myReader.GetString("REQUEST_ORDER_ID");

                    //FUNGSI DELETE DATA REQ_ORDER_LINE SESUAI id_ro
                    String del_ro_line = "DELETE FROM requestorder_line WHERE REQUEST_ORDER_ID='" + id_ro + "'";
                    Crud new_del0 = new Crud();
                    new_del0.NonReturn2(del_ro_line);

                    //---FUNGSI DELETE DATA REQ_ORDER SESUAI id_ro
                    String del_ro_header = "DELETE FROM requestorder WHERE REQUEST_ORDER_ID='" + id_ro + "'";
                    Crud new_del = new Crud();
                    new_del.NonReturn2(del_ro_header);

                    //==FUNGSI DELETE DATA DI inv_line SESUAI id_ro
                    String del_inv_line = "DELETE FROM inventory_line WHERE TRANS_REF_ID= '" + id_ro + "'";
                    Crud new_del1 = new Crud();
                    new_del1.NonReturn2(del_inv_line);
                }
                ckon.con.Close();
            }
            else
            {

            }
        }

        //PILIH DARI MUT_ORDER DAN HAPUS SESUAI DATA YANG BERHUBUNGAN DENGAN ID TERSEBUT BERDASARKAN month_trans
        public void del_mt()
        {
            String sql = "SELECT * FROM mutasiorder WHERE DATE LIKE '_____" + month_trans + "%'";
            ckon.con.Open();
            ckon.cmd = new MySqlCommand(sql, ckon.con);
            ckon.myReader = ckon.cmd.ExecuteReader();
            if (ckon.myReader.HasRows)
            {
                while (ckon.myReader.Read())
                {
                    id_mut = ckon.myReader.GetString("MUTASI_ORDER_ID");

                    //FUNGSI DELETE DATA MUT_ORDER_LINE SESUAI id_mut
                    String del_mut_line = "DELETE FROM mutasiorder_line WHERE MUTASI_ORDER_ID='" + id_mut + "'";
                    Crud new_del0 = new Crud();
                    new_del0.NonReturn2(del_mut_line);

                    //---FUNGSI DELETE DATA MUT_ORDER SESUAI id_mut
                    String del_mut_header = "DELETE FROM mutasiorder WHERE MUTASI_ORDER_ID='" + id_mut + "'";
                    Crud new_del = new Crud();
                    new_del.NonReturn2(del_mut_header);

                    //==FUNGSI DELETE DATA DI inv_line SESUAI id_ro
                    String del_inv_line = "DELETE FROM inventory_line WHERE TRANS_REF_ID= '" + id_mut + "'";
                    Crud new_del1 = new Crud();
                    new_del1.NonReturn2(del_inv_line);
                }
                ckon.con.Close();
            }
            else
            {

            }
        }

        //PILIH DARI RET_ORDER DAN HAPUS SESUAI DATA YANG BERHUBUNGAN DENGAN ID TERSEBUT BERDASARKAN month_trans
        public void del_rt()
        {
            String sql = "SELECT * FROM returnorder WHERE DATE LIKE '_____" + month_trans + "%'";
            ckon.con.Open();
            ckon.cmd = new MySqlCommand(sql, ckon.con);
            ckon.myReader = ckon.cmd.ExecuteReader();
            if (ckon.myReader.HasRows)
            {
                while (ckon.myReader.Read())
                {
                    id_rt = ckon.myReader.GetString("RETURN_ORDER_ID");

                    //FUNGSI DELETE DATA MUT_ORDER_LINE SESUAI id_rT
                    String del_rt_line = "DELETE FROM returnorder_line WHERE RETURN_ORDER_ID='" + id_rt + "'";
                    Crud new_del0 = new Crud();
                    new_del0.NonReturn2(del_rt_line);

                    //---FUNGSI DELETE DATA REQ_ORDER SESUAI id_rT
                    String del_rt_header = "DELETE FROM returnorder WHERE RETURN_ORDER_ID='" + id_rt + "'";
                    Crud new_del = new Crud();
                    new_del.NonReturn2(del_rt_header);

                    //==FUNGSI DELETE DATA DI inv_line SESUAI id_rT
                    String del_inv_line = "DELETE FROM inventory_line WHERE TRANS_REF_ID= '" + id_rt + "'";
                    Crud new_del1 = new Crud();
                    new_del1.NonReturn2(del_inv_line);
                }
                ckon.con.Close();
            }
            else
            {

            }
        }

        //PILIH DARI DEV_ORDER DAN HAPUS SESUAI DATA YANG BERHUBUNGAN DENGAN ID TERSEBUT BERDASARKAN month_trans
        public void del_do()
        {
            String sql = "SELECT * FROM deliveryorder WHERE DATE LIKE '_____" + month_trans + "%'";
            ckon.con.Open();
            ckon.cmd = new MySqlCommand(sql, ckon.con);
            ckon.myReader = ckon.cmd.ExecuteReader();
            if (ckon.myReader.HasRows)
            {
                while (ckon.myReader.Read())
                {
                    id_do = ckon.myReader.GetString("DELIVERY_ORDER_ID");

                    //FUNGSI DELETE DATA DEV_ORDER_LINE SESUAI id_do
                    String del_do_line = "DELETE FROM deliveryorder_line WHERE DELIVERY_ORDER_ID='" + id_do + "'";
                    Crud new_del0 = new Crud();
                    new_del0.NonReturn2(del_do_line);

                    //---FUNGSI DELETE DATA REQ_ORDER SESUAI id_ro
                    String del_do_header = "DELETE FROM deliveryorder WHERE DELIVERY_ORDER_ID='" + id_do + "'";
                    Crud new_del = new Crud();
                    new_del.NonReturn2(del_do_header);

                    //==FUNGSI DELETE DATA DI inv_line SESUAI id_ro
                    String del_inv_line = "DELETE FROM inventory_line WHERE TRANS_REF_ID= '" + id_do + "'";
                    Crud new_del1 = new Crud();
                    new_del1.NonReturn2(del_inv_line);
                }
                ckon.con.Close();
            }
            else
            {

            }
        }

        //PILIH DARI pety_cash DAN HAPUS SESUAI DATA YANG BERHUBUNGAN DENGAN ID TERSEBUT BERDASARKAN month_trans
        public void del_pc()
        {
            String sql = "SELECT * FROM pettycash WHERE DATE LIKE '_____" + month_trans + "%'";
            ckon.con.Open();
            ckon.cmd = new MySqlCommand(sql, ckon.con);
            ckon.myReader = ckon.cmd.ExecuteReader();
            if (ckon.myReader.HasRows)
            {
                while (ckon.myReader.Read())
                {
                    id_pc = ckon.myReader.GetString("PETTY_CASH_ID");

                    //FUNGSI DELETE DATA DEV_ORDER_LINE SESUAI id_do
                    String del_pc_line = "DELETE FROM pettycash_line WHERE PETTY_CASH_ID='" + id_pc + "'";
                    Crud new_del0 = new Crud();
                    new_del0.NonReturn2(del_pc_line);

                    //---FUNGSI DELETE DATA REQ_ORDER SESUAI id_ro
                    String del_pc_header = "DELETE FROM pettycash WHERE PETTY_CASH_ID='" + id_pc + "'";
                    Crud new_del = new Crud();
                    new_del.NonReturn2(del_pc_header);
                }
                ckon.con.Close();
            }
            else
            {

            }
        }

        //======HAPUS CLOSING SHIFT SESUAI month_trans
        public void del_shift()
        {
            String sql = "SELECT * FROM closing_shift WHERE OPENING_TIME LIKE '_____" + month_trans + "%'";
            ckon.con.Open();
            ckon.cmd = new MySqlCommand(sql, ckon.con);
            ckon.myReader = ckon.cmd.ExecuteReader();
            if(ckon.myReader.HasRows)
            {
                while(ckon.myReader.Read())
                {
                    id_shift = ckon.myReader.GetString("_id");

                    String del_shift = "DELETE FROM closing_shift WHERE _id = '" + id_shift + "'";
                    Crud delete = new Crud();
                    delete.NonReturn2(del_shift);
                }
            }
            else
            {

            }
        }

        //======HAPUS CLOSING SHIFT SESUAI month_trans
        public void del_store()
        {
            String sql = "SELECT * FROM closing_store WHERE OPENING_TIME LIKE '_____" + month_trans + "%'";
            ckon.con.Open();
            ckon.cmd = new MySqlCommand(sql, ckon.con);
            ckon.myReader = ckon.cmd.ExecuteReader();
            if (ckon.myReader.HasRows)
            {
                while (ckon.myReader.Read())
                {
                    id_store = ckon.myReader.GetString("_id");

                    String del_store = "DELETE FROM closing_store WHERE _id = '" + id_shift + "'";
                    Crud delete = new Crud();
                    delete.NonReturn2(del_store);
                }
            }
            else
            {

            }
        }
    }
}
