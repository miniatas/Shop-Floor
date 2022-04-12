using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ShopFloor
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
            ModulesClass.FillDowntimeComboBox(comboBox1, "1");
            ModulesClass.FillScrapReasonComboBox(comboBox2, "2");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show((comboBox1.SelectedItem as ComboBoxItem).Key);
            if ((comboBox1.SelectedItem as ComboBoxItem).NotesRequired)
            {
                MessageBox.Show("Notes Required");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show((comboBox2.SelectedItem as ComboBoxItem).Key);
        }
    }
}
