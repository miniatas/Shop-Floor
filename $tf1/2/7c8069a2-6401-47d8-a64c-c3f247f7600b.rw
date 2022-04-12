/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 8/12/2011
 * Time: 3:15 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace ShopFloor
{
	using System;
	using System.Globalization;
	using System.Windows.Forms;

	/// <summary>
	/// Form that accepts decimals.
	/// </summary>
	public partial class GetDecimalInputForm : Form
	{
		private int numberDecimalPlaces;
		
		public GetDecimalInputForm(int numDecPlaces)
		{
			InitializeComponent();
			numberDecimalPlaces = numDecPlaces;
			txtDataEntry.Text = "0.";
			for (int i = 1; i <= numberDecimalPlaces; i++)
			{
				txtDataEntry.Text += "0";
			}
		}
		
		public string Description
		{
			get
			{
				return lblDescription.Text;	
			}
			
			set
			{
				lblDescription.Text = value;
			}
		}
		
		public string UserInput
		{ 
			get  
			{
				return txtDataEntry.Text;
			}
			
			set  
			{
				txtDataEntry.Text = value;
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void TxtDataEntryEnter(object sender, EventArgs e)
		{
			if (decimal.Parse(txtDataEntry.Text, NumberStyles.Number) == 0)
			{
				txtDataEntry.Text = string.Empty;
			}
			
			txtDataEntry.SelectAll();
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void TxtDataEntryLeave(object sender, EventArgs e)
		{
			decimal result;
			if (decimal.TryParse(txtDataEntry.Text, out result) && result > 0)
			{
				txtDataEntry.Text = result.ToString("N" + numberDecimalPlaces.ToString());
				cmdClear.Enabled = true;
				cmdOK.Enabled = true;
				cmdOK.Focus();
			}
			else
			{
				txtDataEntry.Text = "0.";
				for (int i = 1; i <= numberDecimalPlaces; i++)
				{
					txtDataEntry.Text += "0";
				}
				
				cmdClear.Enabled = false;
				cmdOK.Enabled = false;
			}
		}
		
		private void TxtDataEntryKeyDown(object sender, KeyEventArgs e)
		{
			FormatClass.TextBoxKeyDownNumbersOnly(sender, e);
		}
		
		private void TxtDataEntryKeyPress(object sender, KeyPressEventArgs e)
		{
			decimal result;
			if (e.KeyChar == (char)Keys.Return && decimal.TryParse(txtDataEntry.Text, out result) && result > 0)
			{
				this.Close();
			}
			else
			{
				FormatClass.TextBoxKeyPressNumberOrDecimalOnly(sender, e, numberDecimalPlaces);
			}
		}
		
		private void CmdOKClick(object sender, EventArgs e)
		{
			this.Close();
		}
		
		private void CmdClearClick(object sender, EventArgs e)
		{
			txtDataEntry.Text = "0.";
			for (int i = 1; i <= numberDecimalPlaces; i++)
			{
				txtDataEntry.Text += "0";
			}

			cmdOK.Enabled = false;
			cmdClear.Enabled = false;
			txtDataEntry.Focus();
		}
		
		private void CmdAbortClick(object sender, EventArgs e)
		{
			txtDataEntry.Text = string.Empty;
			this.Close();
		}
		
		private void TxtDataEntryKeyUp(object sender, KeyEventArgs e)
		{
			decimal result;
			if (decimal.TryParse(txtDataEntry.Text.Replace(",", ""), out result) && result > 0)
			{
				cmdOK.Enabled = true;
			}
			else
			{
				cmdOK.Enabled = false;
			}
		}
		
		private void GetDecimalInputFormFormClosed(object sender, FormClosedEventArgs e)
		{
			txtDataEntry.Focus();
		}
		
		void GetDecimalInputFormShown(object sender, EventArgs e)
		{
			decimal result;
			if (decimal.TryParse(txtDataEntry.Text.Replace(",", ""), out result) && result > 0)
			{
				cmdOK.Enabled = true;
			}
		}
	}
}
