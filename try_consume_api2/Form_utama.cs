using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace try_consume_api2
{
    public partial class Form_utama : Form
    {
        public Form_utama()
        {
            InitializeComponent();
        }

       
        private void b_article1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.ShowDialog();
        }

        private void b_CostCtg1_Click(object sender, EventArgs e)
        {
            W_CostCategory wcost = new W_CostCategory();
            wcost.ShowDialog();
        }

        private void b_inventory1_Click(object sender, EventArgs e)
        {
            W_Inventorycs winv = new W_Inventorycs();
            winv.ShowDialog();
        }

        private void b_transaction_Click(object sender, EventArgs e)
        {
            W_Transaction wtrans = new W_Transaction();
            wtrans.ShowDialog();
        }

        private void b_returnO_Click(object sender, EventArgs e)
        {
            W_Return_order ro = new W_Return_order();
            ro.ShowDialog();
        }

        private void b_requestO_Click(object sender, EventArgs e)
        {
            W_Request_order req_or = new W_Request_order();
            req_or.ShowDialog();
        }

        private void b_mutasiO_Click(object sender, EventArgs e)
        {
            W_MutasiOrder mut_order = new W_MutasiOrder();
            mut_order.ShowDialog();
        }

        private void b_dev_order_Click(object sender, EventArgs e)
        {
            W_DO_Get wdo = new W_DO_Get();
            wdo.ShowDialog();
        }

        private void b_DO_Put_Click(object sender, EventArgs e)
        {
            W_DO_Put do_put = new W_DO_Put();
            do_put.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            W_StoreMaster wstore = new W_StoreMaster();
            wstore.ShowDialog();
        }

        private void b_item_dmns_Click(object sender, EventArgs e)
        {
            W_item_dmns item = new W_item_dmns();
            item.ShowDialog();
        }

        private void Form_utama_Load(object sender, EventArgs e)
        {
            //DateTime dt = DateTime.Now;
            //label11.Text = dt.ToString("yyyy-MM-dd H:mm:ss");   
        }
        //=====================BUTTON TO OPEN CONFIGURASI===========
        private void b_config_Click(object sender, EventArgs e)
        {
            W_Select_Store select = new W_Select_Store();
            select.ShowDialog();
        }
        //==========================================================
        private void b_promotion_Click(object sender, EventArgs e)
        {
            W_Promotion pr = new W_Promotion();
            pr.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            w_store_relasi rel = new w_store_relasi();
            rel.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            w_petty_cash petty = new w_petty_cash();
            petty.ShowDialog();
        }



        private void b_voucher_Click(object sender, EventArgs e)
        {
            W_Voucher vou = new W_Voucher();
            vou.ShowDialog();
        }
        //=======DELETE TRANSAKSI BERDASARKAN BULAN INI, LALU JEDA 2 BULAN KE BELAKANG
        private void b_delete_Click(object sender, EventArgs e)
        {
            Delete_Data delete = new Delete_Data();
            delete.set_bulan_sekarang();
            delete.del_trans();
            delete.del_ro();
            delete.del_mt();
            delete.del_rt();
            delete.del_do();
            delete.del_pc();
            delete.del_shift();
            delete.del_store();
        }

        private void b_masterDiskon_Click(object sender, EventArgs e)
        {
            W_MasterDiskon mas_Diskon = new W_MasterDiskon();
            mas_Diskon.ShowDialog();
        }

        private void b_employee_Click(object sender, EventArgs e)
        {
            //API_Employee epy = new API_Employee();
            //epy.delete();
            //epy.get_cust_id();
            //epy.get_data_employee().Wait();
            w_employee epy = new w_employee();
            epy.ShowDialog();
        }
        

        private void b_bank_Click(object sender, EventArgs e)
        {
            //API_Bank bank = new API_Bank();
            //bank.delete();
            //bank.get_cust_id();
            //bank.get_data_bank().Wait();
            w_bankcs bank = new w_bankcs();
            bank.ShowDialog();
        }

        private void b_SeqNumber_Click(object sender, EventArgs e)
        {
            W_SeqNumber number = new W_SeqNumber();
            number.ShowDialog();
        }

        private void b_seeLog_Click(object sender, EventArgs e)
        {
            w_logGetData log = new w_logGetData();
            log.ShowDialog();
        }

        private void b_pettycash_Click(object sender, EventArgs e)
        {
            W_BudgetPettyCash cash = new W_BudgetPettyCash();
            cash.ShowDialog();
        }

        private void b_UpdateTable_Click(object sender, EventArgs e)
        {
            
            DialogResult dr = MessageBox.Show("This Function Is Used To Change The Data Structure and is Only Used Once", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (dr ==  DialogResult.OK)
            {
                Update_Table alter = new Update_Table();
                alter.UpdateAllTable();
                alter.alter_table_ro();
                alter.alter_table_rt();
                alter.alter_table_mo();

                MessageBox.Show("Structural Changes Have Been Successful", "Successful Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            
            W_TransHistory transhit = new W_TransHistory();
            transhit.ShowDialog();
        }

        private void b_trans_today_Click(object sender, EventArgs e)
        {
            W_TransToday today = new W_TransToday();
            today.ShowDialog();
        }
    }
}
