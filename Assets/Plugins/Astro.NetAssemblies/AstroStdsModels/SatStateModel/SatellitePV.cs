using System;

namespace AFSPC.AstroStds.Models.SatelliteModel
{
   [Serializable]
   public class SatellitePV
   {
      public SatellitePosition SatPos{get; set;}
      public SatelliteVelocity SatVel { get; set; }

      public SatellitePV()
      {
      }

      public SatellitePV(double[] pos, double[] vel)
      {
         SatPos = new SatellitePosition(pos);
         SatVel = new SatelliteVelocity(vel);
      }
   }
}
