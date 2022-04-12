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

using ShopFloor.Classes.PlateCreation;
using ShopFloor.Classes;
using UserControlsForShopFloor.Classes;
using UserControlsForShopFloor.BusinessClasses;
using UserControlsForShopFloor.PropertyClasses;

namespace ShopFloor
{
    public partial class PlateCreationEntry : UserControl
    {

        private const string plateEdgeLengthDefault = ".5";
        private List<PlateCreationSearchResults> searchResultsList;
        private PlateCreationSearchResults selectedSearchResult;
        private BindingSource searchResults;
        private BindingSource plateStatusInfo;
        private PlateTiffInfo plateTiffInfoFromTextBox;
        private bool isSavingEnabled = false;
        //private string checkInOutUser = StartupForm.UserName;
        private ProductionPlateNumberInfo selectedPlateNumber;
        private List<ProductionPlateNumberInfo> plateNumberList;
        private bool isSearchError;
        private string[] searchOptions = { "Job #", "POW #" };
        private bool hasBeenSaved;
        private bool isRemoveSuccess;
        private PlateDBSaveLogic plateDBSaveLogic;

        private bool isSaved;

        private SqlConnection connString = new SqlConnection();
        private SqlConnection connectionTest = new SqlConnection("Data Source=ovesql01;User ID=testUser;Password=21TriedToDrink;database=Inventory Control;Connection Timeout=60;Persist Security Info=False");
        private SqlConnection connectionTest2 = new SqlConnection("Data Source=ovesql02;User ID=testUser;Password=21TriedToDrink;database=Inventory Control;Connection Timeout=60;Persist Security Info=False");


        private SqlDataAdapter adapter;
        private DataTable searchDataTable;
        List<PlateCreationSearchResults> searchResultItems;

        // Plate Till info

        // Querry Strings for select statements
        private string[] querryStrings = new string[]{

                "SELECT  a.[ItemNo], b.[CustName], a.PowNumber, a.ItemName, a.VersionNumber, @JJNum, a.[ItemActive] FROM [JobJackets].[dbo].[tblItem] " +
            "a INNER JOIN [JobJackets].[dbo].[tblCustomer] b ON a.[CustID] = b.[CustID] Where a.[ItemNo] = "+
            "(select [ItemNo] FROM ovesql02.[JobJackets].[dbo].[tblJobTicket] where [JobJacketNo] = @JJNum)",

             "Select * from ovesql02.Manufacturing.dbo.PlateProduction pp " +
            "JOIN ovesql02.Manufacturing.dbo.PlateProductionGroupPlates ppgp on pp.ProductionPlateID = ppgp.PlateProductionID " +

            "JOIN ovesql02.Manufacturing.dbo.PlateProductionGroup ppg on ppgp.PlateProductionGroupID = ppg.ProductionPlateGroupID " +
            "JOIN ovesql02.Manufacturing.dbo.ArtInfo ai on ai.ProductionPlateGroupID = ppg.ProductionPlateGroupID " +
            "JOIN ovesql02.Manufacturing.dbo.POW p on p.ArtInfoID = ai.ArtInfoID where OWItemNo = @OWItem"

        };

        private string[] parameterChoice = new string[] { "JJNum", "OWItem" };

        private string[] errorStrings = new string[] {"Search Field Empty",
            "Only numbers allowed with this search"};


        private enum SearchType { JobJacketNumber, POWNumber };
        private enum QuerryStrings { searchByJJ, searchByPOW };

        private enum ErrorStrings { emptyOrNull, numbersOnly };

        private enum DropDownOptions { JobNumber, POW };
        public PlateCreationEntry()
        {
            Init();
        }

