/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 8/17/2012
 * Time: 9:32 AM
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
	using System.IO;
	using System.Windows.Forms;

	/// <summary>
	/// Description of SlittingProductionForm.
	/// </summary>
	public partial class SlittingProductionForm : Form
	{
        private CommentForm productionNotesForm;
        private static bool jobHistoryFormOpen = false;
		private string prodJobNumber;
		private string operatorName;
		private string prodMasterItemNumber;
		private DateTime endOfShiftTime;
        private int incomingProdLinearFeet;
        private bool newJob;
		private int currentProductionRecord;
		private DateTime startTime;
		private string jobDescription = string.Empty;
		private bool wipInput;
		private bool wipPrinted;
        private DataTable UPCCodesTable;
		private int inputMasterItemNo;
		private int inputReferenceItemNo;
		private string inputFilmName;
		private int nextDownTimeRecordID = 1;
		private int nextScrapRecordID = 1;
		private bool comboWIP;
		private int streamCount = 0;
		private JobHistoryForm currentJobHistoryForm;
		private SqlConnection connection1 = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
		private SqlConnection connection2 = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
		private SqlDataReader reader;
		private SqlCommand command;
		private DataTable laneJobInformationTable;
		private ConsumedFilmForm consumedFilmForm;
		private string slitMasterItemList = string.Empty;
		private string currentPalletList;
		private int consumedFeet = 0;
		private decimal currentWidth = 0;
		private int currentInputLF;
		private int currentunwindRollNumber = 0;
		private int createdFeet;
		private int defaultSlitRollFeet = 0;
		private int unwindRollNumber;
		private int setNumber;
						
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1306:SetLocaleForDataTypes")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "wip")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		public SlittingProductionForm(string currentOperatorName, string prodJobNo, string prodMasterItemNo, int wipType, int currentProdRecord, DateTime prodStartTime, DateTime endOfShift, int incomingProdLF, bool parNewJob)
		{
			InitializeComponent();
			rtbJobTitle.SelectionAlignment = HorizontalAlignment.Center;
			rtbJobTitle.Text = "Slitting     Operator: " + currentOperatorName + "	    Job " + prodJobNo + "     Start Time: " + prodStartTime.ToString("dddd, MMMM d, yyyy h:mm tt");
			prodJobNumber = prodJobNo;
			operatorName = currentOperatorName;
			prodMasterItemNumber = prodMasterItemNo;
			endOfShiftTime = endOfShift;
            incomingProdLinearFeet = incomingProdLF;
            newJob = parNewJob;
			if (endOfShiftTime > DateTime.Now)
			{
				endOfShiftTimer.Interval = 60000;
				endOfShiftTimer.Enabled = true;
			}
			
			if (wipType == 2)
			{
				comboWIP = false;
                lblProductionExclCurrentPallet.Text = "Pallets Produced/Finished excl Current Pallet";
                cmdGetPallet.Text = "Get Pallet";
			}
			else
			{
				comboWIP = true;
                lblProductionExclCurrentPallet.Text = "Pallets Produced/Finished excl Current Pallets";
                cmdGetPallet.Text = "Get Pallet(s)";
			}
			
			currentProductionRecord = currentProdRecord;
			startTime = prodStartTime;
			command = new SqlCommand("SELECT CAST(b.[Item Type No] AS int), a.[Input Master Item No], b.[Reference Item No], b.[Description], a.[Standard Input Film Width], a.[Master Item No], a.[No Streams], c.[Description], [dbo].[Get Numbers Only](d.[UPC Code]), ISNULL(e.[SlitSpecInstructions], '') FROM [Slitting Specification Table] a INNER JOIN [Inventory Master Table] b ON a.[Input Master Item No] = b.[Master Item No] INNER JOIN [Inventory Master Table] c ON a.[Master Item No] = c.[Master Item No] INNER JOIN ([Finished Goods Specification Table] d INNER JOIN [JobJackets].[dbo].[tblItem] e ON d.[Item No] = e.[ItemNo]) ON a.[Job Jacket No] = d.[Job Jacket No] WHERE ISNULL(a.[Combo Master Item No], a.[Master Item No]) = " + prodMasterItemNo, connection1);
			connection1.Open();
			reader = command.ExecuteReader();
			if (reader.Read())
			{
				this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SlittingProductionFormFormClosing);
				this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SlittingProductionFormFormClosed);
                UPCCodesTable = new DataTable();
                UPCCodesTable.Columns.Add("UPC Code", typeof(string));
				if ((int)reader[0] == 1)
				{
					wipInput = false;
				}
				else
				{
					wipInput = true;
					command = new SqlCommand("SELECT [Reference Item No] FROM [Inventory Master Table] WHERE [Reference Item No] = " + prodJobNumber.Substring(0, prodJobNumber.Length - 2) + "11 and [Item Type No] in (2,3)", connection2);
					connection2.Open();
					object printJobNo = command.ExecuteScalar();
					if (printJobNo == null)
					{
						// No print job, therefore laminations from other jobs could be the input film
						wipPrinted = false;
					}
					else
					{
						wipPrinted = true;
					}
					
					connection2.Close();
				}
				
				inputMasterItemNo = (int)reader[1];
				inputReferenceItemNo = (int)reader[2];
				inputFilmName = reader[3].ToString();
				
				rtbInputFilm.Text = "Input Film per Job Jacket: " + ((decimal)reader[4]).ToString("N4") + "\" " + inputFilmName;
				do	
				{
					slitMasterItemList += reader[5].ToString() + ", ";
					streamCount += (int)reader[6];
					jobDescription += reader[7].ToString().Substring(0, reader[7].ToString().Length - 9) + "/";
                    if (!string.IsNullOrEmpty(reader[8].ToString()))
                    {
                        DataRow row = UPCCodesTable.NewRow();
                        row["UPC Code"] = reader[8];
                        UPCCodesTable.Rows.Add(row);
                    }

                    if (wipType == 2)
                    {
                            //Job Jacket WIP
                        rtbSpecialInstructions.Text = reader[9].ToString();
                    }
                    else
                    {
                        //Combo WIP
                        rtbSpecialInstructions.Text += "Job " + reader[0].ToString() + "\r\n" + reader[9].ToString() + "\r\n\r\n";
                    }
                }
				while (reader.Read());
				
				reader.Close();
				jobDescription = jobDescription.Substring(0,jobDescription.Length - 1);
				slitMasterItemList = slitMasterItemList.Substring(0, slitMasterItemList.Length - 2);
				command = new SqlCommand("DELETE FROM [Current Pallets at Machine Table] WHERE [Pallet ID] IN (SELECT [PALLET ID] FROM [Pallet Table] WHERE [Location ID] = " + MainForm.MachineNumber + ") AND [Master Item No] NOT IN (" + slitMasterItemList + ")", connection1);
				command.ExecuteNonQuery();
				RefreshDgvJobs();
			}
			else
			{
				MessageBox.Show("Error - Job not found in Job Jacket database but did exist at one point.  Please contact the office", "Job Not Found");
				reader.Close();
				connection1.Close();
				this.Close();
			}
			
			command = new SqlCommand("SELECT [Setup Hrs], [Run Hrs], [DT Hrs], [Setup Hrs] + [Run Hrs] + [DT Hrs] FROM [dbo].[Get Standard Production Information] (" + currentProductionRecord.ToString() +")", connection1);
			reader = command.ExecuteReader();
			reader.Read();
			txtSetupHours.Text = ((decimal)reader[0]).ToString("N2");
			txtRunHours.Text = ((decimal)reader[1]).ToString("N2");
			txtDownTimeHours.Text = ((decimal)reader[2]).ToString("N2");
			txtTotalHours.Text = ((decimal)reader[3]).ToString("N2");
            reader.Close();
            ModulesClass.FillDowntimeComboBox(cboDownTimeReasons, "3");
            nextDownTimeRecordID = ModulesClass.FillDowntimeDetails(rtbDownTimeRecords, cboRemoveDowntimeRecord, pnlRemoveDownTimeRecord, currentProductionRecord.ToString());
            ModulesClass.FillScrapReasonComboBox(cboScrapReasons, "3");
            nextScrapRecordID = ModulesClass.FillScrapDetails(rtbScrapRecords, cboRemoveScrapRecord, pnlRemoveScrapRecord, txtTotalScrapPounds, currentProductionRecord.ToString());
			FillSlitSetText();
			FillLooseRollText();
			consumedFilmForm = new ConsumedFilmForm(false);	
				
			// Get rolls consumed for this job to date on this machine
