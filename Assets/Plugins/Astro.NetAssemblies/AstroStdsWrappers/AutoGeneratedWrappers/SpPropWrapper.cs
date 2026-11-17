// This wrapper file was generated automatically by the GenDllWrappers program.
using System.Runtime.InteropServices;



namespace AFSPC.AstroStds.Wrappers
{
   public static partial class SpPropWrapper
   {
      // Provide the path to the dll
      #if WINDOWS
      const string SpPropDll = "spprop";
      #else
      const string SpPropDll = "libspprop.so";
      #endif
      // static constructor
      static SpPropWrapper()
      {
         // Set AS library default path/LD_LIBRARY_PATH environment
         Utilities.Utility.Init();
      }



      /// <summary>
      /// Notes: This function has been deprecated since v9.0. 
      /// Initializes SpProp DLL for use in the program
      /// If this function returns an error, it is recommended that the users stop the program immediately. The error occurs if the users forget to load and initialize all the prerequisite DLLs, as listed in the DLL Prerequisites section, before using this DLL. 
      /// </summary>
      /// <param name="apAddr">The handle that was returned from DllMainInit() (in-Long)</param>
      /// <returns>0 if SpProp.dll is initialized successfully, non-0 if there is an error</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpInit(long apAddr);


      /// <summary>
      /// Returns information about the current version of SpProp DLL. The information is placed in the string parameter you pass in
      /// The returned string provides information about the version number, build date, and the platform of the Sp DLL. 
      /// </summary>
      /// <param name="infoStr">A string to hold the information about SpProp.dll (out-Character[128])</param>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void SpGetInfo(byte[] infoStr);


      /// <summary>
      /// Loads SP-related parameters (SP application controls, prediction controls, numerical integration controls) from a text file
      /// </summary>
      /// <param name="spInputFile">The name of the file containing SP-related parameters (in-Character[512])</param>
      /// <returns>0 if the input file is read successfully, non-0 if there is an error</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpLoadFile(string spInputFile);


      /// <summary>
      /// Loads SP-related parameters (SP application controls, prediction controls, numerical integration controls) and SP elsets from a text file
      /// </summary>
      /// <param name="spInputFile">The name of the file containing SP-related parameters and SP elsets (in-Character[512])</param>
      /// <returns>0 if the input file is read successfully, non-0 if there is an error</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpLoadFileAll(string spInputFile);


      /// <summary>
      /// Saves currently loaded SP-related parameters (SP application controls, prediction controls, integration controls) to a file
      /// The purpose of this function is to save the current SP-related settings, usually used in GUI applications, for future use.
      /// </summary>
      /// <param name="spFile">The name of the file in which to save the settings (in-Character[512])</param>
      /// <param name="saveMode">Specifies whether to create a new file or append to an existing one (0 = create, 1= append) (in-Integer)</param>
      /// <param name="saveForm">Specifies the mode in which to save the file (0 = text format, 1 = not yet implemented, reserved for future) (in-Integer)</param>
      /// <returns>0 if the data is successfully saved to the file, non-0 if there is an error</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpSaveFile(string spFile, int saveMode, int saveForm);


      /// <summary>
      /// Initializes an SP satellite from an SP TLE, SPVEC, or VCM
      /// Internally, when this function is called, the SpProp DLL will look into the right binary tree, based on the vector type extracted from the satKey, and search for the satKey. If found, the SpProp DLL will use the associated SP data to create an SP object for this satellite in its own binary tree. Subsequent calls to propagate this satellite will use the data in the SP object to compute the satellite's new state.
      /// 
      /// The users need to call this routine only once for each satellite they want to propagate or every time the satellite data, from which it was initialized, is changed. The call needs to be placed before any calls to the SP propagate routines (SpPropMse, SpPropDs50UTC...).
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <returns>0 if the satellite is successfully initialized and added to SpProp.dll's set of satellites, non-0 if there is an error</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpInitSat(long satKey);


      /// <summary>
      /// Checks to see if this satellite is initiazlied
      /// </summary>
      /// <param name="satKey">The satellite's unique key. This key will have been returned by one of the routines in Tle.dll. (in-Long)</param>
      /// <returns>1 if the satellite is initialized or 0 if it isn't.</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpIsSatInit(long satKey);


      /// <summary>
      /// Initializes an SP satellite from an SP TLE, SPVEC, or VCM (thread-safe)
      /// Internally, when this function is called, the SpProp DLL will look into the right binary tree, based on the vector type extracted from the satKey, and search for the satKey. If found, the SpProp DLL will use the associated SP data to create an SP object for this satellite in its own binary tree. Subsequent calls to propagate this satellite will use the data in the SP object to compute the satellite's new state.
      /// 
      /// The users need to call this routine only once for each satellite they want to propagate or every time the satellite data, from which it was initialized, is changed. The call needs to be placed before any calls to the SP propagate routines (SpPropMse, SpPropDs50UTC...).
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="xa_spParms">SP parameters for the initialized satellite (in-Double[32])</param>
      /// <returns>0 if the satellite is successfully initialized and added to SpProp.dll's set of satellites, non-0 if there is an error</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpInitSat_MT(long satKey, double[] xa_spParms);


      /// <summary>
      /// Removes a satellite, represented by the satKey, from the set of currently loaded satellites
      /// Calling this function removes all satellites from the set maintained by SpProp.dll. However, the data loaded by Vcm.dll, Tle.dll, SpVec.dll, and ExtEphem.dll is unaffected.
      /// If the users enter an invalid satKey - a non-existing satKey in memory, the function will return a non-zero value indicating an error.
      /// </summary>
      /// <param name="satKey">The unique key of the satellite to be removed (in-Long)</param>
      /// <returns>0 if the satellite is removed successfully, non-0 if there is an error</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpRemoveSat(long satKey);


      /// <summary>
      /// Removes all currently loaded satellites from memory
      /// Calling this function removes all satellites from the set maintained by SpProp.dll. However, the data loaded by Vcm.dll, Tle.dll, SpVec.dll, and ExtEphem.dll is unaffected.
      /// The function returns zero if all the satellites were removed successfully from the SpProp DLL's binary tree.
      /// </summary>
      /// <returns>0 if all satellites are removed successfully from memory, non-0 if there is an error</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpRemoveAllSats();


      /// <summary>
      /// Returns the number of SP objects currently created. 
      /// </summary>
      /// <returns>The number of SP objects currently created.</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpGetCount();


      /// <summary>
      /// Removes all SP-related data from memory (Geo models, Flux records, JPL, DCA, etc.)
      /// </summary>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void SpReset();


      /// <summary>
      /// Retrieves the value of an SP application control parameter
      /// 
      /// The table below shows the values for the xf_SpApp parameter:
      /// 
      /// table
      /// 
      /// Index
      /// Index Interpretation
      /// 
      /// 1 Geopotential directory path
      /// 2 Buffer size
      /// 3 Run mode
      /// 4 Save partials data
      /// 5 Specter compatibility mode
      /// 6 Consider parameter
      /// 7 Decay altitude
      /// 8 Output coordinate system
      /// 9 Additional VCM options
      /// 
      /// </summary>
      /// <param name="xf_SpApp">Predefined value specifying which application control parameter to retrieve (in-Integer)</param>
      /// <param name="valueStr">A string to hold the retrieved application control parameter (out-Character[512])</param>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void SpGetApCtrl(int xf_SpApp, byte[] valueStr);


