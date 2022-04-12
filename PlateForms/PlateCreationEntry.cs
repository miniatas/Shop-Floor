using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using ShopFloor;


using ShopFloor.Classes;
using ShopFloor.Classes.PlateCreation;
using Shop_Floor.Classes.PlateCreation;

namespace ShopFloor
{
    public partial class PlateCreationEntry : UserControl
    {

     
        private List<PlateCreationSearchResults> searchResultsList = new List<PlateCreationSearchResults>();
        private PlateCreationSearchResults selectedSearchResult = new PlateCreationSearchResults();
        private BindingSource searchResults = new BindingSource();
        private BindingSource plateStatusInfo = new BindingSource();
        private DataTable statusTable;
        private string checkInOutUser = StartupForm.UserName;
        private ProductionPlateNumberInfo selectedPlateNumber = new ProductionPlateNumberInfo();
        private List<ProductionPlateNumberInfo> plateNumberList = new List<ProductionPlateNumberInfo>();

        private string[] searchOptions = { "Job #" , "POW #"};
        private string tempLocation;
        private string tempPlateStatus;
        private string textNotesTemp;

        private bool locationChangeActive = false;
        private bool isCheckIn = false;
        private bool isNotesActive = false;
        private bool isSearchError = false;
        
        private bool isActive = true;

        private int? selectedOW;



        private SqlConnection inventoryControl = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=sa;Password=" + StartupForm.Password + ";database=Inventory Control" + ";Connection Timeout=60;Persist Security Info=False");
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable searchDataTable;
        List<PlateCreationSearchResults> searchResultItems;


        // Querry Strings for select statements
        private string[] querryStrings = new string[]{

        
            "Select * FROM [Inventory Control].[dbo].[Get Item Info By JJ]",
                "SELECT  a.[ItemNo], b.[CustName], a.PowNumber, a.VersionNumber,a.[ItemName], a.[ItemActive] FROM [JobJackets].[dbo].[tblItem] " +
            "a INNER JOIN [JobJackets].[dbo].[tblCustomer] b ON a.[CustID] = b.[CustID] Where a.[ItemNo] = [dbo].[Get OW From JobJacketNo](@JJInfo)"

        };

        private string[] errorStrings = new string[] {"Search Field Empty",
            "Only numbers allowed with this search"};


        private enum SearchType { JobJacketNumber, POWNumber };
        private enum QuerryStrings { searchByJJ, searchByPOW };

        private enum ErrorStrings { emptyOrNull, numbersOnly };
        private enum DropDownOptions { JobNumber, POW };
        public PlateCreationEntry()
        {
            InitializeComponent();


        }

        private void Plate_Inventory_Load(object sender, EventArgs e)
        {
            //Loads drop down for search choices
            LoadSearchDrop(searchOptions);

        }

        private void LoadSearchDrop(string[] searchOptions)
        {

            int iterator = 0;

            foreach (string searchField in searchOptions)
            {

                // Search Item is found in the "Classes" folder
                SearchByCombo.Items.Add(new SearchItem(searchField, iterator));
                iterator++;
            }

            // Sets the first index to "OW Item" as the default search
            SearchByCombo.SelectedIndex = 0;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            //Once the search starts, disable it so it cannot be activated multiple times
            SearchByButton.Enabled = false;

            SearchErrorLabel.Text = "";

            if (string.IsNullOrEmpty(SearchByText.Text))
            {
                SearchErrorLabel.Text = "You must have a typed entry to search.";
                SearchByButton.Enabled = true;
                SearchResultCombo.Enabled = false;
                isSearchError = true;

                return;
            }
            else
                isSearchError = false;

            SearchByField();

            //After search is finished, reenable button.
            SearchByButton.Enabled = true;

            //send Search results to be diplayed in other comboBox

        }

