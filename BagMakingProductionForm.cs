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
	/// Description of BagMakingProductionForm.
	/// </summary>
	public partial class BagMakingProductionForm : Form
	{
        private CommentForm productionNotesForm;
        private static bool jobHistoryFormOpen = false;
		private string jobNumber;
		private string operatorName;
		private string prodMasterItemNumber;
		private DateTime endOfShiftTime;
        private int incomingBags;
		private int currentProductionRecord;
		private DateTime startTime;
		private SqlConnection connection1 = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
		private SqlConnection connection2 = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
		private SqlCommand command;
		private SqlDataReader reader;
		private string jobDescription;
		private int currentPalletNumber = 0;
		private int defaultBagsPerCase;
		private int casesPerPallet;
		private decimal drawPerBag;
        private DataTable inputsTable = new DataTable();
		private BindingSource bs;
		private int nextDownTimeRecordID = 1;
		private int nextScrapRecordID = 1;
        private int numberLanes = 0;
        private decimal consumedBags;
        private decimal consumedBags1;
        private decimal consumedBags2;
		private int producedBags;
		private ConsumedFilmForm consumedFilmForm;
		private JobHistoryForm currentJobHistoryForm;
		private decimal poundsPerCase = 0;
		private int currentBagsperCase = 0;
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1306:SetLocaleForDataTypes")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "wip")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		public BagMakingProductionForm(string currentOperatorName, string prodJobNo, string prodMasterItemNo, int wipType, int currentProdRecord, DateTime prodStartTime, DateTime endOfShift, int incomingProdBags, bool parNewJob)
		{
            InitializeComponent();
			rtbJobTitle.SelectionAlignment = HorizontalAlignment.Center;
			rtbJobTitle.Text = "Bag Making   Operator: " + currentOperatorName + "	    Job " + prodJobNo + "     Start Time: " + prodStartTime.ToString("dddd, MMMM d, yyyy h:mm tt");
			jobNumber = prodJobNo;
			operatorName = currentOperatorName;
			prodMasterItemNumber = prodMasterItemNo;
			endOfShiftTime = endOfShift;
            incomingBags = incomingProdBags;
			if (endOfShiftTime > DateTime.Now)
			{
				endOfShiftTimer.Interval = 60000;
				endOfShiftTimer.Enabled = true;
			}
			
			cmdGetPallet.Text = "Get Pallet";
			currentProductionRecord = currentProdRecord;
			startTime = prodStartTime;
			command = new SqlCommand("SELECT b.[Description], CAST(a.[Bags Per Case] AS int), CAST(a.[Cases per Pallet] AS int), a.[Length], a.[Width], ISNULL(a.[Gusset], 0), c.[Description], ISNULL(a.[Zipper Inches], 0), d.[Repeat], ISNULL(e.[BagSpecInstructions], '') FROM [Bag Specification Table] a INNER JOIN [Inventory Master Table] b ON a.[Master Item No] = b.[Master Item No] LEFT JOIN [Inventory Master Table] c ON a.[Zipper Master Item No] = c.[Master Item No] INNER JOIN ([Finished Goods Specification Table] d INNER JOIN [JobJackets].[dbo].[tblItem] e ON d.[Item No] = e.[ItemNo]) ON a.[Master Item no] = d.[Input Master Item No] WHERE a.[Master Item No] = " + prodMasterItemNo, connection1);
			connection1.Open();
			reader = command.ExecuteReader();
			if (reader.Read())
			{
				jobDescription = reader[0].ToString();
				defaultBagsPerCase = (int)reader[1];
				casesPerPallet = (int)reader[2];
				rtbJobDescription.Text = "Description: " + reader[0].ToString() + "\r\nBags per Case: " + defaultBagsPerCase.ToString("N0") + "    Cases per Pallet: " + casesPerPallet.ToString("N0") + "\r\nLength: " + ((decimal)reader[3]).ToString("N5") + "    Width: " + ((decimal)reader[4]).ToString("N5");
				if (!string.IsNullOrEmpty(reader[5].ToString()))
				{
					rtbJobDescription.Text +=  "    Gusset: " + ((decimal)reader[5]).ToString("N5");
			    }
				
				if (!string.IsNullOrEmpty(reader[6].ToString()))
				{
					rtbJobDescription.Text += "\r\nZipper: " + reader[6].ToString() + "    Inches: " + ((decimal)reader[7]).ToString("N5");
				}
				
				rtbJobDescription.Text += "\r\nNo Streams: " + reader[8].ToString();
                drawPerBag = (decimal)reader[8];
                rtbSpecialInstructions.Text = reader[9].ToString();
				reader.Close();
                
                // Get Input Items and Quantities
				command = new SqlCommand("SELECT CAST(a.[Unwind No] AS int), CAST(b.[Item Type No] AS int), a.[WIP Master Item No], b.[Reference Item No], b.[Description], [dbo].[Get Numbers Only](c.[UPC Code]), c.[Stream Width] * c.[Repeat] FROM [Bag WIP Input Table] a INNER JOIN [Inventory Master Table] b ON a.[WIP Master Item No] = b.[Master Item No] INNER JOIN [Finished Goods Specification Table] c ON a.[Bag Master Item No] = c.[Input Master Item No] WHERE a.[Bag Master Item No] = " + prodMasterItemNo + " ORDER BY [Unwind No]", connection1);
				reader = command.ExecuteReader();
				if (reader.Read())
				{
					this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BagMakingProductionFormFormClosing);
					this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BagMakingProductionFormFormClosed);
					DataTable inputsTable = new DataTable();
					inputsTable.Columns.Add("Unwind", typeof(int));
					inputsTable.Columns.Add("Item Type", typeof(int));
					inputsTable.Columns.Add("Master Item No", typeof(int));
					inputsTable.Columns.Add("Job No", typeof(int));
					inputsTable.Columns.Add("Description", typeof(string));
                    inputsTable.Columns.Add("UPC Code", typeof(string));
                    inputsTable.Columns.Add("Roll ID", typeof(string));
					inputsTable.Columns.Add("Default Job No", typeof(int));
					inputsTable.Columns.Add("Default Description", typeof(string));
					inputsTable.Columns.Add("Bags", typeof(decimal));
                    inputsTable.Columns.Add("Roll Width", typeof(decimal));

                    do
					{
						DataRow row = inputsTable.NewRow();
						for (int i = 0; i <= 5; i++)
						{
							row[i] = reader[i];
						}

                        //For now ignore the UPC Codes.  Change for loop above to "i <= 5" and delete command below to implement. DONE 2/23/2018
                        //row[5] = string.Empty;
						row[7] = row[3];
						row[8] = row[4];
						inputsTable.Rows.Add(row);
                        numberLanes++;
                    }
					while (reader.Read());
					
					reader.Close();
					bs = new BindingSource();
					bs.DataSource = inputsTable;
					dgvInputs.DataSource = bs;
					dgvInputs.Columns[0].Width = 50;
					dgvInputs.Columns[1].Visible = false;
					dgvInputs.Columns[2].Visible = false;
					dgvInputs.Columns[3].Width = 50;
					dgvInputs.Columns[4].Width = 225;
                    dgvInputs.Columns[5].Visible = false;
                    dgvInputs.Columns[6].Width = 60;
					dgvInputs.Columns[7].Visible = false;
					dgvInputs.Columns[8].Visible = false;
					dgvInputs.Columns[9].Width = 60;
					dgvInputs.Columns[9].DefaultCellStyle.Format = "N0";
                    dgvInputs.Columns[10].Visible = false;
                    command = new SqlCommand("DELETE FROM [Current Pallets at Machine Table] WHERE [Pallet ID] IN (SELECT [PALLET ID] FROM [Pallet Table] WHERE [Location ID] = " + MainForm.MachineNumber + ") AND [Master Item No] != " + prodMasterItemNo, connection1);
					command.ExecuteNonQuery();
					command = new SqlCommand("SELECT [Setup Hrs], [Run Hrs], [DT Hrs], [Setup Hrs] + [Run Hrs] + [DT Hrs] FROM [dbo].[Get Standard Production Information] (" + currentProductionRecord.ToString() +")", connection1);
					reader = command.ExecuteReader();
					reader.Read();
					txtSetupHours.Text = ((decimal)reader[0]).ToString("N2");
					txtRunHours.Text = ((decimal)reader[1]).ToString("N2");
					txtDownTimeHours.Text = ((decimal)reader[2]).ToString("N2");
					txtTotalHours.Text = ((decimal)reader[3]).ToString("N2");
                    reader.Close();
                    ModulesClass.FillDowntimeComboBox(cboDownTimeReasons, "5");
                    nextDownTimeRecordID = ModulesClass.FillDowntimeDetails(rtbDownTimeRecords, cboRemoveDowntimeRecord, pnlRemoveDownTimeRecord, currentProductionRecord.ToString());
                    ModulesClass.FillScrapReasonComboBox(cboScrapReasons, "5");
                    nextScrapRecordID = ModulesClass.FillScrapDetails(rtbScrapRecords, cboRemoveScrapRecord, pnlRemoveScrapRecord, txtTotalScrapPounds, currentProductionRecord.ToString());
				}
				else
				{
					MessageBox.Show("Error - No inputs found forJob J" + prodJobNo + ".  Please contact the office", "Job Inputs Not Found");
					reader.Close();
					connection1.Close();
					this.Close();
				}
			
			}
			else
			{
				MessageBox.Show("Error - Job not found in Job Jacket database but did exist at one point.  Please contact the office", "Job Not Found");
				reader.Close();
				connection1.Close();
				this.Close();
			}
			
            if (numberLanes > 1)
            {
                lblConsumedBags1.Text = "Bags - UW 1";
                lblConsumedBags2.Visible = true;
                txtConsumedBags2.Visible = true;
            }

			FillCaseText();
			FillLooseCaseText();
            command = new SqlCommand("SELECT ISNULL([Notes], '') FROM [Production Master Table] WHERE [Production ID] = " + currentProductionRecord.ToString(), connection1);
            string comments = command.ExecuteScalar().ToString();
            consumedFilmForm = new ConsumedFilmForm(true);
            connection1.Close();
            productionNotesForm = new CommentForm(jobNumber + " Production Notes", comments, false);
        }

        private void BagMakingProductionForm_Shown(object sender, EventArgs e)
        {
            connection1.Open();
            FillConsumedFilmGrid();
            connection1.Close();
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
		private void FillConsumedFilmGrid()
		{
            consumedBags = 0;
            consumedBags1 = 0;
            consumedBags2 = 0;
            command = new SqlCommand("SELECT e.[Name], a.[Roll ID], CAST(a.[Unwind No] AS int), b.[Width], a.[Start Usage Date], CAST(a.[Start Usage LF] AS int), a.[End Usage Date], CAST(ISNULL(a.[End Usage LF], 0) AS int), c.[Description], c.[Reference Item No] FROM [Production Consumed Roll Table] a INNER JOIN ([Roll Table] b INNER JOIN [Inventory Master Table] c ON b.[Master Item No] = c.[Master Item No]) ON a.[Roll ID] = b.[Roll ID] INNER JOIN ([Production Master Table] d INNER JOIN [Operator Table] e ON d.[Operator ID] = e.[Operator ID]) ON a.[Start Production ID] = d.[Production ID] WHERE a.[Start Usage LF] != ISNULL(a.[End Usage LF], 0) AND a.[Start Production ID] = " + currentProductionRecord.ToString() + " ORDER BY a.[Start Usage Date]", connection1);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                consumedFilmForm.AddRoll(reader[0].ToString(), (DateTime)reader[4], reader[1].ToString(), "Consumed", reader[2].ToString(), ((decimal)reader[3]).ToString("N4") + "\" " + reader[8].ToString(), (int)reader[5]);
                if (reader[6] != DBNull.Value && (int)reader[7] > 0)
                {
                    // There is a return
                    consumedFilmForm.AddRoll(reader[0].ToString(), (DateTime)reader[6], reader[1].ToString(), "Returned", reader[2].ToString(), ((decimal)reader[3]).ToString("N4") + "\" " + reader[8].ToString(), (int)reader[7]);
                }
                else
                {
                    // There is no Return
                    for (int i = 0; i < dgvInputs.Rows.Count; i++)
                    {
                        bool inputRollOK = true;
                        if ((int)dgvInputs["Unwind", i].Value == (int)reader[2])
                        {
                            /* Uncomment if we need to validate consumption on "Save Progress".  Will need to figure a way not to have to validate all consumed rolls.
                            if (!string.IsNullOrEmpty(dgvInputs["UPC Code", i].Value.ToString()))
                            {
                                string overrideAuthorizedBy = string.Empty;
                                inputRollOK = ModulesClass.ValidateUPC(dgvInputs["UPC Code", i].Value.ToString(), out overrideAuthorizedBy);
                                if (inputRollOK && !string.IsNullOrEmpty(overrideAuthorizedBy))
                                {
                                    ModulesClass.SendEmail(2, "Manual UPC Verification", "The UPC code of input Roll R" + reader[1].ToString() + " on unwind " + dgvInputs["UnwindD", i].Value.ToString() + " for job " + jobNumber + " - " + jobDescription + " on line " + MainForm.MachineNumber + " was manually input.  The override was authorized by " + overrideAuthorizedBy + ".");
                                }
                            }
                            */
                            if (inputRollOK)
                            {
                                dgvInputs["Job No", i].Value = (int)reader[9];
                                dgvInputs["Description", i].Value = reader[8].ToString();
                                dgvInputs["Roll ID", i].Value = "R" + reader[1].ToString();
                                dgvInputs["Bags", i].Value = (decimal)(int)reader[5] * (decimal)12 / drawPerBag;
                                dgvInputs["Roll Width", i].Value = (decimal)reader[3];
                            }
                            else
                            { 
                                MessageBox.Show("THe UPC Code of Roll R" + reader[1].ToString() + " was not validated.  You cannot consume a roll without it.", "Consumption Aborted");
                            }

                            break;
                        }
                    }

                    consumedBags += (((decimal)(int)reader[5] - (decimal)(int)reader[7]) * (decimal)12 / drawPerBag) / (decimal)numberLanes;
                    if ((int)reader[2] == 1)
                    {
                        consumedBags1 += ((decimal)(int)reader[5] - (decimal)(int)reader[7]) * (decimal)12 / drawPerBag;
                    }
                    else
                    {
                        consumedBags2 += ((decimal)(int)reader[5] - (decimal)(int)reader[7]) * (decimal)12 / drawPerBag;
                    }
                }
            }

            reader.Close();
            txtConsumedBags1.Text = consumedBags1.ToString("N0");
            txtConsumedBags2.Text = consumedBags2.ToString("N0");
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
		private void BagMakingProductionFormFormClosing(object sender, FormClosingEventArgs e)
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
            string unwindNo = "1";

            if (dgvInputs.Rows.Count > 1)
            {
                OptionsForm pickUnwindForm = new OptionsForm("Unwind", false, true);
                for (int i = 0; i < dgvInputs.Rows.Count; i++)
                {
                    pickUnwindForm.AddOption(dgvInputs[0, i].Value.ToString());
                }

                pickUnwindForm.ShowDialog();
                unwindNo = pickUnwindForm.Option;
                pickUnwindForm.Dispose();
            }

            if (unwindNo != "Abort")
            {
                GetInputForm getRollToConsumeForm = new GetInputForm("Scan/Input Roll to Consume Barcode", "R", 0, 0, true);
                getRollToConsumeForm.ShowDialog();
                if (getRollToConsumeForm.UserInput.Length > 0)
                {
                    command = new SqlCommand("SELECT a.[Master Item No], a.[Width], f.[Description], ISNULL(a.[Location ID], d.[Location ID]), CAST(a.[Current LF] AS int), CAST(ROUND(a.[Original Pounds] * a.[Current LF] / a.[Original LF], 0) AS int), e.[Vendor Roll ID], g.[Description], g.[Inventory Available], b.[Allocated LF], f.[Reference Item No], CAST(f.[Item Type No] AS int), a.[Create Date], j.[Reference Item No], j.[Description], h.[Start Usage Date], h.[Start Usage LF] FROM [Roll Table] a LEFT JOIN ([Allocation Pick Table] b INNER JOIN [Allocation Master Table] c ON b.[Allocation ID] = c.[Allocation ID] AND c.[Master Item No] = " + prodMasterItemNumber + " AND c.[Pick Date] IS NOT NULL AND c.[Release Date] IS NULL AND c.[Void Date] IS NULL) ON a.[Roll ID] = b.[Roll ID] LEFT JOIN [Pallet Table] d ON a.[Pallet ID] = d.[Pallet ID] LEFT JOIN [Roll PO Table] e ON a.[Roll ID] = e.[Roll ID] INNER JOIN [Inventory Master Table] f ON a.[Master Item No] = f.[Master Item No] LEFT JOIN ([Production Consumed Roll Table] h INNER JOIN ([Production Master Table] i INNER JOIN [Inventory Master Table] j ON i.[Master Item No] = j.[Master Item No]) ON h.[Start Production ID] != " + currentProductionRecord.ToString() + " AND h.[Start Production ID] = i.[Production ID] AND i.[Machine No] = " + MainForm.MachineNumber + " AND h.[End Usage Date] IS NULL AND h.[Start Usage Date] > DATEADD(hh, -12, GETDATE())) ON a.[Roll ID] = h.[Roll ID], [Location Table] g WHERE a.[Original LF] > 0 AND ISNULL(a.[Location ID], d.[Location ID]) = g.[Location ID] AND a.[Roll ID] = " + getRollToConsumeForm.UserInput.Substring(1), connection1);
                    connection1.Open();
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {

                        Boolean consumeRoll = false;
                        int itemType = 0;
                        string inputMasterItemNo = string.Empty;
                        int inputJobNumber = 0;
                        string inputDescription = string.Empty;
                        string UPCCode = string.Empty;
                        // Get Master Item No for Unwind
                        foreach (DataGridViewRow row in dgvInputs.Rows)
                        {
                            if (row.Cells[0].Value.ToString().Equals(unwindNo))
                            {
                                itemType = (int)row.Cells["Item Type"].Value;
                                inputMasterItemNo = row.Cells["Master Item No"].Value.ToString();
                                inputJobNumber = (int)row.Cells["Job No"].Value;
                                inputDescription = row.Cells["Description"].Value.ToString();
                                UPCCode = row.Cells["UPC Code"].Value.ToString();
                                break;
                            }
                        }

                        if ((int)reader[11] != 1 && (int)reader[11] != 2 && (int)reader[11] != 3)
                        {
                            MessageBox.Show("Error - roll " + getRollToConsumeForm.UserInput + " " + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + " is not a raw film nor WIP Roll and therefore cannot be consumed", "Invalid Roll");
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
                        else
                        {
                            bool inputRollOK = true;
                            string overrideAuthorizedBy = string.Empty;
                            if (!string.IsNullOrEmpty(UPCCode))
                            {
                                inputRollOK = ModulesClass.ValidateUPC(jobNumber, getRollToConsumeForm.UserInput, UPCCode, string.Empty, out overrideAuthorizedBy);
                            }

                            if (inputRollOK)
                            {
                                DialogResult answer = DialogResult.No;
                                string emailHeader = string.Empty;
                                string emailMessage = string.Empty;
                                if ((itemType == 2 || itemType == 3) && reader[0].ToString() != inputMasterItemNo)
                                {
                                    answer = MessageBox.Show("WIP Roll " + getRollToConsumeForm.UserInput + " of Job J" + reader[10].ToString() + " - " + reader[2].ToString() + " is not a valid input roll for unwind " + unwindNo + " of job " + jobNumber + ".  Do you wish to consume roll anyway (NOT ADVISED)?", "Override? (Requires Supervisor Authorization)", MessageBoxButtons.YesNo);
                                    emailHeader = "Different Job's WIP Roll Consumed";
                                    emailMessage = "On line " + MainForm.MachineNumber + " unwind " + unwindNo + " operator " + operatorName + " just consumed WIP roll " + getRollToConsumeForm.UserInput + " of job " + reader[10].ToString() + " - " + reader[2].ToString() + " for job " + jobNumber + " - " + jobDescription + ".  ";
                                }
                                else if ((int)reader[11] == 1 && reader[9] == DBNull.Value)
                                {
                                    //This film roll is not allocated to this job
                                    command = new SqlCommand("SELECT COUNT(*) FROM [Allocation Master Table] a INNER JOIN [Allocation Reservation Table] b ON a.[Allocation ID] = b.[Allocation ID] WHERE a.[Void Date] IS NULL AND a.[Master Item no] = " + prodMasterItemNumber + " AND b.[Master Item No] = " + reader[0].ToString() + " AND b.[Width] = " + reader[1].ToString(), connection2);
                                    connection2.Open();
                                    int likeRollsAllocated = (int)command.ExecuteScalar();
                                    if (likeRollsAllocated == 0)
                                    {
                                        if (reader[0].ToString() != inputMasterItemNo)
                                        {
                                            answer = MessageBox.Show("Error - roll " + getRollToConsumeForm.UserInput + " " + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + " is not the same film and/or width of any films allocated to job " + jobNumber + ".\r\nDo you wish to consume this roll anyway (NOT ADVISED)?", "Override? (Requires Supervisor Authorization)", MessageBoxButtons.YesNo);
                                            emailHeader = "Unallocated DIFFERENT Film Roll Consumed";
                                            emailMessage = "On line " + MainForm.MachineNumber.ToString() + " operator " + operatorName + " just consumed unallocated film roll " + getRollToConsumeForm.UserInput + "(" + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + ") for job " + jobNumber + " - " + jobDescription + ".  ";
                                        }
                                        else
                                        {
                                            answer = MessageBox.Show("Error - roll " + getRollToConsumeForm.UserInput + " " + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + " is the correct film and width that job " + jobNumber + " specifies but there is no allocation for this job.\r\nDo you wish to consume this roll anyway?", "Override?", MessageBoxButtons.YesNo);
                                            if (answer == DialogResult.Yes)
                                            {
                                                emailHeader = "Unallocated CORRECT Film Roll Consumed";
                                                emailMessage = "On line " + MainForm.MachineNumber.ToString() + " operator " + operatorName + " just consumed unallocated film roll " + getRollToConsumeForm.UserInput + "(" + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + ") for job " + jobNumber + " - " + jobDescription + ".";
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
                                                ModulesClass.SendEmail(2, "Allocated Film Roll Consumed Under Different Job", "Roll " + getRollToConsumeForm.UserInput + " " + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + " was consumed by job " + jobNumber + " - " + jobDescription + ".  It was the correct film but was allocated to job " + reader2[0].ToString() + " - " + reader2[1].ToString() + " on " + ((DateTime)reader2[2]).ToShortDateString() + " at " + ((DateTime)reader2[2]).ToShortTimeString());
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
                                            ModulesClass.SendEmail(2, "Possible uncured Lamination Roll Consumed", "On line " + MainForm.MachineNumber.ToString() + " operator " + operatorName + " just consumed film roll " + getRollToConsumeForm.UserInput + "(" + reader[2].ToString() + ") for job " + jobNumber + "(" + jobDescription + ") that was created on " + ((DateTime)reader[12]).ToShortDateString() + " at " + ((DateTime)reader[12]).ToShortTimeString() + ",  The override was authorized by " + authorizedBy + ".");
                                        }
                                    }
                                }
                            }

                            if (consumeRoll)
                            {
                                if ((int)reader[11] == 1 && reader[6] == DBNull.Value)
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
                                }

                                consumedFilmForm.AddRoll(operatorName, DateTime.Now, getRollToConsumeForm.UserInput.Substring(1), "Consumed", unwindNo, ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString(), (int)reader[4]);
                                for (int i = 0; i < dgvInputs.Rows.Count; i++)
                                {
                                    if (dgvInputs["Unwind", i].Value.ToString() == unwindNo)
                                    {
                                        dgvInputs["Job No", i].Value = (int)reader[10];
                                        dgvInputs["Description", i].Value = reader[2].ToString();
                                        dgvInputs["Roll ID", i].Value = getRollToConsumeForm.UserInput;
                                        dgvInputs["Bags", i].Value = (decimal)(int)reader[4] * (decimal)12 / drawPerBag;
                                        dgvInputs["Roll Width", i].Value = (decimal)reader[1];
                                        break;
                                    }
                                }

                                if (!string.IsNullOrEmpty(overrideAuthorizedBy))
                                {
                                    ModulesClass.SendEmail(2, "Manual UPC Verification", "The UPC code of input Roll " + getRollToConsumeForm.UserInput + " on unwind " + unwindNo + " for job " + jobNumber + " - " + jobDescription + " on line " + MainForm.MachineNumber + " was manually input.  The override was authorized by " + overrideAuthorizedBy + ".");
                                }

                                consumedBags += ((decimal)(int)reader[4] * (decimal)12 / drawPerBag) / (decimal)numberLanes;
                                if (unwindNo == "1")
                                {
                                    consumedBags1 += (decimal)(int)reader[4] * (decimal)12 / drawPerBag;
                                }
                                else
                                {
                                    consumedBags2 += (decimal)(int)reader[4] * (decimal)12 / drawPerBag;
                                }

                                txtConsumedBags1.Text = consumedBags1.ToString("N0");
                                txtConsumedBags2.Text = consumedBags2.ToString("N0");
                                command = new SqlCommand("INSERT INTO [Production Consumed Roll Table] SELECT " + currentProductionRecord.ToString() + ", " + unwindNo + ", " + getRollToConsumeForm.UserInput.Substring(1) + ", '" + DateTime.Now.ToString() + "', " + reader[4].ToString() + ", NULL, NULL, NULL", connection2);
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
                            else
                            {
                                MessageBox.Show("THe UPC Code of Roll" + getRollToConsumeForm.UserInput + " was not validated.  You cannot consume a roll without it.", "Consumption Aborted");
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
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void CmdReturnRollClick(object sender, EventArgs e)
		{
            GetInputForm getRollToReturnForm = new GetInputForm("Scan/Input Roll to Return Barcode", "R", 0, 0, true);
            getRollToReturnForm.ShowDialog();
            if (getRollToReturnForm.UserInput.Length > 0)
            {
                command = new SqlCommand("SELECT TOP 1 b.[Master Item No], b.[Width], c.[Description], d.[Machine No], CAST(a.[Start Usage LF] AS int), CAST(ISNULL(a.[End Usage LF], 0) AS int), a.[Start Usage Date], CAST(a.[Unwind No] AS int),	a.[Start Production ID], e.[Reference Item No],	e.[Description] FROM [Production Consumed Roll Table] a INNER JOIN ([Roll Table] b INNER JOIN [Inventory Master Table] c ON b.[Master Item No] = c.[Master Item No]) ON a.[Roll ID] = b.[Roll ID] INNER JOIN ([Production Master Table] d INNER JOIN [Inventory Master Table] e ON d.[Master Item No] = e.[Master Item No]) ON a.[Start Production ID] = d.[Production ID] WHERE a.[Start Usage LF] != ISNULL(a.[End Usage LF], 0) AND (a.[Start Production ID] = " + currentProductionRecord.ToString() + " OR (d.[Machine No] = " + MainForm.MachineNumber + " AND a.[Start Usage Date] > DATEADD(hh, -12, GETDATE()) AND a.[End Usage Date] IS NULL)) AND a.[Roll ID] = " + getRollToReturnForm.UserInput.Substring(1) + " ORDER BY a.[Start Usage Date] DESC", connection1);
                connection1.Open();
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    GetInputForm returnBagsForm = new GetInputForm("Return Bags", "#", 1, (int)Math.Round((decimal)(int)reader[4] * (decimal)12 / drawPerBag, 0), false);
                    returnBagsForm.ShowDialog();
                    if (returnBagsForm.UserInput.Length > 0)
                    {
                        DialogResult answer = DialogResult.Yes;
                        if ((int)reader[5] != 0)
                        {
                            // There is already a return recorded
                            if ((int)Math.Round((decimal)(int)reader[5] * (decimal)12 / drawPerBag, 0) > int.Parse(returnBagsForm.UserInput, NumberStyles.Number))
                            {
                                MessageBox.Show("You last returned roll " + getRollToReturnForm.UserInput + " " + reader[2].ToString() + " for " + Math.Round((decimal)(int)reader[5] * (decimal)12 / drawPerBag, 0).ToString("N0") + " Bags.  You need to re-consume the roll then return " + int.Parse(returnBagsForm.UserInput, NumberStyles.Number).ToString("N0") + " Bags iF the roll hasn't already been consumed by another job.", "Can't decrease a Return's Amount");
                                answer = DialogResult.No;
                            }
                            else
                            {
                                answer = MessageBox.Show("Roll " + getRollToReturnForm.UserInput + " " + reader[2].ToString() + " has already been returned for " + Math.Round((decimal)(int)reader[5] * (decimal)12 / drawPerBag, 0).ToString("N0") + " Bags.  Do you wish to overwrite the return?", "Change Return Amount?", MessageBoxButtons.YesNo);
                            }
                        }
                        else if ((int)reader[8] != currentProductionRecord)
                        {
                            if (reader[9].ToString() == jobNumber)
                            {
                                answer = MessageBox.Show("Roll " + getRollToReturnForm.UserInput + " was consumed by this job but under a different production record on " + ((DateTime)reader[6]).ToShortDateString() + " at " + ((DateTime)reader[6]).ToShortTimeString() + ".  Do you still wish to return (the return will not affect usage on the current production record)?", "Still Return Roll?", MessageBoxButtons.YesNo);
                            }
                            else
                            {
                                answer = MessageBox.Show("Roll " + getRollToReturnForm.UserInput + " was consumed by job " + reader[9].ToString() + " - " + reader[10].ToString() + " on " + ((DateTime)reader[6]).ToShortDateString() + " at " + ((DateTime)reader[6]).ToShortTimeString() + ".  Do you still with to return (the return will not affect usage on the current production record)?", "Still Return Roll?", MessageBoxButtons.YesNo);
                            }
                        }

                        if (answer == DialogResult.Yes)
                        {
                            if ((int)reader[8] == currentProductionRecord)
                            {
                                if ((int)reader[7] == 1)
                                {
                                    if (consumedBags1 - decimal.Parse(returnBagsForm.UserInput, NumberStyles.Number) < (decimal)producedBags - (decimal)1)
                                    {
                                        answer = MessageBox.Show("The calculated maximum Bags you can return is " + (consumedBags1 - (decimal)producedBags).ToString("N0") + ".  Do you still wish to return " + int.Parse(returnBagsForm.UserInput, NumberStyles.Number).ToString("N0") + " Bags?", "Override Maximum Return Amount?", MessageBoxButtons.YesNo);
                                        if (answer == DialogResult.Yes)
                                        {
                                            ModulesClass.SendEmail(5, "Return Roll Exception", "On line " + MainForm.MachineNumber.ToString() + " operator " + operatorName + " just returned roll " + getRollToReturnForm.UserInput + " for job " + jobNumber + "(" + jobDescription + ") in the amount of " + int.Parse(returnBagsForm.UserInput, NumberStyles.Number).ToString("N0") + " bags when the bags available to return were " + (consumedBags1 - (decimal)producedBags).ToString("N0") + ".");
                                        }
                                    }
                                }
                                else
                                {
                                    if (consumedBags2 - decimal.Parse(returnBagsForm.UserInput, NumberStyles.Number) < (decimal)producedBags - (decimal)1)
                                    {
                                        answer = MessageBox.Show("The calculated maximum Bags you can return is " + (consumedBags2 - (decimal)producedBags).ToString("N0") + ".  Do you still wish to return " + int.Parse(returnBagsForm.UserInput, NumberStyles.Number).ToString("N0") + " Bags?", "Override Maximum Return Amount?", MessageBoxButtons.YesNo);
                                        if (answer == DialogResult.Yes)
                                        {
                                            ModulesClass.SendEmail(5, "Return Roll Exception", "On line " + MainForm.MachineNumber.ToString() + " operator " + operatorName + " just returned roll " + getRollToReturnForm.UserInput + " for job " + jobNumber + "(" + jobDescription + ") in the amount of " + int.Parse(returnBagsForm.UserInput, NumberStyles.Number).ToString("N0") + " bags when the bags available to return were " + (consumedBags2 - (decimal)producedBags).ToString("N0") + ".");
                                        }
                                    }
                                }
                            }

                            if (answer == DialogResult.Yes)
                            {
                                command = new SqlCommand("UPDATE [Production Consumed Roll Table] SET [End Production ID] = " + reader[8].ToString() + ", [End Usage Date] = '" + DateTime.Now.ToString() + "', [End Usage LF] = " + Math.Round(decimal.Parse(returnBagsForm.UserInput, NumberStyles.Number) * drawPerBag / (decimal)12, 0).ToString() + " WHERE [Roll ID] = " + getRollToReturnForm.UserInput.Substring(1) + " AND [Start Usage Date] = '" + ((DateTime)reader[6]).ToString() + "' AND [Start Production ID] = " + reader[8].ToString(), connection2);
                                //command = new SqlCommand("UPDATE [Production Consumed Roll Table] SET [End Production ID] = " + currentProductionRecord.ToString() + ", [End Usage Date] = '" + DateTime.Now.ToString() + "', [End Usage LF] = " + returnLFForm.UserInput + " WHERE [Roll ID] = " + getRollToReturnForm.UserInput.Substring(1) + " AND [Start Usage Date] = '" + ((DateTime)reader[6]).ToString() + "'", connection2);
                                connection2.Open();
                                command.ExecuteNonQuery();
                                connection2.Close();
                                if ((int)reader[8] == currentProductionRecord)
                                {
                                    consumedFilmForm.AddRoll(operatorName, DateTime.Now, getRollToReturnForm.UserInput.Substring(1), "Returned", reader[9].ToString(), ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString(), (int)Math.Round(decimal.Parse(returnBagsForm.UserInput, NumberStyles.Number) * drawPerBag / (decimal)12, 0));
                                    for (int i = 0; i < dgvInputs.Rows.Count; i++)
                                    {
                                        if ((int)dgvInputs["Unwind", i].Value == (int)reader[7])
                                        {
                                            if (dgvInputs["Roll ID", i].Value.ToString() == getRollToReturnForm.UserInput)
                                            {
                                                dgvInputs["Job No", i].Value = dgvInputs["Default Job No", i].Value;
                                                dgvInputs["Description", i].Value = dgvInputs["Default Description", i].Value;
                                                dgvInputs["Roll ID", i].Value = string.Empty;
                                                dgvInputs["Bags", i].Value = DBNull.Value;
                                                dgvInputs["Roll Width", i].Value = DBNull.Value;
                                            }

                                            break;
                                        }
                                    }

                                    consumedBags -= decimal.Parse(returnBagsForm.UserInput, NumberStyles.Number) / (decimal)numberLanes;
                                    if ((int)reader[7] == 1)
                                    {
                                        consumedBags1 -= decimal.Parse(returnBagsForm.UserInput, NumberStyles.Number);
                                    }
                                    else
                                    {
                                        consumedBags2 -= decimal.Parse(returnBagsForm.UserInput, NumberStyles.Number);
                                    }

                                    txtConsumedBags1.Text = consumedBags1.ToString("N0");
                                    txtConsumedBags2.Text = consumedBags2.ToString("N0");
                                    if (decimal.Parse(returnBagsForm.UserInput, NumberStyles.Number) > 0)
                                    {
                                        PrintClass.Label(getRollToReturnForm.UserInput);
                                        //MessageBox.Show("You cannot print a return roll label on your label printer.  Please have someone print out the return label for you.");
                                    }
                                }
                                else
                                {
                                    ModulesClass.SendEmail(5, "Roll Returned after Production Record " + reader[8].ToString() + " closed", "On line " + MainForm.MachineNumber.ToString() + " operator " + operatorName + " just returned roll " + getRollToReturnForm.UserInput + " for job " + reader[9].ToString() + " - " + reader[10].ToString() + " in the amount of " + int.Parse(returnBagsForm.UserInput, NumberStyles.Number).ToString("N0") + " Bags.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Roll return aborted");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Roll return aborted");
                        }

                        returnBagsForm.Dispose();
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
				
				connection1.Open();
				foreach(DataGridViewRow row in dgvInputs.Rows)
				{
					if((int)row.Cells["Item Type"].Value == 2)
					{
						command = new SqlCommand("SELECT a.[Roll ID], b.[Description] FROM [Roll Table] a INNER JOIN [Location Table] b ON a.[Location ID] = b.[Location ID] WHERE b.[Inventory Available] = 1 AND [Current LF] > 0 AND [Master Item No] = " + row.Cells["Master Item No"].Value.ToString(), connection1);
						reader = command.ExecuteReader();
						if (reader.Read())
						{
							MessageBox.Show("Error - you cannont complete this job.  WIP Roll R" + reader[0].ToString() + " is still in inventory in location + " + reader[1].ToString() + ". This roll must be consumed in order to mark the job complete.", "Not all input WIP rolls used");
							reader.Close();
							result = DialogResult.No;
							break;
						}
						
						reader.Close();
					}
				}
				
				connection1.Close();
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
                        command = new SqlCommand("SELECT d.[Standard Square Inches] / (d.[Repeat] * d.[Stream Width]), CAST(ISNULL(SUM(c.[Bags]), 0) AS decimal(9, 2)) FROM [Production Master Table] a LEFT JOIN ([Production Case Table] b INNER JOIN [Case Table] c ON b.[Case ID] = c.[Case ID]) ON a.[Production ID] = b.[Production ID] INNER JOIN [Finished Goods Specification Table] d on a.[Master Item No] = d.[Input Master Item No] WHERE a.[Master Item No] = " + prodMasterItemNumber + " GROUP BY d.[Standard Square Inches] / (d.[Repeat] * d.[Stream Width])", connection1);
                        reader = command.ExecuteReader();
                        reader.Read();
                        if ((decimal)reader[0] * (decimal).95 > (decimal)reader[1])
                        {
                            result = MessageBox.Show("You have only produced " + ((decimal)reader[1]).ToString("N0") + " bags of the " + ((decimal)reader[0]).ToString("N0") + " bags expected for this job.  Do you still wish to mark the job complete?", "Job run Short", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                string authorizedBy = string.Empty;
                                bool authorized = ModulesClass.Authorized(false, ref authorizedBy);
                                if (authorized)
                                {
                                    result = DialogResult.Yes;
                                    ModulesClass.SendEmail(2, "Job " + jobNumber + " (" + jobDescription + ")" + " was run short", "On line " + MainForm.MachineNumber.ToString() + " operator " + operatorName + " completed the job with " + ((decimal)reader[1]).ToString("N0") + " bags of the " + ((decimal)reader[0]).ToString("N0") + " bags expected.  The override was authorized by " + authorizedBy + ".");
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
            if (producedBags > 0 && decimal.Parse(txtRunHours.Text, NumberStyles.Number) == 0)
            {
                MessageBox.Show("Error - you must enter at least 0.25 run hours since you have production.", "Invalid Run Hours");
            }
            else
            {
                int endOutputBags = 0;
                DialogResult answer = DialogResult.Yes;

                if (reasonCode != "3")
                {
                    answer = MessageBox.Show("Have you recorded all the scrap for this job?", "Scrap In?", MessageBoxButtons.YesNo);
                }

                if (answer == DialogResult.Yes)
                {
                    if (reasonCode == "2")
                    {
                        decimal unwindEndBags = 0;

                        GetInputForm partialCaseorRollForm = new GetInputForm("Partial Case Bags", "#", 0, 0, false);
                        if (decimal.Parse(txtRunHours.Text, NumberStyles.Number) > 0 || incomingBags > 0)
                        {
                            partialCaseorRollForm.ShowDialog();
                            if (partialCaseorRollForm.UserInput.Length > 0)
                            {
                                endOutputBags = int.Parse(partialCaseorRollForm.UserInput, NumberStyles.Number);
                            }
                        }

                        answer = MessageBox.Show("Is there any material on the unwind(s) that you have not returned?", "Record Unconsumed End of Shift Roll(s)", MessageBoxButtons.YesNo);
                        if (answer == DialogResult.Yes)
                        {
                            partialCaseorRollForm.NewTitle = "Unwind 1 Bags";
                            foreach (DataGridViewRow row in dgvInputs.Rows)
                            {
                                if (!string.IsNullOrEmpty(row.Cells["Roll ID"].ToString()))
                                {
                                    if (dgvInputs.Rows.Count > 1)
                                    {
                                        partialCaseorRollForm.NewTitle = "Unwind " + row.Cells["Unwind"].Value.ToString() + " Bags";
                                    }

                                    partialCaseorRollForm.MaxValue = (int)row.Cells["Bags"].Value;
                                    partialCaseorRollForm.ShowDialog();
                                    if (!string.IsNullOrEmpty(partialCaseorRollForm.UserInput) && int.Parse(partialCaseorRollForm.UserInput, NumberStyles.Number) > 0)
                                    {
                                        if ((int)row.Cells["Unwind"].Value == 1)
                                        {
                                            if (consumedBags1 - decimal.Parse(partialCaseorRollForm.UserInput, NumberStyles.Number) < (decimal)producedBags + (decimal)endOutputBags - (decimal)1)
                                            {
                                                answer = MessageBox.Show("The calculated maximum Bags you can return is " + (consumedBags1 - ((decimal)producedBags + (decimal)endOutputBags)).ToString("N0") + ".  Do you still wish to return " + int.Parse(partialCaseorRollForm.UserInput, NumberStyles.Number).ToString("N0") + " Bags?", "Override Maximum Return Amount?", MessageBoxButtons.YesNo);
                                                if (answer == DialogResult.Yes)
                                                {
                                                    ModulesClass.SendEmail(5, "Return Roll Exception", "On line " + MainForm.MachineNumber.ToString() + " operator " + operatorName + " just returned roll " + row.Cells["Roll ID"].ToString() + " for job " + jobNumber + "(" + jobDescription + ") in the amount of " + int.Parse(partialCaseorRollForm.UserInput, NumberStyles.Number).ToString("N0") + " bags when the bags available to return were " + (consumedBags1 - ((decimal)producedBags + (decimal)endOutputBags)).ToString("N0") + ".");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (consumedBags2 - decimal.Parse(partialCaseorRollForm.UserInput, NumberStyles.Number) < (decimal)producedBags + (decimal)endOutputBags - (decimal)1)
                                            {
                                                answer = MessageBox.Show("The calculated maximum Bags you can return is " + (consumedBags2 - ((decimal)producedBags + (decimal)endOutputBags)).ToString("N0") + ".  Do you still wish to return " + int.Parse(partialCaseorRollForm.UserInput, NumberStyles.Number).ToString("N0") + " Bags?", "Override Maximum Return Amount?", MessageBoxButtons.YesNo);
                                                if (answer == DialogResult.Yes)
                                                {
                                                    ModulesClass.SendEmail(5, "Return Roll Exception", "On line " + MainForm.MachineNumber.ToString() + " operator " + operatorName + " just returned roll " + row.Cells["Roll ID"].ToString() + " for job " + jobNumber + "(" + jobDescription + ") in the amount of " + int.Parse(partialCaseorRollForm.UserInput, NumberStyles.Number).ToString("N0") + " bags when the bags available to return were " + (consumedBags2 - ((decimal)producedBags + (decimal)endOutputBags)).ToString("N0") + ".");
                                                }
                                            }
                                        }

                                        if (answer == DialogResult.Yes)
                                        {
                                            command = new SqlCommand("execute [dbo].[Save Shift Change Unwind Footage Stored Procedure] " + currentProductionRecord.ToString() + ", " + row.Cells["Unwind"].Value.ToString() + ", 1, " + row.Cells["Roll ID"].Value.ToString().Substring(1) + ", " + (decimal.Parse(partialCaseorRollForm.UserInput) * drawPerBag / (decimal)12).ToString(), connection1);
                                            connection1.Open();
                                            command.ExecuteNonQuery();
                                            connection1.Close();
                                            unwindEndBags += (decimal.Parse(partialCaseorRollForm.UserInput, NumberStyles.Number) * drawPerBag / 12) / (decimal)numberLanes;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Roll return aborted");
                                            answer = DialogResult.Yes;
                                        }
                                    }
                                }
                            }
                        }

                        partialCaseorRollForm.Dispose();
                    }

                    JobToDateStatsForm productionSummaryForm = new JobToDateStatsForm(currentProductionRecord.ToString(), false, endOutputBags.ToString(), txtSetupHours.Text, txtRunHours.Text, txtDownTimeHours.Text);
                    productionSummaryForm.ShowDialog();
                    if (productionSummaryForm.Save)
                    {
                        productionSummaryForm.Dispose();
                        productionNotesForm.Hide();
                        productionNotesForm.WindowState = FormWindowState.Normal;
                        productionNotesForm.ShowDialog();
                        if (productionNotesForm.Comment.Length > 0)
                        {
                            command = new SqlCommand("UPDATE [Production Master Table] SET [Setup Hrs] = " + txtSetupHours.Text + ", [Run Hrs] = " + txtRunHours.Text + ", [DT Hrs] = " + txtDownTimeHours.Text + ", [End Reason ID] = " + reasonCode + ", [End Output LF] = " + endOutputBags.ToString() + ", [Notes] = '" + productionNotesForm.Comment.Replace("'", "''") + "' WHERE [Production ID] = " + currentProductionRecord.ToString(), connection1);
                        }
                        else
                        {
                            command = new SqlCommand("UPDATE [Production Master Table] SET [Setup Hrs] = " + txtSetupHours.Text + ", [Run Hrs] = " + txtRunHours.Text + ", [DT Hrs] = " + txtDownTimeHours.Text + ", [End Reason ID] = " + reasonCode + ", [End Output LF] = " + endOutputBags.ToString() + ", [Notes] = NULL WHERE [Production ID] = " + currentProductionRecord.ToString(), connection1);
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
                        this.FormClosing -= BagMakingProductionFormFormClosing;
                        this.Close();
                    }
                    else
                    {
                        productionSummaryForm.Dispose();
                    }
                }
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
				command = new SqlCommand("SELECT CAST(d.[Reference Item No] as nvarchar(10)) + ' - ' + d.[Description], COUNT(a.[Case ID]), SUM(a.[Original Pounds]) + b.[Blank Weight] FROM [Case Table] a INNER JOIN [Current Pallets at Machine Table] b on a.[Pallet ID] = b.[Pallet ID] INNER JOIN [Pallet Table] c on a.[Pallet ID] = c.[Pallet ID] INNER JOIN [Inventory Master Table] d on a.[Master Item No] = d.[Master Item No] WHERE a.[Pallet ID] = " + readBarcodeForm.UserInput.Substring(1) + " AND c.[Location ID] = " + MainForm.MachineNumber + " GROUP BY CAST(d.[Reference Item No] as nvarchar(10)) + ' - ' + d.[Description], b.[Blank Weight]", connection1);
				connection1.Open();
				reader = command.ExecuteReader();
				if (reader.Read())
				{
					DialogResult answer;
					if ((int)reader[1] == 1)
					{
						answer = MessageBox.Show("There is 1 case available for Job " + reader[0].ToString() + ".  Is this the number or cases you wish to palletize?", "Confirm Number of Cases", MessageBoxButtons.YesNo);
					}
					else
					{
						answer = MessageBox.Show("There are " + ((int)reader[1]).ToString("N0") + " cases available for Job " + reader[0].ToString() + ".  Is this the number or cases you wish to palletize?", "Confirm Number of Cases", MessageBoxButtons.YesNo);
					}
					
					if (answer == DialogResult.Yes)
					{
						command = new SqlCommand("UPDATE [Pallet Table] SET [Weight] = " + (decimal)reader[2] + " where [Pallet ID] = " + readBarcodeForm.UserInput.Substring(1), connection1);
						reader.Close();
						command.ExecuteNonQuery();
						command = new SqlCommand("DELETE FROM [Current Pallets at Machine Table] WHERE [Pallet ID] = " + readBarcodeForm.UserInput.Substring(1), connection1);
						command.ExecuteNonQuery();
						currentPalletNumber = 0;
						FillCaseText();
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
		private void FillCaseText()
		{
			producedBags = 0;
			rtbCreatedBagInfo.Text = string.Empty;
			rtbCurrentPalletProduction.Text = string.Empty;
			command = new SqlCommand("SELECT 'P' + CAST(c.[Pallet ID] AS nvarchar(10)) + ' @ ' + e.[Description], COUNT(a.[Case ID]), SUM([Bags]) FROM [Production Case Table] a INNER JOIN [Production Master Table] b ON a.[Production ID] = b.[Production ID] INNER JOIN ([Case Table] c INNER JOIN ([Pallet Table] d INNER JOIN [Location Table] e ON d.[Location ID] = e.[Location ID] LEFT JOIN [Current Pallets at Machine Table] f ON d.[Pallet ID] = f.[Pallet ID]) ON c.[Pallet ID] = d.[Pallet ID]) ON a.[Case ID] = c.[Case ID] WHERE(a.[Production ID] = " + currentProductionRecord.ToString() + " OR (d.[Location ID] = " + MainForm.MachineNumber + " AND b.[Master Item No] = " + prodMasterItemNumber + ")) AND c.[Bags] != 0 AND f.[Pallet ID] IS NULL GROUP BY 'P' + CAST(c.[Pallet ID] AS nvarchar(10)) + ' @ ' + e.[Description] ORDER BY 'P' + CAST(c.[Pallet ID] AS nvarchar(10)) + ' @ ' + e.[Description]", connection1);
			reader = command.ExecuteReader();
			while (reader.Read())
			{
				rtbCreatedBagInfo.Text += reader[0].ToString() + " - " + ((int)reader[1]).ToString("N0") + " cases, " + ((int)reader[2]).ToString("N0") + " bags\r\n";
			}
			
			reader.Close();
			command = new SqlCommand("SELECT a.[Pallet ID], COUNT(b.[Case ID]), SUM(b.[Bags]) FROM [Current Pallets At Machine Table] a LEFT JOIN [Case Table] b ON a.[Pallet ID] = b.[Pallet ID] INNER JOIN [Pallet Table] c ON a.[Pallet ID] = c.[Pallet ID] WHERE a.[Master Item No] = " + prodMasterItemNumber + " AND c.[Location ID] = " + MainForm.MachineNumber + " GROUP BY a.[Pallet ID]", connection1);
			reader = command.ExecuteReader();
			if (reader.Read())
			{
                currentPalletNumber = (int)reader[0];
                lblCurrentPalletProduction.Text = "Current Pallet";
                if ((int)reader[1] > 0)
                {
                    lblProductionExclCurrentPallet.Text = "Production excl Current Pallet";
                    rtbCurrentPalletProduction.Text = "P" + reader[0].ToString() + "\r\nCases: " + ((int)reader[1]).ToString("N0") + "\r\nBags: " + ((int)reader[2]).ToString("N0");
                }
                else
                {
                    lblProductionExclCurrentPallet.Text = "Production";
                    rtbCurrentPalletProduction.Text = "P" + reader[0].ToString() + "\r\nCases: 0 + \r\nBags: 0";
                }
                cmdGetPallet.Enabled = false;
                cmdClosePallet.Enabled = true;
            }
            else
            { 
				currentPalletNumber = 0;
                cmdGetPallet.Enabled = true;
                cmdClosePallet.Enabled = false;
                lblProductionExclCurrentPallet.Text = "Production";
                lblCurrentPalletProduction.Text = string.Empty;
            }

            reader.Close();
            command = new SqlCommand("SELECT ISNULL(SUM(b.[Bags]), 0) FROM [Production Case Table] a INNER JOIN [Case Table] b ON a.[Case ID] = b.[Case ID] WHERE a.[Production ID] = " + currentProductionRecord.ToString(), connection1);
            producedBags = (int)command.ExecuteScalar() - incomingBags;
            txtProducedBags.Text = producedBags.ToString("N0");
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		private void RtbTextChanged(object sender, EventArgs e)
		{
			((RichTextBox)sender).SelectionStart = ((RichTextBox)sender).Text.Length;
			((RichTextBox)sender).ScrollToCaret();
		}
		
		private void CmdReprintLabelClick(object sender, EventArgs e)
		{
			GetInputForm getRollNumberForm = new GetInputForm("Scan/Input Roll or Case ID", "R", 0, 0, false);
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
				currentJobHistoryForm.Focus();
			}
			else
			{
				currentJobHistoryForm = new JobHistoryForm(jobNumber, prodMasterItemNumber);
				currentJobHistoryForm.Show();
			}
		}
		
		private void BagMakingProductionFormFormClosed(object sender, FormClosedEventArgs e)
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
		
		private void FillLooseCaseText()
		{
			rtbLooseCases.Text = string.Empty;
			command = new SqlCommand("SELECT 'C' + CAST(a.[Case ID] AS nvarchar(10)) + ' Job ' + CAST(b.[Reference Item No] AS nvarchar(10)) + ' (' + b.[Description] + ')' FROM [Case Table] a INNER JOIN [Inventory Master Table] b ON a.[Master Item No] = b.[Master Item No] WHERE a.[Bags] != 0 AND a.[Pallet ID] IS NULL AND b.[Reference Item No] % 100 = 51 AND b.[Item Type No] = 2 AND a.[Location ID] = " + MainForm.MachineNumber, connection1);
			reader = command.ExecuteReader();
			if (reader.Read())
		    {
			    int i = 0;
			    do
			    {
			    	rtbLooseCases.Text += reader[0].ToString() + "\r\n\r\n";
			    	i++;
			    }
			    while (reader.Read());
			    if (i == 1)
			    {
			    	lblLooseCases.Text = "Loose Case";
			    }
			    else
			    {
			    	lblLooseCases.Text = "Loose Cases";
			    }
		    }
			else
			{
				lblLooseCases.Text = string.Empty;
			}
			
			reader.Close();
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void CmdGetPalletClick(object sender, EventArgs e)
		{
			GetInputForm newPalletWeightForm = new GetInputForm("Enter Blank Pallet Weight", "#", 20, 80, false);
			newPalletWeightForm.ShowDialog();
			if (newPalletWeightForm.UserInput.Length > 0 && int.Parse(newPalletWeightForm.UserInput, NumberStyles.Number) > 0)
			{
				command = new SqlCommand("EXECUTE [Create Pallet Stored Procedure] '" + StartupForm.UserName + "', " + MainForm.MachineNumber + ", NULL, NULL", connection1);
				connection1.Open();
				reader = command.ExecuteReader();
				reader.Read();
				currentPalletNumber = (int)reader[0];
				reader.Close();
				command = new SqlCommand("INSERT INTO [Current Pallets at Machine Table] SELECT " + prodMasterItemNumber + ", " + currentPalletNumber.ToString() + ", " + newPalletWeightForm.UserInput, connection1);
				command.ExecuteNonQuery();
				connection1.Close();
				PrintClass.Label("P" + currentPalletNumber.ToString());
			}

			newPalletWeightForm.Dispose();
		}

		void EndOfShiftTimerTick(object sender, EventArgs e)
		{
			if (endOfShiftTime <= DateTime.Now)
			{
				MessageBox.Show("End of Shift.  Please finish recording your production data.", "Shift is Over");
				endOfShiftTimer.Enabled = false;
			}
		}
		void CmdCreateCaseClick(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(rtbLooseCases.Text))
			{
				MessageBox.Show("You cannot create a new case with loose casess at the line.  Please move or palletize the  loose cases listed.", "Loose Cases Exist");
			}
			else if (currentPalletNumber == 0)
			{
				MessageBox.Show("This job does not have a pallet assigned.  Please click the \"Get Pallet\" button and create or enter a pallet number", "Missing Pallet");
			}
			else
			{
				bool goodData = true;
				for (int i = 0; i < dgvInputs.Rows.Count; i++)
				{
					if (dgvInputs["Roll ID", i].Value.ToString() == string.Empty)
					{
						if (dgvInputs.Rows.Count == 1)
						{
							MessageBox.Show("You cannot create a new case without an input roll.  Please scan in an input roll.", "No Input Roll");
						}
						else
						{
							MessageBox.Show("You cannot create a new case with an input roll on unwind " + (i + 1).ToString() + ".  Please scan in an input roll to unwind " + (i + 1).ToString() + ".", "No Input Roll on Unwind " + (i + 1).ToString());
						}
						
						goodData = false;
						break;
					}
				}
				
				if (goodData)
				{
					DialogResult answer = DialogResult.No;
					if (currentBagsperCase == 0)
					{
						currentBagsperCase = defaultBagsPerCase;
					}
					
					string notes = string.Empty;
					if (defaultBagsPerCase != 0 && defaultBagsPerCase == currentBagsperCase && poundsPerCase != 0)
					{
						answer = MessageBox.Show("The current Bags per case is " + defaultBagsPerCase.ToString("N0") + " and the current pounds per case is " + poundsPerCase.ToString("N0") + ".  Do you wish to use these sames numbers?", "Use Same Bags per Case and Pounds?", MessageBoxButtons.YesNo);
					}
				
					if (answer == DialogResult.No)
					{
						UnitInformationForm getCaseInformationForm = new UnitInformationForm("Enter Case Count and Weight", jobDescription, 99999, true);
						getCaseInformationForm.UnitName = "Bags";
						getCaseInformationForm.ShowDialog();
						if (getCaseInformationForm.Units > 0 && getCaseInformationForm.Pounds > 0)
						{
							currentBagsperCase = getCaseInformationForm.Units;
							poundsPerCase = getCaseInformationForm.Pounds;
							notes = getCaseInformationForm.Notes;
						}
						
						getCaseInformationForm.Dispose();
					}
						
					if (defaultBagsPerCase != 0 && poundsPerCase != 0)
					{
						
						string lane = "1";
						if (numberLanes > 1)
						{
							OptionsForm pickLaneForm = new OptionsForm("Lane", false, true);
							for (int i = 1; i <= numberLanes; i++)
							{
								pickLaneForm.AddOption(i.ToString());
							}
			
							pickLaneForm.ShowDialog();
							lane = pickLaneForm.Option;
							pickLaneForm.Dispose();
						}
				
						if (lane != "Abort")
						{
                            if ((producedBags + currentBagsperCase) > consumedBags)
                            {
                                answer = MessageBox.Show("You have only consumed enough film to to create a case of " + (consumedBags - producedBags).ToString("N0") + " bags.  Do you still wish to create a case of " + currentBagsperCase.ToString("N0") + " bags?", "Override (REQUIRES SUPERVISOR APPROVAL)", MessageBoxButtons.YesNo);
                                if (answer == DialogResult.Yes)
                                {
                                    string authorizedBy = string.Empty;
                                    bool authorized = ModulesClass.Authorized(false, ref authorizedBy);
                                    if (! authorized)
                                    {
                                        answer = DialogResult.No;
                                    }
                                }
                            }
                            else
                            {
                                answer = DialogResult.Yes;
                            }

                            if (answer == DialogResult.Yes)
                            {
                                if (notes == string.Empty)
                                {
                                    command = new SqlCommand("EXECUTE [dbo].[Create Production Case Stored Procedure] " + currentProductionRecord.ToString() + ", " + lane + ", NULL, " + prodMasterItemNumber + ", '" + StartupForm.UserName + "', " + currentBagsperCase.ToString() + ", " + poundsPerCase.ToString() + ", " + MainForm.MachineNumber + ", '" + DateTime.Now.ToString() + "'", connection1);
                                }
                                else
                                {
                                    command = new SqlCommand("EXECUTE [dbo].[Create Production Case Stored Procedure] " + currentProductionRecord.ToString() + ", " + lane + ", '" + notes.Replace("'", "''") + "', " + prodMasterItemNumber + ", '" + StartupForm.UserName + "', " + currentBagsperCase.ToString() + ", " + poundsPerCase.ToString() + ", " + MainForm.MachineNumber + ", '" + DateTime.Now.ToString() + "'", connection1);
                                }
                                connection1.Open();
                                int caseNumber = (int)command.ExecuteScalar();
                                PrintClass.Label("C" + caseNumber.ToString());
                                if ((producedBags + currentBagsperCase) > consumedBags)
                                {
                                    command = new SqlCommand("INSERT INTO [Production Case Exception Table] SELECT " + caseNumber.ToString() + "," + (consumedBags- producedBags).ToString("N0"), connection1);
                                    command.ExecuteNonQuery();
                                    ModulesClass.SendEmail(1, "Create Case Exception", "On line " + MainForm.MachineNumber.ToString() + " operator " + operatorName + " just created Case C" + caseNumber.ToString() + " for job " + jobNumber + "(" + jobDescription + ") in the amount of " + currentBagsperCase.ToString("N0") + " bags when the bags available were " + (consumedBags - producedBags).ToString("N0") + ".");
                                }

                                FillLooseCaseText();
                                connection1.Close();
                                producedBags += currentBagsperCase;
                                txtProducedBags.Text = producedBags.ToString("N0");
                                cmdClosePallet.Enabled = true;
                            }
                        }
					}
				}
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		private void CmdMoveItemClick(object sender, EventArgs e)
		{
            DialogResult answer = ModulesClass.GetItemToMove(MainForm.MachineNumber);
            if (answer == DialogResult.Yes)
            {
                connection1.Open();
                FillCaseText();
                FillLooseCaseText();
                connection1.Close();
            }
		}

        
        private void cmdJobPulled_Click(object sender, EventArgs e)
        {
            if (ModulesClass.ValidSaveDate(startTime.AddHours(double.Parse(txtTotalHours.Text, NumberStyles.Number)), endOfShiftTime))
            {
                OptionsForm frmReasonPulledOptions = new OptionsForm("Reason for Pulling Job", false, true);
                command = new SqlCommand("select substring([Description],10,len([Description])-9) from [Save Production Reason Table] where substring([Description],1,9) = 'Pulled - ' order by [End Reason ID]", connection1);
                connection1.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    frmReasonPulledOptions.AddOption(reader[0].ToString());
                }

                reader.Close();
                frmReasonPulledOptions.ShowDialog();
                if (frmReasonPulledOptions.Option == "Abort")
                {
                    frmReasonPulledOptions.Dispose();
                    connection1.Close();
                    MessageBox.Show("Save aborted", "Save Aborted");
                }
                else
                {
                    frmReasonPulledOptions.Dispose();
                    command = new SqlCommand("select cast([End Reason ID] as int) from [Save Production Reason Table] where [Description]='Pulled - " + frmReasonPulledOptions.Option + "'", connection1);
                    int reasonCode = (int)command.ExecuteScalar();
                    connection1.Close();
                    WriteProductionRecord(startTime.AddHours(double.Parse(txtTotalHours.Text, NumberStyles.Number)), reasonCode.ToString());
                }
            }
        }

        private void cmdJobToDateStats_Click(object sender, EventArgs e)
        {
            JobToDateStatsForm jobStatsForm = new JobToDateStatsForm(jobNumber, true, "0", "0", "0", "0");
            jobStatsForm.ShowDialog();
            jobStatsForm.Dispose();
        }
    }
}