      /// <summary>
      /// Retrieves all SP application control parameters with a single function call
      /// </summary>
      /// <param name="geoDir">geopotential directory path (out-Character[512])</param>
      /// <param name="bufSize">buffer size [5000] (out-Integer)</param>
      /// <param name="runMode">run mode: 0=performance priority, [1]=memory priority (out-Integer)</param>
      /// <param name="savePartials">propagate covariance matrix: 0=don't propagate, 1=propagate covariance matrix (if VCM has it) (out-Integer)</param>
      /// <param name="isSpectr">spectr mode: 0=not compatible, 1=SPECTR compatible (out-Integer)</param>
      /// <param name="consider">consider parameter (set it to 12 to match operational system) (out-Double)</param>
      /// <param name="decayAlt">decay altitude (km) [10] (out-Integer)</param>
      /// <param name="outCoord">ouput cooridnate system: [0]=TEME of Date, 1=TEME of Epoch, 2=MEME of J2K (out-Integer)</param>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void SpGetApCtrlAll(byte[] geoDir, out int bufSize, out int runMode, out int savePartials, out int isSpectr, out double consider, out int decayAlt, out int outCoord);


      /// <summary>
      /// Sets an SP application control parameter
      /// See SpGetApCtrl for a list of the values for the xf_SpApp parameter.
      /// </summary>
      /// <param name="xf_SpApp">predefined value specifying which application control parameter to set (in-Integer)</param>
      /// <param name="valueStr">the new value of the specified parameter, expressed as a string (in-Character[512])</param>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void SpSetApCtrl(int xf_SpApp, string valueStr);


      /// <summary>
      /// Sets all SP application control parameters with a single function call
      /// </summary>
      /// <param name="geoDir">geopotential directory path (in-Character[512])</param>
      /// <param name="bufSize">buffer size [5000] (in-Integer)</param>
      /// <param name="runMode">run mode: 0=performance priority, [1]=memory priority (in-Integer)</param>
      /// <param name="savePartials">propagate covariance matrix: 0=don't propagate, 1=propagate covariance matrix (if VCM has it) (in-Integer)</param>
      /// <param name="isSpectr">spectr mode: 0=not compatible, 1=SPECTR compatible (in-Integer)</param>
      /// <param name="consider">consider parameter (set it to 12 to match operational system) (in-Double)</param>
      /// <param name="decayAlt">decay altitude (km) [10] (in-Integer)</param>
      /// <param name="outCoord">ouput cooridnate system: [0]=TEME of Date, 1=TEME of Epoch, 2=MEME of J2K (in-Integer)</param>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void SpSetApCtrlAll(string geoDir, int bufSize, int runMode, int savePartials, int isSpectr, double consider, int decayAlt, int outCoord);


      /// <summary>
      /// Retrieves the value of a numerical integration control parameter
      /// 
      /// The table below shows the values for xf_4P parameter:
      /// 
      /// table
      /// 
      /// Index
      /// Index Interpretation
      /// 
      /// 1   Geopotential model to use
      /// 2   Earth gravity pertubations flag
      /// 3   Drag pertubations flag 
      /// 4   Radiation pressure pertubations flag
      /// 5   Lunar/Solar pertubations flag
      /// 6   F10 value
      /// 7   F10 average value
      /// 8   Ap value
      /// 9   Geopotential truncation order/degree/zonals
      /// 10  Corrector step convergence criterion; exponent of 1/10; default = 10
      /// 11  Outgassing pertubations flag
      /// 12  Solid earth ocean tide pertubations flag
      /// 13  Input vector coordinate system
      /// 14  Nutation terms
      /// 15  Recompute pertubations at each corrector step
      /// 16  Variable of intergration control
      /// 17  Variable step size control
      /// 18  Initial step size
      /// 21  DCA file name
      /// 22  Solar flux file name
      /// 23  Geopotential file name
      /// 24  JPL file name
      /// 25  JPL start time
      /// 26  JPL stop time
      /// 
      /// See the SpProp.pdf document under the librarydocuments folder, section 7.3
      /// "Numerical Integration Control (4P-Card)".  This document has a description
      /// of each column for a 4P card.  For an example, see files in the 
      /// Verify/SpProp/Execution/input folder, such as sp_sat1.inp and look at the 
      /// line that ends with "4P".  
      /// </summary>
      /// <param name="xf_4P">Predefined value specifying which application control parameter to retrieve (in-Integer)</param>
      /// <param name="valueStr">A string to hold the retrieved application control parameter (out-Character[512])</param>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void SpGet4P(int xf_4P, byte[] valueStr);


      /// <summary>
      /// Sets the value of a numerical integration control parameter
      /// See SpGet4P for a list of the values for the xf_4P parameter.
      /// For flux, dca, and jpl file settings, the actual file loading happens when the first SpSatInit() is called. 
      /// If the user needs to load these files instantly, please call the SpLoadFile() instead
      /// See the SpProp.pdf document under the librarydocuments folder, section 7.3
      /// "Numerical Integration Control (4P-Card)".  This document has a description
      /// of each column for a 4P card.  For an example, see files in the 
      /// Verify/SpProp/Execution/input folder, such as sp_sat1.inp and look at the 
      /// line that ends with "4P".  
      /// </summary>
      /// <param name="xf_4P">predefined value specifying which application control parameter to set (in-Integer)</param>
      /// <param name="valueStr">the new value of the specified parameter, expressed as a string (in-Character[512])</param>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void SpSet4P(int xf_4P, string valueStr);


      /// <summary>
      /// Retrieves prediction control parameters
      /// </summary>
      /// <param name="startFrEpoch">start time flag: 0=in days since 1950 UTC, 1=in minutes since epoch (out-Integer)</param>
      /// <param name="stopFrEpoch">stop time flag : 0=in days since 1950 UTC, 1=in minutes since epoch (out-Integer)</param>
      /// <param name="startTime">start time value (out-Double)</param>
      /// <param name="stopTime">stop time value (out-Double)</param>
      /// <param name="interval">step size (min) (out-Double)</param>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void SpGetPredCtrl(out int startFrEpoch, out int stopFrEpoch, out double startTime, out double stopTime, out double interval);


      /// <summary>
      /// Sets prediction control parameters
      /// </summary>
      /// <param name="startFrEpoch">start time flag: 0=in days since 1950 UTC, 1=in minutes since epoch (in-Integer)</param>
      /// <param name="stopFrEpoch">stop time flag : 0=in days since 1950 UTC, 1=in minutes since epoch (in-Integer)</param>
      /// <param name="startTime">start time value (in-Double)</param>
      /// <param name="stopTime">stop time value (in-Double)</param>
      /// <param name="interval">step size (min) (in-Double)</param>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void SpSetPredCtrl(int startFrEpoch, int stopFrEpoch, double startTime, double stopTime, double interval);


      /// <summary>
      /// Retrieves the value of a field of an SP satellite
      /// 
      /// The table below shows the values for the xf_SpSat parameter:
      /// 
      /// table
      /// 
      /// Index
      /// Index Interpretation
      /// 
      /// 1  Satellite number I5
      /// 2  Satellite's epoch time in days since 1950, UTC 
      /// 3  Satellite's epoch time in days since 1950, TAI
      /// 4  Mu value 
      /// 5  Covariance Matrix flag
      /// 6  Integration mode
      /// 7  Nutation terms
      /// 8  Specter compatibility mode
      /// 
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="xf_SpSat">Predefined number specifying which field to retrieve (in-Integer)</param>
      /// <param name="valueStr">A string containing the value of the specified field (out-Character[512])</param>
      /// <returns>0 if successful, non-0 if there is an error</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpGetSatData(long satKey, int xf_SpSat, byte[] valueStr);