//			int nextSetNumber = FillConsumedFilmGrid();
			laneJobInformationTable = new DataTable();
			laneJobInformationTable.Columns.Add("Lane", typeof(int));
			laneJobInformationTable.Columns.Add("prodMasterItemNumber", typeof(int));
			laneJobInformationTable.Columns.Add("JobJacketNumber", typeof(string));
			laneJobInformationTable.Columns.Add("SlitWidth", typeof(decimal));
			laneJobInformationTable.Columns.Add("DefaultPounds", typeof(decimal));
            laneJobInformationTable.Columns.Add("UPC Code", typeof(string));
            laneJobInformationTable.Columns.Add("PalletID", typeof(int));
            
			
			command = new SqlCommand("SELECT TOP 1 c.[Create Date], CAST(c.[Original LF] AS int) FROM [Production Roll Table] a INNER JOIN [Production Master Table] b ON a.[Production ID] = b.[Production ID] INNER JOIN [Roll Table] c ON a.[Roll ID] = c.[Roll ID] WHERE b.[Machine No] = " + MainForm.MachineNumber + " AND b.[Master Item No] = " + prodMasterItemNumber + " AND c.[Original LF] > 0 GROUP BY c.[Create Date], CAST(c.[Original LF] AS int) HAVING COUNT(a.[Roll ID]) = " + streamCount + " ORDER BY c.[Create Date] DESC", connection1);
			reader = command.ExecuteReader();
			if (reader.Read())
			{
				defaultSlitRollFeet = (int)reader[1];
				command = new SqlCommand("SELECT a.[Lane], COALESCE(d.[Master Item No], e.[Input Master Item No], " + prodMasterItemNumber + "), COALESCE(d.[Job Jacket No], e.[Job Jacket No], " + prodJobNumber.Substring(0, prodJobNumber.Length - 2) + "), c.[Width], c.[Original Pounds], [dbo].[Get Numbers Only](e.[UPC Code]), c.[Pallet ID] FROM [Production Roll Table] a INNER JOIN [Production Master Table] b ON a.[Production ID] = b.[Production ID] INNER JOIN ([Roll Table] c LEFT JOIN [Slitting Specification Table] d ON c.[Master Item No] = d.[Master Item No] LEFT JOIN [Finished Goods Specification Table] e ON c.[Master Item No] = e.[Master Item No]) ON a.[Roll ID] = c.[Roll ID] WHERE b.[Machine No] = " + MainForm.MachineNumber + " AND b.[Master Item No] = " + prodMasterItemNumber + " AND c.[Create Date] = '" + ((DateTime)reader[0]).ToString() + "' ORDER BY a.[Lane]", connection1);
				reader.Close();
				reader = command.ExecuteReader();
				
				while (reader.Read())
				{
					DataRow row = laneJobInformationTable.NewRow();
					for (int i = 0; i <= 5; i++)
					{
						row[i] = reader[i];
					}

					laneJobInformationTable.Rows.Add(row);
				}
			}
			else			
			{
				// No Slit Production has been recorded yet or the incorrect number or streams were in the last set
				
				for (int i = 0; i < dgvJobs.RowCount; i++)
            	{
					for (int j = 1; j <= Int32.Parse(dgvJobs.Rows[i].Cells[2].Value.ToString(), NumberStyles.Number); j++)
					{
						DataRow row = laneJobInformationTable.NewRow();
						if (dgvJobs.RowCount == 1)
						{
							row[0] = j;
						}
						row[1] = Int32.Parse(dgvJobs.Rows[i].Cells[3].Value.ToString(), NumberStyles.Number);
						row[2] = dgvJobs.Rows[i].Cells[0].Value.ToString();
						row[3] = dgvJobs.Rows[i].Cells[6].Value.ToString();
						laneJobInformationTable.Rows.Add(row);
					}
				}
				
				if (dgvJobs.RowCount > 1)
				{
					// Combo job so lanes must be assigned
					for (int i = 1; i <= laneJobInformationTable.Rows.Count; i++)
					{
						OptionsForm frmAssignStream = new OptionsForm("Job on Lane " + ConvertClass.GetLaneName(i), false, true);
						foreach (DataRow row in laneJobInformationTable.Rows)
						{
							if (row["Lane"] == DBNull.Value)
							{
								frmAssignStream.AddOption((string)row["JobJacketNumber"]);
							}
						}
							
						frmAssignStream.ShowDialog();
						if (frmAssignStream.Option == "Abort")
						{
							frmAssignStream.Dispose();
							this.Close();
						}
						else
						{
							for (int j = 0; j < laneJobInformationTable.Rows.Count; j++)
							{
								if (laneJobInformationTable.Rows[j]["Lane"] == DBNull.Value && (string)laneJobInformationTable.Rows[j]["JobJacketNumber"] == frmAssignStream.Option)
								{
									laneJobInformationTable.Rows[j]["Lane"] = i;
									break;
								}
							}		
						}
							
						frmAssignStream.Dispose();
					}
				}
			}
				
			reader.Close();
            command = new SqlCommand("SELECT ISNULL([Notes], '') FROM [Production Master Table] WHERE [Production ID] = " + currentProductionRecord.ToString(), connection1);
            string comments = command.ExecuteScalar().ToString();
            connection1.Close();
            productionNotesForm = new CommentForm(prodJobNumber + " Production Notes", comments, false);
        }

        private void SlittingProductionFormShown(object sender, EventArgs e)
        {
            connection1.Open();
            FillConsumedFilmGrid();
            connection1.Close();
            if (laneJobInformationTable.Rows[0]["Lane"] == DBNull.Value)
            {
                if (laneJobInformationTable.Rows.Count == 1)
                {
                    // There is only one lane
                    laneJobInformationTable.Rows[0]["Lane"] = 1;
                }
                else
                {
                    for (int i = 1; i <= laneJobInformationTable.Rows.Count; i++)
                    {
                        if (dgvJobs.Rows.Count > 1)
                        {
                            OptionsForm assignStreamForm = new OptionsForm("Job on Lane " + ConvertClass.GetLaneName(i), false, true);
                            foreach (DataRow row in laneJobInformationTable.Rows)
                            {
                                if (row["Lane"] == DBNull.Value)
                                {
                                    assignStreamForm.AddOption((string)row["JobJacketNumber"]);
                                }
                            }

                            assignStreamForm.ShowDialog();
                            if (assignStreamForm.Option == "Abort")
                            {
                                assignStreamForm.Dispose();
                                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.SlittingProductionFormFormClosing);
                                this.Close();
                            }
                            else
                            {
                                for (int j = 0; j < laneJobInformationTable.Rows.Count; j++)
                                {
                                    if (laneJobInformationTable.Rows[j]["Lane"] == DBNull.Value && (string)laneJobInformationTable.Rows[j]["JobJacketNumber"] == assignStreamForm.Option)
                                    {
                                        laneJobInformationTable.Rows[j]["Lane"] = i;
                                        break;
                                    }
                                }
                            }

                            assignStreamForm.Dispose();
                        }
                        else
                        {
                            laneJobInformationTable.Rows[i - 1]["Lane"] = i;
                        }
                    }
                }
            }

            productionNotesForm.Show();
        }

        public static bool JobHistoryOpen
		{
			get
			{
				return jobHistoryFormOpen;
			}
			
			set
			{
				jobHistoryFormOpen = value;
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private int FillConsumedFilmGrid()
		{
            rtbInputFilm.Text = string.Empty;
            //DialogResult notDone = DialogResult.Yes;
            //DataTable UPCCodesToConfirm = UPCCodesTable;
            command = new SqlCommand("SELECT e.[Name], a.[Roll ID], b.[Width], a.[Start Usage Date], CAST(a.[Start Usage LF] AS int), a.[End Usage Date], CAST(ISNULL(a.[End Usage LF], 0) AS int), c.[Description] FROM [Production Consumed Roll Table] a INNER JOIN ([Roll Table] b INNER JOIN [Inventory Master Table] c ON b.[Master Item No] = c.[Master Item No]) ON a.[Roll ID] = b.[Roll ID] INNER JOIN ([Production Master Table] d INNER JOIN [Operator Table] e ON d.[Operator ID] = e.[Operator ID]) ON a.[Start Production ID] = d.[Production ID] WHERE a.[Start Usage LF] != ISNULL(a.[End Usage LF], 0) AND a.[Start Production ID] = " + currentProductionRecord.ToString() + " ORDER BY a.[Start Usage Date]", connection1);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                consumedFilmForm.AddRoll(reader[0].ToString(), (DateTime)reader[3], reader[1].ToString(), "Consumed", "1", ((decimal)reader[2]).ToString("N4") + "\" " + reader[7].ToString(), (int)reader[4]);
                if (reader[5] != DBNull.Value && (int)reader[6] > 0)
                {
                    // There is a return
                    consumedFilmForm.AddRoll(reader[0].ToString(), (DateTime)reader[5], reader[1].ToString(), "Returned", "1", ((decimal)reader[2]).ToString("N4") + "\" " + reader[7].ToString(), (int)reader[6]);
                }
                else
                {
                    // There is no Return
                    rtbInputFilm.Text = "Currently Roll R" + reader[1].ToString() + " " + ((decimal)reader[2]).ToString("N4") + "\" " + reader[7].ToString() + " @ " + ((int)reader[4]).ToString("N0") + " LF is being consumed";
                    currentInputLF = (int)reader[4];
                    currentWidth = (decimal)reader[2];
                    currentunwindRollNumber = (int)reader[1];
                    /* Partial programming in case we want to check the UPC on a "Save Progress" basis
                    while (notDone == DialogResult.Yes && UPCCodesTable.Rows.Count > 0)
                    {
                        GetInputForm getUPCCode = new GetInputForm("Scan UPC Code", "#", 0, 0, true);
                    }

                    if (UPCCodesTable.Rows.Count == 0)
                    {
                        rtbInputFilm.Text = "Currently Roll R" + reader[1].ToString() + " " + ((decimal)reader[2]).ToString("N4") + "\" " + reader[7].ToString() + " @ " + ((int)reader[4]).ToString("N0") + " LF is being consumed";
                        currentInputLF = (int)reader[4];
                        currentWidth = (decimal)reader[2];
                        currentunwindRollNumber = (int)reader[1];
                    }
                    */
                }

                consumedFeet += (int)reader[4] - (int)reader[6];
            }

            reader.Close();
            txtConsumedFeet.Text = consumedFeet.ToString("N0");
            /*
            if (UPCCodesTable.Rows.Count > 0)
            {
                MessageBox.Show("THe UPC Code(s) for the input roll were validated.  You cannot continue consuming a roll without it.", "Consumption Aborted");
                return 0;
            }
            else
            */
            {
                command = new SqlCommand("SELECT MAX([Set No]) + 1 FROM [Production Roll Table] WHERE [Input Roll ID 1] = " + currentunwindRollNumber, connection1);
                object nextSetNumber = command.ExecuteScalar();
                if (nextSetNumber != DBNull.Value)
                {
                    return Convert.ToInt32(nextSetNumber);
                }
                else
                {
                    return 1;
                }
            }
        }
					
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		private void TxtEnter(object sender, EventArgs e)
		{
			if (decimal.Parse(((TextBox)sender).Text, NumberStyles.Number) == 0)
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
		
		private void TxtNewDTHrsKeyUp(object sender, KeyEventArgs e)
		{
			decimal result;
			if (decimal.TryParse(txtNewDownTimeHours.Text, out result) && result > 0 && cboRemoveDowntimeRecord.Text.Length > 0)
			{
				cmdAddDowntimeRecord.Enabled = false;
			}
			else
			{
				cmdAddDowntimeRecord.Enabled = true;
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		private void TxtLeave(object sender, EventArgs e)
		{
			TextBox txt = (TextBox)sender;
			if (txt.Text.Length > 0 && (decimal.Parse(txt.Text, NumberStyles.Number) * 4).ToString("N2").Substring((decimal.Parse(txt.Text, NumberStyles.Number) * 4).ToString("N2").Length - 2, 2) != "00")
			{
				MessageBox.Show("Error - you must enter time in 1/4th hours", "Invalid Hours");
				((TextBox)sender).Focus();
			}
			else
			{
				decimal result;
				if (decimal.TryParse(txt.Text, out result) && result > 0)
				{
					((TextBox)sender).Text = result.ToString("N2");
				}
				else
				{
					((TextBox)sender).Text = "0.00";
				}
				
				txtTotalHours.Text = (decimal.Parse(txtSetupHours.Text, NumberStyles.Number) + decimal.Parse(txtRunHours.Text, NumberStyles.Number) + decimal.Parse(txtDownTimeHours.Text, NumberStyles.Number)).ToString("N2");
			}
		}
		
		private void TxtNewScrapPoundsKeyPress(object sender, KeyPressEventArgs e)
		{
			FormatClass.TextBoxKeyPressNumbersOnly(e);
		}
		
		private void TxtNewScrapPoundsKeyUp(object sender, KeyEventArgs e)
		{
			decimal result;
			if (decimal.TryParse(txtNewScrapPounds.Text, out result) && result > 0 && cboScrapReasons.Text.Length > 0)
			{
				cmdAddScrapRecord.Enabled = true;
			}
			else
			{
				cmdAddScrapRecord.Enabled = false;
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void TxtNewScrapPoundsLeave(object sender, EventArgs e)
		{
			int result;
			if (int.TryParse(txtNewScrapPounds.Text, out result) && result > 0)
			{
				txtNewScrapPounds.Text = result.ToString("N0");
			}
			else
			{
				txtNewScrapPounds.Text = "0";
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void CmdAddDTClick(object sender, EventArgs e)
		{
            ModulesClass.AddOownTimeReord(ref nextDownTimeRecordID, cboDownTimeReasons, txtNewDownTimeHours, txtDownTimeHours, txtTotalHours, rtbDownTimeRecords, cboRemoveDowntimeRecord, pnlRemoveDownTimeRecord, currentProductionRecord.ToString());
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void CmdRemoveDownTimeRecordClick(object sender, EventArgs e)
		{
            nextDownTimeRecordID = ModulesClass.RemoveDownTime(txtDownTimeHours, txtTotalHours, rtbDownTimeRecords, cboRemoveDowntimeRecord, pnlRemoveDownTimeRecord, currentProductionRecord.ToString());
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void CboDowntimeReasonsSelectedIndexChanged(object sender, EventArgs e)
		{
			if (cboDownTimeReasons.Text.Length == 0)
			{
				cmdAddDowntimeRecord.Enabled = false;
			}
			else if (decimal.Parse(txtNewDownTimeHours.Text, NumberStyles.Number) > 0)
			{
				cmdAddDowntimeRecord.Enabled = true;
				cmdAddDowntimeRecord.Select();
			}
		}
		
		private void CboRemoveDTInfoSelectedIndexChanged(object sender, EventArgs e)
		{
			if (cboRemoveDowntimeRecord.Text.Length == 0)
			{
				cmdRemoveDownTimeRecord.Enabled = false;
			}
			else 
			{
				cmdRemoveDownTimeRecord.Enabled = true;
				cmdRemoveDownTimeRecord.Select();
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void CmdAddScrapRecordClick(object sender, EventArgs e)
		{
            ModulesClass.AddScrapRecord(ref nextScrapRecordID, cboScrapReasons, txtNewScrapPounds, txtTotalScrapPounds, rtbScrapRecords, cboRemoveScrapRecord, pnlRemoveScrapRecord, currentProductionRecord.ToString());
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void CmdRemoveScrapRecordClick(object sender, EventArgs e)
		{
            nextScrapRecordID = ModulesClass.RemoveScrapRecord(txtTotalScrapPounds, rtbScrapRecords, cboRemoveScrapRecord, pnlRemoveScrapRecord, currentProductionRecord.ToString());
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void CboScrapReasonsSelectedIndexChanged(object sender, EventArgs e)
		{
			if (cboScrapReasons.Text.Length == 0)
			{
				cmdAddScrapRecord.Enabled = false;
			}
			else if (decimal.Parse(txtNewScrapPounds.Text, NumberStyles.Number) > 0)
			{
				cmdAddScrapRecord.Enabled = true;
				cmdAddScrapRecord.Select();
			}
		}
		
		private void CboRemoveScrapInfoSelectedIndexChanged(object sender, EventArgs e)
		{
			if (cboRemoveScrapRecord.Text.Length == 0)
			{
				cmdRemoveScrapRecord.Enabled = false;
			}
			else 
			{
				cmdRemoveScrapRecord.Enabled = true;
				cmdRemoveScrapRecord.Select();
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		private void SlittingProductionFormFormClosing(object sender, FormClosingEventArgs e)
		{	
			if (ModulesClass.ValidSaveDate(startTime.AddHours(double.Parse(txtTotalHours.Text, NumberStyles.Number)), endOfShiftTime))
			{
				WriteProductionRecord(startTime.AddHours(double.Parse(txtTotalHours.Text, NumberStyles.Number)), "3");
				this.Close();
			}
			else
			{
				e.Cancel = true;
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		private void CmdConsumeRollClick(object sender, EventArgs e)
		{
            GetInputForm getRollToConsumeForm = new GetInputForm("Scan/Input Roll to Consume Barcode", "R", 0, 0, true);
            getRollToConsumeForm.ShowDialog();
            if (getRollToConsumeForm.UserInput.Length > 0)
            {
                command = new SqlCommand("SELECT a.[Master Item No], a.[Width], f.[Description], ISNULL(a.[Location ID], d.[Location ID]), CAST(ROUND(a.[Current LF], 0) AS int), CAST(ROUND(a.[Original Pounds] * a.[Current LF] / a.[Original LF], 0) AS int), e.[Vendor Roll ID], g.[Description], g.[Inventory Available], b.[Allocated LF], f.[Reference Item No], CAST(f.[Item Type No] AS int), a.[Create Date], j.[Reference Item No], j.[Description], h.[Start Usage Date], h.[Start Usage LF], CAST(CASE WHEN ISNULL(k.[Film Type], '') = 'NON-WOVEN' THEN 1 ELSE 0 END AS bit) FROM [Roll Table] a LEFT JOIN ([Allocation Pick Table] b INNER JOIN [Allocation Master Table] c ON b.[Allocation ID] = c.[Allocation ID] AND c.[Master Item No] = " + prodMasterItemNumber + " AND c.[Pick Date] IS NOT NULL AND c.[Release Date] IS NULL AND c.[Void Date] IS NULL) ON a.[Roll ID] = b.[Roll ID] LEFT JOIN [Pallet Table] d ON a.[Pallet ID] = d.[Pallet ID] LEFT JOIN [Roll PO Table] e ON a.[Roll ID] = e.[Roll ID] INNER JOIN [Inventory Master Table] f ON a.[Master Item No] = f.[Master Item No] LEFT JOIN ([Production Consumed Roll Table] h INNER JOIN ([Production Master Table] i INNER JOIN [Inventory Master Table] j ON i.[Master Item No] = j.[Master Item No]) ON h.[Start Production ID] != " + currentProductionRecord.ToString() + " AND h.[Start Production ID] = i.[Production ID] AND i.[Machine No] = " + MainForm.MachineNumber + " AND h.[End Usage Date] IS NULL AND h.[Start Usage Date] > DATEADD(hh, -12, GETDATE())) ON a.[Roll ID] = h.[Roll ID] LEFT JOIN [Film View] k ON a.[Master Item No] = k.[Master Item No], [Location Table] g WHERE a.[Original LF] > 0 AND ISNULL(a.[Location ID], d.[Location ID]) = g.[Location ID] AND a.[Roll ID] = " + getRollToConsumeForm.UserInput.Substring(1), connection1);
                connection1.Open();
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    bool consumeRoll = false;
                    if ((int)reader[11] != 1 && (int)reader[11] != 2 && (int)reader[11] != 3)
                    {
                        MessageBox.Show("Error - roll " + getRollToConsumeForm.UserInput + " " + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + " is not a raw film nor WIP Roll and therefore is invalid", "Invalid Roll");
                    }
                    else if ((int)reader[4] == 0 && reader[15] == DBNull.Value)
                    {
                        MessageBox.Show("Error - roll " + getRollToConsumeForm.UserInput + " " + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + " at location L" + reader[3].ToString() + " (" + reader[7].ToString() + ")" + " has no inventory available (maybe a return is missing?)", "No Inventory");
                    }
                    else if ((int)reader[4] == 0 && reader[15] != DBNull.Value)
                    {
                        MessageBox.Show("Roll " + getRollToConsumeForm.UserInput + " " + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + " has no inventory available but was consumed by job " + reader[13].ToString() + " - " + reader[14].ToString() + " on this line in the last 12 hours on " + ((DateTime)reader[15]).ToShortDateString() + " at " + ((DateTime)reader[15]).ToShortTimeString() + " in the amount of " + ((decimal)reader[16]).ToString("N0") + " LF.  You must create a return before you can consume this roll for this job.", "Possible Recent Return Missing");
                    }
                    else if (!(bool)reader[8])
                    {
                        MessageBox.Show("Error - roll " + getRollToConsumeForm.UserInput + " " + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + " is unavailable due to being at location L" + reader[3].ToString() + " (" + reader[7].ToString() + ")", "Roll Unavailable");
                    }
                    else if (reader[3].ToString() != MainForm.MachineNumber)
                    {
                        MessageBox.Show("Error - roll " + getRollToConsumeForm.UserInput + " " + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + " is at location L" + reader[3].ToString() + " (" + reader[7].ToString() + ")", "Roll is not at machine");
                    }
                    else if (wipInput && (int)reader[11] != 2 && (int)reader[11] != 3)
                    {
                        MessageBox.Show("Error - roll " + getRollToConsumeForm.UserInput + " " + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + " is not a WIP roll and therefore not for job " + prodJobNumber + " - " + jobDescription + ".", "Invalid Roll");
                    }
                    else
                    {
                        DialogResult consumeDifferentWIPItemRoll = DialogResult.Yes;

                        if (wipInput && (int)reader[0] != inputMasterItemNo)
                        {
                            consumeDifferentWIPItemRoll = MessageBox.Show("Error - roll " + getRollToConsumeForm.UserInput + " " + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + " is not a WIP roll for job " + prodJobNumber + " - " + jobDescription + ".  Do you wish to consume the roll anyway (NO authorization required)?", "Consume WIP Roll from another job?", MessageBoxButtons.YesNo);
                        }

                        bool inputRollOK = true;
                        string overrideAuthorizedBy = string.Empty;
                        if (consumeDifferentWIPItemRoll == DialogResult.Yes && UPCCodesTable.Rows.Count > 0)
                        {
                            inputRollOK = ModulesClass.ValidateUPCCodes(prodJobNumber, getRollToConsumeForm.UserInput, UPCCodesTable, out overrideAuthorizedBy);
                        }

                        if (consumeDifferentWIPItemRoll == DialogResult.Yes && inputRollOK)
                        {
                            DialogResult answer = DialogResult.No;
                            string emailHeader = string.Empty;
                            string emailMessage = string.Empty;
                            if (wipInput && wipPrinted && (int)reader[0] != inputMasterItemNo)
                            {
                                answer = MessageBox.Show("WIP Roll " + getRollToConsumeForm.UserInput + " of Job J" + reader[10].ToString() + " - " + reader[2].ToString() + " is not a valid input roll for job " + prodJobNumber + ".  Do you wish to consume roll anyway (NOT ADVISED)?", "Override? (Requires Supervisor Authorization)", MessageBoxButtons.YesNo);
                                emailHeader = "Different Job's WIP Roll Consumed";
                                emailMessage = "On line " + MainForm.MachineNumber + " operator " + operatorName + " just consumed WIP roll " + getRollToConsumeForm.UserInput + " of job " + reader[10].ToString() + " - " + reader[2].ToString() + " for job " + prodJobNumber + " - " + jobDescription + ".  ";
                            }
                            else if (!wipInput && reader[9] == DBNull.Value)
                            {
                                //This film roll is not allocated to this job
                                command = new SqlCommand("SELECT COUNT(*) FROM [Allocation Master Table] a INNER JOIN [Allocation Reservation Table] b ON a.[Allocation ID] = b.[Allocation ID] WHERE a.[Void Date] IS NULL AND a.[Master Item no] = " + prodMasterItemNumber + " AND b.[Master Item No] = " + reader[0].ToString() + " AND b.[Width] = " + reader[1].ToString(), connection2);
                                connection2.Open();
                                int likeRollsAllocated = (int)command.ExecuteScalar();
                                if (likeRollsAllocated == 0)
                                {
                                    if ((decimal)reader[1] != currentWidth || (int)reader[0] != inputMasterItemNo)
                                    {
                                        answer = MessageBox.Show("Error - roll " + getRollToConsumeForm.UserInput + " " + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + " is not the same film and/or width of any films allocated to job " + prodJobNumber + ".\r\nThe input film for this job is " + inputFilmName + ".\r\nDo you wish to consume this roll anyway (NOT ADVISED)?", "Override? (Requires Supervisor Authorization)", MessageBoxButtons.YesNo);
                                        emailHeader = "Unallocated DIFFERENT Film Roll Consumed";
                                        emailMessage = "On line " + MainForm.MachineNumber.ToString() + " operator " + operatorName + " just consumed unallocated film roll " + getRollToConsumeForm.UserInput + "(" + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + ") for job " + prodJobNumber + " - " + jobDescription + ".  The input film for this job is  " + inputFilmName + ".  ";
                                    }
                                    else
                                    {
                                        answer = MessageBox.Show("Error - roll " + getRollToConsumeForm.UserInput + " " + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + " is the correct film and width that job " + prodJobNumber + " specifies but there is no allocation for this job.\r\nDo you wish to consume this roll anyway?", "Override?", MessageBoxButtons.YesNo);
                                        if (answer == DialogResult.Yes)
                                        {
                                            emailHeader = "Unallocated CORRECT Film Roll Consumed";
                                            emailMessage = "On line " + MainForm.MachineNumber.ToString() + " operator " + operatorName + " just consumed unallocated film roll " + getRollToConsumeForm.UserInput + "(" + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + ") for job " + prodJobNumber + " - " + jobDescription + ".";
                                            // No override required
                                            answer = DialogResult.No;
                                        }
                                    }
                                }
                                else
                                {
                                    command = new SqlCommand("SELECT TOP 1 c.[Reference Item No], c.[Description], b.[Pick Date] FROM[Allocation Pick Table] a INNER JOIN([Allocation Master Table] b INNER JOIN [Inventory Master Table] c ON b.[Master Item No] = c.[Master Item No]) ON a.[Allocation ID] = b.[Allocation ID] WHERE b.[Release Date] IS NULL AND b.[Void Date] IS NULL AND a.[Roll ID] = " + getRollToConsumeForm.UserInput.Substring(1) + " ORDER BY b.[Pick Date] desc", connection2);
                                    SqlDataReader reader2 = command.ExecuteReader();
                                    if (reader2.Read())
                                    {
                                        DialogResult consumeAnyway = MessageBox.Show("Roll " + getRollToConsumeForm.UserInput + " " + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + " is not allocated to this job but is the correct film for this job.  It was last allocated to job " + reader2[0].ToString() + " - " + reader2[1].ToString() + " on " + ((DateTime)reader2[2]).ToShortDateString() + " at " + ((DateTime)reader2[2]).ToShortTimeString() + ".  Do you still wish to consumed?", " Consume Roll?", MessageBoxButtons.YesNo);
                                        if (consumeAnyway == DialogResult.Yes)
                                        {
                                            consumeRoll = true;
                                            ModulesClass.SendEmail(2, "Allocated Film Roll Consumed Under Different Job", "Roll " + getRollToConsumeForm.UserInput + " " + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + " was consumed by job " + prodJobNumber + " - " + jobDescription + ".  It was the correct film but was allocated to job " + reader2[0].ToString() + " - " + reader2[1].ToString() + " on " + ((DateTime)reader2[2]).ToShortDateString() + " at " + ((DateTime)reader2[2]).ToShortTimeString());
                                        }
                                    }
                                    else
                                    {
                                        consumeRoll = true;
                                    }

                                    reader2.Close();
                                }

                                connection2.Close();
                            }
                            else
                            {
                                consumeRoll = true;
                            }

                            if (answer == DialogResult.Yes)
                            {
                                string authorizedBy = string.Empty;
                                bool authorized = ModulesClass.Authorized(false, ref authorizedBy);
                                if (authorized)
                                {
                                    command = new SqlCommand("INSERT INTO [Production Consume Unallocated Roll Exception Table] SELECT " + currentProductionRecord.ToString() + ", " + getRollToConsumeForm.UserInput.Substring(1) + ", GETDATE(), '" + authorizedBy + "'", connection2);
                                    connection2.Open();
                                    command.ExecuteNonQuery();
                                    connection2.Close();
                                    consumeRoll = true;
                                    ModulesClass.SendEmail(2, emailHeader, emailMessage + "The override was authorized by " + authorizedBy + ".");
                                }
                            }

                            if (consumeRoll && reader[10].ToString().Substring(reader[10].ToString().Length - 2, 1) == "2" && (int)reader[11] != 1 && (DateTime)reader[12] > DateTime.Now.AddHours(-12))
                            {
                                consumeRoll = false;
                                answer = MessageBox.Show("WIP Roll " + getRollToConsumeForm.UserInput + " of Job J" + reader[10].ToString() + " - " + reader[2].ToString() + " was created less than 12 hours ago on " + ((DateTime)reader[12]).ToShortDateString() + " at " + ((DateTime)reader[12]).ToShortTimeString() + ".  Do you still wish to consume it anyway (NOT ADVISED)?", "Override? (Requires Supervisor Authorization)", MessageBoxButtons.YesNo);
                                if (answer == DialogResult.Yes)
                                {
                                    string authorizedBy = string.Empty;
                                    bool authorized = ModulesClass.Authorized(false, ref authorizedBy);
                                    if (authorized)
                                    {
                                        consumeRoll = true;
                                        ModulesClass.SendEmail(2, "Possible uncured Lamination Roll Consumed", "On line " + MainForm.MachineNumber.ToString() + " operator " + operatorName + " just consumed film roll " + getRollToConsumeForm.UserInput + "(" + reader[2].ToString() + ") for job " + prodJobNumber + "(" + jobDescription + ") that was created on " + ((DateTime)reader[12]).ToShortDateString() + " at " + ((DateTime)reader[12]).ToShortTimeString() + ",  The override was authorized by " + authorizedBy + ".");
                                    }
                                }
                            }
                        }

                        if (consumeRoll)
                        {
                            if ((int)reader[11] == 1 && reader[6] == DBNull.Value)
                            {
                                GetInputForm getVendorRollIDForm = new GetInputForm("Scan/Input Vendor Roll ID (Hit [Abort] if none)", "*", 0, 0, false);
                                do
                                {
                                    getVendorRollIDForm.ShowDialog();
                                    if (reader.GetBoolean(17))
                                    {
                                        MessageBox.Show("You MUST enter a roll ID on Non-Woven rolls.", "Vendor Roll ID Required");
                                    }
                                }
                                while (reader.GetBoolean(17) && string.IsNullOrEmpty(getVendorRollIDForm.UserInput));

                                if (!string.IsNullOrEmpty(getVendorRollIDForm.UserInput))
                                {
                                    command = new SqlCommand("UPDATE [Roll PO Table] SET [Vendor Roll ID]='" + getVendorRollIDForm.UserInput.Replace("'", "''") + "' WHERE [Roll ID]=" + getRollToConsumeForm.UserInput.Substring(1), connection2);
                                    connection2.Open();
                                    command.ExecuteNonQuery();
                                    connection2.Close();
                                }

                                getVendorRollIDForm.Dispose();
                            }

                            if (newJob)
                            {
                                MessageBox.Show("This is the first roll consumed on this job.  You must have a startup approval.  Please have someone in authority authorize by login.", "New Job");
                                string authorizedBy = string.Empty;
                                bool authorized = ModulesClass.Authorized(true, ref authorizedBy);
                                if (!authorized)
                                {
                                    consumeRoll = false;
                                }
                                else
                                {
                                    if (UPCCodesTable.Rows.Count > 0)
                                    {
                                        consumeRoll = ModulesClass.ValidateUPCCodes(prodJobNumber, getRollToConsumeForm.UserInput, UPCCodesTable, out overrideAuthorizedBy);
                                    }

                                    if (consumeRoll)
                                    {
                                        newJob = false;
                                        command = new SqlCommand("INSERT INTO [Startup Approval Table] SELECT " + currentProductionRecord + ", '" + authorizedBy + "', GETDATE()", connection2);
                                        connection2.Open();
                                        command.ExecuteNonQuery();
                                        connection2.Close();
                                    }
                                }
                            }

                            if (consumeRoll)
                            {
                                if (!string.IsNullOrEmpty(overrideAuthorizedBy))
                                {
                                    if (comboWIP)
                                    {
                                        ModulesClass.SendEmail(2, "Manual UPC Verification", "The UPC codes of input Roll " + getRollToConsumeForm.UserInput + " for combo job " + prodJobNumber + " - " + jobDescription + " on line " + MainForm.MachineNumber + " were manually input.  The override was authorized by " + overrideAuthorizedBy + ".");
                                    }
                                    else
                                    {
                                        ModulesClass.SendEmail(2, "Manual UPC Verification", "The UPC code of input Roll " + getRollToConsumeForm.UserInput + " for job " + prodJobNumber + " - " + jobDescription + " on line " + MainForm.MachineNumber + " was manually input.  The override was authorized by " + overrideAuthorizedBy + ".");
                                    }
                                }

                                currentWidth = (decimal)reader[1];
                                consumedFilmForm.AddRoll(operatorName, DateTime.Now, getRollToConsumeForm.UserInput.Substring(1), "Consumed", "1", currentWidth.ToString("N4") + "\" " + reader[2].ToString(), (int)reader[4]);
                                rtbInputFilm.Text = "Currently Roll " + getRollToConsumeForm.UserInput + " " + currentWidth.ToString("N4") + "\" " + reader[2].ToString() + " @ " + ((int)reader[4]).ToString("N0") + " LF and " + ((int)reader[5]).ToString("N0") + " LBS is being consumed";
                                currentunwindRollNumber = int.Parse(getRollToConsumeForm.UserInput.Substring(1), NumberStyles.Number);
                                consumedFeet += (int)reader[4];
                                currentInputLF = (int)reader[4];
                                txtConsumedFeet.Text = consumedFeet.ToString("N0");
                                command = new SqlCommand("INSERT INTO [Production Consumed Roll Table] SELECT " + currentProductionRecord.ToString() + ", 1, " + getRollToConsumeForm.UserInput.Substring(1) + ", '" + DateTime.Now.ToString() + "', " + reader[4].ToString() + ", NULL, NULL, NULL", connection2);
                                connection2.Open();
                                command.ExecuteNonQuery();
                                command = new SqlCommand("SELECT [Comment] FROM [Roll Comment Table] WHERE [Roll ID] = " + getRollToConsumeForm.UserInput.Substring(1), connection2);
                                string comment = (string)command.ExecuteScalar();
                                connection2.Close();
                                if (!string.IsNullOrEmpty(comment))
                                {
                                    MessageBox.Show(comment, "Comment for Roll " + getRollToConsumeForm.UserInput);
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Error - roll " + getRollToConsumeForm.UserInput + " not found", "Roll Doesn't Exist");
                }

                reader.Close();
                connection1.Close();
            }

            getRollToConsumeForm.Dispose();
        }
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void CmdReturnRollClick(object sender, EventArgs e)
		{
            GetInputForm getRollToReturnForm = new GetInputForm("Scan/Input Roll to Return Barcode", "R", 0, 0, true);
            getRollToReturnForm.ShowDialog();
            if (getRollToReturnForm.UserInput.Length > 0)
            {
                command = new SqlCommand("SELECT TOP 1 b.[Master Item No], b.[Width], c.[Description], d.[Machine No], CAST(ROUND(a.[Start Usage LF], 0) AS int), CAST(ISNULL(ROUND(a.[End Usage LF], 0), 0) AS int), a.[Start Usage Date], a.[Start Production ID], e.[Reference Item No],	e.[Description] FROM [Production Consumed Roll Table] a INNER JOIN ([Roll Table] b INNER JOIN [Inventory Master Table] c ON b.[Master Item No] = c.[Master Item No]) ON a.[Roll ID] = b.[Roll ID] INNER JOIN ([Production Master Table] d INNER JOIN [Inventory Master Table] e ON d.[Master Item No] = e.[Master Item No]) ON a.[Start Production ID] = d.[Production ID] WHERE a.[Start Usage LF] != ISNULL(a.[End Usage LF], 0) AND (a.[Start Production ID] = " + currentProductionRecord.ToString() + " OR (d.[Machine No] = " + MainForm.MachineNumber + " AND a.[Start Usage Date] > DATEADD(hh, -12, GETDATE()) AND a.[End Usage Date] IS NULL)) AND a.[Roll ID] = " + getRollToReturnForm.UserInput.Substring(1) + " ORDER BY a.[Start Usage Date] DESC", connection1);
                connection1.Open();
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    GetInputForm returnLFForm = new GetInputForm("Return LF", "#", 1, (int)reader[4], false);
                    returnLFForm.ShowDialog();
                    if (returnLFForm.UserInput.Length > 0)
                    {
                        DialogResult answer = DialogResult.Yes;
                        if ((int)reader[5] != 0)
                        {
                            // There is already a return recorded
                            if ((int)reader[5] > int.Parse(returnLFForm.UserInput, NumberStyles.Number))
                            {
                                MessageBox.Show("You last returned roll " + getRollToReturnForm.UserInput + " " + reader[2].ToString() + " for " + ((int)reader[5]).ToString("N0") + " LF.  You need to re-consume the roll then return " + int.Parse(returnLFForm.UserInput, NumberStyles.Number).ToString("N0") + " LF IF the roll hasn't already been consumed by another job.", "Can't decrease a Return's Amount");
                                answer = DialogResult.No;
                            }
                            else
                            {
                                answer = MessageBox.Show("Roll " + getRollToReturnForm.UserInput + " " + reader[2].ToString() + " has already been returned for " + ((int)reader[5]).ToString("N0") + " LF.  Do you wish to overwrite the return?", "Change Return Amount?", MessageBoxButtons.YesNo);
                            }
                        }
                        else if ((int)reader[7] != currentProductionRecord)
                        {
                            if (reader[8].ToString() == prodJobNumber)
                            {
                                answer = MessageBox.Show("Roll " + getRollToReturnForm.UserInput + " was consumed by this job but under a different production record on " + ((DateTime)reader[6]).ToShortDateString() + " at " + ((DateTime)reader[6]).ToShortTimeString() + ".  Do you still wish to return (the return will not affect usage on the current production record)?", "Still Return Roll?", MessageBoxButtons.YesNo);
                            }
                            else
                            {
                                answer = MessageBox.Show("Roll " + getRollToReturnForm.UserInput + " was consumed by job " + reader[8].ToString() + " - " + reader[9].ToString() + " on " + ((DateTime)reader[6]).ToShortDateString() + " at " + ((DateTime)reader[6]).ToShortTimeString() + ".  Do you still with to return (the return will not affect usage on the current production record)?", "Still Return Roll?", MessageBoxButtons.YesNo);
                            }
                        }

                        if (answer == DialogResult.Yes)
                        {
                            if ((int)reader[7] == currentProductionRecord)
                            {
                                int maximumLF = consumedFeet + (int)reader[5] - createdFeet + 3000;
                                int maximumLFNoTolerance = consumedFeet + (int)reader[5] - createdFeet;
                                if (int.Parse(returnLFForm.UserInput, NumberStyles.Number) > maximumLF)
                                {
                                    answer = MessageBox.Show("The calculated maximum LF you can return is " + maximumLF.ToString("N0") + ".  Do you still wish to return " + int.Parse(returnLFForm.UserInput, NumberStyles.Number).ToString("N0") + " LF?", "Override Maximum Return Amount?", MessageBoxButtons.YesNo);
                                    if (answer == DialogResult.Yes)
                                    {
                                        ModulesClass.SendEmail(1, "Return Roll Exception", "On line " + MainForm.MachineNumber.ToString() + " operator " + operatorName + " just returned roll " + getRollToReturnForm.UserInput + " for job " + prodJobNumber + "(" + jobDescription + ") in the amount of " + int.Parse(returnLFForm.UserInput, NumberStyles.Number).ToString("N0") + " LF when the LF available to return was " + maximumLFNoTolerance.ToString("N0") + ".");
                                    }
                                }

                                if (answer == DialogResult.Yes)
                                {
                                    consumedFeet -= int.Parse(returnLFForm.UserInput, NumberStyles.Number);
                                    currentInputLF = 0;
                                    txtConsumedFeet.Text = consumedFeet.ToString("N0");
                                    rtbInputFilm.Text = string.Empty;
                                    currentWidth = 0;
                                    unwindRollNumber = 0;
                                }

                                if (int.Parse(returnLFForm.UserInput, NumberStyles.Number) == maximumLF || (int)reader[5] != 0)
                                {
                                    consumedFilmForm.ClearGrid();
                                    FillConsumedFilmGrid();
                                }
                                else
                                {
                                    consumedFilmForm.AddRoll(operatorName, DateTime.Now, getRollToReturnForm.UserInput.Substring(1), "Returned", "1", ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString(), int.Parse(returnLFForm.UserInput, NumberStyles.Number));
                                }
                            }
                            else
                            {
                                ModulesClass.SendEmail(1, "Roll Returned after Production Record " + reader[7].ToString() + " closed", "On line " + MainForm.MachineNumber.ToString() + " operator " + operatorName + " just returned roll " + getRollToReturnForm.UserInput + " for job " + reader[8].ToString() + " - " + reader[9].ToString() + " in the amount of " + int.Parse(returnLFForm.UserInput, NumberStyles.Number).ToString("N0") + " LF.");
                            }

                            if (answer == DialogResult.Yes)
                            {
                                command = new SqlCommand("UPDATE [Production Consumed Roll Table] SET [End Production ID] = " + reader[7].ToString() + ", [End Usage Date] = '" + DateTime.Now.ToString() + "', [End Usage LF] = " + returnLFForm.UserInput + " WHERE [Roll ID] = " + getRollToReturnForm.UserInput.Substring(1) + " AND [Start Usage Date] = '" + ((DateTime)reader[6]).ToString() + "' AND [Start Production ID] = " + reader[7].ToString(), connection2);
                                connection2.Open();
                                command.ExecuteNonQuery();
                                connection2.Close();
                                PrintClass.Label(getRollToReturnForm.UserInput);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Roll return aborted");
                        }

                        returnLFForm.Dispose();
                    }
                }
                else
                {
                    MessageBox.Show("Error - Roll " + getRollToReturnForm.UserInput + " has not been consumed on this line within the last 12 hours.", "Roll Not Found");
                }

                reader.Close();
                connection1.Close();
            }

            getRollToReturnForm.Dispose();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		private void CmdJobCompleteClick(object sender, EventArgs e)
		{
			if (ModulesClass.ValidSaveDate(startTime.AddHours(double.Parse(txtTotalHours.Text, NumberStyles.Number)), endOfShiftTime))
			{
				DialogResult result = DialogResult.Yes;
				
				if (wipInput)
				{
					command = new SqlCommand("SELECT a.[Roll ID], b.[Description] FROM [Roll Table] a INNER JOIN [Location Table] b ON a.[Location ID] = b.[Location ID] WHERE b.[Inventory Available] = 1 AND [Current LF] > 0 AND [Master Item No] = " + inputMasterItemNo, connection1);
					connection1.Open();
					reader = command.ExecuteReader();
					if (reader.Read())
					{
						MessageBox.Show("Error - you cannot complete this job.  WIP Roll R" + reader[0].ToString() + " is still in inventory in location + " + reader[1].ToString() + ". This roll must be consumed in order to mark the job complete.", "Not all input WIP rolls used");
						result = DialogResult.No;						
					}
					
					reader.Close();
					connection1.Close();
				}
				
				if (result == DialogResult.Yes)
				{
					command = new SqlCommand("SELECT [Production ID], [Machine No], DATEADD(minute, ([Setup Hrs] + [Run Hrs] + [DT Hrs]) * 60, [Start Time]) FROM [Production Master Table] WHERE [End Reason ID] = 1 AND [Master Item No] = " + prodMasterItemNumber, connection1);
					connection1.Open();
					reader = command.ExecuteReader();
					if (reader.Read())
					{
						if (startTime.AddHours(double.Parse(txtTotalHours.Text, NumberStyles.Number)) < (DateTime)reader[2])
						{
							MessageBox.Show("Error - there is an end of job record for this job on machine " + reader[1].ToString() + " at a later date (" + ((DateTime)reader[2]).ToString("MMMM d, yyyy h:mm tt") + ").  Save aborted.", "Save Aborted");
							result = DialogResult.No;
						}
						else
						{
							result = MessageBox.Show("There already is an end of job record on machine " + reader[1].ToString() + " (" + ((DateTime)reader[2]).ToString("MMMM d, yyyy h:mm tt") + ").  Do you wish to change it?", "Change End Time", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                connection2.Open();
                                command = new SqlCommand("UPDATE [Production Master Table] SET [End Reason ID] = 3 WHERE [Production ID] = " + reader[0].ToString(), connection2);
                                command.ExecuteNonQuery();
                                connection2.Close();
                            }
                            else
							{
								MessageBox.Show("Save Aborted.");
							}
						}
					}

					reader.Close();
					if (result == DialogResult.Yes)
                    {
                        command = new SqlCommand("SELECT d.[Standard Production Master Linear Feet], ISNULL(SUM(c.[Original LF] / d.[No Streams]), 0) FROM [Production Master Table] a LEFT JOIN ([Production Roll Table] b INNER JOIN [Roll Table] c ON b.[Roll ID] = c.[Roll ID]) ON a.[Production ID] = b.[Production ID] INNER JOIN [Slitting Master Job Standards View] d ON a.[Master Item No]= d.[Master Item No] WHERE a.[Master Item No] = " + prodMasterItemNumber + " GROUP BY d.[Standard Production Master Linear Feet]", connection1);
                        reader = command.ExecuteReader();
                        reader.Read();
                        if ((decimal)reader[0] * (decimal).95 > (decimal)reader[1])
                        {
                            result = MessageBox.Show("You have only produced " + ((decimal)reader[1]).ToString("N0") + " production LF of the " + ((decimal)reader[0]).ToString("N0") + " production LF expected for this job.  Do you still wish to mark the job complete?", "Job run Short", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                string authorizedBy = string.Empty;
                                bool authorized = ModulesClass.Authorized(false, ref authorizedBy);
                                if (authorized)
                                {
                                    result = DialogResult.Yes;
                                    ModulesClass.SendEmail(2, "Job " + prodJobNumber + " (" + jobDescription + ")" + " was run short", "On line " + MainForm.MachineNumber.ToString() + " operator " + operatorName + " completed the job with " + ((decimal)reader[1]).ToString("N0") + " production LF of the " + ((decimal)reader[0]).ToString("N0") + " production LF expected.  The override was authorized by " + authorizedBy + ".");
                                }
                                else
                                {
                                    result = DialogResult.No;
                                }
                            }
                        }

                        reader.Close();
                        connection1.Close();
                        if (result == DialogResult.Yes)
                        {
                             WriteProductionRecord(startTime.AddHours(double.Parse(txtTotalHours.Text, NumberStyles.Number)), "1");
                        }
                    }
                    else
                    {
                        connection1.Close();
                    }
                }
			}
		}
			
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void CmdJobPulledClick(object sender, EventArgs e)
		{
			if (ModulesClass.ValidSaveDate(startTime.AddHours(double.Parse(txtTotalHours.Text, NumberStyles.Number)), endOfShiftTime))
			{
				OptionsForm reasonPulledOptionsForm = new OptionsForm("Reason for Pulling Job", false, true);
				command = new SqlCommand("select substring([Description],10,len([Description])-9) from [Save Production Reason Table] where substring([Description],1,9) = 'Pulled - ' order by [End Reason ID]", connection1);
				connection1.Open();
				reader = command.ExecuteReader();
				while (reader.Read())
				{
					reasonPulledOptionsForm.AddOption(reader[0].ToString());
				}
				
				reader.Close();
				reasonPulledOptionsForm.ShowDialog();
				if (reasonPulledOptionsForm.Option == "Abort")
				{
                    reasonPulledOptionsForm.Dispose();
                    connection1.Close();
                    reasonPulledOptionsForm.Dispose();
                    MessageBox.Show("Save aborted", "Save Aborted");
				}
				else
				{
                    reasonPulledOptionsForm.Dispose();
                    command = new SqlCommand("select cast([End Reason ID] as int) from [Save Production Reason Table] where [Description]='Pulled - " + reasonPulledOptionsForm.Option + "'", connection1);
                    int reasonCode = (int)command.ExecuteScalar();
                    connection1.Close();
                    WriteProductionRecord(startTime.AddHours(double.Parse(txtTotalHours.Text, NumberStyles.Number)), reasonCode.ToString());
				}
            }
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void CmdEndofShiftClick(object sender, EventArgs e)
		{
			if (ModulesClass.ValidSaveDate(startTime.AddHours(double.Parse(txtTotalHours.Text, NumberStyles.Number)), endOfShiftTime))
			{
				WriteProductionRecord(startTime.AddHours(double.Parse(txtTotalHours.Text, NumberStyles.Number)), "2");
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void CmdSaveAndCloseClick(object sender, EventArgs e)
		{
			if (ModulesClass.ValidSaveDate(startTime.AddHours(double.Parse(txtTotalHours.Text, NumberStyles.Number)), endOfShiftTime))
			{
				WriteProductionRecord(startTime.AddHours(double.Parse(txtTotalHours.Text, NumberStyles.Number)), "3");
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void WriteProductionRecord(DateTime saveTime, string reasonCode)
		{
            if (createdFeet > 0 && decimal.Parse(txtRunHours.Text, NumberStyles.Number) == 0)
            {
                MessageBox.Show("Error - you must enter at least 0.25 run hours since you have production.", "Invalid Run Hours");
            }
            else
            {
                int endOutputLF = 0;

                if (reasonCode == "2")
                {
                    // End of shift so check for partially produced roll
                    int unwindEndLF = 0;

                    GetInputForm partialRollLFForm = new GetInputForm("In Process Roll LF", "#", 0, consumedFeet - createdFeet + 100000, false);
                    DialogResult answer = DialogResult.No;
                    if (decimal.Parse(txtRunHours.Text, NumberStyles.Number) > 0)
                    {
                        while (answer == DialogResult.No)
                        {
                            partialRollLFForm.ShowDialog();
                            if (partialRollLFForm.UserInput.Length > 0)
                            {
                                if (int.Parse(partialRollLFForm.UserInput, NumberStyles.Number) > consumedFeet - createdFeet + 3000)
                                {
                                    answer = MessageBox.Show("The calculated maximum LF you can create is " + (consumedFeet - createdFeet + 3000).ToString("N0") + ".  Do you still wish to create a partial roll @ " + int.Parse(partialRollLFForm.UserInput, NumberStyles.Number).ToString("N0") + " LF?", "Override Maximum LF for Partial Roll?", MessageBoxButtons.YesNo);
                                }
                                else
                                {
                                    answer = DialogResult.Yes;
                                }

                                if (answer == DialogResult.Yes)
                                {
                                    endOutputLF = int.Parse(partialRollLFForm.UserInput, NumberStyles.Number);
                                }
                            }
                            else
                            {
                                answer = DialogResult.Yes;
                            }
                        }
                    }

                    if (currentInputLF > 0)
                    {
                        partialRollLFForm.NewTitle = "Unwind LF";
                        partialRollLFForm.UserInput = "0";
                        partialRollLFForm.MaxValue = currentInputLF;
                        partialRollLFForm.ShowDialog();
                        if (!string.IsNullOrEmpty(partialRollLFForm.UserInput) && int.Parse(partialRollLFForm.UserInput, NumberStyles.Number) > 0)
                        {
                            command = new SqlCommand("execute [dbo].[Save Shift Change Unwind Footage Stored Procedure] " + currentProductionRecord + ", 1, 1, " + currentunwindRollNumber.ToString() + ", " + partialRollLFForm.UserInput, connection1);
                            connection1.Open();
                            command.ExecuteNonQuery();
                            connection1.Close();
                            unwindEndLF = int.Parse(partialRollLFForm.UserInput, NumberStyles.Number);
                        }
                    }

                    partialRollLFForm.Dispose();
                }


                JobToDateStatsForm productionSummaryForm = new JobToDateStatsForm(currentProductionRecord.ToString(), false, endOutputLF.ToString(), txtSetupHours.Text, txtRunHours.Text, txtDownTimeHours.Text);
                productionSummaryForm.ShowDialog();
                if (productionSummaryForm.Save)
                {
                    productionSummaryForm.Dispose();
                    productionNotesForm.Hide();
                    productionNotesForm.WindowState = FormWindowState.Normal;
                    productionNotesForm.ShowDialog();
                    if (productionNotesForm.Comment.Length > 0)
                    {
                        command = new SqlCommand("UPDATE [Production Master Table] SET [Setup Hrs] = " + txtSetupHours.Text + ", [Run Hrs] = " + txtRunHours.Text + ", [DT Hrs] = " + txtDownTimeHours.Text + ", [End Reason ID] = " + reasonCode + ", [End Output LF] = " + endOutputLF.ToString() + ", [Notes] = '" + productionNotesForm.Comment.Replace("'", "''") + "' WHERE [Production ID] = " + currentProductionRecord.ToString(), connection1);
                    }
                    else
                    {
                        command = new SqlCommand("UPDATE [Production Master Table] SET [Setup Hrs] = " + txtSetupHours.Text + ", [Run Hrs] = " + txtRunHours.Text + ", [DT Hrs] = " + txtDownTimeHours.Text + ", [End Reason ID] = " + reasonCode + ", [End Output LF] = " + endOutputLF.ToString() + ", [Notes] = NULL WHERE [Production ID] = " + currentProductionRecord.ToString(), connection1);
                    }

                    productionNotesForm.Dispose();
                    connection1.Open();
                    command.ExecuteNonQuery();
                    if (reasonCode == "1")
                    {
                        command = new SqlCommand("UPDATE [Allocation Master Table] SET [Released By] = '" + StartupForm.UserName + "', [Release Date] = GETDATE() WHERE [Pick Date] IS NOT NULL AND [Release Date] IS NULL AND [Void Date] IS NULL AND [Master Item No] = " + prodMasterItemNumber, connection1);
                        command.ExecuteNonQuery();
                    }

                    connection1.Close();
                    this.FormClosing -= SlittingProductionFormFormClosing;
                    this.Close();
                }
                else
                {
                    productionSummaryForm.Dispose();
                }
            }
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void CmdCreateSlitSetClick(object sender, EventArgs e)
		{
			DialogResult answer;
			
			if (!string.IsNullOrEmpty(rtbLooseSlitRolls.Text))
			{
				MessageBox.Show("You cannot create a new slit set with loose rolls at the line.  Please move or palletize the  loose rolls listed.", "Loose Rolls Exist");
			}
			else if (currentunwindRollNumber == 0)
			{
				MessageBox.Show("You cannot create a new slit set without an input roll.  Please scan in an input roll.", "No Input Roll");
			}
			else if (cmdGetPallet.Enabled)
			{
				if (comboWIP)
				{
					MessageBox.Show("One or more Jobs do not have pallets assigned.  Please click the \"Get Pallet(s)\" button and create or enter the missing pallet number(s)r", "Missing Pallet(s)");
				}
				else
				{
					MessageBox.Show("This job does not have a pallet assigned.  Please click the \"Get Pallet\" button and create or enter a pallet number", "Missing Pallet");
				}
			}
			else
			{
				bool goodData = false;
				answer = DialogResult.No;
					
				if (defaultSlitRollFeet != 0)
				{
					answer = MessageBox.Show("Current LF per roll is " + defaultSlitRollFeet.ToString("N0") + ".  Do you wish to use the same roll footage and pounds?", "Use Same Footage and Pounds?", MessageBoxButtons.YesNo);
				}
					
				if (answer == DialogResult.No)
				{
					GetInputForm getNewSlitRollLFForm = new GetInputForm("Enter New Roll Footage", "#", 0, 999999, false);
					getNewSlitRollLFForm.ShowDialog();
					if (getNewSlitRollLFForm.UserInput.Length > 0 && int.Parse(getNewSlitRollLFForm.UserInput, NumberStyles.Number) > 0)
					{
						defaultSlitRollFeet = int.Parse(getNewSlitRollLFForm.UserInput, NumberStyles.Number);
						getNewSlitRollLFForm.Dispose();
						if (laneJobInformationTable.Rows.Count > 1)
						{
							answer = MessageBox.Show("Do you wish to use the same roll weight across all lanes?", "All rolls weigh the same?", MessageBoxButtons.YesNo);
						}
						else
						{
							answer = DialogResult.Yes;
						}
						
						if (answer == DialogResult.Yes)
						{
							GetDecimalInputForm getNewSlitRollPoundsForm = new GetDecimalInputForm(2);
							if (laneJobInformationTable.Rows.Count == 1)
							{
								getNewSlitRollPoundsForm.Description = "Enter Roll Weight";
							}
							else
							{
								getNewSlitRollPoundsForm.Description = "Enter Roll Weight for All Lanes";
							}
								
							getNewSlitRollPoundsForm.ShowDialog();
							if (getNewSlitRollPoundsForm.UserInput.Length > 0)
							{
								for (int i = 0; i < laneJobInformationTable.Rows.Count; i++)
								{
									laneJobInformationTable.Rows[i]["DefaultPounds"] = decimal.Parse(getNewSlitRollPoundsForm.UserInput, NumberStyles.Number);
								}
								
								goodData = true;
							}
									
							getNewSlitRollPoundsForm.Dispose();
						}
						else
						{
							bool allGoodLaneData = true;
							GetDecimalInputForm getNewSlitRollPoundsForm = new GetDecimalInputForm(2);
							for (int i = 1; i <= laneJobInformationTable.Rows.Count; i++)
							{
								getNewSlitRollPoundsForm.Description = "Enter Lane " + ConvertClass.GetLaneName(i) + " Pounds";
								getNewSlitRollPoundsForm.ShowDialog();
								if (getNewSlitRollPoundsForm.UserInput.Length > 0)
								{
									for (int j = 0; j < laneJobInformationTable.Rows.Count; j++)
									{
										if ((int)laneJobInformationTable.Rows[j]["Lane"] == i)
										{
											laneJobInformationTable.Rows[j]["DefaultPounds"] = decimal.Parse(getNewSlitRollPoundsForm.UserInput, NumberStyles.Number);
											break;
										}
									}
								}
								else
								{
									allGoodLaneData = false;
									getNewSlitRollPoundsForm.Dispose();
									break;
								}
							}
								
							if (allGoodLaneData)
							{
								getNewSlitRollPoundsForm.Dispose();
								goodData = true;
							}
						}
					}
					else
					{
						getNewSlitRollLFForm.Dispose();
						return;
					}
				}
				else
				{
					goodData = true;
				}
				
				if (goodData)
				{
					command = new SqlCommand("SELECT COUNT(*) FROM [Production Roll Table] WHERE [Set No] IS NOT NULL AND [Input Roll ID 1] = " + currentunwindRollNumber, connection1);
					connection1.Open();
					int recordCount = (int)command.ExecuteScalar();
					if (recordCount == 0)
					{
						setNumber = 1;
					}
					else
					{
						command = new SqlCommand("SELECT MAX([Set No]) + 1 FROM [Production Roll Table] WHERE [Set No] IS NOT NULL AND ISNULL([Input Roll ID 1], 0) = " + currentunwindRollNumber, connection1);
						setNumber = (int)command.ExecuteScalar();
					}
					
					connection1.Close();
					answer = MessageBox.Show("Do you wish to create slit set R" + currentunwindRollNumber.ToString() + "-" + setNumber.ToString() + " @ " + defaultSlitRollFeet.ToString("N0") + " LF?", "Create Slit Set?", MessageBoxButtons.YesNo);
					if (answer == DialogResult.Yes)
					{
                        bool goodFeet = true;

						if (createdFeet + defaultSlitRollFeet > consumedFeet + 3000)
						{
							answer = MessageBox.Show("The calculated maximum linear feet you can create is " + (consumedFeet - createdFeet + 3000).ToString("N0") + ".  Do you still wish to create a slit set at " + defaultSlitRollFeet.ToString("N0") + " LF?", "Override Maximum Linear Feet? (REQUIRES SUPERVISOR APPROVAL)", MessageBoxButtons.YesNo);
							if (answer == DialogResult.Yes)
							{
                                string authorizedBy = string.Empty;
                                bool authorized = ModulesClass.Authorized(false, ref authorizedBy);
                                if (!authorized)
                                {
                                    goodFeet = false;
								}
							}
                        }
						
						if (answer == DialogResult.Yes && goodFeet)
						{
                            bool inputRollOK = true;
                            string overrideAuthorizedBy = string.Empty;
                            if (UPCCodesTable.Rows.Count > 0)
                            {
                                inputRollOK = ModulesClass.ValidateUPCCodes(prodJobNumber, string.Empty, UPCCodesTable, out overrideAuthorizedBy);
                            }

                            if (inputRollOK)
                            {
                                decimal totalPounds = 0;
                                DateTime createDate = DateTime.Now;
                                connection1.Open();
                                Boolean firstRoll = true;
                                for (int i = 0; i < laneJobInformationTable.Rows.Count; i++)
                                {
                                    totalPounds += (decimal)laneJobInformationTable.Rows[i]["DefaultPounds"];
                                    command = new SqlCommand("EXECUTE [Create Production Roll Stored Procedure] " + currentProductionRecord.ToString() + ", " + currentunwindRollNumber.ToString() + ", NULL, " + setNumber.ToString() + ", " + laneJobInformationTable.Rows[i]["Lane"].ToString() + ", NULL, " + laneJobInformationTable.Rows[i]["prodMasterItemNumber"].ToString() + ", '" + StartupForm.UserName + "', " + defaultSlitRollFeet.ToString() + ", " + laneJobInformationTable.Rows[i]["DefaultPounds"].ToString() + ", " + laneJobInformationTable.Rows[i]["SlitWidth"].ToString() + ", " + MainForm.MachineNumber + ", '" + createDate.ToString() + "'", connection1);
                                    unwindRollNumber = (int)command.ExecuteScalar();
                                    if (createdFeet + defaultSlitRollFeet > consumedFeet + 3000 && firstRoll)
                                    {
                                        command = new SqlCommand("INSERT INTO [Production Roll Exception Table] SELECT " + unwindRollNumber.ToString() + ", " + (consumedFeet + 3000 - createdFeet).ToString(), connection1);
                                        command.ExecuteNonQuery();
                                        ModulesClass.SendEmail(1, "Create Roll Exception", "On line " + MainForm.MachineNumber.ToString() + " operator " + operatorName + " just created slit set R" + currentunwindRollNumber.ToString() + "-" + setNumber.ToString() + " whose first roll is R" + unwindRollNumber.ToString() + " for job " + prodJobNumber + "(" + jobDescription + ") in the amount of " + defaultSlitRollFeet.ToString("N0") + " linear feet when the linear feet available was " + (consumedFeet - createdFeet).ToString("N0") + ".");
                                    }

                                    if (currentunwindRollNumber != 0)
                                    {
                                        PrintClass.Label("R" + unwindRollNumber.ToString());
                                    }
                                    else
                                    {
                                        MessageBox.Show("Error - with no input roll core labels cannot be printed", "No Input Roll Defined");
                                    }

                                    if (firstRoll && !string.IsNullOrEmpty(overrideAuthorizedBy))
                                    {
                                        if (comboWIP)
                                        {
                                            ModulesClass.SendEmail(2, "Manual UPC Verification", "The UPC codes of Created Slit Roll " + unwindRollNumber.ToString() + " and possibly other rolls of its slit set for combo job " + prodJobNumber + " - " + jobDescription + " on line " + MainForm.MachineNumber + " were manually input.  The override was authorized by " + overrideAuthorizedBy + ".");
                                        }
                                        else
                                        {
                                            ModulesClass.SendEmail(2, "Manual UPC Verification", "The UPC code of Created Slit Roll " + unwindRollNumber.ToString() + " and the rest of its set for job " + prodJobNumber + " - " + jobDescription + " on line " + MainForm.MachineNumber + " was manually input.  The override was authorized by " + overrideAuthorizedBy + ".");
                                        }
                                    }

                                    firstRoll = false;
                                }

                                FillLooseRollText();
                                connection1.Close();
                                createdFeet += defaultSlitRollFeet;
                                txtCreatedFeet.Text = createdFeet.ToString("N0");
                                cmdClosePallet.Enabled = true;
                            }
						}
					}
				}
				else
				{
					defaultSlitRollFeet = 0;
				}
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		private void CmdMoveRollClick(object sender, EventArgs e)
		{
            DialogResult answer = ModulesClass.GetItemToMove(MainForm.MachineNumber);
            if (answer == DialogResult.Yes)
            {
                connection1.Open();
                FillSlitSetText();
                FillLooseRollText();
                connection1.Close();
            }
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		private void CmdClosePalletClick(object sender, EventArgs e)
		{
			GetInputForm readBarcodeForm = new GetInputForm("Scan/Input Barcode", "P", 0, 0, true);
			readBarcodeForm.ShowDialog();
			if (readBarcodeForm.UserInput.Length > 0)
			{
				command = new SqlCommand("SELECT CAST(d.[Reference Item No] as nvarchar(10)) + ' - ' + d.[Description], COUNT(*), SUM(a.[Original Pounds] * a.[Current LF] / a.[Original LF]) + b.[Blank Weight] FROM [Roll Table] a INNER JOIN [Current Pallets at Machine Table] b on a.[Pallet ID] = b.[Pallet ID] INNER JOIN [Pallet Table] c on a.[Pallet ID] = c.[Pallet ID] INNER JOIN [Inventory Master Table] d on a.[Master Item No] = d.[Master Item No] WHERE a.[Pallet ID] = " + readBarcodeForm.UserInput.Substring(1) + " AND c.[Location ID] = " + MainForm.MachineNumber + " GROUP BY CAST(d.[Reference Item No] as nvarchar(10)) + ' - ' + d.[Description], b.[Blank Weight]", connection1);
				connection1.Open();
				reader = command.ExecuteReader();
				if (reader.Read())
				{
					DialogResult answer;
					if ((int)reader[1] == 1)
					{
						answer = MessageBox.Show("There is 1 roll available for Job " + reader[0].ToString() + ".  Is this the number or rolls you wish to palletize?", "Confirm Number of Rolls", MessageBoxButtons.YesNo);
					}
					else
					{
						answer = MessageBox.Show("There are " + ((int)reader[1]).ToString("N0") + " rolls available for Job " + reader[0].ToString() + ".  Is this the number or rolls you wish to palletize?", "Confirm Number of Rolls", MessageBoxButtons.YesNo);
					}
					
					if (answer == DialogResult.Yes)
					{
						command = new SqlCommand("UPDATE [Pallet Table] SET [Weight] = " + (decimal)reader[2] + " where [Pallet ID] = " + readBarcodeForm.UserInput.Substring(1), connection1);
						reader.Close();
						command.ExecuteNonQuery();
						command = new SqlCommand("DELETE FROM [Current Pallets at Machine Table] WHERE [Pallet ID] = " + readBarcodeForm.UserInput.Substring(1), connection1);
						command.ExecuteNonQuery();
   						RefreshDgvJobs();
						FillSlitSetText();
					}
					else
					{
						reader.Close();
					}
				}
				else
				{
					reader.Close();
					MessageBox.Show("Pallet " + readBarcodeForm.UserInput + " not found at " + MainForm.MachineNumber + ".", "Invalid Pallet");
				}

				connection1.Close();
			}
			
			readBarcodeForm.Close();
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void FillSlitSetText()
		{
			rtbCreatedWIPInfo.Text = string.Empty;
			rtbCurrentPalletProduction.Text = string.Empty;
            command = new SqlCommand("SELECT 'P' + CAST(c.[Pallet ID] AS nvarchar(10)) + ' @ ' + f.[Description] + ' (Job ' + CAST(d.[Reference Item No] AS nvarchar(10)) + ')', COUNT(c.[Roll ID]), SUM(c.[Original LF]), SUM(c.[Original Pounds]) FROM [Production Roll Table] a INNER JOIN [Production Master Table] b ON a.[Production ID] = b.[Production ID] INNER JOIN ([Roll Table] c INNER JOIN[Inventory Master Table] d ON c.[Master Item No] = d.[Master Item No] INNER JOIN ([Pallet Table] e INNER JOIN [Location Table] f ON e.[Location ID] = f.[Location ID] LEFT JOIN [Current Pallets at Machine Table] g ON e.[Pallet ID] = g.[Pallet ID]) ON c.[Pallet ID] = e.[Pallet ID]) ON a.[Roll ID] = c.[Roll ID] WHERE (a.[Production ID] = " + currentProductionRecord.ToString() + " OR (e.[Location ID] = " + MainForm.MachineNumber + " AND b.[Master Item No] = " + prodMasterItemNumber + ")) AND c.[Original LF] != 0 AND g.[Pallet ID] IS NULL GROUP BY 'P' + CAST(c.[Pallet ID] AS nvarchar(10)) + ' @ ' + f.[Description] + ' (Job ' + CAST(d.[Reference Item No] AS nvarchar(10)) + ')' ORDER BY 'P' + CAST(c.[Pallet ID] AS nvarchar(10)) + ' @ ' + f.[Description] + ' (Job ' + CAST(d.[Reference Item No] AS nvarchar(10)) + ')'", connection1);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                if ((int)reader[1] == 1)
                {
                    rtbCreatedWIPInfo.Text += reader[0].ToString() + " - 1 roll @ " + ((decimal)reader[2]).ToString("N0") + " LF and " + ((decimal)reader[3]).ToString("N0") + " LBS\r\n";
                }
                else
                {
                    rtbCreatedWIPInfo.Text += reader[0].ToString() + " - " + ((int)reader[1]).ToString("N0") + " rolls @ " + ((decimal)reader[2]).ToString("N0") + " LF and " + ((decimal)reader[3]).ToString("N0") + " LBS\r\n";
                }
            }

            reader.Close();

            if (string.IsNullOrEmpty(currentPalletList))
            {
                lblCurrentPalletProduction.Text = string.Empty;
                cmdClosePallet.Enabled = false;
            }
			else
			{
				cmdClosePallet.Enabled = true;
				command = new SqlCommand("SELECT 'P' + CAST(a.[Pallet ID] AS nvarchar(10)) + ' (Job ' + CAST(f.[Reference Item No] AS nvarchar(10)) + ')', COUNT(c.[Roll ID]), c.[Original LF], c.[Original Pounds] FROM [Current Pallets at Machine Table] a INNER JOIN [Pallet Table] b ON a.[Pallet ID] = b.[Pallet ID] LEFT JOIN ([Roll Table] c INNER JOIN ([Production Roll Table] d INNER JOIN [Production Master Table] e ON d.[Production ID] = e.[Production ID]) ON c.[Roll ID] = d.[Roll ID] INNER JOIN [Inventory Master Table] f ON c.[Master Item No] = f.[Master Item No]) ON a.[Pallet ID] = c.[Pallet ID] WHERE b.[Location ID] = " + MainForm.MachineNumber + " AND e.[Master Item No] = " + prodMasterItemNumber + " GROUP BY 'P' + CAST(a.[Pallet ID] AS nvarchar(10)) + ' (Job ' + CAST(f.[Reference Item No] AS nvarchar(10)) + ')', c.[Original LF], c.[Original Pounds] ORDER BY 'P' + CAST(a.[Pallet ID] AS nvarchar(10)) + ' (Job ' + CAST(f.[Reference Item No] AS nvarchar(10)) + ')', c.[Original LF], c.[Original Pounds] DESC", connection1);
				reader = command.ExecuteReader();
				while (reader.Read())
				{
					if ((int)reader[1] == 0)
                    {
                        rtbCurrentPalletProduction.Text += reader[0].ToString() + " - no rolls placed on pallet yet\r\n";
                    }
                    else if ((int)reader[1] == 1)
					{
						rtbCurrentPalletProduction.Text += reader[0].ToString() + " - 1 roll @ " + ((decimal)reader[2]).ToString("N0") + " LF and " + ((decimal)reader[3]).ToString("N0") + " LBS\r\n";
					}
					else
					{
						rtbCurrentPalletProduction.Text += reader[0].ToString() + " - " + ((int)reader[1]).ToString("N0") + " rolls @ " + ((decimal)reader[2]).ToString("N0") + " LF and " + ((decimal)reader[3]).ToString("N0") + " LBS\r\n";
					}
				}
				
				reader.Close();
				if (currentPalletList.IndexOf(',') == -1)
				{
					lblCurrentPalletProduction.Text = "Current Pallet";
				}
				else
				{
					lblCurrentPalletProduction.Text = "Current Pallets";
				}
			}

            command = new SqlCommand("SELECT CAST(ISNULL(ROUND(SUM(c.[Original LF] / d.[No Streams]), 0), 0) AS int) FROM [Production Master Table] a INNER JOIN ([Production Roll Table] b INNER JOIN [Roll Table] c ON b.[Roll ID] = c.[Roll ID]) ON a.[Production ID] = b.[Production ID] INNER JOIN [Slitting Master Job Standards View] d ON a.[Master Item No] = d.[Master Item No] WHERE a.[Production ID] = " + currentProductionRecord.ToString(), connection1);
            createdFeet = (int)command.ExecuteScalar() - incomingProdLinearFeet;
			txtCreatedFeet.Text = createdFeet.ToString("N0");
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		private void RtbTextChanged(object sender, EventArgs e)
		{
			((RichTextBox)sender).SelectionStart = ((RichTextBox)sender).Text.Length;
			((RichTextBox)sender).ScrollToCaret();
		}
		
		private void CmdReprintLabelClick(object sender, EventArgs e)
		{
			GetInputForm getunwindRollNumberForm = new GetInputForm("Scan/Input Roll ID", "R", 0, 0, false);
			getunwindRollNumberForm.ShowDialog();
			if (getunwindRollNumberForm.UserInput.Length > 0)
			{
				PrintClass.Label(getunwindRollNumberForm.UserInput);
			}
			
			getunwindRollNumberForm.Dispose();
		}
		
		private void CmdJobHistoryClick(object sender, EventArgs e)
		{
			if (jobHistoryFormOpen)
			{
				currentJobHistoryForm.Focus();
			}
			else
			{
				currentJobHistoryForm = new JobHistoryForm(prodJobNumber, prodMasterItemNumber);
				currentJobHistoryForm.Show();
			}
		}
		
		private void SlittingProductionFormFormClosed(object sender, FormClosedEventArgs e)
		{
			consumedFilmForm.Dispose();
			if (jobHistoryFormOpen)
			{
				currentJobHistoryForm.Close();
			}
		}
		
		private void CmdShowFilmUsageClick(object sender, EventArgs e)
		{
			if (consumedFilmForm.CanFocus)
			{
				consumedFilmForm.Focus();
			}
			else
			{
				consumedFilmForm.Show();
			}
		}
		
		private void FillLooseRollText()
		{
			rtbLooseSlitRolls.Text = string.Empty;
			command = new SqlCommand("SELECT 'R' + CAST(a.[Roll ID] AS nvarchar(10)) + ' Job ' + CAST(b.[Reference Item No] AS nvarchar(10)) + ' (' + b.[Description] + ')' FROM [Roll Table] a INNER JOIN [Inventory Master Table] b ON a.[Master Item No] = b.[Master Item No] WHERE a.[Current LF] != 0 AND [Pallet ID] IS NULL AND b.[Reference Item No] % 100 = 31 AND b.[Item Type No] = 2 AND a.[Location ID] = " + MainForm.MachineNumber, connection1);
			reader = command.ExecuteReader();
			if (reader.Read())
		    {
			    int i = 0;
			    do
			    {
			    	rtbLooseSlitRolls.Text += reader[0].ToString() + "\r\n\r\n";
			    	i++;
			    }
			    while (reader.Read());
			    if (i == 1)
			    {
			    	lblLooseSlitRolls.Text = "Loose Roll";
			    }
			    else
			    {
			    	lblLooseSlitRolls.Text = "Loose Rolls";
			    }
		    }
			else
			{
				lblLooseSlitRolls.Text = string.Empty;
			}
			
			reader.Close();
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void RefreshDgvJobs()
		{
			dgvJobs.Rows.Clear();
			cmdGetPallet.Enabled = false;
			currentPalletList = string.Empty;
			command = new SqlCommand("SELECT a.[Job Jacket No], b.[Description], a.[No Streams], a.[Master Item No], c.[Pallet ID], cast(c.[Blank Weight] AS int), a.[Stream Width] FROM [Slitting Specification Table] a INNER JOIN [Inventory Master Table] b ON a.[Master Item No] = b.[Master Item No] LEFT JOIN ([Current Pallets at Machine Table] c INNER JOIN [Pallet Table] d ON c.[Pallet ID] = d.[Pallet ID] and d.[Location ID] = " + MainForm.MachineNumber + ") ON a.[Master Item No] = c.[Master Item No] WHERE ISNULL(a.[Combo Master Item No], a.[Master Item No]) = " + prodMasterItemNumber, connection1);
			reader = command.ExecuteReader();
			while (reader.Read())
			{
				if (reader[4] == DBNull.Value)
				{
					// No pallet assigned to Job
					dgvJobs.Rows.Add(reader[0].ToString(), reader[1].ToString(), ((int)reader[2]).ToString("N0"), reader[3].ToString(), null, null, reader[6].ToString());
					cmdGetPallet.Enabled = true;
				}
				else
				{
					dgvJobs.Rows.Add(reader[0].ToString(), reader[1].ToString(), ((int)reader[2]).ToString("N0"), reader[3].ToString(), "P" + reader[4].ToString(), ((int)reader[5]).ToString("N0"), reader[6].ToString());
					currentPalletList += reader[4].ToString() + ", ";
				}
			}
			
			reader.Close();
			if (!string.IsNullOrEmpty(currentPalletList))
			{
				currentPalletList = currentPalletList.Substring(0, currentPalletList.Length - 2);
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void CmdGetPalletClick(object sender, EventArgs e)
		{
			DialogResult answer;
			
			foreach (DataGridViewRow row in dgvJobs.Rows)
			{
				if (row.Cells[4].Value == null)
				{
					answer = MessageBox.Show("Do you wish to create a new pallet for job " + row.Cells[0].Value.ToString() + "?", "Create Pallet?", MessageBoxButtons.YesNo);
					if (answer == DialogResult.Yes)
					{
						GetInputForm newPalletWeightForm = new GetInputForm("Enter Blank Pallet Weight", "#", 20, 80, false);
						newPalletWeightForm.ShowDialog();
						if (newPalletWeightForm.UserInput.Length > 0 && int.Parse(newPalletWeightForm.UserInput, NumberStyles.Number) > 0)
						{
							command = new SqlCommand("EXECUTE [Create Pallet Stored Procedure] '" + StartupForm.UserName + "', " + MainForm.MachineNumber + ", NULL, NULL", connection1);
							connection1.Open();
							reader = command.ExecuteReader();
							reader.Read();
							int palletNumber = (int)reader[0];
							reader.Close();
							command = new SqlCommand("INSERT INTO [Current Pallets at Machine Table] SELECT " + Int32.Parse(row.Cells[3].Value.ToString(), NumberStyles.Number).ToString() + ", " + palletNumber.ToString() + ", " + newPalletWeightForm.UserInput, connection1);
							command.ExecuteNonQuery();
							connection1.Close();
							PrintClass.Label("P" + palletNumber.ToString());
						}

						newPalletWeightForm.Dispose();
					}
					else
					{
						answer = MessageBox.Show("Do you wish to use an existing pallet for job " + row.Cells[0].Value.ToString() + "?", "Use Exising Pallet?", MessageBoxButtons.YesNo);
						if (answer == DialogResult.Yes)
						{
							GetInputForm getPalletIDForm = new GetInputForm("Scan/Input Pallet ID", "P", 0, 0, false);
							getPalletIDForm.ShowDialog();
							if (getPalletIDForm.UserInput.Length > 0)
							{
								command = new SqlCommand("SELECT a.[Master Item No], b.[Reference Item No], b.[Description], c.[Location ID], CASE WHEN e.[Pallet ID] IS NOT NULL THEN c.[Location ID] END, ISNULL(c.[Weight] - SUM(a.[Original Pounds] * a.[Current LF] / a.[Original LF]), 40) FROM [Roll Table] a INNER JOIN [Inventory Master Table] b ON a.[Master Item No] = b.[Master Item No] INNER JOIN ([Pallet Table] c INNER JOIN [Location Table] d ON c.[Location ID] = d.[Location ID]) ON a.[Pallet ID] = c.[Pallet ID] LEFT JOIN [Current Pallets at Machine Table] e ON a.[Pallet ID] = e.[Pallet ID] WHERE d.[Inventory Available] = 1 AND a.[Current LF] > 0 AND a.[Pallet ID] = " + getPalletIDForm.UserInput.Substring(1) + " GROUP BY a.[Master Item No], b.[Reference Item No], b.[Description], c.[Location ID], CASE WHEN e.[Pallet ID] IS NOT NULL THEN c.[Location ID] END, c.[Weight]", connection1);
								connection1.Open();
								reader = command.ExecuteReader();
								if (reader.Read())
								{
									if (reader[4] != DBNull.Value)
									{
										MessageBox.Show("Pallet " + getPalletIDForm.UserInput + " is currently in production at machine " + reader[4].ToString() + " and thus cannot be moved.", "Invalid Pallet");
									}
									else if (reader[0].ToString() != row.Cells[3].Value.ToString())
									{
										MessageBox.Show("Pallet " + getPalletIDForm.UserInput + " contains " + reader[1].ToString() + " - " + reader[2].ToString() + " and thus cannot be used.", "Invalid Pallet");
									}
									else
									{
										// Valid Pallet
										connection2.Open();
										if (reader[3].ToString() != MainForm.MachineNumber)
										{
											command = new SqlCommand("INSERT INTO [Move Transaction Table] SELECT '" + StartupForm.UserName + "', GETDATE(), NULL, " + getPalletIDForm.UserInput.Substring(1) + ", " + MainForm.MachineNumber, connection2);
											command.ExecuteNonQuery();
										}
										
										command = new SqlCommand("INSERT INTO [Current Pallets at Machine Table] SELECT " + row.Cells[3].Value.ToString() + ", " + getPalletIDForm.UserInput.Substring(1) + ", " + reader[5].ToString(), connection2);
										command.ExecuteNonQuery();
										connection2.Close();
									}
								}
								else
								{
									MessageBox.Show("Pallet " + getPalletIDForm.UserInput + " Not Found.", "Invalid Pallet");
								}
									
								reader.Close();
								connection1.Close();
							}
							
							getPalletIDForm.Dispose();
						}
					}
				}
			}
			
			connection1.Open();
			RefreshDgvJobs();
			connection1.Close();
		}
		void EndOfShiftTimerTick(object sender, EventArgs e)
		{
			if (endOfShiftTime <= DateTime.Now)
			{
				MessageBox.Show("End of Shift.  Please finish recording your production data.", "Shift is Over");
				endOfShiftTimer.Enabled = false;
			}
		}

        private void cmdJobToDateStats_Click(object sender, EventArgs e)
        {
            JobToDateStatsForm jobStatsForm = new JobToDateStatsForm(prodJobNumber, true, "0", "0", "0", "0");
            jobStatsForm.ShowDialog();
            jobStatsForm.Dispose();
        }
    }
}
