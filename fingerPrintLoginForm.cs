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
using SecuGen.FDxSDKPro.Windows;

namespace ShopFloor
{
    public partial class fingerPrintLoginForm : Form
    {
        private SGFPMDeviceName fpReaderDeviceName;
        private int fpReaderDeviceID;
        private int fpReaderError;
       
        private SGFingerPrintManager fpReaderManager;
        private SGFPMDeviceInfoParam fpReaderInfo = new SGFPMDeviceInfoParam();
        private SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
        private SqlDataReader reader;
        private SqlCommand command;
        private string userName = string.Empty;

        public fingerPrintLoginForm()
        {
            InitializeComponent();
        }

        public string UserID
        {
            get
            {
                return userName;
            }
        }

        private void fingerPrintLoginForm_Shown(object sender, EventArgs e)
        {
            fpReaderManager = new SGFingerPrintManager();
            fpReaderDeviceName = SGFPMDeviceName.DEV_AUTO;
            fpReaderDeviceID = (int)(SGFPMPortAddr.USB_AUTO_DETECT);
            fpReaderError = fpReaderManager.Init(fpReaderDeviceName);
            fpReaderError = fpReaderManager.OpenDevice(fpReaderDeviceID);
            if (fpReaderError == (int)SGFPMError.ERROR_NONE)
            {
                fpReaderError = fpReaderManager.GetDeviceInfo(fpReaderInfo);
            }
            else
            {
                ModulesClass.SendEmail(3, "Fingerprint Scanner not Found on login", "On " + DateTime.Now.ToLongDateString() + " at " + DateTime.Now.ToLongTimeString() + " on Workstation " + System.Environment.MachineName.ToString().Replace("'", "''") + " when " + StartupForm.UserName + " tried to login their fingerprint no fingerprint scanner was found.");
                MessageBox.Show("Error - no fingerprint scanner found. (Is it unplugged?).  Press OK to continue.", "No Fingerprint Scanner Found");
            }

            if (fpReaderError != (int)SGFPMError.ERROR_NONE)
            {
                ModulesClass.DisplayFPReaderError("OpenDevice()", fpReaderError);
                this.Close();
            }
            else
            {
                byte[] fpImage;
                byte[] imageTemplate;
                byte[] storedTemplate;
                fpImage = new byte[fpReaderInfo.ImageWidth * fpReaderInfo.ImageHeight];
                imageTemplate = new byte[400];
                storedTemplate = new byte[400];
                fpReaderError = fpReaderManager.GetImageEx(fpImage, 10000, pbFp.Handle.ToInt32(), 80);
                if (fpReaderError == 0)
                {
                    fpReaderError = fpReaderManager.CreateTemplate(null, fpImage, imageTemplate);
                    if (fpReaderError != (int)SGFPMError.ERROR_NONE)
                    {
                        ModulesClass.DisplayFPReaderError("CreateTemplate()", fpReaderError);
                        this.Close();
                    }
                    else
                    {
                        bool matched = false;
                        command = new SqlCommand("SELECT [User ID], [Fingerprint Template] FROM [Fingerprint Template Table] WHERE [Active] = 1", connection);
                        connection.Open();
                        reader = command.ExecuteReader();
                        while (reader.Read() && !matched)
                        {
                            storedTemplate = (byte[])reader[1];
                            fpReaderError = fpReaderManager.MatchTemplateEx(imageTemplate, SGFPMTemplateFormat.SG400, 0, storedTemplate, SGFPMTemplateFormat.SG400, 0, SGFPMSecurityLevel.NORMAL, ref matched);
                            if (fpReaderError == (int)SGFPMError.ERROR_NONE)
                            {
                                if (matched)
                                {
                                    userName = reader[0].ToString();
                                }
                            }
                            else
                            {
                                ModulesClass.DisplayFPReaderError("MatchTemplate()", fpReaderError);
                            }
                        }

                        if (!matched)
                        {
                            MessageBox.Show("Fingerprint Not Found", "No Match");
                        }

                        reader.Close();
                        connection.Close();
                        this.Close();
                    }
                }
                else
                {
                    ModulesClass.DisplayFPReaderError("GetLiveImageEx()", fpReaderError);
                    this.Close();
                }
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
