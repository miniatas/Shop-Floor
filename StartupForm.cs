using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ShopFloor
{
    public partial class StartupForm : Form
    {
        private static bool workingFPScanner;
        private static bool fpScanDefault;
        private static string loginName = string.Empty;
        public StartupForm()
        {
            this.InitializeComponent();
            SqlConnection connection = new SqlConnection("Data Source=" + ServerName + "; User ID=sa; Password=" + Password + ";database=" + Database + ";Connection Timeout=60;Persist Security Info=False;");
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
                    loginName = ModulesClass.GetFPUserID();
                }
                else
                {
                    LoginForm tryLoginForm = new LoginForm();
                    tryLoginForm.ShowDialog();
                    loginName = tryLoginForm.UserName;
                    tryLoginForm.Dispose();
                }

                if (!string.IsNullOrEmpty(loginName))
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
                    Environment.Exit(Environment.ExitCode);
                }
                else
                {
                    Environment.Exit(Environment.ExitCode);
                }
            }
            else
            {
                MessageBox.Show("You are using an outdated version of the Shop Floor Program.  Please create a shortcut to or run the program " + reader[1].ToString(), "Outdated Program");
                reader.Close();
                connection.Close();
                Environment.Exit(Environment.ExitCode);
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToLower")]
        public static string UserName
        {
            get
            {
                return loginName;
            }
        }

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
    }
}
