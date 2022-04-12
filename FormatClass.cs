/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 7/20/2011
 * Time: 5:17 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace ShopFloor
{
	using System;
	using System.Globalization;
	using System.Windows.Forms;
	
	/// <summary>
	/// Common Windows Form Formatting.
	/// </summary>
	public static class FormatClass
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public static void TextBoxEnterNumbersOnly(object sender)
		{
			int result;
			
			if (int.TryParse(((TextBox)sender).Text, out result) && result == 0)
			{
				((TextBox)sender).Text = string.Empty;
			}
			
			((TextBox)sender).SelectAll();
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public static void TextBoxEnterNumbersOrDecimalOnly(object sender)
		{
			if (decimal.Parse(((TextBox)sender).Text, NumberStyles.Number) == 0)
			{
				((TextBox)sender).Text = string.Empty;
			}
			
			((TextBox)sender).SelectAll();
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public static void TextBoxLeaveNumbersOnly(object sender)
		{
			int result;
			if (int.TryParse(((TextBox)sender).Text.Replace(",", ""), out result) && result > 0)
			{
				((TextBox)sender).Text = result.ToString("N0");
			}
			else
			{
				((TextBox)sender).Text = "0";
			}
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public static void TextBoxLeaveNumbersOrDecimalOnly(object sender, char numberDecimals)
		{
			decimal result;
			if (decimal.TryParse(((TextBox)sender).Text.Replace(",", ""), out result) && result > 0)
			{
				((TextBox)sender).Text = result.ToString("N" + numberDecimals);
			}
			else
			{
				((TextBox)sender).Text = 0.ToString("N" + numberDecimals);
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		public static void TextBoxKeyPressNumbersOnly(KeyPressEventArgs e)
		{
			// Accept only numbers and back space.
			if (e.KeyChar == (char)Keys.Return)
			{
			 	SendKeys.Send("{TAB}");
			}
			else if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
			{
				MessageBox.Show("Error - only numbers allowed", "Date Entry Error");
				e.Handled = true;
			}
			else
			{
				e.Handled = false;
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		public static void TextBoxKeyPressNumberOrDecimalOnly(object sender, KeyPressEventArgs e, int numberDecimals)
		{
			TextBox txt = (TextBox)sender;
			// Accept only numbers and back space and decimal - if a decimal isn't already present
			if (e.KeyChar == (char)Keys.Return)
			{
			 	SendKeys.Send("{TAB}");
			}
			else if (e.KeyChar == '.' && txt.Text.Length == txt.SelectionLength)
			{
				((TextBox)sender).Text = ".";
				((TextBox)sender).SelectionStart = 1;
				e.Handled = true;
			}
			else if (e.KeyChar == '.' && txt.Text.Contains("."))
			{
			    MessageBox.Show("Error - only one decimal place allowed", "Date Entry Error");
				e.Handled = true;
			}
			else if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b' && e.KeyChar != '.')
			{
				MessageBox.Show("Error - only numbers allowed", "Date Entry Error");
				e.Handled = true;
			}
			else if (e.KeyChar != '\b' && txt.Text.Contains(".") && txt.Text.Substring(txt.Text.IndexOf('.')).Length > numberDecimals)
			{
				e.Handled = true;
			}
			else
			{
				e.Handled = false;
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public static void TextBoxKeyDownNumbersOnly(object sender, KeyEventArgs e)
  		// A space cannot be contained in a numeric field			
		{
			TextBox txt = (TextBox)sender;
			if (e.KeyCode == Keys.Space)
			{
				e.SuppressKeyPress = true;
			}
			else if (txt.SelectionLength == txt.Text.Length)
			{
				((TextBox)sender).Text = string.Empty;
			}
		}

		public static void ComboBoxKeyPress(KeyPressEventArgs e)
			// If a combo box selection is selected via the enter or return keys
		{
			if (e.KeyChar == (char)Keys.Return || e.KeyChar == (char)Keys.Enter)
			{
			 	SendKeys.Send("{TAB}");
			}
		}
	}
}
