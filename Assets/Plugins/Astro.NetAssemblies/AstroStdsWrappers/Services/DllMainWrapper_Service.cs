using System;
using AFSPC.AstroStds.Utilities;
using System.ComponentModel;

namespace AFSPC.AstroStds.Wrappers
{
   public static partial class DllMainWrapper
   {
      /// <summary>
      /// Orbital Element Types enum
      /// </summary>
      public enum EltTypeEnum
      {
         [Description("TLE (SGP)")]
         ELTTYPE_TLE_SGP = 1,    // Orbital elset type - SGP Tle type 0
         [Description("TLE (SGP4)")]
         ELTTYPE_TLE_SGP4 = 2,    // Orbital elset type - SGP4 Tle type 2
         [Description("TLE (SP)")]
         ELTTYPE_TLE_SP = 3,    // Orbital elset type - SP Tle type 6
         [Description("SP (B1P)")]
         ELTTYPE_SPVEC_B1P = 4,    // Orbital elset type - SP Vector
         [Description("VCM")]
         ELTTYPE_VCM = 5,    // Orbital elset type - VCM
         [Description("EXT EPHEM")]
         ELTTYPE_EXTEPH = 6,    // Orbital elset type - External ephemeris
         [Description("TLE (XP)")]
         ELTTYPE_TLE_XP = 7,   // Orbital elset type - SGP Tle type 4 - XP
         [Description("TLE (PPT3)")]
         ELTTYPE_TLE_PPT3 = 8   // Orbital elset type - SGP Tle type 4 - XP
      };

      // Save File Mode
      public static int
         IOSAVE_NEW = 0,         // Create a new file whether or not the file already exists
         IOSAVE_APPEND = 1;      // Append contents to the current file

      // Save as file format
      public static int
         SAVE_CARDFORM = 0,      // Save working data in card-format form
         SAVE_FREEFORM = 1,      // Save working data in free-format form
         SAVE_XMLFORM = 2;      // Save working data in xml-format form


      public static int   //error code 0 = OK, 1 = Warning, 2 = Error
         IDX_ERR_NONE = 0,
         IDX_ERR_WARN = 1,
         IDX_ERR_ERROR = 2;

      public enum KeyOrder
      {
         Ascending = 0,
         Descending = 1,
         AsRead = 2,
         Quick = 9
      }

      // Return the last logged message in string format
      public static string GetLastErrMsgStr()
      {
         byte[] lastErrMsg = new byte[LOGMSGLEN];

         DllMainWrapper.GetLastErrMsg(lastErrMsg);

         return Utility.BytesToString(lastErrMsg);
      }


      /// <summary>
      /// Return error message string associated with the input errCode
      /// </summary>
      /// <param name="errCode"></param>
      /// <returns></returns>
      public static string GetErrMsgStr(int errCode)
      {
         var buf = new byte[512];
         DllMainWrapper.GetErrMsg(errCode, buf);
         return Utilities.Utility.BytesToString(buf).Trim('\0').Trim();
      }

      /// <summary>
      /// Returns last informational message string
      /// </summary>
      /// <returns>the last information message string</returns>
      public static string GetLastInfoMsgStr()
      {
         byte[] lastInfoMsg = new byte[LOGMSGLEN];

         DllMainWrapper.GetLastInfoMsg(lastInfoMsg);

         return Utility.BytesToString(lastInfoMsg);
      }

      // Show last logged error message and exit
      public static void ShowMsgAndTerminate()
      {
         byte[] lastErrMsg = new byte[LOGMSGLEN];

         GetLastErrMsg(lastErrMsg);
         Console.WriteLine(Utility.BytesToString(lastErrMsg));
         System.Environment.Exit(1);
      }

      public static char SecClassIntToChar(int secClassInt)
      {
         if(secClassInt == 1) 
            return 'U';
         else if(secClassInt == 2) 
            return 'C';
         else if(secClassInt == 3) 
            return 'S';
         else
            return 'U';
      }

   }
}

