// ***********************************************************************
// Assembly         : AFSPC.AstroStds.Models
// Author           : nguyend
// Created          : 05-19-2015
//
// Last Modified By : nguyend
// Last Modified On : 12-05-2014
// ***********************************************************************
// <copyright file="ComboService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AFSPC.AstroStds.Wrappers;
using System.IO;
using AFSPC.AstroStds.Utilities;
using AFSPC.AstroStds.Models.SatelliteModel;

/// <summary>
/// The ComboModel namespace.
/// </summary>
namespace AFSPC.AstroStds.Models.ComboModel
{
   /// <summary>
   /// Class ComboService.
   /// </summary>
   public static class ComboService
   {

      // Print COMBO control paramters
      /// <summary>
      /// Prints the combo CTRLS.
      /// </summary>
      /// <param name="sw">The sw.</param>
      public static void PrintComboCtrls(StreamWriter sw)
      {
         string[] outCoordSys = new string[] {"true equator, mean equinox of date ", 
                                           "true equator, mean equinox of Epoch", 
                                           "mean equator, mean equinox of FK5  "};

         // control for computing relative minima
         string[] compRelMin = new string[] { "no", "yes" }; // (analytic not available-- using brute force",   //EAJ changed from Analytic to "yes"                        

         // print options
         string[] prtOptStr = new string[] {"relative geometry only                    ",  
                                          "rel. geom & position/velocity             ",  
                                          "rel. geom, pos/vel, & lat/long/height     ",  
                                          "rel. geom, pos/vel, lat/long/ht, rel. pos.",  
                                          "none                                      "};

         string dashLine = "-------------------------------------------------------------------------------";
         string ddashLine = "===============================================================================";


         sw.WriteLine(" {0}", ddashLine);
         sw.WriteLine(" |                          C O M B O   O P T I O N S                          |");
         sw.WriteLine(" {0}", dashLine);


         if (Combo7P.TimeFlg == ComboWrapper.CARD_7P_FRDATE)
         {
            sw.WriteLine(" |  A.  TIME OF INTEREST START, UTC:   {0, -40}|", TimeFuncWrapper.UTCToDtg20Str(Combo7P.StartTime));
            sw.WriteLine(" |  B.  TIME OF INTEREST END  , UTC:   {0, -40}|", TimeFuncWrapper.UTCToDtg20Str(Combo7P.StopTime));
         }
         else
         {
            sw.WriteLine(" |  A.  TIME OF INTEREST START, MSE:   {0, 10:F3}{1, 30}|", Combo7P.StartTime, " ");
            sw.WriteLine(" |  B.  TIME OF INTEREST END  , MSE:   {0, 10:F3}{1, 30}|", Combo7P.StopTime, " ");
         }
         sw.WriteLine(" |  C.  COMPUTE RELATIVE MINIMA?       {0, -40}|", compRelMin[Combo7P.RelMinFlg]);
         sw.WriteLine(" |  D.  COMPUTE ABSOLUTE MINIMUM?      {0, -40}|", Combo7P.AbsMinFlg == 1 ? "yes" : "no");
         sw.WriteLine(" |  E.  FIND PERIODS CLOSE PROX.?      {0, -40}|", Combo7P.CloseProxFlg == 1 ? "yes" : "no");
         sw.WriteLine(" |  F.  OUTPUT REL. EPHEMERIS PTs?     {0, -40}|", Combo7P.RelEphFlg == 1 ? "yes" : "no");
         sw.WriteLine(" |  G.  MAX SEPARATION DISTANCE:       {0, 10:F3} km{1, 27}|", Combo7P.RelMinLim, " ");
         sw.WriteLine(" |  H.  CLOSE PROXIMITY LIMIT:         {0, 10:F3} km{1, 27}|", Combo7P.CloseProxLim, " ");
         sw.WriteLine(" |  I.  REL EPHEM OUTPUT INTERVAL:     {0, 9:F2} min{1, 27}|", Combo7P.RelEphInterval, " ");
         //EAJ: sw.WriteLine(" |  J.  DIHEDRAL ANGLE LIMIT:          {0, 9:F2} deg{1, 27}|", Combo7P.AngleLim, " ");
         sw.WriteLine(" |  K.  PRINTED OUTPUT OPTION:      {0, -43}|", prtOptStr[Combo7P.PrtOpt]);
         if (Combo7P.PocSigma == 0)
            sw.WriteLine(" |  L.  PROBABILITY OF COLLISION:      {0, -40}|", "not requested");
         else
         {
            sw.WriteLine(" |  L.  PROBABILITY OF COLLISION:   {0, 6:F3} sigma {1, -30}|", Combo7P.PocSigma, " ");
            sw.WriteLine(" |      PRIMARY EFFECTIVE RADIUS:   {0, 6:F1} meters{1, -30}|", Combo7P.PriRadius_Meter, " ");
            sw.WriteLine(" |      SECDARY EFFECTIVE RADIUS:   {0, 6:F1} meters{1, -30}|", Combo7P.SecRadius_Meter, " ");
         }

         sw.WriteLine(" {0}", ddashLine);
      }



