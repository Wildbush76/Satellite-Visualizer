using AFSPC.AstroStds.Models.ObsModel;
using AFSPC.AstroStds.Models.RotasModel;
using AFSPC.AstroStds.Utilities;
using AFSPC.AstroStds.Wrappers;
using System.Text;

namespace AFSPC.AstroStds.Models.DcModel
{
   public static class DcPrintings
   {

      public static void PrintResidualsHeader(StringBuilder sb)
      {
         //               1     2          3           4   5  6   7    8     9   10    11  12   13     14  15   16    17   18    19   20         21   22 23    
         sb.AppendLine("TAG   STA  DATE   TIME        REV  D  U   R   DELTA  R   BETA  R  DELTA  R   RANGE  R RA      R  DEC     R  RR RES     VECTOR RJ O\n");
         sb.AppendLine("NO.   NO. YY DDD HHMM SS.SSS  NO.  B DEG  J   T MIN  J   DEG   J  H  KM  J  RES KM  J RES DEG J  RES DEG J  KM/SEC     MAG KM FL T\n");
      }

      public static void PrintObResiduals(StringBuilder sb, Obs obs, Residuals residuals, ObsRejectionData obsRej)
      {
         string rejFlg;
         string[] obRejErrStr = new string[] { "Sensor data retrieval error", "No sensor location found", "Residual computation error" };
         double azOrRA, elOrDec;

         int obsType = residuals.ObsType;

         // no residual data avaialable due to error
         if (obsRej.Error != 0)
         {
            sb.AppendLine(string.Format(" {0:D5} {1:D3} {2}  **** {3} ****",
               obs.SatNum,                                    // 1. tag no.
               obs.SenNum,                                    // 2. sta no.
               obs.ObsTimeDs50UTC.Dtg18Str,                   // 3. date time YY/DDD HHMM SS.SSS
               obRejErrStr[obsRej.Error - 1]));
            return;
         }


         if (DcParameters.NBruce != 0 || obs.ObsType == 'P' || obs.ObsType == 'V')
            rejFlg = "NR";
         else if (obsRej.Error == 100)
            rejFlg = "XX";
         else if (obsRej.Azimuth + obsRej.Elevation + obsRej.Range + obsRej.RangeRate > 0)
            rejFlg = string.Format("*{0:D1}", obsRej.Azimuth + obsRej.Elevation + obsRej.Range + obsRej.RangeRate);
         else
            rejFlg = "  ";


         //             1   2  3   4   5   6    7/8      9/10     11/12    13/14    15/16   17/18      19/20   21   22 23
         //formatStr = "%5d %3d %s %4d %c %3.0f  %c%8.4f  %c%7.4f %c%8.3f  %c%8.3f  %c%7.3f  %c%8.3f  %c%8.4f %11.3f %s %1d\n";
         string formatStr = "{0:D5} {1:D3} {2} {3,5} {4} {5,3:F0}  {6}{7,8:F4}  {8}{9,7:F4} {10}{11,8:F3}  {12}{13,8:F3}  {14}{15,7:F3}  {16}{17,8:F3}  {18}{19,8:F4}{20,11:F3} {21} {22:D1}\n";


         // for obs that have RA/Dec
         if (obsType == 5 || obsType == 9 || obsType == 19)
         {
            azOrRA = residuals.RADelta_deg;
            elOrDec = residuals.DecDelta_deg;
         }
         else // for obs that have Az/El
         {
            azOrRA = residuals.AzDelta_deg;
            elOrDec = residuals.ElDelta_deg;
         }


         sb.AppendLine(string.Format(formatStr,
            obs.SatNum,                                   // 1. tag no.
            obs.SenNum,                                   // 2. sta no.
            obs.ObsTimeDs50UTC.Dtg18Str,                  // 3. date time YY/DDD HHMM SS.SSS
            (int)residuals.RevNum,                         // 4. rev no.
            DcParameters.IsApplySenBiases ? 'Y' : 'N',        // 5. debias flag 'Y' = yes or ' ' = no
            residuals.SatArgOfLatitude_deg,                    // 6. u deg
            obsRej.Time == 1 ? '*' : ' ',                  // 7. time rejection
            Utility.NonNegativeZero(residuals.DeltaT_min, 4),          // 8. delta t (min)
            obsRej.Beta == 1 ? '*' : ' ',                  // 9. delta beta rejection flag
            Utility.NonNegativeZero(residuals.BetaDelta_deg, 4),            //10. beta (deg)
            obsRej.Height == 1 ? '*' : ' ',                //11. delta height rejection flag 
            Utility.NonNegativeZero(residuals.HeightDelta_km, 3),          //12. delta height (km)
            obsRej.Range == 1 ? '*' : ' ',                 //13. range residual rejection flag
            Utility.NonNegativeZero(residuals.RangeDelta_km, 3),  //14. range residual (km)
            obsRej.Azimuth == 1 ? '*' : ' ',                      //15. azimuth residual rejection flag
            Utility.NonNegativeZero(azOrRA, 3),                   //16. azimuth residual (deg)
            obsRej.Elevation == 1 ? '*' : ' ',                    //17. elevation residual rejection flag
            Utility.NonNegativeZero(elOrDec, 3),                  //18. elevation residual (deg)
            obsRej.RangeRate == 1 ? '*' : ' ',                       //19. range rate residual rejection flag
            Utility.NonNegativeZero(residuals.RngRtDelta_kmps, 4),   //20. range rate residual (km/sec)
            Utility.NonNegativeZero(residuals.VMag_km, 3),           //21. vector magnitude (km)
            rejFlg,                                                  //22. rejection flag: 'NR'=no rejection
            residuals.ObsType));                                     //23. obs type      
      }

