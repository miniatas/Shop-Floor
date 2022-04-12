using Shop_Floor.PlateForms.PlateProperties;
using ShopFloor.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace ShopFloor
{
    public partial class Plate_Inventory : Form
    {
        private List<string> clickRowHeader = new List<string> { "Company", "Item Description", "WO Item #" };
        private List<SearchResults> searchRestulsExample = new List<SearchResults>();
        private BindingSource searchResults = new BindingSource();
        private BindingSource plateStatusInfo = new BindingSource();
        private BindingSource calculatedTiffFields = new BindingSource();
        private DataTable statusTable;
        private string checkInOutUser = StartupForm.UserName;
        
        private string[] searchOptions = { "Company", "Item", "OW Item", "Job #" };
        private string tempLocation;
        private string tempPlateStatus;
        private string textNotesTemp;

        private bool locationChangeActive = false;
        private bool isCheckIn = false;
        private bool isNotesActive = false;
        private bool isSearchError = false;

        private bool isActive = true;

        private int? selectedOW;
       
       

        private SqlConnection connection1 = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=Inventory Control" + ";Connection Timeout=60;Persist Security Info=False");
       
        private SqlDataAdapter adapter;
        private DataTable searchDataTable;
        List<SearchResults> searchItems;
        

        // Querry Strings for select statements
        private string [] querryStrings= new string[]{"Select * FROM [Inventory Control].[dbo].[Get Item Info By Company]",
            "Select * FROM [Inventory Control].[dbo].[Get Item Info By Descrip]",
            "Select * FROM [Inventory Control].[dbo].[Get Item Info By OWItem]",
            "Select * FROM [Inventory Control].[dbo].[Get Item Info By JJ]",
            "SELECT [PlateLocation] FROM [dbo].[PlateLocation] WHERE ",
            "SELECT [OWItem#],[CheckInStatus],[CheckInDate],[CheckOutDate],[CheckInUser],[CheckOutUser],[PlateNotes],[TimesCheckedIn] FROM[dbo].[PlateCheckInStatus] WHERE "

        };

        private string[] errorStrings = new string[] {"Search Field Empty",
            "Only numbers allowed with this search"};
            

        private enum SearchType { company, description, owItem, lastJJ };
        private enum QuerryStrings { searchByCompany, searchByDescrip, searchByOW, searchByJJ, searchPlateLocation, searchStatus};

        private enum ErrorStrings { emptyOrNull, numbersOnly};
        private enum DropDownOptions { company, itemDescription, OWItem, LastJJ};
     
        public Plate_Inventory()
        {
            InitializeComponent();

        }

        

        private void Plate_Inventory_Load(object sender, EventArgs e)
        {
            //Loads drop down for search choices
            LoadSearchDrop(searchOptions);
       
        }

        private void LoadSearchDrop(string[] searchOptions) {

            int iterator = 0;

            foreach (string searchField in searchOptions) {

                // Search Item is found in the "Classes" folder
                dropSearchType.Items.Add(new SearchItem(searchField, iterator));
                iterator++;
            }
         
            // Sets the first index to "OW Item" as the default search
            dropSearchType.SelectedIndex = 2;
        }

        private void searchButton_Click(object sender, EventArgs e) {

            

            //Once the search starts, disable it so it cannot be activated multiple times
            searchButton.Enabled = false;

            labelSearchWarning.Text = "";

            if (string.IsNullOrEmpty(textSearch.Text))
            {
                labelSearchWarning.Text = "You must have a typed entry to search.";
                searchButton.Enabled = true;
                dropSearchResults.Enabled = false;
                isSearchError = true;
                
                return;
            }
            else
                isSearchError = false;

            dropSearchResults.Enabled = true;
            string field = "";


            //clear prior search results
            dropSearchResults.Items.Clear();

            //Create new search item
            SearchItem searchSelected = dropSearchType.SelectedItem as SearchItem;
            
            //Determines the search type, searches by that type
            if (searchSelected.Id == (int)DropDownOptions.company)
            {
                field = "Company";
                SqlSearcher(SearchType.company, field);
            }

            if (searchSelected.Id == (int)DropDownOptions.itemDescription)
            {
                field = "ItemDescription";
      


                SqlSearcher(SearchType.description, field);

            }

            if (searchSelected.Id == (int)DropDownOptions.OWItem)
            {
                field = "OWItem#";
                bool isNum;
                isNum = int.TryParse(textSearch.Text.Trim(), out int temp);


                if (!isNum)
                {
                    labelSearchWarning.Text = "You must enter a valid number for this search";
                    searchButton.Enabled = true;
                    ClearSearchResultFields();
                    return;
                }
                SqlSearcher(SearchType.owItem, field);
            }

            if (searchSelected.Id == (int)DropDownOptions.LastJJ)
            {
                field = "LastJJ";
                bool isNum;
                isNum = int.TryParse(textSearch.Text.Trim(), out int temp);


                if (!isNum)
                {
                    labelSearchWarning.Text = "You must enter a valid number for this search";
                    ClearSearchResultFields();
                    searchButton.Enabled = true;
                    return;
                }
                SqlSearcher(SearchType.lastJJ, field);
            }

            BindSearchResults();
            //After search is finished, reenable button.
            searchButton.Enabled = true;

            //send Search results to be diplayed in other comboBox

        }

        private bool CheckNullEntry(string entry) {


            if (string.IsNullOrEmpty(textSearch.Text))
            {
                return true;
            }
            else
                return false;

        }

        // Searches for the plate options
        private void SqlSearcher(SearchType type, string field)
        {
            
            searchDataTable = new DataTable();
            searchItems = new List<SearchResults>();
            string textField = textSearch.Text;
            var querryAdaptor = new SqlDataAdapter();

            // Changes textfield querry by type
            textField = QuerryByType(type, textField);

            if (type == SearchType.company)
            {
                // Returns the adaptor for the querry
                querryAdaptor = RunSearchQuerry(querryStrings[(int)QuerryStrings.searchByCompany], textField);
            }

            else if (type == SearchType.description)
            {
                // Returns the adaptor for the querry
                querryAdaptor = RunSearchQuerry(querryStrings[(int)QuerryStrings.searchByDescrip], textField);
            }

            else if (type == SearchType.owItem)
            {
                // Returns the adaptor for the querry
                querryAdaptor = RunSearchQuerry(querryStrings[(int)QuerryStrings.searchByOW], textField);
            }

            else if (type == SearchType.lastJJ)
            {
                // Returns the adaptor for the querry
                querryAdaptor = RunSearchQuerry(querryStrings[(int)QuerryStrings.searchByJJ], textField);
            }
            else
            {
           
                throw new Exception("Missing correct Enum Type" + type);
            }

            // Fills DataTable
            querryAdaptor.Fill(searchDataTable);

            // Close adapter once finished
            querryAdaptor.SelectCommand.Connection.Close();
            connection1.Close();
            
            // Checks each row
            foreach (DataRow row in searchDataTable.Rows)
            {
                SearchResults temp = new SearchResults();

                temp.CompanyName = row.Field<string>("Customer Name");
                temp.ItemDescription = row.Field<string>("Description");
                temp.OW_Item = row.Field<int>("Item No");
                temp.LastJJ = row.Field<int>("Last Job Number");
                temp.IsActive = row.Field<bool>("ItemActive");
                
                // Adds this info to the search results drop down
                searchItems.Add(temp);
            }
        }

        private SqlDataAdapter RunQuerry<T>(string querryCommand, string field, T formEntry) {

            adapter = new SqlDataAdapter();
          
            var cmd = new System.Data.SqlClient.SqlCommand();

            if (connection1.State == ConnectionState.Closed)
            {
                connection1.Open();
            }
                cmd.Connection = connection1;
                cmd.CommandType = CommandType.Text;

            // Set up to avoid SQL injection attacks
                cmd.CommandText = querryCommand + field + " = @field";
                cmd.Parameters.AddWithValue("@field", formEntry);
                adapter.SelectCommand = cmd;

                return adapter;

            

        }

        private SqlDataAdapter RunSearchQuerry<T>(string querryCommand, T formEntry)
        {
            adapter = new SqlDataAdapter();

            var cmd = new System.Data.SqlClient.SqlCommand();
            var formEntryFix = "(" + formEntry + ")";
            if (connection1.State == ConnectionState.Closed)
            {
                connection1.Open();
            }
            cmd.Connection = connection1;
            cmd.CommandType = CommandType.Text;

            // Set up to avoid SQL injection attacks
            cmd.CommandText = querryCommand + formEntryFix;
         
            adapter.SelectCommand = cmd;

            return adapter;
        } 
        // When a wild card search is needed
        private SqlDataAdapter RunQuerryWild<T>(string querryCommand, string field, T formEntry)
        {

            adapter = new SqlDataAdapter();

            var cmd = new System.Data.SqlClient.SqlCommand();
            if (connection1.State == ConnectionState.Closed)
            {
                connection1.Open();
            }

            cmd.Connection = connection1;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = querryCommand + field + " LIKE @field";

            T querryText = formEntry;

            cmd.Parameters.AddWithValue("@field", formEntry);
            adapter.SelectCommand = cmd;

            return adapter;

        }

        private string QuerryByType(SearchType type, string formEntryString) {

            //Gives the entry a "wildcard" tag
            if (type == SearchType.company || type == SearchType.description)
            {
                formEntryString = "'%" + formEntryString + "%'";
            }

            return formEntryString;
        }

        // Binds the search results to the dropdown
        private void BindSearchResults()
        {
            int iterator = 0;
            foreach (SearchResults searchResult in searchItems)
            {

                dropSearchResults.Items.Add(new SearchItem(searchResult.GetSearchString(), iterator));
                iterator++;

            }
            if (dropSearchResults.Items.Count == 0)
            {
                dropSearchResults.Items.Add(new SearchItem("No search results found.", 0));
                isSearchError = true;
                ClearSearchResultFields();
            }
            dropSearchResults.SelectedIndex = 0;
        }
        //Displays the extra information once a user makes a choice from the search results drop down
        private void itemDescrip_Click(object sender, EventArgs e)
        {
            if (isSearchError == true) { return; }
            int itemSelection = dropSearchResults.SelectedIndex;

            selectedOW = searchItems[itemSelection].OW_Item;

            textCustomer.Text = searchItems[itemSelection].CompanyName;
            textDescription.Text = searchItems[itemSelection].ItemDescription;
            textOwItem.Text = selectedOW.ToString();
            textJJ.Text = searchItems[itemSelection].LastJJ.ToString();
            isActive = searchItems[itemSelection].IsActive;
            CheckIfActive();
            FindLocation(selectedOW);
            FindStatus(selectedOW);

        }

        private void ClearSearchResultFields()
        {
            textLocation.Text = "";

            textDescription.Text = "";
            textOwItem.Text = "";
            textJJ.Text = "";
            textCustomer.Text = "";

            textPlateStatus.Text = "";
            textStatus.Text = "";
            textStatus.BackColor = Color.White;

            gridStatusView.DataSource = null;
            gridStatusView.Columns.Clear();
            gridStatusView.Rows.Clear();
            gridStatusView.Refresh();


        }

        //Finds plate location
        private void FindLocation(int? owItem) {

            var locationTable= new DataTable();
            
            string field = "OWItem#";
            adapter = new SqlDataAdapter();
            adapter = RunQuerry(querryStrings[(int)QuerryStrings.searchPlateLocation], field, owItem);
            adapter.Fill(locationTable);
            adapter.SelectCommand.Connection.Close();
            connection1.Close();

            string location = locationTable.Rows[0]["PlateLocation"].ToString().Trim();

            if (string.IsNullOrEmpty(location)) 
            {

                location = "No Location";
            }

            textLocation.Text = location;
        }

       
        private void FindStatus(int? owItem) {

            statusTable = new DataTable();
            string field = "OWItem#";

            adapter = new SqlDataAdapter();
            adapter = RunQuerry(querryStrings[(int)QuerryStrings.searchStatus], field, owItem.ToString());
            adapter.Fill(statusTable);
            adapter.SelectCommand.Connection.Close();
            connection1.Close();

            // Sorts the dataview, so that it will match the datatable rows for selection purposes
            DataView dataView = new DataView(statusTable);
            dataView.Sort = "CheckInDate desc";
            statusTable = dataView.ToTable();

            if (Convert.IsDBNull(statusTable.Rows[0]["CheckInStatus"]))
            {
                isCheckIn = false;
                ChangeCheckInButtonText(isCheckIn);
            }
            else
            {
                isCheckIn = Convert.ToBoolean(statusTable.Rows[0]["CheckInStatus"]);
                ChangeCheckInButtonText(isCheckIn);
                textPlateStatus.Text = statusTable.Rows[0]["PlateNotes"].ToString();
                statusTable.Columns.Remove(statusTable.Columns["CheckInStatus"]);
            }
            BindPlateStatusInfo(statusTable);
        }

        //Method to swtich "Check In" text for the button
        private void ChangeCheckInButtonText(bool checkInStatus)
        {
            if (checkInStatus == true)
            {
                buttonCheckIn.Text = "Check Out";

            }
            else
                buttonCheckIn.Text = "Check In";

            //Converts bool status into string message
            textStatus.Text = CheckInBoolToText(checkInStatus);
            PaintCheckInText();
        }
        private string CheckInBoolToText(Nullable<bool> checkIn) {

            string checkInText = "Checked Out";

            if (checkIn == (bool?)null)
            {
                checkInText = "Never Checked In";
                isCheckIn = false;
            }

            if (checkIn == true)
            {
                checkInText = "Checked In";
            }

            return checkInText;
        }

        private void BindPlateStatusInfo(DataTable binding) {

            plateStatusInfo.DataSource = binding;
            gridStatusView.DataSource = plateStatusInfo;
            gridStatusView.Columns["TimesCheckedIn"].Visible = false;
            gridStatusView.Sort(this.gridStatusView.Columns["TimesCheckedIn"], ListSortDirection.Descending);
                       
        }

        private void locationChange_Click(object sender, EventArgs e) {

            if (locationChangeActive == false)
            {

                tempLocation = textLocation.Text.Trim();
                textLocation.ReadOnly = false;
                locationChangeActive = true;

                buttonCancelLocation.Visible=true;
                buttonCancelLocation.Enabled = true;

                buttonLocationChange.Text = "Save";

            }
            else
            {
                locationChangeActive = false;
                textLocation.ReadOnly = true;
                buttonCancelLocation.Visible = false;
                buttonCancelLocation.Enabled = false;
                buttonLocationChange.Text = "Change";
                bool success = SaveLocationDB(selectedOW);
            }

        }

        private bool SaveLocationDB(int? owItem)
        {
            if (selectedOW == 0 )
            {
                return false;
            }
            else
            {
                bool success = false;
                var cmd = new System.Data.SqlClient.SqlCommand();

                buttonLocationChange.Enabled = false;

                if (connection1.State == ConnectionState.Closed)
                {
                    connection1.Open();
                }
                cmd.Connection = connection1;
                cmd.CommandType = CommandType.Text;

                //pulls in private OWnumber
                cmd.CommandText = "UPDATE [dbo].[PlateLocation] SET [PlateLocation] = @field , [PlateLocationDate] = @plateDate WHERE OWItem#= @owItem";
                cmd.Parameters.AddWithValue("@field", textLocation.Text.Trim());
                cmd.Parameters.AddWithValue("@plateDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.AddWithValue("@owItem", selectedOW);
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                connection1.Close();
                buttonLocationChange.Enabled = true;
                return success;

            }
        }

        private void buttonChangeNotes_Click(object sender, EventArgs e)
        {
            
            if (isNotesActive == false)
            {
                textNotesTemp = textPlateStatus.Text.Trim();
                isNotesActive = true;
                textPlateStatus.ReadOnly = true;
                tempPlateStatus = textPlateStatus.Text.Trim();
                textPlateStatus.ReadOnly = false;
                buttonCancelNotes.Visible = true;
                buttonCancelNotes.Enabled = true;
                buttonChangeNotes.Enabled = true;
                buttonChangeNotes.Text = "Save";

            }
            else
            {
                isNotesActive = false;
                textPlateStatus.ReadOnly = false;
                buttonCancelNotes.Visible = false;
                buttonCancelNotes.Enabled = false;
               
                bool success = SaveNotesDB(selectedOW);

                buttonChangeNotes.Enabled = true;
                buttonChangeNotes.Text = "Change";
            }
            buttonChangeNotes.Enabled = true;

        }

        // Check in morning
        private bool SaveNotesDB(int? selectedOW)
        {
            bool success = false;
            var cmd = new System.Data.SqlClient.SqlCommand();

            buttonChangeNotes.Enabled = false;

            if (connection1 != null && connection1.State == ConnectionState.Closed)
            {
                connection1.Open();
            }
            cmd.Connection = connection1;
            cmd.CommandType = CommandType.Text;

            if (!Convert.IsDBNull(statusTable.Rows[0]["TimesCheckedIn"]))
            {
                //pulls in private OWnumber
                cmd.CommandText = "UPDATE [dbo].[PlateCheckInStatus] SET [PlateNotes] = @field WHERE OWItem#= @owItem and TimesCheckedIn = @timesCheckedIn";
                cmd.Parameters.AddWithValue("@timesCheckedIn", statusTable.Rows[0]["TimesCheckedIn"]);
            }
            else
            {
                cmd.CommandText = "UPDATE [dbo].[PlateCheckInStatus] SET [PlateNotes] = @field WHERE OWItem#= @owItem";
            }

            cmd.Parameters.AddWithValue("@field", textPlateStatus.Text.Trim());
            cmd.Parameters.AddWithValue("@owItem", selectedOW);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            connection1.Close();
            statusTable.Rows[0]["PlateNotes"] = textPlateStatus.Text.Trim();
            BindPlateStatusInfo(statusTable);
            buttonChangeNotes.Enabled = true;
            return success;

        }

        class SearchResults
        {

            private string companyName;
            private string itemDescription;
            private int? ow_Item;
            private int lastJJ;
            private bool itemActive;
            private string lastOrderDate;
            private int? yearsOld;
            

            public SearchResults()
            {
               
            }                  
            
            public int? OW_Item { get => ow_Item; set => ow_Item = value; }
            public string CompanyName { get => companyName; set => companyName = value; }
            public string ItemDescription { get => itemDescription; set => itemDescription = value; }
            public int LastJJ { get => lastJJ; set => lastJJ = value; }
            public string LastOrderDate { get => lastOrderDate; set => lastOrderDate = value; }
            public int? YearsOld { get => yearsOld; set => yearsOld = value; }

            public bool IsActive { get => itemActive; set => itemActive = value; }
            public string GetSearchString() {

                if (OW_Item == (int?)null) {

                    return "Error:  Missing OW Item Number";
                }

                string comboString = CompanyName + "       " + ItemDescription + "     WO: " + OW_Item.ToString() + "     JJ: " + LastJJ;



                return comboString;
            }

       
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void cancelLocation(object sender, EventArgs e)
        {
            textLocation.Text = tempLocation;
            textLocation.ReadOnly = true;
            buttonLocationChange.Text = "Change";
            locationChangeActive = false;
            buttonCancelLocation.Enabled = false;
            buttonCancelLocation.Hide();
        }
        
        private void buttonCancelNotes_Click(object sender, EventArgs e)
        {
            buttonCancelNotes.Text = textNotesTemp;
            textPlateStatus.ReadOnly = true;
            buttonChangeNotes.Text = "Change";
            isNotesActive = false;
            buttonCancelNotes.Enabled = false;
            buttonCancelNotes.Hide();
        }

        private void CheckIn_Click(object sender, EventArgs e)
        {

            buttonCheckIn.Enabled = false;

            if (isCheckIn == true)
            {

                var confirmResult = MessageBox.Show("Are you sure you want to check out the plates for " + selectedOW + "?",
                                                    "Confirm Checkout.",
                                                     MessageBoxButtons.OKCancel);

                try
                {
                    if (confirmResult == DialogResult.OK)
                    {
                        string checkOutUser = StartupForm.UserName;
                        UpdateCheckOutDB(checkInOutUser);
                        isCheckIn = false;
                        textStatus.Text = CheckInBoolToText(isCheckIn);
                        ChangeCheckInButtonText(isCheckIn);
                        FindStatus(selectedOW);


                    }
                    else
                    {
                        // Do nothing
                    }
                }
                catch (Exception ex)
                {


                    //public static void SendEmail(int groupID, string emailSubject, string emailMessage)
                    ModulesClass.SendEmail(4, "PlateInventory Fail, checkout", ex.Message + "\n" + ex.Source);
                    MessageBox.Show(ex.Message);

                }



            }
            else
            {
                var confirmResult = MessageBox.Show("Are you sure you want to check in the plates for " + selectedOW + "?",
                                                    "Confirm Check In.",
                                                     MessageBoxButtons.OKCancel);

                try
                {
                    if (confirmResult == DialogResult.OK)
                    {

                        UpdateCheckInDB(checkInOutUser);
                        isCheckIn = true;
                        SaveLocationDB(selectedOW);
                        textStatus.Text = CheckInBoolToText(isCheckIn);
                        ChangeCheckInButtonText(isCheckIn);
                        FindStatus(selectedOW);

                    }
                    else
                    {
                        // Do nothing
                    }
                }
                catch (Exception ex)
                {


                    //public static void SendEmail(int groupID, string emailSubject, string emailMessage)
                    ModulesClass.SendEmail(4, "PlateInventory Fail, check in", ex.Message + "\n" + ex.Source);
                    MessageBox.Show(ex.Message);

                }
            }
           

                buttonCheckIn.Enabled = true;
            }
        

        private void PaintCheckInText()

        {
            if (isCheckIn == false) 
                textStatus.BackColor = Color.Red;
            else
                textStatus.BackColor = Color.Green;
        }

        private void UpdateCheckOutDB(string userName)
        {
            
            var cmd = new System.Data.SqlClient.SqlCommand();

            if (connection1 != null && connection1.State == ConnectionState.Closed)
            {
                connection1.Open();
            }

                cmd.Connection = connection1;
            cmd.CommandType = CommandType.Text;

            
            int? newTimeCheckedIn = (int)statusTable.Rows[0]["TimesCheckedIn"]; 
            

            cmd.CommandText = "UPDATE [dbo].[PlateCheckInStatus] SET [CheckInStatus] = @isCheckIn, [CheckOutUser] = @userName, [CheckOutDate] = @checkOutDate" +
                    " WHERE [OWItem#] = @OWItem AND [TimesCheckedIn] = @currentTimesCheckedIn";

            cmd.Parameters.AddWithValue("@currentTimesCheckedIn", newTimeCheckedIn);
            
            cmd.Parameters.AddWithValue("@isCheckIn", false);
            cmd.Parameters.AddWithValue("@userName", userName);
            cmd.Parameters.AddWithValue("@checkOutDate", SqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.AddWithValue("@OWItem", selectedOW);
            

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            connection1.Close();
        }

        private void UpdateCheckInDB(string userName)
        {

            int newTimesCheckedIn = 0;

            if (!DBNull.Value.Equals(statusTable.Rows[0]["TimesCheckedIn"]))
            {
                newTimesCheckedIn = (int)statusTable.Rows[0]["TimesCheckedIn"];
            }
            newTimesCheckedIn++;
            var cmd = new System.Data.SqlClient.SqlCommand();
            if (connection1.State == ConnectionState.Closed)
            {
                connection1.Open();
            }

            cmd.Connection = connection1;
            cmd.CommandType = CommandType.Text;

            if (Convert.IsDBNull(statusTable.Rows[0]["TimesCheckedIn"]))
            {
                try
                {
                    cmd.CommandText = "UPDATE [dbo].[PlateCheckInStatus] SET [CheckInStatus] = @isCheckIn, [CheckOutUser] = @userName, [TimesCheckedIn] = 1, [CheckInDate] = @checkInDate" +
                      " WHERE [OWItem#] = @OWItem";

                    cmd.Parameters.AddWithValue("@checkInDate", SqlDbType.DateTime).Value = DateTime.Now;
                }
                catch (Exception ex)
                {

                    //public static void SendEmail(int groupID, string emailSubject, string emailMessage)
                    ModulesClass.SendEmail(4, "PlateInventory Fail, first time checkin", ex.Message + "\n" + ex.Source);
                }
            }

            else
                try
                {
                    {
                        cmd.CommandText = "Insert INTO [dbo].[PlateCheckInStatus] ([CheckInStatus] , [CheckInUser], [CheckInDate], [OWItem#], [PlateNotes], [TimesCheckedIn])" +
                            " VALUES (@isCheckIn, @userName, @checkInDate, @OWItem, @plateNotes, @currentTimesCheckedIn)";

                        cmd.Parameters.AddWithValue("@checkInDate", SqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                catch (Exception ex) {

                    //public static void SendEmail(int groupID, string emailSubject, string emailMessage)
                    ModulesClass.SendEmail(4, "PlateInventory Fail, repeat checkin", ex.Message + "\n" + ex.Source);
                }
            try
            {
                cmd.Parameters.AddWithValue("@isCheckIn", true);
                cmd.Parameters.AddWithValue("@userName", userName);

                cmd.Parameters.AddWithValue("@OWItem", selectedOW);
                cmd.Parameters.AddWithValue("@plateNotes", textPlateStatus.Text.Trim());
                cmd.Parameters.AddWithValue("@currentTimesCheckedIn", newTimesCheckedIn);

            }
            catch (Exception ex) {

                //public static void SendEmail(int groupID, string emailSubject, string emailMessage)
                ModulesClass.SendEmail(4, "PlateInventory Fail, Value addition fail", ex.Message + "\n" + ex.Source);
            }
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            connection1.Close();

         
        }

        PlateTiffRowDisplay SelectedPlateTiffRowDisplay { get; set; } = new PlateTiffRowDisplay();
        private void textSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                searchButton.PerformClick();
                e.Handled = true;
            }
        }

        private void CheckIfActive()
        {
            if (this.isActive)
            {

                InactivePanel.Hide();
                InActive.Hide();
                buttonCheckIn.Show();
            }
            else {

                InactivePanel.Show();
                InActive.Show();
                buttonCheckIn.Hide();
            }
        }

      
    }
    }



