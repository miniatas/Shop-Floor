/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 7/20/2011
 * Time: 4:05 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace ShopFloor
{
    using ShopFloor.Classes;
    using System;
	using System.Data.SqlClient;
	using System.Drawing;
	using System.Drawing.Printing;
	using System.Windows.Forms;
	
	/// <summary>
	/// All barcode printing routines.
	/// </summary>
	public static class PrintClass
	{
		private static string id;
		private static string productType;
		private static string jobNumber;
		private static string description;
		private static string finishedGoodType;
        private static string userName = StartupForm.UserName;
        private static bool blindShipment = false;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        public static void PrintOpacityQCCheck(PrintQCOpacity printQCOpacity)
        {
            PrintDocument labelDocument = new PrintDocument();
            // labelDocument.PrinterSettings.PrinterName = MainForm.strRollPrinterName;
            labelDocument.DefaultPageSettings.Landscape = false;
            labelDocument.DefaultPageSettings.Margins.Top = 10;
            labelDocument.DefaultPageSettings.Margins.Left = 10;
            labelDocument.DefaultPageSettings.Margins.Right = 10;
            labelDocument.DefaultPageSettings.Margins.Bottom = 0;


            try
            {
               
                //labelDocument.PrinterSettings.PrinterName = System.Drawing.Printing.;
                labelDocument.PrintPage += (sender, e) => PrintOpacityQCForm(printQCOpacity, e);
                labelDocument.Print();
                labelDocument.Dispose();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);

            }
        }

        private static void PrintOpacityQCForm(PrintQCOpacity printQCOpacity, PrintPageEventArgs e) {

            Graphics g = e.Graphics;
            float currentY;

            currentY = e.MarginBounds.Top + PrintMessageWithFont(g, e, "Arial", 15, "Job #  " + printQCOpacity.JobNumber, e.MarginBounds.Top, Brushes.Black, StringAlignment.Near, null);
            currentY += PrintMessageWithFont(g, e, "Arial", 15, "Roll #:  " + printQCOpacity.RollNumber, currentY + 5, Brushes.Black, StringAlignment.Near, null);
            currentY += PrintMessageWithFont(g, e, "Arial", 15, "Opacity:  " + printQCOpacity.Opacity, currentY + 5, Brushes.Black, StringAlignment.Near, null);
            currentY += PrintMessageWithFont(g, e, "Arial", 15, "Date/Time:  " + printQCOpacity.CurrentDateTime, currentY + 5, Brushes.Black, StringAlignment.Near, null);
            //currentY += PrintMessageWithFont(g, e, "Arial", 15, "Operator:  " + printQCOpacity.OperatorName, currentY + 5, Brushes.Black, StringAlignment.Near, null);
            currentY += PrintMessageWithFont(g, e, "Arial", 18, "Lead Signature ", currentY + 5, Brushes.Black, StringAlignment.Center,  FontStyle.Underline);

            g.Dispose();
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
        public static void Label(string lookupId)
        {
            bool preview = false;
            int noCopies = 0;
            SqlCommand command;
            SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
            id = lookupId.Substring(1);
            if (lookupId.Substring(0, 1) == "P")
            {
                command = new SqlCommand("SELECT TOP 1 COALESCE(d.[Description], j.[Description], g.[Description]), COALESCE(c.[Reference Item No], i.[Reference Item No], f.[Reference Item No]), ISNULL(c.[Description], f.[Description]), CASE WHEN b.[Roll ID] IS NOT NULL THEN 'Roll' ELSE 'Case' END FROM [Pallet Table] a LEFT JOIN ([Roll Table] b INNER JOIN ([Inventory Master Table] c INNER JOIN [Item Type Table] d ON c.[Item Type No] = d.[Item Type No]) ON b.[Master Item No] = c.[Master Item No]) ON a.[Pallet ID] = b.[Pallet ID] LEFT JOIN ([Case Table] h INNER JOIN ([Inventory Master Table] i INNER JOIN [Item Type Table] j ON i.[Item Type No] = j.[Item Type No]) ON h.[Master Item No] = i.[Master Item No]) ON a.[Pallet ID] = h.[Pallet ID] LEFT JOIN ([Current Pallets at Machine Table] e INNER JOIN ([Inventory Master Table] f INNER JOIN [Item Type Table] g ON f.[Item Type No] = g.[Item Type No]) ON e.[Master Item No] =f.[Master Item No]) ON a.[Pallet ID] = e.[Pallet ID] where a.[Pallet ID] = " + id, connection);
            }
            else if (lookupId.Substring(0, 1) == "R")
            {
                command = new SqlCommand("select c.[Description], b.[Reference Item No], b.[Description] from [Roll Table] a inner join ([Inventory Master Table] b inner join [Item Type Table] c on b.[Item Type No]=c.[Item Type No]) on a.[Master Item No]=b.[Master Item No] where a.[Roll ID]=" + id, connection);
            }
            else if (lookupId.Substring(0, 1) == "C")
            {
                command = new SqlCommand("select c.[Description], b.[Reference Item No], b.[Description] from [Case Table] a inner join ([Inventory Master Table] b inner join [Item Type Table] c on b.[Item Type No]=c.[Item Type No]) on a.[Master Item No]=b.[Master Item No] where a.[Case ID]=" + id, connection);
            }
            else if (lookupId.Substring(0, 1) == "T")
            {
                // Tote
                command = new SqlCommand("select [Tote ID] from [Tote Table] where [Tote ID]=" + id, connection);
            }
            else
            {
                // Job Jacket Number for PIck List
                command = new SqlCommand("SELECT [Description] FROM [Inventory Master Table] WHERE [Reference Item No] = " + lookupId.Substring(1) + " AND [Item Type No] IN (2, 3)", connection);
            }

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                if (lookupId.Substring(0, 1) == "T")
                {
                    productType = "Adhesive";
                }
                else if (lookupId.Substring(0, 1) == "J")
                {
                    productType = "Pick List";
                    jobNumber = lookupId;
                    description = reader[0].ToString();
                }
                else
                {
                    productType = reader[0].ToString();
                    jobNumber = reader[1].ToString();
                    description = reader[2].ToString();
                    if (lookupId.Substring(0, 1) == "P")
                    {
                        finishedGoodType = reader[3].ToString();
                    }
                }

                reader.Close();

                //Check for Finished WIP or Good item and - if so - whether it is a blind shipment
                if ((lookupId.Substring(0, 1) == "R" || lookupId.Substring(0, 1) == "C") && (productType == "WIP" || productType == "Finished Goods"))
                {
                    if (lookupId.Substring(0, 1) == "R")
                    {
                        //Roll
                        if (productType == "WIP")
                        {
                            command = new SqlCommand("SELECT CAST(FLOOR(b.[Reference Item No] / 100) AS nvarchar(10)) FROM [Roll Table] a INNER JOIN [Inventory Master Table] b ON a.[Master Item No] = b.[Master Item No] WHERE [Roll ID] = " + id, connection);
                        }
                        else
                        {
                            command = new SqlCommand("SELECT CAST(b.[Reference Item No] AS nvarchar(10)) FROM [Roll Table] a INNER JOIN [Inventory Master Table] b ON a.[Master Item No] = b.[Master Item No] WHERE [Roll ID] = " + id, connection);
                        }
                    }
                    else
                    {
                        //Case
                        if (productType == "WIP")
                        {
                            command = new SqlCommand("SELECT CAST(FLOOR(b.[Reference Item No] / 100) AS nvarchar(10)) FROM [Case Table] a INNER JOIN [Inventory Master Table] b ON a.[Master Item No] = b.[Master Item No] WHERE [Case ID] = " + id, connection);
                        }
                        else
                        {
                            command = new SqlCommand("SELECT CAST(b.[Reference Item No] AS nvarchar(10)) FROM [Case Table] a INNER JOIN [Inventory Master Table] b ON a.[Master Item No] = b.[Master Item No] WHERE [Case ID] = " + id, connection);
                        }
                    }

                    string jobNo = command.ExecuteScalar().ToString();
                    if (!string.IsNullOrEmpty(jobNo))
                    {
                        //Check whether Blind Shipment
                        command = new SqlCommand("SELECT ISNULL([JobBlindShipment], 0) FROM [JobJackets].[dbo].[tblJobTicket] WHERE [JobJacketNo] = '" + jobNo + "'", connection);
                        blindShipment = (bool)command.ExecuteScalar();
                    }
                }


				connection.Close();
				//bool MainForm.LabelPrinter = false;
//				if (MainForm.MachineNumber != "0" && MainForm.MachineNumber.Substring(0, 2) == "13")
//				{
//					MainForm.LabelPrinter = true;				
//				}
				
				PrintDocument labelDocument = new PrintDocument();
				// labelDocument.PrinterSettings.PrinterName = MainForm.strRollPrinterName;
				labelDocument.DefaultPageSettings.Landscape = true;
				labelDocument.DefaultPageSettings.Margins.Top = 10;
				labelDocument.DefaultPageSettings.Margins.Left = 10;
				labelDocument.DefaultPageSettings.Margins.Right = 10;
				labelDocument.DefaultPageSettings.Margins.Bottom = 0;
				switch (productType)
				{
					case "Raw Film":
						if (MainForm.LabelPrinter)
						{
							MessageBox.Show("Error - you cannot print raw film labels on a label printer", "Invalid Printer");
						}
						else if (lookupId.Substring(0, 1) == "R")
						{
							// Print roll labels
							labelDocument.PrintPage += new PrintPageEventHandler(PrintFilmRollLabel);
						}
						else
						{
						    labelDocument.PrintPage += new PrintPageEventHandler(PrintFilmPalletLabel);
						}
						
						break;
					case "WIP":
						if (jobNumber.Substring(jobNumber.Length - 2, 2) == "31" || jobNumber.Substring(jobNumber.Length - 2, 2) == "32" || jobNumber.Substring(jobNumber.Length - 2, 2) == "33" || jobNumber.Substring(jobNumber.Length - 2, 2) == "36" || jobNumber.Substring(jobNumber.Length - 2, 2) == "37" || jobNumber.Substring(jobNumber.Length - 2, 2) == "61")
					    {
							if (!MainForm.LabelPrinter && lookupId.Substring(0, 1) == "R")
							{
								labelDocument.PrintPage += new PrintPageEventHandler(PrintSlitorFGRollPageLabel);
							}
							else if (lookupId.Substring(0, 1) == "R")
							{
								jobNumber = jobNumber.Substring(0, jobNumber.Length - 2);
								labelDocument.PrintPage += new PrintPageEventHandler(PrintSlitorFGCoreLabel);
							}
							else if (MainForm.LabelPrinter)
							{
								labelDocument.PrintPage += new PrintPageEventHandler(PrintSlitRollPalletLabel);
							}
							else
							{
								labelDocument.PrintPage += new PrintPageEventHandler(PrintSlitWIPPalletDescriptionLabel);
							}
					    }
						else if (jobNumber.Substring(jobNumber.Length - 2, 2) == "51")
						{
							if (!MainForm.LabelPrinter && lookupId.Substring(0, 1) == "C")
							{
								labelDocument.PrintPage += new PrintPageEventHandler(PrintCasePageLabel);
							}
							else if (lookupId.Substring(0, 1) == "C")
							{
								jobNumber = jobNumber.Substring(0, jobNumber.Length - 2);
								labelDocument.PrintPage += new PrintPageEventHandler(PrintCaseLabel);
							}
							else if (MainForm.LabelPrinter)
							{
								labelDocument.PrintPage += new PrintPageEventHandler(PrintCasePalletLabel);
							}
							else
							{
								labelDocument.PrintPage += new PrintPageEventHandler(PrintCasePalletDescriptionLabel);
							}
						}
					    else
					    {
					    	if (MainForm.LabelPrinter)
					    	{
								MessageBox.Show("Error - you cannot print non-slit or bag WIP film label on a label printer", "Invalid Printer");
					    	}
					    	else
					    	{
								labelDocument.PrintPage += new PrintPageEventHandler(PrintWIPRollLabel);
					    	}
					    }
					    
						break;
					case "Combo WIP":
						if (MainForm.LabelPrinter)
						{
							MessageBox.Show("Error - you cannot print non-slit WIP film labels on a label printer", "Invalid Printer");
						}
						else
						{
							labelDocument.PrintPage += new PrintPageEventHandler(PrintWIPRollLabel);
						}
						
						break;
					case "Finished Goods":
						if (lookupId.Substring(0, 1) == "R")
						{
							if (!MainForm.LabelPrinter)
							{
								labelDocument.PrintPage += new PrintPageEventHandler(PrintSlitorFGRollPageLabel);
							}
							else
							{
								labelDocument.PrintPage += new PrintPageEventHandler(PrintSlitorFGCoreLabel);
							}
						}
						else if (lookupId.Substring(0, 1) == "C")
						{
							if (!MainForm.LabelPrinter)
							{
								labelDocument.PrintPage += new PrintPageEventHandler(PrintCasePageLabel);
							}
							else
							{
								labelDocument.PrintPage += new PrintPageEventHandler(PrintCaseLabel);
							}
						}
						else
						{
							if (MainForm.LabelPrinter)
							{
								MessageBox.Show("Error - you cannot print finished goods pallet labels on a label printer", "Invalid Printer");
							}
							else
							{
								if (finishedGoodType == "Roll")
								{
									labelDocument.PrintPage += new PrintPageEventHandler(PrintFGRollPalletLabel);
								}
								else
								{
									labelDocument.PrintPage += new PrintPageEventHandler(PrintFGCasePalletLabel);
								}
								preview = true;
								noCopies = 4;
							}
						}
						
						break;
					case "Adhesive":
						if (MainForm.LabelPrinter)
						{
							MessageBox.Show("Error - you cannot print adhesive tote labels on a label printer", "Invalid Printer");
						}
						else
						{
							labelDocument.PrintPage += new PrintPageEventHandler(PrintAdhesiveToteLabel);
						}
						
						break;
					case "Pick List":
						command = new SqlCommand("SELECT COUNT(*) FROM [Allocation Master Table] a INNER JOIN [Inventory Master Table] b ON a.[Master Item No] = b.[Master Item No] WHERE a.[Pick Date] IS NOT NULL AND a.[Release Date] IS NULL AND a.[Void Date] IS NULL AND b.[Item Type No] IN (2, 3) AND b.[Reference Item No] = " + lookupId.Substring(1), connection);
						connection.Open();
						int numberOfPickLists = (int)command.ExecuteScalar();
						connection.Close();
						if (numberOfPickLists == 0)
						{
							MessageBox.Show("Error - ther are no open pick lists for job " + lookupId + " - " + description, "Nothing to Print");
						}
						else
						{
							if (MainForm.LabelPrinter)
							{
								MessageBox.Show("Error - you cannot print film pick lists on a label printer", "Invalid Printer");
							}
							else
							{
								labelDocument.PrintPage += new PrintPageEventHandler(PrintFilmPickList);
								preview = true;
								noCopies = 1;
							}
						}
						
						break;
				}
				
				if (preview)
				{
					MyPrintPreviewDialog prevDialogLabel = new MyPrintPreviewDialog(noCopies);
					prevDialogLabel.Document = labelDocument;
					prevDialogLabel.WindowState = FormWindowState.Maximized;
					prevDialogLabel.ShowDialog();
					prevDialogLabel.Dispose();
				}
				else
				{
					labelDocument.Print();
				}
                
                labelDocument.Dispose();
			}
			else
			{
				reader.Close();
				connection.Close();
				if (lookupId.Substring(0, 1) == "P")
				{
					MessageBox.Show("Error - pallet " + lookupId + " not found", "Invalid Pallet");
				}
				else if (lookupId.Substring(0, 1) == "R")
				{
					MessageBox.Show("Error - roll " + lookupId + " not found", "Invalid Roll");
				}
				else if (lookupId.Substring(0, 1) == "C")
				{
					MessageBox.Show("Error - case" + lookupId + " not found", "Invalid Case");
				}
				         
				else  if (lookupId.Substring(0, 1) == "T")
				{
					// Tote
					MessageBox.Show("Error - Adhesives Tote " + lookupId + " not found", "Invalid Tote");
				}
				else 
				{
					// Job Jacket for Pick List
					MessageBox.Show("Error - Job " + lookupId + " not found", "Invalid Job");
				}
			}
		}

        public static void Comment(string rollOrCaseID, string comment)
        {
            id = rollOrCaseID;
            description = comment;
            PrintDocument CommentDocument = new PrintDocument();
            CommentDocument.DefaultPageSettings.Landscape = true;
            CommentDocument.DefaultPageSettings.Margins.Top = 10;
            CommentDocument.DefaultPageSettings.Margins.Left = 10;
            CommentDocument.DefaultPageSettings.Margins.Right = 10;
            CommentDocument.DefaultPageSettings.Margins.Bottom = 0;
            if (MainForm.LabelPrinter)
            {
                CommentDocument.PrintPage += new PrintPageEventHandler(PrintLabelComment);
            }
            else
            {
                CommentDocument.PrintPage += new PrintPageEventHandler(PrintFullPageComment);
            }

            CommentDocument.Print();
            CommentDocument.Dispose();
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private static void PrintFilmRollLabel(object sender, PrintPageEventArgs e)
		{
			SqlCommand command;
			string notesToPrint = string.Empty;
			SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
			command = new SqlCommand("SELECT NULL /*c.[Film Specification]*/, NULL /*c.[Notes]*/, a.[Notes], a.[Pallet ID], a.[Roll ID], CASE WHEN d.[Film Type ID] < 10 THEN '0' + CAST(d.[Film Type ID] AS char(1)) ELSE CAST(d.[Film Type ID] AS char(2)) END + '-' + REPLICATE('0', 5 - LEN(d.[Part No])) + CAST(d.[Part No] as varchar(5)) + '-' + CAST(FLOOR(c.[width]) AS char(2)) + CASE WHEN FLOOR((c.[width] - FLOOR(c.[width])) * 16) < 10 THEN '0' ELSE '' END + CAST(FLOOR((c.[width] - FLOOR(c.[width])) * 16) AS varchar(2)), b.width, d.[Description], 'LF', CAST(ROUND(b.[Current LF], 0) as int), CAST(ROUND(b.[Original Pounds] * b.[Current LF] /  b.[Original LF], 0) AS int), a.[PO Number], b.[Create Date], CAST(c.[Sample] AS int), CAST(c.[Cust Supplied] AS int), CAST(CASE WHEN d.[Film Type] = 'NON-WOVEN' THEN 1 ELSE 0 END AS int), a.[Vendor Roll ID] FROM [Roll PO Table] a INNER JOIN [Roll Table] b ON a.[Roll ID] = b.[Roll ID] INNER JOIN ([Film Purchase Detail Table] c INNER JOIN [Film View] d ON c.[Master Item No] = d.[Master Item No]) ON a.[PO Number] = c.[PO Number] AND a.[PO Line Item No] = c.[PO Line Item No] WHERE a.[Roll ID] = " + id, connection);
            connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			reader.Read();
			Brush brushColor = GetBrushColor((int)reader[13], (int)reader[14], (int)reader[15]);
			if (reader[16] != DBNull.Value)
			{
				notesToPrint = "Vendor Roll ID: " + reader[16].ToString() + "\r\n";
			}
			
			if (reader[0] != DBNull.Value)
			{
				notesToPrint += reader[0].ToString() + "\r\n";
			}
				
			if (reader[1] != DBNull.Value)
			{
				notesToPrint += reader[1].ToString() + "\r\n";
			}
			
			if (reader[2] != DBNull.Value)
			{
				notesToPrint += "Roll-Specific Info: " + reader[2].ToString() + "\r\n";
			}

			PrintPageLabel(e, "P" + reader[3].ToString(), "R" + reader[4].ToString(), ((decimal)reader[6]).ToString("N4") + "\" " + reader[7].ToString(), reader[5].ToString(), notesToPrint, 0, reader[8].ToString(), (int)reader[9], (int)reader[10], reader[11].ToString(), (DateTime)reader[12], brushColor);
			reader.Close();
			connection.Close();
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private static void PrintWIPRollLabel(object sender, PrintPageEventArgs e)
		{
			Graphics g = e.Graphics;
			SqlCommand command;
			SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
			if (productType == "WIP")
			{
				command = new SqlCommand("select c.[JobJacketNo],d.[ItemName],case when isnull(c.[NoCut],0)=0 then 1 else abs(c.[NoCut]) end,cast(round(a.[Current LF],0) as int),cast(round(a.[Original Pounds]*a.[Current LF]/a.[Original LF],0) as int),ltrim(rtrim(b.[Notes])),a.[Create Date],isnull(cast(b.[Set No] as int),0) from [Roll Table] a left join [Production Roll Table] b on a.[Roll ID]=b.[Roll ID], [JobJackets].[dbo].[tblJobTicket] c inner join [JobJackets].[dbo].[tblItem] d on c.[ItemNo]=d.[ItemNo] where a.[Roll ID]=" + id + " and c.[JobJacketNo]='" + jobNumber.Substring(0, jobNumber.Length - 2) + "'", connection);
			}
			else
			{
				// Combo Job
				command = new SqlCommand("select c.[JobJacketNo],d.[ItemName],case when isnull(c.[NoCut],0)=0 then 1 else abs(c.[NoCut]) end,cast(round(a.[Current LF],0) as int),cast(round(a.[Original Pounds]*a.[Current LF]/a.[Original LF],0) as int),ltrim(rtrim(b.[Notes])),a.[Create Date],isnull(cast(b.[Set No] as int),0) from [Roll Table] a left join [Production Roll Table] b on a.[Roll ID]=b.[Roll ID], [JobJackets].[dbo].[tblJobTicket] c inner join [JobJackets].[dbo].[tblItem] d on c.[ItemNo]=d.[ItemNo] where a.[Roll ID]=" + id + " and c.[ComboNo]=" + jobNumber.Substring(0, jobNumber.Length - 2) + " order by c.[JobJacketNo]", connection);
			}
			
			connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			reader.Read();
			int linearFeet = (int)reader[3];
			int pounds = (int)reader[4];
			string notes = reader[5].ToString();
			DateTime createDate = (DateTime)reader[6];
			int setNumber = (int)reader[7];
			string jobInformation = string.Empty;
			if (productType == "WIP")
			{
				if ((int)reader[2] == 1)
				{
					jobInformation = reader[1].ToString() + " (1 Stream)";
				}
				else
				{
					jobInformation = reader[1].ToString() + " (" + reader[2].ToString() + " Streams)";
				}
			}
			else 
			{
				// Combo Job
				do
				{
					if ((int)reader[2] == 1)
					{
						jobInformation += "Job " + reader[0].ToString() + " - " + reader[1].ToString() + " (1 Stream)\r\n";
					}
					else
					{
						jobInformation += "Job " + reader[0].ToString() + " - " + reader[1].ToString() + " (" + reader[2].ToString() + " Streams)\r\n";
					}
				}
				while (reader.Read());
			}
			
			reader.Close();
			command = new SqlCommand("SELECT TOP 1 [Notes] + ' (QC''d by ' + [Created By] + ')' FROM [Adjustment Transaction Table] WHERE [Roll ID] = " + id + " AND [Adjustment Reason ID] = 56 AND [Notes] IS NOT NULL ORDER BY [Create Date] DESC", connection);
			string qcNotes = (string)command.ExecuteScalar();
			if (!string.IsNullOrEmpty(qcNotes))
			{
				notes = notes + ' ' + qcNotes;
            }
			connection.Close();
			float currentY = e.MarginBounds.Top + PrintMessage(g, e, "Arial", 50, "Roll Label (R" + id + ")",  e.MarginBounds.Top, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Free 3 of 9", 100, "*R" + id + "*", currentY + 20, Brushes.Black, StringAlignment.Center);
			if (jobNumber.Length == 7)
			{
				currentY += PrintMessage(g, e, "Arial", 50, "J0" + jobNumber.Substring(0, jobNumber.Length - 2) + "-" + jobNumber.Substring(jobNumber.Length - 2, 2) + "-" + setNumber.ToString(), currentY + 20, Brushes.Black, StringAlignment.Center);
			}
			else if (jobNumber.Length == 6)
			{
				currentY += PrintMessage(g, e, "Arial", 50, "J00" + jobNumber.Substring(0, 4) + "-" + jobNumber.Substring(4, 2) + "-" + setNumber.ToString(), currentY + 20, Brushes.Black, StringAlignment.Center);
			}
			else
			{
				currentY += PrintMessage(g, e, "Arial", 50, "J" + jobNumber.Substring(0, jobNumber.Length - 2) + "-" + jobNumber.Substring(jobNumber.Length - 2, 2) + "-" + setNumber.ToString(), currentY + 20, Brushes.Black, StringAlignment.Center);
			}
			
			if (productType == "WIP")
			{
				currentY += PrintMessage(g, e, "Arial", 30, jobInformation, currentY + 20, Brushes.Black, StringAlignment.Center);
			}
			else
			{
				currentY += PrintMessage(g, e, "Arial", 20, jobInformation.Substring(0, jobInformation.Length - 2), currentY + 20, Brushes.Black, StringAlignment.Center);
			}
			
			currentY += PrintMessage(g, e, "Arial", 30, createDate.ToString(), currentY + 30, Brushes.Black, StringAlignment.Center);
			if (notes.Length > 0)
			{
				currentY += PrintMessage(g, e, "Arial", 20, notes, currentY + 30, Brushes.Black, StringAlignment.Center);
			}
		    
			currentY += PrintMessage(g, e, "Arial", 20, linearFeet.ToString("N0") + " LF          " + pounds.ToString("N0") + " Standard Film Pounds", currentY + 40, Brushes.Black, StringAlignment.Center);
		    currentY += PrintMessage(g, e, "Free 3 of 9", 30, "*R" + id + "*", currentY + 50, Brushes.Black, StringAlignment.Center);
		    g.Dispose();
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private static void PrintSlitorFGCoreLabel(object sender, PrintPageEventArgs e)
		{
			Graphics g = e.Graphics;
			float currentY;
			string filmStructure = string.Empty;
			SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
			SqlCommand command;
			connection.Open();
			command = new SqlCommand("select ISNULL(d.[CustNameOverride],e.[CustName]),d.[ItemName],c.[ItemNo],cast(round(a.[Original Pounds]*a.[Current LF]/a.[Original LF],2) as dec(9,2)),c.[UPCCode],c.[CustPO],a.[Width],a.[Create Date],case when coalesce(c.[RepeatComexi],c.[RepeatOthers],0)!=0 then cast(floor(a.[Current LF]*12/isnull(c.[RepeatComexi],c.[RepeatOthers])) as int) end,cast(round(a.[Current LF],0) as int),isnull(b.[Input Roll ID 1],0),cast(isnull(b.[Set No],0) as int),cast(isnull(b.[Lane],0) as int),f.Number,ISNULL(c.[UnwindSlit],'?'), c.[CustID], cast(round(a.[Current LF] * .3048, 0) as int),[dbo].[Get Non-Woven Vendor Roll Numbers](a.[Roll ID]) from [Roll Table] a left join ([Production Roll Table] b inner join ([Production Master Table] g inner join [Operator Table] f on g.[Operator ID]=f.[Operator ID]) on b.[Production ID] = g.[Production ID]) on a.[Roll ID]=b.[Roll ID], [JobJackets].[dbo].[tblJobTicket] c inner join [JobJackets].[dbo].[tblItem] d on c.[ItemNo]=d.[ItemNo] inner join [JobJackets].[dbo].[tblCustomer] e on c.[CustID]=e.[CustID] where a.[Roll ID]=" + id + " and c.[JobJacketNo]='" + jobNumber + "'", connection);
			SqlDataReader reader = command.ExecuteReader();
			reader.Read();
			// I HATE DOING THIS, BUT HARDCODE FOR TRINIDAD BENHAM TO PRINT STRUCTURES ON CORE LABELS
			if (reader[0].ToString() == "TRINIDAD BENHAM")
			{
				SqlConnection connection2 = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
				command = new SqlCommand("EXECUTE [Get Abbreviated Film Structure] " + jobNumber.Substring(0, jobNumber.Length - 2), connection2);
				connection2.Open();
				filmStructure = (string)command.ExecuteScalar();
				connection2.Close();
			}
			
			currentY = e.MarginBounds.Top + PrintMessage(g, e, "Arial", 8, "R" + id, e.MarginBounds.Top, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Free 3 of 9", 30, "*R" + id + "*", currentY + 5, Brushes.Black, StringAlignment.Center);
			if (!blindShipment)
			{
				currentY += PrintMessage(g, e, "Arial", 8, reader[0].ToString(), currentY + 5, Brushes.Black, StringAlignment.Center);
			}

			currentY += PrintMessage(g, e, "Arial", 8, reader[1].ToString(), currentY + 5, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Arial", 8, "OW Item #: " + reader[2].ToString(), currentY + 5, Brushes.Black, StringAlignment.Center);
			if (!string.IsNullOrEmpty(filmStructure))
		    {
			 	currentY += PrintMessage(g, e, "Arial", 8, "Structure: " + filmStructure, currentY + 5, Brushes.Black, StringAlignment.Center);
			}
			currentY += PrintMessage(g, e, "Arial", 8, "Net Roll Wt: " + ((decimal)reader[3]).ToString("N2"), currentY + 5, Brushes.Black, StringAlignment.Center);
			if (reader[4] != DBNull.Value)
			{
				currentY += PrintMessage(g, e, "Arial", 8, "UPC #: " + reader[4].ToString(), currentY + 5, Brushes.Black, StringAlignment.Center);
			}
			
			if (reader[5] != DBNull.Value)
			{
				currentY += PrintMessage(g, e, "Arial", 8, "Cust PO #: " + reader[5].ToString(), currentY + 5, Brushes.Black, StringAlignment.Center);
			}
			
			currentY += PrintMessage(g, e, "Arial", 8, "Order #: " + jobNumber, currentY + 5, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Arial", 8, "Width: " + ((decimal)reader[6]).ToString("N3") + "   Unwind: " + reader[14].ToString(), currentY + 5, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Arial", 8, "Date: " + ((DateTime)reader[7]).ToString("MM/dd/yyyy"), currentY + 5, Brushes.Black, StringAlignment.Center);
			if (reader[8] != DBNull.Value)
			{
                if ((int)reader[15] == 763)
                {
                    currentY += PrintMessage(g, e, "Arial", 8, "Imps/Roll: " + ((int)reader[8]).ToString("N0") + "  Feet/Roll: " + ((int)reader[9]).ToString("N0") + "  M/Roll: " + ((int)reader[16]).ToString("N0"), currentY + 5, Brushes.Black, StringAlignment.Center);
                      
                }
                else
                {
                    currentY += PrintMessage(g, e, "Arial", 8, "Imps/Roll: " + ((int)reader[8]).ToString("N0") + "        Feet/Roll: " + ((int)reader[9]).ToString("N0"), currentY + 5, Brushes.Black, StringAlignment.Center);
                }
			}
			else
			{
				currentY += PrintMessage(g, e, "Arial", 8, "Feet/Roll: " + ((int)reader[9]).ToString("N0"), currentY + 5, Brushes.Black, StringAlignment.Center);			
			}
			
			if ((int)reader[10] != 0)
			{
				currentY += PrintMessage(g, e, "Arial", 8, "Current Set: R" + ((int)reader[10]).ToString() + "-" + ((int)reader[11]).ToString() + " Lane: " + ConvertClass.GetLaneName((int)reader[12]) + " OperatorID: " + reader[13].ToString(), currentY + 5, Brushes.Black, StringAlignment.Center);	
			}

            if ((int)reader[15] == 763)
            {
                currentY += PrintMessage(g, e, "Arial", 8, "EUPE", currentY + 5, Brushes.Black, StringAlignment.Center);
            }
            // Vendor Roll ID is reader[17]
            reader.Close();
			connection.Close();
			g.Dispose();
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private static void PrintCaseLabel(object sender, PrintPageEventArgs e)
		{
			Graphics g = e.Graphics;
			float currentY;
			string filmStructure = string.Empty;
			SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
			SqlCommand command;
			connection.Open();
			command = new SqlCommand("SELECT ISNULL(d.[CustNameOverride],e.[CustName]), d.[ItemName], c.[ItemNo], cast(ROUND(a.[Original Pounds], 2) AS dec(9, 2)), c.[UPCCode], c.[CustPO], a.[Create Date], a.[Bags], ISNULL(b.[Job Case ID], 0), f.[Number] FROM [Case Table] a LEFT JOIN ([Production Case Table] b INNER JOIN ([Production Master Table] g INNER JOIN [Operator Table] f ON g.[Operator ID] = f.[Operator ID]) ON b.[Production ID] = g.[Production ID]) ON a.[Case ID] = b.[Case ID], [JobJackets].[dbo].[tblJobTicket] c INNER JOIN [JobJackets].[dbo].[tblItem] d ON c.[ItemNo] = d.[ItemNo] INNER JOIN [JobJackets].[dbo].[tblCustomer] e ON c.[CustID] = e.[CustID] WHERE a.[Case ID]=" + id + " AND c.[JobJacketNo] = '" + jobNumber + "'", connection);
			SqlDataReader reader = command.ExecuteReader();
			reader.Read();
			currentY = e.MarginBounds.Top + PrintMessage(g, e, "Arial", 8, "C" + id, e.MarginBounds.Top, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Free 3 of 9", 30, "*C" + id + "*", currentY + 5, Brushes.Black, StringAlignment.Center);
			if (!blindShipment)
			{
				currentY += PrintMessage(g, e, "Arial", 8, reader[0].ToString(), currentY + 5, Brushes.Black, StringAlignment.Center);
			}

			currentY += PrintMessage(g, e, "Arial", 8, reader[1].ToString(), currentY + 5, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Arial", 8, "OW Item #: " + reader[2].ToString(), currentY + 5, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Arial", 8, "Net Case Wt: " + ((decimal)reader[3]).ToString("N2"), currentY + 5, Brushes.Black, StringAlignment.Center);
			if (reader[4] != DBNull.Value)
			{
				currentY += PrintMessage(g, e, "Arial", 8, "UPC #: " + reader[4].ToString(), currentY + 5, Brushes.Black, StringAlignment.Center);
			}
			
			if (reader[5] != DBNull.Value)
			{
				currentY += PrintMessage(g, e, "Arial", 8, "Cust PO #: " + reader[5].ToString(), currentY + 5, Brushes.Black, StringAlignment.Center);
			}
			
			currentY += PrintMessage(g, e, "Arial", 8, "Order #: " + jobNumber, currentY + 5, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Arial", 8, "Date: " + ((DateTime)reader[6]).ToString("MM/dd/yyyy"), currentY + 5, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Arial", 8, "# Bags: " + ((int)reader[7]).ToString("N0"), currentY + 5, Brushes.Black, StringAlignment.Center);
			if ((int)reader[8] != 0)
			{
				currentY += PrintMessage(g, e, "Arial", 8, "Job Case No.: C" + ((int)reader[8]).ToString() + " OperatorID: " + reader[9].ToString(), currentY + 5, Brushes.Black, StringAlignment.Center);	
			}
			
			reader.Close();
			connection.Close();
			g.Dispose();
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private static void PrintSlitorFGRollPageLabel(object sender, PrintPageEventArgs e)
		{
			Graphics g = e.Graphics;
			SqlCommand command;
			SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
			connection.Open();
			command = new SqlCommand("select ISNULL(d.[CustNameOverride],e.[CustName]),d.[ItemName],c.[ItemNo],cast(round(a.[Original Pounds]*a.[Current LF]/a.[Original LF],2) as dec(9,2)),c.[UPCCode],c.[CustPO],a.[Width],a.[Create Date],case when coalesce(c.[RepeatComexi],c.[RepeatOthers],0)!=0 then cast(floor(a.[Current LF]*12/isnull(c.[RepeatComexi],c.[RepeatOthers])) as int) end,cast(round(a.[Current LF],0) as int),isnull(b.[Input Roll ID 1],0),cast(isnull(b.[Set No],0) as int),cast(isnull(b.[Lane],0) as int),f.Number,ISNULL(c.[UnwindSlit],'?') from [Roll Table] a left join ([Production Roll Table] b inner join ([Production Master Table] g inner join [Operator Table] f on g.[Operator ID]=f.[Operator ID]) on b.[Production ID] = g.[Production ID]) on a.[Roll ID]=b.[Roll ID], [JobJackets].[dbo].[tblJobTicket] c inner join [JobJackets].[dbo].[tblItem] d on c.[ItemNo]=d.[ItemNo] inner join [JobJackets].[dbo].[tblCustomer] e on c.[CustID]=e.[CustID] where a.[Roll ID]=" + id + " and c.[JobJacketNo]='" + jobNumber.Substring(0, jobNumber.Length - 2) + "'", connection);
			SqlDataReader reader = command.ExecuteReader();
			reader.Read();
			float currentY = e.MarginBounds.Top + PrintMessage(g, e, "Arial", 50, "Roll Label (R" + id + ")",  e.MarginBounds.Top, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Free 3 of 9", 100, "*R" + id + "*", currentY + 20, Brushes.Black, StringAlignment.Center);
			if (productType == "WIP")
			{
				 	currentY += 30 + PrintMessage(g, e, "Arial", 50, "Slit Job J" + jobNumber.Substring(0, jobNumber.Length - 2) + "-" + jobNumber.Substring(jobNumber.Length - 2, 2), currentY + 20, Brushes.Black, StringAlignment.Center);
			}
			else
			{
				currentY += 30 + PrintMessage(g, e, "Arial", 50, "Job J" + jobNumber, currentY + 20, Brushes.Black, StringAlignment.Center);
			}

			if (!blindShipment)
			{
				currentY += PrintMessage(g, e, "Arial", 20, reader[0].ToString(), currentY + 5, Brushes.Black, StringAlignment.Center);
			}

			currentY += PrintMessage(g, e, "Arial", 20, reader[1].ToString(), currentY + 5, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Arial", 20, "OW Item #: " + reader[2].ToString(), currentY + 5, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Arial", 20, "Net Roll Wt: " + ((decimal)reader[3]).ToString("N2"), currentY + 5, Brushes.Black, StringAlignment.Center);
			if (reader[4] != DBNull.Value)
			{
				currentY += PrintMessage(g, e, "Arial", 20, "UPC #: " + reader[4].ToString(), currentY + 5, Brushes.Black, StringAlignment.Center);
			}
			
			if (reader[5] != DBNull.Value)
			{
				currentY += PrintMessage(g, e, "Arial", 20, "Cust PO #: " + reader[5].ToString(), currentY + 5, Brushes.Black, StringAlignment.Center);
			}
			
			currentY += PrintMessage(g, e, "Arial", 20, "Width: " + ((decimal)reader[6]).ToString("N3") + "   Unwind: " + reader[14].ToString(), currentY + 5, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Arial", 20, "Date: " + ((DateTime)reader[7]).ToString("MM/dd/yyyy"), currentY + 5, Brushes.Black, StringAlignment.Center);
            if (reader[8] != DBNull.Value)
			{
				currentY += PrintMessage(g, e, "Arial", 20, "Imps/Roll: " + ((int)reader[8]).ToString("N0") + "        Feet/Roll: " + ((int)reader[9]).ToString("N0"), currentY + 5, Brushes.Black, StringAlignment.Center);
			}
			else
			{
				currentY += PrintMessage(g, e, "Arial", 20, "Feet/Roll: " + ((int)reader[9]).ToString("N0"), currentY + 5, Brushes.Black, StringAlignment.Center);			
			}
			
			if ((int)reader[10] != 0)
			{
				currentY += PrintMessage(g, e, "Arial", 20, "Current Set: R" + ((int)reader[10]).ToString() + "-" + ((int)reader[11]).ToString() + " Lane: " + ConvertClass.GetLaneName((int)reader[12]) + " OperatorID: " + reader[13].ToString(), currentY + 5, Brushes.Black, StringAlignment.Center);	
			}
			
			reader.Close();
			connection.Close();
			g.Dispose();
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private static void PrintCasePageLabel(object sender, PrintPageEventArgs e)
		{
			Graphics g = e.Graphics;
			SqlCommand command;
			SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
			connection.Open();
			command = new SqlCommand("SELECT ISNULL(d.[CustNameOverride],e.[CustName]), d.[ItemName], c.[ItemNo], CAST(ROUND(a.[Original Pounds], 2) AS dec(9, 2)), c.[UPCCode], c.[CustPO], a.[Create Date], a.[Bags], ISNULL(b.[Job Case ID], 0), f.[Number] FROM [Case Table] a LEFT JOIN ([Production Case Table] b INNER JOIN ([Production Master Table] g INNER JOIN [Operator Table] f ON g.[Operator ID] = f.[Operator ID]) ON b.[Production ID] = g.[Production ID]) ON a.[Case ID] = b.[Case ID], [JobJackets].[dbo].[tblJobTicket] c INNER JOIN [JobJackets].[dbo].[tblItem] d ON c.[ItemNo] = d.[ItemNo] INNER JOIN [JobJackets].[dbo].[tblCustomer] e ON c.[CustID] = e.[CustID] WHERE a.[Case ID] = " + id + " AND c.[JobJacketNo] = '" + jobNumber.Substring(0, jobNumber.Length - 2) + "'", connection);
			SqlDataReader reader = command.ExecuteReader();
			reader.Read();
			float currentY = e.MarginBounds.Top + PrintMessage(g, e, "Arial", 50, "Case Label (C" + id + ")",  e.MarginBounds.Top, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Free 3 of 9", 100, "*C" + id + "*", currentY + 20, Brushes.Black, StringAlignment.Center);
			currentY += 30 + PrintMessage(g, e, "Arial", 50, "Job J" + jobNumber, currentY + 20, Brushes.Black, StringAlignment.Center);
			if (!blindShipment)
			{
				currentY += PrintMessage(g, e, "Arial", 20, reader[0].ToString(), currentY + 5, Brushes.Black, StringAlignment.Center);
			}

			currentY += PrintMessage(g, e, "Arial", 20, reader[1].ToString(), currentY + 5, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Arial", 20, "OW Item #: " + reader[2].ToString(), currentY + 5, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Arial", 20, "Net Case Wt: " + ((decimal)reader[3]).ToString("N2"), currentY + 5, Brushes.Black, StringAlignment.Center);
			if (reader[4] != DBNull.Value)
			{
				currentY += PrintMessage(g, e, "Arial", 20, "UPC #: " + reader[4].ToString(), currentY + 5, Brushes.Black, StringAlignment.Center);
			}
			
			if (reader[5] != DBNull.Value)
			{
				currentY += PrintMessage(g, e, "Arial", 20, "Cust PO #: " + reader[5].ToString(), currentY + 5, Brushes.Black, StringAlignment.Center);
			}
			
			currentY += PrintMessage(g, e, "Arial", 20, "Date: " + ((DateTime)reader[6]).ToString("MM/dd/yyyy"), currentY + 5, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Arial", 20, "# Bags: " + ((int)reader[7]).ToString("N0"), currentY + 5, Brushes.Black, StringAlignment.Center);
			if ((int)reader[8] != 0)
			{
				currentY += PrintMessage(g, e, "Arial", 20, "Job Case No.: C" + reader[8].ToString() + "     Operator: " + reader[9].ToString(), currentY + 5, Brushes.Black, StringAlignment.Center);
			}
			
			reader.Close();
			connection.Close();
			g.Dispose();
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private static void PrintFilmPalletLabel(object sender, PrintPageEventArgs e)
		{
			SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
			SqlCommand command = new SqlCommand("SELECT CASE WHEN d.[Film Type ID] < 10 THEN '0' + CAST(d.[Film Type ID] AS char(1)) ELSE CAST(d.[Film Type ID] AS char(2)) END + '-' + REPLICATE('0', 5 - LEN(d.[Part No])) + CAST(d.[Part No] AS varchar(5)) + '-' + CAST(floor(c.[width]) AS char(2)) + CASE WHEN FLOOR((c.[width] - FLOOR(c.[width])) * 16) < 10 THEN '0' ELSE '' END + CAST(FLOOR((c.[width] - FLOOR(c.[width])) * 16) AS varchar(2)), b.[width], d.[Description], COUNT(b.[Roll ID]), 'LF' , SUM(CAST(ROUND(b.[Original LF], 0) AS int)), CAST(ROUND(SUM(b.[Original Pounds]), 0) AS int), a.[PO Number], b.[Create Date], CAST(c.[Sample] AS int), CAST(c.[Cust Supplied] AS int), CAST(CASE WHEN d.[Film Type] = 'NON-WOVEN' THEN 1 ELSE 0 END AS int), e.[Vendor Pallet ID] FROM [Roll PO Table] a INNER JOIN [Roll Table] b ON a.[Roll ID] = b.[Roll ID] INNER JOIN ([Film Purchase Detail Table] c INNER JOIN [Film View] d ON c.[Master Item No] = d.[Master Item No]) ON a.[PO Number] = c.[PO Number] AND a.[PO Line Item No] = c.[PO Line Item No] INNER JOIN [Pallet Table] e ON a.[Pallet ID] = e.[Pallet ID] WHERE a.[Pallet ID] = " + id + " GROUP BY CASE WHEN d.[Film Type ID] < 10 THEN '0' + CAST(d.[Film Type ID] AS char(1)) ELSE CAST(d.[Film Type ID] AS char(2)) END + '-' + REPLICATE('0', 5 - LEN(d.[Part No])) + CAST(d.[Part No] AS varchar(5)) + '-' + CAST(floor(c.[width]) AS char(2)) + CASE WHEN FLOOR((c.[width] - FLOOR(c.[width])) * 16) < 10 THEN '0' ELSE '' END + CAST(FLOOR((c.[width] - FLOOR(c.[width])) * 16) AS varchar(2)), b.[width], d.[Description], a.[PO Number], b.[Create Date], CAST(c.[Sample] AS int), CAST(c.[Cust Supplied] AS int), CAST(CASE WHEN d.[Film Type] = 'NON-WOVEN' THEN 1 ELSE 0 END AS int), e.[Vendor Pallet ID]", connection);
			string notesToPrint = string.Empty;
			connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			reader.Read();
			Brush brushColor = GetBrushColor((int)reader[9], (int)reader[10], (int)reader[11]);
			if (reader[12] != DBNull.Value)
			{
				notesToPrint = "Vendor Pallet ID: " + reader[12].ToString();
			}
			
			PrintPageLabel(e, "P" + id, null, ((decimal)reader[1]).ToString("N4") + "\" " + reader[2].ToString(), reader[0].ToString(), notesToPrint, (int)reader[3], reader[4].ToString(), (int)reader[5], (int)reader[6], reader[7].ToString(), (DateTime)reader[8], brushColor);
			reader.Close();
			connection.Close();
		}



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        private static void PrintFGRollPalletLabel(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            float currentY;
            SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
            SqlCommand command = new SqlCommand("SELECT c.[CustID], ISNULL(d.[CustNameOverride],e.[CustName]), c.[CustPO], c.[ItemNo], d.[ItemName], c.[JobJacketNo], c.[UPCCode], c.[UOM], a.[Width], cast(a.[Original Units] as int), cast(round(a.[Current LF], 0) as int), cast(case when a.[Original LF] != 0 then round(a.[Original Pounds] * a.[Current LF] / a.[Original LF], 2) else 0 end as dec(9, 2)), count(*), cast(round(b.[Weight], 2) as dec(9, 2)), b.[Create Date], case when c.[CustID] in (148,101,112,117) and (upper(c.[ItemStatus]) like '%NEW%' or upper(c.[ItemStatus]) like '%CHANGE%') then c.[ItemStatus] end, cast(case when f.[Job Jacket No] IS NOT NULL OR isnull(e.[BlindShipment], 0) = 1 or isnull(c.[JobBlindShipment], 0) = 1 then 1 else 0 end as bit) from [Roll Table] a inner join [Pallet Table] b on a.[Pallet ID] = b.[Pallet ID], [JobJackets].[dbo].[tblJobTicket] c inner join [JobJackets].[dbo].[tblItem] d on c.[ItemNo]=d.[ItemNo] inner join [JobJackets].[dbo].[tblCustomer] e on c.[CustID]=e.[CustID] left join [Blind Shipment Job Table] f on c.[JobJacketNo] = f.[Job Jacket No] where a.[Pallet ID]=" + id + " and c.[JobJacketNo]='" + jobNumber + "' group by c.[CustID], ISNULL(d.[CustNameOverride],e.[CustName]), c.[CustPO], c.[ItemNo], d.[ItemName], c.[JobJacketNo], c.[UPCCode], c.[UOM], a.[Width], cast(a.[Original Units] as int), cast(round(a.[Current LF], 0) as int), cast(case when a.[Original LF] != 0 then round(a.[Original Pounds] * a.[Current LF] / a.[Original LF], 2) else 0 end as dec(9, 2)), cast(round(b.[Weight], 2) as dec(9, 2)), b.[Create Date], case when c.[CustID] in (148,101,112,117) and (upper(c.[ItemStatus]) like '%NEW%' or upper(c.[ItemStatus]) like '%CHANGE%') then c.[ItemStatus] end, cast(case when f.[Job Jacket No] IS NOT NULL OR isnull(e.[BlindShipment], 0) = 1 or isnull(c.[JobBlindShipment], 0) = 1 then 1 else 0 end as bit)", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
			string custID = reader[0].ToString();
            string jobJacketNumber = reader[5].ToString();
            string unitOfMeasure = reader[7].ToString();
            int totalUnits = 0;
            int totalFeet = 0;
            decimal netWeight = 0;
            int rollCount = 0;
            decimal grossWeight = (decimal)reader[13];
            DateTime palletDate = (DateTime)reader[14];
            bool blindShipment = reader.GetBoolean(16);
			currentY = e.MarginBounds.Top + PrintMessage(g, e, "Arial Rounded MT Bold", 30, "Finished Goods Pallet Label (P" + id + ")", e.MarginBounds.Top, Brushes.Black, StringAlignment.Center);
            currentY += PrintMessage(g, e, "Free 3 of 9", 80, "*P" + id + "*", currentY + 10, Brushes.Black, StringAlignment.Center);
			if (!blindShipment)
			{
				currentY += PrintMessage(g, e, "Arial", 15, reader[1].ToString(), currentY + 10, Brushes.Black, StringAlignment.Center);
			}

            currentY += PrintMessage(g, e, "Arial Rounded MT Bold", 30, reader[4].ToString(), currentY + 5, Brushes.Black, StringAlignment.Center);
            if (reader[15] != DBNull.Value)
            {
                currentY += PrintMessage(g, e, "Arial Rounded MT Bold", 15, "*** NOTE: " + reader[15].ToString() + " ***", currentY + 5, Brushes.Black, StringAlignment.Center);
            }

            if (reader[2] == DBNull.Value)
            {
                currentY += PrintMessage(g, e, "Arial", 15, "Item No: " + reader[0].ToString() + "-" + reader[3].ToString() + "     Size: " + ((decimal)reader[8]).ToString("N4") + "\"", currentY + 5, Brushes.Black, StringAlignment.Center);
            }
            else
            {
                currentY += PrintMessage(g, e, "Arial", 15, "PO #: " + reader[2].ToString() + "     Item No: " + reader[0].ToString() + "-" + reader[3].ToString() + "     Size: " + ((decimal)reader[8]).ToString("N4") + "\"", currentY + 5, Brushes.Black, StringAlignment.Center);
            }

            if (reader[6] != DBNull.Value)
            {
                currentY += PrintMessage(g, e, "Free 3 of 9", 30, "*" + reader[6].ToString() + "*", currentY + 20, Brushes.Black, StringAlignment.Center);
                currentY += PrintMessage(g, e, "Arial", 10, "UPC No.: " + reader[6].ToString(), currentY + 20, Brushes.Black, StringAlignment.Center);
            }

            //currentY += 10;
            do
            {
                if (unitOfMeasure == "ROLLS")
                {
                    if ((int)reader[12] == 1)
                    {
                        currentY += PrintMessage(g, e, "Arial", 15, "1 roll @ " + ((int)reader[10]).ToString("N0") + " FT and " + ((decimal)reader[11]).ToString("N2") + " Lbs", currentY + 15, Brushes.Black, StringAlignment.Center);
                    }
                    else
                    {
                        currentY += PrintMessage(g, e, "Arial", 15, ((int)reader[12]).ToString("N0") + " rolls @ " + ((int)reader[10]).ToString("N0") + " FT and " + ((decimal)reader[11]).ToString("N2") + " Lbs Per Roll", currentY + 15, Brushes.Black, StringAlignment.Center);
                    }
                }
                else if (unitOfMeasure != "LBS" && unitOfMeasure != "LF")
                {
                    if ((int)reader[12] == 1)
                    {
                        currentY += PrintMessage(g, e, "Arial", 15, "1 roll @ " + ((int)reader[9]).ToString("N0") + " " + unitOfMeasure + " and " + ((int)reader[10]).ToString("N0") + " FT and " + ((decimal)reader[11]).ToString("N2") + " Lbs", currentY + 15, Brushes.Black, StringAlignment.Center);
                    }
                    else
                    {
                        currentY += PrintMessage(g, e, "Arial", 15, ((int)reader[12]).ToString("N0") + " rolls @ " + ((int)reader[9]).ToString("N0") + " " + unitOfMeasure + " and " + ((int)reader[10]).ToString("N0") + " FT and " + ((decimal)reader[11]).ToString("N2") + " Lbs Per Roll", currentY + 15, Brushes.Black, StringAlignment.Center);
                    }
                }
                else
                {
                    if ((int)reader[12] == 1)
                    {
                        currentY += PrintMessage(g, e, "Arial", 15, "1 roll @ " + ((int)reader[10]).ToString("N0") + " FT and " + ((decimal)reader[11]).ToString("N2") + " Lbs", currentY + 15, Brushes.Black, StringAlignment.Center);
                    }
                    else
                    {
                        currentY += PrintMessage(g, e, "Arial", 15, ((int)reader[12]).ToString("N0") + " rolls @ " + ((int)reader[10]).ToString("N0") + " FT and " + ((decimal)reader[11]).ToString("N2") + " Lbs Per Roll", currentY + 15, Brushes.Black, StringAlignment.Center);
                    }
                }

                totalUnits += (int)reader[9] * (int)reader[12];
                totalFeet += (int)reader[10] * (int)reader[12];
                netWeight += (decimal)reader[11] * (int)reader[12];
                rollCount += (int)reader[12];
            }
            while (reader.Read());
            reader.Close();
            connection.Close();
            if (unitOfMeasure == "ROLLS")
            {
                if (rollCount == 1)
                {
                    currentY += PrintMessage(g, e, "Arial", 25, "Pallet Totals: 1 roll     " + totalFeet.ToString("N0") + " Feet", currentY + 25, Brushes.Black, StringAlignment.Center);
                }
                else
                {
                    currentY += PrintMessage(g, e, "Arial", 25, "Pallet Totals: " + rollCount.ToString("N0") + " rolls     " + totalFeet.ToString("N0") + " Feet", currentY + 30, Brushes.Black, StringAlignment.Center);
                }
            }
            else if (unitOfMeasure != "LBS" && unitOfMeasure != "LF")
            {
                if (rollCount == 1)
                {
                    currentY += PrintMessage(g, e, "Arial", 25, "Pallet Totals: 1 roll     " + totalUnits.ToString("N0") + " " + unitOfMeasure + "     " + totalFeet.ToString("N0") + " Feet", currentY + 25, Brushes.Black, StringAlignment.Center);
                }
                else
                {
                    currentY += PrintMessage(g, e, "Arial", 25, "Pallet Totals: " + rollCount.ToString("N0") + " rolls     " + totalUnits.ToString("N0") + " " + unitOfMeasure + "     " + totalFeet.ToString("N0") + " Feet", currentY + 30, Brushes.Black, StringAlignment.Center);
                }
            }
            else
            {
                if (rollCount == 1)
                {
                    currentY += PrintMessage(g, e, "Arial", 25, "Pallet Totals: 1 roll     " + totalFeet.ToString("N0") + " Feet", currentY + 30, Brushes.Black, StringAlignment.Center);
                }
                else
                {
                    currentY += PrintMessage(g, e, "Arial", 25, "Pallet Totals: " + rollCount.ToString("N0") + " rolls     " + totalFeet.ToString("N0") + " Feet", currentY + 30, Brushes.Black, StringAlignment.Center);
                }
            }

            currentY += PrintMessage(g, e, "Arial", 25, "Pound Totals:  Gross: " + grossWeight.ToString("N2") + "  Tare: " + (grossWeight - netWeight).ToString("N2") + "  Net: " + netWeight.ToString("N2"), currentY + 25, Brushes.Black, StringAlignment.Center);
            currentY += PrintMessage(g, e, "Arial Rounded MT Bold", 30, "OW Order No: " + jobJacketNumber + "  # Rolls: " + rollCount.ToString("N0"), currentY + 35, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Arial", 10, "STORAGE INSTRUCTIONS", currentY + 40, Brushes.Black, StringAlignment.Center);
			if (custID != "845")
			{
				currentY += PrintMessage(g, e, "Arial", 8, "1. Storage at temperature of 65F to 85F and humidity of 40% to 60% is required.\r\n2. Rotate stock to use oldest material first.\r\n3. Store in a clean, dry place away from direct heat and sunlight.\r\n4. Re-wrap unused material for storage.\r\n5. HANDLE WITH CARE - DO NOT DROP.\r\n6. Return pallet label, core label and sample with any questions or inquiries regarding this material.", currentY + 40, Brushes.Black, StringAlignment.Center);
			}
			else //Donahue Corry Nordic
			{
				currentY += PrintMessage(g, e, "Arial", 8, "1. Rotate stock to use oldest material first.\r\n2. Re-wrap unused material for storage.\r\n3. HANDLE WITH CARE - DO NOT DROP.\r\n4. Return pallet label, core label and sample with any questions or inquiries regarding this material.", currentY + 40, Brushes.Black, StringAlignment.Center);
			}

            if (!blindShipment)
            {
                currentY += PrintMessage(g, e, "Arial Rounded MT Bold", 10, "OVERWRAPS FLEXIBLES ~ 3950 La Reunion Pkwy ~ Dallas ~ TX 75212 ~ (214) 634-0427     Date:  " + palletDate.ToString("d") + "\f", currentY + 50, Brushes.Black, StringAlignment.Center);
            }
            else
            {
                currentY += PrintMessage(g, e, "Arial Rounded MT Bold", 10, "Date:  " + palletDate.ToString("d") + "\f", currentY + 50, Brushes.Black, StringAlignment.Center);
            }

			g.Dispose();
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private static void PrintFGCasePalletLabel(object sender, PrintPageEventArgs e)
		{
			Graphics g = e.Graphics;
			float currentY;
			SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
			SqlCommand command = new SqlCommand("SELECT c.[CustID], ISNULL(d.[CustNameOverride],e.[CustName]), c.[CustPO], c.[ItemNo], d.[ItemName], c.[JobJacketNo], c.[UPCCode], c.[UOM], a.[Bags], CAST(ROUND(CASE WHEN a.[Original Bags] > 0 THEN a.[Original Pounds] * a.[Bags] / a.[Original Bags] ELSE 0 END, 2) AS dec(9,2)), COUNT(*), CAST(ROUND(ISNULL(b.[Weight], 0), 2) AS dec(9,2)), b.[Create Date], CASE WHEN c.[CustID] IN (148,101,112,117) AND (UPPER(c.[ItemStatus]) LIKE '%NEW%' OR UPPER(c.[ItemStatus]) LIKE '%CHANGE%') THEN c.[ItemStatus] END, CAST(CASE WHEN f.[Job Jacket No] IS NOT NULL OR ISNULL(e.[BlindShipment], 0) = 1 OR ISNULL(c.[JobBlindShipment], 0) = 1 THEN 1 ELSE 0 END AS bit) FROM	[Case Table] a INNER JOIN [Pallet Table] b ON a.[Pallet ID] = b.[Pallet ID], [JobJackets].[dbo].[tblJobTicket] c INNER JOIN [JobJackets].[dbo].[tblItem] d ON c.[ItemNo] = d.[ItemNo] INNER JOIN [JobJackets].[dbo].[tblCustomer] e ON c.[CustID] = e.[CustID]  LEFT JOIN [Blind Shipment Job Table] f ON c.[JobJacketNo] = f.[Job Jacket No] WHERE a.[Pallet ID] = " + id + " AND c.[JobJacketNo] = '" + jobNumber + "' GROUP BY c.[CustID], ISNULL(d.[CustNameOverride],e.[CustName]), c.[CustPO], c.[ItemNo], d.[ItemName], c.[JobJacketNo], c.[UPCCode], c.[UOM], a.[Bags], CAST(ROUND(CASE WHEN a.[Original Bags] > 0 THEN a.[Original Pounds] * a.[Bags] / a.[Original Bags] ELSE 0 END, 2) AS dec(9,2)), CAST(ROUND(ISNULL(b.[Weight], 0), 2) AS dec(9,2)), b.[Create Date], CASE WHEN c.[CustID] IN (148,101,112,117) AND (UPPER(c.[ItemStatus]) LIKE '%NEW%' OR UPPER(c.[ItemStatus]) LIKE '%CHANGE%') THEN c.[ItemStatus] END, CAST(CASE WHEN f.[Job Jacket No] IS NOT NULL OR ISNULL(e.[BlindShipment], 0) = 1 OR ISNULL(c.[JobBlindShipment], 0) = 1 THEN 1 ELSE 0 END AS bit)", connection);
			connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			reader.Read();
			string jobJacketNumber = reader[5].ToString();
			string unitOfMeasure = reader[7].ToString();
			int totalBags = 0;
			decimal netWeight = 0;
			int caseCount = 0;
			decimal grossWeight = (decimal)reader[11];
			DateTime palletDate = (DateTime)reader[12];
            bool blindShipment = reader.GetBoolean(14);
			currentY = e.MarginBounds.Top + PrintMessage(g, e, "Arial Rounded MT Bold", 30, "Finished Goods Pallet Label (P" + id + ")",  e.MarginBounds.Top, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Free 3 of 9", 80, "*P" + id + "*", currentY + 10, Brushes.Black, StringAlignment.Center);
			if (!blindShipment)
			{
				currentY += PrintMessage(g, e, "Arial", 15, reader[1].ToString(), currentY + 10, Brushes.Black, StringAlignment.Center);
			}

			currentY += PrintMessage(g, e, "Arial Rounded MT Bold", 30, reader[4].ToString(), currentY + 5, Brushes.Black, StringAlignment.Center);
			if (reader[13] != DBNull.Value)
			{
				currentY += PrintMessage(g, e, "Arial Rounded MT Bold", 15, "*** NOTE: " + reader[13].ToString() + " ***", currentY + 5, Brushes.Black, StringAlignment.Center);
			}
			
			if (reader[2] == DBNull.Value)
			{
				currentY += PrintMessage(g, e, "Arial", 15, "Item No: " + reader[0].ToString() + "-" + reader[3].ToString(), currentY + 5, Brushes.Black, StringAlignment.Center);
			}
			else
			{
				currentY += PrintMessage(g, e, "Arial", 15, "PO #: " + reader[2].ToString() + "     Item No: " + reader[0].ToString() + "-" + reader[3].ToString(), currentY + 5, Brushes.Black, StringAlignment.Center);
			}
		
			if (reader[6] != DBNull.Value)
			{
				currentY += PrintMessage(g, e, "Free 3 of 9", 30, "*" + reader[6].ToString() + "*", currentY + 20, Brushes.Black, StringAlignment.Center);
				currentY += PrintMessage(g, e, "Arial", 10, "UPC No.: " + reader[6].ToString(), currentY + 20, Brushes.Black, StringAlignment.Center);
			}
			
			do
			{
				if ((int)reader[10] == 1)
				{
					currentY += PrintMessage(g, e, "Arial", 15, "1 Case @ " + ((int)reader[8]).ToString("N0") + " Bags and " + ((decimal)reader[9]).ToString("N2") + " Lbs", currentY + 15, Brushes.Black, StringAlignment.Center);
				}
				else
				{
					currentY += PrintMessage(g, e, "Arial", 15, ((int)reader[10]).ToString("N0") + " Cases @ " + ((int)reader[8]).ToString("N0") + " Bags and " + ((decimal)reader[9]).ToString("N2") + " Lbs per Case", currentY + 15, Brushes.Black, StringAlignment.Center);
				}
				
				totalBags += (int)reader[10] * (int)reader[8];
				netWeight += (decimal)(int)reader[10] * (decimal)reader[9];
				caseCount += (int)reader[10];
			}
			while (reader.Read());
			
			reader.Close();
			connection.Close();
			if (caseCount == 1)
			{
				currentY += PrintMessage(g, e, "Arial", 25, "Pallet Totals: 1 Case     " + totalBags.ToString("N0") + " Bags", currentY + 25, Brushes.Black, StringAlignment.Center);
			}
			else
			{
				currentY += PrintMessage(g, e, "Arial", 25, "Pallet Totals: " + caseCount.ToString("N0") + " Cases     " +  totalBags.ToString("N0") + " Bags", currentY + 30, Brushes.Black, StringAlignment.Center);
			}
			
			currentY += PrintMessage(g, e, "Arial", 25, "Pound Totals:  Gross: " + grossWeight.ToString("N2") + "  Tare: " + (grossWeight - netWeight).ToString("N2") + "  Net: " + netWeight.ToString("N2"), currentY + 25, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Arial Rounded MT Bold", 30, "OW Order No: " + jobJacketNumber + "  # Cases: " + caseCount.ToString("N0"), currentY + 35, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Arial", 10, "STORAGE INSTRUCTIONS", currentY + 40, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Arial", 8, "1. Storage at temperature of 65F to 85F and humidity of 40% to 60% is required.\r\n2. Rotate stock to use oldest material first.\r\n3. Store in a clean, dry place away from direct heat and sunlight.\r\n4. Re-wrap unused material for storage.\r\n5. HANDLE WITH CARE - DO NOT DROP.\r\n6. Return pallet label, core label and sample with any questions or inquiries regarding this material.", currentY + 40, Brushes.Black, StringAlignment.Center);
            if (!blindShipment)
            {
                currentY += PrintMessage(g, e, "Arial Rounded MT Bold", 10, "OVERWRAPS FLEXIBLES ~ 3950 La Reunion Pkwy ~ Dallas ~ TX 75212 ~ (214) 634-0427     Date:  " + palletDate.ToString("d") + "\f", currentY + 50, Brushes.Black, StringAlignment.Center);
            }
            else
            {
                currentY += PrintMessage(g, e, "Arial Rounded MT Bold", 10, "Date:  " + palletDate.ToString("d") + "\f", currentY + 50, Brushes.Black, StringAlignment.Center);
            }

			g.Dispose();
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private static void PrintSlitWIPPalletDescriptionLabel(object sender, PrintPageEventArgs e)
		{
			Graphics g = e.Graphics;
			float currentY;
			SqlCommand command;
			SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
			
			command = new SqlCommand("SELECT TOP 1 c.[Reference Item No], c.[Description], b.[Create Date] FROM [Roll Table] a INNER JOIN [Pallet Table] b ON a.[Pallet ID] = b.[Pallet ID] INNER JOIN [Inventory Master Table] c ON a.[Master Item No] = c.[Master Item No] WHERE a.[Pallet ID] = " + id, connection);
			connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			if (reader.Read())
			{
				currentY = e.MarginBounds.Top + PrintMessage(g, e, "Arial Rounded MT Bold", 30, "Slit WIP Pallet Label (P" + id + ")",  e.MarginBounds.Top, Brushes.Black, StringAlignment.Center);
				currentY += PrintMessage(g, e, "Free 3 of 9", 80, "*P" + id + "*", currentY + 10, Brushes.Black, StringAlignment.Center);
			
				if (reader[0].ToString().Length == 7)
				{
					currentY += PrintMessage(g, e, "Arial", 50, "J0" + reader[0].ToString().Substring(0, reader[0].ToString().Length - 2) + "-" + reader[0].ToString().Substring(reader[0].ToString().Length - 2, 2), currentY + 20, Brushes.Black, StringAlignment.Center);
				}
				else
				{
					currentY += PrintMessage(g, e, "Arial", 50, "J" + reader[0].ToString().Substring(0, reader[0].ToString().Length - 2) + "-" + reader[0].ToString().Substring(reader[0].ToString().Length - 2, 2), currentY + 20, Brushes.Black, StringAlignment.Center);
				}
			
				currentY += PrintMessage(g, e, "Arial", 30, reader[1].ToString(), currentY + 30, Brushes.Black, StringAlignment.Center);
				currentY += PrintMessage(g, e, "Arial", 30, "Created on " +((DateTime)reader[2]).ToString("MM/dd/yyyy"), currentY + 40, Brushes.Black, StringAlignment.Center);
			}
			else
			{
				MessageBox.Show("Error - This is not a label printer and there are no rolls on this pallet", "Error");
			}
			
			reader.Close();
			connection.Close();
			g.Dispose();
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private static void PrintCasePalletDescriptionLabel(object sender, PrintPageEventArgs e)
		{
			Graphics g = e.Graphics;
			float currentY;
			SqlCommand command;
			SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
			
			command = new SqlCommand("SELECT TOP 1 c.[Reference Item No], c.[Description], b.[Create Date] FROM [Case Table] a INNER JOIN [Pallet Table] b ON a.[Pallet ID] = b.[Pallet ID] INNER JOIN [Inventory Master Table] c ON a.[Master Item No] = c.[Master Item No] WHERE a.[Pallet ID] = " + id, connection);
			connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			if (reader.Read())
			{
				currentY = e.MarginBounds.Top + PrintMessage(g, e, "Arial Rounded MT Bold", 30, "Bag WIP Pallet Label (P" + id + ")",  e.MarginBounds.Top, Brushes.Black, StringAlignment.Center);
				currentY += PrintMessage(g, e, "Free 3 of 9", 80, "*P" + id + "*", currentY + 10, Brushes.Black, StringAlignment.Center);
			
				if (reader[0].ToString().Length == 7)
				{
					currentY += PrintMessage(g, e, "Arial", 50, "J0" + reader[0].ToString().Substring(0, reader[0].ToString().Length - 2) + "-" + reader[0].ToString().Substring(reader[0].ToString().Length - 2, 2), currentY + 20, Brushes.Black, StringAlignment.Center);
				}
				else
				{
					currentY += PrintMessage(g, e, "Arial", 50, "J" + reader[0].ToString().Substring(0, reader[0].ToString().Length - 2) + "-" + reader[0].ToString().Substring(reader[0].ToString().Length - 2, 2), currentY + 20, Brushes.Black, StringAlignment.Center);
				}
			
				currentY += PrintMessage(g, e, "Arial", 30, reader[1].ToString(), currentY + 30, Brushes.Black, StringAlignment.Center);
				currentY += PrintMessage(g, e, "Arial", 30, "Created on " +((DateTime)reader[2]).ToString("MM/dd/yyyy"), currentY + 40, Brushes.Black, StringAlignment.Center);
			}
			else
			{
				MessageBox.Show("Error - This is not a label printer and there are no rolls on this pallet", "Error");
			}
			
			reader.Close();
			connection.Close();
			g.Dispose();
		}
		
		private static void PrintSlitRollPalletLabel(object sender, PrintPageEventArgs e)
		{
			Graphics g = e.Graphics;
			float currentY;
			currentY = e.MarginBounds.Top + PrintMessage(g, e, "Arial", 8, "Slit Roll Pallet Label", e.MarginBounds.Top, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Free 3 of 9", 30, "*P" + id + "*", currentY + 10, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Arial", 8, "P" + id, currentY + 10, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Arial", 8, "Job " + jobNumber.Substring(0, jobNumber.Length - 2), currentY + 10, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Arial", 8, description, currentY + 10, Brushes.Black, StringAlignment.Center);
			g.Dispose();
		}
		
		private static void PrintCasePalletLabel(object sender, PrintPageEventArgs e)
		{
			Graphics g = e.Graphics;
			float currentY;
			currentY = e.MarginBounds.Top + PrintMessage(g, e, "Arial", 8, "Bag Pallet Label", e.MarginBounds.Top, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Free 3 of 9", 30, "*P" + id + "*", currentY + 10, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Arial", 8, "P" + id, currentY + 10, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Arial", 8, "Job " + jobNumber.Substring(0, jobNumber.Length - 2), currentY + 10, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Arial", 8, description, currentY + 10, Brushes.Black, StringAlignment.Center);
			g.Dispose();
		}
		
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private static void PrintAdhesiveToteLabel(object sender, PrintPageEventArgs e)
		{
			Graphics g = e.Graphics;
			float currentY;
			SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
			SqlCommand command = new SqlCommand("select b.[Vendor],b.[Part No],a.[PO Number],a.[Batch ID],a.[Create Date],a.[Pounds] from [Tote Table] a inner join [Adhesive Table] b on a.[Master Item No]=b.[Master Item No] where a.[Tote ID]=" + id, connection);
			connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			reader.Read();
			currentY = e.MarginBounds.Top + PrintMessage(g, e, "Arial", 45, "Adhesive Tote Label (T" + id + ")", e.MarginBounds.Top, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Free 3 of 9", 100, "*T" + id + "*", currentY + 10, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Arial", 40, "Vendor: " + reader[0].ToString(), currentY + 10, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Arial", 40, "Part No.: " + reader[1].ToString(), currentY + 10, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Arial", 40, "PO No.: " + ((int)reader[2]).ToString(), currentY + 10, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Arial", 40, "Batch ID: " + reader[3].ToString(), currentY + 10, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Arial", 30, "Create Date: " + ((DateTime)reader[4]).ToString("MM/dd/yyyy"), currentY + 20, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Arial", 30, ((decimal)reader[5]).ToString("N2") + " Pounds ", currentY + 10, Brushes.Black, StringAlignment.Center);
			currentY += PrintMessage(g, e, "Free 3 of 9", 30, "*T" + id + "*", currentY + 20, Brushes.Black, StringAlignment.Center);
			reader.Close();
			connection.Close();
		}
		
		private static void PrintFilmPickList(object sender, PrintPageEventArgs e)
		{
			Graphics g = e.Graphics;
			float currentY;
			SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
			SqlCommand command = new SqlCommand("SELECT	CAST(ISNULL(b.[Lamination Film ID], 0) AS int),	a.[Roll ID], d.[Width], e.[Description], d.[Pallet ID], i.[Description], CAST(ROUND(a.[Allocated LF], 0) AS int), CAST(ROUND(d.[Current LF], 0) AS int), CAST(ROUND(d.[Original Pounds] * d.[Current LF] / d.[Original LF], 0) AS int), CASE WHEN i.[Inventory Available] = 0 THEN 'ROLL IS NOT AVAILABLE PER LOCATION CODE' WHEN d.[Current LF] = 0 AND g.[Roll ID] IS NOT NULL THEN 'ROLL NOT IN INVENTORY BUT ROLL WAS PUT IN PRODUCTION ON LINE ' + CAST(h.[Machine No] AS nvarchar(10)) + ' AT ' + CAST(g.[Start Usage Date] AS nvarchar(20)) WHEN d.[Current LF] = 0 THEN 'ROLL NOT IN INVENTORY' WHEN ROUND(a.[Allocated LF], 0) > ROUND(d.[Current LF], 0) THEN 'ROLL IS SMALLER THAN ALLOCATED AMOUNT' WHEN ROUND(a.[Allocated LF], 0) < ROUND(d.[Current LF], 0) THEN 'PARTIAL ALLOCATION' END FROM [Allocation Pick Table] a INNER JOIN ([Allocation Master Table] b INNER JOIN [Inventory Master Table] c ON b.[Master Item No] = c.[Master Item No]) ON a.[Allocation ID] = b.[Allocation ID] INNER JOIN ([Roll Table] d INNER JOIN [Inventory Master Table] e ON d.[Master Item No] = e.[Master Item No] LEFT JOIN [Pallet Table] f ON d.[Pallet ID] = f.[Pallet ID]) ON a.[Roll ID] = d.[Roll ID] LEFT JOIN ([Production Consumed Roll Table] g INNER JOIN [Production Master Table] h ON g.[Start Production ID] = h.[Production ID]) ON a.[Roll ID] = g.[Roll ID] AND g.[End Usage Date] IS NULL, [Location Table] i WHERE	b.[Pick Date] IS NOT NULL AND b.[Release Date] IS NULL AND b.[Void Date] IS NULL AND ISNULL(f.[Location ID], d.[Location ID]) = i.[Location ID] AND c.[Reference Item No] = " + id + " ORDER BY b.[Lamination Film ID], a.[Priority], a.[Roll ID]", connection);
			currentY = e.MarginBounds.Top + PrintMessage(g, e, "Arial", 12, "PICK LIST FOR JOB " + jobNumber + " - " + description + " AS OF " + DateTime.Now.ToString(), e.MarginBounds.Top, Brushes.Black, StringAlignment.Center) + 20;
 		    connection.Open();
	        SqlDataReader reader = command.ExecuteReader();
            reader.Read();
 		    int currentFilm = (int)reader[0] + 1;
	        if (jobNumber.Substring(jobNumber.Length - 2, 1) == "2")
 		    {
	        	currentY += PrintMessage(g, e, "Arial", 12, "Lam Film " + currentFilm.ToString(), currentY + 10, Brushes.Black, StringAlignment.Near);
	        }

           	string rollInfo;
           	do
            {
           		if ((int)reader[0] + 1 != currentFilm)
           		{
           			currentFilm = (int)reader[0] + 1;
           			currentY += PrintMessage(g, e, "Arial", 12, "Lam Film " + currentFilm.ToString(), currentY + 20, Brushes.Black, StringAlignment.Near) + 10;
           		}
	                        		
        		rollInfo = "  R" + reader[1].ToString();
        		if (!string.IsNullOrEmpty(reader[4].ToString()))
        		{
        			rollInfo += " on P" + reader[4].ToString();
        		}
                    		
        		rollInfo +=  " at " + reader[5].ToString() + "  (" + ((decimal)reader[2]).ToString("N4") + "\" " + reader[3].ToString() + "):  ";
        		if ((int)reader[6] == (int) reader[7])
        		{
        			rollInfo += ((int)reader[6]).ToString("N0") + " LF & " + ((int)reader[8]).ToString("N0") + " Lbs";
        		}
        		else
        		{
        			rollInfo += "Allocated " + ((int)reader[6]).ToString("N0") + " LF of " + ((int)reader[7]).ToString("N0") + " LF & " + ((int)reader[8]).ToString("N0") + " Lbs Available";
        		}
                
				currentY += PrintMessage(g, e, "Arial", 10, rollInfo, currentY + 10, Brushes.Black, StringAlignment.Near);        		
        		if (!string.IsNullOrEmpty(reader[9].ToString()))
        		{
        			currentY += PrintMessage(g, e, "Arial", 9, "        " + reader[9].ToString(), currentY + 10, Brushes.Black, StringAlignment.Near) + 10;
        		}
            }
        	while (reader.Read());
        	
        	reader.Close();
        	command = new SqlCommand("SELECT CAST(ROUND([Current Inventory LF 1], 0) AS int), CAST(ROUND([Current Inventory Pounds 1], 0) AS int), CAST(ROUND([Picked LF 1], 0) AS int), CAST(ROUND([Picked Pounds 1], 0) AS int), CAST(ROUND([Current Inventory LF 2], 0) AS int), CAST(ROUND([Current Inventory Pounds 2], 0) AS int), CAST(ROUND([Picked LF 2], 0) AS int), CAST(ROUND([Picked Pounds 2], 0) AS int) FROM [Open Allocation and Pick Totals by Job View] WHERE [Job No] = " + id, connection);
        	reader = command.ExecuteReader();
        	reader.Read();
        	if ((int)reader[2] != 0 && (int)reader[6] != 0)
        	{
        		currentY += PrintMessage(g, e, "Arial", 12, "Lam Film 1 has " + ((int)reader[0]).ToString("N0") + " LF and " + ((int)reader[1]).ToString("N0") + " pounds available of the " + ((int)reader[2]).ToString("N0") + " LF and " + ((int)reader[3]).ToString("N0") + " pounds allocated", currentY + 20, Brushes.Black, StringAlignment.Center) + 10;
                currentY += PrintMessage(g, e, "Arial", 12, "Lam Film 2 has " + ((int)reader[4]).ToString("N0") + " LF and " + ((int)reader[5]).ToString("N0") + " pounds available of the " + ((int)reader[6]).ToString("N0") + " LF and " + ((int)reader[7]).ToString("N0") + " pounds allocated", currentY + 10, Brushes.Black, StringAlignment.Center);
        	}
        	else if ((int)reader[2] != 0)
        	{
        		currentY += PrintMessage(g, e, "Arial", 12, ((int)reader[0]).ToString("N0") + " LF and " + ((int)reader[1]).ToString("N0") + " pounds of the " + ((int)reader[2]).ToString("N0") + " LF and " + ((int)reader[3]).ToString("N0") + " pounds allocated is available", currentY + 20, Brushes.Black, StringAlignment.Center);        	}
        	else
        	{
        		currentY += PrintMessage(g, e, "Arial", 12, ((int)reader[4]).ToString("N0") + " LF and " + ((int)reader[5]).ToString("N0") + " pounds of the " + ((int)reader[6]).ToString("N0") + " LF and " + ((int)reader[7]).ToString("N0") + " pounds allocated is available", currentY + 20, Brushes.Black, StringAlignment.Center);
        	}

        	reader.Close();
        	connection.Close();
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private static void PrintPageLabel(PrintPageEventArgs e, string palletBarcodeNumber, string rollBarcodeNumber, string description, string partNumber, string notes, int rollCount, string unitOfMeasure, int unitCount, int pounds, string purchcaseOrderNumber, DateTime createDate, Brush brushColor)
		{
			// Print a pallet or roll barcode label
			Graphics g = e.Graphics;
			float currentY;
			if (palletBarcodeNumber != null && rollBarcodeNumber != null)
			{
				currentY = e.MarginBounds.Top + PrintMessage(g, e, "Arial", 45, "Roll Label (" + rollBarcodeNumber + " from " + palletBarcodeNumber + ")",  e.MarginBounds.Top, brushColor, StringAlignment.Center);
				currentY += PrintMessage(g, e, "Free 3 of 9", 100, "*" + rollBarcodeNumber + "*", currentY + 10, Brushes.Black, StringAlignment.Center);
			}
			else if (rollBarcodeNumber != null)
			{
				currentY = e.MarginBounds.Top + PrintMessage(g, e, "Arial", 50, "Roll Label (" + rollBarcodeNumber + ")",  e.MarginBounds.Top, brushColor, StringAlignment.Center);
				currentY += PrintMessage(g, e, "Free 3 of 9", 100, "*" + rollBarcodeNumber + "*", currentY + 10, Brushes.Black, StringAlignment.Center);
			}
			else
			{
				currentY = e.MarginBounds.Top + PrintMessage(g, e, "Arial", 50, "Pallet Label (" + palletBarcodeNumber + ")", e.MarginBounds.Top, brushColor, StringAlignment.Center);
				currentY += PrintMessage(g, e, "Free 3 of 9", 100, "*" + palletBarcodeNumber + "*", currentY + 10, Brushes.Black, StringAlignment.Center);
			}
			
			currentY += PrintMessage(g, e, "Arial", 60, description, currentY, brushColor, StringAlignment.Center);
            
            // Added info, user name
            currentY += PrintMessage(g, e, "Arial", 60, userName, currentY, brushColor, StringAlignment.Center);
            // 

            currentY += PrintMessage(g, e, "Arial", 20, partNumber, currentY, brushColor, StringAlignment.Center);
			if (purchcaseOrderNumber.Length > 0)
			{
				currentY += PrintMessage(g, e, "Arial", 20, "PO # " + purchcaseOrderNumber, currentY, brushColor, StringAlignment.Center);
			}
			
			currentY += PrintMessage(g, e, "Arial", 20, createDate.ToString(), currentY, brushColor, StringAlignment.Center);
			if (notes.Length > 0)
			{
				currentY += PrintMessage(g, e, "Arial", 10, notes, currentY, brushColor, StringAlignment.Center);
			}
			
			if (rollCount == 0)
			{
			    currentY += PrintMessage(g, e, "Arial", 50, unitCount.ToString("N0") + " " + unitOfMeasure + "  " + pounds.ToString("N0") + " Pounds", currentY, brushColor, StringAlignment.Center);
			}
			else
			{
				currentY += PrintMessage(g, e, "Arial", 30, "No. Rolls: " + rollCount.ToString("N0") + "   Total " + unitOfMeasure + ": " + unitCount.ToString("N0"), currentY, brushColor, StringAlignment.Center);
				currentY += PrintMessage(g, e, "Arial", 50, "Total Pounds: " + pounds.ToString("N0"), currentY, brushColor, StringAlignment.Center);
			}
			
			if (palletBarcodeNumber != null && rollBarcodeNumber == null)
			{
				currentY += PrintMessage(g, e, "Free 3 of 9", 30, "*" + palletBarcodeNumber + "*", currentY, Brushes.Black, StringAlignment.Center);
			}
			else
			{
				currentY += PrintMessage(g, e, "Free 3 of 9", 30, "*" + rollBarcodeNumber + "*", currentY, Brushes.Black, StringAlignment.Center);
			}
			
			g.Dispose();
		}
		
		private static float PrintMessage(Graphics g, PrintPageEventArgs e, string fontName, float fontPoints, string printString, float y, Brush brushColor, StringAlignment sAlignment)
		{
            // Print a line centered for a pallet or roll barcode label and return the height of the printed line
       
    
			float printHeight;
			Font printFont = new Font(fontName, fontPoints);

            RectangleF r = new RectangleF(e.MarginBounds.Left, y, e.MarginBounds.Right - e.MarginBounds.Left + 1, e.PageBounds.Height - y + 1);
			SizeF layoutSize = new SizeF(e.MarginBounds.Right - e.MarginBounds.Left + 1, e.PageBounds.Height - y + 1);
			SizeF stringSize = new SizeF();
			StringFormat format = new StringFormat();
			format.Alignment = sAlignment;
			g.DrawString(printString, printFont, brushColor, r, format);
			stringSize = g.MeasureString(printString, printFont, layoutSize);
			printHeight = stringSize.Height;
			printFont.Dispose();
			return printHeight;
		}

        private static float PrintMessageWithFont(Graphics g, PrintPageEventArgs e, string fontName, float fontPoints, string printString, float y, Brush brushColor, StringAlignment sAlignment, FontStyle? fontStyle)
        {
            // Print a line centered for a pallet or roll barcode label and return the height of the printed line


            float printHeight;
            Font printFont = new Font(fontName, fontPoints);

            if (fontStyle != null)
            {
                printFont = new Font(fontName, fontPoints, fontStyle.Value);

            }
            RectangleF r = new RectangleF(e.MarginBounds.Left, y, e.MarginBounds.Right - e.MarginBounds.Left + 1, e.PageBounds.Height - y + 1);
            SizeF layoutSize = new SizeF(e.MarginBounds.Right - e.MarginBounds.Left + 1, e.PageBounds.Height - y + 1);
            SizeF stringSize = new SizeF();
            StringFormat format = new StringFormat();
            format.Alignment = sAlignment;
            g.DrawString(printString, printFont, brushColor, r, format);
            stringSize = g.MeasureString(printString, printFont, layoutSize);
            printHeight = stringSize.Height;
            printFont.Dispose();
            return printHeight;
        }

        private static Brush GetBrushColor(int sample, int customerSupplied, int nonWoven)
		{
			if (sample == 1)
			{
				return Brushes.Purple;
			}
			else if (customerSupplied == 1)
			{
				return Brushes.Red;
			}
			else if (nonWoven == 1)
			{
				return Brushes.Green;
			}
			else
			{
				return Brushes.Blue;
			}
		}

        private static void PrintLabelComment(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            float currentY;
            currentY = e.MarginBounds.Top + PrintMessage(g, e, "Arial", 8, id + " Comments", e.MarginBounds.Top, Brushes.Black, StringAlignment.Center);
            currentY += PrintMessage(g, e, "Arial", 8, description, currentY + 5, Brushes.Black, StringAlignment.Center);
            g.Dispose();
        }

        private static void PrintFullPageComment(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            float currentY;
            currentY = e.MarginBounds.Top + PrintMessage(g, e, "Arial", 20, id + " Comments", e.MarginBounds.Top, Brushes.Black, StringAlignment.Center);
            currentY += PrintMessage(g, e, "Arial", 14, description, currentY + 10, Brushes.Black, StringAlignment.Center);
            g.Dispose();
        }
    }
}
