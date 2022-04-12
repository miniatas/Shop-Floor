using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Floor.PlateForms.PlateProperties
{
    class PlateTiffCalculationsFromTab
    {
        public int ProductionPlateID { get; }

        public int MakeOfPlate { get; set; }
        public decimal FinalTiffImageableArea
        {
            get;          
        }

        public decimal TotalCroppedArea { get;  }
        public decimal TotalEdgeScrapArea { get;  }

        public decimal TotalTiffArea { get;  }

    }
}
