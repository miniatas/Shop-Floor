using ShopFloor.PlateForms.PlateCreationBL;
using ShopFloor.PlateForms.PlateProperties;
using ShopFloor;
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
using ShopFloor.PlateForms.PlateProperties;
using Shop_Floor.PlateForms.PlateProperties;
using System.Reflection;
using Shop_Floor.GeneralTools;
using Shop_Floor.PlateForms.PlateCreationBL;
using UserControlsForShopFloor.PropertyClasses;
using UserControlsForShopFloor.Classes;

namespace ShopFloor.PlateForms
{
    public partial class PlateCreationMain : Form
    {

        private BindingSource tabBinding = new BindingSource();
        private PlateCreationTabLogic plateCreationTabLogic = new PlateCreationTabLogic();
        private List<PlateTillDimensions> plateTillDimensionsList = new List<PlateTillDimensions>();
        private List<int> plateTillMasterItemNumbers = new List<int>();
        private SqlConnection connection = new SqlConnection("Data Source=" + StartupForm.ServerName + ";User ID=" + StartupForm.UserName + "Password=" + StartupForm.Password + ";database=Manufacturing" + ";Connection Timeout=60;Persist Security Info=False");
        private SqlConnection testConnection = new SqlConnection("Data Source=ovesql02;User ID=testUser;Password=21TriedToDrink;database=Manufacturing;Connection Timeout=60;Persist Security Info=False");
        private List<PlateTiffRowItems> calculatedGridView = new List<PlateTiffRowItems>();
        private BindingList<PlateTiffRowItems> calculateRowSource;
        private BindingSource tiffDisplaySource;
        private PlateTillDimensions selectedPlateTillType;
        private List<string> uomList = new List<string> { "Inch" };
        private bool isReadyForFullSave = false;
        private decimal tillTotalArea;
        private decimal tillImageableArea;

        private bool canAddNewTab;
        private bool canFinalize;
        private List<bool> canFinalizeList = new List<bool>();

        public PlateCreationMain()
        {
            InitializeComponent();
            InitMethods();

        }

        private void InitMethods() {

            calculateRowSource = new BindingList<PlateTiffRowItems>(calculatedGridView);
            tiffDisplaySource = new BindingSource(calculateRowSource, null);
            TiffCalculatedGridView.DataSource = tiffDisplaySource;

            Connection = testConnection;
            AddNewTiffTab();
            //AddPlateTableRange();
            PopulateUOM();
            GetPlateTillDimensionOptions();
            RemoveSelectedTiffButton.Enabled = false;
            
        }


        // Populates for the Unit of Measure dropdown selection.  Currently its only inches
        private void PopulateUOM()
        {
            foreach (string uom in uomList)
                UomCombo.Items.Add(uom);

            UomCombo.SelectedIndex = 0;
        }

        private void TillTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = TillTypeCombo.SelectedIndex;

            selectedPlateTillType = plateTillDimensionsList[selectedIndex];

            tillTotalArea = CalculateArea(selectedPlateTillType.Length, selectedPlateTillType.Width);
            tillImageableArea = CalculateArea(selectedPlateTillType.ImageableLength, selectedPlateTillType.ImageableWidth);

            TillAreaLabel.Text = tillTotalArea.ToString();
            ImageableAreaLabel.Text = tillImageableArea.ToString();

        }

        private void AddTiffButton_Click(object sender, EventArgs e)
        {
            if (canAddNewTab)
            {

                AddNewTiffTab();

                int indexToSelect = TiffEntryTabControl.TabCount - 1;
                TiffEntryTabControl.SelectedIndex = indexToSelect;
                AddTiffErrorLabel.Text = "";
            }
            else
            {

                AddTiffErrorLabel.Text = "You must save the previous entries before adding a new tiff";
            }
            EnableCanFinalize();
        }

        private bool CheckIfTabCanBeAdded() {

            bool canBeAdded = true;

            List<PlateCreationEntry> plateEntry = TiffEntryTabControl.TabPages.Cast<Control>()
          .SelectMany(page => page.Controls.OfType<PlateCreationEntry>())
          .ToList();

            plateEntry.ForEach(
                plateSection =>
                {
                    if (canBeAdded)
                    {
                        if (!plateSection.IsSafeToRetrieveInfo())
                            canBeAdded = false;

                    }
                });
                
            return canBeAdded;
        }
        private void AddTiffRow(PlateTiffRowItems plateTiffRowItems) {

            calculateRowSource.Add(plateTiffRowItems);

        }


        private void AddPlateTableRange() {


            TiffCalculatedGridView.Columns.AddRange(CreateNewRow());

        }