        public void Init() {
            searchResults = new BindingSource();
            plateStatusInfo = new BindingSource();
            PlateTiffInfoFromTextBox = new PlateTiffInfo();
            searchResultsList = new List<PlateCreationSearchResults>();
            selectedSearchResult = new PlateCreationSearchResults();
            PlateTillTiffCombos = new PlateTillTiffCombos();


            //Enable for Testing purposes


            PlateTillTiffCombos.PlateTillDimensionsID = 1;

            isRemoveSuccess = false;
            isSearchError = false;
            hasBeenSaved = false;
            InitializeComponent();

            LoadSearchTypeDrop(searchOptions);
            ChangeTextBoxesEnabled(false);
            // For testing
            ConfigureConnectionString(connectionTest);

            SaveErrorLabel.ForeColor = Color.Red;
            SaveTiffButton.Visible = false;
            SaveTiffButton.Enabled = false;

            RemoveTiffButton.Visible = false;
            RemoveTiffButton.Enabled = false;
            PlateTiffInfoFromTextBox.ExtendedCutLength = decimal.Parse(plateEdgeLengthDefault);



            //private string checkInOutUser = StartupForm.UserName;
            selectedPlateNumber = new ProductionPlateNumberInfo();
            plateNumberList = new List<ProductionPlateNumberInfo>();

            plateDBSaveLogic = new PlateDBSaveLogic();
            searchResultItems = new List<PlateCreationSearchResults>();
            plateDBSaveLogic.Connection = connectionTest2;

            ClearSearchResultFields();
        }
        private void ChangeTextBoxesEnabled(bool isEnabled) {

            TiffWidthText.Enabled = isEnabled;
            TiffLengthText.Enabled = isEnabled;
            CroppedLengthText.Enabled = isEnabled;
            CroppedWidthText.Enabled = isEnabled;
            EdgeScrapText.Enabled = isEnabled;
        }

        // Loads the strings and selection values for the search types
        private void LoadSearchTypeDrop(string[] searchOptions)
        {

            int iterator = 0;

            foreach (string searchField in searchOptions)
            {

                // Search Item is found in the "Classes" folder
                SearchByCombo.Items.Add(new SearchItemSub(searchField, iterator));
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

                isSearchError = true;

                return;
            }
            else
                isSearchError = false;

            SearchByType();

            //After search is finished, reenable button.
            SearchByButton.Enabled = true;

            //send Search results to be diplayed in other comboBox

        }

        private void SearchByType()
        {

            ConfigureConnectionString(connectionTest2);
            SearchResultCombo.Enabled = true;
            searchDataTable = new DataTable();
            string textField = SearchByText.Text;
            var querryAdaptor = new SqlDataAdapter();

            //clear prior search results
            searchResultItems.Clear();
            SearchResultCombo.Items.Clear();

            //Create new search item
            SearchItemSub searchSelected = SearchByCombo.SelectedItem as SearchItemSub;


            //Determines the search type, searches by that type
            if (searchSelected.Id == (int)DropDownOptions.JobNumber)
            {
                querryAdaptor = RunSearchQuerry(querryStrings[(int)QuerryStrings.searchByJJ], textField, parameterChoice[searchSelected.Id]);
            }

            //if (searchSelected.Id == (int)DropDownOptions.POW)
            //{
            //    querryAdaptor = RunSearchQuerry(querryStrings[(int)QuerryStrings.searchByPOW], textField);
            //}

            querryAdaptor.Fill(searchDataTable);
            ConvertSearchTableToList(searchDataTable);

            // Close adapter once finished
            querryAdaptor.SelectCommand.Connection.Close();
            connString.Close();

            BindSearchResults();

        }


        //Binds the rowFields of the search type into a list for use
        private void ConvertSearchTableToList(DataTable searchDataTable)
        {

            foreach (DataRow row in searchDataTable.Rows)
            {
                PlateCreationSearchResults temp = new PlateCreationSearchResults();

                temp.Company = row.Field<string>("CustName");
                temp.ItemDescription = row.Field<string>("ItemName");
                temp.OWItem = row.Field<int>("ItemNo");
                temp.JobJacketNumber = row.ItemArray[5].ToString();
                temp.POW = row.Field<int?>("POWNumber");
                temp.Version = row.Field<Int16?>("VersionNumber");
                temp.IsActive = row.Field<bool?>("ItemActive");


                // Adds this info to the search results drop down
                searchResultItems.Add(temp);
            }
        }

