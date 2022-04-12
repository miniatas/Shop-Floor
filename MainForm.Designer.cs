/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 11/30/2010
 * Time: 1:53 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ShopFloor
{
	public partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ToolStripMenuItem rollLabelToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem palletLabelToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem reprintToolStripMenuItem;
		private System.Windows.Forms.Label lblMachineOrPrinterInformation;
		private System.Windows.Forms.ToolStripMenuItem machineOrOperationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem scanToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem printerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem finishingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem productionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem returnToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem issueToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem issuesReturnsToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStripMainMenu;
		private System.Windows.Forms.Label lblUserInformation;
		private System.Windows.Forms.Panel pnlBottom;
		private System.Windows.Forms.ToolStripMenuItem adhesiveReceiveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem adhesiveUseToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem adhesivesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem adhesiveToteLabelToolStripMenuItem;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">True if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) 
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lblMachineOrPrinterInformation = new System.Windows.Forms.Label();
            this.lblUserInformation = new System.Windows.Forms.Label();
            this.menuStripMainMenu = new System.Windows.Forms.MenuStrip();
            this.scanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.issuesReturnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pickRollsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.issueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.returnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.voidIssueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoReplaceFilmPickListRollToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualReplacePickListRollToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printPickListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.issuePickListForStarpakToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plateInventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PlateCreation = new System.Windows.Forms.ToolStripMenuItem();
            this.PlateInventory = new System.Windows.Forms.ToolStripMenuItem();
            this.productionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reworkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.finishingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.finishWIPPalletToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewFGPalletToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.revertFGPalletToWIPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewNonProductionFGPalletToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.raToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.machineOrOperationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adhesivesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adhesiveReceiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adhesiveUseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productionEditStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reprintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.palletLabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rollLabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adhesiveToteLabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filmPickListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userAdminToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.matchFingerprintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlBottom.SuspendLayout();
            this.menuStripMainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.lblMachineOrPrinterInformation);
            this.pnlBottom.Controls.Add(this.lblUserInformation);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 567);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1208, 35);
            this.pnlBottom.TabIndex = 0;
            // 
            // lblMachineOrPrinterInformation
            // 
            this.lblMachineOrPrinterInformation.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblMachineOrPrinterInformation.Location = new System.Drawing.Point(662, 0);
            this.lblMachineOrPrinterInformation.Name = "lblMachineOrPrinterInformation";
            this.lblMachineOrPrinterInformation.Size = new System.Drawing.Size(546, 35);
            this.lblMachineOrPrinterInformation.TabIndex = 1;
            this.lblMachineOrPrinterInformation.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lblUserInformation
            // 
            this.lblUserInformation.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblUserInformation.Location = new System.Drawing.Point(0, 0);
            this.lblUserInformation.Name = "lblUserInformation";
            this.lblUserInformation.Size = new System.Drawing.Size(258, 35);
            this.lblUserInformation.TabIndex = 0;
            this.lblUserInformation.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // menuStripMainMenu
            // 
            this.menuStripMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scanToolStripMenuItem,
            this.issuesReturnsToolStripMenuItem,
            this.printerToolStripMenuItem,
            this.plateInventoryToolStripMenuItem,
            this.productionToolStripMenuItem,
            this.reworkToolStripMenuItem,
            this.finishingToolStripMenuItem,
            this.raToolStripMenuItem,
            this.createtoolStripMenuItem,
            this.machineOrOperationToolStripMenuItem,
            this.adhesivesToolStripMenuItem,
            this.productionEditStripMenuItem,
            this.reprintToolStripMenuItem,
            this.userAdminToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuStripMainMenu.Location = new System.Drawing.Point(0, 0);
            this.menuStripMainMenu.Name = "menuStripMainMenu";
            this.menuStripMainMenu.Size = new System.Drawing.Size(1208, 24);
            this.menuStripMainMenu.TabIndex = 1;
            this.menuStripMainMenu.Text = "menuStrip1";
            // 
            // scanToolStripMenuItem
            // 
            this.scanToolStripMenuItem.Name = "scanToolStripMenuItem";
            this.scanToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.scanToolStripMenuItem.Text = "Scan";
            this.scanToolStripMenuItem.Click += new System.EventHandler(this.ScanToolStripMenuItemClick);
            // 
            // issuesReturnsToolStripMenuItem
            // 
            this.issuesReturnsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pickRollsToolStripMenuItem,
            this.issueToolStripMenuItem,
            this.returnToolStripMenuItem,
            this.voidIssueToolStripMenuItem,
            this.autoReplaceFilmPickListRollToolStripMenuItem,
            this.manualReplacePickListRollToolStripMenuItem,
            this.printPickListToolStripMenuItem,
            this.issuePickListForStarpakToolStripMenuItem});
            this.issuesReturnsToolStripMenuItem.Name = "issuesReturnsToolStripMenuItem";
            this.issuesReturnsToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.issuesReturnsToolStripMenuItem.Text = "Issues/Returns";
            this.issuesReturnsToolStripMenuItem.Visible = false;
            // 
            // pickRollsToolStripMenuItem
            // 
            this.pickRollsToolStripMenuItem.Name = "pickRollsToolStripMenuItem";
            this.pickRollsToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.pickRollsToolStripMenuItem.Text = "Issue Film Pick List";
            this.pickRollsToolStripMenuItem.Click += new System.EventHandler(this.PickRollsToolStripMenuItemClick);
            // 
            // issueToolStripMenuItem
            // 
            this.issueToolStripMenuItem.Name = "issueToolStripMenuItem";
            this.issueToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.issueToolStripMenuItem.Text = "Issue";
            this.issueToolStripMenuItem.Click += new System.EventHandler(this.IssueToolStripMenuItemClick);
            // 
            // returnToolStripMenuItem
            // 
            this.returnToolStripMenuItem.Name = "returnToolStripMenuItem";
            this.returnToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.returnToolStripMenuItem.Text = "Return";
            this.returnToolStripMenuItem.Click += new System.EventHandler(this.ReturnToolStripMenuItemClick);
            // 
            // voidIssueToolStripMenuItem
            // 
            this.voidIssueToolStripMenuItem.Name = "voidIssueToolStripMenuItem";
            this.voidIssueToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.voidIssueToolStripMenuItem.Text = "Void Issue";
            this.voidIssueToolStripMenuItem.Click += new System.EventHandler(this.VoidIssueToolStripMenuItemClick);
            // 
            // autoReplaceFilmPickListRollToolStripMenuItem
            // 
            this.autoReplaceFilmPickListRollToolStripMenuItem.Name = "autoReplaceFilmPickListRollToolStripMenuItem";
            this.autoReplaceFilmPickListRollToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.autoReplaceFilmPickListRollToolStripMenuItem.Text = "Auto-Replace Pick List Roll";
            this.autoReplaceFilmPickListRollToolStripMenuItem.Click += new System.EventHandler(this.AutoReplaceFilmPickListRollToolStripMenuItemClick);
            // 
            // manualReplacePickListRollToolStripMenuItem
            // 
            this.manualReplacePickListRollToolStripMenuItem.Name = "manualReplacePickListRollToolStripMenuItem";
            this.manualReplacePickListRollToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.manualReplacePickListRollToolStripMenuItem.Text = "Manual Replace Pick List Roll";
            this.manualReplacePickListRollToolStripMenuItem.Click += new System.EventHandler(this.ManualReplacePickListRollToolStripMenuItemClick);
            // 
            // printPickListToolStripMenuItem
            // 
            this.printPickListToolStripMenuItem.Name = "printPickListToolStripMenuItem";
            this.printPickListToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.printPickListToolStripMenuItem.Text = "Print Pick List";
            this.printPickListToolStripMenuItem.Click += new System.EventHandler(this.PrintPickListToolStripMenuItemClick);
            // 
            // issuePickListForStarpakToolStripMenuItem
            // 
            this.issuePickListForStarpakToolStripMenuItem.Name = "issuePickListForStarpakToolStripMenuItem";
            this.issuePickListForStarpakToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.issuePickListForStarpakToolStripMenuItem.Text = "Issue Pick List for Starpak";
            this.issuePickListForStarpakToolStripMenuItem.Click += new System.EventHandler(this.IssuePickListForStarpakToolStripMenuItemClick);
            // 
            // printerToolStripMenuItem
            // 
            this.printerToolStripMenuItem.Name = "printerToolStripMenuItem";
            this.printerToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.printerToolStripMenuItem.Text = "Printers Setup";
            this.printerToolStripMenuItem.Click += new System.EventHandler(this.PrinterToolStripMenuItemClick);
            // 
            // plateInventoryToolStripMenuItem
            // 
            this.plateInventoryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PlateCreation,
            this.PlateInventory});
            this.plateInventoryToolStripMenuItem.Name = "plateInventoryToolStripMenuItem";
            this.plateInventoryToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.plateInventoryToolStripMenuItem.Text = "Plates";
            this.plateInventoryToolStripMenuItem.Visible = false;
            // 
            // PlateCreation
            // 
            this.PlateCreation.Name = "PlateCreation";
            this.PlateCreation.Size = new System.Drawing.Size(153, 22);
            this.PlateCreation.Text = "Plate Creation";
            this.PlateCreation.Click += new System.EventHandler(this.PlateCreation_Click);
            // 
            // PlateInventory
            // 
            this.PlateInventory.Name = "PlateInventory";
            this.PlateInventory.Size = new System.Drawing.Size(153, 22);
            this.PlateInventory.Text = "Plate Inventory";
            this.PlateInventory.Click += new System.EventHandler(this.plateInventoryToolStripMenuItem_Click);
            // 
            // productionToolStripMenuItem
            // 
            this.productionToolStripMenuItem.Name = "productionToolStripMenuItem";
            this.productionToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.productionToolStripMenuItem.Text = "Production";
            this.productionToolStripMenuItem.Visible = false;
            this.productionToolStripMenuItem.Click += new System.EventHandler(this.ProductionToolStripMenuItemClick);
            // 
            // reworkToolStripMenuItem
            // 
            this.reworkToolStripMenuItem.Name = "reworkToolStripMenuItem";
            this.reworkToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.reworkToolStripMenuItem.Text = "Rework";
            this.reworkToolStripMenuItem.Click += new System.EventHandler(this.ReworkToolStripMenuItemClick);
            // 
            // finishingToolStripMenuItem
            // 
            this.finishingToolStripMenuItem.DoubleClickEnabled = true;
            this.finishingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.finishWIPPalletToolStripMenuItem,
            this.createNewFGPalletToolStripMenuItem,
            this.revertFGPalletToWIPToolStripMenuItem,
            this.createNewNonProductionFGPalletToolStripMenuItem});
            this.finishingToolStripMenuItem.Name = "finishingToolStripMenuItem";
            this.finishingToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.finishingToolStripMenuItem.Text = "Finishing";
            this.finishingToolStripMenuItem.Visible = false;
            // 
            // finishWIPPalletToolStripMenuItem
            // 
            this.finishWIPPalletToolStripMenuItem.Name = "finishWIPPalletToolStripMenuItem";
            this.finishWIPPalletToolStripMenuItem.Size = new System.Drawing.Size(274, 22);
            this.finishWIPPalletToolStripMenuItem.Text = "Finish WIP Pallet";
            this.finishWIPPalletToolStripMenuItem.Click += new System.EventHandler(this.FinishWIPPalletToolStripMenuItemClick);
            // 
            // createNewFGPalletToolStripMenuItem
            // 
            this.createNewFGPalletToolStripMenuItem.Name = "createNewFGPalletToolStripMenuItem";
            this.createNewFGPalletToolStripMenuItem.Size = new System.Drawing.Size(274, 22);
            this.createNewFGPalletToolStripMenuItem.Text = "Create New FG Pallet";
            this.createNewFGPalletToolStripMenuItem.Click += new System.EventHandler(this.CreateNewFGPalletToolStripMenuItemClick);
            // 
            // revertFGPalletToWIPToolStripMenuItem
            // 
            this.revertFGPalletToWIPToolStripMenuItem.Name = "revertFGPalletToWIPToolStripMenuItem";
            this.revertFGPalletToWIPToolStripMenuItem.Size = new System.Drawing.Size(274, 22);
            this.revertFGPalletToWIPToolStripMenuItem.Text = "Revert FG Pallet to WIP";
            this.revertFGPalletToWIPToolStripMenuItem.Click += new System.EventHandler(this.RevertFGPalletToWIPToolStripMenuItemClick);
            // 
            // createNewNonProductionFGPalletToolStripMenuItem
            // 
            this.createNewNonProductionFGPalletToolStripMenuItem.Name = "createNewNonProductionFGPalletToolStripMenuItem";
            this.createNewNonProductionFGPalletToolStripMenuItem.Size = new System.Drawing.Size(274, 22);
            this.createNewNonProductionFGPalletToolStripMenuItem.Text = "Create New Non-Production FG Pallet";
            this.createNewNonProductionFGPalletToolStripMenuItem.Click += new System.EventHandler(this.createNewNonProductionFGPalletToolStripMenuItem_Click);
            // 
            // raToolStripMenuItem
            // 
            this.raToolStripMenuItem.Name = "raToolStripMenuItem";
            this.raToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.raToolStripMenuItem.Text = "RAs";
            this.raToolStripMenuItem.Visible = false;
            this.raToolStripMenuItem.Click += new System.EventHandler(this.raToolStripMenuItem_Click);
            // 
            // createtoolStripMenuItem
            // 
            this.createtoolStripMenuItem.Name = "createtoolStripMenuItem";
            this.createtoolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.createtoolStripMenuItem.Text = "Create";
            this.createtoolStripMenuItem.Visible = false;
            this.createtoolStripMenuItem.Click += new System.EventHandler(this.CreatetoolStripMenuItemClick);
            // 
            // machineOrOperationToolStripMenuItem
            // 
            this.machineOrOperationToolStripMenuItem.Name = "machineOrOperationToolStripMenuItem";
            this.machineOrOperationToolStripMenuItem.Size = new System.Drawing.Size(135, 20);
            this.machineOrOperationToolStripMenuItem.Text = "Machine or Operation";
            this.machineOrOperationToolStripMenuItem.Visible = false;
            this.machineOrOperationToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.MachineOrOperationToolStripMenuItemDropDownItemClicked);
            // 
            // adhesivesToolStripMenuItem
            // 
            this.adhesivesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adhesiveReceiveToolStripMenuItem,
            this.adhesiveUseToolStripMenuItem});
            this.adhesivesToolStripMenuItem.Name = "adhesivesToolStripMenuItem";
            this.adhesivesToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.adhesivesToolStripMenuItem.Text = "Adhesives";
            this.adhesivesToolStripMenuItem.Visible = false;
            this.adhesivesToolStripMenuItem.Click += new System.EventHandler(this.AdhesivesToolStripMenuItemClick);
            // 
            // adhesiveReceiveToolStripMenuItem
            // 
            this.adhesiveReceiveToolStripMenuItem.Name = "adhesiveReceiveToolStripMenuItem";
            this.adhesiveReceiveToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.adhesiveReceiveToolStripMenuItem.Text = "Receive";
            this.adhesiveReceiveToolStripMenuItem.Visible = false;
            this.adhesiveReceiveToolStripMenuItem.Click += new System.EventHandler(this.AdhesiveReceiveToolStripMenuItemClick);
            // 
            // adhesiveUseToolStripMenuItem
            // 
            this.adhesiveUseToolStripMenuItem.Name = "adhesiveUseToolStripMenuItem";
            this.adhesiveUseToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.adhesiveUseToolStripMenuItem.Text = "Use";
            this.adhesiveUseToolStripMenuItem.Visible = false;
            this.adhesiveUseToolStripMenuItem.Click += new System.EventHandler(this.AdhesiveUseToolStripMenuItemClick);
            // 
            // productionEditStripMenuItem
            // 
            this.productionEditStripMenuItem.Name = "productionEditStripMenuItem";
            this.productionEditStripMenuItem.Size = new System.Drawing.Size(106, 20);
            this.productionEditStripMenuItem.Text = "Production Edits";
            this.productionEditStripMenuItem.Visible = false;
            this.productionEditStripMenuItem.Click += new System.EventHandler(this.ProductionEditStripMenuItemClick);
            // 
            // reprintToolStripMenuItem
            // 
            this.reprintToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.palletLabelToolStripMenuItem,
            this.rollLabelToolStripMenuItem,
            this.adhesiveToteLabelToolStripMenuItem,
            this.filmPickListToolStripMenuItem});
            this.reprintToolStripMenuItem.Name = "reprintToolStripMenuItem";
            this.reprintToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.reprintToolStripMenuItem.Text = "Reprint";
            // 
            // palletLabelToolStripMenuItem
            // 
            this.palletLabelToolStripMenuItem.Name = "palletLabelToolStripMenuItem";
            this.palletLabelToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.palletLabelToolStripMenuItem.Text = "Pallet Label";
            this.palletLabelToolStripMenuItem.Click += new System.EventHandler(this.PalletLabelToolStripMenuItemClick);
            // 
            // rollLabelToolStripMenuItem
            // 
            this.rollLabelToolStripMenuItem.Name = "rollLabelToolStripMenuItem";
            this.rollLabelToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.rollLabelToolStripMenuItem.Text = "Roll or Case Label";
            this.rollLabelToolStripMenuItem.Click += new System.EventHandler(this.RollLabelToolStripMenuItemClick);
            // 
            // adhesiveToteLabelToolStripMenuItem
            // 
            this.adhesiveToteLabelToolStripMenuItem.Name = "adhesiveToteLabelToolStripMenuItem";
            this.adhesiveToteLabelToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.adhesiveToteLabelToolStripMenuItem.Text = "Adhesive Tote Label";
            this.adhesiveToteLabelToolStripMenuItem.Click += new System.EventHandler(this.AdhesiveToteLabelToolStripMenuItemClick);
            // 
            // filmPickListToolStripMenuItem
            // 
            this.filmPickListToolStripMenuItem.Name = "filmPickListToolStripMenuItem";
            this.filmPickListToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.filmPickListToolStripMenuItem.Text = "Film Pick List";
            this.filmPickListToolStripMenuItem.Click += new System.EventHandler(this.FilmPickListToolStripMenuItemClick);
            // 
            // userAdminToolStripMenuItem
            // 
            this.userAdminToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addUserToolStripMenuItem,
            this.editUserToolStripMenuItem,
            this.disableUserToolStripMenuItem,
            this.matchFingerprintToolStripMenuItem});
            this.userAdminToolStripMenuItem.Name = "userAdminToolStripMenuItem";
            this.userAdminToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.userAdminToolStripMenuItem.Text = "User Admin";
            this.userAdminToolStripMenuItem.Visible = false;
            // 
            // addUserToolStripMenuItem
            // 
            this.addUserToolStripMenuItem.Name = "addUserToolStripMenuItem";
            this.addUserToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.addUserToolStripMenuItem.Text = "Add User";
            this.addUserToolStripMenuItem.Click += new System.EventHandler(this.addUserToolStripMenuItem_Click);
            // 
            // editUserToolStripMenuItem
            // 
            this.editUserToolStripMenuItem.Name = "editUserToolStripMenuItem";
            this.editUserToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.editUserToolStripMenuItem.Text = "Edit User";
            this.editUserToolStripMenuItem.Click += new System.EventHandler(this.editUserToolStripMenuItem_Click);
            // 
            // disableUserToolStripMenuItem
            // 
            this.disableUserToolStripMenuItem.Name = "disableUserToolStripMenuItem";
            this.disableUserToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.disableUserToolStripMenuItem.Text = "Disable User";
            this.disableUserToolStripMenuItem.Click += new System.EventHandler(this.disableUserToolStripMenuItem_Click);
            // 
            // matchFingerprintToolStripMenuItem
            // 
            this.matchFingerprintToolStripMenuItem.Name = "matchFingerprintToolStripMenuItem";
            this.matchFingerprintToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.matchFingerprintToolStripMenuItem.Text = "Match Fingerprint";
            this.matchFingerprintToolStripMenuItem.Click += new System.EventHandler(this.matchFingerprintToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1208, 602);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.menuStripMainMenu);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStripMainMenu;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shop Floor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
            this.pnlBottom.ResumeLayout(false);
            this.menuStripMainMenu.ResumeLayout(false);
            this.menuStripMainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.ToolStripMenuItem printPickListToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem productionEditStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem reworkToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem manualReplacePickListRollToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem autoReplaceFilmPickListRollToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem filmPickListToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pickRollsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem voidIssueToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem createtoolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem revertFGPalletToWIPToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem createNewFGPalletToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem issuePickListForStarpakToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem finishWIPPalletToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createNewNonProductionFGPalletToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem raToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userAdminToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disableUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem matchFingerprintToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem plateInventoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PlateInventory;
        private System.Windows.Forms.ToolStripMenuItem PlateCreation;
    }
}
