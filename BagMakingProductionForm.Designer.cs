/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 8/17/2012
 * Time: 9:32 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ShopFloor
{
	public partial class BagMakingProductionForm
	{
		private System.Windows.Forms.Button cmdGetPallet;
		private System.Windows.Forms.RichTextBox rtbLooseCases;
		private System.Windows.Forms.Label lblLooseCases;
		private System.Windows.Forms.Label lblScrapDetail;
		private System.Windows.Forms.Label lblDowntimeDetail;
		private System.Windows.Forms.Label lblCurrentPalletProduction;
		private System.Windows.Forms.RichTextBox rtbCurrentPalletProduction;
		private System.Windows.Forms.Label lblProductionExclCurrentPallet;
		private System.Windows.Forms.RichTextBox rtbCreatedBagInfo;
		private System.Windows.Forms.Panel pnlHours;
		private System.Windows.Forms.Button cmdConsumeRoll;
		private System.Windows.Forms.Button cmdReturnRoll;
		private System.Windows.Forms.Button cmdCreateCase;
		private System.Windows.Forms.Label lblConsumedBags1;
		private System.Windows.Forms.Label lblSetupHs;
		private System.Windows.Forms.Label lblRunHrs;
		private System.Windows.Forms.Label lblDTHrs;
		private System.Windows.Forms.Label lblTotHrs;
		private System.Windows.Forms.Button cmdAddDowntimeRecord;
		private System.Windows.Forms.Label lblJobHours;
		private System.Windows.Forms.ComboBox cboDownTimeReasons;
		private System.Windows.Forms.TextBox txtSetupHours;
		private System.Windows.Forms.TextBox txtRunHours;
		private System.Windows.Forms.TextBox txtDownTimeHours;
		private System.Windows.Forms.Label lblRemoveDTInfo;
		private System.Windows.Forms.ComboBox cboRemoveDowntimeRecord;
		private System.Windows.Forms.Button cmdRemoveDownTimeRecord;
		private System.Windows.Forms.Panel pnlRemoveDownTimeRecord;
		private System.Windows.Forms.TextBox txtTotalHours;
		private System.Windows.Forms.Panel pnlSaveHours;
		private System.Windows.Forms.TextBox txtNewDownTimeHours;
		private System.Windows.Forms.Label lblSaveHours;
		private System.Windows.Forms.TextBox txtConsumedBags1;
		private System.Windows.Forms.Label lblProducedBags;
		private System.Windows.Forms.TextBox txtProducedBags;
		private System.Windows.Forms.Button cmdClosePallet;
		private System.Windows.Forms.Button cmdMoveItem;
		private System.Windows.Forms.Button cmdJobHistory;
		private System.Windows.Forms.RichTextBox rtbDownTimeRecords;
		private System.Windows.Forms.Button cmdReprintLabel;
		private System.Windows.Forms.Button cmdAddScrapRecord;
		private System.Windows.Forms.Button cmdShowFilmUsage;
		private System.Windows.Forms.ComboBox cboScrapReasons;
		private System.Windows.Forms.TextBox txtNewScrapPounds;
		private System.Windows.Forms.RichTextBox rtbScrapRecords;
		private System.Windows.Forms.Label lblRemoveScrapInfo;
		private System.Windows.Forms.Panel pnlRemoveScrapRecord;
		private System.Windows.Forms.TextBox txtTotalScrapPounds;
		private System.Windows.Forms.Label lblTotalScrapPounds;
		private System.Windows.Forms.Button cmdSaveAndClose;
		private System.Windows.Forms.ComboBox cboRemoveScrapRecord;
		private System.Windows.Forms.Button cmdJobComplete;
		private System.Windows.Forms.Button cmdRemoveScrapRecord;
		private System.Windows.Forms.Button cmdJobPulled;
		private System.Windows.Forms.Button cmdEndofShift;

		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">True if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmdEndofShift = new System.Windows.Forms.Button();
            this.cmdJobPulled = new System.Windows.Forms.Button();
            this.cmdRemoveScrapRecord = new System.Windows.Forms.Button();
            this.cmdJobComplete = new System.Windows.Forms.Button();
            this.cboRemoveScrapRecord = new System.Windows.Forms.ComboBox();
            this.cmdSaveAndClose = new System.Windows.Forms.Button();
            this.lblTotalScrapPounds = new System.Windows.Forms.Label();
            this.txtTotalScrapPounds = new System.Windows.Forms.TextBox();
            this.pnlRemoveScrapRecord = new System.Windows.Forms.Panel();
            this.lblRemoveScrapInfo = new System.Windows.Forms.Label();
            this.rtbScrapRecords = new System.Windows.Forms.RichTextBox();
            this.txtNewScrapPounds = new System.Windows.Forms.TextBox();
            this.cboScrapReasons = new System.Windows.Forms.ComboBox();
            this.cmdShowFilmUsage = new System.Windows.Forms.Button();
            this.cmdAddScrapRecord = new System.Windows.Forms.Button();
            this.cmdReprintLabel = new System.Windows.Forms.Button();
            this.rtbDownTimeRecords = new System.Windows.Forms.RichTextBox();
            this.cmdJobHistory = new System.Windows.Forms.Button();
            this.cmdMoveItem = new System.Windows.Forms.Button();
            this.cmdClosePallet = new System.Windows.Forms.Button();
            this.txtProducedBags = new System.Windows.Forms.TextBox();
            this.lblProducedBags = new System.Windows.Forms.Label();
            this.txtConsumedBags1 = new System.Windows.Forms.TextBox();
            this.lblSaveHours = new System.Windows.Forms.Label();
            this.txtNewDownTimeHours = new System.Windows.Forms.TextBox();
            this.pnlSaveHours = new System.Windows.Forms.Panel();
            this.txtTotalHours = new System.Windows.Forms.TextBox();
            this.pnlRemoveDownTimeRecord = new System.Windows.Forms.Panel();
            this.cmdRemoveDownTimeRecord = new System.Windows.Forms.Button();
            this.cboRemoveDowntimeRecord = new System.Windows.Forms.ComboBox();
            this.lblRemoveDTInfo = new System.Windows.Forms.Label();
            this.txtDownTimeHours = new System.Windows.Forms.TextBox();
            this.txtRunHours = new System.Windows.Forms.TextBox();
            this.txtSetupHours = new System.Windows.Forms.TextBox();
            this.cboDownTimeReasons = new System.Windows.Forms.ComboBox();
            this.lblJobHours = new System.Windows.Forms.Label();
            this.cmdAddDowntimeRecord = new System.Windows.Forms.Button();
            this.lblTotHrs = new System.Windows.Forms.Label();
            this.lblDTHrs = new System.Windows.Forms.Label();
            this.lblRunHrs = new System.Windows.Forms.Label();
            this.lblSetupHs = new System.Windows.Forms.Label();
            this.lblConsumedBags1 = new System.Windows.Forms.Label();
            this.cmdCreateCase = new System.Windows.Forms.Button();
            this.cmdReturnRoll = new System.Windows.Forms.Button();
            this.cmdConsumeRoll = new System.Windows.Forms.Button();
            this.pnlHours = new System.Windows.Forms.Panel();
            this.rtbCreatedBagInfo = new System.Windows.Forms.RichTextBox();
            this.lblProductionExclCurrentPallet = new System.Windows.Forms.Label();
            this.rtbCurrentPalletProduction = new System.Windows.Forms.RichTextBox();
            this.lblCurrentPalletProduction = new System.Windows.Forms.Label();
            this.lblDowntimeDetail = new System.Windows.Forms.Label();
            this.lblScrapDetail = new System.Windows.Forms.Label();
            this.lblLooseCases = new System.Windows.Forms.Label();
            this.rtbLooseCases = new System.Windows.Forms.RichTextBox();
            this.cmdGetPallet = new System.Windows.Forms.Button();
            this.rtbJobTitle = new System.Windows.Forms.RichTextBox();
            this.rtbJobDescription = new System.Windows.Forms.RichTextBox();
            this.dgvInputs = new System.Windows.Forms.DataGridView();
            this.endOfShiftTimer = new System.Windows.Forms.Timer(this.components);
            this.JobNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdJobToDateStats = new System.Windows.Forms.Button();
            this.txtConsumedBags2 = new System.Windows.Forms.TextBox();
            this.lblConsumedBags2 = new System.Windows.Forms.Label();
            this.lblSpecialInstructions = new System.Windows.Forms.Label();
            this.rtbSpecialInstructions = new System.Windows.Forms.RichTextBox();
            this.pnlRemoveScrapRecord.SuspendLayout();
            this.pnlSaveHours.SuspendLayout();
            this.pnlRemoveDownTimeRecord.SuspendLayout();
            this.pnlHours.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInputs)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdEndofShift
            // 
            this.cmdEndofShift.Location = new System.Drawing.Point(137, 3);
            this.cmdEndofShift.Name = "cmdEndofShift";
            this.cmdEndofShift.Size = new System.Drawing.Size(60, 50);
            this.cmdEndofShift.TabIndex = 2;
            this.cmdEndofShift.Text = "End of Shift";
            this.cmdEndofShift.UseVisualStyleBackColor = true;
            this.cmdEndofShift.Click += new System.EventHandler(this.CmdEndofShiftClick);
            // 
            // cmdJobPulled
            // 
            this.cmdJobPulled.Location = new System.Drawing.Point(70, 3);
            this.cmdJobPulled.Name = "cmdJobPulled";
            this.cmdJobPulled.Size = new System.Drawing.Size(60, 50);
            this.cmdJobPulled.TabIndex = 1;
            this.cmdJobPulled.Text = "Job Pulled";
            this.cmdJobPulled.UseVisualStyleBackColor = true;
            this.cmdJobPulled.Click += new System.EventHandler(this.cmdJobPulled_Click);
            // 
            // cmdRemoveScrapRecord
            // 
            this.cmdRemoveScrapRecord.Enabled = false;
            this.cmdRemoveScrapRecord.Location = new System.Drawing.Point(233, 3);
            this.cmdRemoveScrapRecord.Name = "cmdRemoveScrapRecord";
            this.cmdRemoveScrapRecord.Size = new System.Drawing.Size(57, 27);
            this.cmdRemoveScrapRecord.TabIndex = 1;
            this.cmdRemoveScrapRecord.Text = "Remove";
            this.cmdRemoveScrapRecord.UseVisualStyleBackColor = true;
            this.cmdRemoveScrapRecord.Click += new System.EventHandler(this.CmdRemoveScrapRecordClick);
            // 
            // cmdJobComplete
            // 
            this.cmdJobComplete.Location = new System.Drawing.Point(3, 3);
            this.cmdJobComplete.Name = "cmdJobComplete";
            this.cmdJobComplete.Size = new System.Drawing.Size(60, 50);
            this.cmdJobComplete.TabIndex = 0;
            this.cmdJobComplete.Text = "Job Complete";
            this.cmdJobComplete.UseVisualStyleBackColor = true;
            this.cmdJobComplete.Click += new System.EventHandler(this.CmdJobCompleteClick);
            // 
            // cboRemoveScrapRecord
            // 
            this.cboRemoveScrapRecord.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRemoveScrapRecord.FormattingEnabled = true;
            this.cboRemoveScrapRecord.Items.AddRange(new object[] {
            ""});
            this.cboRemoveScrapRecord.Location = new System.Drawing.Point(156, 7);
            this.cboRemoveScrapRecord.Name = "cboRemoveScrapRecord";
            this.cboRemoveScrapRecord.Size = new System.Drawing.Size(57, 21);
            this.cboRemoveScrapRecord.TabIndex = 0;
            this.cboRemoveScrapRecord.SelectedIndexChanged += new System.EventHandler(this.CboRemoveScrapInfoSelectedIndexChanged);
            // 
            // cmdSaveAndClose
            // 
            this.cmdSaveAndClose.Location = new System.Drawing.Point(204, 3);
            this.cmdSaveAndClose.Name = "cmdSaveAndClose";
            this.cmdSaveAndClose.Size = new System.Drawing.Size(60, 50);
            this.cmdSaveAndClose.TabIndex = 3;
            this.cmdSaveAndClose.Text = "Save Progress";
            this.cmdSaveAndClose.UseVisualStyleBackColor = true;
            this.cmdSaveAndClose.Click += new System.EventHandler(this.CmdSaveAndCloseClick);
            // 
            // lblTotalScrapPounds
            // 
            this.lblTotalScrapPounds.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalScrapPounds.Location = new System.Drawing.Point(329, 484);
            this.lblTotalScrapPounds.Name = "lblTotalScrapPounds";
            this.lblTotalScrapPounds.Size = new System.Drawing.Size(123, 14);
            this.lblTotalScrapPounds.TabIndex = 11;
            this.lblTotalScrapPounds.Text = "Total Scrap Pounds:";
            this.lblTotalScrapPounds.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTotalScrapPounds
            // 
            this.txtTotalScrapPounds.Enabled = false;
            this.txtTotalScrapPounds.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalScrapPounds.ForeColor = System.Drawing.Color.Black;
            this.txtTotalScrapPounds.Location = new System.Drawing.Point(464, 482);
            this.txtTotalScrapPounds.Name = "txtTotalScrapPounds";
            this.txtTotalScrapPounds.Size = new System.Drawing.Size(70, 20);
            this.txtTotalScrapPounds.TabIndex = 11;
            this.txtTotalScrapPounds.Text = "0";
            this.txtTotalScrapPounds.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // pnlRemoveScrapRecord
            // 
            this.pnlRemoveScrapRecord.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlRemoveScrapRecord.Controls.Add(this.cmdRemoveScrapRecord);
            this.pnlRemoveScrapRecord.Controls.Add(this.cboRemoveScrapRecord);
            this.pnlRemoveScrapRecord.Controls.Add(this.lblRemoveScrapInfo);
            this.pnlRemoveScrapRecord.Location = new System.Drawing.Point(286, 505);
            this.pnlRemoveScrapRecord.Name = "pnlRemoveScrapRecord";
            this.pnlRemoveScrapRecord.Size = new System.Drawing.Size(300, 37);
            this.pnlRemoveScrapRecord.TabIndex = 12;
            this.pnlRemoveScrapRecord.Visible = false;
            // 
            // lblRemoveScrapInfo
            // 
            this.lblRemoveScrapInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemoveScrapInfo.Location = new System.Drawing.Point(6, 10);
            this.lblRemoveScrapInfo.Name = "lblRemoveScrapInfo";
            this.lblRemoveScrapInfo.Size = new System.Drawing.Size(158, 23);
            this.lblRemoveScrapInfo.TabIndex = 0;
            this.lblRemoveScrapInfo.Text = "Remove Scrap Item No.:";
            // 
            // rtbScrapRecords
            // 
            this.rtbScrapRecords.BackColor = System.Drawing.SystemColors.Control;
            this.rtbScrapRecords.ForeColor = System.Drawing.Color.Blue;
            this.rtbScrapRecords.Location = new System.Drawing.Point(286, 356);
            this.rtbScrapRecords.Name = "rtbScrapRecords";
            this.rtbScrapRecords.ReadOnly = true;
            this.rtbScrapRecords.Size = new System.Drawing.Size(300, 90);
            this.rtbScrapRecords.TabIndex = 7;
            this.rtbScrapRecords.TabStop = false;
            this.rtbScrapRecords.Text = "";
            // 
            // txtNewScrapPounds
            // 
            this.txtNewScrapPounds.Location = new System.Drawing.Point(286, 460);
            this.txtNewScrapPounds.Name = "txtNewScrapPounds";
            this.txtNewScrapPounds.Size = new System.Drawing.Size(52, 20);
            this.txtNewScrapPounds.TabIndex = 8;
            this.txtNewScrapPounds.Text = "0";
            this.txtNewScrapPounds.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNewScrapPounds.Enter += new System.EventHandler(this.TxtEnter);
            this.txtNewScrapPounds.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtKeyDown);
            this.txtNewScrapPounds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNewScrapPoundsKeyPress);
            this.txtNewScrapPounds.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtNewScrapPoundsKeyUp);
            this.txtNewScrapPounds.Leave += new System.EventHandler(this.TxtNewScrapPoundsLeave);
            // 
            // cboScrapReasons
            // 
            this.cboScrapReasons.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboScrapReasons.FormattingEnabled = true;
            this.cboScrapReasons.Location = new System.Drawing.Point(351, 459);
            this.cboScrapReasons.Name = "cboScrapReasons";
            this.cboScrapReasons.Size = new System.Drawing.Size(135, 21);
            this.cboScrapReasons.TabIndex = 9;
            this.cboScrapReasons.SelectedIndexChanged += new System.EventHandler(this.CboScrapReasonsSelectedIndexChanged);
            // 
            // cmdShowFilmUsage
            // 
            this.cmdShowFilmUsage.Location = new System.Drawing.Point(492, 733);
            this.cmdShowFilmUsage.Name = "cmdShowFilmUsage";
            this.cmdShowFilmUsage.Size = new System.Drawing.Size(100, 27);
            this.cmdShowFilmUsage.TabIndex = 20;
            this.cmdShowFilmUsage.Text = "Show Film Usage";
            this.cmdShowFilmUsage.UseVisualStyleBackColor = true;
            this.cmdShowFilmUsage.Click += new System.EventHandler(this.CmdShowFilmUsageClick);
            // 
            // cmdAddScrapRecord
            // 
            this.cmdAddScrapRecord.Enabled = false;
            this.cmdAddScrapRecord.Location = new System.Drawing.Point(494, 454);
            this.cmdAddScrapRecord.Name = "cmdAddScrapRecord";
            this.cmdAddScrapRecord.Size = new System.Drawing.Size(92, 27);
            this.cmdAddScrapRecord.TabIndex = 10;
            this.cmdAddScrapRecord.Text = "Add Scrap Lbs";
            this.cmdAddScrapRecord.UseVisualStyleBackColor = true;
            this.cmdAddScrapRecord.Click += new System.EventHandler(this.CmdAddScrapRecordClick);
            // 
            // cmdReprintLabel
            // 
            this.cmdReprintLabel.Location = new System.Drawing.Point(698, 655);
            this.cmdReprintLabel.Name = "cmdReprintLabel";
            this.cmdReprintLabel.Size = new System.Drawing.Size(85, 27);
            this.cmdReprintLabel.TabIndex = 22;
            this.cmdReprintLabel.Text = "Reprint Label";
            this.cmdReprintLabel.UseVisualStyleBackColor = true;
            this.cmdReprintLabel.Click += new System.EventHandler(this.CmdReprintLabelClick);
            // 
            // rtbDownTimeRecords
            // 
            this.rtbDownTimeRecords.BackColor = System.Drawing.SystemColors.Control;
            this.rtbDownTimeRecords.ForeColor = System.Drawing.Color.Blue;
            this.rtbDownTimeRecords.Location = new System.Drawing.Point(7, 505);
            this.rtbDownTimeRecords.Name = "rtbDownTimeRecords";
            this.rtbDownTimeRecords.ReadOnly = true;
            this.rtbDownTimeRecords.Size = new System.Drawing.Size(274, 134);
            this.rtbDownTimeRecords.TabIndex = 4;
            this.rtbDownTimeRecords.TabStop = false;
            this.rtbDownTimeRecords.Text = "";
            this.rtbDownTimeRecords.TextChanged += new System.EventHandler(this.RtbTextChanged);
            // 
            // cmdJobHistory
            // 
            this.cmdJobHistory.Location = new System.Drawing.Point(94, 454);
            this.cmdJobHistory.Name = "cmdJobHistory";
            this.cmdJobHistory.Size = new System.Drawing.Size(100, 27);
            this.cmdJobHistory.TabIndex = 3;
            this.cmdJobHistory.Text = "Show Job History";
            this.cmdJobHistory.UseVisualStyleBackColor = true;
            // 
            // cmdMoveItem
            // 
            this.cmdMoveItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdMoveItem.Location = new System.Drawing.Point(798, 655);
            this.cmdMoveItem.Name = "cmdMoveItem";
            this.cmdMoveItem.Size = new System.Drawing.Size(80, 27);
            this.cmdMoveItem.TabIndex = 23;
            this.cmdMoveItem.Text = "&Move Item";
            this.cmdMoveItem.UseVisualStyleBackColor = true;
            this.cmdMoveItem.Click += new System.EventHandler(this.CmdMoveItemClick);
            // 
            // cmdClosePallet
            // 
            this.cmdClosePallet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClosePallet.Location = new System.Drawing.Point(747, 697);
            this.cmdClosePallet.Name = "cmdClosePallet";
            this.cmdClosePallet.Size = new System.Drawing.Size(100, 27);
            this.cmdClosePallet.TabIndex = 25;
            this.cmdClosePallet.Text = "Close Pallet";
            this.cmdClosePallet.UseVisualStyleBackColor = true;
            this.cmdClosePallet.Click += new System.EventHandler(this.CmdClosePalletClick);
            // 
            // txtProducedBags
            // 
            this.txtProducedBags.Enabled = false;
            this.txtProducedBags.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProducedBags.ForeColor = System.Drawing.Color.Black;
            this.txtProducedBags.Location = new System.Drawing.Point(492, 706);
            this.txtProducedBags.Name = "txtProducedBags";
            this.txtProducedBags.Size = new System.Drawing.Size(98, 20);
            this.txtProducedBags.TabIndex = 17;
            this.txtProducedBags.Text = "0";
            this.txtProducedBags.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblProducedBags
            // 
            this.lblProducedBags.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProducedBags.Location = new System.Drawing.Point(489, 681);
            this.lblProducedBags.Name = "lblProducedBags";
            this.lblProducedBags.Size = new System.Drawing.Size(98, 20);
            this.lblProducedBags.TabIndex = 16;
            this.lblProducedBags.Text = "Produced Bags";
            this.lblProducedBags.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtConsumedBags1
            // 
            this.txtConsumedBags1.Enabled = false;
            this.txtConsumedBags1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConsumedBags1.ForeColor = System.Drawing.Color.Black;
            this.txtConsumedBags1.Location = new System.Drawing.Point(285, 706);
            this.txtConsumedBags1.Name = "txtConsumedBags1";
            this.txtConsumedBags1.Size = new System.Drawing.Size(98, 20);
            this.txtConsumedBags1.TabIndex = 15;
            this.txtConsumedBags1.Text = "0";
            this.txtConsumedBags1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblSaveHours
            // 
            this.lblSaveHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaveHours.Location = new System.Drawing.Point(7, 675);
            this.lblSaveHours.Name = "lblSaveHours";
            this.lblSaveHours.Size = new System.Drawing.Size(275, 28);
            this.lblSaveHours.TabIndex = 47;
            this.lblSaveHours.Text = "Save Job Hours";
            this.lblSaveHours.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNewDownTimeHours
            // 
            this.txtNewDownTimeHours.Location = new System.Drawing.Point(6, 67);
            this.txtNewDownTimeHours.Name = "txtNewDownTimeHours";
            this.txtNewDownTimeHours.Size = new System.Drawing.Size(50, 20);
            this.txtNewDownTimeHours.TabIndex = 4;
            this.txtNewDownTimeHours.Text = "0.00";
            this.txtNewDownTimeHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNewDownTimeHours.Enter += new System.EventHandler(this.TxtEnter);
            this.txtNewDownTimeHours.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtKeyDown);
            this.txtNewDownTimeHours.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtKeyPress);
            this.txtNewDownTimeHours.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtNewDTHrsKeyUp);
            this.txtNewDownTimeHours.Leave += new System.EventHandler(this.TxtLeave);
            // 
            // pnlSaveHours
            // 
            this.pnlSaveHours.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSaveHours.Controls.Add(this.cmdSaveAndClose);
            this.pnlSaveHours.Controls.Add(this.cmdEndofShift);
            this.pnlSaveHours.Controls.Add(this.cmdJobPulled);
            this.pnlSaveHours.Controls.Add(this.cmdJobComplete);
            this.pnlSaveHours.Location = new System.Drawing.Point(7, 704);
            this.pnlSaveHours.Name = "pnlSaveHours";
            this.pnlSaveHours.Size = new System.Drawing.Size(274, 56);
            this.pnlSaveHours.TabIndex = 6;
            // 
            // txtTotalHours
            // 
            this.txtTotalHours.Enabled = false;
            this.txtTotalHours.Location = new System.Drawing.Point(216, 34);
            this.txtTotalHours.Name = "txtTotalHours";
            this.txtTotalHours.Size = new System.Drawing.Size(50, 20);
            this.txtTotalHours.TabIndex = 3;
            this.txtTotalHours.Text = "0.00";
            this.txtTotalHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pnlRemoveDownTimeRecord
            // 
            this.pnlRemoveDownTimeRecord.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlRemoveDownTimeRecord.Controls.Add(this.cmdRemoveDownTimeRecord);
            this.pnlRemoveDownTimeRecord.Controls.Add(this.cboRemoveDowntimeRecord);
            this.pnlRemoveDownTimeRecord.Controls.Add(this.lblRemoveDTInfo);
            this.pnlRemoveDownTimeRecord.Location = new System.Drawing.Point(7, 643);
            this.pnlRemoveDownTimeRecord.Name = "pnlRemoveDownTimeRecord";
            this.pnlRemoveDownTimeRecord.Size = new System.Drawing.Size(274, 37);
            this.pnlRemoveDownTimeRecord.TabIndex = 5;
            this.pnlRemoveDownTimeRecord.Visible = false;
            // 
            // cmdRemoveDownTimeRecord
            // 
            this.cmdRemoveDownTimeRecord.Enabled = false;
            this.cmdRemoveDownTimeRecord.Location = new System.Drawing.Point(207, 3);
            this.cmdRemoveDownTimeRecord.Name = "cmdRemoveDownTimeRecord";
            this.cmdRemoveDownTimeRecord.Size = new System.Drawing.Size(57, 27);
            this.cmdRemoveDownTimeRecord.TabIndex = 1;
            this.cmdRemoveDownTimeRecord.Text = "Remove";
            this.cmdRemoveDownTimeRecord.UseVisualStyleBackColor = true;
            this.cmdRemoveDownTimeRecord.Click += new System.EventHandler(this.CmdRemoveDownTimeRecordClick);
            // 
            // cboRemoveDowntimeRecord
            // 
            this.cboRemoveDowntimeRecord.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRemoveDowntimeRecord.FormattingEnabled = true;
            this.cboRemoveDowntimeRecord.Items.AddRange(new object[] {
            ""});
            this.cboRemoveDowntimeRecord.Location = new System.Drawing.Point(146, 7);
            this.cboRemoveDowntimeRecord.Name = "cboRemoveDowntimeRecord";
            this.cboRemoveDowntimeRecord.Size = new System.Drawing.Size(57, 21);
            this.cboRemoveDowntimeRecord.TabIndex = 0;
            this.cboRemoveDowntimeRecord.SelectedIndexChanged += new System.EventHandler(this.CboRemoveDTInfoSelectedIndexChanged);
            // 
            // lblRemoveDTInfo
            // 
            this.lblRemoveDTInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemoveDTInfo.Location = new System.Drawing.Point(6, 10);
            this.lblRemoveDTInfo.Name = "lblRemoveDTInfo";
            this.lblRemoveDTInfo.Size = new System.Drawing.Size(144, 23);
            this.lblRemoveDTInfo.TabIndex = 0;
            this.lblRemoveDTInfo.Text = "Remove D/T Item No.:";
            // 
            // txtDownTimeHours
            // 
            this.txtDownTimeHours.Enabled = false;
            this.txtDownTimeHours.Location = new System.Drawing.Point(146, 34);
            this.txtDownTimeHours.Name = "txtDownTimeHours";
            this.txtDownTimeHours.Size = new System.Drawing.Size(50, 20);
            this.txtDownTimeHours.TabIndex = 2;
            this.txtDownTimeHours.Text = "0.00";
            this.txtDownTimeHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtRunHours
            // 
            this.txtRunHours.Location = new System.Drawing.Point(76, 34);
            this.txtRunHours.Name = "txtRunHours";
            this.txtRunHours.Size = new System.Drawing.Size(50, 20);
            this.txtRunHours.TabIndex = 1;
            this.txtRunHours.Text = "0.00";
            this.txtRunHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRunHours.Enter += new System.EventHandler(this.TxtEnter);
            this.txtRunHours.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtKeyDown);
            this.txtRunHours.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtKeyPress);
            this.txtRunHours.Leave += new System.EventHandler(this.TxtLeave);
            // 
            // txtSetupHours
            // 
            this.txtSetupHours.Location = new System.Drawing.Point(6, 34);
            this.txtSetupHours.Name = "txtSetupHours";
            this.txtSetupHours.Size = new System.Drawing.Size(50, 20);
            this.txtSetupHours.TabIndex = 0;
            this.txtSetupHours.Text = "0.00";
            this.txtSetupHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSetupHours.Enter += new System.EventHandler(this.TxtEnter);
            this.txtSetupHours.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtKeyDown);
            this.txtSetupHours.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtKeyPress);
            this.txtSetupHours.Leave += new System.EventHandler(this.TxtLeave);
            // 
            // cboDownTimeReasons
            // 
            this.cboDownTimeReasons.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDownTimeReasons.FormattingEnabled = true;
            this.cboDownTimeReasons.Location = new System.Drawing.Point(62, 67);
            this.cboDownTimeReasons.Name = "cboDownTimeReasons";
            this.cboDownTimeReasons.Size = new System.Drawing.Size(139, 21);
            this.cboDownTimeReasons.TabIndex = 5;
            this.cboDownTimeReasons.SelectedIndexChanged += new System.EventHandler(this.CboDowntimeReasonsSelectedIndexChanged);
            // 
            // lblJobHours
            // 
            this.lblJobHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJobHours.Location = new System.Drawing.Point(7, 339);
            this.lblJobHours.Name = "lblJobHours";
            this.lblJobHours.Size = new System.Drawing.Size(272, 14);
            this.lblJobHours.TabIndex = 2;
            this.lblJobHours.Text = "Job Hours";
            this.lblJobHours.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdAddDowntimeRecord
            // 
            this.cmdAddDowntimeRecord.Enabled = false;
            this.cmdAddDowntimeRecord.Location = new System.Drawing.Point(207, 63);
            this.cmdAddDowntimeRecord.Name = "cmdAddDowntimeRecord";
            this.cmdAddDowntimeRecord.Size = new System.Drawing.Size(59, 27);
            this.cmdAddDowntimeRecord.TabIndex = 6;
            this.cmdAddDowntimeRecord.Text = "Add D/T";
            this.cmdAddDowntimeRecord.UseVisualStyleBackColor = true;
            this.cmdAddDowntimeRecord.Click += new System.EventHandler(this.CmdAddDTClick);
            // 
            // lblTotHrs
            // 
            this.lblTotHrs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotHrs.Location = new System.Drawing.Point(216, 4);
            this.lblTotHrs.Name = "lblTotHrs";
            this.lblTotHrs.Size = new System.Drawing.Size(50, 27);
            this.lblTotHrs.TabIndex = 3;
            this.lblTotHrs.Text = "Total Hrs";
            this.lblTotHrs.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblDTHrs
            // 
            this.lblDTHrs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDTHrs.Location = new System.Drawing.Point(146, 4);
            this.lblDTHrs.Name = "lblDTHrs";
            this.lblDTHrs.Size = new System.Drawing.Size(50, 27);
            this.lblDTHrs.TabIndex = 2;
            this.lblDTHrs.Text = "D/T Hrs";
            this.lblDTHrs.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblRunHrs
            // 
            this.lblRunHrs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRunHrs.Location = new System.Drawing.Point(76, 4);
            this.lblRunHrs.Name = "lblRunHrs";
            this.lblRunHrs.Size = new System.Drawing.Size(50, 27);
            this.lblRunHrs.TabIndex = 1;
            this.lblRunHrs.Text = "Run Hrs";
            this.lblRunHrs.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblSetupHs
            // 
            this.lblSetupHs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSetupHs.Location = new System.Drawing.Point(6, 4);
            this.lblSetupHs.Name = "lblSetupHs";
            this.lblSetupHs.Size = new System.Drawing.Size(50, 27);
            this.lblSetupHs.TabIndex = 0;
            this.lblSetupHs.Text = "Setup Hrs";
            this.lblSetupHs.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblConsumedBags1
            // 
            this.lblConsumedBags1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConsumedBags1.Location = new System.Drawing.Point(285, 681);
            this.lblConsumedBags1.Name = "lblConsumedBags1";
            this.lblConsumedBags1.Size = new System.Drawing.Size(98, 20);
            this.lblConsumedBags1.TabIndex = 15;
            this.lblConsumedBags1.Text = "Consumed Bags";
            this.lblConsumedBags1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdCreateCase
            // 
            this.cmdCreateCase.Location = new System.Drawing.Point(598, 655);
            this.cmdCreateCase.Name = "cmdCreateCase";
            this.cmdCreateCase.Size = new System.Drawing.Size(85, 27);
            this.cmdCreateCase.TabIndex = 21;
            this.cmdCreateCase.Text = "&Create Case";
            this.cmdCreateCase.UseVisualStyleBackColor = true;
            this.cmdCreateCase.Click += new System.EventHandler(this.CmdCreateCaseClick);
            // 
            // cmdReturnRoll
            // 
            this.cmdReturnRoll.Location = new System.Drawing.Point(389, 732);
            this.cmdReturnRoll.Name = "cmdReturnRoll";
            this.cmdReturnRoll.Size = new System.Drawing.Size(98, 27);
            this.cmdReturnRoll.TabIndex = 19;
            this.cmdReturnRoll.Text = "Return Roll";
            this.cmdReturnRoll.UseVisualStyleBackColor = true;
            this.cmdReturnRoll.Click += new System.EventHandler(this.CmdReturnRollClick);
            // 
            // cmdConsumeRoll
            // 
            this.cmdConsumeRoll.Location = new System.Drawing.Point(286, 732);
            this.cmdConsumeRoll.Name = "cmdConsumeRoll";
            this.cmdConsumeRoll.Size = new System.Drawing.Size(98, 27);
            this.cmdConsumeRoll.TabIndex = 17;
            this.cmdConsumeRoll.Text = "Consume Roll";
            this.cmdConsumeRoll.UseVisualStyleBackColor = true;
            this.cmdConsumeRoll.Click += new System.EventHandler(this.CmdConsumeRollClick);
            // 
            // pnlHours
            // 
            this.pnlHours.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlHours.Controls.Add(this.txtNewDownTimeHours);
            this.pnlHours.Controls.Add(this.txtTotalHours);
            this.pnlHours.Controls.Add(this.txtDownTimeHours);
            this.pnlHours.Controls.Add(this.txtRunHours);
            this.pnlHours.Controls.Add(this.txtSetupHours);
            this.pnlHours.Controls.Add(this.cboDownTimeReasons);
            this.pnlHours.Controls.Add(this.cmdAddDowntimeRecord);
            this.pnlHours.Controls.Add(this.lblTotHrs);
            this.pnlHours.Controls.Add(this.lblDTHrs);
            this.pnlHours.Controls.Add(this.lblRunHrs);
            this.pnlHours.Controls.Add(this.lblSetupHs);
            this.pnlHours.Location = new System.Drawing.Point(7, 356);
            this.pnlHours.Name = "pnlHours";
            this.pnlHours.Size = new System.Drawing.Size(274, 95);
            this.pnlHours.TabIndex = 2;
            // 
            // rtbCreatedBagInfo
            // 
            this.rtbCreatedBagInfo.BackColor = System.Drawing.SystemColors.Control;
            this.rtbCreatedBagInfo.ForeColor = System.Drawing.Color.Blue;
            this.rtbCreatedBagInfo.Location = new System.Drawing.Point(592, 356);
            this.rtbCreatedBagInfo.Name = "rtbCreatedBagInfo";
            this.rtbCreatedBagInfo.ReadOnly = true;
            this.rtbCreatedBagInfo.Size = new System.Drawing.Size(286, 90);
            this.rtbCreatedBagInfo.TabIndex = 21;
            this.rtbCreatedBagInfo.TabStop = false;
            this.rtbCreatedBagInfo.Text = "";
            // 
            // lblProductionExclCurrentPallet
            // 
            this.lblProductionExclCurrentPallet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductionExclCurrentPallet.Location = new System.Drawing.Point(592, 339);
            this.lblProductionExclCurrentPallet.Name = "lblProductionExclCurrentPallet";
            this.lblProductionExclCurrentPallet.Size = new System.Drawing.Size(286, 14);
            this.lblProductionExclCurrentPallet.TabIndex = 20;
            this.lblProductionExclCurrentPallet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rtbCurrentPalletProduction
            // 
            this.rtbCurrentPalletProduction.BackColor = System.Drawing.SystemColors.Control;
            this.rtbCurrentPalletProduction.ForeColor = System.Drawing.Color.Blue;
            this.rtbCurrentPalletProduction.Location = new System.Drawing.Point(286, 568);
            this.rtbCurrentPalletProduction.Name = "rtbCurrentPalletProduction";
            this.rtbCurrentPalletProduction.ReadOnly = true;
            this.rtbCurrentPalletProduction.Size = new System.Drawing.Size(300, 110);
            this.rtbCurrentPalletProduction.TabIndex = 14;
            this.rtbCurrentPalletProduction.TabStop = false;
            this.rtbCurrentPalletProduction.Text = "";
            // 
            // lblCurrentPalletProduction
            // 
            this.lblCurrentPalletProduction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentPalletProduction.Location = new System.Drawing.Point(286, 546);
            this.lblCurrentPalletProduction.Name = "lblCurrentPalletProduction";
            this.lblCurrentPalletProduction.Size = new System.Drawing.Size(300, 19);
            this.lblCurrentPalletProduction.TabIndex = 13;
            this.lblCurrentPalletProduction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDowntimeDetail
            // 
            this.lblDowntimeDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDowntimeDetail.Location = new System.Drawing.Point(7, 488);
            this.lblDowntimeDetail.Name = "lblDowntimeDetail";
            this.lblDowntimeDetail.Size = new System.Drawing.Size(273, 14);
            this.lblDowntimeDetail.TabIndex = 67;
            this.lblDowntimeDetail.Text = "Downtime Detail";
            this.lblDowntimeDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblScrapDetail
            // 
            this.lblScrapDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScrapDetail.Location = new System.Drawing.Point(286, 339);
            this.lblScrapDetail.Name = "lblScrapDetail";
            this.lblScrapDetail.Size = new System.Drawing.Size(272, 14);
            this.lblScrapDetail.TabIndex = 7;
            this.lblScrapDetail.Text = "Scrap Detail";
            this.lblScrapDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLooseCases
            // 
            this.lblLooseCases.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLooseCases.Location = new System.Drawing.Point(592, 458);
            this.lblLooseCases.Name = "lblLooseCases";
            this.lblLooseCases.Size = new System.Drawing.Size(286, 19);
            this.lblLooseCases.TabIndex = 68;
            this.lblLooseCases.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rtbLooseCases
            // 
            this.rtbLooseCases.BackColor = System.Drawing.SystemColors.Control;
            this.rtbLooseCases.ForeColor = System.Drawing.Color.Blue;
            this.rtbLooseCases.Location = new System.Drawing.Point(592, 482);
            this.rtbLooseCases.Name = "rtbLooseCases";
            this.rtbLooseCases.ReadOnly = true;
            this.rtbLooseCases.Size = new System.Drawing.Size(286, 157);
            this.rtbLooseCases.TabIndex = 69;
            this.rtbLooseCases.TabStop = false;
            this.rtbLooseCases.Text = "";
            // 
            // cmdGetPallet
            // 
            this.cmdGetPallet.Enabled = false;
            this.cmdGetPallet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetPallet.Location = new System.Drawing.Point(630, 697);
            this.cmdGetPallet.Name = "cmdGetPallet";
            this.cmdGetPallet.Size = new System.Drawing.Size(100, 27);
            this.cmdGetPallet.TabIndex = 24;
            this.cmdGetPallet.UseVisualStyleBackColor = true;
            this.cmdGetPallet.Click += new System.EventHandler(this.CmdGetPalletClick);
            // 
            // rtbJobTitle
            // 
            this.rtbJobTitle.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.rtbJobTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbJobTitle.ForeColor = System.Drawing.Color.White;
            this.rtbJobTitle.Location = new System.Drawing.Point(7, 3);
            this.rtbJobTitle.Name = "rtbJobTitle";
            this.rtbJobTitle.ReadOnly = true;
            this.rtbJobTitle.Size = new System.Drawing.Size(871, 25);
            this.rtbJobTitle.TabIndex = 70;
            this.rtbJobTitle.TabStop = false;
            this.rtbJobTitle.Text = "";
            // 
            // rtbJobDescription
            // 
            this.rtbJobDescription.BackColor = System.Drawing.SystemColors.Control;
            this.rtbJobDescription.ForeColor = System.Drawing.Color.Blue;
            this.rtbJobDescription.Location = new System.Drawing.Point(6, 25);
            this.rtbJobDescription.Name = "rtbJobDescription";
            this.rtbJobDescription.ReadOnly = true;
            this.rtbJobDescription.Size = new System.Drawing.Size(421, 127);
            this.rtbJobDescription.TabIndex = 71;
            this.rtbJobDescription.TabStop = false;
            this.rtbJobDescription.Text = "";
            // 
            // dgvInputs
            // 
            this.dgvInputs.AllowUserToAddRows = false;
            this.dgvInputs.AllowUserToDeleteRows = false;
            this.dgvInputs.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvInputs.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInputs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvInputs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInputs.Location = new System.Drawing.Point(429, 25);
            this.dgvInputs.MultiSelect = false;
            this.dgvInputs.Name = "dgvInputs";
            this.dgvInputs.ReadOnly = true;
            this.dgvInputs.RowHeadersVisible = false;
            this.dgvInputs.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInputs.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvInputs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInputs.Size = new System.Drawing.Size(449, 127);
            this.dgvInputs.TabIndex = 72;
            // 
            // endOfShiftTimer
            // 
            this.endOfShiftTimer.Tick += new System.EventHandler(this.EndOfShiftTimerTick);
            // 
            // JobNo
            // 
            this.JobNo.HeaderText = "Unwind";
            this.JobNo.Name = "JobNo";
            this.JobNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.JobNo.Width = 50;
            // 
            // cmdJobToDateStats
            // 
            this.cmdJobToDateStats.Location = new System.Drawing.Point(630, 733);
            this.cmdJobToDateStats.Name = "cmdJobToDateStats";
            this.cmdJobToDateStats.Size = new System.Drawing.Size(217, 27);
            this.cmdJobToDateStats.TabIndex = 26;
            this.cmdJobToDateStats.Text = "Job-to-Date Stats";
            this.cmdJobToDateStats.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdJobToDateStats.UseVisualStyleBackColor = true;
            this.cmdJobToDateStats.Click += new System.EventHandler(this.cmdJobToDateStats_Click);
            // 
            // txtConsumedBags2
            // 
            this.txtConsumedBags2.Enabled = false;
            this.txtConsumedBags2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConsumedBags2.ForeColor = System.Drawing.Color.Black;
            this.txtConsumedBags2.Location = new System.Drawing.Point(389, 706);
            this.txtConsumedBags2.Name = "txtConsumedBags2";
            this.txtConsumedBags2.Size = new System.Drawing.Size(97, 20);
            this.txtConsumedBags2.TabIndex = 16;
            this.txtConsumedBags2.Text = "0";
            this.txtConsumedBags2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtConsumedBags2.Visible = false;
            // 
            // lblConsumedBags2
            // 
            this.lblConsumedBags2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConsumedBags2.Location = new System.Drawing.Point(389, 681);
            this.lblConsumedBags2.Name = "lblConsumedBags2";
            this.lblConsumedBags2.Size = new System.Drawing.Size(98, 20);
            this.lblConsumedBags2.TabIndex = 77;
            this.lblConsumedBags2.Text = "Bags - UW 2";
            this.lblConsumedBags2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblConsumedBags2.Visible = false;
            // 
            // lblSpecialInstructions
            // 
            this.lblSpecialInstructions.AutoSize = true;
            this.lblSpecialInstructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpecialInstructions.Location = new System.Drawing.Point(7, 159);
            this.lblSpecialInstructions.Name = "lblSpecialInstructions";
            this.lblSpecialInstructions.Size = new System.Drawing.Size(119, 13);
            this.lblSpecialInstructions.TabIndex = 78;
            this.lblSpecialInstructions.Text = "Special Instructions";
            // 
            // rtbSpecialInstructions
            // 
            this.rtbSpecialInstructions.BackColor = System.Drawing.SystemColors.Control;
            this.rtbSpecialInstructions.ForeColor = System.Drawing.Color.Blue;
            this.rtbSpecialInstructions.Location = new System.Drawing.Point(6, 175);
            this.rtbSpecialInstructions.Name = "rtbSpecialInstructions";
            this.rtbSpecialInstructions.ReadOnly = true;
            this.rtbSpecialInstructions.Size = new System.Drawing.Size(872, 161);
            this.rtbSpecialInstructions.TabIndex = 79;
            this.rtbSpecialInstructions.TabStop = false;
            this.rtbSpecialInstructions.Text = "";
            // 
            // BagMakingProductionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 770);
            this.ControlBox = false;
            this.Controls.Add(this.rtbSpecialInstructions);
            this.Controls.Add(this.lblSpecialInstructions);
            this.Controls.Add(this.txtConsumedBags2);
            this.Controls.Add(this.lblConsumedBags2);
            this.Controls.Add(this.cmdJobToDateStats);
            this.Controls.Add(this.dgvInputs);
            this.Controls.Add(this.rtbJobDescription);
            this.Controls.Add(this.rtbJobTitle);
            this.Controls.Add(this.cmdGetPallet);
            this.Controls.Add(this.rtbLooseCases);
            this.Controls.Add(this.lblLooseCases);
            this.Controls.Add(this.lblScrapDetail);
            this.Controls.Add(this.lblDowntimeDetail);
            this.Controls.Add(this.lblCurrentPalletProduction);
            this.Controls.Add(this.lblProductionExclCurrentPallet);
            this.Controls.Add(this.lblTotalScrapPounds);
            this.Controls.Add(this.txtTotalScrapPounds);
            this.Controls.Add(this.pnlRemoveScrapRecord);
            this.Controls.Add(this.rtbScrapRecords);
            this.Controls.Add(this.txtNewScrapPounds);
            this.Controls.Add(this.cboScrapReasons);
            this.Controls.Add(this.cmdShowFilmUsage);
            this.Controls.Add(this.cmdAddScrapRecord);
            this.Controls.Add(this.cmdReprintLabel);
            this.Controls.Add(this.cmdJobHistory);
            this.Controls.Add(this.cmdMoveItem);
            this.Controls.Add(this.cmdClosePallet);
            this.Controls.Add(this.txtProducedBags);
            this.Controls.Add(this.lblProducedBags);
            this.Controls.Add(this.txtConsumedBags1);
            this.Controls.Add(this.lblSaveHours);
            this.Controls.Add(this.pnlSaveHours);
            this.Controls.Add(this.pnlRemoveDownTimeRecord);
            this.Controls.Add(this.lblJobHours);
            this.Controls.Add(this.lblConsumedBags1);
            this.Controls.Add(this.cmdCreateCase);
            this.Controls.Add(this.cmdReturnRoll);
            this.Controls.Add(this.cmdConsumeRoll);
            this.Controls.Add(this.pnlHours);
            this.Controls.Add(this.rtbDownTimeRecords);
            this.Controls.Add(this.rtbCurrentPalletProduction);
            this.Controls.Add(this.rtbCreatedBagInfo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BagMakingProductionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Shown += new System.EventHandler(this.BagMakingProductionForm_Shown);
            this.pnlRemoveScrapRecord.ResumeLayout(false);
            this.pnlSaveHours.ResumeLayout(false);
            this.pnlRemoveDownTimeRecord.ResumeLayout(false);
            this.pnlHours.ResumeLayout(false);
            this.pnlHours.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInputs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.RichTextBox rtbJobTitle;
		private System.Windows.Forms.RichTextBox rtbJobDescription;
		private System.Windows.Forms.DataGridView dgvInputs;
		private System.Windows.Forms.DataGridViewTextBoxColumn JobNo;
		private System.Windows.Forms.Timer endOfShiftTimer;
        private System.Windows.Forms.Button cmdJobToDateStats;
        private System.Windows.Forms.TextBox txtConsumedBags2;
        private System.Windows.Forms.Label lblConsumedBags2;
        private System.Windows.Forms.Label lblSpecialInstructions;
        private System.Windows.Forms.RichTextBox rtbSpecialInstructions;
    }
}
