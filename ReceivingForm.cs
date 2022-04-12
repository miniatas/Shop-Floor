/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 12/3/2010
 * Time: 4:23 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace ShopFloor
{
	using System;
	using System.Data;
	using System.Data.SqlClient;
	using System.Globalization;
	using System.Windows.Forms;
    
	/// <summary>
	/// Description of Form1.
	/// </summary>
	public partial class ReceivingForm : Form
	{
		private string purchaseOrderNumber;
		private string purchaseOrderLineItemNumber;
		private string masterItemNumber;
		private decimal filmWidth;
		private decimal filmYield;
		private SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
		private SqlDataReader reader;
		private SqlCommand command;
		private DataTable newPalletTable;
		private int palletNumber;
		private int rowCount;
		private DateTime palletCreateDate;
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1306:SetLocaleForDataTypes")]
		public ReceivingForm(string purchaseOrderNo, string purchaseOrderLineItemNo, string masterItemNo, decimal width, decimal yield)
		{
			InitializeComponent();
			
			purchaseOrderNumber = purchaseOrderNo;
			purchaseOrderLineItemNumber = purchaseOrderLineItemNo;
			masterItemNumber = masterItemNo;
			filmWidth = width;
			filmYield = yield;
			newPalletTable = new DataTable();
			newPalletTable.Columns.Add("RowNumber", typeof(int));
			newPalletTable.Columns.Add("RollCount", typeof(int));
			newPalletTable.Columns.Add("UnitsPerRoll", typeof(int));
			newPalletTable.Columns.Add("UOM", typeof(string));
			newPalletTable.Columns.Add("PoundsPerRoll", typeof(int));
			newPalletTable.Columns.Add("Notes", typeof(string));
			lblPONumberTitles.Text = "PO NUMBER:\r\nORDER DATE:\r\nVENDOR:\r\nBUYER:";
			lblPartNumberTitles.Text = "PART NUMBER\r\nDESCRIPTION:\r\nREQUIRED DATE:\r\nUNITS ORDERED:\r\nNOTES:";
			
			connection.Open();
			command = new SqlCommand("select description from [UOM Table] where [Pallet Build Order] is not NULL order by [Pallet Build Order]", connection);
			reader = command.ExecuteReader();
			while (reader.Read())
			{
				cboUnitOfMeasure.Items.Add(reader[0].ToString()); 
			}
			
			reader.Close();
			connection.Close();
			cboUnitOfMeasure.SelectedIndex = 0;
		}
		
		public void PurchaseOrderDetails(string details)
		{
			lblPONumberDetails.Text = details; 
		}
		
		public void PartDetails(string details)
		{
			{
				lblPartNumberDetails.Text = details;	
			}
		}
		
		public void SpecificationDetails(string details)
		{
			rtbSpecificationDetails.Text = details;
			cmdShowSpecifications.Visible = true;
		}
		
		private void TxtKeyPress(object sender, KeyPressEventArgs e)
		{
			FormatClass.TextBoxKeyPressNumbersOnly(e);
		}
		
		private void TxtKeyDown(object sender, KeyEventArgs e)
		{
			FormatClass.TextBoxKeyDownNumbersOnly(sender, e);
		}

		private void CboUnitOfMeasureKeyPress(object sender, KeyPressEventArgs e)
		{
			FormatClass.ComboBoxKeyPress(e);
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void TxtLeave(object sender, EventArgs e)
		{
			if (txtRollCount.Text.Length > 0 || txtUnitCount.Text.Length > 0 || txtPoundsPerRoll.Text.Length > 0)
			{
				cmdClear.Enabled = true;
			}
			else
			{
				cmdClear.Enabled = false;
			}
						
			if ((txtRollCount.Text.Length > 0 && int.Parse(txtRollCount.Text, NumberStyles.Number) > 0) && ((txtUnitCount.Text.Length > 0  && int.Parse(txtUnitCount.Text, NumberStyles.Number) > 0) || (txtPoundsPerRoll.Text.Length > 0 && int.Parse(txtPoundsPerRoll.Text, NumberStyles.Number) > 0 && cbxCalculateUnitsorPounds.Checked)) && ((txtPoundsPerRoll.Text.Length > 0  && int.Parse(txtPoundsPerRoll.Text, NumberStyles.Number) > 0) || (txtUnitCount.Text.Length > 0 && int.Parse(txtUnitCount.Text, NumberStyles.Number) > 0 && cbxCalculateUnitsorPounds.Checked)))
			{
				cmdAdd.Enabled = true;
				cmdAdd.Focus();
			}
			else
			{
				cmdAdd.Enabled = false;
			}
		}
		
		private void CmdClearClicbx(object sender, EventArgs e)
		{
			ClearPalletInfoData();
			txtRollCount.Focus();
		}

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        private void CmdAddClick(object sender, EventArgs e)
        {
            DataRow row = newPalletTable.NewRow();
            rowCount += 1;
            row["RowNumber"] = rowCount;
            row["RollCount"] = int.Parse(txtRollCount.Text, NumberStyles.Number);
            if (txtUnitCount.Text.Length > 0)
            {
                row["UnitsPerRoll"] = int.Parse(txtUnitCount.Text, NumberStyles.Number);
            }

            row["UOM"] = cboUnitOfMeasure.Text;
            if (txtPoundsPerRoll.Text.Length > 0)
            {
                row["PoundsPerRoll"] = int.Parse(txtPoundsPerRoll.Text, NumberStyles.Number);
            }

            if (txtNotes.Text.Length > 0)
            {
                row["Notes"] = txtNotes.Text;
            }

            DialogResult result = DialogResult.Yes;
            if (!cbxCalculateUnitsorPounds.Checked)
            {
                command = new SqlCommand("SELECT [dbo].[UOM Conversion] (" + txtUnitCount.Text + ", '" + cboUnitOfMeasure.Text.Replace("'", "''") + "', NULL, " + filmWidth.ToString() + ", NULL, " + filmYield.ToString() + ", 'LBS')", connection);
                connection.Open();
                decimal calcPounds = (decimal)command.ExecuteScalar();
                connection.Close();
                decimal percentPoundsOff = Decimal.Divide(calcPounds, decimal.Parse(txtPoundsPerRoll.Text, NumberStyles.Number));
                if (percentPoundsOff > (decimal)1.05 || percentPoundsOff < (decimal).95)
                {
                    result = MessageBox.Show("The calculated pounds based upon the standard yield of this film is " + calcPounds.ToString("N0") + ".  Do you still wish to record receipt?", "Receive Roll Anyway", MessageBoxButtons.YesNo);
                }
            }

            if (result == DialogResult.Yes)
            {
                newPalletTable.Rows.Add(row);
                if (!cbxCalculateUnitsorPounds.Checked)
                {
                    if (int.Parse(txtRollCount.Text, NumberStyles.Number) == 1)
                    {
                        lblPalletInformation.Text += rowCount.ToString("N0") + ": 1 roll @ " + int.Parse(txtUnitCount.Text, NumberStyles.Number).ToString("N0") + " " + cboUnitOfMeasure.Text + " and " + int.Parse(txtPoundsPerRoll.Text, NumberStyles.Number).ToString("N0") + " pounds\r\n";
                    }
                    else
                    {
                        lblPalletInformation.Text += rowCount.ToString("N0") + ": " + int.Parse(txtRollCount.Text, NumberStyles.Number).ToString("N0") + " rolls @ " + int.Parse(txtUnitCount.Text, NumberStyles.Number).ToString("N0") + " " + cboUnitOfMeasure.Text + " and " + int.Parse(txtPoundsPerRoll.Text, NumberStyles.Number).ToString("N0") + " pounds per roll\r\n";
                    }
                }
                else
                {
                    if (txtUnitCount.Text.Length == 0)
                    {
                        if (txtPoundsPerRoll.Text == "1")
                        {
                            lblPalletInformation.Text += rowCount.ToString() + ": 1 roll @ " + int.Parse(txtPoundsPerRoll.Text, NumberStyles.Number).ToString("N0") + " pounds\r\n";
                        }
                        else
                        {
                            lblPalletInformation.Text += rowCount.ToString() + ": " + int.Parse(txtRollCount.Text, NumberStyles.Number).ToString("N0") + " rolls @ " + int.Parse(txtPoundsPerRoll.Text, NumberStyles.Number).ToString("N0") + " pounds per roll\r\n";
                        }
                    }
                    else
                    {
                        if (txtUnitCount.Text == "1")
                        {
                            lblPalletInformation.Text += rowCount.ToString() + ": 1 roll @ " + int.Parse(txtUnitCount.Text, NumberStyles.Number).ToString("N0") + " " + cboUnitOfMeasure.Text + "\r\n";
                        }
                        else
                        {
                            lblPalletInformation.Text += rowCount.ToString() + ": " + int.Parse(txtRollCount.Text, NumberStyles.Number).ToString("N0") + " rolls @ " + int.Parse(txtUnitCount.Text, NumberStyles.Number).ToString("N0") + " " + cboUnitOfMeasure.Text + " per roll\r\n";
                        }
                    }
                }

                ClearPalletInfoData();
                pnlRemoveRolls.Visible = true;
                if (rowCount == 1)
                {
                    cboRemoveRolls.Items.Add(string.Empty);
                }

                cboRemoveRolls.Items.Add(rowCount.ToString());
                cboUnitOfMeasure.Enabled = false;
                cmdSave.Enabled = true;
                txtRollCount.Focus();
            }
        }
			
		private void ClearPalletInfoData()
		{
			txtUnitCount.Text = string.Empty;
			txtRollCount.Text = string.Empty;
			txtPoundsPerRoll.Text = string.Empty;
			txtNotes.Text = string.Empty;
			cboUnitOfMeasure.Enabled = true;
			cmdAdd.Enabled = false;
			cmdClear.Enabled = false;
		}
		
		private void ClearRollInfo()
		{
			lblPalletInformation.Text = string.Empty;
			cmdRemove.Enabled = false;
			cmdSave.Enabled = false;
			newPalletTable.Clear();
			rowCount = 0;
			cboRemoveRolls.Items.Clear();
			cmdRemove.Enabled = false;
			pnlRemoveRolls.Visible = false;
		}
		
		private void CmdAbortClicbx(object sender, EventArgs e)
		{
			this.Close();
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void CmdRemoveClicbx(object sender, EventArgs e)
		{
			newPalletTable.Rows[int.Parse(cboRemoveRolls.Text, NumberStyles.Number) - 1].Delete();
			lblPalletInformation.Text = string.Empty;
			rowCount = 0;
			foreach (DataRow row in newPalletTable.Rows)
			{
				rowCount += 1;
				row["RowNumber"] = rowCount;
				if (row.IsNull("UnitsPerRoll"))
				{
					if (int.Parse(txtRollCount.Text, NumberStyles.Number) == 1)
					{
						lblPalletInformation.Text += rowCount.ToString() + ": 1 roll @ " + ((int)row["PoundsPerRoll"]).ToString("N0") + " pounds\r\n";
					}
					else
					{
						lblPalletInformation.Text += rowCount.ToString() + ": " + ((int)row["RollCount"]).ToString("N0") + " rolls @ " + ((int)row["PoundsPerRoll"]).ToString("N0") + " pounds per roll\r\n";
					}
				}
				else if (row.IsNull("PoundsPerRoll"))
				{
					if (int.Parse(txtRollCount.Text, NumberStyles.Number) == 1)
					{
						lblPalletInformation.Text += rowCount.ToString() + ": 1 roll @ " + ((int)row["UnitsPerRoll"]).ToString("N0") + " " + cboUnitOfMeasure.Text + "\r\n";
					}
					else
					{
						lblPalletInformation.Text += rowCount.ToString() + ": " + ((int)row["RollCount"]).ToString("N0") + " rolls @ " + ((int)row["UnitsPerRoll"]).ToString("N0") + " " + cboUnitOfMeasure.Text + " per roll\r\n";
					}
				}
				else
				{
					if ((int)row["RollCount"] == 1)
					{
						lblPalletInformation.Text += rowCount.ToString("N0") + ": 1 roll @ " + ((int)row["UnitsPerRoll"]).ToString("N0") + " " + cboUnitOfMeasure.Text + " and " + ((int)row["PoundsPerRoll"]).ToString("N0") + " pounds\r\n";
					}
					else
					{
						lblPalletInformation.Text += rowCount.ToString("N0") + ": " + ((int)row["RollCount"]).ToString("N0") + " rolls @ " + ((int)row["UnitsPerRoll"]).ToString("N0") + " " + cboUnitOfMeasure.Text + " and " + ((int)row["PoundsPerRoll"]).ToString("N0") + " pounds per roll\r\n";
					}
				}
			}
			
			if (newPalletTable.Rows.Count == 0)
			{
				cboRemoveRolls.Items.Clear();
				pnlRemoveRolls.Visible = false;
				cmdSave.Enabled = false;
			}
			else
			{
				cboRemoveRolls.Items.RemoveAt(cboRemoveRolls.Items.Count - 1);
				cboRemoveRolls.Refresh();
				cboRemoveRolls.SelectedIndex = 0;
			}
		}
		
		private void CboRemoveRollsSelectedIndexChanged(object sender, EventArgs e)
		{
			if (cboRemoveRolls.Text.Length == 0)
			{
				cmdRemove.Enabled = false;
			}
			else
			{
				cmdRemove.Enabled = true;
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void CmdSaveClick(object sender, EventArgs e)
		{
			DialogResult answer = MessageBox.Show("Save Pallet?", "Save", MessageBoxButtons.YesNo);
			if (answer == DialogResult.Yes)
			{
				int unitsPerRoll;
				int poundsPerRoll;
				string unitsKnown;
				string poundsKnown;
				int rollNumber;
				string location;
				if (System.Environment.MachineName.ToString().Replace("'", "''") == "OVER-V205563")  //Sergio's PC
				{
					location = "20000";
				}
				else
				{
					location = "0";
				}

				if (txtVendorPalletNumber.Text.Replace(" ", string.Empty).Length == 0)
				{
					command = new SqlCommand("execute [Create Pallet Stored Procedure] '" + StartupForm.UserName + "'," + location + ",null,null", connection);
				}
				else
				{
					command = new SqlCommand("execute [Create Pallet Stored Procedure] '" + StartupForm.UserName + "'," + location + ",null,'" + txtVendorPalletNumber.Text.Replace("'", "''") + "'", connection);
				}
				
				connection.Open();
				reader = command.ExecuteReader();
				reader.Read();
				palletNumber = (int)reader[0];
				palletCreateDate = (DateTime)reader[1];
				reader.Close();
				foreach (DataRow row in newPalletTable.Rows)
				{
					if (row.IsNull("UnitsPerRoll"))
					{
						unitsKnown = "0";
						poundsKnown = "1";
						poundsPerRoll = (int)row["PoundsPerRoll"];
						command = new SqlCommand("select cast(round([dbo].[UOM Conversion] (" + poundsPerRoll.ToString() + ",'LBS',NULL," + filmWidth.ToString() + ",NULL," + filmYield.ToString() + ",'" + row["UOM"] + "'),0) as int)", connection);
						unitsPerRoll = (int)command.ExecuteScalar();
					}
					else if (row.IsNull("PoundsPerRoll"))
					{
						poundsKnown = "0";
						unitsKnown = "1";
						unitsPerRoll = (int)row["UnitsPerRoll"];
						command = new SqlCommand("select cast(round([dbo].[UOM Conversion] (" + unitsPerRoll.ToString() + ",'" + row["UOM"] + "',NULL," + filmWidth.ToString() + ",NULL," + filmYield.ToString() + ",'LBS'),0) as int)", connection);
						poundsPerRoll = (int)command.ExecuteScalar();
					}
					else
					{
						poundsKnown = "1";
						unitsKnown = "1";
						poundsPerRoll = (int)row["PoundsPerRoll"];
						unitsPerRoll = (int)row["UnitsPerRoll"];
					}
					
					for (int i = 1; i <= (int)row["RollCount"]; i++)
					{
						if (row["Notes"] != DBNull.Value)
						{
							command = new SqlCommand("execute [Create Roll PO Stored Procedure] " + palletNumber.ToString() + "," + purchaseOrderNumber + "," + purchaseOrderLineItemNumber + "," + masterItemNumber + ",'" + StartupForm.UserName + "','" + palletCreateDate.ToString() + "','" + row["UOM"].ToString() + "'," + unitsPerRoll.ToString() + "," + poundsPerRoll.ToString() + "," + filmWidth.ToString() + "," + filmYield.ToString() + "," + unitsKnown + "," + poundsKnown + ",'" + row["Notes"].ToString().Replace("'", "''") + "'", connection);
						}
						else
						{
							command = new SqlCommand("execute [Create Roll PO Stored Procedure] " + palletNumber.ToString() + "," + purchaseOrderNumber + "," + purchaseOrderLineItemNumber + "," + masterItemNumber + ",'" + StartupForm.UserName + "','" + palletCreateDate.ToString() + "','" + row["UOM"].ToString() + "'," + unitsPerRoll.ToString() + "," + poundsPerRoll.ToString() + "," + filmWidth.ToString() + "," + filmYield.ToString() + "," + unitsKnown + "," + poundsKnown + ",NULL", connection);
						}
						
						rollNumber = (int)command.ExecuteScalar();
						PrintClass.Label("R" + rollNumber.ToString());
					}
				}
				
				connection.Close();
				PrintClass.Label("P" + palletNumber.ToString());
				ClearRollInfo();
				ClearPalletInfoData();
				txtVendorPalletNumber.Text = string.Empty;
				txtRollCount.Focus();
			}
		}
		
		private void CbxCalcUnitsCheckedChanged(object sender, EventArgs e)
		{
			if (cbxCalculateUnitsorPounds.Checked)
			{
				if (txtUnitCount.Text.Length > 0 && txtUnitCount.Text != "0")
				{
					txtPoundsPerRoll.Text = string.Empty;
					lblPoundsPerRoll.Visible = false;
					txtPoundsPerRoll.Visible = false;
				}
				else
				{
					txtUnitCount.Text = string.Empty;
					lblUnitCount.Visible = false;
					txtUnitCount.Visible = false;
				}
				
				TxtLeave(sender, e);
			}
			else
			{
				cmdAdd.Enabled = false;
				lblPoundsPerRoll.Visible = true;
				txtPoundsPerRoll.Visible = true;
				lblUnitCount.Visible = true;
				txtUnitCount.Visible = true;
				if (string.IsNullOrEmpty(txtPoundsPerRoll.Text))
				{
					txtPoundsPerRoll.Focus();
				}
				else
				{
					txtUnitCount.Focus();
				}
			}
		}
		
		private void CmdShowSpecificationsClick(object sender, EventArgs e)
		{
			if (cmdShowSpecifications.Text == "Show Specs")
			{
				rtbSpecificationDetails.Visible = true;
				cmdShowSpecifications.Text = "Hide Specs";
			}
			else
			{
				rtbSpecificationDetails.Visible = false;
				cmdShowSpecifications.Text = "Show Specs";
			}
		}
	}
}
