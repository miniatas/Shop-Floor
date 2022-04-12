/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 9/17/2013
 * Time: 3:30 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ShopFloor
{
	partial class productionEditForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.cmdJobToDateStats = new System.Windows.Forms.Button();
            this.cboShift = new System.Windows.Forms.ComboBox();
            this.cmdCreateNewDTProductionRecord = new System.Windows.Forms.Button();
            this.cboMachineFilter = new System.Windows.Forms.ComboBox();
            this.lblMachineFilter = new System.Windows.Forms.Label();
            this.lblShift = new System.Windows.Forms.Label();
            this.dtpProductionDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.cboEditIncrement = new System.Windows.Forms.DataGridView();
            this.pnlDownTimeInfo = new System.Windows.Forms.Panel();
            this.rtbNewJobDTRecordNotes = new System.Windows.Forms.RichTextBox();
            this.lblNewDTNotes = new System.Windows.Forms.Label();
            this.cmdDone = new System.Windows.Forms.Button();
            this.lblAddDownTime = new System.Windows.Forms.Label();
            this.txtNewDownTimeHours = new System.Windows.Forms.TextBox();
            this.cboDowntimeReasons = new System.Windows.Forms.ComboBox();
            this.cmdAddDowntimeRecord = new System.Windows.Forms.Button();
            this.lblDownTimeDetail = new System.Windows.Forms.Label();
            this.dgvDownTimeDetail = new System.Windows.Forms.DataGridView();
            this.RecordID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Notes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Hours = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdSaveNewDTProductionRecord = new System.Windows.Forms.Button();
            this.cmdAbortNewDTProductionRecord = new System.Windows.Forms.Button();
            this.lblDTProductionRecordNots = new System.Windows.Forms.Label();
            this.rtbDTReasonNotes = new System.Windows.Forms.RichTextBox();
            this.lblDTProductionRecordReason = new System.Windows.Forms.Label();
            this.lblDTProductionRecordHours = new System.Windows.Forms.Label();
            this.txtDTProductionRecordHours = new System.Windows.Forms.TextBox();
            this.cboDTProductionRecordReason = new System.Windows.Forms.ComboBox();
            this.lblAddDTProductionRecord = new System.Windows.Forms.Label();
            this.cboDTRecordMachine = new System.Windows.Forms.ComboBox();
            this.lblDTRecordMachine = new System.Windows.Forms.Label();
            this.pnlAddNewDTProductionRecord = new System.Windows.Forms.Panel();
            this.pnlHoursByLine = new System.Windows.Forms.Panel();
            this.dgvHoursByLine = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblHoursByLine = new System.Windows.Forms.Label();
            this.cboEditMethod = new System.Windows.Forms.ComboBox();
            this.lblEditMethod = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboEditIncrement)).BeginInit();
            this.pnlDownTimeInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDownTimeDetail)).BeginInit();
            this.pnlAddNewDTProductionRecord.SuspendLayout();
            this.pnlHoursByLine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoursByLine)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlTop.Controls.Add(this.lblEditMethod);
            this.pnlTop.Controls.Add(this.cboEditMethod);
            this.pnlTop.Controls.Add(this.cmdJobToDateStats);
            this.pnlTop.Controls.Add(this.cboShift);
            this.pnlTop.Controls.Add(this.cmdCreateNewDTProductionRecord);
            this.pnlTop.Controls.Add(this.cboMachineFilter);
            this.pnlTop.Controls.Add(this.lblMachineFilter);
            this.pnlTop.Controls.Add(this.lblShift);
            this.pnlTop.Controls.Add(this.dtpProductionDate);
            this.pnlTop.Controls.Add(this.lblDate);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1644, 45);
            this.pnlTop.TabIndex = 0;
            // 
            // cmdJobToDateStats
            // 
            this.cmdJobToDateStats.Location = new System.Drawing.Point(1469, 10);
            this.cmdJobToDateStats.Name = "cmdJobToDateStats";
            this.cmdJobToDateStats.Size = new System.Drawing.Size(133, 23);
            this.cmdJobToDateStats.TabIndex = 8;
            this.cmdJobToDateStats.Text = "Job-to-Date Stats";
            this.cmdJobToDateStats.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdJobToDateStats.UseVisualStyleBackColor = true;
            this.cmdJobToDateStats.Click += new System.EventHandler(this.cmdJobToDateStats_Click);
            // 
            // cboShift
            // 
            this.cboShift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboShift.FormattingEnabled = true;
            this.cboShift.Items.AddRange(new object[] {
            "Day",
            "Night"});
            this.cboShift.Location = new System.Drawing.Point(415, 7);
            this.cboShift.Name = "cboShift";
            this.cboShift.Size = new System.Drawing.Size(66, 21);
            this.cboShift.TabIndex = 5;
            this.cboShift.SelectedIndexChanged += new System.EventHandler(this.CboShiftSelectedIndexChanged);
            // 
            // cmdCreateNewDTProductionRecord
            // 
            this.cmdCreateNewDTProductionRecord.Location = new System.Drawing.Point(649, 8);
            this.cmdCreateNewDTProductionRecord.Name = "cmdCreateNewDTProductionRecord";
            this.cmdCreateNewDTProductionRecord.Size = new System.Drawing.Size(188, 23);
            this.cmdCreateNewDTProductionRecord.TabIndex = 6;
            this.cmdCreateNewDTProductionRecord.Text = "Create/Edit Downtime Records";
            this.cmdCreateNewDTProductionRecord.UseVisualStyleBackColor = true;
            this.cmdCreateNewDTProductionRecord.Click += new System.EventHandler(this.CmdCreateNewDTProductionRecordClick);
            // 
            // cboMachineFilter
            // 
            this.cboMachineFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMachineFilter.FormattingEnabled = true;
            this.cboMachineFilter.Items.AddRange(new object[] {
            "All"});
            this.cboMachineFilter.Location = new System.Drawing.Point(287, 7);
            this.cboMachineFilter.Name = "cboMachineFilter";
            this.cboMachineFilter.Size = new System.Drawing.Size(76, 21);
            this.cboMachineFilter.TabIndex = 3;
            this.cboMachineFilter.SelectedIndexChanged += new System.EventHandler(this.CboMachineFilterSelectedIndexChanged);
            // 
            // lblMachineFilter
            // 
            this.lblMachineFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMachineFilter.Location = new System.Drawing.Point(228, 7);
            this.lblMachineFilter.Name = "lblMachineFilter";
            this.lblMachineFilter.Size = new System.Drawing.Size(64, 23);
            this.lblMachineFilter.TabIndex = 2;
            this.lblMachineFilter.Text = "Machine:";
            // 
            // lblShift
            // 
            this.lblShift.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShift.Location = new System.Drawing.Point(380, 7);
            this.lblShift.Name = "lblShift";
            this.lblShift.Size = new System.Drawing.Size(42, 23);
            this.lblShift.TabIndex = 4;
            this.lblShift.Text = "Shift:";
            // 
            // dtpProductionDate
            // 
            this.dtpProductionDate.CustomFormat = "MMMM d, yyyy";
            this.dtpProductionDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpProductionDate.Location = new System.Drawing.Point(49, 7);
            this.dtpProductionDate.Name = "dtpProductionDate";
            this.dtpProductionDate.Size = new System.Drawing.Size(162, 20);
            this.dtpProductionDate.TabIndex = 1;
            // 
            // lblDate
            // 
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(10, 7);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(96, 23);
            this.lblDate.TabIndex = 0;
            this.lblDate.Text = "Date:";
            // 
            // cboEditIncrement
            // 
            this.cboEditIncrement.AllowUserToAddRows = false;
            this.cboEditIncrement.AllowUserToDeleteRows = false;
            this.cboEditIncrement.AllowUserToOrderColumns = true;
            this.cboEditIncrement.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.cboEditIncrement.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.cboEditIncrement.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.cboEditIncrement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.cboEditIncrement.DefaultCellStyle = dataGridViewCellStyle2;
            this.cboEditIncrement.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboEditIncrement.Location = new System.Drawing.Point(0, 45);
            this.cboEditIncrement.MultiSelect = false;
            this.cboEditIncrement.Name = "cboEditIncrement";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.cboEditIncrement.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.cboEditIncrement.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.cboEditIncrement.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.cboEditIncrement.Size = new System.Drawing.Size(1644, 504);
            this.cboEditIncrement.TabIndex = 1;
            this.cboEditIncrement.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvProductionSummaryCellEndEdit);
            this.cboEditIncrement.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DgvProductionSummaryEditingControlShowing);
            this.cboEditIncrement.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvProductionSummaryRowEnter);
            this.cboEditIncrement.DoubleClick += new System.EventHandler(this.DgvProductionSummaryDoubleClick);
            // 
            // pnlDownTimeInfo
            // 
            this.pnlDownTimeInfo.Controls.Add(this.rtbNewJobDTRecordNotes);
            this.pnlDownTimeInfo.Controls.Add(this.lblNewDTNotes);
            this.pnlDownTimeInfo.Controls.Add(this.cmdDone);
            this.pnlDownTimeInfo.Controls.Add(this.lblAddDownTime);
            this.pnlDownTimeInfo.Controls.Add(this.txtNewDownTimeHours);
            this.pnlDownTimeInfo.Controls.Add(this.cboDowntimeReasons);
            this.pnlDownTimeInfo.Controls.Add(this.cmdAddDowntimeRecord);
            this.pnlDownTimeInfo.Controls.Add(this.lblDownTimeDetail);
            this.pnlDownTimeInfo.Controls.Add(this.dgvDownTimeDetail);
            this.pnlDownTimeInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlDownTimeInfo.Enabled = false;
            this.pnlDownTimeInfo.Location = new System.Drawing.Point(0, 549);
            this.pnlDownTimeInfo.Name = "pnlDownTimeInfo";
            this.pnlDownTimeInfo.Size = new System.Drawing.Size(742, 248);
            this.pnlDownTimeInfo.TabIndex = 2;
            // 
            // rtbNewJobDTRecordNotes
            // 
            this.rtbNewJobDTRecordNotes.ForeColor = System.Drawing.Color.Blue;
            this.rtbNewJobDTRecordNotes.Location = new System.Drawing.Point(68, 196);
            this.rtbNewJobDTRecordNotes.Name = "rtbNewJobDTRecordNotes";
            this.rtbNewJobDTRecordNotes.Size = new System.Drawing.Size(366, 40);
            this.rtbNewJobDTRecordNotes.TabIndex = 4;
            this.rtbNewJobDTRecordNotes.Text = "";
            // 
            // lblNewDTNotes
            // 
            this.lblNewDTNotes.AutoSize = true;
            this.lblNewDTNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewDTNotes.Location = new System.Drawing.Point(12, 196);
            this.lblNewDTNotes.Name = "lblNewDTNotes";
            this.lblNewDTNotes.Size = new System.Drawing.Size(44, 13);
            this.lblNewDTNotes.TabIndex = 7;
            this.lblNewDTNotes.Text = "Notes:";
            // 
            // cmdDone
            // 
            this.cmdDone.Location = new System.Drawing.Point(569, 202);
            this.cmdDone.Name = "cmdDone";
            this.cmdDone.Size = new System.Drawing.Size(139, 34);
            this.cmdDone.TabIndex = 6;
            this.cmdDone.Text = "Done";
            this.cmdDone.UseVisualStyleBackColor = true;
            this.cmdDone.Click += new System.EventHandler(this.CmdDoneClick);
            // 
            // lblAddDownTime
            // 
            this.lblAddDownTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddDownTime.Location = new System.Drawing.Point(12, 145);
            this.lblAddDownTime.Name = "lblAddDownTime";
            this.lblAddDownTime.Size = new System.Drawing.Size(422, 16);
            this.lblAddDownTime.TabIndex = 1;
            this.lblAddDownTime.Text = "Add Job Down Time Record";
            this.lblAddDownTime.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtNewDownTimeHours
            // 
            this.txtNewDownTimeHours.Location = new System.Drawing.Point(12, 167);
            this.txtNewDownTimeHours.Name = "txtNewDownTimeHours";
            this.txtNewDownTimeHours.Size = new System.Drawing.Size(56, 20);
            this.txtNewDownTimeHours.TabIndex = 2;
            this.txtNewDownTimeHours.Text = "0.00";
            this.txtNewDownTimeHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNewDownTimeHours.Enter += new System.EventHandler(this.TxtEnter);
            this.txtNewDownTimeHours.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtKeyDown);
            this.txtNewDownTimeHours.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtKeyPress);
            this.txtNewDownTimeHours.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtNewDownTimeHoursKeyUp);
            this.txtNewDownTimeHours.Leave += new System.EventHandler(this.TxtHoursLeave);
            // 
            // cboDowntimeReasons
            // 
            this.cboDowntimeReasons.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDowntimeReasons.FormattingEnabled = true;
            this.cboDowntimeReasons.Location = new System.Drawing.Point(68, 167);
            this.cboDowntimeReasons.Name = "cboDowntimeReasons";
            this.cboDowntimeReasons.Size = new System.Drawing.Size(366, 21);
            this.cboDowntimeReasons.TabIndex = 3;
            this.cboDowntimeReasons.SelectedIndexChanged += new System.EventHandler(this.CboDowntimeReasonsSelectedIndexChanged);
            // 
            // cmdAddDowntimeRecord
            // 
            this.cmdAddDowntimeRecord.Enabled = false;
            this.cmdAddDowntimeRecord.Location = new System.Drawing.Point(440, 167);
            this.cmdAddDowntimeRecord.Name = "cmdAddDowntimeRecord";
            this.cmdAddDowntimeRecord.Size = new System.Drawing.Size(67, 69);
            this.cmdAddDowntimeRecord.TabIndex = 5;
            this.cmdAddDowntimeRecord.Text = "Add";
            this.cmdAddDowntimeRecord.UseVisualStyleBackColor = true;
            this.cmdAddDowntimeRecord.Click += new System.EventHandler(this.CmdAddDowntimeRecordClick);
            // 
            // lblDownTimeDetail
            // 
            this.lblDownTimeDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDownTimeDetail.Location = new System.Drawing.Point(12, 0);
            this.lblDownTimeDetail.Name = "lblDownTimeDetail";
            this.lblDownTimeDetail.Size = new System.Drawing.Size(724, 23);
            this.lblDownTimeDetail.TabIndex = 1;
            this.lblDownTimeDetail.Text = "Down Time Detail";
            this.lblDownTimeDetail.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // dgvDownTimeDetail
            // 
            this.dgvDownTimeDetail.AllowUserToAddRows = false;
            this.dgvDownTimeDetail.AllowUserToDeleteRows = false;
            this.dgvDownTimeDetail.AllowUserToResizeColumns = false;
            this.dgvDownTimeDetail.AllowUserToResizeRows = false;
            this.dgvDownTimeDetail.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvDownTimeDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDownTimeDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RecordID,
            this.Description,
            this.Notes,
            this.Hours});
            this.dgvDownTimeDetail.Location = new System.Drawing.Point(12, 24);
            this.dgvDownTimeDetail.MultiSelect = false;
            this.dgvDownTimeDetail.Name = "dgvDownTimeDetail";
            this.dgvDownTimeDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvDownTimeDetail.Size = new System.Drawing.Size(724, 118);
            this.dgvDownTimeDetail.TabIndex = 0;
            this.dgvDownTimeDetail.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDownTimeDetail_CellEndEdit);
            this.dgvDownTimeDetail.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvDownTimeDetail_EditingControlShowing);
            this.dgvDownTimeDetail.DoubleClick += new System.EventHandler(this.DgvDownTimeDetailDoubleClick);
            // 
            // RecordID
            // 
            this.RecordID.HeaderText = "";
            this.RecordID.Name = "RecordID";
            this.RecordID.ReadOnly = true;
            this.RecordID.Visible = false;
            // 
            // Description
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Description.DefaultCellStyle = dataGridViewCellStyle5;
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 250;
            // 
            // Notes
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Notes.DefaultCellStyle = dataGridViewCellStyle6;
            this.Notes.HeaderText = "Notes";
            this.Notes.Name = "Notes";
            this.Notes.Width = 360;
            // 
            // Hours
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Format = "N2";
            dataGridViewCellStyle7.NullValue = null;
            this.Hours.DefaultCellStyle = dataGridViewCellStyle7;
            this.Hours.HeaderText = "Hours";
            this.Hours.Name = "Hours";
            this.Hours.ReadOnly = true;
            this.Hours.Width = 50;
            // 
            // cmdSaveNewDTProductionRecord
            // 
            this.cmdSaveNewDTProductionRecord.Enabled = false;
            this.cmdSaveNewDTProductionRecord.Location = new System.Drawing.Point(649, 101);
            this.cmdSaveNewDTProductionRecord.Name = "cmdSaveNewDTProductionRecord";
            this.cmdSaveNewDTProductionRecord.Size = new System.Drawing.Size(64, 30);
            this.cmdSaveNewDTProductionRecord.TabIndex = 8;
            this.cmdSaveNewDTProductionRecord.Text = "Save";
            this.cmdSaveNewDTProductionRecord.UseVisualStyleBackColor = true;
            this.cmdSaveNewDTProductionRecord.Click += new System.EventHandler(this.CmdSaveNewDTProductionRecordClick);
            // 
            // cmdAbortNewDTProductionRecord
            // 
            this.cmdAbortNewDTProductionRecord.Location = new System.Drawing.Point(649, 145);
            this.cmdAbortNewDTProductionRecord.Name = "cmdAbortNewDTProductionRecord";
            this.cmdAbortNewDTProductionRecord.Size = new System.Drawing.Size(64, 30);
            this.cmdAbortNewDTProductionRecord.TabIndex = 9;
            this.cmdAbortNewDTProductionRecord.Text = "Abort";
            this.cmdAbortNewDTProductionRecord.UseVisualStyleBackColor = true;
            this.cmdAbortNewDTProductionRecord.Click += new System.EventHandler(this.CmdAbortNewDTProductionRecordClick);
            // 
            // lblDTProductionRecordNots
            // 
            this.lblDTProductionRecordNots.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDTProductionRecordNots.Location = new System.Drawing.Point(23, 67);
            this.lblDTProductionRecordNots.Name = "lblDTProductionRecordNots";
            this.lblDTProductionRecordNots.Size = new System.Drawing.Size(50, 19);
            this.lblDTProductionRecordNots.TabIndex = 16;
            this.lblDTProductionRecordNots.Text = "Notes";
            this.lblDTProductionRecordNots.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rtbDTReasonNotes
            // 
            this.rtbDTReasonNotes.Location = new System.Drawing.Point(23, 89);
            this.rtbDTReasonNotes.Name = "rtbDTReasonNotes";
            this.rtbDTReasonNotes.Size = new System.Drawing.Size(620, 97);
            this.rtbDTReasonNotes.TabIndex = 7;
            this.rtbDTReasonNotes.Text = "";
            // 
            // lblDTProductionRecordReason
            // 
            this.lblDTProductionRecordReason.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDTProductionRecordReason.Location = new System.Drawing.Point(148, 24);
            this.lblDTProductionRecordReason.Name = "lblDTProductionRecordReason";
            this.lblDTProductionRecordReason.Size = new System.Drawing.Size(439, 19);
            this.lblDTProductionRecordReason.TabIndex = 3;
            this.lblDTProductionRecordReason.Text = "Reason";
            this.lblDTProductionRecordReason.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDTProductionRecordHours
            // 
            this.lblDTProductionRecordHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDTProductionRecordHours.Location = new System.Drawing.Point(593, 29);
            this.lblDTProductionRecordHours.Name = "lblDTProductionRecordHours";
            this.lblDTProductionRecordHours.Size = new System.Drawing.Size(50, 19);
            this.lblDTProductionRecordHours.TabIndex = 5;
            this.lblDTProductionRecordHours.Text = "Hours";
            this.lblDTProductionRecordHours.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDTProductionRecordHours
            // 
            this.txtDTProductionRecordHours.Location = new System.Drawing.Point(593, 49);
            this.txtDTProductionRecordHours.Name = "txtDTProductionRecordHours";
            this.txtDTProductionRecordHours.Size = new System.Drawing.Size(50, 20);
            this.txtDTProductionRecordHours.TabIndex = 6;
            this.txtDTProductionRecordHours.Text = "0.00";
            this.txtDTProductionRecordHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDTProductionRecordHours.Enter += new System.EventHandler(this.TxtEnter);
            this.txtDTProductionRecordHours.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtKeyDown);
            this.txtDTProductionRecordHours.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtKeyPress);
            this.txtDTProductionRecordHours.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtDTProductionRecordHoursKeyUp);
            this.txtDTProductionRecordHours.Leave += new System.EventHandler(this.TxtHoursLeave);
            // 
            // cboDTProductionRecordReason
            // 
            this.cboDTProductionRecordReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDTProductionRecordReason.FormattingEnabled = true;
            this.cboDTProductionRecordReason.Location = new System.Drawing.Point(148, 46);
            this.cboDTProductionRecordReason.Name = "cboDTProductionRecordReason";
            this.cboDTProductionRecordReason.Size = new System.Drawing.Size(439, 21);
            this.cboDTProductionRecordReason.TabIndex = 4;
            this.cboDTProductionRecordReason.SelectedIndexChanged += new System.EventHandler(this.CboDTProductionRecordReasonSelectedIndexChanged);
            // 
            // lblAddDTProductionRecord
            // 
            this.lblAddDTProductionRecord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAddDTProductionRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddDTProductionRecord.Location = new System.Drawing.Point(3, 4);
            this.lblAddDTProductionRecord.Name = "lblAddDTProductionRecord";
            this.lblAddDTProductionRecord.Size = new System.Drawing.Size(735, 23);
            this.lblAddDTProductionRecord.TabIndex = 0;
            this.lblAddDTProductionRecord.Text = "Add Departmental Downtime Production Record";
            this.lblAddDTProductionRecord.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboDTRecordMachine
            // 
            this.cboDTRecordMachine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDTRecordMachine.FormattingEnabled = true;
            this.cboDTRecordMachine.Location = new System.Drawing.Point(93, 47);
            this.cboDTRecordMachine.Name = "cboDTRecordMachine";
            this.cboDTRecordMachine.Size = new System.Drawing.Size(49, 21);
            this.cboDTRecordMachine.TabIndex = 2;
            this.cboDTRecordMachine.SelectedIndexChanged += new System.EventHandler(this.cboDTRecordMachine_SelectedIndexChanged);
            // 
            // lblDTRecordMachine
            // 
            this.lblDTRecordMachine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDTRecordMachine.Location = new System.Drawing.Point(23, 44);
            this.lblDTRecordMachine.Name = "lblDTRecordMachine";
            this.lblDTRecordMachine.Size = new System.Drawing.Size(64, 23);
            this.lblDTRecordMachine.TabIndex = 1;
            this.lblDTRecordMachine.Text = "Machine:";
            this.lblDTRecordMachine.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlAddNewDTProductionRecord
            // 
            this.pnlAddNewDTProductionRecord.Controls.Add(this.cmdSaveNewDTProductionRecord);
            this.pnlAddNewDTProductionRecord.Controls.Add(this.lblAddDTProductionRecord);
            this.pnlAddNewDTProductionRecord.Controls.Add(this.cmdAbortNewDTProductionRecord);
            this.pnlAddNewDTProductionRecord.Controls.Add(this.lblDTRecordMachine);
            this.pnlAddNewDTProductionRecord.Controls.Add(this.lblDTProductionRecordNots);
            this.pnlAddNewDTProductionRecord.Controls.Add(this.cboDTRecordMachine);
            this.pnlAddNewDTProductionRecord.Controls.Add(this.rtbDTReasonNotes);
            this.pnlAddNewDTProductionRecord.Controls.Add(this.cboDTProductionRecordReason);
            this.pnlAddNewDTProductionRecord.Controls.Add(this.lblDTProductionRecordReason);
            this.pnlAddNewDTProductionRecord.Controls.Add(this.txtDTProductionRecordHours);
            this.pnlAddNewDTProductionRecord.Controls.Add(this.lblDTProductionRecordHours);
            this.pnlAddNewDTProductionRecord.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlAddNewDTProductionRecord.Enabled = false;
            this.pnlAddNewDTProductionRecord.Location = new System.Drawing.Point(906, 549);
            this.pnlAddNewDTProductionRecord.Name = "pnlAddNewDTProductionRecord";
            this.pnlAddNewDTProductionRecord.Size = new System.Drawing.Size(738, 248);
            this.pnlAddNewDTProductionRecord.TabIndex = 4;
            // 
            // pnlHoursByLine
            // 
            this.pnlHoursByLine.Controls.Add(this.dgvHoursByLine);
            this.pnlHoursByLine.Controls.Add(this.lblHoursByLine);
            this.pnlHoursByLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHoursByLine.Location = new System.Drawing.Point(742, 549);
            this.pnlHoursByLine.Name = "pnlHoursByLine";
            this.pnlHoursByLine.Size = new System.Drawing.Size(164, 248);
            this.pnlHoursByLine.TabIndex = 3;
            // 
            // dgvHoursByLine
            // 
            this.dgvHoursByLine.AllowUserToAddRows = false;
            this.dgvHoursByLine.AllowUserToDeleteRows = false;
            this.dgvHoursByLine.AllowUserToResizeColumns = false;
            this.dgvHoursByLine.AllowUserToResizeRows = false;
            this.dgvHoursByLine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHoursByLine.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dgvHoursByLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvHoursByLine.Location = new System.Drawing.Point(0, 29);
            this.dgvHoursByLine.MultiSelect = false;
            this.dgvHoursByLine.Name = "dgvHoursByLine";
            this.dgvHoursByLine.ReadOnly = true;
            this.dgvHoursByLine.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHoursByLine.Size = new System.Drawing.Size(164, 219);
            this.dgvHoursByLine.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn2.HeaderText = "Line";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 55;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.Format = "N2";
            dataGridViewCellStyle9.NullValue = null;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn3.HeaderText = "Hours";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 55;
            // 
            // lblHoursByLine
            // 
            this.lblHoursByLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHoursByLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHoursByLine.Location = new System.Drawing.Point(0, 0);
            this.lblHoursByLine.Name = "lblHoursByLine";
            this.lblHoursByLine.Size = new System.Drawing.Size(164, 23);
            this.lblHoursByLine.TabIndex = 2;
            this.lblHoursByLine.Text = "Hours by Line";
            this.lblHoursByLine.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboEditMethod
            // 
            this.cboEditMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEditMethod.FormattingEnabled = true;
            this.cboEditMethod.Items.AddRange(new object[] {
            "1/4 Hours",
            "100ths of a Hour"});
            this.cboEditMethod.Location = new System.Drawing.Point(1014, 7);
            this.cboEditMethod.Name = "cboEditMethod";
            this.cboEditMethod.Size = new System.Drawing.Size(95, 21);
            this.cboEditMethod.TabIndex = 7;
            // 
            // lblEditMethod
            // 
            this.lblEditMethod.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditMethod.Location = new System.Drawing.Point(927, 8);
            this.lblEditMethod.Name = "lblEditMethod";
            this.lblEditMethod.Size = new System.Drawing.Size(81, 23);
            this.lblEditMethod.TabIndex = 9;
            this.lblEditMethod.Text = "Edit Method:";
            // 
            // productionEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1644, 797);
            this.Controls.Add(this.pnlHoursByLine);
            this.Controls.Add(this.pnlAddNewDTProductionRecord);
            this.Controls.Add(this.pnlDownTimeInfo);
            this.Controls.Add(this.cboEditIncrement);
            this.Controls.Add(this.pnlTop);
            this.HelpButton = true;
            this.Name = "productionEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Production Editing Form";
            this.Load += new System.EventHandler(this.ProductionEditFormLoad);
            this.pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboEditIncrement)).EndInit();
            this.pnlDownTimeInfo.ResumeLayout(false);
            this.pnlDownTimeInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDownTimeDetail)).EndInit();
            this.pnlAddNewDTProductionRecord.ResumeLayout(false);
            this.pnlAddNewDTProductionRecord.PerformLayout();
            this.pnlHoursByLine.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoursByLine)).EndInit();
            this.ResumeLayout(false);

		}
		
		private System.Windows.Forms.Panel pnlTop;
		private System.Windows.Forms.Label lblDTRecordMachine;
		private System.Windows.Forms.ComboBox cboDTRecordMachine;
		private System.Windows.Forms.Label lblAddDTProductionRecord;
		private System.Windows.Forms.ComboBox cboDTProductionRecordReason;
		private System.Windows.Forms.TextBox txtDTProductionRecordHours;
		private System.Windows.Forms.Label lblDTProductionRecordHours;
		private System.Windows.Forms.Label lblDTProductionRecordReason;
		private System.Windows.Forms.RichTextBox rtbDTReasonNotes;
		private System.Windows.Forms.Label lblDTProductionRecordNots;
		private System.Windows.Forms.Button cmdAbortNewDTProductionRecord;
		private System.Windows.Forms.Button cmdSaveNewDTProductionRecord;
		private System.Windows.Forms.Panel pnlAddNewDTProductionRecord;
		private System.Windows.Forms.Button cmdCreateNewDTProductionRecord;
		private System.Windows.Forms.Label lblShift;
		private System.Windows.Forms.Label lblMachineFilter;
		private System.Windows.Forms.ComboBox cboMachineFilter;
		private System.Windows.Forms.Label lblDate;
		private System.Windows.Forms.Label lblAddDownTime;
		private System.Windows.Forms.Button cmdDone;
		private System.Windows.Forms.Button cmdAddDowntimeRecord;
		private System.Windows.Forms.ComboBox cboDowntimeReasons;
		private System.Windows.Forms.TextBox txtNewDownTimeHours;
		private System.Windows.Forms.DataGridView dgvDownTimeDetail;
		private System.Windows.Forms.Label lblDownTimeDetail;
		private System.Windows.Forms.Panel pnlDownTimeInfo;
		private System.Windows.Forms.DataGridView cboEditIncrement;
		private System.Windows.Forms.ComboBox cboShift;
		private System.Windows.Forms.DateTimePicker dtpProductionDate;
		private System.Windows.Forms.Panel pnlHoursByLine;
		private System.Windows.Forms.Label lblHoursByLine;
		private System.Windows.Forms.DataGridView dgvHoursByLine;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.Button cmdJobToDateStats;
        private System.Windows.Forms.RichTextBox rtbNewJobDTRecordNotes;
        private System.Windows.Forms.Label lblNewDTNotes;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecordID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Notes;
        private System.Windows.Forms.DataGridViewTextBoxColumn Hours;
        private System.Windows.Forms.ComboBox cboEditMethod;
        private System.Windows.Forms.Label lblEditMethod;
    }
}
