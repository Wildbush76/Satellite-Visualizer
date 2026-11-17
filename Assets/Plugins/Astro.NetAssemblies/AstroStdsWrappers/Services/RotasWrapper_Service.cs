using System;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.ComponentModel;

namespace AFSPC.AstroStds.Wrappers
{
   public static partial class RotasWrapper
   {
      public enum LightTimeCorrection
      {
         /*
         [Description("0 - No Light-Time Corr.")]
         NoLTC = 0,
         [Description("1- Apply Analytically")]
         Analytical = 1,
         [Description("2- Apply Exactly")]
         Exactly = 2 */
         [Description("No light-time corr.")]
         NoLTC = 0,
         [Description("Apply analytical")]
         Analytical = 1,
         [Description("Apply exactly")]
         Exactly = 2,

      }

      public enum ObservationTag
      {
         [Description("CaveNet Tag")]
         CaveNet = 0,
         [Description("Spadoc Tag")]
         Spadoc = 1,
         [Description("Original Tag")]
         Original = 2,
         [Description("Ignore Tag")]
         IgnoreTag = 3,
      }

      public enum ResCompMethodFlag
      {
         [Description("Delta 427M")]
         Delta427M = 1,
         [Description("Spadoc 4C")]
         Spadoc4C = 2,
      }

   }
}
