namespace ShopFloor
{
    partial class fingerPrintLoginForm
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
            this.pbFp = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbFp)).BeginInit();
            this.SuspendLayout();
            // 
            // pbFp
            // 
            this.pbFp.BackColor = System.Drawing.SystemColors.Window;
            this.pbFp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbFp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbFp.Location = new System.Drawing.Point(0, 0);
            this.pbFp.Name = "pbFp";
            this.pbFp.Size = new System.Drawing.Size(158, 174);
            this.pbFp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbFp.TabIndex = 28;
            this.pbFp.TabStop = false;
            // 
            // fingerPrintLoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(158, 174);
            this.ControlBox = false;
            this.Controls.Add(this.pbFp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "fingerPrintLoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scan Fingerprint";
            this.Shown += new System.EventHandler(this.fingerPrintLoginForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pbFp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbFp;
    }
}