// This wrapper file was generated automatically by the GenDllWrappers program.
using System.Runtime.InteropServices;



namespace AFSPC.AstroStds.Wrappers
{
   public static partial class ExtEphemWrapper
   {
      // Provide the path to the dll
      #if WINDOWS
      const string ExtEphemDll = "extephem";
      #else
      const string ExtEphemDll = "libextephem.so";
      #endif
      // static constructor
      static ExtEphemWrapper()
      {
         // Set AS library default path/LD_LIBRARY_PATH environment
         Utilities.Utility.Init();
      }



      /// <summary>
      /// Notes: This function has been deprecated since v9.0. 
      /// Initializes ExtEphem DLL for use in the program
      /// If this function returns an error, it is recommended that the users stop the program immediately. 
      /// The error occurs if the users forget to load and initialize all the prerequisite DLLs, as listed 
      /// in the DLL Prerequisite section, before using this DLL.
      /// </summary>
      /// <param name="apAddr">The handle that was returned from DllMainInit() (in-Long)</param>
      /// <returns>0 if ExtEphem DLL is initialized successfully, non-0 if there is an error</returns>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ExtEphInit(long apAddr);


      /// <summary>
      /// Returns information about the current version of ExtEphem DLL. 
      /// The information is placed in the string parameter passed in.
      /// The returned string provides information about the version number, build date, and the platform of the ExtEphem DLL. 
      /// </summary>
      /// <param name="infoStr">A string to hold the information about ExtEphem.dll (out-Character[128])</param>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void ExtEphGetInfo(byte[] infoStr);


      /// <summary>
      /// Loads a file containing EXTEPHEM's
      /// The users can use this function repeatedly to load EXTEPHEMs from different input files. 
      /// However, only unique satKeys are stored in the binary tree. Duplicated EXTEPHEMs 
      /// (determined by same file name, satellite number + epoch) won't be stored.
      /// 
      /// EXTEPHEMs can be included directly in the main input file or they can be read from a 
      /// separate file identified with "EPHFIL =[pathname\filename]".
      /// 
      /// This function only reads EXTEPHEMs from the main input file or EXTEPHEMs from the file 
      /// identified with EPHFIL in the input file. It won't read anything else.
      /// </summary>
      /// <param name="extEphFile">The name of the file containing external ephemeris data to be loaded (in-Character[512])</param>
      /// <returns>0 if the input file is read successfully, non-0 if there is an error</returns>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ExtEphLoadFile(string extEphFile);


      /// <summary>
      /// Saves the currently loaded EXTEPHEM's to a file (EPHFIL=input file name)
      /// </summary>
      /// <param name="extEphFile">The name of the file in which to save the settings (in-Character[512])</param>
      /// <param name="saveMode">Specifies whether to create a new file or append to an existing one (0 = create, 1= append) (in-Integer)</param>
      /// <param name="saveForm">Specifies the mode in which to save the file (0 = text format, 1 = not yet implemented, reserved for future) (in-Integer)</param>
      /// <returns>0 if the data is successfully saved to the file, non-0 if there is an error</returns>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ExtEphSaveFile(string extEphFile, int saveMode, int saveForm);


      /// <summary>
      /// Removes an EXTEPHEM represented by the satKey from memory
      /// If the users enter an invalid satKey (the satKey does not exist in memory), the function will return a non-zero value indicating an error.
      /// </summary>
      /// <param name="satKey">The unique key of the satellite to be removed (in-Long)</param>
      /// <returns>0 if the satellite is removed successfully, non-0 if there is an error.</returns>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ExtEphRemoveSat(long satKey);


      /// <summary>
      /// Removes all EXTEPHEMS from memory
      /// </summary>
      /// <returns>0 if all satellites are removed successfully from memory, non-0 if there is an error</returns>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ExtEphRemoveAllSats();


      /// <summary>
      /// Returns the number of EXTEPHEM's currently loaded
      /// See ExtEphGetLoaded for example.
      /// This function is useful for dynamically allocating memory for the array that is passed to the function ExtEphGetLoaded().
      /// </summary>
      /// <returns>The number of EXTEPHEM's currently loaded</returns>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ExtEphGetCount();


