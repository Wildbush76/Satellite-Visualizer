using System;

namespace AFSPC.AstroStds.Models.SatelliteModel
{
   [Serializable]
   public class SatelliteVelocity 
   {
      public double XD_kmps { get; set; }
      public double YD_kmps { get; set; }
      public double ZD_kmps { get; set; }

      public SatelliteVelocity() { }

      public SatelliteVelocity(double[] vel)
      {
         XD_kmps = vel[0];
         YD_kmps = vel[1];
         ZD_kmps = vel[2];
      }

      public double[] ToArray()
      {
         return new double[] { XD_kmps, YD_kmps, ZD_kmps };

      }

   }
}
