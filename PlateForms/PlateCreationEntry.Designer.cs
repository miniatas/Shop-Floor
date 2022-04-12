namespace ShopFloor
{
    partial class PlateCreationEntry
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SearchResultCombo = new System.Windows.Forms.ComboBox();
            this.SearchByCombo = new System.Windows.Forms.ComboBox();
            this.SearchByText = new System.Windows.Forms.TextBox();
            this.SearchByButton = new System.Windows.Forms.Button();
            this.SearchResultLabel = new System.Windows.Forms.Label();
            this.lineShapeSearchDivider = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.LPILabel = new System.Windows.Forms.Label();
            this.ColorLabel = new System.Windows.Forms.Label();
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
            this.ColorDisplayLabel = new System.Windows.Forms.Label();
            this.LPIDisplayLabel = new System.Windows.Forms.Label();
            this.SearchErrorLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SearchResultCombo
            // 
            this.SearchResultCombo.FormattingEnabled = true;
            this.SearchResultCombo.Location = new System.Drawing.Point(138, 57);
            this.SearchResultCombo.Name = "SearchResultCombo";
            this.SearchResultCombo.Size = new System.Drawing.Size(1058, 21);
            this.SearchResultCombo.TabIndex = 25;
            this.SearchResultCombo.SelectedIndexChanged += new System.EventHandler(this.SearchResultCombo_SelectedIndexChanged);
            // 
            // SearchByCombo
            // 
            this.SearchByCombo.FormattingEnabled = true;
            this.SearchByCombo.Location = new System.Drawing.Point(402, 13);
            this.SearchByCombo.Name = "SearchByCombo";
            this.SearchByCombo.Size = new System.Drawing.Size(121, 21);
            this.SearchByCombo.TabIndex = 26;
            this.SearchByCombo.Text = "Search by...";
            this.SearchByCombo.SelectedIndexChanged += new System.EventHandler(this.SearchByCombo_SelectedIndexChanged);
            // 
            // SearchByText
            // 
            this.SearchByText.Location = new System.Drawing.Point(540, 13);
            this.SearchByText.Name = "SearchByText";
            this.SearchByText.Size = new System.Drawing.Size(175, 20);
            this.SearchByText.TabIndex = 27;
            // 
            // SearchByButton
            // 
            this.SearchByButton.Location = new System.Drawing.Point(734, 11);
            this.SearchByButton.Name = "SearchByButton";
            this.SearchByButton.Size = new System.Drawing.Size(75, 23);
            this.SearchByButton.TabIndex = 28;
            this.SearchByButton.Text = "Search";
            this.SearchByButton.UseVisualStyleBackColor = true;
            // 
            // SearchResultLabel
            // 
            this.SearchResultLabel.AutoSize = true;
            this.SearchResultLabel.Location = new System.Drawing.Point(28, 60);
            this.SearchResultLabel.Name = "SearchResultLabel";
            this.SearchResultLabel.Size = new System.Drawing.Size(77, 13);
            this.SearchResultLabel.TabIndex = 29;
            this.SearchResultLabel.Text = "Search Result:";
            // 
            // lineShapeSearchDivider
            // 
            this.lineShapeSearchDivider.Name = "lineShapeSearchDivider";
            this.lineShapeSearchDivider.X1 = 27;
            this.lineShapeSearchDivider.X2 = 1215;
            this.lineShapeSearchDivider.Y1 = 95;
            this.lineShapeSearchDivider.Y2 = 95;
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
            // LPILabel
            // 
            this.LPILabel.AutoSize = true;
            this.LPILabel.Location = new System.Drawing.Point(522, 119);
            this.LPILabel.Name = "LPILabel";
            this.LPILabel.Size = new System.Drawing.Size(26, 13);
            this.LPILabel.TabIndex = 44;
            this.LPILabel.Text = "LPI:";
            // 
            // ColorLabel
            // 
            this.ColorLabel.AutoSize = true;
            this.ColorLabel.Location = new System.Drawing.Point(332, 122);
            this.ColorLabel.Name = "ColorLabel";
            this.ColorLabel.Size = new System.Drawing.Size(34, 13);
            this.ColorLabel.TabIndex = 43;
            this.ColorLabel.Text = "Color:";
            // 
            // SaveTiffButton
            // 
            this.SaveTiffButton.Location = new System.Drawing.Point(1121, 255);
            this.SaveTiffButton.Name = "SaveTiffButton";
            this.SaveTiffButton.Size = new System.Drawing.Size(75, 23);
            this.SaveTiffButton.TabIndex = 42;
            this.SaveTiffButton.Text = "Save";
            this.SaveTiffButton.UseVisualStyleBackColor = true;
            // 
            // RemoveTiffButton
            // 
            this.RemoveTiffButton.Location = new System.Drawing.Point(31, 255);
            this.RemoveTiffButton.Name = "RemoveTiffButton";
            this.RemoveTiffButton.Size = new System.Drawing.Size(75, 23);
            this.RemoveTiffButton.TabIndex = 41;
            this.RemoveTiffButton.Text = "Remove Tiff";
            this.RemoveTiffButton.UseVisualStyleBackColor = true;
            // 
            // CroppedWidthText
            // 
            this.CroppedWidthText.Location = new System.Drawing.Point(381, 208);
            this.CroppedWidthText.Name = "CroppedWidthText";
            this.CroppedWidthText.Size = new System.Drawing.Size(100, 20);
            this.CroppedWidthText.TabIndex = 40;
            // 
            // CroppedWidthLabel
            // 
            this.CroppedWidthLabel.AutoSize = true;
            this.CroppedWidthLabel.Location = new System.Drawing.Point(285, 211);
            this.CroppedWidthLabel.Name = "CroppedWidthLabel";
            this.CroppedWidthLabel.Size = new System.Drawing.Size(81, 13);
            this.CroppedWidthLabel.TabIndex = 39;
            this.CroppedWidthLabel.Text = "Cropped Width:";
            // 
            // CroppedLengthText
            // 
            this.CroppedLengthText.Location = new System.Drawing.Point(138, 208);
            this.CroppedLengthText.Name = "CroppedLengthText";
            this.CroppedLengthText.Size = new System.Drawing.Size(100, 20);
            this.CroppedLengthText.TabIndex = 38;
            // 
            // CroppedLengthLabel
            // 
            this.CroppedLengthLabel.AutoSize = true;
            this.CroppedLengthLabel.Location = new System.Drawing.Point(30, 208);
            this.CroppedLengthLabel.Name = "CroppedLengthLabel";
            this.CroppedLengthLabel.Size = new System.Drawing.Size(86, 13);
            this.CroppedLengthLabel.TabIndex = 37;
            this.CroppedLengthLabel.Text = "Cropped Length:";
            // 
            // TiffWidthText
            // 
            this.TiffWidthText.Location = new System.Drawing.Point(381, 161);
            this.TiffWidthText.Name = "TiffWidthText";
            this.TiffWidthText.Size = new System.Drawing.Size(100, 20);
            this.TiffWidthText.TabIndex = 36;
            // 
            // TiffWidthLabel
            // 
            this.TiffWidthLabel.AutoSize = true;
            this.TiffWidthLabel.Location = new System.Drawing.Point(310, 165);
            this.TiffWidthLabel.Name = "TiffWidthLabel";
            this.TiffWidthLabel.Size = new System.Drawing.Size(56, 13);
            this.TiffWidthLabel.TabIndex = 35;
            this.TiffWidthLabel.Text = "Tiff Width:";
            // 
            // PlateNumberLabel
            // 
            this.PlateNumberLabel.AutoSize = true;
            this.PlateNumberLabel.Location = new System.Drawing.Point(30, 122);
            this.PlateNumberLabel.Name = "PlateNumberLabel";
            this.PlateNumberLabel.Size = new System.Drawing.Size(41, 13);
            this.PlateNumberLabel.TabIndex = 32;
            this.PlateNumberLabel.Text = "Plate #";
            // 
            // TiffLengthText
            // 
            this.TiffLengthText.Location = new System.Drawing.Point(138, 165);
            this.TiffLengthText.Name = "TiffLengthText";
            this.TiffLengthText.Size = new System.Drawing.Size(100, 20);
            this.TiffLengthText.TabIndex = 34;
            // 
            // SelectPlateCombo
            // 
            this.SelectPlateCombo.FormattingEnabled = true;
            this.SelectPlateCombo.Location = new System.Drawing.Point(138, 119);
            this.SelectPlateCombo.Name = "SelectPlateCombo";
            this.SelectPlateCombo.Size = new System.Drawing.Size(121, 21);
            this.SelectPlateCombo.TabIndex = 31;
            this.SelectPlateCombo.Text = "Select Plate";
            // 
            // TiffLengthLabel
            // 
            this.TiffLengthLabel.AutoSize = true;
            this.TiffLengthLabel.Location = new System.Drawing.Point(30, 168);
            this.TiffLengthLabel.Name = "TiffLengthLabel";
            this.TiffLengthLabel.Size = new System.Drawing.Size(61, 13);
            this.TiffLengthLabel.TabIndex = 33;
            this.TiffLengthLabel.Text = "Tiff Length:";
            // 
            // ColorDisplayLabel
            // 
            this.ColorDisplayLabel.AutoSize = true;
            this.ColorDisplayLabel.Location = new System.Drawing.Point(381, 122);
            this.ColorDisplayLabel.Name = "ColorDisplayLabel";
            this.ColorDisplayLabel.Size = new System.Drawing.Size(35, 13);
            this.ColorDisplayLabel.TabIndex = 45;
            this.ColorDisplayLabel.Text = "label1";
            // 
            // LPIDisplayLabel
            // 
            this.LPIDisplayLabel.AutoSize = true;
            this.LPIDisplayLabel.Location = new System.Drawing.Point(555, 121);
            this.LPIDisplayLabel.Name = "LPIDisplayLabel";
            this.LPIDisplayLabel.Size = new System.Drawing.Size(35, 13);
            this.LPIDisplayLabel.TabIndex = 46;
            this.LPIDisplayLabel.Text = "label2";
            // 
            // SearchErrorLabel
            // 
            this.SearchErrorLabel.AutoSize = true;
            this.SearchErrorLabel.Location = new System.Drawing.Point(842, 19);
            this.SearchErrorLabel.Name = "SearchErrorLabel";
            this.SearchErrorLabel.Size = new System.Drawing.Size(0, 13);
            this.SearchErrorLabel.TabIndex = 47;
            // 
            // PlateCreationEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SearchErrorLabel);
            this.Controls.Add(this.LPIDisplayLabel);
            this.Controls.Add(this.ColorDisplayLabel);
            this.Controls.Add(this.LPILabel);
            this.Controls.Add(this.ColorLabel);
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
            this.Size = new System.Drawing.Size(1243, 304);
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
        private System.Windows.Forms.Label LPILabel;
        private System.Windows.Forms.Label ColorLabel;
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
        private System.Windows.Forms.Label ColorDisplayLabel;
        private System.Windows.Forms.Label LPIDisplayLabel;
        private System.Windows.Forms.Label SearchErrorLabel;
    }
}