      /// <summary>
      /// Retrieves all of the currently loaded satKeys. These satKeys can be used to access the external ephemeris data for the EXTEPHEM's
      /// It is recommended that ExtEphGetCount() is used to count how many satellites are currently loaded in the ExtEphem DLL's binary tree. 
      /// The users then use this number to dynamically allocate the satKeys array and pass it to this function.
      /// 
      /// If the users prefer to pass a static array to the function, ensure that it is big enough to store all the satKeys in memory.
      /// </summary>
      /// <param name="order">Specifies the order in which the satKeys should be returned: 0=ascending, 1=descending, 2=order as loaded (in-Integer)</param>
      /// <param name="satKeys">The array in which to store the satKeys (out-Long[*])</param>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void ExtEphGetLoaded(int order, long[] satKeys);


      /// <summary>
      /// Allows for an EXTEPHEM to be added to memory without using an input file. The function creates a place holder for an EXTEPHEM
      /// If the same satellite (same satNum and epochDs50UTC) was previously added to the ExtEphem DLL's binary tree, 
      /// the function will generate a new unique satKey. This is very useful when the users want to compare ephemerides 
      /// of the same satellite number and same epoch time from different sources. 
      /// </summary>
      /// <param name="satNum">Satellite number (in-Integer)</param>
      /// <param name="epochDs50UTC">Epoch time in ds50UTC (in-Double)</param>
      /// <param name="ae">Mean Earth radius (km) (in-Double)</param>
      /// <param name="ke">Earth gravitational constant (in-Double)</param>
      /// <param name="coordSys">Ephemeris coordinate system (see COORD_?) (in-Integer)</param>
      /// <returns>The satKey of the newly added EXTEPHEM on success, a negative value on error</returns>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern long ExtEphAddSat(int satNum, double epochDs50UTC, double ae, double ke, int coordSys);


      /// <summary>
      /// Allows for an EXTEPHEM to be added to memory without using an input file. The function creates a place holder for an EXTEPHEM
      /// If the same satellite (same satNum and epochDs50UTC) was previously added to the ExtEphem DLL's binary tree, 
      /// the function will generate a new unique satKey. This is very useful when the users want to compare ephemerides 
      /// of the same satellite number and same epoch time from different sources. 
      /// </summary>
      /// <param name="satNum">Satellite number (in-Integer)</param>
      /// <param name="epochDs50UTC">Epoch time in ds50UTC (in-Double)</param>
      /// <param name="ae">Mean Earth radius (km) (in-Double)</param>
      /// <param name="ke">Earth gravitational constant (in-Double)</param>
      /// <param name="coordSys">Ephemeris coordinate system (see COORD_?) (in-Integer)</param>
      /// <param name="satKey">The satKey of the newly added EXTEPHEM on success, a negative value on error (out-Long)</param>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void ExtEphAddSatML(int satNum, double epochDs50UTC, double ae, double ke, int coordSys, out long satKey);


      /// <summary>
      /// Adds an ephemeris point to the end of an EXTEPHEM's set of ephemeris points
      /// The new ephemeris point will only be added to the array if its time is greater than the times of all points already in the array. 
      /// Therefore, the array is always in sorted order (t1 < t2 <... < tn).
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="ds50UTC">Epoch time in ds50UTC (in-Double)</param>
      /// <param name="pos">Position at cuurent time (km) (in-Double[3])</param>
      /// <param name="vel">Velocity at current time (km/sec) (in-Double[3])</param>
      /// <param name="revNum">The ephemeris point revolution number (in-Integer)</param>
      /// <returns>0 if the ephemeris point is added successfully to the requested satellite, non-0 if there is an error</returns>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ExtEphAddSatEphem(long satKey, double ds50UTC, double[] pos, double[] vel, int revNum);


