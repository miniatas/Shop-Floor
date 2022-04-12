/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 11/30/2010
 * Time: 1:53 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace ShopFloor
{
    using ShopFloor;
    using ShopFloor.PlateForms;

    using System;
	using System.Data.SqlClient;
	using System.Drawing.Printing;
	using System.Globalization;
	using System.Runtime.InteropServices;
	using System.Windows.Forms;
	
	/// <summary>
	/// Allows access depending on login rights to the other forms in the application.
	/// </summary>
	public partial class MainForm : Form
	{
		private static string machineNumber = "0";
		private static int operatorNumber = 0;
        private static string operatorName = string.Empty;
        private static bool userCanShipReceive = false;
		private static bool userCanConsign = false;
        private static bool userCanOverride = false;
        private static bool userCanAccessAllInventory = false;
		private static bool labelPrinter = false;
        private static bool workingScanner = false;
		private static bool localProduction;
        private static bool noUPCValidationRequired = false;
		private string originalDefaultPrinter;
		private SqlConnection connection1 = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
		private SqlConnection connection2 = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
		private SqlConnection connection3 = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
		private SqlDataReader reader1;
		private SqlDataReader reader2;
		private SqlCommand command;
		private bool machineWorkstation = false;
		private DialogResult answer;

		// Use the two items below and the commented out asssignment if you need different pallet and roll printers
		// Currently they both print on plain paper so the default printer will work for both
		// public static string strPalletPrinterName;
		// public static string strRollPrinterName;
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		public MainForm()
		{
			this.InitializeComponent();
			
			lblUserInformation.Text = "Logged in as: " + StartupForm.UserName;
			originalDefaultPrinter = GetDefaultPrinter();
            command = new SqlCommand("SELECT [Job Rights], [Can Edit Hrs], [Can Consign], [Can Override], [Can Access All Inventory], [No UPC Validation Required], [Administrator], [Can Access Plate Inventory] FROM [User Rights Table] WHERE [User ID]='" + StartupForm.UserName + "'", connection1);
            connection1.Open();
            reader1 = command.ExecuteReader();
            if (reader1.Read())
            {
                productionEditStripMenuItem.Visible = reader1.GetBoolean(1);
                userCanConsign = reader1.GetBoolean(2);
                userCanOverride = reader1.GetBoolean(3);
                userCanAccessAllInventory = reader1.GetBoolean(4);
                noUPCValidationRequired = reader1.GetBoolean(5);
                userAdminToolStripMenuItem.Visible = reader1.GetBoolean(6);
                plateInventoryToolStripMenuItem.Visible = reader1.GetBoolean(7);
                switch (reader1[0].ToString())
                {
                    case "All":
                        raToolStripMenuItem.Visible = true;
                        createtoolStripMenuItem.Visible = true;
                        machineOrOperationToolStripMenuItem.Visible = true;
                        userCanShipReceive = true;
                        machineOrOperationToolStripMenuItem.DropDownItems.Add("Material Planning");
                        machineOrOperationToolStripMenuItem.DropDownItems.Add("Shipping/Receiving");
                        command = new SqlCommand("SELECT [Machine No], [Machine Name] FROM [Machine Table] WHERE [Operation ID] != 4 ORDER BY [Machine No]", connection1);
                        reader1.Close();
                        reader1 = command.ExecuteReader();
                        while (reader1.Read())
                        {
                            machineOrOperationToolStripMenuItem.DropDownItems.Add("Machine " + reader1[0].ToString() + " - " + reader1[1].ToString());
                        }

                        machineOrOperationToolStripMenuItem.DropDownItems.Add("Finishing");
                        break;
                    case "Material Planning":
                        createtoolStripMenuItem.Visible = true;
                        issuesReturnsToolStripMenuItem.Visible = true;
                        break;
                    case "Shipping/Receiving":
                        createtoolStripMenuItem.Visible = true;
                        issuesReturnsToolStripMenuItem.Visible = true;
                        userCanShipReceive = true;
                        finishingToolStripMenuItem.Visible = true;
                        adhesivesToolStripMenuItem.Visible = true;
                        adhesiveReceiveToolStripMenuItem.Visible = true;
                        adhesiveUseToolStripMenuItem.Visible = true;
                        break;
                    case "Finishing":
                        createtoolStripMenuItem.Visible = true;
                        finishingToolStripMenuItem.Visible = true;
                        break;
                }
            }

            reader1.Close();
            command = new SqlCommand("SELECT a.[Machine No], b.[Machine Name], a.[Pallet Printer Name], a.[Roll Printer Name], a.[Label Printer], a.[Working Scanner] FROM [Workstation Machine Assignment Table] a INNER JOIN [Machine Table] b ON a.[Machine No] = b.[Machine No] WHERE [Workstation Name] = '" + System.Environment.MachineName.ToString().Replace("'", "''") + "'", connection1);
			reader1 = command.ExecuteReader();
            if (reader1.Read())
            {
                // This computer is at a machine workstation
                printerToolStripMenuItem.Visible = false;
                lblMachineOrPrinterInformation.Text = "Machine " + reader1[0].ToString() + " - " + reader1[1].ToString();
                machineWorkstation = true;
                machineNumber = reader1[0].ToString();
                labelPrinter = reader1.GetBoolean(4);
                workingScanner = reader1.GetBoolean(5);
                productionToolStripMenuItem.Visible = true;
                if (machineNumber.Substring(1, 1) == "2")
                {
                    adhesivesToolStripMenuItem.Visible = true;
                }
            }
                
            reader1.Close();
            connection1.Close();
		}
		
		public static string MachineNumber
		{
			get
			{
				return machineNumber;
			}
		}
		
		public static int OperatorNumber
		{
			get
			{
				return operatorNumber;
			}
		}

        public static string OperatorName
        {
            get
            {
                return operatorName;
            }
        }

        public static bool CanShipReceive
		{
			get
			{
				return userCanShipReceive;
			}
		}

        public static bool UserCanAccessAllInventory
        {
            get
            {
                return userCanAccessAllInventory;
            }
        }

        public static bool UserCanOverride
        {
            get
            {
                return userCanOverride;
            }
        }

        public static bool LabelPrinter
		{
			get
			{
				return labelPrinter;	
			}
		}

        public static bool WorkingScanner
        {
            get
            {
                return workingScanner;
            }
        }

        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			Application.Exit();
		}
		
		private void PalletLabelPrinterToolStripMenuItemClick(object sender, EventArgs e)
		{
/*			PrintDialog pDialog = new PrintDialog();
			pDialog.ShowDialog();
			connection1.Open();
			command = new SqlCommand("update [Workstation Machine Assignment Table] set [Pallet Printer Name]='" + pDialog.PrinterSettings.PrinterName.Replace("'", "''") + "' where [Workstation Name]='" + System.Environment.MachineName.ToString().Replace("'", "''") + "'", connection1);
			command.ExecuteNonQuery();
			connection1.Close();
			strPalletPrinterName = pDialog.PrinterSettings.PrinterName;
*/		
		}
		
		private void RollLabelPrinterToolStripMenuItemClick(object sender, EventArgs e)
		{
/*			PrintDialog pDialog = new PrintDialog();
			pDialog.ShowDialog();
			connection1.Open();
			command = new SqlCommand("update [Workstation Machine Assignment Table] set [Roll Printer Name]='" + pDialog.PrinterSettings.PrinterName.Replace("'", "''") + "' where [Workstation Name]=" + System.Environment.MachineName.ToString().Replace("'", "''") + "'", connection1);
			command.ExecuteNonQuery();
			connection1.Close();
			strRollPrinterName = pDialog.PrinterSettings.PrinterName;
*/		
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		private void ScanToolStripMenuItemClick(object sender, EventArgs e)
		{
			// Scan a barcode and react accordingly
			GetInputForm readBarcodeForm = new GetInputForm("Scan/Input Barcode", "%", 0, 0, true);
			readBarcodeForm.ShowDialog();
			if (readBarcodeForm.UserInput.Length > 0)
			{
				connection1.Open();
				switch (readBarcodeForm.UserInput.Substring(0, 1))
				{
					case "P": // Pallet barcode label
						command = new SqlCommand("SELECT TOP 1 a.[Master Item No], a.[Width], b.[Description], c.[Description], b.[Reference Item No], CASE WHEN f.[Pallet ID] IS NOT NULL THEN d.[Location ID] END, ISNULL(d.[Location ID], 7), CAST(CASE WHEN g.[Pallet ID] IS NOT NULL THEN 1 ELSE 0 END AS bit), e.[Inventory Available], e.[Description] FROM [Roll Table] a INNER JOIN ([Inventory Master Table] b INNER JOIN [Item Type Table] c ON b.[Item Type No]= c.[Item Type No]) ON a.[Master Item No] = b.[Master Item No] INNER JOIN ([Pallet Table] d INNER JOIN [Location Table] e ON ISNULL(d.[Location ID], 7) = e.[Location ID]) ON a.[Pallet ID] = d.[Pallet ID] LEFT JOIN [Current Pallets at Machine Table] f ON a.[Pallet ID] = f.[Pallet ID] LEFT JOIN [Consigned Pallet Table] g ON a.[Pallet ID] = g.[Pallet ID] AND g.[Shipped By] IS NULL AND g.[Voided By] IS NULL WHERE a.[Current LF] > 0 AND a.[Pallet ID]=" + readBarcodeForm.UserInput.Substring(1), connection1);
						reader1 = command.ExecuteReader();
						if (reader1.Read())
						{
							if (reader1[5] != DBNull.Value)
							{
								MessageBox.Show("Pallet " + readBarcodeForm.UserInput + " is currently being used in production on Job " + reader1[4].ToString() + " on Machine " + reader1[5].ToString() + ".", "Pallet Unavailable");
								reader1.Close();
							}
							else if (!userCanAccessAllInventory && !reader1.GetBoolean(8))
							{
								MessageBox.Show("Pallet " + readBarcodeForm.UserInput + " is not available becuase it is in location L" + reader1[6].ToString() + " - " + reader1[9].ToString(), "Pallet not Available");
								reader1.Close();
							}
							else
							{
								decimal width = (decimal)reader1[1];
								string description = reader1[2].ToString();
								string partType = reader1[3].ToString(); 
								int locationID = (int)reader1[6];
								bool consigned = reader1.GetBoolean(7);
                                bool palletAvailable = reader1.GetBoolean(8);
                                string locationName = reader1[9].ToString();
								reader1.Close();
								string action;
								if (userCanShipReceive && partType == "Raw Film")
								{
									command = new SqlCommand("select count(distinct [Pallet ID]) from [Roll PO Table] where [Pallet ID]=" + readBarcodeForm.UserInput.Substring(1), connection1);
									int palletFound = (int)command.ExecuteScalar();
									if (palletFound == 1)
									{
										OptionsForm frmPalletOptions = new OptionsForm(width.ToString("00.000") + "\" " + description, false, true);
										frmPalletOptions.AddOption("Move Pallet");
										frmPalletOptions.AddOption("Void Pallet Receipt");
										frmPalletOptions.ShowDialog();
										action = frmPalletOptions.Option;
										frmPalletOptions.Dispose();
									}
									else
									{
										action = "Move Pallet";
									}
								}
								else if (userCanConsign && partType == "Finished Goods")
								{
									OptionsForm frmPalletOptions = new OptionsForm(description, false, true);
									if (locationID != 9999 || userCanAccessAllInventory)
									{
										frmPalletOptions.AddOption("Move Pallet");
									}
									
									if (! consigned)
									{
										frmPalletOptions.AddOption("Consign Pallet");
									}
									else
									{
										frmPalletOptions.AddOption("Ship Consigned Pallet");
										frmPalletOptions.AddOption("Void Consigned Pallet");
									}
									
									frmPalletOptions.ShowDialog();
									action = frmPalletOptions.Option;
									frmPalletOptions.Dispose();
								}
								else
								{
									action = "Move Pallet";
								}
								
								switch (action)
								{
									case "Move Pallet":
                                        DialogResult answer = DialogResult.Yes;
                                        if (!palletAvailable)
                                        {
                                            if (locationID == 9999)
                                            {
                                                answer = MessageBox.Show("Pallet " + readBarcodeForm.UserInput + " has been recorded as shipped to the customer.  Do you still wish to move it?", "Pallet's Status is Shipped to Customer", MessageBoxButtons.YesNo);
                                            }
                                            else
                                            {
                                                answer = MessageBox.Show("Pallet " + readBarcodeForm.UserInput + " is in unavailable location L" + locationID.ToString() + " - " + locationName + ".  Do you still wish to move it?", "Pallet Currently Unavailable", MessageBoxButtons.YesNo);
                                            }
                                        }
                                        
                                        if (answer == DialogResult.Yes)
                                        {
                                            MovePallet(readBarcodeForm.UserInput.Substring(1));
                                        }

										break;
									case "Void Pallet Receipt":
										command = new SqlCommand("select e.[RefNumber] from [Roll PO Table] a inner join [Roll Table] b on a.[Roll ID]=b.[Roll ID] inner join [Pallet Table] c on a.[Pallet ID]=c.[Pallet ID] inner join [Film Purchase Detail Table] d on a.[PO Number]=d.[PO Number] and a.[PO Line Item No]=d.[PO Line Item No] left join [QB Item Receipt Xref Table] e on cast(a.[PO Number] as nvarchar(10))+'-'+cast(a.[PO Line Item No] as nvarchar(10))+case when d.[Consignment]=1 then '-'+cast(a.[Pallet ID] as nvarchar(10)) else '' end=e.[RefNumber] and cast(c.[Create Date] as Date)=e.[Create Date] where a.[Pallet ID]=" + readBarcodeForm.UserInput.Substring(1) + " group by e.[RefNumber]", connection1);
										string referenceNumber = command.ExecuteScalar().ToString();
										if (string.IsNullOrEmpty(referenceNumber))
										{
											answer = MessageBox.Show("Void Pallet Receipt " + readBarcodeForm.UserInput + "?", "Confirm Voiding", MessageBoxButtons.YesNo);
											if (answer == DialogResult.Yes)
											{
												command = new SqlCommand("update [Roll PO Table] set [Voided By]='" + StartupForm.UserName + "',[Void Date]=getdate() where [Pallet ID]=" + readBarcodeForm.UserInput.Substring(1), connection1);
												command.ExecuteNonQuery();
												command = new SqlCommand("update [Roll Table] set [Original Units]=0,[Original Pounds]=0,[Original LF]=0,[Current LF]=0 where [Pallet ID]=" + readBarcodeForm.UserInput.Substring(1), connection1);
												command.ExecuteNonQuery();
												command = new SqlCommand("update [Pallet Table] set [Location ID]=NULL where [Pallet ID]=" + readBarcodeForm.UserInput.Substring(1), connection1);
												command.ExecuteNonQuery();
											}
										}
										else	
										{
											MessageBox.Show("Error - this pallet receipt has been transferred to QuickBooks.  Please contact accounting to handle.", "Cannot void Pallet");
										}
										
										break;
									case "Consign Pallet":
										command = new SqlCommand("INSERT INTO [Consigned Pallet Table] SELECT " + readBarcodeForm.UserInput.Substring(1) + ", '" + StartupForm.UserName + "', GETDATE(),  NULL, NULL, NULL, NULL", connection1);
										command.ExecuteNonQuery();
										MovePallet(readBarcodeForm.UserInput.Substring(1));
										break;
									case "Ship Consigned Pallet":
										command = new SqlCommand("UPDATE [Consigned Pallet Table] SET [Shipped By] = '" +  StartupForm.UserName + "', [Ship Date] = GETDATE() WHERE [Voided By] IS NULL AND [Pallet ID] = " + readBarcodeForm.UserInput.Substring(1), connection1);
										command.ExecuteNonQuery();
										command = new SqlCommand("UPDATE [Pallet Table] SET [Location ID] = 9999 WHERE [Pallet ID] = " + readBarcodeForm.UserInput.Substring(1), connection1);
										command.ExecuteNonQuery();
										break;
									case "Void Consigned Pallet":
										command = new SqlCommand("UPDATE [Consigned Pallet Table] SET [Voided By] = '" +  StartupForm.UserName + "', [Void Date] = GETDATE() WHERE [Voided By] IS NULL AND [Pallet ID] = " + readBarcodeForm.UserInput.Substring(1), connection1);
										command.ExecuteNonQuery();
										break;
								}
							}
						}
						else
						{
							reader1.Close();
							command = new SqlCommand("SELECT TOP 1 a.[Master Item No], b.[Description], b.[Reference Item No], ISNULL(c.[Location ID],  7), d.[Inventory Available], d.[Description] FROM [Case Table] a INNER JOIN [Inventory Master Table] b ON a.[Master Item No] = b.[Master Item No] INNER JOIN ([Pallet Table] c INNER JOIN [Location Table] d ON ISNULL(c.[Location ID], 7) = d.[Location ID]) ON a.[Pallet ID] = c.[Pallet ID] WHERE a.[Bags] > 0 AND a.[Pallet ID]=" + readBarcodeForm.UserInput.Substring(1), connection1);
							reader1 = command.ExecuteReader();
							if (reader1.Read())
							{
								if (!reader1.GetBoolean(4) && !userCanAccessAllInventory)
								{
									MessageBox.Show("Error - pallet " + readBarcodeForm.UserInput + " is in location L" + reader1[3].ToString() + " - " + reader1[5].ToString() + ".", "Inventory Unavailable");
									reader1.Close();
								}
								else
								{
                                    DialogResult answer = DialogResult.Yes;
                                    if (!reader1.GetBoolean(4))
                                    {
                                        if ((int)reader1[3] == 9999)
                                        {
                                            answer = MessageBox.Show("Pallet " + readBarcodeForm.UserInput + " has been recorded as shipped to the customer.  Do you still wish to move it?", "Inventory's Status is Shipped to Customer", MessageBoxButtons.YesNo);
                                        }
                                        else
                                        {
                                            answer = MessageBox.Show("Pallet " + readBarcodeForm.UserInput + " is currently unavailable in location L" + reader1[3].ToString() + " - " + reader1[5].ToString() + ".  Do you still wish to move it?", "Inventory Currently Unavailable", MessageBoxButtons.YesNo);
                                        }
                                    }

                                    reader1.Close();
                                    if (answer == DialogResult.Yes)
                                    {
                                        MovePallet(readBarcodeForm.UserInput.Substring(1));
                                    }
								}
							}
							else
							{
								MessageBox.Show("Error - Pallet " + readBarcodeForm.UserInput + " not found.", "Pallet not Found");
							}
						}
						
						connection1.Close();
						break;
					case "R": // Roll barcode label
						command = new SqlCommand("SELECT a.[Master Item No], a.[Width], b.[Description], COALESCE(d.[Location ID], a.[Location ID], 7), CAST(ROUND(a.[Current LF], 0) AS int), ISNULL(a.[Pallet ID], 0), c.[Description], CAST(b.[Reference Item No] AS nvarchar(10)), CAST(CASE WHEN a.[Original LF] = a.[Current LF] THEN 0 ELSE 1 END AS bit), e.[Inventory Available], e.[Description], CAST(b.[Item Type No] AS int) FROM [Roll Table] a INNER JOIN ([Inventory Master Table] b INNER JOIN [Item Type Table] c ON b.[Item Type No] = c.[Item Type No]) ON a.[Master Item No] = b.[Master Item No] LEFT JOIN [Pallet Table] d ON a.[Pallet ID] = d.[Pallet ID], [Location Table] e WHERE COALESCE(d.[Location ID], a.[Location ID], 7) = e.[Location ID] AND a.[Roll ID] = " + readBarcodeForm.UserInput.Substring(1), connection1);
						reader1 = command.ExecuteReader();
						if (reader1.Read())
						{
							if (reader1[3].ToString() == machineNumber && (int)reader1[4] == 0)
							{
								MessageBox.Show("This roll is currently being or was previously consumed on machine " + machineNumber + ".  You must go into production and return it.", "Invalid Roll");
								reader1.Close();
							}
							else if (reader1[6].ToString() == "Finished Goods")
							{
								MessageBox.Show("Finished Goods Transactions cannot be handled here.", "No Options");
								reader1.Close();
							}
                            else if (!reader1.GetBoolean(9) && !userCanAccessAllInventory)
                            {
                                MessageBox.Show("Roll " + readBarcodeForm.UserInput + " is unavailable at location L" + reader1[3].ToString() + " - " + reader1[10] + ".  Please make sure the roll is valid before moving it.", "Roll currently Unavailable");
                            }
                            else if ((int)reader1[4] <= 0 && !userCanAccessAllInventory)
                            {
                                MessageBox.Show("Roll " + readBarcodeForm.UserInput + " has no inventory, therefore you cannot move the roll.", "Roll has no Inventory");
                            }
                            else
							{
								string wipType = "0";
								int masterItemNumber = (int)reader1[0];
								decimal width = (decimal)reader1[1];
								string itemDescription = "(" + reader1[7].ToString() + ") " + reader1[2].ToString();
								int locationID = (int)reader1[3];
								int currentLF = (int)reader1[4];
								int palletNumber = (int)reader1[5];
								string partType = reader1[6].ToString();
								if (partType.Contains("WIP"))
								{
									wipType = reader1[7].ToString().Substring(reader1[7].ToString().Length - 2, 2);
								}								
								
								bool rollUsed = reader1.GetBoolean(8);
								bool rollAvailable = reader1.GetBoolean(9);
								string locationName = reader1[10].ToString();
                                int itemType = (int)reader1[11];
								reader1.Close();
								string action;
								if (partType == "Raw Film" && !rollUsed)
								{
									// Unused Film Rolls can't be edited due to the impact on Quickbooks.  Must be done in office.
									action = "Move Roll";
								}
								else
								{
                                    OptionsForm rollOptionsForm = new OptionsForm(width.ToString("00.000") + "\" " + itemDescription, false, true);
                                    if (currentLF > 0)
                                    {
                                        rollOptionsForm.AddOption("Move Roll");
                                        if (userCanAccessAllInventory)
                                        {
                                            rollOptionsForm.AddOption("Scrap Roll");
											rollOptionsForm.AddOption("Roll Notes");
										}
                                    }
                                    else
                                    {
                                        MessageBox.Show("Roll " + readBarcodeForm.UserInput + "has no inventory, therefore the only option you have is to edit the roll to bring it back into inventory.", "Roll has not Inventory");
                                    }

                                    if (userCanAccessAllInventory)
                                    {
                                        rollOptionsForm.AddOption("Edit Roll");
                                    }

                                    if (wipType.Substring(0, 1) == "1" || wipType.Substring(0, 1) == "2")
                                    {
                                        rollOptionsForm.AddOption("QC Check Roll");
                                    }

                                    rollOptionsForm.ShowDialog();
                                    action = rollOptionsForm.Option;
                                    rollOptionsForm.Dispose();
								}
								
								switch (action)
								{
									case "Move Roll":
                                        DialogResult answer = DialogResult.Yes;
                                        if (!rollAvailable)
                                        {
                                            if (locationID == 9999)
                                            {
                                                answer = MessageBox.Show("Roll " + readBarcodeForm.UserInput + " has been recorded as shipped to the customer.  Do you still wish to move it?", "Roll's Status is Shipped to Customer", MessageBoxButtons.YesNo);
                                            }
                                            else
                                            {
                                                answer = MessageBox.Show("Roll " + readBarcodeForm.UserInput + " is in unavailable location L" + locationID.ToString() + " - " + locationName + ".  Do you still wish to move it?", "Roll Currently Unavailable", MessageBoxButtons.YesNo);
                                            }
                                        }

                                        if (answer == DialogResult.Yes)
                                        {
                                            ModulesClass.MoveRoll(readBarcodeForm.UserInput, masterItemNumber, width, itemDescription, palletNumber, partType);
                                        }

										break;
                                    case "QC Check Roll":
                                        ModulesClass.QCRollCheck(wipType, itemType, readBarcodeForm.UserInput);
										break;
									case "Edit Roll":
										UnitInformationForm frmRollAdjustmentInformation = new UnitInformationForm("Adjust Roll " + readBarcodeForm.UserInput, width.ToString("00.000") + "\" " + itemDescription, 99999, true);
										command = new SqlCommand("select cast([Current LF] as int),[Original Pounds]*[Current LF]/[Original LF] from [Roll Table] where [Roll ID]=" + readBarcodeForm.UserInput.Substring(1), connection1);
										reader1 = command.ExecuteReader();
										reader1.Read();
										frmRollAdjustmentInformation.Units = (int)reader1[0];
										frmRollAdjustmentInformation.Pounds = (decimal)reader1[1];
										reader1.Close();
										frmRollAdjustmentInformation.ShowDialog();
										if (frmRollAdjustmentInformation.Units > 0 && frmRollAdjustmentInformation.Pounds > 0)
										{
											OptionsForm adjustmentReasonOptionsForm = new OptionsForm(width.ToString("00.000") + "\" " + itemDescription, false, true);
											command = new SqlCommand("select [Description] from [Adjustment Reason Table] order by isnull([Operation ID],0),[Display Order]", connection1);
											reader1 = command.ExecuteReader();
											while (reader1.Read())
											{
												adjustmentReasonOptionsForm.AddOption(reader1[0].ToString());
											}
											
											reader1.Close();
											adjustmentReasonOptionsForm.ShowDialog();
											if (adjustmentReasonOptionsForm.Option != "Abort")
											{
												if (frmRollAdjustmentInformation.Notes.Length > 0)
												{
													command = new SqlCommand("insert into [Adjustment Transaction Table] select '" + StartupForm.UserName + "',GETDATE()," + readBarcodeForm.UserInput.Substring(1) + ",a.[Current LF],a.[Original Pounds]*a.[Current LF]/a.[Original LF]," + frmRollAdjustmentInformation.Units.ToString() + "," + frmRollAdjustmentInformation.Pounds.ToString() + ",b.[Adjustment Reason ID],'" + frmRollAdjustmentInformation.Notes.Replace("'", "''") + "' from [Roll Table] a, [Adjustment Reason Table] b where a.[Roll ID]=" + readBarcodeForm.UserInput.Substring(1) + " and b.[Description]='" + adjustmentReasonOptionsForm.Option.Replace("'", "''") + "'", connection1);
												}
												else
												{
													command = new SqlCommand("insert into [Adjustment Transaction Table] select '" + StartupForm.UserName + "',GETDATE()," + readBarcodeForm.UserInput.Substring(1) + ",a.[Current LF],a.[Original Pounds]*a.[Current LF]/a.[Original LF]," + frmRollAdjustmentInformation.Units.ToString() + "," + frmRollAdjustmentInformation.Pounds.ToString() + ",b.[Adjustment Reason ID],null from [Roll Table] a, [Adjustment Reason Table] b where a.[Roll ID]=" + readBarcodeForm.UserInput.Substring(1) + " and b.[Description]='" + adjustmentReasonOptionsForm.Option.Replace("'", "''") + "'", connection1);
												}
												
												command.ExecuteNonQuery();
												PrintClass.Label(readBarcodeForm.UserInput);
											}
											
											adjustmentReasonOptionsForm.Dispose();
											frmRollAdjustmentInformation.Dispose();
										}
										
										break;
									case "Scrap Roll":
										OptionsForm frmScrapRollReasonOptions = new OptionsForm(width.ToString("00.000") + "\" " + itemDescription, false, true);
										command = new SqlCommand("select [Description] from [Adjustment Reason Table] order by isnull([Operation ID],0),[Display Order]", connection1);
										reader1 = command.ExecuteReader();
										while (reader1.Read())
										{
											frmScrapRollReasonOptions.AddOption(reader1[0].ToString());
										}
										
										reader1.Close();
										frmScrapRollReasonOptions.ShowDialog();
										if (frmScrapRollReasonOptions.Option != "Abort")
										{
                                            CommentForm reasonForScrappingForm = new CommentForm("Reason for Scrapping", string.Empty, true);
                                            reasonForScrappingForm.ShowDialog();
                                            if (reasonForScrappingForm.Comment.Length > 0)
                                            {
                                                command = new SqlCommand("insert into [Adjustment Transaction Table] select '" + StartupForm.UserName + "',GETDATE()," + readBarcodeForm.UserInput.Substring(1) + ",a.[Current LF],a.[Original Pounds]*a.[Current LF]/a.[Original LF],0,0,b.[Adjustment Reason ID],'" + reasonForScrappingForm.Comment.Replace("'", "''") + "' from [Roll Table] a, [Adjustment Reason Table] b where a.[Roll ID]=" + readBarcodeForm.UserInput.Substring(1) + " and b.[Description]='" + frmScrapRollReasonOptions.Option.Replace("'", "''") + "'", connection1);
                                            }
                                            else
                                            {
                                                command = new SqlCommand("insert into [Adjustment Transaction Table] select '" + StartupForm.UserName + "',GETDATE()," + readBarcodeForm.UserInput.Substring(1) + ",a.[Current LF],a.[Original Pounds]*a.[Current LF]/a.[Original LF],0,0,b.[Adjustment Reason ID],null from [Roll Table] a, [Adjustment Reason Table] b where a.[Roll ID]=" + readBarcodeForm.UserInput.Substring(1) + " and b.[Description]='" + frmScrapRollReasonOptions.Option.Replace("'", "''") + "'", connection1);
                                            }

											command.ExecuteNonQuery();
                                            reasonForScrappingForm.Dispose();
										}
										
										frmScrapRollReasonOptions.Dispose();
									break;
									case "Roll Notes":
										CommentForm rollNotes = new CommentForm("Roll Notes", string.Empty, true);
										rollNotes.ShowDialog();
										if (rollNotes.Comment.Length > 0)
										{
											command = new SqlCommand("insert into [Adjustment Transaction Table] select '" + StartupForm.UserName + "',GETDATE()," + readBarcodeForm.UserInput.Substring(1) + ",[Current LF],[Original Pounds]*[Current LF]/[Original LF],[Current LF],[Original Pounds]*[Current LF]/[Original LF],56,'" + rollNotes.Comment.Replace("'", "''") + "' from [Roll Table] where [Roll ID]=" + readBarcodeForm.UserInput.Substring(1), connection1);
										}

										command.ExecuteNonQuery();
										rollNotes.Dispose();
										break;

								}
							}
						}
						else
						{
							reader1.Close();
							MessageBox.Show("Error - roll " + readBarcodeForm.UserInput + " not found", "Invalid Roll");
						}
						
						connection1.Close();
						break;
					case "C": //Case barcode label
						command = new SqlCommand("SELECT a.[Master Item No], b.[Description], COALESCE(d.[Location ID], a.[Location ID], 7), ISNULL(a.[Pallet ID], 0), c.[Description], CAST(b.[Reference Item No] AS nvarchar(10)), e.[Inventory Available], e.[Description], a.[Bags], a.[Original Pounds] FROM [Case Table] a INNER JOIN ([Inventory Master Table] b INNER JOIN [Item Type Table] c ON b.[Item Type No] = c.[Item Type No]) ON a.[Master Item No] = b.[Master Item No] LEFT JOIN [Pallet Table] d ON a.[Pallet ID] = d.[Pallet ID], [Location Table] e WHERE COALESCE(d.[Location ID], a.[Location ID], 7) = e.[Location ID] AND a.[Case ID] = " + readBarcodeForm.UserInput.Substring(1), connection1);
						reader1 = command.ExecuteReader();
						if (reader1.Read())
						{
							if (reader1[4].ToString() == "Finished Good")
							{
								MessageBox.Show("Finished Goods Transactions cannot be handled here.", "No Options");
								reader1.Close();
							}
							else if (!reader1.GetBoolean(6) && ! userCanAccessAllInventory)
							{
								MessageBox.Show("Case " + readBarcodeForm.UserInput + " is unavailable at location L" + reader1[2].ToString() + " - " + reader1[7].ToString() + ".  Please make sure the case is valid before moving it.", "Case currently Unavailable");
								reader1.Close();
							}
                            else if ((int)reader1[8] <= 0  && !userCanAccessAllInventory)
                            {
                                MessageBox.Show("Case " + readBarcodeForm.UserInput + ", therefore you cannot move the case.", "Case has no Inventory");
                                reader1.Close();
                            }
                            else
							{
								int masterItemNumber = (int)reader1[0];
                                string itemDescription = "(" + reader1[5].ToString() + ") " + reader1[1].ToString();
                                int locationID = (int)reader1[2];
                                int palletNumber = (int)reader1[3];
                                string partType = reader1[4].ToString();
                                bool caseAvailable = reader1.GetBoolean(6);
                                string locationName = reader1[7].ToString();
                                int bags = (int)reader1[8];
                                decimal pounds = (decimal)reader1[9];
								reader1.Close();
                                string action = "Move Case";
								
                                if (userCanAccessAllInventory)
                                {
                                    OptionsForm caseOptionsForm = new OptionsForm(itemDescription, false, true);
									if (bags > 0)
                                    {
                                        caseOptionsForm.AddOption("Move Case");
                                        caseOptionsForm.AddOption("Scrap Case");
										caseOptionsForm.AddOption("Case Notes");

									}
                                    else
                                    {
                                        MessageBox.Show("Case " + readBarcodeForm.UserInput + "has no inventory, therefore the only option you have is to edit the case to bring it back into inventory.", "Case has not Inventory");
                                    }

                                    caseOptionsForm.AddOption("Edit Case");
									caseOptionsForm.ShowDialog();
                                    action = caseOptionsForm.Option;
                                    caseOptionsForm.Dispose();
                                }
//								caseOptionsForm.AddOption("QC Check Case");
								
								switch (action)
								{
									case "Move Case":
                                        DialogResult answer = DialogResult.Yes;
                                        if (!caseAvailable)
                                        {
                                            if (locationID == 9999)
                                            {
                                                answer = MessageBox.Show("Case " + readBarcodeForm.UserInput + " has been recorded as shipped to the customer.  Do you still wish to move it?", "Case's Status is Shipped to Customer", MessageBoxButtons.YesNo);
                                            }
                                            else
                                            {
                                                answer = MessageBox.Show("Case " + readBarcodeForm.UserInput + " is in unavailable location L" + locationID.ToString() + " - " + locationName + ".  Do you still wish to move it?", "Case Currently Unavailable", MessageBoxButtons.YesNo);
                                            }
                                        }

                                        if (answer == DialogResult.Yes)
                                        {
                                            ModulesClass.MoveCase(readBarcodeForm.UserInput, masterItemNumber, itemDescription, palletNumber, partType);
                                        }
                                        break;
									case "Edit Case":
										if (partType == "Finished Goods")
										{
											MessageBox.Show("You cannont edit a finished goods case.  Please unfinish the pallet for the case and they try again.", "Cannot edit a Finished Goods Case");
										}
										else
										{
											UnitInformationForm getCaseInformationForm = new UnitInformationForm("Enter Case Count and Weight", itemDescription, 99999, true);
											getCaseInformationForm.Units = bags;
											getCaseInformationForm.Pounds = pounds;
											getCaseInformationForm.UnitName = "Bags";
											getCaseInformationForm.ShowDialog();
											if (getCaseInformationForm.Units > 0 && getCaseInformationForm.Pounds > 0)
											{
												OptionsForm adjustmentReasonOptionsForm = new OptionsForm(itemDescription, false, true);
												command = new SqlCommand("SELECT [Description] FROM [Adjustment Reason Table] WHERE [Operation ID] = 5 ORDER BY [Display Order]", connection1);
												reader1 = command.ExecuteReader();
												while (reader1.Read())
												{
													adjustmentReasonOptionsForm.AddOption(reader1[0].ToString());
												}
												
												reader1.Close();
												adjustmentReasonOptionsForm.ShowDialog();
												if (adjustmentReasonOptionsForm.Option != "Abort")
												{
													if (getCaseInformationForm.Notes.Length > 0)
													{
														command = new SqlCommand("INSERT INTO [Case Adjustment Transaction Table] SELECT '" + StartupForm.UserName + "', GETDATE(), " + readBarcodeForm.UserInput.Substring(1) + ", a.[Bags],  a.[Bags] / a.[Original Bags] * a.[Original Pounds], " + getCaseInformationForm.Units.ToString() + "," + getCaseInformationForm.Pounds.ToString() + ", b.[Adjustment Reason ID], '" + getCaseInformationForm.Notes.Replace("'", "''") + "' FROM [Case Table] a, [Adjustment Reason Table] b WHERE a.[Case ID] = " + readBarcodeForm.UserInput.Substring(1) + " AND b.[OperatIon ID] = 5 AND b.[Description] = '" + adjustmentReasonOptionsForm.Option.Replace("'", "''") + "'", connection1);
													}
													else
													{
														command = new SqlCommand("INSERT INTO [Case Adjustment Transaction Table] SELECT '" + StartupForm.UserName + "', GETDATE(), " + readBarcodeForm.UserInput.Substring(1) + ", a.[Bags],  a.[Bags] / a.[Original Bags] * a.[Original Pounds], " + getCaseInformationForm.Units.ToString() + "," + getCaseInformationForm.Pounds.ToString() + ", b.[Adjustment Reason ID], NULL FROM [Case Table] a, [Adjustment Reason Table] b WHERE a.[Case ID] = " + readBarcodeForm.UserInput.Substring(1) + " AND b.[Operation ID] = 5 AND b.[Description] = '" + adjustmentReasonOptionsForm.Option.Replace("'", "''") + "'", connection1);
													}
												
													command.ExecuteNonQuery();
													PrintClass.Label(readBarcodeForm.UserInput);
												}
												
												adjustmentReasonOptionsForm.Dispose();
											}
																					
											getCaseInformationForm.Dispose();
										}

										break;
									case "Scrap Case":
										if (partType == "Finished Goods")
										{
											MessageBox.Show("You cannont scrap a finished goods case.  Please unfinish the pallet for the case and they try again.", "Cannot scrap a Finished Goods Case");
										}
										else
										{
											OptionsForm adjustmentReasonOptionsForm = new OptionsForm(itemDescription, false, true);
											command = new SqlCommand("SELECT [Description] FROM [Adjustment Reason Table] WHERE [Operation ID] = 5 ORDER BY [Display Order]", connection1);
											reader1 = command.ExecuteReader();
											while (reader1.Read())
											{
												adjustmentReasonOptionsForm.AddOption(reader1[0].ToString());
											}
												
											reader1.Close();
											adjustmentReasonOptionsForm.ShowDialog();
											if (adjustmentReasonOptionsForm.Option != "Abort")
											{
                                                CommentForm reasonForScrappingForm = new CommentForm("Reason for Scrapping", string.Empty, true);
                                                reasonForScrappingForm.ShowDialog();
                                                if (reasonForScrappingForm.Comment.Length > 0)
                                                {
                                                    command = new SqlCommand("INSERT INTO [Case Adjustment Transaction Table] SELECT '" + StartupForm.UserName + "', GETDATE(), " + readBarcodeForm.UserInput.Substring(1) + ", a.[Bags], a.[Bags] / a.[Original Bags] * a.[Original Pounds], 0, 0, b.[Adjustment Reason ID],'" + reasonForScrappingForm.Comment.Replace("'", "''") + "' FROM [Case Table] a, [Adjustment Reason Table] b WHERE a.[Case ID] = " + readBarcodeForm.UserInput.Substring(1) + " AND b.[Description] = '" + adjustmentReasonOptionsForm.Option.Replace("'", "''") + "'", connection1);
                                                }
                                                else
                                                {
                                                    command = new SqlCommand("INSERT INTO [Case Adjustment Transaction Table] SELECT '" + StartupForm.UserName + "', GETDATE(), " + readBarcodeForm.UserInput.Substring(1) + ", a.[Bags], a.[Bags] / a.[Original Bags] * a.[Original Pounds], 0, 0, b.[Adjustment Reason ID], NULL FROM [Case Table] a, [Adjustment Reason Table] b WHERE a.[Case ID] = " + readBarcodeForm.UserInput.Substring(1) + " AND b.[Description] = '" + adjustmentReasonOptionsForm.Option.Replace("'", "''") + "'", connection1);
                                                }

												command.ExecuteNonQuery();
                                                reasonForScrappingForm.Dispose();
											}
											
											adjustmentReasonOptionsForm.Dispose();
										}
										
										break;
									case "Case Notes":
										CommentForm caseNotes = new CommentForm("Case Notes", string.Empty, true);
										caseNotes.ShowDialog();
										if (caseNotes.Comment.Length > 0)
										{
											command = new SqlCommand("insert into [Case Adjustment Transaction Table] select '" + StartupForm.UserName + "',GETDATE()," + readBarcodeForm.UserInput.Substring(1) + ",[Bags],[Original Pounds],[Bags],[Original Pounds],56,'" + caseNotes.Comment.Replace("'", "''") + "' from [Roll Table] where [Roll ID]=" + readBarcodeForm.UserInput.Substring(1), connection1);
										}

										command.ExecuteNonQuery();
										caseNotes.Dispose();
										break;
								}
							}
						}
						else
						{
							reader1.Close();
							MessageBox.Show("Error - roll " + readBarcodeForm.UserInput + " not found", "Invalid Roll");
						}
						
						connection1.Close();
						break;

					case "T": // Adhesive tote barcode
						command = new SqlCommand("select a.[Tote ID],a.[Opened By],a.[Closed Date],a.[Location ID],b.[Description] from [Tote Table] a inner join [Location Table] b on a.[Location ID]=b.[Location ID] where b.[Inventory Available]=1 and [Tote ID]=" + readBarcodeForm.UserInput.Substring(1), connection1);
						reader1 = command.ExecuteReader();
						if (reader1.Read())
						{
							if (!string.IsNullOrEmpty(reader1[2].ToString()))
							{
								MessageBox.Show("Error - Tote " + readBarcodeForm.UserInput + " has already been used.", "Invalid Tote");
							}
							else if (!string.IsNullOrEmpty(reader1[1].ToString()))
							{
								MessageBox.Show("Error - Tote " + readBarcodeForm.UserInput + " is currently being used in Location L" + ((int)reader1[3]).ToString() + "(" + reader1[4].ToString() + ")", "Tote in Use");
							}
							else
							{
								GetInputForm frmReadLocation = new GetInputForm("Scan/Input Location", "L", 0, 0, false);
								frmReadLocation.ShowDialog();
								if (frmReadLocation.UserInput.Length > 0)
								{
									if (frmReadLocation.UserInput.Substring(0, 1) == "P")
									{
										MessageBox.Show("Error - totes cannot be put on pallets.", "Invalid Move");
									}
									else
									{
										command = new SqlCommand("select [Description] from [Location Table] where [Location ID]=" + frmReadLocation.UserInput.Substring(1), connection2);
										connection2.Open();
										string locationDescription = (string)command.ExecuteScalar();
										if (!string.IsNullOrEmpty(locationDescription))
										{
											answer = MessageBox.Show("Move tote " + readBarcodeForm.UserInput + " to " + locationDescription + "?", "Confirm Move", MessageBoxButtons.YesNo);
											if (answer == DialogResult.Yes)
											{
												command = new SqlCommand("update [Tote Table] set [Location ID]=" + frmReadLocation.UserInput.Substring(1) + " where [Tote ID]=" + readBarcodeForm.UserInput.Substring(1), connection2);
												command.ExecuteNonQuery();
											}
										}
										
										connection2.Close();
									}
								}
								
								frmReadLocation.Dispose();
							}
						}
						else
						{
							MessageBox.Show("Error - tote " + readBarcodeForm.UserInput + " not found.", "Invalid Tote");
						}
						
						reader1.Close();
						connection1.Close();
						break;
					case "O": // Purchase order barcode
						if (userCanShipReceive)
						{
							command = new SqlCommand("select a.[PO Number],convert(varchar,a.[Date],101),b.[Name],a.[Buyer] from [Purchase Table] a inner join [Vendor Table] b on a.[Vendor ID]=b.[Vendor ID] where a.[PO Number]=" + readBarcodeForm.UserInput.Substring(1), connection1);
							reader1 = command.ExecuteReader();
							if (reader1.Read())
							{
								GetInputForm frmGetPurchasePartNumber = new GetInputForm("Input Part Number", "I", 0, 0, false);
								frmGetPurchasePartNumber.ShowDialog();
								if (frmGetPurchasePartNumber.UserInput.Length > 0)
								{
									command = new SqlCommand("select case when [Film Type ID]< 10 then '0'+cast([Film Type ID] as char(1)) else cast([Film Type ID] as char(2)) end +replicate('0',5-len([Part No]))+cast([Part No] as varchar(5))+case when width < 10 then '0'+cast(floor(width) as char(1)) else cast(floor(width) as char(2)) end+case when floor((width-FLOOR(width))*16)<10 then '0' else '' end+cast(floor((width-FLOOR(width))*16) as varchar(2)),[Film Specification],convert(varchar,[Required Date],101),[width],cast(round([Units],0) as int),b.[Description],[Notes],c.[Description],[PO Line Item No],a.[Master Item No],b.[Std Yield] from [Film Purchase Detail Table] a inner join [Film View] b on a.[Master Item No]=b.[Master Item No] inner join [UOM Table] c on a.[UOM ID]=c.[UOM ID] where [PO Number]=" + reader1[0].ToString() + " and case when [Film Type ID]< 10 then '0'+cast([Film Type ID] as char(1)) else cast([Film Type ID] as char(2)) end +replicate('0',5-len([Part No]))+cast([Part No] as varchar(5))+case when width < 10 then '0'+cast(floor(width) as char(1)) else cast(floor(width) as char(2)) end+case when floor((width-FLOOR(width))*16)<10 then '0' else '' end+cast(floor((width-FLOOR(width))*16) as varchar(2))='" + frmGetPurchasePartNumber.UserInput.Substring(1) + "'", connection2);
									connection2.Open();
									reader2 = command.ExecuteReader();
									if (reader2.Read())
									{
										ReceivingForm frmReceiving = new ReceivingForm(reader1[0].ToString(), reader2[8].ToString(), reader2[9].ToString(), (decimal)reader2[3], (decimal)reader2[10]);
   										frmReceiving.PurchaseOrderDetails(reader1[0].ToString() + "\r\n" + reader1[1].ToString() + "\r\n" + reader1[2].ToString() + "\r\n" + reader1[3].ToString());
   										reader1.Close();
   										connection1.Close();
   										frmReceiving.PartDetails(reader2[0].ToString().Substring(2, 5) + "\r\n" + reader2[3].ToString().Substring(0, 6) + "\" " + reader2[5].ToString() + "\r\n" + reader2[2].ToString() + "\r\n" + ((int)reader2[4]).ToString("N0") + " " + reader2[7].ToString() + "\r\n" + reader2[6].ToString());
   										if (reader2[1] != DBNull.Value)
   										{
   											frmReceiving.SpecificationDetails(reader2[1].ToString());
   										}
   										
   										reader2.Close();
   										connection2.Close();
   										frmReceiving.ShowDialog();
   										frmReceiving.Dispose();
									}
									else
									{
										reader1.Close();
										connection1.Close();
										reader2.Close();
										connection2.Close();
										MessageBox.Show("Error - Part No. " + frmGetPurchasePartNumber.UserInput + " not found on PO No. " + readBarcodeForm.UserInput, "Invalid Part No.");
									}
								}
								
								frmGetPurchasePartNumber.Dispose();
							}
							else
							{
								reader1.Close();
								connection1.Close();
								MessageBox.Show("Error - PO No. " + readBarcodeForm.UserInput + " not found", "Invalid PO");
							}
						}
						else
						{
							connection1.Close();
							MessageBox.Show("Error - you are not authorized to receive in inventory", "No Authorization");
						}
						
						break;
					case "I": // PO line item barcode
						connection1.Close();
						MessageBox.Show("Error - you must scan the PO No. before scanning the Part No.", "Invalid Scan");
						break;
					case "L": // Location barcode
						connection1.Close();
						MessageBox.Show("Error - you must scan the Pallet or Roll you are moving before scanning the location", "Invalid Scan");
						break;
					case "J": // Job Jacket
						if (readBarcodeForm.UserInput.Length == 7) // Try to create bag
						{
							answer = MessageBox.Show("Is this a bag?", "Create Bag Specs", MessageBoxButtons.YesNo);
							if (answer == DialogResult.Yes)
							{
								command = new SqlCommand("SELECT [Master Item No], [Description] FROM [Inventory Master Table] WHERE [Item Type No] = 4 AND [Reference Item No] = " + readBarcodeForm.UserInput.Substring(1), connection1);
								reader1 = command.ExecuteReader();
								if (reader1.Read())
								{
									int masterItemNo = (int)reader1[0];
									string jobDescription = reader1[1].ToString();
									reader1.Close();
									command = new SqlCommand("SELECT b.[Reference Item No], b.[Description] FROM [Bag WIP Input Table] a INNER JOIN [Inventory Master Table] b ON a.[Bag Master Item No] = b.[Master Item No] INNER JOIN [Finished Goods Specification Table] c ON a.[WIP Master Item No] = c.[Input Master Item No] WHERE c.[Job Jacket No] = " + readBarcodeForm.UserInput.Substring(1), connection1);
									reader1 = command.ExecuteReader();
									if (reader1.Read())
									{
										MessageBox.Show("Error - Job " + readBarcodeForm.UserInput + " is already part of existing bag job " + reader1[0].ToString() + " - " + reader1[1].ToString() + ".", "Job Part of Existing Bag Job");
										reader1.Close();
										connection1.Close();
									}
									else
									{
										reader1.Close();
										connection1.Close();
									}
								}
								else
								{
									reader1.Close();
									connection1.Close();
									MessageBox.Show("Error - Finished Good Job " + readBarcodeForm.UserInput + " not found.", "Job Not Found");
								}
							}
							else
							{
								connection1.Close();
							}
						}
						else if (readBarcodeForm.UserInput.Length == 9 && readBarcodeForm.UserInput.Substring(7) == "51") // Look for existing Bag Spec
						{
							command = new SqlCommand("SELECT a.[Master Item No], a.[Description] FROM [Inventory Master Table] a INNER JOIN [Bag Specification Table] b ON a.[Master Item No] = b.[Master Item No] WHERE a.[Item Type No] = 7 AND a.[Reference Item No] = " + readBarcodeForm.UserInput.Substring(1), connection1);
							reader1 = command.ExecuteReader();
							if (reader1.Read())
							{
								int masterItemNo = (int)reader1[0];
								string jobDescription = reader1[1].ToString();
								reader1.Close();
								connection1.Close();
							}
							else
							{
								reader1.Close();
								connection1.Close();
								MessageBox.Show("Error - Bag Job " + readBarcodeForm.UserInput + " not found.", "Job Not Found");
							}
						}
						else							
						{
							connection1.Close();
							MessageBox.Show("Error - the Job Jacket Number must be 6 digits long or a bag item", "Invalid Job Number");
						}
						
						break;
					default: // Unknown barcode
						connection1.Close();
						MessageBox.Show("Error - The first character in the barcode is invalid", "Invalid Barcode");
						break;
				}
			}
			
			readBarcodeForm.Dispose();
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void ProductionToolStripMenuItemClick(object sender, EventArgs e)
		{
			OpenProductionForm(string.Empty);
		}
		
		
		void ReworkToolStripMenuItemClick(object sender, EventArgs e)
		{
			OptionsForm pickMachine = new OptionsForm("Rework Machine", false, true);
			command = new SqlCommand("SELECT ISNULL([Physical Machine No], [Machine No]), [Machine Name] FROM [Machine Table] WHERE [Operation ID] = 4 ORDER BY [Machine No]", connection1);
			connection1.Open();
			reader1 = command.ExecuteReader();
			int currentIndex = 0;
			while (reader1.Read())
			{
				pickMachine.AddOption(reader1[1].ToString());
				if (reader1[0].ToString() == machineNumber)
				{
					pickMachine.InitialSelectedIndex = currentIndex;
				}
				
				currentIndex++;
			}
			
			reader1.Close();
			
			pickMachine.ShowDialog();
			
			if (pickMachine.Option != "Abort")
			{
				command = new SqlCommand("SELECT [Machine No], [Physical Machine No] FROM [Machine Table] WHERE [Machine Name] = '" + pickMachine.Option + "'", connection1);
				reader1 = command.ExecuteReader();
				reader1.Read();
				string reworkMachineNumber = reader1[0].ToString();
				machineNumber = reader1[1].ToString();
				reader1.Close();
				connection1.Close();
				pickMachine.Dispose();
				OpenProductionForm(reworkMachineNumber);
			}
			else
			{
				connection1.Close();
				pickMachine.Dispose();
			}
		}
		
		private void OpenProductionForm(string reworkMachineNumber)
		{
            
            string currentMachineNumber;
            bool newJob = true;

            if (string.IsNullOrEmpty(reworkMachineNumber))
            {
                currentMachineNumber = machineNumber;
            }
            else
            {
                currentMachineNumber = reworkMachineNumber;
            }

            //
            // Determine Operator
            //
            if (operatorNumber == 0)
            {
                command = new SqlCommand("SELECT a.[Number] + ' - ' + a.[Name], CAST(a.[Operator ID] AS int) FROM [Operator Table] a INNER JOIN [Machine Table] b ON a.[Operation ID] = b.[Operation ID] WHERE a.[User ID] = '" + StartupForm.UserName + "' AND b.[Machine No] = " + machineNumber + " AND a.[Active] = 1", connection1);
                connection1.Open();
                reader1 = command.ExecuteReader();
                if (reader1.Read())
                {
                    answer = MessageBox.Show("Is the operator logging in " + reader1[0].ToString() + "?", "Correct Operator?", MessageBoxButtons.YesNo);
                    if (answer == DialogResult.Yes)
                    {
                        operatorName = reader1[0].ToString();
                        operatorNumber = (int)reader1[1];
                    }
                }

                if (!reader1.HasRows || operatorNumber == 0)
                {
                    reader1.Close();
                    OptionsForm getOperatorForm = new OptionsForm("Choose Operator", false, true);
                    command = new SqlCommand("SELECT a.[Number] + ' - ' + a.[Name] FROM [Operator Table] a INNER JOIN [Machine Table] b ON ISNULL(a.[Operation ID], b.[Operation ID]) = b.[Operation ID] WHERE b.[Machine No] = " + currentMachineNumber + " AND a.[Active] = 1 ORDER BY [Number]", connection1);
                    reader1 = command.ExecuteReader();
                    while (reader1.Read())
                    {
                        getOperatorForm.AddOption(reader1[0].ToString());
                    }

                    reader1.Close();
                    getOperatorForm.ShowDialog();
                    if (getOperatorForm.Option != "Abort")
                    {
                        operatorName = getOperatorForm.Option;
                        command = new SqlCommand("SELECT CAST([Operator ID] AS int) FROM [Operator Table] WHERE [Number] + ' - ' + [Name] = '" + getOperatorForm.Option + "'", connection1);
                        operatorNumber = (int)command.ExecuteScalar();
                    }

                    getOperatorForm.Dispose();
                }
                else
                {
                    reader1.Close();
                }

                connection1.Close();
            }
			
			if (operatorNumber != 0)
			{
				bool userValid = true;
                int rewindFeet = 0;
			    command = new SqlCommand("SELECT a.[Last or Current Production ID], CAST(b.[Operator ID] AS int), [dbo].[RoundTime](ISNULL(b.[Start Time] + (ISNULL(b.[Setup Hrs], 0) + ISNULL(b.[DT Hrs], 0) + ISNULL(b.[Run Hrs], 0)) / 24, GETDATE() - 1), .25), CAST(ISNULL(b.[End Reason ID], 3) AS int), b.[Master Item No], CAST(ISNULL(b.[End Output LF], 0) AS int), c.[Reference Item No], c.[Description], b.[Start Time], ISNULL(a.[Last Operating DateTime], CAST(CAST(GETDATE()-180 AS date) AS datetime)), COALESCE(SUM(e.[Original LF]), SUM(g.[Original Bags]), 0), CAST(CASE WHEN h.[Production ID] IS NOT NULL THEN 1 ELSE 0 END AS bit) FROM [Machine Table] a LEFT JOIN ([Production Master Table] b INNER JOIN [Inventory Master Table] c ON b.[Master Item No] = c.[Master Item No]) ON a.[Last or Current Production ID] = b.[Production ID] LEFT JOIN ([Production Roll Table] d LEFT JOIN [Roll Table] e ON d.[Roll ID] = e.[Roll ID]) ON a.[Last or Current Production ID] = d.[Production ID] LEFT JOIN ([Production Case Table] f INNER JOIN [Case Table] g ON f.[Case ID] = g.[Case ID]) ON a.[Last or Current Production ID] = f.[Production ID] LEFT JOIN [Startup Approval Table] h ON a.[Last or Current Production ID] = h.[Production ID] WHERE a.[Machine No] = " + currentMachineNumber + " GROUP BY a.[Last or Current Production ID], CAST(b.[Operator ID] AS int), [dbo].[RoundTime](ISNULL(b.[Start Time] + (ISNULL(b.[Setup Hrs], 0) + ISNULL(b.[DT Hrs], 0) + ISNULL(b.[Run Hrs], 0)) / 24, GETDATE() - 1), .25), CAST(ISNULL(b.[End Reason ID], 3) AS int), b.[Master Item No], CAST(ISNULL(b.[End Output LF], 0) AS int), c.[Reference Item No], c.[Description], b.[Start Time], ISNULL(a.[Last Operating DateTime], CAST(CAST(GETDATE()-180 AS date) AS datetime)), CAST(CASE WHEN h.[Production ID] IS NOT NULL THEN 1 ELSE 0 END AS bit)", connection1);
                connection1.Open();
				reader1 = command.ExecuteReader();
				reader1.Read();
				if ((int)reader1[3] == 3 && reader1[1] != DBNull.Value && (int)reader1[1] != operatorNumber)
				{
					// Production Record is still open but operator has changed.  Check to see if record needs to be closed.
					command = new SqlCommand("SELECT [Number] + ' - ' + [Name] FROM [Operator Table] WHERE [Operator ID] = " + reader1[1].ToString(), connection2);
					connection2.Open();
					string currentOperatorName = (string)command.ExecuteScalar();
					if (machineWorkstation)
					{
						answer = MessageBox.Show("Operator " + currentOperatorName + " is currently logged into machine " + currentMachineNumber + " running job J" + reader1[6].ToString() + " - " + reader1[7].ToString() + ".  Do You wish to log in anyway?", "Change Operator?", MessageBoxButtons.YesNo);
					}
					else
					{
						answer = DialogResult.Yes;
					}
					
					if (answer == DialogResult.Yes)
					{
						command = new SqlCommand("UPDATE [Production Master Table] SET [End Reason ID] = 4 WHERE [Production ID] = " + reader1[0].ToString(), connection2);
						command.ExecuteNonQuery();
                        if (machineWorkstation)
                        {
                            ModulesClass.SendEmail(2, "Forced Production Record Close", "On machine " + currentMachineNumber + " job J" + reader1[6].ToString() + " - " + reader1[7].ToString() + " being run by " + currentOperatorName + " had production record " + reader1[0].ToString() + " forced closed by " + StartupForm.UserName + ".");
                        }
					}
					else
					{
						userValid = false;
					}
				
					connection2.Close();
				}

                if (userValid)
                {
                    if (((int)reader1[3] == 2 || (int)reader1[3] == 3) && (reader1.GetBoolean(11) || (decimal)reader1[10] != 0))
                    {
                        newJob = false;
                    }

                    int currentProductionRecord = 0;
                    int currentJobNumber = 0;
                    DateTime productionStartTime;
                    DateTime endOfShiftTime = DateTime.Today;
                    int lastProductionID = 0;
                    DialogResult result = DialogResult.Yes;
                    if ((int)reader1[3] == 3 && reader1[1] != DBNull.Value && (int)reader1[1] == operatorNumber)
                    {
                        // Production Record is still open
                        currentProductionRecord = (int)reader1[0];
                        currentJobNumber = (int)reader1[6];
                        productionStartTime = (DateTime)reader1[8];
                        command = new SqlCommand("SELECT DATEADD(mi, -b.[Minute Offset] + CASE WHEN b.[End Time] < b.[Start Time] then DATEDIFF(mi, b.[End Time], b.[Start Time]) * 2 - 2 ELSE DATEDIFF(mi, b.[Start Time], b.[End Time]) + 1 END, CAST(CAST('" + productionStartTime.ToString() + "' AS date) AS datetime)) FROM [Machine Table] a INNER JOIN [Shift Time by Operation Table] b ON a.[Operation ID] = b.[Operation ID] WHERE a.[Machine No] = " + currentMachineNumber + " AND '" + productionStartTime.ToString() + "' BETWEEN b.[Effective Date] AND ISNULL(b.[Expire Date], GETDATE()) AND CAST(DATEADD(mi, b.[Minute Offset], '" + productionStartTime.ToString() + "') AS time) BETWEEN CAST(DATEADD(mi, b.[Minute Offset], b.[Start Time]) AS time) AND CAST(DATEADD(mi, b.[Minute Offset], b.[End Time]) AS time)", connection2);
                        connection2.Open();
                        endOfShiftTime = (DateTime)command.ExecuteScalar();
                        connection2.Close();
                    }
                    else
                    {
                        DateTimePickerForm frmGetProductionStartTime;
                        if (reader1[2] != DBNull.Value && (DateTime)reader1[2] >= DateTime.Parse("2015-07-12 07:00:00"))
                        {
                            if (machineWorkstation)
                            {
                                frmGetProductionStartTime = new DateTimePickerForm("Enter Start Time", (DateTime)reader1[2], (DateTime)reader1[2]);
                            }
                            else
                            {
                                frmGetProductionStartTime = new DateTimePickerForm("Enter Start Time", DateTime.Today.AddHours(7), (DateTime)reader1[9]);
                            }
                        }
                        else
                        {
                            frmGetProductionStartTime = new DateTimePickerForm("Enter Start Time", DateTime.Today.AddHours(7), (DateTime)reader1[9]);
                        }

                        frmGetProductionStartTime.ShowDialog();
                        productionStartTime = new DateTime(frmGetProductionStartTime.Selection.Year, frmGetProductionStartTime.Selection.Month, frmGetProductionStartTime.Selection.Day, frmGetProductionStartTime.Selection.Hour, frmGetProductionStartTime.Selection.Minute, 0);
                        frmGetProductionStartTime.Dispose();

                        if (productionStartTime < DateTime.Now.AddHours(-12))
                        {
                            result = MessageBox.Show("The Start time you entered is over 12 hours before the current time.  Is this correct?", "Confirm Start Time", MessageBoxButtons.YesNo);
                        }

                        if (result == DialogResult.Yes)
                        {
                            command = new SqlCommand("SELECT DATEADD(mi, -b.[Minute Offset] + CASE WHEN b.[End Time] < b.[Start Time] then DATEDIFF(mi, b.[End Time], b.[Start Time]) * 2 - 2 ELSE DATEDIFF(mi, b.[Start Time], b.[End Time]) + 1 END, CAST(CAST('" + productionStartTime.ToString() + "' AS date) AS datetime)) FROM [Machine Table] a INNER JOIN [Shift Time by Operation Table] b ON a.[Operation ID] = b.[Operation ID] WHERE a.[Machine No] = " + currentMachineNumber + " AND '" + productionStartTime.ToString() + "' BETWEEN b.[Effective Date] AND ISNULL(b.[Expire Date], GETDATE()) AND CAST(DATEADD(mi, b.[Minute Offset], '" + productionStartTime.ToString() + "') AS time) BETWEEN CAST(DATEADD(mi, b.[Minute Offset], b.[Start Time]) AS time) AND CAST(DATEADD(mi, b.[Minute Offset], b.[End Time]) AS time)", connection2);
                            connection2.Open();
                            endOfShiftTime = (DateTime)command.ExecuteScalar();
                            connection2.Close();
                            GetInputForm readJobJacketForm = new GetInputForm("Scan/Input Job Jacket No. + Process Code", "J", 0, 0, true);
                            readJobJacketForm.ShowDialog();
                            if (readJobJacketForm.UserInput.Length > 0)
                            {
                                if (readJobJacketForm.UserInput.Length != 9)
                                {
                                    MessageBox.Show("Error - the barcode must be 9 digits long-\"J\"+6-digit JJ #+Operation Tag", "Invalid Barcode");
                                }
                                else
                                {
                                    currentJobNumber = int.Parse(readJobJacketForm.UserInput.Substring(1), NumberStyles.Number);
                                    if ((int)reader1[3] == 2 && (int)reader1[6] != currentJobNumber)
                                    {
                                        // The last job was still active on the machine at shift end but the entered job number is not the same
                                        MessageBox.Show("Error - Currently Job J" + reader1[6].ToString() + " - " + reader1[7].ToString() + " is active on machine " + machineNumber + ".  You must either pull or complete the job first before changing jobs.", "Invalid Job Number");
                                        currentJobNumber = (int)reader1[6];
                                    }

                                    if ((int)reader1[3] == 2 && (int)reader1[6] == currentJobNumber)
                                    {
                                        //The job is continuing from prior shift
                                        bool showMessage = false;
                                        string messageText = "For job J" + reader1[6].ToString() + " - " + reader1[7].ToString() + " the system shows at end of last shift:";
                                        if ((int)reader1[5] != 0)
                                        {
                                            rewindFeet = (int)reader1[5];
                                            messageText += "\r\nUnfinished Production Footage of " + ((int)reader1[5]).ToString("N0") + " LF";
                                            showMessage = true;
                                        }

                                        command = new SqlCommand("SELECT [Unwind No], [End Shift Roll ID], [End Shift LF] FROM [Shift Change Unwind Footage Table] WHERE [End Shift LF] > 0 AND [Production ID] = " + reader1[0].ToString(), connection2);
                                        connection2.Open();
                                        reader2 = command.ExecuteReader();
                                        if (reader2.Read())
                                        {
                                            showMessage = true;
                                            lastProductionID = (int)reader1[0];
                                            do
                                            {
                                                messageText += "\r\nUnconsumed Footage on Unwind " + reader2[0].ToString() + " of Roll R" + reader2[1].ToString() + " @ " + ((decimal)reader2[2]).ToString("N0") + " LF";
                                            }
                                            while (reader2.Read());
                                        }

                                        reader2.Close();
                                        connection2.Close();
                                        if (showMessage)
                                        {
                                            MessageBox.Show(messageText + "\r\nIf you disagree please notify your lead.", "Unfinished Rolls on Rewind and/or Unwind(s)");
                                        }
                                    }
                                }
                            }

                            readJobJacketForm.Dispose();
                        }
                    }
                    
                    reader1.Close();
                    if (currentJobNumber != 0)
                    {
                        string operationName;

                        if (string.IsNullOrEmpty(reworkMachineNumber))
                        {

                            command = new SqlCommand("SELECT b.[Description] + ' ' + c.[Description] FROM [Machine Table] a INNER JOIN [Operation Table] b ON a.[Operation ID] = b.[Operation ID] INNER JOIN [Production Process Table] c ON a.[Operation ID] = c.[Operation ID] WHERE a.[Machine No]=" + machineNumber + " AND CAST(a.[Operation ID] AS varchar(1)) + CAST(c.[Process ID] AS varchar(1)) = '" + (currentJobNumber % 100).ToString() + "'", connection1);
                            operationName = (string)command.ExecuteScalar();
                        }
                        else
                        {
                            operationName = "Rework Production";
                        }

                        if (!string.IsNullOrEmpty(operationName))
                        {
                            if (operationName.Substring(0, 5) == "Press" && currentJobNumber.ToString().Substring(currentJobNumber.ToString().Length - 2, 1) != "1")
                            {
                                connection1.Close();
                                MessageBox.Show("Error - Job " + currentJobNumber.ToString() + " is not a print job and cannot be run on machine " + machineNumber + ".", "Invalid Job");
                            }
                            else if (operationName.Substring(0, 8) == "Adhesive" && currentJobNumber.ToString().Substring(currentJobNumber.ToString().Length - 2, 1) != "2")
                            {
                                connection1.Close();
                                 MessageBox.Show("Error - Job " + currentJobNumber.ToString() + " is not an adhesive lamination job and cannot be run on machine " + machineNumber + ".", "Invalid Job");
                             }
                            else if (operationName.Substring(0, 8) == "Slitting" && currentJobNumber.ToString().Substring(currentJobNumber.ToString().Length - 2, 1) != "3")
                            {
                                connection1.Close();
                                MessageBox.Show("Error - Job " + currentJobNumber.ToString() + " is not a slit job and cannot be run on machine " + machineNumber + ".", "Invalid Job");
                            }
                            else if (operationName.Substring(0, 10) == "Bag Making" && currentJobNumber.ToString().Substring(currentJobNumber.ToString().Length - 2, 2) != "51")
                            {
                                connection1.Close();
                                MessageBox.Show("Error - Job " + currentJobNumber.ToString() + " is not a bag making job and cannot be run on machine " + machineNumber + ".", "Invalid Job");
                            }
							else if (operationName.Substring(0, 8) == "Innolock" && currentJobNumber.ToString().Substring(currentJobNumber.ToString().Length - 2, 2) != "61")
							{
								connection1.Close();
								MessageBox.Show("Error - Job " + currentJobNumber.ToString() + " is not a innolock job and cannot be run on machine " + machineNumber + ".", "Invalid Job");
							}
							else
                            {
                                command = new SqlCommand("SELECT [Master Item No], CAST([Item Type No] AS int) FROM [Inventory Master Table] WHERE [Item Type No] IN (2, 3) AND [Reference Item No] = " + currentJobNumber.ToString(), connection1);
                                reader1 = command.ExecuteReader();
                                if (reader1.Read())
                                {
                                    string reworkReason = string.Empty;
									if (currentProductionRecord == 0)
									{
										bool goodJob = true;
										connection2.Open();
										if (operationName == "Rework Production")
										{
											if ((currentJobNumber.ToString().EndsWith("31") || currentJobNumber.ToString().EndsWith("33") || currentJobNumber.ToString().EndsWith("36") || currentJobNumber.ToString().EndsWith("37")) && (int)reader1[1] == 3)
                                            {
                                                reader1.Close();
                                                MessageBox.Show("Error - slit job " + currentJobNumber.ToString() + " is a combo job.  You must scan/enter the individual job number in for rework.", "Invalid Job Number");
                                                goodJob = false;
                                            }

                                            if (goodJob)
                                            {
                                                OptionsForm reworkReasonForm = new OptionsForm("Reason for Rework", false, true);
                                                command = new SqlCommand("SELECT [Description] FROM [Rework Reason Table] ORDER BY [Index Order]", connection2);
                                                reader2 = command.ExecuteReader();
                                                while (reader2.Read())
                                                {
                                                    reworkReasonForm.AddOption(reader2[0].ToString());
                                                }

                                                reader2.Close();
                                                reworkReasonForm.ShowDialog();
                                                if (reworkReasonForm.Option == "Abort")
                                                {
                                                    reader1.Close();
                                                    goodJob = false;
                                                }
                                                else
                                                {
                                                    reworkReason = reworkReasonForm.Option.Replace("'", "''");
                                                }

                                                reworkReasonForm.Dispose();
                                             }
                                        }
                                        else if ((int)reader1[1] == 2 && (operationName == "Slitting Production" || operationName == "Slitting Perf/Slit Production" || operationName == "Slitting Pass 1 Laser Score/Slit Production" || operationName == "Slitting Pass 2 Laser Score/Slit Production"))
                                        {
                                            // Make sure Job isn't a combo
                                            command = new SqlCommand("SELECT ISNULL(b.[Reference Item No], 0) FROM [Slitting Specification Table] a LEFT JOIN [Inventory Master Table] b on a.[Combo Master Item No] = b.[Master Item No] WHERE a.[Master Item No] = " + reader1[0].ToString(), connection2);
                                            int comboNo = (int)command.ExecuteScalar();
                                            if (comboNo > 0)
                                            {
                                                reader1.Close();
                                                MessageBox.Show("Error - job " + currentJobNumber.ToString() + " is part of combo job " + comboNo.ToString() + ".  You must scan/enter the combo job number in for slitting.", "Invalid Job Number");
                                                goodJob = false;
                                            }
                                        }

                                        if (goodJob)
                                        {
                                            command = new SqlCommand("EXECUTE [Create Production Master Table Record Stored Procedure] " + operatorNumber.ToString() + ", '" + StartupForm.UserName + "', " + currentMachineNumber + ", " + reader1[0].ToString() + ", '" + productionStartTime.ToString() + "', " + rewindFeet.ToString(), connection2);
                                            currentProductionRecord = (int)command.ExecuteScalar();
                                            if (machineWorkstation)
                                            {
                                                command = new SqlCommand("UPDATE [Machine Table] SET [Last Operating DateTime] = '" + productionStartTime.ToString() + "', [Last or Current Production ID] = " + currentProductionRecord.ToString() + " WHERE [Machine No] = " + currentMachineNumber, connection2);
                                                command.ExecuteNonQuery();
                                            }

                                            if (!string.IsNullOrEmpty(reworkMachineNumber))
                                            {
                                                command = new SqlCommand("INSERT INTO [Production Rework Reason Table] SELECT " + currentProductionRecord.ToString() + ", [Rework Reason ID] FROM [Rework Reason Table] WHERE [Description] = '" + reworkReason + "'", connection2);
                                                command.ExecuteNonQuery();
                                            }

                                            if (lastProductionID != 0)
                                            {
                                                command = new SqlCommand("SELECT [Uwind No], [End Shift Roll ID], [End Shift LF] FROM [Shift Change Unwind Footage Table] WHERE [End Shift LF] > 0 AND [Production ID] = " + lastProductionID.ToString(), connection3);
                                                connection3.Open();
                                                reader2 = command.ExecuteReader();
                                                while (reader2.Read())
                                                {
                                                    command = new SqlCommand("execute [dbo].[Save Shift Change Unwind Footage Stored Procedure] " + currentProductionRecord.ToString() + ", " + reader2[0].ToString() + ", 0," + reader2[1].ToString() + ", " + reader2[2].ToString(), connection2);
                                                    command.ExecuteNonQuery();
                                                }

                                                reader2.Close();
                                                connection3.Close();
                                            }
                                        }

                                        connection2.Close();
                                    }

                                    if (currentProductionRecord != 0)
                                    {
                                        if (newJob == true)
                                        {
                                            if (operationName == "Bag Making Production")
                                            {
                                                command = new SqlCommand("SELECT ISNULL(SUM([Bags]), 0) FROM [Case Table] WHERE [Master Item No] = " + reader1[0].ToString(), connection2);
                                                connection2.Open();
                                                int productionToDate = (int)command.ExecuteScalar();
                                                if (productionToDate > 0)
                                                {
                                                    newJob = false;
                                                }

                                                connection2.Close();
                                            }
                                            else if (operationName != "Rework Production")
                                            {
                                                command = new SqlCommand("SELECT ISNULL(SUM([Original LF]), 0) FROM [Roll Table] WHERE [Master Item No] = " + reader1[0].ToString(), connection2);
                                                connection2.Open();
                                                decimal productionToDate = (decimal)command.ExecuteScalar();
                                                if (productionToDate > 0)
                                                {
                                                    newJob = false;
                                                }

                                                connection2.Close();
                                            }
                                            else
                                            {
                                                newJob = false;
                                            }
                                        }

                                        Form productionForm;
                                        
                                        switch (operationName)
                                        {
                                            case "Rework Production":
                                                productionForm = new ReworkProductionForm(reworkMachineNumber, operatorName, currentJobNumber.ToString(), reader1[0].ToString(), (int)reader1[1], currentProductionRecord, productionStartTime, endOfShiftTime, rewindFeet);
                                                break;
                                            case "Bag Making Production":
                                                productionForm = new BagMakingProductionForm(operatorName, currentJobNumber.ToString(), reader1[0].ToString(), (int)reader1[1], currentProductionRecord, productionStartTime, endOfShiftTime, rewindFeet, newJob);
                                                break;
                                            case "Slitting Production":
												productionForm = new SlittingProductionForm(operatorName, currentJobNumber.ToString(), reader1[0].ToString(), (int)reader1[1], currentProductionRecord, productionStartTime, endOfShiftTime, rewindFeet, newJob);
                                                break;
											case "Slitting Perf/Slit Production":
												productionForm = new SlittingProductionForm(operatorName, currentJobNumber.ToString(), reader1[0].ToString(), (int)reader1[1], currentProductionRecord, productionStartTime, endOfShiftTime, rewindFeet, newJob);
												break;
											case "Slitting Pass 1 Laser Score/Slit Production":
												productionForm = new SlittingProductionForm(operatorName, currentJobNumber.ToString(), reader1[0].ToString(), (int)reader1[1], currentProductionRecord, productionStartTime, endOfShiftTime, rewindFeet, newJob);
												break;
											case "Slitting Pass 2 Laser Score/Slit Production":
												productionForm = new SlittingProductionForm(operatorName, currentJobNumber.ToString(), reader1[0].ToString(), (int)reader1[1], currentProductionRecord, productionStartTime, endOfShiftTime, rewindFeet, newJob);
												break;
											case "Inno-lok Production":
												productionForm = new InnolokProductionForm(operatorName, currentJobNumber.ToString(), reader1[0].ToString(), currentProductionRecord, productionStartTime, endOfShiftTime, rewindFeet, newJob);
												break;
											default: //Printing or Laminating or Perf Production Only or Pass 1 or 2 Laser Scoring Only
                                                 productionForm = new ProductionForm(operationName, operatorName, currentJobNumber.ToString(), reader1[0].ToString(), (int)reader1[1], currentProductionRecord, productionStartTime, endOfShiftTime,rewindFeet, newJob);
                                                 break;
                                        }

                                        reader1.Close();
                                        connection1.Close();
                                        productionForm.ShowDialog();
                                        productionForm.Dispose();
                                    }
                                }
                                else
                                {
                                    reader1.Close();
                                    connection1.Close();
                                    MessageBox.Show("Error - Job Not Found", "Invalid Job Number");
                                 }
                            }
                        }
                        else
                        {
                            connection1.Close();
                            MessageBox.Show("Error - Invalid Process (" + (currentJobNumber % 100).ToString() + ") for Machine " + machineNumber, "Invalid Operation");
                         }
                    }
                    else
                    {
                        connection1.Close();
                     }
                }
                else
                {
                    reader1.Close();
                    connection1.Close();
                 }
    		}
		}
		
		private void MachineOrOperationToolStripMenuItemDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			if (e.ClickedItem.ToString().Substring(0, 7) == "Machine")
			{
				machineNumber = e.ClickedItem.ToString().Substring(8, 3);
				if (machineNumber.Substring(1, 1) == "2")
				{
					adhesivesToolStripMenuItem.Visible = true;
					adhesiveReceiveToolStripMenuItem.Visible = false;
					adhesiveUseToolStripMenuItem.Visible = false;
				}
				else
				{
					adhesivesToolStripMenuItem.Visible = false;
				}
				
				productionToolStripMenuItem.Visible = true;
				issuesReturnsToolStripMenuItem.Visible = false;
				finishingToolStripMenuItem.Visible = false;
				userCanShipReceive = false;
			}
			else
			{
				productionToolStripMenuItem.Visible = false;
				machineNumber = "0";
				switch (e.ClickedItem.ToString())
				{
					case "Material Planning":
						issuesReturnsToolStripMenuItem.Visible = true;
						finishingToolStripMenuItem.Visible = false;
						adhesivesToolStripMenuItem.Visible = false;
						userCanShipReceive = false;
						break;
					case "Shipping/Receiving":
						adhesivesToolStripMenuItem.Visible = true;
						adhesiveReceiveToolStripMenuItem.Visible = true;
						adhesiveUseToolStripMenuItem.Visible = true;
						issuesReturnsToolStripMenuItem.Visible = false;
						finishingToolStripMenuItem.Visible = false;
						userCanShipReceive = true;
						break;
					case "Finishing":
						finishingToolStripMenuItem.Visible = true;
						issuesReturnsToolStripMenuItem.Visible = false;
						adhesivesToolStripMenuItem.Visible = false;
						userCanShipReceive = false;
						break;
				}
			}
			
			lblMachineOrPrinterInformation.Text = e.ClickedItem.ToString();
		}
	
		private void RollLabelToolStripMenuItemClick(object sender, EventArgs e)
		{
			GetInputForm getRollNumberForm = new GetInputForm("Input Roll ID", "R", 0, 0, false);
			getRollNumberForm.ShowDialog();
			if (getRollNumberForm.UserInput.Length > 0)
			{
				PrintClass.Label(getRollNumberForm.UserInput);
			}
			
			getRollNumberForm.Dispose();
		}
		
		private void PalletLabelToolStripMenuItemClick(object sender, EventArgs e)
		{
			GetInputForm getPalletNumberForm = new GetInputForm("Input Pallet ID", "P", 0, 0, false);
			getPalletNumberForm.ShowDialog();
			if (getPalletNumberForm.UserInput.Length > 0)
			{
				command = new SqlCommand("SELECT b.[Pallet ID], ISNULL(b.[Weight], 0), CAST(ROUND(SUM(a.[Current LF] / a.[Original LF] * a.[Original Pounds]), 0) AS int) FROM [Roll Table] a INNER JOIN [Pallet Table] b ON a.[Pallet ID] = b.[Pallet Id] INNER JOIN [Inventory Master Table] c ON a.[Master Item No] = c.[Master Item No] WHERE c.[Item Type No] = 4 AND a.[Pallet ID] = " + getPalletNumberForm.UserInput.Substring(1) + " GROUP BY b.[Pallet ID], b.[Weight]", connection1);
				connection1.Open();
				reader1 = command.ExecuteReader();
				if (reader1.Read())
				{
					GetInputForm frmGetGrossWeight = new GetInputForm("Gross Pallet Weight", "#", (int)reader1[2], (int)reader1[2] + 200, false);
					while (frmGetGrossWeight.UserInput.Length == 0)
					{
						if ((decimal)reader1[1] != 0)
						{
							frmGetGrossWeight.UserInput = reader1[1].ToString();	
						}
						
						frmGetGrossWeight.ShowDialog();
						if (frmGetGrossWeight.UserInput.Length == 0)
						{
							MessageBox.Show("You MUST enter a pallet weight", "No Going Back!");
						}
					}

					command = new SqlCommand("UPDATE [" + StartupForm.FGDatabase + "].[dbo].[tblFGPallet] SET [PltGrossWeight] = " + frmGetGrossWeight.UserInput + ", [PltNetWeight] = " + reader1[2].ToString() + ", [PltBlankWeight] = " + (int.Parse(frmGetGrossWeight.UserInput, NumberStyles.Number) - (int)reader1[2]) + " WHERE [PltID] = " + getPalletNumberForm.UserInput.Substring(1), connection1);
					reader1.Close();
					command.ExecuteNonQuery();
					command = new SqlCommand("UPDATE [Pallet Table] SET [Weight] = " + frmGetGrossWeight.UserInput + " WHERE [Pallet ID] = " + getPalletNumberForm.UserInput.Substring(1), connection1);
					command.ExecuteNonQuery();
				}
				
				connection1.Close();
				PrintClass.Label(getPalletNumberForm.UserInput);
			}
			
			getPalletNumberForm.Dispose();
		}
		
		private void AdhesiveToteLabelToolStripMenuItemClick(object sender, EventArgs e)
		{
			GetInputForm getToteNumberForm = new GetInputForm("Input Tote ID", "T", 0, 0, false);
			getToteNumberForm.ShowDialog();
			if (getToteNumberForm.UserInput.Length > 0)
			{
				PrintClass.Label(getToteNumberForm.UserInput);
			}
			
			getToteNumberForm.Dispose();
		}
		
		void FilmPickListToolStripMenuItemClick(object sender, EventArgs e)
		{
			GetInputForm readJobJacketForm = new GetInputForm("Scan/Input Job Jacket No. + Process Code", "J", 0, 0, false);
			readJobJacketForm.ShowDialog();
			if (readJobJacketForm.UserInput.Length > 0)
			{
				if (readJobJacketForm.UserInput.Length != 9)
				{
					MessageBox.Show("Error - the barcode must be 9 digits long-\"J\"+6-digit JJ #+Operation Tag", "Invalid Barcode");	
				}
				else
				{
					PickListReportForm pickListForm = new PickListReportForm(readJobJacketForm.UserInput.Substring(1));
					pickListForm.ShowDialog();
					pickListForm.Dispose();
				}
			}
			
			readJobJacketForm.Dispose();
		}
		
		private void AdhesiveReceiveToolStripMenuItemClick(object sender, EventArgs e)
		{
			ReceiveAdhesivesForm frmRecieveAdhesives = new ReceiveAdhesivesForm();
			frmRecieveAdhesives.ShowDialog();
			frmRecieveAdhesives.Dispose();
		}
		
		private void AdhesiveUseToolStripMenuItemClick(object sender, EventArgs e)
		{
			UseAdhesiveTote();
		}
		
		private void AdhesivesToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (!adhesiveReceiveToolStripMenuItem.Visible)
			{
				UseAdhesiveTote();
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void UseAdhesiveTote()
		{
			string currentMachineNumber;
			
			if (machineNumber == "0")
			{
				// Not a Machine's Computer
				OptionsForm frmGetMachineNumber = new OptionsForm("Choose Laminator", false, true);
				command = new SqlCommand("select cast(a.[Machine No] as nvarchar(10)) + ' - ' + a.[Machine Name] from [Machine Table] a inner join [Operation Table] b on a.[Operation ID]=b.[Operation ID] where b.[Description]='Adhesive Lamination' order by a.[Machine No]", connection1);
				connection1.Open();
				reader1 = command.ExecuteReader();
				while (reader1.Read())
				{
					frmGetMachineNumber.AddOption(reader1[0].ToString());
				}
				
				reader1.Close();
				connection1.Close();
				frmGetMachineNumber.ShowDialog();
				currentMachineNumber = frmGetMachineNumber.Option.Substring(0, frmGetMachineNumber.Option.IndexOf(" "));
				frmGetMachineNumber.Dispose();
			}
			else
			{
				currentMachineNumber = machineNumber;
			}
			
			if (currentMachineNumber != "Abort")
			{
				GetInputForm frmReadToteNumber = new GetInputForm("Tote Barcode to be Consumed", "T", 0, 0, true);
				frmReadToteNumber.ShowDialog();
				if (frmReadToteNumber.UserInput.Length > 0)
				{
					command = new SqlCommand("select a.[Tote ID],a.[Opened By],a.[Master Item No],b.[Part No] from [Tote Table] a inner join [Adhesive Table] b on a.[Master Item No]=b.[Master Item No] where [Tote ID]=" + frmReadToteNumber.UserInput.Substring(1), connection1);
					connection1.Open();
					reader1 = command.ExecuteReader();
					if (reader1.Read())
					{
						if (!string.IsNullOrEmpty(reader1[1].ToString()))
						{
							MessageBox.Show("Error - Tote " + frmReadToteNumber.UserInput + " has already been used.", "Invalid Tote");
						}
						else
						{	
							int toteToRemove;
							command = new SqlCommand("select [Tote ID] from [Tote Table] where [Machine No]=" + currentMachineNumber + " and [Master Item No]=" + ((int)reader1[2]).ToString() + " and [Opened Date] is not NULL and [Closed Date] is NULL", connection2);
							connection2.Open();
							object findCurrentTote = command.ExecuteScalar();
							if (findCurrentTote != null)
							{
								toteToRemove = Convert.ToInt32(findCurrentTote);
							}
							else
							{
								toteToRemove = 0;
							}
							
							if (toteToRemove != 0)
							{
								answer = MessageBox.Show("Do you wish to replace tote T" + ((int)toteToRemove).ToString() + " of "  + reader1[3].ToString() + " with tote " + frmReadToteNumber.UserInput + "?", "Confirm Replacement", MessageBoxButtons.YesNo);
							}
							else
							{
								answer = DialogResult.Yes;
							}
							
							if (answer == DialogResult.Yes)
							{
								DateTime currentTime = DateTime.Now;
								command = new SqlCommand("update [Tote Table] set [Location ID]=null,[Closed By]='" + StartupForm.UserName + "',[Closed Date]='" + currentTime + "' where [Tote ID]=" + toteToRemove, connection2);
								command.ExecuteNonQuery();
								command = new SqlCommand("update [Tote Table] set [Location ID]="  + currentMachineNumber + ",[Machine No]=" + currentMachineNumber + ",[Opened By]='" + StartupForm.UserName + "',[Opened Date]='" + currentTime + "' where [Tote ID]=" + frmReadToteNumber.UserInput.Substring(1), connection2);
								command.ExecuteNonQuery();
							}
							
							connection2.Close();
						}
					}
					else
					{
						MessageBox.Show("Error - tote " + frmReadToteNumber.UserInput + " not found.", "Invalid Tote");
					}
					
					reader1.Close();
					connection1.Close();
				}
				
				frmReadToteNumber.Dispose();
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void IssueToolStripMenuItemClick(object sender, EventArgs e)
		{
			GetInputForm readJobJacketForm = new GetInputForm("Scan/Input Job Jacket No. + Process Code", "J", 0, 0, true);
			readJobJacketForm.ShowDialog();
			if (readJobJacketForm.UserInput.Length > 0)
			{
				if (readJobJacketForm.UserInput.Length != 9)
				{
					MessageBox.Show("Error - the barcode must be 9 digits long-\"J\" + 6-digit Job # + Operation Tag", "Invalid Barcode");
				}
				else 
				{
					Boolean validAllocation = true;
					if (readJobJacketForm.UserInput.Substring(7,1) == "1")
					{
						command = new SqlCommand("SELECT a.[Master Item No], a.[Item Type No], b.[Standard Film Width], b.[Print Width], b.[Standard Gauge], b.[Base Linear Feet for Allocation], b.[Base Linear Feet for Allocation] * 12 * b.[Standard Film Width] / [Standard Yield] AS [Standard Input Pounds], c.[Film Type], ISNULL(c.[Compatibility Group], 'NONE') AS [Compatibility Group], c.[Brand], c.[Description] AS [Film Description], cast(b.[Film Customer Supplied] as int) FROM [Inventory Master Table] a INNER JOIN ([Printing Specification Table] b LEFT JOIN [Film View] c ON b.[Input Master Item No] = c.[Master Item No]) ON a.[Master Item No] = b.[Master Item No] WHERE a.[Item Type No] IN (2, 3) AND a.[Reference Item No] = " + readJobJacketForm.UserInput.Substring(1), connection1);
					//	command = new SqlCommand("SELECT a.[Master Item No], a.[Item Type No], b.[Standard Film Width], b.[Print Width], b.[Standard Gauge], b.[Standard Input Linear Feet],      b.[Standard Input Linear Feet] * 12 * b.[Standard Film Width] / [Standard Yield] AS [Standard Input Pounds],      c.[Film Type], ISNULL(c.[Compatibility Group], 'NONE') AS [Compatibility Group], c.[Brand], c.[Description] AS [Film Description], cast(b.[Film Customer Supplied] as int) FROM [Inventory Master Table] a INNER JOIN ([Printing Specification Table] b LEFT JOIN [Film View] c ON b.[Input Master Item No] = c.[Master Item No]) ON a.[Master Item No] = b.[Master Item No] WHERE a.[Item Type No] IN (2, 3) AND a.[Reference Item No] = " + readJobJacketForm.UserInput.Substring(1), connection1);
					}
					else if (readJobJacketForm.UserInput.Substring(7, 2) == "21")
					{
						command = new SqlCommand("SELECT a.[Master Item No], a.[Item Type No], b.[Standard Film Width], b.[Base Linear Feet for Allocation], b.[Base Linear Feet for Allocation] * 12 * b.[Standard Film Width] / b.[Standard Film 1 Yield] AS [Standard Input Pounds 1], b.[Base Linear Feet for Allocation] * 12 * [Standard Film Width] / b.[Standard Film 2 Yield] AS [Standard Input Pounds 2], c.[Film Type] AS [Film Type 1], ISNULL(c.[Compatibility Group], 'NONE') AS [Compatibility Group 1], c.[Brand] AS [Brand 1], c.[Description] AS [Film 1 Description], b.[Standard Film 1 Gauge], CAST(b.[Film 1 Customer Supplied] AS int) AS [Film 1 Customer Supplied], d.[Film Type] AS [Film Type 2], ISNULL(d.[Compatibility Group], 'NONE') AS [Compatibility Group 2], d.[Brand] AS [Brand 2], d.[Description] AS [Film 2 Description], b.[Standard Film 2 Gauge], CAST(b.[Film 2 Customer Supplied] AS int) AS [Film 2 Customer Supplied] FROM [Inventory Master Table] a INNER JOIN ([Lamination Specification Table] b LEFT JOIN [Film View] c ON b.[Input Master Item No 1] = c.[Master Item No] LEFT JOIN [Film View] d ON b.[Input Master Item No 2] = d.[Master Item No]) ON a.[Master Item No] = b.[Master Item No] WHERE a.[Item Type No] IN (2, 3) AND a.[Reference Item No] = " + readJobJacketForm.UserInput.Substring(1), connection1);
					//	command = new SqlCommand("SELECT a.[Master Item No], a.[Item Type No], b.[Standard Film Width], b.[Standard Input Linear Feet],      b.[Standard Input Linear Feet] * 12 * b.[Standard Film Width] / b.[Standard Film 1 Yield] AS [Standard Input Pounds 1],      b.[Standard Input Linear Feet] * 12 * [Standard Film Width] / b.[Standard Film 2 Yield] AS [Standard Input Pounds 2],      c.[Film Type] AS [Film Type 1], ISNULL(c.[Compatibility Group], 'NONE') AS [Compatibility Group 1], c.[Brand] AS [Brand 1], c.[Description] AS [Film 1 Description], b.[Standard Film 1 Gauge], CAST(b.[Film 1 Customer Supplied] AS int) AS [Film 1 Customer Supplied], d.[Film Type] AS [Film Type 2], ISNULL(d.[Compatibility Group], 'NONE') AS [Compatibility Group 2], d.[Brand] AS [Brand 2], d.[Description] AS [Film 2 Description], b.[Standard Film 2 Gauge], CAST(b.[Film 2 Customer Supplied] AS int) AS [Film 2 Customer Supplied] FROM [Inventory Master Table] a INNER JOIN ([Lamination Specification Table] b LEFT JOIN [Film View] c ON b.[Input Master Item No 1] = c.[Master Item No] LEFT JOIN [Film View] d ON b.[Input Master Item No 2] = d.[Master Item No]) ON a.[Master Item No] = b.[Master Item No] WHERE a.[Item Type No] IN (2, 3) AND a.[Reference Item No] = " + readJobJacketForm.UserInput.Substring(1), connection1);	
					}
					else if (readJobJacketForm.UserInput.Substring(7, 2) == "22")
					{
						command = new SqlCommand("SELECT a.[Master Item No], a.[Item Type No], b.[Standard Film Width], b.[Base Linear Feet for Allocation], b.[Base Linear Feet for Allocation] * 12 * b.[Standard Film Width] / b.[Standard Film 1 Yield] AS [Standard Input Pounds 1], b.[Base Linear Feet for Allocation] * 12 * [Standard Film Width] / b.[Standard Film 2 Yield] AS [Standard Input Pounds 2], c.[Film Type] AS [Film Type 1], ISNULL(c.[Compatibility Group], 'NONE') AS [Compatibility Group 1], c.[Brand] AS [Brand 1], c.[Description] AS [Film 1 Description], b.[Standard Film 1 Gauge], CAST(b.[Film 1 Customer Supplied] AS int) AS [Film 1 Customer Supplied], d.[Film Type] AS [Film Type 2], ISNULL(d.[Compatibility Group], 'NONE') AS [Compatibility Group 2], d.[Brand] AS [Brand 2], d.[Description] AS [Film 2 Description], b.[Standard Film 2 Gauge], CAST(b.[Film 2 Customer Supplied] AS int) AS [Film 2 Customer Supplied] FROM [Inventory Master Table] a INNER JOIN ([Lamination Specification Table] b LEFT JOIN [Film View] c ON b.[Input Master Item No 1] = c.[Master Item No] LEFT JOIN [Film View] d ON b.[Input Master Item No 2] = d.[Master Item No]) ON a.[Master Item No] = b.[Master Item No] WHERE a.[Item Type No] IN (2, 3) AND a.[Reference Item No] = " + readJobJacketForm.UserInput.Substring(1), connection1);
						//	command = new SqlCommand("SELECT a.[Master Item No], a.[Item Type No], b.[Standard Film Width], b.[Standard Input Linear Feet],      b.[Standard Input Linear Feet] * 12 * b.[Standard Film Width] / b.[Standard Film 1 Yield] AS [Standard Input Pounds 1],      b.[Standard Input Linear Feet] * 12 * [Standard Film Width] / b.[Standard Film 2 Yield] AS [Standard Input Pounds 2],      c.[Film Type] AS [Film Type 1], ISNULL(c.[Compatibility Group], 'NONE') AS [Compatibility Group 1], c.[Brand] AS [Brand 1], c.[Description] AS [Film 1 Description], b.[Standard Film 1 Gauge], CAST(b.[Film 1 Customer Supplied] AS int) AS [Film 1 Customer Supplied], d.[Film Type] AS [Film Type 2], ISNULL(d.[Compatibility Group], 'NONE') AS [Compatibility Group 2], d.[Brand] AS [Brand 2], d.[Description] AS [Film 2 Description], b.[Standard Film 2 Gauge], CAST(b.[Film 2 Customer Supplied] AS int) AS [Film 2 Customer Supplied] FROM [Inventory Master Table] a INNER JOIN ([Lamination Specification Table] b LEFT JOIN [Film View] c ON b.[Input Master Item No 1] = c.[Master Item No] LEFT JOIN [Film View] d ON b.[Input Master Item No 2] = d.[Master Item No]) ON a.[Master Item No] = b.[Master Item No] WHERE a.[Item Type No] IN (2, 3) AND a.[Reference Item No] = " + readJobJacketForm.UserInput.Substring(1), connection1);	
					}
					else if (readJobJacketForm.UserInput.Substring(7, 2) == "31" || readJobJacketForm.UserInput.Substring(7, 2) == "33")
					{
						command = new SqlCommand("SELECT TOP 1 ISNULL(b.[Combo Master Item No], b.[Master Item No]) as [Master Item No], a.[Item Type No], b.[Standard Input Film Width], b.[Standard Gauge], b.[Base Linear Feet for Allocation], b.[Base Linear Feet for Allocation] * 12 * b.[Standard Input Film Width] / [Standard Yield] AS [Standard Input Pounds], c.[Film Type], ISNULL(c.[Compatibility Group], 'NONE') AS [Compatibility Group], c.[Brand], c.[Description] AS [Film Description], cast(b.[Film Customer Supplied] as int) FROM [Inventory Master Table] a INNER JOIN ([Slitting Specification Table] b LEFT JOIN [Film View] c ON b.[Input Master Item No] = c.[Master Item No]) ON a.[Master Item No] = ISNULL(b.[Combo Master Item No], b.[Master Item No]) WHERE a.[Item Type No] IN (2, 3) AND a.[Reference Item No] = " + readJobJacketForm.UserInput.Substring(1) + " ORDER BY b.[Job Jacket No]", connection1);
					//  command = new SqlCommand("SELECT TOP 1 ISNULL(b.[Combo Master Item No], b.[Master Item No]) as [Master Item No], a.[Item Type No], b.[Standard Input Film Width], b.[Standard Gauge], b.[Standard Input Master Linear Feet], b.[Standard Input Master Linear Feet] * 12 * b.[Standard Input Film Width] / [Standard Yield] AS [Standard Input Pounds], c.[Film Type], ISNULL(c.[Compatibility Group], 'NONE') AS [Compatibility Group], c.[Brand], c.[Description] AS [Film Description], cast(b.[Film Customer Supplied] as int) FROM [Inventory Master Table] a INNER JOIN ([Slitting Specification Table] b LEFT JOIN [Film View] c ON b.[Input Master Item No] = c.[Master Item No]) ON a.[Master Item No] = ISNULL(b.[Combo Master Item No], b.[Master Item No]) WHERE a.[Item Type No] IN (2, 3) AND a.[Reference Item No] = " + readJobJacketForm.UserInput.Substring(1) + " ORDER BY b.[Job Jacket No]", connection1);
					}
					else if (readJobJacketForm.UserInput.Substring(7, 2) == "32")
					{
						command = new SqlCommand("SELECT a.[Master Item No], a.[Item Type No], b.[Standard Film Width], b.[Standard Gauge], b.[Base Linear Feet for Allocation], b.[Base Linear Feet for Allocation] * 12 * b.[Standard Film Width] / b.[Standard Yield] AS [Standard Input Pounds], c.[Film Type], ISNULL(c.[Compatibility Group], 'NONE') AS [Compatibility Group], c.[Brand], c.[Description] AS [Film Description], cast(b.[Film Customer Supplied] as int) FROM [Inventory Master Table] a INNER JOIN ([Perforation Specification Table] b LEFT JOIN [Film View] c ON b.[Input Master Item No] = c.[Master Item No]) ON a.[Master Item No] = b.[Master Item No] WHERE a.[Item Type No] IN (2, 3) AND a.[Reference Item No] = " + readJobJacketForm.UserInput.Substring(1), connection1);
						//  command = new SqlCommand("SELECT TOP 1 ISNULL(b.[Combo Master Item No], b.[Master Item No]) as [Master Item No], a.[Item Type No], b.[Standard Input Film Width], b.[Standard Gauge], b.[Standard Input Master Linear Feet], b.[Standard Input Master Linear Feet] * 12 * b.[Standard Input Film Width] / [Standard Yield] AS [Standard Input Pounds], c.[Film Type], ISNULL(c.[Compatibility Group], 'NONE') AS [Compatibility Group], c.[Brand], c.[Description] AS [Film Description], cast(b.[Film Customer Supplied] as int) FROM [Inventory Master Table] a INNER JOIN ([Slitting Specification Table] b LEFT JOIN [Film View] c ON b.[Input Master Item No] = c.[Master Item No]) ON a.[Master Item No] = ISNULL(b.[Combo Master Item No], b.[Master Item No]) WHERE a.[Item Type No] IN (2, 3) AND a.[Reference Item No] = " + readJobJacketForm.UserInput.Substring(1) + " ORDER BY b.[Job Jacket No]", connection1);
					}

					else if (readJobJacketForm.UserInput.Substring(7, 1) == "5")
                    {
                        command = new SqlCommand("SELECT a.[Master Item No], a.[Item Type No], d.[MatPrintSize1_1], c.[Gauge], d.[LinearFeet] * 1.1, d.[LinearFeet] * 12 * d.[MatPrintSize1_1] / c.[Std Yield] * 1.1, c.[Film Type], ISNULL(c.[Compatibility Group], 'NONE'), c.[Brand], c.[Description], CAST(d.[blnCSMatPrint1] AS int) FROM [Inventory Master Table] a INNER JOIN ([Bag WIP Input Table] b LEFT JOIN [Film View] c ON b.[WIP Master Item No] = c.[Master Item No]) ON a.[Master Item No] = b.[Bag Master Item No]  INNER JOIN [JobJackets].[dbo].[tblJobTicket] d ON FLOOR(a.[Reference Item No] / 100) = d.[JobJacketNo] WHERE a.[Item Type No] IN (2, 3) AND a.[Reference Item No] = " + readJobJacketForm.UserInput.Substring(1), connection1);
                        //  command = new SqlCommand("SELECT TOP 1 ISNULL(b.[Combo Master Item No], b.[Master Item No]) as [Master Item No], a.[Item Type No], b.[Standard Input Film Width], b.[Standard Gauge], b.[Standard Input Master Linear Feet], b.[Standard Input Master Linear Feet] * 12 * b.[Standard Input Film Width] / [Standard Yield] AS [Standard Input Pounds], c.[Film Type], ISNULL(c.[Compatibility Group], 'NONE') AS [Compatibility Group], c.[Brand], c.[Description] AS [Film Description], cast(b.[Film Customer Supplied] as int) FROM [Inventory Master Table] a INNER JOIN ([Slitting Specification Table] b LEFT JOIN [Film View] c ON b.[Input Master Item No] = c.[Master Item No]) ON a.[Master Item No] = ISNULL(b.[Combo Master Item No], b.[Master Item No]) WHERE a.[Item Type No] IN (2, 3) AND a.[Reference Item No] = " + readJobJacketForm.UserInput.Substring(1) + " ORDER BY b.[Job Jacket No]", connection1);
                    }
					else if (readJobJacketForm.UserInput.Substring(7, 1) == "61")
					{
						command = new SqlCommand("SELECT a.[Master Item No], a.[Item Type No], b.[Standard Width], b.[Standard Gauge], b.[Base Linear Feet for Allocation], b.[Base Linear Feet for Allocation] * 12 * b.[Standard Width] / b.[Standard Yield] AS [Standard Input Pounds], c.[Film Type], ISNULL(c.[Compatibility Group], 'NONE') AS [Compatibility Group], c.[Brand], c.[Description] AS [Film Description], cast(b.[Film Customer Supplied] as int) FROM [Inventory Master Table] a INNER JOIN ([Innolok Specification Table] b LEFT JOIN [Film View] c ON b.[Input Master Item No] = c.[Master Item No]) ON a.[Master Item No] = b.[Master Item No] WHERE a.[Item Type No] IN (2, 3) AND a.[Reference Item No] = " + readJobJacketForm.UserInput.Substring(1), connection1);
						//  command = new SqlCommand("SELECT TOP 1 ISNULL(b.[Combo Master Item No], b.[Master Item No]) as [Master Item No], a.[Item Type No], b.[Standard Input Film Width], b.[Standard Gauge], b.[Standard Input Master Linear Feet], b.[Standard Input Master Linear Feet] * 12 * b.[Standard Input Film Width] / [Standard Yield] AS [Standard Input Pounds], c.[Film Type], ISNULL(c.[Compatibility Group], 'NONE') AS [Compatibility Group], c.[Brand], c.[Description] AS [Film Description], cast(b.[Film Customer Supplied] as int) FROM [Inventory Master Table] a INNER JOIN ([Slitting Specification Table] b LEFT JOIN [Film View] c ON b.[Input Master Item No] = c.[Master Item No]) ON a.[Master Item No] = ISNULL(b.[Combo Master Item No], b.[Master Item No]) WHERE a.[Item Type No] IN (2, 3) AND a.[Reference Item No] = " + readJobJacketForm.UserInput.Substring(1) + " ORDER BY b.[Job Jacket No]", connection1);
					}
					else
					{
						MessageBox.Show("Invalid Job Process (" + readJobJacketForm.UserInput.Substring(7, 2) + ")", "Invalid Job Process");
						validAllocation = false;
					}
					
					if (validAllocation)
					{
						connection1.Open();
						reader1 = command.ExecuteReader();
						if (reader1.Read())
						{
							string jobDescriptions = string.Empty;
							command = new SqlCommand("SELECT a.[JobJacketNo], b.[ItemName] FROM [JobJackets].[dbo].[tblJobTicket] a INNER JOIN [JobJackets].[dbo].[tblItem] b ON a.[ItemNo] = b.[ItemNo] WHERE CASE WHEN " + reader1[1].ToString() + " = 2 THEN a.[JobJacketNo] ELSE CAST(a.[ComboNo] AS nvarchar(10)) END = '" + int.Parse(readJobJacketForm.UserInput.Substring(1, readJobJacketForm.UserInput.Length - 3)) + "' ORDER BY a.[JobJacketNo]", connection2);
							connection2.Open();
							reader2 = command.ExecuteReader();
							while (reader2.Read())
							{
								jobDescriptions += " Job " + reader2[0].ToString() + " - " + reader2[1].ToString() + "\r\n";
							}
							
							reader2.Close();
							connection2.Close();
							string filmNumber = string.Empty;
							if (readJobJacketForm.UserInput.Substring(7, 1) == "2")
							{
								// Lamination can have two input films
								if (reader1[9] == DBNull.Value && reader1[15] == DBNull.Value)
								{
									MessageBox.Show("Error - Lam Job " + readJobJacketForm.UserInput + " does not use raw film", "Invalid Job");
									validAllocation = false;
								}
								else
								{
									if (reader1[9] != DBNull.Value && reader1[15] != DBNull.Value)
									{
										OptionsForm pickFilmForm = new OptionsForm("Pick Film for Job " + readJobJacketForm.UserInput, false, true);
										pickFilmForm.AddOption("1:" + reader1[9].ToString());
			   	                	    pickFilmForm.AddOption("2:" + reader1[15].ToString());
										pickFilmForm.ShowDialog();
										if (pickFilmForm.Option != "Abort")
										{
											filmNumber = (int.Parse(pickFilmForm.Option.Substring(0, 1)) - 1).ToString();
										}
										else
										{
											validAllocation = false;
										}

										pickFilmForm.Dispose();
									}
									else if (reader1[9] != DBNull.Value)
									{
										filmNumber = "0";
									}
									else
									{
										filmNumber = "1";
									}
									
									if (validAllocation)
								    {
										command = new SqlCommand("SELECT a.[Allocation ID], b.[Description] AS [Allocation Method], 100 + a.[Overrun Percent Allowance] * 100, a.[Picked By], a.[Pick Date], SUM(c.[Allocated LF]) AS [Allocated LF], SUM(c.[Allocated Pounds]) AS [Allocated Pounds], MAX(c.[Priority]) as [Last Priority] FROM [Allocation Master Table] a INNER JOIN [Allocation Method Table] b ON a.[Allocation Method ID] = b.[Allocation Method ID] INNER JOIN [Allocation Reservation Table] c ON a.[Allocation ID] = c.[Allocation ID] WHERE a.[Release Date] IS NULL AND a.[Void Date] IS NULL AND a.[Master Item No] = " + reader1[0].ToString() + " AND [Lamination Film ID] = " + filmNumber + " GROUP BY a.[Allocation ID], b.[Description], a.[Overrun Percent Allowance], a.[Picked By], a.[Pick Date]", connection2);
									}
								}
							}
							else 
							{
								if (readJobJacketForm.UserInput.Substring(7, 1) == "1" && string.IsNullOrEmpty(reader1[7].ToString()))
								{
									MessageBox.Show("Error - Print Job " + readJobJacketForm.UserInput + " does not use raw film", "Invalid Job");
									validAllocation = false;
								}
								else if (readJobJacketForm.UserInput.Substring(7, 1) == "3" && string.IsNullOrEmpty(reader1[6].ToString()))
								{
									MessageBox.Show("Error - Slit Job " + readJobJacketForm.UserInput + " does not use raw film", "Invalid Job");
									validAllocation = false;
								}
								else
								{
									command = new SqlCommand("SELECT a.[Allocation ID], b.[Description] AS [Allocation Method], 100 + a.[Overrun Percent Allowance] * 100, a.[Picked By], a.[Pick Date], SUM(c.[Allocated LF]) AS [Allocated LF], SUM(c.[Allocated Pounds]) AS [Allocated Pounds], MAX(c.[Priority]) as [Last Priority] FROM [Allocation Master Table] a INNER JOIN [Allocation Method Table] b ON a.[Allocation Method ID] = b.[Allocation Method ID] INNER JOIN [Allocation Reservation Table] c ON a.[Allocation ID] = c.[Allocation ID] WHERE a.[Release Date] IS NULL AND a.[Void Date] IS NULL AND a.[Master Item No] = " + reader1[0].ToString() + " GROUP BY a.[Allocation ID], b.[Description], a.[Overrun Percent Allowance], a.[Picked By], a.[Pick Date]", connection2);
								}
							}
							
							if (validAllocation)
							{
								IssueForm newIssueForm;
								connection2.Open();
								reader2 = command.ExecuteReader();
								if (reader2.Read())
								{
									// There is already an allocation
									if (!string.IsNullOrEmpty(reader2[3].ToString()))
									{
										MessageBox.Show("This allocation has already been picked by " + reader2[3].ToString() + " at " + ((DateTime)reader2[4]).ToString() + ".  You must void the pick in order to modify the alloation.", "Allocation Already Picked");
										validAllocation = false;
									}
									
									if (readJobJacketForm.UserInput.Substring(7, 1) == "1")
									{
										
										newIssueForm = new IssueForm((int)reader1[0], "Material Issue for Print Job " + readJobJacketForm.UserInput, jobDescriptions, (decimal)reader1[2], (decimal)reader1[3], (decimal)reader1[4], (decimal)reader1[5], (decimal)reader1[6], reader1[7].ToString(), reader1[8].ToString(), reader1[9].ToString(), reader1[10].ToString(), (int)reader1[11], (int)reader2[0], reader2[1].ToString(), (decimal)reader2[2], (decimal)reader2[5], (decimal)reader2[6], "NULL", (int)reader2[7]);
									}
									else if (readJobJacketForm.UserInput.Substring(7, 1) == "2")
									{
										if (filmNumber == "0")
										{
   											newIssueForm = new IssueForm((int)reader1[0], "Material Issue for Lam Job " + readJobJacketForm.UserInput, jobDescriptions, (decimal)reader1[2], 0, (decimal)reader1[10], (decimal)reader1[3], (decimal)reader1[4], reader1[6].ToString(), reader1[7].ToString(), reader1[8].ToString(), reader1[9].ToString(), (int)reader1[11], (int)reader2[0], reader2[1].ToString(), (decimal)reader2[2], (decimal)reader2[5], (decimal)reader2[6], filmNumber, (int)reader2[7]);
										}
										else
										{
											newIssueForm = new IssueForm((int)reader1[0], "Material Issue for Lam Job " + readJobJacketForm.UserInput, jobDescriptions, (decimal)reader1[2], 0, (decimal)reader1[16], (decimal)reader1[3], (decimal)reader1[5], reader1[12].ToString(), reader1[13].ToString(), reader1[14].ToString(), reader1[15].ToString(), (int)reader1[17], (int)reader2[0], reader2[1].ToString(), (decimal)reader2[2], (decimal)reader2[5], (decimal)reader2[6], filmNumber, (int)reader2[7]);
										}
									}
									else
									{
										newIssueForm = new IssueForm((int)reader1[0], "Material Issue for Slit Job " + readJobJacketForm.UserInput, jobDescriptions, (decimal)reader1[2], 0, (decimal)reader1[3], (decimal)reader1[4], (decimal)reader1[5], reader1[6].ToString(), reader1[7].ToString(), reader1[8].ToString(), reader1[9].ToString(), (int)reader1[10], (int)reader2[0], reader2[1].ToString(), (decimal)reader2[2], (decimal)reader2[5], (decimal)reader2[6], "NULL", (int)reader2[7]);			
									}
								}
								else
								{
									// There is no allocation
									if (readJobJacketForm.UserInput.Substring(7, 1) == "1")
									{
										decimal defaultAllocationPercentage;
										if ((decimal)reader1[5] < 8000)
										{
											defaultAllocationPercentage = (decimal)160;
										}
										else if ((decimal)reader1[5] < 9000)
										{
											defaultAllocationPercentage = (decimal)150;
										}
										else if ((decimal)reader1[5] < 10000)
										{
											defaultAllocationPercentage = (decimal)140;
										}
										else if ((decimal)reader1[5] < 15000)
										{
											defaultAllocationPercentage = (decimal)130;
										}
										else if ((decimal)reader1[5] < 20000)
										{
											defaultAllocationPercentage = (decimal)120;
										}
										else if ((decimal)reader1[5] < 30000)
										{
											defaultAllocationPercentage = (decimal)115;
										}
										else
										{
											defaultAllocationPercentage = (decimal)110;
										}
										
										newIssueForm = new IssueForm((int)reader1[0], "Material Issue for Print Job " + readJobJacketForm.UserInput, jobDescriptions, (decimal)reader1[2], (decimal)reader1[3], (decimal)reader1[4], (decimal)reader1[5], (decimal)reader1[6], reader1[7].ToString(), reader1[8].ToString(), reader1[9].ToString(), reader1[10].ToString(), (int)reader1[11], 0, "LF", defaultAllocationPercentage, 0, 0, "NULL", 0);
									}
									else if (readJobJacketForm.UserInput.Substring(7, 1) == "2")
									{
										if (filmNumber == "0")
										{
											newIssueForm = new IssueForm((int)reader1[0], "Material Issue for Lam Job " + readJobJacketForm.UserInput, jobDescriptions, (decimal)reader1[2], 0, (decimal)reader1[10], (decimal)reader1[3], (decimal)reader1[4], reader1[6].ToString(), reader1[7].ToString(), reader1[8].ToString(), reader1[9].ToString(), (int)reader1[11], 0, "LF", (decimal)110, 0, 0, filmNumber, 0);
										}
										else
										{
											newIssueForm = new IssueForm((int)reader1[0], "Material Issue for Lam Job " + readJobJacketForm.UserInput, jobDescriptions, (decimal)reader1[2], 0, (decimal)reader1[16], (decimal)reader1[3], (decimal)reader1[5], reader1[12].ToString(), reader1[13].ToString(), reader1[14].ToString(), reader1[15].ToString(), (int)reader1[17], 0, "LF", (decimal)110, 0, 0, filmNumber, 0);
										}
										
									}
									else
									{
										newIssueForm = new IssueForm((int)reader1[0], "Material Issue for Slit Job " + readJobJacketForm.UserInput, jobDescriptions, (decimal)reader1[2], 0, (decimal)reader1[3], (decimal)reader1[4], (decimal)reader1[5], reader1[6].ToString(), reader1[7].ToString(), reader1[8].ToString(), reader1[9].ToString(), (int)reader1[10], 0, "LF", (decimal)110, 0, 0, "NULL", 0);
									}
								}
								
								if (validAllocation)
								{
									reader2.Close();
									connection2.Close();
									reader1.Close();
									connection1.Close();
									newIssueForm.ShowDialog();
								}
								else
								{
									reader2.Close();
									connection2.Close();
								}
								
								newIssueForm.Dispose();
							}
							
							if (!validAllocation)
							{
								reader1.Close();
								connection1.Close();
							}
						}
						else
						{
							reader1.Close();
							connection1.Close();
							MessageBox.Show("Error - Job " + readJobJacketForm.UserInput + " not found", "Invalid Job Number");
						}
					}
				}
			}
			
			readJobJacketForm.Dispose();
		}
		
		void FinishWIPPalletToolStripMenuItemClick(object sender, EventArgs e)
		{
            bool inputOK = true;
            string overrideAuthorizedBy = string.Empty;
            GetInputForm getPalletNumberForm = new GetInputForm("Scan/Input Pallet ID", "P", 0, 0, true);
			getPalletNumberForm.ShowDialog();
			if (getPalletNumberForm.UserInput.Length > 0)
			{
				command = new SqlCommand("SELECT a.[Master Item No], b.[Job Jacket No], c.[Description], CAST(ISNULL(ROUND(d.[Weight], 0), 0) AS int), CAST(ROUND(SUM(a.[Original Pounds] * a.[Current LF] / a.[Original LF]), 0) AS int), COUNT(a.[Roll ID]), [dbo].[Get Numbers Only](b.[UPC Code]) FROM [Roll Table] a INNER JOIN [Finished Goods Specification Table] b ON a.[Master Item No] = b.[Input Master Item No] INNER JOIN [Inventory Master Table] c ON a.[Master Item No] = c.[Master Item No] INNER JOIN ([Pallet Table] d INNER JOIN [Location Table] e ON d.[Location ID] = e.[Location ID]) ON a.[Pallet ID] = d.[Pallet ID] WHERE e.[Inventory Available] = 1 AND a.[Pallet ID] = " + getPalletNumberForm.UserInput.Substring(1) + "GROUP BY a.[Master Item No], b.[Job Jacket No], c.[Description], CAST(ISNULL(ROUND(d.[Weight], 0), 0) AS int), [dbo].[Get Numbers Only](b.[UPC Code])", connection1);
				connection1.Open();
				reader1 = command.ExecuteReader();
                if (reader1.Read())
                {
                    // This is a pallet of Finished WIP
                    if (!string.IsNullOrEmpty(reader1[6].ToString()) && !noUPCValidationRequired)
                    {
                        inputOK = ModulesClass.ValidateUPC(reader1[2].ToString(), string.Empty, reader1[6].ToString(), getPalletNumberForm.UserInput, out overrideAuthorizedBy);
                    }

                    if (inputOK)
                    {
                        GetInputForm getRollCountForm = new GetInputForm("No. of Rolls", "#", 1, 9999, false);
                        getRollCountForm.ShowDialog();
                        if (getRollCountForm.UserInput.Length > 0 && int.Parse(getRollCountForm.UserInput) == (int)reader1[5])
                        {
                            int max;
                            if ((int)reader1[3] == 0)
                            {
                                max = (int)reader1[4];
                            }
                            else
                            {
                                max = (int)reader1[3];
                            }

                            GetInputForm frmGetGrossWeight = new GetInputForm("Gross Pallet Weight", "#", (int)reader1[4], max + 100, false);
                            frmGetGrossWeight.ShowDialog();
                            if (frmGetGrossWeight.UserInput.Length > 0)
                            {
                                command = new SqlCommand("UPDATE [Pallet Table] SET [Created By] = '" + StartupForm.UserName + "', [Create Date] = GETDATE(), [Weight] = " + frmGetGrossWeight.UserInput + ", [Location ID] = CASE WHEN ISNULL([Location ID], 191) BETWEEN 200 AND 10000 THEN [Location ID] ELSE 191 END WHERE [Pallet ID] = " + getPalletNumberForm.UserInput.Substring(1), connection2);
                                connection2.Open();
                                command.ExecuteNonQuery();
                                command = new SqlCommand("UPDATE [Roll Table] SET [Master Item No] = a.[Master Item no], [UOM ID] = b.[UOM ID], [Original Units] = CASE WHEN a.[UOM] = 'LBS' THEN [Original Pounds] WHEN a.[UOM] = 'ROLLS' THEN 1 ELSE FLOOR([dbo].[UOM Conversion]([Current LF], 'LF', a.[Repeat], a.[Stream Width], a.[Linear Feet per Roll], a.[Standard Yield], CASE WHEN a.[UOM]='PCS' THEN 'IMPS' ELSE a.[UOM] END) * a.[Multiplier]) END FROM [Finished Goods Specification Table] a INNER JOIN [UOM Table] b ON a.[UOM] = b.[Description] WHERE [Pallet ID] = " + getPalletNumberForm.UserInput.Substring(1) + " AND a.[Job Jacket No] = " + reader1[1].ToString(), connection2);
                                command.ExecuteNonQuery();
                                command = new SqlCommand("EXECUTE [dbo].[Create Finished Goods Database Records] " + getPalletNumberForm.UserInput.Substring(1), connection2);
                                command.ExecuteNonQuery();
                                connection2.Close();
                                PrintClass.Label(getPalletNumberForm.UserInput);
                            }

                            frmGetGrossWeight.Dispose();
                        }
                        else if (getRollCountForm.UserInput.Length > 0)
                        {
                            MessageBox.Show("Error - the number of rolls you entered does not match the records", "Invalid Number of Rolls");
                        }

                        getRollCountForm.Dispose();
                        if (!string.IsNullOrEmpty(overrideAuthorizedBy))
                        {
                            ModulesClass.SendEmail(2, "Manual UPC Verification", "The UPC code of Finished Goods Pallet P" + getPalletNumberForm.UserInput + " for job " + reader1[1].ToString() + " - " + reader1[2].ToString() + " was manually input.  The override was authorized by " + overrideAuthorizedBy + ".");
                        }
                    }
                }
                else
				{
					command = new SqlCommand("SELECT a.[Master Item No], b.[Job Jacket No], c.[Description], CAST(ISNULL(ROUND(d.[Weight], 0), 0) AS int), CAST(ROUND(SUM(a.[Original Pounds] * a.[Bags] / a.[Original Bags]), 0) AS int), COUNT(a.[Case ID]), [dbo].[Get Numbers Only](b.[UPC Code]) FROM [Case Table] a INNER JOIN [Finished Goods Specification Table] b ON a.[Master Item No] = b.[Input Master Item No] INNER JOIN [Inventory Master Table] c ON a.[Master Item No] = c.[Master Item No] INNER JOIN ([Pallet Table] d INNER JOIN [Location Table] e ON d.[Location ID] = e.[Location ID]) ON a.[Pallet ID] = d.[Pallet ID] WHERE e.[Inventory Available] = 1 AND a.[Pallet ID] = " + getPalletNumberForm.UserInput.Substring(1) + "GROUP BY a.[Master Item No], b.[Job Jacket No], c.[Description], CAST(ISNULL(ROUND(d.[Weight], 0), 0) AS int), [dbo].[Get Numbers Only](b.[UPC Code])", connection1);
					reader1.Close();
					reader1 = command.ExecuteReader();
					if (reader1.Read())
                    {
                        // This is a pallet of Finished WIP
                        if (!string.IsNullOrEmpty(reader1[6].ToString()) && !noUPCValidationRequired)
                        {
                            inputOK = ModulesClass.ValidateUPC(reader1[2].ToString(), string.Empty, reader1[6].ToString(), getPalletNumberForm.UserInput, out overrideAuthorizedBy);
                        }

                        if (inputOK)
                        {
							GetInputForm getCaseCountForm = new GetInputForm("No. of Cases", "#", 1, 9999, false);
							getCaseCountForm.ShowDialog();
							if (getCaseCountForm.UserInput.Length > 0 && int.Parse(getCaseCountForm.UserInput) == (int)reader1[5])
							{
								int max;
								if ((int)reader1[3] == 0)
								{
									max = (int)reader1[4];
								}
								else
								{
									max = (int)reader1[3];
								}
								
								GetInputForm frmGetGrossWeight = new GetInputForm("Gross Pallet Weight", "#", (int)reader1[4], max + 100, false);
								frmGetGrossWeight.ShowDialog();
								if (frmGetGrossWeight.UserInput.Length > 0)
								{
									command = new SqlCommand("UPDATE [Pallet Table] SET [Created By] = '" + StartupForm.UserName + "', [Create Date] = GETDATE(), [Weight] = " + frmGetGrossWeight.UserInput + ", [Location ID] = CASE WHEN ISNULL([Location ID], 191) < 200 THEN 191 ELSE[Location ID] END WHERE [Pallet ID] = " + getPalletNumberForm.UserInput.Substring(1), connection2);
									connection2.Open();
									command.ExecuteNonQuery();
									command = new SqlCommand("UPDATE [Case Table] SET [Master Item No] = a.[Master Item no] FROM [Finished Goods Specification Table] a WHERE [Pallet ID] = " + getPalletNumberForm.UserInput.Substring(1) + " AND [Job Jacket No] = " + reader1[1].ToString(), connection2);
									command.ExecuteNonQuery();
									command = new SqlCommand("EXECUTE [dbo].[Create Finished Goods Case Database Records] " + getPalletNumberForm.UserInput.Substring(1), connection2);
									command.ExecuteNonQuery();
									connection2.Close();
									PrintClass.Label(getPalletNumberForm.UserInput);
								}
						
								frmGetGrossWeight.Dispose();
							}
							else if (getCaseCountForm.UserInput.Length > 0)
							{
								MessageBox.Show("Error - the number of cases you entered does not match the records", "Invalid Number of Cases");
							}
					
							getCaseCountForm.Dispose();
                            if (!string.IsNullOrEmpty(overrideAuthorizedBy))
                            {
                                ModulesClass.SendEmail(2, "Manual UPC Verification", "The UPC code of Finished Goods Pallet P" + getPalletNumberForm.UserInput + " for job " + reader1[1].ToString() + " - " + reader1[2].ToString() + " was manually input.  The override was authorized by " + overrideAuthorizedBy + ".");
                            }
                        }
					}
                    else
                    {
                        inputOK = false;
                    }
				}
                
                if (!inputOK)
                {
                    command = new SqlCommand("SELECT DISTINCT f.[Description] + ' ' + CAST(e.[Reference Item No] AS nvarchar(10)) + ' - ' + e.[Description] FROM [Pallet Table] a INNER JOIN [Location Table] b ON a.[Location ID] = b.[Location ID] LEFT JOIN [Roll Table] c ON a.[Pallet ID] = c.[Pallet ID] LEFT JOIN [Case Table] d ON a.[Pallet ID] = d.[Pallet ID], ([Inventory Master Table] e INNER JOIN [Item Type Table] f ON e.[Item Type No] = f.[Item Type No]) WHERE b.[Inventory Available] = 1 AND ISNULL(c.[Master Item No], d.[Master Item No]) = e.[Master Item No] AND a.[Pallet ID] = " + getPalletNumberForm.UserInput.Substring(1), connection2);
                    connection2.Open();
                    string description = (string)command.ExecuteScalar();
                    connection2.Close();
                    if (!string.IsNullOrEmpty(description))
                    {
                        MessageBox.Show("Pallet " + getPalletNumberForm.UserInput + " is a pallet of " + description + ", which is not a finished WIP Pallet", "Invalid Pallet");
                    }
                    else
                    {
                        MessageBox.Show("Pallet Not Found", "Invalid Pallet");
                    }
                }

                reader1.Close();
			    connection1.Close();
			}

			getPalletNumberForm.Dispose();
		}	
		
		void CreateNewFGPalletToolStripMenuItemClick(object sender, EventArgs e)
		{
            ModulesClass.CreateFGPallet();
/*
            GetInputForm readBarcodeForm = new GetInputForm("Scan/Input Barcode", "R", 0, 0);
			readBarcodeForm.ShowDialog();
			if (readBarcodeForm.UserInput.Length > 0)
			{
				if (readBarcodeForm.UserInput.Substring(0,1) == "R")
				{
					command = new SqlCommand("SELECT a.[Master Item No], c.[Description], CASE WHEN c.[Description] = 'Finished Goods' THEN a.[Pallet ID] ELSE 0 END, ISNULL(CAST(b.[Reference Item No] AS nvarchar(10)), ''), b.[Description] FROM [Roll Table] a INNER JOIN ([Inventory Master Table] b INNER JOIN [Item Type Table] c ON b.[Item Type No] = c.[Item Type No]) ON a.[Master Item No] = b.[Master Item No] LEFT JOIN [Pallet Table] d ON a.[Pallet ID] = d.[Pallet ID], [Location Table] e WHERE ISNULL(d.[Location ID], a.[Location ID]) = e.[Location ID] AND e.[Inventory Available] = 1 AND a.[Current LF] > 0 AND a.[Roll ID] = " + readBarcodeForm.UserInput.Substring(1), connection1);
					connection1.Open();
					reader1 = command.ExecuteReader();
					if (reader1.Read())
					{
						string itemType = reader1[1].ToString();
						if (itemType != "WIP" && itemType != "Finished Goods")
						{
							MessageBox.Show("Error - this roll is " + itemType + " item (" + reader1[3].ToString() + ")" + reader1[4].ToString() + ".  Only Finished WIP and Finished Goods can be palletized here.", "Invalid Roll Product Type");
						}
						else
						{
							int currentPalletNumber = (int)reader1[2];
							if (itemType == "WIP")
							{
								command = new SqlCommand("SELECT a.[Master Item No], a.[Input Master Item No], a.[Job Jacket No], b.[Description] FROM [Finished Goods Specification Table] a INNER JOIN [Inventory Master Table] b ON a.[Master Item No] = b.[Master Item No] WHERE a.[Input Master Item No] = " + reader1[0].ToString(), connection1);
							}
							else
							{
								command = new SqlCommand("SELECT a.[Master Item No], a.[Input Master Item No], a.[Job Jacket No], b.[Description] FROM [Finished Goods Specification Table] a INNER JOIN [Inventory Master Table] b ON a.[Master Item No] = b.[Master Item No] WHERE a.[Master Item No] = " + reader1[0].ToString(), connection1);
							}
						
							reader1.Close();
							reader1 = command.ExecuteReader();
							if (reader1.Read())
							{
								DialogResult answer = MessageBox.Show("Do you wish to create a finished goods pallet for Job " + reader1[2].ToString() + " - " + reader1[3].ToString() + "?", "Create Pallet Confirmation", MessageBoxButtons.YesNo);
								if (answer == DialogResult.Yes)
								{
									int finishedGoodMasterItemNumber = (int)reader1[0];
									int wipMasterItemNumber = (int)reader1[1];
									int jobJacketNumber = (int)reader1[2];
									string jobDescription = reader1[3].ToString();
									reader1.Close();
									command = new SqlCommand("EXECUTE [Create Pallet Stored Procedure] '" + StartupForm.UserName + "', 191, 0, NULL", connection1);
									reader1 = command.ExecuteReader();
									reader1.Read();
									int palletNumber = (int)reader1[0];
									reader1.Close();
									if (currentPalletNumber != 0)
									{
										MessageBox.Show("YOU WILL NEED TO REPRINT FINISHED GOODS PALLET LABEL P" + currentPalletNumber.ToString() + " IF IT STILL EXISTS SINCE YOU ARE MOVING THIS ROLL OFF OF IT!", "DON'T FORGET TO REPRINT LABEL!");
									}
								
									command = new SqlCommand("INSERT INTO [Move Transaction Table] SELECT '" + StartupForm.UserName + "', GETDATE(), " + readBarcodeForm.UserInput.Substring(1) + ", " + palletNumber.ToString() + ", NULL", connection1);
									command.ExecuteNonQuery();
									if (itemType == "WIP")
									{
										command = new SqlCommand("UPDATE [Roll Table] SET [Master Item No] = a.[Master Item no], [UOM ID] = b.[UOM ID], [Original Units] = CASE WHEN a.[UOM] = 'LBS' THEN [Original Pounds] WHEN a.[UOM] = 'ROLLS' THEN 1 ELSE FLOOR([dbo].[UOM Conversion]([Current LF], 'LF', a.[Repeat], a.[Stream Width], a.[Linear Feet per Roll], a.[Standard Yield], CASE WHEN a.[UOM]='PCS' THEN 'IMPS' ELSE a.[UOM] END) * a.[Multiplier]) END FROM [Finished Goods Specification Table] a INNER JOIN [UOM Table] b ON a.[UOM] = b.[Description] WHERE [Roll ID] = " + readBarcodeForm.UserInput.Substring(1) + " AND a.[Master Item No] = " + finishedGoodMasterItemNumber.ToString(), connection1);
										command.ExecuteNonQuery();
									}
									
									while (answer == DialogResult.Yes)
									{
										answer = MessageBox.Show("Do you wish to add more rolls to pallet P" + palletNumber.ToString()+ "?", "", MessageBoxButtons.YesNo);
										if (answer == DialogResult.Yes)
										{
											readBarcodeForm.UserInput = "";
											readBarcodeForm.ShowDialog();
											if (readBarcodeForm.UserInput.Length > 0)
											{
												command = new SqlCommand("SELECT a.[Master Item No], b.[Reference Item No], b.[Description], CASE WHEN b.[Item Type No] = 4 THEN a.[Pallet ID] ELSE 0 END FROM [Roll Table] a INNER JOIN [Inventory Master Table] b on a.[Master Item No] = b.[Master Item No] LEFT JOIN ([Pallet Table] c INNER JOIN [Location Table] d ON c.[Location ID] = d.[Location ID]) ON a.[Pallet ID] = c.[Pallet ID] LEFT JOIN [Location Table] e ON a.[Location ID] = e.[Location ID] WHERE ISNULL(d.[Inventory Available], e.[Inventory Available]) = 1 AND a.[Current LF] > 0 AND b.[Item Type No] in (2, 4) AND a.[Roll ID] = " + readBarcodeForm.UserInput.Substring(1), connection1);
												reader1 = command.ExecuteReader();
												if (reader1.Read())
												{
													if ((int)reader1[0] == finishedGoodMasterItemNumber || (int)reader1[0] == wipMasterItemNumber)
													{
														// Valid FG or Finished WIP Roll
														if ((int)reader1[3] != 0 && (int)reader1[3] != currentPalletNumber)
														{
															MessageBox.Show("YOU WILL NEED TO REPRINT FINISHED GOODS PALLET LABEL P" + reader1[3].ToString() + " IF IT STILL EXISTS SINCE YOU ARE MOVING THIS ROLL OFF OF IT!", "DON'T FORGET TO REPRINT LABEL!");
															currentPalletNumber = (int)reader1[3];
														}
													
														if ((int)reader1[0] == wipMasterItemNumber)
														{
															// Finished WIP Roll
															reader1.Close();
															command = new SqlCommand("UPDATE [Roll Table] SET [Master Item No] = a.[Master Item no], [UOM ID] = b.[UOM ID], [Original Units] = CASE WHEN a.[UOM] = 'LBS' THEN [Original Pounds] WHEN a.[UOM] = 'ROLLS' THEN 1 ELSE FLOOR([dbo].[UOM Conversion]([Current LF], 'LF', a.[Repeat], a.[Stream Width], a.[Linear Feet per Roll], a.[Standard Yield], CASE WHEN a.[UOM]='PCS' THEN 'IMPS' ELSE a.[UOM] END) * a.[Multiplier]) END FROM [Finished Goods Specification Table] a INNER JOIN [UOM Table] b ON a.[UOM] = b.[Description] WHERE [Roll ID] = " + readBarcodeForm.UserInput.Substring(1) + " AND a.[Master Item No] = " + finishedGoodMasterItemNumber.ToString(), connection1);
															command.ExecuteNonQuery();
														}
														else
														{
															reader1.Close();
														}
														
														command = new SqlCommand("INSERT INTO [Move Transaction Table] SELECT '" + StartupForm.UserName + "', GETDATE(), " + readBarcodeForm.UserInput.Substring(1) + ", " + palletNumber.ToString() + ", NULL", connection1);
														command.ExecuteNonQuery();
													}
													else
													{
														MessageBox.Show("Error - roll" + readBarcodeForm.UserInput + " is for job " + reader1[1].ToString() + " - " + reader1[2].ToString() + ", not job " + jobJacketNumber.ToString() + " - " + jobDescription, "Roll belongs to wrong Job");
														reader1.Close();
													}
												}
												else
												{
													reader1.Close();
													MessageBox.Show("Error - finished good or WIP finished roll " + readBarcodeForm.UserInput + " not found", "Roll Not Found");
												}
											}
										}
									}
							
									command = new SqlCommand("SELECT SUM([Current LF] / [Original LF] * [Original Pounds]) FROM [Roll Table] WHERE [Pallet ID] = " + palletNumber, connection1);
									decimal netWeight = (decimal)command.ExecuteScalar();
									GetInputForm frmGetGrossWeight = new GetInputForm("Gross Pallet Weight", "#", (int)Math.Floor(netWeight + 1), (int)Math.Round(netWeight, 0) + 200);
									while (frmGetGrossWeight.UserInput.Length == 0)
									{
										frmGetGrossWeight.ShowDialog();
										if (frmGetGrossWeight.UserInput.Length == 0)
										{
											MessageBox.Show("You MUST enter a valid pallet weight", "No Going Back!");
										}
									}
								
									command = new SqlCommand("UPDATE [Pallet Table] SET [Weight] = " + frmGetGrossWeight.UserInput + " WHERE [Pallet ID] = " + palletNumber.ToString(), connection1);
									frmGetGrossWeight.Dispose();
									command.ExecuteNonQuery();
									command = new SqlCommand("DELETE FROM [" + StartupForm.FGDatabase + "].[dbo].[tblFGRoll] WHERE [RollID] IN (SELECT [Roll ID] from [Roll Table] WHERE [Pallet ID] = " + palletNumber.ToString() + ")", connection1);
									command.ExecuteNonQuery();
									command = new SqlCommand("EXECUTE [dbo].[Create Finished Goods Database Records] " + palletNumber.ToString(), connection1);
									command.ExecuteNonQuery();
									PrintClass.Label("P" + palletNumber.ToString());
								}
								else
								{
									reader1.Close();
								}
							}
							else
							{
								reader1.Close();
								MessageBox.Show("This WIP roll is not a finished WIP roll", "Invalid Roll");
							}
						}
					}
					else
					{
						reader1.Close();
						MessageBox.Show("Error - roll not found", "Invalid Roll");
					}
					
					connection1.Close();
				}
				else // Pallet of Bags
				{
					command = new SqlCommand("SELECT a.[Master Item No], c.[Description], CASE WHEN c.[Description] = 'Finished Goods' THEN a.[Pallet ID] ELSE 0 END, ISNULL(CAST(b.[Reference Item No] AS nvarchar(10)), ''), b.[Description] FROM [Case Table] a INNER JOIN ([Inventory Master Table] b INNER JOIN [Item Type Table] c ON b.[Item Type No] = c.[Item Type No]) ON a.[Master Item No] = b.[Master Item No] LEFT JOIN [Pallet Table] d ON a.[Pallet ID] = d.[Pallet ID], [Location Table] e WHERE ISNULL(d.[Location ID], a.[Location ID]) = e.[Location ID] AND e.[Inventory Available] = 1 AND a.[Bags] > 0 AND a.[Case ID] = " + readBarcodeForm.UserInput.Substring(1), connection1);
					connection1.Open();
					reader1 = command.ExecuteReader();
					if (reader1.Read())
					{
						string itemType = reader1[1].ToString();
						if (itemType != "WIP" && itemType != "Finished Goods")
						{
							MessageBox.Show("Error - this case is " + itemType + " item (" + reader1[3].ToString() + ")" + reader1[4].ToString() + ".  Only finished WIP and Finished Goods can be palletized here.", "Invalid Case Product Type");
						}
						else
						{
							int currentPalletNumber = (int)reader1[2];
							if (itemType == "WIP")
							{
								command = new SqlCommand("SELECT a.[Master Item No], a.[Input Master Item No], a.[Job Jacket No], b.[Description] FROM [Finished Goods Specification Table] a INNER JOIN [Inventory Master Table] b ON a.[Master Item No] = b.[Master Item No] WHERE a.[Input Master Item No] = " + reader1[0].ToString(), connection1);
							}
							else
							{
								command = new SqlCommand("SELECT a.[Master Item No], a.[Input Master Item No], a.[Job Jacket No], b.[Description] FROM [Finished Goods Specification Table] a INNER JOIN [Inventory Master Table] b ON a.[Master Item No] = b.[Master Item No] WHERE a.[Master Item No] = " + reader1[0].ToString(), connection1);
							}
						
							reader1.Close();
							reader1 = command.ExecuteReader();
							if (reader1.Read())
							{
								DialogResult answer = MessageBox.Show("Do you wish to create a finished goods pallet for Job " + reader1[2].ToString() + " - " + reader1[3].ToString() + "?", "Create Pallet Confirmation", MessageBoxButtons.YesNo);
								if (answer == DialogResult.Yes)
								{
									int finishedGoodMasterItemNumber = (int)reader1[0];
									int wipMasterItemNumber = (int)reader1[1];
									int jobJacketNumber = (int)reader1[2];
									string jobDescription = reader1[3].ToString();
									reader1.Close();
									command = new SqlCommand("EXECUTE [Create Pallet Stored Procedure] '" + StartupForm.UserName + "', 191, 0, NULL", connection1);
									reader1 = command.ExecuteReader();
									reader1.Read();
									int palletNumber = (int)reader1[0];
									reader1.Close();
									if (currentPalletNumber != 0)
									{
										MessageBox.Show("YOU WILL NEED TO REPRINT FINISHED GOODS PALLET LABEL P" + currentPalletNumber.ToString() + " IF IT STILL EXISTS SINCE YOU ARE MOVING THIS CASE OFF OF IT!", "DON'T FORGET TO REPRINT LABEL!");
									}
								
									command = new SqlCommand("INSERT INTO [Move Case Transaction Table] SELECT '" + StartupForm.UserName + "', GETDATE(), " + readBarcodeForm.UserInput.Substring(1) + ", " + palletNumber.ToString() + ", NULL", connection1);
									command.ExecuteNonQuery();
									if (itemType == "WIP")
									{
										command = new SqlCommand("UPDATE [Case Table] SET [Master Item No] = " + finishedGoodMasterItemNumber.ToString() + " WHERE [Case ID] = " + readBarcodeForm.UserInput.Substring(1), connection1);
										command.ExecuteNonQuery();
									}
									
									while (answer == DialogResult.Yes)
									{
										answer = MessageBox.Show("Do you wish to add more cases to pallet P" + palletNumber.ToString()+ "?", "", MessageBoxButtons.YesNo);
										if (answer == DialogResult.Yes)
										{
											readBarcodeForm.UserInput = "";
											readBarcodeForm.ShowDialog();
											if (readBarcodeForm.UserInput.Length > 0)
											{
												command = new SqlCommand("SELECT a.[Master Item No], b.[Reference Item No], b.[Description], CASE WHEN b.[Item Type No] = 4 THEN a.[Pallet ID] ELSE 0 END FROM [Case Table] a INNER JOIN [Inventory Master Table] b on a.[Master Item No] = b.[Master Item No] LEFT JOIN ([Pallet Table] c INNER JOIN [Location Table] d ON c.[Location ID] = d.[Location ID]) ON a.[Pallet ID] = c.[Pallet ID] LEFT JOIN [Location Table] e ON a.[Location ID] = e.[Location ID] WHERE ISNULL(d.[Inventory Available], e.[Inventory Available]) = 1 AND a.[Bags] > 0 AND b.[Item Type No] in (2, 4) AND a.[Case ID] = " + readBarcodeForm.UserInput.Substring(1), connection1);
												reader1 = command.ExecuteReader();
												if (reader1.Read())
												{
													if ((int)reader1[0] == finishedGoodMasterItemNumber || (int)reader1[0] == wipMasterItemNumber)
													{
														// Valid FG or Finished WIP Case
														if ((int)reader1[3] != 0 && (int)reader1[3] != currentPalletNumber)
														{
															MessageBox.Show("YOU WILL NEED TO REPRINT FINISHED GOODS PALLET LABEL P" + reader1[3].ToString() + " IF IT STILL EXISTS SINCE YOU ARE MOVING THIS CASE OFF OF IT!", "DON'T FORGET TO REPRINT LABEL!");
															currentPalletNumber = (int)reader1[3];
														}
													
														if ((int)reader1[0] == wipMasterItemNumber)
														{
															// Finished WIP Case
															reader1.Close();
															command = new SqlCommand("UPDATE [Case Table] SET [Master Item No] = " + finishedGoodMasterItemNumber.ToString() + " WHERE [Case ID] = " + readBarcodeForm.UserInput.Substring(1), connection1);
															command.ExecuteNonQuery();
														}
														else
														{
															reader1.Close();
														}
														
														command = new SqlCommand("INSERT INTO [Move Case Transaction Table] SELECT '" + StartupForm.UserName + "', GETDATE(), " + readBarcodeForm.UserInput.Substring(1) + ", " + palletNumber.ToString() + ", NULL", connection1);
														command.ExecuteNonQuery();
													}
													else
													{
														MessageBox.Show("Error - case " + readBarcodeForm.UserInput + " is for job " + reader1[1].ToString() + " - " + reader1[2].ToString() + ", not job " + jobJacketNumber.ToString() + " - " + jobDescription, "Case belongs to wrong Job");
														reader1.Close();
													}
												}
												else
												{
													reader1.Close();
													MessageBox.Show("Error - finished good or WIP finished case " + readBarcodeForm.UserInput + " not found", "Casel Not Found");
												}
											}
										}
									}
							
									command = new SqlCommand("SELECT SUM([Original Pounds]) FROM [Case Table] WHERE [Pallet ID] = " + palletNumber, connection1);
									decimal netWeight = (decimal)command.ExecuteScalar();
									GetInputForm frmGetGrossWeight = new GetInputForm("Gross Pallet Weight", "#", (int)Math.Floor(netWeight + 1), (int)Math.Round(netWeight, 0) + 200);
									while (frmGetGrossWeight.UserInput.Length == 0)
									{
										frmGetGrossWeight.ShowDialog();
										if (frmGetGrossWeight.UserInput.Length == 0)
										{
											MessageBox.Show("You MUST enter a valid pallet weight", "No Going Back!");
										}
									}
								
									command = new SqlCommand("UPDATE [Pallet Table] SET [Weight] = " + frmGetGrossWeight.UserInput + " WHERE [Pallet ID] = " + palletNumber.ToString(), connection1);
									frmGetGrossWeight.Dispose();
									command.ExecuteNonQuery();
									command = new SqlCommand("DELETE FROM [" + StartupForm.FGDatabase + "].[dbo].[tblFGRoll] WHERE [RollID] - 10000000 IN (SELECT [Case ID] from [Case Table] WHERE [Pallet ID] = " + palletNumber.ToString() + ")", connection1);
									command.ExecuteNonQuery();
									command = new SqlCommand("EXECUTE [dbo].[Create Finished Goods Case Database Records] " + palletNumber.ToString(), connection1);
									command.ExecuteNonQuery();
									PrintClass.Label("P" + palletNumber.ToString());
								}
								else
								{
									reader1.Close();
								}
							}
							else
							{
								reader1.Close();
								MessageBox.Show("This WIP case is not a finished WIP roll", "Invalid Roll");
							}
						}
					}
					else
					{
						reader1.Close();
						MessageBox.Show("Error - case not found", "Invalid Roll");
					}
					
					connection1.Close();
				}
			}
			
			readBarcodeForm.Dispose();
        */
		}
		
		void RevertFGPalletToWIPToolStripMenuItemClick(object sender, EventArgs e)
		{
			GetInputForm readBarcodeForm = new GetInputForm("Scan/Input Barcode", "P", 0, 0, true);
			readBarcodeForm.ShowDialog();
			if (readBarcodeForm.UserInput.Length > 0)
			{
				command = new SqlCommand("SELECT TOP 1 a.[Master Item No], b.[Reference Item No], b.[Description], c.[Description], a.[Pallet ID] FROM [Roll Table] a INNER JOIN ([Inventory Master Table] b INNER JOIN [Item Type Table] c ON b.[Item Type No] = c.[Item Type No]) ON a.[Master Item No] = b.[Master Item No] LEFT JOIN [Pallet Table] d ON a.[Pallet ID] = d.[Pallet ID], [Location Table] e WHERE ISNULL(d.[Location ID], a.[Location ID]) = e.[Location ID] AND e.[Inventory Available] = 1 AND a.[Current LF] > 0 AND a.[Pallet ID] = " + readBarcodeForm.UserInput.Substring(1), connection1);
				connection1.Open();
				reader1 = command.ExecuteReader();
				if (reader1.Read())
				{
					if (reader1[3].ToString() == "Finished Goods")
					{
						// Valid rolls to Unfinish
						DialogResult answer = MessageBox.Show("Do you wish to revert pallet " + readBarcodeForm.UserInput + " of Job " + reader1[1].ToString() + " - " + reader1[2] + " to WIP?", "Confirm FG  move to WIP", MessageBoxButtons.YesNo);
                        if (answer == DialogResult.Yes)
                        {
                            command = new SqlCommand("SELECT a.[Input Master Item No], b.[Reference Item No] % 100 FROM [Finished Goods Specification Table] a INNER JOIN [Inventory Master Table] b ON a.[Input Master Item No] = b.[Master Item No] WHERE a.[Master Item No] = " + reader1[0].ToString(), connection2);
                            connection2.Open();
                            reader2 = command.ExecuteReader();
                            reader2.Read();
                            if ((int)reader2[1] != 51)
                            {
                                command = new SqlCommand("UPDATE [Roll Table] SET [Pallet ID] = CASE WHEN " + reader2[1].ToString() + " in ('31', '61') THEN a.[Pallet ID] ELSE NULL END, [Master Item No] = " + reader2[0].ToString() + ", [UOM ID] = 2, [Original Units] = [Original LF], [Location ID] = CASE WHEN " + reader2[1].ToString() + " IN ('31', '61') THEN NULL ELSE a.[Location ID] END FROM [Pallet Table] a WHERE [Roll Table].[Pallet ID] = a.[Pallet ID] AND a.[Pallet ID] = " + readBarcodeForm.UserInput.Substring(1), connection1);
                                reader1.Close();
                                command.ExecuteNonQuery();
                                command = new SqlCommand("DELETE FROM [" + StartupForm.FGDatabase + "].[dbo].[tblFGRoll] WHERE [PltID] = " + readBarcodeForm.UserInput.Substring(1), connection1);
                                command.ExecuteNonQuery();
                                command = new SqlCommand("DELETE FROM [" + StartupForm.FGDatabase + "].[dbo].[tblFGPallet] WHERE [PltID] = " + readBarcodeForm.UserInput.Substring(1), connection1);
                                command.ExecuteNonQuery();
                            }
                            else
                            {
                                reader1.Close();
                                MessageBox.Show("Reverted pallet " + readBarcodeForm.UserInput + " should be bags, not rolls, so it cannot be reverted into rolls.", "Invalid WIP Roll Item");
                            }

                            reader2.Close();
                            connection2.Close();
                        }
                        else
                        {
                            reader1.Close();
                        }
					}
					else
					{
						MessageBox.Show("Pallet " + readBarcodeForm.UserInput + " is the " + reader1[3] + " item  of job " + reader1[1].ToString() + " - " + reader1[2] + ", not a finished good", "Invalid Item Type");
						reader1.Close();
					}
				}
				else
				{
					reader1.Close();
					command = new SqlCommand("SELECT TOP 1 a.[Master Item No], b.[Reference Item No], b.[Description], c.[Description], a.[Pallet ID], e.[Inventory Available] FROM [Case Table] a INNER JOIN ([Inventory Master Table] b INNER JOIN [Item Type Table] c ON b.[Item Type No] = c.[Item Type No]) ON a.[Master Item No] = b.[Master Item No] INNER JOIN ([Pallet Table] d INNER JOIN [Location Table] e ON d.[Location ID] = e.[Location ID]) ON a.[Pallet ID] = d.[Pallet ID] WHERE a.[Bags] > 0 AND a.[Pallet ID] = " + readBarcodeForm.UserInput.Substring(1), connection1);
					reader1 = command.ExecuteReader();
					if (reader1.Read())
					{
						if (reader1[3].ToString() == "Finished Goods")
						{
							// Valid Pallet of Bags to Unfinish
							DialogResult answer = MessageBox.Show("Do you wish to revert pallet " + readBarcodeForm.UserInput + " of Job " + reader1[1].ToString() + " - " + reader1[2] + " to WIP?", "Confirm FG Case move to WIP", MessageBoxButtons.YesNo);
							if (answer == DialogResult.Yes)
							{
								command = new SqlCommand("UPDATE [Case Table] SET [Master Item No] = a.[Input Master Item No] FROM [Finished Goods Specification Table] a WHERE a.[Master Item No] = " + reader1[0].ToString() + " AND [Case Table].[Pallet ID] = " + readBarcodeForm.UserInput.Substring(1), connection1);
								reader1.Close();
								command.ExecuteNonQuery();
								command = new SqlCommand("DELETE FROM [" + StartupForm.FGDatabase + "].[dbo].[tblFGRoll] WHERE [PltID] = " + readBarcodeForm.UserInput.Substring(1), connection1);
								command.ExecuteNonQuery();
								command = new SqlCommand("DELETE FROM [" + StartupForm.FGDatabase + "].[dbo].[tblFGPallet] WHERE [PltID] = " + readBarcodeForm.UserInput.Substring(1), connection1);
								command.ExecuteNonQuery();
							}
						}
						else
						{
							MessageBox.Show("Pallet " + readBarcodeForm.UserInput + " is the " + reader1[3] + " item " + reader1[2] + ", not a finished good", "Invalid Item Type");
							reader1.Close();
						}
					}
					else
					{
						reader1.Close();
						MessageBox.Show("Pallet " + readBarcodeForm.UserInput + " not found", "Not Found");
					}
				}
				
				connection1.Close();
			}
		}
		
		private void UpdateAllocation(string action)
		{
			GetInputForm readJobJacketForm = new GetInputForm("Scan/Input Job Jacket No. + Process Code", "J", 0, 0, false);
			readJobJacketForm.ShowDialog();
			if (readJobJacketForm.UserInput.Length > 0)
			{
				if (readJobJacketForm.UserInput.Length != 9)
				{
					MessageBox.Show("Error - the barcode must be 9 digits long-\"J\"+6-digit JJ #+Operation Tag", "Invalid Barcode");
				}
				else 
				{
					command = new SqlCommand("SELECT a.[Master Item No], a.[Description], CAST(b.[Number of Non-WIP Input Films] AS int) FROM [Inventory Master Table] a INNER JOIN [Number of Non-WIP Input Films by Job View] b ON a.[Master Item No] = b.[Master Item No] WHERE [Item Type No] IN (2, 3) AND [Reference Item No] = " + readJobJacketForm.UserInput.Substring(1), connection1);
					connection1.Open();
					reader1 = command.ExecuteReader();
					if (reader1.Read())
					{
						if ((int)reader1[2] == 0)
						{
							MessageBox.Show("Job " + readJobJacketForm.UserInput + " " + reader1[1].ToString() + " does not use Non-WIP film so there are no film allocations", "Invalid Job");
						}
						else
						{
							int masterItemNumber = (int)reader1[0];
							string itemDescription = reader1[1].ToString();
							int numberFilms = (int)reader1[2];
							reader1.Close();
							command = new SqlCommand("SELECT COUNT(*) as [Number of Open Allocation], SUM(CASE WHEN [Pick Date] IS NULL THEN 0 ELSE 1 END) AS [Item Picked] FROM [Allocation Master Table] WHERE [Release Date] IS NULL AND [Void Date] IS NULL AND [Master Item No] = " + masterItemNumber.ToString(), connection1);
							reader1 = command.ExecuteReader();
							reader1.Read();
							if ((int)reader1[0] == 0)
							{
								MessageBox.Show("There are no open allocations for job " + readJobJacketForm.UserInput, "No Open Allocations");
							}
							else if (action == "Pick")
							{
								if ((int)reader1[0] != numberFilms)
								{
									// There are two films needed but only one allocated
									MessageBox.Show("You must have open allocations for both lamination films in order to pick.", "Allocation Missing");
								}
								else if (reader1[0] == reader1[1])
								{
								 	// All allocations have been picked
								 	if (numberFilms == 1)
								 	{
								 		MessageBox.Show("The allocation for this job has already been picked", "Allocation Picked");
								 	}
								 	else
								 	{
								 		MessageBox.Show("The allocations for this job have already been picked", "Allocations Picked");
								 	}
								 }
								 else
								 {
								 	// Good to go
								 	if (localProduction)
								 	{
								 		command = new SqlCommand("[Create Allocation Pick List Stored Procedure] '" + StartupForm.UserName + "'," + readJobJacketForm.UserInput.Substring(1) + ", 0", connection2);
								 	}
								 	else
								 	{
								 		command = new SqlCommand("[Create Starpak Allocation Pick List Stored Procedure] '" + StartupForm.UserName + "'," + readJobJacketForm.UserInput.Substring(1) + ", 0", connection2);
								 	}
								 	connection2.Open();
								 	string pickResult = command.ExecuteScalar().ToString();
								 	connection2.Close();
								 	if (string.IsNullOrEmpty(pickResult))
								 	{
								 		// Successful PIck
							 			PickListReportForm pickListForm = new PickListReportForm(readJobJacketForm.UserInput.Substring(1));
							 			pickListForm.ShowDialog();
							 			pickListForm.Dispose();
								 	}
								 	else if (pickResult.Substring(pickResult.Length - 1) == "?")
								 	{
								 		DialogResult answer = MessageBox.Show(pickResult, "Still Pick?", MessageBoxButtons.YesNo);
								 		if (answer == DialogResult.Yes)
								 		{
								 			// Successful Pick
								 			if (localProduction)
								 			{
								 				command = new SqlCommand("[Create Allocation Pick List Stored Procedure] '" + StartupForm.UserName + "'," + readJobJacketForm.UserInput.Substring(1) + ", 1", connection2);
								 			}
								 			else
								 			{
								 				command = new SqlCommand("[Create Starpak Allocation Pick List Stored Procedure] '" + StartupForm.UserName + "'," + readJobJacketForm.UserInput.Substring(1) + ", 1", connection2);
								 			}
							 				connection2.Open();
										 	pickResult = command.ExecuteScalar().ToString();
										 	connection2.Close();
										 	if (string.IsNullOrEmpty(pickResult))
										 	{		
								 				PickListReportForm pickListForm = new PickListReportForm(readJobJacketForm.UserInput.Substring(1));
							 					pickListForm.ShowDialog();
							 					pickListForm.Dispose();
									 		}
								 		}
								 	}
								 	else
								 	{
								 		MessageBox.Show(pickResult, "Pick Failed");
								 	}
								 }
							}
							else if (action == "AutoReplace" || action == "ManualReplace")
							{
								int filmNumber = 0;
								if ((int)reader1[1] == 2)
								{
									// Must choose which web film to replace
									OptionsForm pickFilmForm = new OptionsForm("Pick Film for Job " + readJobJacketForm.UserInput, false, true);
									pickFilmForm.AddOption("1");
		   	                	    pickFilmForm.AddOption("2");
									pickFilmForm.ShowDialog();
									if (pickFilmForm.Option != "Abort")
									{
										filmNumber = int.Parse(pickFilmForm.Option) - 1;
									}
									else
									{
										filmNumber = -1;
									}
									
									pickFilmForm.Dispose();
								}
								
								reader1.Close();
								if (filmNumber != -1)
								{
									//Get Roll to Replace
									GetInputForm getRollNumberForm = new GetInputForm("Picked Roll ID", "R", 0, 0, true);
									getRollNumberForm.ShowDialog();
									if (getRollNumberForm.UserInput.Length > 0)
									{
										command = new SqlCommand("SELECT b.[Allocation ID], a.[Priority], c.[Cust Supplied], c.[Sample], d.[Master Item No], d.[Width], a.[Allocated LF] FROM [Allocation Pick Table] a INNER JOIN [Allocation Master Table] b ON a.[Allocation ID] = b.[Allocation ID] INNER JOIN [Allocation Reservation Table] c ON a.[Allocation ID] = c.[Allocation ID] AND a.[Priority] = c.[Priority] INNER JOIN [Roll Table] d ON a.[Roll ID] = d.[Roll ID] WHERE b.[Void Date] IS NULL AND b.[Release Date] IS NULL AND b.[Pick Date] IS NOT NULL AND b.[Master Item No] = " + masterItemNumber.ToString() + " AND ISNULL(b.[Lamination Film ID], 0) = " + filmNumber.ToString() + " AND a.[Roll ID] = " + getRollNumberForm.UserInput.Substring(1), connection1);
										reader1 = command.ExecuteReader();
										if (reader1.Read())
										{
											// Roll Picked
											if (action == "AutoReplace")
											{
												command = new SqlCommand("[Replace Pick List Roll Stored Procedure] " + getRollNumberForm.UserInput.Substring(1) + ", " + reader1[0].ToString() + ", " + reader1[1].ToString() + ", " + reader1[2].ToString() + ", " + reader1[3].ToString() + ", " + reader1[4].ToString()+ ", " + reader1[5].ToString() + ", " + reader1[6].ToString(), connection1);
												reader1.Close();
												string replaceResult = command.ExecuteScalar().ToString();
								 		 		if (string.IsNullOrEmpty(replaceResult))
								 				{
								 					// Successful Exchange
								 					PickListReportForm pickListForm = new PickListReportForm(readJobJacketForm.UserInput.Substring(1));
								 					pickListForm.ShowDialog();
								 					pickListForm.Dispose();
									 			}
									 		 	else
									 		 	{
									 		 		MessageBox.Show(replaceResult, "Replacement Failed");
									 		 	}
											}
											else
											{
												reader1.Close();
												command = new SqlCommand("SELECT a.[Allocation ID], a.[Priority], c.[Width], d.[Description], CAST(ROUND(a.[Allocated LF], 0) AS int) FROM [Allocation Pick Table] a INNER JOIN [Allocation Master Table] b ON a.[Allocation ID] = b.[Allocation ID] INNER JOIN ([Roll Table] c INNER JOIN [Inventory Master Table] d ON c.[Master Item No] = d.[Master Item No]) ON a.[Roll ID] = c.[Roll ID] WHERE b.[Pick Date] IS NOT NULL AND b.[Release Date] IS NULL AND b.[Void Date] IS NULL AND b.[Master Item No] = " + masterItemNumber + " AND ISNULL(b.[Lamination Film ID], 0) = " + filmNumber + " AND a.[Roll ID] = " + getRollNumberForm.UserInput.Substring(1), connection1);
												reader1 = command.ExecuteReader();
												if (reader1.Read())
												{
													string rollToReplace = getRollNumberForm.UserInput;
													getRollNumberForm.NewTitle = "Roll ID to Add";
													getRollNumberForm.UserInput = string.Empty;
													getRollNumberForm.ShowDialog();
													if (getRollNumberForm.UserInput.Length > 0)
													{
														command = new SqlCommand("SELECT a.[Width], b.[Description], CAST(ROUND(a.[Current LF], 0) AS int), CAST(ROUND(a.[Current LF] - ISNULL(c.[Allocated LF], 0), 0) AS int) AS [Available LF] FROM [Roll Table] a INNER JOIN [Inventory Master Table] b ON a.[Master Item No] = b.[Master Item No] LEFT JOIN [Active Total Picked by Roll ID View] c ON a.[Roll ID] = c.[Roll ID] LEFT JOIN [Pallet Table] d ON a.[Pallet ID] = d.[Pallet ID], [Location Table] e WHERE ISNULL(d.[Location ID], a.[Location ID]) = e.[Location ID] AND e.[Inventory Available] = 1 AND a.[Roll ID] = " + getRollNumberForm.UserInput.Substring(1), connection2);
														connection2.Open();
														reader2 = command.ExecuteReader();
														if (reader2.Read())
														{
															if ((int)reader2[2] <= 0)
															{
																reader2.Close();
																MessageBox.Show("Error - roll " + getRollNumberForm.UserInput + " has no inventory", "Roll has no Inventory");
															}
															else if ((int)reader2[3] <= 0)
															{
																reader2.Close();
																MessageBox.Show("Error - roll " + getRollNumberForm.UserInput + " is completely allocated", "No Inventory Available");
															}
															else if ((int)reader2[3] < (int)reader1[4])
															{
																answer = MessageBox.Show("Roll " + getRollNumberForm.UserInput + " of " + ((decimal)reader2[0]).ToString("N4") + "\" " + reader2[1].ToString() + " has only " + ((int)reader2[2]).ToString("N0") + " LF available to replace the " + ((int)reader1[4]).ToString("N4") + " LF of " + rollToReplace + " " + ((decimal)reader1[2]).ToString("N4") + "\" " + reader1[3].ToString() + ". Do you still wish to replace?", "Allocated Anyway?", MessageBoxButtons.YesNo);
																if (answer == DialogResult.Yes)
																{
																	command = new SqlCommand("INSERT INTO [Allocation Pick Replacement Transaction Table] SELECT '" + StartupForm.UserName + "', GETDATE(), " + reader1[0].ToString() + ", " + reader1[1].ToString() + ", " + rollToReplace.Substring(1) + ", " + reader1[4].ToString() + ", " + getRollNumberForm.UserInput.Substring(1) + ", " + reader2[2].ToString(), connection2);
																	reader2.Close();
																	command.ExecuteNonQuery();
																}
															}
															else
															{
																reader2.Close();
																command = new SqlCommand("INSERT INTO [Allocation Pick Replacement Transaction Table] SELECT '" + StartupForm.UserName + "', GETDATE(), " + reader1[0].ToString() + ", " + reader1[1].ToString() + ", " + rollToReplace.Substring(1) + ", " + reader1[4].ToString() + ", " + getRollNumberForm.UserInput.Substring(1) + ", " + reader1[4].ToString(), connection2);
																command.ExecuteNonQuery();
															}
														}
														else
														{
															reader2.Close();
															MessageBox.Show("Error - Roll " + getRollNumberForm.UserInput + " not found", "Invalid Roll ID");
														}

														connection2.Close();
													}
												}
												else
												{
													MessageBox.Show("Error - Roll " + getRollNumberForm.UserInput + " is not allocated to job " + readJobJacketForm.UserInput, "Invalid Roll ID");
												}
											}
										}
										else
										{
											reader1.Close();
											if (numberFilms == 1)
											{
												MessageBox.Show("Error - Roll " + getRollNumberForm.UserInput + " is not allocated to job " + readJobJacketForm.UserInput + " - " + itemDescription, "Invalid Roll");
											}
											else
											{
												MessageBox.Show("Error - Roll " + getRollNumberForm.UserInput + " is not allocated to job " + readJobJacketForm.UserInput + " - " + itemDescription + " film " + (filmNumber + 1).ToString(), "Invalid Roll");
											}
										}
									}
				
									getRollNumberForm.Dispose();
								}
							}
							else
							{
								reader1.Close();
								command = new SqlCommand("SELECT [Allocation ID], [Picked By], [Pick Date] FROM [Allocation Master Table] WHERE [Release Date] IS NULL AND [Void Date] IS NULL AND [Master Item No] = " + masterItemNumber.ToString() + " ORDER BY [Pick Date], [Allocation ID]", connection1);
								reader1 = command.ExecuteReader();
								reader1.Read();
								DialogResult answer = DialogResult.No;
								if (action == "Release" && string.IsNullOrEmpty(reader1[1].ToString()))
								{
									MessageBox.Show("Inventory has not been picked for this job.  Void the allocation if you wish to unallocate.", "Invalid Release");
								}
								else
								{
									if (action == "Release")
									{
										answer = MessageBox.Show("Do You wish to return all unused and partial rolls allocated to job " + readJobJacketForm.UserInput + " - " + itemDescription + "?", "Return Rolls", MessageBoxButtons.YesNo);
									}
									else
									{
										answer = MessageBox.Show("Do You wish to void the allocation for job " + readJobJacketForm.UserInput + " - " + itemDescription + "?", "Void Allocation", MessageBoxButtons.YesNo);
									}
								}
									
								if (answer == DialogResult.Yes)
								{
									connection2.Open();
									do
									{
										if (action == "Release")
										{
											command = new SqlCommand("UPDATE [Allocation Master Table] SET [Released By] =  '" + StartupForm.UserName + "', [Release Date] = GETDATE() WHERE [Allocation ID] = " + reader1[0].ToString(), connection2);
										}
										else
										{
											//action = Void
											command = new SqlCommand("UPDATE [Allocation Master Table] SET [Voided By] =  '" + StartupForm.UserName + "', [Void Date] = GETDATE() WHERE [Allocation ID] = " + reader1[0].ToString(), connection2);
										}

										command.ExecuteNonQuery();
									}
									while (reader1.Read());
									
									connection2.Close();
								}
							}
						}
					}
					else
					{
						MessageBox.Show("Error - Job " + readJobJacketForm.UserInput + " not found", "Job Not Found");
					}
					
					reader1.Close();
					connection1.Close();
				}
			}
			
			readJobJacketForm.Close();
            
		}
		
		void ReturnToolStripMenuItemClick(object sender, EventArgs e)
		{
			UpdateAllocation("Release");
		}
		
		void VoidIssueToolStripMenuItemClick(object sender, EventArgs e)
		{
			UpdateAllocation("Void");
		}
		
		void PickRollsToolStripMenuItemClick(object sender, EventArgs e)
		{
			localProduction = true;
			UpdateAllocation("Pick");
		}
		
		void IssuePickListForStarpakToolStripMenuItemClick(object sender, EventArgs e)
		{
			localProduction = false;
			UpdateAllocation("Pick");
		}
		
		void AutoReplaceFilmPickListRollToolStripMenuItemClick(object sender, EventArgs e)
		{
			UpdateAllocation("AutoReplace");
		}
		
		void ManualReplacePickListRollToolStripMenuItemClick(object sender, EventArgs e)
		{
			UpdateAllocation("ManualReplace");
		}
		
		void PrinterToolStripMenuItemClick(object sender, EventArgs e)
		{
			OptionsForm choosePrinterForm = new OptionsForm("Choose Printer", true, true);
			foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
	        {
	          	choosePrinterForm.AddOption(printer);
	        }
			
			choosePrinterForm.ShowDialog();
			if (choosePrinterForm.Option != "Abort")
			{
				myPrinters.SetDefaultPrinter(choosePrinterForm.Option);
				lblMachineOrPrinterInformation.Text = "Current Printer: " + choosePrinterForm.Option;
				DialogResult answer = MessageBox.Show("Is " + choosePrinterForm.Option + " a label printer?", "Label Printer", MessageBoxButtons.YesNo);
				if (answer == DialogResult.Yes)
				{
					labelPrinter = true;
				}
				else
				{
					labelPrinter = false;
				}
			}
			
			choosePrinterForm.Dispose();
		}
		
		private string GetDefaultPrinter()
		{
    		PrinterSettings settings = new PrinterSettings();
   			 foreach (string printer in PrinterSettings.InstalledPrinters)
   			 {
   		     	settings.PrinterName = printer;
    		   	if (settings.IsDefaultPrinter)
    		   	{
            		return printer;
    			}
   			 }
    		
   			 return string.Empty;
		}
		
		private static class myPrinters
	    {
	        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
	        public static extern bool SetDefaultPrinter(string Name);
	    }
		
		private void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			myPrinters.SetDefaultPrinter(originalDefaultPrinter);
		}
		
		private void ProductionEditStripMenuItemClick(object sender, EventArgs e)
		{
			productionEditForm prodEditForm = new productionEditForm();
			prodEditForm.ShowDialog();
			prodEditForm.Dispose();
		}
		
		private void PrintPickListToolStripMenuItemClick(object sender, EventArgs e)
		{
			GetInputForm readJobJacketForm = new GetInputForm("Scan/Input Job Jacket No. + Process Code", "J", 0, 0, true);
			readJobJacketForm.ShowDialog();
			if (readJobJacketForm.UserInput.Length > 0)
			{
				if (readJobJacketForm.UserInput.Length != 9)
				{
					MessageBox.Show("Error - the barcode must be 9 digits long-\"J\"+6-digit JJ #+Operation Tag", "Invalid Barcode");	
				}
				else
				{
					PickListReportForm pickListForm = new PickListReportForm(readJobJacketForm.UserInput.Substring(1));
					pickListForm.ShowDialog();
					pickListForm.Dispose();
				}
			}
			
			readJobJacketForm.Dispose();
		}
		
		private void MovePallet(string palletID)
		{
			GetInputForm frmReadLocation = new GetInputForm("Scan/Input Location", "L", 0, 0, false);
			frmReadLocation.ShowDialog();
			if (frmReadLocation.UserInput.Length > 0)
			{
				string locationDescription = null;
				bool suppressMessage = false;
				if (frmReadLocation.UserInput.Substring(0, 1) == "L")
				{
					command = new SqlCommand("select [Description] from [Location Table] where [Location ID]=" + frmReadLocation.UserInput.Substring(1), connection1);
					locationDescription = (string)command.ExecuteScalar();
				}
				else
				{
					MessageBox.Show("Error - you cannot move a pallet into another pallet", "Invalid Move");
					suppressMessage = true;
				}
											
				if (!string.IsNullOrEmpty(locationDescription))
				{
					answer = MessageBox.Show("Move Pallet " + palletID + " to " + locationDescription + "?", "Confirm Move", MessageBoxButtons.YesNo);
					if (answer == DialogResult.Yes)
					{
						command = new SqlCommand("insert into [Move Transaction Table] select '" + StartupForm.UserName + "',GETDATE(),NULL," + palletID + "," + frmReadLocation.UserInput.Substring(1), connection1);
						command.ExecuteNonQuery();
					}
					else
					{
						MessageBox.Show("Move aborted");
					}
				}
				else if (!suppressMessage)
				{
					MessageBox.Show("Error - location " + frmReadLocation.UserInput + " does not exist", "Invalid Location");
				}
			}
			
			frmReadLocation.Dispose();
		}

        private void CreatetoolStripMenuItemClick(object sender, EventArgs e)
        {
            GetInputForm readJobJacketForm = new GetInputForm("Scan/Input Job Jacket No. + Process Code", "J", 0, 0, true);
            readJobJacketForm.ShowDialog();
            if (readJobJacketForm.UserInput.Length > 0)
            {
                if (readJobJacketForm.UserInput.Length != 9)
                {
                    MessageBox.Show("Error - the barcode must be 9 digits long-\"J\"+6-digit JJ #+Operation Tag", "Invalid Barcode");
                }
                else
                {
                    command = new SqlCommand("SELECT [Master Item No], [Description] FROM [Inventory Master Table] WHERE [Item Type No] IN (2, 3) AND [Reference Item No] = " + readJobJacketForm.UserInput.Substring(1), connection1);
                    connection1.Open();
                    reader1 = command.ExecuteReader();
                    if (reader1.Read())
                    {
                        int masterItemNo = (int)reader1[0];
                        string itemDescription = reader1[1].ToString();
                        reader1.Close();
                        if (readJobJacketForm.UserInput.EndsWith("51"))
                        {
                            string inputCaseID = "";
                            UnitInformationForm newCaseBagsAndPoundsForm = new UnitInformationForm("New Case Bags and Pounds", itemDescription, 0, true);
                            newCaseBagsAndPoundsForm.UnitName = "Bags";
                            DialogResult answer = MessageBox.Show("Do you have an input Case ID to enter (input Case will be removed from inventory)?", "Input Case", MessageBoxButtons.YesNo);
                            if (answer == DialogResult.Yes)
                            {
                                GetInputForm getCaseNumberForm = new GetInputForm("Input Case ID", "C", 0, 0, true);
                                getCaseNumberForm.ShowDialog();
                                if (getCaseNumberForm.UserInput.Length > 0)
                                {
                                    command = new SqlCommand("SELECT a.[Master Item No], b.[Reference Item No], b.[Description], CAST(b.[Item Type No] AS int), CAST(ROUND(a.[Bags], 0) AS int), a.[Original Pounds] FROM [Case Table] a INNER JOIN [Inventory Master Table] b ON a.[Master Item No] = b.[Master Item No] WHERE a.[Case ID]=" + getCaseNumberForm.UserInput.Substring(1), connection1);
                                    reader1 = command.ExecuteReader();
                                    if (reader1.Read())
                                    {
                                        if ((int)reader1[3] == 4)
                                        {
                                            answer = MessageBox.Show("Do you wish to create a case of " + readJobJacketForm.UserInput + " " + itemDescription + " from finished goods case " + getCaseNumberForm.UserInput + " of J" + reader1[1].ToString() + " " + reader1[2].ToString() + "?", "Create Case?", MessageBoxButtons.YesNo);
                                        }
                                        else
                                        {
                                            answer = MessageBox.Show("Do you wish to create a case of " + readJobJacketForm.UserInput + " " + itemDescription + " from WIP case " + getCaseNumberForm.UserInput + " of J" + reader1[1].ToString() + " " + reader1[2].ToString() + "?", "Create Case?", MessageBoxButtons.YesNo);
                                        }

                                        if (answer == DialogResult.Yes)
                                        {
                                            newCaseBagsAndPoundsForm.Units = (int)reader1[4];
                                            newCaseBagsAndPoundsForm.Pounds = (decimal)reader1[5];
                                            inputCaseID = getCaseNumberForm.UserInput.Substring(1);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Error - Case " + getCaseNumberForm.UserInput + " not found", "Invalid Case");
                                        answer = DialogResult.No;
                                    }

                                    reader1.Close();
                                }

                                getCaseNumberForm.Dispose();
                            }
                            else
                            {
                                answer = MessageBox.Show("Do you wish to create a case of " + readJobJacketForm.UserInput + " " + itemDescription + "?", "Create Case", MessageBoxButtons.YesNo);
                            }

                            if (answer == DialogResult.Yes)
                            {
                                newCaseBagsAndPoundsForm.ShowDialog();
                                if (newCaseBagsAndPoundsForm.Units > 0)
                                {
                                    OptionsForm createCaseReasonForm = new OptionsForm("Reason for Creation of Case", false, true);
                                    command = new SqlCommand("SELECT [Description] FROM [Created Case Reason Table] ORDER BY [Created Case Reason ID]", connection1);
                                    reader1 = command.ExecuteReader();
                                    while (reader1.Read())
                                    {
                                        createCaseReasonForm.AddOption(reader1[0].ToString());
                                    }

                                    reader1.Close();
                                    createCaseReasonForm.ShowDialog();
                                    if (createCaseReasonForm.Option != "Abort")
                                    {
                                        int numberOfCasesToCreate = 1;
                                        if (string.IsNullOrEmpty(inputCaseID))
                                        {
                                            GetInputForm numberOfCasesToCreateForm = new GetInputForm("Number or Cases", "#", 1, 200, false);
                                            numberOfCasesToCreateForm.UserInput = "1";
                                            numberOfCasesToCreateForm.ShowDialog();
                                            if (numberOfCasesToCreateForm.UserInput.Length > 0)
                                            {
                                                numberOfCasesToCreate = int.Parse(numberOfCasesToCreateForm.UserInput, NumberStyles.Number);
                                            }
                                            else
                                            {
                                                numberOfCasesToCreate = 0;
                                            }

                                            numberOfCasesToCreateForm.Dispose();
                                        }

                                        for (int i = 1; i <= numberOfCasesToCreate; i++)
                                        {
                                            // Create Case
                                            if (string.IsNullOrEmpty(inputCaseID))
                                            {
                                                command = new SqlCommand("EXECUTE [Add Created Case Stored Procedure] " + masterItemNo.ToString() + ", '" + StartupForm.UserName + "', " + newCaseBagsAndPoundsForm.Units.ToString() + ", " + newCaseBagsAndPoundsForm.Pounds.ToString() + ", NULL, '" + createCaseReasonForm.Option + "', '" + newCaseBagsAndPoundsForm.Notes.Replace("'", "''") + "'", connection1);
                                            }
                                            else
                                            {
                                                command = new SqlCommand("EXECUTE [Add Created Case Stored Procedure] " + masterItemNo.ToString() + ", '" + StartupForm.UserName + "', " + newCaseBagsAndPoundsForm.Units.ToString() + ", " + newCaseBagsAndPoundsForm.Pounds.ToString() + ", " + inputCaseID + ", '" + createCaseReasonForm.Option + "', '" + newCaseBagsAndPoundsForm.Notes.Replace("'", "''") + "'", connection1);
                                            }

                                            int caseNumber = (int)command.ExecuteScalar();
                                            PrintClass.Label("C" + caseNumber.ToString());
                                        }
                                    }

                                    createCaseReasonForm.Dispose();
                                }
                            }

                            newCaseBagsAndPoundsForm.Dispose();
                        }
                        else
                        {
                            string inputRollID = "";
                            decimal rollWidth = 0;
                            UnitInformationForm newRollFootageAndPoundsForm = new UnitInformationForm("New Roll Footage and Pounds", itemDescription, 0, true);
                            DialogResult answer = MessageBox.Show("Do you have an input roll ID to enter (input roll will be removed from inventory)?", "Input Roll", MessageBoxButtons.YesNo);
                            if (answer == DialogResult.Yes)
                            {
                                GetInputForm getRollNumberForm = new GetInputForm("Input Roll ID", "R", 0, 0, true);
                                getRollNumberForm.ShowDialog();
                                if (getRollNumberForm.UserInput.Length > 0)
                                {
                                    command = new SqlCommand("SELECT a.[Master Item No], b.[Reference Item No], b.[Description], CAST(b.[Item Type No] AS int), CAST(ROUND(a.[Current LF], 0) AS int), a.[Original Pounds] * a.[Current LF] / a.[Original LF], a.[Width] FROM [Roll Table] a INNER JOIN [Inventory Master Table] b ON a.[Master Item No] = b.[Master Item No] WHERE a.[Roll ID]=" + getRollNumberForm.UserInput.Substring(1), connection1);
                                    reader1 = command.ExecuteReader();
                                    if (reader1.Read())
                                    {
                                        if (int.Parse(readJobJacketForm.UserInput.Substring(1), NumberStyles.Number) == (int)reader1[1])
                                        {
                                            if ((int)reader1[3] == 1)
                                            {
                                                answer = MessageBox.Show("Do you wish to create a roll of " + readJobJacketForm.UserInput + " " + itemDescription + " from film roll " + getRollNumberForm.UserInput + " of " + reader1[2].ToString() + "?", "Create Roll?", MessageBoxButtons.YesNo);
                                            }
                                            else if ((int)reader1[3] == 4)
                                            {
                                                answer = MessageBox.Show("Do you wish to create a roll of " + readJobJacketForm.UserInput + " " + itemDescription + " from finished goods roll " + getRollNumberForm.UserInput + " of J" + reader1[1].ToString() + " " + reader1[2].ToString() + "?", "Create Roll?", MessageBoxButtons.YesNo);
                                            }
                                            else
                                            {
                                                answer = MessageBox.Show("Do you wish to create a roll of " + readJobJacketForm.UserInput + " " + itemDescription + " from WIP roll " + getRollNumberForm.UserInput + " of J" + reader1[1].ToString() + " " + reader1[2].ToString() + "?", "Create Roll?", MessageBoxButtons.YesNo);
                                            }
                                        }
                                        else
                                        {
                                            if ((int)reader1[3] == 1)
                                            {
                                                answer = MessageBox.Show("Do you wish to create a roll of " + readJobJacketForm.UserInput + " " + itemDescription + " from film roll " + getRollNumberForm.UserInput + " of DIFFERENT FILM " + reader1[2].ToString() + "?", "Create Roll (Authorization Required)?", MessageBoxButtons.YesNo);
                                            }
                                            else if ((int)reader1[3] == 4)
                                            {
                                                answer = MessageBox.Show("Do you wish to create a roll of " + readJobJacketForm.UserInput + " " + itemDescription + " from finished goods roll " + getRollNumberForm.UserInput + " of DIFFERENT JOB J" + reader1[1].ToString() + " " + reader1[2].ToString() + "?", "Create Roll (Authorization Required)?", MessageBoxButtons.YesNo);
                                            }
                                            else
                                            {
                                                answer = MessageBox.Show("Do you wish to create a roll of " + readJobJacketForm.UserInput + " " + itemDescription + " from WIP roll " + getRollNumberForm.UserInput + " of DIFFERENT JOB J" + reader1[1].ToString() + " " + reader1[2].ToString() + "?", "Create Roll (Authorization Required)?", MessageBoxButtons.YesNo);
                                            }

                                            if (answer == DialogResult.Yes && !userCanOverride)
                                            {
                                                string authorizedBy = string.Empty;
                                                bool authorized = ModulesClass.Authorized(false, ref authorizedBy);
                                                if (authorized)
                                                {
                                                    if ((int)reader1[3] == 1)
                                                    {
                                                        ModulesClass.SendEmail(2, "Film Roll Created from Roll of a different Film", "A roll of " + readJobJacketForm.UserInput + " " + itemDescription + " from film roll " + getRollNumberForm.UserInput + " of DIFFERENT FILM " + reader1[2].ToString() + " was created. The override was authorized by " + authorizedBy + ".");
                                                    }
                                                    else if ((int)reader1[3] == 4)
                                                    {
                                                        ModulesClass.SendEmail(2, "Finished Good Roll Created from Roll of a different Job", "A roll of " + readJobJacketForm.UserInput + " " + itemDescription + " from finished goods roll " + getRollNumberForm.UserInput + " of DIFFERENT JOB J" + reader1[1].ToString() + " " + reader1[2].ToString() + " was created. The override was authorized by " + authorizedBy + ".");
                                                    }
                                                    else
                                                    {
                                                        ModulesClass.SendEmail(2, "WIP Roll Created from Roll of a different Job", "A roll of " + readJobJacketForm.UserInput + " " + itemDescription + " from WIP roll " + getRollNumberForm.UserInput + " of DIFFERENT JOB J" + reader1[1].ToString() + " " + reader1[2].ToString() + " was created. The override was authorized by " + authorizedBy + ".");
                                                    }
                                                }
                                                else
                                                {
                                                    answer = DialogResult.No;
                                                }
                                            }
                                        }

                                        if (answer == DialogResult.Yes)
                                        {
                                            newRollFootageAndPoundsForm.Units = (int)reader1[4];
                                            newRollFootageAndPoundsForm.Pounds = (decimal)reader1[5];
                                            rollWidth = (decimal)reader1[6];
                                            inputRollID = getRollNumberForm.UserInput.Substring(1);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Error - Roll " + getRollNumberForm.UserInput + " not found", "Invalid Roll");
                                        answer = DialogResult.No;
                                    }

                                    reader1.Close();
                                }

                                getRollNumberForm.Dispose();
                            }
                            else
                            {
                                answer = MessageBox.Show("Do you wish to creaete a roll of " + readJobJacketForm.UserInput + " " + itemDescription + "?", "Create Roll", MessageBoxButtons.YesNo);
                            }

                            if (answer == DialogResult.Yes)
                            {
                                newRollFootageAndPoundsForm.ShowDialog();
                                if (newRollFootageAndPoundsForm.Units > 0)
                                {
                                    GetDecimalInputForm getRollWidthForm = new GetDecimalInputForm(4);
                                    getRollWidthForm.Description = "Enter Roll Width";
                                    getRollWidthForm.UserInput = rollWidth.ToString();
                                    do
                                    {
                                        if (getRollWidthForm.UserInput.Length > 0 && decimal.Parse(getRollWidthForm.UserInput, NumberStyles.Number) > 60)
                                        {
                                            MessageBox.Show("You cannot create a roll width greater than 60 inches", "Roll Too Wide");
                                        }

                                        getRollWidthForm.ShowDialog();
                                    }
                                    while (getRollWidthForm.UserInput.Length > 0 && decimal.Parse(getRollWidthForm.UserInput, NumberStyles.Number) > 60);

                                    if (getRollWidthForm.UserInput.Length > 0)
                                    {
                                        OptionsForm createRollReasonForm = new OptionsForm("Reason for Creation of Roll", false, true);
                                        command = new SqlCommand("SELECT [Description] FROM [Created Roll Reason Table] ORDER BY [Created Roll Reason ID]", connection1);
                                        reader1 = command.ExecuteReader();
                                        while (reader1.Read())
                                        {
                                            createRollReasonForm.AddOption(reader1[0].ToString());
                                        }

                                        reader1.Close();
                                        createRollReasonForm.ShowDialog();
                                        if (createRollReasonForm.Option != "Abort")
                                        {
                                            int numberOfRollsToCreate = 1;
                                            if (string.IsNullOrEmpty(inputRollID))
                                            {
                                                GetInputForm numberOfRollsToCreateForm = new GetInputForm("Number or Rolls", "#", 1, 50, false);
                                                numberOfRollsToCreateForm.UserInput = "1";
                                                numberOfRollsToCreateForm.ShowDialog();
                                                if (numberOfRollsToCreateForm.UserInput.Length > 0)
                                                {
                                                    numberOfRollsToCreate = int.Parse(numberOfRollsToCreateForm.UserInput, NumberStyles.Number);
                                                }
                                                else
                                                {
                                                    numberOfRollsToCreate = 0;
                                                }

                                                numberOfRollsToCreateForm.Dispose();
                                            }

                                            for (int i = 1; i <= numberOfRollsToCreate; i++)
                                            {
                                                // Create Roll
                                                if (string.IsNullOrEmpty(inputRollID))
                                                {
                                                    command = new SqlCommand("EXECUTE [Add Created Roll Stored Procedure] " + masterItemNo.ToString() + ", '" + StartupForm.UserName + "', " + newRollFootageAndPoundsForm.Units.ToString() + ", " + newRollFootageAndPoundsForm.Pounds.ToString() + ", " + getRollWidthForm.UserInput + ", NULL, '" + createRollReasonForm.Option + "', '" + newRollFootageAndPoundsForm.Notes.Replace("'", "''") + "'", connection1);
                                                }
                                                else
                                                {
                                                    command = new SqlCommand("EXECUTE [Add Created Roll Stored Procedure] " + masterItemNo.ToString() + ", '" + StartupForm.UserName + "', " + newRollFootageAndPoundsForm.Units.ToString() + ", " + newRollFootageAndPoundsForm.Pounds.ToString() + ", " + getRollWidthForm.UserInput + ", " + inputRollID + ", '" + createRollReasonForm.Option + "', '" + newRollFootageAndPoundsForm.Notes.Replace("'", "''") + "'", connection1);
                                                }

                                                int rollNumber = (int)command.ExecuteScalar();
                                                PrintClass.Label("R" + rollNumber.ToString());
                                            }
                                        }

                                        createRollReasonForm.Dispose();
                                    }

                                    getRollWidthForm.Dispose();
                                }
                            }

                            newRollFootageAndPoundsForm.Dispose();
                        }
                    }
                    else
                    {
                        reader1.Close();
                        MessageBox.Show("Error -Job " + readJobJacketForm.UserInput + " not found", "Invalid Job");
                    }

                    connection1.Close();
                }
            }

            readJobJacketForm.Dispose();
        }

        private void createNewNonProductionFGPalletToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetInputForm readJobJacketForm = new GetInputForm("Scan/Input Non-Production Job Jacket No.", "J", 0, 0, true);
            readJobJacketForm.ShowDialog();
            if (readJobJacketForm.UserInput.Length > 0)
            {
                if (readJobJacketForm.UserInput.Length != 7)
                {
                    MessageBox.Show("Error - the barcode must be 7 digits long-\"J\"+6-digit JJ #", "Invalid Barcode");
                }
                else
                {
                    command = new SqlCommand("SELECT a.[Input Master Item No], a.[Stream Width], b.[Description], b.[Item Type No], a.[Master Item No], c.[Description], a.[Job Jacket No] FROM [Finished Goods Specification Table] a INNER JOIN [Inventory Master Table] b ON a.[Input Master Item No] = b.[Master Item No]  INNER JOIN [Inventory Master Table] c ON a.[Master Item No] = c.[Master Item No] WHERE a.[Job Jacket No] = " + int.Parse(readJobJacketForm.UserInput.Substring(1), NumberStyles.Number).ToString(), connection1);
                    connection1.Open();
                    reader1 = command.ExecuteReader();
                    if (reader1.Read())
                    {
                        if (reader1[3].ToString() != "1")
                        {
                            reader1.Close();
                            connection1.Close();
                            MessageBox.Show("Error - Job " + readJobJacketForm.UserInput + "'s input film is not raw film", "Invalid Non-Production Job Number");
                        }
                        else
                        {
                            int filmMasterItemNo = (int)reader1[0];
                            decimal filmWidth = (decimal)reader1[1];
                            string filmDescription = ((decimal)reader1[1]).ToString("N4") + "\" " + reader1[2].ToString();
                            int jobMasterItemNo = (int)reader1[4];
                            string jobDescription = "Job " + readJobJacketForm.UserInput + " " + reader1[5].ToString();
                            int jobJacketNo = (int)reader1[6];
                            reader1.Close();
                            connection1.Close();
                            GetInputForm getPalletNumberForm = new GetInputForm("Scan/Input Pallet ID (Press Abort if Done)", "P", 0, 0, true);
                            bool notDone = true;
                            while (notDone)
                            {
                                getPalletNumberForm.ShowDialog();
                                if (getPalletNumberForm.UserInput.Length > 0)
                                {
                                    command = new SqlCommand("SELECT TOP 1 b.[Master Item No], b.[Width], a.[Location ID], c.[Inventory Available], c.[Description], d.[Description] FROM [Pallet Table] a INNER JOIN ([Roll Table] b INNER JOIN [Inventory Master Table] d ON b.[Master Item No] = d.[Master Item No]) ON a.[Pallet ID] = b.[Pallet ID] INNER JOIN [Location Table] c ON a.[Location ID] = c.[Location ID] WHERE b.[Current LF] != 0 AND a.[Pallet ID] = " + getPalletNumberForm.UserInput.Substring(1), connection1);
                                    connection1.Open();
                                    reader1 = command.ExecuteReader();
                                    if (reader1.Read())
                                    {
                                        if ((int)reader1[0] != filmMasterItemNo || (decimal)reader1[1] != filmWidth)
                                        {
                                            MessageBox.Show("Error - Pallet " + getPalletNumberForm.UserInput + " is " + ((decimal)reader1[1]).ToString("N4") + "\" " + reader1[5].ToString() + ", not " + filmDescription + ".", "Wrong Product and/or Width");
                                        }
                                        else if (!reader1.GetBoolean(3))
                                        {
                                            MessageBox.Show("Error - Pallet " + getPalletNumberForm.UserInput + " is unavailable in location L" + reader1[2].ToString() + " " + reader1[4].ToString() + ".", "Pallet Unavailable");
                                        }
                                        else
                                        {
                                            command = new SqlCommand("SELECT SUM([Original Pounds] * [Current LF] / [Original LF]) + 45 FROM [Roll Table] WHERE [Original LF] != 0 AND [Pallet ID] = " + getPalletNumberForm.UserInput.Substring(1), connection2);
                                            connection2.Open();
                                            decimal grossPalletWeight = (decimal)command.ExecuteScalar();
                                            command = new SqlCommand("UPDATE [Pallet Table] SET [Weight] = " + grossPalletWeight.ToString() + " WHERE [Pallet ID] = " + getPalletNumberForm.UserInput.Substring(1), connection2);
                                            command.ExecuteNonQuery();
                                            //Leave the rolls in [Roll Table] with the Film Part No
                                            //command = new SqlCommand("UPDATE [Roll Table] SET [Master Item No] = " +  jobMasterItemNo.ToString() + ", [UOM ID] = b.[UOM ID], [Original Units] = CASE WHEN a.[UOM] = 'LBS' THEN [Original Pounds] WHEN a.[UOM] = 'ROLLS' THEN 1 ELSE FLOOR([dbo].[UOM Conversion]([Current LF], 'LF', a.[Repeat], a.[Stream Width], a.[Linear Feet per Roll], a.[Standard Yield], CASE WHEN a.[UOM]='PCS' THEN 'IMPS' ELSE a.[UOM] END) * a.[Multiplier]) END FROM [Finished Goods Specification Table] a INNER JOIN [UOM Table] b ON a.[UOM] = b.[Description] WHERE [Pallet ID] = " + getPalletNumberForm.UserInput.Substring(1) + " AND a.[Master Item NO] = " + jobMasterItemNo.ToString(), connection2);
                                            //command.ExecuteNonQuery();
                                            command = new SqlCommand("EXECUTE [dbo].[Create Finished Goods Database Records] " + getPalletNumberForm.UserInput.Substring(1), connection2);
                                            command.ExecuteNonQuery();
                                            command = new SqlCommand("UPDATE [FinishedGoods].[dbo].[tblFGPallet] SET [JobJacketNo] = '" + jobJacketNo.ToString() + "' WHERE [PltID] = " + getPalletNumberForm.UserInput.Substring(1), connection2);
                                            command.ExecuteNonQuery();
                                            connection2.Close();
                                            MessageBox.Show("Pallet " + getPalletNumberForm.UserInput + " is now Finished as " + readJobJacketForm.UserInput + " " + jobDescription + ".", "Success!");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Error - Pallet" + getPalletNumberForm.UserInput + " not found.", "Pallet Not Found");
                                    }

                                    reader1.Close();
                                    connection1.Close();
                                    getPalletNumberForm.UserInput = string.Empty;
                                }
                                else
                                {
                                    notDone = false;
                                }
                            }

                            getPalletNumberForm.Close();
                        }
                    }
                    else
                    {
                        reader1.Close();
                        connection1.Close();
                        MessageBox.Show("Error - Job " + readJobJacketForm.UserInput + " not found.", "Job Not Found");
                    }
                }
            }

            readJobJacketForm.Close();
        }

        private void raToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetInputForm readInputForm = new GetInputForm("Scan/Input Job Jacket Number", "J", 0, 0, true);
            readInputForm.ShowDialog();
            if (readInputForm.UserInput.Length != 0 && readInputForm.UserInput.Length != 7)
            {
                MessageBox.Show("Error - the job jacket number must be entered as \"J\" follored by 6 digits.", "Invalid Entry");
                readInputForm.Close();
            }
            else if (readInputForm.UserInput.Length == 7)
            {
                DialogResult updateRA = DialogResult.No;
                string raNumber = string.Empty;
                command = new SqlCommand("SELECT COUNT(b.[RA Number]), a.[Description] FROM [Inventory Master Table] a LEFT JOIN [RA Master Table] b ON a.[Master Item No] = b.[Master Item No] WHERE a.[Item Type No] = 4 AND a.[Reference Item No] = " + readInputForm.UserInput.Substring(1) + " GROUP BY a.[Description]", connection1);
                connection1.Open();
                reader1 = command.ExecuteReader();
                if (reader1.Read())
                {
                    int raCountForJob = (int)reader1[0];
                    string jobDescription = reader1[1].ToString();
                    reader1.Close();
                    if (raCountForJob > 0)
                    {
                        if (raCountForJob == 1)
                        {
                            command = new SqlCommand("SELECT ISNULL(b.[Master RA Number], 0) FROM [Inventory Master Table] a LEFT JOIN [RA Master Table] b ON a.[Master Item No] = b.[Master Item No] WHERE a.[Item Type No] = 4 AND a.[Reference Item No] = " + readInputForm.UserInput.Substring(1), connection1);
                            int masterRANumber = (int)command.ExecuteScalar();
                            if (masterRANumber == 0)
                            {
                                updateRA = MessageBox.Show("A RA already exist for " + readInputForm.UserInput + " " + jobDescription + ".Do you wish to add to or edit it?", "Update Existing RA?", MessageBoxButtons.YesNo);
                            }
                            else
                            {
                                updateRA = MessageBox.Show("A RA already exist for " + readInputForm.UserInput + " " + jobDescription + " under Master RA " + masterRANumber.ToString() + ".Do you wish to add to or edit it?", "Update Existing RA?", MessageBoxButtons.YesNo);
                            }
                            if (updateRA == DialogResult.Yes)
                            {
                                command = new SqlCommand("SELECT a.[RA Number] FROM [RA Master Table] a INNER JOIN [Inventory Master Table] b ON a.[Master Item No] = b.[Master Item No] WHERE b.[Reference Item No] = " + readInputForm.UserInput.Substring(1), connection1);
                                raNumber = command.ExecuteScalar().ToString();
                            }
                        }
                        else
                        {
                            updateRA = MessageBox.Show("Multiple Return Authorizations already exist for " + readInputForm.UserInput + " " + jobDescription + ".Do you wish to add to or edit one of them?", "Update Existing RA?", MessageBoxButtons.YesNo);
                            if (updateRA == DialogResult.Yes)
                            {
                                command = new SqlCommand("SELECT b.[Description], a.[RA Number], a.[Created By], a.[Create Date], a.[Description], ISNULL(a.[Master RA Number], 0) FROM [RA Master Table] a INNER JOIN [Inventory Master Table] b ON a.[Master Item No] = b.[Master Item No] WHERE b.[Reference Item No] = " + readInputForm.UserInput.Substring(1) + " order by a.[RA Number]", connection1);
                                reader1 = command.ExecuteReader();
                                reader1.Read();
                                OptionsForm pickRAForm = new OptionsForm("Existing RA's for " + readInputForm.UserInput + " " + reader1[0].ToString(), true, true);
                                pickRAForm.Width = 700;
                                do
                                {
                                    if ((int)reader1[5] == 0)
                                    {
                                        pickRAForm.AddOption("RA " + reader1[1].ToString() + " by " + reader1[2].ToString() + " on " + ((DateTime)reader1[3]).ToShortDateString() + " for " + reader1[4].ToString());
                                    }
                                    else
                                    {
                                        pickRAForm.AddOption("RA " + reader1[1].ToString() + " of Master RA" + reader1[5].ToString() + "  by " + reader1[2].ToString() + " on " + ((DateTime)reader1[3]).ToShortDateString() + " for " + reader1[4].ToString());
                                    }
                                    
                                }
                                while (reader1.Read());

                                reader1.Close();
                                pickRAForm.ShowDialog();
                                if (pickRAForm.Option != "Abort")
                                {
                                    raNumber = pickRAForm.Option.Substring(3, pickRAForm.Option.IndexOf(" ") + 2);
                                }
                                else
                                {
                                    updateRA = DialogResult.No;
                                }

                                pickRAForm.Close();
                            }
                        }
                    }

                    DialogResult answer = DialogResult.Yes;
                    if (updateRA == DialogResult.No)
                    {
                        command = new SqlCommand("SELECT a.[Master Item No], a.[Description] FROM [Inventory Master Table] a INNER JOIN ([Finished Goods Specification Table] b INNER JOIN [JobJackets].[dbo].[tblCustomer] c ON b.[Cust ID] = c.[CustID] INNER JOIN [Inventory Master Table] d ON b.[Input Master Item No] = d.[Master Item No]) ON a.[Reference Item No] = b.[Job Jacket No] WHERE a.[Reference Item No] = " + readInputForm.UserInput.Substring(1), connection1);
                        reader1 = command.ExecuteReader();
                        if (reader1.Read())
                        {
                            answer = MessageBox.Show("Do you wish to create a new return authorization for job " + readInputForm.UserInput + " - " + reader1[1].ToString() + "?", "Create new RA?", MessageBoxButtons.YesNo);
                            if (answer == DialogResult.Yes)
                            {
                                GetInputForm masterRANumberForm = new GetInputForm("Master RA Number (Clcik Abort if none)", "#", 0, 0, false);
                                masterRANumberForm.ShowDialog();
                                CommentForm raReasonForm = new CommentForm("Reason for Return", string.Empty, false);
                                raReasonForm.ShowDialog();
                                if (raReasonForm.Comment.Trim().Length > 0)
                                {
                                    if (string.IsNullOrEmpty(masterRANumberForm.UserInput))
                                    {
                                        command = new SqlCommand("execute [dbo].[Create RA Stored Procedure] '" + StartupForm.UserName + "', '" + reader1[0].ToString() + "', '" + raReasonForm.Comment.Replace("'", "''") + "', NULL", connection2);
                                    }
                                    else
                                    {
                                        command = new SqlCommand("execute [dbo].[Create RA Stored Procedure] '" + StartupForm.UserName + "', '" + reader1[0].ToString() + "', '" + raReasonForm.Comment.Replace("'", "''") + "', " + masterRANumberForm.UserInput, connection2);
                                    }

                                    raReasonForm.Close();
                                    masterRANumberForm.Close();
                                    connection2.Open();
                                    raNumber = command.ExecuteScalar().ToString();
                                    connection2.Close();
                                }
                                else
                                {
                                    raReasonForm.Close();
                                    MessageBox.Show("Return Authorization aborted (You must enter a comment for the return", "RA Aborted");
                                    answer = DialogResult.No;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Error - job jacket number" + readInputForm.UserInput + " not found", "Job Not Found");
                            answer = DialogResult.No;
                        }

                        reader1.Close();
                        connection1.Close();
                        readInputForm.Close();
                    }
                    else
                    {
                        connection1.Close();
                        readInputForm.Close();
                    }

                    if (answer == DialogResult.Yes)
                    {
                        RAForm inputRAform;
                        inputRAform = new RAForm(raNumber);
                        inputRAform.ShowDialog();
                        inputRAform.Close();
                    }
                }
                else
                {
                    reader1.Close();
                    connection1.Close();
                    MessageBox.Show("Job " + readInputForm.UserInput + " not found", "Job Not Found");
                    readInputForm.Close();
                }
            }
            else
            {
                readInputForm.Close();
            }
        }

        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserAdminForm addUserForm = new UserAdminForm(string.Empty);
            addUserForm.ShowDialog();
            addUserForm.Dispose();
        }

        private void editUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetInputForm getUserIDForm = new GetInputForm("User ID", "*", 0, 0, false);
            getUserIDForm.TextLowerCase();
            getUserIDForm.ShowDialog();
            if (!string.IsNullOrEmpty(getUserIDForm.UserInput.Trim()))
            {
                command = new SqlCommand("SELECT name FROM sys.server_principals WHERE name = '" + getUserIDForm.UserInput.Trim().Replace("'", "''") + "'", connection1);
                connection1.Open();
                string userName = (string)command.ExecuteScalar();
                connection1.Close();
                if (!string.IsNullOrEmpty(userName))
                {
                    getUserIDForm.Dispose();
                    UserAdminForm editUserForm = new UserAdminForm(userName);
                    editUserForm.ShowDialog();
                    editUserForm.Dispose();
                }
                else
                {
                    MessageBox.Show("Error - User ID '" + getUserIDForm.UserInput + "' not found", "User Not Found");
                    getUserIDForm.Dispose();
                }
            }
            else
            {
                getUserIDForm.Dispose();
            }
        }

        private void disableUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetInputForm getUserIDForm = new GetInputForm("User ID", "*", 0, 0, false);
            getUserIDForm.TextLowerCase();
            getUserIDForm.ShowDialog();
            if (!string.IsNullOrEmpty(getUserIDForm.UserInput.Trim()))
            {
                command = new SqlCommand("SELECT name FROM sys.server_principals WHERE name = '" + getUserIDForm.UserInput.Trim().Replace("'", "''") + "'", connection1);
                connection1.Open();
                string userName = (string)command.ExecuteScalar();
                connection1.Close();
                if (!string.IsNullOrEmpty(userName))
                {
                    command = new SqlCommand("UPDATE [User Rights Table] SET [Active] = 0 WHERE [User ID] = " + getUserIDForm.UserInput.Trim().Replace("'", "''") + "'", connection1);
                    connection1.Open();
                    command.ExecuteNonQuery();
                    command = new SqlCommand("UPDATE [Operator Table] SET [Active] = 0 WHERE [User ID] = " + getUserIDForm.UserInput.Trim().Replace("'", "''") + "'", connection1);
                    command.ExecuteNonQuery();
                    connection1.Close();
                }
                else
                {
                    MessageBox.Show("Error - User ID '" + getUserIDForm.UserInput + "' not found", "User Not Found");
                    getUserIDForm.Dispose();
                }
            }
            else
            {
                getUserIDForm.Dispose();
            }
        }

        private void matchFingerprintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fingerPrintLoginForm fpLoginForm = new fingerPrintLoginForm();
            fpLoginForm.ShowDialog();
            fpLoginForm.Dispose();
        }

        private void plateInventoryToolStripMenuItem_Click(object sender, EventArgs e) {


            Plate_Inventory plateInventoryForm = new Plate_Inventory();
        //    plateInventoryForm.MdiParent= this;
            plateInventoryForm.ShowDialog(this);
        }

        private void PlateCreation_Click(object sender, EventArgs e)
        {


            PlateCreationMain plateCreation = new PlateCreationMain();
            //    plateInventoryForm.MdiParent= this;
            plateCreation.ShowDialog(this);
        }
    }
}

