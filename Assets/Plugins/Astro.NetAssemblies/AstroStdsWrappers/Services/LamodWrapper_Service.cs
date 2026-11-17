using System;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.ComponentModel;

namespace AFSPC.AstroStds.Wrappers
{
   public static partial class LamodWrapper
   {
      public static int LAMODINFOSTRLEN = 128;
      public static double EPSTIMEDAYS = (0.001 / 1440.0);


      public static int
         CARD_1P_FREPOCH = 0,     // Specified time span in minutes since epoch
         CARD_1P_FRDATE = 1;      // Specified time span in UTC  

      // Lamod look mode
      public static int
         STEPMODE_CULM = 0,
         STEPMODE_STEP = 1,
         STEPMODE_TASK = 2;
         

      // Indexes represent schedule modes
      public static char
         IDX_SCHED_OBS = 'O',  // Obs schedule mode
         IDX_SCHED_PASS = 'P';  // Pass schedule mode


 

      public enum StepModeEnum
      {
         [Description("0- Culmination Mode")]
         CulmMode = 0,
         [Description("1- Step Mode")]
         StepMode = 1,
         [Description("2- Task Mode")]
         TaskMode = 2,
      }

      /*
      public enum ProcModeEnum
      {
         [Description("O- OBS  SCHED")]
         ProcModeObsSched = 'O',
         [Description("P- PASS SCHED")]
         ProcModePasSched = 'P',
         [Description("S- SATELLITE")]
         ProcModeSatellite = 'S',
         [Description("R- SENSOR")]
         ProcModeSensor = 'R'
      }*/

      public enum ProcModeEnum
      {
         [Description("Sen/Sat/TimeSpan")]
         ProcModeObsSched = 'O',
         [Description("Sat/Sen/TimeSpan")]
         ProcModePasSched = 'P',
         [Description("Sat/TimeSpan/Sen")]
         ProcModeSatellite = 'S',
         [Description("Sen/TimeSpan/Sat")]
         ProcModeSensor = 'R'
      }


      public enum GenObsFlgEnum
      {
         [Description("0- Do Not Form OBS")]
         GenObsNone,
         [Description("1- B3 OBS From Looks")]
         GenObsB3,
         [Description("2- Transmission OBS From Looks")]
         GenObsTTY,
         [Description("3- SP Tasker V Obs")]
         GenObsSPTasker,
         [Description("4- CSV OBS From Looks")]
         GenObsCsv,
      }

      public enum SenOptVisFlgEnum
      {
         [Description("0- Accept All Passes (ground optical only)")]
         OptVisFlg0 = 0,
         [Description("1- Optical Visible Passes (optical sen)")]
         OptVisFlg1 = 1,
         [Description("2- 1 with Solar Aspect Angle < Max")]
         OptVisFlg2 = 2,
         [Description("3- Sen In Dark/No Restrctn on Sat (IR sen)")]
         OptVisFlg3 = 3,
         [Description("9- Use Sensor Limits Record Value")]
         OptVisFlg9 = 9
      }

      public enum NosticFlgEnum
      {
         [Description("0- None")]
         Nostic0,
         [Description("1- Print sen/sat position at each look")]
         Nostic1,
         [Description("2- Print pass-by-pass diagnostics")]
         Nostic2,
         [Description("3- Print sen/sat position and diagnostics")]
         Nostic3
      }
   }
}