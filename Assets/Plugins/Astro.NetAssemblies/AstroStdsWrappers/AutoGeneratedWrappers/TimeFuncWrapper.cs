// This wrapper file was generated automatically by the GenDllWrappers program.
using System.Runtime.InteropServices;



namespace AFSPC.AstroStds.Wrappers
{
   public static partial class TimeFuncWrapper
   {
      // Provide the path to the dll
      #if WINDOWS
      const string TimeFuncDll = "timefunc";
      #else
      const string TimeFuncDll = "libtimefunc.so";
      #endif
      // static constructor
      static TimeFuncWrapper()
      {
         // Set AS library default path/LD_LIBRARY_PATH environment
         Utilities.Utility.Init();
      }



      /// <summary>
      /// Notes: This function has been deprecated since v9.0. 
      /// Initializes the TimeFunc DLL for use in the program. 
      /// If this function returns an error, it is recommended that you stop the program immediately.
      /// 
      /// An error will occur if you forget to load and initialize all the prerequisite DLLs, as listed in the DLL Prerequisites section of the accompanying documentation, before using this DLL.
      /// </summary>
      /// <param name="apAddr">The pointer that was returned from DllMain.DllMainInit (in-Long)</param>
      /// <returns>Returns zero indicating the TimeFunc DLL has been initialized successfully. Other values indicate an error.</returns>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TimeFuncInit(long apAddr);


      /// <summary>
      /// Returns the information about the TimeFunc DLL.  The information is placed in the string parameter you pass in.
      /// The returned string provides information about the version number, build date, and the platform of the TimeFunc DLL.
      /// </summary>
      /// <param name="infoStr">A string to hold the information about TimeFunc.dll. (out-Character[128])</param>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void TimeFuncGetInfo(byte[] infoStr);


      /// <summary>
      /// Loads timing constants data from an input file.
      /// Time constants can be included directly in the main input file or they can be read from a separate file identified with "TIMFIL=[pathname\filename]".
      /// 
      /// The input file is read in two passes. The function first looks for "TIMFIL=" lines, then it looks for timing constant data which was included directly. The result of this is that data entered using both methods will be processed, but the "TIMFIL=" data will be processed first.
      /// 
      /// The time constants are also read in from each VCM. However, only the most recent time constants among VCMs are stored in the memory, see VCM.dll documentation.
      /// See the "Time Constants Data Description" section in the accompanying TimeFunc documentation file for supported formats.
      /// </summary>
      /// <param name="tconFile">The name of the file from which to read timing constants data. (in-Character[512])</param>
      /// <returns>0 if the input file has been loaded successfully, non-0 if error.</returns>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TConLoadFile(string tconFile);


      /// <summary>
      /// Loads timing constants data and prediction control (6P-card) from an input file.
      /// Time constants can be included directly in the main input file or they can be read from a separate file identified with "TIMFIL=[pathname\filename]".
      /// 
      /// The input file is read in two passes. The function first looks for "TIMFIL=" lines, then it looks for timing constant data which was included directly. The result of this is that data entered using both methods will be processed, but the "TIMFIL=" data will be processed first.
      /// 
      /// The time constants are also read in from each VCM. However, only the most recent time constants among VCMs are stored in the memory, see VCM.dll documentation.
      /// </summary>
      /// <param name="tconFile">The name of the file from which to read timing constants data and/or prediction control data. (in-Character[512])</param>
      /// <returns>0 if the input file is loaded successfully, non-0 if there is an error.</returns>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TimeFuncLoadFile(string tconFile);


      /// <summary>
      /// Checks to see if timing constants have been loaded into memory.
      /// The timing constants can be loaded from a timing constants file or from VCM(s).  See TConLoadFile, TConAddOne, and TConAddARec functions.
      /// </summary>
      /// <returns>1 if timing constants data have been loaded, 0 if not.</returns>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int IsTConFileLoaded();


