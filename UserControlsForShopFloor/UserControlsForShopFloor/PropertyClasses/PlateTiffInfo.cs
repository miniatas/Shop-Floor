using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControlsForShopFloor.Classes
{
   public class PlateTiffInfo
    {
        public int? PlateTiffInfoID { get; set; }
        public int? ProductionPlateID { get; set; }
        public decimal? TiffLength { get; set; }
        public decimal? TiffWidth { get; set; }
        public decimal? CroppedLength { get; set; }
        public decimal? CroppedWidth { get; set; }
        public decimal? ExtendedCutLength { get; set; }


    }
}
