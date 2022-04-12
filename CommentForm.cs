/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 9/24/2014
 * Time: 2:32 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ShopFloor
{
	/// <summary>
	/// Description of Form1.
	/// </summary>
	public partial class CommentForm : Form
	{
        private bool confirmClose = false;
		public CommentForm(string title, string comment, bool saveVisible)
		{
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            
			InitializeComponent();
            Text = title;
            rtbComment.Text = comment;
            pnlBottom.Visible = saveVisible;
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
		}
		
		public string Comment
		{
			get
			{
				return rtbComment.Text.Trim();
			}
		}
		
        public int setWidth
        {
            set
            {
                this.Width = value;
            }
        }
        
		void CmdSaveClick(object sender, EventArgs e)
		{
            confirmClose = true;
			this.Close();
		}

        private void CommentForm_Shown(object sender, EventArgs e)
        {
            if (Modal)
            {
                pnlBottom.Visible = true;
            }
        }

        private void CommentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!confirmClose)
            {
                MessageBox.Show("You cannot close the Notes window while entering production", "Invalid Click");
                e.Cancel = true;    
            }
        }
    }
}