      /// <summary>
      /// Saves currently loaded timing constants data to a file.
      /// The data will be saved in the format specified by the form parameter, regardless of the format or method originally used to load it.
      /// </summary>
      /// <param name="tconFile">The name of the file in which to save the timing constants data. (in-Character[512])</param>
      /// <param name="saveMode">Specifies whether to create a new file or append to an existing one. (0 = Create new file, 1= Append to existing file) (in-Integer)</param>
      /// <param name="saveForm">Specifies the format in which to save the file. (0 = SPECTER Print Record format, 1 = XML format (future implementation)) (in-Integer)</param>
      /// <returns>0 if the data is successfully saved to the file, non-0 if there is an error.</returns>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TConSaveFile(string tconFile, int saveMode, int saveForm);


      /// <summary>
      /// Adds a timing constant record to memory. Note that this function is solely for backward compatible with legacy software.
      /// Notes: only the latest timing record is stored in memory using this method. Input timing record will be skipped/ignored if it's earlier than the existing one
      /// The users should use TConLoadFile or TimeFuncLoadFile to load timing constants file instead.
      /// </summary>
      /// <param name="refDs50UTC">Reference time (days since 1950, UTC) (in-Double)</param>
      /// <param name="leapDs50UTC">Leap Second time (days since 1950, UTC) (in-Double)</param>
      /// <param name="taiMinusUTC">TAI minus UTC offset at the reference time (seconds) (in-Double)</param>
      /// <param name="ut1MinusUTC">UT1 minus UTC offset at the reference time (seconds) (in-Double)</param>
      /// <param name="ut1Rate">UT1 rate of change versus UTC at the reference time (msec/day) (in-Double)</param>
      /// <param name="polarX">Polar wander (X direction) at the reference time (arc-seconds) (in-Double)</param>
      /// <param name="polarY">Polar wander (Y direction) at the reference time (arc-seconds) (in-Double)</param>
      /// <returns>0 if the timing constants record is successfully added to memory, non-0 if there is an error.</returns>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TConAddARec(double refDs50UTC, double leapDs50UTC, double taiMinusUTC, double ut1MinusUTC, double ut1Rate, double polarX, double polarY);


      /// <summary>
      /// Adds one timing constant record to memory. This API can be used to avoid TConLoadFile's file I/O
      /// </summary>
      /// <param name="refDs50UTC">Reference time (days since 1950, UTC) (in-Double)</param>
      /// <param name="taiMinusUTC">TAI minus UTC offset at the reference time (seconds) (in-Double)</param>
      /// <param name="ut1MinusUTC">UT1 minus UTC offset at the reference time (seconds) (in-Double)</param>
      /// <param name="ut1Rate">UT1 rate of change versus UTC at the reference time (msec/day) (in-Double)</param>
      /// <param name="polarX">Polar wander (X direction) at the reference time (arc-seconds) (in-Double)</param>
      /// <param name="polarY">Polar wander (Y direction) at the reference time (arc-seconds) (in-Double)</param>
      /// <returns>0 if the timing constants record is successfully added to memory, non-0 if there is an error.</returns>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TConAddOne(double refDs50UTC, double taiMinusUTC, double ut1MinusUTC, double ut1Rate, double polarX, double polarY);


      /// <summary>
      /// Retrieves the timing constants record, if exists, at the requested input time in ds50UTC.
      /// If the requested record is not found, 0's will be returned for all of the constants. You can use this fact to determine whether the record was found or not. Simply check the taiMinusUTC value after calling this function. Since that value can never be 0 for a valid record, if it is 0 the record was not found.
      /// </summary>
      /// <param name="ds50UTC">Input days since 1950, UTC (in-Double)</param>
      /// <param name="taiMinusUTC">Returned TAI minus UTC offset at requested time (seconds) (out-Double)</param>
      /// <param name="ut1MinusUTC">Returned UT1 minus UTC offset at requested time (seconds) (out-Double)</param>
      /// <param name="ut1Rate">Returned UT1 rate of change versus UTC at Reference time (msec/day) (out-Double)</param>
      /// <param name="polarX">Returned interpolated polar wander (X direction) at requested time (arc-seconds) (out-Double)</param>
      /// <param name="polarY">Returned interpolated polar wander (Y direction) at requested time (arc-seconds) (out-Double)</param>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void UTCToTConRec(double ds50UTC, out double taiMinusUTC, out double ut1MinusUTC, out double ut1Rate, out double polarX, out double polarY);