        private DataGridViewColumn[] CreateNewRow() {

            PlateTiffRowDisplay plateTiffColumns = new PlateTiffRowDisplay();


            plateTiffColumns.PlateNumber.DataPropertyName = "ProductionPlateID";

            plateTiffColumns.TiffArea.DataPropertyName = "TotalTiffArea";
            plateTiffColumns.CroppedArea.DataPropertyName = "TotalCroppedArea";
            plateTiffColumns.ExtendedArea.DataPropertyName = "TotalEdgeScrapArea";
            plateTiffColumns.TiffImageableArea.DataPropertyName = "TotalTillArea";



            DataGridViewColumn[] dgc = new DataGridViewColumn[] {
            plateTiffColumns.PlateNumber,
            plateTiffColumns.MakeNumber,
            plateTiffColumns.SurfaceOrReverse,
            plateTiffColumns.IsCommonVersion,
            plateTiffColumns.IsCommonPow,
            plateTiffColumns.TiffArea,
            plateTiffColumns.CroppedArea,
            plateTiffColumns.ExtendedArea,
            plateTiffColumns.TiffImageableArea,
            plateTiffColumns.ImageableAreaPercent};

            return dgc;

        }

        private void AddNewTiffTab() {

            int currentTabCount = TiffEntryTabControl.TabPages.Count + 1;
            Control newTabControl = plateCreationTabLogic.CreateNewTiffEntryTab(currentTabCount);
            TiffEntryTabControl.Controls.Add(newTabControl);

            RemoveSelectedTiffButton.Enabled = true;

        }

        private void RemoveSelectedTiffButton_Click(object sender, EventArgs e)
        {

            int tabCount = TiffEntryTabControl.TabPages.Count;

            int index = TiffEntryTabControl.SelectedIndex;
            TabPage tabPage = TiffEntryTabControl.TabPages[index];
            PlateCreationEntry selectedEntryPage = PlateCreationEntryInTab(tabPage);
            var bleh = selectedEntryPage.PlateTiffInfoFromTextBox;

            //if (selectedEntryPage.HasBeenSaved)
            //{
            //    selectedEntryPage.DeletePlateTiff();

            //}
            //if (selectedEntryPage.IsRemoveSuccess || selectedEntryPage.HasBeenSaved == false)
            //{

        
            int gridRowCount = calculatedGridView.Count;

               
                
                    TiffEntryTabControl.TabPages.Remove(TiffEntryTabControl.SelectedTab);
            int newTabCount = TiffEntryTabControl.TabPages.Count;

            if (newTabCount < 2) {

                RemoveSelectedTiffButton.Enabled = false;

            }
                    FixTabDisplayCount(index);

                if (tabCount == gridRowCount)
                    RemoveDisplayGrid(index);

            EnableCanFinalize();
        }

        private void RemoveDisplayGrid(int indexToRemove) {

            if (indexToRemove == 0)
                calculatedGridView.Clear();
            else
          //  calculatedGridView.RemoveAt(indexToRemove);
            TiffCalculatedGridView.Rows.RemoveAt(indexToRemove);
            TiffCalculatedGridView.Refresh();
            FillCalculatedGrid();


        }
        private void FixTabDisplayCount(int indexRemoved) {

            int indexToChange = indexRemoved;
            int tabCount = TiffEntryTabControl.TabCount;

            
            while (indexToChange != tabCount) {
                indexToChange++;
                TiffEntryTabControl.TabPages[indexToChange-1].Text = "Tiff " + indexToChange.ToString();
            }



        }
        // Used to convert a tabPages control into the .dll User Control 
        private PlateCreationEntry PlateCreationEntryInTab(TabPage selectedTabPage) {

            PlateCreationEntry plateCreationEntry = new PlateCreationEntry();

            foreach (Control Ctl in selectedTabPage.Controls)
            {
              
                if (Ctl is PlateCreationEntry)
                {
                    plateCreationEntry = Ctl as PlateCreationEntry;
                }

            }

            return plateCreationEntry;
            
        }

        private void NewTillButton_Click(object sender, EventArgs e)
        {

        }

        private void GetPlateTillDimensionOptions() {

            plateTillDimensionsList.Clear();

            PlateDimensionQuerries plateDimensionQuerry = new PlateDimensionQuerries();

            plateTillDimensionsList = plateDimensionQuerry.SelectAllPlateDimensions(Connection);

            if (plateTillDimensionsList.Count > 0)
            GetPlateTillDescriptions(plateTillDimensionsList);

        }

        private void GetPlateTillDescriptions(List<PlateTillDimensions> dimensionList) {

            List<string> masterItemNumbers = new List<string>();

            PlateDimensionQuerries plateDescriptionQuerry = new PlateDimensionQuerries();

            foreach (PlateTillDimensions plateTillDimension in dimensionList)
            {

                int temp;

                temp = plateTillDimension.MasterItemNumber;
                masterItemNumbers.Add(temp.ToString());

            }

            List<string> plateDimensionDescriptions = plateDescriptionQuerry.GetPlateDimensionDescriptions(Connection, masterItemNumbers);

            BindPlateTillDimensionsToCombo(plateDimensionDescriptions);


        }

