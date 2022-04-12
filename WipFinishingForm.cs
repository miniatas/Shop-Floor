/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 11/1/2011
 * Time: 10:59 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace ShopFloor
{
	using System;
	using System.Data.SqlClient;
	using System.Drawing;
	using System.Globalization;
	using System.Windows.Forms;

	/// <summary>
	/// Form for finishing non-slit product.
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Wip")]
	public partial class WipFinishingForm : Form
	{
		private int masterItemNumber;
		private string jobNumber;
		private SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
		private SqlDataReader reader;
		private SqlCommand command;
		private decimal conversionFactor;
		private string uomDescription;
		
		public WipFinishingForm(int masterItemNo, string jobNo, string firstRollNo, int firstRollLinearFeet, decimal firstRollPounds)
		{
			InitializeComponent();
			
			jobNumber = jobNo;
			masterItemNumber = masterItemNo;
			command = new SqlCommand("select b.[CustName],c.[ItemName],[dbo].[UOM Conversion](1,'LF',isnull(case when a.[RepeatComexi]>0 then a.[RepeatComexi] end,a.[RepeatOthers]),isnull(a.[SlitWidth]/isnull(a.ODtxt, 1),a.[MatPrintSize1_1]),case when a.UOM = 'ROLLS' then a.[LinearFeet]/a.[OrderAmt] else NULL end,case when isnull(432000.0/e.[Std Yield],0)+isnull(432000.0/f.[Std Yield],0)+isnull(432000.0/g.[Std Yield],0)+isnull(432000.0/h.[Std Yield],0)>0 then 432000.0/(isnull(432000.0/e.[Std Yield],0)+isnull(432000.0/f.[Std Yield],0)+isnull(432000.0/g.[Std Yield],0)+isnull(432000.0/h.[Std Yield],0)) else NULL end, case when a.[UOM]='PCS' then 'IMPS' else a.[UOM] end), case when a.[UOM]='PCS' then 'IMPS' else a.[UOM] end from [JobJackets].[dbo].[tblJobTicket] a inner join [JobJackets].[dbo].[tblCustomer] b on a.[CustID]=b.[CustID] inner join [JobJackets].[dbo].[tblItem]c on a.[ItemNo]=c.[ItemNo] inner join [UOM Table] d on case when a.[UOM]='PCS' then 'IMPS' else a.[UOM] end=d.[Description] left join [Film View] e on a.[MatPrint1_1]=e.[Part No] left join [Film View] f on a.[MatLam1_1]=f.[Part No] left join [Film View] g on a.[MatLam2_1]=g.[Part No] left join [Film View] h on a.[MatLam3_1]=h.[Part No] where a.[JobJacketNo]='" + jobNumber.Substring(0, jobNumber.Length - 2) + "'", connection);
			connection.Open();
			reader = command.ExecuteReader();
			reader.Read();
			rtbJobInformation.Text = "Job Jacket No.: " + jobNumber.Substring(0, jobNumber.Length - 2) + "\r\nCustomer:         " + reader[0].ToString() + "\r\nDescription:      " + reader[1].ToString();
			conversionFactor = (decimal)reader[2];
			uomDescription = reader[3].ToString();
			reader.Close();
			connection.Close();
			if (uomDescription == "LBS")
			{
				dgvRollsToPalletize.Columns[3].Visible = false;
				lblRollsToPalletize.Width = 300;
				dgvRollsToPalletize.Width = 300;
				cmdAddRoll.Left = 338;
				cmdCreatePallet.Left = 338;
			}
			
			AddRoll(firstRollNo, firstRollLinearFeet, firstRollPounds);
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void AddRoll(string rollNumber, int linearFeet, decimal pounds)
		{
			if (uomDescription == "LBS")
			{
				dgvRollsToPalletize.Rows.Add(rollNumber, pounds.ToString("N2"), "LBS", pounds.ToString("N2"), linearFeet.ToString("N0"));
			}
			else
			{
				dgvRollsToPalletize.Rows.Add(rollNumber, Math.Floor((decimal)linearFeet * conversionFactor).ToString("N0"), uomDescription, pounds.ToString("N2"), linearFeet.ToString("N0"));
			}
		}
		
		private void DgvRollsToPalletizeRowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			if (dgvRollsToPalletize.Rows.Count == 0)
			{
				cmdCreatePallet.Enabled = false;
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		private void CmdAddRollClick(object sender, EventArgs e)
		{
			GetInputForm frmReadBarcode = new GetInputForm("Scan/Input Roll Barcode", "R", 0, 0, true);
			frmReadBarcode.ShowDialog();
			if (frmReadBarcode.UserInput.Length > 0)
			{
				command = new SqlCommand("select a.[Master Item No],isnull(a.[Pallet ID],0),cast(round(a.[Current LF],0) as int),a.[Original Pounds]*a.[Current LF]/a.[Original LF] from [Roll Table] a left join [Pallet Table] b on a.[Pallet ID]=b.[Pallet ID], [Location Table] c where isnull(a.[Location ID],b.[Location ID])=c.[Location ID] and c.[Inventory Available]=1 and a.[Roll ID]=" + frmReadBarcode.UserInput.Substring(1) + " and a.[Current LF]>0", connection);
				connection.Open();
				reader = command.ExecuteReader();
				if (reader.Read())
				{
					if ((int)reader[1] != 0)
					{
						MessageBox.Show("Error - roll " + frmReadBarcode.UserInput + " is palletized.  You cannot finish a roll that is palletized.", "Invalid Roll");
					}
					else
					{
						if ((int)reader[0] == masterItemNumber)
						{
							AddRoll(frmReadBarcode.UserInput, (int)reader[2], (decimal)reader[3]);
							cmdCreatePallet.Enabled = true;	
						}
						else
						{
							MessageBox.Show("Error - roll " + frmReadBarcode.UserInput + " is not for job " + jobNumber + ".", "Invalid Roll");
						}
					}
				}
				else
				{
					MessageBox.Show("Error - roll " + frmReadBarcode.UserInput + " not found.", "Roll Not Found");
				}
				
				reader.Close();
				connection.Close();
			}
			
			frmReadBarcode.Dispose();
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		private void CmdCreatePalletClick(object sender, EventArgs e)
		{
			string rollList = string.Empty;			
			decimal netWeight = 0;
			for (int i = 0; i < dgvRollsToPalletize.Rows.Count; ++i) 
			{     
				rollList += dgvRollsToPalletize.Rows[i].Cells[0].Value.ToString().Substring(1) + ",";
				netWeight += decimal.Parse(dgvRollsToPalletize.Rows[i].Cells[3].Value.ToString(), NumberStyles.Number);
			}
			
			GetInputForm frmGetGrossWeight = new GetInputForm("Gross Pallet Weight", "#", (int)Math.Floor(netWeight + 1), (int)Math.Round(netWeight, 0) + 200, false);
			frmGetGrossWeight.ShowDialog();
			if (frmGetGrossWeight.UserInput.Length > 0)
			{
				command = new SqlCommand("execute [Create Pallet Stored Procedure] '" + StartupForm.UserName + "',191," + frmGetGrossWeight.UserInput + ",NULL", connection);
				connection.Open();
				reader = command.ExecuteReader();
				reader.Read();
				int palletNumber = (int)reader[0];
				reader.Close();
				command = new SqlCommand("update [Roll Table] set [Pallet ID]=" + palletNumber.ToString() + ",[Location ID]=NULL,[Master Item No]=d.[Master Item No],[UOM ID]=c.[UOM ID],[Original Units]=case when b.[UOM]='LBS' then [Original Pounds] when b.[UOM]='Rolls' then 1 else floor([dbo].[UOM Conversion]([Current LF],'LF',isnull(case when b.[RepeatComexi]>0 then b.[RepeatComexi] end,b.[RepeatOthers]),isnull(b.[SlitWidth]/isnull(b.ODtxt, 1),b.[MatPrintSize1_1]),NULL,case when isnull(432000.0/e.[Std Yield],0)+isnull(432000.0/f.[Std Yield],0)+isnull(432000.0/g.[Std Yield],0)+isnull(432000.0/h.[Std Yield],0)>0 then 432000.0/(isnull(432000.0/e.[Std Yield],0)+isnull(432000.0/f.[Std Yield],0)+isnull(432000.0/g.[Std Yield],0)+isnull(432000.0/h.[Std Yield],0)) else NULL end, case when b.[UOM]='PCS' then 'IMPS' else b.[UOM] end)) end from [Inventory Master Table] a inner join ([JobJackets].[dbo].[tblJobTicket] b inner join [UOM Table] c on case when b.[UOM]='PCS' then 'IMPS' else b.[UOM] end=c.[Description] inner join [Inventory Master Table] d on b.[JobJacketNo]=cast(d.[Reference Item No] as nvarchar(10)) left join [Film View] e on b.[MatPrint1_1]=e.[Part No] left join [Film View] f on b.[MatLam1_1]=f.[Part No] left join [Film View] g on b.[MatLam2_1]=g.[Part No] left join [Film View] h on b.[MatLam3_1]=h.[Part No]) on substring(cast(a.[Reference Item No] as nvarchar(10)),1,len(cast(a.[Reference Item No] as nvarchar(10)))-2)=b.[JobJacketNo] where [Roll Table].[Master Item No]=a.[Master Item No] and [Roll Table].[Roll ID] in (" + rollList.Substring(0, rollList.Length - 1) + ")", connection);
				command.ExecuteNonQuery();
				command = new SqlCommand("execute [dbo].[Create Finished Goods Database Records] " + palletNumber.ToString(), connection);
				command.ExecuteNonQuery();
				connection.Close();
				for (int i = 1; i <= 4; i++)
				{
					PrintClass.Label("P" + palletNumber.ToString());
				}
			}
			
			frmGetGrossWeight.Dispose();
			this.Close();
		}
	}
}