      /// <summary>
      /// Removes all the timing constants records in memory.
      /// </summary>
      /// <returns>0 if all timing constants records are successfully removed from memory, non-0 if there is an error.</returns>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TConRemoveAll();


      /// <summary>
      /// Converts an internal time in ds50UTC to a string in DTG20 format. The resulting string takes the form "YYYY/DDD HHMM SS.SSS".
      /// The input ds50UTC must be greater than 2192.0, which corresponds to a time later than 1st Jan 1956. Any input value less than or equal to 2192.0 will yield "1956/001 0000 00.000".
      /// </summary>
      /// <param name="ds50UTC">Days since 1950, UTC to be converted. (in-Double)</param>
      /// <param name="dtg20">A string to hold the result of the conversion. (out-Character[20])</param>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void UTCToDTG20(double ds50UTC, byte[] dtg20);


      /// <summary>
      /// Converts a time in ds50UTC to a time in DTG19 format. The resulting string takes the form "YYYYMonDDHHMMSS.SSS".
      /// See "UTCToDTG20" for an example.
      /// The input ds50UTC must be greater than 2192.0, which corresponds to a time later than 1st Jan 1956. Any input value less than or equal to 2192.0 will yield "1956Jan01000000.000".
      /// Note, the return value is in the DTG19 format "YYYYMonDDHHMMSS.SSS", not the "YY DDD HH MM SS.SSS" format.
      /// </summary>
      /// <param name="ds50UTC">Days since 1950, UTC to be converted. (in-Double)</param>
      /// <param name="dtg19">A string to hold the result of the conversion. (out-Character[19])</param>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void UTCToDTG19(double ds50UTC, byte[] dtg19);


      /// <summary>
      /// Converts a time in ds50UTC to a time in DTG17 format. The resulting string takes the form "YYYY/DDD.DDDDDDDD" format.
      /// See "UTCToDTG20" for an example.
      /// The input ds50UTC must be greater than 2192.0, which corresponds to a time later than 1st Jan 1956. Any input value less than or equal to 2192.0 will yield "1956/001.00000000".
      /// </summary>
      /// <param name="ds50UTC">Days since 1950, UTC to be converted. (in-Double)</param>
      /// <param name="dtg17">A string to hold the result of the conversion. (out-Character[17])</param>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void UTCToDTG17(double ds50UTC, byte[] dtg17);


      /// <summary>
      /// Converts a time in ds50UTC to a time in DTG15 format. The resulting string takes the form "YYDDDHHMMSS.SSS".
      /// See "UTCToDTG20" for an example.
      /// The input ds50UTC must be greater than 2192.0, which corresponds to a time later than 1st Jan 1956. Any input value less than or equal to 2192.0 will yield "56001000000.000".
      /// </summary>
      /// <param name="ds50UTC">Days since 1950, UTC to be converted. (in-Double)</param>
      /// <param name="dtg15">A string to hold the result of the conversion. (out-Character[15])</param>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void UTCToDTG15(double ds50UTC, byte[] dtg15);


      /// <summary>
      /// Converts an internal time in ds50UTC to the specified DTG format - dtgFmt 
      /// The input ds50UTC must be greater than 2192.0, which corresponds to a time later than 1st Jan 1956. Any input value less than or equal to 2192.0 will yield "1956/001 0000 00.000".
      /// </summary>
      /// <param name="ds50UTC">Days since 1950, UTC to be converted. (in-Double)</param>
      /// <param name="dtgFmt">Requested DTG format (see DTGFMT_?) (in-Integer)</param>
      /// <param name="outDtg">A string to hold the result matching the requested DTG format. (out-Character[32])</param>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void UTCToDTG(double ds50UTC, int dtgFmt, byte[] outDtg);


