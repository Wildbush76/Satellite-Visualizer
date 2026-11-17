using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using AFSPC.AstroStds.Utilities;
using System.ComponentModel;

namespace AFSPC.AstroStds.Wrappers
{
   public static partial class SpPropWrapper
   {
      // Indexes represent coordinate systems
      public static int
         IDX_OUTCOORD_DATE = 0,
         IDX_OUTCOORD_EPOCH = 1,
         IDX_OUTCOORD_J2000 = 2;

      // Extract covariance matrix 3x3 from the covariance matrix 6x6
      public static void CovMtx3x3(double[,] covMtx_6x6, double[,] covMtx_3x3)
      {
         int row, col;

         for (row = 0; row < 3; row++)
            for (col = 0; col < 3; col++)
               covMtx_3x3[row, col] = covMtx_6x6[row, col];

      }

      // Indexes of Integration mode
      public static int
         INTEG_NONE = 0,
         INTEG_NUMERICAL = 1,
         INTEG_ANALYTIC = 2;

      public enum GeoIdxEnum
      {
         [Description("Geopotential File")]
         GeoIdx0 = 0,
         [Description("SP003 (STEM68R) 6th order")]
         GeoIdx3 = 3,
         [Description("SP003 (STEM68R) 8th order")]
         GeoIdx4 = 4,
         [Description("WGS72 J2-J5 (zonals) only")]
         GeoIdx5 = 5,
         [Description("WGS72 thru (6,6) only")]
         GeoIdx6 = 6,
         [Description("WGS72 thru (4,4) only")]
         GeoIdx7 = 7,
         [Description("WGS72 thru (8,8) only")]
         GeoIdx8 = 8,
         [Description("WGS84 9th order")]
         GeoIdx9 = 9,
         [Description("WGS84 J2-J5 (zonals) only")]
         GeoIdx10 = 10,
         [Description("NWL-8C 12 order")]
         GeoIdx11 = 11,
         [Description("WGS72 12th order")]
         GeoIdx12 = 12,
         [Description("WGS84 18th order")]
         GeoIdx18 = 18,
         [Description("WGS72 user-specified")]
         GeoIdx20 = 20,
         [Description("WGS84 user-specified")]
         GeoIdx21 = 21,
         [Description("GEM5 22nd order")]
         GeoIdx22 = 22,
         [Description("WGS72 24th order")]
         GeoIdx24 = 24,
         [Description("WGS72 12th order & (13,13)-(29,13)")]
         GeoIdx25 = 25,
         [Description("WGS72 12th order & (14,14), (15,14)")]
         GeoIdx26 = 26,
         [Description("WGS72 12th order & (13,13), (14,14), (15,14) GEM9 terms")]
         GeoIdx27 = 27,
         [Description("GEM9 30th order (thru 30, 30)")]
         GeoIdx30 = 30,
         [Description("JGM2 41st order (thru 41, 41)")]
         GeoIdx40 = 40,
         [Description("WGS84 41st order (thru 41, 41)")]
         GeoIdx41 = 41,
         [Description("WGS84 full 70th order (thru 70, 70)")]
         GeoIdx70 = 70,
         [Description("EGM96 70th order (thru 70, 70)")]
         GeoIdx71 = 71,
      };


      public enum DragPertEnum
      {
         [Description("No Drag")]
         DragPert0 = 0,
         [Description("Jachia 64")]
         DragPert1 = 1,
         [Description("Jachia 70")]
         DragPert2 = 2,
         [Description("DCA File")]
         DragPert3 = 3
      };


      public enum LunSolPertEnum
      {
         [Description("Apply None")]
         LunSolPert0 = 0,
         [Description("Analytic")]
         LunSolPert1 = 1,
         [Description("JPL Ephemerides File")]
         LunSolPert2 = 2
      };


      public enum InputCoordEnum
      {
         [Description("Epoch")]
         InpCoord0 = 0,
         [Description("J2000")]
         InpCoord1 = 1,
         [Description("Offset")]
         InpCoord2 = 2
      };

      public enum VarOfIntegCtrlEnum
      {
         [Description("Auto Selection")]
         VarOfIntegCtrl0 = 0,
         [Description("Time Integration")]
         VarOfIntegCtrl1 = 1,
         [Description("S Integration")]
         VarOfIntegCtrl2 = 2
      };

      public enum VarStepSizeCtrlEnum
      {
         [Description("Variable")]
         VarStepSizeCtrl0 = 0,
         [Description("Fixed")]
         VarStepSizeCtrl1 = 1,
         [Description("Semi-Fixed")]
         VarStepSizeCtrl2 = 2
      };

      public enum OutputCoordEnum
      {
         [Description("Date")]
         InpCoordDate = 0,
         [Description("Epoch")]
         InpCoordEpoch = 1,
         [Description("J2000")]
         InpCoordJ2000 = 2
      };


