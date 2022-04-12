using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Floor.PlateForms.PlateProperties
{
    class PlateTiffRowItems
    {
        public int PlateNumber { get; set; }
        public int MakeNumber { get; set; } 
   
        public bool IsCommonVersion { get; set; } 
        public bool IsCommonPow { get; set; } 
        public decimal TiffArea { get; set; } 
        public string SurfaceOrReverse { get; set; } 
        public decimal CroppedArea { get; set; } 
        public decimal ExtendedArea { get; set; }
        public decimal TiffImageableArea { get; set; } 
        public decimal ImageableAreaPercent { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string p)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }
    }
}

