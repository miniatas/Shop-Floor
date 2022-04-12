/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 4/7/2011
 * Time: 11:48 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ShopFloor
{
	public partial class UnitInformationForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdClear;
		private System.Windows.Forms.Button cmdAbort;
		private System.Windows.Forms.Label lblJobDescription;
		private System.Windows.Forms.Label lblNotes;
		private System.Windows.Forms.TextBox txtNotes;
		private System.Windows.Forms.TextBox txtPounds;
		private System.Windows.Forms.Label lblPounds;
		private System.Windows.Forms.Label lblUnit;
		private System.Windows.Forms.TextBox txtUnits;
		
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
			this.lblUnit = new System.Windows.Forms.Label();
			this.lblPounds = new System.Windows.Forms.Label();
			this.txtUnits = new System.Windows.Forms.TextBox();
			this.txtPounds = new System.Windows.Forms.TextBox();
			this.txtNotes = new System.Windows.Forms.TextBox();
			this.lblNotes = new System.Windows.Forms.Label();
			this.lblJobDescription = new System.Windows.Forms.Label();
			this.cmdAbort = new System.Windows.Forms.Button();
			this.cmdClear = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblUnit
			// 
			this.lblUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblUnit.Location = new System.Drawing.Point(124, 135);
			this.lblUnit.Name = "lblUnit";
			this.lblUnit.Size = new System.Drawing.Size(67, 20);
			this.lblUnit.TabIndex = 1;
			this.lblUnit.Text = "Feet";
			this.lblUnit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// lblPounds
			// 
			this.lblPounds.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblPounds.Location = new System.Drawing.Point(228, 135);
			this.lblPounds.Name = "lblPounds";
			this.lblPounds.Size = new System.Drawing.Size(67, 20);
			this.lblPounds.TabIndex = 2;
			this.lblPounds.Text = "Pounds";
			this.lblPounds.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// txtUnits
			// 
			this.txtUnits.ForeColor = System.Drawing.Color.Blue;
			this.txtUnits.Location = new System.Drawing.Point(124, 158);
			this.txtUnits.Name = "txtUnits";
			this.txtUnits.Size = new System.Drawing.Size(67, 20);
			this.txtUnits.TabIndex = 0;
			this.txtUnits.Text = "0";
			this.txtUnits.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtUnits.Enter += new System.EventHandler(this.TxtFeetEnter);
			this.txtUnits.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxKeyDownNumbersOnly);
			this.txtUnits.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtFeetKeyPress);
			this.txtUnits.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtFeetKeyUp);
			this.txtUnits.Leave += new System.EventHandler(this.TxtFeetLeave);
			// 
			// txtPounds
			// 
			this.txtPounds.ForeColor = System.Drawing.Color.Blue;
			this.txtPounds.Location = new System.Drawing.Point(228, 158);
			this.txtPounds.Name = "txtPounds";
			this.txtPounds.Size = new System.Drawing.Size(67, 20);
			this.txtPounds.TabIndex = 1;
			this.txtPounds.Text = "0.00";
			this.txtPounds.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtPounds.Enter += new System.EventHandler(this.TxtPoundsEnter);
			this.txtPounds.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxKeyDownNumbersOnly);
			this.txtPounds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPoundsKeyPress);
			this.txtPounds.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtPoundsKeyUp);
			this.txtPounds.Leave += new System.EventHandler(this.TxtPoundsLeave);
			// 
			// txtNotes
			// 
			this.txtNotes.ForeColor = System.Drawing.Color.Blue;
			this.txtNotes.Location = new System.Drawing.Point(10, 199);
			this.txtNotes.Multiline = true;
			this.txtNotes.Name = "txtNotes";
			this.txtNotes.Size = new System.Drawing.Size(400, 128);
			this.txtNotes.TabIndex = 2;
			// 
			// lblNotes
			// 
			this.lblNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblNotes.Location = new System.Drawing.Point(13, 185);
			this.lblNotes.Name = "lblNotes";
			this.lblNotes.Size = new System.Drawing.Size(396, 14);
			this.lblNotes.TabIndex = 2;
			this.lblNotes.Text = "Notes";
			this.lblNotes.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// lblJobDescription
			// 
			this.lblJobDescription.ForeColor = System.Drawing.Color.Blue;
			this.lblJobDescription.Location = new System.Drawing.Point(10, 6);
			this.lblJobDescription.Name = "lblJobDescription";
			this.lblJobDescription.Size = new System.Drawing.Size(400, 133);
			this.lblJobDescription.TabIndex = 5;
			this.lblJobDescription.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// cmdAbort
			// 
			this.cmdAbort.Location = new System.Drawing.Point(244, 339);
			this.cmdAbort.Name = "cmdAbort";
			this.cmdAbort.Size = new System.Drawing.Size(57, 27);
			this.cmdAbort.TabIndex = 5;
			this.cmdAbort.Text = "&Abort";
			this.cmdAbort.UseVisualStyleBackColor = true;
			this.cmdAbort.Click += new System.EventHandler(this.CmdAbortClick);
			// 
			// cmdClear
			// 
			this.cmdClear.Enabled = false;
			this.cmdClear.Location = new System.Drawing.Point(179, 339);
			this.cmdClear.Name = "cmdClear";
			this.cmdClear.Size = new System.Drawing.Size(57, 27);
			this.cmdClear.TabIndex = 4;
			this.cmdClear.Text = "&Clear";
			this.cmdClear.UseVisualStyleBackColor = true;
			this.cmdClear.Click += new System.EventHandler(this.CmdClearClick);
			// 
			// cmdOK
			// 
			this.cmdOK.Enabled = false;
			this.cmdOK.Location = new System.Drawing.Point(114, 339);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(57, 27);
			this.cmdOK.TabIndex = 3;
			this.cmdOK.Text = "&OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.CmdOKClick);
			// 
			// UnitInformationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(420, 373);
			this.Controls.Add(this.cmdAbort);
			this.Controls.Add(this.cmdClear);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.lblJobDescription);
			this.Controls.Add(this.lblNotes);
			this.Controls.Add(this.txtNotes);
			this.Controls.Add(this.txtPounds);
			this.Controls.Add(this.txtUnits);
			this.Controls.Add(this.lblPounds);
			this.Controls.Add(this.lblUnit);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UnitInformationForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UnitInformationFormFormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
