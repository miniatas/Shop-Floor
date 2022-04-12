using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopFloor
{
    public partial class RAForm : Form
    {
        private string raNumber;
        private SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
        private SqlDataReader reader;
        private SqlCommand command;
        string returnReason;
        string job;
        string masterItemNumber;
        string item;
        string prefix;
        string returnUnit;
        string returnUnitTableFieldName;
        decimal linearFeetperUnit;
        string lookupUnit;
        string preEditValue;

        public RAForm(string raNo)
        {
            InitializeComponent();
            raNumber = raNo;
            connection.Open();
            fillForm();
            connection.Close();
        }

        private void cmdReturnItem_Click(object sender, EventArgs e)
        {
            DialogResult moreItems = DialogResult.Yes;
            GetInputForm ItemForm = new GetInputForm("Scan/Input " + item + " ID", prefix, 0, 0, false);
            while (moreItems == DialogResult.Yes)
            {
                ItemForm.ShowDialog();
                if (ItemForm.UserInput.Length > 0)
                {
                    command = new SqlCommand("SELECT a.[Master Item No], b.[Reference Item No], b.[Description], ISNULL(c.[Location ID], a.[Location ID]), d.[Description], CAST(a.[" + lookupUnit + "] AS int), ISNULL(e.[RA Number], 0), ISNULL(a.[Pallet ID], 0) FROM [" + item + " Table] a INNER JOIN [Inventory Master Table] b ON a.[Master Item No] = b.[Master Item No] LEFT JOIN [Pallet Table] c ON a.[Pallet ID] = c.[Pallet ID] LEFT JOIN [RA " + item + " Table] e ON a.[" + item + " ID] = e.[" + item + " ID] AND e.[Void Date] IS NULL, [Location Table] d WHERE a.[" + item + " ID] = " + ItemForm.UserInput.Substring(1) + " AND ISNULL(c.[Location ID], a.[Location ID]) = d.[Location ID]", connection);
                    connection.Open();
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        if ((int)reader[6] != 0)
                        {
                            MessageBox.Show(item + " " + ItemForm.UserInput + " of Job " + reader[1].ToString() + " " + reader[2].ToString() + " already has been returned on RA " + reader[6].ToString() + ".  You must change or void the return", item + " Already Returned");
                            reader.Close();
                        }
                        else if (reader[0].ToString() != masterItemNumber)
                        {
                            MessageBox.Show(item + " " + ItemForm.UserInput + " of Job " + reader[1].ToString() + " " + reader[2].ToString() + " is not the correct item for this RA.", "Wrong Item");
                            reader.Close();
                        }
                        else if (reader[3].ToString() != "9999")
                        {
                            MessageBox.Show(item + " " + ItemForm.UserInput + " is not marked as shipped and is at location L" + reader[3].ToString() + " " + reader[4].ToString() + ".", item + " not Shipped");
                            reader.Close();
                        }
                        else
                        {
                            string palletID;
                            if (reader[7].ToString() == "0")
                            {
                                palletID = "NULL";
                            }
                            else
                            {
                                palletID = reader[7].ToString();
                            }

                            GetInputForm UnitsReturnedForm = new GetInputForm(returnUnit + " Returned", "#", 0, (int)reader[5], false);
                            UnitsReturnedForm.UserInput = reader[5].ToString();
                            reader.Close();
                            UnitsReturnedForm.ShowDialog();
                            if (!string.IsNullOrEmpty(UnitsReturnedForm.UserInput) && int.Parse(UnitsReturnedForm.UserInput, System.Globalization.NumberStyles.Number) > 0)
                            {
                                command = new SqlCommand("INSERT INTO [RA " + item + " Table] SELECT " + raNumber + ", '" + StartupForm.UserName + "', GETDATE(), " + palletID + ", " + ItemForm.UserInput.Substring(1) + ", " + UnitsReturnedForm.UserInput + ", NULL, NULL, NULL", connection);
                                command.ExecuteNonQuery();
                                dgvRAItems.Rows.Add(ItemForm.UserInput, (int.Parse(UnitsReturnedForm.UserInput, System.Globalization.NumberStyles.Number)).ToString("N0"), palletID);
                                dgvRAItems.FirstDisplayedScrollingRowIndex = dgvRAItems.RowCount - 1;
                                DialogResult answer = MessageBox.Show("Do you wish to re-print " + item + " " + ItemForm.UserInput + "'s Label?", "Print Label?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                                if (answer == DialogResult.Yes)
                                {
                                    PrintClass.Label(ItemForm.UserInput);
                                }
                            }

                            UnitsReturnedForm.Close();
                        }

                        connection.Close();
                    }
                    else
                    {
                        reader.Close();
                        connection.Close();
                        MessageBox.Show("Error - " + ItemForm.UserInput + " not Found.", "Not Found");
                    }

                    moreItems = MessageBox.Show("Do you wish to return more existing " + item + "s?", "Return More?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    ItemForm.UserInput = string.Empty;
                }
                else
                {
                    moreItems = DialogResult.No;
                }
            }

            ItemForm.Close();
        }

        private void cmdCreateItem_Click(object sender, EventArgs e)
        {
            DialogResult moreItems = DialogResult.Yes;
            int numberOfItemsToCreate = 0;
            UnitInformationForm NewItemForm = new UnitInformationForm("Returned " + returnUnit + " and Pounds", job, 0, true);
            NewItemForm.UnitName = returnUnit;
            NewItemForm.Notes = returnReason;
            while (moreItems == DialogResult.Yes)
            {
                NewItemForm.ShowDialog();
                if (NewItemForm.Units > 0 && NewItemForm.Pounds > 0)
                {
                    GetInputForm numberOfItemsToCreateForm = new GetInputForm("Number of " + item + "s", "#", 1, 200, false);
                    numberOfItemsToCreateForm.UserInput = "1";
                    numberOfItemsToCreateForm.ShowDialog();
                    if (numberOfItemsToCreateForm.UserInput.Length > 0)
                    {
                        numberOfItemsToCreate = int.Parse(numberOfItemsToCreateForm.UserInput, NumberStyles.Number);
                    }
                    else
                    {
                        numberOfItemsToCreate = 0;
                    }

                    numberOfItemsToCreateForm.Dispose();
                }

                int itemNumber;
                connection.Open();
                for (int i = 1; i <= numberOfItemsToCreate; i++)
                {
                    
                    if (string.IsNullOrEmpty(NewItemForm.Notes))
                    {
                        command = new SqlCommand("EXECUTE [Add Created " + item + " Stored Procedure] " + masterItemNumber + ", '" + StartupForm.UserName + "', " + Math.Round((decimal)NewItemForm.Units * linearFeetperUnit, 0).ToString() + ", " + NewItemForm.Pounds.ToString() + ", NULL, 'Customer Return', NULL", connection);
                        itemNumber = (int)command.ExecuteScalar();
                        command = new SqlCommand("INSERT INTO [RA " + item + " Table] SELECT " + raNumber + ", '" + StartupForm.UserName + "', GETDATE(), NULL, " + itemNumber.ToString() + ", " + NewItemForm.Units.ToString() + ", NULL, NULL, NULL", connection);
                        dgvRAItems.Rows.Add(prefix + itemNumber.ToString(), NewItemForm.Units.ToString("N0"), "0");
                    }
                    else
                    {
                        command = new SqlCommand("EXECUTE [Add Created " + item + " Stored Procedure] " + masterItemNumber + ", '" + StartupForm.UserName + "', " + Math.Round((decimal)NewItemForm.Units * linearFeetperUnit, 0).ToString() + ", " + NewItemForm.Pounds.ToString() + ", NULL, 'Customer Return', '" + NewItemForm.Notes.Replace("'", "''") + "'", connection);
                        itemNumber = (int)command.ExecuteScalar();
                        command = new SqlCommand("INSERT INTO [RA " + item + " Table] SELECT " + raNumber + ", '" + StartupForm.UserName + "', GETDATE(), NULL, " + itemNumber.ToString() + ", " + NewItemForm.Units.ToString() + ", '" + NewItemForm.Notes.Replace("'", "''") + "', NULL, NULL", connection);
                        dgvRAItems.Rows.Add(prefix + itemNumber.ToString(), NewItemForm.Units.ToString("N0"), "0", NewItemForm.Notes);
                    }

                    command.ExecuteNonQuery();
                    dgvRAItems.FirstDisplayedScrollingRowIndex = dgvRAItems.RowCount - 1;
                    PrintClass.Label(prefix + itemNumber.ToString());
                }

                connection.Close();
                moreItems = MessageBox.Show("Do you wish to return more existing " + item + "s?", "Return More?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }

            NewItemForm.Dispose();
        }

        private void cmdMoveItem_Click(object sender, EventArgs e)
        {
            GetInputForm readBarcodeForm = new GetInputForm("Scan/Input Barcode", "R", 0, 0, false);
            readBarcodeForm.ShowDialog();
            if (readBarcodeForm.UserInput.Length > 0)
            {
                if (readBarcodeForm.UserInput.Substring(0, 1) == "R")
                {
                    command = new SqlCommand("SELECT a.[Master Item No], a.[Width], '(' + CAST(b.[Reference Item No] AS nvarchar(10)) + ') ' + b.[Description], ISNULL(a.[Location ID], d.[Location ID]), CAST(ROUND(a.[Current LF], 0) AS int), CAST(ROUND(e.[Start Usage LF], 0) AS int), ISNULL(a.[Pallet ID], 0), c.[Description] FROM [Roll Table] a INNER JOIN ([Inventory Master Table] b INNER JOIN [Item Type Table] c ON b.[Item Type No] = c.[Item Type No]) ON a.[Master Item No] = b.[Master Item No] LEFT JOIN [Pallet Table] d ON a.[Pallet ID] = d.[Pallet ID] LEFT JOIN [Production Consumed Roll Table] e ON a.[Roll ID] = e.[Roll ID] AND e.[End Usage LF] IS NULL, [Location Table] f WHERE ISNULL(a.[Location ID], d.[Location ID]) = f.[Location ID] AND a.[Roll ID] = " + readBarcodeForm.UserInput.Substring(1) + " AND (a.[Current LF] > 0 OR ISNULL(a.[Location ID], d.[Location ID]) = " + MainForm.MachineNumber + ")", connection);
                    connection.Open();
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        if (reader[3].ToString() == MainForm.MachineNumber && (int)reader[4] == 0 && (int)reader[5] > 0)
                        {
                            MessageBox.Show("This roll is now or has been consumed on machine " + MainForm.MachineNumber + ".  You must go into production and return it.", "Invalid Roll");
                        }
                        else
                        {
                            ModulesClass.MoveRoll(readBarcodeForm.UserInput, (int)reader[0], (decimal)reader[1], reader[2].ToString(), (int)reader[6], reader[7].ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error - roll " + readBarcodeForm.UserInput + " not found", "Roll Not Found");
                    }

                    reader.Close();
                    connection.Close();
                }
                else
                {
                    // Move Case
                    command = new SqlCommand("SELECT a.[Master Item No], '(' + CAST(b.[Reference Item No] AS nvarchar(10)) + ') ' + b.[Description], ISNULL(a.[Pallet ID], 0), c.[Description] FROM [Case Table] a INNER JOIN ([Inventory Master Table] b INNER JOIN [Item Type Table] c ON b.[Item Type No] = c.[Item Type No]) ON a.[Master Item No] = b.[Master Item No] LEFT JOIN [Pallet Table] d ON a.[Pallet ID] = d.[Pallet ID], [Location Table] e WHERE ISNULL(a.[Location ID], d.[Location ID]) = e.[Location ID] AND a.[Case ID] = " + readBarcodeForm.UserInput.Substring(1), connection);
                    connection.Open();
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        ModulesClass.MoveCase(readBarcodeForm.UserInput, (int)reader[0], reader[1].ToString(), (int)reader[2], reader[3].ToString());
                    }
                    else
                    {
                        MessageBox.Show("Error - case " + readBarcodeForm.UserInput + " not found", "Case Not Found");
                    }

                    reader.Close();
                    connection.Close();
                }
            }

            readBarcodeForm.Dispose();
        }

        private void dgvRAItems_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvRAItems.CurrentCell.Value != null)
            {
                preEditValue = dgvRAItems.CurrentCell.Value.ToString();
            }
            else
            {
                preEditValue = string.Empty;
            }
        }

        private void dgvRAItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
            if (dgvRAItems.CurrentCell.Value == null) 
            {
                if (e.ColumnIndex == 1)
                {
                    dgvRAItems.CurrentCell.Value = "0";
                }
                else //e.columnIndex = 3
                {
                    dgvRAItems.CurrentCell.Value = string.Empty;
                }
            }

            if (dgvRAItems.CurrentCell.Value.ToString() != preEditValue)
            {
                if (e.ColumnIndex == 1)
                {
                    int result;
                    if (int.TryParse(dgvRAItems.CurrentCell.Value.ToString(), out result))
                    {
                        if (result < 0)
                        {
                            dgvRAItems.CancelEdit();
                            dgvRAItems.CurrentCell.Value = preEditValue;
                        }
                        else
                        {
                            int noUsageRecords = 0;
                            connection.Open();
                            if (item == "Roll") //At some point will be able to "Consume" Bags, but not yet
                            {
                                command = new SqlCommand("SELECT COUNT(*) FROM [Production Consumed Roll Table] WHERE [Start Usage LF] != [End Usage LF] AND [Roll ID] = " + dgvRAItems[0, e.RowIndex].Value.ToString().Substring(1), connection);
                                noUsageRecords = (int)command.ExecuteScalar();
                            }

                            if (noUsageRecords == 0)
                            {
                                command = new SqlCommand("UPDATE [RA " + item + " Table] SET [Voided By] = '" + StartupForm.UserName + "', [Void Date] = GETDATE() WHERE [Void Date] IS NULL AND [RA Number] = " + raNumber + " AND [" + item + " ID] = " + dgvRAItems[0, e.RowIndex].Value.ToString().Substring(1), connection);
                                command.ExecuteNonQuery();
                                if (result == 0)
                                { 
                                    BeginInvoke(new MethodInvoker(delegate
                                    {
                                        dgvRAItems.Rows.RemoveAt(e.RowIndex);
                                    }));
                                }
                                else
                                {
                                    string palletID;
                                    if (dgvRAItems[2, e.RowIndex] == null)
                                    {
                                        palletID = "NULL";
                                    }
                                    else
                                    {
                                        palletID = dgvRAItems[2, e.RowIndex].Value.ToString();
                                    }

                                    if (dgvRAItems[3, e.RowIndex].Value == null) // There is no comment
                                    {
                                        command = new SqlCommand("INSERT INTO [RA " + item + " Table] SELECT " + raNumber + ", '" + StartupForm.UserName + "', GETDATE(), " + palletID + ", " + dgvRAItems[0, e.RowIndex].Value.ToString().Substring(1) + ", " + dgvRAItems[1, e.RowIndex].Value.ToString() + ", NULL, NULL, NULL", connection);
                                    }
                                    else
                                    {
                                        command = new SqlCommand("INSERT INTO [RA " + item + " Table] SELECT " + raNumber + ", '" + StartupForm.UserName + "', GETDATE(), " + palletID + ", " + dgvRAItems[0, e.RowIndex].Value.ToString().Substring(1) + ", " + dgvRAItems[1, e.RowIndex].Value.ToString() + ", '" + dgvRAItems[3, e.RowIndex].Value.ToString() + "', NULL, NULL", connection);
                                    }

                                    command.ExecuteNonQuery();
                                    dgvRAItems.CurrentCell.Value = (int.Parse(dgvRAItems.CurrentCell.Value.ToString(), NumberStyles.Number)).ToString("N0");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Error - you cannot edit a " + item + " after it has a consumption record, which " + prefix + dgvRAItems[0, e.RowIndex].ToString() + " has.", "Cannot Change Return");
                            }

                            connection.Close();
                        }
                    }
                    else
                    {
                        dgvRAItems.CancelEdit();
                        dgvRAItems.CurrentCell.Value = preEditValue;
                    }
                }
                else if (e.ColumnIndex == 3 && dgvRAItems.CurrentCell.Value.ToString() != preEditValue)
                {
                    command = new SqlCommand("UPDATE [RA " + item + " Table] SET [Comments] = '" + dgvRAItems[3, e.RowIndex].Value.ToString().Replace("'", "''") + "' WHERE [Void Date] IS NULL AND [RA Number] = " + raNumber + " AND [" + item + " ID] = " + dgvRAItems[0, e.RowIndex].Value.ToString().Substring(1), connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        private void cmdChangeJob_Click(object sender, EventArgs e)
        {
            GetInputForm readInput = new GetInputForm("Scan/Input Job Jacket Number", "J", 0, 0, true);
            readInput.ShowDialog();
            if (readInput.UserInput.Length != 0 && readInput.UserInput.Length != 7)
            {
                MessageBox.Show("Error - the job jacket number must be entered as \"J\" follored by 6 digits.", "Invalid Entry");
                readInput.Close();
            }
            else if (readInput.UserInput.Length == 7)
            {
                DialogResult updateRA = DialogResult.No;
                command = new SqlCommand("SELECT COUNT(b.[RA Number]), a.[Description] FROM [Inventory Master Table] a LEFT JOIN [RA Master Table] b ON a.[Master Item No] = b.[Master Item No] WHERE a.[Item Type No] = 4 AND a.[Reference Item No] = " + readInput.UserInput.Substring(1) + " GROUP BY a.[Description]", connection);
                connection.Open();
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    int raCountForJob = (int)reader[0];
                    string jobDescription = reader[1].ToString();
                    reader.Close();
                    command = new SqlCommand("SELECT a.[RA Number], a.[Created By], a.[Create Date], a.[Description], ISNULL(a.[Master RA Number], 0) FROM [RA Master Table] a INNER JOIN [Inventory Master Table] b ON a.[Master Item No] = b.[Master Item No] WHERE b.[Reference Item No] = " + readInput.UserInput.Substring(1) + " order by a.[RA Number]", connection);
                    reader = command.ExecuteReader();
                    reader.Read();
                    if (raCountForJob > 0)
                    {
                        if (raCountForJob == 1)
                        {
                            if ((int)reader[4] == 0)
                            {
                                updateRA = MessageBox.Show("Do you wish to edit RA " + reader[0].ToString() + " created by " + reader[1].ToString() + " on " + ((DateTime)reader[2]).ToShortDateString() + " for " + reader[3].ToString() + "?", "Update Existing RA?", MessageBoxButtons.YesNo);
                            }
                            else
                            {
                                updateRA = MessageBox.Show("Do you wish to edit RA " + reader[0].ToString() + " of Master RA" + reader[4].ToString() + "  created by " + reader[1].ToString() + " on " + ((DateTime)reader[2]).ToShortDateString() + " for " + reader[3].ToString() + "?", "Update Existing RA?", MessageBoxButtons.YesNo);
                            }

                            if (updateRA == DialogResult.Yes)
                            {
                                raNumber = reader[0].ToString();
                                reader.Close();
                                fillForm();
                            }
                            else
                            {
                                reader.Close();
                            }
                        }
                        else
                        {
                            OptionsForm pickRAForm = new OptionsForm("Existing RA's for " + readInput.UserInput + " " + reader[0].ToString(), true, true);
                            pickRAForm.Width = 700;
                            do
                            {
                                if ((int)reader[4] == 0)
                                {
                                    pickRAForm.AddOption("RA " + reader[0].ToString() + "  by " + reader[1].ToString() + " on " + ((DateTime)reader[2]).ToShortDateString() + " for " + reader[3].ToString());
                                }
                                else
                                {
                                    pickRAForm.AddOption("RA " + reader[0].ToString() + "of Master RA" + reader[4].ToString() + "  by " + reader[1].ToString() + " on " + ((DateTime)reader[2]).ToShortDateString() + " for " + reader[3].ToString());
                                }

                            }
                            while (reader.Read());

                            reader.Close();
                            pickRAForm.ShowDialog();
                            if (pickRAForm.Option != "Abort")
                            {
                                raNumber = pickRAForm.Option.Substring(3).Substring(0, pickRAForm.Option.IndexOf(" "));
                                fillForm();
                            }

                            pickRAForm.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No RA Exists for job " + readInput.UserInput + ".  You must exit the RA Module back to the main menu and create a new RA.", "No Existing RA");
                        reader.Close();
                    }
                }
                else
                {
                    reader.Close();
                    MessageBox.Show("Error - job " + readInput.UserInput + " not found.", "Invalid Job No");
                }

                connection.Close();
            }

            readInput.Dispose();
        }

        private void fillForm()
        {
            command = new SqlCommand("SELECT c.[CustName], b.[Job Jacket No], e.[Description], a.[Description], e.[Master Item No], d.[Reference Item No], b.[UOM], b.[Standard Finished Goods Linear Feet] / b.[Order Quantity], ISNULL(a.[Master RA Number], 0) FROM [RA Master Table] a INNER JOIN ([Finished Goods Specification Table] b INNER JOIN [JobJackets].[dbo].[tblCustomer] c ON b.[Cust ID] = c.[CustID] INNER JOIN [Inventory Master Table] d ON b.[Input Master Item No] = d.[Master Item No]) ON a.[Master Item No] = b.[Master Item No] INNER JOIN [Inventory Master Table] e ON a.[Master Item No] = e.[Master Item No] WHERE a.[RA Number] = " + raNumber, connection);
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                if ((int)reader[8] != 0)
                {
                    rtbRAInfo.Text = "Master RA Number: " + reader[8].ToString() + "\r\n";
                }
                rtbRAInfo.Text = rtbRAInfo.Text + "RA Number: " + raNumber + "\r\nCustomer: " + reader[0].ToString() + "\r\nJob " + reader[1].ToString() + " " + reader[2].ToString() + "\r\n\r\n" + reader[3].ToString();
                returnReason = reader[3].ToString();
                job = reader[1].ToString() + " " + reader[2].ToString();
                masterItemNumber = reader[4].ToString();
                if (reader[5].ToString().EndsWith("51"))
                {
                    item = "Case";
                    prefix = "C";
                    returnUnit = "Bags";
                    returnUnitTableFieldName = "Bags";
                    linearFeetperUnit = 1; //Case Table does not use LF, so set to 1 for Bags
                    lookupUnit = "Original Bags";
                }
                else
                {
                    item = "Roll";
                    prefix = "R";
                    returnUnit = reader[6].ToString();
                    returnUnitTableFieldName = "Units";
                    linearFeetperUnit = (decimal)reader[7];
                    lookupUnit = "Original Units";
                }

                reader.Close();
                dgvRAItems.Rows.Clear();
                dgvRAItems.Columns[0].HeaderText = item + " ID";
                dgvRAItems.Columns[1].HeaderText = returnUnit;
                cmdReturnExistingItem.Text = "&Return RA " + item;
                cmdCreateItem.Text = "&Create RA " + item;
                cmdMoveItem.Text = "&Move " + item;
                command = new SqlCommand("SELECT '" + prefix + "' +  CAST([" + item + " ID] AS nvarchar(10)), [Returned " + returnUnitTableFieldName + "], ISNULL([Pallet ID], 0), [Comments] FROM [RA " + item + " Table] WHERE [RA Number] = " + raNumber + " AND [Void Date] IS NULL ORDER BY [" + item + " ID]", connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (string.IsNullOrEmpty(reader[3].ToString()))
                    {
                        dgvRAItems.Rows.Add(reader[0].ToString(), ((decimal)reader[1]).ToString("N0"), reader[2].ToString());
                    }
                    else
                    {
                        dgvRAItems.Rows.Add(reader[0].ToString(), ((decimal)reader[1]).ToString("N0"), reader[2].ToString(), reader[3].ToString());
                    }
                }

                if (dgvRAItems.RowCount > 0)
                {
                    dgvRAItems.FirstDisplayedScrollingRowIndex = dgvRAItems.RowCount - 1;
                }

                reader.Close();
            }
        }
    }
}
