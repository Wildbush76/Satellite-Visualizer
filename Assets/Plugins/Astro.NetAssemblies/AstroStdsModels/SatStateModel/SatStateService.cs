using System;
using System.Collections.Generic;
using System.Text;
using AFSPC.AstroStds.Wrappers;
using AFSPC.AstroStds.Utilities;
using System.IO;


namespace AFSPC.AstroStds.Models.SatelliteModel
{
   public static class SatStateService
   {

      // Return orbital element type from the input satKey
      public static int GetOrbEltType(long satKey)
      {
         return Convert.ToInt16(satKey % 10);
      }

      // Return propagation type from the input satKey
      public static int GetPropType(long satKey)
      {
         int orbEltType = GetOrbEltType(satKey);

         if (orbEltType == DllMainWrapper.ELTTYPE_TLE_SGP  || orbEltType == DllMainWrapper.ELTTYPE_TLE_SGP4 || 
             orbEltType == DllMainWrapper.ELTTYPE_TLE_PPT3 || orbEltType == DllMainWrapper.ELTTYPE_TLE_XP)
            return DllMainWrapper.PROPTYPE_GP;
         else if (orbEltType == DllMainWrapper.ELTTYPE_TLE_SP || orbEltType == DllMainWrapper.ELTTYPE_SPVEC_B1P || orbEltType == DllMainWrapper.ELTTYPE_VCM)
            return DllMainWrapper.PROPTYPE_SP;
         else if (orbEltType == DllMainWrapper.ELTTYPE_EXTEPH)
            return DllMainWrapper.PROPTYPE_X;
         else
            return DllMainWrapper.PROPTYPE_UK;

      }

      // Count number of loaded satellites of the specified type
      public static int SatStateGetTypeCount(int type, List<long> satKeys)
      {
         int count = 0;

         foreach (long satKey in satKeys)
         {
            if (GetOrbEltType(satKey) == type)
               count++;
         }

         return count;
      }


      // Determine if the orbital element's satKey is SGP4
      public static bool SatStateIsSgp4(long satKey)
      {
         int orbEltType = GetOrbEltType(satKey);

         return (orbEltType == DllMainWrapper.ELTTYPE_TLE_SGP  || orbEltType == DllMainWrapper.ELTTYPE_TLE_PPT3 || 
                 orbEltType == DllMainWrapper.ELTTYPE_TLE_SGP4 || orbEltType == DllMainWrapper.ELTTYPE_TLE_XP);
      }


      // Determine if the orbital element's satKey is SP
      public static bool SatStateIsSp(long satKey)
      {
         int orbEltType = GetOrbEltType(satKey);

         return (orbEltType == DllMainWrapper.ELTTYPE_TLE_SP || orbEltType == DllMainWrapper.ELTTYPE_SPVEC_B1P || orbEltType == DllMainWrapper.ELTTYPE_VCM);
      }


      // Determine if the orbital element's satKey is external ephemeris
      public static bool SatStateIsExtEphem(long satKey)
      {
         int orbEltType = GetOrbEltType(satKey);

         return (orbEltType == DllMainWrapper.ELTTYPE_EXTEPH);
      }


      // Return the double value of the specified field of a satellite
      public static double GetSatDataDoubleField(long satKey, int idx)
      {
         return Utility.GetKey64Double(SatStateWrapper.SatStateGetSatDataField, satKey, idx);
      }


