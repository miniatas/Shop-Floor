/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 9/29/2011
 * Time: 2:42 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace ShopFloor
{
	using System;
	using System.Data.SqlClient;
	using System.Drawing;
	using System.Windows.Forms;

	/// <summary>
	/// The quality control input from for lamination.
	/// </summary>
	public partial class LaminatingQCCheckForm : Form
	{
		private string rollNumber;

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		public LaminatingQCCheckForm(string rollId, string jobInformation, decimal glueWidth, decimal printRepeat, decimal gauge)
		{
			InitializeComponent();

			rollNumber = rollId;
			rtbJobInformation.Text = jobInformation;
			txtGlueWidth.Text = glueWidth.ToString("N4");
			txtPrintRepeat.Text = printRepeat.ToString("N4");
			txtFilmGauge.Text = gauge.ToString("N3");
			if (printRepeat == 0)
			{
				txtPrintRepeat.Enabled = false;
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		private void LaminatingQCCheckFormFormClosing(object sender, FormClosingEventArgs e)
		{
			DialogResult answer = MessageBox.Show("You will lose all information entered.  Exit anyway?", "Warning!", MessageBoxButtons.YesNo);
			if (answer == DialogResult.No)
			{
				e.Cancel = true;
			}
		}
		
		private void TxtNumbersorDecimalEnter(object sender, EventArgs e)
		{
			FormatClass.TextBoxEnterNumbersOrDecimalOnly(sender);
		}
		
		private void TxtNumbersorDecimalLeaveTwoDecimalPlaces(object sender, EventArgs e)
		{
			FormatClass.TextBoxLeaveNumbersOrDecimalOnly(sender, '2');
		}
		
		private void TxtNumbersorDecimalLeaveThreeDecimalPlaces(object sender, EventArgs e)
		{
			FormatClass.TextBoxLeaveNumbersOrDecimalOnly(sender, '3');
		}
		
		private void TxtNumbersorDecimalLeaveFourDecimalPlaces(object sender, EventArgs e)
		{
			FormatClass.TextBoxLeaveNumbersOrDecimalOnly(sender, '4');
		}
		
		private void TxtNumbersorDecimalKeyPressTwoDecimalPlaces(object sender, KeyPressEventArgs e)
		{
			FormatClass.TextBoxKeyPressNumberOrDecimalOnly(sender, e, 2);
		}

		private void TxtNumbersorDecimalKeyPressThreeDecimalPlaces(object sender, KeyPressEventArgs e)
		{
			FormatClass.TextBoxKeyPressNumberOrDecimalOnly(sender, e, 3);
		}	
		
		private void TxtNumbersorDecimalKeyPressFourDecimalPlaces(object sender, KeyPressEventArgs e)
		{
			FormatClass.TextBoxKeyPressNumberOrDecimalOnly(sender, e, 4);
		}	
		
		private void TxtNumbersKeyDown(object sender, KeyEventArgs e)
		{
			FormatClass.TextBoxKeyDownNumbersOnly(sender, e);
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
		
		private void BtnAcceptRollClick(object sender, EventArgs e)
		{
			WriteLaminationQCRecord(true);
		}
		
		private void BtnRejectRollClick(object sender, EventArgs e)
		{
			WriteLaminationQCRecord(false);
		}
		
		private void WriteLaminationQCRecord(bool accept)
		{
			string sqlScript = "insert into [Lamination QC Check Table] select " + rollNumber + ",";
			if (accept)
			{
				sqlScript += "1";
			}
			else
			{
				sqlScript += "0";
			}
			
			sqlScript += ",'" + StartupForm.UserName + "',GETDATE()," + txtGlueWidth.Text + "," + txtPrintRepeat.Text + "," + txtFilmGauge.Text + ",";
			if (string.IsNullOrEmpty(cbxCoating.Text))
			{
				sqlScript += "NULL,";
			}
			else
			{
				sqlScript += "'" + cbxCoating.Text + "',";
			}
			
			sqlScript += ModulesClass.GetBooleanResult(cbxAppearance.SelectedIndex) + txtAdhesiveWeightOperatorSide.Text + "," + txtAdhesiveWeightGearSize.Text + "," + txtBond.Text + "," + txtSeal.Text + "," + ModulesClass.GetPassFailResult(cbxStreaks.SelectedIndex) + ModulesClass.GetPassFailResult(cbxSkips.SelectedIndex) + ModulesClass.GetBooleanResult(cbxCurl.SelectedIndex) + ModulesClass.GetBooleanResult(cbxCof.SelectedIndex);
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
			this.FormClosing -= LaminatingQCCheckFormFormClosing;
			this.Close();
		}
	}
}
