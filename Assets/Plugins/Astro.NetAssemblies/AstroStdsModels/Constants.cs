using System;

namespace AFSPC.AstroStds.Models
{
   public static class Constants
   {
      public const double PI = Math.PI;
      public const double TWOPI = 2 * Math.PI;
      public const double DEG2RAD = Math.PI / 180.0;
      public const double RAD2DEG = 180.0 / Math.PI;
      public const double HRSPERDAY = 24.0;
      public const double MINSPERDAY = 1440.0;
      public const double SECSPERDAY = 86400.0;
      public const double MSPERDAY = 86400000.0;
      public const double KM2NM = 1 / 1.852;
      public const double MU_WGS72 = 398600.8; //km^2/sec^2
      public const double MU_EGM96 = 398600.5; //km^2/sec^2


      // difference tolerance
      public static double TOLERANCE = 1e-8;
   }
}
