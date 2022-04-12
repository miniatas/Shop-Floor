using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ShopFloor.PlateForms.PlateCreationBL
{
    class PlateCreationTabLogic
    {
        public TabPage CreateNewTiffEntryTab(int nextTabNumber) {

            string nextTab = nextTabNumber.ToString();

            TabPage nextTiffPage = new TabPage
            {
                Name = "Tiff" + nextTab,
                Text = "Tiff " + nextTab
            };
            PlateCreationEntry plateCreationEntry = new PlateCreationEntry();

            plateCreationEntry.Name = "PlateCreationEntry" + nextTiffPage.Name;
            plateCreationEntry.Dock = DockStyle.Fill;
            plateCreationEntry.BackColor = Color.CornflowerBlue;
            nextTiffPage.Controls.Add(plateCreationEntry);
           

            return nextTiffPage;

        }



    }
}