      /// <summary>
      /// Converts a time in one of the DTG formats to a time in ds50UTC. DTG15, DTG17, DTG19, and DTG20 formats are accepted.
      /// See "UTCToDTG20" for an example.
      /// During the conversion, this function processes only numbers and the '.' character. This means that you can format dtgStr in a format that makes sense. You can use spaces and the '/' character for example if you wish.
      /// 
      /// The function can process dates from 1950 to 2049. Any input outside this range will yield "0d0".
      /// This function supports DTG19 inputs in both "YY DDD HH MM SS.SSS" and "YYYYMonDDHHMMSS.SSS" formats.
      /// </summary>
      /// <param name="dtg">The string to convert. Can be any of the DTG formats previously documented. (in-Character[20])</param>
      /// <returns>The number of days since 1950, UTC. Partial days may be returned.</returns>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern double DTGToUTC(string dtg);


      /// <summary>
      /// An extension to DTGToUTC to support newer time formats (up to 32-character long)
      /// DD Mon YYYY HH:MM:SS.SSS
      /// YYYY/mm/DD HH:MM:SS.SSS or YYYY-mm-DDTHH:MM:SS.SSS
      /// YYYY/mm/DD HH:MM:SS.SSSSSS or YYYY-mm-DDTHH:MM:SS.SSSSSS
      /// YYYY ddd (DD Mon) HH:MM:SS.SSS
      /// YYYY-DDDTHH:MM:SS.SSSZ or YYYY ddd HH MM SS.SSS
      /// </summary>
      /// <param name="dtg">The string to convert. Can be any of the DTG formats previously documented. (in-Character[32])</param>
      /// <returns>The number of days since 1950, UTC. Partial days may be returned.</returns>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern double DTGToUTCExt(string dtg);


      /// <summary>
      /// Converts a time in ds50UTC to a time in ds50TAI using timing constants records in memory. 
      /// If no timing constants records were loaded, ds50UTC and ds50TAI are the same.
      /// Partial days may be returned.
      /// </summary>
      /// <param name="ds50UTC">Days since 1950, UTC to be converted. (in-Double)</param>
      /// <returns>The number of days since 1950, TAI. Partial days may be returned.</returns>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern double UTCToTAI(double ds50UTC);


      /// <summary>
      /// Converts a time in ds50UTC to a time in ds50UT1 using timing constants records in memory. 
      /// If no timing constants records were loaded, ds50UTC and ds50UT1 are the same. 
      /// Partial days may be returned.
      /// </summary>
      /// <param name="ds50UTC">Days since 1950, UTC to be converted. (in-Double)</param>
      /// <returns>The number of days since 1950, UT1. Partial days may be returned.</returns>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern double UTCToUT1(double ds50UTC);


      /// <summary>
      /// Converts a time in ds50UTC to a time in ds50ET (Ephemeris Time/Terrestrial Time) using timing constants records in memory. 
      /// </summary>
      /// <param name="ds50UTC">Days since 1950, UTC to be converted. (in-Double)</param>
      /// <returns>The number of days since 1950, ET (Ephemeris Time/Terrestrial Time). Partial days may be returned.</returns>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern double UTCToET(double ds50UTC);


      /// <summary>
      /// Converts a time in ds50TAI to a time in ds50UTC using timing constants records in memory. 
      /// If no timing constants records were loaded, ds50TAI and ds50UTC are the same. 
      /// Partial days may be returned.
      /// </summary>
      /// <param name="ds50TAI">Days since 1950, TAI to be converted. (in-Double)</param>
      /// <returns>The number of Days since 1950, UTC. Partial days may be returned.</returns>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern double TAIToUTC(double ds50TAI);


      /// <summary>
      /// Converts a time in ds50TAI to a time in ds50UT1 using timing constants records in memory. 
      /// If no timing constants records were loaded, ds50TAI and ds50UT1 are the same. 
      /// Partial days may be returned.
      /// </summary>
      /// <param name="ds50TAI">Days since 1950, TAI to be converted. (in-Double)</param>
      /// <returns>The number of days since 1950, UT1. Partial days may be returned.</returns>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern double TAIToUT1(double ds50TAI);


