using System;
using System.Collections.Generic;
using AFSPC.AstroStds.Wrappers;
using System.IO;

namespace AFSPC.AstroStds.Models
{
   public static class SunMoonEphemGen
   {
      public static void GenEphem()
      {
         TimeFuncWrapper.TConLoadFile(@"I:\data\TimeConstants\full_time_constants.dat");
         double startTime = TimeFuncWrapper.DTGToUTC("2012262000000.000");
         double stopTime = TimeFuncWrapper.DTGToUTC("2012266000000.000");
         List<string> moonLines = new List<string>();
         List<string> sunLines = new List<string>();

         double[] moonPosJ2K = new double[3];
         double[] sunPosJ2K = new double[3];
         for (double ds50UTC = startTime; ds50UTC <= stopTime; ds50UTC += (1/24.0)) //(10 / 1440.0))
         {
            double[] moonPos = new double[3];
            double[] moonUnitVec = new double[3];
            double[] sunPos = new double[3];
            double[] sunUnitVec = new double[3];

            double moonDist, sunDist;

            double ds50ET = TimeFuncWrapper.UTCToET(ds50UTC);
            //AstroFuncWrapper.CompMoonPos(TimeFuncWrapper.UTCToET(ds50UTC), moonUnitVec, out moonDist);
            AstroFuncWrapper.CompMoonPos(TimeFuncWrapper.UTCToET(ds50UTC), moonUnitVec, out moonDist);
            AstroFuncWrapper.CompSunMoonPos(ds50ET, sunUnitVec, out sunDist, moonUnitVec, out moonDist);
            moonPos[0] = moonUnitVec[0] * moonDist;
            moonPos[1] = moonUnitVec[1] * moonDist;
            moonPos[2] = moonUnitVec[2] * moonDist;
            sunPos[0] = sunUnitVec[0] * sunDist;
            sunPos[1] = sunUnitVec[1] * sunDist;
            sunPos[2] = sunUnitVec[2] * sunDist;

            //AstroFuncWrapper.RotDateToJ2K(1, 4, TimeFuncWrapper.UTCToTAI(ds50UTC), moonPos, moonPos, moonPosJ2K, moonPosJ2K);
            moonLines.Add(String.Format("{0} {1:E8} {2:E8} {3:E8} {4:E8} {5:E8} {6:E8}", TimeFuncWrapper.UTCToDtg15Str(ds50UTC), moonPos[0], moonPos[1], moonPos[2], moonUnitVec[0], moonUnitVec[1], moonUnitVec[2]));
            //moonLines.Add(String.Format("{0} {1:E8} {2:E8} {3:E8}          J2K", TimeFuncWrapper.UTCToDtg19Str(ds50UTC), moonPosJ2K[0], moonPosJ2K[1], moonPosJ2K[2]));

            //AstroFuncWrapper.RotDateToJ2K(1, 4, TimeFuncWrapper.UTCToTAI(ds50UTC), sunPos, sunPos, sunPosJ2K, sunPosJ2K);
            sunLines.Add(String.Format("{0} {1:E8} {2:E8} {3:E8} {4:E8} {5:E8} {6:E8}", TimeFuncWrapper.UTCToDtg15Str(ds50UTC), sunPos[0], sunPos[1], sunPos[2], sunPos[0], sunPos[1], sunPos[2]));
            //sunLines.Add(String.Format("{0} {1:E8} {2:E8} {3:E8}          J2K", TimeFuncWrapper.UTCToDtg19Str(ds50UTC), sunPosJ2K[0], sunPosJ2K[1], sunPosJ2K[2]));

         }

         File.WriteAllLines(@"C:\temp\moon_ephem.txt", moonLines.ToArray());
         File.WriteAllLines(@"C:\temp\sun_ephem.txt", sunLines.ToArray());
      }

   }
}