      /// <summary>
      /// Adds an ephemeris point (including covariance matrix) to the end of an EXTEPHEM's set of ephemeris points
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="ds50UTC">Epoch time in ds50UTC (in-Double)</param>
      /// <param name="pos">Position at cuurent time (km) (in-Double[3])</param>
      /// <param name="vel">Velocity at current time (km/sec) (in-Double[3])</param>
      /// <param name="revNum">The ephemeris point revolution number (in-Integer)</param>
      /// <param name="covUVW">The covariance matrix in vector format (21 terms in one-dimensional array) (in-Double[21])</param>
      /// <returns>0 if the ephemeris point is added successfully to the requested satellite, non-0 if there is an error</returns>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ExtEphAddSatEphemCovMtx(long satKey, double ds50UTC, double[] pos, double[] vel, int revNum, double[] covUVW);


      /// <summary>
      /// Adds an ephemeris point (including covariance matrix) to the end of an EXTEPHEM's set of ephemeris points
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="ds50UTC">Epoch time in ds50UTC (in-Double)</param>
      /// <param name="pos">Position at cuurent time (km) (in-Double[3])</param>
      /// <param name="vel">Velocity at current time (km/sec) (in-Double[3])</param>
      /// <param name="revNum">The ephemeris point revolution number (in-Integer)</param>
      /// <param name="extArr">The extra array: 1-21=lower triangle matrix, 22-128=future use (in-Double[128])</param>
      /// <returns>0 if the ephemeris point is added successfully to the requested satellite, non-0 if there is an error</returns>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ExtEphAddSatEphemExt(long satKey, double ds50UTC, double[] pos, double[] vel, int revNum, double[] extArr);


      /// <summary>
      /// Loads satellite data from an external ephemeris file (any valid external ephemeris file formats) and returns a satKey on success
      /// </summary>
      /// <param name="extEphFile">The name of the file containing external ephemeris data to be loaded (in-Character[512])</param>
      /// <returns>The satKey of the newly added EXTEPHEM on success, a zero or negative value on error</returns>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern long ExtEphAddSatFrFile(string extEphFile);


      /// <summary>
      /// Loads satellite data from an external ephemeris file (any valid external ephemeris file formats) and returns a satKey on success
      /// </summary>
      /// <param name="extEphFile">The name of the file containing external ephemeris data to be loaded (in-Character[512])</param>
      /// <param name="satKey">The satKey of the newly added EXTEPHEM on success, a zero or negative value on error (out-Long)</param>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void ExtEphAddSatFrFileML(string extEphFile, out long satKey);


      /// <summary>
      /// Checks to see if this satellite is initiazlied/loaded
      /// </summary>
      /// <param name="satKey">The satellite's unique key. This key will have been returned by one of the routines in Tle.dll. (in-Long)</param>
      /// <returns>1 if the satellite is initialized or 0 if it isn't.</returns>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ExtEphIsSatInit(long satKey);


      /// <summary>
      /// Gets number of epehemeris points associated with satKey
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="numOfPts">Number of ephemeris points (out-Integer)</param>
      /// <returns>0 if the data is successfully retrieved, non-0 if there is an error</returns>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ExtEphGetNumPts(long satKey, out int numOfPts);


      /// <summary>
      /// Retrieves all data for an EXTEPHEM with a single function call
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="satNum">Satellite number (out-Integer)</param>
      /// <param name="satName">Satellite international designator (out-Character[8])</param>
      /// <param name="recName">Record name (default to source file path, fileLoc) (out-Character[128])</param>
      /// <param name="epochDs50UTC">Satellite epoch time in ds50UTC (out-Double)</param>
      /// <param name="ae">Mean Earth radius (km) (out-Double)</param>
      /// <param name="ke">Earth gravitational constant (er**3/2 per minute) (out-Double)</param>
      /// <param name="pos">Position at epoch (km) (out-Double[3])</param>
      /// <param name="vel">Velocity at epoch (km/s) (out-Double[3])</param>
      /// <param name="coordSys">Ephemeris coordinate (see COORD_?) (out-Integer)</param>
      /// <param name="numOfPts">Number of ephemeris points (out-Integer)</param>
      /// <param name="fileLoc">File location (out-Character[512])</param>
      /// <returns>0 if the data is successfully retrieved, non-0 if there is an error</returns>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ExtEphGetAllFields(long satKey, out int satNum, byte[] satName, byte[] recName, out double epochDs50UTC, out double ae, out double ke, double[] pos, double[] vel, out int coordSys, out int numOfPts, byte[] fileLoc);


