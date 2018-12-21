namespace try_consume_api2
{
    partial class W_item_dmns
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(W_item_dmns));
            this.b_item = new Bunifu.Framework.UI.BunifuThinButton2();
            this.SuspendLayout();
            // 
            // b_item
            // 
            this.b_item.ActiveBorderThickness = 1;
            this.b_item.ActiveCornerRadius = 20;
            this.b_item.ActiveFillColor = System.Drawing.Color.White;
            this.b_item.ActiveForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.b_item.ActiveLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.b_item.BackColor = System.Drawing.SystemColors.Control;
            this.b_item.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("b_item.BackgroundImage")));
            this.b_item.ButtonText = "RELOAD ITEM DIMENSION";
            this.b_item.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b_item.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.b_item.ForeColor = System.Drawing.Color.SeaGreen;
            this.b_item.IdleBorderThickness = 1;
            this.b_item.IdleCornerRadius = 20;
            this.b_item.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.b_item.IdleForecolor = System.Drawing.Color.White;
            this.b_item.IdleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.b_item.Location = new System.Drawing.Point(90, 91);
            this.b_item.Margin = new System.Windows.Forms.Padding(5);
            this.b_item.Name = "b_item";
            this.b_item.Size = new System.Drawing.Size(255, 74);
            this.b_item.TabIndex = 0;
            this.b_item.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.b_item.Click += new System.EventHandler(this.b_item_Click);
            // 
            // W_item_dmns
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 281);
            this.Controls.Add(this.b_item);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "W_item_dmns";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuThinButton2 b_item;
    }
}