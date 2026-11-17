using System;

namespace AFSPC.AstroStds.Models.SatelliteModel
{
   [Serializable]
   public class SatelliteLLH 
   {
      public double Latitude_deg { get; set; }
      public double Longitude_deg { get; set; }
      public double Height_km { get; set; }

      public SatelliteLLH() { }

      public SatelliteLLH(double[] llh)
      {
         Latitude_deg = llh[0];
         Longitude_deg = llh[1];
         Height_km = llh[2];
      }

      public double[] ToArray()
      {
         return new double[] { Latitude_deg, Longitude_deg, Height_km };

      }

   }
}
