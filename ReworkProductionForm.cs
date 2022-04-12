/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 8/20/2013
 * Time: 9:03 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
	
namespace ShopFloor
{
	
	using System;
	using System.Data;
	using System.Data.SqlClient;
	using System.Globalization;
	using System.IO;
	using System.Windows.Forms;
	/// <summary>
	/// Description of ReworkProductionForm.
	/// </summary>
	public partial class ReworkProductionForm : Form
	{
        private CommentForm productionNotesForm;
        private static bool jobHistoryFormOpen = false;
		private string reworkMachineNumber;
		private string jobNumber;
        private string jobDescription = string.Empty;   
        private string operatorName;
        private string prodMasterItemNumber;
		private DateTime endOfShiftTime;
		private int currentProductionRecord;
		private DateTime startTime;
		private int nextDownTimeRecordID = 1;
		private int nextScrapRecordID = 1;
		private JobHistoryForm frmCurrentJobHistory;
		private SqlConnection connection1 = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
		private SqlConnection connection2 = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
		private SqlDataReader reader;
		private SqlCommand command;
		private ConsumedFilmForm currentConsumedFilmForm;
		private int consumedFeet;
		private decimal currentWidth;
		private int currentRollNumber;
		private int currentInputLF = 0;
        private DataTable UPCCodesTable = new DataTable();
        private bool isPrinted = true;
        private int createdFeet;
		private int rollNumber;
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1306:SetLocaleForDataTypes")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "wip")]
		public ReworkProductionForm(string reworkMachineNo, string currentOperatorName, string currentJobNumber, string prodMasterItemNo, int wipType, int currentProdRecord, DateTime prodStartTime, DateTime endOfShift, int incomingProdLF)
		{
            InitializeComponent();
			rtbJobTitle.SelectionAlignment = HorizontalAlignment.Center;
			rtbJobTitle.Text = "Rework Production on Line " + reworkMachineNo + "  Operator: " + operatorName + "  Job " + currentJobNumber + "     Start Time: " + prodStartTime.ToString("dddd, MMMM d, yyyy h:mm tt");
			reworkMachineNumber = reworkMachineNo;
			jobNumber = currentJobNumber;
            operatorName = currentOperatorName;
            prodMasterItemNumber = prodMasterItemNo;
			endOfShiftTime = endOfShift;
			if (endOfShiftTime > DateTime.Now)
			{
				endOfShiftTimer.Interval = 60000;
				endOfShiftTimer.Enabled = true;
			}

            if (currentJobNumber.Substring(currentJobNumber.Length - 2, 1) == "1" || currentJobNumber.Substring(currentJobNumber.Length - 2, 1) == "2")
            { 
                // Check whether item is printed
                if (currentJobNumber.Substring(currentJobNumber.Length - 2, 1) == "1")
                {
                
                    command = new SqlCommand("SELECT CAST(CASE WHEN [dbo].[Get Print Application Type](a.[Master Item No]) = 'OPV' AND b.[Item Type No] = 1 THEN 0 ELSE 1 END AS bit) FROM [Printing Specification Table] a INNER JOIN [Inventory Master Table] b ON a.[Input Master Item No] = b.[Master Item No] WHERE a.[Master Item No] = " + prodMasterItemNo, connection1);
                }
                else
                {
                    command = new SqlCommand("SELECT CAST(CASE WHEN ISNULL(b.[Printed], 0) = 1 OR ISNULL(c.[Printed], 0) = 1 THEN 1 ELSE 0 END AS bit) FROM [Lamination Specification Table] a OUTER APPLY [Lam Input Printed WIP](1, a.[Master Item No]) b OUTER APPLY [dbo].[Lam Input Printed WIP](2, a.[Master Item No]) c WHERE a.[Master Item No] = " + prodMasterItemNo, connection1);
                }

                connection1.Open();
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isPrinted = reader.GetBoolean(0);
                }

                reader.Close();
                connection1.Close();
            }

