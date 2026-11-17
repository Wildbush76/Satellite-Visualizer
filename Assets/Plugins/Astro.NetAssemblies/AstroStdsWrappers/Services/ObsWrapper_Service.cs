using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using AFSPC.AstroStds.Utilities;

namespace AFSPC.AstroStds.Wrappers
{
   public static partial class ObsWrapper
   {
      public static long[] GetCurrLoadedObsKeys(int order)
      {
         int numObss = ObsGetCount();
         if (numObss > 0)
         {
            long[] obsKeys = new long[numObss];
            ObsGetLoaded(order, obsKeys);
            return obsKeys;
         }
         else
         {
            return null;
         }
      }

      public static long[] GetCurrLoadedObsKeys()
      {
         int numObss = ObsGetCount();
         if (numObss > 0)
         {
            long[] obsKeys = new long[numObss];
            ObsGetLoaded(9, obsKeys);
            return obsKeys;
         }
         else
         {
            return null;
         }
      }

      public static long[] ObsKeysFrFile(string inputFile)
      {
         long[] prevObsKeys = GetCurrLoadedObsKeys();
         long[] newObsKeys;

         ObsLoadFile(inputFile);
         if (prevObsKeys == null)
            newObsKeys = GetCurrLoadedObsKeys();
         else
            newObsKeys = GetCurrLoadedObsKeys().Except(prevObsKeys).ToArray<long>();

         return newObsKeys;
      }

      public static long[] ObsKeysFrFile(string inputFile, int order)
      {
         long[] prevObsKeys = GetCurrLoadedObsKeys();
         long[] newObsKeys;

         ObsLoadFile(inputFile);
         if (prevObsKeys == null)
            newObsKeys = GetCurrLoadedObsKeys(order);
         else
            newObsKeys = GetCurrLoadedObsKeys(order).Except(prevObsKeys).ToArray<long>();

         return newObsKeys;
      }
   }
}