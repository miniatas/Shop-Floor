namespace ShopFloor
{
    partial class registerFingerprintForm
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
            this.pbFP1 = new System.Windows.Forms.PictureBox();
            this.pbFP2 = new System.Windows.Forms.PictureBox();
            this.lblFingerPrintScan1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdCapture1 = new System.Windows.Forms.Button();
            this.cmdVerifyandSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbFP1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFP2)).BeginInit();
            this.SuspendLayout();
            // 
            // pbFP1
            // 
            this.pbFP1.BackColor = System.Drawing.SystemColors.Window;
            this.pbFP1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbFP1.Location = new System.Drawing.Point(12, 33);
            this.pbFP1.Name = "pbFP1";
            this.pbFP1.Size = new System.Drawing.Size(104, 128);
            this.pbFP1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbFP1.TabIndex = 28;
            this.pbFP1.TabStop = false;
            // 
            // pbFP2
            // 
            this.pbFP2.BackColor = System.Drawing.SystemColors.Window;
            this.pbFP2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbFP2.Location = new System.Drawing.Point(140, 33);
            this.pbFP2.Name = "pbFP2";
            this.pbFP2.Size = new System.Drawing.Size(104, 128);
            this.pbFP2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbFP2.TabIndex = 29;
            this.pbFP2.TabStop = false;
            // 
            // lblFingerPrintScan1
            // 
            this.lblFingerPrintScan1.AutoSize = true;
            this.lblFingerPrintScan1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFingerPrintScan1.Location = new System.Drawing.Point(13, 17);
            this.lblFingerPrintScan1.Name = "lblFingerPrintScan1";
            this.lblFingerPrintScan1.Size = new System.Drawing.Size(100, 13);
            this.lblFingerPrintScan1.TabIndex = 2;
            this.lblFingerPrintScan1.Text = "Fingerprint Scan #1";
            this.lblFingerPrintScan1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(141, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Fingerprint Scan #2";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdCapture1
            // 
            this.cmdCapture1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.cmdCapture1.Location = new System.Drawing.Point(11, 167);
            this.cmdCapture1.Name = "cmdCapture1";
            this.cmdCapture1.Size = new System.Drawing.Size(104, 23);
            this.cmdCapture1.TabIndex = 0;
            this.cmdCapture1.Text = "Capture";
            this.cmdCapture1.UseVisualStyleBackColor = false;
            this.cmdCapture1.Click += new System.EventHandler(this.cmdCapture1_Click);
            // 
            // cmdVerifyandSave
            // 
            this.cmdVerifyandSave.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.cmdVerifyandSave.Enabled = false;
            this.cmdVerifyandSave.Location = new System.Drawing.Point(139, 167);
            this.cmdVerifyandSave.Name = "cmdVerifyandSave";
            this.cmdVerifyandSave.Size = new System.Drawing.Size(104, 23);
            this.cmdVerifyandSave.TabIndex = 1;
            this.cmdVerifyandSave.Text = "Verify & Save";
            this.cmdVerifyandSave.UseVisualStyleBackColor = false;
            this.cmdVerifyandSave.Click += new System.EventHandler(this.cmdCapture2_Click);
            // 
            // registerFingerprintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 204);
            this.ControlBox = false;
            this.Controls.Add(this.cmdVerifyandSave);
            this.Controls.Add(this.cmdCapture1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFingerPrintScan1);
            this.Controls.Add(this.pbFP2);
            this.Controls.Add(this.pbFP1);
            this.Name = "registerFingerprintForm";
            this.Text = "Register Fingerprint";
            this.Shown += new System.EventHandler(this.registerFingerprintForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pbFP1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFP2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbFP1;
        private System.Windows.Forms.PictureBox pbFP2;
        private System.Windows.Forms.Label lblFingerPrintScan1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdCapture1;
        private System.Windows.Forms.Button cmdVerifyandSave;
    }
}