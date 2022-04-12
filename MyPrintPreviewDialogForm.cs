/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 4/24/2013
 * Time: 10:18 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Reflection;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace ShopFloor
{
	using System;
	using System.Reflection;
	using System.Drawing;
	using System.Drawing.Printing;
	using System.Windows.Forms;
	/// <summary>
	/// Description of Form1.
	/// </summary>
	public partial class MyPrintPreviewDialog : System.Windows.Forms.PrintPreviewDialog
	{
		private ToolStripButton myPrintButton;
 		private int numberCopies;
	 	public MyPrintPreviewDialog(int noCopies) : base()
 		{
 			Type t = typeof(PrintPreviewDialog);
 			FieldInfo fi = t.GetField("toolStrip1", BindingFlags.Instance | BindingFlags.NonPublic);
 			FieldInfo fi2 = t.GetField("printToolStripButton", BindingFlags.Instance | BindingFlags.NonPublic);
 			ToolStrip toolStrip1 = (ToolStrip)fi.GetValue(this);
 			ToolStripButton printButton = (ToolStripButton)fi2.GetValue(this);
 			printButton.Visible = false;
 			myPrintButton = new ToolStripButton();
 			myPrintButton.ToolTipText = printButton.ToolTipText;
 			myPrintButton.ImageIndex = 0;
 			ToolStripItem[] oldButtons = new ToolStripItem [toolStrip1.Items.Count];
 			for ( int i = 0; i < oldButtons.Length; i++ )
 			{
 				oldButtons[i] = toolStrip1.Items[i];
 			}
 
 			toolStrip1.Items.Clear();
 			toolStrip1.Items.Add (myPrintButton);
 			for ( int i = 0; i < oldButtons.Length; i++ )
 			{
 				toolStrip1.Items.Add(oldButtons[i]);
 			}
		
 			toolStrip1.ItemClicked += new ToolStripItemClickedEventHandler(toolStrip1_Click);
 			numberCopies = noCopies;
		}
 
 		private void toolStrip1_Click(object sender, ToolStripItemClickedEventArgs eventargs)
 		{
 			if (eventargs.ClickedItem == myPrintButton)
 			{
			// if you want to pick printer and number of copies use below 3 lines:
 			//	PrintDialog printDialog1 = new PrintDialog();
 			//	printDialog1.Document = this.Document;
  			//	if (printDialog1.ShowDialog() == DialogResult.OK)*/
 				{
 					for (int i=1; i <= numberCopies; i++)
 					{
 						this.Document.Print();
 					}
 				
 					this.Close();
				}
 			}
 		}
	}
}
