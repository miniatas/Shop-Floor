/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 9/23/2011
 * Time: 1:16 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace ShopFloor
{
	using System;
	using System.Data;
	using System.Data.SqlClient;
	using System.Drawing;
	using System.Globalization;	
	using System.Windows.Forms;

	/// <summary>
	/// Receive in and print barcode labels for adhesive totes.
	/// </summary>
	public partial class ReceiveAdhesivesForm : Form
	{
		private SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
		private SqlDataReader reader;
		private SqlCommand command;
		
		public ReceiveAdhesivesForm()
		{
			InitializeComponent();
			
			command = new SqlCommand("select [Part No] from [Adhesive Table] where [Active]=1 order by [Part No]", connection);
			connection.Open();
			reader = command.ExecuteReader();
			while (reader.Read())
			{
				cbxPartNumber.Items.Add(reader[0].ToString());
			}
			
			reader.Close();
			connection.Close();
			cbxPartNumber.SelectedIndex = 0;
		}
		
		private void TextBoxEnterNumbersOnly(object sender, EventArgs e)
		{
			FormatClass.TextBoxEnterNumbersOnly(sender);
		}
		
		private void TextBoxKeyDownNumbersOnly(object sender, KeyEventArgs e)
		{
			FormatClass.TextBoxKeyDownNumbersOnly(sender, e);
		}
		
		private void TextBoxKeyPressNumbersOnly(object sender, KeyPressEventArgs e)
		{
			FormatClass.TextBoxKeyPressNumbersOnly(e);
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		private void TxtNumberOfTotesLeave(object sender, EventArgs e)
		{
			int result;
			
			if (int.TryParse(txtNumberOfTotes.Text, out result) && result > 12)
			{
				MessageBox.Show("Error - the max number of totes that can be entered is 12", "Too Many Totes");
				txtNumberOfTotes.Text = "3";
			}
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void TxtPurchaseOrderNumberKeyUp(object sender, KeyEventArgs e)
		{
			int result;
			if (int.TryParse(txtPurchaseOrderNumber.Text, out result) && result > 0 && int.Parse(txtNumberOfTotes.Text, NumberStyles.Number) > 0 && decimal.Parse(txtPoundsPerTote.Text, NumberStyles.Number) > 0 && !string.IsNullOrEmpty(txtBatchId.Text))
			{
				cmdSave.Enabled = true;
			}
			else
			{
				cmdSave.Enabled = false;
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void TxtNumberOfTotesKeyUp(object sender, KeyEventArgs e)
		{
			int result;

			if (int.TryParse(txtNumberOfTotes.Text, out result) && result > 0 && int.TryParse(txtPurchaseOrderNumber.Text, out result) && result > 0 && decimal.Parse(txtPoundsPerTote.Text, NumberStyles.Number) > 0 && !string.IsNullOrEmpty(txtBatchId.Text))
			{
				cmdSave.Enabled = true;
			}
			else
			{
				cmdSave.Enabled = false;
			}
		}
		
		private void TxtPoundsPerToteEnter(object sender, EventArgs e)
		{
			FormatClass.TextBoxEnterNumbersOrDecimalOnly(sender);
		}
		
		private void TxtPoundsPerToteLeave(object sender, EventArgs e)
		{
			FormatClass.TextBoxLeaveNumbersOrDecimalOnly(sender, '2');
		}
		
		private void TxtPoundsPerToteKeyPress(object sender, KeyPressEventArgs e)
		{
			FormatClass.TextBoxKeyPressNumberOrDecimalOnly(sender, e, 2);
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void TxtPoundsPerToteKeyUp(object sender, KeyEventArgs e)
		{
			decimal result1;
			int result2;
			
			if (decimal.TryParse(txtPoundsPerTote.Text, out result1) && result1 > 0 && int.TryParse(txtPurchaseOrderNumber.Text, out result2) && result2 > 0 && int.TryParse(txtNumberOfTotes.Text, out result2) && result2 > 0 && !string.IsNullOrEmpty(txtBatchId.Text))
			{
				cmdSave.Enabled = true;
			}
			else
			{
				cmdSave.Enabled = false;
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void TxtBatchIdKeyUp(object sender, KeyEventArgs e)
		{
			int result1;
			decimal result2;
				
			if (int.TryParse(txtPurchaseOrderNumber.Text, out result1) && result1 > 0 && int.TryParse(txtNumberOfTotes.Text, out result1) && result1 > 0 && decimal.TryParse(txtPoundsPerTote.Text, out result2) && result2 > 0 && !string.IsNullOrEmpty(txtBatchId.Text))
			{
				cmdSave.Enabled = true;
			}
			else
			{
				cmdSave.Enabled = false;
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void CmdSaveClick(object sender, EventArgs e)
		{
			int currentToteNumber;
			connection.Open();
			for (int i = 1; i <= int.Parse(txtNumberOfTotes.Text, NumberStyles.Number); i++)
			{
				command = new SqlCommand("execute [Create Tote Stored Procedure] '" + cbxPartNumber.Text.Replace("'", "''") + "'," + txtPurchaseOrderNumber.Text + ",'" + txtBatchId.Text.Replace("'", "''") + "','" + StartupForm.UserName + "','" + DateTime.Now + "'," + decimal.Parse(txtPoundsPerTote.Text, NumberStyles.Number).ToString(), connection);
				currentToteNumber = (int)command.ExecuteScalar();
				PrintClass.Label("T" + currentToteNumber.ToString());
			}
			
			connection.Close();
			txtBatchId.Text = string.Empty;
			cmdSave.Enabled = false;
			txtPurchaseOrderNumber.Focus();
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		private void ReceiveAdhesivesFormFormClosing(object sender, FormClosingEventArgs e)
		{
			if (cmdSave.Enabled)
			{
				DialogResult answer = MessageBox.Show("You have not saved this purchase.  Exit anyway?", "Warning!", MessageBoxButtons.YesNo);
				if (answer == DialogResult.No)
				{
					e.Cancel = true;
				}
			}
		}
	}
}
