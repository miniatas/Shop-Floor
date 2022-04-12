/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 8/20/2013
 * Time: 9:03 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ShopFloor
{
	partial class ReworkProductionForm
	{
			/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Panel pnlRemoveScrapRecord;
		private System.Windows.Forms.Label lblTotalScrapPounds;
		private System.Windows.Forms.TextBox txtTotalScrapPounds;
		private System.Windows.Forms.RichTextBox rtbUnwindCurrentRoll;
		private System.Windows.Forms.TextBox txtNewScrapPounds;
		private System.Windows.Forms.RichTextBox rtbScrapRecords;
		private System.Windows.Forms.Button cmdRemoveDownTimeRecord;
		private System.Windows.Forms.Label lblRemoveScrapInfo;
		private System.Windows.Forms.ComboBox cboRemoveScrapRecord;
		private System.Windows.Forms.Button cmdRemoveScrapRecord;
		private System.Windows.Forms.Button cmdAddScrapRecord;
		private System.Windows.Forms.ComboBox cboScrapReasons;
		private System.Windows.Forms.Button cmdShowFilmUsage;
		private System.Windows.Forms.RichTextBox rtbCreatedRollInfo;
		private System.Windows.Forms.Button cmdReprintLabel;
		private System.Windows.Forms.RichTextBox rtbDownTimeRecords;
		private System.Windows.Forms.Button cmdJobHistory;
		private System.Windows.Forms.Button cmdMoveRoll;
		private System.Windows.Forms.TextBox txtConsumedFeet;
		private System.Windows.Forms.Label lblConsumedLF;
		private System.Windows.Forms.Label lblCreatedLF;
		private System.Windows.Forms.TextBox txtCreatedFeet;
		private System.Windows.Forms.Button cmdSaveAndClose;
		private System.Windows.Forms.Button cmdJobComplete;
		private System.Windows.Forms.Button cmdJobPulled;
		private System.Windows.Forms.Button cmdEndofShift;
		private System.Windows.Forms.Label lblSaveHours;
		private System.Windows.Forms.Panel pnlSaveHours;
		private System.Windows.Forms.Panel pnlCreateWIPInfo;
		private System.Windows.Forms.Button cmdCreateRoll;
		private System.Windows.Forms.Button cmdReturnRoll;
		private System.Windows.Forms.Button cmdConsumeRoll;
		private System.Windows.Forms.TextBox txtSetupHours;
		private System.Windows.Forms.TextBox txtTotalHours;
		private System.Windows.Forms.TextBox txtRunHours;
		private System.Windows.Forms.TextBox txtNewDownTimeHours;
		private System.Windows.Forms.ComboBox cboRemoveDowntimeRecord;
		private System.Windows.Forms.Label lblRemoveDTInfo;
		private System.Windows.Forms.Panel pnlRemoveDownTimeRecord;
		private System.Windows.Forms.ComboBox cboDownTimeReasons;
		private System.Windows.Forms.TextBox txtDownTimeHours;
		private System.Windows.Forms.Button cmdAddDowntimeRecord;
		private System.Windows.Forms.Label lblSetupHs;
		private System.Windows.Forms.Label lblDTHrs;
		private System.Windows.Forms.Label lblRunHrs;
		private System.Windows.Forms.Label lblTotHrs;
		private System.Windows.Forms.Label lblJobHours;
		private System.Windows.Forms.Panel pnlHours;
		private System.Windows.Forms.RichTextBox rtbJobDescriptions;

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
            this.components = new System.ComponentModel.Container();
            this.pnlHours = new System.Windows.Forms.Panel();
            this.txtNewDownTimeHours = new System.Windows.Forms.TextBox();
            this.txtTotalHours = new System.Windows.Forms.TextBox();
            this.txtDownTimeHours = new System.Windows.Forms.TextBox();
            this.txtRunHours = new System.Windows.Forms.TextBox();
            this.txtSetupHours = new System.Windows.Forms.TextBox();
            this.cboDownTimeReasons = new System.Windows.Forms.ComboBox();
            this.cmdAddDowntimeRecord = new System.Windows.Forms.Button();
            this.lblTotHrs = new System.Windows.Forms.Label();
            this.lblDTHrs = new System.Windows.Forms.Label();
            this.lblRunHrs = new System.Windows.Forms.Label();
            this.lblSetupHs = new System.Windows.Forms.Label();
            this.lblJobHours = new System.Windows.Forms.Label();
            this.pnlRemoveDownTimeRecord = new System.Windows.Forms.Panel();
            this.cmdRemoveDownTimeRecord = new System.Windows.Forms.Button();
            this.cboRemoveDowntimeRecord = new System.Windows.Forms.ComboBox();
            this.lblRemoveDTInfo = new System.Windows.Forms.Label();
            this.cmdConsumeRoll = new System.Windows.Forms.Button();
            this.cmdReturnRoll = new System.Windows.Forms.Button();
            this.cmdCreateRoll = new System.Windows.Forms.Button();
            this.pnlCreateWIPInfo = new System.Windows.Forms.Panel();
            this.rtbCreatedRollInfo = new System.Windows.Forms.RichTextBox();
            this.lblConsumedLF = new System.Windows.Forms.Label();
            this.pnlSaveHours = new System.Windows.Forms.Panel();
            this.cmdSaveAndClose = new System.Windows.Forms.Button();
            this.cmdEndofShift = new System.Windows.Forms.Button();
            this.cmdJobPulled = new System.Windows.Forms.Button();
            this.cmdJobComplete = new System.Windows.Forms.Button();
            this.lblSaveHours = new System.Windows.Forms.Label();
            this.txtConsumedFeet = new System.Windows.Forms.TextBox();
            this.txtCreatedFeet = new System.Windows.Forms.TextBox();
            this.lblCreatedLF = new System.Windows.Forms.Label();
            this.cmdMoveRoll = new System.Windows.Forms.Button();
            this.cmdJobHistory = new System.Windows.Forms.Button();
            this.rtbDownTimeRecords = new System.Windows.Forms.RichTextBox();
            this.cmdReprintLabel = new System.Windows.Forms.Button();
            this.rtbUnwindCurrentRoll = new System.Windows.Forms.RichTextBox();
            this.cmdShowFilmUsage = new System.Windows.Forms.Button();
            this.txtNewScrapPounds = new System.Windows.Forms.TextBox();
            this.cboScrapReasons = new System.Windows.Forms.ComboBox();
            this.cmdAddScrapRecord = new System.Windows.Forms.Button();
            this.rtbScrapRecords = new System.Windows.Forms.RichTextBox();
            this.pnlRemoveScrapRecord = new System.Windows.Forms.Panel();
            this.cmdRemoveScrapRecord = new System.Windows.Forms.Button();
            this.cboRemoveScrapRecord = new System.Windows.Forms.ComboBox();
            this.lblRemoveScrapInfo = new System.Windows.Forms.Label();
            this.txtTotalScrapPounds = new System.Windows.Forms.TextBox();
            this.lblTotalScrapPounds = new System.Windows.Forms.Label();
            this.rtbJobDescriptions = new System.Windows.Forms.RichTextBox();
            this.rtbJobTitle = new System.Windows.Forms.RichTextBox();
            this.rtbInputFilm = new System.Windows.Forms.RichTextBox();
            this.endOfShiftTimer = new System.Windows.Forms.Timer(this.components);
            this.cmdCreatePallet = new System.Windows.Forms.Button();
            this.cmdCreateFGPallet = new System.Windows.Forms.Button();
            this.pnlHours.SuspendLayout();
            this.pnlRemoveDownTimeRecord.SuspendLayout();
            this.pnlCreateWIPInfo.SuspendLayout();
            this.pnlSaveHours.SuspendLayout();
            this.pnlRemoveScrapRecord.SuspendLayout();
            this.SuspendLayout();
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
            this.pnlHours.Location = new System.Drawing.Point(8, 192);
            this.pnlHours.Name = "pnlHours";
            this.pnlHours.Size = new System.Drawing.Size(274, 95);
            this.pnlHours.TabIndex = 0;
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
            // lblJobHours
            // 
            this.lblJobHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJobHours.Location = new System.Drawing.Point(8, 175);
            this.lblJobHours.Name = "lblJobHours";
            this.lblJobHours.Size = new System.Drawing.Size(72, 14);
            this.lblJobHours.TabIndex = 2;
            this.lblJobHours.Text = "Job Hours";
            // 
            // pnlRemoveDownTimeRecord
            // 
            this.pnlRemoveDownTimeRecord.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlRemoveDownTimeRecord.Controls.Add(this.cmdRemoveDownTimeRecord);
            this.pnlRemoveDownTimeRecord.Controls.Add(this.cboRemoveDowntimeRecord);
            this.pnlRemoveDownTimeRecord.Controls.Add(this.lblRemoveDTInfo);
            this.pnlRemoveDownTimeRecord.Location = new System.Drawing.Point(8, 455);
            this.pnlRemoveDownTimeRecord.Name = "pnlRemoveDownTimeRecord";
            this.pnlRemoveDownTimeRecord.Size = new System.Drawing.Size(274, 37);
            this.pnlRemoveDownTimeRecord.TabIndex = 1;
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
            this.cmdRemoveDownTimeRecord.Click += new System.EventHandler(this.CmdRemoveDowntimeRecordClick);
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
            this.lblRemoveDTInfo.TabIndex = 1;
            this.lblRemoveDTInfo.Text = "Remove D/T Item No.:";
            // 
            // cmdConsumeRoll
            // 
            this.cmdConsumeRoll.Location = new System.Drawing.Point(288, 162);
            this.cmdConsumeRoll.Name = "cmdConsumeRoll";
            this.cmdConsumeRoll.Size = new System.Drawing.Size(100, 27);
            this.cmdConsumeRoll.TabIndex = 4;
            this.cmdConsumeRoll.Text = "Consume Roll";
            this.cmdConsumeRoll.UseVisualStyleBackColor = true;
            this.cmdConsumeRoll.Click += new System.EventHandler(this.CmdConsumeRollClick);
            // 
            // cmdReturnRoll
            // 
            this.cmdReturnRoll.Location = new System.Drawing.Point(394, 162);
            this.cmdReturnRoll.Name = "cmdReturnRoll";
            this.cmdReturnRoll.Size = new System.Drawing.Size(100, 27);
            this.cmdReturnRoll.TabIndex = 5;
            this.cmdReturnRoll.Text = "Return Roll";
            this.cmdReturnRoll.UseVisualStyleBackColor = true;
            this.cmdReturnRoll.Click += new System.EventHandler(this.CmdReturnRollClick);
            // 
            // cmdCreateRoll
            // 
            this.cmdCreateRoll.Location = new System.Drawing.Point(606, 162);
            this.cmdCreateRoll.Name = "cmdCreateRoll";
            this.cmdCreateRoll.Size = new System.Drawing.Size(80, 27);
            this.cmdCreateRoll.TabIndex = 6;
            this.cmdCreateRoll.Text = "Create Roll";
            this.cmdCreateRoll.UseVisualStyleBackColor = true;
            this.cmdCreateRoll.Click += new System.EventHandler(this.CmdCreateWIPRollClick);
            // 
            // pnlCreateWIPInfo
            // 
            this.pnlCreateWIPInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlCreateWIPInfo.Controls.Add(this.rtbCreatedRollInfo);
            this.pnlCreateWIPInfo.Location = new System.Drawing.Point(606, 192);
            this.pnlCreateWIPInfo.Name = "pnlCreateWIPInfo";
            this.pnlCreateWIPInfo.Size = new System.Drawing.Size(274, 326);
            this.pnlCreateWIPInfo.TabIndex = 10;
            // 
            // rtbCreatedRollInfo
            // 
            this.rtbCreatedRollInfo.BackColor = System.Drawing.SystemColors.Control;
            this.rtbCreatedRollInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbCreatedRollInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbCreatedRollInfo.ForeColor = System.Drawing.Color.Blue;
            this.rtbCreatedRollInfo.Location = new System.Drawing.Point(0, 0);
            this.rtbCreatedRollInfo.Name = "rtbCreatedRollInfo";
            this.rtbCreatedRollInfo.ReadOnly = true;
            this.rtbCreatedRollInfo.Size = new System.Drawing.Size(270, 322);
            this.rtbCreatedRollInfo.TabIndex = 2;
            this.rtbCreatedRollInfo.TabStop = false;
            this.rtbCreatedRollInfo.Text = "";
            this.rtbCreatedRollInfo.TextChanged += new System.EventHandler(this.RtbTextChanged);
            // 
            // lblConsumedLF
            // 
            this.lblConsumedLF.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConsumedLF.Location = new System.Drawing.Point(361, 296);
            this.lblConsumedLF.Name = "lblConsumedLF";
            this.lblConsumedLF.Size = new System.Drawing.Size(90, 21);
            this.lblConsumedLF.TabIndex = 11;
            this.lblConsumedLF.Text = "Consumed LF:";
            this.lblConsumedLF.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pnlSaveHours
            // 
            this.pnlSaveHours.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSaveHours.Controls.Add(this.cmdSaveAndClose);
            this.pnlSaveHours.Controls.Add(this.cmdEndofShift);
            this.pnlSaveHours.Controls.Add(this.cmdJobPulled);
            this.pnlSaveHours.Controls.Add(this.cmdJobComplete);
            this.pnlSaveHours.Location = new System.Drawing.Point(8, 524);
            this.pnlSaveHours.Name = "pnlSaveHours";
            this.pnlSaveHours.Size = new System.Drawing.Size(274, 56);
            this.pnlSaveHours.TabIndex = 2;
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
            this.cmdJobPulled.Click += new System.EventHandler(this.CmdJobPulledClick);
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
            // lblSaveHours
            // 
            this.lblSaveHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaveHours.Location = new System.Drawing.Point(8, 495);
            this.lblSaveHours.Name = "lblSaveHours";
            this.lblSaveHours.Size = new System.Drawing.Size(174, 28);
            this.lblSaveHours.TabIndex = 13;
            this.lblSaveHours.Text = "Save Job Hours";
            this.lblSaveHours.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtConsumedFeet
            // 
            this.txtConsumedFeet.Enabled = false;
            this.txtConsumedFeet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConsumedFeet.ForeColor = System.Drawing.Color.Black;
            this.txtConsumedFeet.Location = new System.Drawing.Point(457, 293);
            this.txtConsumedFeet.Name = "txtConsumedFeet";
            this.txtConsumedFeet.Size = new System.Drawing.Size(70, 20);
            this.txtConsumedFeet.TabIndex = 14;
            this.txtConsumedFeet.Text = "0";
            this.txtConsumedFeet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCreatedFeet
            // 
            this.txtCreatedFeet.Enabled = false;
            this.txtCreatedFeet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCreatedFeet.ForeColor = System.Drawing.Color.Black;
            this.txtCreatedFeet.Location = new System.Drawing.Point(808, 526);
            this.txtCreatedFeet.Name = "txtCreatedFeet";
            this.txtCreatedFeet.Size = new System.Drawing.Size(70, 20);
            this.txtCreatedFeet.TabIndex = 16;
            this.txtCreatedFeet.Text = "0";
            this.txtCreatedFeet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCreatedLF
            // 
            this.lblCreatedLF.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreatedLF.Location = new System.Drawing.Point(702, 528);
            this.lblCreatedLF.Name = "lblCreatedLF";
            this.lblCreatedLF.Size = new System.Drawing.Size(100, 21);
            this.lblCreatedLF.TabIndex = 15;
            this.lblCreatedLF.Text = "Created LF:";
            this.lblCreatedLF.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmdMoveRoll
            // 
            this.cmdMoveRoll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdMoveRoll.Location = new System.Drawing.Point(692, 162);
            this.cmdMoveRoll.Name = "cmdMoveRoll";
            this.cmdMoveRoll.Size = new System.Drawing.Size(80, 27);
            this.cmdMoveRoll.TabIndex = 7;
            this.cmdMoveRoll.Text = "Move Roll";
            this.cmdMoveRoll.UseVisualStyleBackColor = true;
            this.cmdMoveRoll.Click += new System.EventHandler(this.CmdMoveRollClick);
            // 
            // cmdJobHistory
            // 
            this.cmdJobHistory.Location = new System.Drawing.Point(96, 162);
            this.cmdJobHistory.Name = "cmdJobHistory";
            this.cmdJobHistory.Size = new System.Drawing.Size(100, 27);
            this.cmdJobHistory.TabIndex = 3;
            this.cmdJobHistory.Text = "Show Job History";
            this.cmdJobHistory.UseVisualStyleBackColor = true;
            this.cmdJobHistory.Click += new System.EventHandler(this.CmdJobHistoryClick);
            // 
            // rtbDownTimeRecords
            // 
            this.rtbDownTimeRecords.BackColor = System.Drawing.SystemColors.Control;
            this.rtbDownTimeRecords.ForeColor = System.Drawing.Color.Blue;
            this.rtbDownTimeRecords.Location = new System.Drawing.Point(9, 294);
            this.rtbDownTimeRecords.Name = "rtbDownTimeRecords";
            this.rtbDownTimeRecords.ReadOnly = true;
            this.rtbDownTimeRecords.Size = new System.Drawing.Size(272, 155);
            this.rtbDownTimeRecords.TabIndex = 21;
            this.rtbDownTimeRecords.TabStop = false;
            this.rtbDownTimeRecords.Text = "";
            this.rtbDownTimeRecords.TextChanged += new System.EventHandler(this.RtbTextChanged);
            // 
            // cmdReprintLabel
            // 
            this.cmdReprintLabel.Location = new System.Drawing.Point(632, 560);
            this.cmdReprintLabel.Name = "cmdReprintLabel";
            this.cmdReprintLabel.Size = new System.Drawing.Size(91, 30);
            this.cmdReprintLabel.TabIndex = 9;
            this.cmdReprintLabel.Text = "Reprint Label";
            this.cmdReprintLabel.UseVisualStyleBackColor = true;
            this.cmdReprintLabel.Click += new System.EventHandler(this.CmdReprintLabelClick);
            // 
            // rtbUnwindCurrentRoll
            // 
            this.rtbUnwindCurrentRoll.BackColor = System.Drawing.SystemColors.Control;
            this.rtbUnwindCurrentRoll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbUnwindCurrentRoll.ForeColor = System.Drawing.Color.Blue;
            this.rtbUnwindCurrentRoll.Location = new System.Drawing.Point(288, 192);
            this.rtbUnwindCurrentRoll.Name = "rtbUnwindCurrentRoll";
            this.rtbUnwindCurrentRoll.ReadOnly = true;
            this.rtbUnwindCurrentRoll.Size = new System.Drawing.Size(312, 95);
            this.rtbUnwindCurrentRoll.TabIndex = 22;
            this.rtbUnwindCurrentRoll.TabStop = false;
            this.rtbUnwindCurrentRoll.Text = "";
            // 
            // cmdShowFilmUsage
            // 
            this.cmdShowFilmUsage.Location = new System.Drawing.Point(502, 162);
            this.cmdShowFilmUsage.Name = "cmdShowFilmUsage";
            this.cmdShowFilmUsage.Size = new System.Drawing.Size(100, 27);
            this.cmdShowFilmUsage.TabIndex = 23;
            this.cmdShowFilmUsage.Text = "Show Film Usage";
            this.cmdShowFilmUsage.UseVisualStyleBackColor = true;
            this.cmdShowFilmUsage.Click += new System.EventHandler(this.CmdShowFilmUsageClick);
            // 
            // txtNewScrapPounds
            // 
            this.txtNewScrapPounds.Location = new System.Drawing.Point(294, 331);
            this.txtNewScrapPounds.Name = "txtNewScrapPounds";
            this.txtNewScrapPounds.Size = new System.Drawing.Size(50, 20);
            this.txtNewScrapPounds.TabIndex = 7;
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
            this.cboScrapReasons.Location = new System.Drawing.Point(359, 331);
            this.cboScrapReasons.Name = "cboScrapReasons";
            this.cboScrapReasons.Size = new System.Drawing.Size(135, 21);
            this.cboScrapReasons.TabIndex = 8;
            this.cboScrapReasons.SelectedIndexChanged += new System.EventHandler(this.CboScrapReasonsSelectedIndexChanged);
            // 
            // cmdAddScrapRecord
            // 
            this.cmdAddScrapRecord.Enabled = false;
            this.cmdAddScrapRecord.Location = new System.Drawing.Point(502, 327);
            this.cmdAddScrapRecord.Name = "cmdAddScrapRecord";
            this.cmdAddScrapRecord.Size = new System.Drawing.Size(91, 27);
            this.cmdAddScrapRecord.TabIndex = 9;
            this.cmdAddScrapRecord.Text = "Add Scrap Lbs";
            this.cmdAddScrapRecord.UseVisualStyleBackColor = true;
            this.cmdAddScrapRecord.Click += new System.EventHandler(this.CmdAddScrapRecordClick);
            // 
            // rtbScrapRecords
            // 
            this.rtbScrapRecords.BackColor = System.Drawing.SystemColors.Control;
            this.rtbScrapRecords.ForeColor = System.Drawing.Color.Blue;
            this.rtbScrapRecords.Location = new System.Drawing.Point(288, 361);
            this.rtbScrapRecords.Name = "rtbScrapRecords";
            this.rtbScrapRecords.ReadOnly = true;
            this.rtbScrapRecords.Size = new System.Drawing.Size(312, 155);
            this.rtbScrapRecords.TabIndex = 24;
            this.rtbScrapRecords.TabStop = false;
            this.rtbScrapRecords.Text = "";
            this.rtbScrapRecords.TextChanged += new System.EventHandler(this.RtbTextChanged);
            // 
            // pnlRemoveScrapRecord
            // 
            this.pnlRemoveScrapRecord.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlRemoveScrapRecord.Controls.Add(this.cmdRemoveScrapRecord);
            this.pnlRemoveScrapRecord.Controls.Add(this.cboRemoveScrapRecord);
            this.pnlRemoveScrapRecord.Controls.Add(this.lblRemoveScrapInfo);
            this.pnlRemoveScrapRecord.Location = new System.Drawing.Point(288, 524);
            this.pnlRemoveScrapRecord.Name = "pnlRemoveScrapRecord";
            this.pnlRemoveScrapRecord.Size = new System.Drawing.Size(312, 37);
            this.pnlRemoveScrapRecord.TabIndex = 25;
            this.pnlRemoveScrapRecord.Visible = false;
            // 
            // cmdRemoveScrapRecord
            // 
            this.cmdRemoveScrapRecord.Enabled = false;
            this.cmdRemoveScrapRecord.Location = new System.Drawing.Point(242, 3);
            this.cmdRemoveScrapRecord.Name = "cmdRemoveScrapRecord";
            this.cmdRemoveScrapRecord.Size = new System.Drawing.Size(57, 27);
            this.cmdRemoveScrapRecord.TabIndex = 1;
            this.cmdRemoveScrapRecord.Text = "Remove";
            this.cmdRemoveScrapRecord.UseVisualStyleBackColor = true;
            this.cmdRemoveScrapRecord.Click += new System.EventHandler(this.CmdRemoveScrapRecordClick);
            // 
            // cboRemoveScrapRecord
            // 
            this.cboRemoveScrapRecord.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRemoveScrapRecord.FormattingEnabled = true;
            this.cboRemoveScrapRecord.Items.AddRange(new object[] {
            ""});
            this.cboRemoveScrapRecord.Location = new System.Drawing.Point(170, 7);
            this.cboRemoveScrapRecord.Name = "cboRemoveScrapRecord";
            this.cboRemoveScrapRecord.Size = new System.Drawing.Size(57, 21);
            this.cboRemoveScrapRecord.TabIndex = 0;
            this.cboRemoveScrapRecord.SelectedIndexChanged += new System.EventHandler(this.CboRemoveScrapInfoSelectedIndexChanged);
            // 
            // lblRemoveScrapInfo
            // 
            this.lblRemoveScrapInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemoveScrapInfo.Location = new System.Drawing.Point(6, 10);
            this.lblRemoveScrapInfo.Name = "lblRemoveScrapInfo";
            this.lblRemoveScrapInfo.Size = new System.Drawing.Size(158, 23);
            this.lblRemoveScrapInfo.TabIndex = 1;
            this.lblRemoveScrapInfo.Text = "Remove Scrap Item No.:";
            // 
            // txtTotalScrapPounds
            // 
            this.txtTotalScrapPounds.Enabled = false;
            this.txtTotalScrapPounds.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalScrapPounds.ForeColor = System.Drawing.Color.Black;
            this.txtTotalScrapPounds.Location = new System.Drawing.Point(523, 567);
            this.txtTotalScrapPounds.Name = "txtTotalScrapPounds";
            this.txtTotalScrapPounds.Size = new System.Drawing.Size(70, 20);
            this.txtTotalScrapPounds.TabIndex = 27;
            this.txtTotalScrapPounds.Text = "0";
            this.txtTotalScrapPounds.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotalScrapPounds
            // 
            this.lblTotalScrapPounds.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalScrapPounds.Location = new System.Drawing.Point(394, 567);
            this.lblTotalScrapPounds.Name = "lblTotalScrapPounds";
            this.lblTotalScrapPounds.Size = new System.Drawing.Size(123, 21);
            this.lblTotalScrapPounds.TabIndex = 28;
            this.lblTotalScrapPounds.Text = "Total Scrap Pounds:";
            this.lblTotalScrapPounds.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rtbJobDescriptions
            // 
            this.rtbJobDescriptions.BackColor = System.Drawing.SystemColors.Control;
            this.rtbJobDescriptions.ForeColor = System.Drawing.Color.Blue;
            this.rtbJobDescriptions.Location = new System.Drawing.Point(7, 43);
            this.rtbJobDescriptions.Name = "rtbJobDescriptions";
            this.rtbJobDescriptions.ReadOnly = true;
            this.rtbJobDescriptions.Size = new System.Drawing.Size(871, 86);
            this.rtbJobDescriptions.TabIndex = 0;
            this.rtbJobDescriptions.TabStop = false;
            this.rtbJobDescriptions.Text = "";
            // 
            // rtbJobTitle
            // 
            this.rtbJobTitle.BackColor = System.Drawing.Color.Red;
            this.rtbJobTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbJobTitle.ForeColor = System.Drawing.Color.White;
            this.rtbJobTitle.Location = new System.Drawing.Point(8, 12);
            this.rtbJobTitle.Name = "rtbJobTitle";
            this.rtbJobTitle.ReadOnly = true;
            this.rtbJobTitle.Size = new System.Drawing.Size(871, 25);
            this.rtbJobTitle.TabIndex = 0;
            this.rtbJobTitle.TabStop = false;
            this.rtbJobTitle.Text = "";
            // 
            // rtbInputFilm
            // 
            this.rtbInputFilm.BackColor = System.Drawing.SystemColors.Control;
            this.rtbInputFilm.ForeColor = System.Drawing.Color.Blue;
            this.rtbInputFilm.Location = new System.Drawing.Point(7, 135);
            this.rtbInputFilm.Name = "rtbInputFilm";
            this.rtbInputFilm.ReadOnly = true;
            this.rtbInputFilm.Size = new System.Drawing.Size(871, 21);
            this.rtbInputFilm.TabIndex = 29;
            this.rtbInputFilm.TabStop = false;
            this.rtbInputFilm.Text = "";
            // 
            // endOfShiftTimer
            // 
            this.endOfShiftTimer.Tick += new System.EventHandler(this.EndOfShiftTimerTick);
            // 
            // cmdCreatePallet
            // 
            this.cmdCreatePallet.Location = new System.Drawing.Point(750, 560);
            this.cmdCreatePallet.Name = "cmdCreatePallet";
            this.cmdCreatePallet.Size = new System.Drawing.Size(91, 30);
            this.cmdCreatePallet.TabIndex = 30;
            this.cmdCreatePallet.Text = "Create Pallet";
            this.cmdCreatePallet.UseVisualStyleBackColor = true;
            this.cmdCreatePallet.Visible = false;
            this.cmdCreatePallet.Click += new System.EventHandler(this.cmdCreatePallet_Click);
            // 
            // cmdCreateFGPallet
            // 
            this.cmdCreateFGPallet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCreateFGPallet.Location = new System.Drawing.Point(778, 162);
            this.cmdCreateFGPallet.Name = "cmdCreateFGPallet";
            this.cmdCreateFGPallet.Size = new System.Drawing.Size(101, 27);
            this.cmdCreateFGPallet.TabIndex = 31;
            this.cmdCreateFGPallet.Text = "Create FG Pallet";
            this.cmdCreateFGPallet.UseVisualStyleBackColor = true;
            this.cmdCreateFGPallet.Click += new System.EventHandler(this.cmdCreateFGPallet_Click);
            // 
            // ReworkProductionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 634);
            this.ControlBox = false;
            this.Controls.Add(this.cmdCreateFGPallet);
            this.Controls.Add(this.cmdCreatePallet);
            this.Controls.Add(this.rtbInputFilm);
            this.Controls.Add(this.rtbJobTitle);
            this.Controls.Add(this.rtbJobDescriptions);
            this.Controls.Add(this.lblTotalScrapPounds);
            this.Controls.Add(this.txtTotalScrapPounds);
            this.Controls.Add(this.pnlRemoveScrapRecord);
            this.Controls.Add(this.rtbScrapRecords);
            this.Controls.Add(this.txtNewScrapPounds);
            this.Controls.Add(this.cboScrapReasons);
            this.Controls.Add(this.cmdShowFilmUsage);
            this.Controls.Add(this.cmdAddScrapRecord);
            this.Controls.Add(this.rtbUnwindCurrentRoll);
            this.Controls.Add(this.cmdReprintLabel);
            this.Controls.Add(this.rtbDownTimeRecords);
            this.Controls.Add(this.cmdJobHistory);
            this.Controls.Add(this.cmdMoveRoll);
            this.Controls.Add(this.txtCreatedFeet);
            this.Controls.Add(this.lblCreatedLF);
            this.Controls.Add(this.txtConsumedFeet);
            this.Controls.Add(this.lblSaveHours);
            this.Controls.Add(this.pnlSaveHours);
            this.Controls.Add(this.lblConsumedLF);
            this.Controls.Add(this.pnlCreateWIPInfo);
            this.Controls.Add(this.cmdCreateRoll);
            this.Controls.Add(this.cmdReturnRoll);
            this.Controls.Add(this.cmdConsumeRoll);
            this.Controls.Add(this.pnlRemoveDownTimeRecord);
            this.Controls.Add(this.lblJobHours);
            this.Controls.Add(this.pnlHours);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReworkProductionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Shown += new System.EventHandler(this.ReworkProductionForm_Shown);
            this.pnlHours.ResumeLayout(false);
            this.pnlHours.PerformLayout();
            this.pnlRemoveDownTimeRecord.ResumeLayout(false);
            this.pnlCreateWIPInfo.ResumeLayout(false);
            this.pnlSaveHours.ResumeLayout(false);
            this.pnlRemoveScrapRecord.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.RichTextBox rtbInputFilm;
		private System.Windows.Forms.RichTextBox rtbJobTitle;
		private System.Windows.Forms.Timer endOfShiftTimer;
        private System.Windows.Forms.Button cmdCreatePallet;
        private System.Windows.Forms.Button cmdCreateFGPallet;
    }
}
