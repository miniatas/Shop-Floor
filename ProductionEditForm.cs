/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 9/17/2013
 * Time: 3:30 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace ShopFloor
{
	public partial class productionEditForm : Form
	{
		private SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
		private SqlDataReader reader;
		private SqlCommand command;
		private DataTable productionDetailTable;
		private BindingSource bs;
		private string preEditValue;
		private int nextDowntimeRecordID;
		
		public productionEditForm()
		{
			InitializeComponent();
			productionDetailTable = new DataTable();
			productionDetailTable.Columns.Add("Prod ID", typeof(int));
			productionDetailTable.Columns.Add("Operator", typeof(string));
			productionDetailTable.Columns.Add("Operation ID", typeof(int));
			productionDetailTable.Columns.Add("Mach No", typeof(int));
			productionDetailTable.Columns.Add("Job No", typeof(int));
			productionDetailTable.Columns.Add("Description", typeof(string));
			productionDetailTable.Columns.Add("Start Time", typeof(DateTime));
			productionDetailTable.Columns.Add("Setup Hrs", typeof(decimal));
			productionDetailTable.Columns.Add("Run Hrs", typeof(decimal));
			productionDetailTable.Columns.Add("DT Hrs", typeof(decimal));
			productionDetailTable.Columns.Add("End Time", typeof(DateTime));
			productionDetailTable.Columns.Add("End Reason", typeof(string));
			productionDetailTable.Columns.Add("Prod LF", typeof(decimal));
			productionDetailTable.Columns.Add("Unwind 1 Consumed LF", typeof(decimal));
			productionDetailTable.Columns.Add("Unwind 2 Consumed LF", typeof(decimal));
			productionDetailTable.Columns.Add("Std Ft per Min", typeof(decimal));
			productionDetailTable.Columns.Add("Act Ft per Min", typeof(decimal));
			productionDetailTable.Columns.Add("Fav(Unf) Ft per Min", typeof(decimal));
			productionDetailTable.Columns.Add("Item Type No", typeof(int));
			productionDetailTable.Columns.Add("Charged DT Hrs", typeof(decimal));
			productionDetailTable.Columns.Add("Std Scrap Ft", typeof(decimal));
			productionDetailTable.Columns.Add("Act Scrap Ft", typeof(decimal));
			productionDetailTable.Columns.Add("Fav(Unf) Ft", typeof(decimal));
			productionDetailTable.Columns.Add("Notes", typeof(string));
			productionDetailTable.Columns[16].Expression = "IIF([Setup Hrs] + [Run Hrs] + [Charged DT Hrs] = 0, 0, [Prod LF] / ([Setup Hrs] + [Run Hrs] + [Charged DT Hrs]) / 60)";
			productionDetailTable.Columns[17].Expression = "IIF([Setup Hrs] + [Run Hrs] + [Charged DT Hrs] = 0, 0, [Prod LF] / ([Setup Hrs] + [Run Hrs] + [Charged DT Hrs]) / 60 - [Std Ft per Min])";
			productionDetailTable.Columns[22].Expression = "[Std Scrap Ft] - [Act Scrap Ft]";
			bs = new BindingSource();
			bs.DataSource = productionDetailTable;
			cboEditIncrement.DataSource = bs;
			cboEditIncrement.Columns[0].Width = 40;
			cboEditIncrement.Columns[0].ReadOnly = true;
			cboEditIncrement.Columns[1].Width = 60;
			cboEditIncrement.Columns[1].DefaultCellStyle.ForeColor = Color.Magenta;
			cboEditIncrement.Columns[1].ReadOnly = true;
			cboEditIncrement.Columns[2].Visible = false;
			cboEditIncrement.Columns[3].Width = 40;
			cboEditIncrement.Columns[3].ReadOnly = true;
			cboEditIncrement.Columns[4].Width = 60;
			cboEditIncrement.Columns[4].ReadOnly = true;
			cboEditIncrement.Columns[5].Width = 260;
			cboEditIncrement.Columns[5].ReadOnly = true;
			cboEditIncrement.Columns[6].Width = 120;
			cboEditIncrement.Columns[6].DefaultCellStyle.ForeColor = Color.Magenta;
			cboEditIncrement.Columns[6].ReadOnly = true;
			cboEditIncrement.Columns[7].Width = 50;
			cboEditIncrement.Columns[7].DefaultCellStyle.ForeColor = Color.Blue;
			cboEditIncrement.Columns[7].DefaultCellStyle.Format = "N2";
			cboEditIncrement.Columns[8].Width = 50;
			cboEditIncrement.Columns[8].DefaultCellStyle.ForeColor = Color.Blue;
			cboEditIncrement.Columns[8].DefaultCellStyle.Format = "N2";
			cboEditIncrement.Columns[9].Width = 50;
			cboEditIncrement.Columns[9].DefaultCellStyle.ForeColor = Color.Magenta;
			cboEditIncrement.Columns[9].DefaultCellStyle.Format = "N2";
			cboEditIncrement.Columns[9].ReadOnly = true;
			cboEditIncrement.Columns[10].Visible = false;
			cboEditIncrement.Columns[11].Width = 90;
			cboEditIncrement.Columns[11].DefaultCellStyle.ForeColor = Color.Magenta;
			cboEditIncrement.Columns[11].ReadOnly = true;
			cboEditIncrement.Columns[12].Width = 80;
			cboEditIncrement.Columns[12].DefaultCellStyle.Format = "N0";
			cboEditIncrement.Columns[12].ReadOnly = true;
			cboEditIncrement.Columns[13].Width = 80;
			cboEditIncrement.Columns[13].DefaultCellStyle.Format = "N0";
			cboEditIncrement.Columns[13].ReadOnly = true;
			cboEditIncrement.Columns[14].Width = 80;
			cboEditIncrement.Columns[14].DefaultCellStyle.Format = "N0";
			cboEditIncrement.Columns[14].ReadOnly = true;
			cboEditIncrement.Columns[15].Width = 60;
			cboEditIncrement.Columns[15].DefaultCellStyle.Format = "N2";
			cboEditIncrement.Columns[15].ReadOnly = true;
			cboEditIncrement.Columns[16].Width = 60;
			cboEditIncrement.Columns[16].DefaultCellStyle.Format = "N2";
			cboEditIncrement.Columns[16].ReadOnly = true;
			cboEditIncrement.Columns[17].Width = 60;
			cboEditIncrement.Columns[17].DefaultCellStyle.Format = "#,##0.00;(#,##0.00)";
			cboEditIncrement.Columns[17].ReadOnly = true;
			cboEditIncrement.Columns[18].Visible = false;
			cboEditIncrement.Columns[19].Visible = false;
			cboEditIncrement.Columns[20].Width = 60;
			cboEditIncrement.Columns[20].DefaultCellStyle.Format =  "#,##0;(#,##0)";
			cboEditIncrement.Columns[20].ReadOnly = true;
			cboEditIncrement.Columns[21].Width = 60;
			cboEditIncrement.Columns[21].DefaultCellStyle.Format = "#,##0;(#,##0)";
			cboEditIncrement.Columns[21].ReadOnly = true;
			cboEditIncrement.Columns[22].Width = 60;
			cboEditIncrement.Columns[22].DefaultCellStyle.Format = "#,##0;(#,##0)";
			cboEditIncrement.Columns[22].ReadOnly = true;
			cboEditIncrement.Columns[23].Width = 160;
			cboEditIncrement.Columns[23].DefaultCellStyle.ForeColor = Color.Blue;
            ((DataGridViewTextBoxColumn)cboEditIncrement.Columns[23]).MaxInputLength = 500;
		
			for (int i = 0; i <= 22; i++)
			{
				cboEditIncrement.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
			}

			cboEditMethod.SelectedIndex = 0;
			cboShift.SelectedIndex = 0;
			dtpProductionDate.Value = DateTime.Today.AddDays(-1);
			dtpProductionDate.ValueChanged += new EventHandler(DtpProductionDateValueChanged);
			cboDowntimeReasons.SelectedIndexChanged += new EventHandler(CboDowntimeReasonsSelectedIndexChanged);
		}
		
		void ProductionEditFormLoad(object sender, EventArgs e)
		{
			RefreshProductionSummary();
		}
		
		private void RefreshProductionSummary()
		{
			cboEditIncrement.RowEnter -= DgvProductionSummaryRowEnter;
			cboMachineFilter.SelectedIndexChanged -= CboMachineFilterSelectedIndexChanged;
			productionDetailTable.DefaultView.RowFilter = string.Empty;
			string currentSelectedMachineNo = cboMachineFilter.Text;
			cboMachineFilter.Items.Clear();
			cboDTRecordMachine.Items.Clear();
			productionDetailTable.Rows.Clear();
			dgvHoursByLine.Rows.Clear();
			command = new SqlCommand("SELECT a.[Production ID], b.[Operator], a.[Operation ID], a.[Machine No], b.[Reference Item No], b.[Job Description], a.[Start Time], b.[Setup Hrs], b.[Run Hrs], b.[DT Hrs], DATEADD(mi, (b.[Setup Hrs] + b.[Run Hrs] + b.[DT Hrs]) * 60, a.[Start Time]),  b.[End Reason], b.[Prod LF], b.[Unwind 1 Input LF], b.[Unwind 2 Input LF], CASE WHEN b.[Std Setup Hrs] + b.[Std Run Hrs] + b.[Std DT Hrs] != 0 THEN b.[Prod LF] / (b.[Std Setup Hrs] + b.[Std Run Hrs] + b.[Std DT Hrs]) / 60 ELSE 0 END, CAST(b.[Item Type No] AS int), b.[Job DT Hrs],  b.[Std Scrap Feet], (b.[Unwind 1 Input LF] + b.[Unwind 2 Input LF]) / CASE WHEN a.[Operation ID] = 2 THEN 2 ELSE 1 END - b.[Prod LF], a.[Notes] FROM [Production Date and Shift by Production ID View] a OUTER APPLY [Get Standard Production Information] (a.[Production ID]) b WHERE a.[Production Date] = '" + dtpProductionDate.Value + "' AND a.[Shift] = '" + cboShift.Text + "' ORDER BY a.[Machine No], a.[Start Time]", connection);
			connection.Open();
			reader = command.ExecuteReader();
			DataRow row;
			if (reader.Read())
			{
				string currentMachineNo = reader[3].ToString();
				decimal currentMachineHours = 0;
		//		DateTime currentEndTime = (DateTime)reader[6];
				cboMachineFilter.Items.Add("All");
				cboMachineFilter.Items.Add(currentMachineNo);
				cboDTRecordMachine.Items.Add(currentMachineNo);
				do
				{
					if (currentMachineNo != reader[3].ToString())
					{
						dgvHoursByLine.Rows.Add(currentMachineNo, currentMachineHours);
						if ((decimal)dgvHoursByLine.Rows[dgvHoursByLine.RowCount - 1].Cells[1].Value > 12)
    					{
							dgvHoursByLine.Rows[dgvHoursByLine.RowCount - 1].Cells[1].Style = new DataGridViewCellStyle{ForeColor = Color.Red};
    					}

						currentMachineNo = reader[3].ToString();
						currentMachineHours = (decimal)reader[7] + (decimal)reader[8] + (decimal)reader[9];
						cboMachineFilter.Items.Add(currentMachineNo);
						cboDTRecordMachine.Items.Add(currentMachineNo);
		//				currentEndTime = (DateTime)reader[6];
					}
					else					
					{
						currentMachineHours += (decimal)reader[7] + (decimal)reader[8] + (decimal)reader[9];
					}
					
					row = productionDetailTable.NewRow();
					for (int i = 0; i <= 20; i++)
					{
						if (i < 16)
						{
							row[i] = reader[i];
						}
						else if (i < 20)
						{
							row[i + 2] = reader[i];
						}
						else
						{
							row[23] = reader[20];
						}
					}
					
					productionDetailTable.Rows.Add(row);
				}
				while (reader.Read());
				
				reader.Close();
				connection.Close();
				dgvHoursByLine.Rows.Add(currentMachineNo, currentMachineHours);
				if ((decimal)dgvHoursByLine.Rows[dgvHoursByLine.RowCount - 1].Cells[1].Value > 12)
				{
					dgvHoursByLine.Rows[dgvHoursByLine.RowCount - 1].Cells[1].Style = new DataGridViewCellStyle{ForeColor = Color.Red};
				}
				
				UpdateDownTimeInfoGrid(0);
				if (!string.IsNullOrEmpty(currentSelectedMachineNo) && currentSelectedMachineNo != "All")
				{
					if (cboMachineFilter.FindStringExact(currentSelectedMachineNo) != -1)
					{
						cboMachineFilter.SelectedIndex = cboMachineFilter.FindStringExact(currentSelectedMachineNo);
						productionDetailTable.DefaultView.RowFilter = "[Mach No] = " + currentSelectedMachineNo;
					}
					else
					{
						cboMachineFilter.SelectedIndex = 0;
					}
				}
				else
				{
					cboMachineFilter.SelectedIndex = 0;
				}
			}
			else
			{
				reader.Close();
				connection.Close();
				dgvDownTimeDetail.Rows.Clear();
			}
			if (cboEditIncrement.Rows.Count > 0)
			{
				FormatDgvProductionSummary(-1);
			}
			
			cboMachineFilter.SelectedIndexChanged += new EventHandler(CboMachineFilterSelectedIndexChanged);
			cboEditIncrement.RowEnter += new DataGridViewCellEventHandler(DgvProductionSummaryRowEnter);
		}
		
		void DgvProductionSummaryDoubleClick(object sender, EventArgs e)
		{
			cboEditIncrement.RowEnter -= DgvProductionSummaryRowEnter;
			switch (cboEditIncrement.CurrentCell.ColumnIndex)
			{
				case 1:
					
					// Operator Change
					if (cboEditIncrement[1, cboEditIncrement.CurrentCell.RowIndex].Value.ToString().Length == 0)
					{
						MessageBox.Show("This downtime record does not need an operator", "Edit not Allowed");
					}
					else
					{
						int operatorID = GetOperatorID(cboEditIncrement[3, cboEditIncrement.CurrentCell.RowIndex].Value.ToString());
						if (operatorID != 0)
						{
							command = new SqlCommand("SELECT [Name] FROM [Operator Table] WHERE [Operator ID] = " + operatorID, connection);
							connection.Open();
							string operatorName = command.ExecuteScalar().ToString();
							if (operatorName != cboEditIncrement[1, cboEditIncrement.CurrentCell.RowIndex].Value.ToString())
							{
								command = new SqlCommand("EXECUTE [Add Production Master Change History Table Record] " + cboEditIncrement[0, cboEditIncrement.CurrentCell.RowIndex].Value.ToString() + ", '" + StartupForm.UserName + "'", connection);
								command.ExecuteNonQuery();
								command = new SqlCommand("UPDATE [Production Master Table] SET [Operator ID] = " + operatorID + " WHERE [Production ID] = " + cboEditIncrement[0, cboEditIncrement.CurrentCell.RowIndex].Value.ToString(), connection);
								command.ExecuteNonQuery();
								cboEditIncrement[1, cboEditIncrement.CurrentCell.RowIndex].Value = operatorName;
								productionDetailTable.AcceptChanges();
								cboEditIncrement[1, cboEditIncrement.CurrentCell.RowIndex].Selected = true;
							}
							
							connection.Close();
						}
					}
					
					break;

				case 6:
					// Start Time Change
					DateTimePickerForm newStartTimeForm = new DateTimePickerForm("Enter new Start Time for Production ID " + cboEditIncrement.CurrentRow.Cells[0].Value.ToString(), (DateTime)cboEditIncrement[6, cboEditIncrement.CurrentCell.RowIndex].Value, DateTime.Today.AddDays(-31));
					newStartTimeForm.ShowDialog();
					if (newStartTimeForm.Selection != (DateTime)cboEditIncrement[6, cboEditIncrement.CurrentCell.RowIndex].Value)
					{
						command = new SqlCommand("EXECUTE [Add Production Master Change History Table Record] " + cboEditIncrement[0, cboEditIncrement.CurrentCell.RowIndex].Value.ToString() + ", '" + StartupForm.UserName + "'", connection);
						connection.Open();
						command.ExecuteNonQuery();
						command = new SqlCommand("UPDATE [Production Master Table] SET [Start Time] = '" + newStartTimeForm.Selection + "' WHERE [Production ID] = " + cboEditIncrement[0, cboEditIncrement.CurrentCell.RowIndex].Value.ToString(), connection);
						command.ExecuteNonQuery();
						connection.Close();
						cboEditIncrement[6, cboEditIncrement.CurrentCell.RowIndex].Value = newStartTimeForm.Selection;
						productionDetailTable.AcceptChanges();
						RefreshProductionSummary();
						FormatDgvProductionSummary(-1);	
						cboEditIncrement[6, cboEditIncrement.CurrentCell.RowIndex].Selected = true;
					}
					
					newStartTimeForm.Dispose();
					break;
					
				case 9:
					// Downtime Hours Change
					{
						pnlTop.Enabled = false;
						cboEditIncrement.Enabled = false;
						cboEditIncrement.DefaultCellStyle.BackColor = Color.LightGray;
						pnlDownTimeInfo.Enabled = true;
						cboDowntimeReasons.SelectedIndexChanged -= CboDowntimeReasonsSelectedIndexChanged;
						cboDowntimeReasons.Items.Clear();
                        command = new SqlCommand("SELECT a.[Description] FROM [Downtime Reason Table] a INNER JOIN [Machine Table] b ON a.[Operation ID] = b.[Operation ID] WHERE a.[Active] = 1 AND b.[Machine No] = " + cboEditIncrement[3, cboEditIncrement.CurrentCell.RowIndex].Value.ToString() + " ORDER BY a.[Display Order]", connection);
                        connection.Open();
						reader = command.ExecuteReader();
						cboDowntimeReasons.Items.Add(string.Empty);
						while (reader.Read())
						{
							cboDowntimeReasons.Items.Add(reader[0].ToString());
						}
				
						reader.Close();
						connection.Close();
						cboDowntimeReasons.SelectedIndexChanged += new EventHandler(CboDowntimeReasonsSelectedIndexChanged);
						dgvDownTimeDetail.DefaultCellStyle.BackColor = Color.White;
						dgvDownTimeDetail.Select();
					}
					
					break;
				case 11:
					// End Reason Change
					OptionsForm getEndReasonForm = new OptionsForm("Choose End Reason", false, true);
					command = new SqlCommand("SELECT [Description] FROM [Save Production Reason Table] ORDER BY CASE WHEN [Description] = '" + cboEditIncrement[11, cboEditIncrement.CurrentCell.RowIndex].Value.ToString().Replace("'", "''") + "' THEN 0 ELSE [End Reason ID] END", connection);
					connection.Open();
					reader = command.ExecuteReader();
					while (reader.Read())
					{
						getEndReasonForm.AddOption(reader[0].ToString());
					}
					
					reader.Close();
					getEndReasonForm.ShowDialog();
					if (getEndReasonForm.Option != "Abort" && getEndReasonForm.Option != cboEditIncrement[11, cboEditIncrement.CurrentCell.RowIndex].Value.ToString())
					{
						command = new SqlCommand("EXECUTE [Add Production Master Change History Table Record] " + cboEditIncrement[0, cboEditIncrement.CurrentCell.RowIndex].Value.ToString() + ", '" + StartupForm.UserName + "'", connection);
						command.ExecuteNonQuery();
						command = new SqlCommand("UPDATE [Production Master Table] SET [End Reason ID] = a.[End Reason ID] FROM [Save Production Reason Table] a WHERE [Production ID] = " + cboEditIncrement[0, cboEditIncrement.CurrentCell.RowIndex].Value.ToString() + " AND a.[Description] = '" + getEndReasonForm.Option.Replace("'", "''") + "'", connection);
						command.ExecuteNonQuery();
						cboEditIncrement[11, cboEditIncrement.CurrentCell.RowIndex].Value = getEndReasonForm.Option;
						productionDetailTable.AcceptChanges();
						cboEditIncrement[11, cboEditIncrement.CurrentCell.RowIndex].Selected = true;
					}
					
					connection.Close();					
					getEndReasonForm.Dispose();
					break;
			}
			cboEditIncrement.RowEnter += new DataGridViewCellEventHandler(DgvProductionSummaryRowEnter);				
		
		}
		
		void DgvProductionSummaryCellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			
			if (cboEditIncrement.CurrentCell.Value.ToString() != preEditValue)
			{
				if ((int)cboEditIncrement[18, cboEditIncrement.CurrentCell.RowIndex].Value == 6)
				{	
					MessageBox.Show("This is a downtime production record.  You cannot edit the hours or reasons.", "Edit not Allowed");
					cboEditIncrement.CancelEdit();
					cboEditIncrement.CurrentCell.Value = preEditValue;
				}	
				else if (e.ColumnIndex == 7 || e.ColumnIndex == 8)
				{
					decimal result;
					if (decimal.TryParse(cboEditIncrement.CurrentCell.Value.ToString(), out result))
					{
						if (cboEditMethod.Text == "1/4 Hours" && cboEditIncrement.CurrentCell.Value.ToString().Length > 0 && (decimal.Parse(cboEditIncrement.CurrentCell.Value.ToString(), NumberStyles.Number) * 4).ToString("N2").Substring((decimal.Parse(cboEditIncrement.CurrentCell.Value.ToString(), NumberStyles.Number) * 4).ToString("N2").Length - 2, 2) != "00")
						{
							MessageBox.Show("Error - you must enter time in 1/4th hours", "Invalid Hours");
							cboEditIncrement.CancelEdit();
							cboEditIncrement.CurrentCell.Value = preEditValue;
						}
						else
						{
							decimal hoursAdjustment =  decimal.Parse(cboEditIncrement.CurrentCell.Value.ToString(), NumberStyles.Number) - decimal.Parse(preEditValue, NumberStyles.Number);
							cboEditIncrement.CurrentCell.Value = (decimal.Parse(cboEditIncrement.CurrentCell.Value.ToString(), NumberStyles.Number)).ToString("N2");
							command = new SqlCommand("INSERT INTO [Production Hours Adjustment Table] SELECT " + cboEditIncrement.CurrentRow.Cells[0].Value.ToString() + ", '" + StartupForm.UserName + "', GETDATE(), " + (e.ColumnIndex - 6).ToString() + ", NULL, " + hoursAdjustment.ToString(), connection);
							connection.Open();
							command.ExecuteNonQuery();
							connection.Close();
							int newRowIndex = -1;
							foreach(DataGridViewRow row in dgvHoursByLine.Rows)
							{
								if(row.Cells[0].Value.ToString().Equals(cboEditIncrement[3, e.RowIndex].Value.ToString()))
    							{
        							newRowIndex = row.Index;
        							break;
    							}
							}

							decimal newLineHours = decimal.Parse(dgvHoursByLine[1, newRowIndex].Value.ToString(), NumberStyles.Number) + hoursAdjustment;
							dgvHoursByLine[1, newRowIndex].Value = newLineHours.ToString("N2");
							if (newLineHours > 12)
							{				
								dgvHoursByLine.Rows[newRowIndex].Cells[1].Style = new DataGridViewCellStyle{ForeColor = Color.Red};
							}
							productionDetailTable.AcceptChanges();
							FormatDgvProductionSummary(cboEditIncrement.CurrentCell.RowIndex);
						}
					}
					else
					{
						cboEditIncrement.CancelEdit();
						cboEditIncrement.CurrentCell.Value = preEditValue;
					}
				}
				else if (e.ColumnIndex == 23)
				{
					command = new SqlCommand("EXECUTE [Add Production Master Change History Table Record] " + cboEditIncrement[0, cboEditIncrement.CurrentCell.RowIndex].Value.ToString() + ", '" + StartupForm.UserName + "'", connection);
					connection.Open();
					command.ExecuteNonQuery();
					command = new SqlCommand("UPDATE [Production Master Table] SET [Notes] = '" + cboEditIncrement.CurrentCell.Value.ToString().Replace("'", "''") + "' WHERE [Production ID] = " + cboEditIncrement[0, cboEditIncrement.CurrentCell.RowIndex].Value.ToString(), connection);
					command.ExecuteNonQuery();
					connection.Close();
					productionDetailTable.AcceptChanges();
				}
			}
		}
		
		void DgvProductionSummaryEditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			preEditValue = cboEditIncrement.CurrentCell.Value.ToString();
		}
		
		private void TxtEnter(object sender, EventArgs e)
		{
            decimal parseOutput = -1;
			if (decimal.TryParse(((TextBox)sender).Text, out parseOutput) == true)
			{
				((TextBox)sender).Text = string.Empty;
			}
			
			((TextBox)sender).SelectAll();
		}
		
		private void TxtKeyDown(object sender, KeyEventArgs e)
		{	
			FormatClass.TextBoxKeyDownNumbersOnly(sender, e);
		}
		
		private void TxtKeyPress(object sender, KeyPressEventArgs e)
		{
			FormatClass.TextBoxKeyPressNumberOrDecimalOnly(sender, e, 2);
		}

        void DgvProductionSummaryRowEnter(object sender, DataGridViewCellEventArgs e)
		{
			int newRowIndex = -1;
			foreach(DataGridViewRow row in dgvHoursByLine.Rows)
			{
				if(row.Cells[0].Value.ToString().Equals(cboEditIncrement[3, e.RowIndex].Value.ToString()))
    			{
        			newRowIndex = row.Index;
        			break;
    			}
			}
			
			dgvHoursByLine.Rows[newRowIndex].Selected = true;
			dgvHoursByLine.FirstDisplayedScrollingRowIndex = newRowIndex;
			UpdateDownTimeInfoGrid(e.RowIndex);
		}
		
		void TxtNewDownTimeHoursKeyUp(object sender, KeyEventArgs e)
		{
			decimal result;
			if (decimal.TryParse(txtNewDownTimeHours.Text, out result) && result > 0 && cboDowntimeReasons.Text.Length > 0)
			{
				cmdAddDowntimeRecord.Enabled = true;
			}
			else
			{
				cmdAddDowntimeRecord.Enabled = false;
			}
		}
		
		void TxtHoursLeave(object sender, EventArgs e)
		{
			if (cboEditMethod.Text == "1/4 Hours" && ((TextBox)sender).Text.Length > 0 && (decimal.Parse(((TextBox)sender).Text, NumberStyles.Number) * 4).ToString("N2").Substring((decimal.Parse(((TextBox)sender).Text, NumberStyles.Number) * 4).ToString("N2").Length - 2, 2) != "00")
			{
				MessageBox.Show("Error - you must enter time in 1/4th hours", "Invalid Hours");
				((TextBox)sender).Focus();
			}
			else
			{
				decimal result;
				if (decimal.TryParse(((TextBox)sender).Text, out result) && result > 0)
				{
					((TextBox)sender).Text = result.ToString("N2");
				}
				else
				{
					((TextBox)sender).Text = "0.00";
				}
			}
		}
		
		void CboDowntimeReasonsSelectedIndexChanged(object sender, EventArgs e)
		{
			if (cboDowntimeReasons.Text.Length == 0 || decimal.Parse(txtNewDownTimeHours.Text, NumberStyles.Number) == 0)
			{
				cmdAddDowntimeRecord.Enabled = false;
			}
			else
			{
				cmdAddDowntimeRecord.Enabled = true;
                rtbNewJobDTRecordNotes.Focus();
			}
		}
		
		void CmdAddDowntimeRecordClick(object sender, EventArgs e)
		{
			command = new SqlCommand("INSERT INTO [Production Downtime Hours Table] SELECT " + cboEditIncrement[0, cboEditIncrement.CurrentCell.RowIndex].Value.ToString() + ", " + nextDowntimeRecordID.ToString() + ", a.[Downtime Reason ID], 0 FROM [Downtime Reason Table] a INNER JOIN [Machine Table] b ON a.[Operation ID] = b.[Operation ID] WHERE b.[Machine No] = " + cboEditIncrement[3, cboEditIncrement.CurrentCell.RowIndex].Value.ToString() + " AND a.[Description] = '" + cboDowntimeReasons.Text.Replace("'", "''") + "'", connection);
			connection.Open();
			command.ExecuteNonQuery();
			command = new SqlCommand("INSERT INTO [Production Hours Adjustment Table] SELECT " + cboEditIncrement[0, cboEditIncrement.CurrentCell.RowIndex].Value.ToString() + ", '" + StartupForm.UserName + "', GETDATE(),  3, " + nextDowntimeRecordID.ToString() + ", " + txtNewDownTimeHours.Text, connection);
			command.ExecuteNonQuery();
            if (!string.IsNullOrEmpty(rtbNewJobDTRecordNotes.Text))
            {
                command = new SqlCommand("INSERT INTO [Production Downtime Notes Table] SELECT " + cboEditIncrement[0, cboEditIncrement.CurrentCell.RowIndex].Value.ToString() + ", " + nextDowntimeRecordID.ToString() + ", '" + rtbNewJobDTRecordNotes.Text.Replace("'", "''") + "'", connection);
                command.ExecuteNonQuery();
            }
            connection.Close();
			dgvDownTimeDetail.Rows.Add(nextDowntimeRecordID.ToString(), cboDowntimeReasons.Text, rtbNewJobDTRecordNotes.Text, txtNewDownTimeHours.Text);
			nextDowntimeRecordID ++;
			cboDowntimeReasons.Text = string.Empty;
			txtNewDownTimeHours.Text = "0.00";
			cmdAddDowntimeRecord.Enabled = false;
			cmdDone.Enabled = true;
		}
		
		void DgvDownTimeDetailDoubleClick(object sender, EventArgs e)
		{
			DialogResult answer = MessageBox.Show("Do you wish to 0 out the hours of this downtime record?", "Delete Downtime?", MessageBoxButtons.YesNo);
			if (answer == DialogResult.Yes)
			{
				command = new SqlCommand("INSERT INTO [Production Hours Adjustment Table] SELECT " + cboEditIncrement[0, cboEditIncrement.CurrentCell.RowIndex].Value.ToString() + ", '" + StartupForm.UserName + "', GETDATE(),  3, " +  dgvDownTimeDetail[0, dgvDownTimeDetail.CurrentRow.Index].Value.ToString() + ", -" + dgvDownTimeDetail[3, dgvDownTimeDetail.CurrentRow.Index].Value.ToString(), connection);
				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();
				dgvDownTimeDetail.Rows.RemoveAt(dgvDownTimeDetail.CurrentRow.Index);
			}
		}
		
		void CmdDoneClick(object sender, EventArgs e)
		{
			decimal dtHours = 0;
			foreach(DataGridViewRow row in dgvDownTimeDetail.Rows)
			{
				dtHours += decimal.Parse(row.Cells[3].Value.ToString(), NumberStyles.Number);
			}
			
			if (dtHours != decimal.Parse(cboEditIncrement.CurrentCell.Value.ToString(), NumberStyles.Number))
			{
				RefreshProductionSummary();
			}
			
			pnlDownTimeInfo.Enabled = false;
			dgvDownTimeDetail.DefaultCellStyle.BackColor = Color.LightGray;
			pnlTop.Enabled = true;
			cboEditIncrement.Enabled = true;
			cboEditIncrement.DefaultCellStyle.BackColor = Color.White;
			cboEditIncrement.Select();
		}
		
		void UpdateDownTimeInfoGrid(int rowIndex)
		{
			dgvDownTimeDetail.Rows.Clear();
			command = new SqlCommand("SELECT CAST(a.[Record ID] AS int), b.[Description], ISNULL(d.[Notes], ''), a.[Hours] + ISNULL(c.[Hours], 0) FROM [Production Downtime Hours Table] a INNER JOIN [Downtime Reason Table] b ON a.[Downtime Reason ID] = b.[Downtime Reason ID] LEFT JOIN [Production Downtime Hours Adjustments by Production ID and Record ID View] c ON a.[Production ID] = c.[Production ID] AND a.[Record ID] = c.[Record ID] LEFT JOIN [Production Downtime Notes Table] d ON a.[Production ID] = d.[Production ID] and a.[Record ID] = d.[Record ID] WHERE a.[Production ID] = " + cboEditIncrement[0, rowIndex].Value.ToString() + " ORDER BY a.[Record ID]", connection);
			connection.Open();
			reader = command.ExecuteReader();
			if (reader.Read())
			{
				do
				{
					if ((decimal)reader[3] != 0)
					{
						dgvDownTimeDetail.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString());
					}
					
					nextDowntimeRecordID = (int)reader[0];
				}
				while (reader.Read());
				
				nextDowntimeRecordID++;
			}
			else
			{
				nextDowntimeRecordID = 1;
			}
			
			reader.Close();
			connection.Close();
		}
		
		void dtpValueChanged(object sender, EventArgs e)
		{
			if ((decimal)((DateTimePicker)sender).Value.Minute / 15 != Math.Round((decimal)((DateTimePicker)sender).Value.Minute / 15, 0))
			{
				MessageBox.Show("Error - you must enter a time in 1/4th hours", "Invalid Start Time");
				((DateTimePicker)sender).Focus();
			}
		}
		
		void DtpProductionDateValueChanged(object sender, EventArgs e)
		{
				RefreshProductionSummary();
		}
		
		void CboMachineFilterSelectedIndexChanged(object sender, EventArgs e)
		{
			if (cboMachineFilter.Items.Count > 0)
			{
				if (cboMachineFilter.Text == "All")
				{
					productionDetailTable.DefaultView.RowFilter = string.Empty;
				}
				else
				{
					productionDetailTable.DefaultView.RowFilter = "[Mach No] = " + cboMachineFilter.Text;
				}
				
				FormatDgvProductionSummary(-1);
			}
		}
		
		void TxtDTProductionRecordHoursKeyUp(object sender, KeyEventArgs e)
		{
			decimal result;
			if (decimal.TryParse(txtDTProductionRecordHours.Text, out result) && result > 0 && cboDTProductionRecordReason.Text.Length > 0)
			{
				cmdSaveNewDTProductionRecord.Enabled = true;
			}
			else
			{
				cmdSaveNewDTProductionRecord.Enabled = false;
			}
		}
		
		void CboDTProductionRecordReasonSelectedIndexChanged(object sender, EventArgs e)
		{
			if (cboDTProductionRecordReason.Text.Length == 0 || decimal.Parse(txtDTProductionRecordHours.Text, NumberStyles.Number) == 0)
			{
				cmdSaveNewDTProductionRecord.Enabled = false;
			}
			else
			{
				cmdSaveNewDTProductionRecord.Enabled = true;
				cmdSaveNewDTProductionRecord.Select();
			}
		}
		
		void CmdCreateNewDTProductionRecordClick(object sender, EventArgs e)
		{
			cboEditIncrement.RowEnter -= DgvProductionSummaryRowEnter;
			pnlTop.Enabled = false;
			cboEditIncrement.Enabled = false;
			cboEditIncrement.DefaultCellStyle.BackColor = Color.LightGray;
			if (cboMachineFilter.Text != "All")
			{
				cboDTRecordMachine.Text = cboMachineFilter.Text;
			}
			else
			{
				cboDTRecordMachine.SelectedIndex = 0;
			}
			
			txtDTProductionRecordHours.Text = "0.00";
			cboDTProductionRecordReason.SelectedIndex = 0;
			rtbDTReasonNotes.Text = string.Empty;
			cmdSaveNewDTProductionRecord.Enabled = false;
			pnlAddNewDTProductionRecord.Enabled = true;
			cboDTRecordMachine.Focus();
		}
		
		void HideNewDowntimeRecordPanel()
		{
			pnlAddNewDTProductionRecord.Enabled = false;
			pnlTop.Enabled = true;
			cboEditIncrement.Enabled = true;
			cboEditIncrement.DefaultCellStyle.BackColor = Color.White;
			cboEditIncrement.RowEnter += new DataGridViewCellEventHandler(DgvProductionSummaryRowEnter);
			cboEditIncrement.Focus();
		}
		void CmdAbortNewDTProductionRecordClick(object sender, EventArgs e)
		{
			 HideNewDowntimeRecordPanel();
		}
		
		void CmdSaveNewDTProductionRecordClick(object sender, EventArgs e)
		{
			connection.Open();
			command = new SqlCommand("SELECT CAST([Downtime Reason ID] AS int), [Operator] FROM [Downtime Reason Table] WHERE [Operation ID] = 9 AND [Description] = '" + cboDTProductionRecordReason.Text.Replace("'", "''") + "'", connection);
			reader = command.ExecuteReader();
			reader.Read();
			int dtReason = (int)reader[0];
			bool operatorCharged = reader.GetBoolean(1);
			reader.Close();
			connection.Close();
			bool OKtoSave = true;
			int operatorNumber = 0;
			if (operatorCharged)
			{
				operatorNumber = GetOperatorID(cboDTRecordMachine.Text);
				if (operatorNumber == 0)
				{
					OKtoSave = false;
				}
			}
				
			if (OKtoSave)
			{
				command = new SqlCommand("SELECT [Master Item No] FROM [Inventory Master Table] WHERE [Item Type No] = 6 AND [Description] = '" + cboDTProductionRecordReason.Text.Replace("'", "''") + "'", connection);
				connection.Open();
				int masterItemNumber = (int)command.ExecuteScalar();
				if (operatorNumber == 0)
				{
					command = new SqlCommand("EXECUTE [Create Production Master Table Record Stored Procedure] NULL, '" + StartupForm.UserName + "', " + cboDTRecordMachine.Text + ", " + masterItemNumber.ToString() + ", '" + cboEditIncrement[10, cboEditIncrement.CurrentCell.RowIndex].Value.ToString() + "', NULL", connection);
				}
				else
				{
					command = new SqlCommand("EXECUTE [Create Production Master Table Record Stored Procedure] " + operatorNumber.ToString() + ", '" + StartupForm.UserName + "', " + cboDTRecordMachine.Text + ", " + masterItemNumber.ToString() + ", '" + cboEditIncrement[10, cboEditIncrement.CurrentCell.RowIndex].Value.ToString() + "', NULL", connection);
				}

				int productionRecordID = (int)command.ExecuteScalar();
				if (rtbDTReasonNotes.Text.Trim().Length > 0)
				{
					command = new SqlCommand("UPDATE [Production Master Table] SET [End Reason ID] = 1, [Notes] = '" + rtbDTReasonNotes.Text.Replace("'", "''") + "' WHERE [Production ID] = " + productionRecordID.ToString(), connection);
				}
				else
				{
					command = new SqlCommand("UPDATE [Production Master Table] SET [End Reason ID] = 1 WHERE [Production ID] = " + productionRecordID.ToString(), connection);
				}
				    
				command.ExecuteNonQuery();
					
				command = new SqlCommand("INSERT INTO [Production Downtime Hours Table] SELECT " + productionRecordID.ToString() + ", 1, [Downtime Reason ID], 0 FROM [Downtime Reason Table] WHERE [Operation ID] = 9 AND [Description] = '" + cboDTProductionRecordReason.Text.Replace("'","''") + "'", connection);
				command.ExecuteNonQuery();
				command = new SqlCommand("INSERT INTO [Production Hours Adjustment Table] SELECT " + productionRecordID.ToString() + ", '" + StartupForm.UserName + "', GETDATE(),  3, 1, " + txtDTProductionRecordHours.Text, connection);
				command.ExecuteNonQuery();
				connection.Close();
				RefreshProductionSummary();
				HideNewDowntimeRecordPanel();
			}
			else
			{
				MessageBox.Show("Save Aborted", "Downtime Record not Created");
			}
		}
		
		int GetOperatorID(string machineNumber)
		{
			int operatorNumber;
			OptionsForm getOperatorForm = new OptionsForm("Choose Operator", false, true);
			command = new SqlCommand("SELECT a.[Number] + ' - ' + a.[Name] FROM [Operator Table] a INNER JOIN [Machine Table] b ON ISNULL(a.[Operation ID], b.[Operation ID]) = b.[Operation ID] WHERE b.[Machine No] = " + machineNumber + " AND a.[Active] = 1 ORDER BY [Number]", connection);
			connection.Open();
			reader = command.ExecuteReader();
			if (reader.Read())
			{
				do
				{
					getOperatorForm.AddOption(reader[0].ToString());
				}
				while (reader.Read());
					
				reader.Close();
			}
			else
			{
				reader.Close();
				command = new SqlCommand("SELECT [Number] + ' - ' + [Name] FROM [Operator Table] WHERE [Active] = 1 ORDER BY [Number]", connection);
				reader = command.ExecuteReader();
				while (reader.Read())
				{
					getOperatorForm.AddOption(reader[0].ToString());
				}
						
				reader.Close();
			}
			
			
			getOperatorForm.ShowDialog();
			if (getOperatorForm.Option != "Abort")
			{
			    command = new SqlCommand("SELECT CAST([Operator ID] AS int) FROM [Operator Table] WHERE [Number] + ' - ' + [Name] = '" + getOperatorForm.Option + "'", connection);
			    operatorNumber = (int)command.ExecuteScalar();
			}
			else
			{
				operatorNumber = 0;
			}

			connection.Close();
			getOperatorForm.Dispose();
			return operatorNumber;
		}
		
		void CboShiftSelectedIndexChanged(object sender, EventArgs e)
		{
			RefreshProductionSummary();
		}
		
		void FormatDgvProductionSummary(int row)
		{
			int firstRow;
			int lastRow;
			
			if (row == -1)
			{
				firstRow = 0;
				lastRow = cboEditIncrement.Rows.Count - 1;
			}
			else
			{
				firstRow = row;
				lastRow = row;
			}
	
			for (int i = firstRow; i <= lastRow; i++)
			{
				for (int j=12; j<=17; j++)
				{
					FormatDecimalCell(i, j);
				}
					
				for (int j = 19; j <= 22; j++)
				{
					FormatDecimalCell(i, j);
				}
			}
		}
		
		void FormatDecimalCell(int row, int column)
		{
			if ((decimal)cboEditIncrement[column, row].Value < 0)
			{
				cboEditIncrement.Rows[row].Cells[column].Style = new DataGridViewCellStyle{ForeColor = Color.Red};
			}
		}

        private void cboDTRecordMachine_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboDTProductionRecordReason.Items.Clear();
            connection.Open();
            command = new SqlCommand("SELECT a.[Description] FROM [Downtime Reason Table] a INNER JOIN [Machine Table] b ON a.[Operation ID] = b.[Operation ID] WHERE b.[Machine No] = " + cboDTRecordMachine.Text + " ORDER BY [Display Order]", connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                cboDTProductionRecordReason.Items.Add(reader[0].ToString());
            }

            reader.Close();
            connection.Close();
            cboDTProductionRecordReason.SelectedIndex = 0;
        }

        private void cmdJobToDateStats_Click(object sender, EventArgs e)
        {
            JobToDateStatsForm jobStatsForm = new JobToDateStatsForm(cboEditIncrement[4, cboEditIncrement.CurrentCell.RowIndex].Value.ToString(), true, "0", "0", "0", "0");
            jobStatsForm.ShowDialog();
            jobStatsForm.Dispose();
        }

        private void dgvDownTimeDetail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {   
            if (dgvDownTimeDetail.CurrentCell.Value == null)
            {
                command = new SqlCommand("EXECUTE [Add or Update Downtime Notes Table] " + cboEditIncrement[0, cboEditIncrement.CurrentCell.RowIndex].Value.ToString() + ", " + dgvDownTimeDetail[0, dgvDownTimeDetail.CurrentCell.RowIndex].Value.ToString() + ", NULL", connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            else if (dgvDownTimeDetail.CurrentCell.Value.ToString() != preEditValue)
            {
                command = new SqlCommand("EXECUTE [Add or Update Downtime Notes Table] " + cboEditIncrement[0, cboEditIncrement.CurrentCell.RowIndex].Value.ToString() + ", " + dgvDownTimeDetail[0, dgvDownTimeDetail.CurrentCell.RowIndex].Value.ToString() + ", '" + dgvDownTimeDetail[2, dgvDownTimeDetail.CurrentCell.RowIndex].Value.ToString().Replace("'", "''") + "'", connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        private void dgvDownTimeDetail_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            preEditValue = cboEditIncrement.CurrentCell.Value.ToString();
        }
    }
}