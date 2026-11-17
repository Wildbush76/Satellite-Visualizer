using System;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.ComponentModel;

namespace AFSPC.AstroStds.Wrappers
{
   public static partial class ComboWrapper
   {
      public static int
         CARD_7P_FREPOCH = 0,
         CARD_7P_FRDATE = 1;

      // Print options
      public static int
         PRT_RELGEOONLY = 0,
         PRT_POSVEL = 1,
         PRT_LLH = 2,
         PRT_RELPOS = 3,
         PRT_NOOUTPUT = 4;



      public enum RelMinEnum
      {
         [Description("0- Do not compute relative minima")]
         RelMin0,
         [Description("1- Compute relative minima")]
         RelMin1,
      }

      public enum PrintOptsEnum
      {
         [Description("0- Relative geometry only")]
         PrintOpt0,
         [Description("1- Position/Velocity (plus option 0)")]
         PrintOpt1,
         [Description("2- Lat, Long, Height (plus option 1)")]
         PrintOpt2,
         [Description("3- Relative Position (plus option 2)")]
         PrintOpt3,
         [Description("4- No output")]
         PrintOpt4,
      }
   }
}
