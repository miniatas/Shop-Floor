namespace ShopFloor
{
    partial class InnolokProductionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle83 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle84 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtDownTimeHours = new System.Windows.Forms.TextBox();
            this.rtbSpecialInstructions = new System.Windows.Forms.RichTextBox();
            this.lblJobHours = new System.Windows.Forms.Label();
            this.txtTotalHours = new System.Windows.Forms.TextBox();
            this.lblConsumedLF = new System.Windows.Forms.Label();
            this.txtNewDownTimeHours = new System.Windows.Forms.TextBox();
            this.rtbInputFilm = new System.Windows.Forms.RichTextBox();
            this.cmdCreateRoll = new System.Windows.Forms.Button();
            this.cmdReturnRoll = new System.Windows.Forms.Button();
            this.cmdConsumeRoll = new System.Windows.Forms.Button();
            this.pnlHours = new System.Windows.Forms.Panel();
            this.txtRunHours = new System.Windows.Forms.TextBox();
            this.txtSetupHours = new System.Windows.Forms.TextBox();
            this.cboDownTimeReasons = new System.Windows.Forms.ComboBox();
            this.cmdAddDowntimeRecord = new System.Windows.Forms.Button();
            this.lblTotHrs = new System.Windows.Forms.Label();
            this.lblDTHrs = new System.Windows.Forms.Label();
            this.lblRunHrs = new System.Windows.Forms.Label();
            this.lblSetupHs = new System.Windows.Forms.Label();
            this.dgvJobs = new System.Windows.Forms.DataGridView();
            this.JobNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JobDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoStreams = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MasterItemNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentPalletNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PalletWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StreamWidth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rtbCurrentPalletProduction = new System.Windows.Forms.RichTextBox();
            this.cmdGetPallet = new System.Windows.Forms.Button();
            this.rtbLooseRolls = new System.Windows.Forms.RichTextBox();
            this.lblLooseRolls = new System.Windows.Forms.Label();
            this.lblScrapDetail = new System.Windows.Forms.Label();
            this.lblDowntimeDetail = new System.Windows.Forms.Label();
            this.lblCurrentPalletProduction = new System.Windows.Forms.Label();
            this.lblProductionExclCurrentPallet = new System.Windows.Forms.Label();
            this.rtbCreatedWIPInfo = new System.Windows.Forms.RichTextBox();
            this.endOfShiftTimer = new System.Windows.Forms.Timer(this.components);
            this.cmdJobToDateStats = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rtbJobTitle = new System.Windows.Forms.RichTextBox();
            this.lblTotalScrapPounds = new System.Windows.Forms.Label();
            this.txtTotalScrapPounds = new System.Windows.Forms.TextBox();
            this.cmdSaveAndClose = new System.Windows.Forms.Button();
            this.cboRemoveScrapRecord = new System.Windows.Forms.ComboBox();
            this.pnlRemoveScrapRecord = new System.Windows.Forms.Panel();
            this.cmdRemoveScrapRecord = new System.Windows.Forms.Button();
            this.lblRemoveScrapInfo = new System.Windows.Forms.Label();
            this.rtbScrapRecords = new System.Windows.Forms.RichTextBox();
            this.txtNewScrapPounds = new System.Windows.Forms.TextBox();
            this.cboScrapReasons = new System.Windows.Forms.ComboBox();
            this.cmdShowFilmUsage = new System.Windows.Forms.Button();
            this.cmdAddScrapRecord = new System.Windows.Forms.Button();
            this.cmdReprintLabel = new System.Windows.Forms.Button();
            this.cmdMoveRoll = new System.Windows.Forms.Button();
            this.cmdClosePallet = new System.Windows.Forms.Button();
            this.txtCreatedFeet = new System.Windows.Forms.TextBox();
            this.cmdEndofShift = new System.Windows.Forms.Button();
            this.cmdJobHistory = new System.Windows.Forms.Button();
            this.lblCreatedLF = new System.Windows.Forms.Label();
            this.txtConsumedFeet = new System.Windows.Forms.TextBox();
            this.lblSaveHours = new System.Windows.Forms.Label();
            this.pnlSaveHours = new System.Windows.Forms.Panel();
            this.cmdJobPulled = new System.Windows.Forms.Button();
            this.cmdJobComplete = new System.Windows.Forms.Button();
            this.pnlRemoveDownTimeRecord = new System.Windows.Forms.Panel();
            this.cmdRemoveDownTimeRecord = new System.Windows.Forms.Button();
            this.cboRemoveDowntimeRecord = new System.Windows.Forms.ComboBox();
            this.lblRemoveDTInfo = new System.Windows.Forms.Label();
            this.rtbDownTimeRecords = new System.Windows.Forms.RichTextBox();
            this.pnlHours.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJobs)).BeginInit();
            this.pnlRemoveScrapRecord.SuspendLayout();
            this.pnlSaveHours.SuspendLayout();
            this.pnlRemoveDownTimeRecord.SuspendLayout();
            this.SuspendLayout();
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
            // rtbSpecialInstructions
            // 
            this.rtbSpecialInstructions.BackColor = System.Drawing.SystemColors.Control;
            this.rtbSpecialInstructions.ForeColor = System.Drawing.Color.Blue;
            this.rtbSpecialInstructions.Location = new System.Drawing.Point(7, 158);
            this.rtbSpecialInstructions.Name = "rtbSpecialInstructions";
            this.rtbSpecialInstructions.ReadOnly = true;
            this.rtbSpecialInstructions.Size = new System.Drawing.Size(871, 183);
            this.rtbSpecialInstructions.TabIndex = 112;
            this.rtbSpecialInstructions.TabStop = false;
            this.rtbSpecialInstructions.Text = "";
            // 
            // lblJobHours
            // 
            this.lblJobHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJobHours.Location = new System.Drawing.Point(7, 370);
            this.lblJobHours.Name = "lblJobHours";
            this.lblJobHours.Size = new System.Drawing.Size(272, 14);
            this.lblJobHours.TabIndex = 76;
            this.lblJobHours.Text = "Job Hours";
            this.lblJobHours.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // lblConsumedLF
            // 
            this.lblConsumedLF.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConsumedLF.Location = new System.Drawing.Point(337, 712);
            this.lblConsumedLF.Name = "lblConsumedLF";
            this.lblConsumedLF.Size = new System.Drawing.Size(90, 20);
            this.lblConsumedLF.TabIndex = 93;
            this.lblConsumedLF.Text = "Consumed LF";
            this.lblConsumedLF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // rtbInputFilm
            // 
            this.rtbInputFilm.BackColor = System.Drawing.SystemColors.Control;
            this.rtbInputFilm.ForeColor = System.Drawing.Color.Blue;
            this.rtbInputFilm.Location = new System.Drawing.Point(7, 347);
            this.rtbInputFilm.Name = "rtbInputFilm";
            this.rtbInputFilm.ReadOnly = true;
            this.rtbInputFilm.Size = new System.Drawing.Size(871, 21);
            this.rtbInputFilm.TabIndex = 75;
            this.rtbInputFilm.TabStop = false;
            this.rtbInputFilm.Text = "";
            this.rtbInputFilm.TextChanged += new System.EventHandler(this.rtbInputFilm_TextChanged);
            // 
            // cmdCreateRoll
            // 
            this.cmdCreateRoll.Location = new System.Drawing.Point(592, 682);
            this.cmdCreateRoll.Name = "cmdCreateRoll";
            this.cmdCreateRoll.Size = new System.Drawing.Size(85, 27);
            this.cmdCreateRoll.TabIndex = 99;
            this.cmdCreateRoll.Text = "&Create Roll";
            this.cmdCreateRoll.UseVisualStyleBackColor = true;
            this.cmdCreateRoll.Click += new System.EventHandler(this.CmdCreateRoll);
            // 
            // cmdReturnRoll
            // 
            this.cmdReturnRoll.Location = new System.Drawing.Point(385, 763);
            this.cmdReturnRoll.Name = "cmdReturnRoll";
            this.cmdReturnRoll.Size = new System.Drawing.Size(100, 27);
            this.cmdReturnRoll.TabIndex = 97;
            this.cmdReturnRoll.Text = "Return Roll";
            this.cmdReturnRoll.UseVisualStyleBackColor = true;
            this.cmdReturnRoll.Click += new System.EventHandler(this.cmdReturnRoll_Click);
            // 
            // cmdConsumeRoll
            // 
            this.cmdConsumeRoll.Location = new System.Drawing.Point(286, 763);
            this.cmdConsumeRoll.Name = "cmdConsumeRoll";
            this.cmdConsumeRoll.Size = new System.Drawing.Size(93, 27);
            this.cmdConsumeRoll.TabIndex = 96;
            this.cmdConsumeRoll.Text = "Consume Roll";
            this.cmdConsumeRoll.UseVisualStyleBackColor = true;
            this.cmdConsumeRoll.Click += new System.EventHandler(this.cmdConsumeRoll_Click);
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
            this.pnlHours.Location = new System.Drawing.Point(7, 387);
            this.pnlHours.Name = "pnlHours";
            this.pnlHours.Size = new System.Drawing.Size(274, 95);
            this.pnlHours.TabIndex = 77;
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
            this.cboDownTimeReasons.SelectedIndexChanged += new System.EventHandler(this.cboDownTimeReasons_SelectedIndexChanged);
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
            this.cmdAddDowntimeRecord.Click += new System.EventHandler(this.cmdAddDowntimeRecord_Click);
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
            // dgvJobs
            // 
            this.dgvJobs.AllowUserToAddRows = false;
            this.dgvJobs.AllowUserToDeleteRows = false;
            this.dgvJobs.AllowUserToResizeColumns = false;
            this.dgvJobs.AllowUserToResizeRows = false;
            dataGridViewCellStyle83.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter;
            dataGridViewCellStyle83.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle83.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle83.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle83.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle83.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle83.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvJobs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle83;
            this.dgvJobs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJobs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.JobNo,
            this.JobDescription,
            this.NoStreams,
            this.MasterItemNo,
            this.CurrentPalletNumber,
            this.PalletWeight,
            this.StreamWidth});
            this.dgvJobs.Location = new System.Drawing.Point(7, 34);
            this.dgvJobs.MultiSelect = false;
            this.dgvJobs.Name = "dgvJobs";
            this.dgvJobs.ReadOnly = true;
            this.dgvJobs.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle84.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvJobs.RowsDefaultCellStyle = dataGridViewCellStyle84;
            this.dgvJobs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvJobs.Size = new System.Drawing.Size(871, 100);
            this.dgvJobs.TabIndex = 74;
            // 
            // JobNo
            // 
            this.JobNo.HeaderText = "Job #";
            this.JobNo.Name = "JobNo";
            this.JobNo.ReadOnly = true;
            this.JobNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.JobNo.Width = 60;
            // 
            // JobDescription
            // 
            this.JobDescription.HeaderText = "Description";
            this.JobDescription.Name = "JobDescription";
            this.JobDescription.ReadOnly = true;
            this.JobDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.JobDescription.Width = 600;
            // 
            // NoStreams
            // 
            this.NoStreams.HeaderText = "# Streams";
            this.NoStreams.Name = "NoStreams";
            this.NoStreams.ReadOnly = true;
            this.NoStreams.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NoStreams.Width = 80;
            // 
            // MasterItemNo
            // 
            this.MasterItemNo.HeaderText = "Master Item No";
            this.MasterItemNo.Name = "MasterItemNo";
            this.MasterItemNo.ReadOnly = true;
            this.MasterItemNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MasterItemNo.Visible = false;
            // 
            // CurrentPalletNumber
            // 
            this.CurrentPalletNumber.HeaderText = "Pallet #";
            this.CurrentPalletNumber.Name = "CurrentPalletNumber";
            this.CurrentPalletNumber.ReadOnly = true;
            this.CurrentPalletNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CurrentPalletNumber.Width = 70;
            // 
            // PalletWeight
            // 
            this.PalletWeight.HeaderText = "Pallet Weight";
            this.PalletWeight.Name = "PalletWeight";
            this.PalletWeight.ReadOnly = true;
            this.PalletWeight.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PalletWeight.Visible = false;
            // 
            // StreamWidth
            // 
            this.StreamWidth.HeaderText = "Stream Width";
            this.StreamWidth.Name = "StreamWidth";
            this.StreamWidth.ReadOnly = true;
            this.StreamWidth.Visible = false;
            // 
            // rtbCurrentPalletProduction
            // 
            this.rtbCurrentPalletProduction.BackColor = System.Drawing.SystemColors.Control;
            this.rtbCurrentPalletProduction.ForeColor = System.Drawing.Color.Blue;
            this.rtbCurrentPalletProduction.Location = new System.Drawing.Point(286, 599);
            this.rtbCurrentPalletProduction.Name = "rtbCurrentPalletProduction";
            this.rtbCurrentPalletProduction.ReadOnly = true;
            this.rtbCurrentPalletProduction.Size = new System.Drawing.Size(300, 110);
            this.rtbCurrentPalletProduction.TabIndex = 91;
            this.rtbCurrentPalletProduction.TabStop = false;
            this.rtbCurrentPalletProduction.Text = "";
            // 
            // cmdGetPallet
            // 
            this.cmdGetPallet.Enabled = false;
            this.cmdGetPallet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetPallet.Location = new System.Drawing.Point(632, 726);
            this.cmdGetPallet.Name = "cmdGetPallet";
            this.cmdGetPallet.Size = new System.Drawing.Size(100, 27);
            this.cmdGetPallet.TabIndex = 104;
            this.cmdGetPallet.UseVisualStyleBackColor = true;
            this.cmdGetPallet.Click += new System.EventHandler(this.cmdGetPallet_Click);
            // 
            // rtbLooseRolls
            // 
            this.rtbLooseRolls.BackColor = System.Drawing.SystemColors.Control;
            this.rtbLooseRolls.ForeColor = System.Drawing.Color.Blue;
            this.rtbLooseRolls.Location = new System.Drawing.Point(592, 513);
            this.rtbLooseRolls.Name = "rtbLooseRolls";
            this.rtbLooseRolls.ReadOnly = true;
            this.rtbLooseRolls.Size = new System.Drawing.Size(286, 157);
            this.rtbLooseRolls.TabIndex = 109;
            this.rtbLooseRolls.TabStop = false;
            this.rtbLooseRolls.Text = "";
            // 
            // lblLooseRolls
            // 
            this.lblLooseRolls.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLooseRolls.Location = new System.Drawing.Point(592, 489);
            this.lblLooseRolls.Name = "lblLooseRolls";
            this.lblLooseRolls.Size = new System.Drawing.Size(286, 19);
            this.lblLooseRolls.TabIndex = 108;
            this.lblLooseRolls.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblScrapDetail
            // 
            this.lblScrapDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScrapDetail.Location = new System.Drawing.Point(286, 370);
            this.lblScrapDetail.Name = "lblScrapDetail";
            this.lblScrapDetail.Size = new System.Drawing.Size(272, 14);
            this.lblScrapDetail.TabIndex = 82;
            this.lblScrapDetail.Text = "Scrap Detail";
            this.lblScrapDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDowntimeDetail
            // 
            this.lblDowntimeDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDowntimeDetail.Location = new System.Drawing.Point(7, 519);
            this.lblDowntimeDetail.Name = "lblDowntimeDetail";
            this.lblDowntimeDetail.Size = new System.Drawing.Size(273, 14);
            this.lblDowntimeDetail.TabIndex = 107;
            this.lblDowntimeDetail.Text = "Downtime Detail";
            this.lblDowntimeDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrentPalletProduction
            // 
            this.lblCurrentPalletProduction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentPalletProduction.Location = new System.Drawing.Point(286, 577);
            this.lblCurrentPalletProduction.Name = "lblCurrentPalletProduction";
            this.lblCurrentPalletProduction.Size = new System.Drawing.Size(300, 19);
            this.lblCurrentPalletProduction.TabIndex = 90;
            this.lblCurrentPalletProduction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProductionExclCurrentPallet
            // 
            this.lblProductionExclCurrentPallet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductionExclCurrentPallet.Location = new System.Drawing.Point(592, 370);
            this.lblProductionExclCurrentPallet.Name = "lblProductionExclCurrentPallet";
            this.lblProductionExclCurrentPallet.Size = new System.Drawing.Size(286, 14);
            this.lblProductionExclCurrentPallet.TabIndex = 100;
            this.lblProductionExclCurrentPallet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rtbCreatedWIPInfo
            // 
            this.rtbCreatedWIPInfo.BackColor = System.Drawing.SystemColors.Control;
            this.rtbCreatedWIPInfo.ForeColor = System.Drawing.Color.Blue;
            this.rtbCreatedWIPInfo.Location = new System.Drawing.Point(592, 387);
            this.rtbCreatedWIPInfo.Name = "rtbCreatedWIPInfo";
            this.rtbCreatedWIPInfo.ReadOnly = true;
            this.rtbCreatedWIPInfo.Size = new System.Drawing.Size(286, 90);
            this.rtbCreatedWIPInfo.TabIndex = 101;
            this.rtbCreatedWIPInfo.TabStop = false;
            this.rtbCreatedWIPInfo.Text = "";
            // 
            // cmdJobToDateStats
            // 
            this.cmdJobToDateStats.Location = new System.Drawing.Point(632, 764);
            this.cmdJobToDateStats.Name = "cmdJobToDateStats";
            this.cmdJobToDateStats.Size = new System.Drawing.Size(220, 27);
            this.cmdJobToDateStats.TabIndex = 111;
            this.cmdJobToDateStats.Text = "Job-to-Date Stats";
            this.cmdJobToDateStats.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdJobToDateStats.UseVisualStyleBackColor = true;
            this.cmdJobToDateStats.Click += new System.EventHandler(this.cmdJobToDateStats_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 113;
            this.label1.Text = "Special Instructions";
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
            this.rtbJobTitle.TabIndex = 110;
            this.rtbJobTitle.TabStop = false;
            this.rtbJobTitle.Text = "";
            // 
            // lblTotalScrapPounds
            // 
            this.lblTotalScrapPounds.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalScrapPounds.Location = new System.Drawing.Point(329, 515);
            this.lblTotalScrapPounds.Name = "lblTotalScrapPounds";
            this.lblTotalScrapPounds.Size = new System.Drawing.Size(123, 14);
            this.lblTotalScrapPounds.TabIndex = 88;
            this.lblTotalScrapPounds.Text = "Total Scrap Pounds:";
            this.lblTotalScrapPounds.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTotalScrapPounds
            // 
            this.txtTotalScrapPounds.Enabled = false;
            this.txtTotalScrapPounds.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalScrapPounds.ForeColor = System.Drawing.Color.Black;
            this.txtTotalScrapPounds.Location = new System.Drawing.Point(464, 513);
            this.txtTotalScrapPounds.Name = "txtTotalScrapPounds";
            this.txtTotalScrapPounds.Size = new System.Drawing.Size(70, 20);
            this.txtTotalScrapPounds.TabIndex = 87;
            this.txtTotalScrapPounds.Text = "0";
            this.txtTotalScrapPounds.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmdSaveAndClose
            // 
            this.cmdSaveAndClose.Location = new System.Drawing.Point(205, 1);
            this.cmdSaveAndClose.Name = "cmdSaveAndClose";
            this.cmdSaveAndClose.Size = new System.Drawing.Size(60, 50);
            this.cmdSaveAndClose.TabIndex = 3;
            this.cmdSaveAndClose.Text = "Save Progress";
            this.cmdSaveAndClose.UseVisualStyleBackColor = true;
            this.cmdSaveAndClose.Click += new System.EventHandler(this.cmdSaveAndClose_Click);
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
            // 
            // pnlRemoveScrapRecord
            // 
            this.pnlRemoveScrapRecord.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlRemoveScrapRecord.Controls.Add(this.cmdRemoveScrapRecord);
            this.pnlRemoveScrapRecord.Controls.Add(this.cboRemoveScrapRecord);
            this.pnlRemoveScrapRecord.Controls.Add(this.lblRemoveScrapInfo);
            this.pnlRemoveScrapRecord.Location = new System.Drawing.Point(286, 536);
            this.pnlRemoveScrapRecord.Name = "pnlRemoveScrapRecord";
            this.pnlRemoveScrapRecord.Size = new System.Drawing.Size(300, 37);
            this.pnlRemoveScrapRecord.TabIndex = 89;
            this.pnlRemoveScrapRecord.Visible = false;
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
            this.cmdRemoveScrapRecord.Click += new System.EventHandler(this.cmdRemoveScrapRecord_Click);
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
            this.rtbScrapRecords.Location = new System.Drawing.Point(286, 387);
            this.rtbScrapRecords.Name = "rtbScrapRecords";
            this.rtbScrapRecords.ReadOnly = true;
            this.rtbScrapRecords.Size = new System.Drawing.Size(300, 90);
            this.rtbScrapRecords.TabIndex = 83;
            this.rtbScrapRecords.TabStop = false;
            this.rtbScrapRecords.Text = "";
            // 
            // txtNewScrapPounds
            // 
            this.txtNewScrapPounds.Location = new System.Drawing.Point(286, 491);
            this.txtNewScrapPounds.Name = "txtNewScrapPounds";
            this.txtNewScrapPounds.Size = new System.Drawing.Size(52, 20);
            this.txtNewScrapPounds.TabIndex = 84;
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
            this.cboScrapReasons.Location = new System.Drawing.Point(351, 490);
            this.cboScrapReasons.Name = "cboScrapReasons";
            this.cboScrapReasons.Size = new System.Drawing.Size(135, 21);
            this.cboScrapReasons.TabIndex = 85;
            this.cboScrapReasons.SelectedIndexChanged += new System.EventHandler(this.cboScrapReasons_SelectedIndexChanged);
            // 
            // cmdShowFilmUsage
            // 
            this.cmdShowFilmUsage.Location = new System.Drawing.Point(494, 764);
            this.cmdShowFilmUsage.Name = "cmdShowFilmUsage";
            this.cmdShowFilmUsage.Size = new System.Drawing.Size(100, 27);
            this.cmdShowFilmUsage.TabIndex = 98;
            this.cmdShowFilmUsage.Text = "Show Film Usage";
            this.cmdShowFilmUsage.UseVisualStyleBackColor = true;
            this.cmdShowFilmUsage.Click += new System.EventHandler(this.cmdShowFilmUsage_Click);
            // 
            // cmdAddScrapRecord
            // 
            this.cmdAddScrapRecord.Enabled = false;
            this.cmdAddScrapRecord.Location = new System.Drawing.Point(494, 485);
            this.cmdAddScrapRecord.Name = "cmdAddScrapRecord";
            this.cmdAddScrapRecord.Size = new System.Drawing.Size(92, 27);
            this.cmdAddScrapRecord.TabIndex = 86;
            this.cmdAddScrapRecord.Text = "Add Scrap Lbs";
            this.cmdAddScrapRecord.UseVisualStyleBackColor = true;
            this.cmdAddScrapRecord.Click += new System.EventHandler(this.cmdAddScrapRecord_Click);
            // 
            // cmdReprintLabel
            // 
            this.cmdReprintLabel.Location = new System.Drawing.Point(692, 682);
            this.cmdReprintLabel.Name = "cmdReprintLabel";
            this.cmdReprintLabel.Size = new System.Drawing.Size(85, 27);
            this.cmdReprintLabel.TabIndex = 102;
            this.cmdReprintLabel.Text = "Reprint Label";
            this.cmdReprintLabel.UseVisualStyleBackColor = true;
            this.cmdReprintLabel.Click += new System.EventHandler(this.cmdReprintLabel_Click);
            // 
            // cmdMoveRoll
            // 
            this.cmdMoveRoll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdMoveRoll.Location = new System.Drawing.Point(792, 682);
            this.cmdMoveRoll.Name = "cmdMoveRoll";
            this.cmdMoveRoll.Size = new System.Drawing.Size(85, 27);
            this.cmdMoveRoll.TabIndex = 103;
            this.cmdMoveRoll.Text = "&Move Roll";
            this.cmdMoveRoll.UseVisualStyleBackColor = true;
            this.cmdMoveRoll.Click += new System.EventHandler(this.cmdMoveRoll_Click);
            // 
            // cmdClosePallet
            // 
            this.cmdClosePallet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClosePallet.Location = new System.Drawing.Point(752, 726);
            this.cmdClosePallet.Name = "cmdClosePallet";
            this.cmdClosePallet.Size = new System.Drawing.Size(100, 27);
            this.cmdClosePallet.TabIndex = 105;
            this.cmdClosePallet.Text = "Close Pallet";
            this.cmdClosePallet.UseVisualStyleBackColor = true;
            this.cmdClosePallet.Click += new System.EventHandler(this.cmdClosePallet_Click);
            // 
            // txtCreatedFeet
            // 
            this.txtCreatedFeet.Enabled = false;
            this.txtCreatedFeet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCreatedFeet.ForeColor = System.Drawing.Color.Black;
            this.txtCreatedFeet.Location = new System.Drawing.Point(444, 733);
            this.txtCreatedFeet.Name = "txtCreatedFeet";
            this.txtCreatedFeet.Size = new System.Drawing.Size(90, 20);
            this.txtCreatedFeet.TabIndex = 95;
            this.txtCreatedFeet.Text = "0";
            this.txtCreatedFeet.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cmdEndofShift
            // 
            this.cmdEndofShift.Location = new System.Drawing.Point(138, 1);
            this.cmdEndofShift.Name = "cmdEndofShift";
            this.cmdEndofShift.Size = new System.Drawing.Size(60, 50);
            this.cmdEndofShift.TabIndex = 2;
            this.cmdEndofShift.Text = "End of Shift";
            this.cmdEndofShift.UseVisualStyleBackColor = true;
            this.cmdEndofShift.Click += new System.EventHandler(this.cmdEndofShift_Click);
            // 
            // cmdJobHistory
            // 
            this.cmdJobHistory.Location = new System.Drawing.Point(94, 485);
            this.cmdJobHistory.Name = "cmdJobHistory";
            this.cmdJobHistory.Size = new System.Drawing.Size(100, 27);
            this.cmdJobHistory.TabIndex = 78;
            this.cmdJobHistory.Text = "Show Job History";
            this.cmdJobHistory.UseVisualStyleBackColor = true;
            this.cmdJobHistory.Click += new System.EventHandler(this.cmdJobHistory_Click);
            // 
            // lblCreatedLF
            // 
            this.lblCreatedLF.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreatedLF.Location = new System.Drawing.Point(444, 710);
            this.lblCreatedLF.Name = "lblCreatedLF";
            this.lblCreatedLF.Size = new System.Drawing.Size(90, 20);
            this.lblCreatedLF.TabIndex = 94;
            this.lblCreatedLF.Text = "Created LF";
            this.lblCreatedLF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtConsumedFeet
            // 
            this.txtConsumedFeet.Enabled = false;
            this.txtConsumedFeet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConsumedFeet.ForeColor = System.Drawing.Color.Black;
            this.txtConsumedFeet.Location = new System.Drawing.Point(337, 735);
            this.txtConsumedFeet.Name = "txtConsumedFeet";
            this.txtConsumedFeet.Size = new System.Drawing.Size(90, 20);
            this.txtConsumedFeet.TabIndex = 92;
            this.txtConsumedFeet.Text = "0";
            this.txtConsumedFeet.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblSaveHours
            // 
            this.lblSaveHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaveHours.Location = new System.Drawing.Point(7, 706);
            this.lblSaveHours.Name = "lblSaveHours";
            this.lblSaveHours.Size = new System.Drawing.Size(275, 28);
            this.lblSaveHours.TabIndex = 106;
            this.lblSaveHours.Text = "Save Job Hours";
            this.lblSaveHours.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlSaveHours
            // 
            this.pnlSaveHours.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSaveHours.Controls.Add(this.cmdSaveAndClose);
            this.pnlSaveHours.Controls.Add(this.cmdEndofShift);
            this.pnlSaveHours.Controls.Add(this.cmdJobPulled);
            this.pnlSaveHours.Controls.Add(this.cmdJobComplete);
            this.pnlSaveHours.Location = new System.Drawing.Point(7, 735);
            this.pnlSaveHours.Name = "pnlSaveHours";
            this.pnlSaveHours.Size = new System.Drawing.Size(274, 56);
            this.pnlSaveHours.TabIndex = 81;
            // 
            // cmdJobPulled
            // 
            this.cmdJobPulled.Location = new System.Drawing.Point(71, 1);
            this.cmdJobPulled.Name = "cmdJobPulled";
            this.cmdJobPulled.Size = new System.Drawing.Size(60, 50);
            this.cmdJobPulled.TabIndex = 1;
            this.cmdJobPulled.Text = "Job Pulled";
            this.cmdJobPulled.UseVisualStyleBackColor = true;
            this.cmdJobPulled.Click += new System.EventHandler(this.cmdJobPulled_Click);
            // 
            // cmdJobComplete
            // 
            this.cmdJobComplete.Location = new System.Drawing.Point(4, 1);
            this.cmdJobComplete.Name = "cmdJobComplete";
            this.cmdJobComplete.Size = new System.Drawing.Size(60, 50);
            this.cmdJobComplete.TabIndex = 0;
            this.cmdJobComplete.Text = "Job Complete";
            this.cmdJobComplete.UseVisualStyleBackColor = true;
            this.cmdJobComplete.Click += new System.EventHandler(this.cmdJobComplete_Click);
            // 
            // pnlRemoveDownTimeRecord
            // 
            this.pnlRemoveDownTimeRecord.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlRemoveDownTimeRecord.Controls.Add(this.cmdRemoveDownTimeRecord);
            this.pnlRemoveDownTimeRecord.Controls.Add(this.cboRemoveDowntimeRecord);
            this.pnlRemoveDownTimeRecord.Controls.Add(this.lblRemoveDTInfo);
            this.pnlRemoveDownTimeRecord.Location = new System.Drawing.Point(7, 674);
            this.pnlRemoveDownTimeRecord.Name = "pnlRemoveDownTimeRecord";
            this.pnlRemoveDownTimeRecord.Size = new System.Drawing.Size(274, 37);
            this.pnlRemoveDownTimeRecord.TabIndex = 80;
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
            this.cmdRemoveDownTimeRecord.Click += new System.EventHandler(this.cmdRemoveDownTimeRecord_Click);
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
            this.cboRemoveDowntimeRecord.SelectedIndexChanged += new System.EventHandler(this.cboRemoveDowntimeRecord_SelectedIndexChanged);
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
            // rtbDownTimeRecords
            // 
            this.rtbDownTimeRecords.BackColor = System.Drawing.SystemColors.Control;
            this.rtbDownTimeRecords.ForeColor = System.Drawing.Color.Blue;
            this.rtbDownTimeRecords.Location = new System.Drawing.Point(7, 536);
            this.rtbDownTimeRecords.Name = "rtbDownTimeRecords";
            this.rtbDownTimeRecords.ReadOnly = true;
            this.rtbDownTimeRecords.Size = new System.Drawing.Size(274, 134);
            this.rtbDownTimeRecords.TabIndex = 79;
            this.rtbDownTimeRecords.TabStop = false;
            this.rtbDownTimeRecords.Text = "";
            // 
            // InnolokProductionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 809);
            this.ControlBox = false;
            this.Controls.Add(this.rtbSpecialInstructions);
            this.Controls.Add(this.lblJobHours);
            this.Controls.Add(this.lblConsumedLF);
            this.Controls.Add(this.rtbInputFilm);
            this.Controls.Add(this.cmdCreateRoll);
            this.Controls.Add(this.cmdReturnRoll);
            this.Controls.Add(this.cmdConsumeRoll);
            this.Controls.Add(this.pnlHours);
            this.Controls.Add(this.dgvJobs);
            this.Controls.Add(this.rtbCurrentPalletProduction);
            this.Controls.Add(this.cmdGetPallet);
            this.Controls.Add(this.rtbLooseRolls);
            this.Controls.Add(this.lblLooseRolls);
            this.Controls.Add(this.lblScrapDetail);
            this.Controls.Add(this.lblDowntimeDetail);
            this.Controls.Add(this.lblCurrentPalletProduction);
            this.Controls.Add(this.lblProductionExclCurrentPallet);
            this.Controls.Add(this.rtbCreatedWIPInfo);
            this.Controls.Add(this.cmdJobToDateStats);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtbJobTitle);
            this.Controls.Add(this.lblTotalScrapPounds);
            this.Controls.Add(this.txtTotalScrapPounds);
            this.Controls.Add(this.pnlRemoveScrapRecord);
            this.Controls.Add(this.rtbScrapRecords);
            this.Controls.Add(this.txtNewScrapPounds);
            this.Controls.Add(this.cboScrapReasons);
            this.Controls.Add(this.cmdShowFilmUsage);
            this.Controls.Add(this.cmdAddScrapRecord);
            this.Controls.Add(this.cmdReprintLabel);
            this.Controls.Add(this.cmdMoveRoll);
            this.Controls.Add(this.cmdClosePallet);
            this.Controls.Add(this.txtCreatedFeet);
            this.Controls.Add(this.cmdJobHistory);
            this.Controls.Add(this.lblCreatedLF);
            this.Controls.Add(this.txtConsumedFeet);
            this.Controls.Add(this.lblSaveHours);
            this.Controls.Add(this.pnlSaveHours);
            this.Controls.Add(this.pnlRemoveDownTimeRecord);
            this.Controls.Add(this.rtbDownTimeRecords);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InnolokProductionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Shown += new System.EventHandler(this.InnolokProductionFormShown);
            this.pnlHours.ResumeLayout(false);
            this.pnlHours.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJobs)).EndInit();
            this.pnlRemoveScrapRecord.ResumeLayout(false);
            this.pnlSaveHours.ResumeLayout(false);
            this.pnlRemoveDownTimeRecord.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDownTimeHours;
        private System.Windows.Forms.RichTextBox rtbSpecialInstructions;
        private System.Windows.Forms.Label lblJobHours;
        private System.Windows.Forms.TextBox txtTotalHours;
        private System.Windows.Forms.Label lblConsumedLF;
        private System.Windows.Forms.TextBox txtNewDownTimeHours;
        private System.Windows.Forms.RichTextBox rtbInputFilm;
        private System.Windows.Forms.Button cmdCreateRoll;
        private System.Windows.Forms.Button cmdReturnRoll;
        private System.Windows.Forms.Button cmdConsumeRoll;
        private System.Windows.Forms.Panel pnlHours;
        private System.Windows.Forms.TextBox txtRunHours;
        private System.Windows.Forms.TextBox txtSetupHours;
        private System.Windows.Forms.ComboBox cboDownTimeReasons;
        private System.Windows.Forms.Button cmdAddDowntimeRecord;
        private System.Windows.Forms.Label lblTotHrs;
        private System.Windows.Forms.Label lblDTHrs;
        private System.Windows.Forms.Label lblRunHrs;
        private System.Windows.Forms.Label lblSetupHs;
        private System.Windows.Forms.DataGridView dgvJobs;
        private System.Windows.Forms.DataGridViewTextBoxColumn JobNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn JobDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoStreams;
        private System.Windows.Forms.DataGridViewTextBoxColumn MasterItemNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentPalletNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn PalletWeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn StreamWidth;
        private System.Windows.Forms.RichTextBox rtbCurrentPalletProduction;
        private System.Windows.Forms.Button cmdGetPallet;
        private System.Windows.Forms.RichTextBox rtbLooseRolls;
        private System.Windows.Forms.Label lblLooseRolls;
        private System.Windows.Forms.Label lblScrapDetail;
        private System.Windows.Forms.Label lblDowntimeDetail;
        private System.Windows.Forms.Label lblCurrentPalletProduction;
        private System.Windows.Forms.Label lblProductionExclCurrentPallet;
        private System.Windows.Forms.RichTextBox rtbCreatedWIPInfo;
        private System.Windows.Forms.Timer endOfShiftTimer;
        private System.Windows.Forms.Button cmdJobToDateStats;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtbJobTitle;
        private System.Windows.Forms.Label lblTotalScrapPounds;
        private System.Windows.Forms.TextBox txtTotalScrapPounds;
        private System.Windows.Forms.Button cmdSaveAndClose;
        private System.Windows.Forms.ComboBox cboRemoveScrapRecord;
        private System.Windows.Forms.Panel pnlRemoveScrapRecord;
        private System.Windows.Forms.Button cmdRemoveScrapRecord;
        private System.Windows.Forms.Label lblRemoveScrapInfo;
        private System.Windows.Forms.RichTextBox rtbScrapRecords;
        private System.Windows.Forms.TextBox txtNewScrapPounds;
        private System.Windows.Forms.ComboBox cboScrapReasons;
        private System.Windows.Forms.Button cmdShowFilmUsage;
        private System.Windows.Forms.Button cmdAddScrapRecord;
        private System.Windows.Forms.Button cmdReprintLabel;
        private System.Windows.Forms.Button cmdMoveRoll;
        private System.Windows.Forms.Button cmdClosePallet;
        private System.Windows.Forms.TextBox txtCreatedFeet;
        private System.Windows.Forms.Button cmdEndofShift;
        private System.Windows.Forms.Button cmdJobHistory;
        private System.Windows.Forms.Label lblCreatedLF;
        private System.Windows.Forms.TextBox txtConsumedFeet;
        private System.Windows.Forms.Label lblSaveHours;
        private System.Windows.Forms.Panel pnlSaveHours;
        private System.Windows.Forms.Button cmdJobPulled;
        private System.Windows.Forms.Button cmdJobComplete;
        private System.Windows.Forms.Panel pnlRemoveDownTimeRecord;
        private System.Windows.Forms.Button cmdRemoveDownTimeRecord;
        private System.Windows.Forms.ComboBox cboRemoveDowntimeRecord;
        private System.Windows.Forms.Label lblRemoveDTInfo;
        private System.Windows.Forms.RichTextBox rtbDownTimeRecords;
    }
}