        private void BindPlateTillDimensionsToCombo(List<string> plateTillDimensions) {

            int index = 0;

            if (plateTillDimensions.Count > 0) {
                foreach (string plateTillDimension in plateTillDimensions) {

                    TillTypeCombo.Items.Add(new SearchItem(plateTillDimension, index));

                }
            } else {

                TillTypeCombo.Items.Add(new SearchItem("Database Error", 0));

            }

            TillTypeCombo.SelectedIndex = 0;                          

        }

        private decimal CalculateArea(decimal length, decimal width) {

            return length * width;

        }

        SqlConnection Connection { get; set; }


        private void TiffEntryTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    

        private void SaveTiffTab_Click(object sender, EventArgs e)
        {
            Control selectedTab = TiffEntryTabControl.SelectedTab;
            int finalizeCount = canFinalizeList.Count;
            int currentTabIndex = TiffEntryTabControl.SelectedIndex;
            int tabCount = TiffEntryTabControl.TabCount;

            List<PlateCreationEntry> plateEntry = TiffEntryTabControl.TabPages.Cast<Control>()
            .SelectMany(page => page.Controls.OfType<PlateCreationEntry>())
            .ToList();


            if (plateEntry[currentTabIndex].IsSafeToRetrieveInfo())
            {
                ConversionTools converstionTools = new ConversionTools();
                PlateTiffRowItems plateTiffRowItems = new PlateTiffRowItems();
                PlateCreationTotals calculatedFields = new PlateCreationTotals();
            

                var convertFrom = plateEntry[currentTabIndex].TiffToolCalculatedFields;

                plateTiffRowItems.PlateNumber = plateEntry[currentTabIndex].PlateTiffInfoFromTextBox.ProductionPlateID.Value;
             
                plateTiffRowItems.IsCommonVersion = true;
                plateTiffRowItems.ImageableAreaPercent = convertFrom.FinalTiffImageableArea;
                plateTiffRowItems.SurfaceOrReverse = "Surface";

                plateTiffRowItems.TiffArea = convertFrom.TotalTiffArea;
                plateTiffRowItems.ImageableAreaPercent = convertFrom.PercentCoverageTiff;
                plateTiffRowItems.ExtendedArea = convertFrom.TotalEdgeScrapArea;
                plateTiffRowItems.CroppedArea = convertFrom.TotalCroppedArea;


                if (plateTiffRowItems.CroppedArea > 0)
                    plateTiffRowItems.TiffImageableArea = plateTiffRowItems.CroppedArea;
                else
                    plateTiffRowItems.TiffImageableArea = plateTiffRowItems.TiffArea;


                plateTiffRowItems.ImageableAreaPercent = CalculateTiffAreaPercent(plateTiffRowItems.TiffImageableArea, tillImageableArea);

                plateTiffRowItems.MakeNumber = GetMakeNumber(plateTiffRowItems.PlateNumber);
                //PlateIDandMake.ProductionPlateID = plateEntry.PlateTiffInfoFromTextBox.ProductionPlateID.Value;

                int rowSourceCount = calculateRowSource.Count;

                if (tabCount == rowSourceCount)
                {
                    UpdateTiffRow(currentTabIndex, plateTiffRowItems);
                }

                if (tabCount - 1 == rowSourceCount)
                {

                    AddTiffRow(plateTiffRowItems);

                }

                FillCalculatedGrid();
                AddTiffErrorLabel.Text = "";
                canAddNewTab = true;
                canFinalize = true;
           
               
          
            }
            else {

               
                canAddNewTab = false;
                canFinalize = false;
            }
            if (finalizeCount < tabCount)
                canFinalizeList.Add(canFinalize);
            else
                canFinalizeList[currentTabIndex] = canFinalize;

            EnableCanFinalize();
        }

        private void EnableCanFinalize() {

            bool isReady = false;

            int isFinalizeCount = canFinalizeList.Count;
            int tabCount = TiffEntryTabControl.TabCount;

            if (isFinalizeCount == tabCount)
            {
                int count = 0;
                canFinalizeList.ForEach(canFinalize =>
                {

                    if (canFinalize == true)
                    {

                        count++;
                        
                    }


                });

                if (count == isFinalizeCount)
                    isReady = true;


             

            }

            FinalizeButton.Enabled = isReady;

        }

        private int GetMakeNumber(int plateNumber) {

            int makeNumber = CheckAndReturnMakeIfPlateUsedAgain(plateNumber);

            if (makeNumber == 0)
            {
                MakeQuerries makeQuerries = new MakeQuerries();
                PlateMakeDetails plateMakeDetails = new PlateMakeDetails();
                plateMakeDetails.ProductionPlateID = plateNumber;
                plateMakeDetails = makeQuerries.GetMakeNumber(plateMakeDetails, Connection);

                makeNumber = plateMakeDetails.PlateMakeID;                

            }

            makeNumber++;

            return makeNumber;
        }