      /// <summary>
      /// Retrieves the value of a specific field of an EXTEPHEM
      /// 
      /// When using xf_ExtEph = 11, the input coordinate system is returned as an integer value.  The table below shows the coordinate system values:
      /// 
      /// table
      /// 
      /// Value
      /// Coordinate System
      /// 
      /// 1  ECI TEME of DATE
      /// 2  MEME of J2K
      /// 3  Earth Fixed Greenwich (EFG)
      /// 4  Earch Centered Rotation (ECR)
      /// 100Invalid
      ///    
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="xf_ExtEph">Predefined number specifying which field to retrieve (see XF_EXTEPH_?) (in-Integer)</param>
      /// <param name="valueStr">A string to contain the value of the requested field (out-Character[512])</param>
      /// <returns>0 if the EXTEPHEM data is successfully retrieved, non-0 if there is an error</returns>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ExtEphGetField(long satKey, int xf_ExtEph, byte[] valueStr);


      /// <summary>
      /// Updates the value of a specific field of an EXTEPHEM
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="xf_ExtEph">Predefined number specifying which field to set (see XF_EXTEPH_?) (in-Integer)</param>
      /// <param name="valueStr">The new value of the specified field, expressed as a string (in-Character[512])</param>
      /// <returns>0 if the EXTEPHEM data is successfully updated, non-0 if there is an error</returns>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ExtEphSetField(long satKey, int xf_ExtEph, string valueStr);


      /// <summary>
      /// Retrieves the times (in days since 1950 UTC) of the start and end ephemeris points of the EXTEPHEM  
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="startDs50UTC">The ephemeris start time (first ephemeris point) in days since 1950, UTC (out-Double)</param>
      /// <param name="endDs50UTC">The ephemeris end time (last ephemeris point) in days since 1950, UTC (out-Double)</param>
      /// <returns>0 if successful, non-0 if there is an error</returns>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ExtEphStartEndTime(long satKey, out double startDs50UTC, out double endDs50UTC);


      /// <summary>
      /// Retrieves the data for a specific point within an EXTEPHEM
      /// It is important to know that the array subscript starts at one (not zero).
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="index">The position number of the ephemeris point to be retrieved (1=first point) (in-Integer)</param>
      /// <param name="ds50UTC">The resulting time in ds50UTC (out-Double)</param>
      /// <param name="pos">The resulting position (km) (out-Double[3])</param>
      /// <param name="vel">The resulting velocity (km/s) (out-Double[3])</param>
      /// <param name="revNum">The resulting revolution number (out-Integer)</param>
      /// <returns>0 if the ephemeris data is successfully retrieved, non-0 if there is an error</returns>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ExtEphGetEphemeris(long satKey, int index, out double ds50UTC, double[] pos, double[] vel, out int revNum);


      /// <summary>
      /// Retrieves the data (including the covariance matrix) for a specific point within an EXTEPHEM
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="index">The position number of the ephemeris point to be retrieved (1=first point) (in-Integer)</param>
      /// <param name="ds50UTC">The resulting time in ds50UTC (out-Double)</param>
      /// <param name="pos">The resulting position (km) (out-Double[3])</param>
      /// <param name="vel">The resulting velocity (km/s) (out-Double[3])</param>
      /// <param name="revNum">The resulting revolution number (out-Integer)</param>
      /// <param name="covMtx">The 6x6 covariance matrix (out-Double[6, 6])</param>
      /// <returns>0 if the ephemeris data is successfully retrieved, non-0 if there is an error</returns>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ExtEphGetCovMtx(long satKey, int index, out double ds50UTC, double[] pos, double[] vel, out int revNum, double[,] covMtx);


