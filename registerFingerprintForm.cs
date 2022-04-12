using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SecuGen.FDxSDKPro.Windows;

namespace ShopFloor
{
    public partial class registerFingerprintForm : Form
    {
        private SGFPMDeviceName fpReaderDeviceName;
        private int fpReaderDeviceID;
        private int fpReaderError;
        private byte[] fpImage1;
        private byte[] fpImage2;
        private byte[] image1Template;
        private SGFingerPrintManager fpReaderManager;
        private SGFPMDeviceInfoParam fpReaderInfo = new SGFPMDeviceInfoParam();
        private SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
        private SqlCommand command;
        bool matched = false;
        int tries = 0;
        public registerFingerprintForm()
        {
            InitializeComponent();
        }

        private void cmdCapture1_Click(object sender, EventArgs e)
        {
            fpReaderError = fpReaderManager.GetImage(fpImage1);
            if (fpReaderError == (int)SGFPMError.ERROR_NONE)
            {
                fpReaderError = fpReaderManager.CreateTemplate(null, fpImage1, image1Template);
                if (fpReaderError != (int)SGFPMError.ERROR_NONE)
                {
                    ModulesClass.DisplayFPReaderError("CreateTemplate()", fpReaderError);
                    pbFP1.Image = null;
                }
                else
                {
                    DrawImage(fpImage1, pbFP1);
                    cmdVerifyandSave.Enabled = true;
                    cmdVerifyandSave.Select();
                }
            }
            else
            {
                ModulesClass.DisplayFPReaderError("GetImage()", fpReaderError);
            }
        }

        private void cmdCapture2_Click(object sender, EventArgs e)
        {
            byte[] image2Template;


            image2Template = new byte[400];
            fpReaderError = fpReaderManager.GetImage(fpImage2);
            if (fpReaderError == (int)SGFPMError.ERROR_NONE)
            {
                fpReaderError = fpReaderManager.CreateTemplate(null, fpImage2, image2Template);
                if (fpReaderError != (int)SGFPMError.ERROR_NONE)
                {
                    ModulesClass.DisplayFPReaderError("CreateTemplate()", fpReaderError);
                    pbFP2.Image = null;
                }
                else
                {
                    DrawImage(fpImage2, pbFP2);
                    fpReaderError = fpReaderManager.MatchTemplate(image1Template, image2Template, SGFPMSecurityLevel.NORMAL, ref matched);
                    if (fpReaderError == (int)SGFPMError.ERROR_NONE)
                    {
                        if (matched)
                        {
                            MessageBox.Show("Fingerprint Verified");
                            command = new SqlCommand("INSERT INTO [Fingerprint Template Table] SELECT '" + StartupForm.UserName + "', " + " @ByteValue" + ", 1", connection);
                            command.Parameters.Add("@ByteValue", SqlDbType.Binary).Value = image1Template;
                            connection.Open();
                            command.ExecuteNonQuery();
                            connection.Close();
                            this.Close();
                        }
                        else
                        {
                            if (tries == 2)
                            {
                                MessageBox.Show("Fingerprint registration Failed (Next time please make sure to try a different finger).", "No Fingerprint Registered");
                                ModulesClass.SendEmail(3, "Failed Fingerprint Registration for " + StartupForm.UserName, "At " + DateTime.Now.ToShortDateString() + " " + StartupForm.UserName + "'s fingerprint registration field 3 times.");
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Fingerprint not verified. Maybe try another finger? Please try again.", "Try Again");
                                pbFP1.Image = null;
                                pbFP2.Image = null;
                                cmdVerifyandSave.Enabled = false;
                                tries++;
                                cmdCapture1.Select();
                            }
                        }
                    }
                    else
                    {
                        ModulesClass.DisplayFPReaderError("MatchTemplate()", fpReaderError);
                    }
                }
            }
            else
            {
                ModulesClass.DisplayFPReaderError("GetImage()", fpReaderError);
            }
        }

        private void DrawImage(Byte[] imageData, PictureBox picBox)
        {
            int colorValue;
            Bitmap bmp = new Bitmap(fpReaderInfo.ImageWidth, fpReaderInfo.ImageHeight);
            picBox.Image = (Image)bmp;

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    colorValue = (int)imageData[(j * fpReaderInfo.ImageWidth) + i];
                    bmp.SetPixel(i, j, Color.FromArgb(colorValue, colorValue, colorValue));
                }
            }
            picBox.Refresh();
        }

        private void registerFingerprintForm_Shown(object sender, EventArgs e)
        {
            fpReaderManager = new SGFingerPrintManager();
            fpReaderDeviceName = SGFPMDeviceName.DEV_AUTO;
            fpReaderDeviceID = (int)(SGFPMPortAddr.USB_AUTO_DETECT);
            fpReaderError = fpReaderManager.Init(fpReaderDeviceName);
            fpReaderError = fpReaderManager.OpenDevice(fpReaderDeviceID);
            if (fpReaderError != (int)SGFPMError.ERROR_NONE)
            {
                ModulesClass.SendEmail(3, "Fingerprint Scanner not Found on regisration", "On " + DateTime.Now.ToLongDateString() + " at " + DateTime.Now.ToLongTimeString() + " on Workstation " + System.Environment.MachineName.ToString().Replace("'", "''") + " when " + StartupForm.UserName + " tried to register their fingerprint no fingerprint scanner was found.");
                MessageBox.Show("Error - no fingerprint scanner found. (Is it unplugged?).  Press OK to continue.", "No Fingerprint Scanner Found");
                this.Close();
            }
            else
            {
                fpReaderError = fpReaderManager.GetDeviceInfo(fpReaderInfo);
                if (fpReaderError != (int)SGFPMError.ERROR_NONE)
                {
                    ModulesClass.DisplayFPReaderError("OpenDevice()", fpReaderError);
                    this.Close();
                }
                else
                {
                    fpImage1 = new byte[fpReaderInfo.ImageWidth * fpReaderInfo.ImageHeight];
                    fpImage2 = new byte[fpReaderInfo.ImageWidth * fpReaderInfo.ImageHeight];
                    image1Template = new byte[400];
                }
            }
        }
    }
}
