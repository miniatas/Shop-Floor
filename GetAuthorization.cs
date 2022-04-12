using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ShopFloor
{
    public partial class GetAuthorizationOld : Form
    {
        private bool qualityCheck;
        private bool authorized;
        private string loginName;
        public GetAuthorizationOld(bool parQualityCheck)
        {
            InitializeComponent();
            this.Visible = false;
            qualityCheck = parQualityCheck;
        }


        private void GetAuthorization_Shown(object sender, EventArgs e)
        {
            if (StartupForm.FpScanDefault && StartupForm.WorkingFPScanner)
            {
                loginName = ModulesClass.GetFPUserID();
            }
            else
            {
                LoginForm getLoginForm = new LoginForm();
                getLoginForm.ShowDialog();
                loginName = getLoginForm.UserName;
                getLoginForm.Dispose();
            }

            if (!string.IsNullOrEmpty(loginName))
            {
                SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False;");
                SqlCommand command;
                connection.Open();
                if (qualityCheck)
                {
                    command = new SqlCommand("SELECT CAST(CASE WHEN [Can Override] = 1 OR [Job Rights] = 'QA' THEN 1 ELSE 0 END AS bit) FROM [User Rights Table]  WHERE [User ID] = '" + loginName + "'", connection);
                }
                else
                {
                    command = new SqlCommand("SELECT [Can Override] FROM [User Rights Table] WHERE [User ID] = '" + loginName + "'", connection);
                }

                bool? canOverride = (bool?)command.ExecuteScalar();
                if (canOverride.HasValue && (bool)canOverride)
                {
                    authorized = true;
                    connection.Close();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error - either the password is wrong or \"" + loginName + "\" does not have override authorization rights", "User Cannot Override");
                    connection.Close();
                }
            }
            else
            {
                authorized = false;
            }
        }

        public string UserName
        {
            get
            {
                return loginName;
            }
        }

        public bool OKToOverride
        {
            get
            {
                return authorized;
            }
        }
    }
}
