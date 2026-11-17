// ***********************************************************************
// Assembly         : AFSPC.AstroStds.Models
// Author           : nguyend
// Created          : 05-19-2015
//
// Last Modified By : nguyend
// Last Modified On : 07-27-2015
// ***********************************************************************
// <copyright file="DaysSince50UTC.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using AFSPC.AstroStds.Wrappers;


/// <summary>
/// The Models namespace.
/// </summary>
namespace AFSPC.AstroStds.Models
{
   [Serializable]
   /// <summary>
   /// Class DaysSince50UTC.
   /// </summary>
   public class DaysSince50UTC
   {
      /// <summary>
      /// The DS50 UTC
      /// </summary>
      private double ds50UTC;
      /// <summary>
      /// The yr
      /// </summary>
      private int yr, month, dayOfMonth, hour, minute, dayOfYearInt;
      /// <summary>
      /// The second
      /// </summary>
      private double second, dayOfYearReal;

      /// <summary>
      /// Enum DtgFormats
      /// </summary>
      public enum DtgFormats
      {
         /// <summary>
         /// The yyy yf dd ds HHM ms s sd SSS
         /// </summary>
         YYYYfDDDsHHMMsSSdSSS,
         /// <summary>
         /// The y yf dd ds HHM ms s sd SSS
         /// </summary>
         YYfDDDsHHMMsSSdSSS,
         /// <summary>
         /// The yyyy mon DDHHMMS sd SSS
         /// </summary>
         YYYYMonDDHHMMSSdSSS,
         /// <summary>
         /// The yyy yf dd dd DDDDDDDD
         /// </summary>
         YYYYfDDDdDDDDDDDD,
         /// <summary>
         /// The yyyydddhhmms sd SSS
         /// </summary>
         YYYYDDDHHMMSSdSSS,
         /// <summary>
         /// The yydddhhmms sd SSS
         /// </summary>
         YYDDDHHMMSSdSSS,
         /// <summary>
         /// The y ys dd ds h hs m ms s sd SSSSSSS
         /// </summary>
         YYsDDDsHHsMMsSSdSSSSSSS,
         /// <summary>
         /// The y ys dd ds h hs m ms s sd SSS
         /// </summary>
         YYsDDDsHHsMMsSSdSSS
      }

      public DaysSince50UTC()
      {

      }   