      // Print satellite detail information
      public static void PrintSatelliteData(StreamWriter sw, long satKey)
      {
         string dashLine = new string('-', 79);
         string ddashLine = new string('=', 79);

         int satNum;
         byte[] satName = new byte[8];
         byte[] recName = new byte[128];
         int elsetNum;
         int ephType;
         int revNum;
         double epochDs50UTC;
         double mnAnomaly;
         double node;
         double omega;
         double period;
         double eccen;
         double incli;
         double mnMotion;
         double bField;
         double perigeeHt;
         double apogeeHt;
         double apogee;
         double perigee;
         double A;


         int errCode;

         errCode = SatStateWrapper.SatStateGetSatDataAll(satKey, out satNum, satName, out ephType, out revNum, out  epochDs50UTC, out  bField,
                                         out elsetNum, out  incli, out  node, out eccen, out  omega, out  mnAnomaly, out  mnMotion,
                                         out  period, out  perigeeHt, out  apogeeHt, out perigee, out apogee, out A);

         if (errCode != 0)
            return;

         sw.WriteLine(" " + ddashLine);
         sw.WriteLine(" |                           SATELLITE {0,5} DATA                              |", satNum);
         sw.WriteLine(" " + dashLine);

         sw.WriteLine(" |  ELEMENT NUMBER :        {0, -50} |", elsetNum);
         sw.WriteLine(" |  INT. DESIGNATOR:        {0, -50} |", Utility.BytesToString(satName));
         sw.WriteLine(" |  EPHEMERIS TYPE:         {0, -50} |", SatStateWrapper.EltTypeStr[ephType - 1]);
         sw.WriteLine(" |  EPOCH REVOLUION #:      {0, -50} |", revNum);

         sw.WriteLine(" |  EPOCH TIME:             {0, -50} |", TimeFuncWrapper.UTCToDtg20Str(epochDs50UTC));

         sw.WriteLine(" |  MEAN ANOMALY (deg):     {0,9:F4}{1, -41} |", mnAnomaly, " ");
         sw.WriteLine(" |  RIGHT ASCENSION (deg):  {0,9:F4}{1, -41} |", node, " ");
         sw.WriteLine(" |  ARG. OF PERIGEE (deg):  {0,9:F4}{1, -41} |", omega, " ");
         sw.WriteLine(" |  PERIOD (min):           {0,9:F4}{1, -41} |", period, " ");
         sw.WriteLine(" |  ECCENTRICITY:           {0,9:F7}{1, -41} |", eccen, " ");
         sw.WriteLine(" |  INCLINATION (deg):      {0,9:F4}{1, -41} |", incli, " ");
         sw.WriteLine(" |  MEAN MOTION (rev/day):  {0,13:F9}{1, -37} |", mnMotion, " ");
         sw.WriteLine(" |  BSTAR/BTERM             {0,12:F9}{1, -38} |", bField, " ");
         sw.WriteLine(" |  PERIGEE HEIGHT (km):    {0,10:F3}{1, -40} |", perigeeHt, " ");
         sw.WriteLine(" |  APOGEE HEIGHT(km):      {0,10:F3}{1, -40} |", apogeeHt, " ");
         sw.WriteLine(" " + ddashLine);
         if (SatStateIsExtEphem(satKey))
         {
            double AE, Ke;
            int coordSys;
            int numOfPts;
            double[] pos = new double[3];
            double[] vel = new double[3];
            byte[] fileLoc = new byte[DllMainWrapper.FILEPATHLEN];

            errCode = ExtEphemWrapper.ExtEphGetAllFields(satKey, out satNum, satName, recName, out epochDs50UTC, out AE, out Ke, pos, vel,
                                                         out coordSys, out numOfPts, fileLoc);
            if (errCode != 0)
            {
               ErrorMessages.SaveErrorMessage();
               return;
            }


            double startTimeDs50UTC, endTimeDs50UTC;

            errCode = ExtEphemWrapper.ExtEphStartEndTime(satKey, out startTimeDs50UTC, out endTimeDs50UTC);
            if (errCode != 0)
            {
               ErrorMessages.SaveErrorMessage();
               return;
            }
            sw.WriteLine();
            sw.WriteLine("    Additional Data For External Ephemeris File");
            sw.WriteLine("    Input Coordinate System: " + ((ExtEphemWrapper.InputCoordSystems)coordSys).ToDescription());
            sw.WriteLine("    Number of ephemeris points: " + numOfPts);
            sw.WriteLine("    Ephemeris time span: " + TimeFuncWrapper.UTCToDtg20Str(startTimeDs50UTC) + " to " + TimeFuncWrapper.UTCToDtg20Str(endTimeDs50UTC));
            sw.WriteLine("    File Path: " + Utility.BytesToString(fileLoc));
            sw.WriteLine();
         }
      }


      // Build a satellite information string (SatID/SatEpoch/SatType) via its input satKey
      public static string ToSatDetail(long satKey)
      {
         StringBuilder satDetailStr = new StringBuilder(34);

         string satName = Utility.GetKey64String(SatStateWrapper.SatStateGetSatDataField, satKey, SatStateWrapper.XF_SAT_NAME);

         string satKeyStr = string.Format("{0, 19}", satKey);

         satDetailStr.AppendFormat("  {0:D5}    {1}/{2}.{3}    {4, 8}",
            Convert.ToInt32(satKeyStr.Substring(11, 7)),
            satKeyStr.Substring(9, 2), //YY 
            satKeyStr.Substring(1, 3), //DDD
            satKeyStr.Substring(3, 6), //dddddd
            satName);

         return satDetailStr.ToString();
      }