        private void SearchByField() {


            SearchResultCombo.Enabled = true;
            searchDataTable = new DataTable();
            string textField = SearchByText.Text;
            var querryAdaptor = new SqlDataAdapter();

            //clear prior search results
            SearchResultCombo.Items.Clear();

            //Create new search item
            SearchItem searchSelected = SearchByCombo.SelectedItem as SearchItem;

            //Determines the search type, searches by that type
            if (searchSelected.Id == (int)DropDownOptions.JobNumber)
            {
                querryAdaptor = RunSearchQuerry(querryStrings[(int)QuerryStrings.searchByJJ], textField);
            }

            if (searchSelected.Id == (int)DropDownOptions.POW)
            {
                querryAdaptor = RunSearchQuerry(querryStrings[(int)QuerryStrings.searchByPOW], textField);
            }

            querryAdaptor.Fill(searchDataTable);
            ConvertSearchTableToList(searchDataTable);

            // Close adapter once finished
            querryAdaptor.SelectCommand.Connection.Close();
            inventoryControl.Close();

            BindSearchResults();

        }

        private void ConvertSearchTableToList(DataTable searchDataTable) {

            foreach (DataRow row in searchDataTable.Rows)
            {
                PlateCreationSearchResults temp = new PlateCreationSearchResults();

                temp.Company = row.Field<string>("Customer Name");
                temp.ItemDescription = row.Field<string>("Description");
                temp.OWItem = row.Field<int>("Item No");
                temp.JobJacketNumber = row.Field<int>("Last Job Number");
                temp.POW = row.Field<int>("POW");
                temp.Version = row.Field<int>("Version");

                // Adds this info to the search results drop down
                searchResultItems.Add(temp);
            }
        }


        private SqlDataAdapter RunSearchQuerry<T>(string querryCommand, T formEntry)
        {
            adapter = new SqlDataAdapter();

            var cmd = new System.Data.SqlClient.SqlCommand();



            var formEntryFix = "(" + formEntry + ")";
            if (inventoryControl.State == ConnectionState.Closed)
            {
                inventoryControl.Open();
            }
            cmd.Connection = inventoryControl;
            cmd.CommandType = CommandType.Text;

            // Set up to avoid SQL injection attacks
            cmd.CommandText = querryCommand + formEntryFix;

            adapter.SelectCommand = cmd;

            return adapter;
        }

        private string GetSearchResultString(PlateCreationSearchResults searchResults)
        {      

            string comboString = searchResults.Company + "  " + searchResults.ItemDescription + 
                "   JJ: " + searchResults.JobJacketNumber.ToString() + "     POW: " + searchResults.POW.ToString() + 
                "  Version: " + searchResults.Version.ToString();
                       
            return comboString;
        }


   
    private void BindSearchResults()
        {
            int iterator = 0;
            foreach (PlateCreationSearchResults searchResult in searchResultItems)
            {

                SearchResultCombo.Items.Add(new SearchItem(GetSearchResultString(searchResult), iterator));
                iterator++;

            }
            if (SearchResultCombo.Items.Count == 0)
            {
                SearchResultCombo.Items.Add(new SearchItem("No search results found.", 0));
                isSearchError = true;
                ClearSearchResultFields();
            }
            SearchResultCombo.SelectedIndex = 0;
        }
        //Displays the extra information once a user makes a choice from the search results drop down
        private void SearchResultCombo_Click(object sender, EventArgs e)
        {
            if (isSearchError == true) { return; }
            int itemSelection = SearchResultCombo.SelectedIndex;

            this.selectedSearchResult = searchResultItems[itemSelection];
        }

        private List<ProductionPlateNumberInfo> QueryPlateNumbers() {

            List<ProductionPlateNumberInfo> plateNumbers = new List<ProductionPlateNumberInfo>();

            if (selectedSearchResult.JobJacketNumber != null) {



                }

            return plateNumbers;
        }

        private void OnPlateNumberSelection() {

            int index = SelectPlateCombo.SelectedIndex;

            selectedPlateNumber = plateNumberList[index];
            
            ColorDisplayLabel.Text = selectedPlateNumber.Color;
            LPIDisplayLabel.Text = selectedPlateNumber.LPI.ToString();
            

        }

        private void SaveTiff() {



        }

        private void ClearSearchResultFields()
        {
            SearchByText.Text = "";

            SearchResultCombo.Items.Clear();
            SelectPlateCombo.Items.Clear();
            ColorDisplayLabel.Text = "";
            LPIDisplayLabel.Text = "";
            SearchErrorLabel.Text = "";

            TiffLengthText.Text = "";
            TiffWidthText.Text = "";
            this.selectedSearchResult = new PlateCreationSearchResults();

            CroppedWidthText.Text = "";
            CroppedLengthText.Text = "";

        }

        private void SearchResultCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SearchByCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