      /// <summary>
      /// Interpolates the external ephemeris data to the requested time in minutes since the satellite's epoch time
      /// The coordinate system of the output position and velocity is the same as the input ephemerides.
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="mse">The requested time in minutes since the satellite's epoch time (in-Double)</param>
      /// <param name="ds50UTC">The resulting time in ds50UTC (out-Double)</param>
      /// <param name="pos">The resulting position (km) (out-Double[3])</param>
      /// <param name="vel">The resulting velocity (km/s) (out-Double[3])</param>
      /// <param name="revNum">The resulting revolution number (out-Integer)</param>
      /// <returns>0 if the external ephemeris data is successfully interpolated, non-0 if there is an error</returns>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ExtEphMse(long satKey, double mse, out double ds50UTC, double[] pos, double[] vel, out int revNum);


      /// <summary>
      /// Interpolates the external ephemeris data to the requested time in minutes since the satellite's epoch time
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="mse">The requested time in minutes since the satellite's epoch time (in-Double)</param>
      /// <param name="ds50UTC">The resulting time in ds50UTC (out-Double)</param>
      /// <param name="pos">The resulting position (km) (out-Double[3])</param>
      /// <param name="vel">The resulting velocity (km/s) (out-Double[3])</param>
      /// <param name="revNum">The resulting revolution number (out-Integer)</param>
      /// <param name="covMtx">The 6x6 covariance matrix (out-Double[6, 6])</param>
      /// <returns>0 if the external ephemeris data is successfully interpolated, non-0 if there is an error</returns>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ExtEphMseCovMtx(long satKey, double mse, out double ds50UTC, double[] pos, double[] vel, out int revNum, double[,] covMtx);


      /// <summary>
      /// Interpolates the external ephemeris data to the requested time in days since 1950, UTC
      /// The coordinate system of the output position and velocity is the same as the input ephemerides.
      /// Note: For mobile objects (coordinate systems= COORD_WPTRL, COORD_HCSRL, COORD_WPTGC, COORD_HCSGC), this function actually computes mobile object at time ds50UTC
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="ds50UTC">The requested time in ds50UTC (in-Double)</param>
      /// <param name="mse">The resulting time in minutes since the satellite's epoch time (out-Double)</param>
      /// <param name="pos">The resulting position (km) (out-Double[3])</param>
      /// <param name="vel">The resulting velocity (km/s) (out-Double[3])</param>
      /// <param name="revNum">The resulting revolution number (out-Integer)</param>
      /// <returns>0 if the external ephemeris data is successfully interpolated, non-0 if there is an error</returns>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ExtEphDs50UTC(long satKey, double ds50UTC, out double mse, double[] pos, double[] vel, out int revNum);


      /// <summary>
      /// Interpolates the external ephemeris data to the requested time in days since 1950, UTC
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="ds50UTC">The requested time in ds50UTC (in-Double)</param>
      /// <param name="mse">The resulting time in minutes since the satellite's epoch time (out-Double)</param>
      /// <param name="pos">The resulting position (km) (out-Double[3])</param>
      /// <param name="vel">The resulting velocity (km/s) (out-Double[3])</param>
      /// <param name="revNum">The resulting revolution number (out-Integer)</param>
      /// <param name="covMtx">The 6x6 covariance matrix (out-Double[6, 6])</param>
      /// <returns>0 if the external ephemeris data is successfully interpolated, non-0 if there is an error</returns>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ExtEphDs50UTCCovMtx(long satKey, double ds50UTC, out double mse, double[] pos, double[] vel, out int revNum, double[,] covMtx);


