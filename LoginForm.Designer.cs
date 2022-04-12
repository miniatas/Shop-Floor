/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 3/10/2011
 * Time: 2:24 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ShopFloor
{
	public partial class LoginForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label lblPassword;
		private System.Windows.Forms.Button cmdAbort;
		private System.Windows.Forms.Button cmdLogin;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.TextBox txtUserName;
		private System.Windows.Forms.Label lblUserName;
		
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
			this.lblUserName = new System.Windows.Forms.Label();
			this.lblPassword = new System.Windows.Forms.Label();
			this.txtUserName = new System.Windows.Forms.TextBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.cmdLogin = new System.Windows.Forms.Button();
			this.cmdAbort = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblUserName
			// 
			this.lblUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (byte)0);
			this.lblUserName.Location = new System.Drawing.Point(12, 35);
			this.lblUserName.Name = "lblUserName";
			this.lblUserName.Size = new System.Drawing.Size(94, 24);
			this.lblUserName.TabIndex = 0;
			this.lblUserName.Text = "User Name:";
			// 
			// lblPassword
			// 
			this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (byte)0);
			this.lblPassword.Location = new System.Drawing.Point(12, 71);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(69, 24);
			this.lblPassword.TabIndex = 1;
			this.lblPassword.Text = "Password:";
			// 
			// txtUserName
			// 
			this.txtUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtUserName.Location = new System.Drawing.Point(96, 36);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.Size = new System.Drawing.Size(132, 20);
			this.txtUserName.TabIndex = 0;
			this.txtUserName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtUserNameKeyPress);
			// 
			// txtPassword
			// 
			this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtPassword.Location = new System.Drawing.Point(96, 72);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(132, 20);
			this.txtPassword.TabIndex = 1;
			this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPasswordKeyPress);
			// 
			// cmdLogin
			// 
			this.cmdLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			this.cmdLogin.Location = new System.Drawing.Point(12, 135);
			this.cmdLogin.Name = "cmdLogin";
			this.cmdLogin.Size = new System.Drawing.Size(80, 30);
			this.cmdLogin.TabIndex = 2;
			this.cmdLogin.Text = "Login";
			this.cmdLogin.Click += new System.EventHandler(this.CmdLoginClick);
			// 
			// cmdAbort
			// 
			this.cmdAbort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			this.cmdAbort.Location = new System.Drawing.Point(148, 135);
			this.cmdAbort.Name = "cmdAbort";
			this.cmdAbort.Size = new System.Drawing.Size(80, 30);
			this.cmdAbort.TabIndex = 3;
			this.cmdAbort.Text = "Abort";
			this.cmdAbort.Click += new System.EventHandler(this.CmdAbortClick);
			// 
			// LoginForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(240, 185);
			this.ControlBox = false;
			this.Controls.Add(this.cmdAbort);
			this.Controls.Add(this.cmdLogin);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.txtUserName);
			this.Controls.Add(this.lblPassword);
			this.Controls.Add(this.lblUserName);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			this.Name = "LoginForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Login";
			this.ResumeLayout(false);
			this.PerformLayout();
		}
	}
}
