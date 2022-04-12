using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFloor.Classes.PlateCreation
{
    class ProductionPlateNumberInfo
    {
        public int ProductionPlateNumber { get; set; }
        public string Color { get; set; }
        public int? LPI { get; set; }

        public int PlateTypeID { get; set; }

        public bool IsPOWCommon { get; set; }

        public bool IsVersionCommon { get; set; }

        public int? InkPercentage { get; set; }

               
    }
}