      //// Build a satellite epoch date time string via its input satKey
      // JJJddddddYYSSSSSSSE
      public static string ToSatEpochDecDay(long satKey)
      {
         StringBuilder satDetailStr = new StringBuilder(12);
         string satKeyStr = string.Format("{0, 19}", satKey).Replace(" ", "0");

         satDetailStr.AppendFormat(" {0}/{1:D3}.{2}",
            satKeyStr.Substring(9, 2), //YY 
            satKeyStr.Substring(0, 3).Replace(" ", "0"), //DDD
            satKeyStr.Substring(3, 6).Replace(" ", "0")); //dddddd

         return satDetailStr.ToString();
      }


      // Retrieve satellite name from the input satKey
      public static string GetSatName(long satKey)
      {
         string satName = Utility.GetKey64String(SatStateWrapper.SatStateGetSatDataField, satKey, SatStateWrapper.XF_SAT_NAME);

         return satName;
         /*
         byte[] satName = new byte[8];

         int errCode;

         errCode = SatStateWrapper.SatStateGetSatDataField(satKey, SatStateWrapper.XF_SAT_NAME, satName);

         if (errCode != 0)
            return null;
         else
            return Utility.BytesToString(satName).Trim();
          * */
      }


      // Return full string description of the orbital element type
      public static string GetSatTypeString(long satKey)
      {
         return SatStateWrapper.EltTypeStr[GetOrbEltType(satKey)];
      }


      // Return short string description of the orbital element type
      public static string GetRecTypeString(long satKey)
      {
         return SatStateWrapper.RecTypeStr[GetOrbEltType(satKey) - 1];
      }


      // Return record name of the satellite
      public static string GetRecName(long satKey)
      {
         string recName;

         // External ephemeris stores record name in the dll
         if (SatStateIsExtEphem(satKey))
         {
            
            byte[] valueStr = new byte[DllMainWrapper.GETSETSTRLEN];
            recName = Utility.GetKey64String(ExtEphemWrapper.ExtEphGetField, satKey, ExtEphemWrapper.XF_EXTEPH_RECNAME);

            if (recName == "")
            {
               ExtEphemWrapper.ExtEphGetField(satKey, ExtEphemWrapper.XF_EXTEPH_FILEPATH, valueStr);
               string filePath = Utility.BytesToStringLRTrim(valueStr);
               recName = Path.GetFileName(filePath);
            }
         }
         // ... other orbital elements don't have record name; the record name will be generated automatically
         else
         {
            double epochDs50UTC = GetSatDataDoubleField(satKey, SatStateWrapper.XF_SAT_EPOCH);
            DateTime epochDateTime = TimeFuncWrapper.Ds50UTCToDateTime(epochDs50UTC);
            string recNameEpoch = epochDateTime.Month.ToString("00") + "/" + epochDateTime.Day.ToString("00") + " " + epochDateTime.Hour.ToString("00") + ":" + epochDateTime.Minute.ToString("00");
            recName = GetRecTypeString(satKey) + recNameEpoch;
         }

         return recName;
      }


      /// <summary>
      /// Retrieve new TLE data (in arrays format) using the input time (newEpochDs50UTC), position (pos), velocity (vel) and existing TLE data
      /// </summary>
      /// <param name="satKey"></param>
      /// <param name="newEpochDs50UTC"></param>
      /// <param name="pos"></param>
      /// <param name="vel"></param>
      /// <param name="errCode"></param>
      /// <param name="xa_tle"></param>
      /// <param name="buf"></param>
      public static void NewTleFrPosVel(long satKey, double newEpochDs50UTC, double[] pos, double[] vel, out int errCode, out double[] xa_tle, out string strBuf)
      {
         xa_tle = new double[64];
         byte[] buf = new byte[512];
         TleWrapper.TleDataToArray(satKey, xa_tle, buf);

         // update epoch time to match maneuver time
         xa_tle[TleWrapper.XA_TLE_EPOCH] = newEpochDs50UTC;

         errCode = Sgp4PropWrapper.Sgp4PosVelToTleArr(pos, vel, xa_tle);
         strBuf = Utility.BytesToString(buf);
      }
   }
}
