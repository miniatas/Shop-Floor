/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 4/21/2011
 * Time: 11:37 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace ShopFloor
{
	using System;
	using System.Data.SqlClient;
	using System.Drawing;
	using System.Drawing.Printing;
	using System.Windows.Forms;

	/// <summary>
	/// Validate the number of rolls, enter the weight, and print the label for a pallet of finished goods.
	/// </summary>
	public partial class FinishingForm : Form
	{
		private SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
		private SqlDataReader reader;
		private SqlCommand command;
		private string palletNumber;
		private int masterItemNumber;
		private string jobNumber;
		private int itemNumber;
		private int rollCount;
		private DateTime palletDate;
		private Font palletRollListDetailFont;
		private Font palletRollListHeaderFont;
		private float lineHeight;
		private bool palletExists;
		private bool palletChanged = false;
		
		public FinishingForm(string palletNo, int masterItemNo, string jobNo, int itemNo, string jobDescription, int numberOfRolls, bool isFinishedGood)
		{
			this.InitializeComponent();
			
			this.Text = "Finishing  User ID: " + StartupForm.UserName + "	 Job " + jobNo + " - " + jobDescription;
			this.palletNumber = palletNo;
			this.masterItemNumber = masterItemNo;
			jobNumber = jobNo;
			itemNumber = itemNo;
			rollCount = numberOfRolls;
			palletExists = isFinishedGood;
			cmdMoveRoll.Enabled = !isFinishedGood;
			cmdAddRoll.Enabled = isFinishedGood;
			cmdRemoveRoll.Enabled = isFinishedGood;
		}
		
		private void FinishingFormShown(object sender, EventArgs e)
		{
			if (palletExists)
			{
				FillDgvPalletRolls();
			}
			else
			{
				VerifyRollCount();
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		private void VerifyRollCount()
		{
			GetInputForm getRollCountForm = new GetInputForm("No. of Rolls", "#", 1, 9999, false);
			getRollCountForm.ShowDialog();
			if (getRollCountForm.UserInput.Length > 0 && int.Parse(getRollCountForm.UserInput) == rollCount)
			{
				int netWeight = NetWeight();
				GetInputForm frmGetGrossWeight = new GetInputForm("Gross Pallet Weight", "#", netWeight, netWeight + 200, false);
				frmGetGrossWeight.ShowDialog();
				if (frmGetGrossWeight.UserInput.Length > 0)
				{
					command = new SqlCommand("select getdate()", connection);
					connection.Open();
					palletDate = (DateTime)command.ExecuteScalar();
					command = new SqlCommand("update [Pallet Table] set [Created By]='" + StartupForm.UserName + "',[Create Date]='" + palletDate.ToString() + "',[Weight]=" + frmGetGrossWeight.UserInput + ", [Location ID]=191 where [Pallet ID]=" + palletNumber.Substring(1), connection);
					command.ExecuteNonQuery();
					if (palletExists)
					{
						command = new SqlCommand("update [" + StartupForm.FGDatabase + "].[dbo].[tblFGPallet] set [PltGrossWeight]=" + frmGetGrossWeight.UserInput + ",[PltNetWeight]=" + netWeight.ToString() + ",[PltBlankWeight]=" + (int.Parse(frmGetGrossWeight.UserInput) - netWeight) + " where [PltID]=" + palletNumber.Substring(1), connection);
						command.ExecuteNonQuery();
						palletChanged = false;
					}
					else
					{
						command = new SqlCommand("update [Roll Table] set [Master Item No]=d.[Master Item No],[UOM ID]=c.[UOM ID],[Original Units]=case when b.[UOM] in ('LBS', 'LBS_STD') then [Original Pounds] when b.[UOM]='ROLLS' then 1 else floor([dbo].[UOM Conversion]([Current LF],'LF',isnull(case when b.[RepeatComexi]>0 then b.[RepeatComexi] end,b.[RepeatOthers]),isnull(b.[SlitWidth],b.[MatPrintSize1_1]),NULL,case when isnull(432000.0/e.[Std Yield],0)+isnull(432000.0/f.[Std Yield],0)+isnull(432000.0/g.[Std Yield],0)+isnull(432000.0/h.[Std Yield],0)>0 then 432000.0/(isnull(432000.0/e.[Std Yield],0)+isnull(432000.0/f.[Std Yield],0)+isnull(432000.0/g.[Std Yield],0)+isnull(432000.0/h.[Std Yield],0)) else NULL end, case when b.[UOM]='PCS' then 'IMPS' else b.[UOM] end))*isnull(b.ODtxt, 1) end from [Inventory Master Table] a inner join ([JobJackets].[dbo].[tblJobTicket] b inner join [UOM Table] c on case when b.[UOM]='PCS' then 'IMPS' else b.[UOM] end=c.[Description] inner join [Inventory Master Table] d on b.[JobJacketNo]=cast(d.[Reference Item No] as nvarchar(10)) left join [Film View] e on b.[MatPrint1_1]=e.[Part No] left join [Film View] f on b.[MatLam1_1]=f.[Part No] left join [Film View] g on b.[MatLam2_1]=g.[Part No] left join [Film View] h on b.[MatLam3_1]=h.[Part No]) on substring(cast(a.[Reference Item No] as nvarchar(10)),1,len(cast(a.[Reference Item No] as nvarchar(10)))-2)=b.[JobJacketNo] where [Roll Table].[Master Item No]=a.[Master Item No] and [Roll Table].[Pallet ID]="  + palletNumber.Substring(1), connection);
						command.ExecuteNonQuery();
						command = new SqlCommand("execute [dbo].[Create Finished Goods Database Records] " + palletNumber.Substring(1), connection);
						command.ExecuteNonQuery();
					}
					
					connection.Close();
					for (int i = 1; i <= 5; i++)
				    {
						PrintClass.Label(palletNumber);
					}
				}
				
				frmGetGrossWeight.Dispose();
				getRollCountForm.Dispose();
				this.Close();
			}
			else if (getRollCountForm.UserInput.Length > 0)
			{
				getRollCountForm.Dispose();
				MessageBox.Show("Error - the number of rolls you entered does not match the records", "Invalid Number of Rolls");
				FillDgvPalletRolls();
                this.Close();
			}
			else
			{
				getRollCountForm.Dispose();
				this.Close();
			}
		}
		
		private void FillDgvPalletRolls()
		{
			int newrollCount = 0;
			dgvPalletRolls.Rows.Clear();
			command = new SqlCommand("select a.[Roll ID],a.[Input Roll ID 1],a.[Set No],cast(a.[Lane] as int) FROM [Production Roll Table] a inner join [Roll Table] b on a.[Roll ID]=b.[Roll ID] where b.[Pallet ID]=" + palletNumber.Substring(1) + " order by a.[Roll ID]", connection);
			connection.Open();
			reader = command.ExecuteReader();
			while (reader.Read())
			{
				if (reader[3] == DBNull.Value)
				{
					dgvPalletRolls.Rows.Add("R" + reader[0].ToString(), reader[1].ToString() + "-" + reader[2].ToString(), null);										
				}
				else
				{
					dgvPalletRolls.Rows.Add("R" + reader[0].ToString(), reader[1].ToString() + "-" + reader[2].ToString(), ConvertClass.GetLaneName((int)reader[3]));					
				}
	
				newrollCount += 1;
			}
			
            reader.Close();
            connection.Close();
            rollCount = newrollCount;
            dgvPalletRolls.Visible = true;
            cmdFinish.Visible = false;
            cmdPrintList.Visible = true;
            cmdCloseList.Visible = true;
            cmdMoveRoll.Visible = true;
		}
		
		private void CmdPrintClick(object sender, EventArgs e)
		{
			PrintDocument palletRollList = new PrintDocument();
			// palletRollList.PrinterSettings.PrinterName = MainForm.strPalletPrinterName;
			PrintPreviewDialog palletRollListPreview = new PrintPreviewDialog();
			palletRollListPreview.Document = palletRollList;
			palletRollList.PrintPage += new PrintPageEventHandler(PrintPalletRollList);
			palletRollList.BeginPrint += new PrintEventHandler(PdPalletRollList_BeginPrint);
			palletRollList.EndPrint += new PrintEventHandler(PdPalletRollList_EndPrint);
			if (palletRollListPreview.ShowDialog() == DialogResult.OK)
			{
				palletRollList.Print();
			}
		}

		private void PrintPalletRollList(object sender, PrintPageEventArgs e)
		{
			Graphics g = e.Graphics;
			int xPosition = e.MarginBounds.Left;
			int lineCount = 0;
			lineHeight = palletRollListHeaderFont.GetHeight(g);
			int linesPerPage = (int)(((e.MarginBounds.Bottom - e.MarginBounds.Top) / lineHeight) - 2);
			float yPosition = (2 * lineHeight) + e.MarginBounds.Top;
			int headerPosition = e.MarginBounds.Top;
			PrintHdr(g, headerPosition, e.MarginBounds.Left);
			for (int i = 0; i < dgvPalletRolls.Rows.Count; i++)
			{
				yPosition += lineHeight + 5;
				g.DrawString(dgvPalletRolls.Rows[i].Cells[0].Value.ToString(), palletRollListDetailFont, Brushes.Black, xPosition + 20, yPosition);
				if (lineCount > linesPerPage)
				{
					e.HasMorePages = true;
					break;
				}
			}
			
			g.Dispose();
		}
		
		private void PdPalletRollList_BeginPrint(object sender, PrintEventArgs e)
		{
			palletRollListDetailFont = new Font("Arial", 12);
			palletRollListHeaderFont = new Font(palletRollListDetailFont, FontStyle.Bold);
		}
		
		private void PdPalletRollList_EndPrint(object sender, PrintEventArgs e)
		{
			palletRollListDetailFont.Dispose();
			palletRollListHeaderFont.Dispose();
		}
		
		private void PrintHdr(Graphics g, int yPosition, int xPosition)
		{
			g.DrawString("Roll List for Pallet " + palletNumber, palletRollListHeaderFont, Brushes.Black, xPosition, yPosition);
		}
		
		private void CmdCloseListClick(object sender, EventArgs e)
		{
			dgvPalletRolls.Visible = false;
	      	cmdFinish.Visible = true;
			cmdPrintList.Visible = false;
			cmdCloseList.Visible = false;
			cmdMoveRoll.Visible = false;
		}
		
		private void CmdFinishClick(object sender, EventArgs e)
		{
			VerifyRollCount();
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		private void CmdMoveRollClick(object sender, EventArgs e)
		{
			GetInputForm frmReadBarcode = new GetInputForm("Scan/Input Barcode", "R", 0, 0, true);
			frmReadBarcode.ShowDialog();
			if (frmReadBarcode.UserInput.Length > 0)
			{
				command = new SqlCommand("select a.[Master Item No],a.[Width],c.[View Name],isnull(a.[Pallet ID],0),c.[Description] from [Roll Table] a inner join ([Inventory Master Table] b inner join [Item Type Table] c on b.[Item Type No]=c.[Item Type No]) on a.[Master Item No]=b.[Master Item No] left join [Pallet Table] d on a.[Pallet ID]=d.[Pallet ID], [Location Table] e where isnull(a.[Location ID],d.[Location ID])=e.[Location ID] and e.[Inventory Available]=1 and a.[Current LF]>0 and a.[Roll ID]=" + frmReadBarcode.UserInput.Substring(1), connection);
				connection.Open();
				reader = command.ExecuteReader();
				if (reader.Read())
				{
					int moveMasterItemNumber = (int)reader[0];
					decimal moveRollWidth = (decimal)reader[1];
					int moveFromPalletNumber = (int)reader[3];
					string partType = reader[4].ToString();
					command = new SqlCommand("select [Description] from " + reader[2].ToString() + " where [Master Item No]=" + reader[0].ToString(), connection);
					reader.Close();
					string itemDescription = (string)command.ExecuteScalar();
					connection.Close();
					if (ModulesClass.MoveRoll(frmReadBarcode.UserInput, moveMasterItemNumber, moveRollWidth, itemDescription, moveFromPalletNumber, partType) == DialogResult.Yes && moveMasterItemNumber == masterItemNumber)
					{
						FillDgvPalletRolls();
					}
				}				
				else
				{
					reader.Close();
					connection.Close();
					MessageBox.Show("Error - roll " + frmReadBarcode.UserInput + " not found", "Roll Not Found");
				}
                connection.Close();
				frmReadBarcode.Dispose();
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void CmdAddRollClick(object sender, EventArgs e)
		{
			GetInputForm frmReadBarcode = new GetInputForm("Scan/Input Barcode", "R", 0, 0, true);
			frmReadBarcode.ShowDialog();
			if (frmReadBarcode.UserInput.Length > 0)
			{
				command = new SqlCommand("select a.[Master Item No],d.[JobJacketNo],d.[ItemNo],e.[ItemName],a.[Width] from [Roll Table] a inner join ([Inventory Master Table] b inner join [Item Type Table] c on b.[Item Type No]=c.[Item Type No] inner join ([JobJackets].[dbo].[tblJobTicket] d inner join [JobJackets].[dbo].[tblItem] e on d.[ItemNo]=e.[ItemNo]) on substring(cast(b.[Reference Item No] as nvarchar(10)), 1, len(cast(b.[Reference Item No] as nvarchar(10))) - 2)=d.[JobJacketNo]) on a.[Master Item No]=b.[Master Item No] inner join [Location Table] f on a.[Location ID]=f.[Location ID] where f.[Inventory Available]=1 and c.[Description] like '%WIP%' and a.[Current LF]>0 and a.[Roll ID]=" + frmReadBarcode.UserInput.Substring(1), connection);
				connection.Open();
				reader = command.ExecuteReader();
				if (reader.Read())
				{
					// WIP Roll Found
					DialogResult result = DialogResult.No;					
					if (reader[1].ToString() == jobNumber)
					{
						// WIP Roll to Finished Good Move of Same Job
						result = DialogResult.Yes;
					}
					else if ((int)reader[2] == itemNumber)
					{
						// WIP Roll of Same Item Number
						result = MessageBox.Show("This is a WIP roll for job " + reader[1].ToString() + " which is the same item number (" + ((int)reader[2]).ToString() + ") as job " + jobNumber + ". Do you wish to convert it to a finished good and add to pallet?", "Confirm Job Number Change and Finished Good", MessageBoxButtons.YesNo);
					}
					else
					{
						MessageBox.Show("Error - roll " + frmReadBarcode.UserInput + " is a WIP Roll for job " + reader[1].ToString()  + " and item number " + ((int)reader[2]).ToString() + ". You cannot add a different item number to this pallet", "Invalid Roll");
					}
					
					if (result == DialogResult.Yes)
					{
						command = new SqlCommand("insert into [Change Roll Master Item Number Table] select '" + StartupForm.UserName + "',GETDATE()," + palletNumber.Substring(1) + "," + frmReadBarcode.UserInput.Substring(1) + "," + ((int)reader[0]).ToString() + "," + masterItemNumber.ToString(), connection);
						reader.Close();
						command.ExecuteNonQuery();
						connection.Close();
						FillDgvPalletRolls();
						palletChanged = true;
					}
					else
					{
						reader.Close();
						connection.Close();
					}
				}
				else
				{
					reader.Close();
					connection.Close();
					MessageBox.Show("Error - roll " + frmReadBarcode.UserInput + " not found or is not a WIP Roll", "Roll Not Found");
				}
                connection.Close();
			}

			frmReadBarcode.Dispose();
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		private void CmdRemoveRollClick(object sender, EventArgs e)
		{
			GetInputForm frmReadBarcode = new GetInputForm("Scan/Input Barcode", "R", 0, 0, true);
			frmReadBarcode.ShowDialog();
			if (frmReadBarcode.UserInput.Length > 0)
			{
				command = new SqlCommand("select isnull([Pallet ID],0) from [Roll Table] where [Current LF]>0 and [Roll ID]=" + frmReadBarcode.UserInput.Substring(1), connection);
				connection.Open();
				int? foundPalletNumber = (int?)command.ExecuteScalar();
				if (foundPalletNumber.HasValue)
				{
					// Roll Found
					if (foundPalletNumber != int.Parse(palletNumber.Substring(1)))
					{
						connection.Close();
						MessageBox.Show("Error - roll " + frmReadBarcode.UserInput + " does not belong to pallet " + palletNumber + ".", "Invalid Roll");
					}
					else
					{
						// Unfinish Roll
						command = new SqlCommand("select top 1 [Master Item No] from [Inventory Master Table] where cast([Reference Item No] as nvarchar(10)) like '" + jobNumber + "%' order by [Reference Item No] desc", connection);
						int newMasterItemNumber = (int)command.ExecuteScalar();
						command = new SqlCommand("insert into [Change Roll Master Item Number Table] select '" + StartupForm.UserName + "',GETDATE()," + palletNumber.Substring(1) + "," + frmReadBarcode.UserInput.Substring(1) + "," + masterItemNumber.ToString() + "," + newMasterItemNumber.ToString(), connection);
						command.ExecuteNonQuery();
						connection.Close();
						FillDgvPalletRolls();
						palletChanged = true;
					}
				}
				else
				{
					connection.Close();
					MessageBox.Show("Error - roll " + frmReadBarcode.UserInput + "not found.", "Invalid Roll");
				}

                connection.Close();
			}
	
			frmReadBarcode.Dispose();
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		private void FinishingFormFormClosing(object sender, FormClosingEventArgs e)
		{
			if (palletChanged)
			{
				MessageBox.Show("Error - you must re-enter pallet weight and re-print the pallet label", "The Pallet has Changed");
				e.Cancel = true;
			}
		}
		
		private int NetWeight()
		{
			command = new SqlCommand("select cast(sum([Original Pounds] * [Current LF] / [Original LF]) as int) from [Roll Table] where [Pallet ID]=" + palletNumber.Substring(1), connection);
			connection.Open();
			int pounds = (int)command.ExecuteScalar();
			connection.Close();
			return pounds;
		}
	}
}
