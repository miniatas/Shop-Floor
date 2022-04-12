/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 8/24/2011
 * Time: 12:33 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace ShopFloor
{
	using System;
	using System.Data.SqlClient;
	using System.Windows.Forms;
	
	/// <summary>
	/// Create a non-modal form to show job hour history on a job while on the production form.
	/// </summary>
	public partial class JobHistoryForm : Form
	{
		private SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
		private SqlCommand command;
		private SqlDataReader reader;
		private string masterItemNumber;

		public JobHistoryForm(string jobNo, string masterItemNo)
		{
			InitializeComponent();
			
			this.Text = jobNo + " Job History";
			masterItemNumber = masterItemNo;
			ProductionForm.JobHistoryOpen = true;
			command = new SqlCommand("SELECT a.[Production ID], a.[Machine No], b.[Name], a.[Start Time], a.[Setup Hrs], a.[Run Hrs], a.[DT Hrs], DATEADD(minute, (a.[Setup Hrs] + a.[Run Hrs] + a.[DT Hrs]) * 60, a.[Start Time]), c.[Description] FROM [Production Master Table] a INNER JOIN [Operator Table] b ON a.[Operator ID] = b.[Operator ID] INNER JOIN [Save Production Reason Table] c ON a.[End Reason ID] = c.[End Reason ID] WHERE a.[Master Item No]  = " + masterItemNumber + " ORDER BY a.[Start Time], a.[Machine No]", connection);
			connection.Open();
			reader = command.ExecuteReader();
			while (reader.Read())
			{
				if ((decimal)reader[6] > 0)
				{
					dgvJobHistory.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), "Details",  reader[7].ToString(), reader[8].ToString());
				}
				else
				{
					dgvJobHistory.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), string.Empty,  reader[7].ToString(), reader[8].ToString());
				}
			}
			
			reader.Close();
			connection.Close();
			if (dgvJobHistory.Rows.Count > 0)
			{
				dgvJobHistory.FirstDisplayedScrollingRowIndex = dgvJobHistory.Rows.Count - 1;
			}
		}
		
		private void JobHistoryFormFormClosed(object sender, FormClosedEventArgs e)
		{
			ProductionForm.JobHistoryOpen = false;
			this.Dispose();
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		private void DgvJobHistoryCellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0 && e.ColumnIndex == 7 && dgvJobHistory.Rows[e.RowIndex].Cells[7].Value.ToString() == "Details")
			{
				string downTimeDetail = string.Empty;
				command = new SqlCommand("select b.[Description],a.[Hours] from [Downtime Hours Table] a inner join [Downtime Reason Table] b on a.[Downtime Reason ID]=b.[Downtime Reason ID] where a.[Prod Record ID]=" + dgvJobHistory.Rows[e.RowIndex].Cells[0].Value.ToString() + " order by b.[Display Order],a.[Hours] desc", connection);
				connection.Open();
				reader = command.ExecuteReader();
				while (reader.Read())
				{
					downTimeDetail += reader[0].ToString() + " for " + ((decimal)reader[1]).ToString("N2") + " hours\r\n";
				}
				
				reader.Close();
				connection.Close();
				MessageBox.Show(downTimeDetail, "Downtime Detail");
			}
		}
	}
}
