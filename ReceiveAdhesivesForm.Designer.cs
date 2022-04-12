/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 9/23/2011
 * Time: 1:16 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ShopFloor
{
	public partial class ReceiveAdhesivesForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.TextBox txtBatchId;
		private System.Windows.Forms.Label lblBatchId;
		private System.Windows.Forms.Button cmdSave;
		private System.Windows.Forms.TextBox txtPoundsPerTote;
		private System.Windows.Forms.Label lblPoundsPerTote;
		private System.Windows.Forms.TextBox txtNumberOfTotes;
		private System.Windows.Forms.Label lblNumberOfTotes;
		private System.Windows.Forms.ComboBox cbxPartNumber;
		private System.Windows.Forms.Label lblPartNumber;
		private System.Windows.Forms.TextBox txtPurchaseOrderNumber;
		private System.Windows.Forms.Label lblPurchaseOrderNumber;
		
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
			this.lblPurchaseOrderNumber = new System.Windows.Forms.Label();
			this.txtPurchaseOrderNumber = new System.Windows.Forms.TextBox();
			this.lblPartNumber = new System.Windows.Forms.Label();
			this.cbxPartNumber = new System.Windows.Forms.ComboBox();
			this.lblNumberOfTotes = new System.Windows.Forms.Label();
			this.txtNumberOfTotes = new System.Windows.Forms.TextBox();
			this.lblPoundsPerTote = new System.Windows.Forms.Label();
			this.txtPoundsPerTote = new System.Windows.Forms.TextBox();
			this.cmdSave = new System.Windows.Forms.Button();
			this.lblBatchId = new System.Windows.Forms.Label();
			this.txtBatchId = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// lblPurchaseOrderNumber
			// 
			this.lblPurchaseOrderNumber.Location = new System.Drawing.Point(11, 24);
			this.lblPurchaseOrderNumber.Name = "lblPurchaseOrderNumber";
			this.lblPurchaseOrderNumber.Size = new System.Drawing.Size(66, 23);
			this.lblPurchaseOrderNumber.TabIndex = 0;
			this.lblPurchaseOrderNumber.Text = "PO Number:";
			// 
			// txtPurchaseOrderNumber
			// 
			this.txtPurchaseOrderNumber.Location = new System.Drawing.Point(83, 21);
			this.txtPurchaseOrderNumber.Name = "txtPurchaseOrderNumber";
			this.txtPurchaseOrderNumber.Size = new System.Drawing.Size(169, 20);
			this.txtPurchaseOrderNumber.TabIndex = 1;
			this.txtPurchaseOrderNumber.Enter += new System.EventHandler(this.TextBoxEnterNumbersOnly);
			this.txtPurchaseOrderNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxKeyDownNumbersOnly);
			this.txtPurchaseOrderNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKeyPressNumbersOnly);
			this.txtPurchaseOrderNumber.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtPurchaseOrderNumberKeyUp);
			// 
			// lblPartNumber
			// 
			this.lblPartNumber.Location = new System.Drawing.Point(11, 61);
			this.lblPartNumber.Name = "lblPartNumber";
			this.lblPartNumber.Size = new System.Drawing.Size(72, 23);
			this.lblPartNumber.TabIndex = 2;
			this.lblPartNumber.Text = "Part Number:";
			// 
			// cbxPartNumber
			// 
			this.cbxPartNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxPartNumber.FormattingEnabled = true;
			this.cbxPartNumber.Location = new System.Drawing.Point(83, 61);
			this.cbxPartNumber.Name = "cbxPartNumber";
			this.cbxPartNumber.Size = new System.Drawing.Size(169, 21);
			this.cbxPartNumber.TabIndex = 3;
			// 
			// lblNumberOfTotes
			// 
			this.lblNumberOfTotes.Location = new System.Drawing.Point(11, 98);
			this.lblNumberOfTotes.Name = "lblNumberOfTotes";
			this.lblNumberOfTotes.Size = new System.Drawing.Size(53, 23);
			this.lblNumberOfTotes.TabIndex = 4;
			this.lblNumberOfTotes.Text = "# Totes:";
			// 
			// txtNumberOfTotes
			// 
			this.txtNumberOfTotes.Location = new System.Drawing.Point(60, 95);
			this.txtNumberOfTotes.Name = "txtNumberOfTotes";
			this.txtNumberOfTotes.Size = new System.Drawing.Size(23, 20);
			this.txtNumberOfTotes.TabIndex = 5;
			this.txtNumberOfTotes.Text = "3";
			this.txtNumberOfTotes.Enter += new System.EventHandler(this.TextBoxEnterNumbersOnly);
			this.txtNumberOfTotes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxKeyDownNumbersOnly);
			this.txtNumberOfTotes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKeyPressNumbersOnly);
			this.txtNumberOfTotes.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtNumberOfTotesKeyUp);
			this.txtNumberOfTotes.Leave += new System.EventHandler(this.TxtNumberOfTotesLeave);
			// 
			// lblPoundsPerTote
			// 
			this.lblPoundsPerTote.Location = new System.Drawing.Point(89, 98);
			this.lblPoundsPerTote.Name = "lblPoundsPerTote";
			this.lblPoundsPerTote.Size = new System.Drawing.Size(94, 23);
			this.lblPoundsPerTote.TabIndex = 6;
			this.lblPoundsPerTote.Text = "Pounds Per Tote:";
			// 
			// txtPoundsPerTote
			// 
			this.txtPoundsPerTote.Location = new System.Drawing.Point(178, 95);
			this.txtPoundsPerTote.Name = "txtPoundsPerTote";
			this.txtPoundsPerTote.Size = new System.Drawing.Size(74, 20);
			this.txtPoundsPerTote.TabIndex = 7;
			this.txtPoundsPerTote.Text = "2,502.22";
			this.txtPoundsPerTote.Enter += new System.EventHandler(this.TxtPoundsPerToteEnter);
			this.txtPoundsPerTote.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxKeyDownNumbersOnly);
			this.txtPoundsPerTote.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPoundsPerToteKeyPress);
			this.txtPoundsPerTote.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtPoundsPerToteKeyUp);
			this.txtPoundsPerTote.Leave += new System.EventHandler(this.TxtPoundsPerToteLeave);
			// 
			// cmdSave
			// 
			this.cmdSave.Enabled = false;
			this.cmdSave.Location = new System.Drawing.Point(88, 172);
			this.cmdSave.Name = "cmdSave";
			this.cmdSave.Size = new System.Drawing.Size(95, 29);
			this.cmdSave.TabIndex = 10;
			this.cmdSave.Text = "Save";
			this.cmdSave.UseVisualStyleBackColor = true;
			this.cmdSave.Click += new System.EventHandler(this.CmdSaveClick);
			// 
			// lblBatchId
			// 
			this.lblBatchId.Location = new System.Drawing.Point(11, 135);
			this.lblBatchId.Name = "lblBatchId";
			this.lblBatchId.Size = new System.Drawing.Size(63, 16);
			this.lblBatchId.TabIndex = 8;
			this.lblBatchId.Text = "Batch ID:";
			// 
			// txtBatchId
			// 
			this.txtBatchId.Location = new System.Drawing.Point(73, 132);
			this.txtBatchId.Name = "txtBatchId";
			this.txtBatchId.Size = new System.Drawing.Size(179, 20);
			this.txtBatchId.TabIndex = 9;
			this.txtBatchId.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtBatchIdKeyUp);
			// 
			// ReceiveAdhesivesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(266, 215);
			this.Controls.Add(this.txtBatchId);
			this.Controls.Add(this.lblBatchId);
			this.Controls.Add(this.cmdSave);
			this.Controls.Add(this.txtPoundsPerTote);
			this.Controls.Add(this.lblPoundsPerTote);
			this.Controls.Add(this.txtNumberOfTotes);
			this.Controls.Add(this.lblNumberOfTotes);
			this.Controls.Add(this.cbxPartNumber);
			this.Controls.Add(this.lblPartNumber);
			this.Controls.Add(this.txtPurchaseOrderNumber);
			this.Controls.Add(this.lblPurchaseOrderNumber);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ReceiveAdhesivesForm";
			this.Text = "Receive Adhesives";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReceiveAdhesivesFormFormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
	}
}
