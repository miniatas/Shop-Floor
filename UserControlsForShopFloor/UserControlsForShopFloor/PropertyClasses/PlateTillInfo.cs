using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControlsForShopFloor.Classes
{
    class PlateTillInfo
    {
        public int PlateTillID { get; set; }
        public int PlateTillDimensionsID { get; set; }
        public int PlateTillGroupID { get; set; }

        public decimal PlateTillLength { get; set; }
        public decimal PlateTIllWidth { get; set; }
        public decimal ImageableLength { get; set; }
        public decimal ImageableWidth { get; set; }

    }
}
