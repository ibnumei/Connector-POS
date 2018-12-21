namespace try_consume_api2
{
    partial class W_Select_Store
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(W_Select_Store));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.combo_store = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.b_select = new Bunifu.Framework.UI.BunifuThinButton2();
            this.dgv_status = new Bunifu.Framework.UI.BunifuCustomDataGrid();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_status)).BeginInit();
            this.SuspendLayout();
            // 
            // combo_store
            // 
            this.combo_store.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.combo_store.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combo_store.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combo_store.FormattingEnabled = true;
            this.combo_store.Location = new System.Drawing.Point(107, 45);
            this.combo_store.Name = "combo_store";
            this.combo_store.Size = new System.Drawing.Size(413, 28);
            this.combo_store.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label11.Location = new System.Drawing.Point(166, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(289, 33);
            this.label11.TabIndex = 5;
            this.label11.Text = "Please Select Store";
            // 
            // b_select
            // 
            this.b_select.ActiveBorderThickness = 2;
            this.b_select.ActiveCornerRadius = 20;
            this.b_select.ActiveFillColor = System.Drawing.Color.White;
            this.b_select.ActiveForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.b_select.ActiveLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.b_select.BackColor = System.Drawing.Color.White;
            this.b_select.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("b_select.BackgroundImage")));
            this.b_select.ButtonText = "SELECT";
            this.b_select.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b_select.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.b_select.ForeColor = System.Drawing.Color.SeaGreen;
            this.b_select.IdleBorderThickness = 2;
            this.b_select.IdleCornerRadius = 20;
            this.b_select.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.b_select.IdleForecolor = System.Drawing.Color.White;
            this.b_select.IdleLineColor = System.Drawing.Color.White;
            this.b_select.Location = new System.Drawing.Point(107, 81);
            this.b_select.Margin = new System.Windows.Forms.Padding(5);
            this.b_select.Name = "b_select";
            this.b_select.Size = new System.Drawing.Size(413, 57);
            this.b_select.TabIndex = 28;
            this.b_select.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.b_select.Click += new System.EventHandler(this.b_select_Click);
            // 
            // dgv_status
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgv_status.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_status.BackgroundColor = System.Drawing.Color.White;
            this.dgv_status.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_status.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_status.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_status.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_status.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_status.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_status.DoubleBuffered = true;
            this.dgv_status.EnableHeadersVisualStyles = false;
            this.dgv_status.HeaderBgColor = System.Drawing.Color.Silver;
            this.dgv_status.HeaderForeColor = System.Drawing.Color.White;
            this.dgv_status.Location = new System.Drawing.Point(32, 146);
            this.dgv_status.Name = "dgv_status";
            this.dgv_status.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv_status.RowHeadersVisible = false;
            this.dgv_status.RowTemplate.Height = 25;
            this.dgv_status.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv_status.Size = new System.Drawing.Size(557, 412);
            this.dgv_status.TabIndex = 29;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column1.HeaderText = "DATA";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column2.HeaderText = "STATUS";
            this.Column2.Name = "Column2";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(220, 578);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(203, 75);
            this.textBox1.TabIndex = 30;
            // 
            // W_Select_Store
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(624, 665);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dgv_status);
            this.Controls.Add(this.b_select);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.combo_store);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "W_Select_Store";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.W_Select_Store_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_status)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox combo_store;
        private System.Windows.Forms.Label label11;
        private Bunifu.Framework.UI.BunifuThinButton2 b_select;
        private Bunifu.Framework.UI.BunifuCustomDataGrid dgv_status;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}