      /// <summary>
      /// Retrieves all fields of an SP satellite with a single function call
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="satNum">Satellite number (out-Integer)</param>
      /// <param name="epochDs50UTC">Satellite's epoch time in days since 1950 UTC (out-Double)</param>
      /// <param name="epochDs50TAI">Satellite's epoch time in days since 1950 TAI (out-Double)</param>
      /// <param name="mu">mu value from Satellite's geopotential model (out-Double)</param>
      /// <param name="hasCovMtx">0=This satellite doesn't have covariance matrix, 1=This satellite has covariance matrix (out-Integer)</param>
      /// <param name="integMode">Partials computation mode: 0=no partials, 1=numerical, 2=semi-analytic (out-Integer)</param>
      /// <param name="nTerms">number of nutation terms (out-Integer)</param>
      /// <param name="isSpectr">Spectr compatible mode: 0=not compatible, 1=compatible (out-Integer)</param>
      /// <returns>0 if successful, non-0 if there is an error</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpGetSatDataAll(long satKey, out int satNum, out double epochDs50UTC, out double epochDs50TAI, out double mu, out int hasCovMtx, out int integMode, out int nTerms, out int isSpectr);


      /// <summary>
      /// Propagates a satellite, represented by the satKey, to the time expressed in minutes since the 
      /// satellite's epoch time
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="mse">The requested time in minutes since the satellite's epoch time (in-Double)</param>
      /// <param name="xa_timeTypes">The output array that stores different time types (see XA_TIMETYPES_?) (out-Double[5])</param>
      /// <param name="revNum">The resulting revolution number (out-Integer)</param>
      /// <param name="posJ2K">The resulting position vector (km) in MEME of J2K (out-Double[3])</param>
      /// <param name="velJ2K">The resulting velocity vector (km/s) in MEME of J2K (out-Double[3])</param>
      /// <returns>0 if successful, non-0 if there is an error</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpPropMse(long satKey, double mse, double[] xa_timeTypes, out int revNum, double[] posJ2K, double[] velJ2K);


      /// <summary>
      /// Propagates a satellite, represented by the satKey, to the time expressed in days since 1950, UTC. 
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="ds50UTC">The requested time in days since 1950 UTC (in-Double)</param>
      /// <param name="xa_timeTypes">The output array that stores different time types (see XA_TIMETYPES_?) (out-Double[5])</param>
      /// <param name="revNum">The resulting revolution number (out-Integer)</param>
      /// <param name="posJ2K">The resuling position vector (km) in MEME of J2K (out-Double[3])</param>
      /// <param name="velJ2K">The resulting velocity vector (km/s) in MEME of J2K (out-Double[3])</param>
      /// <returns>0 if successful, non-0 if there is an error</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpPropDs50UTC(long satKey, double ds50UTC, double[] xa_timeTypes, out int revNum, double[] posJ2K, double[] velJ2K);


      /// <summary>
      /// Propagates a satellite, represented by the satKey, to the time expressed in days since 1950, UTC. 
      /// It only returns the latitude, longitude, and height at that time in coordinate system of Date
      /// It is the users' responsibility to decide what to do with the returned value. For example, if the users want to check for decay or low altitude, they can put that logic into their own code.
      /// 
      /// This function is built especially for application that plots ground trace.
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="ds50UTC">The requested time in days since 1950 UTC (in-Double)</param>
      /// <param name="llh">The resulting array: 1=geodetic latitude (deg), 2=geodetic longitude (deg), 3=height (km) (out-Double[3])</param>
      /// <returns>0 if successful, non-0 if there is an error</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpPropDs50UtcLLH(long satKey, double ds50UTC, double[] llh);


      /// <summary>
      /// Propagates a satellite, represented by the satKey, to the time expressed in days since 1950, UTC. 
      /// It only returns the satellite's ECI position in TEME of DATE
      /// It is the users' responsibility to decide what to do with the returned value. For example, if the users want to check for decay or low altitude, they can put that logic into their own code.
      /// 
      /// This function is built especially for application that plots satellites' positions in 3D.
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="ds50UTC">The requested time in days since 1950 UTC (in-Double)</param>
      /// <param name="pos">The resulting ECI position (km) in TEME of Date (out-Double[3])</param>
      /// <returns>0 if successful, non-0 if there is an error</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpPropDs50UtcPos(long satKey, double ds50UTC, double[] pos);


      /// <summary>
      /// Propagates a satellite, represented by the satKey, to the time expressed in either minutes since epoch or days since 1950, UTC. 
      /// 
      /// All propagation data is returned by this function.
      /// </summary>
      /// <param name="satKey">The unique key of the satellite to propagate. (in-Long)</param>
      /// <param name="timeType">The propagation time type: 0 = minutes since epoch, 1 = days since 1950, UTC (in-Integer)</param>
      /// <param name="timeIn">The time to propagate to, expressed in either minutes since epoch or days since 1950, UTC. (in-Double)</param>
      /// <param name="covType">Covariance matrix type, if available: 0=don't propagate, 1=UVW, 2=ECI_DATE, 3=EQNX_DATE, 12=ECI_J2K, 13=EQNX_J2K (in-Integer)</param>
      /// <param name="xa_spOut">The array that stores all Sp propagation data (see XA_SPOUT_?) (out-Double[128])</param>
      /// <returns>0 if the propagation is successful, 2 on error.</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpPropAll(long satKey, int timeType, double timeIn, int covType, double[] xa_spOut);


      /// <summary>
      /// Propagates a satellite, represented by the satKey, to the time expressed in days since 1950, UTC. 
      /// It only returns the position and velocity in TEME of Date
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="ds50UTC">The requested time in days since 1950 UTC (in-Double)</param>
      /// <param name="posDate">The resulting ECI position (km) in TEME of Date (out-Double[3])</param>
      /// <param name="velDate">The resulting ECI velocity (km/s) in TEME of Date (out-Double[3])</param>
      /// <returns>0 if successful, non-0 if there is an error</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpGetStateDs50UTC(long satKey, double ds50UTC, double[] posDate, double[] velDate);


      /// <summary>
      /// Reepochs the state of an existing SP object associated with the satKey. 
      /// Proper initialization is handled internally so no need to call SpSatInit() afterward 
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="ds50UTC">The requested new epoch time in days since 1950 UTC (in-Double)</param>
      /// <param name="posDate">The initial ECI position (km) in TEME of Date (in-Double[3])</param>
      /// <param name="velDate">The initial ECI velocity (km/s) in TEME of Date (in-Double[3])</param>
      /// <returns>0 if successful, non-0 if there is an error</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpSetStateDs50UTC(long satKey, double ds50UTC, double[] posDate, double[] velDate);


      /// <summary>
      /// Updates an SP object associated with the satKey with the new input data stored in setDataArr. 
      /// Then propagates the updated SP object to the requested time propTimeDs50UTC. 
      /// Resulting propagated data will be stored in propDataArr.
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="setDataArr">0-2: posECI (km), 3-5: velECI (km/s), 6: bTerm (m2/kg), 7: agom (m2/kg) (in-Double[128])</param>
      /// <param name="propTimeDs50UTC">The requested time in days since 1950 UTC (in-Double)</param>
      /// <param name="propDataArr">0-2: out posECI (km), 3-5: out velECI (km/s), 6-127: NA (out-Double[128])</param>
      /// <returns>0 if successful, non-0 if there is an error</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpSetAndProp(long satKey, double[] setDataArr, double propTimeDs50UTC, double[] propDataArr);


      /// <summary>
      /// This function is reserved for future use. The purpose is to return propagator output data based on user special requests that have not yet been determined
      /// Use this function immediately after the call to SpPropMse or SpPropDs50UTC. 
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="index">Index specified ouput data (in-Integer)</param>
      /// <param name="destArr">The resulting array (out-Double[*])</param>
      /// <returns>0 if successful, non-0 if there is an error</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpGetPropOut(long satKey, int index, double[] destArr);


