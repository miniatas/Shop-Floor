/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 2/10/2011
 * Time: 11:11 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ShopFloor
{
	public partial class OptionsForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.ComboBox cboOption;
		private System.Windows.Forms.Label lblDescription;
		
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
			this.cboOption = new System.Windows.Forms.ComboBox();
			this.lblDescription = new System.Windows.Forms.Label();
			this.cmdOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// cboOption
			// 
			this.cboOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboOption.Location = new System.Drawing.Point(4, 58);
			this.cboOption.Name = "cboOption";
			this.cboOption.Size = new System.Drawing.Size(184, 21);
			this.cboOption.TabIndex = 1;
			// 
			// lblDescription
			// 
			this.lblDescription.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDescription.ForeColor = System.Drawing.Color.Blue;
			this.lblDescription.Location = new System.Drawing.Point(4, 8);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(184, 47);
			this.lblDescription.TabIndex = 2;
			this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// cmdOK
			// 
			this.cmdOK.Location = new System.Drawing.Point(4, 85);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(184, 24);
			this.cmdOK.TabIndex = 0;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.CmdOKClick);
			// 
			// OptionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(192, 111);
			this.ControlBox = false;
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.lblDescription);
			this.Controls.Add(this.cboOption);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "OptionsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Options";
			this.Shown += new System.EventHandler(this.OptionsFormShown);
			this.ResumeLayout(false);
		}
	}
}
