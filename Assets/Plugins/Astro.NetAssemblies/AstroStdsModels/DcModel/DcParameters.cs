using System;
using System.Text;
using AFSPC.AstroStds.Wrappers;
using System.IO;
using AFSPC.AstroStds.Utilities;

namespace AFSPC.AstroStds.Models.DcModel
{
   public static class DcParameters
   {
      public static BatchDCWrapper.DCPrintOption PrintOption { get; set; }
      public static bool IsApplySenBiases { get; set; }   // apply biases from sensor file
      public static bool IsWeightedDC { get; set; }       // weighted DC
      public static double RMSMultiplier { get; set; }     // RMS multiplier
      public static BatchDCWrapper.EpochPlacementCtrl EpochPlacement { get; set; }      // Epoch placement control
      public static bool Corr_AG { get; set; }
      public static bool Corr_AF { get; set; }
      public static bool Corr_PSI { get; set; }
      public static bool Corr_CHI { get; set; }
      public static bool Corr_L { get; set; }
      public static bool Corr_N { get; set; }
      public static bool Corr_B { get; set; }
      public static bool Corr_AGOM { get; set; }
      public static DaysSince50UTC SpecifiedEpoch { get; set; }
      public static BatchDCWrapper.CorrectionOrder CorrOrder { get; set; }
      public static bool CorrectOneIterOnly { get; set; } // Apply correction order for one iteration only
      public static BatchDCWrapper.ResidualsSelection ResidSelect { get; set; }
      public static bool ShowSensorPerformance { get; set; }
      public static bool DeweightByObsPerTrack { get; set; }
      public static BatchDCWrapper.IterationSummaryOption IterSummaryOption { get; set; }
      public static bool UseFastNumPartials { get; set; }
      public static int LTC { get; set; }
      public static double FirstPassDeltaTRej { get; set; }
      public static int NBruce { get; set; } // number of iteration to allow no auto rejection of residuals
      public static BatchDCWrapper.DCPropagationMethod DCPropMethod { get; set; }
      public static bool CorrectBStar { get; set; }
      public static double ConvCritTime { get; set; }
      public static double ConvCrit7Elt { get; set; }
      public static int MaxInterationNum { get; set; }
      public static bool UsePredictedRMS { get; set; } // true: use predicted RMS, false: use previous RMS_km
      public static double BResetValue { get; set; }

      static DcParameters()
      {
         GetParams();
      }

      /// <summary>
      /// Set default for all DC control parameters
      /// </summary>
      public static void SetDefault()
      {
         // integer-typed data fields   
         PrintOption = BatchDCWrapper.DCPrintOption.FirstLastSumamry;
         IsApplySenBiases = false;
         IsWeightedDC = false;
         EpochPlacement = BatchDCWrapper.EpochPlacementCtrl.NodalXingAtTime;

         Corr_AG = true;
         Corr_AF = true;
         Corr_PSI = true;
         Corr_CHI = true;
         Corr_L = true;
         Corr_N = true;
         Corr_B = true;
         Corr_AGOM = false;


         CorrOrder = BatchDCWrapper.CorrectionOrder.NOnly7Elem;
         CorrectOneIterOnly = false;
         ResidSelect = BatchDCWrapper.ResidualsSelection.All;
         ShowSensorPerformance = false;
         DeweightByObsPerTrack = false;
         IterSummaryOption = BatchDCWrapper.IterationSummaryOption.None;
         UseFastNumPartials = false;
         LTC = 1;
         NBruce = 0;
         DCPropMethod = BatchDCWrapper.DCPropagationMethod.SGP4;
         CorrectBStar = false;
         MaxInterationNum = 10;
         UsePredictedRMS = false;

         // real-typed data field
         RMSMultiplier = 4.0;
         SpecifiedEpoch = 0;
         FirstPassDeltaTRej = 20.0;
         ConvCritTime = 5.0;
         ConvCrit7Elt = 1.0;
         BResetValue = 0.01;

      }


