/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 8/12/2011
 * Time: 3:15 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ShopFloor
{
	public partial class GetDecimalInputForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.Panel pnlDescription;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdClear;
		private System.Windows.Forms.Button cmdAbort;
		private System.Windows.Forms.TextBox txtDataEntry;
		
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
			this.txtDataEntry = new System.Windows.Forms.TextBox();
			this.cmdAbort = new System.Windows.Forms.Button();
			this.cmdClear = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.pnlDescription = new System.Windows.Forms.Panel();
			this.lblDescription = new System.Windows.Forms.Label();
			this.pnlDescription.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtDataEntry
			// 
			this.txtDataEntry.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.txtDataEntry.ForeColor = System.Drawing.Color.Blue;
			this.txtDataEntry.Location = new System.Drawing.Point(12, 27);
			this.txtDataEntry.Name = "txtDataEntry";
			this.txtDataEntry.Size = new System.Drawing.Size(186, 20);
			this.txtDataEntry.TabIndex = 4;
			this.txtDataEntry.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtDataEntry.Enter += new System.EventHandler(this.TxtDataEntryEnter);
			this.txtDataEntry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDataEntryKeyDown);
			this.txtDataEntry.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtDataEntryKeyPress);
			this.txtDataEntry.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtDataEntryKeyUp);
			this.txtDataEntry.Leave += new System.EventHandler(this.TxtDataEntryLeave);
			// 
			// cmdAbort
			// 
			this.cmdAbort.Location = new System.Drawing.Point(141, 53);
			this.cmdAbort.Name = "cmdAbort";
			this.cmdAbort.Size = new System.Drawing.Size(57, 27);
			this.cmdAbort.TabIndex = 7;
			this.cmdAbort.Text = "&Abort";
			this.cmdAbort.UseVisualStyleBackColor = true;
			this.cmdAbort.Click += new System.EventHandler(this.CmdAbortClick);
			// 
			// cmdClear
			// 
			this.cmdClear.Enabled = false;
			this.cmdClear.Location = new System.Drawing.Point(76, 53);
			this.cmdClear.Name = "cmdClear";
			this.cmdClear.Size = new System.Drawing.Size(57, 27);
			this.cmdClear.TabIndex = 6;
			this.cmdClear.Text = "&Clear";
			this.cmdClear.UseVisualStyleBackColor = true;
			this.cmdClear.Click += new System.EventHandler(this.CmdClearClick);
			// 
			// cmdOK
			// 
			this.cmdOK.Enabled = false;
			this.cmdOK.Location = new System.Drawing.Point(11, 53);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(57, 27);
			this.cmdOK.TabIndex = 5;
			this.cmdOK.Text = "&OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.CmdOKClick);
			// 
			// pnlDescription
			// 
			this.pnlDescription.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.pnlDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlDescription.Controls.Add(this.lblDescription);
			this.pnlDescription.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlDescription.Location = new System.Drawing.Point(0, 0);
			this.pnlDescription.Name = "pnlDescription";
			this.pnlDescription.Size = new System.Drawing.Size(208, 21);
			this.pnlDescription.TabIndex = 0;
			// 
			// lblDescription
			// 
			this.lblDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblDescription.Location = new System.Drawing.Point(0, 0);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(206, 19);
			this.lblDescription.TabIndex = 1;
			this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// GetDecimalInputForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(208, 89);
			this.ControlBox = false;
			this.Controls.Add(this.pnlDescription);
			this.Controls.Add(this.txtDataEntry);
			this.Controls.Add(this.cmdAbort);
			this.Controls.Add(this.cmdClear);
			this.Controls.Add(this.cmdOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "GetDecimalInputForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GetDecimalInputFormFormClosed);
			this.Shown += new System.EventHandler(this.GetDecimalInputFormShown);
			this.pnlDescription.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
	}
}
