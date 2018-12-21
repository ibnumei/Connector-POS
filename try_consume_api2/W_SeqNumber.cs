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
    public partial class W_SeqNumber : Form
    {
        public W_SeqNumber()
        {
            InitializeComponent();
        }

        private void b_reload_Click(object sender, EventArgs e)
        {
            //API_SequenceNumber seq_num = new API_SequenceNumber();

            //seq_num.get_cust_id();
            //seq_num.GetSeqNumTrans().Wait();

            //seq_num.GetSeqNumReqOrder().Wait();
            //seq_num.GetSeqNumMutOrder().Wait();
            //seq_num.GetSeqNumRetOrder().Wait();
            //seq_num.GetSeqNumPetCash().Wait();
            //seq_num.GetSeqNumClosingShift().Wait();
            //seq_num.GetSeqNumClosingStore().Wait();

            API_SeqNum2 seq_num = new API_SeqNum2();
            seq_num.get_cust_id();
            seq_num.GetSeqNumTrans().Wait();
            seq_num.GetSeqNumReqOrder().Wait();
            seq_num.GetSeqNumMutOrder().Wait();
            seq_num.GetSeqNumRetOrder().Wait();
            seq_num.GetSeqNumPetCash().Wait();
            seq_num.GetSeqNumClosingShift().Wait();
            seq_num.GetSeqNumClosingStore().Wait();
        }

        private void b_DisSelItem_Click(object sender, EventArgs e)
        {
            API_SeqNum2 seq_num = new API_SeqNum2();
            seq_num.get_cust_id();
            seq_num.GetSeqNumTrans().Wait();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            API_SeqNum2 seq_num = new API_SeqNum2();
            seq_num.get_cust_id();
            seq_num.GetSeqNumReqOrder().Wait();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            API_SeqNum2 seq_num = new API_SeqNum2();
            seq_num.get_cust_id();
            seq_num.GetSeqNumMutOrder().Wait();
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            API_SeqNum2 seq_num = new API_SeqNum2();
            seq_num.get_cust_id();
            seq_num.GetSeqNumRetOrder().Wait();
        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            API_SeqNum2 seq_num = new API_SeqNum2();
            seq_num.get_cust_id();
            seq_num.GetSeqNumPetCash().Wait();
        }

        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {
            API_SeqNum2 seq_num = new API_SeqNum2();
            seq_num.get_cust_id();
            seq_num.GetSeqNumClosingShift().Wait();
        }

        private void bunifuThinButton27_Click(object sender, EventArgs e)
        {
            API_SeqNum2 seq_num = new API_SeqNum2();
            seq_num.get_cust_id();
            seq_num.GetSeqNumClosingStore().Wait();
        }
    }
}
