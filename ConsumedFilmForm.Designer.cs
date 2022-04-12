/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 8/25/2011
 * Time: 1:35 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ShopFloor
{
	public partial class ConsumedFilmForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.DataGridView dgvConsumedRolls;
		
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvConsumedRolls = new System.Windows.Forms.DataGridView();
            this.operatorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transactionDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rollNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.activity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unwind = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.linearFeet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConsumedRolls)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvConsumedRolls
            // 
            this.dgvConsumedRolls.AllowUserToAddRows = false;
            this.dgvConsumedRolls.AllowUserToDeleteRows = false;
            this.dgvConsumedRolls.AllowUserToResizeColumns = false;
            this.dgvConsumedRolls.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvConsumedRolls.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvConsumedRolls.ColumnHeadersHeight = 20;
            this.dgvConsumedRolls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvConsumedRolls.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.operatorName,
            this.transactionDate,
            this.rollNumber,
            this.activity,
            this.unwind,
            this.description,
            this.linearFeet});
            this.dgvConsumedRolls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvConsumedRolls.Location = new System.Drawing.Point(0, 0);
            this.dgvConsumedRolls.MultiSelect = false;
            this.dgvConsumedRolls.Name = "dgvConsumedRolls";
            this.dgvConsumedRolls.ReadOnly = true;
            this.dgvConsumedRolls.RowHeadersWidth = 20;
            this.dgvConsumedRolls.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvConsumedRolls.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvConsumedRolls.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvConsumedRolls.Size = new System.Drawing.Size(1111, 262);
            this.dgvConsumedRolls.TabIndex = 26;
            // 
            // operatorName
            // 
            this.operatorName.HeaderText = "Operator";
            this.operatorName.Name = "operatorName";
            this.operatorName.ReadOnly = true;
            this.operatorName.Width = 150;
            // 
            // transactionDate
            // 
            this.transactionDate.HeaderText = "Date";
            this.transactionDate.Name = "transactionDate";
            this.transactionDate.ReadOnly = true;
            // 
            // rollNumber
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.rollNumber.DefaultCellStyle = dataGridViewCellStyle2;
            this.rollNumber.HeaderText = "Roll #";
            this.rollNumber.Name = "rollNumber";
            this.rollNumber.ReadOnly = true;
            this.rollNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.rollNumber.Width = 60;
            // 
            // activity
            // 
            this.activity.HeaderText = "Activity";
            this.activity.Name = "activity";
            this.activity.ReadOnly = true;
            this.activity.Width = 70;
            // 
            // unwind
            // 
            this.unwind.HeaderText = "Unwind #";
            this.unwind.Name = "unwind";
            this.unwind.ReadOnly = true;
            this.unwind.Width = 60;
            // 
            // description
            // 
            this.description.HeaderText = "Description";
            this.description.Name = "description";
            this.description.ReadOnly = true;
            this.description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.description.Width = 570;
            // 
            // linearFeet
            // 
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = null;
            this.linearFeet.DefaultCellStyle = dataGridViewCellStyle3;
            this.linearFeet.HeaderText = "LF";
            this.linearFeet.Name = "linearFeet";
            this.linearFeet.ReadOnly = true;
            this.linearFeet.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.linearFeet.Width = 70;
            // 
            // ConsumedFilmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 262);
            this.Controls.Add(this.dgvConsumedRolls);
            this.Name = "ConsumedFilmForm";
            this.Text = "Consumed Film History";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConsumedFilmFormFormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvConsumedRolls)).EndInit();
            this.ResumeLayout(false);

		}

        private System.Windows.Forms.DataGridViewTextBoxColumn operatorName;
        private System.Windows.Forms.DataGridViewTextBoxColumn transactionDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn rollNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn activity;
        private System.Windows.Forms.DataGridViewTextBoxColumn unwind;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewTextBoxColumn linearFeet;
    }
}
