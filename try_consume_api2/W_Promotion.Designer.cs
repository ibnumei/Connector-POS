namespace try_consume_api2
{
    partial class W_Promotion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(W_Promotion));
            this.b_reload2 = new Bunifu.Framework.UI.BunifuThinButton2();
            this.SuspendLayout();
            // 
            // b_reload2
            // 
            this.b_reload2.ActiveBorderThickness = 1;
            this.b_reload2.ActiveCornerRadius = 20;
            this.b_reload2.ActiveFillColor = System.Drawing.Color.White;
            this.b_reload2.ActiveForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.b_reload2.ActiveLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.b_reload2.BackColor = System.Drawing.SystemColors.Control;
            this.b_reload2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("b_reload2.BackgroundImage")));
            this.b_reload2.ButtonText = "RELOAD PROMOTION";
            this.b_reload2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b_reload2.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.b_reload2.ForeColor = System.Drawing.Color.Gray;
            this.b_reload2.IdleBorderThickness = 2;
            this.b_reload2.IdleCornerRadius = 20;
            this.b_reload2.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.b_reload2.IdleForecolor = System.Drawing.Color.White;
            this.b_reload2.IdleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.b_reload2.Location = new System.Drawing.Point(111, 84);
            this.b_reload2.Margin = new System.Windows.Forms.Padding(5);
            this.b_reload2.Name = "b_reload2";
            this.b_reload2.Size = new System.Drawing.Size(233, 74);
            this.b_reload2.TabIndex = 6;
            this.b_reload2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.b_reload2.Click += new System.EventHandler(this.b_reload2_Click);
            // 
            // W_Promotion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 261);
            this.Controls.Add(this.b_reload2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "W_Promotion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }

        #endregion
        private Bunifu.Framework.UI.BunifuThinButton2 b_reload2;
    }
}