      //// Get combo ouput data from the dll and store it into ComboOutput structure
      //public static void GetComboOutput(long priSatKey, long secSatKey, ComboOutput* comboOut)
      //{
      //   // Reset values of comboOutput structure's
      //   memset(comboOut, 0, sizeof(ComboOutput));

      //   // Populate comboOutput
      //   comboOut.PriSatKey = priSatKey;
      //   comboOut.SecSatKey = secSatKey;

      //   // Calling the Combo dll's function to return combo output data
      //   comboOut.ErrCode = ComboCompPriSec(priSatKey, secSatKey,
      //      &comboOut.AbsMinDs50UTC,
      //      &comboOut.AbsMinDist,
      //      comboOut.RelMinDs50UTCs,
      //      &comboOut.NumOfRelMins,
      //      comboOut.EntryDs50UTCs,
      //      comboOut.ExitDs50UTCs,
      //      &comboOut.NumOfEntryExits);
      //}


      // Print primatry/secondary closest approach
      /// <summary>
      /// Prints the closest approach.
      /// </summary>
      /// <param name="sw">The sw.</param>
      /// <param name="comboOut">The combo out.</param>
      public static void PrintClosestApproach(StreamWriter sw, ComboOutput comboOut)
      {
         int priSatNum, secSatNum;

         priSatNum = ElOpsWrapper.SatNumOf(comboOut.PriSatKey);
         secSatNum = ElOpsWrapper.SatNumOf(comboOut.SecSatKey);

         // Primary/secondary passes different filters/tests
         if (comboOut.AbsMinDs50UTC == 0 || comboOut.AbsMinDist > 1.0E10)
         {
            sw.WriteLine(" ... SECONDARY SATELLITE {0:D5} NO CLOSE APPROACHES FOUND", secSatNum);
            return;
         }
         else if (comboOut.AbsMinDist < Combo7P.CloseProxLim)
         {
            PrintComboHeader(sw, priSatNum);
            if (Combo7P.AbsMinFlg == 1 && Combo7P.RelMinFlg == 0)
            {
               sw.WriteLine("  {0:D5}  {1,23}{2,11:F3}   ****  ABSL MIN ; PRIM. SAT {3:D5}  ****",
                 secSatNum, TimeFuncWrapper.UTCToDtg23Str(comboOut.AbsMinDs50UTC), comboOut.AbsMinDist, priSatNum);
               sw.WriteLine("____________________________________________________________________________________");
            }
            else
            {
               PrintRelativeMinima(sw, comboOut);
            }

         }
      }


      // Print COMBO header
      /// <summary>
      /// Prints the combo header.
      /// </summary>
      /// <param name="sw">The sw.</param>
      /// <param name="priSatNum">The pri sat number.</param>
      public static void PrintComboHeader(StreamWriter sw, int priSatNum)
      {
         sw.WriteLine(" {0:D5}            TIME             DISTANCE  REL VEL  DELTA-T  DELTA-BETA  DELTA-HT", priSatNum);
         sw.WriteLine("  vs     YY DDD HH MM SS.SSSSSSS       km     km/sec   minutes   degrees       km   ");
         sw.WriteLine("____________________________________________________________________________________");
         sw.WriteLine();
      }



