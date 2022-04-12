namespace ShopFloor
{
    partial class UserAdminForm
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
            this.lblUserID = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblVerifyPassword = new System.Windows.Forms.Label();
            this.txtVerifyPassword = new System.Windows.Forms.TextBox();
            this.lblUserRights = new System.Windows.Forms.Label();
            this.cbUserRights = new System.Windows.Forms.ComboBox();
            this.ckCanEditHours = new System.Windows.Forms.CheckBox();
            this.ckCanConsignInventory = new System.Windows.Forms.CheckBox();
            this.ckCanOverride = new System.Windows.Forms.CheckBox();
            this.ckCanAccessAllInventory = new System.Windows.Forms.CheckBox();
            this.ckAdministrator = new System.Windows.Forms.CheckBox();
            this.ckNoUPCValidationReqd = new System.Windows.Forms.CheckBox();
            this.pbFP1 = new System.Windows.Forms.PictureBox();
            this.pbFP2 = new System.Windows.Forms.PictureBox();
            this.cmdCapture1 = new System.Windows.Forms.Button();
            this.cmdCapture2 = new System.Windows.Forms.Button();
            this.lblFingerPrintScan1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdRecaptureFP = new System.Windows.Forms.Button();
            this.ckChangePassword = new System.Windows.Forms.CheckBox();
            this.pnlDepartments = new System.Windows.Forms.Panel();
            this.ckRework = new System.Windows.Forms.CheckBox();
            this.ckBagMaking = new System.Windows.Forms.CheckBox();
            this.ckSlit = new System.Windows.Forms.CheckBox();
            this.ckLam = new System.Windows.Forms.CheckBox();
            this.ckPress = new System.Windows.Forms.CheckBox();
            this.lblDepartments = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.ckCanAccessPlates = new System.Windows.Forms.CheckBox();
            this.ckCanEdiMachinePeriphials = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbFP1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFP2)).BeginInit();
            this.pnlDepartments.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserID.Location = new System.Drawing.Point(12, 16);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(54, 13);
            this.lblUserID.TabIndex = 0;
            this.lblUserID.Text = "User ID:";
            // 
            // txtUserID
            // 
            this.txtUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserID.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtUserID.Location = new System.Drawing.Point(64, 13);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(210, 20);
            this.txtUserID.TabIndex = 1;
            this.txtUserID.Leave += new System.EventHandler(this.txtUserID_Leave);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(12, 203);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 8;
            this.lblPassword.Text = "Password:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(80, 203);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(194, 20);
            this.txtPassword.TabIndex = 9;
            // 
            // lblVerifyPassword
            // 
            this.lblVerifyPassword.Location = new System.Drawing.Point(12, 239);
            this.lblVerifyPassword.Name = "lblVerifyPassword";
            this.lblVerifyPassword.Size = new System.Drawing.Size(56, 30);
            this.lblVerifyPassword.TabIndex = 11;
            this.lblVerifyPassword.Text = "Verify Password:";
            // 
            // txtVerifyPassword
            // 
            this.txtVerifyPassword.Location = new System.Drawing.Point(80, 249);
            this.txtVerifyPassword.Name = "txtVerifyPassword";
            this.txtVerifyPassword.PasswordChar = '*';
            this.txtVerifyPassword.Size = new System.Drawing.Size(194, 20);
            this.txtVerifyPassword.TabIndex = 12;
            // 
            // lblUserRights
            // 
            this.lblUserRights.AutoSize = true;
            this.lblUserRights.Location = new System.Drawing.Point(12, 169);
            this.lblUserRights.Name = "lblUserRights";
            this.lblUserRights.Size = new System.Drawing.Size(65, 13);
            this.lblUserRights.TabIndex = 6;
            this.lblUserRights.Text = "User Rights:";
            // 
            // cbUserRights
            // 
            this.cbUserRights.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUserRights.FormattingEnabled = true;
            this.cbUserRights.Location = new System.Drawing.Point(80, 169);
            this.cbUserRights.Name = "cbUserRights";
            this.cbUserRights.Size = new System.Drawing.Size(194, 21);
            this.cbUserRights.TabIndex = 7;
            // 
            // ckCanEditHours
            // 
            this.ckCanEditHours.AutoSize = true;
            this.ckCanEditHours.Location = new System.Drawing.Point(12, 286);
            this.ckCanEditHours.Name = "ckCanEditHours";
            this.ckCanEditHours.Size = new System.Drawing.Size(97, 17);
            this.ckCanEditHours.TabIndex = 13;
            this.ckCanEditHours.Text = "Can Edit Hours";
            this.ckCanEditHours.UseVisualStyleBackColor = true;
            // 
            // ckCanConsignInventory
            // 
            this.ckCanConsignInventory.AutoSize = true;
            this.ckCanConsignInventory.Location = new System.Drawing.Point(125, 286);
            this.ckCanConsignInventory.Name = "ckCanConsignInventory";
            this.ckCanConsignInventory.Size = new System.Drawing.Size(133, 17);
            this.ckCanConsignInventory.TabIndex = 14;
            this.ckCanConsignInventory.Text = "Can Consign Inventory";
            this.ckCanConsignInventory.UseVisualStyleBackColor = true;
            // 
            // ckCanOverride
            // 
            this.ckCanOverride.AutoSize = true;
            this.ckCanOverride.Location = new System.Drawing.Point(12, 316);
            this.ckCanOverride.Name = "ckCanOverride";
            this.ckCanOverride.Size = new System.Drawing.Size(88, 17);
            this.ckCanOverride.TabIndex = 15;
            this.ckCanOverride.Text = "Can Override";
            this.ckCanOverride.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckCanOverride.UseVisualStyleBackColor = true;
            // 
            // ckCanAccessAllInventory
            // 
            this.ckCanAccessAllInventory.AutoSize = true;
            this.ckCanAccessAllInventory.Location = new System.Drawing.Point(125, 316);
            this.ckCanAccessAllInventory.Name = "ckCanAccessAllInventory";
            this.ckCanAccessAllInventory.Size = new System.Drawing.Size(144, 17);
            this.ckCanAccessAllInventory.TabIndex = 16;
            this.ckCanAccessAllInventory.Text = "Can Access All Inventory";
            this.ckCanAccessAllInventory.UseVisualStyleBackColor = true;
            // 
            // ckAdministrator
            // 
            this.ckAdministrator.AutoSize = true;
            this.ckAdministrator.Location = new System.Drawing.Point(12, 346);
            this.ckAdministrator.Name = "ckAdministrator";
            this.ckAdministrator.Size = new System.Drawing.Size(86, 17);
            this.ckAdministrator.TabIndex = 17;
            this.ckAdministrator.Text = "Administrator";
            this.ckAdministrator.UseVisualStyleBackColor = true;
            // 
            // ckNoUPCValidationReqd
            // 
            this.ckNoUPCValidationReqd.AutoSize = true;
            this.ckNoUPCValidationReqd.Location = new System.Drawing.Point(125, 346);
            this.ckNoUPCValidationReqd.Name = "ckNoUPCValidationReqd";
            this.ckNoUPCValidationReqd.Size = new System.Drawing.Size(145, 17);
            this.ckNoUPCValidationReqd.TabIndex = 18;
            this.ckNoUPCValidationReqd.Text = "No UPC Validation Req\'d";
            this.ckNoUPCValidationReqd.UseVisualStyleBackColor = true;
            // 
            // pbFP1
            // 
            this.pbFP1.BackColor = System.Drawing.SystemColors.Window;
            this.pbFP1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbFP1.Location = new System.Drawing.Point(298, 71);
            this.pbFP1.Name = "pbFP1";
            this.pbFP1.Size = new System.Drawing.Size(104, 128);
            this.pbFP1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbFP1.TabIndex = 27;
            this.pbFP1.TabStop = false;
            // 
            // pbFP2
            // 
            this.pbFP2.BackColor = System.Drawing.SystemColors.Window;
            this.pbFP2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbFP2.Location = new System.Drawing.Point(445, 71);
            this.pbFP2.Name = "pbFP2";
            this.pbFP2.Size = new System.Drawing.Size(104, 128);
            this.pbFP2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbFP2.TabIndex = 28;
            this.pbFP2.TabStop = false;
            // 
            // cmdCapture1
            // 
            this.cmdCapture1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.cmdCapture1.Location = new System.Drawing.Point(298, 208);
            this.cmdCapture1.Name = "cmdCapture1";
            this.cmdCapture1.Size = new System.Drawing.Size(104, 23);
            this.cmdCapture1.TabIndex = 22;
            this.cmdCapture1.Text = "Capture";
            this.cmdCapture1.UseVisualStyleBackColor = false;
            this.cmdCapture1.Click += new System.EventHandler(this.cmdCapture1_Click);
            // 
            // cmdCapture2
            // 
            this.cmdCapture2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.cmdCapture2.Enabled = false;
            this.cmdCapture2.Location = new System.Drawing.Point(445, 208);
            this.cmdCapture2.Name = "cmdCapture2";
            this.cmdCapture2.Size = new System.Drawing.Size(104, 23);
            this.cmdCapture2.TabIndex = 23;
            this.cmdCapture2.Text = "Capture";
            this.cmdCapture2.UseVisualStyleBackColor = false;
            this.cmdCapture2.Click += new System.EventHandler(this.cmdCapture2_Click);
            // 
            // lblFingerPrintScan1
            // 
            this.lblFingerPrintScan1.AutoSize = true;
            this.lblFingerPrintScan1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFingerPrintScan1.Location = new System.Drawing.Point(299, 53);
            this.lblFingerPrintScan1.Name = "lblFingerPrintScan1";
            this.lblFingerPrintScan1.Size = new System.Drawing.Size(100, 13);
            this.lblFingerPrintScan1.TabIndex = 15;
            this.lblFingerPrintScan1.Text = "Fingerprint Scan #1";
            this.lblFingerPrintScan1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(446, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Fingerprint Scan #2";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(345, 328);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(150, 35);
            this.cmdSave.TabIndex = 24;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdRecaptureFP
            // 
            this.cmdRecaptureFP.Location = new System.Drawing.Point(354, 12);
            this.cmdRecaptureFP.Name = "cmdRecaptureFP";
            this.cmdRecaptureFP.Size = new System.Drawing.Size(128, 27);
            this.cmdRecaptureFP.TabIndex = 21;
            this.cmdRecaptureFP.Text = "Recapture Fingerprint";
            this.cmdRecaptureFP.UseVisualStyleBackColor = true;
            this.cmdRecaptureFP.Visible = false;
            this.cmdRecaptureFP.Click += new System.EventHandler(this.cmdRecaptureFP_Click);
            // 
            // ckChangePassword
            // 
            this.ckChangePassword.AutoSize = true;
            this.ckChangePassword.Location = new System.Drawing.Point(80, 225);
            this.ckChangePassword.Name = "ckChangePassword";
            this.ckChangePassword.Size = new System.Drawing.Size(112, 17);
            this.ckChangePassword.TabIndex = 10;
            this.ckChangePassword.Text = "Change Password";
            this.ckChangePassword.UseVisualStyleBackColor = true;
            this.ckChangePassword.Visible = false;
            this.ckChangePassword.CheckedChanged += new System.EventHandler(this.ckChangePassword_CheckedChanged);
            // 
            // pnlDepartments
            // 
            this.pnlDepartments.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlDepartments.Controls.Add(this.ckRework);
            this.pnlDepartments.Controls.Add(this.ckBagMaking);
            this.pnlDepartments.Controls.Add(this.ckSlit);
            this.pnlDepartments.Controls.Add(this.ckLam);
            this.pnlDepartments.Controls.Add(this.ckPress);
            this.pnlDepartments.Location = new System.Drawing.Point(12, 93);
            this.pnlDepartments.Name = "pnlDepartments";
            this.pnlDepartments.Size = new System.Drawing.Size(262, 60);
            this.pnlDepartments.TabIndex = 5;
            // 
            // ckRework
            // 
            this.ckRework.AutoSize = true;
            this.ckRework.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ckRework.Location = new System.Drawing.Point(212, 12);
            this.ckRework.Name = "ckRework";
            this.ckRework.Size = new System.Drawing.Size(48, 31);
            this.ckRework.TabIndex = 4;
            this.ckRework.Text = "Rework";
            this.ckRework.UseVisualStyleBackColor = true;
            // 
            // ckBagMaking
            // 
            this.ckBagMaking.AutoSize = true;
            this.ckBagMaking.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ckBagMaking.Location = new System.Drawing.Point(168, 12);
            this.ckBagMaking.Name = "ckBagMaking";
            this.ckBagMaking.Size = new System.Drawing.Size(30, 31);
            this.ckBagMaking.TabIndex = 3;
            this.ckBagMaking.Text = "Bag";
            this.ckBagMaking.UseVisualStyleBackColor = true;
            // 
            // ckSlit
            // 
            this.ckSlit.AutoSize = true;
            this.ckSlit.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ckSlit.Location = new System.Drawing.Point(119, 12);
            this.ckSlit.Name = "ckSlit";
            this.ckSlit.Size = new System.Drawing.Size(25, 31);
            this.ckSlit.TabIndex = 2;
            this.ckSlit.Text = "Slit";
            this.ckSlit.UseVisualStyleBackColor = true;
            // 
            // ckLam
            // 
            this.ckLam.AutoSize = true;
            this.ckLam.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ckLam.Location = new System.Drawing.Point(63, 12);
            this.ckLam.Name = "ckLam";
            this.ckLam.Size = new System.Drawing.Size(31, 31);
            this.ckLam.TabIndex = 1;
            this.ckLam.Text = "Lam";
            this.ckLam.UseVisualStyleBackColor = true;
            // 
            // ckPress
            // 
            this.ckPress.AutoSize = true;
            this.ckPress.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ckPress.Location = new System.Drawing.Point(7, 12);
            this.ckPress.Name = "ckPress";
            this.ckPress.Size = new System.Drawing.Size(37, 31);
            this.ckPress.TabIndex = 0;
            this.ckPress.Text = "Press";
            this.ckPress.UseVisualStyleBackColor = true;
            // 
            // lblDepartments
            // 
            this.lblDepartments.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartments.Location = new System.Drawing.Point(12, 75);
            this.lblDepartments.Name = "lblDepartments";
            this.lblDepartments.Size = new System.Drawing.Size(262, 13);
            this.lblDepartments.TabIndex = 4;
            this.lblDepartments.Text = "Departments";
            this.lblDepartments.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtName.Location = new System.Drawing.Point(64, 46);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(210, 20);
            this.txtName.TabIndex = 3;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(12, 49);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Name:";
            // 
            // ckCanAccessPlates
            // 
            this.ckCanAccessPlates.AutoSize = true;
            this.ckCanAccessPlates.Location = new System.Drawing.Point(12, 376);
            this.ckCanAccessPlates.Name = "ckCanAccessPlates";
            this.ckCanAccessPlates.Size = new System.Drawing.Size(115, 17);
            this.ckCanAccessPlates.TabIndex = 19;
            this.ckCanAccessPlates.Text = "Can Access Plates";
            this.ckCanAccessPlates.UseVisualStyleBackColor = true;
            // 
            // ckCanEdiMachinePeriphials
            // 
            this.ckCanEdiMachinePeriphials.AutoSize = true;
            this.ckCanEdiMachinePeriphials.Location = new System.Drawing.Point(125, 376);
            this.ckCanEdiMachinePeriphials.Name = "ckCanEdiMachinePeriphials";
            this.ckCanEdiMachinePeriphials.Size = new System.Drawing.Size(158, 17);
            this.ckCanEdiMachinePeriphials.TabIndex = 20;
            this.ckCanEdiMachinePeriphials.Text = "Can Edit Machine Periphials";
            this.ckCanEdiMachinePeriphials.UseVisualStyleBackColor = true;
            // 
            // UserAdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 429);
            this.Controls.Add(this.ckCanEdiMachinePeriphials);
            this.Controls.Add(this.ckCanAccessPlates);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblDepartments);
            this.Controls.Add(this.pnlDepartments);
            this.Controls.Add(this.ckChangePassword);
            this.Controls.Add(this.cmdRecaptureFP);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFingerPrintScan1);
            this.Controls.Add(this.cmdCapture2);
            this.Controls.Add(this.cmdCapture1);
            this.Controls.Add(this.pbFP2);
            this.Controls.Add(this.pbFP1);
            this.Controls.Add(this.ckNoUPCValidationReqd);
            this.Controls.Add(this.ckAdministrator);
            this.Controls.Add(this.ckCanAccessAllInventory);
            this.Controls.Add(this.ckCanOverride);
            this.Controls.Add(this.ckCanConsignInventory);
            this.Controls.Add(this.ckCanEditHours);
            this.Controls.Add(this.txtVerifyPassword);
            this.Controls.Add(this.lblVerifyPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtUserID);
            this.Controls.Add(this.lblUserID);
            this.Controls.Add(this.cbUserRights);
            this.Controls.Add(this.lblUserRights);
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Name = "UserAdminForm";
            this.Text = "User Administration";
            ((System.ComponentModel.ISupportInitialize)(this.pbFP1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFP2)).EndInit();
            this.pnlDepartments.ResumeLayout(false);
            this.pnlDepartments.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblVerifyPassword;
        private System.Windows.Forms.TextBox txtVerifyPassword;
        private System.Windows.Forms.Label lblUserRights;
        private System.Windows.Forms.ComboBox cbUserRights;
        private System.Windows.Forms.CheckBox ckCanEditHours;
        private System.Windows.Forms.CheckBox ckCanConsignInventory;
        private System.Windows.Forms.CheckBox ckCanOverride;
        private System.Windows.Forms.CheckBox ckCanAccessAllInventory;
        private System.Windows.Forms.CheckBox ckAdministrator;
        private System.Windows.Forms.CheckBox ckNoUPCValidationReqd;
        private System.Windows.Forms.PictureBox pbFP1;
        private System.Windows.Forms.PictureBox pbFP2;
        private System.Windows.Forms.Button cmdCapture1;
        private System.Windows.Forms.Button cmdCapture2;
        private System.Windows.Forms.Label lblFingerPrintScan1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdRecaptureFP;
        private System.Windows.Forms.CheckBox ckChangePassword;
        private System.Windows.Forms.Panel pnlDepartments;
        private System.Windows.Forms.CheckBox ckRework;
        private System.Windows.Forms.CheckBox ckBagMaking;
        private System.Windows.Forms.CheckBox ckSlit;
        private System.Windows.Forms.CheckBox ckLam;
        private System.Windows.Forms.CheckBox ckPress;
        private System.Windows.Forms.Label lblDepartments;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.CheckBox ckCanAccessPlates;
        private System.Windows.Forms.CheckBox ckCanEdiMachinePeriphials;
    }
}