      /// <summary>
      /// Get DC control parameters from BatchDC dll
      /// </summary>
      public static void GetParams()
      {
         int[] xai_ctrl = new int[256];
         double[] xar_ctrl = new double[256];
         byte[] xas_ctrl = new byte[DllMainWrapper.GETSETSTRLEN];
         BatchDCWrapper.BatchDCGetParams(xai_ctrl, xar_ctrl, xas_ctrl);

         // integer-typed data fields   
         PrintOption = (BatchDCWrapper.DCPrintOption)xai_ctrl[BatchDCWrapper.XAI_CTRL_PRINTOPTION];
         IsApplySenBiases = xai_ctrl[BatchDCWrapper.XAI_CTRL_DEBIASOBS] == 1;
         IsWeightedDC = xai_ctrl[BatchDCWrapper.XAI_CTRL_WEIGHTEDDC] == 1;
         EpochPlacement = (BatchDCWrapper.EpochPlacementCtrl)xai_ctrl[BatchDCWrapper.XAI_CTRL_EPOCHOPTION];

         Corr_AG = xai_ctrl[BatchDCWrapper.XAI_CTRL_CORRECT_AG] == 1;
         Corr_AF = xai_ctrl[BatchDCWrapper.XAI_CTRL_CORRECT_AF] == 1;
         Corr_PSI = xai_ctrl[BatchDCWrapper.XAI_CTRL_CORRECT_PSI] == 1;
         Corr_CHI = xai_ctrl[BatchDCWrapper.XAI_CTRL_CORRECT_CHI] == 1;
         Corr_L = xai_ctrl[BatchDCWrapper.XAI_CTRL_CORRECT_L] == 1;
         Corr_N = xai_ctrl[BatchDCWrapper.XAI_CTRL_CORRECT_N] == 1;
         Corr_B = xai_ctrl[BatchDCWrapper.XAI_CTRL_CORRECT_B] == 1;
         Corr_AGOM = xai_ctrl[BatchDCWrapper.XAI_CTRL_CORRECT_AGOM] == 1;


         CorrOrder = (BatchDCWrapper.CorrectionOrder)xai_ctrl[BatchDCWrapper.XAI_CTRL_CORRECTORDER];
         CorrectOneIterOnly = xai_ctrl[BatchDCWrapper.XAI_CTRL_FOR1ITERONLY] == 1;
         ResidSelect = (BatchDCWrapper.ResidualsSelection)xai_ctrl[BatchDCWrapper.XAI_CTRL_RESIDSELECT];
         ShowSensorPerformance = xai_ctrl[BatchDCWrapper.XAI_CTRL_SENPERFORM] == 1;
         DeweightByObsPerTrack = xai_ctrl[BatchDCWrapper.XAI_CTRL_DWOBSPERTRCK] == 1;
         IterSummaryOption = (BatchDCWrapper.IterationSummaryOption)xai_ctrl[BatchDCWrapper.XAI_CTRL_ITERSUMOPT];
         UseFastNumPartials = xai_ctrl[BatchDCWrapper.XAI_CTRL_PARTIALMETH] == 1;
         LTC = xai_ctrl[BatchDCWrapper.XAI_CTRL_LTC];
         NBruce = xai_ctrl[BatchDCWrapper.XAI_CTRL_BRUCE];
         DCPropMethod = (BatchDCWrapper.DCPropagationMethod)xai_ctrl[BatchDCWrapper.XAI_CTRL_PROPMETHOD];
         CorrectBStar = xai_ctrl[BatchDCWrapper.XAI_CTRL_CORRECTBVSX] == 1;
         MaxInterationNum = xai_ctrl[BatchDCWrapper.XAI_CTRL_MAXOFITERS];
         UsePredictedRMS = xai_ctrl[BatchDCWrapper.XAI_CTRL_USEPREDRMS] == 1;

         // real-typed data fields
         RMSMultiplier = xar_ctrl[BatchDCWrapper.XAR_CTRL_RMSMULT];
         SpecifiedEpoch = xar_ctrl[BatchDCWrapper.XAR_CTRL_USEREPOCH];
         FirstPassDeltaTRej = xar_ctrl[BatchDCWrapper.XAR_CTRL_1STPASDELTAT];
         ConvCritTime = xar_ctrl[BatchDCWrapper.XAR_CTRL_CNVCRITONT];
         ConvCrit7Elt = xar_ctrl[BatchDCWrapper.XAR_CTRL_CNVCRITON7ELT];
         BResetValue = xar_ctrl[BatchDCWrapper.XAR_CTRL_BRESET];

      }

