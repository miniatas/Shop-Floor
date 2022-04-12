/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 9/29/2011
 * Time: 1:40 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace ShopFloor
{
    using ShopFloor.Classes;
    using System;
	using System.Data.SqlClient;
	using System.Drawing;
	using System.Windows.Forms;
	
	/// <summary>
	/// The quality control input from for printing.
	/// </summary>
	public partial class PrintingQCCheckForm : Form
	{
		private string rollNumber;
        private string jobJacketNumber;

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		public PrintingQCCheckForm(string rollId, string jobInformation, decimal printedFilmWidth, decimal printRepeat, decimal filmGauge, 
            string jobJacketNumber)
		{
			InitializeComponent();
            this.jobJacketNumber = jobJacketNumber;
			rollNumber = rollId;
			rtbJobInformation.Text = jobInformation;

			txtPrintedFilmWidth.Text = printedFilmWidth.ToString("N4");
			txtPrintRepeat.Text = printRepeat.ToString("N4");
			txtFilmGauge.Text = filmGauge.ToString("N3");
		}

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]


   
        private void PrintingQCCheckFormFormClosing(object sender, FormClosingEventArgs e)
		{
			DialogResult answer = MessageBox.Show("You will lose all information entered.  Exit anyway?", "Warning!", MessageBoxButtons.YesNo);
			if (answer == DialogResult.No)
			{
				e.Cancel = true;
			}
		}

    

		
		private void WritePrintQCRecord(bool accept)
		{
			string sqlScript = "insert into [Print QC Check Table] select " + rollNumber + ",";
			if (accept)
			{
				sqlScript += "1";
			}
			else
			{
				sqlScript += "0";
			}
			
			sqlScript += ",'" + StartupForm.UserName + "',GETDATE()," + txtPrintedFilmWidth.Text + "," + txtPrintRepeat.Text + "," + txtFilmGauge.Text + "," + txtTreatment.Text + "," + ModulesClass.GetBooleanResult(cbxSeal.SelectedIndex) + ModulesClass.GetBooleanResult(cbxCoating.SelectedIndex) + txtInkAdhesion.Text + "," + ModulesClass.GetBooleanResult(cbxColorInSpec.SelectedIndex) + ModulesClass.GetBooleanResult(cbxPrintMiss.SelectedIndex) + ModulesClass.GetBooleanResult(cbxDirtyPrint.SelectedIndex) + ModulesClass.GetBooleanResult(cbxPinholes.SelectedIndex) + ModulesClass.GetBooleanResult(cbxGhosting.SelectedIndex) + ModulesClass.GetBooleanResult(cbxRegister.SelectedIndex) + ModulesClass.GetBooleanResult(cbxStreaks.SelectedIndex) + ModulesClass.GetBooleanResult(cbxUpc.SelectedIndex) + ModulesClass.GetBooleanResult(cbxGc.SelectedIndex) + txtWhiteInkOpacity.Text + ",";
			if (rtbComments.Text.Length == 0)
			{
				sqlScript += "null";
			}
			else
			{
				sqlScript += "'" + rtbComments.Text.Replace("'", "''") + "'";
			}
			
			SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
			SqlCommand command = new SqlCommand(sqlScript, connection);
			connection.Open();
			command.ExecuteNonQuery();
			if (!accept)
			{
				command = new SqlCommand("update [Roll Table] set [Location ID]=9 where [Roll ID]=" + rollNumber, connection);
				command.ExecuteNonQuery();
			}

			connection.Close();
			this.FormClosing -= PrintingQCCheckFormFormClosing;
			this.Close();
		}
		
		private void BtnAcceptRollClick(object sender, EventArgs e)
		{
			WritePrintQCRecord(true);
		}
		
		private void BtnRejectRollClick(object sender, EventArgs e)
		{
			WritePrintQCRecord(false);
		}
		
		private void TxtNumbersorDecimalEnter(object sender, EventArgs e)
		{
			FormatClass.TextBoxEnterNumbersOrDecimalOnly(sender);
		}
		
		private void TxtNumbersorDecimalLeave(object sender, EventArgs e)
		{
			FormatClass.TextBoxLeaveNumbersOrDecimalOnly(sender, '4');
		}
		
		private void TxtNumbersKeyDown(object sender, KeyEventArgs e)
		{
			FormatClass.TextBoxKeyDownNumbersOnly(sender, e);
		}
		
		private void TxtNumbersorDecimalKeyPress(object sender, KeyPressEventArgs e)
		{
			FormatClass.TextBoxKeyPressNumberOrDecimalOnly(sender, e, 4);
		}	
		
		private void TxtFilmGaugeLeave(object sender, EventArgs e)
		{
			FormatClass.TextBoxLeaveNumbersOrDecimalOnly(sender, '3');
		}
		
		private void TxtFilmGaugeKeyPress(object sender, KeyPressEventArgs e)
		{
			FormatClass.TextBoxKeyPressNumberOrDecimalOnly(sender, e, 3);
		}
		
		private void TxtNumbersEnter(object sender, EventArgs e)
		{
			FormatClass.TextBoxEnterNumbersOnly(sender);
		}
		
		private void TxtNumbersLeave(object sender, EventArgs e)
		{
			FormatClass.TextBoxLeaveNumbersOnly(sender);
		}
		
		private void TxtNumbersKeyPress(object sender, KeyPressEventArgs e)
		{
			FormatClass.TextBoxKeyPressNumbersOnly(e);
		}

        private void IsOpacityValid(object sender, System.ComponentModel.CancelEventArgs e)
        {
             int minOpacity = 60;
             int maxOpacity = 100;


            int intValue = int.Parse(txtWhiteInkOpacity.Text);
            if (intValue >= minOpacity && intValue <= maxOpacity)
            {

                OpacityAlert.Visible = false;

            }
            else {

                OpacityAlert.Visible = true;
            }
        }

        private void txtWhiteInkOpacity_TextChanged(object sender, EventArgs e)
        {

        }

        private void PrintOpacity(object sender, MouseEventArgs e)
        {
            PrintQCOpacity printQCOpacity = new PrintQCOpacity();

            printQCOpacity.JobNumber = this.jobJacketNumber;
            printQCOpacity.RollNumber = rollNumber;
            printQCOpacity.Opacity = txtWhiteInkOpacity.Text;
            printQCOpacity.CurrentDateTime = DateTime.Now.ToString();
            //printQCOpacity.OperatorName = StartupForm.UserName;

            PrintClass.PrintOpacityQCCheck(printQCOpacity);


        }
    }
}