      // Print relative minima if requested
      /// <summary>
      /// Prints the relative minima.
      /// </summary>
      /// <param name="sw">The sw.</param>
      /// <param name="comboOut">The combo out.</param>
      public static void PrintRelativeMinima(StreamWriter sw, ComboOutput comboOut)
      {
         //double[] deltas = new double[100];
         EphemDeltas deltas = new EphemDeltas();
         double[] priPos = new double[3];
         double[] priVel = new double[3];
         double[] priLLH = new double[3];
         double[] secPos = new double[3];
         double[] secVel = new double[3];
         double[] secLLH = new double[3];
         double mse;
         int revNum;
         double relMinDs50UTC;
         double ds50UTC;
         double startDs50UTC, stopDs50UTC;
         char atBoundary;

         int k;
         int errCode;

         int priSatNum, secSatNum;

         priSatNum = ElOpsWrapper.SatNumOf(comboOut.PriSatKey);
         secSatNum = ElOpsWrapper.SatNumOf(comboOut.SecSatKey);

         ComputeStartStopDs50UTC(comboOut.PriSatKey, out startDs50UTC, out stopDs50UTC);

         int count = 0; // number of entry/exit times

         // Loop through the array of times of relative minima
         for (k = 0; k < comboOut.NumOfRelMins; k++)
         {
            relMinDs50UTC = comboOut.RelMinDs50UTCs[k];

            // Compute deltas of primary/secondary at the current relative minima
            deltas = new EphemDeltas(comboOut.PriSatKey, comboOut.SecSatKey, relMinDs50UTC, true);
            if (deltas.ErrCode != 0)
            {
               ErrorMessages.SaveErrorMessage();
               return;
            }

            // Check to see if computing relative minima flag is on
            if (Combo7P.RelMinFlg != 0)
            {
               if (relMinDs50UTC == startDs50UTC || relMinDs50UTC == stopDs50UTC)
                  atBoundary = 'B';
               else
                  atBoundary = ' ';

               PrintPriSecDeltas(sw, secSatNum, relMinDs50UTC, deltas, atBoundary);

               // Add string to indicate the current relative minimum is the absolute minimum
               if (relMinDs50UTC == comboOut.AbsMinDs50UTC)
                  sw.WriteLine(" ABSL MIN");
               else
                  sw.WriteLine("");
            }

            if (Combo7P.PrtOpt > 0 && Combo7P.PrtOpt < 4)
            {
               // Compute and print primary/secondary position, velocity
               errCode = SatStateWrapper.SatStateDs50UTC(comboOut.PriSatKey, relMinDs50UTC, out mse, out revNum, priPos, priVel, priLLH);
               errCode = SatStateWrapper.SatStateDs50UTC(comboOut.SecSatKey, relMinDs50UTC, out mse, out revNum, secPos, secVel, secLLH);

               PrintPriSecPosVel(sw, priPos, priVel, secPos, secVel);

               // Print primary/secondary lat, long, height and relative position
               if (Combo7P.PrtOpt == 2 || Combo7P.PrtOpt == 3)
               {

                  PrintPriSecLLH(sw, priLLH, secLLH);

                  // Print relative position
                  if (Combo7P.PrtOpt == 3)
                  {
                     PrintPriSecRelPosVel(sw, deltas);
                  }
               }
            }

            // Check to see if control for computing periods of close proximity flag is on
            if (Combo7P.CloseProxFlg == 1)
            {
               // Note: the entry/exit times could be zeros which indicates that the entry/exit times are not available

               // Find the correct entry/exit times for this relative minimum
               //if (((comboOut.EntryDs50UTCs[count] != 0 && relMinDs50UTC < comboOut.EntryDs50UTCs[count]) || 
               if (comboOut.ExitDs50UTCs[count] != 0 && relMinDs50UTC > comboOut.ExitDs50UTCs[count] &&
                  count < comboOut.NumOfEntryExits)
                  count++;

               // If the secondary satellite is in sustained relative geometry, there could be 
               // multiple relative minima within that same entry/exit times
               if (comboOut.EntryDs50UTCs[count] <= relMinDs50UTC && relMinDs50UTC <= comboOut.ExitDs50UTCs[count] ||
                   comboOut.EntryDs50UTCs[count] == 0 || comboOut.ExitDs50UTCs[count] == 0)
               {

                  // Compute deltas at entry time
                  ds50UTC = comboOut.EntryDs50UTCs[count];
                  if (ds50UTC != 0)
                  {
                     deltas = new EphemDeltas(comboOut.PriSatKey, comboOut.SecSatKey, ds50UTC, true);
                     if (deltas.ErrCode != 0)
                     {
                        ErrorMessages.SaveErrorMessage();
                        return;
                     }


                     sw.WriteLine("{0, 9}{1}  {2,9:F3}     ****  ENTRY TIME  ****", " ",
                        TimeFuncWrapper.UTCToDtg19aStr(comboOut.EntryDs50UTCs[count]), deltas.Pos_km);
                  }

                  // Compute deltas at exit time
                  ds50UTC = comboOut.ExitDs50UTCs[count];
                  if (ds50UTC != 0)
                  {
                     deltas = new EphemDeltas(comboOut.PriSatKey, comboOut.SecSatKey, ds50UTC, true);
                     if (deltas.ErrCode != 0)
                     {
                        ErrorMessages.SaveErrorMessage();
                        return;
                     }

                     sw.WriteLine("{0, 9}{1}  {2,9:F3}     ****  EXIT  TIME  ****", " ",
                        TimeFuncWrapper.UTCToDtg19aStr(comboOut.ExitDs50UTCs[count]), deltas.Pos_km);
                  }
               }
            }

            // if requesting Probability of Collision, be sure input is compatible
            // Check to see if both primary/secondary satellites are VCMs
            if (Combo7P.PocSigma != 0 && SatStateWrapper.SatStateHasCovMtx(comboOut.PriSatKey) == 1 && SatStateWrapper.SatStateHasCovMtx(comboOut.SecSatKey) == 1)
               CalcAndPrintPOC(sw, relMinDs50UTC, comboOut);

         }
         sw.WriteLine("____________________________________________________________________________________");
         sw.WriteLine("");

      }