            if (wipType == 2) 
			{
				// Job Jacket WIP
				command = new SqlCommand("SELECT a.[Job Jacket No], b.[Description], a.[No Streams], ISNULL(a.[Combo No], 0), [dbo].[Get Numbers Only](a.[UPC Code]) FROM [Finished Goods Specification Table] a INNER JOIN [Inventory Master Table] b ON a.[Master Item No] = b.[Master Item No] WHERE a.[Job Jacket No] = " + currentJobNumber.ToString().Substring(0, currentJobNumber.ToString().Length - 2), connection1);
			}
			else 
			{
				// Combo Job WIP
				command = new SqlCommand("SELECT a.[Job Jacket No], b.[Description], a.[No Streams], CAST(0 AS int), [dbo].[Get Numbers Only](a.[UPC Code]) FROM [Finished Goods Specification Table] a INNER JOIN [Inventory Master Table] b ON a.[Master Item No]=b.[Master Item No] WHERE a.[Combo No] = " + currentJobNumber.ToString().Substring(0, currentJobNumber.ToString().Length - 2) + " ORDER BY a.[Job Jacket No]", connection1);
			}

			currentProductionRecord = currentProdRecord;
			startTime = prodStartTime;
			connection1.Open();
			reader = command.ExecuteReader();
			reader.Read();
			if (reader.HasRows)
			{
                
                UPCCodesTable.Columns.Add("UPC Code", typeof(string));
                do
				{
					if (reader[2].ToString() == "1")
					{
						rtbJobDescriptions.Text += "Job " + reader[0].ToString() + " - " + reader[1].ToString() + " (1 Stream)\r\n";
					}
					else
					{
						rtbJobDescriptions.Text += "Job " + reader[0].ToString() + " - " + reader[1].ToString() + " (" + reader[2].ToString() + " Streams)\r\n";
					}

                    jobDescription += reader[1].ToString() + " \\ ";

                    if (isPrinted && !string.IsNullOrEmpty(reader[4].ToString()))
                    {
                        DataRow row = UPCCodesTable.NewRow();
                        row["UPC Code"] = reader[4];
                        UPCCodesTable.Rows.Add(row);
                    }
                }
				while (reader.Read());
									
				reader.Close();
                jobDescription = jobDescription.Substring(0, jobDescription.Length - 3);
			}
			else
			{
				MessageBox.Show("Error - Job not found in Job Jacket database but did exist at one point.  Please contact the office", "Job Not Found");
				reader.Close();
				connection1.Close();
				this.Close();
			}
			
			// Form is valid so add form close events
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProductionFormFormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ProductionFormFormClosed);
			command = new SqlCommand("SELECT [Setup Hrs], [Run Hrs], [DT Hrs], [Setup Hrs] + [Run Hrs] + [DT Hrs] FROM [dbo].[Get Standard Production Information] (" + currentProductionRecord.ToString() +")", connection1);
			reader = command.ExecuteReader();
			reader.Read();
			txtSetupHours.Text = ((decimal)reader[0]).ToString("N2");
			txtRunHours.Text = ((decimal)reader[1]).ToString("N2");
			txtDownTimeHours.Text = ((decimal)reader[2]).ToString("N2");
			txtTotalHours.Text = ((decimal)reader[3]).ToString("N2");
            reader.Close();
            ModulesClass.FillDowntimeComboBox(cboDownTimeReasons, jobNumber.Substring(jobNumber.Length - 2, 1));
            nextDownTimeRecordID = ModulesClass.FillDowntimeDetails(rtbDownTimeRecords, cboRemoveDowntimeRecord, pnlRemoveDownTimeRecord, currentProductionRecord.ToString());
            ModulesClass.FillScrapReasonComboBox(cboScrapReasons, jobNumber.Substring(jobNumber.Length - 2, 1));
            nextScrapRecordID = ModulesClass.FillScrapDetails(rtbScrapRecords, cboRemoveScrapRecord, pnlRemoveScrapRecord, txtTotalScrapPounds, currentProductionRecord.ToString());
			currentConsumedFilmForm = new ConsumedFilmForm(false);
			FillConsumedFilmGrid();

