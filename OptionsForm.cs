/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 2/10/2011
 * Time: 11:11 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace ShopFloor
{
	using System;
	using System.Windows.Forms;

	/// <summary>
	/// Description of Form1.
	/// </summary>
	public partial class OptionsForm : Form
	{
		private int initialSelectedIndex = 0;
        private bool abortOption;

		public OptionsForm(string description, bool largeForm, bool abortChoice)
		{
			InitializeComponent();
			
			this.lblDescription.Text = description;
            abortOption = abortChoice;
			if (largeForm)
			{
				this.Width = 700;
                this.Height = 149;
				lblDescription.Width = 676;
				cboOption.Width = 676;
				cmdOK.Width = 676;
			}
		}
		
		public string Option
		{ 
  			get 
  			{
  				return cboOption.Text;
  			}
		}

        public ComboBoxItem ComboBoxItemOption
        {
            get
            {
                return cboOption.SelectedItem as ComboBoxItem;
            }
        }

        public int InitialSelectedIndex
		{
			set
			{
				initialSelectedIndex = value;
			}
		}
		
        public string DisplayMember
        {
            set
            {
                cboOption.DisplayMember = value;
            }
        }
		public void AddOption(string newOption)
		{
			cboOption.Items.Add(newOption);
		}

        public void AddComboItemClassOption(ComboBoxItem newOption)
        {
            cboOption.Items.Add(newOption);
        }

        private void CmdOKClick(object sender, EventArgs e)
		{
			this.Close();
		}
		
		void OptionsFormShown(object sender, EventArgs e)
		{
            if (abortOption)
            {
                cboOption.Items.Add("Abort");
            }
			cboOption.SelectedIndex = initialSelectedIndex;
		}
	}
}
