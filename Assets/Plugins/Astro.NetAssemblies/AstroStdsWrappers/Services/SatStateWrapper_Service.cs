using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using AFSPC.AstroStds.Utilities;

namespace AFSPC.AstroStds.Wrappers
{
   public static partial class SatStateWrapper
   {
      // Full description of satellite's orbital element type
      public static string[] EltTypeStr = new string[] { "TLE (SGP)", "TLE (SGP4)", "TLE (SP)", "SP Vector (B1P)", "VCM", "EXT EPHEMERIS" };

      // Short description of satellite's orbital element type
      public static string[] RecTypeStr = new string[] { "TLE ", "TLE ", "SPTLE ", "SPVEC ", "VCM ", "EXTEPH ", "TLE ", "TLEXP ", "PPT3 "};


      // Returns all currently loaded satKeys
      public static long[] GetCurrLoadedSatKeys(int order)
      {
         int numSats = SatStateGetCount();
         if(numSats > 0)
         {
            long[] satKeys = new long[numSats];
            SatStateGetLoaded(order, satKeys);
            return satKeys;
         }
         else
         {
            return null;
         }
      }

      // Returns all currently loaded satKeys
      public static long[] GetCurrLoadedSatKeys()
      {
         int numSats = SatStateGetCount();
         if (numSats > 0)
         {
            long[] satKeys = new long[numSats];
            SatStateGetLoaded(2, satKeys);
            return satKeys;
         }
         else
         {
            return null;
         }
      }

      public static long[] SatKeysFrFile(string inputFile)
      {
         long[] prevSatKeys = GetCurrLoadedSatKeys();
         long[] newSatKeys;

         SatStateLoadFile(inputFile, XF_TASK_SATONLY);
         if (prevSatKeys == null)
            newSatKeys = GetCurrLoadedSatKeys();
         else
            newSatKeys = GetCurrLoadedSatKeys().Except(prevSatKeys).ToArray<long>();

         return newSatKeys;
      }

      public static long[] SatKeysFrFile(string inputFile, int xf_task)
      {
         long[] prevSatKeys = GetCurrLoadedSatKeys();
         long[] newSatKeys;

         SatStateLoadFile(inputFile, xf_task);
         if (prevSatKeys == null)
            newSatKeys = GetCurrLoadedSatKeys();
         else
            newSatKeys = GetCurrLoadedSatKeys().Except(prevSatKeys).ToArray<long>();

         return newSatKeys;
      }

   }
}