      /// <summary>
      /// Get DC control parameters from BatchDC dll
      /// </summary>
      public static void GetParams(double[] xa_dcCtrl)
      {
         // integer-typed data fields   
         IsApplySenBiases = xa_dcCtrl[BatchDCWrapper.XA_DCCTRL_DEBIASOBS] == 1;
         IsWeightedDC = xa_dcCtrl[BatchDCWrapper.XA_DCCTRL_WEIGHTEDDC] == 1;
         EpochPlacement = (BatchDCWrapper.EpochPlacementCtrl)xa_dcCtrl[BatchDCWrapper.XA_DCCTRL_EPOCHOPTION];

         Corr_AG = xa_dcCtrl[BatchDCWrapper.XA_DCCTRL_CORRECT_AG] == 1;
         Corr_AF = xa_dcCtrl[BatchDCWrapper.XA_DCCTRL_CORRECT_AF] == 1;
         Corr_PSI = xa_dcCtrl[BatchDCWrapper.XA_DCCTRL_CORRECT_PSI] == 1;
         Corr_CHI = xa_dcCtrl[BatchDCWrapper.XA_DCCTRL_CORRECT_CHI] == 1;
         Corr_L = xa_dcCtrl[BatchDCWrapper.XA_DCCTRL_CORRECT_L] == 1;
         Corr_N = xa_dcCtrl[BatchDCWrapper.XA_DCCTRL_CORRECT_N] == 1;
         Corr_B = xa_dcCtrl[BatchDCWrapper.XA_DCCTRL_CORRECT_B] == 1;
         Corr_AGOM = xa_dcCtrl[BatchDCWrapper.XA_DCCTRL_CORRECT_AGOM] == 1;


         CorrOrder = (BatchDCWrapper.CorrectionOrder)xa_dcCtrl[BatchDCWrapper.XA_DCCTRL_CORRECTORDER];
         CorrectOneIterOnly = xa_dcCtrl[BatchDCWrapper.XA_DCCTRL_FOR1ITERONLY] == 1;
         ResidSelect = (BatchDCWrapper.ResidualsSelection)xa_dcCtrl[BatchDCWrapper.XA_DCCTRL_RESIDSELECT];

         DeweightByObsPerTrack = xa_dcCtrl[BatchDCWrapper.XA_DCCTRL_DWOBSPERTRCK] == 1;
         UseFastNumPartials = xa_dcCtrl[BatchDCWrapper.XA_DCCTRL_PARTIALMETH] == 1;
         LTC = (int)xa_dcCtrl[BatchDCWrapper.XA_DCCTRL_LTC];
         NBruce = (int)xa_dcCtrl[BatchDCWrapper.XA_DCCTRL_BRUCE];
         DCPropMethod = (BatchDCWrapper.DCPropagationMethod)xa_dcCtrl[BatchDCWrapper.XA_DCCTRL_PROPMETHOD];
         CorrectBStar = xa_dcCtrl[BatchDCWrapper.XA_DCCTRL_CORRECTBVSX] == 1;
         MaxInterationNum = (int)xa_dcCtrl[BatchDCWrapper.XA_DCCTRL_MAXOFITERS];
         UsePredictedRMS = xa_dcCtrl[BatchDCWrapper.XA_DCCTRL_USEPREDRMS] == 1;

         // real-typed data fields
         RMSMultiplier = xa_dcCtrl[BatchDCWrapper.XA_DCCTRL_RMSMULT];
         SpecifiedEpoch = xa_dcCtrl[BatchDCWrapper.XA_DCCTRL_USEREPOCH];
         FirstPassDeltaTRej = xa_dcCtrl[BatchDCWrapper.XA_DCCTRL_1STPASDELTAT];
         ConvCritTime = xa_dcCtrl[BatchDCWrapper.XA_DCCTRL_CNVCRITONT];
         ConvCrit7Elt = xa_dcCtrl[BatchDCWrapper.XA_DCCTRL_CNVCRITON7ELT];
         BResetValue = xa_dcCtrl[BatchDCWrapper.XA_DCCTRL_BRESET];
      }