      /// <summary>
      /// Converts a year and a number of days to a time in ds50UTC. 
      /// Partial days may be returned.
      /// </summary>
      /// <param name="year">Two or four digit years are accepted. (in-Integer)</param>
      /// <param name="dayOfYear">The day of year. Partial days can be specified. (in-Double)</param>
      /// <returns>The number of days since 1950, UTC. Partial days may be returned.</returns>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern double YrDaysToUTC(int year, double dayOfYear);


      /// <summary>
      /// Converts a time in ds50UTC to a year and day of year.
      /// The input ds50UTC must be greater than 2192.0, which corresponds to a time later than 1st Jan 1956. Any input value less than or equal to 2192.0 will yield Year=1956, Day=1.0.
      /// </summary>
      /// <param name="ds50UTC">Days since 1950, UTC to be converted. (in-Double)</param>
      /// <param name="year">A reference to a variable in which to place the 4-digit year. (out-Integer)</param>
      /// <param name="dayOfYear">A reference to a variable in which to place the day of year. Partial days may be expressed in this variable. (out-Double)</param>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void UTCToYrDays(double ds50UTC, out int year, out double dayOfYear);


      /// <summary>
      /// Converts a set of time components (year, day of year, hour, minute, second) to a time in ds50UTC. 
      /// Partial days may be returned.
      /// See "TimeComps2ToUTC" for a function which takes a month and day instead of a day of year value.
      /// </summary>
      /// <param name="year">Two or four digit years are accepted. (in-Integer)</param>
      /// <param name="dayOfYear">The day of year, expressed as a whole number. (in-Integer)</param>
      /// <param name="hh">The hour. (in-Integer)</param>
      /// <param name="mm">The minute. (in-Integer)</param>
      /// <param name="sss">The second, including partial seconds if desired. (in-Double)</param>
      /// <returns>The number of Days since 1950, UTC. Partial days may be returned.</returns>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern double TimeComps1ToUTC(int year, int dayOfYear, int hh, int mm, double sss);


      /// <summary>
      /// Converts a time in ds50UTC to its individual components (year, day of year, hour, minute, second).
      /// See "TimeComps1ToUTC" for an example.
      /// The input ds50UTC must be greater than 2192.0, which corresponds to a time later than 1st Jan 1956. Any input value less than or equal to 2192.0 will be reset to that value.
      /// </summary>
      /// <param name="ds50UTC">Days since 1950, UTC to be converted. (in-Double)</param>
      /// <param name="year">A reference to a variable in which to store the 4-digit year. (out-Integer)</param>
      /// <param name="dayOfYear">A reference to a variable in which to store the day of year. (out-Integer)</param>
      /// <param name="hh">A reference to a variable in which to store the hour. (out-Integer)</param>
      /// <param name="mm">A reference to a variable in which to store the minute. (out-Integer)</param>
      /// <param name="sss">A reference to a variable in which to store the second. Partial seconds may be expressed if necessary. (out-Double)</param>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void UTCToTimeComps1(double ds50UTC, out int year, out int dayOfYear, out int hh, out int mm, out double sss);


      /// <summary>
      /// Converts a set of time components (year, month, day of month, hour, minute, second) to a time in ds50UTC. 
      /// Partial days may be returned.
      /// See "TimeComps1ToUTC" for a function which takes a day of year value instead of a month and day.
      /// </summary>
      /// <param name="year">Two or four digit years are accepted. (in-Integer)</param>
      /// <param name="mon">The month. (in-Integer)</param>
      /// <param name="dayOfMonth">The day of the month. (in-Integer)</param>
      /// <param name="hh">The hour. (in-Integer)</param>
      /// <param name="mm">The minute. (in-Integer)</param>
      /// <param name="sss">The second. (in-Double)</param>
      /// <returns>The number of Days since 1950, UTC. Partial days may be returned.</returns>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern double TimeComps2ToUTC(int year, int mon, int dayOfMonth, int hh, int mm, double sss);