      /// <summary>
      /// Extensible routine which retrieves/interpolates external ephemeris data based on user's request
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="xf_getEph">Input type: 1=using MSE, 2=using Ds50UTC, 3=using index (one-based) (in-Integer)</param>
      /// <param name="inVal">Input value as indicated in the input type (in-Double)</param>
      /// <param name="extArr">The resulting array: 1st=mse, 2=ds50UTC, 3-5=pos, 5-8=vel, 9=revNum, 10-30=6x6 covMtx lower triangle (out-Double[128])</param>
      /// <returns>0 if the external ephemeris data is successfully interpolated, non-0 if there is an error</returns>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ExtEphXten(long satKey, int xf_getEph, double inVal, double[] extArr);


      /// <summary>
      /// This function returns a string that represents the EXTFIL= directive used to read a particular EXTEPHEM
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="line">A string representing the directive used to read a particular EXTEPHEM (out-Character[512])</param>
      /// <returns>0 if the line is retrieved successfully, non-0 if there is an error</returns>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ExtEphGetLine(long satKey, byte[] line);


      /// <summary>
      /// Returns the first satKey that matches the satNum in the EXTEPHEM binary tree
      /// This function is useful when ExtEphem DLL is used in applications that requires only one record (one EXTEPHEM entry) 
      /// for one satellite and the applications refer to that EXTEPHEM by its satellite number. 
      /// However, the Astrodynamic Standard Shared library only uses satKeys; this function helps to return the associated satKey of that satellite.
      /// </summary>
      /// <param name="satNum">input satellite number (in-Integer)</param>
      /// <returns>The satellite's unique key</returns>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern long ExtEphGetSatKey(int satNum);


      /// <summary>
      /// Returns the first satKey that matches the satNum in the EXTEPHEM binary tree
      /// This function is useful when ExtEphem DLL is used in applications that requires only one record (one EXTEPHEM entry) 
      /// for one satellite and the applications refer to that EXTEPHEM by its satellite number. 
      /// However, the Astrodynamic Standard Shared library only uses satKeys; this function helps to return the associated satKey of that satellite.
      /// </summary>
      /// <param name="satNum">input satellite number (in-Integer)</param>
      /// <param name="satKey">The satellite's unique key (out-Long)</param>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void ExtEphGetSatKeyML(int satNum, out long satKey);


      /// <summary>
      /// Creates satKey from EXTEPHEM's satelite number and date time group string
      /// This is the proper way to reconstruct a satKey from its fields. If the users use their own routine to do this, the computed satKey might be different. 
      /// </summary>
      /// <param name="satNum">input satellite number (in-Integer)</param>
      /// <param name="epochDtg">input date time group string: [yy]yydddhhmmss.sss or [yy]yyddd.ddddddd or DTG15, DTG17, DTG20 (in-Character[20])</param>
      /// <returns>The satellite's unique key</returns>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern long ExtEphFieldsToSatKey(int satNum, string epochDtg);


      /// <summary>
      /// Creates satKey from EXTEPHEM's satelite number and date time group string
      /// This is the proper way to reconstruct a satKey from its fields. If the users use their own routine to do this, the computed satKey might be different. 
      /// </summary>
      /// <param name="satNum">input satellite number (in-Integer)</param>
      /// <param name="epochDtg">input date time group string: [yy]yydddhhmmss.sss or [yy]yyddd.ddddddd or DTG15, DTG17, DTG20 (in-Character[20])</param>
      /// <param name="satKey">The satellite's unique key (out-Long)</param>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void ExtEphFieldsToSatKeyML(int satNum, string epochDtg, out long satKey);