        //Binds the rowFields of the plate search into a list for use
        private void ConvertPlateSearchToList(DataTable plateNumTable)
        {

            foreach (DataRow row in plateNumTable.Rows)
            {
                ProductionPlateNumberInfo temp = new ProductionPlateNumberInfo();

                temp.ProductionPlateNumber = row.Field<int>("ProductionPlateID");
                temp.Color = row.Field<string>("Color");
                temp.PlateTypeID = row.Field<int>("PlateTypeID");
                temp.IsPOWCommon = row.Field<bool>("IsCommon");
          
                temp.InkPercentage = row.Field<int?>("InkPercent");
                temp.LPI = row.Field<int?>("Lpi");

                plateNumberList.Add(temp);
            }
        }

        //Converts a querry command, the search string, and the search parameter into a SqlDataAdapater
        private SqlDataAdapter RunSearchQuerry(string querryCommand, string formEntry, string parameterSelected)
        {
            adapter = new SqlDataAdapter();

            var cmd = new System.Data.SqlClient.SqlCommand();


            if (connString.State == ConnectionState.Closed)
            {
                connString.Open();
            }
            cmd.Connection = connString;
            cmd.CommandType = CommandType.Text;

            // Set up to avoid SQL injection attacks
            cmd.CommandText = querryCommand;
            cmd.Parameters.AddWithValue(parameterSelected, formEntry);

            adapter.SelectCommand = cmd;

            return adapter;
        }


        // Takes the "IsActive" bit and turns it into a string readable
        private string IsActiveReadableTranslation(bool? isActive)
        {

            if (isActive.HasValue)
            {
                if (isActive.Value)
                    return "Active";

                else
                    return "InActive";
            }
            else
            {
                return "Unknown, check system";
            }
        }

        // Concatenates string display for search combobox selection
        private string GetSearchResultString(PlateCreationSearchResults searchResults)
        {
            string activeStatus = IsActiveReadableTranslation(searchResults.IsActive);

            string comboString = "OWItem: " + searchResults.OWItem + " " + searchResults.Company + "  " + searchResults.ItemDescription +
                "   JJ: " + searchResults.JobJacketNumber + "     POW: " + searchResults.POW.ToString() +
                "  Version: " + searchResults.Version.ToString() + " Status: " + activeStatus;

            return comboString;
        }

        // Concatenates string display for plate combobox selection
        private string GetPlateNumberString(ProductionPlateNumberInfo searchResults)
        {

            string comboString = "Plate # " + searchResults.ProductionPlateNumber +
                 " Color: " + searchResults.Color + " LPI: " + searchResults.LPI;


            return comboString;

        }


        // For search type only
        private void BindSearchResults()
        {
            int iterator = 0;
            foreach (PlateCreationSearchResults searchResult in searchResultItems)
            {

                SearchResultCombo.Items.Add(new SearchItemSub(GetSearchResultString(searchResult), iterator));
                iterator++;

            }
            if (SearchResultCombo.Items.Count == 0)
            {
                ClearSearchResultFields();
                SearchResultCombo.Items.Add(new SearchItemSub("No search results found.", 0));
                isSearchError = true;
            }

            SearchResultCombo.SelectedIndex = 0;
        }

        // For plate search only
        private void BindPlateSearchResults()
        {
            isSavingEnabled = false;
            int iterator = 0;
            foreach (ProductionPlateNumberInfo searchResult in plateNumberList)
            {
                int plateNumberID = searchResult.ProductionPlateNumber;
                SelectPlateCombo.Items.Add(new SearchItemSub(GetPlateNumberString(searchResult), plateNumberID));
                iterator++;

            }
            if (SelectPlateCombo.Items.Count == 0)
            {
                ClearPlateResultFields();
                SelectPlateCombo.Items.Add(new SearchItemSub("No plate numbers found.", 0));
                ChangeTextBoxesEnabled(false);

                isSearchError = true;

            }
            else { ChangeTextBoxesEnabled(true);
                isSavingEnabled = true;
            }

            SelectPlateCombo.SelectedIndex = 0;

        }