            createdFeet = -incomingProdLF;
			command = new SqlCommand("SELECT a.[Roll ID], CAST(ROUND(b.[Original LF], 0) AS int) FROM [Production Roll Table] a INNER JOIN [Roll Table] b ON a.[Roll ID] = b.[Roll ID] WHERE a.[Production ID] = " + currentProductionRecord.ToString() + " AND b.[Original LF] != 0 ORDER BY b.[Roll ID]", connection1);
			reader = command.ExecuteReader();
			while (reader.Read())
			{
				rtbCreatedRollInfo.Text += "WIP Roll " + reader[0].ToString() + " @ " + ((int)reader[1]).ToString("N0") + " Feet\r\n";
				createdFeet += (int)reader[1];
			}
			
			reader.Close();
			txtCreatedFeet.Text = createdFeet.ToString("N0");
            command = new SqlCommand("SELECT ISNULL([Notes], '') FROM [Production Master Table] WHERE [Production ID] = " + currentProductionRecord.ToString(), connection1);
            string comments = command.ExecuteScalar().ToString();
            connection1.Close();
            if (currentJobNumber.EndsWith("31") || currentJobNumber.EndsWith("32") || currentJobNumber.EndsWith("33"))
			{
                cmdCreatePallet.Visible = true;
            }