      /// <summary>
      /// Set BatchDC control paramters from the class back to the BatchDC dll
      /// </summary>
      public static void SetParams()
      {
         int[] xai_ctrl = new int[256];
         double[] xar_ctrl = new double[256];
         string xas_ctrl = "";

         // integer-typed data fields   
         xai_ctrl[BatchDCWrapper.XAI_CTRL_PRINTOPTION] = (int)PrintOption;
         xai_ctrl[BatchDCWrapper.XAI_CTRL_DEBIASOBS] = IsApplySenBiases ? 1 : 0;
         xai_ctrl[BatchDCWrapper.XAI_CTRL_WEIGHTEDDC] = IsWeightedDC ? 1 : 0;
         xai_ctrl[BatchDCWrapper.XAI_CTRL_EPOCHOPTION] = (int)EpochPlacement;

         xai_ctrl[BatchDCWrapper.XAI_CTRL_CORRECT_AG] = Corr_AG ? 1 : 0;
         xai_ctrl[BatchDCWrapper.XAI_CTRL_CORRECT_AF] = Corr_AF ? 1 : 0;
         xai_ctrl[BatchDCWrapper.XAI_CTRL_CORRECT_PSI] = Corr_PSI ? 1 : 0;
         xai_ctrl[BatchDCWrapper.XAI_CTRL_CORRECT_CHI] = Corr_CHI ? 1 : 0;
         xai_ctrl[BatchDCWrapper.XAI_CTRL_CORRECT_L] = Corr_L ? 1 : 0;
         xai_ctrl[BatchDCWrapper.XAI_CTRL_CORRECT_N] = Corr_N ? 1 : 0;
         xai_ctrl[BatchDCWrapper.XAI_CTRL_CORRECT_B] = Corr_B ? 1 : 0;
         xai_ctrl[BatchDCWrapper.XAI_CTRL_CORRECT_AGOM] = Corr_AGOM ? 1 : 0;


         xai_ctrl[BatchDCWrapper.XAI_CTRL_CORRECTORDER] = (int)CorrOrder;
         xai_ctrl[BatchDCWrapper.XAI_CTRL_FOR1ITERONLY] = CorrectOneIterOnly ? 1 : 0;
         xai_ctrl[BatchDCWrapper.XAI_CTRL_RESIDSELECT] = (int)ResidSelect;
         xai_ctrl[BatchDCWrapper.XAI_CTRL_SENPERFORM] = ShowSensorPerformance ? 1 : 0;
         xai_ctrl[BatchDCWrapper.XAI_CTRL_DWOBSPERTRCK] = DeweightByObsPerTrack ? 1 : 0;
         xai_ctrl[BatchDCWrapper.XAI_CTRL_ITERSUMOPT] = (int)IterSummaryOption;
         xai_ctrl[BatchDCWrapper.XAI_CTRL_PARTIALMETH] = UseFastNumPartials ? 1 : 0;
         xai_ctrl[BatchDCWrapper.XAI_CTRL_LTC] = LTC;
         xai_ctrl[BatchDCWrapper.XAI_CTRL_BRUCE] = NBruce;
         xai_ctrl[BatchDCWrapper.XAI_CTRL_PROPMETHOD] = (int)DCPropMethod;
         xai_ctrl[BatchDCWrapper.XAI_CTRL_CORRECTBVSX] = CorrectBStar ? 1 : 0;
         xai_ctrl[BatchDCWrapper.XAI_CTRL_MAXOFITERS] = MaxInterationNum;
         xai_ctrl[BatchDCWrapper.XAI_CTRL_USEPREDRMS] = UsePredictedRMS ? 1 : 0;

         // real-typed data fields
         xar_ctrl[BatchDCWrapper.XAR_CTRL_RMSMULT] = RMSMultiplier;
         xar_ctrl[BatchDCWrapper.XAR_CTRL_USEREPOCH] = SpecifiedEpoch;
         xar_ctrl[BatchDCWrapper.XAR_CTRL_1STPASDELTAT] = FirstPassDeltaTRej;
         xar_ctrl[BatchDCWrapper.XAR_CTRL_CNVCRITONT] = ConvCritTime;
         xar_ctrl[BatchDCWrapper.XAR_CTRL_CNVCRITON7ELT] = ConvCrit7Elt;
         xar_ctrl[BatchDCWrapper.XAR_CTRL_BRESET] = BResetValue;

         BatchDCWrapper.BatchDCSetParams(xai_ctrl, xar_ctrl, xas_ctrl);

      }

