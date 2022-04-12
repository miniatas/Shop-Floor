namespace ShopFloor.PlateForms
{
    partial class PlateCreationMain
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ImageableAreaLabel = new System.Windows.Forms.Label();
            this.TillAreaLabel = new System.Windows.Forms.Label();
            this.UomCombo = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TillTypeCombo = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TiffCalculatedGridView = new System.Windows.Forms.DataGridView();
            this.TotalsGridView = new System.Windows.Forms.DataGridView();
            this.ImageableAreaTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ImageableScrap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ImageableScrapPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddTiffButton = new System.Windows.Forms.Button();
            this.SaveAllButton = new System.Windows.Forms.Button();
            this.TiffEntryTabControl = new System.Windows.Forms.TabControl();
            this.RemoveSelectedTiffButton = new System.Windows.Forms.Button();
            this.FinalizeButton = new System.Windows.Forms.Button();
            this.SaveTiffTab = new System.Windows.Forms.Button();
            this.FinalizeErrorLabel = new System.Windows.Forms.Label();
            this.AddTiffErrorLabel = new System.Windows.Forms.Label();
            this.FinalizeMessageLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TiffCalculatedGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TotalsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightSalmon;
            this.groupBox1.Controls.Add(this.ImageableAreaLabel);
            this.groupBox1.Controls.Add(this.TillAreaLabel);
            this.groupBox1.Controls.Add(this.UomCombo);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.TillTypeCombo);
            this.groupBox1.Location = new System.Drawing.Point(12, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1199, 63);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "New or Search";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // ImageableAreaLabel
            // 
            this.ImageableAreaLabel.AutoSize = true;
            this.ImageableAreaLabel.Location = new System.Drawing.Point(715, 25);
            this.ImageableAreaLabel.Name = "ImageableAreaLabel";
            this.ImageableAreaLabel.Size = new System.Drawing.Size(13, 13);
            this.ImageableAreaLabel.TabIndex = 10;
            this.ImageableAreaLabel.Text = "0";
            // 
            // TillAreaLabel
            // 
            this.TillAreaLabel.AutoSize = true;
            this.TillAreaLabel.Location = new System.Drawing.Point(383, 27);
            this.TillAreaLabel.Name = "TillAreaLabel";
            this.TillAreaLabel.Size = new System.Drawing.Size(13, 13);
            this.TillAreaLabel.TabIndex = 9;
            this.TillAreaLabel.Text = "0";
            // 
            // UomCombo
            // 
            this.UomCombo.FormattingEnabled = true;
            this.UomCombo.Location = new System.Drawing.Point(1025, 22);
            this.UomCombo.Name = "UomCombo";
            this.UomCombo.Size = new System.Drawing.Size(58, 21);
            this.UomCombo.TabIndex = 8;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(923, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "Unit of Measure:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(323, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Till Area";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(368, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 13);
            this.label5.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(625, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Imageable Area:";
            // 
            // TillTypeCombo
            // 
            this.TillTypeCombo.FormattingEnabled = true;
            this.TillTypeCombo.Location = new System.Drawing.Point(6, 22);
            this.TillTypeCombo.Name = "TillTypeCombo";
            this.TillTypeCombo.Size = new System.Drawing.Size(200, 21);
            this.TillTypeCombo.TabIndex = 3;
            this.TillTypeCombo.Text = "Select Till Dimensions";
            this.TillTypeCombo.SelectedIndexChanged += new System.EventHandler(this.TillTypeCombo_SelectedIndexChanged);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1116, 287);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(74, 77);
            this.button4.TabIndex = 16;
            this.button4.Text = "Add Tiff";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(131, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "DIMENSION SELECTION";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.TiffCalculatedGridView);
            this.panel1.Location = new System.Drawing.Point(12, 467);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1199, 249);
            this.panel1.TabIndex = 19;
            // 
            // TiffCalculatedGridView
            // 
            this.TiffCalculatedGridView.AllowUserToAddRows = false;
            this.TiffCalculatedGridView.AllowUserToDeleteRows = false;
            this.TiffCalculatedGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.TiffCalculatedGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TiffCalculatedGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.TiffCalculatedGridView.Location = new System.Drawing.Point(3, 0);
            this.TiffCalculatedGridView.Name = "TiffCalculatedGridView";
            this.TiffCalculatedGridView.ShowEditingIcon = false;
            this.TiffCalculatedGridView.Size = new System.Drawing.Size(1199, 243);
            this.TiffCalculatedGridView.TabIndex = 0;
            // 
            // TotalsGridView
            // 
            this.TotalsGridView.AllowUserToAddRows = false;
            this.TotalsGridView.AllowUserToDeleteRows = false;
            this.TotalsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TotalsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ImageableAreaTotal,
            this.ImageableScrap,
            this.ImageableScrapPercent});
            this.TotalsGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.TotalsGridView.Location = new System.Drawing.Point(12, 761);
            this.TotalsGridView.Name = "TotalsGridView";
            this.TotalsGridView.Size = new System.Drawing.Size(677, 102);
            this.TotalsGridView.TabIndex = 20;
            // 
            // ImageableAreaTotal
            // 
            this.ImageableAreaTotal.HeaderText = "Total Imageable Area";
            this.ImageableAreaTotal.Name = "ImageableAreaTotal";
            // 
            // ImageableScrap
            // 
            this.ImageableScrap.HeaderText = "Total Imageable Scrap";
            this.ImageableScrap.Name = "ImageableScrap";
            // 
            // ImageableScrapPercent
            // 
            this.ImageableScrapPercent.HeaderText = "Imageable Scrap %";
            this.ImageableScrapPercent.Name = "ImageableScrapPercent";
            // 
            // AddTiffButton
            // 
            this.AddTiffButton.Location = new System.Drawing.Point(210, 421);
            this.AddTiffButton.Name = "AddTiffButton";
            this.AddTiffButton.Size = new System.Drawing.Size(75, 23);
            this.AddTiffButton.TabIndex = 22;
            this.AddTiffButton.Text = "Add Tiff";
            this.AddTiffButton.UseVisualStyleBackColor = true;
            this.AddTiffButton.Click += new System.EventHandler(this.AddTiffButton_Click);
            // 
            // SaveAllButton
            // 
            this.SaveAllButton.Enabled = false;
            this.SaveAllButton.Location = new System.Drawing.Point(1116, 421);
            this.SaveAllButton.Name = "SaveAllButton";
            this.SaveAllButton.Size = new System.Drawing.Size(75, 23);
            this.SaveAllButton.TabIndex = 23;
            this.SaveAllButton.Text = "Save All";
            this.SaveAllButton.UseVisualStyleBackColor = true;
            this.SaveAllButton.Visible = false;
            // 
            // TiffEntryTabControl
            // 
            this.TiffEntryTabControl.Location = new System.Drawing.Point(13, 100);
            this.TiffEntryTabControl.Name = "TiffEntryTabControl";
            this.TiffEntryTabControl.SelectedIndex = 0;
            this.TiffEntryTabControl.Size = new System.Drawing.Size(1198, 315);
            this.TiffEntryTabControl.TabIndex = 0;
            this.TiffEntryTabControl.SelectedIndexChanged += new System.EventHandler(this.TiffEntryTabControl_SelectedIndexChanged);
            // 
            // RemoveSelectedTiffButton
            // 
            this.RemoveSelectedTiffButton.Enabled = false;
            this.RemoveSelectedTiffButton.Location = new System.Drawing.Point(33, 421);
            this.RemoveSelectedTiffButton.Name = "RemoveSelectedTiffButton";
            this.RemoveSelectedTiffButton.Size = new System.Drawing.Size(131, 23);
            this.RemoveSelectedTiffButton.TabIndex = 24;
            this.RemoveSelectedTiffButton.Text = "Remove Selected Tiff";
            this.RemoveSelectedTiffButton.UseVisualStyleBackColor = true;
            this.RemoveSelectedTiffButton.Click += new System.EventHandler(this.RemoveSelectedTiffButton_Click);
            // 
            // FinalizeButton
            // 
            this.FinalizeButton.Enabled = false;
            this.FinalizeButton.Location = new System.Drawing.Point(566, 896);
            this.FinalizeButton.Name = "FinalizeButton";
            this.FinalizeButton.Size = new System.Drawing.Size(123, 23);
            this.FinalizeButton.TabIndex = 25;
            this.FinalizeButton.Text = "Finalize Creation";
            this.FinalizeButton.UseVisualStyleBackColor = true;
            this.FinalizeButton.Click += new System.EventHandler(this.TheFinalQuery);
            // 
            // SaveTiffTab
            // 
            this.SaveTiffTab.Location = new System.Drawing.Point(958, 421);
            this.SaveTiffTab.Name = "SaveTiffTab";
            this.SaveTiffTab.Size = new System.Drawing.Size(113, 23);
            this.SaveTiffTab.TabIndex = 26;
            this.SaveTiffTab.Text = "Save Current Tab";
            this.SaveTiffTab.UseVisualStyleBackColor = true;
            this.SaveTiffTab.Click += new System.EventHandler(this.SaveTiffTab_Click);
            // 
            // FinalizeErrorLabel
            // 
            this.FinalizeErrorLabel.AutoSize = true;
            this.FinalizeErrorLabel.Location = new System.Drawing.Point(728, 901);
            this.FinalizeErrorLabel.Name = "FinalizeErrorLabel";
            this.FinalizeErrorLabel.Size = new System.Drawing.Size(0, 13);
            this.FinalizeErrorLabel.TabIndex = 27;
            // 
            // AddTiffErrorLabel
            // 
            this.AddTiffErrorLabel.AutoSize = true;
            this.AddTiffErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.AddTiffErrorLabel.Location = new System.Drawing.Point(307, 430);
            this.AddTiffErrorLabel.Name = "AddTiffErrorLabel";
            this.AddTiffErrorLabel.Size = new System.Drawing.Size(0, 13);
            this.AddTiffErrorLabel.TabIndex = 28;
            // 
            // FinalizeMessageLabel
            // 
            this.FinalizeMessageLabel.AutoSize = true;
            this.FinalizeMessageLabel.ForeColor = System.Drawing.Color.Red;
            this.FinalizeMessageLabel.Location = new System.Drawing.Point(734, 901);
            this.FinalizeMessageLabel.Name = "FinalizeMessageLabel";
            this.FinalizeMessageLabel.Size = new System.Drawing.Size(0, 13);
            this.FinalizeMessageLabel.TabIndex = 29;
            // 
            // PlateCreationMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1224, 1031);
            this.Controls.Add(this.FinalizeMessageLabel);
            this.Controls.Add(this.AddTiffErrorLabel);
            this.Controls.Add(this.FinalizeErrorLabel);
            this.Controls.Add(this.SaveTiffTab);
            this.Controls.Add(this.FinalizeButton);
            this.Controls.Add(this.RemoveSelectedTiffButton);
            this.Controls.Add(this.SaveAllButton);
            this.Controls.Add(this.AddTiffButton);
            this.Controls.Add(this.TiffEntryTabControl);
            this.Controls.Add(this.TotalsGridView);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.groupBox1);
            this.Name = "PlateCreationMain";
            this.Text = "PlateCreationMain";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TiffCalculatedGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TotalsGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox TillTypeCombo;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView TiffCalculatedGridView;
        private System.Windows.Forms.DataGridView TotalsGridView;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox UomCombo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button AddTiffButton;
        private System.Windows.Forms.Button SaveAllButton;
        private System.Windows.Forms.TabControl TiffEntryTabControl;
        private System.Windows.Forms.Button RemoveSelectedTiffButton;
        private System.Windows.Forms.Label ImageableAreaLabel;
        private System.Windows.Forms.Label TillAreaLabel;
        private System.Windows.Forms.Button FinalizeButton;
        private System.Windows.Forms.Button SaveTiffTab;
        private System.Windows.Forms.DataGridViewTextBoxColumn ImageableAreaTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn ImageableScrap;
        private System.Windows.Forms.DataGridViewTextBoxColumn ImageableScrapPercent;
        private System.Windows.Forms.Label FinalizeErrorLabel;
        private System.Windows.Forms.Label AddTiffErrorLabel;
        private System.Windows.Forms.Label FinalizeMessageLabel;
    }
}