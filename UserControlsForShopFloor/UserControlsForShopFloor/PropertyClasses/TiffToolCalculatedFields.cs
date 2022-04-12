using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControlsForShopFloor.PropertyClasses
{
    public class TiffToolCalculatedFields
    {

        public decimal FinalTiffImageableArea { get {

                decimal finalTiffImageable = 0;

                // If there's cropped area, it should be smaller than the tiff area
                if (TotalCroppedArea > 0 && TotalCroppedArea < TotalTillArea && TotalEdgeScrapArea > 0) {

                    finalTiffImageable = TotalCroppedArea + TotalEdgeScrapArea;
                } else
                    finalTiffImageable = TotalTiffArea + TotalEdgeScrapArea;
                
                return finalTiffImageable;
            }
        }

        public decimal TotalCroppedArea { get; set; }
        public decimal TotalEdgeScrapArea { get; set; }

        public decimal TotalTiffArea { get; set; }
        public decimal PercentCoverageCropped { get; set; }
        public decimal PercentCoverageTiff { get; set; }

        public decimal PercentCoverageEdgeScrap { get; set; }
        public decimal TotalTillArea { get; set; }




    }
}
