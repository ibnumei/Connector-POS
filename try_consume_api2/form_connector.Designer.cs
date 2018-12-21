namespace try_consume_api2
{
    partial class form_connector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form_connector));
            this.label5 = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.b_save = new Bunifu.Framework.UI.BunifuThinButton2();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Narrow", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(62, 50);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 29);
            this.label5.TabIndex = 17;
            this.label5.Text = "PASSWORD";
            // 
            // txtPass
            // 
            this.txtPass.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPass.Location = new System.Drawing.Point(66, 85);
            this.txtPass.Margin = new System.Windows.Forms.Padding(4);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(285, 35);
            this.txtPass.TabIndex = 16;
            this.txtPass.UseSystemPasswordChar = true;
            // 
            // b_save
            // 
            this.b_save.ActiveBorderThickness = 2;
            this.b_save.ActiveCornerRadius = 20;
            this.b_save.ActiveFillColor = System.Drawing.Color.White;
            this.b_save.ActiveForecolor = System.Drawing.Color.Gray;
            this.b_save.ActiveLineColor = System.Drawing.Color.Gray;
            this.b_save.BackColor = System.Drawing.Color.WhiteSmoke;
            this.b_save.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("b_save.BackgroundImage")));
            this.b_save.ButtonText = "SAVE";
            this.b_save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b_save.Font = new System.Drawing.Font("Arial Narrow", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.b_save.ForeColor = System.Drawing.Color.SeaGreen;
            this.b_save.IdleBorderThickness = 2;
            this.b_save.IdleCornerRadius = 20;
            this.b_save.IdleFillColor = System.Drawing.Color.Gray;
            this.b_save.IdleForecolor = System.Drawing.Color.White;
            this.b_save.IdleLineColor = System.Drawing.Color.White;
            this.b_save.Location = new System.Drawing.Point(64, 140);
            this.b_save.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.b_save.Name = "b_save";
            this.b_save.Size = new System.Drawing.Size(287, 81);
            this.b_save.TabIndex = 29;
            this.b_save.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.b_save.Click += new System.EventHandler(this.b_save_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.b_save);
            this.groupBox1.Controls.Add(this.txtPass);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(50, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(412, 256);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Set Your Password";
            // 
            // form_connector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(509, 405);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "form_connector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.form_connector_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPass;
        private Bunifu.Framework.UI.BunifuThinButton2 b_save;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}