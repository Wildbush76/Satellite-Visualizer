using System;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace AFSPC.AstroStds.Wrappers
{
   public static partial class EnvConstWrapper
   {
      public enum GeoConstIdxEnum
      {
         [Description("EGM-96")]
         GeoIdxEgm96 = 96,
         [Description("WGS-84")]
         GeoIdxWgs84 = 84,
         [Description("JGM-2")]
         GeoIdxJgm2 = 2,
         [Description("STEMP68")]
         GeoIdxStem68 = 68,
         [Description("GEM5")]
         GeoIdxGem5 = 5,
         [Description("GEM9")]
         GeoIdxGem9 = 9,
         [Description("WGS-72")]
         GeoIdxWgs72 = 72
      };

      public enum FkIdxEnum
      {
         [Description("FK5")]
         FkIdx5 = 5,
         [Description("FK4")]
         FkIdx4 = 4
      };


   }
}