            productionNotesForm = new CommentForm(jobNumber + " Production Notes", comments, false);
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
		private void FillConsumedFilmGrid()
		{
            consumedFeet = 0;
            currentWidth = 0;
            currentRollNumber = 0;
            cmdReturnRoll.Visible = false;
            SqlConnection consumedFilmConnection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
            command = new SqlCommand("SELECT e.[Name], a.[Roll ID], b.[Width], a.[Start Usage Date], CAST(a.[Start Usage LF] AS int), a.[End Usage Date], CAST(ISNULL(a.[End Usage LF], 0) AS int), c.[Description] FROM [Production Consumed Roll Table] a INNER JOIN ([Roll Table] b INNER JOIN [Inventory Master Table] c ON b.[Master Item No] = c.[Master Item No]) ON a.[Roll ID] = b.[Roll ID] INNER JOIN ([Production Master Table] d INNER JOIN [Operator Table] e ON d.[Operator ID] = e.[Operator ID]) ON a.[Start Production ID] = d.[Production ID] WHERE a.[Start Usage LF] != ISNULL(a.[End Usage LF], 0) AND a.[Start Production ID] = " + currentProductionRecord.ToString() + " ORDER BY a.[Start Usage Date]", consumedFilmConnection);
            consumedFilmConnection.Open();
            SqlDataReader consumedFilmReader = command.ExecuteReader();
            while (consumedFilmReader.Read())
            {
                currentConsumedFilmForm.AddRoll(consumedFilmReader[0].ToString(), (DateTime)consumedFilmReader[3], consumedFilmReader[1].ToString(), "Consumed", "1", ((decimal)consumedFilmReader[2]).ToString("N4") + "\" " + consumedFilmReader[7].ToString(), (int)consumedFilmReader[4]);
                if (consumedFilmReader[5] != DBNull.Value && (int)consumedFilmReader[6] > 0)
                {
                    // There is a return
                    currentConsumedFilmForm.AddRoll(consumedFilmReader[0].ToString(), (DateTime)consumedFilmReader[5], consumedFilmReader[1].ToString(), "Returned", "1", ((decimal)consumedFilmReader[2]).ToString("N4") + "\" " + consumedFilmReader[7].ToString(), (int)consumedFilmReader[6]);
                    
                }
                else
                {
                    // There is no Return
                    cmdReturnRoll.Visible = true;
                    rtbInputFilm.Text = "Currently Roll R" + consumedFilmReader[1].ToString() + " " + ((decimal)consumedFilmReader[2]).ToString("N4") + "\" " + consumedFilmReader[7].ToString() + " @ " + ((int)consumedFilmReader[4]).ToString("N0") + " LF is being consumed";
                    currentRollNumber = (int)consumedFilmReader[1];
                    currentInputLF = (int)consumedFilmReader[4];
                    currentWidth = (decimal)consumedFilmReader[2];
                }

                consumedFeet += (int)consumedFilmReader[4] - (int)consumedFilmReader[6];
            }

            consumedFilmReader.Close();
            consumedFilmConnection.Close();
            txtConsumedFeet.Text = consumedFeet.ToString("N0");
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
		private void CmdRemoveDowntimeRecordClick(object sender, EventArgs e)
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
		private void ProductionFormFormClosing(object sender, FormClosingEventArgs e)
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
                command = new SqlCommand("SELECT a.[Master Item No], a.[Width], d.[Description], ISNULL(a.[Location ID], b.[Location ID]), CAST(ROUND(a.[Current LF], 0) AS int), CAST(ROUND(a.[Original Pounds] * a.[Current LF] / a.[Original LF], 0) AS int), c.[Vendor Roll ID], f.[Description], f.[Inventory Available], d.[Reference Item No], CAST(d.[Item Type No] AS int), e.[Comment] FROM [Roll Table] a LEFT JOIN [Pallet Table] b ON a.[Pallet ID] = b.[Pallet ID] LEFT JOIN [Roll PO Table] c ON a.[Roll ID] = c.[Roll ID] INNER JOIN [Inventory Master Table] d ON a.[Master Item No] = d.[Master Item No]  LEFT JOIN [Roll Comment Table] e ON a.[Roll ID] = e.[Roll ID], [Location Table] f WHERE a.[Original LF] > 0 AND ISNULL(a.[Location ID], b.[Location ID]) = f.[Location ID] AND a.[Roll ID] = " + getRollToConsumeForm.UserInput.Substring(1), connection1);
                connection1.Open();
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    if ((int)reader[4] == 0)
                    {
                        MessageBox.Show("Error - roll " + getRollToConsumeForm.UserInput + " " + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + " at location L" + reader[3].ToString() + " (" + reader[7].ToString() + ")" + " has no inventory available (maybe a return is missing?)", "No Inventory");
                    }
                    else if (!(bool)reader[8])
                    {
                        MessageBox.Show("Error - roll " + getRollToConsumeForm.UserInput + " " + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + " is unavailable due to being at location L" + reader[3].ToString() + " (" + reader[7].ToString() + ")", "Roll Unavailable");
                    }
                    else if (reader[3].ToString() != reworkMachineNumber)
                    {
                        MessageBox.Show("Error - roll " + getRollToConsumeForm.UserInput + " " + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + " is at location L" + reader[3].ToString() + " (" + reader[7].ToString() + ")", "Roll is not at machine");
                    }
                    else
                    {
                        bool UPCCodeValidated;
                        if (isPrinted)
                        {
                            bool inputRollOK = true;
                            string overrideAuthorizedBy = string.Empty;
                            if (UPCCodesTable.Rows.Count > 0)
                            {
                                inputRollOK = ModulesClass.ValidateUPCCodes(jobNumber, getRollToConsumeForm.UserInput, UPCCodesTable, out overrideAuthorizedBy);
                            }

                            if (!inputRollOK)
                            {
                                MessageBox.Show("UPC Code not validated. THe roll cannont be consumed.", "UPC Code not validated");
                                UPCCodeValidated = false;
                            }
                            else
                            {
                                UPCCodeValidated = true;
                                if (!string.IsNullOrEmpty(overrideAuthorizedBy))
                                {
                                    ModulesClass.SendEmail(2, "Manual UPC Verification", "The UPC codes of input Roll " + getRollToConsumeForm.UserInput + " for rework job " + jobNumber + " - " + jobDescription + " on line " + MainForm.MachineNumber + " were manually input.  The override was authorized by " + overrideAuthorizedBy + ".");
                                }
                            }
                        }
                        else
                        {
                            UPCCodeValidated = true;
                        }

                        if (UPCCodeValidated)
                        {
                            GetInputForm getVendorRollIDForm = new GetInputForm("Scan/Input Vendor Roll ID (Hit [Abort] if none)", "*", 0, 0, false);
                            getVendorRollIDForm.ShowDialog();
                            if (!string.IsNullOrEmpty(getVendorRollIDForm.UserInput))
                            {
                                command = new SqlCommand("UPDATE [Roll PO Table] SET [Vendor Roll ID]='" + getVendorRollIDForm.UserInput.Replace("'", "''") + "' WHERE [Roll ID]=" + getRollToConsumeForm.UserInput.Substring(1), connection2);
                                connection2.Open();
                                command.ExecuteNonQuery();
                                connection2.Close();
                            }

                            getVendorRollIDForm.Dispose();

                            currentWidth = (decimal)reader[1];
                            currentConsumedFilmForm.AddRoll(operatorName, DateTime.Now, getRollToConsumeForm.UserInput.Substring(1), "Consumed", "1", currentWidth.ToString("N4") + "\" " + reader[2].ToString(), (int)reader[4]);
                            rtbUnwindCurrentRoll.Text = "Currently Roll " + getRollToConsumeForm.UserInput + " " + currentWidth.ToString("N4") + "\" " + reader[2].ToString() + " @ " + ((int)reader[4]).ToString("N0") + " LF and " + ((int)reader[5]).ToString("N0") + " LBS is being consumed";
                            currentRollNumber = int.Parse(getRollToConsumeForm.UserInput.Substring(1), NumberStyles.Number);
                            currentInputLF = (int)reader[4];
                            consumedFeet += (int)reader[4];
                            txtConsumedFeet.Text = consumedFeet.ToString("N0");
                            command = new SqlCommand("INSERT INTO [Production Consumed Roll Table] SELECT " + currentProductionRecord.ToString() + ", 1, " + getRollToConsumeForm.UserInput.Substring(1) + ", '" + DateTime.Now.ToString() + "', " + reader[4].ToString() + ", NULL, NULL, NULL", connection2);
                            connection2.Open();
                            command.ExecuteNonQuery();
                            connection2.Close();
                            if (!string.IsNullOrEmpty(reader[11].ToString()))
                            {
                                MessageBox.Show(reader[11].ToString(), "Roll Comments");
                            }
                        }

                        cmdReturnRoll.Visible = true;
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
                command = new SqlCommand("SELECT TOP 1 b.[Master Item No], b.[Width], c.[Description], d.[Machine No], CAST(ROUND(a.[Start Usage LF], 0) AS int), CAST(ISNULL(ROUND(a.[End Usage LF], 0), 0) AS int), a.[Start Usage Date] FROM [Production Consumed Roll Table] a INNER JOIN ([Roll Table] b INNER JOIN [Inventory Master Table] c ON b.[Master Item No] = c.[Master Item No]) ON a.[Roll ID] = b.[Roll ID] INNER JOIN [Production Master Table] d ON a.[Start Production ID] = d.[Production ID] WHERE	a.[Start Usage LF] != ISNULL(a.[End Usage LF], 0) AND d.[Master Item No] = " + prodMasterItemNumber + " AND a.[Roll ID] = " + getRollToReturnForm.UserInput.Substring(1) + " ORDER BY a.[Start Usage Date] DESC", connection1);
                connection1.Open();
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    if (reader[3].ToString() != reworkMachineNumber)
                    {
                        MessageBox.Show("Error - roll " + getRollToReturnForm.UserInput + " " + reader[2].ToString() + " is allocated to this job but is on Machine " + reader[3].ToString(), "Wrong Machine");
                    }
                    else
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
                                    MessageBox.Show("You last returned roll " + getRollToReturnForm + " " + reader[2].ToString() + " for " + ((int)reader[5]).ToString("N0") + " LF.  You need to re-allocate the roll then return " + int.Parse(returnLFForm.UserInput, NumberStyles.Number).ToString("N0") + " LF IF the roll hasn't already been re-allocated", "Can't decrease a Return's Amount");
                                    answer = DialogResult.No;
                                }
                                else
                                {
                                    answer = MessageBox.Show("Roll " + getRollToReturnForm + " " + reader[2].ToString() + " has already been returned for " + ((int)reader[5]).ToString("N0") + " LF.  Do you wish to overwrite the return?", "Change Return Amount?", MessageBoxButtons.YesNo);
                                }
                            }

