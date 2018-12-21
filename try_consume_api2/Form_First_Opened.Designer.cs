namespace try_consume_api2
{
    partial class Form_First_Opened
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_First_Opened));
            this.b_connection = new Bunifu.Framework.UI.BunifuThinButton2();
            this.b_connector = new Bunifu.Framework.UI.BunifuThinButton2();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // b_connection
            // 
            this.b_connection.ActiveBorderThickness = 2;
            this.b_connection.ActiveCornerRadius = 20;
            this.b_connection.ActiveFillColor = System.Drawing.Color.White;
            this.b_connection.ActiveForecolor = System.Drawing.Color.LimeGreen;
            this.b_connection.ActiveLineColor = System.Drawing.Color.LimeGreen;
            this.b_connection.BackColor = System.Drawing.Color.WhiteSmoke;
            this.b_connection.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("b_connection.BackgroundImage")));
            this.b_connection.ButtonText = "CONNECTION";
            this.b_connection.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b_connection.Font = new System.Drawing.Font("Arial Narrow", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.b_connection.ForeColor = System.Drawing.Color.SeaGreen;
            this.b_connection.IdleBorderThickness = 2;
            this.b_connection.IdleCornerRadius = 20;
            this.b_connection.IdleFillColor = System.Drawing.Color.LimeGreen;
            this.b_connection.IdleForecolor = System.Drawing.Color.White;
            this.b_connection.IdleLineColor = System.Drawing.Color.White;
            this.b_connection.Location = new System.Drawing.Point(479, 99);
            this.b_connection.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.b_connection.Name = "b_connection";
            this.b_connection.Size = new System.Drawing.Size(226, 79);
            this.b_connection.TabIndex = 28;
            this.b_connection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.b_connection.Click += new System.EventHandler(this.b_connection_Click);
            // 
            // b_connector
            // 
            this.b_connector.ActiveBorderThickness = 2;
            this.b_connector.ActiveCornerRadius = 20;
            this.b_connector.ActiveFillColor = System.Drawing.Color.White;
            this.b_connector.ActiveForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.b_connector.ActiveLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.b_connector.BackColor = System.Drawing.Color.WhiteSmoke;
            this.b_connector.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("b_connector.BackgroundImage")));
            this.b_connector.ButtonText = "CONNECTOR POS";
            this.b_connector.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b_connector.Font = new System.Drawing.Font("Arial Narrow", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.b_connector.ForeColor = System.Drawing.Color.SeaGreen;
            this.b_connector.IdleBorderThickness = 2;
            this.b_connector.IdleCornerRadius = 20;
            this.b_connector.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.b_connector.IdleForecolor = System.Drawing.Color.White;
            this.b_connector.IdleLineColor = System.Drawing.Color.White;
            this.b_connector.Location = new System.Drawing.Point(92, 99);
            this.b_connector.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.b_connector.Name = "b_connector";
            this.b_connector.Size = new System.Drawing.Size(228, 79);
            this.b_connector.TabIndex = 29;
            this.b_connector.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.b_connector.Click += new System.EventHandler(this.b_connector_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 23);
            this.label1.TabIndex = 33;
            this.label1.Text = "V   1.1";
            // 
            // Form_First_Opened
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(789, 288);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.b_connector);
            this.Controls.Add(this.b_connection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form_First_Opened";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form_First_Opened_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuThinButton2 b_connection;
        private Bunifu.Framework.UI.BunifuThinButton2 b_connector;
        private System.Windows.Forms.Label label1;
    }
}