      /// <summary>
      /// Initializes a new instance of the <see cref="DaysSince50UTC"/> class.
      /// </summary>
      /// <param name="ds50UTC">The DS50 UTC.</param>
      public DaysSince50UTC(double ds50UTC)
      {
         this.Ds50UTC = ds50UTC;
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="DaysSince50UTC"/> class.
      /// </summary>
      /// <param name="dtgStr">The DTG string.</param>
      public DaysSince50UTC(string dtgStr)
      {
         this.Ds50UTC = TimeFuncWrapper.DTGToUTCExt(dtgStr);
      }


      /// <summary>
      /// Currs the date time.
      /// </summary>
      /// <returns>DaysSince50UTC.</returns>
      public static DaysSince50UTC CurrDateTime()
      {
         DateTime now = DateTime.Now;

         return TimeFuncWrapper.TimeComps1ToUTC(now.Year, now.DayOfYear, now.Hour, now.Minute, now.TimeOfDay.TotalSeconds % 60.0);
      }

      /// <summary>
      /// Dates the time to DS50 UTC.
      /// </summary>
      /// <param name="dateTime">The date time.</param>
      /// <returns>DaysSince50UTC.</returns>
      public static DaysSince50UTC DateTimeToDs50UTC(DateTime dateTime)
      {
         return TimeFuncWrapper.TimeComps1ToUTC(dateTime.Year, dateTime.DayOfYear, dateTime.Hour, dateTime.Minute, dateTime.TimeOfDay.TotalSeconds % 60.0);
      }

      /// <summary>
      /// Dates the time to DS50 UTC.
      /// </summary>
      /// <param name="dateTimeString">The date time string.</param>
      /// <returns>DaysSince50UTC.</returns>
      public static DaysSince50UTC DateTimeToDs50UTC(string dateTimeString)
      {
         DateTime dateTime = Convert.ToDateTime(dateTimeString);
         return TimeFuncWrapper.TimeComps1ToUTC(dateTime.Year, dateTime.DayOfYear, dateTime.Hour, dateTime.Minute, dateTime.TimeOfDay.TotalSeconds % 60.0);
      }

      /// <summary>
      /// Constructor that takes time components
      /// </summary>
      /// <param name="year">2 or 4 digit year (1956-2049)</param>
      /// <param name="dayOfYear">julian day of year</param>
      /// <param name="hours">hour of day (0-23)</param>
      /// <param name="minutes">minute (0-59)</param>
      /// <param name="seconds">seconds</param>
      public DaysSince50UTC(int year, int dayOfYear, int hours, int minutes, double seconds)
      {
         this.Ds50UTC = TimeFuncWrapper.TimeComps1ToUTC(year, dayOfYear, hours, minutes, seconds);
      }


      /// <summary>
      /// Constructor that takes time components
      /// </summary>
      /// <param name="year">2 or 4 digit year (1956-2049)</param>
      /// <param name="dayOfYear">julian day of year and fraction of day</param>
      public DaysSince50UTC(int year, double dayOfYear)
      {
         this.Ds50UTC = TimeFuncWrapper.YrDaysToUTC(year, dayOfYear);
      }


      /// <summary>
      /// Constructor that takes different time components
      /// </summary>
      /// <param name="year">2 or 4 digit year (1956-2049)</param>
      /// <param name="mon">1-January, ..., 12-November</param>
      /// <param name="day">valid day of month</param>
      /// <param name="hours">hour of day (0-23)</param>
      /// <param name="minutes">minute (0-59)</param>
      /// <param name="seconds">seconds</param>
      public DaysSince50UTC(int year, int mon, int day, int hours, int minutes, double seconds)
      {
         this.Ds50UTC = TimeFuncWrapper.TimeComps2ToUTC(year, mon, day, hours, minutes, seconds);
      }



      /// <summary>
      /// days since 1950
      /// </summary>
      /// <value>The DS50 UTC.</value>
      public double Ds50UTC
      {
         get
         {
            return this.ds50UTC;
         }

         set
         {
            this.ds50UTC = value;

            TimeFuncWrapper.UTCToTimeComps2(this.ds50UTC, out this.yr, out this.month, out this.dayOfMonth, out this.hour, out this.minute, out this.second);
            TimeFuncWrapper.UTCToTimeComps1(this.ds50UTC, out this.yr, out this.dayOfYearInt, out this.hour, out this.minute, out this.second);
            this.dayOfYearReal = this.dayOfYearInt + (this.hour / Constants.HRSPERDAY) + (this.minute / Constants.MINSPERDAY) + (this.second / Constants.SECSPERDAY);

         }
      }

      /// <summary>
      /// Gets the year component of the date represented by this instance.
      /// </summary>
      /// <value>The year.</value>
      public int Year
      {
         get { return this.yr; }
      }


      /// <summary>
      /// Gets the day of the year (in integer) represented by this instance.
      /// </summary>
      /// <value>The day of year int.</value>
      public int DayOfYearInt
      {
         get { return this.dayOfYearInt; }
      }


      /// <summary>
      /// Gets the day of the year (in real) represented by this instance.
      /// </summary>
      /// <value>The day of year real.</value>
      public double DayOfYearReal
      {
         get { return this.dayOfYearReal; }
      }

      /// <summary>
      /// Gets the month component of the date represented by this instance.
      /// </summary>
      /// <value>The month.</value>
      public int Month
      {
         get { return this.month; }
      }


      /// <summary>
      /// Gets the day of the month represented by this instance.
      /// </summary>
      /// <value>The day of month.</value>
      public int DayOfMonth
      {
         get { return this.dayOfMonth; }
      }


      /// <summary>
      /// Gets/Sets the hour component of the date represented by this instance.
      /// </summary>
      /// <value>The hour.</value>
      public int Hour
      {
         get { return this.hour; }
         set
         {
            if (value < 0 || value > 24)
               throw new ArgumentOutOfRangeException();
            this.Ds50UTC = TimeFuncWrapper.TimeComps1ToUTC(this.yr, this.dayOfYearInt, value, this.minute, this.second);
         }
      }


      /// <summary>
      /// Gets/Sets the minute component of the date represented by this instance.
      /// </summary>
      /// <value>The minute.</value>
      public int Minute
      {
         get { return this.minute; }
         set
         {
            if (value < 0 || value > 60)
               throw new ArgumentOutOfRangeException();
            this.Ds50UTC = TimeFuncWrapper.TimeComps1ToUTC(this.yr, this.dayOfYearInt, this.hour, value, this.second);
         }
      }


      /// <summary>
      /// Gets/Sets the second (in real) component of the date represented by this instance.
      /// </summary>
      /// <value>The second.</value>
      public double Second
      {
         get { return this.second; }
         set
         {
            if (value < 0 || value > 60)
               throw new ArgumentOutOfRangeException();
            this.Ds50UTC = TimeFuncWrapper.TimeComps1ToUTC(this.yr, this.dayOfYearInt, this.hour, this.minute, value);
         }
      }

      /// <summary>
      /// Adds hours to the current days since 1950 UTC
      /// </summary>
      /// <param name="hours">The hours.</param>
      public void AddHours(double hours)
      {
         this.Ds50UTC += (hours / Constants.HRSPERDAY);
      }

      /// <summary>
      /// Adds minutes to the current days since 1950 UTC
      /// </summary>
      /// <param name="minutes">The minutes.</param>
      public void AddMinutes(double minutes)
      {
         this.Ds50UTC += (minutes / Constants.MINSPERDAY);
      }

      /// <summary>
      /// Adds seconds to the current days since 1950 UTC
      /// </summary>
      /// <param name="seconds">The seconds.</param>
      public void AddSeconds(double seconds)
      {
         this.Ds50UTC += (seconds / Constants.SECSPERDAY);
      }

      // Operator Overloading

      /// <summary>
      /// Adds days to a DaysSince50UTC instance
      /// </summary>
      /// <param name="d1">The d1.</param>
      /// <param name="d2">The d2.</param>
      /// <returns>The result of the operator.</returns>
      public static DaysSince50UTC operator +(DaysSince50UTC d1, double d2)
      {
         return new DaysSince50UTC(d1.Ds50UTC + d2);
      }

      /// <summary>
      /// Subtracts days from a DaysSince50UTC instance
      /// </summary>
      /// <param name="d1">The d1.</param>
      /// <param name="d2">The d2.</param>
      /// <returns>The result of the operator.</returns>
      public static DaysSince50UTC operator -(DaysSince50UTC d1, double d2)
      {
         return new DaysSince50UTC(d1.Ds50UTC - d2);
      }

      /// <summary>
      /// Implicit conversion from a DaysSince50UTC instance to a double
      /// </summary>
      /// <param name="instance">The instance.</param>
      /// <returns>The result of the conversion.</returns>
      public static implicit operator Double(DaysSince50UTC instance)
      {
         return instance.Ds50UTC;
      }

      /// <summary>
      /// Implicit conversion from a double to DaysSince50UTC instance
      /// </summary>
      /// <param name="ds50UTC">The DS50 UTC.</param>
      /// <returns>The result of the conversion.</returns>
      public static implicit operator DaysSince50UTC(double ds50UTC)
      {
         return new DaysSince50UTC(ds50UTC);
      }


      /// <summary>
      /// Returns a DateTime instance from a DaysSince50UTC instance
      /// </summary>
      /// <returns>DateTime.</returns>
      public DateTime ToDateTime()
      {
         return TimeFuncWrapper.Ds50UTCToDateTime(this.Ds50UTC);
      }

      /// <summary>
      /// Converts days since 1950 UTC value to a DateTime instance
      /// </summary>
      /// <param name="ds50UTC">The DS50 UTC.</param>
      /// <returns>DateTime.</returns>
      public static DateTime ToDateTime(double ds50UTC)
      {
         return TimeFuncWrapper.Ds50UTCToDateTime(ds50UTC);
      }

      /// <summary>
      /// Returns days since 1950 ET
      /// </summary>
      /// <value>The et.</value>
      public double ET
      {
         get { return TimeFuncWrapper.UTCToET(this.Ds50UTC); }
      }


      /// <summary>
      /// Returns days since 1950 TAI
      /// </summary>
      /// <value>The tai.</value>
      public double TAI
      {
         get { return TimeFuncWrapper.UTCToTAI(this.Ds50UTC); }
      }


      /// <summary>
      /// Returns days since 1950 UT1
      /// </summary>
      /// <value>The u t1.</value>
      public double UT1
      {
         get { return TimeFuncWrapper.UTCToUT1(this.Ds50UTC); }
      }

      // YYYY/DDD HHMM SS.SSS
      /// <summary>
      /// Returns YYYY/DDD HHMM SS.SSS string
      /// </summary>
      /// <value>The DTG20 string.</value>
      public string Dtg20Str
      {
         get { return TimeFuncWrapper.UTCToDtg20Str(this.Ds50UTC); }
      }

      // YY/DDD HHMM SS.SSS
      /// <summary>
      /// Returns YY/DDD HHMM SS.SSS string
      /// </summary>
      /// <value>The DTG18 string.</value>
      public string Dtg18Str
      {
         get { return TimeFuncWrapper.UTCToDtg18Str(this.Ds50UTC); }
      }

      // YYYYMonDDHHMMSS.SSS
      /// <summary>
      /// Returns YYYYMonDDHHMMSS.SSS string
      /// </summary>
      /// <value>The DTG19 string.</value>
      public string Dtg19Str
      {
         get { return TimeFuncWrapper.UTCToDtg19Str(this.Ds50UTC); }
      }

      // YYYY/DDD.DDDDDDDD
      /// <summary>
      /// Returns YYYY/DDD.DDDDDDDD string
      /// </summary>
      /// <value>The DTG17 string.</value>
      public string Dtg17Str
      {
         get { return TimeFuncWrapper.UTCToDtg17Str(this.Ds50UTC); }
      }

      // YYYYDDDHHMMSS.SSS
      /// <summary>
      /// Returns YYYYDDDHHMMSS.SSS
      /// </summary>
      /// <value>The dtg17a string.</value>
      public string Dtg17aStr
      {
         get { return TimeFuncWrapper.UTCToDtg17aStr(this.Ds50UTC); }
      }

      // YYDDDHHMMSS.SSS
      /// <summary>
      /// Returns YYDDDHHMMSS.SSS string
      /// </summary>
      /// <value>The DTG15 string.</value>
      public string Dtg15Str
      {
         get { return TimeFuncWrapper.UTCToDtg15Str(this.Ds50UTC); }
      }

      // YY DDD HH MM SS.SSSSSSS
      /// <summary>
      /// Returns YY DDD HH MM SS.SSSSSSS string
      /// </summary>
      /// <value>The DTG23 string.</value>
      public string Dtg23Str
      {
         get { return TimeFuncWrapper.UTCToDtg23Str(this.Ds50UTC); }
      }

      // CONVERT DAYS SINCE 1950 TO 'YY DDD HH MM SS.SSS'
      /// <summary>
      /// Returns YY DDD HH MM SS.SSS string
      /// </summary>
      /// <value>The dtg19a string.</value>
      public string Dtg19aStr
      {
         get { return TimeFuncWrapper.UTCToDtg19aStr(this.Ds50UTC); }
      }


      // YYYY DDD HH MM SS.SSSSSSS
      /// <summary>
      /// Returns YYYY DDD HH MM SS.SSSSSSS string
      /// </summary>
      /// <value>The Dtg25 string.</value>

      public string Dtg25Str
      {
         get { return string.Format("{0:D2}", (int)this.Year / 100) + this.Dtg23Str; }
      }

      /// <summary>
      /// Returns theta Greenwich angle (in radians) at the UTC time
      /// </summary>
      /// <value>The theta greenwich.</value>
      public double ThetaGreenwich
      {
         get { return TimeFuncWrapper.ThetaGrnwchFK5(this.UT1); }
      }

      /// <summary>
      /// To the DTG string.
      /// </summary>
      /// <param name="dtg">The DTG.</param>
      /// <returns>System.String.</returns>
      public string ToDtgString(DtgFormats dtg)
      {
         return null;
      }

      /// <summary>
      /// Returns string in this format "Tue Feb 22 09:04:00 UTC 2000"
      /// </summary>
      /// <returns>System.String.</returns>
      public string ToDTGForm1()
      {
         return string.Format("{0:ddd MMM dd HH:mm:ss UTC yyyy}", this.ToDateTime());
      }

      /// <summary>
      /// Returns string in this format "YYYY-mm-DDTHH:MM:SS.SSSZ"
      /// </summary>
      /// <returns>System.String.</returns>
      public string ToDTGForm2()
      {
         return string.Format("{0:yyyy-MM-ddTHH:mm}:{1:00.000}Z", this.ToDateTime(), this.Second);
      }

      public static DaysSince50UTC FrJulianDate(double jd)
      {
         // -----------------find year and days of the year ---------------
         double temp = jd - 2415019.5;
         double tu = temp / 365.25;
         double year = 1900.0 + Math.Floor(tu);
         double leapyrs = Math.Floor((year - 1901) * 0.25);
         // days = temp - ((year - 1900) * 365.0 + leapyrs) + 0.00000000001; % nudge by 8.64x10 - 7 sec to get even outputs
         double days = temp - ((year - 1900) * 365.0 + leapyrs);

         // ------------check for case of beginning of a year------------ -
         if (days < 1.0)
         {
            year = year - 1;
            leapyrs = Math.Floor((year - 1901) * 0.25);
            days = temp - ((year - 1900) * 365.0 + leapyrs);
         }

         // -------------------find remaining data  -----------------------
         double ds50UTC = TimeFuncWrapper.YrDaysToUTC((int)year, days);
         return new DaysSince50UTC(ds50UTC);
      }


   }
}