        private void ClearSearchResultFields()
        {
            SearchByText.Text = "";

            SearchResultCombo.Items.Clear();
            SearchResultCombo.Text = "";
            SelectPlateCombo.Items.Clear();
            SelectPlateCombo.Text = "";

            SearchErrorLabel.Text = "";

            ClearPlateResultFields();

        }

        private void ClearPlateResultFields()
        {
            SelectPlateCombo.Items.Clear();
            plateNumberList.Clear();
            isSavingEnabled = false;
            TiffLengthText.Text = "";
            TiffWidthText.Text = "";
            this.selectedSearchResult = new PlateCreationSearchResults();

            CroppedWidthText.Text = "";
            CroppedLengthText.Text = "";
        }

        private void SearchResultCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = SearchResultCombo.SelectedIndex;
            ClearPlateResultFields();
            PlateCreationSearchResults selectedSearchResult = new PlateCreationSearchResults();
            searchDataTable = new DataTable();
            int? owItem = null;

            if (searchResultItems.Count != 0)
            {
                selectedSearchResult = searchResultItems[selectedIndex] as PlateCreationSearchResults;
            }
            if (selectedSearchResult.OWItem != null)
            {
                owItem = selectedSearchResult.OWItem.Value;
            }
            else
            {

                // Error

            }

            var querryAdaptor = RunSearchQuerry(querryStrings[1], owItem.ToString(), parameterChoice[1]);
            querryAdaptor.Fill(searchDataTable);

            ConvertPlateSearchToList(searchDataTable);

            // Close adapter once finished
            querryAdaptor.SelectCommand.Connection.Close();
            connString.Close();

            BindPlateSearchResults();

        }

        public void ConfigureConnectionString(SqlConnection connectionString)
        {

            connString = connectionString;

        }

        private bool CheckTiffLengthWidth()
        {
            if (String.IsNullOrWhiteSpace(TiffLengthText.Text) || String.IsNullOrWhiteSpace(TiffWidthText.Text)
                || TiffLengthText.Text == "." || TiffWidthText.Text == ".") { return false; }
            else
            {
                PlateTiffInfoFromTextBox.TiffLength = decimal.Parse(TiffLengthText.Text);
                PlateTiffInfoFromTextBox.TiffWidth = decimal.Parse(TiffWidthText.Text);

                return true; }
        }


        // Only call when you're ready for hard saves to server
        public void SaveTiffButton_Click(object sender, EventArgs e)
        {  //Use this to check your forms first and set up your properties for saving
         
            //SearchItemSub selectedPlateItem = SelectPlateCombo.SelectedItem as SearchItemSub;
            //selectedPlateNumber = selectedPlateItem.Id;



            if (CheckTiffLengthWidth() && CheckEdgeScrap())
            {
                if (CheckCroppedLengthWidth()) {

                    if (SaveTiffEntries(PlateTiffInfoFromTextBox, PlateTillTiffCombos))
                    {

                        SaveErrorLabel.Text = "Save Successful!";
                        isSaved = true;
                        hasBeenSaved = true;
                        RemoveTiffButton.Enabled = true;


                    }
                    else
                    {

                        SaveErrorLabel.Text = "Save Failed.";
                    }
                }

            }
            else {

                SaveErrorLabel.Text = "Check Width, Length, and Edge inputs";

            }


        }

