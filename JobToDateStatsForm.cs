
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace ShopFloor
{
    public partial class JobToDateStatsForm : Form
    {
        private string joborProdNumber;
        private bool jobNumber;
        private bool showUnwind;
        private SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
        private SqlDataReader reader;
        private SqlCommand command;
        private bool save;
        public JobToDateStatsForm(string joborProdNo, bool jobNo, string rewindFootage, string setupHrs, string runHrs, string dtHrs)
        {
            InitializeComponent();
            joborProdNumber = joborProdNo;
            jobNumber = jobNo;
            if (jobNo)
            {
                command = new SqlCommand("SELECT [Customer], [Job Description], CASE WHEN [Setup Hrs] + [Run Hrs] + [Job DT Hrs] != 0 THEN [Prod LF] / ([Setup Hrs] + [Run Hrs] + [Job DT Hrs]) / 60 END, CASE WHEN [Std Setup Hrs] + [Std Run Hrs] + [Std DT Hrs] != 0 THEN [Prod LF] / ([Std Setup Hrs] + [Std Run Hrs] + [Std DT Hrs]) / 60 END, CASE WHEN [Setup Hrs] + [Run Hrs] + [Job DT Hrs] != 0  AND [Std Setup Hrs] + [Std Run Hrs] + [Std DT Hrs] != 0 THEN [Prod LF] / ([Setup Hrs] + [Run Hrs] + [Job DT Hrs]) / 60 - [Prod LF] / ([Std Setup Hrs] + [Std Run Hrs] + [Std DT Hrs]) / 60 END, [Setup Hrs], [Std Setup Hrs], [Std Setup Hrs] - [Setup Hrs], [Run Hrs], [Std Run Hrs], [Std Run Hrs] - [Run Hrs], [Job DT Hrs], [Std DT Hrs], [Std DT Hrs] - [Job DT Hrs], [Setup Hrs] + [Run Hrs] + [Job DT Hrs], [Std Setup Hrs] + [Std Run Hrs] + [Std DT Hrs], [Std Setup Hrs] + [Std Run Hrs] + [Std DT Hrs] - ([Setup Hrs] + [Run Hrs] + [Job DT Hrs]), [Setup LF], [Std Make Ready LF], [Std Make Ready LF] - [Setup LF], CASE WHEN [Operation Name] = 'Adhesive showUnwind' THEN ([Unwind 1 Input LF] + [Unwind 2 Input LF]) / 2 ELSE [Unwind 1 Input LF] END - [Prod LF], [Std Scrap Feet], [Std Scrap Feet] - CASE WHEN [Operation Name] = 'Adhesive showUnwind' THEN ([Unwind 1 Input LF] + [Unwind 2 Input LF]) / 2 ELSE [Unwind 1 Input LF] END + [Prod LF], [Prod LF], [Unwind 1 Input LF], [Unwind 2 Input LF], [Req'd LF] FROM [Inventory Master Table] a OUTER APPLY [Get Job Stats] (a.[Master Item No]) b WHERE a.[Reference Item No] = " + joborProdNo, connection);
            }
            else
            {
                command = new SqlCommand("SELECT [Customer], [Job Description], CASE WHEN " + setupHrs + " + " + runHrs + " + " + dtHrs + " != 0 THEN ([Prod LF] + " + rewindFootage + ") / (" + setupHrs + " + " + runHrs + " + " + dtHrs + ") / 60 END, CASE WHEN [Std Setup Hrs] + [Std Run Hrs] + [Std DT Hrs] != 0 THEN ([Prod LF] + " + rewindFootage  + ") / ([Std Setup Hrs] + [Std Run Hrs] + [Std DT Hrs]) / 60 END, CASE WHEN " + setupHrs + " + " + runHrs + " + " + dtHrs + " != 0  AND [Std Setup Hrs] + [Std Run Hrs] + [Std DT Hrs] != 0 THEN ([Prod LF] + " + rewindFootage + ") / (" + setupHrs + " + " + runHrs + " + " + dtHrs + ") / 60 - ([Prod LF] + " + rewindFootage + ") / ([Std Setup Hrs] + [Std Run Hrs] + [Std DT Hrs]) / 60 END, " + setupHrs + ", [Std Setup Hrs], [Std Setup Hrs] - " + setupHrs + ", " + runHrs + ", [Std Run Hrs], [Std Run Hrs] - " + runHrs + ", " + dtHrs + ", [Std DT Hrs], [Std DT Hrs] - " + dtHrs + ", " + setupHrs + " + " + runHrs + " + " + dtHrs + ", [Std Setup Hrs] + [Std Run Hrs] + [Std DT Hrs], [Std Setup Hrs] + [Std Run Hrs] + [Std DT Hrs] - (" + setupHrs + " + " + runHrs + " + " + dtHrs + "), [Setup LF], [Std Make Ready LF], [Std Make Ready LF] - [Setup LF], CASE WHEN [Operation Name] = 'Adhesive showUnwind' THEN ([Unwind 1 Input LF] + [Unwind 2 Input LF]) / 2 ELSE [Unwind 1 Input LF] END - [Prod LF] - " + rewindFootage + ", [Std Scrap Feet], [Std Scrap Feet] - CASE WHEN [Operation Name] = 'Adhesive showUnwind' THEN ([Unwind 1 Input LF] + [Unwind 2 Input LF]) / 2 ELSE [Unwind 1 Input LF] END + [Prod LF] + " + rewindFootage + ", [Prod LF] + " + rewindFootage + ", [Unwind 1 Input LF], [Unwind 2 Input LF], [Operator] from [Get Standard Production Information](" + joborProdNo + ")", connection);
            }

            connection.Open();
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                if (jobNo)
                {
                    this.Text = "Job-to-Date Status as of " + DateTime.Now.ToLongDateString() + " at " + DateTime.Now.ToLongTimeString();
                    rtbJobInfo.Text = "Customer: " + reader[0].ToString() + "\r\n\r\nJob: " + joborProdNo.ToString() + " - " + reader[1].ToString();
                    if (joborProdNo.Substring(joborProdNo.Length - 2, 1) == "2" || joborProdNo.Substring(joborProdNo.Length - 2, 1) == "5")
                    {
                        showUnwind = true;
                    }
                    else
                    {
                        showUnwind = false;
                    }
                }
                else
                {
                    cmdClose.Visible = false;
                    cmdSave.Visible = true;
                    cmdAbort.Visible = true;
                    this.Text = "Summary as of " + DateTime.Now.ToLongDateString() + " at " + DateTime.Now.ToLongTimeString() + " for Production ID " + joborProdNo.ToString();
                    rtbJobInfo.Text = "Customer: " + reader[0].ToString() + "\r\n\r\nJob: " + reader[25].ToString() + " - " + reader[1].ToString() + "\r\nOperator: " + reader[26].ToString();
                    if (reader[26].ToString().Substring(reader[26].ToString().Length - 2, 1) == "2" || reader[26].ToString().Substring(reader[26].ToString().Length - 2, 1) == "5")
                    {
                        showUnwind = true;
                    }
                    else
                    {
                        showUnwind = false;
                    }
                }

                rtbJobInfo.Text = "Customer: " + reader[0].ToString() + "\r\n\r\nJob: " + joborProdNo.ToString() + " - " + reader[1].ToString();
                if (reader[2] != DBNull.Value)
                {
                    txtActFtperMin.Text = ((decimal)reader[2]).ToString("N2");
                }

                if (reader[3] != DBNull.Value)
                {
                    txtStdFtperMin.Text = ((decimal)reader[3]).ToString("N2");
                    if (reader[2] != DBNull.Value)
                    {
                        if ((decimal)reader[4] < 0)
                        {
                            txtFtperMinVar.ForeColor = System.Drawing.Color.Red;
                            txtFtPctVar.ForeColor = System.Drawing.Color.Red;
                        }

                        txtFtperMinVar.Text = ((decimal)reader[4]).ToString("#,##0.00;(#,##0.00)");
                        if ((decimal)reader[3] != 0)
                        {
                            txtFtPctVar.Text = ((decimal)reader[4] / (decimal)reader[3]).ToString("0.00%;(0.00%)");
                        }
                    }
                }

                if (reader[5] != DBNull.Value)
                {
                    txtActSetupHrs.Text = ((decimal)reader[5]).ToString("N2");
                }

                if (reader[6] != DBNull.Value)
                {
                    txtStdSetupHrs.Text = ((decimal)reader[6]).ToString("N2");
                    if (reader[5] != DBNull.Value)
                    {
                        if ((decimal)reader[7] < 0)
                        {
                            txtSetupHrsVar.ForeColor = System.Drawing.Color.Red;
                            txtSetupPctVar.ForeColor = System.Drawing.Color.Red;
                        }

                        txtSetupHrsVar.Text = ((decimal)reader[7]).ToString("#,##0.00;(#,##0.00)");
                        if ((decimal)reader[6] != 0)
                        {
                            txtSetupPctVar.Text = ((decimal)reader[7] / (decimal)reader[6]).ToString("0.00%;(0.00%)");
                        }
                    }
                }

                if (reader[8] != DBNull.Value)
                {
                    txtActRunHrs.Text = ((decimal)reader[8]).ToString("N2");
                }

                if (reader[9] != DBNull.Value)
                {
                    txtStdRunHrs.Text = ((decimal)reader[9]).ToString("N2");
                    if (reader[8] != DBNull.Value)
                    {
                        if ((decimal)reader[10] < 0)
                        {
                            txtRunHrsVar.ForeColor = System.Drawing.Color.Red;
                            txtRunPctVar.ForeColor = System.Drawing.Color.Red;
                        }

                        txtRunHrsVar.Text = ((decimal)reader[10]).ToString("#,##0.00;(#,##0.00)");
                        if ((decimal)reader[9] != 0)
                        {
                            txtRunPctVar.Text = ((decimal)reader[10] / (decimal)reader[9]).ToString("0.00%;(0.00%)");
                        }
                    }
                }

                if (reader[11] != DBNull.Value)
                {
                    txtActDTHrs.Text = ((decimal)reader[11]).ToString("N2");
                }

                if (reader[12] != DBNull.Value)
                {
                    txtStdDTHrs.Text = ((decimal)reader[12]).ToString("N2");
                    if (reader[11] != DBNull.Value)
                    {
                        if ((decimal)reader[13] < 0)
                        {
                            txtDTHrsVar.ForeColor = System.Drawing.Color.Red;
                            txtDTPctVar.ForeColor = System.Drawing.Color.Red;
                        }

                        txtDTHrsVar.Text = ((decimal)reader[13]).ToString("#,##0.00;(#,##0.00)");
                        if ((decimal)reader[12] != 0)
                        {
                            txtDTPctVar.Text = ((decimal)reader[13] / (decimal)reader[12]).ToString("0.00%;(0.00%)");
                        }
                    }
                }

                if (reader[14] != DBNull.Value)
                {
                    txtActTotHrs.Text = ((decimal)reader[14]).ToString("N2");
                }

                if (reader[15] != DBNull.Value)
                {
                    txtStdTotHrs.Text = ((decimal)reader[15]).ToString("N2");
                    if (reader[14] != DBNull.Value)
                    {
                        if ((decimal)reader[16] < 0)
                        {
                            txtTotHrsVar.ForeColor = System.Drawing.Color.Red;
                            txtTotPctVar.ForeColor = System.Drawing.Color.Red;
                        }

                        txtTotHrsVar.Text = ((decimal)reader[16]).ToString("#,##0.00;(#,##0.00)");
                        if ((decimal)reader[15] != 0)
                        {
                            txtTotPctVar.Text = ((decimal)reader[16] / (decimal)reader[15]).ToString("0.00%;(0.00%)");
                        }
                    }
                }

                if (reader[17] != DBNull.Value)
                {
                    txtActMakeReadyLF.Text = ((decimal)reader[17]).ToString("N0");
                }

                if (reader[18] != DBNull.Value)
                {
                    txtStdMakeReadyLF.Text = ((decimal)reader[18]).ToString("N0");
                    if (reader[17] != DBNull.Value)
                    {
                        if ((decimal)reader[19] < 0)
                        {
                            txtMakeReadyLFVar.ForeColor = System.Drawing.Color.Red;
                            txtMakeReadyPctVar.ForeColor = System.Drawing.Color.Red;
                        }

                        txtMakeReadyLFVar.Text = ((decimal)reader[19]).ToString("#,##0;(#,##0)");
                        if ((decimal)reader[18] != 0)
                        {
                            txtMakeReadyPctVar.Text = ((decimal)reader[19] / (decimal)reader[18]).ToString("0.00%;(0.00%)");
                        }
                    }
                }

                if (reader[20] != DBNull.Value)
                {
                    txtActScrapLF.Text = ((decimal)reader[20]).ToString("N0");
                }

                if (reader[21] != DBNull.Value)
                {
                   txtStdScrapLF.Text = ((decimal)reader[21]).ToString("N0");
                    if (reader[20] != DBNull.Value)
                    {
                        if ((decimal)reader[22] < 0)
                        {
                            txtScrapLFVar.ForeColor = System.Drawing.Color.Red;
                            txtScrapPctVar.ForeColor = System.Drawing.Color.Red;
                        }

                        txtScrapLFVar.Text = ((decimal)reader[22]).ToString("#,##0;(#,##0)");
                        if ((decimal)reader[21] != 0)
                        {
                           txtScrapPctVar.Text = ((decimal)reader[22] / (decimal)reader[21]).ToString("0.00%;(0.00%)");
                        }
                    }
                }

                if (reader[23] != DBNull.Value)
                {
                    txtProdLF.Text = ((decimal)reader[23]).ToString("N0");
                }

                if (reader[24] != DBNull.Value)
                {
                    txtUnwind1LF.Text = ((decimal)reader[24]).ToString("N0");
                }

                if (reader[25] != DBNull.Value)
                {
                    txtUnwind2LF.Text = ((decimal)reader[25]).ToString("N0");
                }

                if (jobNo)
                {
                    if (reader[26] != DBNull.Value)
                    {
                        txtReqdLF.Text = ((decimal)reader[26]).ToString("N0");
                        if ((decimal)reader[26] > 0)
                        {
                            txtPctComplete.Text = ((decimal)reader[23] / (decimal)reader[26]).ToString("0.00%");
                        }
                    }
                }
                else
                {
                    lblReqdLF.Visible = false;
                    txtReqdLF.Visible = false;
                    lblPctComplete.Visible = false;
                    txtPctComplete.Visible = false;
                }

                reader.Close();
                connection.Close();
            }
            else
            {
                MessageBox.Show("Error - no production records found.", "No Records");
                reader.Close();
                connection.Close();
                this.Close();
            }
       

        }

        public bool Save
        {
            get
            {
                return save;
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdShowFilmUsage_Click(object sender, EventArgs e)
        {
            ConsumedFilmForm currentConsumedFilmForm;
            SqlConnection consumedFilmConnection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
            SqlCommand command;
            currentConsumedFilmForm = new ConsumedFilmForm(showUnwind);
            if (jobNumber)
            {
                command = new SqlCommand("SELECT e.[Name], a.[Roll ID], CAST(a.[Unwind No] AS int), b.[Width], a.[Start Usage Date], CAST(a.[Start Usage LF] AS int), a.[End Usage Date], CAST(ISNULL(a.[End Usage LF], 0) AS int), c.[Description] FROM [Production Consumed Roll Table] a INNER JOIN ([Roll Table] b INNER JOIN [Inventory Master Table] c ON b.[Master Item No] = c.[Master Item No]) ON a.[Roll ID] = b.[Roll ID] INNER JOIN ([Production Master Table] d INNER JOIN [Operator Table] e ON d.[Operator ID] = e.[Operator ID] INNER JOIN [Inventory Master Table] f ON d.[Master Item No] = f.[Master Item No]) ON a.[Start Production ID] = d.[Production ID] WHERE a.[Start Usage LF] != ISNULL(a.[End Usage LF], 0) AND f.[Reference Item No] = " + joborProdNumber + " ORDER BY a.[Start Usage Date]", consumedFilmConnection);
            }
            else
            {
                command = new SqlCommand("SELECT e.[Name], a.[Roll ID], CAST(a.[Unwind No] AS int), b.[Width], a.[Start Usage Date], CAST(a.[Start Usage LF] AS int), a.[End Usage Date], CAST(ISNULL(a.[End Usage LF], 0) AS int), c.[Description] FROM [Production Consumed Roll Table] a INNER JOIN ([Roll Table] b INNER JOIN [Inventory Master Table] c ON b.[Master Item No] = c.[Master Item No]) ON a.[Roll ID] = b.[Roll ID] INNER JOIN ([Production Master Table] d INNER JOIN [Operator Table] e ON d.[Operator ID] = e.[Operator ID]) ON a.[Start Production ID] = d.[Production ID] WHERE a.[Start Usage LF] != ISNULL(a.[End Usage LF], 0) AND a.[Start Production ID] = " + joborProdNumber + " ORDER BY a.[Start Usage Date]", consumedFilmConnection);
            }

            consumedFilmConnection.Open();
            SqlDataReader consumedFilmReader = command.ExecuteReader();
            while (consumedFilmReader.Read())
            {
                currentConsumedFilmForm.AddRoll(consumedFilmReader[0].ToString(), (DateTime)consumedFilmReader[4], consumedFilmReader[1].ToString(), "Consumed", consumedFilmReader[2].ToString(), ((decimal)consumedFilmReader[3]).ToString("N4") + "\" " + consumedFilmReader[8].ToString(), (int)consumedFilmReader[5]);
                if (consumedFilmReader[6] != DBNull.Value && (int)consumedFilmReader[7] > 0)
                {
                    // There is a return
                    currentConsumedFilmForm.AddRoll(consumedFilmReader[0].ToString(), (DateTime)consumedFilmReader[6], consumedFilmReader[1].ToString(), "Returned", consumedFilmReader[2].ToString(), ((decimal)consumedFilmReader[3]).ToString("N4") + "\" " + consumedFilmReader[8].ToString(), (int)consumedFilmReader[7]);
                }
            }

            consumedFilmReader.Close();
            consumedFilmConnection.Close();
            currentConsumedFilmForm.ShowDialog();
            currentConsumedFilmForm.Dispose();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            save = true;
            this.Close();
        }

        private void cmdAbort_Click(object sender, EventArgs e)
        {
            save = false;
            this.Close();
        }
    }
}
