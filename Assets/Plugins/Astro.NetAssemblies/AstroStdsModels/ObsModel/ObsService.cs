using AFSPC.AstroStds.Wrappers;
using System;
using System.IO;
using System.Text;

namespace AFSPC.AstroStds.Models.ObsModel
{
   public class ObsService
   {
      // Print ob header 
      public static void PrintObsHeader(StreamWriter sw, char obsType)
      {
         if (obsType == 'P' || obsType == 'V')
         {
            sw.WriteLine("  OBS A  TAG  SEN         TIME         OB       X          Y        Z  ");
            sw.WriteLine("  NO. S  NO.  NO.  YY DDD HHMM SS.SSS CL/TP    (km)       (km)     (km)");
            sw.WriteLine(" ======================================================================");
         }
         else
         {
            sw.WriteLine("  OBS A  TAG  SEN         TIME         OB    AZ/RT.A.   EL/DECL   RANGE    RG-RT");
            sw.WriteLine("  NO. S  NO.  NO.  YY DDD HHMM SS.SSS CL/TP   (deg)      (deg)     (km)   (km/s)");
            sw.WriteLine(" ===============================================================================");
         }
      }

      // Print ob header 
      public static void PrintObsHeader(StringBuilder sb, char obsType)
      {
         if (obsType == 'P' || obsType == 'V')
         {
            sb.AppendLine("  OBS A  TAG  SEN         TIME         OB       X          Y        Z  ");
            sb.AppendLine("  NO. S  NO.  NO.  YY DDD HHMM SS.SSS CL/TP    (km)       (km)     (km)");
            sb.AppendLine(" ======================================================================");
         }
         else
         {
            sb.AppendLine("  OBS A  TAG  SEN         TIME         OB    AZ/RT.A.   EL/DECL   RANGE    RG-RT");
            sb.AppendLine("  NO. S  NO.  NO.  YY DDD HHMM SS.SSS CL/TP   (deg)      (deg)     (km)   (km/s)");
            sb.AppendLine(" ===============================================================================");
         }
      }

      // Print ob data
      public static void PrintObsData(StreamWriter sw, int obsNum, Obs obs)
      {
         sw.WriteLine(FormObsLine(obsNum, obs));
      }

      // Print ob data
      public static void PrintObsData(StringBuilder sb, int obsNum, Obs obs)
      {
         sb.AppendLine(FormObsLine(obsNum, obs));
      }

      private static string FormObsLine(int obsNum, Obs obs)
      {
         string line = "";

         char obsType = obs.ObsType;
         line = String.Format(" {0:D4} {1:D1} {2:D5} {3:D3}  {4} {5} {6}", obsNum, obs.ASTAT, obs.SatNum, obs.SenNum,
           obs.ObsTimeDs50UTC.Dtg18Str, obs.SecClass, obsType);


         if ((obsType >= '1' && obsType <= '5') || obsType == '8' || obsType == '9' || obsType == 'M' || obsType == 'O')
            line += String.Format("   {0, 8:F4}{1, 10:F4}", obs.AzOrRA, obs.ElOrDec);
         else if (obsType == 'P' || obsType == 'V')
            line += String.Format("{0, 11:F3}{1, 11:F3}{2, 11:F3}", obs.Pos[0], obs.Pos[1], obs.Pos[2]);

         if (obs.Range_km != 0)
            line += String.Format("{0, 11:F3}", obs.Range_km);

         if (obs.RangeRate_kmps != 0)
            line += String.Format("{0, 7:F4}", obs.RangeRate_kmps);

         return line;
      }
   }
}