      /// <summary>
      /// Converts a time in ds50UTC to its individual components (year, month, day of month, hour, minute, second).
      /// See "TimeComps1ToUTC" for an example.
      /// The input ds50UTC must be greater than 2192.0, which corresponds to a time later than 1st Jan 1956. Any input value less than or equal to 2192.0 will be reset to that value.
      /// </summary>
      /// <param name="ds50UTC">Days since 1950, UTC to be converted. (in-Double)</param>
      /// <param name="year">A reference to a variable in which to store the 4-digit year. (out-Integer)</param>
      /// <param name="month">A reference to a variable in which to store the month. (out-Integer)</param>
      /// <param name="dayOfMonth">A reference to a variable in which to store the day of the month. (out-Integer)</param>
      /// <param name="hh">A reference to a variable in which to store the hour. (out-Integer)</param>
      /// <param name="mm">A reference to a variable in which to store the minute. (out-Integer)</param>
      /// <param name="sss">A reference to a variable in which to store the second. Partial seconds may be expressed if necessary. (out-Double)</param>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void UTCToTimeComps2(double ds50UTC, out int year, out int month, out int dayOfMonth, out int hh, out int mm, out double sss);


      /// <summary>
      /// Computes right ascension of Greenwich at the specified time in ds50UT1. 
      /// The Fk constants as you currently have them set up in EnvConst.dll are used.
      /// EnvConst.dll is not marked as a direct dependency of TimeFunc.dll. However, it obviously must be loaded in order to be able to use this function since you must first obtain a handle via the EnvGetFkPtr() function.
      /// </summary>
      /// <param name="ds50UT1">Input days since 1950, UT1. (in-Double)</param>
      /// <param name="lenvFk">A handle to the FK data. Use the value returned from EnvGetFkPtr(), located in EnvConst.dll. (in-Long)</param>
      /// <returns>Right ascension of Greenwich in radians at the specified time.</returns>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern double ThetaGrnwch(double ds50UT1, long lenvFk);


      /// <summary>
      /// Computes right ascension of Greenwich at the specified time in ds50UT1 using the Fourth Fundamental Catalogue (FK4).
      /// There is no need to load or initialize EnvConst.dll when computing right ascension using this function.
      /// </summary>
      /// <param name="ds50UT1">Days since 1950, UT1. (in-Double)</param>
      /// <returns>Right ascension of Greenwich in radians at the specified time using FK4.</returns>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern double ThetaGrnwchFK4(double ds50UT1);


      /// <summary>
      /// Computes right ascension of Greenwich at the specified time in ds50UT1 using the Fifth Fundamental Catalogue (FK5).
      /// There is no need to load or initialize EnvConst.dll when computing right ascension using this function.
      /// </summary>
      /// <param name="ds50UT1">Input days since 1950, UT1. (in-Double)</param>
      /// <returns>Right ascension of Greenwich in radians at the specified time using FK5.</returns>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern double ThetaGrnwchFK5(double ds50UT1);


      /// <summary>
      /// This function is intended for future use.  No information is currently available.
      /// This function is intended for future use.  No information is currently available.
      /// </summary>
      /// <param name="funcIdx">Input function index (in-Integer)</param>
      /// <param name="frArr">Input (in-Double[*])</param>
      /// <param name="toArr">Output (out-Double[*])</param>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void TimeConvFrTo(int funcIdx, double[] frArr, double[] toArr);


      /// <summary>
      /// Returns prediction control parameters. The parameters are placed in the reference variables passed to this function.
      /// </summary>
      /// <param name="startFrEpoch">Indicates whether startTime is expressed in minutes since epoch. (startFrEpoch = 1: startTime is in minutes since epoch, startFrEpoch = 0: startTime is in days since 1950, UTC) (out-Integer)</param>
      /// <param name="stopFrEpoch">Indicates whether stopTime is expressed in minutes since epoch. (stopFrEpoch = 1: stopTime is in minutes since epoch, stopFrEpoch = 0: stopTime is in days since 1950, UTC) (out-Integer)</param>
      /// <param name="startTime">The start time. Depending on the value of startFrEpoch, start time can be expressed in minutes since epoch or days since 1950, UTC. (out-Double)</param>
      /// <param name="stopTime">The stop time. Depending on the value of stopFrEpoch, stop time can be expressed in minutes since epoch or days since 1950, UTC. (out-Double)</param>
      /// <param name="interval">The Step size (min). (out-Double)</param>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void Get6P(out int startFrEpoch, out int stopFrEpoch, out double startTime, out double stopTime, out double interval);


