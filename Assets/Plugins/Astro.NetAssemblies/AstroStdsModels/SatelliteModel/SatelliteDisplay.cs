using AFSPC.AstroStds.Models.SatelliteModel;
using AFSPC.AstroStds.Models.DisplayModel;
using AFSPC.AstroStds.Utilities;
using AFSPC.AstroStds.Wrappers;
using System;

namespace AFSPC.AstroStds.Models.SatelliteModel
{
   [Serializable]
   public class SatelliteDisplay : ObjectDisplay
   {
      //public bool   SelPrimRefSelected { get; set; } // for selected, primary, or reference
      public int    SatNum { get; set; }
      public string Epoch { get; set; }
      //public bool   SecPredSelected { get; set; } // for secondary, or predicted
      public Satellite Sat { get; set; }
      //public RowCheckStates CheckStates; // { get; set; }


      public SatelliteDisplay(long satKey):base() // call base constructor to generate a random color
      {
         this.ID = satKey;
         this.Label = SatStateService.GetRecName(satKey);

         //this.SelPrimRefSelected = true;
         //this.SecPredSelected = true;
         this.Epoch = SatStateService.ToSatEpochDecDay(satKey);
         this.SatNum = ElOpsWrapper.SatNumOf(satKey);
         this.Sat = new Satellite(satKey);
         //this.CheckStates = new RowCheckStates();
      }

      
      public SatelliteDisplay(ObjectDisplay objDisp):this(objDisp.ID)
      {
         this.Color = objDisp.Color; // Copy objDisp's current color
         this.Label = objDisp.Label;
      }

      public SatelliteDisplay(SatelliteDisplay satDisp)
      {
         this.ID = satDisp.ID;
         this.Color = satDisp.Color;
         this.Label = satDisp.Label;
         this.SymbolSize = satDisp.SymbolSize;
      }

   }
}
