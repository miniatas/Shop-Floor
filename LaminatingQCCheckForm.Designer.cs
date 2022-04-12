/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 9/29/2011
 * Time: 2:42 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ShopFloor
{
	public partial class LaminatingQCCheckForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button btnRejectRoll;
		private System.Windows.Forms.Button btnAcceptRoll;
		private System.Windows.Forms.Label lblComments;
		private System.Windows.Forms.Label lblLaminationQuality;
		private System.Windows.Forms.Label lblCof;
		private System.Windows.Forms.ComboBox cbxCof;
		private System.Windows.Forms.Label lblCurl;
		private System.Windows.Forms.ComboBox cbxCurl;
		private System.Windows.Forms.Label lblSkips;
		private System.Windows.Forms.ComboBox cbxSkips;
		private System.Windows.Forms.Label lblStreaks;
		private System.Windows.Forms.ComboBox cbxStreaks;
		private System.Windows.Forms.Label lblSeal;
		private System.Windows.Forms.TextBox txtSeal;
		private System.Windows.Forms.Label lblBond;
		private System.Windows.Forms.TextBox txtBond;
		private System.Windows.Forms.Label lblAdhesiveWeightGearSide;
		private System.Windows.Forms.TextBox txtAdhesiveWeightGearSize;
		private System.Windows.Forms.Label lblAdhesiveWeightOperatorSide;
		private System.Windows.Forms.TextBox txtAdhesiveWeightOperatorSide;
		private System.Windows.Forms.Label lblAppearance;
		private System.Windows.Forms.Label lblGlueWidth;
		private System.Windows.Forms.Label lblPrintRepeat;
		private System.Windows.Forms.TextBox txtPrintRepeat;
		private System.Windows.Forms.Label lblGauge;
		private System.Windows.Forms.Label lblCoating;
		private System.Windows.Forms.ComboBox cbxCoating;
		private System.Windows.Forms.Label lblDimensionsInInches;
		private System.Windows.Forms.Label lblRawMaterialData;
		private System.Windows.Forms.RichTextBox rtbJobInformation;
		private System.Windows.Forms.ComboBox cbxAppearance;
		private System.Windows.Forms.TextBox txtFilmGauge;
		private System.Windows.Forms.TextBox txtGlueWidth;
		private System.Windows.Forms.RichTextBox rtbComments;
		
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
            this.rtbJobInformation = new System.Windows.Forms.RichTextBox();
            this.lblRawMaterialData = new System.Windows.Forms.Label();
            this.lblDimensionsInInches = new System.Windows.Forms.Label();
            this.cbxCoating = new System.Windows.Forms.ComboBox();
            this.lblCoating = new System.Windows.Forms.Label();
            this.txtFilmGauge = new System.Windows.Forms.TextBox();
            this.lblGauge = new System.Windows.Forms.Label();
            this.txtPrintRepeat = new System.Windows.Forms.TextBox();
            this.lblPrintRepeat = new System.Windows.Forms.Label();
            this.txtGlueWidth = new System.Windows.Forms.TextBox();
            this.lblGlueWidth = new System.Windows.Forms.Label();
            this.cbxAppearance = new System.Windows.Forms.ComboBox();
            this.lblAppearance = new System.Windows.Forms.Label();
            this.txtAdhesiveWeightOperatorSide = new System.Windows.Forms.TextBox();
            this.lblAdhesiveWeightOperatorSide = new System.Windows.Forms.Label();
            this.txtAdhesiveWeightGearSize = new System.Windows.Forms.TextBox();
            this.lblAdhesiveWeightGearSide = new System.Windows.Forms.Label();
            this.txtBond = new System.Windows.Forms.TextBox();
            this.lblBond = new System.Windows.Forms.Label();
            this.txtSeal = new System.Windows.Forms.TextBox();
            this.lblSeal = new System.Windows.Forms.Label();
            this.cbxStreaks = new System.Windows.Forms.ComboBox();
            this.lblStreaks = new System.Windows.Forms.Label();
            this.cbxSkips = new System.Windows.Forms.ComboBox();
            this.lblSkips = new System.Windows.Forms.Label();
            this.cbxCurl = new System.Windows.Forms.ComboBox();
            this.lblCurl = new System.Windows.Forms.Label();
            this.cbxCof = new System.Windows.Forms.ComboBox();
            this.lblCof = new System.Windows.Forms.Label();
            this.lblLaminationQuality = new System.Windows.Forms.Label();
            this.rtbComments = new System.Windows.Forms.RichTextBox();
            this.lblComments = new System.Windows.Forms.Label();
            this.btnAcceptRoll = new System.Windows.Forms.Button();
            this.btnRejectRoll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtbJobInformation
            // 
            this.rtbJobInformation.BackColor = System.Drawing.SystemColors.Control;
            this.rtbJobInformation.ForeColor = System.Drawing.Color.Blue;
            this.rtbJobInformation.Location = new System.Drawing.Point(15, 9);
            this.rtbJobInformation.Name = "rtbJobInformation";
            this.rtbJobInformation.ReadOnly = true;
            this.rtbJobInformation.Size = new System.Drawing.Size(659, 265);
            this.rtbJobInformation.TabIndex = 1;
            this.rtbJobInformation.Text = "";
            // 
            // lblRawMaterialData
            // 
            this.lblRawMaterialData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRawMaterialData.Location = new System.Drawing.Point(15, 347);
            this.lblRawMaterialData.Name = "lblRawMaterialData";
            this.lblRawMaterialData.Size = new System.Drawing.Size(139, 21);
            this.lblRawMaterialData.TabIndex = 28;
            this.lblRawMaterialData.Text = "Raw Material Data";
            this.lblRawMaterialData.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblDimensionsInInches
            // 
            this.lblDimensionsInInches.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDimensionsInInches.Location = new System.Drawing.Point(15, 277);
            this.lblDimensionsInInches.Name = "lblDimensionsInInches";
            this.lblDimensionsInInches.Size = new System.Drawing.Size(139, 21);
            this.lblDimensionsInInches.TabIndex = 27;
            this.lblDimensionsInInches.Text = "Dimension in Inches";
            this.lblDimensionsInInches.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cbxCoating
            // 
            this.cbxCoating.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCoating.FormattingEnabled = true;
            this.cbxCoating.Items.AddRange(new object[] {
            "None",
            "Acrylic",
            "Saran"});
            this.cbxCoating.Location = new System.Drawing.Point(81, 394);
            this.cbxCoating.Name = "cbxCoating";
            this.cbxCoating.Size = new System.Drawing.Size(73, 21);
            this.cbxCoating.TabIndex = 26;
            // 
            // lblCoating
            // 
            this.lblCoating.Location = new System.Drawing.Point(15, 397);
            this.lblCoating.Name = "lblCoating";
            this.lblCoating.Size = new System.Drawing.Size(60, 21);
            this.lblCoating.TabIndex = 25;
            this.lblCoating.Text = "Coating:";
            this.lblCoating.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtFilmGauge
            // 
            this.txtFilmGauge.Location = new System.Drawing.Point(103, 369);
            this.txtFilmGauge.Name = "txtFilmGauge";
            this.txtFilmGauge.Size = new System.Drawing.Size(51, 20);
            this.txtFilmGauge.TabIndex = 20;
            this.txtFilmGauge.Text = "0.000";
            this.txtFilmGauge.Enter += new System.EventHandler(this.TxtNumbersorDecimalEnter);
            this.txtFilmGauge.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtNumbersKeyDown);
            this.txtFilmGauge.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumbersorDecimalKeyPressThreeDecimalPlaces);
            this.txtFilmGauge.Leave += new System.EventHandler(this.TxtNumbersorDecimalLeaveThreeDecimalPlaces);
            // 
            // lblGauge
            // 
            this.lblGauge.Location = new System.Drawing.Point(15, 372);
            this.lblGauge.Name = "lblGauge";
            this.lblGauge.Size = new System.Drawing.Size(82, 21);
            this.lblGauge.TabIndex = 19;
            this.lblGauge.Text = "Gauge (mils):";
            this.lblGauge.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtPrintRepeat
            // 
            this.txtPrintRepeat.Location = new System.Drawing.Point(103, 319);
            this.txtPrintRepeat.Name = "txtPrintRepeat";
            this.txtPrintRepeat.Size = new System.Drawing.Size(51, 20);
            this.txtPrintRepeat.TabIndex = 18;
            this.txtPrintRepeat.Text = "0.0000";
            this.txtPrintRepeat.Enter += new System.EventHandler(this.TxtNumbersorDecimalEnter);
            this.txtPrintRepeat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtNumbersKeyDown);
            this.txtPrintRepeat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumbersorDecimalKeyPressFourDecimalPlaces);
            this.txtPrintRepeat.Leave += new System.EventHandler(this.TxtNumbersorDecimalLeaveFourDecimalPlaces);
            // 
            // lblPrintRepeat
            // 
            this.lblPrintRepeat.Location = new System.Drawing.Point(15, 322);
            this.lblPrintRepeat.Name = "lblPrintRepeat";
            this.lblPrintRepeat.Size = new System.Drawing.Size(82, 21);
            this.lblPrintRepeat.TabIndex = 17;
            this.lblPrintRepeat.Text = "Print Repeat:";
            this.lblPrintRepeat.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtGlueWidth
            // 
            this.txtGlueWidth.Location = new System.Drawing.Point(103, 299);
            this.txtGlueWidth.Name = "txtGlueWidth";
            this.txtGlueWidth.Size = new System.Drawing.Size(51, 20);
            this.txtGlueWidth.TabIndex = 16;
            this.txtGlueWidth.Text = "0.0000";
            this.txtGlueWidth.Enter += new System.EventHandler(this.TxtNumbersorDecimalEnter);
            this.txtGlueWidth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtNumbersKeyDown);
            this.txtGlueWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumbersorDecimalKeyPressFourDecimalPlaces);
            this.txtGlueWidth.Leave += new System.EventHandler(this.TxtNumbersorDecimalLeaveFourDecimalPlaces);
            // 
            // lblGlueWidth
            // 
            this.lblGlueWidth.Location = new System.Drawing.Point(15, 302);
            this.lblGlueWidth.Name = "lblGlueWidth";
            this.lblGlueWidth.Size = new System.Drawing.Size(82, 21);
            this.lblGlueWidth.TabIndex = 15;
            this.lblGlueWidth.Text = "Glue Width:";
            this.lblGlueWidth.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbxAppearance
            // 
            this.cbxAppearance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAppearance.FormattingEnabled = true;
            this.cbxAppearance.Items.AddRange(new object[] {
            "Pass",
            "Fail"});
            this.cbxAppearance.Location = new System.Drawing.Point(318, 299);
            this.cbxAppearance.Name = "cbxAppearance";
            this.cbxAppearance.Size = new System.Drawing.Size(73, 21);
            this.cbxAppearance.TabIndex = 30;
            // 
            // lblAppearance
            // 
            this.lblAppearance.Location = new System.Drawing.Point(175, 302);
            this.lblAppearance.Name = "lblAppearance";
            this.lblAppearance.Size = new System.Drawing.Size(137, 21);
            this.lblAppearance.TabIndex = 29;
            this.lblAppearance.Text = "Appearance:";
            this.lblAppearance.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtAdhesiveWeightOperatorSide
            // 
            this.txtAdhesiveWeightOperatorSide.Location = new System.Drawing.Point(340, 324);
            this.txtAdhesiveWeightOperatorSide.Name = "txtAdhesiveWeightOperatorSide";
            this.txtAdhesiveWeightOperatorSide.Size = new System.Drawing.Size(51, 20);
            this.txtAdhesiveWeightOperatorSide.TabIndex = 32;
            this.txtAdhesiveWeightOperatorSide.Text = "1.20";
            this.txtAdhesiveWeightOperatorSide.Enter += new System.EventHandler(this.TxtNumbersorDecimalEnter);
            this.txtAdhesiveWeightOperatorSide.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtNumbersKeyDown);
            this.txtAdhesiveWeightOperatorSide.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumbersorDecimalKeyPressTwoDecimalPlaces);
            this.txtAdhesiveWeightOperatorSide.Leave += new System.EventHandler(this.TxtNumbersorDecimalLeaveTwoDecimalPlaces);
            // 
            // lblAdhesiveWeightOperatorSide
            // 
            this.lblAdhesiveWeightOperatorSide.Location = new System.Drawing.Point(175, 327);
            this.lblAdhesiveWeightOperatorSide.Name = "lblAdhesiveWeightOperatorSide";
            this.lblAdhesiveWeightOperatorSide.Size = new System.Drawing.Size(159, 21);
            this.lblAdhesiveWeightOperatorSide.TabIndex = 31;
            this.lblAdhesiveWeightOperatorSide.Text = "Adhesive Wt. Opr. (Lbs/Ream):";
            this.lblAdhesiveWeightOperatorSide.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtAdhesiveWeightGearSize
            // 
            this.txtAdhesiveWeightGearSize.Location = new System.Drawing.Point(340, 344);
            this.txtAdhesiveWeightGearSize.Name = "txtAdhesiveWeightGearSize";
            this.txtAdhesiveWeightGearSize.Size = new System.Drawing.Size(51, 20);
            this.txtAdhesiveWeightGearSize.TabIndex = 34;
            this.txtAdhesiveWeightGearSize.Text = "1.20";
            this.txtAdhesiveWeightGearSize.Enter += new System.EventHandler(this.TxtNumbersorDecimalEnter);
            this.txtAdhesiveWeightGearSize.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtNumbersKeyDown);
            this.txtAdhesiveWeightGearSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumbersorDecimalKeyPressTwoDecimalPlaces);
            this.txtAdhesiveWeightGearSize.Leave += new System.EventHandler(this.TxtNumbersorDecimalLeaveTwoDecimalPlaces);
            // 
            // lblAdhesiveWeightGearSide
            // 
            this.lblAdhesiveWeightGearSide.Location = new System.Drawing.Point(175, 347);
            this.lblAdhesiveWeightGearSide.Name = "lblAdhesiveWeightGearSide";
            this.lblAdhesiveWeightGearSide.Size = new System.Drawing.Size(159, 21);
            this.lblAdhesiveWeightGearSide.TabIndex = 33;
            this.lblAdhesiveWeightGearSide.Text = "Adhesive Wt. Gear (Lbs/Ream):";
            this.lblAdhesiveWeightGearSide.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtBond
            // 
            this.txtBond.Location = new System.Drawing.Point(340, 364);
            this.txtBond.Name = "txtBond";
            this.txtBond.Size = new System.Drawing.Size(51, 20);
            this.txtBond.TabIndex = 36;
            this.txtBond.Text = "150";
            this.txtBond.Enter += new System.EventHandler(this.TxtNumbersEnter);
            this.txtBond.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtNumbersKeyDown);
            this.txtBond.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumbersKeyPress);
            this.txtBond.Leave += new System.EventHandler(this.TxtNumbersLeave);
            // 
            // lblBond
            // 
            this.lblBond.Location = new System.Drawing.Point(175, 367);
            this.lblBond.Name = "lblBond";
            this.lblBond.Size = new System.Drawing.Size(159, 21);
            this.lblBond.TabIndex = 35;
            this.lblBond.Text = "Bond (grams):";
            this.lblBond.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtSeal
            // 
            this.txtSeal.Location = new System.Drawing.Point(340, 384);
            this.txtSeal.Name = "txtSeal";
            this.txtSeal.Size = new System.Drawing.Size(51, 20);
            this.txtSeal.TabIndex = 38;
            this.txtSeal.Text = "300";
            this.txtSeal.Enter += new System.EventHandler(this.TxtNumbersEnter);
            this.txtSeal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtNumbersKeyDown);
            this.txtSeal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumbersKeyPress);
            this.txtSeal.Leave += new System.EventHandler(this.TxtNumbersLeave);
            // 
            // lblSeal
            // 
            this.lblSeal.Location = new System.Drawing.Point(175, 387);
            this.lblSeal.Name = "lblSeal";
            this.lblSeal.Size = new System.Drawing.Size(159, 21);
            this.lblSeal.TabIndex = 37;
            this.lblSeal.Text = "Seal (grams):";
            this.lblSeal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbxStreaks
            // 
            this.cbxStreaks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxStreaks.FormattingEnabled = true;
            this.cbxStreaks.Items.AddRange(new object[] {
            "Pass",
            "Fail"});
            this.cbxStreaks.Location = new System.Drawing.Point(318, 409);
            this.cbxStreaks.Name = "cbxStreaks";
            this.cbxStreaks.Size = new System.Drawing.Size(73, 21);
            this.cbxStreaks.TabIndex = 40;
            // 
            // lblStreaks
            // 
            this.lblStreaks.Location = new System.Drawing.Point(175, 412);
            this.lblStreaks.Name = "lblStreaks";
            this.lblStreaks.Size = new System.Drawing.Size(137, 21);
            this.lblStreaks.TabIndex = 39;
            this.lblStreaks.Text = "Streaks:";
            this.lblStreaks.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbxSkips
            // 
            this.cbxSkips.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSkips.FormattingEnabled = true;
            this.cbxSkips.Items.AddRange(new object[] {
            "Pass",
            "Fail"});
            this.cbxSkips.Location = new System.Drawing.Point(318, 434);
            this.cbxSkips.Name = "cbxSkips";
            this.cbxSkips.Size = new System.Drawing.Size(73, 21);
            this.cbxSkips.TabIndex = 42;
            // 
            // lblSkips
            // 
            this.lblSkips.Location = new System.Drawing.Point(175, 437);
            this.lblSkips.Name = "lblSkips";
            this.lblSkips.Size = new System.Drawing.Size(137, 21);
            this.lblSkips.TabIndex = 41;
            this.lblSkips.Text = "Skips:";
            this.lblSkips.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbxCurl
            // 
            this.cbxCurl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCurl.FormattingEnabled = true;
            this.cbxCurl.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.cbxCurl.Location = new System.Drawing.Point(463, 299);
            this.cbxCurl.Name = "cbxCurl";
            this.cbxCurl.Size = new System.Drawing.Size(73, 21);
            this.cbxCurl.TabIndex = 44;
            // 
            // lblCurl
            // 
            this.lblCurl.Location = new System.Drawing.Point(398, 302);
            this.lblCurl.Name = "lblCurl";
            this.lblCurl.Size = new System.Drawing.Size(59, 21);
            this.lblCurl.TabIndex = 43;
            this.lblCurl.Text = "Curl:";
            this.lblCurl.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbxCof
            // 
            this.cbxCof.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCof.FormattingEnabled = true;
            this.cbxCof.Items.AddRange(new object[] {
            "Pass",
            "Fail"});
            this.cbxCof.Location = new System.Drawing.Point(463, 324);
            this.cbxCof.Name = "cbxCof";
            this.cbxCof.Size = new System.Drawing.Size(73, 21);
            this.cbxCof.TabIndex = 46;
            // 
            // lblCof
            // 
            this.lblCof.Location = new System.Drawing.Point(398, 327);
            this.lblCof.Name = "lblCof";
            this.lblCof.Size = new System.Drawing.Size(59, 21);
            this.lblCof.TabIndex = 45;
            this.lblCof.Text = "COF:";
            this.lblCof.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblLaminationQuality
            // 
            this.lblLaminationQuality.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLaminationQuality.Location = new System.Drawing.Point(318, 277);
            this.lblLaminationQuality.Name = "lblLaminationQuality";
            this.lblLaminationQuality.Size = new System.Drawing.Size(218, 21);
            this.lblLaminationQuality.TabIndex = 47;
            this.lblLaminationQuality.Text = "Lamination Quality";
            this.lblLaminationQuality.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // rtbComments
            // 
            this.rtbComments.Location = new System.Drawing.Point(398, 409);
            this.rtbComments.Name = "rtbComments";
            this.rtbComments.Size = new System.Drawing.Size(276, 46);
            this.rtbComments.TabIndex = 49;
            this.rtbComments.Text = "";
            // 
            // lblComments
            // 
            this.lblComments.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComments.Location = new System.Drawing.Point(398, 390);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new System.Drawing.Size(274, 21);
            this.lblComments.TabIndex = 48;
            this.lblComments.Text = "Comments";
            this.lblComments.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnAcceptRoll
            // 
            this.btnAcceptRoll.ForeColor = System.Drawing.Color.Green;
            this.btnAcceptRoll.Location = new System.Drawing.Point(567, 299);
            this.btnAcceptRoll.Name = "btnAcceptRoll";
            this.btnAcceptRoll.Size = new System.Drawing.Size(75, 38);
            this.btnAcceptRoll.TabIndex = 50;
            this.btnAcceptRoll.Text = "Accept Roll";
            this.btnAcceptRoll.UseVisualStyleBackColor = true;
            this.btnAcceptRoll.Click += new System.EventHandler(this.BtnAcceptRollClick);
            // 
            // btnRejectRoll
            // 
            this.btnRejectRoll.ForeColor = System.Drawing.Color.Red;
            this.btnRejectRoll.Location = new System.Drawing.Point(567, 347);
            this.btnRejectRoll.Name = "btnRejectRoll";
            this.btnRejectRoll.Size = new System.Drawing.Size(75, 38);
            this.btnRejectRoll.TabIndex = 51;
            this.btnRejectRoll.Text = "Reject Roll";
            this.btnRejectRoll.UseVisualStyleBackColor = true;
            this.btnRejectRoll.Click += new System.EventHandler(this.BtnRejectRollClick);
            // 
            // LaminatingQCCheckForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 462);
            this.Controls.Add(this.btnRejectRoll);
            this.Controls.Add(this.btnAcceptRoll);
            this.Controls.Add(this.rtbComments);
            this.Controls.Add(this.lblComments);
            this.Controls.Add(this.lblLaminationQuality);
            this.Controls.Add(this.cbxCof);
            this.Controls.Add(this.lblCof);
            this.Controls.Add(this.cbxCurl);
            this.Controls.Add(this.lblCurl);
            this.Controls.Add(this.cbxSkips);
            this.Controls.Add(this.lblSkips);
            this.Controls.Add(this.cbxStreaks);
            this.Controls.Add(this.lblStreaks);
            this.Controls.Add(this.txtSeal);
            this.Controls.Add(this.lblSeal);
            this.Controls.Add(this.txtBond);
            this.Controls.Add(this.lblBond);
            this.Controls.Add(this.txtAdhesiveWeightGearSize);
            this.Controls.Add(this.lblAdhesiveWeightGearSide);
            this.Controls.Add(this.txtAdhesiveWeightOperatorSide);
            this.Controls.Add(this.lblAdhesiveWeightOperatorSide);
            this.Controls.Add(this.cbxAppearance);
            this.Controls.Add(this.lblAppearance);
            this.Controls.Add(this.lblRawMaterialData);
            this.Controls.Add(this.lblDimensionsInInches);
            this.Controls.Add(this.cbxCoating);
            this.Controls.Add(this.lblCoating);
            this.Controls.Add(this.txtFilmGauge);
            this.Controls.Add(this.lblGauge);
            this.Controls.Add(this.txtPrintRepeat);
            this.Controls.Add(this.lblPrintRepeat);
            this.Controls.Add(this.txtGlueWidth);
            this.Controls.Add(this.lblGlueWidth);
            this.Controls.Add(this.rtbJobInformation);
            this.Name = "LaminatingQCCheckForm";
            this.Text = "Laminating QC Check";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LaminatingQCCheckFormFormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
