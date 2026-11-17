using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AFSPC.AstroStds.Wrappers;
using AFSPC.AstroStds.Models.SatStateModel;

namespace AFSPC.AstroStds.Models.LamodModel
{
   public class LamodLook
   {
      public int SatNum { get; set; }
      public string SatRecName { get; set; }
      public long SatKey { get; set; }
      public int SenNum { get; set; }
      public double LookTimeDs50UTC { get; set; } //DateTime LookTime { get; set; }
      public double Azimuth { get; set; }
      public double Elevation { get; set; }
      public double Range { get; set; }
      public double RangeRate { get; set; }


      public LamodLook(int satNum,long satKey,int senNum, double[] lookArr)
      {
         
         //LookTime = TimeFuncWrapper.Ds50UTCToDateTime(lookArr[LamodWrapper.IDX_LOOK_DS50UTC]);
         LookTimeDs50UTC = lookArr[LamodWrapper.IDX_LOOK_DS50UTC];
         Azimuth = lookArr[LamodWrapper.IDX_LOOK_AZIM];
         Elevation = lookArr[LamodWrapper.IDX_LOOK_ELEV];
         Range = lookArr[LamodWrapper.IDX_LOOK_RNG];
         RangeRate = lookArr[LamodWrapper.IDX_LOOK_RNGRT];
         SatNum = satNum;
         SatKey = satKey;
         SenNum = senNum;
         SatRecName = SatStateService.GetRecName(satKey);
         
      }
   }
}
