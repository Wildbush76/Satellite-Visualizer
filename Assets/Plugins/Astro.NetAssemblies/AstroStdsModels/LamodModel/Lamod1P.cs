using AFSPC.AstroStds.Wrappers;
using AFSPC.AstroStds.Utilities;

namespace AFSPC.AstroStds.Models.LamodModel
{
   // Lamod control parameters class
   public static class Lamod1P
   {
      public static int     TimeFlg{ get; set; }
      public static double StartTime { get; set; }
      public static double StopTime { get; set; }
      public static double StepSize { get; set; }
      public static int PrtOpt { get; set; }
      public static LamodWrapper.GenObsFlgEnum PunchObs { get; set; }
      public static LamodWrapper.SenOptVisFlgEnum VisFlg { get; set; }
      public static LamodWrapper.StepModeEnum StepMode { get; set; }
      public static LamodWrapper.ProcModeEnum SchedMode { get; set; }
      public static LamodWrapper.NosticFlgEnum DiagMode { get; set; }
      public static double SolAspAngle { get; set; }
      public static bool UseEphemPoints { get; set; }
      public static bool ApplyLTC { get; set; }

      static Lamod1P()
      {
         Get1P();
      }

      // Copy Lamod control data (Lamod 1P-Card) from the Lamod dll to lamod1P object
      public static void Get1P()
      {
         char schedMode;
         int timeFlg, prtOpt, punchObs, visFlg, stepMode, diagMode;
         double startTime, stopTime, stepSize, solAspAngle;


         LamodWrapper.LamodGet1pAll(out timeFlg, out startTime, out stopTime, out stepSize, out prtOpt, out punchObs,
                                    out visFlg, out stepMode, out schedMode, out diagMode, out solAspAngle);

         TimeFlg = timeFlg;
         StartTime = startTime;
         StopTime = stopTime;
         StepSize = stepSize;
         PrtOpt = prtOpt;
         PunchObs = (LamodWrapper.GenObsFlgEnum)punchObs;
         VisFlg = (LamodWrapper.SenOptVisFlgEnum)visFlg;
         StepMode = (LamodWrapper.StepModeEnum)stepMode;
         SchedMode = (LamodWrapper.ProcModeEnum)schedMode;
         DiagMode = (LamodWrapper.NosticFlgEnum)diagMode;
         SolAspAngle = solAspAngle;

      }


      // Copy data from a lamod1P object to Lamod control data (Lamod 1P-Card) in the Lamod dll
      public static void Set1P()
      {
         char schedMode;
         int timeFlg, prtOpt, punchObs, visFlg, stepMode, diagMode;
         double startTime, stopTime, stepSize, solAspAngle;

         timeFlg = TimeFlg;
         startTime = StartTime;
         stopTime = StopTime;
         stepSize = StepSize;
         prtOpt = PrtOpt;
         punchObs = (int)PunchObs;
         visFlg = (int)VisFlg;
         stepMode = (int)StepMode;
         schedMode = (char)SchedMode;
         diagMode = (int)DiagMode;
         solAspAngle = SolAspAngle;

         LamodWrapper.LamodSet1pAll(timeFlg, startTime, stopTime, stepSize, prtOpt, punchObs,
                                    visFlg, stepMode, schedMode, diagMode, solAspAngle);


      }

      public static string Get1PCard()
      {
         byte[] lamod1pCard = new byte[DllMainWrapper.INPUTCARDLEN];

         LamodWrapper.LamodGet1pCard(lamod1pCard);

         return Utility.BytesToStringRTrim(lamod1pCard);


      }
   }
}
