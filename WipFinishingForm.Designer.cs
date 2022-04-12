/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 11/1/2011
 * Time: 10:59 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ShopFloor
{
	public partial class WipFinishingForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.DataGridViewTextBoxColumn lf;
		private System.Windows.Forms.DataGridViewTextBoxColumn pounds;
		private System.Windows.Forms.DataGridViewTextBoxColumn units;
		private System.Windows.Forms.DataGridViewTextBoxColumn uom;
		private System.Windows.Forms.DataGridViewTextBoxColumn rollId;
		private System.Windows.Forms.Button cmdCreatePallet;
		private System.Windows.Forms.Button cmdAddRoll;
		private System.Windows.Forms.Label lblRollsToPalletize;
		private System.Windows.Forms.DataGridView dgvRollsToPalletize;
		private System.Windows.Forms.RichTextBox rtbJobInformation;
		
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			this.rtbJobInformation = new System.Windows.Forms.RichTextBox();
			this.dgvRollsToPalletize = new System.Windows.Forms.DataGridView();
			this.rollId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.units = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.uom = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.pounds = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.lf = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.lblRollsToPalletize = new System.Windows.Forms.Label();
			this.cmdAddRoll = new System.Windows.Forms.Button();
			this.cmdCreatePallet = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)this.dgvRollsToPalletize).BeginInit();
			this.SuspendLayout();
			// 
			// rtbJobInformation
			// 
			this.rtbJobInformation.BackColor = System.Drawing.SystemColors.Control;
			this.rtbJobInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (byte)0);
			this.rtbJobInformation.ForeColor = System.Drawing.Color.Blue;
			this.rtbJobInformation.Location = new System.Drawing.Point(12, 10);
			this.rtbJobInformation.Name = "rtbJobInformation";
			this.rtbJobInformation.ReadOnly = true;
			this.rtbJobInformation.Size = new System.Drawing.Size(862, 47);
			this.rtbJobInformation.TabIndex = 0;
			// this.rtbJobInformation.Text = string.Empty;
			// 
			// dgvRollsToPalletize
			// 
			this.dgvRollsToPalletize.AllowUserToAddRows = false;
			this.dgvRollsToPalletize.AllowUserToResizeColumns = false;
			this.dgvRollsToPalletize.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvRollsToPalletize.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dgvRollsToPalletize.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvRollsToPalletize.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] 
			                                          {
									this.rollId,
									this.units,
									this.uom,
									this.pounds,
									this.lf
			                                          });
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(byte)0), ((int)(byte)0), ((int)(byte)0));
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvRollsToPalletize.DefaultCellStyle = dataGridViewCellStyle6;
			this.dgvRollsToPalletize.Location = new System.Drawing.Point(12, 97);
			this.dgvRollsToPalletize.Name = "dgvRollsToPalletize";
			this.dgvRollsToPalletize.ReadOnly = true;
			this.dgvRollsToPalletize.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.dgvRollsToPalletize.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvRollsToPalletize.ShowEditingIcon = false;
			this.dgvRollsToPalletize.Size = new System.Drawing.Size(360, 493);
			this.dgvRollsToPalletize.TabIndex = 1;
			this.dgvRollsToPalletize.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.DgvRollsToPalletizeRowsRemoved);
			// 
			// rollId
			// 
			dataGridViewCellStyle2.NullValue = null;
			this.rollId.DefaultCellStyle = dataGridViewCellStyle2;
			this.rollId.HeaderText = "Roll ID";
			this.rollId.Name = "RollID";
			this.rollId.ReadOnly = true;
			this.rollId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.rollId.Width = 60;
			// 
			// units
			// 
			dataGridViewCellStyle3.Format = "N0";
			dataGridViewCellStyle3.NullValue = null;
			this.units.DefaultCellStyle = dataGridViewCellStyle3;
			this.units.HeaderText = "Units";
			this.units.Name = "Units";
			this.units.ReadOnly = true;
			this.units.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.units.Width = 60;
			// 
			// uom
			// 
			this.uom.HeaderText = "UOM";
			this.uom.Name = "UOM";
			this.uom.ReadOnly = true;
			this.uom.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.uom.Width = 60;
			// 
			// pounds
			// 
			dataGridViewCellStyle4.NullValue = null;
			this.pounds.DefaultCellStyle = dataGridViewCellStyle4;
			this.pounds.HeaderText = "Pounds";
			this.pounds.Name = "Pounds";
			this.pounds.ReadOnly = true;
			this.pounds.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.pounds.Width = 60;
			// 
			// lf
			// 
			dataGridViewCellStyle5.NullValue = null;
			this.lf.DefaultCellStyle = dataGridViewCellStyle5;
			this.lf.HeaderText = "LF";
			this.lf.Name = "LF";
			this.lf.ReadOnly = true;
			this.lf.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.lf.Width = 60;
			// 
			// lblRollsToPalletize
			// 
			this.lblRollsToPalletize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (byte)0);
			this.lblRollsToPalletize.Location = new System.Drawing.Point(12, 74);
			this.lblRollsToPalletize.Name = "lblRollsToPalletize";
			this.lblRollsToPalletize.Size = new System.Drawing.Size(360, 20);
			this.lblRollsToPalletize.TabIndex = 1;
			this.lblRollsToPalletize.Text = "Rolls To Palletize";
			this.lblRollsToPalletize.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// cmdAddRoll
			// 
			this.cmdAddRoll.Location = new System.Drawing.Point(398, 187);
			this.cmdAddRoll.Name = "cmdAddRoll";
			this.cmdAddRoll.Size = new System.Drawing.Size(110, 38);
			this.cmdAddRoll.TabIndex = 2;
			this.cmdAddRoll.Text = "Add Roll";
			this.cmdAddRoll.UseVisualStyleBackColor = true;
			this.cmdAddRoll.Click += new System.EventHandler(this.CmdAddRollClick);
			// 
			// cmdCreatePallet
			// 
			this.cmdCreatePallet.Location = new System.Drawing.Point(398, 261);
			this.cmdCreatePallet.Name = "cmdCreatePallet";
			this.cmdCreatePallet.Size = new System.Drawing.Size(110, 38);
			this.cmdCreatePallet.TabIndex = 3;
			this.cmdCreatePallet.Text = "Create Pallet";
			this.cmdCreatePallet.UseVisualStyleBackColor = true;
			this.cmdCreatePallet.Click += new System.EventHandler(this.CmdCreatePalletClick);
			// 
			// WipFinishingForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(884, 602);
			this.Controls.Add(this.cmdCreatePallet);
			this.Controls.Add(this.cmdAddRoll);
			this.Controls.Add(this.lblRollsToPalletize);
			this.Controls.Add(this.dgvRollsToPalletize);
			this.Controls.Add(this.rtbJobInformation);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WipFinishingForm";
			this.Text = "Palletize Non-Slit WIP Inventory";
			((System.ComponentModel.ISupportInitialize)this.dgvRollsToPalletize).EndInit();
			this.ResumeLayout(false);
		}
	}
}
