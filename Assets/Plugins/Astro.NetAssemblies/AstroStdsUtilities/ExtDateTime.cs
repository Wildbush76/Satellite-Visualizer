using System;

namespace AFSPC.AstroStds.Utilities
{
   /// <summary>
   /// Extension to .NET DateTime which adds support for julian dates  
   /// </summary>
   public static class ExtDateTime
   {

      /// <summary>
      /// This function returns a julian date in Months (1-12) and Days(1-31) 
      /// </summary>
      /// <param name="year">input year</param>
      /// <param name="julDays">input julian days</param>
      /// <param name="rMonth">the resulting months</param>
      /// <param name="rDays">the resulting days</param>
      public static void JulToMMDD(int year, int julDays, out int rMonth, out int rDays )
      {
         int month;

         if(DateTime.IsLeapYear(year))
         {
            if(julDays <= 0 || julDays >= 367) //366 = 12/31 00:00:00
               throw new System.ArgumentOutOfRangeException("julDays", julDays, 
                  "Julian days out of range (1-366)");
         }
         else
         {
            if(julDays <= 0 || julDays >= 366) //365 = 12/31 00:00:00
               throw new System.ArgumentOutOfRangeException("julDays", julDays, 
                  "Julian days out of range (1-365)");
         }

         for(month = 1; month <= 12; month++)
         {
            if(julDays <= DateTime.DaysInMonth(year, month))
               break;
            else
               julDays -= DateTime.DaysInMonth(year, month);
         }

         rMonth = month;
         rDays = julDays;
      }

      /// <summary>
      /// This function converts fraction of day into HH MM SS.SSS
      /// </summary>
      /// <param name="dayFrac">input fraction of day</param>
      /// <param name="hh">the resulting hours</param>
      /// <param name="mm">the resulting minutes</param>
      /// <param name="ss">the resulting seconds</param>
      /// <param name="ms">the resulting milliseconds</param>
      public static void DayFracToTime(double dayFrac, 
         out int hh, out int mm, out int ss, out int ms)
      {
         hh = (int) Math.Floor(dayFrac * 24);
         mm = (int) Math.Floor((dayFrac * 1440) % 60);
         double temp = (dayFrac * 86400 % 60);
         ss = (int) Math.Floor (temp);
         ms = (int) Math.Round((temp - ss) * 1000);
         if (ms == 1000) //won't allow ms to reach 1000
            ms = 999;
      }

      /// <summary>
      /// This function works just like DateTime.ToString() mehod but adding several more custom format options:
      /// J : whole julian date - three digit.
      /// j : display day fraction in one digit, this can be repeated, each occurence
      /// will be replace with one digit
      /// </summary>
      /// <param name="datetime">input datetime</param>
      /// <param name="format">datetime format string (allow the extension of J and j)</param>
      /// <returns>the resulting date time string</returns>
      public static string ToString(DateTime datetime, string format)
      {
         string newFormatStr = format;
         string replaceStr;

         //found J pattern - replace it with whole julian date
         if(format.IndexOf('J') >= 0 ) 
         {
            replaceStr = datetime.DayOfYear.ToString("000");
            newFormatStr = newFormatStr.Replace("J", replaceStr);
         }

         //found j pattern - replace it with fraction of a day
         if(newFormatStr.IndexOf('j') >= 0 )
         {
            int idx = newFormatStr.IndexOf('j') + 1;
            string newFormat = "0.0";
            string oldStr = "j";

            for(; idx < newFormatStr.Length && newFormatStr[idx] == 'j' ; idx++)
            {
               oldStr += "j";
               newFormat += "0";
            }

            double dayFrac = datetime.TimeOfDay.TotalDays; 

            replaceStr = dayFrac.ToString(newFormat).Remove(0, 2);
            newFormatStr = newFormatStr.Replace(oldStr, replaceStr);
         }
         return datetime.ToString(newFormatStr);
      }