      public static void PrintBatchDCCtrls(StreamWriter sw, int isSpectr)
      {
         string[] yesNo = new string[] { "no", "yes" };
         string[] ltcStrs = new string[] { "will not be applied",
                                           "will be applied analytically",
                                           "will be applied exactly" };

         string[] resComMethStrs = new string[] { "SPADOC 4", "DELTA/427M" };

         bool isSP = (DCPropMethod == BatchDCWrapper.DCPropagationMethod.SP);
         bool isXP = (DCPropMethod == BatchDCWrapper.DCPropagationMethod.SGP4XP);


         sw.WriteLine("{0} BATCH DC OPTIONS", isSP ? "SP" : "SGP4");
         sw.WriteLine("===============================================================================");
         sw.WriteLine(" A.  PRINTOUT CONTROL FLAG        : {0}", PrintOption.ToDescription());
         sw.WriteLine(" C.  DEBIAS & WEIGHTED DC FLAGS   : {0} debias obs; {1}weighted DC",
            IsApplySenBiases ? "" : "do not", IsWeightedDC ? "" : "un");
         if (NBruce == 0)
            sw.WriteLine("     {0}WEIGHTED 1st PASS REJ. CRIT: {0, 10:F2} km", IsWeightedDC ? "" : "UN", 100.0);


         sw.WriteLine(" D.  RMS MULTIPLIER               : {0, 5:F2}", RMSMultiplier);
         sw.WriteLine(" E.  EPOCH PLACEMENT              : {0}", EpochPlacement.ToDescription());
         if (EpochPlacement == BatchDCWrapper.EpochPlacementCtrl.NodalXingAtTime ||
             EpochPlacement == BatchDCWrapper.EpochPlacementCtrl.AtSpecifiedTime && SpecifiedEpoch > 0)
            sw.WriteLine("        SPECIFIED EPOCH           : {0} (YYDDDHHMMSS.SSS)",
               TimeFuncWrapper.UTCToDtg15Str(SpecifiedEpoch));


         sw.WriteLine(" F.  ITER. or PERFORMANCE AN.SUMM : {0}; {1}Perf Anal Summary", IterSummaryOption.ToDescription(),
            ShowSensorPerformance ? "" : "no ");
         sw.WriteLine(" G.  ELT CORRECTION FLAGS       L : {0}", Corr_L ? "yes" : "no");
         sw.WriteLine(" H.                             N : {0}", Corr_N ? "yes" : "no");
         sw.WriteLine(" I.                         {0} : {1}", (isSP || isXP) ? "BTERM" : "BSTAR", Corr_B ? "yes" : "no");
         sw.WriteLine(" J.                     CHI & PSI : {0}", Corr_CHI ? "yes" : "no");
         sw.WriteLine(" K.                       AF & AG : {0}", Corr_PSI ? "yes" : "no");

         // print subset order/control flags
         sw.WriteLine(" L.  SUBSET/CORRECTION ORDER      : {0}", CorrOrder.ToDescription());

         if (CorrOrder != BatchDCWrapper.CorrectionOrder.SevenElem)
            sw.WriteLine(" M.  SUBSET CONTROL FLAG          : {0}",
               CorrectOneIterOnly ? "correct subset for one iteration only" : "iterate subset to convergence");

         sw.WriteLine(" N.  1st-PASS delta-T REJECT CRIT : {0, 6:F2} min", FirstPassDeltaTRej);

         // print BRUCE(no rejection) flag
         sw.WriteLine(" O.  DO NOT REJECT RESIDUALS FOR  : {0:D2} iterations", NBruce);

         if (DCPropMethod == BatchDCWrapper.DCPropagationMethod.SP)
            sw.WriteLine(" P.  PARTIALS COMPUTATION METHOD  : {0}", UseFastNumPartials ? "numerical" : "analytical");

         // print convergence criteria
         sw.WriteLine(" R.  TIME/7-ELT CONVERGENCE CRIT  : {0, 4:F2} / {1, 4:F2} percent", ConvCritTime, ConvCrit7Elt);

         // print max iterations to convergence
         sw.WriteLine(" S.  MAX # ITERATIONS TO CONVERGE : {0:D2} iterations", MaxInterationNum);

         // print predicted RMS control
         sw.WriteLine(" T.  PREDICTED RMS CONTROL        : {0}",
            UsePredictedRMS ? "use predicted rms for convergence" : "use previous rms for convergence");

         // print track-deweighting flag
         sw.WriteLine(" U.  TRACK DEWEIGHTING ?          : {0}", DeweightByObsPerTrack ? "yes" : "no");

         // print alternate drag correction controls
         if (isSP)
            sw.WriteLine(" V.  CORRECT  AGOM ?              : {0}", Corr_AGOM ? "yes" : "no");
         else
         {
            if (Corr_B)
               sw.WriteLine(" V.  CORRECT X or DRAG TERM ?     : {0}", CorrectBStar ? "DRAG TERM" : "X");
            else
               sw.WriteLine(" V.  CORRECT X or DRAG TERM ?     : neither");

         }

         // print light-time correction control
         sw.WriteLine(" W.  LIGHT-TIME CORRECTION        : {0}", ltcStrs[LTC]);

         sw.WriteLine(" X.  RESIDs COMPUTATION METHOD    : {0}", resComMethStrs[isSpectr]);

         sw.WriteLine("===============================================================================");
         sw.WriteLine();
         sw.WriteLine();


         return;

      }

