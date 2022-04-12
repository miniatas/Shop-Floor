/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 4/7/2011
 * Time: 11:48 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace ShopFloor
{
	using System;
	using System.Drawing;
	using System.Globalization;
	using System.Windows.Forms;
	
	/// <summary>
	/// Description of Form1.
	/// </summary>
	public partial class UnitInformationForm : Form
	{
		private int maximumFeet;
		private bool warning = true;
		
		public UnitInformationForm(string formTitle, string jobDescription, int maxLF, bool getPounds)
		{
			InitializeComponent();
			
			this.Text = formTitle;
			lblJobDescription.Text = jobDescription;
			maximumFeet = maxLF;
			if (!getPounds)
			{
				lblPounds.Visible = false;
				txtPounds.Visible = false;
				lblUnit.Left = 177;
				txtUnits.Left = 177;
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		public int Units
		{ 
			get  
			{
				return int.Parse(txtUnits.Text, NumberStyles.Number);
			}
			
			set
			{
				txtUnits.Text = value.ToString("N0");
			}
		} 
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		public decimal Pounds
		{ 
			get  
			{
				return decimal.Parse(txtPounds.Text, NumberStyles.Number);
			}
			
			set
			{
				txtPounds.Text = value.ToString("N2");
			}
		} 
		
		public string Notes
		{
			get 
			{
				return txtNotes.Text;
			}
			
			set
			{
				txtNotes.Text = value;
			}
		}
		
		public string UnitName
		{
			set
			{
				lblUnit.Text = value;
			}
		}
		
		private void TxtFeetEnter(object sender, EventArgs e)
		{
			FormatClass.TextBoxEnterNumbersOnly(sender);
		}
		
		private void TxtFeetLeave(object sender, EventArgs e)
		{
			FormatClass.TextBoxLeaveNumbersOnly(sender);
		}
		
		private void TextBoxKeyDownNumbersOnly(object sender, KeyEventArgs e)
		{	
			FormatClass.TextBoxKeyDownNumbersOnly(sender, e);
		}
		
		private void TxtFeetKeyPress(object sender, KeyPressEventArgs e)
		{
			FormatClass.TextBoxKeyPressNumbersOnly(e);
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void TxtFeetKeyUp(object sender, KeyEventArgs e)
		{
			int result;
			if (int.TryParse(txtUnits.Text.Replace(",", ""), out result) && result > 0 && (!txtPounds.Visible || decimal.Parse(txtPounds.Text.Replace(",",""), NumberStyles.Number) > 0))
			{
				cmdOK.Enabled = true;
			}
			else
			{
				cmdOK.Enabled = false;
			}
		}
		
		private void TxtPoundsEnter(object sender, EventArgs e)
		{
			FormatClass.TextBoxEnterNumbersOrDecimalOnly(sender);
		}
		
		private void TxtPoundsLeave(object sender, EventArgs e)
		{
			FormatClass.TextBoxLeaveNumbersOrDecimalOnly(sender, '2');
		}
		
		private void TxtPoundsKeyPress(object sender, KeyPressEventArgs e)
		{
			FormatClass.TextBoxKeyPressNumberOrDecimalOnly(sender, e, 2);
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void TxtPoundsKeyUp(object sender, KeyEventArgs e)
		{
			decimal decResult;
			if (decimal.TryParse(txtPounds.Text, out decResult) && decResult > 0 && int.Parse(txtUnits.Text, NumberStyles.Number) > 0)
			{
				cmdOK.Enabled = true;
			}
			else
			{
				cmdOK.Enabled = false;
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		private void UnitInformationFormFormClosing(object sender, FormClosingEventArgs e)
		{
			if (warning)
			{
				DialogResult answer = MessageBox.Show("Are you sure you want to exit without saving", "Exit Without Saving", MessageBoxButtons.YesNo);
				if (answer == DialogResult.Yes)
				{
					ClearData();
				}
				else
				{
					e.Cancel = true;
				}
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void CmdOKClick(object sender, EventArgs e)
		{
			if (maximumFeet > 0 && int.Parse(txtUnits.Text, NumberStyles.Number) > maximumFeet)
			{
				MessageBox.Show("Error - the maximum feet allowed is " + maximumFeet.ToString("N0") + ".  Are you missing an input roll?", "Invalid Roll Feet");
			}
			else
			{
				warning = false;
				this.Close();
			}
		}
		
		private void CmdClearClick(object sender, EventArgs e)
		{
			ClearData();
			cmdOK.Enabled = false;
		}
		
		private void CmdAbortClick(object sender, EventArgs e)
		{
			ClearData();
			warning = false;
			this.Close();
		}
		
		private void ClearData()
		{
			txtUnits.Text = "0";
			txtPounds.Text = "0.00";
			txtNotes.Text = string.Empty;
		}
	}
}
