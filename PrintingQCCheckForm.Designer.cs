/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 9/29/2011
 * Time: 1:40 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ShopFloor
{
	public partial class PrintingQCCheckForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ComboBox cbxStreaks;
		private System.Windows.Forms.ComboBox cbxUpc;
		private System.Windows.Forms.Label lblUpc;
		private System.Windows.Forms.ComboBox cbxGc;
		private System.Windows.Forms.Label lblGc;
		private System.Windows.Forms.Button btnRejectRoll;
		private System.Windows.Forms.Button btnAcceptRoll;
		private System.Windows.Forms.Label lblPrintQuality;
		private System.Windows.Forms.Label lblComments;
		private System.Windows.Forms.ComboBox cbxRegister;
		private System.Windows.Forms.Label lblWhiteInkOpacity;
		private System.Windows.Forms.Label lblStreaks;
		private System.Windows.Forms.Label lblRegister;
		private System.Windows.Forms.Label lblGhosting;
		private System.Windows.Forms.ComboBox cbxGhosting;
		private System.Windows.Forms.Label lblPinholes;
		private System.Windows.Forms.ComboBox cbxPinholes;
		private System.Windows.Forms.Label lblDirtyPrint;
		private System.Windows.Forms.ComboBox cbxDirtyPrint;
		private System.Windows.Forms.Label lblPrintMiss;
		private System.Windows.Forms.ComboBox cbxPrintMiss;
		private System.Windows.Forms.Label lblColorInSpec;
		private System.Windows.Forms.ComboBox cbxColorInSpec;
		private System.Windows.Forms.Label lblInkAdhesion;
		private System.Windows.Forms.TextBox txtInkAdhesion;
		private System.Windows.Forms.Label lblRawMaterialData;
		private System.Windows.Forms.Label lblDimensionsInInches;
		private System.Windows.Forms.Label lblCoating;
		private System.Windows.Forms.ComboBox cbxCoating;
		private System.Windows.Forms.ComboBox cbxSeal;
		private System.Windows.Forms.Label lblSeal;
		private System.Windows.Forms.Label lblTreatment;
		private System.Windows.Forms.TextBox txtTreatment;
		private System.Windows.Forms.Label lblGauge;
		private System.Windows.Forms.Label lblPrintRepeat;
		private System.Windows.Forms.TextBox txtPrintRepeat;
		private System.Windows.Forms.TextBox txtPrintedFilmWidth;
		private System.Windows.Forms.Label lblPrintedFilmWidth;
		private System.Windows.Forms.RichTextBox rtbJobInformation;
		private System.Windows.Forms.RichTextBox rtbComments;
		private System.Windows.Forms.TextBox txtFilmGauge;
		
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
            this.lblPrintedFilmWidth = new System.Windows.Forms.Label();
            this.txtPrintedFilmWidth = new System.Windows.Forms.TextBox();
            this.txtPrintRepeat = new System.Windows.Forms.TextBox();
            this.lblPrintRepeat = new System.Windows.Forms.Label();
            this.txtFilmGauge = new System.Windows.Forms.TextBox();
            this.lblGauge = new System.Windows.Forms.Label();
            this.txtTreatment = new System.Windows.Forms.TextBox();
            this.lblTreatment = new System.Windows.Forms.Label();
            this.lblSeal = new System.Windows.Forms.Label();
            this.cbxSeal = new System.Windows.Forms.ComboBox();
            this.cbxCoating = new System.Windows.Forms.ComboBox();
            this.lblCoating = new System.Windows.Forms.Label();
            this.lblDimensionsInInches = new System.Windows.Forms.Label();
            this.lblRawMaterialData = new System.Windows.Forms.Label();
            this.txtInkAdhesion = new System.Windows.Forms.TextBox();
            this.lblInkAdhesion = new System.Windows.Forms.Label();
            this.cbxColorInSpec = new System.Windows.Forms.ComboBox();
            this.lblColorInSpec = new System.Windows.Forms.Label();
            this.cbxPrintMiss = new System.Windows.Forms.ComboBox();
            this.lblPrintMiss = new System.Windows.Forms.Label();
            this.cbxDirtyPrint = new System.Windows.Forms.ComboBox();
            this.lblDirtyPrint = new System.Windows.Forms.Label();
            this.cbxPinholes = new System.Windows.Forms.ComboBox();
            this.lblPinholes = new System.Windows.Forms.Label();
            this.cbxGhosting = new System.Windows.Forms.ComboBox();
            this.lblGhosting = new System.Windows.Forms.Label();
            this.cbxRegister = new System.Windows.Forms.ComboBox();
            this.lblRegister = new System.Windows.Forms.Label();
            this.cbxStreaks = new System.Windows.Forms.ComboBox();
            this.lblStreaks = new System.Windows.Forms.Label();
            this.cbxUpc = new System.Windows.Forms.ComboBox();
            this.lblUpc = new System.Windows.Forms.Label();
            this.cbxGc = new System.Windows.Forms.ComboBox();
            this.lblGc = new System.Windows.Forms.Label();
            this.lblWhiteInkOpacity = new System.Windows.Forms.Label();
            this.lblComments = new System.Windows.Forms.Label();
            this.rtbComments = new System.Windows.Forms.RichTextBox();
            this.lblPrintQuality = new System.Windows.Forms.Label();
            this.btnAcceptRoll = new System.Windows.Forms.Button();
            this.btnRejectRoll = new System.Windows.Forms.Button();
            this.OpacityAlert = new System.Windows.Forms.Label();
            this.txtWhiteInkOpacity = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtbJobInformation
            // 
            this.rtbJobInformation.BackColor = System.Drawing.SystemColors.Control;
            this.rtbJobInformation.ForeColor = System.Drawing.Color.Blue;
            this.rtbJobInformation.Location = new System.Drawing.Point(15, 9);
            this.rtbJobInformation.Name = "rtbJobInformation";
            this.rtbJobInformation.ReadOnly = true;
            this.rtbJobInformation.Size = new System.Drawing.Size(659, 249);
            this.rtbJobInformation.TabIndex = 0;
            this.rtbJobInformation.Text = "";
            // 
            // lblPrintedFilmWidth
            // 
            this.lblPrintedFilmWidth.Location = new System.Drawing.Point(15, 286);
            this.lblPrintedFilmWidth.Name = "lblPrintedFilmWidth";
            this.lblPrintedFilmWidth.Size = new System.Drawing.Size(98, 21);
            this.lblPrintedFilmWidth.TabIndex = 1;
            this.lblPrintedFilmWidth.Text = "Printed Film Width:";
            this.lblPrintedFilmWidth.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtPrintedFilmWidth
            // 
            this.txtPrintedFilmWidth.Location = new System.Drawing.Point(119, 283);
            this.txtPrintedFilmWidth.Name = "txtPrintedFilmWidth";
            this.txtPrintedFilmWidth.Size = new System.Drawing.Size(51, 20);
            this.txtPrintedFilmWidth.TabIndex = 2;
            this.txtPrintedFilmWidth.Text = "0.0000";
            this.txtPrintedFilmWidth.Enter += new System.EventHandler(this.TxtNumbersorDecimalEnter);
            this.txtPrintedFilmWidth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtNumbersKeyDown);
            this.txtPrintedFilmWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumbersorDecimalKeyPress);
            this.txtPrintedFilmWidth.Leave += new System.EventHandler(this.TxtNumbersorDecimalLeave);
            // 
            // txtPrintRepeat
            // 
            this.txtPrintRepeat.Location = new System.Drawing.Point(119, 303);
            this.txtPrintRepeat.Name = "txtPrintRepeat";
            this.txtPrintRepeat.Size = new System.Drawing.Size(51, 20);
            this.txtPrintRepeat.TabIndex = 4;
            this.txtPrintRepeat.Text = "0.0000";
            this.txtPrintRepeat.Enter += new System.EventHandler(this.TxtNumbersorDecimalEnter);
            this.txtPrintRepeat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtNumbersKeyDown);
            this.txtPrintRepeat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumbersorDecimalKeyPress);
            this.txtPrintRepeat.Leave += new System.EventHandler(this.TxtNumbersorDecimalLeave);
            // 
            // lblPrintRepeat
            // 
            this.lblPrintRepeat.Location = new System.Drawing.Point(15, 306);
            this.lblPrintRepeat.Name = "lblPrintRepeat";
            this.lblPrintRepeat.Size = new System.Drawing.Size(98, 21);
            this.lblPrintRepeat.TabIndex = 3;
            this.lblPrintRepeat.Text = "Print Repeat:";
            this.lblPrintRepeat.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtFilmGauge
            // 
            this.txtFilmGauge.Location = new System.Drawing.Point(119, 363);
            this.txtFilmGauge.Name = "txtFilmGauge";
            this.txtFilmGauge.Size = new System.Drawing.Size(51, 20);
            this.txtFilmGauge.TabIndex = 6;
            this.txtFilmGauge.Text = "0.000";
            this.txtFilmGauge.Enter += new System.EventHandler(this.TxtNumbersorDecimalEnter);
            this.txtFilmGauge.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtNumbersKeyDown);
            this.txtFilmGauge.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtFilmGaugeKeyPress);
            this.txtFilmGauge.Leave += new System.EventHandler(this.TxtFilmGaugeLeave);
            // 
            // lblGauge
            // 
            this.lblGauge.Location = new System.Drawing.Point(15, 366);
            this.lblGauge.Name = "lblGauge";
            this.lblGauge.Size = new System.Drawing.Size(98, 21);
            this.lblGauge.TabIndex = 5;
            this.lblGauge.Text = "Gauge (mils):";
            this.lblGauge.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtTreatment
            // 
            this.txtTreatment.Location = new System.Drawing.Point(119, 383);
            this.txtTreatment.Name = "txtTreatment";
            this.txtTreatment.Size = new System.Drawing.Size(51, 20);
            this.txtTreatment.TabIndex = 8;
            this.txtTreatment.Text = "40";
            this.txtTreatment.Enter += new System.EventHandler(this.TxtNumbersEnter);
            this.txtTreatment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtNumbersKeyDown);
            this.txtTreatment.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumbersKeyPress);
            this.txtTreatment.Leave += new System.EventHandler(this.TxtNumbersLeave);
            // 
            // lblTreatment
            // 
            this.lblTreatment.Location = new System.Drawing.Point(15, 386);
            this.lblTreatment.Name = "lblTreatment";
            this.lblTreatment.Size = new System.Drawing.Size(98, 21);
            this.lblTreatment.TabIndex = 7;
            this.lblTreatment.Text = "Treatment:";
            this.lblTreatment.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblSeal
            // 
            this.lblSeal.Location = new System.Drawing.Point(15, 411);
            this.lblSeal.Name = "lblSeal";
            this.lblSeal.Size = new System.Drawing.Size(76, 21);
            this.lblSeal.TabIndex = 9;
            this.lblSeal.Text = "Seal:";
            this.lblSeal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbxSeal
            // 
            this.cbxSeal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSeal.FormattingEnabled = true;
            this.cbxSeal.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.cbxSeal.Location = new System.Drawing.Point(97, 408);
            this.cbxSeal.Name = "cbxSeal";
            this.cbxSeal.Size = new System.Drawing.Size(73, 21);
            this.cbxSeal.TabIndex = 10;
            // 
            // cbxCoating
            // 
            this.cbxCoating.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCoating.FormattingEnabled = true;
            this.cbxCoating.Items.AddRange(new object[] {
            "None",
            "Acrylic",
            "Saran"});
            this.cbxCoating.Location = new System.Drawing.Point(97, 433);
            this.cbxCoating.Name = "cbxCoating";
            this.cbxCoating.Size = new System.Drawing.Size(73, 21);
            this.cbxCoating.TabIndex = 12;
            // 
            // lblCoating
            // 
            this.lblCoating.Location = new System.Drawing.Point(15, 436);
            this.lblCoating.Name = "lblCoating";
            this.lblCoating.Size = new System.Drawing.Size(76, 21);
            this.lblCoating.TabIndex = 11;
            this.lblCoating.Text = "Coating:";
            this.lblCoating.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblDimensionsInInches
            // 
            this.lblDimensionsInInches.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDimensionsInInches.Location = new System.Drawing.Point(15, 261);
            this.lblDimensionsInInches.Name = "lblDimensionsInInches";
            this.lblDimensionsInInches.Size = new System.Drawing.Size(155, 21);
            this.lblDimensionsInInches.TabIndex = 13;
            this.lblDimensionsInInches.Text = "Dimension in Inches";
            this.lblDimensionsInInches.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblRawMaterialData
            // 
            this.lblRawMaterialData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRawMaterialData.Location = new System.Drawing.Point(15, 341);
            this.lblRawMaterialData.Name = "lblRawMaterialData";
            this.lblRawMaterialData.Size = new System.Drawing.Size(155, 21);
            this.lblRawMaterialData.TabIndex = 14;
            this.lblRawMaterialData.Text = "Raw Material Data";
            this.lblRawMaterialData.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtInkAdhesion
            // 
            this.txtInkAdhesion.Location = new System.Drawing.Point(302, 283);
            this.txtInkAdhesion.Name = "txtInkAdhesion";
            this.txtInkAdhesion.Size = new System.Drawing.Size(51, 20);
            this.txtInkAdhesion.TabIndex = 16;
            this.txtInkAdhesion.Text = "70";
            this.txtInkAdhesion.Enter += new System.EventHandler(this.TxtNumbersEnter);
            this.txtInkAdhesion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtNumbersKeyDown);
            this.txtInkAdhesion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumbersKeyPress);
            this.txtInkAdhesion.Leave += new System.EventHandler(this.TxtNumbersLeave);
            // 
            // lblInkAdhesion
            // 
            this.lblInkAdhesion.Location = new System.Drawing.Point(198, 286);
            this.lblInkAdhesion.Name = "lblInkAdhesion";
            this.lblInkAdhesion.Size = new System.Drawing.Size(98, 21);
            this.lblInkAdhesion.TabIndex = 15;
            this.lblInkAdhesion.Text = "Ink Adhesion %:";
            this.lblInkAdhesion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbxColorInSpec
            // 
            this.cbxColorInSpec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxColorInSpec.FormattingEnabled = true;
            this.cbxColorInSpec.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.cbxColorInSpec.Location = new System.Drawing.Point(280, 308);
            this.cbxColorInSpec.Name = "cbxColorInSpec";
            this.cbxColorInSpec.Size = new System.Drawing.Size(73, 21);
            this.cbxColorInSpec.TabIndex = 18;
            // 
            // lblColorInSpec
            // 
            this.lblColorInSpec.Location = new System.Drawing.Point(198, 311);
            this.lblColorInSpec.Name = "lblColorInSpec";
            this.lblColorInSpec.Size = new System.Drawing.Size(76, 21);
            this.lblColorInSpec.TabIndex = 17;
            this.lblColorInSpec.Text = "Color in Spec.:";
            this.lblColorInSpec.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbxPrintMiss
            // 
            this.cbxPrintMiss.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPrintMiss.FormattingEnabled = true;
            this.cbxPrintMiss.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.cbxPrintMiss.Location = new System.Drawing.Point(280, 333);
            this.cbxPrintMiss.Name = "cbxPrintMiss";
            this.cbxPrintMiss.Size = new System.Drawing.Size(73, 21);
            this.cbxPrintMiss.TabIndex = 20;
            // 
            // lblPrintMiss
            // 
            this.lblPrintMiss.Location = new System.Drawing.Point(198, 336);
            this.lblPrintMiss.Name = "lblPrintMiss";
            this.lblPrintMiss.Size = new System.Drawing.Size(76, 21);
            this.lblPrintMiss.TabIndex = 19;
            this.lblPrintMiss.Text = "Print Miss:";
            this.lblPrintMiss.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbxDirtyPrint
            // 
            this.cbxDirtyPrint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDirtyPrint.FormattingEnabled = true;
            this.cbxDirtyPrint.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.cbxDirtyPrint.Location = new System.Drawing.Point(280, 358);
            this.cbxDirtyPrint.Name = "cbxDirtyPrint";
            this.cbxDirtyPrint.Size = new System.Drawing.Size(73, 21);
            this.cbxDirtyPrint.TabIndex = 22;
            // 
            // lblDirtyPrint
            // 
            this.lblDirtyPrint.Location = new System.Drawing.Point(198, 361);
            this.lblDirtyPrint.Name = "lblDirtyPrint";
            this.lblDirtyPrint.Size = new System.Drawing.Size(76, 21);
            this.lblDirtyPrint.TabIndex = 21;
            this.lblDirtyPrint.Text = "Dirty Print:";
            this.lblDirtyPrint.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbxPinholes
            // 
            this.cbxPinholes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPinholes.FormattingEnabled = true;
            this.cbxPinholes.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.cbxPinholes.Location = new System.Drawing.Point(280, 383);
            this.cbxPinholes.Name = "cbxPinholes";
            this.cbxPinholes.Size = new System.Drawing.Size(73, 21);
            this.cbxPinholes.TabIndex = 24;
            // 
            // lblPinholes
            // 
            this.lblPinholes.Location = new System.Drawing.Point(198, 386);
            this.lblPinholes.Name = "lblPinholes";
            this.lblPinholes.Size = new System.Drawing.Size(76, 21);
            this.lblPinholes.TabIndex = 23;
            this.lblPinholes.Text = "Pinholes:";
            this.lblPinholes.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbxGhosting
            // 
            this.cbxGhosting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxGhosting.FormattingEnabled = true;
            this.cbxGhosting.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.cbxGhosting.Location = new System.Drawing.Point(280, 408);
            this.cbxGhosting.Name = "cbxGhosting";
            this.cbxGhosting.Size = new System.Drawing.Size(73, 21);
            this.cbxGhosting.TabIndex = 26;
            // 
            // lblGhosting
            // 
            this.lblGhosting.Location = new System.Drawing.Point(198, 411);
            this.lblGhosting.Name = "lblGhosting";
            this.lblGhosting.Size = new System.Drawing.Size(76, 21);
            this.lblGhosting.TabIndex = 25;
            this.lblGhosting.Text = "Ghosting:";
            this.lblGhosting.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbxRegister
            // 
            this.cbxRegister.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxRegister.FormattingEnabled = true;
            this.cbxRegister.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.cbxRegister.Location = new System.Drawing.Point(280, 433);
            this.cbxRegister.Name = "cbxRegister";
            this.cbxRegister.Size = new System.Drawing.Size(73, 21);
            this.cbxRegister.TabIndex = 28;
            // 
            // lblRegister
            // 
            this.lblRegister.Location = new System.Drawing.Point(198, 436);
            this.lblRegister.Name = "lblRegister";
            this.lblRegister.Size = new System.Drawing.Size(76, 21);
            this.lblRegister.TabIndex = 27;
            this.lblRegister.Text = "Register:";
            this.lblRegister.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbxStreaks
            // 
            this.cbxStreaks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxStreaks.FormattingEnabled = true;
            this.cbxStreaks.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.cbxStreaks.Location = new System.Drawing.Point(454, 283);
            this.cbxStreaks.Name = "cbxStreaks";
            this.cbxStreaks.Size = new System.Drawing.Size(73, 21);
            this.cbxStreaks.TabIndex = 30;
            // 
            // lblStreaks
            // 
            this.lblStreaks.Location = new System.Drawing.Point(359, 286);
            this.lblStreaks.Name = "lblStreaks";
            this.lblStreaks.Size = new System.Drawing.Size(89, 21);
            this.lblStreaks.TabIndex = 29;
            this.lblStreaks.Text = "Streaks:";
            this.lblStreaks.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbxUpc
            // 
            this.cbxUpc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxUpc.FormattingEnabled = true;
            this.cbxUpc.Items.AddRange(new object[] {
            "Pass",
            "Fail"});
            this.cbxUpc.Location = new System.Drawing.Point(454, 308);
            this.cbxUpc.Name = "cbxUpc";
            this.cbxUpc.Size = new System.Drawing.Size(73, 21);
            this.cbxUpc.TabIndex = 32;
            // 
            // lblUpc
            // 
            this.lblUpc.Location = new System.Drawing.Point(359, 311);
            this.lblUpc.Name = "lblUpc";
            this.lblUpc.Size = new System.Drawing.Size(89, 21);
            this.lblUpc.TabIndex = 31;
            this.lblUpc.Text = "UPC:";
            this.lblUpc.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbxGc
            // 
            this.cbxGc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxGc.FormattingEnabled = true;
            this.cbxGc.Items.AddRange(new object[] {
            "Pass",
            "Fail"});
            this.cbxGc.Location = new System.Drawing.Point(454, 333);
            this.cbxGc.Name = "cbxGc";
            this.cbxGc.Size = new System.Drawing.Size(73, 21);
            this.cbxGc.TabIndex = 34;
            // 
            // lblGc
            // 
            this.lblGc.Location = new System.Drawing.Point(359, 336);
            this.lblGc.Name = "lblGc";
            this.lblGc.Size = new System.Drawing.Size(89, 21);
            this.lblGc.TabIndex = 33;
            this.lblGc.Text = "GC:";
            this.lblGc.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblWhiteInkOpacity
            // 
            this.lblWhiteInkOpacity.Location = new System.Drawing.Point(359, 361);
            this.lblWhiteInkOpacity.Name = "lblWhiteInkOpacity";
            this.lblWhiteInkOpacity.Size = new System.Drawing.Size(111, 21);
            this.lblWhiteInkOpacity.TabIndex = 35;
            this.lblWhiteInkOpacity.Text = "White Ink Opacity %:";
            this.lblWhiteInkOpacity.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblComments
            // 
            this.lblComments.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComments.Location = new System.Drawing.Point(359, 386);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new System.Drawing.Size(301, 21);
            this.lblComments.TabIndex = 37;
            this.lblComments.Text = "Comments";
            this.lblComments.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // rtbComments
            // 
            this.rtbComments.Location = new System.Drawing.Point(359, 405);
            this.rtbComments.Name = "rtbComments";
            this.rtbComments.Size = new System.Drawing.Size(301, 49);
            this.rtbComments.TabIndex = 38;
            this.rtbComments.Text = "";
            // 
            // lblPrintQuality
            // 
            this.lblPrintQuality.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrintQuality.Location = new System.Drawing.Point(280, 261);
            this.lblPrintQuality.Name = "lblPrintQuality";
            this.lblPrintQuality.Size = new System.Drawing.Size(247, 21);
            this.lblPrintQuality.TabIndex = 39;
            this.lblPrintQuality.Text = "Print Quality";
            this.lblPrintQuality.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnAcceptRoll
            // 
            this.btnAcceptRoll.ForeColor = System.Drawing.Color.Green;
            this.btnAcceptRoll.Location = new System.Drawing.Point(567, 261);
            this.btnAcceptRoll.Name = "btnAcceptRoll";
            this.btnAcceptRoll.Size = new System.Drawing.Size(75, 38);
            this.btnAcceptRoll.TabIndex = 40;
            this.btnAcceptRoll.Text = "Accept Roll";
            this.btnAcceptRoll.UseVisualStyleBackColor = true;
            this.btnAcceptRoll.Click += new System.EventHandler(this.BtnAcceptRollClick);
            // 
            // btnRejectRoll
            // 
            this.btnRejectRoll.ForeColor = System.Drawing.Color.Red;
            this.btnRejectRoll.Location = new System.Drawing.Point(567, 305);
            this.btnRejectRoll.Name = "btnRejectRoll";
            this.btnRejectRoll.Size = new System.Drawing.Size(75, 38);
            this.btnRejectRoll.TabIndex = 41;
            this.btnRejectRoll.Text = "Reject Roll";
            this.btnRejectRoll.UseVisualStyleBackColor = true;
            this.btnRejectRoll.Click += new System.EventHandler(this.BtnRejectRollClick);
            // 
            // OpacityAlert
            // 
            this.OpacityAlert.AutoSize = true;
            this.OpacityAlert.CausesValidation = false;
            this.OpacityAlert.ForeColor = System.Drawing.Color.Red;
            this.OpacityAlert.Location = new System.Drawing.Point(371, 382);
            this.OpacityAlert.Name = "OpacityAlert";
            this.OpacityAlert.Size = new System.Drawing.Size(86, 13);
            this.OpacityAlert.TabIndex = 42;
            this.OpacityAlert.Text = "60% Opacity Min";
            this.OpacityAlert.Visible = false;
            // 
            // txtWhiteInkOpacity
            // 
            this.txtWhiteInkOpacity.Location = new System.Drawing.Point(476, 358);
            this.txtWhiteInkOpacity.Name = "txtWhiteInkOpacity";
            this.txtWhiteInkOpacity.Size = new System.Drawing.Size(51, 20);
            this.txtWhiteInkOpacity.TabIndex = 36;
            this.txtWhiteInkOpacity.Text = "60";
            this.txtWhiteInkOpacity.TextChanged += new System.EventHandler(this.txtWhiteInkOpacity_TextChanged);
            this.txtWhiteInkOpacity.Enter += new System.EventHandler(this.TxtNumbersEnter);
            this.txtWhiteInkOpacity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtNumbersKeyDown);
            this.txtWhiteInkOpacity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumbersKeyPress);
            this.txtWhiteInkOpacity.Leave += new System.EventHandler(this.TxtNumbersLeave);
            this.txtWhiteInkOpacity.Validating += new System.ComponentModel.CancelEventHandler(this.IsOpacityValid);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(567, 350);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 43;
            this.button1.Text = "Print Opacity";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PrintOpacity);
            // 
            // PrintingQCCheckForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 462);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.OpacityAlert);
            this.Controls.Add(this.btnRejectRoll);
            this.Controls.Add(this.btnAcceptRoll);
            this.Controls.Add(this.lblPrintQuality);
            this.Controls.Add(this.rtbComments);
            this.Controls.Add(this.lblComments);
            this.Controls.Add(this.txtWhiteInkOpacity);
            this.Controls.Add(this.lblWhiteInkOpacity);
            this.Controls.Add(this.cbxGc);
            this.Controls.Add(this.lblGc);
            this.Controls.Add(this.cbxUpc);
            this.Controls.Add(this.lblUpc);
            this.Controls.Add(this.cbxStreaks);
            this.Controls.Add(this.lblStreaks);
            this.Controls.Add(this.cbxRegister);
            this.Controls.Add(this.lblRegister);
            this.Controls.Add(this.cbxGhosting);
            this.Controls.Add(this.lblGhosting);
            this.Controls.Add(this.cbxPinholes);
            this.Controls.Add(this.lblPinholes);
            this.Controls.Add(this.cbxDirtyPrint);
            this.Controls.Add(this.lblDirtyPrint);
            this.Controls.Add(this.cbxPrintMiss);
            this.Controls.Add(this.lblPrintMiss);
            this.Controls.Add(this.cbxColorInSpec);
            this.Controls.Add(this.lblColorInSpec);
            this.Controls.Add(this.txtInkAdhesion);
            this.Controls.Add(this.lblInkAdhesion);
            this.Controls.Add(this.lblRawMaterialData);
            this.Controls.Add(this.lblDimensionsInInches);
            this.Controls.Add(this.cbxCoating);
            this.Controls.Add(this.lblCoating);
            this.Controls.Add(this.cbxSeal);
            this.Controls.Add(this.lblSeal);
            this.Controls.Add(this.txtTreatment);
            this.Controls.Add(this.lblTreatment);
            this.Controls.Add(this.txtFilmGauge);
            this.Controls.Add(this.lblGauge);
            this.Controls.Add(this.txtPrintRepeat);
            this.Controls.Add(this.lblPrintRepeat);
            this.Controls.Add(this.txtPrintedFilmWidth);
            this.Controls.Add(this.lblPrintedFilmWidth);
            this.Controls.Add(this.rtbJobInformation);
            this.Name = "PrintingQCCheckForm";
            this.Text = "Printing QC Check";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PrintingQCCheckFormFormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private System.Windows.Forms.Label OpacityAlert;
        private System.Windows.Forms.TextBox txtWhiteInkOpacity;
        private System.Windows.Forms.Button button1;
    }
}
