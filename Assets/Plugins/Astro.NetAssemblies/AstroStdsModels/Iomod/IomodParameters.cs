using AFSPC.AstroStds.Utilities;
using AFSPC.AstroStds.Wrappers;
using System.ComponentModel;
using System.IO;
using System.Text;
using static AFSPC.AstroStds.Wrappers.ObsOpsWrapper;


namespace AFSPC.AstroStds.Models.Iomod
{
   public static class IomodParameters
   {
      public static bool PrintIOHG { get; set; }
      public static bool UseFirst3Obs{get; set;}
      public static int SatNum {get; set;}
      public static ObsSortOrderEnum ObsSortOrder {get; set;}
      public static string SatName {get; set;}
      public static bool RunDC {get; set;}
      public static bool RunRotas {get; set;}

      ////(+/ -)1 = (descending / ascending) time, sensor, obstype, elev
      ////(+/ -)2 = (descending / ascending) time, elevation
      ////(+/ -)3 = (descending / ascending) time, sensor, otype, el, satno
      ////(+/ -)4 = (descending / ascending) sensor, satno, time, elev
      ////(+/ -)5 = (descending / ascending) sensor, time, elevation
      ////(+/ -)6 = (descending / ascending) sensor, satno, obstype, time, elev
      ////(+/ -)7 = (descending / ascending) satno, time, sensor, otype, el
      ////(+/ -)8 = (reversed / same)        order of obs were read

      //public enum SortOrderEnum
      //{
      //   [Description("using sort order in obs tree")]
      //   sortP9 = 9,
      //   [Description("ascending time, sensor, obstype, elev")]
      //   sortM1 = -1,
      //   [Description("ascending time, elevation")]
      //   sortM2 = -2,
      //   [Description("ascending time, sensor, otype, el, satno")]
      //   sortM3 = -3,
      //   [Description("ascending sensor, satno, time, elev")]
      //   sortM4 = -4,
      //   [Description("ascending sensor, time, elevation")]
      //   sortM5 = -5,
      //   [Description("ascending sensor, satno, obstype, time, elev")]
      //   sortM6 = -6,
      //   [Description("ascending satno, time, sensor, otype, el")]
      //   sortM7 = -7,
      //   [Description("same order as obs were read")]
      //   sortM8 = -8,
      //   [Description("descending time, sensor, obstype, elev")]
      //   sortP1 =  1,
      //   [Description("descending time, elevation")]
      //   sortP2 =  2,
      //   [Description("descending time, sensor, otype, el, satno")]
      //   sortP3 =  3,
      //   [Description("descending sensor, satno, time, elev")]
      //   sortP4 =  4,
      //   [Description("descending sensor, time, elevation")]
      //   sortP5 =  5,
      //   [Description("descending sensor, satno, obstype, time, elev")]
      //   sortP6 =  6,
      //   [Description("descending satno, time, sensor, otype, el")]
      //   sortP7 =  7,
      //   [Description("reversed order as obs were read")]
      //   sortP8 =  8
      //}


      static IomodParameters()
      {
         GetParams();
      }

      /// <summary>
      /// Get IOMOD control parameters from IOMOD dll
      /// </summary>
      public static void GetParams()
      {
         int printIOHG;
         int useFirst3Obs;
         int satNum;
         int sortOrder;
         byte[] baSatName = new byte[8];
         int runDc;
         int runRotas;
         double[] extArr = new double[128];

         ObsOpsWrapper.IomodGetIpAll(out printIOHG, out useFirst3Obs, out satNum, out sortOrder, baSatName, out runDc, out runRotas, extArr);
         PrintIOHG = (printIOHG == 1);
         UseFirst3Obs = (useFirst3Obs == 1);
         SatNum = satNum;
         ObsSortOrder = (ObsSortOrderEnum)sortOrder;
         SatName = Utility.BytesToStringRTrim(baSatName);
         RunDC = (runDc == 1);
         RunRotas = (runRotas == 1);
      }


      /// <summary>
      /// Set IOMOD control paramters from the class back to the IOMOD dll
      /// </summary>
      public static void SetParams()
      {
         int printIOHG;
         int useFirst3Obs;
         int runDc;
         int runRotas;
         double[] extArr = new double[128];

         printIOHG = PrintIOHG ? 1 : 0;
         useFirst3Obs = UseFirst3Obs ? 1 : 0;
         runDc = RunDC ? 1 : 0;
         runRotas = RunRotas ? 1 : 0;

         ObsOpsWrapper.IomodSetIpAll(printIOHG, useFirst3Obs, SatNum, (int)ObsSortOrder, SatName, runDc, runRotas, extArr);
      }

      public static void PrintIomodCtrls(StreamWriter sw)
      {
         return; 
      }

      public static void PrintIomodCtrls(StringBuilder sb)
      {
         return;
      }
   }
}
