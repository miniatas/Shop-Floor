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
    public partial class UserAdminForm : Form
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
        private SqlDataReader reader;
        private SqlCommand command;
        bool matched = false;

        public UserAdminForm(string userID)
        {
            InitializeComponent();
            cbUserRights.Items.Add("Operator");
            command = new SqlCommand("SELECT DISTINCT [Job Rights] FROM [User Rights Table] WHERE [Active] = 1 ORDER BY [Job Rights]", connection);
            connection.Open();
            reader = command.ExecuteReader();
            while  (reader.Read())
            {
                cbUserRights.Items.Add(reader[0].ToString());
            }

            reader.Close();
            if (!string.IsNullOrEmpty(userID))
            {
                txtUserID.Text = userID;
                txtUserID.Enabled = false;
                txtPassword.Enabled = false;
                txtVerifyPassword.Enabled = false;
                ckChangePassword.Visible = true;
                command = new SqlCommand("SELECT * FROM [Get User Rights] ('" + userID + "')", connection);
                reader = command.ExecuteReader();
                reader.Read();
                if (!string.IsNullOrEmpty(reader[1].ToString()))
                {
                    txtName.Text = reader[1].ToString();
                }

                if (!string.IsNullOrEmpty(reader[2].ToString()))
                {
                    for (int i = 0; i < cbUserRights.Items.Count; i++)
                    {
                        if (reader[2].ToString() == cbUserRights.Items[i].ToString())
                        {
                            cbUserRights.SelectedIndex = i;
                            break;
                        }
                    }
                }

                if (reader.GetBoolean(3))
                {
                    ckCanEditHours.Checked = true;
                }

                if (reader.GetBoolean(4))
                {
                    ckCanConsignInventory.Checked = true;
                }

                if (reader.GetBoolean(5))
                {
                    ckCanOverride.Checked = true;
                }

                if (reader.GetBoolean(6))
                {
                    ckCanAccessAllInventory.Checked = true;
                }

                if (reader.GetBoolean(7))
                {
                    ckNoUPCValidationReqd.Checked = true;
                }

                if (reader.GetBoolean(8))
                {
                    ckAdministrator.Checked = true;
                }
                
                if (reader.GetBoolean(10))
                {
                    ckCanAccessPlates.Checked = true;
                }

                if (reader.GetBoolean(11))
                {
                    ckCanEdiMachinePeriphials.Checked = true;
                }

                if (reader.GetBoolean(12))
                {
                    cmdCapture1.Enabled = false;
                    cmdCapture2.Enabled = false;
                    cmdRecaptureFP.Visible = true;
                }


                if (reader.GetBoolean(14))
                {
                    ckPress.Checked = true;
                }

                if (reader.GetBoolean(16))
                {
                    ckLam.Checked = true;
                }

                if (reader.GetBoolean(18))
                {
                    ckSlit.Checked = true;
                }
                if (reader.GetBoolean(20))
                {
                    ckRework.Checked = true;
                }
                if (reader.GetBoolean(22))
                {
                    ckBagMaking.Checked = true;
                }

                reader.Close();
            }
            else
            {
                cbUserRights.SelectedIndex = 0;
            }

            connection.Close();
            fpReaderManager = new SGFingerPrintManager();
            fpReaderDeviceName = SGFPMDeviceName.DEV_AUTO;
            fpReaderDeviceID = (int)(SGFPMPortAddr.USB_AUTO_DETECT);
            fpReaderError = fpReaderManager.Init(fpReaderDeviceName);
            fpReaderError = fpReaderManager.OpenDevice(fpReaderDeviceID);
            if (fpReaderError == (int)SGFPMError.ERROR_NONE)
            {
                fpReaderError = fpReaderManager.GetDeviceInfo(fpReaderInfo);
            }
            
            if (fpReaderError != (int)SGFPMError.ERROR_NONE)
            {
                ModulesClass.DisplayFPReaderError("OpenDevice()", fpReaderError);
                cmdCapture1.Enabled = false;
            }
            else
            {
                fpImage1 = new byte[fpReaderInfo.ImageWidth * fpReaderInfo.ImageHeight];
                fpImage2 = new byte[fpReaderInfo.ImageWidth * fpReaderInfo.ImageHeight];
                image1Template = new byte[400];
            }
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
                    cmdCapture2.Enabled = true;
                    cmdCapture2.Select();
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
                            cmdCapture1.Enabled = false;
                            cmdCapture2.Enabled = false;
                            cmdRecaptureFP.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("Fingerprint not verified.  Please try again.");
                            pbFP1.Image = null;
                            pbFP2.Image = null;
                            cmdCapture2.Enabled = false;
                            cmdCapture1.Select();
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
        
        private void cmdRecaptureFP_Click(object sender, EventArgs e)
        {
            matched = false;
            pbFP1.Image = null;
            pbFP2.Image = null;
            cmdRecaptureFP.Visible = false;
            cmdCapture1.Enabled = true;
            cmdCapture1.Select();
        }
        
        private void ckChangePassword_CheckedChanged(object sender, EventArgs e)
        {
            if (ckChangePassword.Checked)
            {
                txtPassword.Enabled = true;
                txtVerifyPassword.Enabled = true;
                txtPassword.Select();
            }
            else
            {
                txtPassword.Enabled = false;
                txtVerifyPassword.Enabled = false;
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserID.Text.Trim()))
            {
                MessageBox.Show("User ID Missing.  Please add and try again.", "Missing User ID");
                txtUserID.Focus();
            }
            else if (txtUserID.Text.Trim().Length > 50)
            {
                MessageBox.Show("User ID cannot be over 50 characaters.  Please try again.", "User ID too Long");
                txtUserID.Focus();
            }
            else if (string.IsNullOrEmpty(txtName.Text.Trim()) && (ckPress.Checked || ckLam.Checked || ckSlit.Checked || ckBagMaking.Checked || ckRework.Checked))
            {
                MessageBox.Show("You must enter a user name for an operator.  Please enter one and retry.", "MIssing User Name");
                txtName.Focus();
            }
            else if (txtName.Text.Trim().Length > 50)
            {
                MessageBox.Show("User Name cannot be over 50 characaters.  Please try again.", "User Name too Long");
                txtName.Focus();
            }
            else if (txtPassword.Enabled && (string.IsNullOrEmpty(txtPassword.Text.Trim()) || txtPassword.Text != txtVerifyPassword.Text))
            {
                if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
                {
                    MessageBox.Show("You must create a password.  Please enter and try again.", "Missing password");
                    txtPassword.Focus();
                }
                else
                {
                    MessageBox.Show("Error - passwords don't match.  Please try again.", "Mismatched Passwords");
                    txtPassword.Focus();
                }
            }
            else if (txtPassword.Enabled && txtPassword.Text.Trim().Length < 8)
            {
                MessageBox.Show("Error - password must be at least 8 characters.  Please try again.", "Password too Short");
                txtPassword.Focus();
            }
            else if (!matched && pbFP1.Image != null)
            {
                MessageBox.Show("Error - fingerprint not verified.  Please verify and try again.", "Fingerprint not Verified");
                pbFP1.Select();
            }
            else
            {
                string insertCommand;
                insertCommand = "EXECUTE [dbo].[Add or Edit SQL User Stored Procedure] '" + txtUserID.Text.Trim().Replace("'", "''") + "', ";
                if (string.IsNullOrEmpty(txtName.Text.Trim()))
                {
                    insertCommand += "NULL, ";
                }
                else
                {
                    insertCommand += "'" + txtName.Text.Trim().Replace("'", "''") + "', ";
                }

                if (ckPress.Checked)
                {
                    insertCommand += "1, ";
                }
                else
                {
                    insertCommand += "0, ";
                }

                if (ckLam.Checked)
                {
                    insertCommand += "1, ";
                }
                else
                {
                    insertCommand += "0, ";
                }

                if (ckSlit.Checked)
                {
                    insertCommand += "1, ";
                }
                else
                {
                    insertCommand += "0, ";
                }

                if (ckBagMaking.Checked)
                {
                    insertCommand += "1, ";
                }
                else
                {
                    insertCommand += "0, ";
                }

                if (ckRework.Checked)
                {
                    insertCommand += "1, ";
                }
                else
                {
                    insertCommand += "0, ";
                }

                insertCommand += "'" + cbUserRights.Text + "', ";
                if (txtPassword.Enabled)
                {
                    insertCommand += "'" + txtPassword.Text.Trim().Replace("'", "''") + "', ";
                }
                else
                {
                    insertCommand += "NULL, ";
                }

                if (ckCanEditHours.Checked)
                {
                    insertCommand += "1, ";
                }
                else
                {
                    insertCommand += "0, ";
                }

                if (ckCanConsignInventory.Checked)
                {
                    insertCommand += "1, ";
                }
                else
                {
                    insertCommand += "0, ";
                }

                if (ckCanOverride.Checked)
                {
                    insertCommand += "1, ";
                }
                else
                {
                    insertCommand += "0, ";
                }

                if (ckCanAccessAllInventory.Checked)
                {
                    insertCommand += "1, ";
                }
                else
                {
                    insertCommand += "0, ";
                }

                if (ckAdministrator.Checked)
                {
                    insertCommand += "1, ";
                }
                else
                {
                    insertCommand += "0, ";
                }

                if (ckNoUPCValidationReqd.Checked)
                {
                    insertCommand += "1, ";
                }
                else
                {
                    insertCommand += "0, ";
                }

                if (ckCanAccessPlates.Checked)
                {
                    insertCommand += "1, ";
                }
                else
                {
                    insertCommand += "0, ";
                }

                if (ckCanEdiMachinePeriphials.Checked)
                {
                    insertCommand += "1, ";
                }
                else
                {
                    insertCommand += "0, ";
                }

                if (matched)
                {
                    command = new SqlCommand(insertCommand + " @ByteValue", connection);
                    command.Parameters.Add("@ByteValue", SqlDbType.Binary).Value = image1Template;
                }
                else
                {
                    command = new SqlCommand(insertCommand + " NULL", connection);
                }

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("User Saved", "Success!");
                this.Close();
            }

        }

        private void txtUserID_Leave(object sender, EventArgs e)
        {
            if (txtUserID.Text.Trim().Length > 0)
            {
                command = new SqlCommand("SELECT name FROM sys.server_principals WHERE name = '" + txtUserID.Text.Trim().Replace("'", "''") + "'", connection);
                connection.Open();
                string userName = (string)command.ExecuteScalar();
                connection.Close();
                if (!string.IsNullOrEmpty(userName))
                {
                    MessageBox.Show("User " + txtUserID.Text + " already exists.  You must edit this user.", "Duplicate User ID");
                    this.Close();
                }
            }
        }
    }
}