      public static void PrintAllObResiduals(StringBuilder sb, int iterCount, long satKey, long[] pObsKeys, int numObs, double[,] obsResArr, int[,] obsRejFlags)
      {

         int i;


         sb.AppendLine(string.Format("SATELLITE NO. {0:D5} ---------------------------   BATCH    ITERATION NO.{1, 3}", ElOpsWrapper.SatNumOf(satKey), iterCount));
         PrintResidualsHeader(sb);

         double[] satElts = new double[9];
         double[] obsElts = new double[9];

         // print obs residuals
         for (i = 0; i < numObs; i++)
         {
            double[] currObsResArr = new double[100];

            // Get the residuals from the array
            for (int j = 0; j < 100; j++) currObsResArr[j] = obsResArr[i, j];

            // populate obs residual structure using data received from the DC dll
            Residuals residuals = new Residuals(currObsResArr, satElts, obsElts, true);

            int[] currObsRejFlags = new int[32];
            for (int j = 0; j < 32; j++) currObsResArr[j] = obsRejFlags[i, j];
            ObsRejectionData obsRej = new ObsRejectionData(currObsRejFlags);

            Obs obs = new Obs(pObsKeys[i]);
            PrintObResiduals(sb, obs, residuals, obsRej);
         }

      }


      // Print DC iteration summary for each iteration
      public static void PrintIterSum(StringBuilder sb, int satNum, DcElts updatedDcElts, DcElts dcElts, IterationData itDat)
      {
         string epochDays;
         string[] correctionType = new string[]{ "TIME ", "PLANE", "7-ELT", "IN-TRK", "8-ELT", "SUBELT" };

         sb.AppendLine("\n");
         sb.AppendLine("X MATRIX:\n");
         sb.AppendLine("        L               N             CHI             PSI              AF              AG             BTERM           (n/a)           AGOM\n");
         sb.AppendLine(string.Format("{0,15:E8} {1, 15:E8} {2, 15:E8} {3, 15:E8} {4, 15:E8} {5, 15:E8} {6, 15:E8} {7, 15:E8} {8, 15:E8} \n",
            itDat.XMatrix[0],
            itDat.XMatrix[1],
            itDat.XMatrix[2],
            itDat.XMatrix[3],
            itDat.XMatrix[4],
            itDat.XMatrix[5],
            itDat.XMatrix[6],
            itDat.XMatrix[7],
            itDat.XMatrix[8]));

         sb.AppendLine("\n\n");
         sb.AppendLine(string.Format("SATELLITE NO. {0:D5} ---------------------------   BATCH    ITERATION NO.{1, 3}\n", satNum, dcElts.IterCount));

         sb.AppendLine(string.Format("{0:D5} Residuals Accepted, {1:D5} Residuals Rejected, RMS_km = {2,9:F3}, Predicted RMS_km = {3,9:F3}", 
            itDat.NumResAccepted, itDat.NumResRejected, itDat.RMS_km, itDat.PredRMS_km));

         string oneLine = "";

         if (DcParameters.UsePredictedRMS)
            oneLine = string.Format(", Predicted RMS_km = {0,9:F3}", itDat.PredRMS_km);

         if (DcParameters.IsWeightedDC)
            oneLine = string.Format(", Unweighted RMS_km = {0,9:F3} km", itDat.UnweightedRMS_km);
         oneLine += oneLine + "\n";

         sb.AppendLine(oneLine);

         sb.AppendLine(string.Format("      (convergence quality indicator: {0,12:F3} km)\n", itDat.ConvValue_km));

         string corrTypeStr = GetCorrTypeStr(itDat.Idc);
         sb.AppendLine(string.Format("{0} ITERATION NO.{1:D3} CORRECTED THE FOLLOWING ELTS: {2}\n\n", correctionType[itDat.CorrType], itDat.SubIter, corrTypeStr));

         epochDays = TimeFuncWrapper.UTCToDtg17Str(dcElts.EpochDs50utc);
         if ((int)DcParameters.IterSummaryOption == BatchDCWrapper.ITER_SUM_KEP)
         {

            sb.AppendLine("ELEMENTS    EPOCH         RMS_km         E          I       NODE      OMEGA       M          N        BTERM      PERIOD    AGOM\n");
            sb.AppendLine("             DAY           KM                 DEGREES   DEGREES   DEGREES   DEGREES    REV/DAY     m**2/kg     MIN     m**2/kg\n");
            PrintKeplerianElts(sb, "UPDATED", epochDays.Substring(5), 0, updatedDcElts);
            PrintKeplerianElts(sb, "CURRENT", epochDays.Substring(5), itDat.RMS_km, dcElts);
         }
         else
         {
            sb.AppendLine("ELEMENTS    EPOCH         RMS_km       L        N      CHI         PSI         AF          AG         BTERM      PERIOD    AGOM\n");
            sb.AppendLine("             DAY           KM    DEGREES   REV/DAY                                                 m**2/kg     MIN     m**2/kg\n");
            PrintEquinoctialElts(sb, "UPDATED", epochDays.Substring(5), 0, updatedDcElts);
            PrintEquinoctialElts(sb, "CURRENT", epochDays.Substring(5), itDat.RMS_km, dcElts);
         }


         sb.AppendLine("\n\n");
      }

