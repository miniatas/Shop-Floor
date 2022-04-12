using System;
using System.Drawing;
using System.Windows.Forms;
using UserControlsForShopFloor.PropertyClasses;

namespace ShopFloor
{
    partial class PlateCreationEntry
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        public enum SqlQuerryTypes { Insert, Update, Delete }

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SearchResultCombo = new System.Windows.Forms.ComboBox();
            this.SearchByCombo = new System.Windows.Forms.ComboBox();
            this.SearchByText = new System.Windows.Forms.TextBox();
            this.SearchByButton = new System.Windows.Forms.Button();
            this.SearchResultLabel = new System.Windows.Forms.Label();
            this.lineShapeSearchDivider = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.SaveTiffButton = new System.Windows.Forms.Button();
            this.RemoveTiffButton = new System.Windows.Forms.Button();
            this.CroppedWidthText = new System.Windows.Forms.TextBox();
            this.CroppedWidthLabel = new System.Windows.Forms.Label();
            this.CroppedLengthText = new System.Windows.Forms.TextBox();
            this.CroppedLengthLabel = new System.Windows.Forms.Label();
            this.TiffWidthText = new System.Windows.Forms.TextBox();
            this.TiffWidthLabel = new System.Windows.Forms.Label();
            this.PlateNumberLabel = new System.Windows.Forms.Label();
            this.TiffLengthText = new System.Windows.Forms.TextBox();
            this.SelectPlateCombo = new System.Windows.Forms.ComboBox();
            this.TiffLengthLabel = new System.Windows.Forms.Label();
            this.SearchErrorLabel = new System.Windows.Forms.Label();
            this.SaveErrorLabel = new System.Windows.Forms.Label();
            this.EdgeScrap = new System.Windows.Forms.Label();
            this.EdgeScrapText = new System.Windows.Forms.TextBox();
            this.plateTiffInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.plateDBSaveLogicBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.plateTillTiffCombosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.plateDBSaveLogicBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.plateTiffInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.plateDBSaveLogicBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.plateTillTiffCombosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.plateDBSaveLogicBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // SearchResultCombo
            // 
            this.SearchResultCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SearchResultCombo.FormattingEnabled = true;
            this.SearchResultCombo.Location = new System.Drawing.Point(138, 57);
            this.SearchResultCombo.Name = "SearchResultCombo";
            this.SearchResultCombo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SearchResultCombo.Size = new System.Drawing.Size(1032, 21);
            this.SearchResultCombo.TabIndex = 25;
            this.SearchResultCombo.SelectedIndexChanged += new System.EventHandler(this.SearchResultCombo_SelectedIndexChanged);
            // 
            // SearchByCombo
            // 
            this.SearchByCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SearchByCombo.FormattingEnabled = true;
            this.SearchByCombo.Location = new System.Drawing.Point(402, 13);
            this.SearchByCombo.Name = "SearchByCombo";
            this.SearchByCombo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SearchByCombo.Size = new System.Drawing.Size(121, 21);
            this.SearchByCombo.TabIndex = 26;
            // 
            // SearchByText
            // 
            this.SearchByText.Location = new System.Drawing.Point(540, 13);
            this.SearchByText.Name = "SearchByText";
            this.SearchByText.Size = new System.Drawing.Size(175, 20);
            this.SearchByText.TabIndex = 27;
            this.SearchByText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchByText_KeyDown);
            this.SearchByText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckForNumbersNoSpaces);
            // 
            // SearchByButton
            // 
            this.SearchByButton.Location = new System.Drawing.Point(734, 11);
            this.SearchByButton.Name = "SearchByButton";
            this.SearchByButton.Size = new System.Drawing.Size(75, 23);
            this.SearchByButton.TabIndex = 28;
            this.SearchByButton.Text = "Search";
            this.SearchByButton.UseVisualStyleBackColor = true;
            this.SearchByButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // SearchResultLabel
            // 
            this.SearchResultLabel.AutoSize = true;
            this.SearchResultLabel.Location = new System.Drawing.Point(28, 60);
            this.SearchResultLabel.Name = "SearchResultLabel";
            this.SearchResultLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SearchResultLabel.Size = new System.Drawing.Size(77, 13);
            this.SearchResultLabel.TabIndex = 29;
            this.SearchResultLabel.Text = "Search Result:";
            // 
            // lineShapeSearchDivider
            // 
            this.lineShapeSearchDivider.Name = "lineShapeSearchDivider";
            this.lineShapeSearchDivider.X1 = 27;
            this.lineShapeSearchDivider.X2 = 1174;
            this.lineShapeSearchDivider.Y1 = 95;
            this.lineShapeSearchDivider.Y2 = 97;
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShapeSearchDivider});
            this.shapeContainer1.Size = new System.Drawing.Size(1243, 304);
            this.shapeContainer1.TabIndex = 30;
            this.shapeContainer1.TabStop = false;
            // 
            // SaveTiffButton
            // 
            this.SaveTiffButton.Location = new System.Drawing.Point(1095, 255);
            this.SaveTiffButton.Name = "SaveTiffButton";
            this.SaveTiffButton.Size = new System.Drawing.Size(75, 23);
            this.SaveTiffButton.TabIndex = 42;
            this.SaveTiffButton.Text = "Save";
            this.SaveTiffButton.UseVisualStyleBackColor = true;
            this.SaveTiffButton.Click += new System.EventHandler(this.SaveTiffButton_Click);
            // 
            // RemoveTiffButton
            // 
            this.RemoveTiffButton.Location = new System.Drawing.Point(31, 255);
            this.RemoveTiffButton.Name = "RemoveTiffButton";
            this.RemoveTiffButton.Size = new System.Drawing.Size(75, 23);
            this.RemoveTiffButton.TabIndex = 41;
            this.RemoveTiffButton.Text = "Remove Tiff";
            this.RemoveTiffButton.UseVisualStyleBackColor = true;
            this.RemoveTiffButton.Click += new System.EventHandler(this.RemoveTiffButton_Click);
            // 
            // CroppedWidthText
            // 
            this.CroppedWidthText.Location = new System.Drawing.Point(381, 208);
            this.CroppedWidthText.Name = "CroppedWidthText";
            this.CroppedWidthText.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CroppedWidthText.Size = new System.Drawing.Size(100, 20);
            this.CroppedWidthText.TabIndex = 40;
            this.CroppedWidthText.TextChanged += new System.EventHandler(this.TextBoxChanged);
            this.CroppedWidthText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckForDecimal_KeyPress);
            // 
            // CroppedWidthLabel
            // 
            this.CroppedWidthLabel.AutoSize = true;
            this.CroppedWidthLabel.Location = new System.Drawing.Point(285, 211);
            this.CroppedWidthLabel.Name = "CroppedWidthLabel";
            this.CroppedWidthLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CroppedWidthLabel.Size = new System.Drawing.Size(81, 13);
            this.CroppedWidthLabel.TabIndex = 39;
            this.CroppedWidthLabel.Text = "Cropped Width:";
            // 
            // CroppedLengthText
            // 
            this.CroppedLengthText.Location = new System.Drawing.Point(138, 208);
            this.CroppedLengthText.Name = "CroppedLengthText";
            this.CroppedLengthText.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CroppedLengthText.Size = new System.Drawing.Size(100, 20);
            this.CroppedLengthText.TabIndex = 38;
            this.CroppedLengthText.TextChanged += new System.EventHandler(this.TextBoxChanged);
            this.CroppedLengthText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckForDecimal_KeyPress);
            // 
            // CroppedLengthLabel
            // 
            this.CroppedLengthLabel.AutoSize = true;
            this.CroppedLengthLabel.Location = new System.Drawing.Point(30, 208);
            this.CroppedLengthLabel.Name = "CroppedLengthLabel";
            this.CroppedLengthLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CroppedLengthLabel.Size = new System.Drawing.Size(86, 13);
            this.CroppedLengthLabel.TabIndex = 37;
            this.CroppedLengthLabel.Text = "Cropped Length:";
            // 
            // TiffWidthText
            // 
            this.TiffWidthText.Location = new System.Drawing.Point(381, 161);
            this.TiffWidthText.Name = "TiffWidthText";
            this.TiffWidthText.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TiffWidthText.Size = new System.Drawing.Size(100, 20);
            this.TiffWidthText.TabIndex = 36;
            this.TiffWidthText.TextChanged += new System.EventHandler(this.TextBoxChanged);
            this.TiffWidthText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckForDecimal_KeyPress);
            // 
            // TiffWidthLabel
            // 
            this.TiffWidthLabel.AutoSize = true;
            this.TiffWidthLabel.Location = new System.Drawing.Point(310, 165);
            this.TiffWidthLabel.Name = "TiffWidthLabel";
            this.TiffWidthLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TiffWidthLabel.Size = new System.Drawing.Size(56, 13);
            this.TiffWidthLabel.TabIndex = 35;
            this.TiffWidthLabel.Text = "Tiff Width:";
            // 
            // PlateNumberLabel
            // 
            this.PlateNumberLabel.AutoSize = true;
            this.PlateNumberLabel.Location = new System.Drawing.Point(30, 122);
            this.PlateNumberLabel.Name = "PlateNumberLabel";
            this.PlateNumberLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.PlateNumberLabel.Size = new System.Drawing.Size(41, 13);
            this.PlateNumberLabel.TabIndex = 32;
            this.PlateNumberLabel.Text = "Plate #";
            // 
            // TiffLengthText
            // 
            this.TiffLengthText.Location = new System.Drawing.Point(138, 165);
            this.TiffLengthText.Name = "TiffLengthText";
            this.TiffLengthText.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TiffLengthText.Size = new System.Drawing.Size(100, 20);
            this.TiffLengthText.TabIndex = 34;
            this.TiffLengthText.Click += new System.EventHandler(this.TiffLengthText_Click);
            this.TiffLengthText.TextChanged += new System.EventHandler(this.TextBoxChanged);
            this.TiffLengthText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckForDecimal_KeyPress);
            // 
            // SelectPlateCombo
            // 
            this.SelectPlateCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectPlateCombo.FormattingEnabled = true;
            this.SelectPlateCombo.Location = new System.Drawing.Point(138, 119);
            this.SelectPlateCombo.Name = "SelectPlateCombo";
            this.SelectPlateCombo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SelectPlateCombo.Size = new System.Drawing.Size(343, 21);
            this.SelectPlateCombo.TabIndex = 31;
            this.SelectPlateCombo.SelectedIndexChanged += new System.EventHandler(this.SelectPlateCombo_SelectedIndexChanged);
            // 
            // TiffLengthLabel
            // 
            this.TiffLengthLabel.AutoSize = true;
            this.TiffLengthLabel.Location = new System.Drawing.Point(30, 168);
            this.TiffLengthLabel.Name = "TiffLengthLabel";
            this.TiffLengthLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TiffLengthLabel.Size = new System.Drawing.Size(61, 13);
            this.TiffLengthLabel.TabIndex = 33;
            this.TiffLengthLabel.Text = "Tiff Length:";
            // 
            // SearchErrorLabel
            // 
            this.SearchErrorLabel.AutoSize = true;
            this.SearchErrorLabel.Location = new System.Drawing.Point(842, 19);
            this.SearchErrorLabel.Name = "SearchErrorLabel";
            this.SearchErrorLabel.Size = new System.Drawing.Size(0, 13);
            this.SearchErrorLabel.TabIndex = 47;
            // 
            // SaveErrorLabel
            // 
            this.SaveErrorLabel.AutoSize = true;
            this.SaveErrorLabel.Location = new System.Drawing.Point(889, 260);
            this.SaveErrorLabel.Name = "SaveErrorLabel";
            this.SaveErrorLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SaveErrorLabel.Size = new System.Drawing.Size(0, 13);
            this.SaveErrorLabel.TabIndex = 48;
            this.SaveErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // EdgeScrap
            // 
            this.EdgeScrap.AutoSize = true;
            this.EdgeScrap.Location = new System.Drawing.Point(614, 122);
            this.EdgeScrap.Name = "EdgeScrap";
            this.EdgeScrap.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.EdgeScrap.Size = new System.Drawing.Size(66, 13);
            this.EdgeScrap.TabIndex = 49;
            this.EdgeScrap.Text = "Edge Scrap:";
            // 
            // EdgeScrapText
            // 
            this.EdgeScrapText.Location = new System.Drawing.Point(696, 119);
            this.EdgeScrapText.Name = "EdgeScrapText";
            this.EdgeScrapText.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.EdgeScrapText.Size = new System.Drawing.Size(100, 20);
            this.EdgeScrapText.TabIndex = 50;
            this.EdgeScrapText.Text = ".5";
            this.EdgeScrapText.TextChanged += new System.EventHandler(this.TextBoxChanged);
            this.EdgeScrapText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckForDecimal_KeyPress);
            this.EdgeScrapText.Validated += new System.EventHandler(this.EdgeScrapText_Validated);
            // 
            // plateTiffInfoBindingSource
            // 
            this.plateTiffInfoBindingSource.DataSource = typeof(UserControlsForShopFloor.Classes.PlateTiffInfo);
            // 
            // plateDBSaveLogicBindingSource
            // 
            this.plateDBSaveLogicBindingSource.DataSource = typeof(UserControlsForShopFloor.BusinessClasses.PlateDBSaveLogic);
            // 
            // plateTillTiffCombosBindingSource
            // 
            this.plateTillTiffCombosBindingSource.DataSource = typeof(UserControlsForShopFloor.PropertyClasses.PlateTillTiffCombos);
            // 
            // plateDBSaveLogicBindingSource1
            // 
            this.plateDBSaveLogicBindingSource1.DataSource = typeof(UserControlsForShopFloor.BusinessClasses.PlateDBSaveLogic);
            // 
            // PlateCreationEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.EdgeScrapText);
            this.Controls.Add(this.EdgeScrap);
            this.Controls.Add(this.SaveErrorLabel);
            this.Controls.Add(this.SearchErrorLabel);
            this.Controls.Add(this.SaveTiffButton);
            this.Controls.Add(this.RemoveTiffButton);
            this.Controls.Add(this.CroppedWidthText);
            this.Controls.Add(this.CroppedWidthLabel);
            this.Controls.Add(this.CroppedLengthText);
            this.Controls.Add(this.CroppedLengthLabel);
            this.Controls.Add(this.TiffWidthText);
            this.Controls.Add(this.TiffWidthLabel);
            this.Controls.Add(this.PlateNumberLabel);
            this.Controls.Add(this.TiffLengthText);
            this.Controls.Add(this.SelectPlateCombo);
            this.Controls.Add(this.TiffLengthLabel);
            this.Controls.Add(this.SearchResultLabel);
            this.Controls.Add(this.SearchByCombo);
            this.Controls.Add(this.SearchByText);
            this.Controls.Add(this.SearchByButton);
            this.Controls.Add(this.SearchResultCombo);
            this.Controls.Add(this.shapeContainer1);
            this.Name = "PlateCreationEntry";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(1243, 304);
            ((System.ComponentModel.ISupportInitialize)(this.plateTiffInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.plateDBSaveLogicBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.plateTillTiffCombosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.plateDBSaveLogicBindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.ComboBox SearchResultCombo;
        private System.Windows.Forms.ComboBox SearchByCombo;
        private System.Windows.Forms.TextBox SearchByText;
        private System.Windows.Forms.Button SearchByButton;
        private System.Windows.Forms.Label SearchResultLabel;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShapeSearchDivider;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private System.Windows.Forms.Button SaveTiffButton;
        private System.Windows.Forms.Button RemoveTiffButton;
        private System.Windows.Forms.TextBox CroppedWidthText;
        private System.Windows.Forms.Label CroppedWidthLabel;
        private System.Windows.Forms.TextBox CroppedLengthText;
        private System.Windows.Forms.Label CroppedLengthLabel;
        private System.Windows.Forms.TextBox TiffWidthText;
        private System.Windows.Forms.Label TiffWidthLabel;
        private System.Windows.Forms.Label PlateNumberLabel;
        private System.Windows.Forms.TextBox TiffLengthText;
        private System.Windows.Forms.ComboBox SelectPlateCombo;
        private System.Windows.Forms.Label TiffLengthLabel;
        private System.Windows.Forms.Label SearchErrorLabel;
        private System.Windows.Forms.BindingSource plateTiffInfoBindingSource;
        private System.Windows.Forms.BindingSource plateTillTiffCombosBindingSource;
        private System.Windows.Forms.Label SaveErrorLabel;
        private System.Windows.Forms.BindingSource plateDBSaveLogicBindingSource;
        private System.Windows.Forms.BindingSource plateDBSaveLogicBindingSource1;
        private System.Windows.Forms.Label EdgeScrap;
        private System.Windows.Forms.TextBox EdgeScrapText;
    }
}
