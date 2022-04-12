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
	using System;
	using System.Data.SqlClient;
	using System.Windows.Forms;
	
	/// <summary>
	/// Login form for the Shop Floor System.
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login")]
	public partial class LoginForm : Form
	{
        private static bool workingFPScanner;
        private static bool fpScanDefault;

        public LoginForm()
        {
            InitializeComponent();
        }
/*          SqlConnection connection = new SqlConnection("Data Source=" + ServerName + ";User ID=sa;Password=" + Password + ";database=" + Database + ";Connection Timeout=60;Persist Security Info=False;");
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT [Current Version], [Current Path and File Name], [Fingerprint Scan Default] FROM [Current Program Version Table]", connection);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            // Check Program Version
            if (reader[0].ToString() == "4") // Current Version
            {
                fpScanDefault = reader.GetBoolean(2);
                reader.Close();
                command = new SqlCommand("SELECT COUNT(*) FROM [Workstation Machine Assignment Table] WHERE [Workstation Name] = '" + System.Environment.MachineName.ToString().Replace("'", "''") + "' AND [Working Fingerprint Scanner] = 1", connection);
                int fpscanner = (int)command.ExecuteScalar();
                if (fpscanner == 0)
                {
                    command = new SqlCommand("SELECT COUNT(*) FROM [Non-Production Workstation with Fingerprint Scanner] WHERE [Workstation Name] = '" + System.Environment.MachineName.ToString().Replace("'", "''") + "'", connection);
                    fpscanner = (int)command.ExecuteScalar();
                    if (fpscanner == 0)
                    {
                        workingFPScanner = false;
                        connection.Close();
                    }
                    else
                    {
                        workingFPScanner = true;
                    }
                }
                else
                {
                    workingFPScanner = true;
                }

                if (workingFPScanner && fpScanDefault)
                {
                    int tries = 0;
                    string fpUserName = string.Empty;
                    while (tries < 3 && string.IsNullOrEmpty(fpUserName))
                    {
                        fingerPrintLoginForm matchFingerPrintForm = new fingerPrintLoginForm();
                        matchFingerPrintForm.ShowDialog();
                        if (string.IsNullOrEmpty(matchFingerPrintForm.UserID))
                        {
                            if (tries == 2)
                            {
                              //  ModulesClass.SendEmail(3, loginName + " Fingerprint Verifcation Failed", "On " + DateTime.Now.ToLongDateString() + " at " + DateTime.Now.ToLongTimeString() + " the fingerprint verificaiton for " + loginName + " failed 3 times");
                                MessageBox.Show("Fingerprint not found.  Please see your supervisor to rescan your fingerprint", "Invalid Fingerprint");
                                loginSuccess = false;
                            }

                            tries++;
                        }
                        else if (matchFingerPrintForm.UserID != loginName)
                        {
                            ModulesClass.SendEmail(3, "FALSE POSITIVE FINGERPRINT MATCH", "On " + DateTime.Now.ToLongDateString() + " at " + DateTime.Now.ToLongTimeString() + " the fingerprint scan for " + loginName + " came up as a match for " + fpUserName + ".  Maybe " + fpUserName + " scanned their finger instezad of " + loginName + "?");

                            fpUserName = string.Empty;
                            tries++;
                        }
                        else
                        {
                            fpUserName = matchFingerPrintForm.UserID;
                            if (tries > 0)
                            {
                                ModulesClass.SendEmail(3, loginName + " Fingerprint Verifcation Failed " + tries.ToString() + " Time(s) before Working", "On " + DateTime.Now.ToLongDateString() + " at " + DateTime.Now.ToLongTimeString() + " the fingerprint verificaiton for " + loginName + " failed " + tries.ToString() + " time(s) before suceeding");
                            }
                        }

                        matchFingerPrintForm.Dispose();
                    }

                    if (loginSuccess)
                    {
                        this.Visible = false;
                        MainForm frmMain = new MainForm();
                        DialogResult okToContinue = DialogResult.Yes;
                        if (Database != "Inventory Control")
                        {
                            okToContinue = MessageBox.Show("WARNING: YOU ARE IN A TEST ENVIRONMENT. DO YOU WISH TO CONTINUE (ONLY SAY YES IF YOU KNOW WHAT YOU ARE DOING!)", "Continue?", MessageBoxButtons.YesNo);
                        }

                        if (okToContinue == DialogResult.Yes)
                        {
                            frmMain.ShowDialog();
                        }

                        frmMain.Dispose();
                    }

                    Application.Exit();
                }
            }
            else
            {
                MessageBox.Show("You are using an outdated version of the Shop Floor Program.  Please create a shortcut to or run the program " + reader[1].ToString(), "Outdated Program");
                reader.Close();
                connection.Close();
                Application.Exit();
            }
        }

        public static string ServerName
        {
            get
            {
                return "ovesql01";
            }
        }

        public static string Database
		{
			get
			{
				return "Inventory Control";	
			}
		}
		
		public static string FGDatabase
		{
			get
			{
				if (Database == "Inventory Control")
				{
					return "FinishedGoods";
				}
				else
				{
					return "FinishedGoods - Test";
					        
				}
			}
		}
		
		public static string Password
		{
			get
			{
				return "c0l0mbIa";	
			}
		}
*/		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToLower")]
		public string UserName
		{
			get
			{
                return txtUserName.Text.ToLower();
			}
		}
/*
        public static bool WorkingFPScanner
        {
            get
            {
                return workingFPScanner;
            }
        }

        public static bool FpScanDefault
        {
            get
            {
                return fpScanDefault;
            }
        }
	*/	
		private void CmdAbortClick(object sender, EventArgs e)
		{
            //Application.Exit();
            txtUserName.Text = string.Empty;
            this.Close();
		}
		
		private void CmdLoginClick(object sender, EventArgs e)
		{
			TryLogin();
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
				TryLogin();
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void TryLogin()
		{
			SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=" + txtUserName.Text.Replace("'", "''") + ";Password=" + txtPassword.Text.Replace("'", "''") + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False;");
			try
			{
			    connection.Open();
                /*				SqlCommand command = new SqlCommand("SELECT [Current Version], [Current Path and File Name], [Fingerprint Scan Default] FROM [Current Program Version Table]", connection);
                                SqlDataReader reader = command.ExecuteReader();
                                reader.Read();
                                // Check Program Version
                                if (reader[0].ToString() == "4") // Current Version
                                {
                                    fpScanDefault = reader.GetBoolean(2);
                                    reader.Close();
                                    loginName = txtUserName.Text.ToLower();
                                    command = new SqlCommand("SELECT COUNT(*) FROM [Workstation Machine Assignment Table] WHERE [Workstation Name] = '" + System.Environment.MachineName.ToString().Replace("'", "''") + "' AND [Working Fingerprint Scanner] = 1", connection);
                                    int fpscanner = (int)command.ExecuteScalar();
                                    if (fpscanner == 0)
                                    {
                                        command = new SqlCommand("SELECT COUNT(*) FROM [Non-Production Workstation with Fingerprint Scanner] WHERE [Workstation Name] = '" + System.Environment.MachineName.ToString().Replace("'", "''") + "'", connection);
                                        fpscanner = (int)command.ExecuteScalar();
                                        if (fpscanner == 0)
                                        {
                                            workingFPScanner = false;
                                            connection.Close();
                                        }
                                        else
                                        {
                                            workingFPScanner = true;
                                        }
                                    }
                                    else
                                    {
                                        workingFPScanner = true;
                                    }

                                    if (workingFPScanner)
                                    { 
                                        command = new SqlCommand("SELECT COUNT(*) FROM [Fingerprint Template Table] WHERE [User ID] = '" + loginName + "'", connection);
                                        int match = (int)command.ExecuteScalar();
                                        connection.Close();
                                        if (match == 0)
                                        {
                                            registerFingerprintForm newFingerprintForm = new registerFingerprintForm();
                                            newFingerprintForm.ShowDialog();
                                            newFingerprintForm.Close();
                                        }
                                        else
                                        {
                                            int tries = 0;
                                            string fpUserName = string.Empty;
                                            while (tries < 3 && string.IsNullOrEmpty(fpUserName))
                                            {
                                                fingerPrintLoginForm matchFingerPrintForm = new fingerPrintLoginForm();
                                                matchFingerPrintForm.ShowDialog();
                                                if (string.IsNullOrEmpty(matchFingerPrintForm.UserID))
                                                {
                                                    if (tries == 2)
                                                    {
                                                        ModulesClass.SendEmail(3, loginName + " Fingerprint Verifcation Failed", "On " + DateTime.Now.ToLongDateString() + " at " + DateTime.Now.ToLongTimeString() + " the fingerprint verificaiton for " + loginName + " failed 3 times");
                                                    }

                                                    tries++;
                                                }
                                                else if (matchFingerPrintForm.UserID != loginName)
                                                {
                                                    ModulesClass.SendEmail(3, "FALSE POSITIVE FINGERPRINT MATCH", "On " + DateTime.Now.ToLongDateString() + " at " + DateTime.Now.ToLongTimeString() + " the fingerprint scan for " + loginName + " came up as a match for " + fpUserName + ".  Maybe " + fpUserName + " scanned their finger instezad of " + loginName + "?");

                                                    fpUserName = string.Empty;
                                                    tries++;
                                                }
                                                else
                                                {
                                                    fpUserName = matchFingerPrintForm.UserID;
                                                    if (tries > 0)
                                                    {
                                                        ModulesClass.SendEmail(3, loginName + " Fingerprint Verifcation Failed " + tries.ToString() + " Time(s) before Working", "On " + DateTime.Now.ToLongDateString() + " at " + DateTime.Now.ToLongTimeString() + " the fingerprint verificaiton for " + loginName + " failed " + tries.ToString() + " time(s) before suceeding");
                                                    }
                                                }

                                                matchFingerPrintForm.Dispose();
                                            }
                                        }
                                    }
                                    this.Visible = false;
                                    MainForm frmMain = new MainForm();
                                    /*DialogResult okToContinue = DialogResult.Yes;
                                    if (StartupForm.Database != "Inventory Control")
                                    {
                                        okToContinue = MessageBox.Show("WARNING: YOU ARE IN A TEST ENVIRONMENT. DO YOU WISH TO CONTINUE (ONLY SAY YES IF YOU KNOW WHAT YOU ARE DOING!)", "Continue?", MessageBoxButtons.YesNo);
                                    }

                                    if (okToContinue== DialogResult.Yes)
                                    {
                                        frmMain.ShowDialog();
                                    }

                                    frmMain.Dispose();
                                    Application.Exit();
                                }
                                else
                                {
                                    MessageBox.Show("You are using an outdated version of the Shop Floor Program.  Please create a shortcut to or run the program " + reader[1].ToString(), "Outdated Program");
                                    reader.Close();
                                    connection.Close();
                                    Application.Exit();
                                }
                                */
                connection.Close();
                this.Close();
			}			
			catch (SqlException ex)
			{
				if (ex.Number.Equals(18456))
				{
                    MessageBox.Show("Invalid login for user " + txtUserName.Text, "Invalid Login");
                    /*if (Database != "Inventory Control")
                    {
                        MessageBox.Show("WARNING: YOU ARE IN A TEST ENVIRONMENT - GO TELL YOUR LEAD YOU ARE IN THE WRONG PROGRAM!)", "NOT THE LIVE ENVIRONMENT");
                    }
                    */
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
	}
}
