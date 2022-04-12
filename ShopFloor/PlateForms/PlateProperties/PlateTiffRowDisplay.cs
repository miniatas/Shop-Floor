using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shop_Floor.PlateForms.PlateProperties
{
    public class PlateTiffRowDisplay
    {
      public PlateTiffRowDisplay() {

            // 
            this.PlateNumber.HeaderText = "Plate #";
            this.PlateNumber.Name = "Plate";
            // 
            // MakeNumber
            // 
            this.MakeNumber.HeaderText = "Make #";
            this.MakeNumber.Name = "MakeNumber";

            this.SurfaceOrReverse.HeaderText = "Film Side";
            this.SurfaceOrReverse.Name = "SurfaceOrReverse";
            // 
            this.TiffArea.HeaderText = "Tiff Area";
            this.TiffArea.Name = "TiffArea";
            // 
            // CroppedArea
            // 
            this.CroppedArea.HeaderText = "Cropped Area";
            this.CroppedArea.Name = "CroppedArea";
            // 
            // ExtendedArea
            // 
            this.ExtendedArea.HeaderText = "Extended Cut Area";
            this.ExtendedArea.Name = "ExtendedArea";
            // 
            // TiffImageableArea
            // 
            this.TiffImageableArea.HeaderText = "Tiff Imageable Area";
            this.TiffImageableArea.Name = "TiffImageableArea";
            // 
            // ImageableAreaPercent
            // 
            this.ImageableAreaPercent.HeaderText = "Imageable Area %";
            this.ImageableAreaPercent.Name = "ImageableAreaPercent";
         

        }

        public DataGridViewTextBoxColumn PlateNumber { get; set; } = new DataGridViewTextBoxColumn();
        public DataGridViewTextBoxColumn MakeNumber { get; set; } = new DataGridViewTextBoxColumn();
        public DataGridViewCheckBoxColumn IsCommonVersion { get; set; } = new DataGridViewCheckBoxColumn();
        public DataGridViewCheckBoxColumn IsCommonPow { get; set; } = new DataGridViewCheckBoxColumn();
        public DataGridViewTextBoxColumn TiffArea { get; set; } = new DataGridViewTextBoxColumn();
        public DataGridViewTextBoxColumn SurfaceOrReverse { get; set; } = new DataGridViewTextBoxColumn();
        public DataGridViewTextBoxColumn CroppedArea { get; set; } = new DataGridViewTextBoxColumn();
        public DataGridViewTextBoxColumn ExtendedArea { get; set; } = new DataGridViewTextBoxColumn();
        public DataGridViewTextBoxColumn TiffImageableArea { get; set; } = new DataGridViewTextBoxColumn();
        public DataGridViewTextBoxColumn ImageableAreaPercent { get; set; } = new DataGridViewTextBoxColumn();

    }
}
