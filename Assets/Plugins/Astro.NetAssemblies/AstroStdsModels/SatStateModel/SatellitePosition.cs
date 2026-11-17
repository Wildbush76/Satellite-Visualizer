using System;

namespace AFSPC.AstroStds.Models.SatelliteModel
{
   [Serializable]
   public class SatellitePosition
   {
      public double X_km { get; set; }
      public double Y_km { get; set; }
      public double Z_km { get; set; }

      public SatellitePosition()
      {
         X_km = 0;
         Y_km = 0;
         Z_km = 0;
      }
      public SatellitePosition(double[] pos)
      {
         X_km = pos[0];
         Y_km = pos[1];
         Z_km = pos[2];
      }

      public double[] ToArray()
      {
         return new double[] { X_km, Y_km, Z_km };

      }

   }
}
