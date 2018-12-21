using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace try_consume_api2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        //MENAMBAHKAN WARNA DI DGV
        public void RowsColow()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                int val = Int32.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                if (val == 0)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Yelllow;
                    //timer1.Start();
                }


            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //=================================================================
            dataGridView1.MultiSelect = true;
            dataGridView1.AllowUserToAddRows = false;
        }
    }
}