      /// <summary>
      /// Prints the relative ephem PTS.
      /// </summary>
      /// <param name="sw">The sw.</param>
      /// <param name="priSatKey">The pri sat key.</param>
      /// <param name="secSatKey">The sec sat key.</param>
      public static void PrintRelativeEphemPts(StreamWriter sw, long priSatKey, long secSatKey)
      {
         double startDs50UTC, stopDs50UTC;
         double currDs50UTC;

         double[] priPos = new double[3];
         double[] priVel = new double[3];
         double[] priLLH = new double[3];
         double[] secPos = new double[3];
         double[] secVel = new double[3];
         double[] secLLH = new double[3];
         double mse;
         int revNum;


         // No output
         if (Combo7P.PrtOpt == 4)
            return;

         ComputeStartStopDs50UTC(priSatKey, out startDs50UTC, out stopDs50UTC);

         currDs50UTC = startDs50UTC;

         while (true)
         {
            // Compute deltas of primary/secondary at the current relative minima
            EphemDeltas deltas = new EphemDeltas(priSatKey, secSatKey, currDs50UTC, true);
            if (deltas.ErrCode != 0)
            {
               ErrorMessages.SaveErrorMessage();
               return;
            }

            PrintPriSecDeltas(sw, ElOpsWrapper.SatNumOf(secSatKey), currDs50UTC, deltas, ' ');
            sw.WriteLine("");

            // print only relative geometry
            if (Combo7P.PrtOpt != ComboWrapper.PRT_RELGEOONLY)
            {

               SatStateWrapper.SatStateDs50UTC(priSatKey, currDs50UTC, out mse, out revNum, priPos, priVel, priLLH);
               SatStateWrapper.SatStateDs50UTC(secSatKey, currDs50UTC, out mse, out revNum, secPos, secVel, secLLH);

               if (Combo7P.PrtOpt > ComboWrapper.PRT_RELGEOONLY)
                  PrintPriSecPosVel(sw, priPos, priVel, secPos, secVel);

               if (Combo7P.PrtOpt > ComboWrapper.PRT_POSVEL)
                  PrintPriSecLLH(sw, priLLH, secLLH);

               if (Combo7P.PrtOpt == ComboWrapper.PRT_RELPOS)
                  PrintPriSecRelPosVel(sw, deltas);
            }

            if (currDs50UTC < stopDs50UTC)
            {
               currDs50UTC += (Combo7P.RelEphInterval / 1440.0);
               if (currDs50UTC > stopDs50UTC)
                  currDs50UTC = stopDs50UTC;
            }
            else
               break;
         }
      }