      public static void PrintSpAppCtrl(StreamWriter sw)
      {
         string[] outCoordSys = new string[] {"true equator, mean equinox of date ", 
                                              "true equator, mean equinox of Epoch", 
                                              "mean equator, mean equinox of FK5  "};

         byte[] geoDir = new byte[DllMainWrapper.FILEPATHLEN];
         int buffSize;
         int runMode;
         int savePartials;
         int isSpectr;
         double consider;
         int decayAlt;
         int outCoord;

         string dashLine = new string('-', 79);
         string ddashLine = new string('=', 79);



         SpGetApCtrlAll(geoDir, out buffSize, out runMode, out savePartials, out isSpectr, out consider, out decayAlt, out outCoord);

         sw.WriteLine(" " + ddashLine);
         sw.WriteLine(" |         S P   A P P L I C A T I O N   C O N T R O L   O P T I O N S         |");
         sw.WriteLine(" " + dashLine);
         sw.WriteLine(" | Geo Dir Path   : {0, -59}|", Utility.BytesToString(geoDir));
         sw.WriteLine(" | Buffer size    : {0, 6:D}{1, 53}|", buffSize, " ");
         sw.WriteLine(" | Run Mode       : {0, -59}|", runMode == 1 ? "Memory Priority" : "Performance Priority");
         sw.WriteLine(" | Specter Mode   : {0, -59}|", isSpectr == 1 ? "Compatible" : "Not Compatible");
         sw.WriteLine(" | Consider       : {0,8:F3}{1, 51}|", consider, " ");
         sw.WriteLine(" | Decay Altitude : {0,-6:D} km{1, -50}|", decayAlt, " ");
         sw.WriteLine(" | Output CoordSys: {0, -59}|", outCoordSys[outCoord]);
         sw.WriteLine(" | Save Partials  : {0, -59}|", savePartials == 1 ? "Yes" : "No");
         sw.WriteLine(" " + ddashLine);
         sw.WriteLine("");
      }

      // Convert geo index to geo name
      public static string GeoIdxToStr(int geoIdx)
      {
         switch (geoIdx)
         {
            case 1:
               return "user-specified ASCII geopotential file";
            case 3:
               return "SP003 (STEM68R) 6th order";
            case 4:
               return "SP004 (STEM68R) 8th order";
            case 5:
               return "WGS72 J2-J5 (zonals) ONLY";
            case 6:
               return "WGS72 thru (6,6) ONLY";
            case 7:
               return "WGS72 thru (4,4) ONLY";
            case 8:
               return "WGS72 thru (8,8) ONLY";
            case 9:
               return "WGS84 9th order (thru 9,9 only)";
            case 10:
               return "WGS84 J2-J5 (zonals) ONLY";
            case 11:
               return "NWL-8C 12th order (thru 12,12)";
            case 12:
               return "WGS72 12th order (thru 12,12)";
            case 18:
               return "WGS84 18th order (thru 18,18)";
            case 20:
               return "WGS72 with user-specified o/d/z";
            case 21:
               return "WGS84 with user-specified o/d/z";
            case 22:
               return "GEM5 22nd order (thru 22,16)";
            case 24:
               return "WGS72 24th order (thru 24,24)";
            case 25:
               return "WGS72 12th order & (13,13)-(29,13)";
            case 26:
               return "WGS72 12th order & (14,14), (15,14)";
            case 27:
               return "WGS72 12th order";
            case 30:
               return "GEM9 30th order (thru 30,30)";
            case 40:
               return "JGM2 41st order (thru 41,41)";
            case 41:
               return "WGS84 41st order (thru 41,41)";
            case 70:
               return "WGS84 full 70th order (thru 70,70)";
            case 71:
               return "EGM-96 70th order (thru 70,70)";
            default:
               return "WGS72 12th order";
         }
      }


