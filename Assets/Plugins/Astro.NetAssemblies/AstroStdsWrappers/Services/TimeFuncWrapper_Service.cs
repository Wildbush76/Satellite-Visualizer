using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using AFSPC.AstroStds.Utilities;

namespace AFSPC.AstroStds.Wrappers
{
   public static partial class TimeFuncWrapper
   {

      public static double MINDS50UTC = 2192.0; // The earliest time that is allowed to be used in AstroStds

      public static double CurrTimeInDs50UTC()
      {
         DateTime now = DateTime.Now;

         return TimeComps1ToUTC(now.Year, now.DayOfYear, now.Hour, now.Minute, now.TimeOfDay.TotalSeconds % 60.0);
      }

      public static double DateTimeToDs50UTC(DateTime dateTime)
      {
         return TimeComps1ToUTC(dateTime.Year, dateTime.DayOfYear, dateTime.Hour, dateTime.Minute, dateTime.TimeOfDay.TotalSeconds % 60.0);
      }

      public static double DateTimeToDs50UTC(string dateTimeString)
      {
         DateTime dateTime = Convert.ToDateTime(dateTimeString);
         return TimeComps1ToUTC(dateTime.Year, dateTime.DayOfYear, dateTime.Hour, dateTime.Minute, dateTime.TimeOfDay.TotalSeconds % 60.0);
      }



      /// <summary>
      /// Returns new DateTime() instance
      /// </summary>
      /// <param name="ds50UTC">The input days since 1950 UTC</param>
      /// <returns>new DateTime instance</returns>
      public static DateTime Ds50UTCToDateTime(double ds50UTC)
      {
         int yr, month, dayOfMonth, hh, mm;
         double sss;

         UTCToTimeComps2(ds50UTC, out yr, out month, out dayOfMonth, out hh, out mm, out sss);
         return new DateTime(yr, month, dayOfMonth, hh, mm, (int)sss);
      }

      /// <summary>
      /// Converts ds50UTC to date time group "YYYY/DDD HHMM SS.SSS"
      /// </summary>
      /// <param name="ds50UTC"></param>
      /// <returns></returns>
      public static string UTCToDtg20Str(double ds50UTC)
      {
         byte[] dtg20ByteArr = new byte[20];

         UTCToDTG20(ds50UTC, dtg20ByteArr);

         return Utility.BytesToString(dtg20ByteArr);
      }

      /// <summary>
      /// Converts ds50UTC to date time group "YY/DDD HHMM SS.SSS"
      /// </summary>
      /// <param name="ds50UTC"></param>
      /// <returns></returns>
      public static string UTCToDtg18Str(double ds50UTC)
      {
         byte[] dtg20ByteArr = new byte[20];

         UTCToDTG20(ds50UTC, dtg20ByteArr);

         return Utility.BytesToString(dtg20ByteArr).Substring(2);
      }

      /// <summary>
      /// Converts ds50UTC to date time group "YYYYMonDDHHMMSS.SSS"
      /// </summary>
      /// <param name="ds50UTC"></param>
      /// <returns></returns>
      public static string UTCToDtg19Str(double ds50UTC)
      {
         byte[] dtg19ByteArr = new byte[19];

         UTCToDTG19(ds50UTC, dtg19ByteArr);

         return Utility.BytesToString(dtg19ByteArr);
      }

      /// <summary>
      /// Converts ds50UTC to date time group "YYYY/DDD.DDDDDDDD"
      /// </summary>
      /// <param name="ds50UTC"></param>
      /// <returns></returns>
      public static string UTCToDtg17Str(double ds50UTC)
      {
         byte[] dtg17ByteArr = new byte[17];

         UTCToDTG17(ds50UTC, dtg17ByteArr);

         return Utility.BytesToString(dtg17ByteArr);
      }

      /// <summary>
      /// Converts ds50UTC to date time group "YYYYDDDHHMMSS.SSS"
      /// </summary>
      /// <param name="ds50UTC"></param>
      /// <returns></returns>
      public static string UTCToDtg17aStr(double ds50UTC)
      {
         byte[] dtg20ByteArr = new byte[20];

         UTCToDTG20(ds50UTC, dtg20ByteArr);

         return Utility.BytesToString(dtg20ByteArr).Replace("/", "").Replace(" ", "");
      }


      /// <summary>
      /// Converts ds50UTC to date time group "YYDDDHHMMSS.SSS"
      /// </summary>
      /// <param name="ds50UTC"></param>
      /// <returns></returns>
      public static string UTCToDtg15Str(double ds50UTC)
      {
         byte[] dtg15ByteArr = new byte[15];

         UTCToDTG15(ds50UTC, dtg15ByteArr);

         return Utility.BytesToString(dtg15ByteArr);
      }

      /// <summary>
      /// Converts ds50UTC to date time group "YY DDD HH MM SS.SSSSSSS"
      /// </summary>
      /// <param name="ds50UTC"></param>
      /// <returns></returns>
      public static string UTCToDtg23Str(double ds50UTC)
      {
         int yr, dayOfYear, hh, mm;
         double sss;

         UTCToTimeComps1(ds50UTC, out yr, out dayOfYear, out hh, out mm, out sss);
         return string.Format("{0:D2} {1:D3} {2:D2} {3:D2} {4:00.0000000}", yr % 100, dayOfYear, hh, mm, sss);
      }

      /// <summary>
      /// Converts ds50UTC to date time group "YY DDD HH MM SS.SSS"
      /// </summary>
      /// <param name="ds50UTC"></param>
      /// <returns></returns>
      public static string UTCToDtg19aStr(double ds50UTC)
      {
         string dtg19a;
         int yr, dayOfYear, hh, mm;
         double sss;

         UTCToTimeComps1(ds50UTC, out yr, out dayOfYear, out hh, out mm, out sss);
         dtg19a = string.Format("{0:D2} {1:D3} {2:D2} {3:D2} {4:00.000}", yr%100, dayOfYear, hh, mm, sss);

         return dtg19a;
      }
   }

   public static partial class ExtMethod
   {
      public static List<double> Multiply(this List<double> inpList, double factor)
      {
         List<double> outList = new List<double>();
         for (int i = 0; i < inpList.Count; i++)
         {
            outList.Add(inpList[i] * factor);
         }
         return outList;
      }
   }

}
