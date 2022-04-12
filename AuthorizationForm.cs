/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 9/19/2012
 * Time: 9:27 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace ShopFloor
{
	using System;
	using System.Data.SqlClient;
	using System.Windows.Forms;
	
	/// <summary>
	/// Authrorization to Override Form.
	/// </summary>
	public partial class AuthorizationForm : Form
	{
		private bool authorize;
        private bool qualityCheck;

		public AuthorizationForm(bool parQualityCheck)
		{
			this.InitializeComponent();
            qualityCheck = parQualityCheck;
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToLower")]
		public string UserName
		{
			get
			{
				return txtUserName.Text.ToLower();	
			}
		}
		
		public bool OKToOverride
		{
			get
			{
				return authorize;
			}
		}
		
		private void CmdAbortClick(object sender, EventArgs e)
		{
			authorize = false;
			this.Close();
		}
		
		private void CmdAuthorizeClick(object sender, EventArgs e)
		{
			TryAuthorization();
		}
		
		private void TxtUserNameKeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals((char)Keys.Return) || e.KeyChar.Equals((char)Keys.Enter))
			{
				e.Handled = true;
				txtPassword.Focus();
			}
		}
		
		private void TxtPasswordKeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals((char)Keys.Return) || e.KeyChar.Equals((char)Keys.Enter))
			{
				e.Handled = true;
				TryAuthorization();
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void TryAuthorization()
		{
			SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=" + txtUserName.Text + ";Password=" + txtPassword.Text + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False;");
            SqlCommand command;
            try
			{
				connection.Open();
                if (qualityCheck)
                {
                    command = new SqlCommand("SELECT CAST(CASE WHEN [Can Override] = 1 OR [Job Rights] = 'QA' THEN 1 ELSE 0 END AS bit) FROM [User Rights Table]  WHERE [User ID] = '" + txtUserName.Text.ToLower() + "'", connection);
                }
                else
                {
                    command = new SqlCommand("SELECT [Can Override] FROM [User Rights Table] WHERE [User ID] = '" + txtUserName.Text.ToLower() + "'", connection);
                }
				
				bool? canOverride = (bool?)command.ExecuteScalar();
				if (canOverride.HasValue && (bool)canOverride)
				{
					authorize = true;
					connection.Close();
					this.Close();
				}
				else
				{
                    if (qualityCheck)
                    {
                        MessageBox.Show(txtUserName.Text + "\" does not have Quality Control rights", "User Cannot QA Check");
                    }
                    else
                    {
                        MessageBox.Show("Error - either the password is wrong or \"" + txtUserName.Text + "\" does not have override authorization rights", "User Cannot Override");
                    }

                    connection.Close();
				}
			}			
			catch (SqlException ex)
			{
				if (ex.Number.Equals(18456))
				{
					MessageBox.Show("Invalid login for user " + txtUserName.Text, "Invalid Login");
				}
				else	
				{
					MessageBox.Show("Error " + ex.Number.ToString() + " - " + ex.Message + "\r\n" + ex.StackTrace, "SQL Error");
				}
				
				txtPassword.Text = string.Empty;
				txtPassword.Focus();
				connection.Close();
			}
		}

        private void AuthorizationForm_Shown(object sender, EventArgs e)
        {
            if (MainForm.UserCanOverride)
            {
                authorize = true;
                txtUserName.Text = StartupForm.UserName;
                this.Close();
            }
        }
    }
}
