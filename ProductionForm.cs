/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 2/28/2011
 * Time: 9:04 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace ShopFloor
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.IO;
    using System.Windows.Forms;

    /// <summary>
    /// Enter Hours and Film Usage, Production, and Scrap Production.
    /// </summary>
    public partial class ProductionForm : Form
    {
        private CommentForm productionNotesForm;
        private static bool jobHistoryFormOpen = false;
        private string jobNumber;
        private string operatorName;
        private string prodMasterItemNumber;
        private int wipProdType;
        private DateTime endOfShiftTime;
        private int currentProductionRecord;
        private DateTime startTime;
        private bool newJob;
        private string jobDescription = string.Empty;
        private int wipInputMasterItemNumber1;
        private int wipInputMasterItemNumber2;
        private DataTable UPCCodesTable = new DataTable();
        private string inputFilms = string.Empty;
        private bool input1Printed;
        private bool input2Printed;
        private int numberStreams;
        private decimal filmWidth;
        private int nextDownTimeRecordID = 1;
        private int nextScrapRecordID = 1;
        private JobHistoryForm frmCurrentJobHistory;
        private SqlConnection connection1 = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
        private SqlConnection connection2 = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
        private SqlDataReader reader;
        private SqlCommand command;
        private ConsumedFilmForm currentConsumedFilmForm;
        private int consumedFeet1;
        private int consumedFeet2;
        private decimal currentWidth1;
        private decimal currentWidth2;
        private int currentInputLF1;
        private int currentInputLF2;
        private int unwind1RollNumber;
        private int unwind2RollNumber;
        private int createdFeet;
        private int rollNumber;
        private string operationName;

        //Dictionary dtReasonSource = new Dictionary();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1306:SetLocaleForDataTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "wip")]
        public ProductionForm(string operationName, string currentOperatorName, string currentJobNumber, string prodMasterItemNo, int wipType, int currentProdRecord, DateTime prodStartTime, DateTime endOfShift, int incomingProdLF, bool parNewJob)
        {
            InitializeComponent();
            rtbJobTitle.SelectionAlignment = HorizontalAlignment.Center;
            rtbJobTitle.Text = operationName + "     Operator: " + currentOperatorName + "	    Job " + currentJobNumber + "     Start Time: " + prodStartTime.ToString("dddd, MMMM d, yyyy h:mm tt");
            jobNumber = currentJobNumber;
            operatorName = currentOperatorName;
            prodMasterItemNumber = prodMasterItemNo;
            this.operationName = operationName;
            endOfShiftTime = endOfShift;
            wipProdType = wipType;
            newJob =parNewJob;
            if (endOfShiftTime > DateTime.Now)
            {
                endOfShiftTimer.Interval = 60000;
                endOfShiftTimer.Enabled = true;
            }

            if (wipType == 2)
            {
                // Job Jacket WIP
                command = new SqlCommand("SELECT a.[Job Jacket No], b.[Description], a.[No Streams], ISNULL(a.[Combo No], 0), [dbo].[Get Numbers Only](a.[UPC Code]), ISNULL(c.[PrintingSpecInstruction], ''), ISNULL(c.[LamSpecInstructions], '') FROM [Finished Goods Specification Table] a INNER JOIN [Inventory Master Table] b ON a.[Master Item No] = b.[Master Item No] INNER JOIN [JobJackets].[dbo].[tblItem] c ON a.[Item No] = c.[ItemNo] WHERE a.[Job Jacket No] = " + currentJobNumber.ToString().Substring(0, currentJobNumber.ToString().Length - 2), connection1);
            }
            else
            {
                // Combo Job WIP
                command = new SqlCommand("SELECT a.[Job Jacket No], b.[Description], a.[No Streams], CAST(0 AS int), [dbo].[Get Numbers Only](a.[UPC Code]), ISNULL(c.[PrintingSpecInstruction], ''), ISNULL(c.[LamSpecInstructions], '') FROM [Finished Goods Specification Table] a INNER JOIN [Inventory Master Table] b ON a.[Master Item No] = b.[Master Item No] INNER JOIN [JobJackets].[dbo].[tblItem] c ON a.[Item No] = c.[ItemNo] WHERE a.[Combo No] = " + currentJobNumber.ToString().Substring(0, currentJobNumber.ToString().Length - 2) + " ORDER BY a.[Job Jacket No]", connection1);
            }

            currentProductionRecord = currentProdRecord;
            startTime = prodStartTime;
            connection1.Open();
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                if ((int)reader[3] == 0)
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
                        jobDescription += reader[1].ToString() + "/";

                        if (!string.IsNullOrEmpty(reader[4].ToString()))
                        {
                            DataRow row = UPCCodesTable.NewRow();
                            row["UPC Code"] = reader[4];
                            UPCCodesTable.Rows.Add(row);
                        }

                        if (jobNumber.Substring(jobNumber.Length - 2, 1) == "1") // Press Job
                        {
                            if (wipType == 2)
                            {
                                //Job Jacket WIP
                                rtbSpecialInstructions.Text = reader[5].ToString();
                            }
                            else
                            {
                                //Combo WIP
                                rtbSpecialInstructions.Text += "Job " + reader[0].ToString() + "\r\n" + reader[5].ToString() +"\r\n\r\n";
                            }
                        }
                        else if (jobNumber.Substring(jobNumber.Length - 2, 1) == "2") //Lam Job
                        {
                            if (wipType == 2)
                            {
                                //Job Jacket WIP
                                rtbSpecialInstructions.Text = reader[6].ToString();
                            }
                            else
                            {
                                //Combo WIP
                                rtbSpecialInstructions.Text += "Job " + reader[0].ToString() + "\r\n" + reader[6].ToString() + "\r\n\r\n";
                            }
                        }
                    }
                    while (reader.Read());

                    reader.Close();
                    jobDescription = jobDescription.Substring(0, jobDescription.Length - 1);

                    if (jobNumber.Substring(jobNumber.Length - 2, 1) == "1") // Press Job
                    {
                        command = new SqlCommand("SELECT a.[Input Master Item No], CAST(b.[Item Type No] AS int), b.[Description], CASE WHEN a.[Slit in Line] = 1 THEN a.[No Streams] ELSE 1 END, CASE WHEN a.[Slit in Line] = 1 THEN a.[Stream Width] ELSE a.[Standard Film Width] END, CAST(CASE WHEN [dbo].[Get Print Application Type](a.[Master Item No]) = 'OPV' AND b.[Item Type No] = 1 THEN 0 ELSE 1 END AS bit) FROM [Printing Specification Table] a INNER JOIN [Inventory Master Table] b ON a.[Input Master Item No] = b.[Master Item No] WHERE a.[Master Item No] = " + prodMasterItemNumber, connection1);
                        reader = command.ExecuteReader();
                        reader.Read();
                        if ((int)reader[1] == 1)
                        {
                            // Raw Material Input
                            wipInputMasterItemNumber1 = 0;
                            inputFilms = reader[2].ToString();
                        }
                        else
                        {
                            wipInputMasterItemNumber1 = (int)reader[0];
                        }

                        wipInputMasterItemNumber2 = 0;
                        numberStreams = (int)reader[3];
                        filmWidth = (decimal)reader[4];
                        input1Printed = reader.GetBoolean(5);
                    }
                    else if (jobNumber.Substring(jobNumber.Length - 2, 1) == "2")  // Lamination Job
                    {
                        command = new SqlCommand("SELECT a.[Input Master Item No 1], CAST(b.[Item Type No] AS int), b.[Description], a.[Input Master Item No 2], CAST(c.[Item Type No] AS int), c.[Description], ISNULL(d.[Printed], 0), ISNULL(e.[Printed], 0), a.[Standard Film Width] FROM [Lamination Specification Table] a INNER JOIN [Inventory Master Table] b ON a.[Input Master Item No 1] = b.[Master Item No] INNER JOIN [Inventory Master Table] c ON a.[Input Master Item No 2] = c.[Master Item No] OUTER APPLY [Lam Input Printed WIP](1, a.[Master Item No]) d OUTER APPLY [dbo].[Lam Input Printed WIP](2, a.[Master Item No]) e WHERE a.[Master Item No] = " + prodMasterItemNumber, connection1);
                        reader = command.ExecuteReader();
                        reader.Read();
                        if ((int)reader[1] == 1)
                        {
                            // Raw Material Input
                            wipInputMasterItemNumber1 = 0;
                            inputFilms = reader[2].ToString();
                        }
                        else
                        {
                            wipInputMasterItemNumber1 = (int)reader[0];
                        }

                        if ((int)reader[4] == 1)
                        {
                            // Raw Material Input
                            wipInputMasterItemNumber2 = 0;
                            if (string.IsNullOrEmpty(inputFilms))
                            {
                                inputFilms = reader[5].ToString();
                            }
                            else
                            {
                                inputFilms += " and " + reader[5].ToString();
                            }
                        }
                        else
                        {
                            wipInputMasterItemNumber2 = (int)reader[3];
                        }

                        input1Printed = reader.GetBoolean(6);
                        input2Printed = reader.GetBoolean(7);
                        filmWidth = (decimal)reader[8];
                        numberStreams = 1;
                    }
                    else if (jobNumber.Substring(jobNumber.Length - 2, 2) == "32") // Perf Job
                    {
                        command = new SqlCommand("SELECT a.[Input Master Item No], CAST(b.[Item Type No] AS int), b.[Description], a.[Standard Film Width] FROM [Perforation Specification Table] a INNER JOIN [Inventory Master Table] b ON a.[Input Master Item No] = b.[Master Item No] WHERE a.[Master Item No] = " + prodMasterItemNumber, connection1);
                        reader = command.ExecuteReader();
                        reader.Read();
                        if ((int)reader[1] == 1)
                        {
                            // Raw Material Input
                            wipInputMasterItemNumber1 = 0;
                            inputFilms = reader[2].ToString();
                            input1Printed = false;
                        }
                        else
                        {
                            wipInputMasterItemNumber1 = (int)reader[0];
                            command = new SqlCommand("SELECT [Reference Item No] FROM [Inventory Master Table] WHERE [Reference Item No] = " + jobNumber.Substring(0, jobNumber.Length - 2) + "11 and [Item Type No] in (2,3)", connection2);
                            connection2.Open();
                            object printJobNo = command.ExecuteScalar();
                            if (printJobNo == null)
                            {
                                // No print job, therefore laminations from other jobs could be the input film
                                input1Printed = false;
                            }
                            else
                            {
                                input1Printed = true;
                            }

                            connection2.Close();
                        }

                        wipInputMasterItemNumber2 = 0;
                        numberStreams = 1;
                        filmWidth = (decimal)reader[3];
                        // input1Printed = reader.GetBoolean(5);
                    }
                    else if (jobNumber.Substring(jobNumber.Length - 2, 2) == "34" || jobNumber.Substring(jobNumber.Length - 2, 2) == "35") // Laser Score Job
                    {
                        command = new SqlCommand("SELECT a.[Input Master Item No], CAST(b.[Item Type No] AS int), b.[Description], a.[Standard Film Width] FROM [Laser Scoring Specification Table] a INNER JOIN [Inventory Master Table] b ON a.[Input Master Item No] = b.[Master Item No] WHERE a.[Master Item No] = " + prodMasterItemNumber, connection1);
                        reader = command.ExecuteReader();
                        reader.Read();
                        if ((int)reader[1] == 1)
                        {
                            // Raw Material Input
                            wipInputMasterItemNumber1 = 0;
                            inputFilms = reader[2].ToString();
                            input1Printed = false;
                        }
                        else
                        {
                            wipInputMasterItemNumber1 = (int)reader[0];
                            command = new SqlCommand("SELECT [Reference Item No] FROM [Inventory Master Table] WHERE [Reference Item No] = " + jobNumber.Substring(0, jobNumber.Length - 2) + "11 and [Item Type No] in (2,3)", connection2);
                            connection2.Open();
                            object printJobNo = command.ExecuteScalar();
                            if (printJobNo == null)
                            {
                                // No print job, therefore laminations from other jobs could be the input film
                                input1Printed = false;
                            }
                            else
                            {
                                input1Printed = true;
                            }

                            connection2.Close();
                        }

                        wipInputMasterItemNumber2 = 0;
                        numberStreams = 1;
                        filmWidth = (decimal)reader[3];
                        // input1Printed = reader.GetBoolean(5);
                    }

                    reader.Close();
                }
                else
                {
                    MessageBox.Show("Error - the Job Number entered is part of Combo Job " + ((int)reader[3]).ToString() + ".  You must input the Combo Number or uncombo the Job to record production.", "Invalid Job Number");
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

            // Form is valid so add form close events
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProductionFormFormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ProductionFormFormClosed);

            command = new SqlCommand("SELECT [Setup Hrs], [Run Hrs], [DT Hrs], [Setup Hrs] + [Run Hrs] + [DT Hrs] FROM [dbo].[Get Standard Production Information] (" + currentProductionRecord.ToString() + ")", connection1);
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
            if (jobNumber.Substring(jobNumber.Length - 2, 1) == "2")
            {
                // Adhesive Lamination
                currentConsumedFilmForm = new ConsumedFilmForm(true);
                command = new SqlCommand("select 'T'+cast(a.[Tote ID] as nvarchar(10))+'  ('+b.[Part No]+')' from [Tote Table] a inner join [Adhesive Table] b on a.[Master Item No]=b.[Master Item No] where a.[Opened Date] is not NULL and a.[Closed Date] is null and a.[Machine No]=" + MainForm.MachineNumber, connection1);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    rtbJobDescriptions.Text += "\r\nCurrent Adhesive Tote:  " + reader[0].ToString();
                }

                reader.Close();
            }
            else
            {
                currentConsumedFilmForm = new ConsumedFilmForm(false);
            }

            // Get rolls consumed for this job to date on this machine
            FillConsumedFilmGrid();
            createdFeet = -incomingProdLF;
            if (jobNumber.Substring(jobNumber.Length - 2, 1) == "2")
            {
                // Adhesive Lamination
                lblConsumedLF1.Text = "Consumed: LF 1:";
                lblConsumedLF2.Visible = true;
                txtConsumedFeet2.Visible = true;
                rtbUnwind1CurrentRoll.Height = 45;
                rtbUnwind2CurrentRoll.Visible = true;
                txtConsumedFeet2.Text = consumedFeet2.ToString("N0");
            }
            else
            {
                lblConsumedLF1.Text = "Consumed LF:";
            }

            command = new SqlCommand("SELECT a.[Roll ID], CAST(ROUND(b.[Original LF], 0) AS int) FROM [Production Roll Table] a INNER JOIN [Roll Table] b ON a.[Roll ID] = b.[Roll ID] WHERE a.[Production ID] = " + currentProductionRecord.ToString() + " AND b.[Original LF] > 0  ORDER BY b.[Roll ID]", connection1);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                rtbCreatedWIPInfo.Text += "WIP Roll " + reader[0].ToString() + " @ " + ((int)reader[1]).ToString("N0") + " Feet\r\n";
                createdFeet += (int)reader[1] / numberStreams;
            }

            reader.Close();
            txtCreatedFeet.Text = createdFeet.ToString("N0");
            command = new SqlCommand("SELECT ISNULL([Notes], '') FROM [Production Master Table] WHERE [Production ID] = " + currentProductionRecord.ToString(), connection1);
            string comments = command.ExecuteScalar().ToString();
            connection1.Close();
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
            string currentRollUnwind1Information = string.Empty;
            string currentRollUnwind2Information = string.Empty;
            SqlConnection consumedFilmConnection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
            SqlDataReader consumedFilmReader;
            consumedFeet1 = 0;
            consumedFeet2 = 0;
            currentWidth1 = 0;
            currentWidth2 = 0;
            unwind1RollNumber = 0;
            unwind2RollNumber = 0;
            command = new SqlCommand("SELECT e.[Name], a.[Roll ID], CAST(a.[Unwind No] AS int), b.[Width], a.[Start Usage Date], CAST(a.[Start Usage LF] AS int), a.[End Usage Date], CAST(ISNULL(a.[End Usage LF], 0) AS int), c.[Description] FROM [Production Consumed Roll Table] a INNER JOIN ([Roll Table] b INNER JOIN [Inventory Master Table] c ON b.[Master Item No] = c.[Master Item No]) ON a.[Roll ID] = b.[Roll ID] INNER JOIN ([Production Master Table] d INNER JOIN [Operator Table] e ON d.[Operator ID] = e.[Operator ID]) ON a.[Start Production ID] = d.[Production ID] WHERE a.[Start Usage LF] != ISNULL(a.[End Usage LF], 0) AND a.[Start Production ID] = " + currentProductionRecord.ToString() + " ORDER BY a.[Start Usage Date]", consumedFilmConnection);
            consumedFilmConnection.Open();
            consumedFilmReader = command.ExecuteReader();
            while (consumedFilmReader.Read())
            {
                currentConsumedFilmForm.AddRoll(consumedFilmReader[0].ToString(), (DateTime)consumedFilmReader[4], consumedFilmReader[1].ToString(), "Consumed", consumedFilmReader[2].ToString(), ((decimal)consumedFilmReader[3]).ToString("N4") + "\" " + consumedFilmReader[8].ToString(), (int)consumedFilmReader[5]);
                if (consumedFilmReader[6] != DBNull.Value && (int)consumedFilmReader[7] > 0)
                {
                    // There is a return
                    currentConsumedFilmForm.AddRoll(consumedFilmReader[0].ToString(), (DateTime)consumedFilmReader[6], consumedFilmReader[1].ToString(), "Returned", consumedFilmReader[2].ToString(), ((decimal)consumedFilmReader[3]).ToString("N4") + "\" " + consumedFilmReader[8].ToString(), (int)consumedFilmReader[7]);
                }
                else
                {
                    // There is no Return
                    if ((int)consumedFilmReader[2] == 1)
                    {
                        currentRollUnwind1Information = "Currently Roll R" + consumedFilmReader[1].ToString() + " " + ((decimal)consumedFilmReader[3]).ToString("N4") + "\" " + consumedFilmReader[8].ToString() + " @ " + ((int)consumedFilmReader[5]).ToString("N0") + " LF is being consumed";
                        currentInputLF1 = (int)consumedFilmReader[5];
                        currentWidth1 = (decimal)consumedFilmReader[3];
                        unwind1RollNumber = (int)consumedFilmReader[1];
                    }
                    else
                    {
                        // Input Roll 2 on Laminator
                        currentRollUnwind2Information = "Currently Roll R" + consumedFilmReader[1].ToString() + " " + ((decimal)consumedFilmReader[3]).ToString("N4") + "\" " + consumedFilmReader[8].ToString() + " @ " + ((int)consumedFilmReader[5]).ToString("N0") + " LF is being consumed on unwind 2";
                        currentInputLF2 = (int)consumedFilmReader[5];
                        currentWidth2 = (decimal)consumedFilmReader[3];
                        unwind2RollNumber = (int)consumedFilmReader[1];
                    }
                }

                if ((int)consumedFilmReader[2] == 1)
                {
                    consumedFeet1 += (int)consumedFilmReader[5] - (int)consumedFilmReader[7];
                }
                else
                {
                    // Input Roll 2 on Laminator
                    consumedFeet2 += (int)consumedFilmReader[5] - (int)consumedFilmReader[7];
                }
            }

            consumedFilmReader.Close();
            consumedFilmConnection.Close();
            txtConsumedFeet1.Text = consumedFeet1.ToString("N0");
            txtConsumedFeet2.Text = consumedFeet2.ToString("N0");
            if (currentRollUnwind1Information.Length > 0)
            {
                if (jobNumber.Substring(jobNumber.Length - 2, 1) == "2")
                {
                    rtbUnwind1CurrentRoll.Text = currentRollUnwind1Information + " on unwind 1";
                }
                else
                {
                    rtbUnwind1CurrentRoll.Text = currentRollUnwind1Information;
                }
            }

            if (currentRollUnwind2Information.Length > 0)
            {
                rtbUnwind2CurrentRoll.Text = currentRollUnwind2Information;
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
            string unwindNumber;
            if (jobNumber.Substring(jobNumber.Length - 2, 1) == "2")
            {
                OptionsForm frmUnwindStation = new OptionsForm("Unwind No.", false, true);
                frmUnwindStation.AddOption("1");
                frmUnwindStation.AddOption("2");
                frmUnwindStation.ShowDialog();
                if (frmUnwindStation.Option == "Abort")
                {
                    frmUnwindStation.Dispose();
                    return;
                }
                else
                {
                    unwindNumber = frmUnwindStation.Option;
                    frmUnwindStation.Dispose();
                }
            }
            else
            {
                unwindNumber = "1";
            }

            GetInputForm getRollToConsumeForm = new GetInputForm("Scan/Input Roll to Consume Barcode", "R", 0, 0, true);
            getRollToConsumeForm.ShowDialog();
            if (getRollToConsumeForm.UserInput.Length > 0)
            {
                bool UPCCodeValidated = false;
                command = new SqlCommand("SELECT a.[Master Item No], a.[Width], f.[Description], ISNULL(a.[Location ID], d.[Location ID]), CAST(ROUND(a.[Current LF], 0) AS int), CAST(ROUND(a.[Original Pounds] * a.[Current LF] / a.[Original LF], 0) AS int), e.[Vendor Roll ID], h.[Description], h.[Inventory Available], b.[Allocated LF], f.[Reference Item No], CAST(f.[Item Type No] AS int), ISNULL(g.[Film Type], 'N/A'), a.[Create Date], k.[Reference Item No], k.[Description], i.[Start Usage Date], i.[Start Usage LF]  FROM [Roll Table] a LEFT JOIN ([Allocation Pick Table] b INNER JOIN [Allocation Master Table] c ON b.[Allocation ID] = c.[Allocation ID] AND c.[Master Item No] = " + prodMasterItemNumber + " AND c.[Pick Date] IS NOT NULL AND c.[Release Date] IS NULL AND c.[Void Date] IS NULL) ON a.[Roll ID] = b.[Roll ID] LEFT JOIN [Pallet Table] d ON a.[Pallet ID] = d.[Pallet ID] LEFT JOIN [Roll PO Table] e ON a.[Roll ID] = e.[Roll ID] INNER JOIN [Inventory Master Table] f ON a.[Master Item No] = f.[Master Item No] LEFT JOIN [Film View] g ON a.[Master Item No] = g.[Master Item No] LEFT JOIN ([Production Consumed Roll Table] i INNER JOIN ([Production Master Table] j INNER JOIN [Inventory Master Table] k ON j.[Master Item No] = k.[Master Item No]) ON i.[Start Production ID] != " + currentProductionRecord.ToString() + " AND i.[Start Production ID] = j.[Production ID] AND j.[Machine No] = " + MainForm.MachineNumber + " AND i.[End Usage Date] IS NULL AND i.[Start Usage Date] > DATEADD(hh, -12, GETDATE())) ON a.[Roll ID] = i.[Roll ID], [Location Table] h  WHERE a.[Original LF] > 0 AND ISNULL(a.[Location ID], d.[Location ID]) = h.[Location ID] AND a.[Roll ID] = " + getRollToConsumeForm.UserInput.Substring(1), connection1);
                connection1.Open();
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    bool consumeRoll = false;
                    if ((int)reader[11] != 1 && (int)reader[11] != 2 && (int)reader[11] != 3)
                    {
                        MessageBox.Show("Error - roll " + getRollToConsumeForm.UserInput + " " + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + " is not a raw film nor WIP Roll and therefore is invalid", "Invalid Roll");
                    }
                    else if ((int)reader[4] == 0 && reader[16] == DBNull.Value)
                    {
                        MessageBox.Show("Error - roll " + getRollToConsumeForm.UserInput + " " + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + " at location L" + reader[3].ToString() + " (" + reader[7].ToString() + ")" + " has no inventory available (maybe a return is missing?)", "No Inventory");
                    }
                    else if ((int)reader[4] == 0 && reader[16] != DBNull.Value)
                    {
                        MessageBox.Show("Roll " + getRollToConsumeForm.UserInput + " " + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + " has no inventory available but was consumed by job " + reader[14].ToString() + " - " + reader[15].ToString() + " on this line in the last 12 hours on " + ((DateTime)reader[16]).ToShortDateString() + " at " + ((DateTime)reader[16]).ToShortTimeString() + " in the amount of " + ((decimal)reader[17]).ToString("N0") + " LF.  You must create a return before you can consume this roll for this job.", "Possible Recent Return Missing");
                    }
                    else if ((int)reader[4] == 0 && reader[15] != DBNull.Value)
                    {
                        MessageBox.Show("Error - roll " + getRollToConsumeForm.UserInput + " " + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + " at location L" + reader[3].ToString() + " (" + reader[7].ToString() + ")" + " has no inventory available (maybe a return is missing?)", "No Inventory");
                    }
                    else if (!reader.GetBoolean(8))
                    {
                        MessageBox.Show("Error - roll " + getRollToConsumeForm.UserInput + " " + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + " is unavailable due to being at location L" + reader[3].ToString() + " (" + reader[7].ToString() + ")", "Roll Unavailable");
                    }
                    else if (reader[3].ToString() != MainForm.MachineNumber)
                    {
                        MessageBox.Show("Error - roll " + getRollToConsumeForm.UserInput + " " + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + " is at location L" + reader[3].ToString() + " (" + reader[7].ToString() + ")", "Roll is not at machine");
                    }
                    else if (wipInputMasterItemNumber1 != 0 && wipInputMasterItemNumber2 != 0 && (int)reader[11] != 2 && (int)reader[11] != 3)
                    {
                        MessageBox.Show("Error - roll " + getRollToConsumeForm.UserInput + " " + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + " is not a WIP roll and therefore not for job " + jobNumber + " - " + jobDescription + ".", "Invalid Roll");
                    }
                    else if (jobNumber.Substring(jobNumber.Length - 2, 1) == "2" && (int)reader[11] == 1 && (string)reader[12] == "SETUP")
                    {
                        MessageBox.Show("Error - roll " + getRollToConsumeForm.UserInput + " " + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + " is setup film and cannot be used on a lamination job.", "Invalid Roll");
                    }
                    else if ((int)reader[11] == 2 && reader[10].ToString().Substring(reader[10].ToString().Length - 2, 1) == "3")
                    {
                        MessageBox.Show("Error - roll " + getRollToConsumeForm.UserInput + " " + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + " is a slit roll and cannot be consumed here.", "Invalid Roll");
                    }
                    else
                    {
                        DialogResult answer = DialogResult.No;
                        string emailHeader = string.Empty;
                        string emailMessage = string.Empty;
                        if (((int)reader[11] == 2 || (int)reader[11] == 3) && (int)reader[0] != wipInputMasterItemNumber1 && (int)reader[0] != wipInputMasterItemNumber2)
                        {
                            answer = MessageBox.Show("WIP Roll " + getRollToConsumeForm.UserInput + " of Job J" + reader[10].ToString() + " - " + reader[2].ToString() + " is not a valid input roll for job " + jobNumber + ".  Are you recording production on the correct job?  If so do you wish to consume roll anyway (NOT ADVISED)?", "Override? (Requires Supervisor Authorization)", MessageBoxButtons.YesNo);
                            emailHeader = "Different Job's WIP Roll Consumed";
                            emailMessage = "On line " + MainForm.MachineNumber + " operator " + operatorName + " just consumed WIP roll " + getRollToConsumeForm.UserInput + " of job " + reader[10].ToString() + " - " + reader[2].ToString() + " for job " + jobNumber + " - " + jobDescription + ".  ";
                            if (answer == DialogResult.Yes)
                            {
                                if (reader[10].ToString().Substring(reader[10].ToString().Length - 2, 1) == "1")
                                {
                                    command = new SqlCommand("SELECT CAST(CASE WHEN [dbo].[Get Print Application Type](a.[Master Item No]) = 'OPV' AND b.[Item Type No] = 1 THEN 0 ELSE 1 END AS bit) FROM [Printing Specification Table] a INNER JOIN [Inventory Master Table] b ON a.[Input Master Item No] = b.[Master Item No] WHERE a.[Master Item No] = " + reader[0].ToString(), connection2);
                                }
                                else
                                {
                                    command = new SqlCommand("SELECT CAST(CASE WHEN ISNULL(b.[Printed], 0) = 1 OR ISNULL(c.[Printed], 0) = 1 THEN 1 ELSE 0 END AS bit) FROM [Lamination Specification Table] a OUTER APPLY [Lam Input Printed WIP](1, a.[Master Item No]) b OUTER APPLY [dbo].[Lam Input Printed WIP](2, a.[Master Item No]) c WHERE a.[Master Item No] = " + reader[0].ToString(), connection2);
                                }

                                connection2.Open();
                                bool isPrinted = (bool)command.ExecuteScalar();
                                connection2.Close();
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
                                        answer = DialogResult.No;
                                        consumeRoll = false;
                                    }
                                    else
                                    {
                                        UPCCodeValidated = true;
                                        if (!string.IsNullOrEmpty(overrideAuthorizedBy))
                                        {
                                            if (wipProdType == 3)
                                            {
                                                ModulesClass.SendEmail(2, "Manual UPC Verification", "The UPC codes of input Roll " + getRollToConsumeForm.UserInput + " for combo job " + jobNumber + " - " + jobDescription + " on line " + MainForm.MachineNumber + " were manually input.  The override was authorized by " + overrideAuthorizedBy + ".");
                                            }
                                            else
                                            {
                                                ModulesClass.SendEmail(2, "Manual UPC Verification", "The UPC code of input Roll " + getRollToConsumeForm.UserInput + " for job " + jobNumber + " - " + jobDescription + " on line " + MainForm.MachineNumber + " was manually input.  The override was authorized by " + overrideAuthorizedBy + ".");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if ((int)reader[11] == 1 && reader[9] == DBNull.Value && (string)reader[12] != "SETUP")
                        {
                            //This film roll is not allocated to this job and is not setup film
                            command = new SqlCommand("SELECT COUNT(*) FROM [Allocation Master Table] a INNER JOIN [Allocation Reservation Table] b ON a.[Allocation ID] = b.[Allocation ID] WHERE a.[Void Date] IS NULL AND a.[Master Item no] = " + prodMasterItemNumber + " AND b.[Master Item No] = " + reader[0].ToString() + " AND b.[Width] = " + reader[1].ToString(), connection2);
                            connection2.Open();
                            int likeRollsAllocated = (int)command.ExecuteScalar();
                            if (likeRollsAllocated == 0)
                            {
                                if (((unwindNumber == "1" && (decimal)reader[1] != currentWidth1) || (unwindNumber == "2" && (decimal)reader[1] != currentWidth2)) || ((unwindNumber == "1" && (int)reader[0] != wipInputMasterItemNumber1) || (unwindNumber == "2" && (decimal)reader[1] != wipInputMasterItemNumber2)))
                                {
                                    answer = MessageBox.Show("Error - roll " + getRollToConsumeForm.UserInput + " " + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + " is not the same film and/or width of any films allocated to job " + jobNumber + ".\r\nThe input film(s) for this job are " + inputFilms + ".\r\nDo you wish to consume this roll anyway (NOT ADVISED)?", "Override? (Requires Supervisor Authorization)", MessageBoxButtons.YesNo);
                                    emailHeader = "Unallocated DIFFERENT Film Roll Consumed";
                                    emailMessage = "On line " + MainForm.MachineNumber.ToString() + " operator " + operatorName + " just consumed unallocated film roll " + getRollToConsumeForm.UserInput + "(" + ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString() + ") for job " + jobNumber + " - " + jobDescription + ".  The input film(s) for this job are  " + inputFilms + ".  ";
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
                        //  Cure time changed from 24 to 8 hours on 5/26/17 per Jim Davies
                        if (consumeRoll && jobNumber.Substring(jobNumber.Length - 2, 1) == "2" && reader[10].ToString().Substring(reader[10].ToString().Length - 2, 1) == "2" && (int)reader[11] != 1 && (DateTime)reader[13] > DateTime.Now.AddHours(-8))
                        {
                            consumeRoll = false;
                            answer = MessageBox.Show("WIP Roll " + getRollToConsumeForm.UserInput + " of Job J" + reader[10].ToString() + " - " + reader[2].ToString() + " was created less than 8 hours ago on " + ((DateTime)reader[13]).ToShortDateString() + " at " + ((DateTime)reader[13]).ToShortTimeString() + ".  Do you still wish to consume it anyway (NOT ADVISED)?", "Override? (Requires Supervisor Authorization)", MessageBoxButtons.YesNo);
                            if (answer == DialogResult.Yes)
                            {
                                string authorizedBy = string.Empty;
                                bool authorized = ModulesClass.Authorized(false, ref authorizedBy);
                                if (authorized)
                                {
                                    consumeRoll = true;
                                    ModulesClass.SendEmail(2, "Possible uncured Lamination Roll Consumed", "On line " + MainForm.MachineNumber.ToString() + " operator " + operatorName + " just consumed film roll " + getRollToConsumeForm.UserInput + "(" + reader[2].ToString() + ") for job " + jobNumber + "(" + jobDescription + ") that was created on " + ((DateTime)reader[13]).ToShortDateString() + " at " + ((DateTime)reader[13]).ToShortTimeString() + ",  The override was authorized by " + authorizedBy + ".");
                                }
                            }
                        }
                    }

                    if (consumeRoll)
                    {
                        bool inputRollOK = true;
                        string overrideAuthorizedBy = string.Empty;
                        if (!UPCCodeValidated && UPCCodesTable.Rows.Count > 0 && (input1Printed || input2Printed))
                        {
                            inputRollOK = ModulesClass.ValidateUPCCodes(jobNumber, getRollToConsumeForm.UserInput, UPCCodesTable, out overrideAuthorizedBy);
                        }

                        if (inputRollOK)
                        {

                            if ((int)reader[11] == 1 && reader[6] == DBNull.Value && (string)reader[12] != "SETUP")
                            {
                                GetInputForm getVendorRollIDForm = new GetInputForm("Scan/Input Vendor Roll ID (Hit [Abort] if none)", "*", 0, 0, true);
                                do
                                {
                                    getVendorRollIDForm.ShowDialog();
                                    if (reader[12].ToString() == "NON-WOVEN")
                                    {
                                        if (getVendorRollIDForm.UserInput == string.Empty)
                                        {
                                            MessageBox.Show("You MUST enter a roll ID on Non-Woven rolls.", "Vendor Roll ID Required");
                                        }
                                        else if (getVendorRollIDForm.UserInput == getRollToConsumeForm.UserInput)
                                        {
                                            MessageBox.Show("You MUST enter the VENDOR Roll ID for Non-Woven rolls, not the Overwraps Roll ID", "Invalid Roll ID");
                                            getVendorRollIDForm.UserInput = string.Empty;
                                        }
                                    }
                                }
                                while (reader[12].ToString() == "NON-WOVEN" && string.IsNullOrEmpty(getVendorRollIDForm.UserInput));

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
                                    if (UPCCodesTable.Rows.Count > 0 && (input1Printed || input2Printed))
                                    {
                                        consumeRoll = ModulesClass.ValidateUPCCodes(jobNumber, getRollToConsumeForm.UserInput, UPCCodesTable, out overrideAuthorizedBy);
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
                                if (unwindNumber == "1")
                                {
                                        currentWidth1 = (decimal)reader[1];
                                        currentConsumedFilmForm.AddRoll(operatorName, DateTime.Now, getRollToConsumeForm.UserInput.Substring(1), "Consumed", "1", currentWidth1.ToString("N4") + "\" " + reader[2].ToString(), (int)reader[4]);
                                        if (jobNumber.Substring(jobNumber.Length - 2, 1) == "2")
                                        {
                                            rtbUnwind1CurrentRoll.Text = "Currently Roll " + getRollToConsumeForm.UserInput + " " + currentWidth1.ToString("N4") + "\" " + reader[2].ToString() + " @ " + ((int)reader[4]).ToString("N0") + " LF and " + ((int)reader[5]).ToString("N0") + " LBS is being consumed on unwind 1";
                                        }
                                        else
                                        {
                                            rtbUnwind1CurrentRoll.Text = "Currently Roll " + getRollToConsumeForm.UserInput + " " + currentWidth1.ToString("N4") + "\" " + reader[2].ToString() + " @ " + ((int)reader[4]).ToString("N0") + " LF and " + ((int)reader[5]).ToString("N0") + " LBS is being consumed";
                                        }

                                        unwind1RollNumber = int.Parse(getRollToConsumeForm.UserInput.Substring(1), NumberStyles.Number);
                                        consumedFeet1 += (int)reader[4];
                                        currentInputLF1 = (int)reader[4];
                                        txtConsumedFeet1.Text = consumedFeet1.ToString("N0");
                                }
                                else
                                {
                                        currentWidth2 = (decimal)reader[1];
                                        currentConsumedFilmForm.AddRoll(operatorName, DateTime.Now, getRollToConsumeForm.UserInput.Substring(1), "Consumed", "2", currentWidth2.ToString("N4") + "\" " + reader[2].ToString(), (int)reader[4]);
                                        rtbUnwind2CurrentRoll.Text = "Currently Roll " + getRollToConsumeForm.UserInput + " " + currentWidth2.ToString("N4") + "\" " + reader[2].ToString() + " @ " + ((int)reader[4]).ToString("N0") + " LF and " + ((int)reader[5]).ToString("N0") + " LBS is being consumed on unwind 2";
                                        unwind2RollNumber = int.Parse(getRollToConsumeForm.UserInput.Substring(1), NumberStyles.Number);
                                        consumedFeet2 += (int)reader[4];
                                        currentInputLF2 = (int)reader[4];
                                        txtConsumedFeet2.Text = consumedFeet2.ToString("N0");
                                }

                                if (!string.IsNullOrEmpty(overrideAuthorizedBy))
                                {
                                    if (wipProdType == 3)
                                    {
                                        ModulesClass.SendEmail(2, "Manual UPC Verification", "The UPC codes of input Roll " + getRollToConsumeForm.UserInput + " for combo job " + jobNumber + " - " + jobDescription + " on line " + MainForm.MachineNumber + " were manually input.  The override was authorized by " + overrideAuthorizedBy + ".");
                                    }
                                    else
                                    {
                                        ModulesClass.SendEmail(2, "Manual UPC Verification", "The UPC code of input Roll " + getRollToConsumeForm.UserInput + " for job " + jobNumber + " - " + jobDescription + " on line " + MainForm.MachineNumber + " was manually input.  The override was authorized by " + overrideAuthorizedBy + ".");
                                    }
                                }

                                command = new SqlCommand("INSERT INTO [Production Consumed Roll Table] SELECT " + currentProductionRecord.ToString() + ", " + unwindNumber + ", " + getRollToConsumeForm.UserInput.Substring(1) + ", '" + DateTime.Now.ToString() + "', " + reader[4].ToString() + ", NULL, NULL, NULL", connection2);
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
                command = new SqlCommand("SELECT TOP 1 b.[Master Item No], b.[Width], c.[Description], d.[Machine No], CAST(ROUND(a.[Start Usage LF], 0) AS int), CAST(ISNULL(ROUND(a.[End Usage LF], 0), 0) AS int), a.[Start Usage Date], CAST(a.[Unwind No] AS int),	a.[Start Production ID], e.[Reference Item No],	e.[Description] FROM [Production Consumed Roll Table] a INNER JOIN ([Roll Table] b INNER JOIN [Inventory Master Table] c ON b.[Master Item No] = c.[Master Item No]) ON a.[Roll ID] = b.[Roll ID] INNER JOIN ([Production Master Table] d INNER JOIN [Inventory Master Table] e ON d.[Master Item No] = e.[Master Item No]) ON a.[Start Production ID] = d.[Production ID] WHERE a.[Start Usage LF] != ISNULL(a.[End Usage LF], 0) AND (a.[Start Production ID] = " + currentProductionRecord.ToString() + " OR (d.[Machine No] = " + MainForm.MachineNumber + " AND a.[Start Usage Date] > DATEADD(hh, -12, GETDATE()) AND a.[End Usage Date] IS NULL)) AND a.[Roll ID] = " + getRollToReturnForm.UserInput.Substring(1) + " ORDER BY a.[Start Usage Date] DESC", connection1);
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
                                int maximumLF;
                                int maximumLFNoTolerance;
                                if ((int)reader[7] == 1)
                                {
                                    maximumLF = consumedFeet1 + (int)reader[5] - createdFeet + 3000;
                                    maximumLFNoTolerance = consumedFeet1 + (int)reader[5] - createdFeet;
                                }
                                else
                                {
                                    maximumLF = consumedFeet2 + (int)reader[5] - createdFeet + 3000;
                                    maximumLFNoTolerance = consumedFeet2 + (int)reader[5] - createdFeet;
                                }

                                if (int.Parse(returnLFForm.UserInput, NumberStyles.Number) > maximumLF)
                                {
                                    answer = MessageBox.Show("The calculated maximum LF you can return is " + maximumLF.ToString("N0") + ".  Do you still wish to return " + int.Parse(returnLFForm.UserInput, NumberStyles.Number).ToString("N0") + " LF?", "Override Maximum Return Amount?", MessageBoxButtons.YesNo);
                                    if (answer == DialogResult.Yes)
                                    {
                                        ModulesClass.SendEmail(1, "Return Roll Exception", "On line " + MainForm.MachineNumber.ToString() + " operator " + operatorName + " just returned roll " + getRollToReturnForm.UserInput + " for job " + jobNumber + "(" + jobDescription + ") in the amount of " + int.Parse(returnLFForm.UserInput, NumberStyles.Number).ToString("N0") + " LF when the LF available to return was " + maximumLFNoTolerance.ToString("N0") + ".");
                                    }
                                }

                                if (answer == DialogResult.Yes)
                                {
                                    if ((int)reader[7] == 1)
                                    {
                                        // Roll is on unwind 1
                                        consumedFeet1 -= int.Parse(returnLFForm.UserInput, NumberStyles.Number);
                                        currentInputLF1 = 0;
                                        txtConsumedFeet1.Text = consumedFeet1.ToString("N0");
                                        if (unwind1RollNumber == int.Parse(getRollToReturnForm.UserInput.Substring(1)))
                                        {
                                            // There is no roll on the unwind
                                            rtbUnwind1CurrentRoll.Text = string.Empty;
                                            currentWidth1 = 0;
                                            unwind1RollNumber = 0;
                                        }
                                    }
                                    else
                                    {
                                        // Roll is on unwind 2
                                        consumedFeet2 -= int.Parse(returnLFForm.UserInput, NumberStyles.Number);
                                        currentInputLF2 = 0;
                                        txtConsumedFeet2.Text = consumedFeet2.ToString("N0");
                                        if (unwind1RollNumber == int.Parse(getRollToReturnForm.UserInput.Substring(1)))
                                        {
                                            // There is no roll on the unwind
                                            rtbUnwind2CurrentRoll.Text = string.Empty;
                                            currentWidth2 = 0;
                                            unwind2RollNumber = 0;
                                        }
                                    }
                                }

                                if (int.Parse(returnLFForm.UserInput, NumberStyles.Number) == maximumLF || (int)reader[5] != 0)
                                {
                                    currentConsumedFilmForm.ClearGrid();
                                    FillConsumedFilmGrid();
                                }
                                else
                                {
                                    currentConsumedFilmForm.AddRoll(operatorName, DateTime.Now, getRollToReturnForm.UserInput.Substring(1), "Returned", reader[7].ToString(), ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString(), int.Parse(returnLFForm.UserInput, NumberStyles.Number));
                                }
                            }
                            else
                            {
                                ModulesClass.SendEmail(1, "Roll Returned after Production Record " + reader[8].ToString() + " closed", "On line " + MainForm.MachineNumber.ToString() + " operator " + operatorName + " just returned roll " + getRollToReturnForm.UserInput + " for job " + reader[9].ToString() + " - " + reader[10].ToString() + " in the amount of " + int.Parse(returnLFForm.UserInput, NumberStyles.Number).ToString("N0") + " LF.");
                            }

                            if (answer == DialogResult.Yes)
                            {
                                command = new SqlCommand("UPDATE [Production Consumed Roll Table] SET [End Production ID] = " + reader[8].ToString() + ", [End Usage Date] = '" + DateTime.Now.ToString() + "', [End Usage LF] = " + returnLFForm.UserInput + " WHERE [Roll ID] = " + getRollToReturnForm.UserInput.Substring(1) + " AND [Start Usage Date] = '" + ((DateTime)reader[6]).ToString() + "' AND [Start Production ID] = " + reader[8].ToString(), connection2);
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

                if (wipInputMasterItemNumber1 != 0)
                {
                    command = new SqlCommand("SELECT a.[Roll ID], b.[Description] FROM [Roll Table] a INNER JOIN [Location Table] b ON a.[Location ID] = b.[Location ID] WHERE b.[Inventory Available] = 1 AND [Current LF] > 0 AND [Master Item No] = " + wipInputMasterItemNumber1, connection1);
                    connection1.Open();
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        MessageBox.Show("Error - you cannont complete this job.  WIP Roll R" + reader[0].ToString() + " is still in inventory in location + " + reader[1].ToString() + ". This roll must be consumed in order to mark the job complete.", "Not all input WIP rolls used");
                        result = DialogResult.No;
                    }

                    reader.Close();
                    connection1.Close();
                }

                if (wipInputMasterItemNumber2 != 0)
                {
                    command = new SqlCommand("SELECT a.[Roll ID], b.[Description] FROM [Roll Table] a INNER JOIN [Location Table] b ON a.[Location ID] = b.[Location ID] WHERE b.[Inventory Available] = 1 AND [Current LF] > 0 AND [Master Item No] = " + wipInputMasterItemNumber2, connection1);
                    connection1.Open();
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        MessageBox.Show("Error - you cannont complete this job.  WIP Roll R" + reader[0].ToString() + " is still in inventory in location + " + reader[1].ToString() + ". This roll must be consumed in order to mark the job complete.", "Not all input WIP rolls used");
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
                        command = new SqlCommand("SELECT ISNULL(c.[Standard Production Linear Feet], d.[Standard Production Linear Feet]), ISNULL(SUM(b.[Original LF]), 0) FROM [Inventory Master Table] a LEFT JOIN [Roll Table] b ON a.[Master Item No] = b.[Master Item No] LEFT JOIN [Printing Specification Table] c ON a.[Master Item No] = c.[Master Item No] LEFT JOIN [Lamination Specification Table] d ON a.[Master Item No] = d.[Master Item No] WHERE a.[Master item No] = " + prodMasterItemNumber + " GROUP BY ISNULL(c.[Standard Production Linear Feet], d.[Standard Production Linear Feet])", connection1);
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
                                    ModulesClass.SendEmail(2, "Job " + jobNumber + " (" + jobDescription + ")" + " was run short", "On line " + MainForm.MachineNumber.ToString() + " operator " + operatorName + " completed the job with " + ((decimal)reader[1]).ToString("N0") + " production LF of the " + ((decimal)reader[0]).ToString("N0") + " production LF expected.  The override was authorized by " + authorizedBy + ".");
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
                string pulledReasonID = ModulesClass.GetPulledReason(currentProductionRecord.ToString());
                if (pulledReasonID == "Abort")
                {
                    MessageBox.Show("Save aborted", "Save Aborted");
                }
                else
                {
                    WriteProductionRecord(startTime.AddHours(double.Parse(txtTotalHours.Text, NumberStyles.Number)), pulledReasonID);
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
                    int unwind1EndLF = 0;
                    int unwind2EndLF = 0;
                    GetInputForm partialRollLFForm = new GetInputForm("In Process Roll LF", "#", 0, consumedFeet1 - createdFeet + 100000, false);
                    DialogResult answer = DialogResult.No;
                    if (decimal.Parse(txtRunHours.Text, NumberStyles.Number) > 0)
                    {
                        while (answer == DialogResult.No)
                        {
                            partialRollLFForm.ShowDialog();
                            if (partialRollLFForm.UserInput.Length > 0)
                            {
                                if (int.Parse(partialRollLFForm.UserInput, NumberStyles.Number) > consumedFeet1 - createdFeet + 3000)
                                {
                                    answer = MessageBox.Show("The calculated maximum LF you can create is " + (consumedFeet1 - createdFeet + 3000).ToString("N0") + ".  Do you still wish to create a partial roll @ " + int.Parse(partialRollLFForm.UserInput, NumberStyles.Number).ToString("N0") + " LF?", "Override Maximum LF for Partial Roll?", MessageBoxButtons.YesNo);
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
                    if (currentInputLF1 > 0)
                    {
                        partialRollLFForm.NewTitle = "Unwind 1 LF";
                        partialRollLFForm.UserInput = "0";
                        partialRollLFForm.MaxValue = currentInputLF1;
                        partialRollLFForm.ShowDialog();
                        if (!string.IsNullOrEmpty(partialRollLFForm.UserInput) && int.Parse(partialRollLFForm.UserInput, NumberStyles.Number) > 0)
                        {
                            command = new SqlCommand("execute [dbo].[Save Shift Change Unwind Footage Stored Procedure] " + currentProductionRecord + ", 1, 1, " + unwind1RollNumber.ToString() + ", " + partialRollLFForm.UserInput, connection1);
                            connection1.Open();
                            command.ExecuteNonQuery();
                            connection1.Close();
                            unwind1EndLF = int.Parse(partialRollLFForm.UserInput, NumberStyles.Number);
                        }
                    }

                    if (currentInputLF2 > 0)
                    {
                        partialRollLFForm.NewTitle = "Unwind 2 LF";
                        partialRollLFForm.UserInput = "0";
                        partialRollLFForm.MaxValue = currentInputLF2;
                        partialRollLFForm.ShowDialog();
                        if (!string.IsNullOrEmpty(partialRollLFForm.UserInput) && int.Parse(partialRollLFForm.UserInput, NumberStyles.Number) > 0)
                        {
                            command = new SqlCommand("execute [dbo].[Save Shift Change Unwind Footage Stored Procedure] " + currentProductionRecord + ", 2, 1, " + unwind2RollNumber.ToString() + ", " + partialRollLFForm.UserInput, connection1);
                            connection1.Open();
                            command.ExecuteNonQuery();
                            connection1.Close();
                            unwind2EndLF = int.Parse(partialRollLFForm.UserInput, NumberStyles.Number);
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
                    this.FormClosing -= ProductionFormFormClosing;
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
        private void CmdCreateWIPRollClick(object sender, EventArgs e)
        {
            int maximumFeet;

            if (jobNumber.Substring(jobNumber.Length - 2, 1) != "2")
            {
                maximumFeet = consumedFeet1 - createdFeet + 3000;
            }
            else
            {
                maximumFeet = Math.Min(consumedFeet1 - createdFeet + 3000, consumedFeet2 - createdFeet + 3000);
            }

            UnitInformationForm createWIPRollForm = new UnitInformationForm("Create WIP Roll", rtbJobDescriptions.Text, 999999, false);
            createWIPRollForm.ShowDialog();
            if (createWIPRollForm.Units > 0)
            {
                bool goodFeet = true;
                DialogResult answer = DialogResult.Yes;
                if (createWIPRollForm.Units > maximumFeet)
                {
                    answer = MessageBox.Show("The calculated maximum linear feet you can create is " + maximumFeet.ToString("N0") + ".  Do you still wish to create a roll at " + createWIPRollForm.Units.ToString("N0") + " LF?", "Override Maximum Linear Feet? (REQUIRES SUPERVISOR APPROVAL)", MessageBoxButtons.YesNo);
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
                    decimal rollWidth;
                    string overrideAuthorizedBy = string.Empty;
                    if (UPCCodesTable.Rows.Count > 0 && (input1Printed || input2Printed))
                    {
                        inputRollOK = ModulesClass.ValidateUPCCodes(jobNumber, string.Empty, UPCCodesTable, out overrideAuthorizedBy);
                    }

                    if (inputRollOK)
                    {
                        command = new SqlCommand("SELECT CAST(ISNULL(MAX([Set No]), 0) AS int) FROM [Production Roll Table] a INNER JOIN [Roll Table] b ON a.[Roll ID] = b.[Roll ID] WHERE b.[Master Item No] = " + prodMasterItemNumber, connection1);
                        connection1.Open();
                        int setNumber = (int)command.ExecuteScalar() + 1;
                        for (int i = 1; i <= numberStreams; i++)
                        {
                            if (jobNumber.Substring(jobNumber.Length - 2, 1) == "1")
                            {
                                if (numberStreams == 1 && currentWidth1 != 0)
                                {
                                        rollWidth = currentWidth1;
                                }
                                else
                                {
                                    rollWidth = filmWidth;
                                }

                                if (unwind1RollNumber == 0)
                                {
                                    command = new SqlCommand("EXECUTE [Create Production Roll Stored Procedure] " + currentProductionRecord.ToString() + ", NULL, NULL, " + setNumber + ", NULL, '" + createWIPRollForm.Notes.Replace("'", "''") + "', " + prodMasterItemNumber + ", '" + StartupForm.UserName + "', " + createWIPRollForm.Units.ToString() + ", NULL, " + rollWidth.ToString() + ", " + MainForm.MachineNumber + ", '" + DateTime.Now.ToString() + "'", connection1);
                                }
                                else
                                {
                                    command = new SqlCommand("EXECUTE [Create Production Roll Stored Procedure] " + currentProductionRecord.ToString() + ", " + unwind1RollNumber.ToString() + ", NULL, " + setNumber + ", NULL, '" + createWIPRollForm.Notes.Replace("'", "''") + "', " + prodMasterItemNumber + ", '" + StartupForm.UserName + "', " + createWIPRollForm.Units.ToString() + ", NULL, " + rollWidth.ToString() + ", " + MainForm.MachineNumber + ", '" + DateTime.Now.ToString() + "'", connection1);
                                }
                            }
                            else
                            {
                                rollWidth = Math.Max(currentWidth1, currentWidth2);
                                if (rollWidth == 0)
                                {
                                    rollWidth = filmWidth;
                                }

                                if (unwind1RollNumber == 0 || unwind2RollNumber == 0)
                                {
                                    command = new SqlCommand("EXECUTE [Create Production Roll Stored Procedure] " + currentProductionRecord.ToString() + ", NULL, NULL, " + setNumber + ", NULL, '" + createWIPRollForm.Notes.Replace("'", "''") + "', " + prodMasterItemNumber + ", '" + StartupForm.UserName + "', " + createWIPRollForm.Units.ToString() + ", NULL, " + rollWidth.ToString() + ", 2, '" + DateTime.Now.ToString() + "'", connection1);
                                    //command = new SqlCommand("EXECUTE [Create Production Roll Stored Procedure] " + currentProductionRecord.ToString() + ", NULL, NULL, " + setNumber + ", NULL, '" + createWIPRollForm.Notes.Replace("'", "''") + "', " + prodMasterItemNumber + ", '" + StartupForm.UserName + "', " + createWIPRollForm.Units.ToString() + ", NULL, " + rollWidth.ToString() + ", " + MainForm.MachineNumber + ", '" + DateTime.Now.ToString() + "'", connection1);
                                }
                                else
                                {
                                    command = new SqlCommand("EXECUTE [Create Production Roll Stored Procedure] " + currentProductionRecord.ToString() + ", " + unwind1RollNumber.ToString() + ", " + unwind2RollNumber.ToString() + ", " + setNumber + ", NULL, '" + createWIPRollForm.Notes.Replace("'", "''") + "', " + prodMasterItemNumber + ", '" + StartupForm.UserName + "', " + createWIPRollForm.Units.ToString() + ", NULL, " + rollWidth.ToString() + ", 2, '" + DateTime.Now.ToString() + "'", connection1);
                                    //command = new SqlCommand("EXECUTE [Create Production Roll Stored Procedure] " + currentProductionRecord.ToString() + ", " + unwind1RollNumber.ToString() + ", " + unwind2RollNumber.ToString() + ", " + setNumber + ", NULL, '" + createWIPRollForm.Notes.Replace("'", "''") + "', " + prodMasterItemNumber + ", '" + StartupForm.UserName + "', " + createWIPRoll
                                }
                            }

                            rollNumber = (int)command.ExecuteScalar();

                            if (createWIPRollForm.Notes.Length > 0)
                            {
                                command = new SqlCommand("INSERT INTO [Roll Comment Table] SELECT " + rollNumber.ToString() + ", '" + createWIPRollForm.Notes.Replace("'", "''") + "'", connection1);
                                command.ExecuteNonQuery();
                            }

                            if (createWIPRollForm.Units > maximumFeet)
                            {
                                command = new SqlCommand("INSERT INTO [Production Roll Exception Table] SELECT " + rollNumber.ToString() + "," + maximumFeet.ToString(), connection1);
                                command.ExecuteNonQuery();
                                ModulesClass.SendEmail(1, "Create Roll Exception", "On line " + MainForm.MachineNumber.ToString() + " operator " + operatorName + " just created Roll R" + rollNumber.ToString() + " for job " + jobNumber + "(" + jobDescription + ") in the amount of " + createWIPRollForm.Units.ToString("N0") + " linear feet when the linear feet available was " + (maximumFeet - 3000).ToString("N0") + ".");
                            }

                            rtbCreatedWIPInfo.Text += "WIP Roll " + rollNumber.ToString() + " @ " + createWIPRollForm.Units.ToString("N0") + " Feet\r\n";
                            PrintClass.Label("R" + rollNumber.ToString());

                            // Print a second form for QC if its a print Roll
                            if (operationName.Substring(0, 5) == "Press")
                            {
                                PrintClass.Label("R" + rollNumber.ToString());
                            }

              
                            createdFeet += createWIPRollForm.Units / numberStreams;
                            if (!string.IsNullOrEmpty(overrideAuthorizedBy))
                            {
                                if (wipProdType == 3)
                                {
                                    ModulesClass.SendEmail(2, "Manual UPC Verification", "The UPC codes of Created Roll " + rollNumber.ToString() + "  for combo job " + jobNumber + " - " + jobDescription + " on line " + MainForm.MachineNumber + " were manually input.  The override was authorized by " + overrideAuthorizedBy + ".");
                                }
                                else
                                {
                                    ModulesClass.SendEmail(2, "Manual UPC Verification", "The UPC code of Created Roll " + rollNumber.ToString() + "  for job " + jobNumber + " - " + jobDescription + " on line " + MainForm.MachineNumber + " was manually input.  The override was authorized by " + overrideAuthorizedBy + ".");
                                }
                            }
                        }

                        connection1.Close();
                        txtCreatedFeet.Text = createdFeet.ToString("N0");
                    }
                }

                createWIPRollForm.Dispose();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
        private void CmdMoveRollClick(object sender, EventArgs e)
        {
            ModulesClass.GetItemToMove(MainForm.MachineNumber);
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

        private void ProductionForm_Shown(object sender, EventArgs e)
        {
            productionNotesForm.Show();
        }

        private void cmdJobToDateStats_Click(object sender, EventArgs e)
        {
            JobToDateStatsForm jobStatsForm = new JobToDateStatsForm(jobNumber, true, "0", "0", "0", "0");
            jobStatsForm.ShowDialog();
            jobStatsForm.Dispose();
        }

        private void lblQCCheckRoll_Click(object sender, EventArgs e)
        {
            GetInputForm readBarcodeForm = new GetInputForm("Scan/Input Barcode", "R", 0, 0, true);
            readBarcodeForm.ShowDialog();
            if (readBarcodeForm.UserInput.Length > 0)
            {
                command = new SqlCommand("SELECT [Roll ID] FROM [Production ROll Table] WHERE [Production ID] = " + currentProductionRecord + " AND [Roll ID] = " + readBarcodeForm.UserInput.Substring(1), connection1);
                connection1.Open();
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();
                    ModulesClass.QCRollCheck(jobNumber.Substring(jobNumber.Length - 2, 2), wipProdType, readBarcodeForm.UserInput);

                }
                else
                {
                    reader.Close();
                    MessageBox.Show("Roll " + readBarcodeForm.UserInput + " does not belong to the current production record.", "Invalid Roll");
                }

                connection1.Close();
            }

            readBarcodeForm.Dispose();
        }

    }
}
