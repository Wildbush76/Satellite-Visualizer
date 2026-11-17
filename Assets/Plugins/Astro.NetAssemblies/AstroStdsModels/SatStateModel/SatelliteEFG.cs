using System;

namespace AFSPC.AstroStds.Models.SatelliteModel
{
   [Serializable]
   public class SatelliteEFG 
   {
      public double E_km { get; set; }
      public double F_km { get; set; }
      public double G_km { get; set; }

      public SatelliteEFG()
      {
         E_km = 0;
         F_km = 0;
         G_km = 0;
      }

      public SatelliteEFG(double[] efg)
      {
         E_km = efg[0];
         F_km = efg[1];
         G_km = efg[2];
      }

      public double[] ToArray()
      {
         return new double[] { E_km, F_km, G_km };

      }
   }
}
