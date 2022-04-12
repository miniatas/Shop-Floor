/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 5/31/2013
 * Time: 2:50 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ShopFloor
{
	partial class PickListReportForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.rptPickList = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnlReport = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // rptPickList
            // 
            this.rptPickList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rptPickList.Location = new System.Drawing.Point(0, 0);
            this.rptPickList.Name = "ReportViewer";
            this.rptPickList.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
            this.rptPickList.ServerReport.ReportPath = "/Pick List/Pick List";
            this.rptPickList.ServerReport.ReportServerUrl = new System.Uri("http://reports.overwraps.com/reportserver", System.UriKind.Absolute);
            this.rptPickList.Size = new System.Drawing.Size(396, 246);
            this.rptPickList.TabIndex = 0;
            // 
            // pnlReport
            // 
            this.pnlReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlReport.Location = new System.Drawing.Point(0, 0);
            this.pnlReport.Name = "pnlReport";
            this.pnlReport.Size = new System.Drawing.Size(1032, 730);
            this.pnlReport.TabIndex = 0;
            // 
            // PickListReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 730);
            this.Controls.Add(this.pnlReport);
            this.Name = "PickListReportForm";
            this.ResumeLayout(false);

		}
		private System.Windows.Forms.Panel pnlReport;
		private Microsoft.Reporting.WinForms.ReportViewer rptPickList;
	}
}
