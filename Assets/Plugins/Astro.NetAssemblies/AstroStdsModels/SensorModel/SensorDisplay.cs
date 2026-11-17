using AFSPC.AstroStds.Models.DisplayModel;
using AFSPC.AstroStds.Wrappers;
using System;

namespace AFSPC.AstroStds.Models.SensorModel
{
   [Serializable]
   public class SensorDisplay : ObjectDisplay
   {
      public int SenNum { get; set; }
      public Sensor Sen { get; set; }


      public SensorDisplay(long senKey)
         : base() // call base constructor to generate a random color
      {
         this.Sen = new Sensor(senKey);
         this.ID = senKey;
         this.Label = this.Sen.SenDesc; // SatStateService.GetRecName(senKey);

         this.SenNum = SensorWrapper.SenNumOf(senKey); ;
      }


      public SensorDisplay(ObjectDisplay objDisp)
         : this((int)objDisp.ID)
      {
         this.Color = objDisp.Color; // Copy objDisp's current color
         this.Label = objDisp.Label;
      }
   }
}