      /// <summary>
      /// Returns the covariance matrix of a satellite
      /// Use this function immediately after the call to SpPropMse or SpPropDs50UTC. 
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="covType">Covariance matrix type: 1=UVW, 2=ECI_DATE, 3=EQNX_DATE, 12=ECI_J2K, 13=EQNX_J2K (in-Integer)</param>
      /// <param name="covMtx">Resulting covariance matrix 6x6 (out-Double[6, 6])</param>
      /// <returns>0 if successful, non-0 if there is an error</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpGetCovMtx(long satKey, int covType, double[,] covMtx);


      /// <summary>
      /// Computes the covariance sigma from the input covariance matrix
      /// </summary>
      /// <param name="covMtx">Input covariance matrix (in-Double[6, 6])</param>
      /// <param name="covSigma">Resulting covariance sigma (out-Double[6])</param>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void SpCompCovSigma(double[,] covMtx, double[] covSigma);


      /// <summary>
      /// Sets all numerical integration control parameters with a single function call
      /// See the SpProp.pdf document under the librarydocuments folder, section 7.3
      /// "Numerical Integration Control (4P-Card)".  This document has a description
      /// of each column for a 4P card.  For an example, see files in the 
      /// Verify/SpProp/Execution/input folder, such as sp_sat1.inp and look at the 
      /// line that ends with "4P".  
      /// </summary>
      /// <param name="geoIdx">geopotential model (in-Integer)</param>
      /// <param name="bulgePert">earth gravity flag (in-Integer)</param>
      /// <param name="dragPert">drag pertubation flag (in-Integer)</param>
      /// <param name="radPresPert">radiation pressure pertubation flag (in-Integer)</param>
      /// <param name="lunSolPert">lunar/solar pertubation flag (in-Integer)</param>
      /// <param name="f10">f10 value (in-Integer)</param>
      /// <param name="f10Avg">f10 average value (in-Integer)</param>
      /// <param name="ap">ap (in-Integer)</param>
      /// <param name="trunc">geo truncation order/degree/zonals (in-Integer)</param>
      /// <param name="incr">corrector step convergenece criterion (in-Integer)</param>
      /// <param name="ogPert">outgassing pertubation flag (in-Integer)</param>
      /// <param name="tidePert">solid earth + ocean tide pertubation flag (in-Integer)</param>
      /// <param name="inCoord">input vector coordinate system (in-Integer)</param>
      /// <param name="nTerms">nutation terms (in-Integer)</param>
      /// <param name="reEval">recompute pertubations at each corrector step (in-Integer)</param>
      /// <param name="integStepMode">variable of integration control (in-Integer)</param>
      /// <param name="stepSizeMethod">variable step size control (in-Integer)</param>
      /// <param name="initStepSize">initial step size (in-Double)</param>
      /// <param name="dcaFile">DCA file name (in-Character[512])</param>
      /// <param name="fluxFile">Flux file name (in-Character[512])</param>
      /// <param name="geoFile">Geo file name (in-Character[512])</param>
      /// <param name="jplFile">JPL file name (in-Character[512])</param>
      /// <param name="jplStart">JPL start time (in-Character[512])</param>
      /// <param name="jplStop">JPL stop time (in-Character[512])</param>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void SpSet4pAll(int geoIdx, int bulgePert, int dragPert, int radPresPert, int lunSolPert, int f10, int f10Avg, int ap, int trunc, int incr, int ogPert, int tidePert, int inCoord, int nTerms, int reEval, int integStepMode, int stepSizeMethod, double initStepSize, string dcaFile, string fluxFile, string geoFile, string jplFile, string jplStart, string jplStop);


      /// <summary>
      /// Retrieves all numerical integration control parameters with a single function call
      /// See the SpProp.pdf document under the librarydocuments folder, section 7.3
      /// "Numerical Integration Control (4P-Card)".  This document has a description
      /// of each column for a 4P card.  For an example, see files in the 
      /// Verify/SpProp/Execution/input folder, such as sp_sat1.inp and look at the 
      /// line that ends with "4P".  
      /// </summary>
      /// <param name="geoIdx">geopotential model (out-Integer)</param>
      /// <param name="bulgePert">earth gravity flag (out-Integer)</param>
      /// <param name="dragPert">drag pertubation flag (out-Integer)</param>
      /// <param name="radPresPert">radiation pressure pertubation flag (out-Integer)</param>
      /// <param name="lunSolPert">lunar/solar pertubation flag (out-Integer)</param>
      /// <param name="f10">f10 value (out-Integer)</param>
      /// <param name="f10Avg">f10 average value (out-Integer)</param>
      /// <param name="ap">ap (out-Integer)</param>
      /// <param name="trunc">geo truncation order/degree/zonals (out-Integer)</param>
      /// <param name="incr">corrector step convergenece criterion (out-Integer)</param>
      /// <param name="ogPert">outgassing pertubation flag (out-Integer)</param>
      /// <param name="tidePert">solid earth + ocean tide pertubation flag (out-Integer)</param>
      /// <param name="inCoord">input vector coordinate system (out-Integer)</param>
      /// <param name="nTerms">nutation terms (out-Integer)</param>
      /// <param name="reEval">recompute pertubations at each corrector step (out-Integer)</param>
      /// <param name="integStepMode">variable of integration control (out-Integer)</param>
      /// <param name="stepSizeMethod">variable step size control (out-Integer)</param>
      /// <param name="initStepSize">initial step size (out-Double)</param>
      /// <param name="dcaFile">DCA file name (out-Character[512])</param>
      /// <param name="fluxFile">Flux file name (out-Character[512])</param>
      /// <param name="geoFile">Geo file name (out-Character[512])</param>
      /// <param name="jplFile">JPL file name (out-Character[512])</param>
      /// <param name="jplStart">JPL start time (out-Character[512])</param>
      /// <param name="jplStop">JPL stop time (out-Character[512])</param>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void SpGet4pAll(out int geoIdx, out int bulgePert, out int dragPert, out int radPresPert, out int lunSolPert, out int f10, out int f10Avg, out int ap, out int trunc, out int incr, out int ogPert, out int tidePert, out int inCoord, out int nTerms, out int reEval, out int integStepMode, out int stepSizeMethod, out double initStepSize, byte[] dcaFile, byte[] fluxFile, byte[] geoFile, byte[] jplFile, byte[] jplStart, byte[] jplStop);


      /// <summary>
      /// Read the integration control settings from a 4P-Card.
      /// See the SpProp.pdf document under the librarydocuments folder, section 7.3
      /// "Numerical Integration Control (4P-Card)".  This document has a description
      /// of each column for a 4P card.  For an example, see files in the 
      /// Verify/SpProp/Execution/input folder, such as sp_sat1.inp and look at the 
      /// line that ends with "4P".  
      /// </summary>
      /// <param name="card4P">The input 4P card (in-Character[512])</param>
      /// <returns>0 if the input 4P card is read successfully, non-0 if there is an error</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpSet4PCard(string card4P);


      /// <summary>
      /// Builds and returns the integration control card (4P-Card) from the current settings
      /// See the SpProp.pdf document under the librarydocuments folder, section 7.3
      /// "Numerical Integration Control (4P-Card)".  This document has a description
      /// of each column for a 4P card.  For an example, see files in the 
      /// Verify/SpProp/Execution/input folder, such as sp_sat1.inp and look at the 
      /// line that ends with "4P".  
      /// </summary>
      /// <param name="card4P">A string to hold the retrieved 4P card (out-Character[512])</param>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void SpGet4PCard(byte[] card4P);


