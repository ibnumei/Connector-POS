namespace try_consume_api2
{
    partial class W_DO_Get
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(W_DO_Get));
            this.b_reload = new Bunifu.Framework.UI.BunifuThinButton2();
            this.SuspendLayout();
            // 
            // b_reload
            // 
            this.b_reload.ActiveBorderThickness = 1;
            this.b_reload.ActiveCornerRadius = 20;
            this.b_reload.ActiveFillColor = System.Drawing.Color.White;
            this.b_reload.ActiveForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.b_reload.ActiveLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.b_reload.BackColor = System.Drawing.SystemColors.Control;
            this.b_reload.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("b_reload.BackgroundImage")));
            this.b_reload.ButtonText = "GET DATA";
            this.b_reload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b_reload.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.b_reload.ForeColor = System.Drawing.Color.Gray;
            this.b_reload.IdleBorderThickness = 2;
            this.b_reload.IdleCornerRadius = 20;
            this.b_reload.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.b_reload.IdleForecolor = System.Drawing.Color.White;
            this.b_reload.IdleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.b_reload.Location = new System.Drawing.Point(133, 99);
            this.b_reload.Margin = new System.Windows.Forms.Padding(5);
            this.b_reload.Name = "b_reload";
            this.b_reload.Size = new System.Drawing.Size(175, 74);
            this.b_reload.TabIndex = 3;
            this.b_reload.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.b_reload.Click += new System.EventHandler(this.b_reload_Click);
            // 
            // W_DO_Get
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 281);
            this.Controls.Add(this.b_reload);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "W_DO_Get";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.W_DO_Get_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private Bunifu.Framework.UI.BunifuThinButton2 b_reload;
    }
}