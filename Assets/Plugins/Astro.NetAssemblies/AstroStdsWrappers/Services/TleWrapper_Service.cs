using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace AFSPC.AstroStds.Wrappers
{
   public static partial class TleWrapper
   {

      // TLE types (TLE ephemeris types) - They are different than ELTTYPE
      public enum TleTypeEnum
      {
         TLETYPE_SGP = 0,    // TLE SGP elset (Kozai mean motion)
         TLETYPE_SGP4 = 2,    // TLE SGP4 elset (Brouwer mean motion)
         TLETYPE_XP = 4,    // TLE SGP4-XP elset (Brouwer mean motion)
         TLETYPE_SP = 6,    // TLE SP elset (osculating elements)
      };
   }

}