      /// <summary>
      /// Adds one solar flux record to memory. This API can be used to avoid SpLoadFile's file I/O      
      /// </summary>
      /// <param name="refDs50UTC">Time tag of this record (DS50; UTC) (in-Double)</param>
      /// <param name="f10">f10 value (in-Double)</param>
      /// <param name="f10Avg">f10avg value (in-Double)</param>
      /// <param name="ap">Array of 3-hourly values of Ap (in-Double[8])</param>
      /// <returns>0 if successful, non-0 if there is an error</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpAddFluxRec(double refDs50UTC, double f10, double f10Avg, double[] ap);


      /// <summary>
      /// Returns the times, in days since 1950 UTC, of ephemeris points stored in SP buffer that meet the requirement as specified in the minStepSize
      /// This function is used to retrieve times in which SP propagator stored satellite's state vectors in memory (natural integration step size).
      /// An error will occur if the actualEphPts reaches the maxEphPts. In this case, the function will return all ephemereris times up to the time when that maxEphPts is reached.
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="maxEphPts">Maximum number of possible ephemeris points that the function won't go over. That means actualEphPts can't be greater than maxEphPts even though the time span may have more points than that (in-Integer)</param>
      /// <param name="startDs50UTC">Start time in days since 1950 UTC (in-Double)</param>
      /// <param name="stopDs50UTC">Stop time in days since 1950 UTC (in-Double)</param>
      /// <param name="minStepSize">Option to thin ephem points. Only pick ephem point when its next ephem point is older than the specified minStepSize (minutes). Set to 0 to get all ephem points (SP natural integration step size) (in-Double)</param>
      /// <param name="actualEphPts">Actual number of ephem points that meet the requirements (out-Integer)</param>
      /// <param name="ds50UTCArr">The resulting time array, in days since 1950 UTC, of all ephem points that meet the requirements (out-Double[*])</param>
      /// <returns>0 if the function successfully returns all the ephemeris times of the specified start/stop time span, non-0 if there is an error. Also if</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpGetEphemTimes(long satKey, int maxEphPts, double startDs50UTC, double stopDs50UTC, double minStepSize, out int actualEphPts, double[] ds50UTCArr);


      /// <summary>
      /// Generates ephemerides for the input satellite, represented by its satKey, for the specified time span and step size
      /// Notes: if arrSize isn't big enough to store all the ephemeris points, the function will exit when the ephemArr reaches
      /// that many points (arrSize) and the errCode is set to IDX_ERR_WARN
      /// </summary>
      /// <param name="satKey">The unique key of the satellite to generate ephemerides. (in-Long)</param>
      /// <param name="startDs50UTC">Start time expressed in days since 1950, UTC. (in-Double)</param>
      /// <param name="stopDs50UTC">End time expressed in days since 1950, UTC. (in-Double)</param>
      /// <param name="stepSize">Step size in minutes, set to 0 if want to use SP natural integration step size (in-Double)</param>
      /// <param name="sp_ephem">Output ephemeris type: 1=ECI, 2=J2K. (in-Integer)</param>
      /// <param name="arrSize">Size of input ephemArr (in-Integer)</param>
      /// <param name="ephemArr">Output ephemerides - 0: time in days since 1950 UTC, 1-3: pos (km), 4-6: vel (km/sec) (out-Double[*, 7])</param>
      /// <param name="genEphemPts">Actual number of ephemeris points generated (always &le; arrSize) (out-Integer)</param>
      /// <returns>0 if the propagation is successful, non-0 if there is an error (see GP_ERR_?).</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpGenEphems(long satKey, double startDs50UTC, double stopDs50UTC, double stepSize, int sp_ephem, int arrSize, double[,] ephemArr, out int genEphemPts);


      /// <summary>
      /// This function is similar to SpGenEphems but also returns covariance matrix data
      /// Notes: Make sure to turn on the "save partials" flag so that covariance matrix data can be generated
      /// </summary>
      /// <param name="satKey">The unique key of the satellite to generate ephemerides. (in-Long)</param>
      /// <param name="startDs50UTC">Start time expressed in days since 1950, UTC. (in-Double)</param>
      /// <param name="stopDs50UTC">End time expressed in days since 1950, UTC. (in-Double)</param>
      /// <param name="stepSize">Step size in minutes, set to 0 if want to use SP natural integration step size (in-Double)</param>
      /// <param name="sp_ephem">Output ephemeris type: 1=ECI, 2=J2K. (in-Integer)</param>
      /// <param name="covType">Covariance matrix type, if available: 0=don't propagate,  1=UVW, 2=ECI_DATE, 3=EQNX_DATE, 12=ECI_J2K, 13=EQNX_J2K (in-Integer)</param>
      /// <param name="arrSize">Size of input ephemArr (in-Integer)</param>
      /// <param name="ephemArr">Output ephemerides - 0: time in days since 1950 UTC, 1-3: pos (km), 4-6: vel (km/sec), 7-27: 21-elements lower triagular covariance matrix (out-Double[*, 28])</param>
      /// <param name="genEphemPts">Actual number of ephemeris points generated (always &le; arrSize) (out-Integer)</param>
      /// <returns>0 if the propagation is successful, non-0 if there is an error (see GP_ERR_?).</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpGenEphemsCov(long satKey, double startDs50UTC, double stopDs50UTC, double stepSize, int sp_ephem, int covType, int arrSize, double[,] ephemArr, out int genEphemPts);


      /// <summary>
      /// Generates ephemerides for the input vcm (in string format) for the specified time span and step size
      /// Notes: if arrSize isn't big enough to store all the ephemeris points, the function will exit when the ephemArr reaches
      /// that many points (arrSize) and the errCode is set to IDX_ERR_WARN
      /// </summary>
      /// <param name="vcmString">1-line or concatenated string representation of the VCM (in-Character[4000])</param>
      /// <param name="startDs50UTC">Start time expressed in days since 1950, UTC. (in-Double)</param>
      /// <param name="stopDs50UTC">End time expressed in days since 1950, UTC. (in-Double)</param>
      /// <param name="stepSize">Step size in minutes, set to 0 if want to use SP natural integration step size (in-Double)</param>
      /// <param name="sp_ephem">Output ephemeris type: 1=ECI, 2=J2K. (in-Integer)</param>
      /// <param name="arrSize">Size of input ephemArr (in-Integer)</param>
      /// <param name="ephemArr">Output ephemerides - 0: time in days since 1950 UTC, 1-3: pos (km), 4-6: vel (km/sec) (out-Double[*, 7])</param>
      /// <param name="genEphemPts">Actual number of ephemeris points generated (always &le; arrSize) (out-Integer)</param>
      /// <returns>0 if the propagation is successful, non-0 if there is an error (see GP_ERR_?).</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpGenEphemsVcm_OS(string vcmString, double startDs50UTC, double stopDs50UTC, double stepSize, int sp_ephem, int arrSize, double[,] ephemArr, out int genEphemPts);


      /// <summary>
      /// This function is similar to SpGenEphemsVcm_OS but also returns covariance matrix data
      /// </summary>
      /// <param name="vcmString">1-line or concatenated string representation of the VCM (in-Character[4000])</param>
      /// <param name="startDs50UTC">Start time expressed in days since 1950, UTC. (in-Double)</param>
      /// <param name="stopDs50UTC">End time expressed in days since 1950, UTC. (in-Double)</param>
      /// <param name="stepSize">Step size in minutes, set to 0 if want to use SP natural integration step size (in-Double)</param>
      /// <param name="sp_ephem">Output ephemeris type: 1=ECI, 2=J2K. (in-Integer)</param>
      /// <param name="covType">Covariance matrix type, if available:  1=UVW, 2=ECI_DATE, 3=EQNX_DATE, 12=ECI_J2K, 13=EQNX_J2K (in-Integer)</param>
      /// <param name="arrSize">Size of input ephemArr (in-Integer)</param>
      /// <param name="ephemArr">Output ephemerides - 0: time in days since 1950 UTC, 1-3: pos (km), 4-6: vel (km/sec), 7-27: 21-elements lower triagular covariance matrix (out-Double[*, 28])</param>
      /// <param name="genEphemPts">Actual number of ephemeris points generated (always &le; arrSize) (out-Integer)</param>
      /// <returns>0 if the propagation is successful, non-0 if there is an error (see GP_ERR_?).</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpGenEphemsVcmCov_OS(string vcmString, double startDs50UTC, double stopDs50UTC, double stepSize, int sp_ephem, int covType, int arrSize, double[,] ephemArr, out int genEphemPts);


