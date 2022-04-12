/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 12/1/2010
 * Time: 11:43 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ShopFloor
{
	public partial class GetInputForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.TextBox txtDataEntry;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.Button cmdAbort;
		private System.Windows.Forms.Button cmdClear;
		private System.Windows.Forms.Panel pnlDescription;
		
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
            this.components = new System.ComponentModel.Container();
            this.pnlDescription = new System.Windows.Forms.Panel();
            this.lblDescription = new System.Windows.Forms.Label();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdClear = new System.Windows.Forms.Button();
            this.cmdAbort = new System.Windows.Forms.Button();
            this.txtDataEntry = new System.Windows.Forms.TextBox();
            this.inputTimeoutTimer = new System.Windows.Forms.Timer(this.components);
            this.cmdManual = new System.Windows.Forms.Button();
            this.pnlDescription.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlDescription
            // 
            this.pnlDescription.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDescription.Controls.Add(this.lblDescription);
            this.pnlDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDescription.Location = new System.Drawing.Point(0, 0);
            this.pnlDescription.Name = "pnlDescription";
            this.pnlDescription.Size = new System.Drawing.Size(254, 21);
            this.pnlDescription.TabIndex = 0;
            // 
            // lblDescription
            // 
            this.lblDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDescription.Location = new System.Drawing.Point(0, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(252, 19);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdOK
            // 
            this.cmdOK.Enabled = false;
            this.cmdOK.Location = new System.Drawing.Point(4, 53);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(57, 27);
            this.cmdOK.TabIndex = 1;
            this.cmdOK.Text = "&OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.CmdOKClick);
            // 
            // cmdClear
            // 
            this.cmdClear.Enabled = false;
            this.cmdClear.Location = new System.Drawing.Point(67, 53);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(57, 27);
            this.cmdClear.TabIndex = 2;
            this.cmdClear.Text = "&Clear";
            this.cmdClear.UseVisualStyleBackColor = true;
            this.cmdClear.Click += new System.EventHandler(this.CmdClearClick);
            // 
            // cmdAbort
            // 
            this.cmdAbort.Location = new System.Drawing.Point(193, 53);
            this.cmdAbort.Name = "cmdAbort";
            this.cmdAbort.Size = new System.Drawing.Size(57, 27);
            this.cmdAbort.TabIndex = 4;
            this.cmdAbort.Text = "&Abort";
            this.cmdAbort.UseVisualStyleBackColor = true;
            this.cmdAbort.Click += new System.EventHandler(this.CmdAbortClick);
            // 
            // txtDataEntry
            // 
            this.txtDataEntry.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDataEntry.Location = new System.Drawing.Point(14, 27);
            this.txtDataEntry.Name = "txtDataEntry";
            this.txtDataEntry.Size = new System.Drawing.Size(228, 20);
            this.txtDataEntry.TabIndex = 0;
            this.txtDataEntry.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDataEntry.TextChanged += new System.EventHandler(this.TxtDataEntryTextChanged);
            this.txtDataEntry.Enter += new System.EventHandler(this.TxtDataEntryEnter);
            this.txtDataEntry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDataEntryKeyDown);
            this.txtDataEntry.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtDataEntryKeyPress);
            // 
            // inputTimeoutTimer
            // 
            this.inputTimeoutTimer.Interval = 200;
            this.inputTimeoutTimer.Tick += new System.EventHandler(this.inputTimeoutTimer_Tick);
            // 
            // cmdManual
            // 
            this.cmdManual.Location = new System.Drawing.Point(130, 53);
            this.cmdManual.Name = "cmdManual";
            this.cmdManual.Size = new System.Drawing.Size(57, 27);
            this.cmdManual.TabIndex = 3;
            this.cmdManual.Text = "&Manual";
            this.cmdManual.UseVisualStyleBackColor = true;
            this.cmdManual.Click += new System.EventHandler(this.cmdManual_Click);
            // 
            // GetInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 89);
            this.ControlBox = false;
            this.Controls.Add(this.cmdManual);
            this.Controls.Add(this.txtDataEntry);
            this.Controls.Add(this.cmdAbort);
            this.Controls.Add(this.cmdClear);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.pnlDescription);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "GetInputForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.pnlDescription.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private System.Windows.Forms.Timer inputTimeoutTimer;
        private System.Windows.Forms.Button cmdManual;
    }
}
