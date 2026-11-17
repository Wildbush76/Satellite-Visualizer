using System;
using System.Text;
using System.IO;
using AFSPC.AstroStds.Models.ObsModel;
using AFSPC.AstroStds.Wrappers;
using AFSPC.AstroStds.Models.SatelliteModel;

namespace AFSPC.AstroStds.Models.RotasModel
{
   public static class RotasService
   {
      static RotasService()
      {
      }

      public static void PrintRotasHeader(StreamWriter sw)
      {
         //             0   1     2    3          4              5   6  7      8      9          10       11   12   13       14         20      21          15   16   17    18  19  
         sw.WriteLine(" A  NEW   OLD  STA  DATE   TIME          REV  U  O    DELTA    BETA      DELTA    RANGE     AZ RES   EL RES    RA RES  DEC RES     VECTOR ORB  AGE   OBS P");
         sw.WriteLine(" S  SAT   SAT  NO.  YY DDD HHMM SS.SSS   NO. DEG T    T MIN     DEG      H  KM   RES KM        DEG      DEG       DEG      DEG     MAG KM CLS  DAYS  NO. R");
      }

      public static void PrintRotasHeader(StringBuilder sb)
      {
         //             0   1     2    3          4              5   6  7      8      9          10       11   12   13       14         20      21          15   16   17    18  19  
         sb.AppendLine(" A  NEW   OLD  STA  DATE   TIME          REV  U  O    DELTA    BETA      DELTA    RANGE     AZ RES   EL RES    RA RES  DEC RES     VECTOR ORB  AGE   OBS P");
         sb.AppendLine(" S  SAT   SAT  NO.  YY DDD HHMM SS.SSS   NO. DEG T    T MIN     DEG      H  KM   RES KM        DEG      DEG       DEG      DEG     MAG KM CLS  DAYS  NO. R");
      }

      public static void PrintObsResiduals(StreamWriter sw, int obsNum, Residuals resid)
      {
         string[] SATCAT = new string[]{"SYN", "D-S", "DEC", "ROU"};
         int propType = SatStateService.GetPropType(resid.SatKey);
         char propTypeChar;

         if (propType == DllMainWrapper.PROPTYPE_GP)
            propTypeChar = 'G';
         else if (propType == DllMainWrapper.PROPTYPE_SP)
            propTypeChar = 'S';
         else if (propType == DllMainWrapper.PROPTYPE_X)
            propTypeChar = 'X';
         else
            propTypeChar = ' ';
         
         int obsType = resid.ObsType;
         bool isRangeComputed = false;
         if (obsType == 1 || obsType == 5 || (obsType % 10) == 8 || (obsType % 10) == 9)
            isRangeComputed = true;


         Obs obs = new Obs(resid.ObsKey);
         sw.WriteLine(" {0:D1} {1:D5} {2:D5} {3:D3}  {4} {5:D5} {6:D3} {7} {8, 8:F4} {9, 8:F4} {10, 9:F3} {11, 8:F1}{12}  {13, 8:F5} {14, 8:F5}  {20, 8:F5} {21, 8:F5}   {15, 8:F3} {16, 3} {17, 5:F2} {18:D4} {19, 1}",
            resid.CompASTAT,
            obs.SatNum,
            obs.SatNum,
            obs.SenNum,
            obs.ObsTimeDs50UTC.Dtg18Str,
            (int)resid.RevNum,
            (int)resid.SatArgOfLatitude_deg,
            resid.ObsType,
            resid.DeltaT_min,
            resid.BetaDelta_deg,
            resid.HeightDelta_km,
            resid.RangeDelta_km,
            isRangeComputed ? 'C' : ' ',
            resid.AzDelta_deg,
            resid.ElDelta_deg,
            resid.VMag_km,
            SATCAT[resid.SatCat-1],
            resid.Age_days,
            obsNum,
            propTypeChar,
            resid.RADelta_deg,
            resid.DecDelta_deg);
      }

      public static void PrintObsResiduals(StringBuilder sb, int obsNum, Residuals resid)
      {
         string[] SATCAT = new string[] { "SYN", "D-S", "DEC", "ROU" };
         int propType = SatStateService.GetPropType(resid.SatKey);
         char propTypeChar;

         if (propType == DllMainWrapper.PROPTYPE_GP)
            propTypeChar = 'G';
         else if (propType == DllMainWrapper.PROPTYPE_SP)
            propTypeChar = 'S';
         else if (propType == DllMainWrapper.PROPTYPE_X)
            propTypeChar = 'X';
         else
            propTypeChar = ' ';

         int obsType = resid.ObsType;
         bool isRangeComputed = false;
         if (obsType == 1 || obsType == 5 || (obsType % 10) == 8 || (obsType % 10) == 9)
            isRangeComputed = true;


         Obs obs = new Obs(resid.ObsKey);
         sb.AppendLine(String.Format(" {0:D1} {1:D5} {2:D5} {3:D3}  {4} {5:D5} {6:D3} {7} {8, 8:F4} {9, 8:F4} {10, 9:F3} {11, 8:F1}{12}  {13, 8:F5} {14, 8:F5}  {20, 8:F5} {21, 8:F5}   {15, 8:F3} {16, 3} {17, 5:F2} {18:D4} {19, 1}",
            resid.CompASTAT,
            obs.SatNum,
            obs.SatNum,
            obs.SenNum,
            obs.ObsTimeDs50UTC.Dtg18Str,
            (int)resid.RevNum,
            (int)resid.SatArgOfLatitude_deg,
            resid.ObsType,
            resid.DeltaT_min,
            resid.BetaDelta_deg,
            resid.HeightDelta_km,
            resid.RangeDelta_km,
            isRangeComputed ? 'C' : ' ',
            resid.AzDelta_deg,
            resid.ElDelta_deg,
            resid.VMag_km,
            SATCAT[resid.SatCat - 1],
            resid.Age_days,
            obsNum,
            propTypeChar,
            resid.RADelta_deg,
            resid.DecDelta_deg));
      }
   }
}