      /// <summary>
      /// Propagates all input satellites, represented by their satKeys, to the time expressed in days since 1950, UTC. 
      /// </summary>
      /// <param name="satKeys">The satellite keys of all input satellites (in-Long[*])</param>
      /// <param name="numOfSats">The total number of satellites (in-Integer)</param>
      /// <param name="ds50UTC">The time to propagate all satelllites to, expressed in days since 1950, UTC. (in-Double)</param>
      /// <param name="ephemArr">0-2: pos (km), 3-5: vel (km/sec) - ECI TEME of Date (out-Double[*, 6])</param>
      /// <returns>0 if the propagation is successful, non-0 if there is an error.</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpPropAllSats(long[] satKeys, int numOfSats, double ds50UTC, double[,] ephemArr);


      /// <summary>
      /// Propagates a satellite, represented by the satKey, to the time expressed in either minutes since epoch or days since 1950, UTC. 
      /// 
      /// All propagation data is returned by this function.
      /// </summary>
      /// <param name="satKey">The unique key of the satellite to propagate. (in-Long)</param>
      /// <param name="timeType">The propagation time type: 0 = minutes since epoch, 1 = days since 1950, UTC (in-Integer)</param>
      /// <param name="timeIn">The time to propagate to, expressed in either minutes since epoch or days since 1950, UTC. (in-Double)</param>
      /// <param name="spCoord">Output coordinate systems for pos/vel/acc (see SPCOORD_?) (in-Integer)</param>
      /// <param name="xf_CovMtx">Covariance matrix type (see XF_COVMTX_?) (in-Integer)</param>
      /// <param name="stmType">State transition matrix type (see STMTYPE_?) (in-Integer)</param>
      /// <param name="xa_spExt">The array that stores all Sp propagation data (see XA_SPEXT_?) (out-Double[128])</param>
      /// <returns>0 if the propagation is successful, 2 on error.</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpPropAllExt(long satKey, int timeType, double timeIn, int spCoord, int xf_CovMtx, int stmType, double[] xa_spExt);


      /// <summary>
      /// Loads solar flux file (J70MOD or JBH09) for use in AtmspDensity() 
      /// </summary>
      /// <param name="fluxFile">The name of the file containing SP-related parameters (in-Character[512])</param>
      /// <returns>0 if the input file is read successfully, non-0 if there is an error</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpLoadFluxFile(string fluxFile);


      /// <summary>
      /// Loads DCA temperature coefficient file (DCA1/HASDM1 or DCA2/HASDM2) for use in AtmspDensity()
      /// </summary>
      /// <param name="dcaFile">The name of the file containing SP-related parameters (in-Character[512])</param>
      /// <returns>0 if the input file is read successfully, non-0 if there is an error</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpLoadDCAFile(string dcaFile);


      /// <summary>
      /// Initialize the MSIS parameters file.  Must be done before using MSIS2.1
      /// </summary>
      /// <param name="parmFile">File name (in-Character[512])</param>
      /// <returns>0 if the input file is read successfully, non-0 if there is an error</returns>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int InitMsis(string parmFile);


      /// <summary>
      /// Computes atmospheric density using the input time/location/drag model
      /// </summary>
      /// <param name="ds50UTC">Input time, expressed in days since 1950, UTC. (in-Double)</param>
      /// <param name="llh">Input array: latitude (deg)/longitude (deg)/height (km-above geoid) (in-Double[3])</param>
      /// <param name="drgMdl">Drag perturbations model (see DRGMDL_x for available options (in-Integer)</param>
      /// <param name="rho">Atmospheric density (kg / m**3) (out-Double)</param>
      /// <param name="errCode">Error code - 0 if successful, non-0 if there is an error (out-Integer)</param>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void AtmspDensity(double ds50UTC, double[] llh, int drgMdl, out double rho, out int errCode);


      /// <summary>
      /// Gets DCA Record at specified time
      /// </summary>
      /// <param name="ds50UTC">Request Time (in-Double)</param>
      /// <param name="ds50UTCRec">Time of the DCA record (out-Double)</param>
      /// <param name="flux">S10,S54,M10,M54,Y10,Y54,F10,F54 for IVER=3 JBH09 DCA (out-Double[8])</param>
      /// <param name="storm">ap,Dst,DTC geomagnetic storm values IVER=3 JBH09 DCA (out-Double[3])</param>
      /// <param name="ctxi">Even spherical harmonic coefficients for DTX (out-Double[7, 7])</param>
      /// <param name="stxi">Odd spherical harmonic coefficients for DTX (out-Double[7, 7])</param>
      /// <param name="ctci">Even spherical harmonic coefficients for DTC (out-Double[7, 7])</param>
      /// <param name="stci">Odd spherical harmonic coefficients for DTC (out-Double[7, 7])</param>
      /// <param name="errCode">Error code - 0 if successful, non-0 if there is an error (out-Integer)</param>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void GetDcaRec(double ds50UTC, out double ds50UTCRec, double[] flux, double[] storm, double[,] ctxi, double[,] stxi, double[,] ctci, double[,] stci, out int errCode);


      /// <summary>
      /// Unloads the flux/JBHSGI flux
      /// </summary>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void SpRemoveFlux();


