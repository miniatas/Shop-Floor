/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 4/19/2012
 * Time: 1:10 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ShopFloor
{
	public partial class IssueForm
	{
		private System.Windows.Forms.Label lblAllocationMethod;
		private System.Windows.Forms.ComboBox cbxAllocationMethod;
		private System.Windows.Forms.Button cmdAllocate;
		private System.Windows.Forms.Label lblPercentofNeedToAllocate;
		private System.Windows.Forms.TextBox txtPercentOfNeedToAllocate;
		private System.Windows.Forms.Label lblPercentagePoint;
		private System.Windows.Forms.DataGridView dgvFilm;
        private System.Windows.Forms.DataGridView dgvAllocations;
        private System.Windows.Forms.Panel pnlTotals;
		private System.Windows.Forms.Panel pnlFilters;
		private System.Windows.Forms.RichTextBox rtbJobInformation;
	
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">True if managed resources should be disposed; otherwise, false.</param>
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvFilm = new System.Windows.Forms.DataGridView();
            this.dgvAllocations = new System.Windows.Forms.DataGridView();
            this.rtbJobInformation = new System.Windows.Forms.RichTextBox();
            this.pnlFilters = new System.Windows.Forms.Panel();
            this.ckShowAllFilms = new System.Windows.Forms.CheckBox();
            this.lblPercentofNeedToAllocate = new System.Windows.Forms.Label();
            this.lblAllocationMethod = new System.Windows.Forms.Label();
            this.lblPercentagePoint = new System.Windows.Forms.Label();
            this.cbxAllocationMethod = new System.Windows.Forms.ComboBox();
            this.txtPercentOfNeedToAllocate = new System.Windows.Forms.TextBox();
            this.cmdAllocate = new System.Windows.Forms.Button();
            this.pnlTotals = new System.Windows.Forms.Panel();
            this.lblCurrentAllocationInformation = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllocations)).BeginInit();
            this.pnlFilters.SuspendLayout();
            this.pnlTotals.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvFilm
            // 
            this.dgvFilm.AllowUserToAddRows = false;
            this.dgvFilm.AllowUserToDeleteRows = false;
            this.dgvFilm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.NullValue = null;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFilm.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvFilm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFilm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFilm.Location = new System.Drawing.Point(0, 240);
            this.dgvFilm.MultiSelect = false;
            this.dgvFilm.Name = "dgvFilm";
            this.dgvFilm.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.NullValue = null;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFilm.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFilm.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvFilm.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFilm.Size = new System.Drawing.Size(1031, 322);
            this.dgvFilm.TabIndex = 3;
            this.dgvFilm.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFilmCellDoubleClick);
            // 
            // dgvAllocations
            // 
            this.dgvAllocations.AllowUserToAddRows = false;
            this.dgvAllocations.AllowUserToDeleteRows = false;
            this.dgvAllocations.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvAllocations.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvAllocations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllocations.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvAllocations.Location = new System.Drawing.Point(0, 120);
            this.dgvAllocations.MultiSelect = false;
            this.dgvAllocations.Name = "dgvAllocations";
            this.dgvAllocations.ReadOnly = true;
            this.dgvAllocations.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvAllocations.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvAllocations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAllocations.Size = new System.Drawing.Size(1031, 80);
            this.dgvAllocations.TabIndex = 1;
            // 
            // rtbJobInformation
            // 
            this.rtbJobInformation.BackColor = System.Drawing.SystemColors.Control;
            this.rtbJobInformation.Dock = System.Windows.Forms.DockStyle.Top;
            this.rtbJobInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbJobInformation.ForeColor = System.Drawing.Color.Blue;
            this.rtbJobInformation.Location = new System.Drawing.Point(0, 0);
            this.rtbJobInformation.Name = "rtbJobInformation";
            this.rtbJobInformation.ReadOnly = true;
            this.rtbJobInformation.Size = new System.Drawing.Size(1031, 120);
            this.rtbJobInformation.TabIndex = 0;
            this.rtbJobInformation.Text = "";
            // 
            // pnlFilters
            // 
            this.pnlFilters.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlFilters.Controls.Add(this.ckShowAllFilms);
            this.pnlFilters.Controls.Add(this.lblPercentofNeedToAllocate);
            this.pnlFilters.Controls.Add(this.lblAllocationMethod);
            this.pnlFilters.Controls.Add(this.lblPercentagePoint);
            this.pnlFilters.Controls.Add(this.cbxAllocationMethod);
            this.pnlFilters.Controls.Add(this.txtPercentOfNeedToAllocate);
            this.pnlFilters.Controls.Add(this.cmdAllocate);
            this.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilters.Location = new System.Drawing.Point(0, 200);
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.Size = new System.Drawing.Size(1031, 40);
            this.pnlFilters.TabIndex = 2;
            // 
            // ckShowAllFilms
            // 
            this.ckShowAllFilms.Location = new System.Drawing.Point(3, 9);
            this.ckShowAllFilms.Name = "ckShowAllFilms";
            this.ckShowAllFilms.Size = new System.Drawing.Size(136, 24);
            this.ckShowAllFilms.TabIndex = 12;
            this.ckShowAllFilms.Text = "Show All Film Widths";
            this.ckShowAllFilms.UseVisualStyleBackColor = true;
            this.ckShowAllFilms.CheckedChanged += new System.EventHandler(this.CkShowAllFilmsCheckedChanged);
            // 
            // lblPercentofNeedToAllocate
            // 
            this.lblPercentofNeedToAllocate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPercentofNeedToAllocate.Location = new System.Drawing.Point(3, 5);
            this.lblPercentofNeedToAllocate.Name = "lblPercentofNeedToAllocate";
            this.lblPercentofNeedToAllocate.Size = new System.Drawing.Size(3, 23);
            this.lblPercentofNeedToAllocate.TabIndex = 6;
            this.lblPercentofNeedToAllocate.Text = "% of estimated need to Allocate:";
            // 
            // lblAllocationMethod
            // 
            this.lblAllocationMethod.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAllocationMethod.Location = new System.Drawing.Point(3, 12);
            this.lblAllocationMethod.Name = "lblAllocationMethod";
            this.lblAllocationMethod.Size = new System.Drawing.Size(3, 23);
            this.lblAllocationMethod.TabIndex = 9;
            this.lblAllocationMethod.Text = "Allocation Method:";
            // 
            // lblPercentagePoint
            // 
            this.lblPercentagePoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPercentagePoint.Location = new System.Drawing.Point(3, 9);
            this.lblPercentagePoint.Name = "lblPercentagePoint";
            this.lblPercentagePoint.Size = new System.Drawing.Size(3, 23);
            this.lblPercentagePoint.TabIndex = 8;
            this.lblPercentagePoint.Text = "%";
            // 
            // cbxAllocationMethod
            // 
            this.cbxAllocationMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAllocationMethod.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxAllocationMethod.FormattingEnabled = true;
            this.cbxAllocationMethod.Location = new System.Drawing.Point(3, 9);
            this.cbxAllocationMethod.Name = "cbxAllocationMethod";
            this.cbxAllocationMethod.Size = new System.Drawing.Size(7, 21);
            this.cbxAllocationMethod.TabIndex = 10;
            this.cbxAllocationMethod.SelectedIndexChanged += new System.EventHandler(this.CbxAllocationMethodSelectedIndexChanged);
            // 
            // txtPercentOfNeedToAllocate
            // 
            this.txtPercentOfNeedToAllocate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPercentOfNeedToAllocate.Location = new System.Drawing.Point(3, 9);
            this.txtPercentOfNeedToAllocate.Name = "txtPercentOfNeedToAllocate";
            this.txtPercentOfNeedToAllocate.Size = new System.Drawing.Size(7, 20);
            this.txtPercentOfNeedToAllocate.TabIndex = 7;
            this.txtPercentOfNeedToAllocate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPercentOfNeedToAllocate.Enter += new System.EventHandler(this.TxtPercentOfNeedToAllocateEnter);
            this.txtPercentOfNeedToAllocate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPercentOfNeedToAllocateKeyDown);
            this.txtPercentOfNeedToAllocate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPercentOfNeedToAllocateKeyPress);
            this.txtPercentOfNeedToAllocate.Leave += new System.EventHandler(this.TxtPercentOfNeedToAllocateLeave);
            // 
            // cmdAllocate
            // 
            this.cmdAllocate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAllocate.Location = new System.Drawing.Point(3, 5);
            this.cmdAllocate.Name = "cmdAllocate";
            this.cmdAllocate.Size = new System.Drawing.Size(3, 28);
            this.cmdAllocate.TabIndex = 11;
            this.cmdAllocate.UseVisualStyleBackColor = true;
            this.cmdAllocate.Click += new System.EventHandler(this.CmdAllocateClick);
            // 
            // pnlTotals
            // 
            this.pnlTotals.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlTotals.Controls.Add(this.lblCurrentAllocationInformation);
            this.pnlTotals.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTotals.Location = new System.Drawing.Point(0, 562);
            this.pnlTotals.Name = "pnlTotals";
            this.pnlTotals.Size = new System.Drawing.Size(1031, 40);
            this.pnlTotals.TabIndex = 2;
            // 
            // lblCurrentAllocationInformation
            // 
            this.lblCurrentAllocationInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentAllocationInformation.ForeColor = System.Drawing.Color.Blue;
            this.lblCurrentAllocationInformation.Location = new System.Drawing.Point(0, 0);
            this.lblCurrentAllocationInformation.Name = "lblCurrentAllocationInformation";
            this.lblCurrentAllocationInformation.Size = new System.Drawing.Size(1027, 36);
            this.lblCurrentAllocationInformation.TabIndex = 0;
            this.lblCurrentAllocationInformation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IssueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 602);
            this.Controls.Add(this.dgvFilm);
            this.Controls.Add(this.pnlTotals);
            this.Controls.Add(this.pnlFilters);
            this.Controls.Add(this.dgvAllocations);
            this.Controls.Add(this.rtbJobInformation);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "IssueForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllocations)).EndInit();
            this.pnlFilters.ResumeLayout(false);
            this.pnlFilters.PerformLayout();
            this.pnlTotals.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		private System.Windows.Forms.CheckBox ckShowAllFilms;
		private System.Windows.Forms.Label lblCurrentAllocationInformation;
	}
}