        // Pull this when you want a temp save
        public bool IsSafeToRetrieveInfo()
        {

            bool isSafe = false;

            if (!isSavingEnabled)
            {
                return isSafe;
            }

            if (CheckTiffLengthWidth() && CheckEdgeScrap())
            {
                if (CheckCroppedLengthWidth())
                {
                    if (!IsCroppedAreaSmaller()) {

                        SaveErrorLabel.Text = "Cropped Area must be smaller than Tiff Area";
                    } else {

                        SaveErrorLabel.Text = "Save Successful!";
                        RemoveTiffButton.Enabled = true;
                        isSafe = true;
                    }
                }
                else
                {

                    SaveErrorLabel.Text = "Check Cropped Entries.";
                }

            }
            else
            {
                SaveErrorLabel.Text = "Check Width, Length, and Edge inputs";

            }


            return isSafe;
        }

        private bool CheckEdgeScrap()
        {

            if (String.IsNullOrWhiteSpace(EdgeScrapText.Text) || EdgeScrapText.Text == ".") { return false; }
            else
            {
                PlateTiffInfoFromTextBox.ExtendedCutLength = decimal.Parse(EdgeScrapText.Text);
                return true;
            }
        }


        private bool CheckCroppedLengthWidth() {


            if (CroppedLengthText.Text == "." || CroppedWidthText.Text == ".") {

                return false;
            }

            if ((String.IsNullOrWhiteSpace(CroppedLengthText.Text) && !String.IsNullOrWhiteSpace(CroppedWidthText.Text))
                || (String.IsNullOrWhiteSpace(CroppedWidthText.Text) && !String.IsNullOrWhiteSpace(CroppedLengthText.Text)
                ))
            { return false; }

            if (!String.IsNullOrWhiteSpace(CroppedLengthText.Text) && !String.IsNullOrWhiteSpace(CroppedWidthText.Text)) {

                PlateTiffInfoFromTextBox.CroppedLength = decimal.Parse(CroppedLengthText.Text);
                PlateTiffInfoFromTextBox.CroppedLength = decimal.Parse(CroppedLengthText.Text);
            }
  
            return true; 


        }

        private bool IsCroppedAreaSmaller() {

            bool isSmaller = false;

            if (TiffToolCalculatedFields.TotalCroppedArea < TiffToolCalculatedFields.TotalTiffArea)
                isSmaller = true;


            return isSmaller;

        }
        private bool SaveTiffEntries(PlateTiffInfo plateTillInfo, PlateTillTiffCombos plateTillTiffCombo)
        {
            if (plateDBSaveLogic.SaveTiff(PlateTiffInfoFromTextBox, plateTillTiffCombo))
            {
                PlateTillTiffCombos = plateDBSaveLogic.CurrentTillTiffCombo;
                PlateTiffInfoFromTextBox.PlateTiffInfoID = PlateTillTiffCombos.PlateTiffInfoID;


                return true;
            }
            else return false;
        }



        public bool CheckInfoForTable() {

            bool isGood = false;
            if (CheckTiffLengthWidth() && CheckEdgeScrap())
            {
                if (CheckCroppedLengthWidth())
                {
                    isGood = true;

                }

            }
            else
            {

                SaveErrorLabel.Text = "Check Width, Length, and Edge inputs";

            }
            return isGood;
        }

        private void RemoveTiffButton_Click(object sender, EventArgs e)
        {

            DeletePlateTiff();

        }

        public void DeletePlateTiff() {


            if (plateDBSaveLogic.DeletePlateTiff(PlateTiffInfoFromTextBox))
            {


                PlateTiffInfoFromTextBox = plateDBSaveLogic.CurrentPlateTiffInfo;
                IsRemoveSuccess = true;
                //Application.Exit();
                //Application.Restart();
                ClearSearchResultFields();
                SaveErrorLabel.Text = "Delete Success!";

            }
            else
            {
                IsRemoveSuccess = false;
                SaveErrorLabel.Text = "Delete Failed";

            }

        }