      /// <summary>
      /// Checks to see if this satKey exists (was already loaded) in the ExtEphem binary tree
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <returns>Returns: 1= key exists; 0= key doesn't exist</returns>
      [DllImport(ExtEphemDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ExtEphKeyExists(long satKey);
      
      // Indexes of coordinate systems
      public static int  
         COORD_ECI   = 1,          // ECI TEME of DATE
         COORD_J2K   = 2,          // MEME of J2K
         COORD_EFG   = 3,          // Earth Fixed Greenwich (EFG)
         COORD_ECR   = 4,          // Earch Centered Rotation (ECR)
         COORD_LLH   = 5,          // Lat Lon Height - Geodetic Lat/Lon (deg), Height (km)
         COORD_SEN   = 6,          // Sensor site (ECR) 
         
         COORD_LLHOV = 15,         // Lat Lon Height and an offset vector (range, azimuth, elevation)
         COORD_SENOV = 16,         // Sensor site (ECR) and an offset vector (range, azimuth, elevation)
         COORD_HCSRL = 17,         // Current position (LLH), heading (azimuth), and constant speed of an mobile object that travels in a rhumb line course
         COORD_WPTRL = 18,         // List of waypoints (LLH) that describes the movement of an object that travels in a rhumb line course
         COORD_HCSGC = 19,         // Current position (LLH), initial heading (azimuth), and constant speed of an mobile object that travels in a great circle course
         COORD_WPTGC = 20,         // List of waypoints (LLH) that describes the movement of an object that travels in a great circle course
         
         
         COORD_INVALID = 100;      // Invalid coordinate system
      
      public static int   
         COVMTX_UVW_DATE  =  0,   // UVW convariance matrix - TEME of DATE
         COVMTX_XYZ_DATE  = 10,   // Cartesian covariance matrix - TEME of DATE 
         COVMTX_EQNX_DATE = 20,   // Equinoctial covariance matrix - TEME of DATE
         COVMTX_UVW_J2K   = 30,   // UVW convariance matrix - MEME of J2K
         COVMTX_XYZ_J2K   = 40,   // Cartesian covariance matrix - MEME of J2K
         COVMTX_EQNX_J2K  = 50;   // Equinoctial covariance matrix - MEME of J2K   
         
         
      public static int  
         XF_GETEPH_MSE = 1,     // Get ephemeris data using time in minutes since epoch 
         XF_GETEPH_UTC = 2,     // Get ephemeris data using time in days since 1950 UTC
         XF_GETEPH_IDX = 3;     // Get ephemeris data using index of the element in the array 
         
      // Indexes of EXTEPH data fields
      public static int  
         XF_EXTEPH_SATNUM    =  1,      // Satellite number I5
         XF_EXTEPH_EPOCH     =  2,      // Epoch YYDDDHHMMSS.SSS
         XF_EXTEPH_AE        =  3,      // Earth radius (km)
         XF_EXTEPH_KE        =  4,      // Ke
         XF_EXTEPH_POSX      =  5,      // For COORD_ECI, _J2K, _EFG, _ECR, _SEN : position X (km);   For COORD_LLH, _LLHOV : latitude   (deg)
         XF_EXTEPH_POSY      =  6,      // For COORD_ECI, _J2K, _EFG, _ECR, _SEN : position Y (km);   For COORD_LLH, _LLHOV : longitude  (deg)
         XF_EXTEPH_POSZ      =  7,      // For COORD_ECI, _J2K, _EFG, _ECR, _SEN : position Z (km);   For COORD_LLH, _LLHOV : height     (km)
         XF_EXTEPH_VELX      =  8,      // For COORD_ECI, _J2K, _EFG, _ECR, _SEN : velocity X (km/s); For COORD_LLHOV _SENOV: range      (km) 
         XF_EXTEPH_VELY      =  9,      // For COORD_ECI, _J2K, _EFG, _ECR, _SEN : velocity Y (km/s); For COORD_LLHOV _SENOV: azimuth    (deg)
         XF_EXTEPH_VELZ      = 10,      // For COORD_ECI, _J2K, _EFG, _ECR, _SEN : velocity Z (km/s); For COORD_LLHOV _SENOV: elevation  (deg)
         XF_EXTEPH_COORD     = 11,      // Input coordinate systems (see COORD_? for available types)
         XF_EXTEPH_NUMOFPTS  = 12,      // Num of ephemeris points
         XF_EXTEPH_FILEPATH  = 13,      // Ephemeris file path
         XF_EXTEPH_SATNAME   = 14,      // International Designator
         XF_EXTEPH_RECNAME   = 15;      // Record name
         
   }
// ========================= End of auto generated code ==========================
}