      // Print primary/secondary deltas
      /// <summary>
      /// Prints the pri sec deltas.
      /// </summary>
      /// <param name="sw">The sw.</param>
      /// <param name="secSatNum">The sec sat number.</param>
      /// <param name="currDs50UTC">The curr DS50 UTC.</param>
      /// <param name="deltas">The deltas.</param>
      /// <param name="atBoundary">At boundary.</param>
      public static void PrintPriSecDeltas(StreamWriter sw, int secSatNum, double currDs50UTC, EphemDeltas deltas, char atBoundary)
      {
         sw.Write(" {0}{1:D5}  {2,23}{3,11:F3}{4,8:F3}{5,10:F4}{6,10:F4}{7,13:F3}",
                 atBoundary, secSatNum, TimeFuncWrapper.UTCToDtg23Str(currDs50UTC), deltas.Pos_km, deltas.Vel_kmps,
                 deltas.Time_sec / 60.0, deltas.Beta_deg, deltas.Height_km);
      }

      // Print primary/secondary position and velocity
      /// <summary>
      /// Prints the pri sec position vel.
      /// </summary>
      /// <param name="sw">The sw.</param>
      /// <param name="priPos">The pri position.</param>
      /// <param name="priVel">The pri vel.</param>
      /// <param name="secPos">The sec position.</param>
      /// <param name="secVel">The sec vel.</param>
      public static void PrintPriSecPosVel(StreamWriter sw, double[] priPos, double[] priVel, double[] secPos, double[] secVel)
      {
         sw.WriteLine("{0, 6}PRI.POS.(km)     X ={1, 14:F7}  Y ={2, 14:F7}  Z ={3, 14:F7}", " ", priPos[0], priPos[1], priPos[2]);
         sw.WriteLine("{0, 6}SEC.POS.(km)     X ={1, 14:F7}  Y ={2, 14:F7}  Z ={3, 14:F7}", " ", secPos[0], secPos[1], secPos[2]);
         sw.WriteLine("{0, 6}PRI.VEL.(km/sec) XD={1, 14:F9}  YD={2, 14:F9}  ZD={3, 14:F9}", " ", priVel[0], priVel[1], priVel[2]);
         sw.WriteLine("{0, 6}SEC.VEL.(km/sec) XD={1, 14:F9}  YD={2, 14:F9}  ZD={3, 14:F9}", " ", secVel[0], secVel[1], secVel[2]);
      }


      // Print primary/secondary lat, long, height
      /// <summary>
      /// Prints the pri sec LLH.
      /// </summary>
      /// <param name="sw">The sw.</param>
      /// <param name="priLLH">The pri LLH.</param>
      /// <param name="secLLH">The sec LLH.</param>
      public static void PrintPriSecLLH(StreamWriter sw, double[] priLLH, double[] secLLH)
      {
         // Print lat, long, height
         sw.WriteLine("{0, 6}PRI.GRND.TR.     NLT={1, 13:F9}  ELG={2, 13:F9}  HT={3, 14:F7}", " ", priLLH[0], priLLH[1], priLLH[2]);
         sw.WriteLine("{0, 6}SEC.GRND.TR.     NLT={1, 13:F9}  ELG={2, 13:F9}  HT={3, 14:F7}", " ", secLLH[0], secLLH[1], secLLH[2]);

      }

      // Print primary/secondary relative position
      /// <summary>
      /// Prints the pri sec relative position vel.
      /// </summary>
      /// <param name="sw">The sw.</param>
      /// <param name="deltas">The deltas.</param>
      public static void PrintPriSecRelPosVel(StreamWriter sw, EphemDeltas deltas)
      {
         sw.WriteLine("{0, 6}REL.POS.(km)     DU={1, 14:F7}  DV={2, 14:F7}  DW={3, 14:F7}", " ",
                 deltas.PosRadial_km, deltas.PosIntrack_km, deltas.PosCrossTrack_km);
         sw.WriteLine("{0, 6}REL.VEL.(km/sec) DU={1, 14:F9}  DV={2, 14:F9}  DW={3, 14:F9}", " ",
                 deltas.VelRadial_kmps, deltas.VelIntrack_kmps, deltas.VelCrossTrack_kmps);
      }