        private int CheckAndReturnMakeIfPlateUsedAgain(int plateNumber) {

            int maxMake = 0;

            calculatedGridView.ForEach(row =>
            {
                if (row.PlateNumber == plateNumber) {

                    int nextMake = row.MakeNumber;

                    if (nextMake > maxMake)
                        maxMake = nextMake;

                }
            });
            
            return maxMake;
        }
        private void UpdateTiffRow(int index, PlateTiffRowItems plateTiffRowItems) {


            calculatedGridView.RemoveAt(index);
            calculatedGridView.Insert(index, plateTiffRowItems);
            TiffCalculatedGridView.AutoResizeColumns();
            TiffCalculatedGridView.Refresh();

        }

        private decimal CalculateTiffAreaPercent(decimal tiffArea, decimal tillArea) {


            decimal percentCoverage = 0;
            if (tiffArea >= 0 && tillArea >= 0)
            {
                percentCoverage = (tiffArea / tillArea);
                percentCoverage = Decimal.Round(percentCoverage, 2);

            }
            else {

                throw new Exception("One area is 0 or null");
            }

            return percentCoverage;

        }
       
        public List<PlateMakeDetails> PlateIDandMakeList { get; set; } = new List<PlateMakeDetails>();

        private void FillCalculatedGrid()
        {
            isReadyForFullSave = false;

            PlateCreationTotal = new PlateCreationTotals();

            calculatedGridView.ForEach(rows =>

            
                PlateCreationTotal.TotalTiffImageableArea += rows.TiffImageableArea);

            PlateCreationTotal.TotalImageableScrap = tillImageableArea - PlateCreationTotal.TotalTiffImageableArea;

            if (PlateCreationTotal.TotalImageableScrap > 0 && PlateCreationTotal.TotalTiffImageableArea > 0) {

                decimal temp = PlateCreationTotal.TotalImageableScrap / tillImageableArea;

                PlateCreationTotal.ImageableScrapPercentage = decimal.Round(temp*100, 2).ToString() + "%" ;

            }

            TotalsGridView.Rows.Clear();

            DataGridViewRow row = (DataGridViewRow)TotalsGridView.Rows[0].Clone();

            row.Cells[0].Value = PlateCreationTotal.TotalTiffImageableArea;
            row.Cells[1].Value = PlateCreationTotal.TotalImageableScrap;
            row.Cells[2].Value = PlateCreationTotal.ImageableScrapPercentage;

            TotalsGridView.Rows.Clear();
            TotalsGridView.Rows.Add(row);

            TotalsGridView.Refresh();

            if (PlateCreationTotal.TotalImageableScrap < 0) {

                isReadyForFullSave = false;
                FinalizeErrorLabel.Text = "Cannot Save, scrap is greater than imageable area!";
            }

        }

        
        private PlateCreationTotals CalculateTotals(PlateCreationTotals plateCreationTotal) {

            


            return plateCreationTotal;
        }

        PlateCreationTotals PlateCreationTotal { get; set; } = new PlateCreationTotals();

        private List<PlateTiffInfo> ConvertTabsToPlateTiffInfoList() {

            List<PlateTiffInfo> plateTiffInfoList = new List<PlateTiffInfo>();

            List<PlateCreationEntry> plateEntry = TiffEntryTabControl.TabPages.Cast<Control>()
                .SelectMany(page => page.Controls.OfType<PlateCreationEntry>())
                .ToList();

            plateEntry.ForEach(plateTiff =>
            {
                plateTiffInfoList.Add(plateTiff.PlateTiffInfoFromTextBox);

            });

            return plateTiffInfoList;
        }


        private void TheFinalQuery(object sender, EventArgs e)
        {
            FinalizePlateProduction finalize = new FinalizePlateProduction();

            bool isSuccess;

            List<PlateTiffRowItems> plateTiffRowItemsList = calculateRowSource.ToList<PlateTiffRowItems>();
            PlateTillTiffCombos plateTillTiffCombo = new PlateTillTiffCombos();

            plateTillTiffCombo.PlateTillDimensionsID = selectedPlateTillType.PlateTillDimensionID;

            List<PlateTiffInfo> plateTiffInfoList = ConvertTabsToPlateTiffInfoList();

            if (finalize.TheFinalQuery(plateTiffInfoList, plateTillTiffCombo, plateTiffRowItemsList, Connection))
            {
                FinalizeMessageLabel.Text = "Save Successful!";

            }
            else
            {
                FinalizeMessageLabel.Text = "Save Failed.  Try again or alert IT";
            }
        }
    }
}
