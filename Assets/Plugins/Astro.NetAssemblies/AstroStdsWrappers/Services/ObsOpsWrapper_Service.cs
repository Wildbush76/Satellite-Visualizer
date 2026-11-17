using System;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.ComponentModel;



namespace AFSPC.AstroStds.Wrappers
{
   public static partial class ObsOpsWrapper
   {
      //(+/ -)1 = (descending / ascending) time, sensor, obstype, elev
      //(+/ -)2 = (descending / ascending) time, elevation
      //(+/ -)3 = (descending / ascending) time, sensor, otype, el, satno
      //(+/ -)4 = (descending / ascending) sensor, satno, time, elev
      //(+/ -)5 = (descending / ascending) sensor, time, elevation
      //(+/ -)6 = (descending / ascending) sensor, satno, obstype, time, elev
      //(+/ -)7 = (descending / ascending) satno, time, sensor, otype, el
      //(+/ -)8 = (reversed / same)        order of obs were read

      public enum ObsSortOrderEnum
      {
         [Description("using sort order in obs tree")]
         sortP9 = 9,
         [Description("ascending time, sensor, obstype, elev")]
         sortM1 = -1,
         [Description("ascending time, elevation")]
         sortM2 = -2,
         [Description("ascending time, sensor, otype, el, satno")]
         sortM3 = -3,
         [Description("ascending sensor, satno, time, elev")]
         sortM4 = -4,
         [Description("ascending sensor, time, elevation")]
         sortM5 = -5,
         [Description("ascending sensor, satno, obstype, time, elev")]
         sortM6 = -6,
         [Description("ascending satno, time, sensor, otype, el")]
         sortM7 = -7,
         [Description("same order as obs were read")]
         sortM8 = -8,
         [Description("descending time, sensor, obstype, elev")]
         sortP1 = 1,
         [Description("descending time, elevation")]
         sortP2 = 2,
         [Description("descending time, sensor, otype, el, satno")]
         sortP3 = 3,
         [Description("descending sensor, satno, time, elev")]
         sortP4 = 4,
         [Description("descending sensor, time, elevation")]
         sortP5 = 5,
         [Description("descending sensor, satno, obstype, time, elev")]
         sortP6 = 6,
         [Description("descending satno, time, sensor, otype, el")]
         sortP7 = 7,
         [Description("reversed order as obs were read")]
         sortP8 = 8,
      };
   }
}