      public static void PrintSpIntegCtrl(StreamWriter sw)
      {
         string dashLine = new string('-', 79);
         string ddashLine = new string('=', 79);
         string tempStr;
         string CGNM;

         string[] CINT = new string[] { "auto selection (based upon eccentricity)", "forced t (time)", "forced s-integration" };
         string[] CNAMA = new string[] { "      ", "Jach64", "Jach70", "DCA   " };
         string[] COM = new string[] { "SPADOC", "SPECTR" };
         string[] CVAR = new string[] { "variable", "fixed", "semi-fixed" };
         string[] noYes = new string[] { "no", "yes", "yes" };
         string[] lsPertModes = new string[] { "no", "yes", "jpl", "all", "big", "med", "sma" };

         int geoIdx, bulgePert, dragPert, radPresPert, lunSolPert, f10, f10Avg, ap, trunc, incr, ogPert, tidePert, inCoord, nTerms, reEval, integStepMode, stepSizeMethod;
         double initStepSize;
         byte[] dcaFile = new byte[DllMainWrapper.FILEPATHLEN];
         byte[] fluxFile = new byte[DllMainWrapper.FILEPATHLEN];
         byte[] geoFile = new byte[DllMainWrapper.FILEPATHLEN];
         byte[] jplFile = new byte[DllMainWrapper.FILEPATHLEN];
         byte[] jplStart = new byte[DllMainWrapper.FILEPATHLEN];
         byte[] jplStop = new byte[DllMainWrapper.FILEPATHLEN];

         byte[] geoDir = new byte[DllMainWrapper.FILEPATHLEN];
         int bufSize, runMode, savePartials, isSpectr, decayAlt, outCoord;
         double consider;


         SpGetApCtrlAll(geoDir, out bufSize, out runMode, out savePartials, out isSpectr, out consider, out decayAlt, out outCoord);

         SpGet4pAll(out geoIdx, out bulgePert, out dragPert, out radPresPert, out lunSolPert, out f10, out f10Avg, out ap, out trunc,
                    out incr, out ogPert, out tidePert, out inCoord, out nTerms, out reEval, out integStepMode, out stepSizeMethod,
                    out initStepSize, dcaFile, fluxFile, geoFile, jplFile, jplStart, jplStop);

         sw.WriteLine(" " + ddashLine);
         sw.WriteLine(" |         S P   I N T E G R A T I O N   C O N T R O L   O P T I O N S         |");
         sw.WriteLine(" " + dashLine);
         sw.WriteLine(" | COMPATIBILITY MODE: {0, -56}|", COM[isSpectr]);

         // geopotential name
         if (bulgePert == 0)
            CGNM = "2-body";
         else
            CGNM = GeoIdxToStr(geoIdx); // String.Format("{0, 2}", geoIdx);

         // print bulge perts selected
         sw.WriteLine(" |  A.  MODEL BULGE (GRAVITY) PERTS?  {0, -40} |", noYes[bulgePert]);
         sw.WriteLine(" |      > GEOPOTENTIAL MODEL:         {0, -40} |", CGNM);
         sw.WriteLine(" |      > TRUNCATED TO      :         {0, 3}Z, {1, 3}T{2,30} |", trunc, trunc, " ");
         if (dragPert > 0)
            sw.WriteLine(" |  B.  DRAG PERTURBATIONS MODELED?   {0,-3} ({1, -34}) |", "yes", CNAMA[dragPert]);
         else
            sw.WriteLine(" |  B.  DRAG PERTURBATIONS MODELED?   {0,-3}{1, -37} |", "no", " ");
         sw.WriteLine(" |  C.  LUNAR/SOLAR PERTS MODELED?    {0, -40} |", lsPertModes[lunSolPert]);
         sw.WriteLine(" |      > EARTH/OCEAN TIDES MODELED?  {0, -40} |", noYes[tidePert]);
         sw.WriteLine(" |  D.  RAD. PRESSURE PERTS MODELED?  {0, -40} |", noYes[radPresPert]);
         sw.WriteLine(" |  E.  OUTGASSING PERTS MODELED?     {0, -40} |", noYes[ogPert]);
         sw.WriteLine(" |  F.  CONSTANT FLUX USED?           {0, -40} |", f10 > 0 ? "yes" : "no");
         if (f10 > 0)
            sw.WriteLine(" |      F10, F10avg, Ap:              {0,6:F0}{1,6:F0}{2,7:F2}{3,21} |", f10, f10Avg, ap, " ");
         // if set corrector step convergence criterion to zero, internally that means using the default
         // value instead; default = 14 for SPECTR and default = 10 for non SPECTR
         if (incr == 0)
         {
            if (isSpectr == 1)
               incr = 14;
            else
               incr = 10;
         }

         sw.WriteLine(" |  H.  SP INTEGRATOR CONVERG. CRIT.  1.E-{0, -36} |", incr);
         sw.WriteLine(" |  I.  INTEGRATION VARIABLE:         {0, -40} |", CINT[integStepMode]);
         sw.WriteLine(" |  J.  STEP-SIZE CONTROL:            {0, -40} |", CVAR[stepSizeMethod]);
         if (initStepSize <= 0)
            sw.WriteLine(" |  L.  INITIAL STEP-SIZE (min):      {0, -40} |", "computed from a & e");
         else
            sw.WriteLine(" |  L.  INITIAL STEP-SIZE (min):      {0, 9:f4}{1, -31} |", initStepSize, " ");
         sw.WriteLine(" |  O.  OTHER VALUES SUB-MENU:        {0, -40} |", " ");
         sw.WriteLine(" |      c. CORRECTOR: RE-EVAL PERTS?  {0, -40} |", noYes[reEval]);
         if (nTerms == 0)
            if (isSpectr == 1)
               tempStr = "SPECTR 4-term";
            else
               tempStr = "SPADOC compatible";
         else
            tempStr = String.Format("{0,5}", nTerms);

         sw.WriteLine(" |      e. NUTATION ALGORITHM USED:   {0, -40} |", tempStr);
         sw.WriteLine(" " + dashLine);
         sw.WriteLine();
      }
   }
}