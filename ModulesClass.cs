/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 7/26/2011
 * Time: 3:18 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace ShopFloor
{
   
    using System;
    using System.Globalization;
    using System.Data;
    using System.Data.SqlClient;
    using System.Windows.Forms;
    using System.Net.Mail;
    using System.Text;

    /// <summary>
    /// Library of Procedures accessed by muliple forms.
    /// </summary>
    public static class ModulesClass
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        public static DialogResult MoveRoll(string rollNumber, int masterItemNumber, decimal width, string itemDescription, int palletNumber, string partType)
        {
            DialogResult answer = DialogResult.No;
            GetInputForm readLocationForm = new GetInputForm("Scan/Input Location or Pallet Id", "L", 0, 0, false);
            readLocationForm.ShowDialog();
            if (readLocationForm.UserInput.Length > 0)
            {

                if (partType == "Finished Goods")
                {
                    MessageBox.Show(rollNumber + " is a finished good(" + itemDescription + ").  You need to revert the pallet to WIP before moving the roll.", "Invalid Move");
                }
                else
                {
                    SqlCommand command;
                    SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User Id=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
                    SqlDataReader reader;
                    string locationDescription = null;
                    bool suppressMessage = false;
                    connection.Open();
                    if (readLocationForm.UserInput.Substring(0, 1) == "L")
                    {
                        command = new SqlCommand("SELECT a.[Description], ISNULL(b.[Operation ID], 0) FROM [Location Table] a LEFT JOIN [Machine Table] b ON a.[Location ID] = b.[Machine No] WHERE [Location ID] = " + readLocationForm.UserInput.Substring(1), connection);
                        reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            locationDescription = reader[0].ToString();
                            if (reader[1].ToString() == "4")
                            {
                                // Must give a reason to move a roll to rework
                                reader.Close();
                                suppressMessage = true;
                                CommentForm reworkReasonForm = new CommentForm("Reason for Rework (Leave Blank to Abort Move)", string.Empty, false);
                                reworkReasonForm.setWidth = 500;
                                reworkReasonForm.ShowDialog();
                                if (string.IsNullOrEmpty(reworkReasonForm.Comment))
                                {
                                    MessageBox.Show("Move to " + readLocationForm.UserInput + " - " + locationDescription + " aborted.  You must enter a reason to move a roll to rework.", "Move to Rework Aborted");
                                    locationDescription = null;
                                }
                                else
                                {
                                    command = new SqlCommand("SELECT [Comment] FROM [Roll Comment Table] WHERE [Roll ID] = " + rollNumber.Substring(1), connection);
                                    string oldComment = (string)command.ExecuteScalar();
                                    if (string.IsNullOrEmpty(oldComment))
                                    {
                                        PrintClass.Comment(rollNumber, "Moved to rework by " + StartupForm.UserName + " because of: " + reworkReasonForm.Comment);
                                        command = new SqlCommand("INSERT INTO [Roll Comment Table] SELECT " + rollNumber.Substring(1) + ", 'Moved to rework by " + StartupForm.UserName.Replace("'", "''") + " because of: " + reworkReasonForm.Comment.Replace("'", "''") + "'", connection);
                                    }
                                    else
                                    {
                                        PrintClass.Comment(rollNumber, oldComment + "\r\nMoved to rework by " + StartupForm.UserName + " because of: " + reworkReasonForm.Comment);
                                        command = new SqlCommand("UPDATE [Roll Comment Table] SET [Comment] = [Comment] + '  / Moved to rework by " + StartupForm.UserName.Replace("'", "''") + " because of: " + reworkReasonForm.Comment.Replace("'", "''") + "' WHERE [Roll ID] = " + rollNumber.Substring(1), connection);
                                    }

                                    command.ExecuteNonQuery();
                                }

                                reworkReasonForm.Dispose();
                            }
                            else
                            {
                                reader.Close();
                            }
                        }
                    }
                    else
                    {
                        command = new SqlCommand("SELECT CAST(ISNULL(b.[Operation ID], 0) AS int) FROM [Pallet Table] a LEFT JOIN [Machine Table] b ON a.[Location ID] = b.[Machine No] WHERE [Pallet ID] = " + readLocationForm.UserInput.Substring(1), connection);
                        int operationID = (int)command.ExecuteScalar();
                        if (operationID == 3)
                        {
                            command = new SqlCommand("SELECT DISTINCT 'P' + CAST(a.[Pallet Id] AS nvarchar(10)) + ' at ' + b.[Description], ISNULL(c.[Master Item No], d.[Master Item No]), ISNULL(c.[width], e.[Stream Width]) FROM [Pallet Table] a INNER JOIN [Location Table] b ON a.[Location Id] = b.[Location Id] LEFT JOIN [Roll Table] c ON a.[Pallet Id] = c.[Pallet Id] LEFT JOIN ([Current Pallets at Machine Table] d INNER JOIN [Slitting Specification Table] e ON d.[Master Item No] = e.[Master Item No]) ON a.[Pallet ID] = d.[Pallet ID] WHERE a.[Pallet Id] = " + readLocationForm.UserInput.Substring(1), connection);
                            reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                if ((int)reader[1] != masterItemNumber || (decimal)reader[2] != width)
                                {
                                    MessageBox.Show("Error - Roll " + rollNumber + " is not the same product and width as the product on pallet " + readLocationForm.UserInput, "InvalId Product Move");
                                    suppressMessage = true;
                                }
                                else
                                {
                                    locationDescription = reader[0].ToString();
                                }
                            }
                        }
                        else
                        {
                            command = new SqlCommand("SELECT DISTINCT 'P' + CAST(a.[Pallet Id] AS nvarchar(10)) + ' at ' + b.[Description], ISNULL(c.[Master Item No], d.[Master Item No]) FROM [Pallet Table] a INNER JOIN [Location Table] b ON a.[Location Id] = b.[Location Id] LEFT JOIN [Roll Table] c ON a.[Pallet Id] = c.[Pallet Id] LEFT JOIN ([Current Pallets at Machine Table] d LEFT JOIN [Innolok Specification Table] e ON d.[Master Item No] = e.[Master Item No]) ON a.[Pallet ID] = d.[Pallet ID] WHERE a.[Pallet Id] = " + readLocationForm.UserInput.Substring(1), connection);
                            reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                if ((int)reader[1] != masterItemNumber)
                                {
                                    MessageBox.Show("Error - Roll " + rollNumber + " is not the same product as the product on pallet " + readLocationForm.UserInput, "InvalId Product Move");
                                    suppressMessage = true;
                                }
                                else
                                {
                                    locationDescription = reader[0].ToString();
                                }
                            }
                        }

                        reader.Close();
                    }

                    if (!string.IsNullOrEmpty(locationDescription))
                    {
                        if (!suppressMessage)
                        {
                            answer = MessageBox.Show("Move Roll " + rollNumber + " (" + width.ToString("N4") + "\" " + itemDescription + ") to " + locationDescription + "?", "Confirm Move", MessageBoxButtons.YesNo);
                        }
                        else
                        {
                            answer = DialogResult.Yes;
                        }

                        if (answer == DialogResult.Yes)
                        {
                            if (readLocationForm.UserInput.Substring(0, 1) == "L")
                            {
                                if (partType == "Raw Film" && palletNumber != 0)
                                {
                                    // Move the Location ID from the Pallet Table to the Roll Table as the Pallet is being broken
                                    command = new SqlCommand("update [Roll Table] set [Pallet Id]=null,[Location Id]=a.[Location Id] from [Pallet Table] a where [Roll Table].[Pallet Id]=a.[Pallet Id] and [Roll Table].[Pallet Id]=" + palletNumber, connection);
                                    command.ExecuteNonQuery();
                                    command = new SqlCommand("update [Pallet Table] set [Location Id]=null where [Pallet Id]=" + palletNumber, connection);
                                    command.ExecuteNonQuery();
                                }

                                command = new SqlCommand("insert into [Move Transaction Table] select '" + StartupForm.UserName + "',GETDATE()," + rollNumber.Substring(1) + ",NULL," + readLocationForm.UserInput.Substring(1), connection);
                                command.ExecuteNonQuery();
                            }
                            else
                            {
                                // Roll to Pallet Move
                                command = new SqlCommand("insert into [Move Transaction Table] select '" + StartupForm.UserName + "',GETDATE()," + rollNumber.Substring(1) + "," + readLocationForm.UserInput.Substring(1) + ",NULL", connection);
                                command.ExecuteNonQuery();
                                if (partType == "Finished Goods")
                                {
                                    command = new SqlCommand("UPDATE [" + StartupForm.FGDatabase + "].[dbo].[tblFGRoll] SET [PltID] = " + readLocationForm.UserInput.Substring(1) + " WHERE [RollID] = " + rollNumber.Substring(1), connection);
                                    command.ExecuteNonQuery();
                                    MessageBox.Show("DON'T FORGET YOU MUST INPUT NEW GROSS WEIGHTS AND REPRINT THE PALLET LABELS FOR FINISHED GOODS PALLETS P" + palletNumber.ToString() + " AND " + readLocationForm.UserInput + "!", "MUST UPDATE FINISHED GOODS PALLET INFO");
                                }
                            }

                        }
                        else
                        {
                            MessageBox.Show("Move aborted");
                        }
                    }
                    else if (!suppressMessage)
                    {
                        if (readLocationForm.UserInput.Substring(0, 1) == "L")
                        {
                            MessageBox.Show("Error - location " + readLocationForm.UserInput + " does not exist", "InvalId Location");
                        }
                        else
                        {
                            MessageBox.Show("Error - pallet " + readLocationForm.UserInput + " does not exist", "InvalId Pallet");
                        }
                    }

                    connection.Close();
                }
            }

            readLocationForm.Dispose();
            return answer;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        public static DialogResult MoveCase(string caseNumber, int masterItemNumber, string itemDescription, int palletNumber, string partType)
        {
            DialogResult answer = DialogResult.No;
            GetInputForm readLocationForm = new GetInputForm("Scan/Input Location or Pallet Id", "L", 0, 0, false);
            readLocationForm.ShowDialog();
            if (readLocationForm.UserInput.Length > 0)
            {

                if (partType == "Finished Goods")
                {
                    MessageBox.Show(caseNumber + " is a finished good(" + itemDescription + ").  You need to revert the pallet to WIP before moving the roll.", "Invalid Move");
                }
                else
                {
                    SqlCommand command;
                    SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User Id=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
                    SqlDataReader reader;
                    string locationDescription = null;
                    bool suppressMessage = false;
                    connection.Open();
                    if (readLocationForm.UserInput.Substring(0, 1) == "L")
                    {
                        command = new SqlCommand("SELECT a.[Description], ISNULL(b.[Operation ID], 0) FROM [Location Table] a LEFT JOIN [Machine Table] b ON a.[Location ID] = b.[Machine NO] WHERE [Location ID] = " + readLocationForm.UserInput.Substring(1), connection);
                        reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            locationDescription = reader[0].ToString();
                            if (reader[1].ToString() == "4")
                            {
                                // Must give a reason to move a case to rework
                                reader.Close();
                                suppressMessage = true;
                                CommentForm reworkReasonForm = new CommentForm("Reason for Rework (Leave Blank to Abort Move)", string.Empty, false);
                                reworkReasonForm.setWidth = 500;
                                reworkReasonForm.ShowDialog();
                                if (string.IsNullOrEmpty(reworkReasonForm.Comment))
                                {
                                    MessageBox.Show("Move to " + readLocationForm.UserInput + " - " + locationDescription + " aborted.  You must enter a reason to move a case to rework.", "Move to Rework Aborted");
                                    locationDescription = null;
                                }
                                else
                                {
                                    command = new SqlCommand("SELECT [Comment] FROM [Case Comment Table] WHERE [Case ID] = " + caseNumber.Substring(1), connection);
                                    string oldComment = (string)command.ExecuteScalar();
                                    if (string.IsNullOrEmpty(oldComment))
                                    {
                                        PrintClass.Comment(caseNumber, "Moved to rework by " + StartupForm.UserName + " because of: " + reworkReasonForm.Comment);
                                        command = new SqlCommand("INSERT INTO [Case Comment Table] SELECT " + caseNumber.Substring(1) + ", 'Moved to rework by " + StartupForm.UserName.Replace("'", "''") + " because of: " + reworkReasonForm.Comment.Replace("'", "''") + "'", connection);
                                    }
                                    else
                                    {
                                        PrintClass.Comment(caseNumber, oldComment + "\r\nMoved to rework by " + StartupForm.UserName + " because of: " + reworkReasonForm.Comment);
                                        command = new SqlCommand("UPDATE [Case Comment Table] SET [Comment] = [Comment] + '  / Moved to rework by " + StartupForm.UserName.Replace("'", "''") + " because of: " + reworkReasonForm.Comment.Replace("'", "''") + "' WHERE [Case ID] = " + caseNumber.Substring(1), connection);
                                    }

                                    command.ExecuteNonQuery();
                                }

                                reworkReasonForm.Dispose();
                            }
                            else
                            {
                                reader.Close();
                            }
                        }
                    }
                    else
                    {
                        command = new SqlCommand("SELECT DISTINCT 'P' + CAST(a.[Pallet Id] AS nvarchar(10)) + ' at ' + b.[Description], ISNULL(c.[Master Item No], d.[Master Item No]) FROM [Pallet Table] a INNER JOIN [Location Table] b ON a.[Location ID] = b.[Location ID] LEFT JOIN [Case Table] c ON a.[Pallet ID] = c.[Pallet ID] LEFT JOIN [Current Pallets at Machine Table] d ON a.[Pallet ID] = d.[Pallet ID] WHERE a.[Pallet ID] = " + readLocationForm.UserInput.Substring(1) + " AND ISNULL(c.[Master Item No], d.[Master Item No]) IS NOT NULL", connection);
                        reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            if ((int)reader[1] != masterItemNumber)
                            {
                                MessageBox.Show("Error - Case " + caseNumber + " is not the same product as the product on pallet " + readLocationForm.UserInput, "InvalId Product Move");
                                suppressMessage = true;
                            }
                            else
                            {
                                locationDescription = reader[0].ToString();
                            }
                        }

                        reader.Close();
                    }

                    if (!string.IsNullOrEmpty(locationDescription))
                    {
                        if (!suppressMessage)
                        {
                            answer = MessageBox.Show("Move Case " + caseNumber + " (" + itemDescription + ") to " + locationDescription + "?", "Confirm Move", MessageBoxButtons.YesNo);
                        }
                        else
                        {
                            answer = DialogResult.Yes;
                        }

                        if (answer == DialogResult.Yes)
                        {
                            if (readLocationForm.UserInput.Substring(0, 1) == "L")
                            {
                                command = new SqlCommand("insert into [Move Case Transaction Table] select '" + StartupForm.UserName + "',GETDATE()," + caseNumber.Substring(1) + ",NULL," + readLocationForm.UserInput.Substring(1), connection);
                                command.ExecuteNonQuery();
                            }
                            else
                            {
                                // Case to Pallet Move
                                command = new SqlCommand("insert into [Move Case Transaction Table] select '" + StartupForm.UserName + "',GETDATE()," + caseNumber.Substring(1) + "," + readLocationForm.UserInput.Substring(1) + ",NULL", connection);
                                command.ExecuteNonQuery();
                                if (partType == "Finished Goods")
                                {
                                    command = new SqlCommand("UPDATE [" + StartupForm.FGDatabase + "].[dbo].[tblFGRoll] SET [PltID] = " + readLocationForm.UserInput.Substring(1) + " WHERE [RollID] = " + (int.Parse(caseNumber.Substring(1), NumberStyles.Number) + 10000000).ToString(), connection);
                                    command.ExecuteNonQuery();
                                    MessageBox.Show("DON'T FORGET YOU MUST INPUT NEW GROSS WEIGHTS AND REPRINT THE PALLET LABELS FOR FINISHED GOODS PALLETS P" + palletNumber.ToString() + " AND " + readLocationForm.UserInput + "!", "MUST UPDATE FINISHED GOODS PALLET INFO");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Move aborted");
                        }
                    }
                    else if (!suppressMessage)
                    {
                        if (readLocationForm.UserInput.Substring(0, 1) == "L")
                        {
                            MessageBox.Show("Error - location " + readLocationForm.UserInput + " does not exist", "Invalid Location");
                        }
                        else
                        {
                            MessageBox.Show("Error - pallet " + readLocationForm.UserInput + " does not exist", "Invalid Pallet");
                        }
                    }

                    connection.Close();
                }
            }

            readLocationForm.Dispose();
            return answer;
        }

        public static string GetBooleanResult(int selectedIndex)
        {
            if (selectedIndex == -1)
            {
                // no value assigned
                return "null,";
            }
            else if (selectedIndex == 0)
            {
                // true
                return "1,";
            }
            else
            {
                // false
                return "0,";
            }
        }

        public static string GetPassFailResult(int selectedIndex)
        {
            if (selectedIndex == -1)
            {
                // no value assigned
                return "null,";
            }
            else if (selectedIndex == 0)
            {
                // Pass
                return "0,";
            }
            else
            {
                // fail
                return "1,";
            }
        }

        public static void SendEmailTest(int groupID, string emailSubject, string emailMessage)
        {
            SqlCommand command;
            SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User Id=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
            SqlDataReader reader;
            SmtpClient client = new SmtpClient("smtp.office365.com", 587);
            client.EnableSsl = true;
            MailMessage message = new MailMessage();
            message.Priority = MailPriority.High;
            message.Subject = emailSubject;
            message.SubjectEncoding = Encoding.UTF8;
            message.From = new MailAddress("shopfloor@overwraps.com");
            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("shopfloor@overwraps.com", "ZZIGYPig16");
            message.Subject = emailSubject;
            message.Body = emailMessage;
            command = new SqlCommand("SELECT [E-mail Address] FROM [E-mail Distribution List Table] WHERE [Group ID] = " + groupID.ToString(), connection);
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                message.To.Add(reader[0].ToString());
            }

            connection.Close();

            try
            {
                client.Send(message);
            }
            catch
            {
                MessageBox.Show("Email Could Not Be Sent - Check Email Server", "Email Error", MessageBoxButtons.OK);
            }
        }

        public static void SendEmail(int groupID, string emailSubject, string emailMessage)
        {   SqlCommand command;
            SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User Id=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
            SqlDataReader reader;
            SmtpClient client = new SmtpClient("smtp.office365.com", 587);
            client.EnableSsl = true;
            MailMessage message = new MailMessage();
            message.Priority = MailPriority.High;
            message.Subject = emailSubject;
            message.SubjectEncoding = Encoding.UTF8;
            message.From = new MailAddress("shopfloor@overwraps.com");
            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("shopfloor@overwraps.com", "ZZIGYPig16");
            message.Subject = emailSubject;
            message.Body = emailMessage;
            command = new SqlCommand("SELECT [E-mail Address] FROM [E-mail Distribution List Table] WHERE [Group ID] = " + groupID.ToString(), connection);
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                message.To.Add(reader[0].ToString());
            }

            connection.Close();

            try
            {
                client.Send(message);
            }
            catch
            {
                MessageBox.Show("Email Could Not Be Sent - Check Email Server", "Email Error", MessageBoxButtons.OK);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        public static bool ValidSaveDate(DateTime saveTime, DateTime endOfShift)
        {
            if (saveTime > DateTime.Now.AddMinutes(5))
            {
                MessageBox.Show("Error - the end time for this job (" + saveTime.ToString("MMMM d, yyyy h:mm tt") + ") is greater than the current system time (" + DateTime.Now.ToString() + ").  Save Aborted.", "Invalid End Time");
                return false;
            }
            else if (saveTime > endOfShift)
            {
                MessageBox.Show("Error - the end time of this job - " + saveTime.ToString("MMMM d, yyyy h:mm tt") + " - is past the end of shift - " + endOfShift.ToString("MMMM d, yyyy h:mm tt") + ".  You must either change the hours or create an \"End of Shift\" record.", "Invalid End Time");
                return false;
            }
            else
            {
                DialogResult answer = MessageBox.Show("The end time for this job is " + saveTime.ToString("MMMM d, yyyy h:mm tt") + ".  Is this correct?", "Confirm End Time", MessageBoxButtons.YesNo); if (answer == DialogResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static void CreateFGPallet()
        {
            SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
            SqlDataReader reader;
            SqlCommand command;
            GetInputForm readBarcodeForm = new GetInputForm("Scan/Input Barcode", "R", 0, 0, true);
            readBarcodeForm.ShowDialog();
            if (readBarcodeForm.UserInput.Length > 0)
            {
                if (readBarcodeForm.UserInput.Substring(0, 1) == "R")
                {
                    command = new SqlCommand("SELECT a.[Master Item No], c.[Description], CASE WHEN c.[Description] = 'Finished Goods' THEN a.[Pallet ID] ELSE 0 END, ISNULL(CAST(b.[Reference Item No] AS nvarchar(10)), ''), b.[Description] FROM [Roll Table] a INNER JOIN ([Inventory Master Table] b INNER JOIN [Item Type Table] c ON b.[Item Type No] = c.[Item Type No]) ON a.[Master Item No] = b.[Master Item No] LEFT JOIN [Pallet Table] d ON a.[Pallet ID] = d.[Pallet ID], [Location Table] e WHERE ISNULL(d.[Location ID], a.[Location ID]) = e.[Location ID] AND e.[Inventory Available] = 1 AND a.[Current LF] > 0 AND a.[Roll ID] = " + readBarcodeForm.UserInput.Substring(1), connection);
                    connection.Open();
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        string itemType = reader[1].ToString();
                        if (itemType != "WIP" && itemType != "Finished Goods")
                        {
                            MessageBox.Show("Error - this roll is " + itemType + " item (" + reader[3].ToString() + ")" + reader[4].ToString() + ".  Only Finished WIP and Finished Goods can be palletized here.", "Invalid Roll Product Type");
                        }
                        else
                        {
                            int currentPalletNumber = (int)reader[2];
                            if (itemType == "WIP")
                            {
                                command = new SqlCommand("SELECT a.[Master Item No], a.[Input Master Item No], a.[Job Jacket No], b.[Description] FROM [Finished Goods Specification Table] a INNER JOIN [Inventory Master Table] b ON a.[Master Item No] = b.[Master Item No] WHERE a.[Input Master Item No] = " + reader[0].ToString(), connection);
                            }
                            else
                            {
                                command = new SqlCommand("SELECT a.[Master Item No], a.[Input Master Item No], a.[Job Jacket No], b.[Description] FROM [Finished Goods Specification Table] a INNER JOIN [Inventory Master Table] b ON a.[Master Item No] = b.[Master Item No] WHERE a.[Master Item No] = " + reader[0].ToString(), connection);
                            }

                            reader.Close();
                            reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                DialogResult answer = MessageBox.Show("Do you wish to create a finished goods pallet for Job " + reader[2].ToString() + " - " + reader[3].ToString() + "?", "Create Pallet Confirmation", MessageBoxButtons.YesNo);
                                if (answer == DialogResult.Yes)
                                {
                                    int finishedGoodMasterItemNumber = (int)reader[0];
                                    int wipMasterItemNumber = (int)reader[1];
                                    int jobJacketNumber = (int)reader[2];
                                    string jobDescription = reader[3].ToString();
                                    reader.Close();
                                    command = new SqlCommand("EXECUTE [Create Pallet Stored Procedure] '" + StartupForm.UserName + "', 191, 0, NULL", connection);
                                    reader = command.ExecuteReader();
                                    reader.Read();
                                    int palletNumber = (int)reader[0];
                                    reader.Close();
                                    if (currentPalletNumber != 0)
                                    {
                                        MessageBox.Show("YOU WILL NEED TO REPRINT FINISHED GOODS PALLET LABEL P" + currentPalletNumber.ToString() + " IF IT STILL EXISTS SINCE YOU ARE MOVING THIS ROLL OFF OF IT!", "DON'T FORGET TO REPRINT LABEL!");
                                    }

                                    command = new SqlCommand("INSERT INTO [Move Transaction Table] SELECT '" + StartupForm.UserName + "', GETDATE(), " + readBarcodeForm.UserInput.Substring(1) + ", " + palletNumber.ToString() + ", NULL", connection);
                                    command.ExecuteNonQuery();
                                    if (itemType == "WIP")
                                    {
                                        command = new SqlCommand("UPDATE [Roll Table] SET [Master Item No] = a.[Master Item no], [UOM ID] = b.[UOM ID], [Original Units] = CASE WHEN a.[UOM] = 'LBS' THEN [Original Pounds] WHEN a.[UOM] = 'ROLLS' THEN 1 ELSE FLOOR([dbo].[UOM Conversion]([Current LF], 'LF', a.[Repeat], a.[Stream Width], a.[Linear Feet per Roll], a.[Standard Yield], CASE WHEN a.[UOM]='PCS' THEN 'IMPS' ELSE a.[UOM] END) * a.[Multiplier]) END FROM [Finished Goods Specification Table] a INNER JOIN [UOM Table] b ON a.[UOM] = b.[Description] WHERE [Roll ID] = " + readBarcodeForm.UserInput.Substring(1) + " AND a.[Master Item No] = " + finishedGoodMasterItemNumber.ToString(), connection);
                                        command.ExecuteNonQuery();
                                    }

                                    while (answer == DialogResult.Yes)
                                    {
                                        answer = MessageBox.Show("Do you wish to add more rolls to pallet P" + palletNumber.ToString() + "?", "", MessageBoxButtons.YesNo);
                                        if (answer == DialogResult.Yes)
                                        {
                                            readBarcodeForm.UserInput = "";
                                            readBarcodeForm.ShowDialog();
                                            if (readBarcodeForm.UserInput.Length > 0)
                                            {
                                                command = new SqlCommand("SELECT a.[Master Item No], b.[Reference Item No], b.[Description], CASE WHEN b.[Item Type No] = 4 THEN a.[Pallet ID] ELSE 0 END FROM [Roll Table] a INNER JOIN [Inventory Master Table] b on a.[Master Item No] = b.[Master Item No] LEFT JOIN ([Pallet Table] c INNER JOIN [Location Table] d ON c.[Location ID] = d.[Location ID]) ON a.[Pallet ID] = c.[Pallet ID] LEFT JOIN [Location Table] e ON a.[Location ID] = e.[Location ID] WHERE ISNULL(d.[Inventory Available], e.[Inventory Available]) = 1 AND a.[Current LF] > 0 AND b.[Item Type No] in (2, 4) AND a.[Roll ID] = " + readBarcodeForm.UserInput.Substring(1), connection);
                                                reader = command.ExecuteReader();
                                                if (reader.Read())
                                                {
                                                    if ((int)reader[0] == finishedGoodMasterItemNumber || (int)reader[0] == wipMasterItemNumber)
                                                    {
                                                        // Valid FG or Finished WIP Roll
                                                        if ((int)reader[3] != 0 && (int)reader[3] != currentPalletNumber)
                                                        {
                                                            MessageBox.Show("YOU WILL NEED TO REPRINT FINISHED GOODS PALLET LABEL P" + reader[3].ToString() + " IF IT STILL EXISTS SINCE YOU ARE MOVING THIS ROLL OFF OF IT!", "DON'T FORGET TO REPRINT LABEL!");
                                                            currentPalletNumber = (int)reader[3];
                                                        }

                                                        if ((int)reader[0] == wipMasterItemNumber)
                                                        {
                                                            // Finished WIP Roll
                                                            reader.Close();
                                                            command = new SqlCommand("UPDATE [Roll Table] SET [Master Item No] = a.[Master Item no], [UOM ID] = b.[UOM ID], [Original Units] = CASE WHEN a.[UOM] = 'LBS' THEN [Original Pounds] WHEN a.[UOM] = 'ROLLS' THEN 1 ELSE FLOOR([dbo].[UOM Conversion]([Current LF], 'LF', a.[Repeat], a.[Stream Width], a.[Linear Feet per Roll], a.[Standard Yield], CASE WHEN a.[UOM]='PCS' THEN 'IMPS' ELSE a.[UOM] END) * a.[Multiplier]) END FROM [Finished Goods Specification Table] a INNER JOIN [UOM Table] b ON a.[UOM] = b.[Description] WHERE [Roll ID] = " + readBarcodeForm.UserInput.Substring(1) + " AND a.[Master Item No] = " + finishedGoodMasterItemNumber.ToString(), connection);
                                                            command.ExecuteNonQuery();
                                                        }
                                                        else
                                                        {
                                                            reader.Close();
                                                        }

                                                        command = new SqlCommand("INSERT INTO [Move Transaction Table] SELECT '" + StartupForm.UserName + "', GETDATE(), " + readBarcodeForm.UserInput.Substring(1) + ", " + palletNumber.ToString() + ", NULL", connection);
                                                        command.ExecuteNonQuery();
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Error - roll" + readBarcodeForm.UserInput + " is for job " + reader[1].ToString() + " - " + reader[2].ToString() + ", not job " + jobJacketNumber.ToString() + " - " + jobDescription, "Roll belongs to wrong Job");
                                                        reader.Close();
                                                    }
                                                }
                                                else
                                                {
                                                    reader.Close();
                                                    MessageBox.Show("Error - finished good or WIP finished roll " + readBarcodeForm.UserInput + " not found", "Roll Not Found");
                                                }
                                            }
                                        }
                                    }

                                    command = new SqlCommand("SELECT SUM([Current LF] / [Original LF] * [Original Pounds]) FROM [Roll Table] WHERE [Pallet ID] = " + palletNumber, connection);
                                    decimal netWeight = (decimal)command.ExecuteScalar();
                                    GetInputForm frmGetGrossWeight = new GetInputForm("Gross Pallet Weight", "#", (int)Math.Floor(netWeight + 1), (int)Math.Round(netWeight, 0) + 200, false);
                                    while (frmGetGrossWeight.UserInput.Length == 0)
                                    {
                                        frmGetGrossWeight.ShowDialog();
                                        if (frmGetGrossWeight.UserInput.Length == 0)
                                        {
                                            MessageBox.Show("You MUST enter a valid pallet weight", "No Going Back!");
                                        }
                                    }

                                    command = new SqlCommand("UPDATE [Pallet Table] SET [Weight] = " + frmGetGrossWeight.UserInput + " WHERE [Pallet ID] = " + palletNumber.ToString(), connection);
                                    frmGetGrossWeight.Dispose();
                                    command.ExecuteNonQuery();
                                    command = new SqlCommand("DELETE FROM [" + StartupForm.FGDatabase + "].[dbo].[tblFGRoll] WHERE [RollID] IN (SELECT [Roll ID] from [Roll Table] WHERE [Pallet ID] = " + palletNumber.ToString() + ")", connection);
                                    command.ExecuteNonQuery();
                                    command = new SqlCommand("EXECUTE [dbo].[Create Finished Goods Database Records] " + palletNumber.ToString(), connection);
                                    command.ExecuteNonQuery();
                                    PrintClass.Label("P" + palletNumber.ToString());
                                }
                                else
                                {
                                    reader.Close();
                                }
                            }
                            else
                            {
                                reader.Close();
                                MessageBox.Show("This WIP roll is not a finished WIP roll", "Invalid Roll");
                            }
                        }
                    }
                    else
                    {
                        reader.Close();
                        MessageBox.Show("Error - roll not found", "Invalid Roll");
                    }

                    connection.Close();
                }
                else // Pallet of Bags
                {
                    command = new SqlCommand("SELECT a.[Master Item No], c.[Description], CASE WHEN c.[Description] = 'Finished Goods' THEN a.[Pallet ID] ELSE 0 END, ISNULL(CAST(b.[Reference Item No] AS nvarchar(10)), ''), b.[Description] FROM [Case Table] a INNER JOIN ([Inventory Master Table] b INNER JOIN [Item Type Table] c ON b.[Item Type No] = c.[Item Type No]) ON a.[Master Item No] = b.[Master Item No] LEFT JOIN [Pallet Table] d ON a.[Pallet ID] = d.[Pallet ID], [Location Table] e WHERE ISNULL(d.[Location ID], a.[Location ID]) = e.[Location ID] AND e.[Inventory Available] = 1 AND a.[Bags] > 0 AND a.[Case ID] = " + readBarcodeForm.UserInput.Substring(1), connection);
                    connection.Open();
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        string itemType = reader[1].ToString();
                        if (itemType != "WIP" && itemType != "Finished Goods")
                        {
                            MessageBox.Show("Error - this case is " + itemType + " item (" + reader[3].ToString() + ")" + reader[4].ToString() + ".  Only finished WIP and Finished Goods can be palletized here.", "Invalid Case Product Type");
                        }
                        else
                        {
                            int currentPalletNumber = (int)reader[2];
                            if (itemType == "WIP")
                            {
                                command = new SqlCommand("SELECT a.[Master Item No], a.[Input Master Item No], a.[Job Jacket No], b.[Description] FROM [Finished Goods Specification Table] a INNER JOIN [Inventory Master Table] b ON a.[Master Item No] = b.[Master Item No] WHERE a.[Input Master Item No] = " + reader[0].ToString(), connection);
                            }
                            else
                            {
                                command = new SqlCommand("SELECT a.[Master Item No], a.[Input Master Item No], a.[Job Jacket No], b.[Description] FROM [Finished Goods Specification Table] a INNER JOIN [Inventory Master Table] b ON a.[Master Item No] = b.[Master Item No] WHERE a.[Master Item No] = " + reader[0].ToString(), connection);
                            }

                            reader.Close();
                            reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                DialogResult answer = MessageBox.Show("Do you wish to create a finished goods pallet for Job " + reader[2].ToString() + " - " + reader[3].ToString() + "?", "Create Pallet Confirmation", MessageBoxButtons.YesNo);
                                if (answer == DialogResult.Yes)
                                {
                                    int finishedGoodMasterItemNumber = (int)reader[0];
                                    int wipMasterItemNumber = (int)reader[1];
                                    int jobJacketNumber = (int)reader[2];
                                    string jobDescription = reader[3].ToString();
                                    reader.Close();
                                    command = new SqlCommand("EXECUTE [Create Pallet Stored Procedure] '" + StartupForm.UserName + "', 191, 0, NULL", connection);
                                    reader = command.ExecuteReader();
                                    reader.Read();
                                    int palletNumber = (int)reader[0];
                                    reader.Close();
                                    if (currentPalletNumber != 0)
                                    {
                                        MessageBox.Show("YOU WILL NEED TO REPRINT FINISHED GOODS PALLET LABEL P" + currentPalletNumber.ToString() + " IF IT STILL EXISTS SINCE YOU ARE MOVING THIS CASE OFF OF IT!", "DON'T FORGET TO REPRINT LABEL!");
                                    }

                                    command = new SqlCommand("INSERT INTO [Move Case Transaction Table] SELECT '" + StartupForm.UserName + "', GETDATE(), " + readBarcodeForm.UserInput.Substring(1) + ", " + palletNumber.ToString() + ", NULL", connection);
                                    command.ExecuteNonQuery();
                                    if (itemType == "WIP")
                                    {
                                        command = new SqlCommand("UPDATE [Case Table] SET [Master Item No] = " + finishedGoodMasterItemNumber.ToString() + " WHERE [Case ID] = " + readBarcodeForm.UserInput.Substring(1), connection);
                                        command.ExecuteNonQuery();
                                    }

                                    while (answer == DialogResult.Yes)
                                    {
                                        answer = MessageBox.Show("Do you wish to add more cases to pallet P" + palletNumber.ToString() + "?", "", MessageBoxButtons.YesNo);
                                        if (answer == DialogResult.Yes)
                                        {
                                            readBarcodeForm.UserInput = "";
                                            readBarcodeForm.ShowDialog();
                                            if (readBarcodeForm.UserInput.Length > 0)
                                            {
                                                command = new SqlCommand("SELECT a.[Master Item No], b.[Reference Item No], b.[Description], CASE WHEN b.[Item Type No] = 4 THEN a.[Pallet ID] ELSE 0 END FROM [Case Table] a INNER JOIN [Inventory Master Table] b on a.[Master Item No] = b.[Master Item No] LEFT JOIN ([Pallet Table] c INNER JOIN [Location Table] d ON c.[Location ID] = d.[Location ID]) ON a.[Pallet ID] = c.[Pallet ID] LEFT JOIN [Location Table] e ON a.[Location ID] = e.[Location ID] WHERE ISNULL(d.[Inventory Available], e.[Inventory Available]) = 1 AND a.[Bags] > 0 AND b.[Item Type No] in (2, 4) AND a.[Case ID] = " + readBarcodeForm.UserInput.Substring(1), connection);
                                                reader = command.ExecuteReader();
                                                if (reader.Read())
                                                {
                                                    if ((int)reader[0] == finishedGoodMasterItemNumber || (int)reader[0] == wipMasterItemNumber)
                                                    {
                                                        // Valid FG or Finished WIP Case
                                                        if ((int)reader[3] != 0 && (int)reader[3] != currentPalletNumber)
                                                        {
                                                            MessageBox.Show("YOU WILL NEED TO REPRINT FINISHED GOODS PALLET LABEL P" + reader[3].ToString() + " IF IT STILL EXISTS SINCE YOU ARE MOVING THIS CASE OFF OF IT!", "DON'T FORGET TO REPRINT LABEL!");
                                                            currentPalletNumber = (int)reader[3];
                                                        }

                                                        if ((int)reader[0] == wipMasterItemNumber)
                                                        {
                                                            // Finished WIP Case
                                                            reader.Close();
                                                            command = new SqlCommand("UPDATE [Case Table] SET [Master Item No] = " + finishedGoodMasterItemNumber.ToString() + " WHERE [Case ID] = " + readBarcodeForm.UserInput.Substring(1), connection);
                                                            command.ExecuteNonQuery();
                                                        }
                                                        else
                                                        {
                                                            reader.Close();
                                                        }

                                                        command = new SqlCommand("INSERT INTO [Move Case Transaction Table] SELECT '" + StartupForm.UserName + "', GETDATE(), " + readBarcodeForm.UserInput.Substring(1) + ", " + palletNumber.ToString() + ", NULL", connection);
                                                        command.ExecuteNonQuery();
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Error - case " + readBarcodeForm.UserInput + " is for job " + reader[1].ToString() + " - " + reader[2].ToString() + ", not job " + jobJacketNumber.ToString() + " - " + jobDescription, "Case belongs to wrong Job");
                                                        reader.Close();
                                                    }
                                                }
                                                else
                                                {
                                                    reader.Close();
                                                    MessageBox.Show("Error - finished good or WIP finished case " + readBarcodeForm.UserInput + " not found", "Casel Not Found");
                                                }
                                            }
                                        }
                                    }

                                    command = new SqlCommand("SELECT SUM([Pounds]) FROM [Case Table] WHERE [Pallet ID] = " + palletNumber, connection);
                                    decimal netWeight = (decimal)command.ExecuteScalar();
                                    GetInputForm frmGetGrossWeight = new GetInputForm("Gross Pallet Weight", "#", (int)Math.Floor(netWeight + 1), (int)Math.Round(netWeight, 0) + 200, false);
                                    while (frmGetGrossWeight.UserInput.Length == 0)
                                    {
                                        frmGetGrossWeight.ShowDialog();
                                        if (frmGetGrossWeight.UserInput.Length == 0)
                                        {
                                            MessageBox.Show("You MUST enter a valid pallet weight", "No Going Back!");
                                        }
                                    }

                                    command = new SqlCommand("UPDATE [Pallet Table] SET [Weight] = " + frmGetGrossWeight.UserInput + " WHERE [Pallet ID] = " + palletNumber.ToString(), connection);
                                    frmGetGrossWeight.Dispose();
                                    command.ExecuteNonQuery();
                                    command = new SqlCommand("DELETE FROM [" + StartupForm.FGDatabase + "].[dbo].[tblFGRoll] WHERE [RollID] - 10000000 IN (SELECT [Case ID] from [Case Table] WHERE [Pallet ID] = " + palletNumber.ToString() + ")", connection);
                                    command.ExecuteNonQuery();
                                    command = new SqlCommand("EXECUTE [dbo].[Create Finished Goods Case Database Records] " + palletNumber.ToString(), connection);
                                    command.ExecuteNonQuery();
                                    PrintClass.Label("P" + palletNumber.ToString());
                                }
                                else
                                {
                                    reader.Close();
                                }
                            }
                            else
                            {
                                reader.Close();
                                MessageBox.Show("This WIP case is not a finished WIP roll", "Invalid Roll");
                            }
                        }
                    }
                    else
                    {
                        reader.Close();
                        MessageBox.Show("Error - case not found", "Invalid Roll");
                    }

                    connection.Close();
                }
            }

            readBarcodeForm.Dispose();
        }

        public static void QCRollCheck(string processID, int wipType, string rollID)
        {
            SqlCommand command;
            string jobJacketNumber;
            SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User Id=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
            SqlDataReader reader;
            string jobInformation;
            if (processID.Substring(0, 1) == "1")
            {
                // Print WIP
                if (wipType == 3) // Combo WIP
                {
                    command = new SqlCommand("select d.[CustName],c.[JobJacketNo],c.[ItemNo],e.[ItemName],coalesce(c.[RepeatComexi],c.[RepeatOthers],0),a.[Width], cast(f.[Gauge] as decimal(9,3)),c.[ComboNo] from [Roll Table] a inner join ([Inventory Master Table] b inner join ([JobJackets].[dbo].[tblJobTicket] c inner join [JobJackets].[dbo].[tblCustomer] d on c.[CustID]=d.[CustID] inner join [JobJackets].[dbo].[tblItem] e on c.[ItemNo]=e.[ItemNo] inner join [Film View] f on c.[MatPrint1_1]=f.[Part No]) on substring(cast(b.[Reference Item No] as nvarchar(10)),1,len(cast(b.[Reference Item No] as nvarchar(10)))-2)=cast(c.[ComboNo] as nvarchar(10))) on a.[Master Item No]=b.[Master Item No] where [Roll ID] = " + rollID.Substring(1), connection);
                }
                else
                {
                    command = new SqlCommand("select d.[CustName],c.[JobJacketNo],c.[ItemNo],e.[ItemName],coalesce(c.[RepeatComexi],c.[RepeatOthers],0),a.[Width], cast(f.[Gauge] as decimal(9,3)) from [Roll Table] a inner join ([Inventory Master Table] b inner join ([JobJackets].[dbo].[tblJobTicket] c inner join [JobJackets].[dbo].[tblCustomer] d on c.[CustID]=d.[CustID] inner join [JobJackets].[dbo].[tblItem] e on c.[ItemNo]=e.[ItemNo] inner join [Film View] f on c.[MatPrint1_1]=f.[Part No]) on substring(cast(b.[Reference Item No] as nvarchar(10)),1,len(cast(b.[Reference Item No] as nvarchar(10)))-2)=c.[JobJacketNo]) on a.[Master Item No]=b.[Master Item No] where [Roll ID] = " + rollID.Substring(1), connection);
                }

                connection.Open();
                reader = command.ExecuteReader();
                reader.Read();
                decimal currentRepeat = (decimal)reader[4];
                decimal currentRollWidth = (decimal)reader[5];
                decimal currentFilmGauge = 0;
                if (wipType == 3) // Combo JOb
                {
                    jobInformation = "Roll ID: " + rollID + "\r\nCustomer: " + reader[0].ToString() + "\r\nCombo No: " + ((int)reader[7]).ToString() + "  Film Width: " + currentRollWidth.ToString("N4") + " inches";
                    jobJacketNumber = reader[1].ToString();
                    do
                    {
                        jobInformation += "\r\nJob Jacket No: " + reader[1].ToString() + "  Item No: " + ((int)reader[2]).ToString() + "\r\n  Description: " + reader[3].ToString() + "\r\n  Repeat: " + ((decimal)reader[4]).ToString("N4") + " inches  Film 1 Gauge per Job Jacket: " + ((decimal)reader[6]).ToString("N3") + " mil";

                    }
                    while (reader.Read());
                    jobInformation += "\r\n";
                }
                else
                {
                    jobInformation = "Roll ID: " + rollID + "\r\nCustomer: " + reader[0].ToString() + "\r\nJob Jacket No: " + reader[1].ToString() + "  Item No: " + reader[2].ToString() + "\r\nDescription: " + reader[3].ToString() + "\r\nFilm Width: " + currentRollWidth.ToString("N4") + " inches  Repeat: " + currentRepeat.ToString("N4") + " inches  Film Gauge per Job Jacket: " + ((decimal)reader[6]).ToString("N3") + " mil\r\n";
                    jobJacketNumber = reader[1].ToString();
                }

                reader.Close();
                int currentProducedRollID = int.Parse(rollID.Substring(1));
                do
                {
                    // Drill Down to Raw Material Roll
                    command = new SqlCommand("select a.[Input Roll ID 1],b.[Master Item No],cast(c.[Item Type No] as int),d.[View Name],b.[Width] from [Production Roll Table] a inner join ([Roll Table] b inner join ([Inventory Master Table] c inner join [Item Type Table] d on c.[Item Type No]=d.[Item Type No]) on b.[Master Item No]=c.[Master Item No]) on a.[Input Roll ID 1]=b.[Roll ID] where a.[Roll ID]=" + currentProducedRollID.ToString(), connection);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        if ((int)reader[2] == 1)
                        {
                            // Raw Material Roll Found
                            command = new SqlCommand("select [Description],cast([Gauge] as decimal(9,3)) from " + reader[3].ToString() + " where [Master Item No]=" + reader[1].ToString(), connection);
                            jobInformation += "\r\nRaw Material Input Roll ID: R" + ((int)reader[0]).ToString() + " - " + ((decimal)reader[4]).ToString("N4") + "\" ";
                            reader.Close();
                            reader = command.ExecuteReader();
                            reader.Read();
                            jobInformation += ((decimal)reader[1]).ToString("N3") + " mil " + reader[0].ToString();
                            currentFilmGauge = (decimal)reader[1];
                            reader.Close();
                            currentProducedRollID = 0;
                        }
                        else
                        {
                            currentProducedRollID = (int)reader[0];
                            jobInformation += "\r\nWIP Input Roll ID: R" + currentProducedRollID.ToString();
                            command = new SqlCommand("select [Description] from " + reader[3].ToString() + " where [Master Item No]=" + reader[1].ToString(), connection);
                            reader.Close();
                            jobInformation += " - " + (string)command.ExecuteScalar();
                        }
                    }
                    else
                    {
                        reader.Close();
                        MessageBox.Show("Error - there is no raw material input roll to trace back to", "No Roll Defined");
                        currentProducedRollID = 0;
                    }
                }
                while (currentProducedRollID != 0);

                PrintingQCCheckForm frmPrintQCCheckForm = new PrintingQCCheckForm(rollID.Substring(1), jobInformation,
                    currentRollWidth, currentRepeat, currentFilmGauge, jobJacketNumber);
                connection.Close();
                frmPrintQCCheckForm.ShowDialog();
                frmPrintQCCheckForm.Dispose();
            }
            else
            {
                // Lamination WIP
                if (wipType == 3) // Combo JOb
                {
                    command = new SqlCommand("select d.[CustName],c.[JobJacketNo],c.[ItemNo],e.[ItemName],coalesce(c.[RepeatComexi],c.[RepeatOthers],0),a.[Width],cast(f.[Gauge] as decimal(9,3)),cast(g.[Gauge] as decimal(9,3)),cast(h.[Gauge] as decimal(9,3)),case when isnull(c.[NoCut],0)=0 then 1 else abs(c.[NoCut]) end, isnull(c.[SlitWidth],c.[MatPrintSize1_1]),c.[ComboNo] from [Roll Table] a inner join ([Inventory Master Table] b inner join ([JobJackets].[dbo].[tblJobTicket] c inner join [JobJackets].[dbo].[tblCustomer] d on c.[CustID]=d.[CustID] inner join [JobJackets].[dbo].[tblItem] e on c.[ItemNo]=e.[ItemNo] inner join [Film View] f on c.[MatPrint1_1]=f.[Part No] inner join [Film View] g on c.[MatLam1_1]=g.[Part No] left join [Film View] h on c.[MatLam2_1]=h.[Part No]) on substring(cast(b.[Reference Item No] as nvarchar(10)),1,len(cast(b.[Reference Item No] as nvarchar(10)))-2)=cast(c.[ComboNo] as nvarchar(10))) on a.[Master Item No]=b.[Master Item No] where [Roll ID]=" + rollID.Substring(1), connection);
                }
                else
                {
                    command = new SqlCommand("select d.[CustName],c.[JobJacketNo],c.[ItemNo],e.[ItemName],coalesce(c.[RepeatComexi],c.[RepeatOthers],0),a.[Width],cast(f.[Gauge] as decimal(9,3)),cast(g.[Gauge] as decimal(9,3)),cast(h.[Gauge] as decimal(9,3)),case when isnull(c.[NoCut],0)=0 then 1 else abs(c.[NoCut]) end, isnull(c.[SlitWidth],c.[MatPrintSize1_1]) from [Roll Table] a inner join ([Inventory Master Table] b inner join ([JobJackets].[dbo].[tblJobTicket] c inner join [JobJackets].[dbo].[tblCustomer] d on c.[CustID]=d.[CustID] inner join [JobJackets].[dbo].[tblItem] e on c.[ItemNo]=e.[ItemNo] inner join [Film View] f on c.[MatPrint1_1]=f.[Part No] inner join [Film View] g on c.[MatLam1_1]=g.[Part No] left join [Film View] h on c.[MatLam2_1]=h.[Part No]) on substring(cast(b.[Reference Item No] as nvarchar(10)),1,len(cast(b.[Reference Item No] as nvarchar(10)))-2)=c.[JobJacketNo]) on a.[Master Item No]=b.[Master Item No] where [Roll ID]=" + rollID.Substring(1), connection);
                }

                connection.Open();
                reader = command.ExecuteReader();
                reader.Read();
                decimal currentRepeat = (decimal)reader[4];
                decimal currentRollGlueWidth = (decimal)(int)reader[9] * (decimal)reader[10];
                decimal currentFilmGauge = 0;
                if (wipType == 3) // Combo JOb
                {
                    jobInformation = "Roll ID: " + rollID + "\r\nCustomer: " + reader[0].ToString() + "\r\nCombo No: " + ((int)reader[11]).ToString() + "  Film Width: " + ((decimal)reader[5]).ToString("N4") + " inches";
                    do
                    {
                        if (currentRepeat == 0)
                        {
                            // No Print therefore no Repeat
                            jobInformation += "\r\nJob Jacket No: " + reader[1].ToString() + "  Item No: " + ((int)reader[2]).ToString() + "\r\n  Description: " + reader[3].ToString() + "\r\n";
                        }
                        else
                        {
                            jobInformation += "\r\nJob Jacket No: " + reader[1].ToString() + "  Item No: " + ((int)reader[2]).ToString() + "\r\n  Description: " + reader[3].ToString() + "\r\n  Repeat: " + ((decimal)reader[4]).ToString("N4") + " inches\r\n";
                        }

                        if (processID == "21" && reader[8] == DBNull.Value)
                        {
                            // Job only has One Lamination Pass
                            jobInformation += "Film 1 Gauge per Job Jacket: " + ((decimal)reader[6]).ToString("N3") + " mil  Film 2 Gauge per Job Jacket:" + ((decimal)reader[7]).ToString("N3");
                        }
                        else
                        {
                            // Job might be 1st pass of a double lamination
                            jobInformation += "Film 1 Gauge per Job Jacket: " + ((decimal)reader[7]).ToString("N3") + " mil  Film 2 Gauge per Job Jacket:" + ((decimal)reader[8]).ToString("N3");
                        }

                        if (processID == "22")
                        {
                            jobInformation += "  Film 3 Gauge per Job Jacket:" + ((decimal)reader[6]).ToString("N3");
                        }
                    }
                    while (reader.Read());

                    jobInformation += "\r\n";
                }
                else
                {
                    if (currentRepeat == 0)
                    {
                        // No Print therefore no Repeat
                        jobInformation = "Roll ID: " + rollID + "\r\nCustomer: " + reader[0].ToString() + "\r\nJob Jacket No: " + reader[1].ToString() + "  Item No: " + reader[2].ToString() + "\r\nDescription: " + reader[3].ToString() + "\r\nFilm Width: " + ((decimal)reader[5]).ToString("N4") + " inches\r\n";
                    }
                    else
                    {
                        jobInformation = "Roll ID: " + rollID + "\r\nCustomer: " + reader[0].ToString() + "\r\nJob Jacket No: " + reader[1].ToString() + "  Item No: " + reader[2].ToString() + "\r\nDescription: " + reader[3].ToString() + "\r\nFilm Width: " + ((decimal)reader[5]).ToString("N4") + " inches  Repeat: " + currentRepeat.ToString("N4") + " inches\r\n";
                    }

                    if (processID == "21" && reader[8] == DBNull.Value)
                    {
                        // Job only has One Lamination Pass
                        jobInformation += "Film 1 Gauge per Job Jacket: " + ((decimal)reader[6]).ToString("N3") + " mil  Film 2 Gauge per Job Jacket:" + ((decimal)reader[7]).ToString("N3");
                    }
                    else
                    {
                        jobInformation += "Film 1 Gauge per Job Jacket: " + ((decimal)reader[7]).ToString("N3") + " mil  Film 2 Gauge per Job Jacket:" + ((decimal)reader[8]).ToString("N3");
                    }

                    if (processID == "22")
                    {
                        jobInformation += "  Film 3 Gauge per Job Jacket:" + ((decimal)reader[6]).ToString("N3") + "\r\n";
                    }
                    else
                    {
                        jobInformation += "\r\n";
                    }
                }

                reader.Close();
                int intNoFilms;
                if (processID == "22")
                {
                    intNoFilms = 3;
                }
                else
                {
                    intNoFilms = 2;
                }

                int lam1WIPRollID = 0;
                int unwindNo = 0;
                for (int i = 1; i <= intNoFilms; i++)
                {
                    int currentProducedRollID = int.Parse(rollID.Substring(1));
                    //												do
                    //												{
                    // Drill Down to Raw Material Rolls
                    if (i == 1)
                    {
                        command = new SqlCommand("select a.[Input Roll ID 1],b.[Master Item No],cast(c.[Item Type No] as int),d.[View Name],b.[Width] from [Production Roll Table] a inner join ([Roll Table] b inner join ([Inventory Master Table] c inner join [Item Type Table] d on c.[Item Type No]=d.[Item Type No]) on b.[Master Item No]=c.[Master Item No]) on a.[Input Roll ID 1]=b.[Roll ID] where a.[Roll ID]=" + currentProducedRollID.ToString(), connection);
                    }
                    else if (i == 2)
                    {
                        command = new SqlCommand("select isnull(a.[Input Roll ID 2],a.[Input Roll ID 1]),b.[Master Item No],cast(c.[Item Type No] as int),d.[View Name],b.[Width] from [Production Roll Table] a inner join ([Roll Table] b inner join ([Inventory Master Table] c inner join [Item Type Table] d on c.[Item Type No]=d.[Item Type No]) on b.[Master Item No]=c.[Master Item No]) on isnull(a.[Input Roll ID 2],a.[Input Roll ID 1])=b.[Roll ID] where a.[Roll ID]=" + currentProducedRollID.ToString(), connection);
                    }
                    else
                    {
                        if (unwindNo == 1)
                        {
                            command = new SqlCommand("select a.[Input Roll ID 1],b.[Master Item No],cast(c.[Item Type No] as int),d.[View Name],b.[Width] from [Production Roll Table] a inner join ([Roll Table] b inner join ([Inventory Master Table] c inner join [Item Type Table] d on c.[Item Type No]=d.[Item Type No]) on b.[Master Item No]=c.[Master Item No]) on a.[Input Roll ID 1]=b.[Roll ID] where a.[Roll ID]=" + lam1WIPRollID.ToString(), connection);
                        }
                        else
                        {
                            command = new SqlCommand("select isnull(a.[Input Roll ID 2],a.[Input Roll ID 1]),b.[Master Item No],cast(c.[Item Type No] as int),d.[View Name],b.[Width] from [Production Roll Table] a inner join ([Roll Table] b inner join ([Inventory Master Table] c inner join [Item Type Table] d on c.[Item Type No]=d.[Item Type No]) on b.[Master Item No]=c.[Master Item No]) on isnull(a.[Input Roll ID 2],a.[Input Roll ID 1])=b.[Roll ID] where a.[Roll ID]=" + lam1WIPRollID.ToString(), connection);
                        }
                    }

                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        if ((int)reader[2] == 1)
                        {
                            // Raw Material Roll Found
                            command = new SqlCommand("select [Description],cast([Gauge] as decimal(9,3)) from " + reader[3].ToString() + " where [Master Item No]=" + reader[1].ToString(), connection);
                            jobInformation += "\r\nRaw Material Input Roll ID: R" + ((int)reader[0]).ToString() + " - " + ((decimal)reader[4]).ToString("N4") + "\" ";
                            reader.Close();
                            reader = command.ExecuteReader();
                            reader.Read();
                            jobInformation += ((decimal)reader[1]).ToString("N3") + " mil " + reader[0].ToString() + "\r\n";
                            currentFilmGauge += (decimal)reader[1];
                            reader.Close();
                            currentProducedRollID = 0;
                        }
                        else
                        {
                            currentProducedRollID = (int)reader[0];
                            jobInformation += "\r\nWIP Input Roll ID: R" + currentProducedRollID.ToString();
                            command = new SqlCommand("select a.[Description],b.[Reference Item No] from " + reader[3].ToString() + " a inner join [Inventory Master Table] b on a.[Master Item No]=b.[Master Item No] where a.[Master Item No]=" + reader[1].ToString(), connection);
                            reader.Close();
                            reader = command.ExecuteReader();
                            reader.Read();
                            jobInformation += " (Job " + reader[1].ToString() + ") " + reader[0].ToString();
                            if (((int)reader[1]).ToString().Substring(((int)reader[1]).ToString().Length - 2, 2) == "21")
                            {
                                lam1WIPRollID = currentProducedRollID;
                                if (i == 1)
                                {
                                    // Need to Follow the Film on Unwind 2 for the third film
                                    unwindNo = 2;
                                }
                                else
                                {
                                    unwindNo = 1;
                                }
                            }

                            reader.Close();
                        }
                    }
                    else
                    {
                        reader.Close();
                        MessageBox.Show("Error - there is no raw material input roll to trace back to", "No Roll Defined");
                        currentProducedRollID = 0;
                    }
                    //						}
                    //				    	while (currentProducedRollID != 0);
                }

                connection.Close();
                LaminatingQCCheckForm frmLaminatingQCCheckform = new LaminatingQCCheckForm(rollID.Substring(1), jobInformation, currentRollGlueWidth, currentRepeat, currentFilmGauge);
                frmLaminatingQCCheckform.ShowDialog();
                frmLaminatingQCCheckform.Dispose();
            }
        }

        public static bool ValidateUPC(string jobNumber, string rollID, string UPCToValidate, string palletID, out string overrideAuthorizedBy)
        {
            bool validUPC = true;
            DialogResult retry = DialogResult.Yes;
            GetInputForm getUPCCode = new GetInputForm("Scan UPC Code", "#", 0, 0, true);
            getUPCCode.ShowDialog();
            while (UPCToValidate.TrimStart('0') != getUPCCode.UserInput.TrimStart('0') && retry == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(getUPCCode.UserInput))
                {
                    MessageBox.Show("No UPC Code Entered.  You will need to re-scan the input roll", "Missing UPC Code");
                    retry = DialogResult.No;
                    validUPC = false;
                }
                else
                {
                    if (string.IsNullOrEmpty(rollID))
                    {
                        SendEmail(2, "Invalid Pallet UPC Entered", "On Pallet " + palletID + " in Finishing the UPC code entered for job " + jobNumber + " of " + getUPCCode.UserInput + " was incorrect. The user logged in is " + StartupForm.UserName);
                    }
                    else
                    {
                        SendEmail(2, "Invalid Consumed Roll UPC Entered", "On line " + MainForm.MachineNumber + " the UPC code of roll " + rollID + " entered for job " + jobNumber + " of " + getUPCCode.UserInput + " was incorrect. The user logged in is " + StartupForm.UserName + " and the operator is " + MainForm.OperatorName);
                    }

                    retry = MessageBox.Show("The UPC Code Scanned - \"" + getUPCCode.UserInput + "\" - does not tie with the UPC Expected - \"" + UPCToValidate + "\".  Do you wish to re-try?", "Incorrect UPC Code", MessageBoxButtons.YesNo);
                    if (retry == DialogResult.Yes)
                    {
                        getUPCCode.UserInput = string.Empty;
                        getUPCCode.ShowDialog();
                    }
                    else
                    {
                        validUPC = false;
                    }
                }
            }

            overrideAuthorizedBy = getUPCCode.AuthorizedBy;
            getUPCCode.Dispose();
            return validUPC;
        }

        public static bool ValidateUPCCodes(string jobNumber, string rollID, DataTable UPCCodesTable, out string overrideAuthorizedBy)
        {
            bool inputRollOK = true;
            string authorizedBy = string.Empty;
            DialogResult notDone = DialogResult.Yes;
            DataTable UPCCodesToConfirm = UPCCodesTable.Copy();
            while (notDone == DialogResult.Yes && UPCCodesToConfirm.Rows.Count > 0)
            {
                GetInputForm getUPCCode = new GetInputForm("Scan UPC Code", "#", 0, 0, true);
                getUPCCode.ShowDialog();
                if (string.IsNullOrEmpty(getUPCCode.UserInput))
                {
                    MessageBox.Show("UPC Code Validation aborted.  You cannot consume/create this roll", "No Roll Consumed/Created");
                    inputRollOK = false;
                    notDone = DialogResult.No;
                }
                else
                {
                    bool found = false;
                    foreach (DataRow row in UPCCodesToConfirm.Rows)
                    {
                        if (row["UPC Code"].ToString().TrimStart('0') == getUPCCode.UserInput.TrimStart('0'))
                        {
                            found = true;
                            row.Delete();
                            break;
                        }
                    }

                    if (!found)
                    {
                        if (string.IsNullOrEmpty(rollID))
                        {
                            SendEmail(2, "Invalid Produced Roll UPC Entered", "On line " + MainForm.MachineNumber + " the UPC code entered for job " + jobNumber + " of " + getUPCCode.UserInput + " was incorrect. The user logged in is " + StartupForm.UserName + " and the operator is " + MainForm.OperatorName);
                        }
                        else
                        {
                            SendEmail(2, "Invalid Consumed Roll UPC Entered", "On line " + MainForm.MachineNumber + " the UPC code of roll " + rollID + " entered for job " + jobNumber + " of " + getUPCCode.UserInput + " was incorrect. The user logged in is " + StartupForm.UserName + " and the operator is " + MainForm.OperatorName);
                        }

                        MessageBox.Show("Error - the UPC Code does not match.  Please retry", "Invalid UPC Code");
                    }
                }

                if (!string.IsNullOrEmpty(getUPCCode.AuthorizedBy))
                {
                    authorizedBy = getUPCCode.AuthorizedBy;
                }

                getUPCCode.Dispose();
            }

            overrideAuthorizedBy = authorizedBy;
            return inputRollOK;
        }

        public static void DisplayFPReaderError(string funcName, int iError)
        {
            string text = "";

            switch (iError)
            {
                case 0:                             //SGFDX_ERROR_NONE				= 0,
                    text = "Error none";
                    break;

                case 1:                             //SGFDX_ERROR_CREATION_FAILED	= 1,
                    text = "Can not create object";
                    break;

                case 2:                             //   SGFDX_ERROR_FUNCTION_FAILED	= 2,
                    text = "Function Failed";
                    break;

                case 3:                             //   SGFDX_ERROR_INVALID_PARAM	= 3,
                    text = "Invalid Parameter";
                    break;

                case 4:                          //   SGFDX_ERROR_NOT_USED			= 4,
                    text = "Not used function";
                    break;

                case 5:                                //SGFDX_ERROR_DLLLOAD_FAILED	= 5,
                    text = "Can not create object";
                    break;

                case 6:                                //SGFDX_ERROR_DLLLOAD_FAILED_DRV	= 6,
                    text = "Can not load device driver";
                    break;
                case 7:                                //SGFDX_ERROR_DLLLOAD_FAILED_ALGO = 7,
                    text = "Can not load sgfpamx.dll";
                    break;

                case 51:                //SGFDX_ERROR_SYSLOAD_FAILED	   = 51,	// system file load fail
                    text = "Can not load driver kernel file";
                    break;

                case 52:                //SGFDX_ERROR_INITIALIZE_FAILED  = 52,   // chip initialize fail
                    text = "Failed to initialize the device";
                    break;

                case 53:                //SGFDX_ERROR_LINE_DROPPED		   = 53,   // image data drop
                    text = "Data transmission is not good";
                    break;

                case 54:                //SGFDX_ERROR_TIME_OUT			   = 54,   // getliveimage timeout error
                    text = "Time out";
                    break;

                case 55:                //SGFDX_ERROR_DEVICE_NOT_FOUND	= 55,   // device not found
                    text = "Device not found";
                    break;

                case 56:                //SGFDX_ERROR_DRVLOAD_FAILED	   = 56,   // dll file load fail
                    text = "Can not load driver file";
                    break;

                case 57:                //SGFDX_ERROR_WRONG_IMAGE		   = 57,   // wrong image
                    text = "Wrong Image";
                    break;

                case 58:                //SGFDX_ERROR_LACK_OF_BANDWIDTH  = 58,   // USB Bandwith Lack Error
                    text = "Lack of USB Bandwith";
                    break;

                case 59:                //SGFDX_ERROR_DEV_ALREADY_OPEN	= 59,   // Device Exclusive access Error
                    text = "Device is already opened";
                    break;

                case 60:                //SGFDX_ERROR_GETSN_FAILED		   = 60,   // Fail to get Device Serial Number
                    text = "Device serial number error";
                    break;

                case 61:                //SGFDX_ERROR_UNSUPPORTED_DEV		   = 61,   // Unsupported device
                    text = "Unsupported device";
                    break;

                // Extract & Verification error
                case 101:                //SGFDX_ERROR_FEAT_NUMBER		= 101, // utoo small number of minutiae
                    text = "The number of minutiae is too small";
                    break;

                case 102:                //SGFDX_ERROR_INVALID_TEMPLATE_TYPE		= 102, // wrong template type
                    text = "Template is invalid";
                    break;

                case 103:                //SGFDX_ERROR_INVALID_TEMPLATE1		= 103, // wrong template type
                    text = "1st template is invalid";
                    break;

                case 104:                //SGFDX_ERROR_INVALID_TEMPLATE2		= 104, // vwrong template type
                    text = "2nd template is invalid";
                    break;

                case 105:                //SGFDX_ERROR_EXTRACT_FAIL		= 105, // extraction fail
                    text = "Minutiae extraction failed";
                    break;

                case 106:                //SGFDX_ERROR_MATCH_FAIL		= 106, // matching  fail
                    text = "Matching failed";
                    break;

            }

            MessageBox.Show(text = funcName + " Error # " + iError + " :" + text, "Finger Print Reader Error");
        }

        public static void FillDowntimeComboBox(ComboBox comboBox, string operationID)
        {
            
            SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
            SqlDataReader reader;
            SqlCommand command;
            ComboBoxItem item;
            item = new ComboBoxItem();
            item.Text = string.Empty;
            item.Key = "0";
            item.NotesRequired = false;
            comboBox.Items.Add(item);
            command = new SqlCommand("SELECT [Description], [Downtime Reason ID], [Notes Required] FROM [Downtime Reason Table] WHERE[Operation ID] = " + operationID + " AND [Active] = 1 ORDER BY [Display Order]", connection);
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                item = new ComboBoxItem();
                item.Text = reader[0].ToString();
                item.Key = reader[1].ToString();
                item.NotesRequired = reader.GetBoolean(2);
                comboBox.Items.Add(item);
            }

            reader.Close();
            connection.Close();
            comboBox.DisplayMember = "Text";
            comboBox.SelectedIndex = 0;
        }

        public static void FillScrapReasonComboBox(ComboBox comboBox, string operationID)
        {

            SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
            SqlDataReader reader;
            SqlCommand command;
            ComboBoxItem item;
            item = new ComboBoxItem();
            item.Text = string.Empty;
            item.Key = "0";
            comboBox.Items.Add(item);
            command = new SqlCommand("SELECT SUBSTRING([Description], CHARINDEX('-', [Description]) + 2, LEN([Description]) - CHARINDEX('-', [Description]) - 1), [Adjustment Reason ID] FROM [Adjustment Reason Table] WHERE [Operation ID] = " + operationID + " ORDER BY [Display Order]", connection);
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                item = new ComboBoxItem();
                item.Text = reader[0].ToString();
                item.Key = reader[1].ToString();
                comboBox.Items.Add(item);
            }

            reader.Close();
            connection.Close();
            comboBox.DisplayMember = "Text";
            comboBox.SelectedIndex = 0;
        }

        public static string GetPulledReason(string productionID)
        {

            SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
            SqlDataReader reader;
            SqlCommand command;
            ComboBoxItem item;
            string pulledReason = "Abort";

            OptionsForm pulledReasonForm = new OptionsForm("Reason for Pulling Job", false, true);
            command = new SqlCommand("SELECT SUBSTRING([Description], 10, LEN([Description]) - 9), [End Reason ID], [Notes Required] FROM [Save Production Reason Table] WHERE SUBSTRING([Description],1,9) = 'Pulled - ' ORDER BY [End Reason ID]", connection);
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                item = new ComboBoxItem();
                item.Text = reader[0].ToString();
                item.Key = reader[1].ToString();
                item.NotesRequired = reader.GetBoolean(2);
                pulledReasonForm.AddComboItemClassOption(item);
            }

            reader.Close();
            connection.Close();
            pulledReasonForm.DisplayMember = "Text";
            pulledReasonForm.ShowDialog();
            if (pulledReasonForm.Option != "Abort")
            {
                string authorizedBy = string.Empty;
                bool authorized = Authorized(false, ref authorizedBy);
                if (authorized)
                {
                    if (pulledReasonForm.ComboBoxItemOption.NotesRequired)
                    {
                        CommentForm notesForPullingForm = new CommentForm("Reason job Pulled", string.Empty, false);
                        notesForPullingForm.StartPosition = FormStartPosition.CenterScreen;
                        DialogResult validPull = DialogResult.Yes;
                        while (string.IsNullOrEmpty(notesForPullingForm.Comment) && validPull == DialogResult.Yes)
                        {
                            notesForPullingForm.ShowDialog();
                            if (string.IsNullOrEmpty(notesForPullingForm.Comment))
                            {
                                validPull = MessageBox.Show("Error - you must enter notes for why this job is being pulled.  Do you still wish to pull the job?", "Notes Required", MessageBoxButtons.YesNo);
                            }
                        }

                        if (validPull == DialogResult.Yes)
                        {
                            command = new SqlCommand("DELETE FROM [Production Pulled Reason Notes] WHERE [Production ID] = " + productionID, connection);
                            connection.Open();
                            command.ExecuteNonQuery();
                            command = new SqlCommand("INSERT INTO [Production Pulled Reason Notes] SELECT " + productionID + ", '" + notesForPullingForm.Comment.Replace("'", "''") + "'", connection);
                            command.ExecuteNonQuery();
                            connection.Close();
                            pulledReason = pulledReasonForm.ComboBoxItemOption.Key;
                        }

                        notesForPullingForm.Dispose();
                    }
                    else
                    {
                        pulledReason = pulledReasonForm.ComboBoxItemOption.Key;
                    }
                }
            }

            pulledReasonForm.Dispose();
            return pulledReason;
        }

        public static int FillDowntimeDetails(RichTextBox rtbDownTimeDetails, ComboBox cboDownTimeIDs, Panel pnlRemoveDownTimeRecord, string productionID)
        {
            SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
            SqlDataReader reader;
            SqlCommand command;
            int nextDownTimeRecord = 1;
            rtbDownTimeDetails.Text = string.Empty;
            cboDownTimeIDs.Items.Clear();
            command = new SqlCommand("SELECT CAST(a.[Record ID] AS int), b.[Description], a.[Hours] FROM [Production Downtime Hours Table] a INNER JOIN [Downtime Reason Table] b ON a.[Downtime Reason ID] = b.[Downtime Reason ID] WHERE a.[Production ID] = " + productionID + " ORDER BY a.[Record ID]", connection);
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                if ((decimal)reader[2] != 0)
                {
                    rtbDownTimeDetails.Text += reader[0].ToString() + ": " + reader[1].ToString() + " for " + ((decimal)reader[2]).ToString("N2") + " hours\r\n";
                    cboDownTimeIDs.Items.Add(reader[0].ToString());
                }

                nextDownTimeRecord = (int)reader[0] + 1;
            }

            reader.Close();
            connection.Close();
            if (string.IsNullOrEmpty(rtbDownTimeDetails.Text))
            {
                pnlRemoveDownTimeRecord.Visible = false;
            }
            else
            {
                pnlRemoveDownTimeRecord.Visible = true;
                cboDownTimeIDs.SelectedIndex = 0;
            }

            return nextDownTimeRecord;
        }

        public static int FillScrapDetails(RichTextBox rtbScrapDetails, ComboBox cboScrapIDs, Panel pnlRemoveScrapRecord, TextBox txtTotalScrapPounds, string productionID)
        {
            SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
            SqlDataReader reader;
            SqlCommand command;
            int nextScrapRecord = 1;
            int totalScrapPounds = 0;
            rtbScrapDetails.Text = string.Empty;
            cboScrapIDs.Items.Clear();
            command = new SqlCommand("SELECT CAST(a.[Record ID] AS int), SUBSTRING(b.[Description], CHARINDEX('-', b.[Description]) + 2, LEN(b.[Description]) - CHARINDEX('-', b.[Description]) - 1), a.[Pounds] FROM [Production Scrap Generation Table] a INNER JOIN [Adjustment Reason Table] b ON a.[Adjustment Reason ID] = b.[Adjustment Reason ID] WHERE a.[Production ID] = " + productionID + " AND a.[Pounds] != 0 ORDER BY a.[Record ID]", connection);
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                if ((int)reader[2] != 0)
                {
                    rtbScrapDetails.Text += reader[0].ToString() + ": " + reader[1].ToString() + " for " + ((int)reader[2]).ToString("N0") + " pounds\r\n";
                    cboScrapIDs.Items.Add(reader[0].ToString());
                    totalScrapPounds += (int)reader[2];
                }

                nextScrapRecord = (int)reader[0] + 1;
            }

            reader.Close();
            connection.Close();
            txtTotalScrapPounds.Text = totalScrapPounds.ToString("N0");
            if (string.IsNullOrEmpty(rtbScrapDetails.Text))
            {
                pnlRemoveScrapRecord.Visible = false;
            }
            else
            {
                pnlRemoveScrapRecord.Visible = true;
                cboScrapIDs.SelectedIndex = 0;
            }

            return nextScrapRecord;
        }

        public static int RemoveDownTime(TextBox txtDownTimeHours, TextBox txtTotalHours, RichTextBox rtbDownTimeDetails, ComboBox cboDownTimeIDs, Panel pnlRemoveDownTimeRecord, string productionID)
        {
            SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
            SqlCommand command;
            command = new SqlCommand("SELECT [Hours] FROM [Production Downtime Hours Table] WHERE [Production ID] = " + productionID + " AND [Record ID] = " + cboDownTimeIDs.Text, connection);
            connection.Open();
            decimal hoursToRemove = (decimal)command.ExecuteScalar();
            txtDownTimeHours.Text = (decimal.Parse(txtDownTimeHours.Text, NumberStyles.Number) - hoursToRemove).ToString("N2");
            txtTotalHours.Text = (decimal.Parse(txtTotalHours.Text, NumberStyles.Number) - hoursToRemove).ToString("N2");
            command = new SqlCommand("UPDATE [Production Master Table] SET [DT Hrs] = " + txtDownTimeHours.Text + " WHERE [Production ID] = " + productionID, connection);
            command.ExecuteNonQuery();
            command = new SqlCommand("DELETE FROM [Production Downtime Notes Table] WHERE [Production ID] = " + productionID + " AND [Record ID] = " + cboDownTimeIDs.Text, connection);
            command.ExecuteNonQuery();
            command = new SqlCommand("DELETE FROM [Production Downtime Hours Table] WHERE [Production ID] = " + productionID + " AND [Record ID] = " + cboDownTimeIDs.Text, connection);
            command.ExecuteNonQuery();
            connection.Close();
            int nextDownTimeRecordID = ModulesClass.FillDowntimeDetails(rtbDownTimeDetails, cboDownTimeIDs, pnlRemoveDownTimeRecord, productionID);
            return nextDownTimeRecordID;
        }

        public static void AddOownTimeReord(ref int nextDownTimeRecordID, ComboBox cboDownTimeReasons, TextBox txtAddedDownTimeHours,  TextBox txtDownTimeHours, TextBox txtTotalHours, RichTextBox rtbDownTimeDetails, ComboBox cboDownTimeIDs, Panel pnlRemoveDownTimeRecord, string productionID)
        {
            SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
            SqlCommand command;
            if (cboDownTimeReasons.Text == string.Empty)
            {
                MessageBox.Show("You must enter a reason for the downtime", "No Downtime Reason Entered");
                cboDownTimeReasons.Select();
            }
            else
            {
                DialogResult validDownTime = DialogResult.Yes;
                string downTimeNotes = string.Empty;
                if ((cboDownTimeReasons.SelectedItem as ComboBoxItem).NotesRequired)
                {
                    CommentForm notesForDowntimeForm = new CommentForm("Reason for Downtime", string.Empty, false);
                    notesForDowntimeForm.StartPosition = FormStartPosition.CenterScreen;

                    while (string.IsNullOrEmpty(notesForDowntimeForm.Comment) && validDownTime == DialogResult.Yes)
                    {
                        notesForDowntimeForm.ShowDialog();
                        if (string.IsNullOrEmpty(notesForDowntimeForm.Comment))
                        {
                            validDownTime = MessageBox.Show("Error - you must enter notes for why this job is being pulled.  Do you still wish to pull the job?", "Notes Required", MessageBoxButtons.YesNo);
                        }
                        else
                        {
                            downTimeNotes = notesForDowntimeForm.Comment;
                        }
                    }

                    notesForDowntimeForm.Dispose();
                }

                if (validDownTime == DialogResult.Yes)
                {
                    command = new SqlCommand("INSERT INTO [Production Downtime Hours Table] SELECT " + productionID + ", " + nextDownTimeRecordID.ToString() + ", " + (cboDownTimeReasons.SelectedItem as ComboBoxItem).Key + ", " + decimal.Parse(txtAddedDownTimeHours.Text, NumberStyles.Number).ToString(), connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                    if (!string.IsNullOrEmpty(downTimeNotes))
                    {
                        command = new SqlCommand("INSERT INTO [Production Downtime Notes Table] SELECT " + productionID + ", " + nextDownTimeRecordID.ToString() + ", '" + downTimeNotes.Replace("'", "''") + "'", connection);
                        command.ExecuteNonQuery();
                    }

                    txtDownTimeHours.Text = (decimal.Parse(txtDownTimeHours.Text, NumberStyles.Number) + decimal.Parse(txtAddedDownTimeHours.Text, NumberStyles.Number)).ToString("N2");
                    command = new SqlCommand("UPDATE [Production Master Table] SET [DT Hrs] = " + txtDownTimeHours.Text + " WHERE [Production ID] = " + productionID, connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                    txtTotalHours.Text = (decimal.Parse(txtTotalHours.Text, NumberStyles.Number) + decimal.Parse(txtAddedDownTimeHours.Text, NumberStyles.Number)).ToString("N2");
                    rtbDownTimeDetails.Text += nextDownTimeRecordID.ToString() + ": " + cboDownTimeReasons.Text + " for " + txtAddedDownTimeHours.Text + " hours\r\n";
                    cboDownTimeIDs.Items.Add(nextDownTimeRecordID.ToString());
                    nextDownTimeRecordID++;
                    txtAddedDownTimeHours.Text = "0.00";
                    pnlRemoveDownTimeRecord.Visible = true;
                    cboDownTimeReasons.SelectedIndex = 0;
                    cboDownTimeIDs.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("No description given for downtime, so downtime was not saved", "Downtime Must have Notes");
                }
            }
        }

        public static int RemoveScrapRecord(TextBox txtTotalScrapPounds, RichTextBox rtbScrapDetails, ComboBox cboScrapIDs, Panel pnlRemoveScrapRecord, string productionID)
        {
            SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
            SqlCommand command;
            command = new SqlCommand("SELECT [Pounds] FROM [Production Scrap Generation Table] WHERE [Production ID] = " + productionID + " AND [Record ID] = " + cboScrapIDs.Text, connection);
            connection.Open();
            int poundsToRemove = (int)command.ExecuteScalar();
            txtTotalScrapPounds.Text = (int.Parse(txtTotalScrapPounds.Text, NumberStyles.Number) - poundsToRemove).ToString("N0");
            command = new SqlCommand("DELETE FROM [Production Scrap Generation Table] WHERE [Production ID] = " + productionID + " AND [Record ID] = " + cboScrapIDs.Text, connection);
            command.ExecuteNonQuery();
            connection.Close();
            int nextScrapRecordID = ModulesClass.FillScrapDetails(rtbScrapDetails,cboScrapIDs, pnlRemoveScrapRecord, txtTotalScrapPounds, productionID);
            return nextScrapRecordID;
        }

        public static void AddScrapRecord(ref int nextScrapRecordID, ComboBox cboScrapReasons, TextBox txtAddedScrapPounds, TextBox txtTotalScrapPounds, RichTextBox rtbScrapDetails, ComboBox cboScrapIDs, Panel pnlRemoveScrapRecord, string productionID)
        {
            SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
            SqlCommand command;
            command = new SqlCommand("INSERT INTO [Production Scrap Generation Table] SELECT " + productionID + ", " + nextScrapRecordID.ToString() + ", " + (cboScrapReasons.SelectedItem as ComboBoxItem).Key + ", " + int.Parse(txtAddedScrapPounds.Text, NumberStyles.Number).ToString(), connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            txtTotalScrapPounds.Text = (int.Parse(txtTotalScrapPounds.Text, NumberStyles.Number) + int.Parse(txtAddedScrapPounds.Text, NumberStyles.Number)).ToString("N0");
            rtbScrapDetails.Text += nextScrapRecordID.ToString() + ": " + cboScrapReasons.Text + " for " + int.Parse(txtAddedScrapPounds.Text, NumberStyles.Number).ToString("N0") + " pounds\r\n";
            cboScrapIDs.Items.Add(nextScrapRecordID.ToString());
            nextScrapRecordID++;
            txtAddedScrapPounds.Text = "0";
            pnlRemoveScrapRecord.Visible = true;
            cboScrapReasons.SelectedIndex = 0;
            cboScrapIDs.SelectedIndex = 0;
        }

        public static DialogResult GetItemToMove(string machineNo)
        {
            SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
            SqlCommand command;
            SqlDataReader reader;
            DialogResult answer = DialogResult.No;
            GetInputForm readBarcodeForm = new GetInputForm("Scan/Input Barcode", "R", 0, 0, true);
            readBarcodeForm.ShowDialog();
            if (readBarcodeForm.UserInput.Length > 0)
            {
                if (readBarcodeForm.UserInput.Substring(0, 1) == "R") // Roll
                { 
                    command = new SqlCommand("SELECT a.[Master Item No], a.[Width], b.[Description], ISNULL(a.[Location ID], d.[Location ID]), CAST(ROUND(a.[Current LF], 0) AS int), CAST(ROUND(e.[Start Usage LF], 0) AS int), ISNULL(a.[Pallet ID], 0), c.[Description] FROM [Roll Table] a INNER JOIN ([Inventory Master Table] b INNER JOIN [Item Type Table] c ON b.[Item Type No] = c.[Item Type No]) ON a.[Master Item No] = b.[Master Item No] LEFT JOIN [Pallet Table] d ON a.[Pallet ID] = d.[Pallet ID] LEFT JOIN [Production Consumed Roll Table] e ON a.[Roll ID] = e.[Roll ID] AND e.[End Usage LF] IS NULL, [Location Table] f WHERE ISNULL(a.[Location ID], d.[Location ID]) = f.[Location ID] AND f.[Inventory Available] = 1 AND a.[Roll ID] = " + readBarcodeForm.UserInput.Substring(1) + " AND (a.[Current LF] > 0 OR ISNULL(a.[Location ID], d.[Location ID]) = " + machineNo + ")", connection);
                    connection.Open();
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        if (reader[3].ToString() == machineNo && (int)reader[4] == 0 && (int)reader[5] > 0)
                        {
                            MessageBox.Show("This roll is now or has been consumed on machine " + MainForm.MachineNumber + ".  You must go into production and return it.", "Invalid Roll");
                            reader.Close();
                        }
                        else
                        {
                            answer = MoveRoll(readBarcodeForm.UserInput, (int)reader[0], (decimal)reader[1], reader[2].ToString(), (int)reader[6], reader[7].ToString());
                            reader.Close();
                        }
                    }
                    else
                    {
                        reader.Close();
                        MessageBox.Show("Error - roll " + readBarcodeForm.UserInput + " not found", "Roll Not Found");
                    }

                    connection.Close();
                    readBarcodeForm.Dispose();
                }
                else //Case
                {
                    command = new SqlCommand("SELECT a.[Master Item No], '(' + CAST(b.[Reference Item No] AS nvarchar(10)) + ') ' + b.[Description], ISNULL(a.[Pallet ID], 0), c.[Description] FROM [Case Table] a INNER JOIN ([Inventory Master Table] b INNER JOIN [Item Type Table] c ON b.[Item Type No] = c.[Item Type No]) ON a.[Master Item No] = b.[Master Item No] LEFT JOIN [Pallet Table] d ON a.[Pallet ID] = d.[Pallet ID], [Location Table] e WHERE ISNULL(a.[Location ID], d.[Location ID]) = e.[Location ID] AND e.[Inventory Available] = 1 AND a.[Case ID] = " + readBarcodeForm.UserInput.Substring(1), connection);
                    connection.Open();
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        answer = MoveCase(readBarcodeForm.UserInput, (int)reader[0], reader[1].ToString(), (int)reader[2], reader[3].ToString());
                        reader.Close();
                    }
                    else
                    {
                        reader.Close();
                        MessageBox.Show("Error - case " + readBarcodeForm.UserInput + " not found", "Case Not Found");
                    }

                    connection.Close();
                }
            }

            return answer;
        }

        public static string GetFPUserID()
        {
            int tries = 0;
            string loginName = string.Empty;
            while (tries< 3 && string.IsNullOrEmpty(loginName))
            {
                fingerPrintLoginForm matchFingerPrintForm = new fingerPrintLoginForm();
                matchFingerPrintForm.ShowDialog();
                if (string.IsNullOrEmpty(matchFingerPrintForm.UserID))
                {
                    if (tries == 2)
                    {
//                        ModulesClass.SendEmail(3, loginName + " Fingerprint Verifcation Failed", "On " + DateTime.Now.ToLongDateString() + " at " + DateTime.Now.ToLongTimeString() + " the fingerprint verificaiton for " + loginName + " failed 3 times");
                        MessageBox.Show("Fingerprint not found.  Please see your supervisor to rescan your fingerprint", "Invalid Fingerprint");
                    }

                    tries++;
                }
                else
                {
                    loginName = matchFingerPrintForm.UserID;
//                    if (tries > 0)
//                    {
//                       SendEmail(3, loginName + " Fingerprint Verifcation Failed " + tries.ToString() + " Time(s) before Working", "On " + DateTime.Now.ToLongDateString() + " at " + DateTime.Now.ToLongTimeString() + " the fingerprint verificaiton for " + loginName + " failed " + tries.ToString() + " time(s) before suceeding");
//                    }
                }

                matchFingerPrintForm.Dispose();
            }

            return loginName;
        }

        public static bool Authorized(bool qualityCheck, ref string authorizedBy)
        {
            bool authorized = false;
            
            if (MainForm.UserCanOverride)
            {
                authorized = true;
                authorizedBy = StartupForm.UserName;

            }
            else if (StartupForm.FpScanDefault && StartupForm.WorkingFPScanner)
            {
                MessageBox.Show("Please click OK when ready for fingerprint scan", "Click OK when Ready!");
                string loginName = GetFPUserID();
                if (!string.IsNullOrEmpty(loginName))
                {
                    SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
                    SqlCommand command; connection.Open();
                    if (qualityCheck)
                    {
                        command = new SqlCommand("SELECT CAST(CASE WHEN [Can Override] = 1 OR [Job Rights] = 'QA' THEN 1 ELSE 0 END AS bit) FROM [User Rights Table]  WHERE [User ID] = '" + loginName + "'", connection);
                    }
                    else
                    {
                        command = new SqlCommand("SELECT [Can Override] FROM [User Rights Table] WHERE [User ID] = '" + loginName + "'", connection);
                    }

                    bool? canOverride = (bool?)command.ExecuteScalar();
                    if (canOverride.HasValue && (bool)canOverride)
                    {
                        authorized = true;
                        authorizedBy = loginName;
                    }
                    else
                    {
                        authorized = false;
                        if (qualityCheck)
                        {
                            MessageBox.Show(loginName + "\" does not have Quality Control rights", "User Cannot QA Check");
                        }
                        else
                        {
                            MessageBox.Show("Error - either the password is wrong or \"" + loginName + "\" does not have override authorization rights", "User Cannot Override");
                        }
                    }

                    connection.Close();
                }
                else
                {
                    authorized = false;
                }
            }
            else
            {
                AuthorizationForm authorizeOverrideForm = new AuthorizationForm(qualityCheck);
                authorizeOverrideForm.ShowDialog();
                if (authorizeOverrideForm.OKToOverride)
                {
                    authorized = true;
                    authorizedBy = authorizeOverrideForm.UserName;
                }
                else
                {
                    authorized = false;
                }

                authorizeOverrideForm.Close();
            }

            return authorized;
        }

        public static void ExcessTrimReason(string allocationID, string priority)
        {
            SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=" + StartupForm.Database + ";Connection Timeout=60;Persist Security Info=False");
            SqlDataReader reader;
            SqlCommand command;
            ComboBoxItem item;
            OptionsForm excessTrimReasonForm = new OptionsForm("Reason for Excess Trim", false, false);
            command = new SqlCommand("SELECT [Description], [Excess Trim Reason ID], [Notes Required] FROM [Excess Trim Reason Table] ORDER BY [Excess Trim Reason ID]", connection);
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                item = new ComboBoxItem();
                item.Text = reader[0].ToString();
                item.Key = reader[1].ToString();
                item.NotesRequired = reader.GetBoolean(2);
                excessTrimReasonForm.AddComboItemClassOption(item);
            }

            reader.Close();
            connection.Close();
            excessTrimReasonForm.DisplayMember = "Text";
            excessTrimReasonForm.ShowDialog();
            if (excessTrimReasonForm.Option != "Abort")
            {
                CommentForm notesForExcessTrimForm = new CommentForm("Reason for Excess Trim", string.Empty, true);
                notesForExcessTrimForm.StartPosition = FormStartPosition.CenterScreen;
                bool noteRequired = true;
                while (string.IsNullOrEmpty(notesForExcessTrimForm.Comment) && noteRequired)
                {
                    noteRequired = excessTrimReasonForm.ComboBoxItemOption.NotesRequired;
                    notesForExcessTrimForm.ShowDialog();
                }

                if (string.IsNullOrEmpty(notesForExcessTrimForm.Comment))
                {
                    command = new SqlCommand("INSERT INTO [Excess Trim by Allocation Reason Table] SELECT " + allocationID + ", " + priority + ", " + excessTrimReasonForm.ComboBoxItemOption.Key + ", NULL", connection);
                }
                else
                {
                    command = new SqlCommand("INSERT INTO [Excess Trim by Allocation Reason Table] SELECT " + allocationID + ", " + priority + ", " + excessTrimReasonForm.ComboBoxItemOption.Key + ", '" + notesForExcessTrimForm.Comment.Replace("'", "''") + "'", connection);
                }

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                notesForExcessTrimForm.Dispose();
            }

            excessTrimReasonForm.Dispose();
        }
    }
}