      public static void PrintBatchDCCtrls(StringBuilder sb, int isSpectr)
      {
         string[] yesNo = new string[] { "no", "yes" };
         string[] ltcStrs = new string[] { "will not be applied",
                                           "will be applied analytically",
                                           "will be applied exactly" };

         string[] resComMethStrs = new string[] { "SPADOC 4", "DELTA/427M" };

         bool isSP = (DCPropMethod == BatchDCWrapper.DCPropagationMethod.SP);
         bool isXP = (DCPropMethod == BatchDCWrapper.DCPropagationMethod.SGP4XP);


         sb.AppendLine(string.Format("{0} BATCH DC OPTIONS", isSP ? "SP" : "SGP4"));
         sb.AppendLine("===============================================================================");
         sb.AppendLine();
         sb.AppendLine(string.Format(" A.  PRINTOUT CONTROL FLAG        : {0}", PrintOption.ToDescription()));
         sb.AppendLine(string.Format(" C.  DEBIAS & WEIGHTED DC FLAGS   : {0} debias obs; {1}weighted DC",
            IsApplySenBiases ? "" : "do not", IsWeightedDC ? "" : "un"));
         if (NBruce == 0)
            sb.AppendLine(string.Format("     {0}WEIGHTED 1st PASS REJ. CRIT: {1, 10:F2} km", IsWeightedDC ? "" : "UN", 100.0));


         sb.AppendLine(string.Format(" D.  RMS MULTIPLIER               : {0, 5:F2}", RMSMultiplier));
         sb.AppendLine(string.Format(" E.  EPOCH PLACEMENT              : {0}", EpochPlacement.ToDescription()));
         if (EpochPlacement == BatchDCWrapper.EpochPlacementCtrl.NodalXingAtTime ||
             EpochPlacement == BatchDCWrapper.EpochPlacementCtrl.AtSpecifiedTime && SpecifiedEpoch > 0)
            sb.AppendLine(string.Format("        SPECIFIED EPOCH           : {0} (YYDDDHHMMSS.SSS)",
               TimeFuncWrapper.UTCToDtg15Str(SpecifiedEpoch)));


         sb.AppendLine(string.Format(" F.  ITER. or PERFORMANCE AN.SUMM : {0}; {1}Perf Anal Summary", IterSummaryOption.ToDescription(),
            ShowSensorPerformance ? "" : "no "));
         sb.AppendLine(string.Format(" G.  ELT CORRECTION FLAGS       L : {0}", Corr_L ? "yes" : "no"));
         sb.AppendLine(string.Format(" H.                             N : {0}", Corr_N ? "yes" : "no"));
         sb.AppendLine(string.Format(" I.                         {0} : {1}", (isSP || isXP) ? "BTERM" : "BSTAR", Corr_B ? "yes" : "no"));
         sb.AppendLine(string.Format(" J.                     CHI & PSI : {0}", Corr_CHI ? "yes" : "no"));
         sb.AppendLine(string.Format(" K.                       AF & AG : {0}", Corr_PSI ? "yes" : "no"));

         // print subset order/control flags
         sb.AppendLine(string.Format(" L.  SUBSET/CORRECTION ORDER      : {0}", CorrOrder.ToDescription()));

         if (CorrOrder != BatchDCWrapper.CorrectionOrder.SevenElem)
            sb.AppendLine(string.Format(" M.  SUBSET CONTROL FLAG          : {0}",
               CorrectOneIterOnly ? "correct subset for one iteration only" : "iterate subset to convergence"));

         sb.AppendLine(string.Format(" N.  1st-PASS delta-T REJECT CRIT : {0, 6:F2} min", FirstPassDeltaTRej));

         // print BRUCE(no rejection) flag
         sb.AppendLine(string.Format(" O.  DO NOT REJECT RESIDUALS FOR  : {0:D2} iterations", NBruce));

         if (DCPropMethod == BatchDCWrapper.DCPropagationMethod.SP)
            sb.AppendLine(string.Format(" P.  PARTIALS COMPUTATION METHOD  : {0}", UseFastNumPartials ? "numerical" : "analytical"));

         // print convergence criteria
         sb.AppendLine(string.Format(" R.  TIME/7-ELT CONVERGENCE CRIT  : {0, 4:F2} / {1, 4:F2} percent", ConvCritTime, ConvCrit7Elt));

         // print max iterations to convergence
         sb.AppendLine(string.Format(" S.  MAX # ITERATIONS TO CONVERGE : {0:D2} iterations", MaxInterationNum));

         // print predicted RMS control
         sb.AppendLine(string.Format(" T.  PREDICTED RMS CONTROL        : {0}",
            UsePredictedRMS ? "use predicted rms for convergence" : "use previous rms for convergence"));

         // print track-deweighting flag
         sb.AppendLine(string.Format(" U.  TRACK DEWEIGHTING ?          : {0}", DeweightByObsPerTrack ? "yes" : "no"));

         // print alternate drag correction controls
         if (isSP || isXP)
            sb.AppendLine(string.Format(" V.  CORRECT  AGOM ?              : {0}", Corr_AGOM ? "yes" : "no"));
         else
         {
            if (Corr_B)
               sb.AppendLine(string.Format(" V.  CORRECT X or DRAG TERM ?     : {0}", CorrectBStar ? "DRAG TERM" : "X"));
            else
               sb.AppendLine(string.Format(" V.  CORRECT X or DRAG TERM ?     : neither"));

         }

         // print light-time correction control
         sb.AppendLine(string.Format(" W.  LIGHT-TIME CORRECTION        : {0}", ltcStrs[LTC]));

         sb.AppendLine(string.Format(" X.  RESIDs COMPUTATION METHOD    : {0}", resComMethStrs[isSpectr]));

         sb.AppendLine(string.Format("==============================================================================="));
         sb.AppendLine();
         sb.AppendLine();


         return;

      }

      public static string GetPCard()
      {
         byte[] pcard = new byte[512];

         BatchDCWrapper.BatchDCGetPCard(pcard);
         return Utility.BytesToStringLRTrim(pcard);
      }
   }
}