        private void SearchByText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchByButton.PerformClick();

            }
        }

        private void SelectPlateCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchItemSub plateNumber = (SearchItemSub)SelectPlateCombo.SelectedItem;

            PlateTiffInfoFromTextBox.ProductionPlateID = plateNumber.Id;


        }
        public event EventHandler EdgeLengthChanged;

        public PlateTillTiffCombos PlateTillTiffCombos { get; set; }

        private void SearchByText_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void TiffLengthText_Click(object sender, EventArgs e)
        {

        }

        private void EdgeScrapText_Validated(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == "")
            {
                ((TextBox)sender).Text = plateEdgeLengthDefault;
            }


        }



        private decimal CalculateArea(decimal? length, decimal? width) {

            decimal area = 0;

            if (!IsDecimalNullCheck(width) && !IsDecimalNullCheck(length))
            {

                if (width.Value >= 0 && length.Value >= 0)
                {
                    area = width.Value * length.Value;
                }

            }
            return area;

        }

        public decimal CalculateEdgeScrapArea(decimal? length, decimal? width, decimal? scrapEdge)
        {
            decimal scrapEdgeArea = 0;

            if (!IsDecimalNullCheck(length) && !IsDecimalNullCheck(width) && !IsDecimalNullCheck(scrapEdge))
            {
                if (!IsDecimalNullCheck(width) && !IsDecimalNullCheck(length))
                {
                    decimal scrapEdgeCorners = scrapEdge.Value * scrapEdge.Value * 4;
                    decimal scrapLength = scrapEdge.Value * length.Value * 2;
                    decimal scrapWidth = scrapEdge.Value * width.Value * 2;

                    scrapEdgeArea = scrapEdgeCorners + scrapLength + scrapWidth;
                }
            }

            return scrapEdgeArea;
        }

        public decimal CalculatePercentCoverage(decimal? totalArea, decimal? partialArea) {

            decimal percentCoverage = 0;
            if (IsDecimalNullCheck(totalArea) && IsDecimalNullCheck(partialArea))
            {
                percentCoverage = partialArea.Value / totalArea.Value;
                decimal.Round(percentCoverage, 2, MidpointRounding.AwayFromZero);
            }

            return percentCoverage;


        }

        private bool IsDecimalNullCheck(decimal? nullable) {

            bool isNull = false;

            if (nullable == null) {

                isNull = true;
            }

            return isNull;
        }

        private void CheckForDecimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textbox = (TextBox)sender;

            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.'))
            { e.Handled = true;

            }
            TextBox txtDecimal = sender as TextBox;
            if (e.KeyChar == '.' && txtDecimal.Text.Contains("."))
            {
                e.Handled = true;
            }

            if (e.Handled == false) {

                //string fullEntry = txtDecimal.Text + e.KeyChar;
                //if (!fullEntry.Equals("."))
            }

        }


        private void CheckForNumbersNoSpaces(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (!((char.IsDigit(e.KeyChar) && textBox.TextLength < 7) || e.KeyChar == (char)Keys.Back))

            { e.Handled = true; }


        }

        private void CalculateByTextBoxName(string textboxName, string textValue)
        {
            bool isCalcCroppedEdge = false;
            if (!textValue.Contains("\b"))
            {
                if (textboxName == "TiffLengthText")
                {

                    if (!string.IsNullOrWhiteSpace(textValue) && !string.IsNullOrWhiteSpace(TiffWidthText.Text) && textValue != ".")
                    {
                        PlateTiffInfoFromTextBox.TiffLength = decimal.Parse(textValue);
                        TiffToolCalculatedFields.TotalTiffArea = CalculateArea(decimal.Parse(textValue), decimal.Parse(TiffWidthText.Text));
                    }
                    else
                        TiffToolCalculatedFields.TotalTiffArea = 0;
                }


                if (textboxName == "TiffWidthText")
                {

                    if (!string.IsNullOrWhiteSpace(TiffLengthText.Text) && !string.IsNullOrWhiteSpace(textValue) && textValue != ".")
                    {
                        PlateTiffInfoFromTextBox.TiffWidth = decimal.Parse(textValue);
                        TiffToolCalculatedFields.TotalTiffArea = CalculateArea(decimal.Parse(TiffLengthText.Text), decimal.Parse(textValue));
                    }
                    else
                        TiffToolCalculatedFields.TotalTiffArea = 0;
                }


                if (textboxName == "CroppedLengthText")
                {

                    if (!string.IsNullOrWhiteSpace(textValue) && !string.IsNullOrWhiteSpace(CroppedWidthText.Text) && textValue != ".")
                    {
                        PlateTiffInfoFromTextBox.CroppedLength = decimal.Parse(textValue);
                        TiffToolCalculatedFields.TotalCroppedArea = CalculateArea(decimal.Parse(textValue), decimal.Parse(CroppedWidthText.Text));
                    }
                    else
                        TiffToolCalculatedFields.TotalCroppedArea = 0;
                }
                if (textboxName == "CroppedWidthText")
                {

                    if (!string.IsNullOrWhiteSpace(CroppedLengthText.Text) && !string.IsNullOrWhiteSpace(textValue) && textValue != ".")
                    {
                        PlateTiffInfoFromTextBox.CroppedLength = decimal.Parse(textValue);
                        TiffToolCalculatedFields.TotalCroppedArea = CalculateArea(decimal.Parse(CroppedLengthText.Text), decimal.Parse(textValue));

                    }
                    else
                        TiffToolCalculatedFields.TotalCroppedArea = 0;
                }

            }
            //        var test = this.TiffToolCalculatedFields;

            //if (textboxName != "EdgeScrapText")
            //{

            CalculateEdgeScrapFields(EdgeScrapText.Text);
            //}
        }

        private void CalculateEdgeScrapFields(string edgeScrapText) {

            if (!string.IsNullOrWhiteSpace(TiffLengthText.Text) && !string.IsNullOrWhiteSpace(TiffWidthText.Text) &&
                !string.IsNullOrWhiteSpace(edgeScrapText) && edgeScrapText != ".")
                {
                    PlateTiffInfoFromTextBox.ExtendedCutLength = decimal.Parse(edgeScrapText);
                    this.TiffToolCalculatedFields.TotalEdgeScrapArea = CalculateEdgeScrapArea(decimal.Parse(TiffLengthText.Text),
                    decimal.Parse(TiffWidthText.Text), decimal.Parse(edgeScrapText));
            }
            else
                TiffToolCalculatedFields.TotalEdgeScrapArea = 0;

        }

        public bool IsCroppedSmallerThanTiff() {

            bool isCroppedSmallerThanTiff = false;
            var tiffInfo = PlateTiffInfoFromTextBox;

            if (TiffToolCalculatedFields.TotalCroppedArea < TiffToolCalculatedFields.TotalTiffArea)
                isCroppedSmallerThanTiff = true;

            return isCroppedSmallerThanTiff;
        }

        public TiffToolCalculatedFields TiffToolCalculatedFields { get; set; } = new TiffToolCalculatedFields();
        public bool IsRemoveSuccess
        {
            get { return isRemoveSuccess; }

            private set { isRemoveSuccess = value; }
        }

        public bool IsSaved
        {

            get { return isSaved; }

            private set { isSaved = value; }

        }


        public PlateTiffInfo PlateTiffInfoFromTextBox
        {
            get
            { return this.plateTiffInfoFromTextBox; }

            set
            {

                if (plateTiffInfoFromTextBox == value) return;
                plateTiffInfoFromTextBox = value;

                EdgeLengthChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void TextBoxChanged(object sender, EventArgs e)
        {

            TextBox textBox = sender as TextBox;
            if (!textBox.Text.Equals("."))
                CalculateByTextBoxName(textBox.Name, textBox.Text);

        }

        public bool HasBeenSaved { get {return hasBeenSaved;} }
    } 
    
}
