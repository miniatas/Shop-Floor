/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 12/3/2010
 * Time: 4:23 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ShopFloor
{
	public partial class ReceivingForm
	{
		/// <summary>
		/// Designer variable used to keep tracbx of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.RichTextBox rtbSpecificationDetails;
		private System.Windows.Forms.CheckBox cbxCalculateUnitsorPounds;
		private System.Windows.Forms.Button cmdShowSpecifications;
		private System.Windows.Forms.TextBox txtNotes;
		private System.Windows.Forms.Label lblNotes;
		private System.Windows.Forms.Label lblRemoveRolls;
		private System.Windows.Forms.ComboBox cboRemoveRolls;
		private System.Windows.Forms.Panel pnlRemoveRolls;
		private System.Windows.Forms.Panel pnlPalletRollInfo;
		private System.Windows.Forms.Panel pnlPalletBuild;
		private System.Windows.Forms.Label lblPalletRollInfo;
		private System.Windows.Forms.Label lblPalletInformation;
		private System.Windows.Forms.Button cmdRemove;
		private System.Windows.Forms.Button cmdAbort;
		private System.Windows.Forms.Button cmdSave;
		private System.Windows.Forms.Label lblPalletBuild;
		private System.Windows.Forms.Label lblNumRolls;
		private System.Windows.Forms.TextBox txtRollCount;
		private System.Windows.Forms.Label lblUnitCount;
		private System.Windows.Forms.TextBox txtUnitCount;
		private System.Windows.Forms.Label lblUOM;
		private System.Windows.Forms.ComboBox cboUnitOfMeasure;
		private System.Windows.Forms.Label lblPoundsPerRoll;
		private System.Windows.Forms.TextBox txtPoundsPerRoll;
		private System.Windows.Forms.Button cmdAdd;
		private System.Windows.Forms.Button cmdClear;
		private System.Windows.Forms.Label lblPONumberTitles;
		private System.Windows.Forms.Label lblPartNumberTitles;
		private System.Windows.Forms.Label lblPONumberDetails;
		private System.Windows.Forms.Label lblPartNumberDetails;
		private System.Windows.Forms.Label lblVendorPallletNumber;
		private System.Windows.Forms.TextBox txtVendorPalletNumber;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">True if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) 
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.lblPONumberTitles = new System.Windows.Forms.Label();
            this.lblPartNumberTitles = new System.Windows.Forms.Label();
            this.lblPONumberDetails = new System.Windows.Forms.Label();
            this.lblPartNumberDetails = new System.Windows.Forms.Label();
            this.pnlPalletBuild = new System.Windows.Forms.Panel();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.cbxCalculateUnitsorPounds = new System.Windows.Forms.CheckBox();
            this.cmdClear = new System.Windows.Forms.Button();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.txtPoundsPerRoll = new System.Windows.Forms.TextBox();
            this.lblPoundsPerRoll = new System.Windows.Forms.Label();
            this.cboUnitOfMeasure = new System.Windows.Forms.ComboBox();
            this.lblUOM = new System.Windows.Forms.Label();
            this.txtUnitCount = new System.Windows.Forms.TextBox();
            this.lblUnitCount = new System.Windows.Forms.Label();
            this.txtRollCount = new System.Windows.Forms.TextBox();
            this.lblNumRolls = new System.Windows.Forms.Label();
            this.lblPalletBuild = new System.Windows.Forms.Label();
            this.pnlPalletRollInfo = new System.Windows.Forms.Panel();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdAbort = new System.Windows.Forms.Button();
            this.lblPalletInformation = new System.Windows.Forms.Label();
            this.cmdRemove = new System.Windows.Forms.Button();
            this.lblPalletRollInfo = new System.Windows.Forms.Label();
            this.pnlRemoveRolls = new System.Windows.Forms.Panel();
            this.cboRemoveRolls = new System.Windows.Forms.ComboBox();
            this.lblRemoveRolls = new System.Windows.Forms.Label();
            this.cmdShowSpecifications = new System.Windows.Forms.Button();
            this.rtbSpecificationDetails = new System.Windows.Forms.RichTextBox();
            this.txtVendorPalletNumber = new System.Windows.Forms.TextBox();
            this.lblVendorPallletNumber = new System.Windows.Forms.Label();
            this.pnlPalletBuild.SuspendLayout();
            this.pnlPalletRollInfo.SuspendLayout();
            this.pnlRemoveRolls.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPONumberTitles
            // 
            this.lblPONumberTitles.ForeColor = System.Drawing.Color.Blue;
            this.lblPONumberTitles.Location = new System.Drawing.Point(12, 10);
            this.lblPONumberTitles.Name = "lblPONumberTitles";
            this.lblPONumberTitles.Size = new System.Drawing.Size(187, 53);
            this.lblPONumberTitles.TabIndex = 4;
            // 
            // lblPartNumberTitles
            // 
            this.lblPartNumberTitles.ForeColor = System.Drawing.Color.Blue;
            this.lblPartNumberTitles.Location = new System.Drawing.Point(12, 69);
            this.lblPartNumberTitles.Name = "lblPartNumberTitles";
            this.lblPartNumberTitles.Size = new System.Drawing.Size(187, 64);
            this.lblPartNumberTitles.TabIndex = 5;
            // 
            // lblPONumberDetails
            // 
            this.lblPONumberDetails.ForeColor = System.Drawing.Color.Blue;
            this.lblPONumberDetails.Location = new System.Drawing.Point(118, 10);
            this.lblPONumberDetails.Name = "lblPONumberDetails";
            this.lblPONumberDetails.Size = new System.Drawing.Size(560, 53);
            this.lblPONumberDetails.TabIndex = 6;
            // 
            // lblPartNumberDetails
            // 
            this.lblPartNumberDetails.ForeColor = System.Drawing.Color.Blue;
            this.lblPartNumberDetails.Location = new System.Drawing.Point(118, 69);
            this.lblPartNumberDetails.Name = "lblPartNumberDetails";
            this.lblPartNumberDetails.Size = new System.Drawing.Size(560, 51);
            this.lblPartNumberDetails.TabIndex = 7;
            // 
            // pnlPalletBuild
            // 
            this.pnlPalletBuild.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlPalletBuild.Controls.Add(this.lblNotes);
            this.pnlPalletBuild.Controls.Add(this.txtNotes);
            this.pnlPalletBuild.Controls.Add(this.cbxCalculateUnitsorPounds);
            this.pnlPalletBuild.Controls.Add(this.cmdClear);
            this.pnlPalletBuild.Controls.Add(this.cmdAdd);
            this.pnlPalletBuild.Controls.Add(this.txtPoundsPerRoll);
            this.pnlPalletBuild.Controls.Add(this.lblPoundsPerRoll);
            this.pnlPalletBuild.Controls.Add(this.cboUnitOfMeasure);
            this.pnlPalletBuild.Controls.Add(this.lblUOM);
            this.pnlPalletBuild.Controls.Add(this.txtUnitCount);
            this.pnlPalletBuild.Controls.Add(this.lblUnitCount);
            this.pnlPalletBuild.Controls.Add(this.txtRollCount);
            this.pnlPalletBuild.Controls.Add(this.lblNumRolls);
            this.pnlPalletBuild.Location = new System.Drawing.Point(12, 187);
            this.pnlPalletBuild.Name = "pnlPalletBuild";
            this.pnlPalletBuild.Size = new System.Drawing.Size(336, 112);
            this.pnlPalletBuild.TabIndex = 2;
            // 
            // lblNotes
            // 
            this.lblNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotes.Location = new System.Drawing.Point(8, 58);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(158, 21);
            this.lblNotes.TabIndex = 11;
            this.lblNotes.Text = "Notes";
            this.lblNotes.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(8, 81);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(184, 20);
            this.txtNotes.TabIndex = 8;
            // 
            // cbxCalculateUnitsorPounds
            // 
            this.cbxCalculateUnitsorPounds.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cbxCalculateUnitsorPounds.Checked = true;
            this.cbxCalculateUnitsorPounds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxCalculateUnitsorPounds.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxCalculateUnitsorPounds.Location = new System.Drawing.Point(261, 5);
            this.cbxCalculateUnitsorPounds.Name = "cbxCalculateUnitsorPounds";
            this.cbxCalculateUnitsorPounds.Size = new System.Drawing.Size(73, 51);
            this.cbxCalculateUnitsorPounds.TabIndex = 7;
            this.cbxCalculateUnitsorPounds.Text = "Calc Units or Lbs";
            this.cbxCalculateUnitsorPounds.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cbxCalculateUnitsorPounds.UseVisualStyleBackColor = true;
            this.cbxCalculateUnitsorPounds.CheckedChanged += new System.EventHandler(this.CbxCalcUnitsCheckedChanged);
            // 
            // cmdClear
            // 
            this.cmdClear.Enabled = false;
            this.cmdClear.Location = new System.Drawing.Point(261, 77);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(57, 27);
            this.cmdClear.TabIndex = 10;
            this.cmdClear.Text = "Clear";
            this.cmdClear.UseVisualStyleBackColor = true;
            this.cmdClear.Click += new System.EventHandler(this.CmdClearClicbx);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Enabled = false;
            this.cmdAdd.Location = new System.Drawing.Point(198, 77);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(57, 27);
            this.cmdAdd.TabIndex = 9;
            this.cmdAdd.Text = "Add";
            this.cmdAdd.UseVisualStyleBackColor = true;
            this.cmdAdd.Click += new System.EventHandler(this.CmdAddClick);
            // 
            // txtPoundsPerRoll
            // 
            this.txtPoundsPerRoll.Location = new System.Drawing.Point(73, 35);
            this.txtPoundsPerRoll.Name = "txtPoundsPerRoll";
            this.txtPoundsPerRoll.Size = new System.Drawing.Size(58, 20);
            this.txtPoundsPerRoll.TabIndex = 4;
            this.txtPoundsPerRoll.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPoundsPerRoll.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtKeyDown);
            this.txtPoundsPerRoll.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtKeyPress);
            this.txtPoundsPerRoll.Leave += new System.EventHandler(this.TxtLeave);
            // 
            // lblPoundsPerRoll
            // 
            this.lblPoundsPerRoll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPoundsPerRoll.Location = new System.Drawing.Point(73, 17);
            this.lblPoundsPerRoll.Name = "lblPoundsPerRoll";
            this.lblPoundsPerRoll.Size = new System.Drawing.Size(58, 20);
            this.lblPoundsPerRoll.TabIndex = 4;
            this.lblPoundsPerRoll.Text = "Lbs/Roll";
            this.lblPoundsPerRoll.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cboUnitOfMeasure
            // 
            this.cboUnitOfMeasure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUnitOfMeasure.FormattingEnabled = true;
            this.cboUnitOfMeasure.Location = new System.Drawing.Point(203, 35);
            this.cboUnitOfMeasure.Name = "cboUnitOfMeasure";
            this.cboUnitOfMeasure.Size = new System.Drawing.Size(57, 21);
            this.cboUnitOfMeasure.TabIndex = 6;
            this.cboUnitOfMeasure.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CboUnitOfMeasureKeyPress);
            // 
            // lblUOM
            // 
            this.lblUOM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUOM.Location = new System.Drawing.Point(203, 17);
            this.lblUOM.Name = "lblUOM";
            this.lblUOM.Size = new System.Drawing.Size(58, 20);
            this.lblUOM.TabIndex = 6;
            this.lblUOM.Text = "UOM";
            this.lblUOM.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtUnitCount
            // 
            this.txtUnitCount.Location = new System.Drawing.Point(138, 35);
            this.txtUnitCount.Name = "txtUnitCount";
            this.txtUnitCount.Size = new System.Drawing.Size(58, 20);
            this.txtUnitCount.TabIndex = 5;
            this.txtUnitCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtUnitCount.Visible = false;
            this.txtUnitCount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtKeyDown);
            this.txtUnitCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtKeyPress);
            this.txtUnitCount.Leave += new System.EventHandler(this.TxtLeave);
            // 
            // lblUnitCount
            // 
            this.lblUnitCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnitCount.Location = new System.Drawing.Point(138, 17);
            this.lblUnitCount.Name = "lblUnitCount";
            this.lblUnitCount.Size = new System.Drawing.Size(58, 20);
            this.lblUnitCount.TabIndex = 5;
            this.lblUnitCount.Text = "# Units";
            this.lblUnitCount.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblUnitCount.Visible = false;
            // 
            // txtRollCount
            // 
            this.txtRollCount.Location = new System.Drawing.Point(8, 35);
            this.txtRollCount.Name = "txtRollCount";
            this.txtRollCount.Size = new System.Drawing.Size(58, 20);
            this.txtRollCount.TabIndex = 3;
            this.txtRollCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRollCount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtKeyDown);
            this.txtRollCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtKeyPress);
            this.txtRollCount.Leave += new System.EventHandler(this.TxtLeave);
            // 
            // lblNumRolls
            // 
            this.lblNumRolls.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumRolls.Location = new System.Drawing.Point(8, 17);
            this.lblNumRolls.Name = "lblNumRolls";
            this.lblNumRolls.Size = new System.Drawing.Size(58, 20);
            this.lblNumRolls.TabIndex = 3;
            this.lblNumRolls.Text = "# Rolls";
            this.lblNumRolls.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblPalletBuild
            // 
            this.lblPalletBuild.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPalletBuild.Location = new System.Drawing.Point(12, 172);
            this.lblPalletBuild.Name = "lblPalletBuild";
            this.lblPalletBuild.Size = new System.Drawing.Size(87, 12);
            this.lblPalletBuild.TabIndex = 2;
            this.lblPalletBuild.Text = "Pallet Build";
            this.lblPalletBuild.Visible = false;
            // 
            // pnlPalletRollInfo
            // 
            this.pnlPalletRollInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlPalletRollInfo.Controls.Add(this.cmdSave);
            this.pnlPalletRollInfo.Controls.Add(this.cmdAbort);
            this.pnlPalletRollInfo.Controls.Add(this.lblPalletInformation);
            this.pnlPalletRollInfo.Location = new System.Drawing.Point(354, 187);
            this.pnlPalletRollInfo.Name = "pnlPalletRollInfo";
            this.pnlPalletRollInfo.Size = new System.Drawing.Size(238, 153);
            this.pnlPalletRollInfo.TabIndex = 11;
            // 
            // cmdSave
            // 
            this.cmdSave.Enabled = false;
            this.cmdSave.Location = new System.Drawing.Point(151, 113);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(57, 27);
            this.cmdSave.TabIndex = 13;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.CmdSaveClick);
            // 
            // cmdAbort
            // 
            this.cmdAbort.Location = new System.Drawing.Point(30, 113);
            this.cmdAbort.Name = "cmdAbort";
            this.cmdAbort.Size = new System.Drawing.Size(57, 27);
            this.cmdAbort.TabIndex = 12;
            this.cmdAbort.Text = "Abort";
            this.cmdAbort.UseVisualStyleBackColor = true;
            this.cmdAbort.Click += new System.EventHandler(this.CmdAbortClicbx);
            // 
            // lblPalletInformation
            // 
            this.lblPalletInformation.ForeColor = System.Drawing.Color.Blue;
            this.lblPalletInformation.Location = new System.Drawing.Point(0, 0);
            this.lblPalletInformation.Name = "lblPalletInformation";
            this.lblPalletInformation.Size = new System.Drawing.Size(236, 99);
            this.lblPalletInformation.TabIndex = 10;
            // 
            // cmdRemove
            // 
            this.cmdRemove.Enabled = false;
            this.cmdRemove.Location = new System.Drawing.Point(228, 1);
            this.cmdRemove.Name = "cmdRemove";
            this.cmdRemove.Size = new System.Drawing.Size(57, 27);
            this.cmdRemove.TabIndex = 11;
            this.cmdRemove.Text = "Remove";
            this.cmdRemove.UseVisualStyleBackColor = true;
            this.cmdRemove.Click += new System.EventHandler(this.CmdRemoveClicbx);
            // 
            // lblPalletRollInfo
            // 
            this.lblPalletRollInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPalletRollInfo.Location = new System.Drawing.Point(356, 172);
            this.lblPalletRollInfo.Name = "lblPalletRollInfo";
            this.lblPalletRollInfo.Size = new System.Drawing.Size(100, 12);
            this.lblPalletRollInfo.TabIndex = 9;
            this.lblPalletRollInfo.Text = "Pallet Roll Info";
            this.lblPalletRollInfo.Visible = false;
            // 
            // pnlRemoveRolls
            // 
            this.pnlRemoveRolls.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlRemoveRolls.Controls.Add(this.cboRemoveRolls);
            this.pnlRemoveRolls.Controls.Add(this.lblRemoveRolls);
            this.pnlRemoveRolls.Controls.Add(this.cmdRemove);
            this.pnlRemoveRolls.Location = new System.Drawing.Point(12, 305);
            this.pnlRemoveRolls.Name = "pnlRemoveRolls";
            this.pnlRemoveRolls.Size = new System.Drawing.Size(336, 37);
            this.pnlRemoveRolls.TabIndex = 12;
            this.pnlRemoveRolls.Visible = false;
            // 
            // cboRemoveRolls
            // 
            this.cboRemoveRolls.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRemoveRolls.FormattingEnabled = true;
            this.cboRemoveRolls.Location = new System.Drawing.Point(153, 5);
            this.cboRemoveRolls.Name = "cboRemoveRolls";
            this.cboRemoveRolls.Size = new System.Drawing.Size(57, 21);
            this.cboRemoveRolls.TabIndex = 1;
            this.cboRemoveRolls.SelectedIndexChanged += new System.EventHandler(this.CboRemoveRollsSelectedIndexChanged);
            // 
            // lblRemoveRolls
            // 
            this.lblRemoveRolls.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemoveRolls.Location = new System.Drawing.Point(3, 8);
            this.lblRemoveRolls.Name = "lblRemoveRolls";
            this.lblRemoveRolls.Size = new System.Drawing.Size(144, 23);
            this.lblRemoveRolls.TabIndex = 0;
            this.lblRemoveRolls.Text = "Remove Roll Item No.:";
            // 
            // cmdShowSpecifications
            // 
            this.cmdShowSpecifications.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdShowSpecifications.Location = new System.Drawing.Point(12, 143);
            this.cmdShowSpecifications.Name = "cmdShowSpecifications";
            this.cmdShowSpecifications.Size = new System.Drawing.Size(99, 26);
            this.cmdShowSpecifications.TabIndex = 13;
            this.cmdShowSpecifications.Text = "Show Specs";
            this.cmdShowSpecifications.UseVisualStyleBackColor = true;
            this.cmdShowSpecifications.Visible = false;
            this.cmdShowSpecifications.Click += new System.EventHandler(this.CmdShowSpecificationsClick);
            // 
            // rtbSpecificationDetails
            // 
            this.rtbSpecificationDetails.BackColor = System.Drawing.SystemColors.Control;
            this.rtbSpecificationDetails.ForeColor = System.Drawing.Color.Blue;
            this.rtbSpecificationDetails.Location = new System.Drawing.Point(354, 10);
            this.rtbSpecificationDetails.Name = "rtbSpecificationDetails";
            this.rtbSpecificationDetails.Size = new System.Drawing.Size(238, 159);
            this.rtbSpecificationDetails.TabIndex = 14;
            this.rtbSpecificationDetails.Text = "";
            this.rtbSpecificationDetails.Visible = false;
            // 
            // txtVendorPalletNumber
            // 
            this.txtVendorPalletNumber.Location = new System.Drawing.Point(117, 147);
            this.txtVendorPalletNumber.Name = "txtVendorPalletNumber";
            this.txtVendorPalletNumber.Size = new System.Drawing.Size(215, 20);
            this.txtVendorPalletNumber.TabIndex = 2;
            // 
            // lblVendorPallletNumber
            // 
            this.lblVendorPallletNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVendorPallletNumber.Location = new System.Drawing.Point(117, 133);
            this.lblVendorPallletNumber.Name = "lblVendorPallletNumber";
            this.lblVendorPallletNumber.Size = new System.Drawing.Size(139, 12);
            this.lblVendorPallletNumber.TabIndex = 2;
            this.lblVendorPallletNumber.Text = "Vendor Pallet Number";
            // 
            // ReceivingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 347);
            this.Controls.Add(this.lblVendorPallletNumber);
            this.Controls.Add(this.txtVendorPalletNumber);
            this.Controls.Add(this.rtbSpecificationDetails);
            this.Controls.Add(this.cmdShowSpecifications);
            this.Controls.Add(this.pnlRemoveRolls);
            this.Controls.Add(this.lblPalletRollInfo);
            this.Controls.Add(this.pnlPalletRollInfo);
            this.Controls.Add(this.lblPalletBuild);
            this.Controls.Add(this.pnlPalletBuild);
            this.Controls.Add(this.lblPartNumberDetails);
            this.Controls.Add(this.lblPONumberDetails);
            this.Controls.Add(this.lblPartNumberTitles);
            this.Controls.Add(this.lblPONumberTitles);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReceivingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Receiving";
            this.pnlPalletBuild.ResumeLayout(false);
            this.pnlPalletBuild.PerformLayout();
            this.pnlPalletRollInfo.ResumeLayout(false);
            this.pnlRemoveRolls.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