      /// <summary>
      /// Creates a new DataTime object by parsing data from an input string following specified format.
      /// Allow the use of JJJ for julian days and j for portion of a julian day
      /// </summary>
      /// <param name="strValue">an input string</param>
      /// <param name="strFormat">date format of the input string</param>
      /// <returns>DateTime</returns>
      public static DateTime Parse(string strValue, string strFormat)
      {
         int    year, month, day, hour, minute, julian, second, millisecond;
         int    idx, count, startIdx;

         year = month = day = hour = minute = julian = second = millisecond = 0;


         if (strFormat.IndexOf('y') >= 0)
         {
            startIdx = strFormat.IndexOf('y');
            for (idx = startIdx + 1, count = 1;
               idx < strFormat.Length && strFormat[idx] == 'y';
               idx++, count++) ;

            year = Int32.Parse(strValue.Substring(startIdx, count));

            if (count == 2)
            {
               if (year >= 57 && year <= 99)
                  year += 1900;
               else
                  year += 2000;
            }
         }
         else
         {
            return new DateTime();
         }


         //Either julian day or MMdd needs to be entered, otherwise
         //return a null DateTime object
         if(strFormat.IndexOf("JJJ") >= 0)
         {
            startIdx = strFormat.IndexOf('J');
            julian = 1;
            if (strValue.Length >= startIdx + 3)
               julian = Int32.Parse(strValue.Substring(startIdx, 3));
            try
            {
               JulToMMDD(year, julian, out month, out day);
            }
            catch (ArgumentOutOfRangeException)
            {
               return new DateTime();
            }
         }
         else if (strFormat.IndexOf("MM") >= 0)
         {
            startIdx = strFormat.IndexOf("MM");
            month = 1;
            if (strValue.Length >= startIdx + 2)
               month = Int32.Parse(strValue.Substring(startIdx, 2));

            if (strFormat.IndexOf("dd") >= 0)
            {
               startIdx = strFormat.IndexOf("dd");
               day = 1;
               if (strValue.Length >= startIdx + 2)
                  day = Int32.Parse(strValue.Substring(startIdx, 2));
            }
            else
            {
               return new DateTime();
            }
         }
         else
         {
            return new DateTime();
         }

         if(strFormat.IndexOf('j') >= 0)
         {
            startIdx = strFormat.IndexOf('j');
            for(idx = startIdx + 1, count = 1;
               idx < strFormat.Length && strFormat[idx]=='j';
               idx++, count++);

            double dayFraction = 0;
            if (strValue.Length >= startIdx + count)
               dayFraction = double.Parse("0." + strValue.Substring(startIdx, count));
            DayFracToTime(dayFraction, out hour, out minute, out second, out millisecond);
         }
         else if(strFormat.IndexOf("hh") >=0 )
         {
            startIdx = strFormat.IndexOf("hh");
            hour = 0;
            if (strValue.Length >= startIdx + 2)
               hour = Int32.Parse(strValue.Substring(startIdx, 2));
            if(strFormat.IndexOf("mm") >=0 )
            {
               startIdx = strFormat.IndexOf("mm");
               minute = 0;
               if (strValue.Length >= startIdx + 2)
                  minute = Int32.Parse(strValue.Substring(startIdx, 2));
            }
            if(strFormat.IndexOf("ss") >=0 )
            {
               startIdx = strFormat.IndexOf("ss");
               second = 0;
               if (strValue.Length >= startIdx + 2)
                  second = Int32.Parse(strValue.Substring(startIdx, 2));

            }
            if(strFormat.IndexOf('f') >=0 )
            {
               startIdx = strFormat.IndexOf('f');
               for(idx = startIdx + 1, count = 1;
                  idx < strFormat.Length && strFormat[idx]=='f';
                  idx++, count++);
               double secFrac = 0;
               if (strValue.Length >= startIdx + count)
                  secFrac = double.Parse("0." + strValue.Substring(startIdx, count));
               millisecond = (int) Math.Ceiling(secFrac * 1000.0);
            }
         }
         try
         {
            return new DateTime(year, month, day, hour, minute, second, millisecond);
         }
         catch (ArgumentOutOfRangeException)
         {
            return new DateTime();
         }

      }
   }
}