      public static string GetCorrTypeStr(int[] idc)
      {
         int i;
         string[] corrFlgStrs = new string[] { "L", "N", "CHI", "PSI", "AF", "AG", "B", "NA", "AGOM" };

         string corrTypeStr = "";

         for (i = 0; i < 9; i++)
         {
            if (idc[i] > 0)
            {
               corrTypeStr += corrFlgStrs[i] + "  ";
            }
         }
         return corrTypeStr;
      }


      public static void PrintKeplerianElts(StringBuilder sb, string element, string epochDays, double rms, DcElts dcElts)
      {
         sb.AppendLine(string.Format("{0,7}  {1,12} {2,10:F3}{3,11:F7}{4,10:F4}{5,10:F4}{6,10:F4}{7,10:F4} {8,12:F8}{9,12:F8}{10,8:F1} {11,12:F8}",
            element,
            epochDays,
            rms,
            dcElts.E,
            dcElts.Incli,
            dcElts.Node,
            dcElts.Omega,
            dcElts.Ma,
            dcElts.N,
            dcElts.Bfield,
            1440.0 / dcElts.N,
            dcElts.Agom));
      }

      public static void PrintEquinoctialElts(StringBuilder sb, string element, string epochDays, double rms, DcElts dcElts)
      {
         sb.AppendLine(string.Format("{0,7}  {1,12} {2,10:F3}{3,9:F4}{4,8:F4}{5,11:F8}{6,11:F8}{7,11:F8} {8,11:F8}{9,11:F8}{10,8:F1} {11,12:F8}",
            element,
            epochDays,
            rms,
            dcElts.L,
            dcElts.N,
            dcElts.Chi,
            dcElts.Psi,
            dcElts.Af,
            dcElts.Ag,
            dcElts.Bfield,
            1440.0 / dcElts.N,
            dcElts.Agom));
      }
   }
}