      /// <summary>
      /// Unloads the DCA records
      /// </summary>
      [DllImport(SpPropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void SpRemoveDCAFile();
      
      // VCM additional options
      public static int  
         VCMOPT_USEOWN    = 0,    // use VCM's own data
         VCMOPT_USEGLOBAL = 1;    // force VCM to use global solar flux and timing constants data
      
      // Run Modes
      public static int  
         IDX_RUNMODE_PERF = 0,   // Perfromance priority. This mode allows the buffer of integration points to extend freely
         IDX_RUNMODE_MEM  = 1;   // Memory priority. This mode only allows a number of integration points to be saved in the buffer at any one time
         
      //*******************************************************************************
         
      // Partials Saving Modes
      public static int  
         IDX_PARTIALS_SAVE = 1,     // Save partials in the buffer
         IDX_PARTIALS_DONT = 0;     // Don't save partials in the buffer
      
      // Indexes of different covariance matrices
      public static int  
         XF_COVMTX_UVW_DATE  =  1,     // UVW convariance matrix - TEME of DATE
         XF_COVMTX_XYZ_DATE  =  2,     // Cartesian covariance matrix - TEME of DATE 
         XF_COVMTX_EQNX_DATE =  3,     // Equinoctial covariance matrix - TEME of DATE (not available for "PARTIALS: ANALYTIC")
         XF_COVMTX_UVW_J2K   = 11,     // UVW convariance matrix - MEME of J2K (same result as UVW TEME of DATE)
         XF_COVMTX_XYZ_J2K   = 12,     // Cartesian covariance matrix - MEME of J2K
         XF_COVMTX_EQNX_J2K  = 13;     // Equinoctial covariance matrix - MEME of J2K  (not available for "PARTIALS: ANALYTIC")
      //*******************************************************************************
      
      // Indexes of Lunar/Solar and Planets perturbation modes
      public static int  
         LSPERT_NONE      = 0,   // Lunar/Solar perturbation off
         LSPERT_ANALYTIC  = 1,   // Lunar/Solar perturbation on (using Analytic mode)
         LSPERT_JPL       = 2,   // Lunar/Solar perturbation using JPL ephemeris file
         LSPERT_ALL       = 3,   // Lunar/Solar + All planets perturbation using JPL ephemeris file 
         LSPERT_BIG       = 4,   // Lunar/Solar + Jupiter and Venus using JPL ephemeris file (Big Force Planets) 
         LSPERT_MED       = 5,   // Lunar/Solar + Jupiter, Venus, Saturn, Mars, and Mercury using JPL ephemeris file (Big and Medium Force Planets) 
         LSPERT_SMA       = 6;   // Lunar/Solar + All planets perturbation except Pluto using JPL ephemeris file (Big, Medium, and Small Force Planets)
      
      //*******************************************************************************   
         
      // State Transition Matrix Types
      public static int  
         STMTYPE_UVW       = 1,     // UVW state transition matrix
         STMTYPE_XYZ       = 2,     // Cartesian state transition matrix
         STMTYPE_EQNX      = 3;     // Equinoctial state transition matrix
         
      // Different output coordinate systems options for pos/vel/acc
      public static int  
         SPCOORD_ECI      = 1,      // Output coordinate systems in TEME of Date
         SPCOORD_J2K      = 2;      // Output coordinate systems in MEME of J2K 
         
      // Different drag perturbation models
      public static int  
         DRGMDL_JAC64   =  1,     // Jacchia 64 model
         DRGMDL_JAC70   =  2,     // Jacchia 70 model
         DRGMDL_DCA1    =  3,     // Dynamic Calibration Atmosphere 1 (DCA1/HASDM1) model
         DRGMDL_JBH09   = 40,     // JBH09 model
         DRGMDL_JBHDCA2 = 41,     // JBH09 and Dynamic Calibration Atmosphere 2 (DCA2/HASDM2) 
         DRGMDL_MSISE90 = 90,     // MSISE-90 Model currently in HASDM
         DRGMDL_MSISV21 = 91;     // Latest MSIS model (2.1)
      
      // Indexes of SP 4P card fields
      public static int  
         XF_4P_GEOIDX   = 1,       // Geopotential model to use
         XF_4P_BULGEFLG = 2,       // Earth gravity pertubations flag
         XF_4P_DRAGFLG  = 3,       // Drag pertubations flag 
         XF_4P_RADFLG   = 4,       // Radiation pressure pertubations flag
         XF_4P_LUNSOL   = 5,       // Lunar/Solar pertubations flag
         XF_4P_F10      = 6,       // F10 value
         XF_4P_F10AVG   = 7,       // F10 average value
         XF_4P_AP       = 8,       // Ap value
         XF_4P_TRUNC    = 9,       // Geopotential truncation order/degree/zonals
         XF_4P_CONVERG  = 10,      // Corrector step convergence criterion, exponent of 1/10, default = 10
         XF_4P_OGFLG    = 11,      // Outgassing pertubations flag
         XF_4P_TIDESFLG = 12,      // Solid earth and ocean tide pertubations flag
         XF_4P_INCOORD  = 13,      // Input vector coordinate system
         XF_4P_NTERMS   = 14,      // Nutation terms
         XF_4P_REEVAL   = 15,      // Recompute pertubations at each corrector step
         XF_4P_INTEGCTRL= 16,      // Variable of intergration control
         XF_4P_VARSTEP  = 17,      // Variable step size control
         XF_4P_INITSTEP = 18,      // Initial step size
      
         XF_4P_DCAFILE  = 21,      // DCA file name
         XF_4P_FLUXFILE = 22,      // Solar flux file name
         XF_4P_GEOFILE  = 23,      // Geopotential file name
         XF_4P_JPLFILE  = 24,      // JPL file name
         XF_4P_JPLSTART = 25,      // JPL start time
         XF_4P_JPLSTOP  = 26;      // JPL stop time
      
         //XF_4P_PLANETS  = 27,      // Sets perturbations from all planets to on
         //XF_4P_MERCURY  = 28,      // Sets perturbation from Mercury to on
         //XF_4P_VENUS    = 29,      // Sets perturbation from Venus to on
         //XF_4P_MARS     = 30,      // Sets perturbation from Mars to on
         //XF_4P_JUPITER  = 31,      // Sets perturbation from Jupiter to on
         //XF_4P_SATURN   = 32,      // Sets perturbation from Saturn to on
         //XF_4P_URANUS   = 33,      // Sets perturbation from Uranus to on
         //XF_4P_NEPTUNE  = 34,      // Sets perturbation from Neptune to on
         //XF_4P_PLUTO    = 35       // Sets perturbation from Pluto to on
         
      //*******************************************************************************
      
      // Indexes of SP application control (preference) parameters
      public static int  
         XF_SPAPP_GEODIR   = 1,     // Geopotential directory path
         XF_SPAPP_BUFSIZE  = 2,     // Buffer size
         XF_SPAPP_RUNMODE  = 3,     // Run mode, see IDX_RUNMODE_ for available options
         XF_SPAPP_SAVEPART = 4,     // Save partials data
         XF_SPAPP_SPECTR   = 5,     // Specter compatibility mode
         XF_SPAPP_CONSIDER = 6,     // Consider parameter
         XF_SPAPP_DECAYALT = 7,     // Decay altitude
         XF_SPAPP_OUTCOORD = 8,     // Output coordinate system
         XF_SPAPP_VCMOPT   = 9,     // VCM additional options
         XF_SPAPP_MAXDAYS  =10;     // Maximum number of days before/after epoch a satellite can be propagated to
      
      //*******************************************************************************
      
      // Indexes of data fields of an initialized SP satellite
      public static int  
         XF_SPSAT_SATNUM  = 1,     // Satellite number I5
         XF_SPSAT_DS50UTC = 2,     // Satellite's epoch time in days since 1950, UTC 
         XF_SPSAT_DS50TAI = 3,     // Satellite's epoch time in days since 1950, TAI
         XF_SPSAT_MU      = 4,     // Mu value 
         XF_SPSAT_HASCOV  = 5,     // Covariance Matrix flag
         XF_SPSAT_INTMODE = 6,     // Integration mode
         XF_SPSAT_NTERMS  = 7,     // Nutation terms
         XF_SPSAT_SPECTR  = 8;     // Specter compatibility mode
      
      //*******************************************************************************
      
      //// Indexes of Planetary Control
      //public static int:  
      //   XAI_PLANET_MERCURY = 1,   // 0 = off, 1 = on
      //   XAI_PLANET_VENUS   = 2,   // 0 = off, 1 = on
      //   XAI_PLANET_EARTH   = 3,   // Not used
      //   XAI_PLANET_MARS    = 4,   // 0 = off, 1 = on
      //   XAI_PLANET_JUPITER = 5,   // 0 = off, 1 = on
      //   XAI_PLANET_SATRUN  = 6,   // 0 = off, 1 = on
      //   XAI_PLANET_URANUS  = 7,   // 0 = off, 1 = on
      //   XAI_PLANET_NEPTUNE = 8,   // 0 = off, 1 = on
      //   XAI_PLANET_PLUTO   = 9,   // 0 = off, 1 = on
      //   XAI_PLANET_SIZE    = 9    // Size of array
      //
      ////*******************************************************************************
         
      // Different time types for passing to SpPropAll
      public static int  
         SP_TIMETYPE_MSE      = 0,   // propagation time is in minutes since epoch
         SP_TIMETYPE_DS50UTC  = 1;   // propagation time is in days since 1950, UTC
         
      
      // Sp propagated data   
      public static int     
         XA_SPOUT_UTC       =  0,    // Propagation time in days since 1950, UTC
         XA_SPOUT_MSE       =  1,    // Propagation time in minutes since the satellite's epoch time   
         XA_SPOUT_UT1       =  2,    // Propagation time in days since 1950, UT1
         XA_SPOUT_TAI       =  3,    // Propagation time in days since 1950, TAI
         XA_SPOUT_ET        =  4,    // Propagation time in days since 1950, ET
         XA_SPOUT_REVNUM    =  5,    // Revolution number
         XA_SPOUT_NTERMS    =  6,    // Number of nutation terms
         XA_SPOUT_ISSPECTR  =  7,    // Spectr compatible mode (0=not compatible, 1=compatible)
         XA_SPOUT_HASCOVMTX =  8,    // Has input covariance matrix (0=no, 1=yes)
      
         XA_SPOUT_J2KPOSX   = 10,    // J2K position X (km)
         XA_SPOUT_J2KPOSY   = 11,    // J2K position Y (km)
         XA_SPOUT_J2KPOSZ   = 12,    // J2K position Z (km)
         XA_SPOUT_J2KVELX   = 13,    // J2K velocity X (km/s)
         XA_SPOUT_J2KVELY   = 14,    // J2K velocity Y (km/s)
         XA_SPOUT_J2KVELZ   = 15,    // J2K velocity Z (km/s)
         XA_SPOUT_ECIPOSX   = 16,    // ECI position X (km)
         XA_SPOUT_ECIPOSY   = 17,    // ECI position Y (km)
         XA_SPOUT_ECIPOSZ   = 18,    // ECI position Z (km)
         XA_SPOUT_ECIVELX   = 19,    // ECI velocity X (km/s)
         XA_SPOUT_ECIVELY   = 20,    // ECI velocity Y (km/s)
         XA_SPOUT_ECIVELZ   = 21,    // ECI velocity Z (km/s)
         XA_SPOUT_LAT       = 22,    // Geodetic latitude (deg)
         XA_SPOUT_LON       = 23,    // Geodetic longitude (deg)
         XA_SPOUT_HEIGHT    = 24,    // Height above geoid (km)
      
         XA_SPOUT_COVTYPE   = 30,    // Covariance matrix type, if available:  1=UVW, 2=ECI_DATE, 3=EQNX_DATE, 12=ECI_J2K, 13=EQNX_J2K
         XA_SPOUT_COVMTX    = 31,    // 6 by 6 covariance matrix (31-66)
         
         XA_SPOUT_J2KACCX   = 70,    // J2K acceleration X (km/s^2)
         XA_SPOUT_J2KACCY   = 71,    // J2K acceleration Y (km/s^2)
         XA_SPOUT_J2KACCZ   = 72,    // J2K acceleration Z (km/s^2)
         XA_SPOUT_ECIACCX   = 73,    // ECI acceleration X (km/s^2)
         XA_SPOUT_ECIACCY   = 74,    // ECI acceleration Y (km/s^2)
         XA_SPOUT_ECIACCZ   = 75,    // ECI acceleration Z (km/s^2)
         
         XA_SPOUT_SIZE      = 128;     
         
      // Sp extended propagation data   
      public static int     
         XA_SPEXT_UTC       =  0,    // Propagation time in days since 1950, UTC
         XA_SPEXT_MSE       =  1,    // Propagation time in minutes since the satellite's epoch time   
         XA_SPEXT_UT1       =  2,    // Propagation time in days since 1950, UT1
         XA_SPEXT_TAI       =  3,    // Propagation time in days since 1950, TAI
         XA_SPEXT_ET        =  4,    // Propagation time in days since 1950, ET
         XA_SPEXT_REVNUM    =  5,    // Revolution number
         XA_SPEXT_NTERMS    =  6,    // Number of nutation terms
         XA_SPEXT_ISSPECTR  =  7,    // Spectr compatible mode (0=not compatible, 1=compatible)
         XA_SPEXT_HASCOVMTX =  8,    // Has input covariance matrix (0=no, 1=yes)
      
         XA_SPEXT_COORD     = 10,    // Output coordSysinate of pos/vel/accel: 1=TEME of Date, 2= MEME of J2K
         XA_SPEXT_POSX      = 11,    // position X (km)
         XA_SPEXT_POSY      = 12,    // position Y (km)
         XA_SPEXT_POSZ      = 13,    // position Z (km)
         XA_SPEXT_VELX      = 14,    // velocity X (km/s)
         XA_SPEXT_VELY      = 15,    // velocity Y (km/s)
         XA_SPEXT_VELZ      = 16,    // velocity Z (km/s)
         XA_SPEXT_ACCX      = 17,    // acceleration X (km/s^2)
         XA_SPEXT_ACCY      = 18,    // acceleration Y (km/s^2)
         XA_SPEXT_ACCZ      = 19,    // acceleration Z (km/s^2)
         
         XA_SPEXT_COVTYPE   = 30,    // Covariance matrix type, if available:  1=UVW, 2=ECI_DATE, 3=EQNX_DATE, 12=ECI_J2K, 13=EQNX_J2K
         XA_SPEXT_COVMTX    = 31,    // 6 by 6 covariance matrix (31-66/1D format - 36 elements)
         
         XA_SPEXT_STMTYPE   = 70,    // State transition matrix type, if available: 2=ECI_Date, 3=Eqnx_DATE
         XA_SPEXT_STM       = 71,    // 6x9 state transition matrix  (71-124/1D format - 54 elements - row major)
         
         XA_SPEXT_SIZE      = 128;     
         
      
      // Different options for generating ephemerides from SP
      public static int  
         SP_EPHEM_ECI   = 1,       // ECI TEME of DATE     - 0: time in days since 1950 UTC, 1-3: pos (km), 4-6: vel (km/sec)
         SP_EPHEM_J2K   = 2;       // MEME of J2K (4 terms)- 0: time in days since 1950 UTC, 1-3: pos (km), 4-6: vel (km/sec) 
         
      
      // Different time types returned by the SP propagator
      public static int     
         XA_TIMETYPES_MSE  = 0,    // Time in minutes since the satellite's epoch time   
         XA_TIMETYPES_UTC  = 1,    // Time in days since 1950, UTC
         XA_TIMETYPES_UT1  = 2,    // Time in days since 1950, UT1
         XA_TIMETYPES_TAI  = 3,    // Time in days since 1950, TAI
         XA_TIMETYPES_ET   = 4,    // Time in days since 1950, ET
         
         XA_TIMETYPES_SIZE = 5;      
         
      // Indexes of SP application control (preference) parameters for individual satellite (thread-safe)
      public static int  
         XA_SPPARMS_BUFSIZE  = 2,     // Buffer size, enter zero if default value is desired
         XA_SPPARMS_RUNMODE  = 3,     // Run mode, see IDX_RUNMODE_? for available options
         XA_SPPARMS_SAVEPART = 4,     // Save partials data, 0=don't save partials, 1=save partials (needed for covariance)
         XA_SPPARMS_SPECTR   = 5,     // Specter compatibility mode
         XA_SPPARMS_CONSIDER = 6,     // Consider parameter
         XA_SPPARMS_DECAYALT = 7,     // Decay altitude
         XA_SPPARMS_VCMOPT   = 9,     // VCM additional options, see VCMOPT_? for available options
         XF_SPPARMS_MAXDAYS  =10,     // Maximum number of days before/after epoch a satellite can be propagated to
         XA_SPPARMS_SIZE     =32; 
         
   }
// ========================= End of auto generated code ==========================
}