      /// <summary>
      /// Computes the start stop DS50 UTC.
      /// </summary>
      /// <param name="priSatKey">The pri sat key.</param>
      /// <param name="startDs50UTC">The start DS50 UTC.</param>
      /// <param name="stopDs50UTC">The stop DS50 UTC.</param>
      public static void ComputeStartStopDs50UTC(long priSatKey, out double startDs50UTC, out double stopDs50UTC)
      {
         double epochDs50UTC;

         // Compute start/stop time in Ds50UTC
         if (Combo7P.TimeFlg == ComboWrapper.CARD_7P_FRDATE)
         {
            startDs50UTC = Combo7P.StartTime;
            stopDs50UTC = Combo7P.StopTime;
         }
         else
         {
            epochDs50UTC = SatStateService.GetSatDataDoubleField(priSatKey, SatStateWrapper.XF_SAT_EPOCH);
            startDs50UTC = Utility.MseToDs50UTC(epochDs50UTC, Combo7P.StartTime);
            stopDs50UTC = Utility.MseToDs50UTC(epochDs50UTC, Combo7P.StopTime);
         }
      }

      // Compute and print probability of collision
      public static void CalcAndPrintPOC(StreamWriter sw, double relMinDs50UTC, ComboOutput comboOut)
      {
         int revNum;          // Revolution number
         double mse;             // minutes since satellite's epoch
         double[] priPos = new double[3];       // Position vector (km) 
         double[] priVel = new double[3];       // Velocity vector (km/s) 
         double[] secPos = new double[3];       // Position vector (km) 
         double[] secVel = new double[3];       // Velocity vector (km/s) 

         double[,] priCovXYZ = new double[6, 6];
         double[,] secCovXYZ = new double[6, 6];

         double[,] priCovPosXYZ = new double[3, 3];
         double[,] secCovPosXYZ = new double[3, 3];

         double[] llh = new double[3];
         double[] pocArr = new double[4];
         double mult_sqr, prob_sqr, mult_cir, prob_cir;
         int errCode;



         // Propagate pri/sec satellites to the time of close approach
         SatStateWrapper.SatStateDs50UTC(comboOut.PriSatKey, relMinDs50UTC, out mse, out revNum, priPos, priVel, llh);
         SatStateWrapper.SatStateDs50UTC(comboOut.SecSatKey, relMinDs50UTC, out mse, out revNum, secPos, secVel, llh);

         // Retrieve XYZ covariance matrix
         SpPropWrapper.SpGetCovMtx(comboOut.PriSatKey, SpPropWrapper.XF_COVMTX_XYZ_DATE, priCovXYZ);
         SpPropWrapper.SpGetCovMtx(comboOut.SecSatKey, SpPropWrapper.XF_COVMTX_XYZ_DATE, secCovXYZ);


         // Extract the 3x3 matrix from the 6x6 covariance matrix
         CovMtx3x3(priCovXYZ, priCovPosXYZ);
         CovMtx3x3(secCovXYZ, secCovPosXYZ);


         // Compute probability of collision
         errCode = ComboWrapper.ComboPOC(priPos, priVel, priCovPosXYZ, Combo7P.PriRadius_Meter,
                            secPos, secVel, secCovPosXYZ, Combo7P.SecRadius_Meter,
                            ComboWrapper.XF_COVTYPE_ECI, pocArr);
         if (errCode != 0)
         {
            ErrorMessages.SaveErrorMessage();
            return;
         }

         mult_sqr = pocArr[0];
         prob_sqr = pocArr[1] * 100.0;
         mult_cir = pocArr[2];
         prob_cir = pocArr[3] * 100.0;

         sw.WriteLine("Probability of Collision:  probability   n-Sigma    P/F");
         sw.WriteLine("                Standard:{0, 13}{1, 10:F3}", " ", Combo7P.PocSigma);
         sw.WriteLine("                Circle:  {0, 13:F3}{1, 10:F3}    {2}", prob_cir, mult_cir, mult_cir <= Combo7P.PocSigma ? "PASS" : "FAIL");
         sw.WriteLine("                Square:  {0, 13:F3}{1, 10:F3}    {2}", prob_sqr, mult_sqr, mult_sqr <= Combo7P.PocSigma ? "PASS" : "FAIL");
         sw.WriteLine();
      }


      // Extract covariance matrix 3x3 from the covariance matrix 6x6
      public static void CovMtx3x3(double[,] covMtx_6x6, double[,] covMtx_3x3)
      {
         int row, col;

         for (row = 0; row < 3; row++)
            for (col = 0; col < 3; col++)
               covMtx_3x3[row, col] = covMtx_6x6[row, col];

      }

   }
}
