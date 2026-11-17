using AFSPC.AstroStds.Utilities;
using System.ComponentModel;

namespace AFSPC.AstroStds.Wrappers
{
   public static partial class BatchDCWrapper
   {
      public enum DCPrintOption
      {
         [Description("Print first & last iteration & summary")]
         FirstLastSumamry = 0,
         [Description("Print every pass & summary")]
         EveryPassSummary = 1,
         [Description("Print summary only")]
         SummaryOnly = 2,
         [Description("Print output elements only")]
         ElementOnly = 3,
         [Description("Do not print anything")]
         Nothing = 4,

      };

      public enum EpochPlacementCtrl
      {
         [Description("Nodal Crossing nearest latest ob")]
         NodalXingLatestOb = 0,
         [Description("Exact time of latest ob")]
         LatestOb = 1,
         [Description("Nodal Crossing closest to specified time")]
         NodalXingAtTime = 2,
         [Description("Do not change epoch")]
         AtEpoch = 3,
         [Description("Exact at specified time")]
         AtSpecifiedTime = 4,
         [Description("Middle of obs span")]
         MiddleObsSpan = 5,
         [Description("Exact time of earliest ob")]
         EarliestOb = 6,
         [Description("Beginning of day of Epoch")]
         BeginDayEpoch = 7,
         [Description("Beginning of day of Last obs")]
         BeginDayLastObs = 8,
         [Description("Nodal crossing nearest epoch")]
         NodalXingNearEpoch = 9,
      };

      public enum CorrectionOrder
      {
         [Description("7-element only")]
         SevenElem = 0,
         [Description("Time, then 7-element")]
         Time7Elem = 1,
         [Description("Time, plane, then 7-element")]
         TimePlan7Elem = 2,
         [Description("Plane, time, then 7-element")]
         PlanTime7Elem = 3,
         [Description("Plane, then 7-element")]
         Plan7Elem = 4,
         [Description("n only, then 7-element")]
         NOnly7Elem = 5,
         [Description("L only, then 7-element")]
         LOnly7Elem = 6,
         [Description("L & n only, then 7-element")]
         LNOnly7Elem = 7,
         [Description("Af/Ag, L, n only, then 7-element")]
         AfAgLNOnly7Elem = 8,
         [Description("6-element, then 7-element")]
         SixElem7Elem = 9,

      };

      public enum ResidualsSelection
      {
         [Description("All, use all reported observables")]
         All = 0,
         [Description("Use range residuals only")]
         RangeOnly = 1,
         [Description("Use Az/El residuals only")]
         AzElOnly = 2,
         [Description("Use delta-height residuals only")]
         DeltaHOnly = 3,
         [Description("Use Beta residuals only")]
         BetaOnly = 4,
         [Description("Use Az/El & delta-height residuals only")]
         AzElDeltaHNOnly = 6,
         [Description("Use range-rate only")]
         RangeRateOnly = 8,

      };

      public enum IterationSummaryOption
      {
         [Description("No iteration summary")]
         None = 0,
         [Description("Print summary every iteration (Keplerian elts)")]
         EveryIterationKep = 1,
         [Description("Print summary every iteration (Equinoctial elts)")]
         EveryIterationEqnx = 2,

      };

      //public enum LightTimeCorrectionOption
      //{
      //   [Description("1 - Apply analytically")]
      //   Analytically = 1,
      //   [Description("2 - Apply exactly")]
      //   Exactly = 2,
      //}

      public enum DCPropagationMethod
      {
         [Description("SGP4")]
         SGP4 = 0,
         [Description("SGP4-XP")]
         SGP4XP = 4,
         [Description("SP")]
         SP = 99,
      }

      public static double GetFitSpanDays(double edr)
      {
         double fitSpanInDays = 0;
         if (edr <= 0)
            fitSpanInDays = 12.0;
         else if (edr > 0 && edr <= 0.00060)
            fitSpanInDays = 7.0;
         else if (edr > 0.00060 && edr <= 0.001)
            fitSpanInDays = 4.0;
         else if (edr > 0.001 && edr <= 0.0015)
            fitSpanInDays = 3.0;
         else if (edr > 0.0015 && edr <= 0.002)
            fitSpanInDays = 2.5;
         else if (edr > 0.002 && edr <= 0.003)
            fitSpanInDays = 2.0;
         else if (edr > 0.003 && edr <= 0.006)
            fitSpanInDays = 1.5;
         else if (edr > 0.006 && edr <= 0.009)
            fitSpanInDays = 1.5;
         else if (edr > 0.009 && edr <= 0.015)
            fitSpanInDays = 1.0;
         else if (edr > 0.015 && edr <= 0.05)
            fitSpanInDays = 1.0;
         else if (edr > 0.05)
            fitSpanInDays = 0.5;

         return fitSpanInDays;
      }
   }
}
