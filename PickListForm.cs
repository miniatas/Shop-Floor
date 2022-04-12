/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 5/31/2013
 * Time: 2:50 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ShopFloor
{
	/// <summary>
	/// Description of Form1.
	/// </summary>
	/// 
	
	public partial class PickListReportForm : Form
	{
		public PickListReportForm(string jobNo)
		{
			InitializeComponent();
			
			List<ReportParameter> paramList = new List<ReportParameter>();
			paramList.Add(new ReportParameter("JobNo", jobNo, false));
   			rptPickList.ServerReport.SetParameters(paramList);
   			rptPickList.Parent = pnlReport;
			rptPickList.RefreshReport();
		}
	}
}
