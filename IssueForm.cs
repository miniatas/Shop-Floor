/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 4/19/2012
 * Time: 1:10 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace ShopFloor
{
	using System;
	using System.Data;
	using System.Data.SqlClient;
	using System.Globalization;
	using System.Windows.Forms;

	/// <summary>
	/// Description of Form1.
	/// </summary>
	public partial class IssueForm : Form
	{
		private SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=120;Persist Security Info=False");
		private SqlDataReader reader;
		private SqlCommand command;
		private string commandText; 
		private DataTable inventoryTable;
        private DataTable allocationTable;
		private BindingSource bsFilms;
        private BindingSource bsAllocations;
		private int masterItemNumber;
		private string defaultFilmWidth;
		private decimal filmWidth;
		private decimal printWidth;
		private string defaultGauge;
		private decimal stdInputFeet;
		private decimal stdInputPounds;
		private int filmCustomerSupplied;
		private int allocationID;
		private decimal allocatedLF;
		private decimal allocatedPounds;
		private string laminationFilmNumber;
		private int nextPriority;

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cust")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1306:SetLocaleForDataTypes")]
		public IssueForm(int masterItemNo, string title, string jobDescriptions, decimal stdFilmWidth, decimal stdPrintWidth, decimal stdFilmGauge, decimal inputFeet, decimal inputPounds, string filmType, string filmCompatibilityGroup, string filmBrand, string filmDescription, int filmCS, int allocID, string defaultAllocationMethod, decimal percentOfNeedstoAllocate, decimal incomingAllocatedLF, decimal incomingAllocatedPounds, string filmNo, int lastPriority)
		{
			InitializeComponent();
			this.Text = title;
			if (printWidth != 0)
			{
				rtbJobInformation.Text = jobDescriptions + "\r\n Std Film Width: " + ((decimal)stdFilmWidth).ToString("N4") + "     Print Width: " + ((decimal)printWidth).ToString("N4") + "     Std Gauge: " + ((decimal)stdFilmGauge).ToString("N3") + "\r\n Film Type: " + filmType + "     Compatibility Group: " + filmCompatibilityGroup + "\r\n Brand: " + filmBrand + "     Description: " + filmDescription;
			}
			else
			{
				rtbJobInformation.Text = jobDescriptions + "\r\n Std Film Width: " + ((decimal)stdFilmWidth).ToString("N4") + "     Std Gauge: " + ((decimal)stdFilmGauge).ToString("N3") + "\r\n Film Type: " + filmType + "     Compatibility Group: " + filmCompatibilityGroup + "\r\n Brand: " + filmBrand + "     Description: " + filmDescription;
			}
			
			if (filmCustomerSupplied == 1)
			{
				rtbJobInformation.Text += " (Cust-Supplied)";
			}
			
			// Override incorrect Locations and Widths from the designer on pnlFilters
			lblPercentofNeedToAllocate.Location = new System.Drawing.Point(500, 10);
			lblPercentofNeedToAllocate.Width = 105;
			txtPercentOfNeedToAllocate.Location = new System.Drawing.Point(605, 7);
			txtPercentOfNeedToAllocate.Width = 40;
			lblPercentagePoint.Location = new System.Drawing.Point(645, 10);
			lblPercentagePoint.Width = 10;
			lblAllocationMethod.Location = new System.Drawing.Point(685, 10);
			lblAllocationMethod.Width = 100;
			cbxAllocationMethod.Location = new System.Drawing.Point(785, 7);
			cbxAllocationMethod.Width = 50;
			cmdAllocate.Location = new System.Drawing.Point(870, 5);
			cmdAllocate.Width = 120;
			masterItemNumber = masterItemNo;
			defaultFilmWidth = ((decimal)stdFilmWidth).ToString("N4");
			filmWidth = stdFilmWidth;
			printWidth = stdPrintWidth;
			defaultGauge = ((decimal)stdFilmGauge).ToString("N3");
			txtPercentOfNeedToAllocate.Text = ((decimal)percentOfNeedstoAllocate).ToString("N0");
			stdInputFeet = inputFeet;
			stdInputPounds = inputPounds;
			filmCustomerSupplied = filmCS;
			allocationID = allocID;
			allocatedLF = incomingAllocatedLF;
			allocatedPounds = incomingAllocatedPounds;
			laminationFilmNumber = filmNo;
			nextPriority = lastPriority + 1;
			command = new SqlCommand("select [Description] from [Allocation Method Table] order by [Allocation Method ID]", connection);
			connection.Open();
			reader = command.ExecuteReader();
			while (reader.Read())
			{
				cbxAllocationMethod.Items.Add(reader[0].ToString());
			}
			
			reader.Close();
			connection.Close();
			cbxAllocationMethod.Text = defaultAllocationMethod;
			UpdateAllocationInformationText();
			commandText = commandText = "SELECT	[Master Item No], [Description], [Width], CASE WHEN [Sample] = 1 THEN 'Yes' ELSE 'No' END AS [Sample], [LF on Order], [LBS on Order], [LF in Stock], [LBS in Stock], CASE WHEN FLOOR([LF in Stock] - [LF Allocated]) > 0 THEN [LF in Stock] - [LF Allocated] ELSE 0 END AS [LF in Stock Avail], CASE WHEN FLOOR([LBS in Stock] - [LBS Allocated]) > 0 THEN [LBS in Stock] - [LBS Allocated] ELSE 0 END AS [LBS in Stock Avail], [LF on Order] + [LF in Stock] - [LF Allocated] AS [LF Available (Needed)], [LBS on Order] + [LBS in Stock] - [LBS Allocated] AS [LBS Available (Needed)], [Feet per Pound] FROM [Get Film Availability] (" + filmCustomerSupplied.ToString();
			if (filmCompatibilityGroup != "NONE")
			{
				commandText += ", '" + filmCompatibilityGroup.Replace("'", "''") + "', NULL)";
			}
			else
			{
				commandText += ", NULL, '" + filmBrand.Replace("'", "''") + "')";
			}
			
			commandText += " ORDER BY ABS(" + stdFilmGauge + " - [Gauge]), ABS(" + stdFilmWidth + " - [Width]), CASE WHEN [Brand] = '" + filmBrand.Replace("'", "''") + "' THEN ' ' ELSE [Brand] END";
			inventoryTable = new DataTable();
			inventoryTable.Columns.Add("Master Item No", typeof(int));
			inventoryTable.Columns.Add("Film Name", typeof(string));
			inventoryTable.Columns.Add("Width", typeof(decimal));
			inventoryTable.Columns.Add("Sample", typeof(string));
			inventoryTable.Columns.Add("LF on Order", typeof(decimal));
			inventoryTable.Columns.Add("LBS on Order", typeof(decimal));
			inventoryTable.Columns.Add("LF in Stock", typeof(decimal));
			inventoryTable.Columns.Add("LBS in Stock", typeof(decimal));
			inventoryTable.Columns.Add("LF in Stock Avail", typeof(decimal));
			inventoryTable.Columns.Add("LBS in Stock Avail", typeof(decimal));
			inventoryTable.Columns.Add("LF Avail (Needed)", typeof(decimal));
			inventoryTable.Columns.Add("LBS Avail (Needed)", typeof(decimal));
			inventoryTable.Columns.Add("Feet per Pound", typeof(decimal));
			bsFilms = new BindingSource();
			bsFilms.DataSource = inventoryTable;
			dgvFilm.DataSource = bsFilms;
			dgvFilm.Columns[1].Width = 200;
			dgvFilm.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
			dgvFilm.Columns[2].Width = 60;
			dgvFilm.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
			dgvFilm.Columns[3].Width = 60;
			dgvFilm.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
			for (int i = 4; i <= 11; i++)
			{
				dgvFilm.Columns[i].Width = 80;
				dgvFilm.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
				dgvFilm.Columns[i].DefaultCellStyle.Format = "N0";
			}
			
			dgvFilm.Columns[0].Visible = false;
			dgvFilm.Columns[12].Visible = false;

            allocationTable = new DataTable();
            allocationTable.Columns.Add("Film", typeof(string));
            allocationTable.Columns.Add("Width", typeof(decimal));
            allocationTable.Columns.Add("Allocation LF", typeof(decimal));
            allocationTable.Columns.Add("Allocated Pounds", typeof(decimal));
            bsAllocations = new BindingSource();
            bsAllocations.DataSource = allocationTable;
            dgvAllocations.DataSource = bsAllocations;
            dgvAllocations.Columns[0].Width = 500;
            dgvAllocations.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvAllocations.Columns[1].Width = 100;
            dgvAllocations.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvAllocations.Columns[1].DefaultCellStyle.Format = "N4";
            dgvAllocations.Columns[2].Width = 200;
            dgvAllocations.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvAllocations.Columns[2].DefaultCellStyle.Format = "N0";
            dgvAllocations.Columns[3].Width = 200;
            dgvAllocations.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvAllocations.Columns[3].DefaultCellStyle.Format = "N0";
            RefreshFilms();
        }
		
		private void RefreshFilms()
		{
            DataRow row;

            connection.Open();
            if (allocationID != 0)
            {
                allocationTable.Rows.Clear();
                command = new SqlCommand("SELECT b.[Description], a.[Width], a.[Allocated LF], a.[Allocated Pounds] FROM [Allocation Reservation Table] a INNER JOIN [Inventory Master Table] b ON a.[Master Item No] = b.[Master Item No] WHERE a.[Allocation ID] = " + allocationID + " ORDER BY a.[Priority]", connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    row = allocationTable.NewRow();
                    for (int i = 0; i <= 3; i++)
                    {
                        row[i] = reader[i];
                    }

                    allocationTable.Rows.Add(row);
                }

                reader.Close();
            }

            inventoryTable.Rows.Clear();
			command = new SqlCommand(commandText, connection);
			reader = command.ExecuteReader();
			if (reader.Read())
			{
				do
				{
					row = inventoryTable.NewRow();
					for (int i = 0; i <= 12; i++)
					{
						row[i] = reader[i];
					}
				
					inventoryTable.Rows.Add(row);
				}
				while (reader.Read());
		
				reader.Close();
				connection.Close();
				FilterData();
			}
			else
			{
				reader.Close();
				connection.Close();
				MessageBox.Show("Error - there is no film on hand nor on order available for allocation.", "No Film Found");
				if (cmdAllocate.Text == "Allocate")
				{
					cmdAllocate.Enabled = false;
				}
			}
		}
		
		private void TxtPercentOfNeedToAllocateKeyDown(object sender, KeyEventArgs e)
		{
			FormatClass.TextBoxKeyDownNumbersOnly(sender, e);
		}
		
		private void TxtPercentOfNeedToAllocateKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			FormatClass.TextBoxKeyPressNumbersOnly(e);
		}
		
		private void TxtPercentOfNeedToAllocateLeave(object sender, EventArgs e)
		{
			if (txtPercentOfNeedToAllocate.Text.Length == 0)
			{
				txtPercentOfNeedToAllocate.Text = "100";
			}
			
			UpdateAllocationInformationText();
		}
		
		private void UpdateAllocationInformationText()
		{
			if (allocationID == 0)
			{
				//No allocation record, therefore you can pick the allocation method
				cbxAllocationMethod.Enabled = true;
			}
			else
			{
				cbxAllocationMethod.Enabled = false;
			}
			
			if (cbxAllocationMethod.Text == "LF")
			{
				lblCurrentAllocationInformation.Text = "Currently you have allocated " + allocatedLF.ToString("N0") + " LF of the " + (stdInputFeet * decimal.Parse(txtPercentOfNeedToAllocate.Text, NumberStyles.Number) / 100).ToString("N0") + " LF needed";
				if (Math.Round(allocatedLF, 0) >= Math.Round(stdInputFeet * decimal.Parse(txtPercentOfNeedToAllocate.Text, NumberStyles.Number) / 100, 0))
				{
					cmdAllocate.Text = "Unallocate";
					cmdAllocate.Enabled = true;	
				}
				else
				{
					cmdAllocate.Text = "Allocate";
				}
			}
			else
			{
				lblCurrentAllocationInformation.Text = "Currently you have allocated " + allocatedPounds.ToString("N0") + " pounds of the " + (stdInputPounds * decimal.Parse(txtPercentOfNeedToAllocate.Text, NumberStyles.Number) / 100).ToString("N0") + " pounds needed";
				if (Math.Round(allocatedPounds, 0) >=  Math.Round(stdInputPounds * decimal.Parse(txtPercentOfNeedToAllocate.Text, NumberStyles.Number) / 100, 0))
				{
					cmdAllocate.Text = "Unallocate";
					cmdAllocate.Enabled = true;
				}
				else
				{
					cmdAllocate.Text = "Allocate";
				}
			}

		}
		
		void TxtPercentOfNeedToAllocateEnter(object sender, System.EventArgs e)
		{
			if (decimal.Parse(((TextBox)sender).Text, NumberStyles.Number) == 0)
			{
				((TextBox)sender).Text = string.Empty;
			}
			
			((TextBox)sender).SelectAll();
		}
		
		void CbxAllocationMethodSelectedIndexChanged(object sender, EventArgs e)
		{	
			UpdateAllocationInformationText();
		}
		
		void CmdAllocateClick(object sender, EventArgs e)
		{
			if (cmdAllocate.Text == "Allocate")
			{
				Allocate(dgvFilm.CurrentCell.RowIndex);
			}
			else
			{
				// Unallocate
				command = new SqlCommand("UPDATE [Allocation Master Table] SET [Voided By] = '" + StartupForm.UserName + "', [Void Date] = GETDATE() WHERE [Allocation ID] = " + allocationID.ToString(), connection);
				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();
				cmdAllocate.Text = "Allocate";
				allocationID = 0;
				nextPriority = 1;
				allocatedLF = 0;
				allocatedPounds = 0;
			}
			
			UpdateAllocationInformationText();
			RefreshFilms();
		}
		
		void dgvFilmCellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			Allocate(dgvFilm.CurrentCell.RowIndex);
			UpdateAllocationInformationText();
			RefreshFilms();
		}
		
		private void Allocate(int rowNumber)
		{
			string allocationType;
			if (cbxAllocationMethod.Text == "LF" && allocatedLF + (decimal)dgvFilm[8, rowNumber].Value >= stdInputFeet * decimal.Parse(txtPercentOfNeedToAllocate.Text, NumberStyles.Number) / 100)
			{
				allocationType = "Complete";
			}
			else if (cbxAllocationMethod.Text == "LBS" && allocatedPounds + (decimal)dgvFilm[9, rowNumber].Value >= stdInputPounds * decimal.Parse(txtPercentOfNeedToAllocate.Text, NumberStyles.Number) / 100)
			{
				allocationType = "Complete";
			}
			else
			{
				OptionsForm allocationOptionsForm = new OptionsForm("Allocation Options", true, true);
				if (cbxAllocationMethod.Text == "LF")
				{
					if ((decimal)dgvFilm[8, rowNumber].Value > 0)
					{
						allocationOptionsForm.AddOption("Allocate only the available in stock amount of " + ((decimal)dgvFilm[8, rowNumber].Value).ToString("N0") + " LF for a partial allocation");
					}

					if (allocatedLF + (decimal)dgvFilm[10, rowNumber].Value >= stdInputFeet * decimal.Parse(txtPercentOfNeedToAllocate.Text, NumberStyles.Number) / 100)
					{
						allocationOptionsForm.AddOption("Allocate enough available LF for a complete allocation");
					}
					else
					{
						if ((decimal)dgvFilm[4, rowNumber].Value > 0 && (decimal)dgvFilm[10, rowNumber].Value > 0)
						{
							allocationOptionsForm.AddOption("Allocate the available " + ((decimal)dgvFilm[10, rowNumber].Value).ToString("N0") + " LF for a partial allocation");
						}
						
						allocationOptionsForm.AddOption("Allocate enough LF for a complete allocation (you will need to create a PO)");
					}
				}
				else
				{
					if ((decimal)dgvFilm[9, rowNumber].Value > 0)
					{
						allocationOptionsForm.AddOption("Allocate only the available in stock amount of " + ((decimal)dgvFilm[9, rowNumber].Value).ToString("N0") + " LBS for a partial allocation");					}
					
					if (allocatedPounds + (decimal)dgvFilm[11, rowNumber].Value >= stdInputPounds * decimal.Parse(txtPercentOfNeedToAllocate.Text, NumberStyles.Number) / 100)
					{
						allocationOptionsForm.AddOption("Allocate enough available LBS for a complete allocation");
					}
					else
					{
						if ((decimal)dgvFilm[5, rowNumber].Value > 0 && (decimal)dgvFilm[11, rowNumber].Value > 0)
						{
							allocationOptionsForm.AddOption("Allocate the available " + ((decimal)dgvFilm[11, rowNumber].Value).ToString("N0") + " LBS for a partial allocation");
						}
						
						allocationOptionsForm.AddOption("Allocate enough LBS for a complete allocation (you will need to create a PO)");
					}
				}
				
				allocationOptionsForm.ShowDialog();
				allocationType = allocationOptionsForm.Option;
				allocationOptionsForm.Dispose();
			}
			
			if (allocationType != "Abort")
			{
                DialogResult answer = DialogResult.Yes;
                bool excessTrim = false;
                if ((decimal)dgvFilm[2, rowNumber].Value > filmWidth + (decimal).249)
                {
                    answer = MessageBox.Show("The film you are choosing is wider than the requested width.  Are you sure you wish to use this film?", "Wider Film then Necessary Picked", MessageBoxButtons.YesNo);
                    excessTrim = true;
                }
                else if ((decimal)dgvFilm[2, rowNumber].Value < printWidth)
                {
                    MessageBox.Show("Error - the film you are attempting to pick is less than the print width", "Invalid Film Width");
                    answer = DialogResult.No;
                }
                else if ((decimal)dgvFilm[2, rowNumber].Value < filmWidth)
                {
                    answer = MessageBox.Show("The film you are allocating is less than the requested width.  Please make sure the film is wide enough for this job - i.e. atleast has 1/2\" trim if it is to be laminated", "Film narrower than Requested", MessageBoxButtons.YesNo);
                }

                if (answer == DialogResult.Yes)
                {
                    if (allocationID == 0)
                    {
                        command = new SqlCommand("EXECUTE [Create Allocation Master Record Stored Procedure] " + masterItemNumber.ToString() + ", " + laminationFilmNumber + ", '" + cbxAllocationMethod.Text + "', " + (decimal.Parse(txtPercentOfNeedToAllocate.Text, NumberStyles.Number) / 100 - 1).ToString() + ", '" + StartupForm.UserName + "'", connection);
                        connection.Open();
                        allocationID = (int)command.ExecuteScalar();
                        connection.Close();
                    }

                    string sample = "0";
                    if (dgvFilm[3, rowNumber].Value.ToString() == "Yes")
                    {
                        sample = "1";
                    }

                    if (allocationType == "Complete")
                    {
                        // Allocate completely and there is enough inventory available
                        if (cbxAllocationMethod.Text == "LF")
                        {
                            command = new SqlCommand("insert into [Allocation Reservation Table] SELECT " + allocationID.ToString() + ", " + nextPriority.ToString() + ", " + filmCustomerSupplied.ToString() + ", " + sample + ", " + ((int)dgvFilm[0, rowNumber].Value).ToString() + ", " + ((decimal)dgvFilm[2, rowNumber].Value).ToString() + ", " + (stdInputFeet * decimal.Parse(txtPercentOfNeedToAllocate.Text, NumberStyles.Number) / 100 - allocatedLF).ToString() + ", " + ((decimal)dgvFilm[11, rowNumber].Value * ((decimal)stdInputFeet * decimal.Parse(txtPercentOfNeedToAllocate.Text, NumberStyles.Number) / 100 - allocatedLF) / (decimal)dgvFilm[10, rowNumber].Value).ToString(), connection);
                            allocatedPounds += (decimal)dgvFilm[11, rowNumber].Value * ((decimal)stdInputFeet * decimal.Parse(txtPercentOfNeedToAllocate.Text, NumberStyles.Number) / 100 - allocatedLF) / (decimal)dgvFilm[10, rowNumber].Value;
                            allocatedLF = stdInputFeet * decimal.Parse(txtPercentOfNeedToAllocate.Text, NumberStyles.Number) / 100;
                        }
                        else
                        {
                            command = new SqlCommand("insert into [Allocation Reservation Table] SELECT " + allocationID.ToString() + ", " + nextPriority.ToString() + ", " + filmCustomerSupplied.ToString() + ", " + sample + ", " + ((int)dgvFilm[0, rowNumber].Value).ToString() + ", " + ((decimal)dgvFilm[2, rowNumber].Value).ToString() + ", " + ((decimal)dgvFilm[10, rowNumber].Value * ((decimal)stdInputPounds * decimal.Parse(txtPercentOfNeedToAllocate.Text, NumberStyles.Number) / 100 - allocatedPounds) / (decimal)dgvFilm[11, rowNumber].Value).ToString() + ", " + (stdInputPounds * decimal.Parse(txtPercentOfNeedToAllocate.Text, NumberStyles.Number) / 100 - allocatedPounds).ToString(), connection);
                            allocatedLF += (decimal)dgvFilm[10, rowNumber].Value * ((decimal)stdInputPounds * decimal.Parse(txtPercentOfNeedToAllocate.Text, NumberStyles.Number) / 100 - allocatedPounds) / (decimal)dgvFilm[11, rowNumber].Value;
                            allocatedPounds = stdInputPounds * decimal.Parse(txtPercentOfNeedToAllocate.Text, NumberStyles.Number) / 100;
                        }
                    }
                    else if (allocationType.Substring(0, 15) == "Allocate enough")
                    {
                        // Allocate completely but there isn't enough available.  Use standard yield to calculate the amounts needed.
                        if (cbxAllocationMethod.Text == "LF")
                        {
                            command = new SqlCommand("insert into [Allocation Reservation Table] SELECT " + allocationID.ToString() + ", " + nextPriority.ToString() + ", " + filmCustomerSupplied.ToString() + ", " + sample + ", " + ((int)dgvFilm[0, rowNumber].Value).ToString() + ", " + ((decimal)dgvFilm[2, rowNumber].Value).ToString() + ", " + (stdInputFeet * decimal.Parse(txtPercentOfNeedToAllocate.Text, NumberStyles.Number) / 100 - allocatedLF).ToString() + ", " + ((stdInputFeet * decimal.Parse(txtPercentOfNeedToAllocate.Text, NumberStyles.Number) / 100 - allocatedLF) / (decimal)dgvFilm[12, rowNumber].Value).ToString(), connection);
                            allocatedPounds += (stdInputFeet * decimal.Parse(txtPercentOfNeedToAllocate.Text, NumberStyles.Number) / 100 - allocatedLF) / (decimal)dgvFilm[12, rowNumber].Value;
                            allocatedLF = stdInputFeet * decimal.Parse(txtPercentOfNeedToAllocate.Text, NumberStyles.Number) / 100;
                        }
                        else
                        {
                            command = new SqlCommand("insert into [Allocation Reservation Table] SELECT " + allocationID.ToString() + ", " + nextPriority.ToString() + ", " + filmCustomerSupplied.ToString() + ", " + sample + ", " + ((int)dgvFilm[0, rowNumber].Value).ToString() + ", " + ((decimal)dgvFilm[2, rowNumber].Value).ToString() + ", " + ((stdInputPounds * decimal.Parse(txtPercentOfNeedToAllocate.Text, NumberStyles.Number) / 100 - allocatedPounds) * (decimal)dgvFilm[12, rowNumber].Value).ToString() + ", " + (stdInputPounds * decimal.Parse(txtPercentOfNeedToAllocate.Text, NumberStyles.Number) / 100 - allocatedPounds).ToString(), connection);
                            allocatedLF += (stdInputPounds * decimal.Parse(txtPercentOfNeedToAllocate.Text, NumberStyles.Number) / 100 - allocatedPounds) * (decimal)dgvFilm[12, rowNumber].Value;
                            allocatedPounds = stdInputPounds * decimal.Parse(txtPercentOfNeedToAllocate.Text, NumberStyles.Number) / 100;
                        }
                    }
                    else if (allocationType.Substring(0, 13) == "Allocate only")
                    {
                        // Allocate only the in stock inventory
                        command = new SqlCommand("insert into [Allocation Reservation Table] SELECT " + allocationID.ToString() + ", " + nextPriority.ToString() + ", " + filmCustomerSupplied.ToString() + ", " + sample + ", " + ((int)dgvFilm[0, rowNumber].Value).ToString() + ", " + ((decimal)dgvFilm[2, rowNumber].Value).ToString() + ", " + ((decimal)dgvFilm[8, rowNumber].Value).ToString() + ", " + ((decimal)dgvFilm[9, rowNumber].Value).ToString(), connection);
                        allocatedLF += (decimal)dgvFilm[8, rowNumber].Value;
                        allocatedPounds += (decimal)dgvFilm[9, rowNumber].Value;
                    }
                    else
                    {
                        // Allocate all available inventory/on order quantity
                        command = new SqlCommand("insert into [Allocation Reservation Table] SELECT " + allocationID.ToString() + ", " + nextPriority.ToString() + ", " + filmCustomerSupplied.ToString() + ", " + sample + ", " + ((int)dgvFilm[0, rowNumber].Value).ToString() + ", " + ((decimal)dgvFilm[2, rowNumber].Value).ToString() + ", " + ((decimal)dgvFilm[10, rowNumber].Value).ToString() + ", " + ((decimal)dgvFilm[11, rowNumber].Value).ToString(), connection);
                        allocatedLF += (decimal)dgvFilm[10, rowNumber].Value;
                        allocatedPounds += (decimal)dgvFilm[11, rowNumber].Value;
                    }

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    if (excessTrim)
                    {
                        ModulesClass.ExcessTrimReason(allocationID.ToString(), nextPriority.ToString());
                    }

                    nextPriority++;
                }
			}
		}
		
		private void FilterData()
		{
			if (ckShowAllFilms.Checked)
			{
				inventoryTable.DefaultView.RowFilter = string.Empty;
				cmdAllocate.Enabled = true;
			}
			else
			{
				if (printWidth != 0)
				{	
					inventoryTable.DefaultView.RowFilter = "[Width] >= " + printWidth.ToString();
				}
				else
				{
					inventoryTable.DefaultView.RowFilter = "[Width] >= " + filmWidth.ToString();
				}
				
				if (dgvFilm.Rows.Count == 0)
				{
					cmdAllocate.Enabled = false;
				}
			}
		}
		
		private void CkShowAllFilmsCheckedChanged(object sender, EventArgs e)
		{
			FilterData();
		}
    }
}
