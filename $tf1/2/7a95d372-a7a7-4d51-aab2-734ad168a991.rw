/*
 * Created by SharpDevelop.
 * User: minimumiatas
 * Date: 3/29/2011
 * Time: 3:11 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ShopFloor
{
	public partial class DateTimePickerForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.DateTimePicker dtpStartTime;
		private System.Windows.Forms.Button cmdOK;
		
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
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.cmdOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.CustomFormat = "MMMM d, yyyy h:mm tt";
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartTime.Location = new System.Drawing.Point(12, 31);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(223, 20);
            this.dtpStartTime.TabIndex = 1;
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(82, 230);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(100, 38);
            this.cmdOK.TabIndex = 0;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.CmdOKClick);
            // 
            // DateTimePickerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(247, 280);
            this.ControlBox = false;
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.dtpStartTime);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DateTimePickerForm";
            this.ResumeLayout(false);

		}
    }
}
