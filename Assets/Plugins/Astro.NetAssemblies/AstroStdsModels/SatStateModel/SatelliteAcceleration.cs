using System;

namespace AFSPC.AstroStds.Models.SatStateModel
{
   [Serializable]
   public class SatelliteAcceleration
   {

      public double XDD_km { get; set; }
      public double YDD_km { get; set; }
      public double ZDD_km { get; set; }

      public SatelliteAcceleration()
      {
         XDD_km = 0;
         YDD_km = 0;
         ZDD_km = 0;
      }
      public SatelliteAcceleration(double[] acc)
      {
         XDD_km = acc[0];
         YDD_km = acc[1];
         ZDD_km = acc[2];
      }

      public double[] ToArray()
      {
         return new double[] { XDD_km, YDD_km, ZDD_km };
      }
   }
}
