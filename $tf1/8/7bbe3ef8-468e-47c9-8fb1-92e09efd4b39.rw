/*
 * Created by SharpDevelop.
 * User: minimumiatas
 * Date: 3/29/2011
 * Time: 3:11 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace ShopFloor
{
	using System;
	using System.Windows.Forms;

	/// <summary>
	///  Allows a user to pick a time in 15 minute increments but not before a minimum time parameter.
	/// </summary>
	public partial class DateTimePickerForm : Form
	{
        DateTime minimumStartTime;
		public DateTimePickerForm(string formDescription, DateTime startDateTime, DateTime minimum)
		{
           
            InitializeComponent();
            Text = formDescription;
            dtpStartTime.Value = startDateTime;
            minimumStartTime = minimum;
		}
		
		public DateTime Selection
		{ 
  			get  
  			{
  				return dtpStartTime.Value;
  			}
		} 
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		private void CmdOKClick(object sender, EventArgs e)
		{
            if ((decimal)dtpStartTime.Value.Minute / 15 != Math.Round((decimal)dtpStartTime.Value.Minute / 15, 0))
            {
                MessageBox.Show("Error - you must enter a time in 1/4th hours", "Invalid Time");
                dtpStartTime.Focus();
            }
            else if (dtpStartTime.Value > DateTime.Now)
            {
                MessageBox.Show("Error - you cannot start a job in the future", "Invalid Time");
                dtpStartTime.Focus();
            }
            else if (dtpStartTime.Value < minimumStartTime)
            { 
                MessageBox.Show("Error - you cannot start a job before " + minimumStartTime.ToLongDateString() + " at " + minimumStartTime.ToLongTimeString(), "Invalid Time");
                dtpStartTime.Focus();
            }
			else
			{
				Close();
			}
		}
	}
}