                            if (answer == DialogResult.Yes)
                            {
                                DateTime currentDateTime = DateTime.Now;
                                if (answer == DialogResult.Yes)
                                {
                                    command = new SqlCommand("UPDATE [Production Consumed Roll Table] SET [End Production ID] = " + currentProductionRecord.ToString() + ", [End Usage Date] = '" + DateTime.Now.ToString() + "', [End Usage LF] = " + returnLFForm.UserInput + " WHERE [Roll ID] = " + getRollToReturnForm.UserInput.Substring(1) + " AND [Start Usage Date] = '" + ((DateTime)reader[6]).ToString() + "'", connection2);
                                    connection2.Open();
                                    command.ExecuteNonQuery();
                                    connection2.Close();
                                    PrintClass.Label(getRollToReturnForm.UserInput);
                                    consumedFeet -= int.Parse(returnLFForm.UserInput, NumberStyles.Number);
                                    txtConsumedFeet.Text = consumedFeet.ToString("N0");
                                    if (currentRollNumber == int.Parse(getRollToReturnForm.UserInput.Substring(1)))
                                    {
                                        // There is no roll on the unwind
                                        rtbInputFilm.Text = string.Empty;
                                        currentWidth = 0;
                                        currentRollNumber = 0;
                                        currentInputLF = 0;
                                    }

                                    if ((int)reader[5] != 0)
                                    {
                                        currentConsumedFilmForm.ClearGrid();
                                        FillConsumedFilmGrid();
                                    }
                                    else
                                    {
                                        currentConsumedFilmForm.AddRoll(operatorName, DateTime.Now, getRollToReturnForm.UserInput.Substring(1), "Returned", reader[9].ToString(), ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString(), int.Parse(returnLFForm.UserInput, NumberStyles.Number));
                                    }
                                }
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
                    MessageBox.Show("Error - Roll " + getRollToReturnForm.UserInput + " has not been consumed on this job.", "Roll Not Found");
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
						if (result == DialogResult.No)
						{
							MessageBox.Show("Save Aborted.");
						}
						else
						{
							connection2.Open();
							command = new SqlCommand("UPDATE [Production Master Table] SET [End Reason ID] = 3 WHERE [Production ID] = " + reader[0].ToString(), connection2);
							command.ExecuteNonQuery();
							connection2.Close();
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
		}
			
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void CmdJobPulledClick(object sender, EventArgs e)
		{
			if (ModulesClass.ValidSaveDate(startTime.AddHours(double.Parse(txtTotalHours.Text, NumberStyles.Number)), endOfShiftTime))
			{
				OptionsForm reasonPulledOptionsForm = new OptionsForm("Reason for Pulling Job", false, true);
				command = new SqlCommand("SELECT SUBSTRING([Description], 10, LEN([Description]) - 9) FROM [Save Production Reason Table] WHERE SUBSTRING([Description], 1, 9) = 'Pulled - ' ORDER BY [End Reason ID]", connection1);
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
					connection1.Close();
                    reasonPulledOptionsForm.Dispose();
                    MessageBox.Show("Save aborted", "Save Aborted");
				}
				else
				{
					command = new SqlCommand("SELECT CAST([End Reason ID] AS int) FROM [Save Production Reason Table] WHERE [Description] = 'Pulled - " + reasonPulledOptionsForm.Option + "'", connection1);
					int reasonCode = (int)command.ExecuteScalar();
					connection1.Close();
                    reasonPulledOptionsForm.Dispose();
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
                    GetInputForm partialRollLFForm = new GetInputForm("In Process Roll LF", "#", 0, consumedFeet - createdFeet + 100000, false);
                    if (decimal.Parse(txtRunHours.Text, NumberStyles.Number) > 0)
                    {
                        partialRollLFForm.ShowDialog();
                        if (partialRollLFForm.UserInput.Length > 0)
                        {
                            endOutputLF = int.Parse(partialRollLFForm.UserInput, NumberStyles.Number);
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
                            command = new SqlCommand("execute [dbo].[Save Shift Change Unwind Footage Stored Procedure] " + currentProductionRecord + ", 1, 1, " + currentRollNumber.ToString() + ", " + partialRollLFForm.UserInput, connection1);
                            connection1.Open();
                            command.ExecuteNonQuery();
                            connection1.Close();
                        }
                    }

                    partialRollLFForm.Dispose();
                }

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
                connection1.Close();
                this.FormClosing -= ProductionFormFormClosing;
                this.Close();
            }
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void CmdCreateWIPRollClick(object sender, EventArgs e)
		{
			UnitInformationForm createWIPRollForm = new UnitInformationForm("Create WIP Roll", rtbJobDescriptions.Text, 0, true);
			createWIPRollForm.ShowDialog();
			if (createWIPRollForm.Units > 0)
			{
				GetDecimalInputForm getRollWidthForm = new GetDecimalInputForm(4);
				getRollWidthForm.Description = "Roll Width";
				getRollWidthForm.UserInput = currentWidth.ToString();
				getRollWidthForm.ShowDialog();
				if (!string.IsNullOrEmpty(getRollWidthForm.UserInput))
				{
					connection1.Open();
					if (currentRollNumber == 0)
					{
						command = new SqlCommand("EXECUTE [Create Production Roll Stored Procedure] " + currentProductionRecord.ToString() + ", NULL, NULL, 0, NULL, '" + createWIPRollForm.Notes.Replace("'", "''") + "', " + prodMasterItemNumber + ", '" + StartupForm.UserName + "', " + createWIPRollForm.Units.ToString() + ", " + createWIPRollForm.Pounds.ToString() + ", " + getRollWidthForm.UserInput + ", " + reworkMachineNumber + ", '" + DateTime.Now.ToString() + "'", connection1);
					}
					else
					{
						command = new SqlCommand("EXECUTE [Create Production Roll Stored Procedure] " + currentProductionRecord.ToString() + ", " + currentRollNumber.ToString() + ", NULL, 0, NULL, '" + createWIPRollForm.Notes.Replace("'", "''") + "', " + prodMasterItemNumber + ", '" + StartupForm.UserName + "', " + createWIPRollForm.Units.ToString() + ", " + createWIPRollForm.Pounds.ToString() + ", " + getRollWidthForm.UserInput + ", " + reworkMachineNumber + ", '" + DateTime.Now.ToString() + "'", connection1);
					}

					rollNumber = (int)command.ExecuteScalar();
					rtbCreatedRollInfo.Text += "WIP Roll " + rollNumber.ToString() + " @ " + createWIPRollForm.Units.ToString("N0") + " Feet\r\n";
					PrintClass.Label("R" + rollNumber.ToString());
					createdFeet += createWIPRollForm.Units;
					connection1.Close();
					txtCreatedFeet.Text = createdFeet.ToString("N0");
				}
				
				getRollWidthForm.Dispose();
			}
		
			createWIPRollForm.Dispose();
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		private void CmdMoveRollClick(object sender, EventArgs e)
		{
            ModulesClass.GetItemToMove(reworkMachineNumber);
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		private void RtbTextChanged(object sender, EventArgs e)
		{
			((RichTextBox)sender).SelectionStart = ((RichTextBox)sender).Text.Length;
			((RichTextBox)sender).ScrollToCaret();
		}
		
		private void CmdReprintLabelClick(object sender, EventArgs e)
		{
			GetInputForm getRollNumberForm = new GetInputForm("Scan/Input Roll ID", "R", 0, 0, false);
			getRollNumberForm.ShowDialog();
			if (getRollNumberForm.UserInput.Length > 0)
			{
				PrintClass.Label(getRollNumberForm.UserInput);
			}
			
			getRollNumberForm.Dispose();
		}
		
		private void CmdJobHistoryClick(object sender, EventArgs e)
		{
			if (jobHistoryFormOpen)
			{
				frmCurrentJobHistory.Focus();
			}
			else
			{
				frmCurrentJobHistory = new JobHistoryForm(jobNumber, prodMasterItemNumber);
				frmCurrentJobHistory.Show();
			}
		}
		
		private void ProductionFormFormClosed(object sender, FormClosedEventArgs e)
		{
			currentConsumedFilmForm.Dispose();
			if (jobHistoryFormOpen)
			{
				frmCurrentJobHistory.Close();
			}
		}
		
		private void CmdShowFilmUsageClick(object sender, EventArgs e)
		{
			if (currentConsumedFilmForm.CanFocus)
			{
				currentConsumedFilmForm.Focus();
			}
			else
			{
				currentConsumedFilmForm.Show();
			}
		}
		
		void EndOfShiftTimerTick(object sender, EventArgs e)
		{
			if (endOfShiftTime <= DateTime.Now)
			{
				MessageBox.Show("End of Shift.  Please finish recording your production data.", "Shift is Over");
				endOfShiftTimer.Enabled = false;
			}
		}

        private void ReworkProductionForm_Shown(object sender, EventArgs e)
        {
            productionNotesForm.Show();
        }

        private void cmdCreatePallet_Click(object sender, EventArgs e)
        {
            DialogResult answer;
            answer = MessageBox.Show("Do you wish to create a new pallet for job " + jobNumber + "?", "Create Pallet?", MessageBoxButtons.YesNo);
            if (answer == DialogResult.Yes)
            {
                GetInputForm newPalletWeightForm = new GetInputForm("Enter Blank Pallet Weight", "#", 20, 80, false);
                newPalletWeightForm.ShowDialog();
                if (newPalletWeightForm.UserInput.Length > 0 && int.Parse(newPalletWeightForm.UserInput, NumberStyles.Number) > 0)
                {
                    command = new SqlCommand("EXECUTE [Create Pallet Stored Procedure] '" + StartupForm.UserName + "', " + reworkMachineNumber + ", NULL, NULL", connection1);
                    connection1.Open();
                    reader = command.ExecuteReader();
                    reader.Read();
                    int palletNumber = (int)reader[0];
                    reader.Close();
                    command = new SqlCommand("INSERT INTO [Current Pallets at Machine Table] SELECT " + prodMasterItemNumber + ", " + palletNumber.ToString() + ", " + newPalletWeightForm.UserInput, connection1);
                    command.ExecuteNonQuery();
                    connection1.Close();
                    PrintClass.Label("P" + palletNumber.ToString());
                }

                newPalletWeightForm.Dispose();
            }

        }

        private void cmdCreateFGPallet_Click(object sender, EventArgs e)
        {
            ModulesClass.CreateFGPallet();
        }
    }
}
