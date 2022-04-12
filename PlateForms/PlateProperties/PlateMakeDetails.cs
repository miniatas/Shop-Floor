using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFloor.PlateForms.PlateProperties
{
  public  class PlateMakeDetails
    {
        public int ProductionPlateID { get; set; } 
        public int PlateMakeID { get; set; }
        public int PlateTiffInfoID { get; set; }
        public DateTime DateCreated { get; set; }
        public int ImpressionsRan { get; set; }
        public int FeetRan { get; set; }
        public int TimesMounted { get; set; }
        public byte[] RowRevision { get; set; }
        public string RowRevisionUser { get; set; }
        public int PlateStatusID { get; set; }
        public int PlateMakeDestructionID { get; set; }

    }
}
