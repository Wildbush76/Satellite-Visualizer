using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using AFSPC.AstroStds.Wrappers;
using AFSPC.AstroStds.Utilities;

namespace AFSPC.AstroStds.Wrappers
{
   public static partial class Sgp4PropWrapper
   {
      public static int Xyz2Tle(int yr, double days, double[] posIn, double[] velIn, out string tle1, out string tle2)
      {
         bool useDrag = false;
         return Xyz2Tle(yr, days, 99999, posIn, velIn, useDrag, out tle1, out tle2);
      }

      public static int Xyz2Tle(int yr, double days, int satNum, double[] posIn, double[] velIn, out string tle1, out string tle2)
      {
         bool useDrag = false;
         return Xyz2Tle(yr, days, satNum, posIn, velIn, useDrag, out tle1, out tle2);
 
      }

      public static int Xyz2Tle(double ds50UTC, int satNum, double[] posIn, double[] velIn, bool useDrag, out string tle1, out string tle2)
      {
         int year;
         double dayOfYear;
         TimeFuncWrapper.UTCToYrDays(ds50UTC, out year, out dayOfYear);

         return Xyz2Tle(year, dayOfYear, satNum, posIn, velIn, useDrag, out tle1, out tle2);

      }


      public static int Xyz2Tle(int yr, double days, int satNum, double[] posIn, double[] velIn, bool useDrag, out string tle1, out string tle2)
      {
         double ndot;
         double bstar;
         byte[] line1 = new byte[512];
         byte[] line2 = new byte[512];

         double[] posOut = new double[3];              //Position (km)
         double[] velOut = new double[3];              //Velocity (km/s)
         double[] sgp4MeanKep = new double[6];

         double A, browerN, kozaiN, ecc, incl, node, MA, omega;
         int errCode;

         tle1 = tle2 = null;

         errCode = 0;

         // Convert osculating vectors pos, vel to sgp4MeanKep
         errCode = Sgp4PosVelToKep(yr, days, posIn, velIn, posOut, velOut, sgp4MeanKep);
         bool totalFailure = (posOut[0] == 0 && posOut[1] == 0 && posOut[2] == 0);

         if (errCode != 0 && totalFailure)
            return errCode;

         A = sgp4MeanKep[AstroFuncWrapper.XA_KEP_A];
         ecc = sgp4MeanKep[AstroFuncWrapper.XA_KEP_E];
         incl = sgp4MeanKep[AstroFuncWrapper.XA_KEP_INCLI];
         node = sgp4MeanKep[AstroFuncWrapper.XA_KEP_NODE];
         MA = sgp4MeanKep[AstroFuncWrapper.XA_KEP_MA];
         omega = sgp4MeanKep[AstroFuncWrapper.XA_KEP_OMEGA];
         browerN = AstroFuncWrapper.AToN(A);

         // Convert Brouwer mean motion to Kozai mean motion
         kozaiN = AstroFuncWrapper.BrouwerToKozai(ecc, incl, browerN);

         if (useDrag)
         {
            // compute initial drag
            AstroFuncWrapper.GetInitialDrag(sgp4MeanKep[AstroFuncWrapper.XA_KEP_A], ecc, out ndot, out bstar);
         }
         else
         {
            ndot = 0;
            bstar = 0;
         }

         // Create GP TLE - ephemeris type 0
         TleWrapper.TleGPFieldsToLines(satNum % 100000, ' ', "NewTLE", yr, days, ndot, 0.0, bstar, 0, 0, incl, node,
                                       ecc, omega, MA, kozaiN, 0, line1, line2);

         tle1 = Utility.BytesToStringLRTrim(line1);
         tle2 = Utility.BytesToStringLRTrim(line2);

         return errCode;
      }
   }
}