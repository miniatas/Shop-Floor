/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 12/1/2010
 * Time: 11:43 AM
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
	/// TextBox form used for reading barcodes and keyed entries.
	/// </summary>
	public partial class GetInputForm : Form
	{
		private string firstChar;
		private int minimumValue;
		private int maximumValue;
        private bool scannerInputOnly;
        private bool manualOverride = false;
        private string authorizedBy = string.Empty;
		
		public GetInputForm(string parDescription, string parPrefix, int parMinValue, int parMaxValue, bool parScannerInputOnly)
		{
			InitializeComponent();
			
			lblDescription.Text = parDescription;
			firstChar = parPrefix;
			minimumValue = parMinValue;
			maximumValue = parMaxValue;
            scannerInputOnly = parScannerInputOnly;
            if (! scannerInputOnly || ! MainForm.WorkingScanner)
            {
                Hide_cmdManual();
            }
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo")]
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
		
		public string NewTitle
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
		
		public int MaxValue
		{
			set
			{
				maximumValue = value;
			}
		}

        public bool ManualOverride
        {
            get
            {
                return manualOverride;
            }
        }

        public string AuthorizedBy
        {
            get
            {
                return authorizedBy;
            }
        }

        public void TextLowerCase()
        {
            txtDataEntry.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
        }

		private void CmdAbortClick(object sender, EventArgs e)
		{
			txtDataEntry.Text = string.Empty;
			this.Close();
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		private void CmdOKClick(object sender, EventArgs e)
		{
			if (txtDataEntry.Text.Length > 0 && firstChar == "#" && minimumValue > 0 && int.Parse(txtDataEntry.Text, NumberStyles.Number) < minimumValue)
			{
				MessageBox.Show("Error - the smallest value allowed is " + minimumValue.ToString("N0"), "Too small a value");
				txtDataEntry.SelectAll();
				txtDataEntry.Focus();
			}
			else
			{
				this.Close();
			}
		}
		
		private void CmdClearClick(object sender, EventArgs e)
		{
			txtDataEntry.Text = string.Empty;
            txtDataEntry.Enabled = true;
			txtDataEntry.Focus();
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void TxtDataEntryKeyPress(object sender, KeyPressEventArgs e)
		{
			if (string.IsNullOrEmpty(txtDataEntry.Text))
			{
                if (e.KeyChar == '*')
				{
					e.Handled = true;
				}
				else if (e.KeyChar.ToString().ToUpper() == firstChar || (firstChar == "R" && e.KeyChar.ToString().ToUpper() == "C")|| (firstChar == "L" && e.KeyChar.ToString().ToUpper() == "P") || (firstChar == "%" && char.IsLetter(e.KeyChar)) || (firstChar == "#" && char.IsDigit(e.KeyChar)) || e.KeyChar == '\b' || firstChar == "*")
				{
					e.Handled = false;
				}
				else
				{
					if (firstChar == "%")
					{
						MessageBox.Show("Error - the first character of the barcode must be a letter", "Invalid Input");
					}
					else if (firstChar == "#")
					{
						MessageBox.Show("Error - only numbers allowed", "Date Entry Error");
					}
					else if (firstChar == "L")
					{
						MessageBox.Show("Error - the first character of the barcode must be an 'L' or 'P'", "Invalid Input");
					}
					else if (firstChar == "R")
					{
						MessageBox.Show("Error - the first character of the barcode must be an 'R' or 'C'", "Invalid Input");
					}
					else
					{
						MessageBox.Show("Error - the first character of the barcode must be '" + firstChar + "'", "Invalid Input");
					}

                    e.Handled = true;
				}

                if (scannerInputOnly && MainForm.WorkingScanner)
                {
                    inputTimeoutTimer.Start();
                }
            }
			else 
			{
				// Input must be a number, "*", return, or backspace unless the firstChar is the wildcard("#")
				if (e.KeyChar == '*' || e.KeyChar == (char)Keys.Return)
				{
					if (txtDataEntry.Text.Length > 0 && firstChar == "#" && minimumValue > 0 && int.Parse(txtDataEntry.Text, NumberStyles.Number) < minimumValue)
					{
						MessageBox.Show("Error - the smallest value allowed is " + minimumValue.ToString("N0"), "Too small a value");
						txtDataEntry.SelectAll();
						txtDataEntry.Focus();
						e.Handled = true;
					}
					else
					{
						e.Handled = true;
						this.Close();
					}
				}
				else if (firstChar != "*" && !char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
				{
					MessageBox.Show("Error - only numbers allowed", "Date Entry Error");
					e.Handled = true;
				}
				else
				{
					e.Handled = false;
				}
			}
		}
		
		private void TxtDataEntryKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter && txtDataEntry.ToString().Length == 0)
			{
				SelectNextControl(txtDataEntry, true, true, false, true);
			}
			else if (e.KeyCode == Keys.Space)
			{
				e.SuppressKeyPress = true;	
			}
		}
		
		private void TxtDataEntryEnter(object sender, EventArgs e)
		{
			txtDataEntry.SelectAll();
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		private void TxtDataEntryTextChanged(object sender, EventArgs e)
		{
			if (txtDataEntry.Text.Length > 0 && firstChar == "#" && maximumValue > 0 && int.Parse(txtDataEntry.Text, NumberStyles.Number) > maximumValue)
			{
				MessageBox.Show("Error - the largest value allowed is " + maximumValue.ToString("N0"), "Too large a value");
				txtDataEntry.Text = txtDataEntry.Text.Substring(0, txtDataEntry.Text.Length - 1);
				txtDataEntry.SelectAll();
				txtDataEntry.Focus();
			}
			
			if (txtDataEntry.Text.Length == 1)
			{
				cmdClear.Enabled = true;
				if (firstChar == "#" || firstChar == "*")
				{
					cmdOK.Enabled = true;
				}
			}
			else if (txtDataEntry.Text.Length > 1)
			{
				cmdOK.Enabled = true;
			}
			else
			{
				cmdOK.Enabled = false;
				cmdClear.Enabled = false;
			}
		}

        private void inputTimeoutTimer_Tick(object sender, EventArgs e)
        {
            txtDataEntry.Enabled = false;
            inputTimeoutTimer.Stop();
        }

        private void Hide_cmdManual()
        {
            cmdManual.Visible = false;
            cmdOK.Left = 36;
            cmdClear.Left = 101;
            cmdAbort.Left = 166;
        }
        private void cmdManual_Click(object sender, EventArgs e)
        {
            DialogResult answer;

            answer = MessageBox.Show("You are not allowed to manually input here without a supervisor override authorization.  Do you wish to get one?", "Override? (Requires Supervisor Authorization)", MessageBoxButtons.YesNo);

            if (answer == DialogResult.Yes)
            {
                string authorizedBy = string.Empty;
                bool authorized = ModulesClass.Authorized(false, ref authorizedBy);
                if (authorized)
                {
                    manualOverride = true;
                 //   ModulesClass.SendEmail(2, "Manual Input Override", "A manual input override on a required scanner entry field was authorized on machine " + MainForm.MachineNumber + ".  The override was authorized by " + authorizeOverrideForm.UserName + ".");
                    scannerInputOnly = false;
                    Hide_cmdManual();
                    txtDataEntry.Focus();
                }
            }
        }
    }
}
