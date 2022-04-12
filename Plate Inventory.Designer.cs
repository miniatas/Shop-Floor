namespace ShopFloor
{
    partial class Plate_Inventory
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Title = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dropSearchType = new System.Windows.Forms.ComboBox();
            this.SelectSearch = new System.Windows.Forms.Label();
            this.enterSearch = new System.Windows.Forms.Label();
            this.textSearch = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.dropSearchResults = new System.Windows.Forms.ComboBox();
            this.labelSearchResults = new System.Windows.Forms.Label();
            this.selectedCustomer = new System.Windows.Forms.Label();
            this.itemDescrip = new System.Windows.Forms.Label();
            this.owItem = new System.Windows.Forms.Label();
            this.lastJJ = new System.Windows.Forms.Label();
            this.textCustomer = new System.Windows.Forms.TextBox();
            this.textDescription = new System.Windows.Forms.TextBox();
            this.textOwItem = new System.Windows.Forms.TextBox();
            this.textJJ = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textLocation = new System.Windows.Forms.TextBox();
            this.buttonLocationChange = new System.Windows.Forms.Button();
            this.textStatus = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonCheckIn = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.textPlateStatus = new System.Windows.Forms.TextBox();
            this.buttonChangeNotes = new System.Windows.Forms.Button();
            this.buttonCancelNotes = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonCancelLocation = new System.Windows.Forms.Button();
            this.gridStatusView = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelSearchWarning = new System.Windows.Forms.Label();
            this.labelHistory = new System.Windows.Forms.Label();
            this.InactivePanel = new System.Windows.Forms.Panel();
            this.InActive = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStatusView)).BeginInit();
            this.panel3.SuspendLayout();
            this.InactivePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.Location = new System.Drawing.Point(546, 5);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(132, 24);
            this.Title.TabIndex = 0;
            this.Title.Text = "Plate inventory";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.Title, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1225, 35);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // dropSearchType
            // 
            this.dropSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropSearchType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dropSearchType.FormattingEnabled = true;
            this.dropSearchType.Location = new System.Drawing.Point(92, 40);
            this.dropSearchType.Name = "dropSearchType";
            this.dropSearchType.Size = new System.Drawing.Size(121, 23);
            this.dropSearchType.TabIndex = 2;
            // 
            // SelectSearch
            // 
            this.SelectSearch.AutoSize = true;
            this.SelectSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.SelectSearch.Location = new System.Drawing.Point(89, 8);
            this.SelectSearch.Name = "SelectSearch";
            this.SelectSearch.Size = new System.Drawing.Size(132, 17);
            this.SelectSearch.TabIndex = 3;
            this.SelectSearch.Text = "Select Search Type";
            // 
            // enterSearch
            // 
            this.enterSearch.AutoSize = true;
            this.enterSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.enterSearch.Location = new System.Drawing.Point(493, 8);
            this.enterSearch.Name = "enterSearch";
            this.enterSearch.Size = new System.Drawing.Size(91, 17);
            this.enterSearch.TabIndex = 4;
            this.enterSearch.Text = "Enter Search";
            // 
            // textSearch
            // 
            this.textSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.textSearch.Location = new System.Drawing.Point(324, 41);
            this.textSearch.Name = "textSearch";
            this.textSearch.Size = new System.Drawing.Size(428, 21);
            this.textSearch.TabIndex = 5;
            this.textSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textSearch_KeyPress);
            // 
            // searchButton
            // 
            this.searchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.searchButton.Location = new System.Drawing.Point(944, 38);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 6;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // dropSearchResults
            // 
            this.dropSearchResults.Enabled = false;
            this.dropSearchResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dropSearchResults.FormattingEnabled = true;
            this.dropSearchResults.Location = new System.Drawing.Point(145, 111);
            this.dropSearchResults.Name = "dropSearchResults";
            this.dropSearchResults.Size = new System.Drawing.Size(774, 23);
            this.dropSearchResults.TabIndex = 7;
            this.dropSearchResults.SelectedIndexChanged += new System.EventHandler(this.itemDescrip_Click);
            // 
            // labelSearchResults
            // 
            this.labelSearchResults.AutoSize = true;
            this.labelSearchResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.labelSearchResults.Location = new System.Drawing.Point(487, 81);
            this.labelSearchResults.Name = "labelSearchResults";
            this.labelSearchResults.Size = new System.Drawing.Size(104, 17);
            this.labelSearchResults.TabIndex = 8;
            this.labelSearchResults.Text = "Search Results";
            // 
            // selectedCustomer
            // 
            this.selectedCustomer.AutoSize = true;
            this.selectedCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.selectedCustomer.Location = new System.Drawing.Point(57, 15);
            this.selectedCustomer.Name = "selectedCustomer";
            this.selectedCustomer.Size = new System.Drawing.Size(127, 17);
            this.selectedCustomer.TabIndex = 9;
            this.selectedCustomer.Text = "Selected Customer";
            // 
            // itemDescrip
            // 
            this.itemDescrip.AutoSize = true;
            this.itemDescrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.itemDescrip.Location = new System.Drawing.Point(424, 15);
            this.itemDescrip.Name = "itemDescrip";
            this.itemDescrip.Size = new System.Drawing.Size(109, 17);
            this.itemDescrip.TabIndex = 10;
            this.itemDescrip.Text = "Item Description";
            this.itemDescrip.Click += new System.EventHandler(this.itemDescrip_Click);
            // 
            // owItem
            // 
            this.owItem.AutoSize = true;
            this.owItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.owItem.Location = new System.Drawing.Point(778, 15);
            this.owItem.Name = "owItem";
            this.owItem.Size = new System.Drawing.Size(70, 17);
            this.owItem.TabIndex = 11;
            this.owItem.Text = "OW Item#";
            // 
            // lastJJ
            // 
            this.lastJJ.AutoSize = true;
            this.lastJJ.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lastJJ.Location = new System.Drawing.Point(927, 15);
            this.lastJJ.Name = "lastJJ";
            this.lastJJ.Size = new System.Drawing.Size(115, 17);
            this.lastJJ.TabIndex = 12;
            this.lastJJ.Text = "Last Job Jacket#";
            // 
            // textCustomer
            // 
            this.textCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.textCustomer.Location = new System.Drawing.Point(36, 47);
            this.textCustomer.Multiline = true;
            this.textCustomer.Name = "textCustomer";
            this.textCustomer.ReadOnly = true;
            this.textCustomer.Size = new System.Drawing.Size(178, 20);
            this.textCustomer.TabIndex = 13;
            this.textCustomer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textDescription
            // 
            this.textDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.textDescription.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textDescription.Location = new System.Drawing.Point(234, 47);
            this.textDescription.Name = "textDescription";
            this.textDescription.ReadOnly = true;
            this.textDescription.Size = new System.Drawing.Size(473, 21);
            this.textDescription.TabIndex = 14;
            this.textDescription.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textOwItem
            // 
            this.textOwItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.textOwItem.Location = new System.Drawing.Point(734, 47);
            this.textOwItem.Name = "textOwItem";
            this.textOwItem.ReadOnly = true;
            this.textOwItem.Size = new System.Drawing.Size(153, 21);
            this.textOwItem.TabIndex = 15;
            this.textOwItem.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textJJ
            // 
            this.textJJ.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.textJJ.Location = new System.Drawing.Point(914, 47);
            this.textJJ.Multiline = true;
            this.textJJ.Name = "textJJ";
            this.textJJ.ReadOnly = true;
            this.textJJ.Size = new System.Drawing.Size(139, 20);
            this.textJJ.TabIndex = 16;
            this.textJJ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label8.Location = new System.Drawing.Point(72, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 17);
            this.label8.TabIndex = 17;
            this.label8.Text = "Location";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // textLocation
            // 
            this.textLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.textLocation.Location = new System.Drawing.Point(46, 34);
            this.textLocation.Name = "textLocation";
            this.textLocation.ReadOnly = true;
            this.textLocation.Size = new System.Drawing.Size(111, 21);
            this.textLocation.TabIndex = 18;
            // 
            // buttonLocationChange
            // 
            this.buttonLocationChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.buttonLocationChange.Location = new System.Drawing.Point(173, 33);
            this.buttonLocationChange.Name = "buttonLocationChange";
            this.buttonLocationChange.Size = new System.Drawing.Size(62, 23);
            this.buttonLocationChange.TabIndex = 19;
            this.buttonLocationChange.Text = "Change ";
            this.buttonLocationChange.UseVisualStyleBackColor = true;
            this.buttonLocationChange.Click += new System.EventHandler(this.locationChange_Click);
            // 
            // textStatus
            // 
            this.textStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.textStatus.ForeColor = System.Drawing.SystemColors.Window;
            this.textStatus.Location = new System.Drawing.Point(397, 35);
            this.textStatus.Name = "textStatus";
            this.textStatus.ReadOnly = true;
            this.textStatus.Size = new System.Drawing.Size(100, 21);
            this.textStatus.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label9.Location = new System.Drawing.Point(394, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 17);
            this.label9.TabIndex = 21;
            this.label9.Text = "Check In Status";
            // 
            // buttonCheckIn
            // 
            this.buttonCheckIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.buttonCheckIn.Location = new System.Drawing.Point(526, 32);
            this.buttonCheckIn.Name = "buttonCheckIn";
            this.buttonCheckIn.Size = new System.Drawing.Size(75, 23);
            this.buttonCheckIn.TabIndex = 22;
            this.buttonCheckIn.Text = "Check In";
            this.buttonCheckIn.UseVisualStyleBackColor = true;
            this.buttonCheckIn.Click += new System.EventHandler(this.CheckIn_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label10.Location = new System.Drawing.Point(566, 409);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 17);
            this.label10.TabIndex = 23;
            this.label10.Text = "Plate Status";
            // 
            // textPlateStatus
            // 
            this.textPlateStatus.BackColor = System.Drawing.SystemColors.InfoText;
            this.textPlateStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.textPlateStatus.ForeColor = System.Drawing.Color.White;
            this.textPlateStatus.Location = new System.Drawing.Point(294, 429);
            this.textPlateStatus.Multiline = true;
            this.textPlateStatus.Name = "textPlateStatus";
            this.textPlateStatus.ReadOnly = true;
            this.textPlateStatus.Size = new System.Drawing.Size(620, 68);
            this.textPlateStatus.TabIndex = 24;
            // 
            // buttonChangeNotes
            // 
            this.buttonChangeNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.buttonChangeNotes.Location = new System.Drawing.Point(937, 449);
            this.buttonChangeNotes.Name = "buttonChangeNotes";
            this.buttonChangeNotes.Size = new System.Drawing.Size(75, 23);
            this.buttonChangeNotes.TabIndex = 25;
            this.buttonChangeNotes.Text = "Change";
            this.buttonChangeNotes.UseVisualStyleBackColor = true;
            this.buttonChangeNotes.Click += new System.EventHandler(this.buttonChangeNotes_Click);
            // 
            // buttonCancelNotes
            // 
            this.buttonCancelNotes.Enabled = false;
            this.buttonCancelNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.buttonCancelNotes.Location = new System.Drawing.Point(1044, 449);
            this.buttonCancelNotes.Name = "buttonCancelNotes";
            this.buttonCancelNotes.Size = new System.Drawing.Size(75, 23);
            this.buttonCancelNotes.TabIndex = 27;
            this.buttonCancelNotes.Text = "Cancel";
            this.buttonCancelNotes.UseVisualStyleBackColor = true;
            this.buttonCancelNotes.Visible = false;
            this.buttonCancelNotes.Click += new System.EventHandler(this.buttonCancelNotes_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 648);
            this.splitter1.TabIndex = 28;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Lime;
            this.panel1.Controls.Add(this.textJJ);
            this.panel1.Controls.Add(this.textOwItem);
            this.panel1.Controls.Add(this.textDescription);
            this.panel1.Controls.Add(this.textCustomer);
            this.panel1.Controls.Add(this.lastJJ);
            this.panel1.Controls.Add(this.owItem);
            this.panel1.Controls.Add(this.itemDescrip);
            this.panel1.Controls.Add(this.selectedCustomer);
            this.panel1.Location = new System.Drawing.Point(66, 219);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1095, 83);
            this.panel1.TabIndex = 29;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Yellow;
            this.panel2.Controls.Add(this.buttonCancelLocation);
            this.panel2.Controls.Add(this.buttonCheckIn);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.textStatus);
            this.panel2.Controls.Add(this.buttonLocationChange);
            this.panel2.Controls.Add(this.textLocation);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Location = new System.Drawing.Point(294, 325);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(620, 71);
            this.panel2.TabIndex = 30;
            // 
            // buttonCancelLocation
            // 
            this.buttonCancelLocation.Enabled = false;
            this.buttonCancelLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.buttonCancelLocation.Location = new System.Drawing.Point(253, 33);
            this.buttonCancelLocation.Name = "buttonCancelLocation";
            this.buttonCancelLocation.Size = new System.Drawing.Size(75, 23);
            this.buttonCancelLocation.TabIndex = 23;
            this.buttonCancelLocation.Text = "Cancel";
            this.buttonCancelLocation.UseVisualStyleBackColor = true;
            this.buttonCancelLocation.Visible = false;
            this.buttonCancelLocation.Click += new System.EventHandler(this.cancelLocation);
            // 
            // gridStatusView
            // 
            this.gridStatusView.AllowUserToAddRows = false;
            this.gridStatusView.AllowUserToDeleteRows = false;
            this.gridStatusView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridStatusView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridStatusView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridStatusView.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridStatusView.Location = new System.Drawing.Point(211, 539);
            this.gridStatusView.Name = "gridStatusView";
            this.gridStatusView.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridStatusView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridStatusView.Size = new System.Drawing.Size(774, 97);
            this.gridStatusView.TabIndex = 31;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Gold;
            this.panel3.Controls.Add(this.labelSearchWarning);
            this.panel3.Controls.Add(this.enterSearch);
            this.panel3.Controls.Add(this.textSearch);
            this.panel3.Controls.Add(this.searchButton);
            this.panel3.Controls.Add(this.dropSearchType);
            this.panel3.Controls.Add(this.SelectSearch);
            this.panel3.Controls.Add(this.labelSearchResults);
            this.panel3.Controls.Add(this.dropSearchResults);
            this.panel3.Location = new System.Drawing.Point(66, 54);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1095, 148);
            this.panel3.TabIndex = 32;
            // 
            // labelSearchWarning
            // 
            this.labelSearchWarning.ForeColor = System.Drawing.Color.Red;
            this.labelSearchWarning.Location = new System.Drawing.Point(436, 65);
            this.labelSearchWarning.Name = "labelSearchWarning";
            this.labelSearchWarning.Size = new System.Drawing.Size(206, 16);
            this.labelSearchWarning.TabIndex = 9;
            this.labelSearchWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelHistory
            // 
            this.labelHistory.AutoSize = true;
            this.labelHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.labelHistory.Location = new System.Drawing.Point(547, 509);
            this.labelHistory.Name = "labelHistory";
            this.labelHistory.Size = new System.Drawing.Size(110, 17);
            this.labelHistory.TabIndex = 33;
            this.labelHistory.Text = "Check In History";
            // 
            // InactivePanel
            // 
            this.InactivePanel.BackColor = System.Drawing.Color.Red;
            this.InactivePanel.Hide();
            this.InactivePanel.Controls.Add(this.InActive);
            this.InactivePanel.Location = new System.Drawing.Point(36, 325);
            this.InactivePanel.Name = "InactivePanel";
            this.InactivePanel.Size = new System.Drawing.Size(243, 71);
            this.InactivePanel.TabIndex = 34;
          
            // 
            // InActive
            // 
            this.InActive.AutoSize = true;
            this.InActive.Hide();
            this.InActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.InActive.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.InActive.Location = new System.Drawing.Point(5, 32);
            this.InActive.Name = "InActive";
            this.InActive.Size = new System.Drawing.Size(235, 17);
            this.InActive.TabIndex = 11;
            this.InActive.Text = "Order is InActive, Cannot Check Out";
           
            // 
            // Plate_Inventory
            // 
            this.AccessibleName = "SearchResults";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1225, 648);
            this.Controls.Add(this.InactivePanel);
            this.Controls.Add(this.labelHistory);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.gridStatusView);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.buttonCancelNotes);
            this.Controls.Add(this.buttonChangeNotes);
            this.Controls.Add(this.textPlateStatus);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Plate_Inventory";
            this.Text = "Plate_Inventory";
            this.Load += new System.EventHandler(this.Plate_Inventory_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStatusView)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.InactivePanel.ResumeLayout(false);
            this.InactivePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox dropSearchType;
        private System.Windows.Forms.Label SelectSearch;
        private System.Windows.Forms.Label enterSearch;
        private System.Windows.Forms.TextBox textSearch;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.ComboBox dropSearchResults;
        private System.Windows.Forms.Label labelSearchResults;
        private System.Windows.Forms.Label selectedCustomer;
        private System.Windows.Forms.Label itemDescrip;
        private System.Windows.Forms.Label owItem;
        private System.Windows.Forms.Label lastJJ;
        private System.Windows.Forms.TextBox textCustomer;
        private System.Windows.Forms.TextBox textDescription;
        private System.Windows.Forms.TextBox textOwItem;
        private System.Windows.Forms.TextBox textJJ;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textLocation;
        private System.Windows.Forms.Button buttonLocationChange;
        private System.Windows.Forms.TextBox textStatus;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonCheckIn;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textPlateStatus;
        private System.Windows.Forms.Button buttonChangeNotes;
        private System.Windows.Forms.Button buttonCancelNotes;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView gridStatusView;
        private System.Windows.Forms.Button buttonCancelLocation;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label labelHistory;
        private System.Windows.Forms.Label labelSearchWarning;
        private System.Windows.Forms.Panel InactivePanel;
        private System.Windows.Forms.Label InActive;
    }
}