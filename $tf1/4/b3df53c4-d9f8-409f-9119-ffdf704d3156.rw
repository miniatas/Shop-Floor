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
	using System;
	using System.Drawing;
	using System.Windows.Forms;

	/// <summary>
	/// Create a non-modal form to show film used on a job while on the production form.
	/// </summary>
	public partial class ConsumedFilmForm : Form
	{
		public ConsumedFilmForm(bool unwindVisible)
		{
            InitializeComponent();

            dgvConsumedRolls.Columns[4].Visible = unwindVisible;
            if (!unwindVisible)
            {
                Width = this.Width - 60;
            }
        }
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        public void AddRoll(string operatorName, DateTime transactionDate, string rollNumber, string activity, string unwind, string description, int feet)
        {
            dgvConsumedRolls.Rows.Add(operatorName, transactionDate.ToString("g"), "R" + rollNumber, activity, unwind, description, feet.ToString("N0"));
            dgvConsumedRolls.Rows[this.dgvConsumedRolls.Rows.Count - 1].Selected = true;
            dgvConsumedRolls.FirstDisplayedScrollingRowIndex = this.dgvConsumedRolls.Rows.Count - 1;
        }

        public void ClearGrid()
        {
            dgvConsumedRolls.Rows.Clear();
        }

        private void ConsumedFilmFormFormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Visible = false;
        }
    }
}