      /// <summary>
      /// Sets prediction control parameters.
      /// </summary>
      /// <param name="startFrEpoch">Indicates whether startTime is expressed in minutes since epoch. (startFrEpoch = 1: Value of startTime is in minutes since epoch, startFrEpoch = 0: Value of startTime is in days since 1950, UTC) (in-Integer)</param>
      /// <param name="stopFrEpoch">Indicates whether stopTime is expressed in minutes since epoch. (stopFrEpoch = 1: Value of stopTime is in minutes since epoch, stopFrEpoch = 0: Value of stopTime is in days since 1950, UTC) (in-Integer)</param>
      /// <param name="startTime">Start time. (in-Double)</param>
      /// <param name="stopTime">Stop time. (in-Double)</param>
      /// <param name="interval">Step size (min). (in-Double)</param>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void Set6P(int startFrEpoch, int stopFrEpoch, double startTime, double stopTime, double interval);


      /// <summary>
      /// Returns current prediction control parameters in form of a 6P-Card string.
      /// </summary>
      /// <param name="card6PLine">The resulting 6P-Card string. (out-Character[512])</param>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void Get6PCardLine(byte[] card6PLine);


      /// <summary>
      /// Returns the time span of the loaded timing constants - the earliest and latest of loaded timing constant records
      /// </summary>
      /// <param name="numOfRecs">Number of timing constants records loaded in memory (zero if timing constants aren't loaded) (out-Integer)</param>
      /// <param name="frTimeDs50UTC">The time, in days since 1950 UTC,  of the earliest timing constant record loaded in memory (zero if timing constants aren't loaded) (out-Double)</param>
      /// <param name="toTimeDs50UTC">The time, in days since 1950 UTC,  of the latest timing constant record loaded in memory (zero if timing constants aren't loaded) (out-Double)</param>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void TConTimeSpan(out int numOfRecs, out double frTimeDs50UTC, out double toTimeDs50UTC);


      /// <summary>
      /// Returns the current cpu time in ds50Utc
      /// </summary>
      /// <returns>Current Time in days since 1950 UTC</returns>
      [DllImport(TimeFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern double GetCurrentUTC();
      
      // Available output date time group (DTG) formats 
      public static int  
         DTGFMT_DTG15   =  1,    // YYDDDHHMMSS.SSS              (DTG15)
         DTGFMT_DTG17   =  2,    // YYYY/DDD.DDDDDDDD            (DTG17)
         DTGFMT_DTG19   =  3,    // YYYYMonDDHHMMSS.SSS          (DTG19)
         DTGFMT_DTG20   =  4,    // YYYY/DDD HHMM SS.SSS         (DTG20)
         DTGFMT_DTG21   =  5,    // YYYY ddd HH MM SS.SSS        (DTG21)
         DTGFMT_DTG22   =  6,    // YYYY-DDDTHH:MM:SS.SSSZ       (DTG22)
         DTGFMT_DTG24   =  7,    // DD Mon YYYY HH:MM:SS.SSS     (DTG24)
         DTGFMT_DTG25A  =  8,    // YYYY/mm/DD HH:MM:SS.SSSSSS   (DTG25A)
         DTGFMT_DTG25B  =  9,    // YYYY-mm-DD HH:MM:SS.SSSSSS   (DTG25B)
         DTGFMT_DTG30   = 10,    // YYYY ddd (DD Mon) HH:MM:SS.SSS  (DTG30)
         DTGFMT_DTGISO  = 11;    // ISO-8601 YYYY-MM-DDTHH:MM:SS.SSZ  (DTGISO)
      
   }
// ========================= End of auto generated code ==========================
}
