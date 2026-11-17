// This wrapper file was generated automatically by the GenDllWrappers program.
using System.Runtime.InteropServices;



namespace AFSPC.AstroStds.Wrappers
{
   public static partial class Sgp4PropWrapper
   {
      // Provide the path to the dll
      #if WINDOWS
      const string Sgp4PropDll = "sgp4prop";
      #else
      const string Sgp4PropDll = "libsgp4prop.so";
      #endif
      // static constructor
      static Sgp4PropWrapper()
      {
         // Set AS library default path/LD_LIBRARY_PATH environment
         Utilities.Utility.Init();
      }



      /// <summary>
      /// Notes: This function has been deprecated since v9.0. 
      /// Initializes the Sgp4 DLL for use in the program.
      /// 
      /// If this function returns an error, it is recommended that you stop the program immediately.
      /// 
      /// An error will occur if you forget to load and initialize all the prerequisite DLLs, as listed in the DLL Prerequisites section of the accompanying documentation, before using this DLL.
      /// </summary>
      /// <param name="apAddr">The handle that was returned from DllMainInit(). See the documentation for DllMain.dll for details. (in-Long)</param>
      /// <returns>0 if Sgp4Prop.dll is initialized successfully, non-0 if there is an error.</returns>
      [DllImport(Sgp4PropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int Sgp4Init(long apAddr);


      /// <summary>
      /// Returns information about the current version of Sgp4Prop.dll. The information is placed in the string parameter you pass in.
      /// The returned string provides information about the version number, build date, and platform.
      /// </summary>
      /// <param name="infoStr">A string to hold the information about Sgp4Prop.dll. (out-Character[128])</param>
      [DllImport(Sgp4PropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void Sgp4GetInfo(byte[] infoStr);


      /// <summary>
      /// Loads SGP4-related parameters (prediction controls, JPL settings) and SGP4 elsets from a text file
      /// Not available for Web Assembly.
      /// </summary>
      /// <param name="sgp4InputFile">The name of the file containing SGP4-related parameters and SGP4 elsets (in-Character[512])</param>
      /// <returns>0 if the input file is read successfully, non-0 if there is an error</returns>
      [DllImport(Sgp4PropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int Sgp4LoadFileAll(string sgp4InputFile);


      /// <summary>
      /// Saves currently loaded SGP4-related parameters (SGP4 application controls, prediction controls, integration controls) to a file
      /// The purpose of this function is to save the current SGP4-related settings, usually used in GUI applications, for future use.
      /// Not available for Web Assembly.
      /// </summary>
      /// <param name="sgp4File">The name of the file in which to save the settings (in-Character[512])</param>
      /// <param name="saveMode">Sgp4ecifies whether to create a new file or append to an existing one (0 = create, 1= append) (in-Integer)</param>
      /// <param name="saveForm">Sgp4ecifies the mode in which to save the file (0 = text format, 1 = not yet implemented, reserved for future) (in-Integer)</param>
      /// <returns>0 if the data is successfully saved to the file, non-0 if there is an error</returns>
      [DllImport(Sgp4PropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int Sgp4SaveFile(string sgp4File, int saveMode, int saveForm);


      /// <summary>
      /// Initializes an SGP4 satellite from an SGP or SGP4 TLE.
      /// Internally, when this function is called, Tle.dll's set of TLEs is searched for the provided satKey. If found, the associated TLE data will be used to create an SGP4 satellite which then will be added to Sgp4Prop.dll's set of satellites. Subsequent calls to propagate this satellite will use the data in this set to compute the satellite's new state.
      /// 
      /// This routine should be called once for each satellite you wish to propagate before propagation begins, or any time the associated data that is stored by Tle.dll is changed.
      /// 
      /// The call to this routine needs to be placed before any calls to the SGP4 propagator routines (Sgp4PropMse(), Sgp4PropDs50UTC(), etc.).
      /// </summary>
      /// <param name="satKey">The satellite's unique key. This key will have been returned by one of the routines in Tle.dll. (in-Long)</param>
      /// <returns>0 if the satellite is successfully initialized and added to Sgp4Prop.dll's set of satellites, non-0 if there is an error.</returns>
      [DllImport(Sgp4PropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int Sgp4InitSat(long satKey);


      /// <summary>
      /// Checks to see if this satellite is initiazlied
      /// </summary>
      /// <param name="satKey">The satellite's unique key. This key will have been returned by one of the routines in Tle.dll. (in-Long)</param>
      /// <returns>1 if the satellite is initialized or 0 if it isn't.</returns>
      [DllImport(Sgp4PropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int Sgp4IsSatInit(long satKey);


      /// <summary>
      /// Removing a satellite from the propagator's set of satellites does not affect the corresponding TLE data loaded by calls to routines in Tle.dll.
      /// </summary>
      /// <param name="satKey">The satellite's unique key. (in-Long)</param>
      /// <returns>0 if the satellite is removed successfully, non-0 if there is an error.</returns>
      [DllImport(Sgp4PropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int Sgp4RemoveSat(long satKey);


      /// <summary>
      /// Removes all currently loaded satellites from memory.
      /// Calling this function removes all satellites from the set maintained by Sgp4Prop.dll. However, the TLE data loaded by Tle.dll is unaffected by this function.
      /// </summary>
      /// <returns>0 if all satellites are removed successfully from memory, non-0 if there is an error.</returns>
      [DllImport(Sgp4PropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int Sgp4RemoveAllSats();


      /// <summary>
      /// Returns the number of GP objects currently created. 
      /// </summary>
      /// <returns>The number of GP objects currently created.</returns>
      [DllImport(Sgp4PropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int Sgp4GetCount();


      /// <summary>
      /// Propagates a satellite, represented by the satKey, to the time expressed in minutes since the satellite's epoch time. 
      /// The resulting data about the satellite is placed in the various reference parameters.
      /// It is the users' responsibility to decide what to do with the returned value. For example, if the users want to check for decay or low altitude, they can put that logic into their own code.
      /// 
      /// This function can be called in random time requests.
      /// The following cases will result in an error:
      /// 
      /// Semi major axis A 1.0D6
      /// Eccentricity E >= 1.0 or E 
      /// Mean anomaly MA>=1.0D10
      /// Hyperbolic orbit E2>= 1.0
      /// satKey doesn't exist in the set of loaded satellites
      /// FK model not set to FK5
      /// 
      /// </summary>
      /// <param name="satKey">The satellite's unique key. (in-Long)</param>
      /// <param name="mse">The time to propagate to, specified in minutes since the satellite's epoch time. (in-Double)</param>
      /// <param name="ds50UTC">Resulting time in days since 1950, UTC. (out-Double)</param>
      /// <param name="pos">Resulting ECI position vector (km) in True Equator and Mean Equinox of Epoch. (out-Double[3])</param>
      /// <param name="vel">Resulting ECI velocity vector (km/s) in True Equator and Mean Equinox of Epoch. (out-Double[3])</param>
      /// <param name="llh">Resulting geodetic latitude (deg), longitude(deg), and height (km). (out-Double[3])</param>
      /// <returns>0 if the propagation is successful, non-0 if there is an error (see GP_ERR_?).</returns>
      [DllImport(Sgp4PropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int Sgp4PropMse(long satKey, double mse, out double ds50UTC, double[] pos, double[] vel, double[] llh);


      /// <summary>
      /// Propagates a satellite, represented by the satKey, to the time expressed in days since 1950, UTC. 
      /// The resulting data about the satellite is placed in the pos (position), vel (velocity), and llh (Lat/Lon/Height) parameters.
      /// It is the users' responsibility to decide what to do with the returned value. For example, if the users want to check for decay or low altitude, they can put that logic into their own code.
      /// The following cases will result in an error:
      /// 
      /// Semi major axis A 1.0D6
      /// Eccentricity E >= 1.0 or E 
      /// Mean anomaly MA>=1.0D10
      /// Hyperbolic orbit E2>= 1.0
      /// satKey doesn't exist in the set of loaded satellites
      /// GEO model not set to WGS-72 and/or FK model not set to FK5
      /// 
      /// </summary>
      /// <param name="satKey">The unique key of the satellite to propagate. (in-Long)</param>
      /// <param name="ds50UTC">The time to propagate to, expressed in days since 1950, UTC. (in-Double)</param>
      /// <param name="mse">Resulting time in minutes since the satellite's epoch time. (out-Double)</param>
      /// <param name="pos">Resulting ECI position vector (km) in True Equator and Mean Equinox of Epoch. (out-Double[3])</param>
      /// <param name="vel">Resulting ECI velocity vector (km/s) in True Equator and Mean Equinox of Epoch. (out-Double[3])</param>
      /// <param name="llh">Resulting geodetic latitude (deg), longitude(deg), and height (km). (out-Double[3])</param>
      /// <returns>0 if the propagation is successful, non-0 if there is an error (see GP_ERR_?).</returns>
      [DllImport(Sgp4PropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int Sgp4PropDs50UTC(long satKey, double ds50UTC, out double mse, double[] pos, double[] vel, double[] llh);


      /// <summary>
      /// Propagates a satellite, represented by the satKey, to the time expressed in days since 1950, UTC. 
      /// The resulting data about the satellite is placed in the pos (position), vel (velocity) parameters.
      /// </summary>
      /// <param name="satKey">The unique key of the satellite to propagate. (in-Long)</param>
      /// <param name="ds50UTC">The time to propagate to, expressed in days since 1950, UTC. (in-Double)</param>
      /// <param name="pos">Resulting ECI position vector (km) in True Equator and Mean Equinox of Epoch. (out-Double[3])</param>
      /// <param name="vel">Resulting ECI velocity vector (km/s) in True Equator and Mean Equinox of Epoch. (out-Double[3])</param>
      /// <returns>0 if the propagation is successful, non-0 if there is an error (see GP_ERR_?).</returns>
      [DllImport(Sgp4PropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int Sgp4PropDs50UtcPosVel(long satKey, double ds50UTC, double[] pos, double[] vel);


      /// <summary>
      /// Propagates a satellite, represented by the satKey, to the time expressed in days since 1950, UTC. 
      /// Only the geodetic information is returned by this function.
      /// It is the users' responsibility to decide what to do with the returned value. For example, if the users want to check for decay or low altitude, they can put that logic into their own code.
      /// 
      /// This function is similar to Sgp4PropDs50UTC but returns only LLH.  This function is designed especially for applications which plot ground traces.
      /// The following cases will result in an error:
      /// 
      /// Semi major axis A 1.0D6
      /// Eccentricity E >= 1.0 or E 
      /// Mean anomaly MA>=1.0D10
      /// Hyperbolic orbit E2>= 1.0
      /// satKey doesn't exist in the set of loaded satellites
      /// GEO model not set to WGS-72 and/or FK model not set to FK5
      /// 
      /// </summary>
      /// <param name="satKey">The unique key of the satellite to propagate. (in-Long)</param>
      /// <param name="ds50UTC">The time to propagate to, expressed in days since 1950, UTC. (in-Double)</param>
      /// <param name="llh">Resulting geodetic latitude (deg), longitude(deg), and height (km). (out-Double[3])</param>
      /// <returns>0 if the propagation is successful, non-0 if there is an error (see GP_ERR_?).</returns>
      [DllImport(Sgp4PropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int Sgp4PropDs50UtcLLH(long satKey, double ds50UTC, double[] llh);


      /// <summary>
      /// Propagates a satellite, represented by the satKey, to the time expressed in days since 1950, UTC. 
      /// Only the ECI position vector is returned by this function.
      /// It is the users' responsibility to decide what to do with the returned value. For example, if the users want to check for decay or low altitude, they can put that logic into their own code.
      /// 
      /// This function is similar to Sgp4PropDs50UTC but returns only ECI position vector.  This function is designed especially for applications which plot satellite position in 3D.
      /// The following cases will result in an error:
      /// 
      /// Semi major axis A 1.0D6
      /// Eccentricity E >= 1.0 or E 
      /// Mean anomaly MA>=1.0D10
      /// Hyperbolic orbit E2>= 1.0
      /// satKey doesn't exist in the set of loaded satellites
      /// GEO model not set to WGS-72 and/or FK model not set to FK5
      /// 
      /// </summary>
      /// <param name="satKey">The unique key of the satellite to propagate. (in-Long)</param>
      /// <param name="ds50UTC">The time to propagate to, expressed in days since 1950, UTC. (in-Double)</param>
      /// <param name="pos">Resulting ECI position vector (km) in True Equator and Mean Equinox of Epoch. (out-Double[3])</param>
      /// <returns>0 if the propagation is successful, non-0 if there is an error (see GP_ERR_?).</returns>
      [DllImport(Sgp4PropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int Sgp4PropDs50UtcPos(long satKey, double ds50UTC, double[] pos);


      /// <summary>
      /// Retrieves propagator's precomputed results. This function can be used to obtain results from 
      /// a propagation which are not made available through calls to the propagation functions themselves.
      /// 
      /// See example in Sgp4PropMse or Sgp4PropDs50UTC.
      /// 
      /// This function should be called immediately after a successful call to Sgp4PropMse() or Sgp4PropDs50UTC() to retrieve the desired values.
      /// 
      /// It is the caller's responsibility to ensure that the array passed in the destArray parameter is large enough to hold the requested values. The required size can be found by looking at the destArray size column of the table below describing valid index values.
      /// 
      /// The destArray Arrangement column lists the order of the elements in the array. It is not necessarily the subscript of the element in the array since this is language-dependent. For example, in C/C++ the first element in every array is the zero-subscripted element. In other programming languages, the subscript of the first element is 1.
      /// 
      /// Note: This function is not thread safe, please use Sgp4PropAll() instead 
      /// 
      /// The table below shows the values for the xf_Sgp4Out parameter:
      /// 
      /// table
      /// 
      /// Index
      /// Index Interpretation
      /// DestArray size
      /// DestArray Arrangement
      /// 
      /// 1Revolution number1Revolution number (based on the Osculating Keplerian
      /// Elements)
      /// 2Nodal Apogee Perigee3nodal period (minutes)apogee
      /// (km)perigee (km)
      /// 3Mean Keplerian Elements6semi-major axis (km)eccentricity
      /// (unitless)inclination (degree)mean anomaly (degree)right ascension of the ascending node
      /// (degree)argument of perigee (degree)
      /// 4Osculating Keplerian Elements6Same as Mean Keplerian Elements
      /// 
      /// </summary>
      /// <param name="satKey">The unique key of the satellite for which to retrieve results. (in-Long)</param>
      /// <param name="xf_Sgp4Out">Specifies which propagator outputs to retrieve. (in-Integer)</param>
      /// <param name="destArr">Array to receive the resulting propagator outputs. (out-Double[*])</param>
      /// <returns>0 if the requested information is retrieved successfully, non-0 if there is an error.</returns>
      [DllImport(Sgp4PropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int Sgp4GetPropOut(long satKey, int xf_Sgp4Out, double[] destArr);


      /// <summary>
      /// Propagates a satellite, represented by the satKey, to the time expressed in either minutes since epoch or days since 1950, UTC. 
      /// All propagation data is returned by this function.
      /// </summary>
      /// <param name="satKey">The unique key of the satellite to propagate. (in-Long)</param>
      /// <param name="timeType">The propagation time type: 0 = minutes since epoch, 1 = days since 1950, UTC (in-Integer)</param>
      /// <param name="timeIn">The time to propagate to, expressed in either minutes since epoch or days since 1950, UTC. (in-Double)</param>
      /// <param name="xa_Sgp4Out">The array that stores all Sgp4 propagation data (see XA_SGP4OUT_?) (out-Double[64])</param>
      /// <returns>0 if the propagation is successful, non-0 if there is an error (see GP_ERR_?)</returns>
      [DllImport(Sgp4PropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int Sgp4PropAll(long satKey, int timeType, double timeIn, double[] xa_Sgp4Out);


      /// <summary>
      /// Converts osculating position and velocity vectors to a set of mean Keplerian SGP4 elements.
      /// The new position and velocity vectors are the results of using SGP4 propagator to propagate the computed sgp4MeanKep to the time specified in year and day of epoch time. 
      /// They should be closely matched with the input osculating position and velocity vectors.
      /// 
      /// The mean Keplerian elements are SGP4's Brouwer mean motion not SGP's Kozai mean motion.
      /// Notes: Even if the function fails, the less acurate results may still be availalbe 
      /// </summary>
      /// <param name="yr">2 or 4 digit year of the epoch time. (in-Integer)</param>
      /// <param name="day">Day of year of the epoch time. (in-Double)</param>
      /// <param name="pos">Input osculating position vector (km). (in-Double[3])</param>
      /// <param name="vel">Input osculating velocity vector (km/s). (in-Double[3])</param>
      /// <param name="posNew">Resulting position vector (km) propagated from the input xa_kep. (out-Double[3])</param>
      /// <param name="velNew">Resulting velocity vector (km/s) propagated from the input xa_kep. (out-Double[3])</param>
      /// <param name="xa_kep">Resulting set of Sgp4 mean Keplerian elements (see XA_KEP_?). (out-Double[6])</param>
      /// <returns>0 if the conversion is successful, non-0 if there is an error.</returns>
      [DllImport(Sgp4PropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int Sgp4PosVelToKep(int yr, double day, double[] pos, double[] vel, double[] posNew, double[] velNew, double[] xa_kep);


      /// <summary>
      /// Converts osculating position and velocity vectors to TLE array - allows bstar/bterm, drag values to be used in the conversion if desired
      /// The function is similar to Sgp4PosVelToKep but allows the user to specify agom (XP mode) and bstar/bterm values, if desired, to be used in solving for the new Keplerian elements.
      /// 
      /// The updated elements returned in the xa_tle array is of type SGP and the mean motion is Kozai mean motion.
      /// Notes: Even if the function fails, the less acurate results may still be availalbe 
      /// </summary>
      /// <param name="pos">Input osculating position vector (km). (in-Double[3])</param>
      /// <param name="vel">Input osculating velocity vector (km/s). (in-Double[3])</param>
      /// <param name="xa_tle">Input/Output array containing TLE's numerical fields (see XA_TLE_?); required data include epoch, ephemType, drag, bstar/bterm (inout-Double[64])</param>
      /// <returns>0 if the conversion is successful, non-0 if there is an error.</returns>
      [DllImport(Sgp4PropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int Sgp4PosVelToTleArr(double[] pos, double[] vel, double[] xa_tle);


      /// <summary>
      /// Reepochs a loaded TLE, represented by the satKey, to a new epoch.
      /// </summary>
      /// <param name="satKey">The unique key of the satellite to reepoch. (in-Long)</param>
      /// <param name="reEpochDs50UTC">The new epoch, express in days since 1950, UTC. (in-Double)</param>
      /// <param name="line1Out">A string to hold the first line of the reepoched TLE. (out-Character[512])</param>
      /// <param name="line2Out">A string to hold the second line of the reepoched TLE. (out-Character[512])</param>
      /// <returns>0 if the reepoch is successful, non-0 if there is an error.</returns>
      [DllImport(Sgp4PropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int Sgp4ReepochTLE(long satKey, double reEpochDs50UTC, byte[] line1Out, byte[] line2Out);


      /// <summary>
      /// Reepochs a loaded TLE, represented by the satKey, to a new epoch in Csv format.
      /// </summary>
      /// <param name="satKey">The unique key of the satellite to reepoch. (in-Long)</param>
      /// <param name="reEpochDs50UTC">The new epoch, express in days since 1950, UTC. (in-Double)</param>
      /// <param name="csvLine">A string to hold the reepoched CSV. (out-Character[512])</param>
      /// <returns>0 if the reepoch is successful, non-0 if there is an error.</returns>
      [DllImport(Sgp4PropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int Sgp4ReepochCsv(long satKey, double reEpochDs50UTC, byte[] csvLine);


      /// <summary>
      /// Sets DIRECTORY/FOLDER path to the Sgp4 Open License file if the license file doesn't exist in the current folder or those specified in PATH/LD_LIBRARY_PATH environment
      /// Note: This function has been revised since v9.6. It's only needed if the "SGP4_Open_License.txt" isn't located in current folder or those folders specified in PATH/LD_LIBRARY_PATH environment.
      ///       Also, this requires a directory/folder, not a file
      /// </summary>
      /// <param name="licFilePath">The file path to the Sgp4 Open License file (in-Character[512])</param>
      [DllImport(Sgp4PropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void Sgp4SetLicFilePath(string licFilePath);


      /// <summary>
      /// Gets the current path to the Sgp4 Open License file
      /// Note: This function has been revised since v9.6. It's only needed if the "SGP4_Open_License.txt" isn't located in current folder or those folders specified in PATH/LD_LIBRARY_PATH environment
      /// </summary>
      /// <param name="licFilePath">The file path to the Sgp4 Open License file (out-Character[512])</param>
      [DllImport(Sgp4PropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void Sgp4GetLicFilePath(byte[] licFilePath);


      /// <summary>
      /// Generates ephemerides for the input satellite, represented by its satKey, for the specified time span and step size
      /// Notes: if arrSize isn't big enough to store all the ephemeris points, the function will exit when the ephemArr reaches
      /// that many points (arrSize) and the errCode is set to IDX_ERR_WARN
      /// </summary>
      /// <param name="satKey">The unique key of the satellite to generate ephemerides. (in-Long)</param>
      /// <param name="startTime">Start time expressed in days since 1950, UTC. (in-Double)</param>
      /// <param name="endTime">End time expressed in days since 1950, UTC. (in-Double)</param>
      /// <param name="stepSize">Step size in minutes (static); enter predefine negative values (DYN_SS_?) to request dynamic step size (in-Double)</param>
      /// <param name="sgp4_ephem">Output ephemeris type 1=ECI, 2=J2K. (in-Integer)</param>
      /// <param name="arrSize">Size of input ephemArr (in-Integer)</param>
      /// <param name="ephemArr">Output ephemerides - 0: time in days since 1950 UTC, 1-3: pos (km), 4-6: vel (km/sec) (out-Double[*, 7])</param>
      /// <param name="genEphemPts">Actual number of ephemeris points generated (always &le; arrSize) (out-Integer)</param>
      /// <returns>0 if the propagation is successful, non-0 if there is an error (see GP_ERR_?).</returns>
      [DllImport(Sgp4PropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int Sgp4GenEphems(long satKey, double startTime, double endTime, double stepSize, int sgp4_ephem, int arrSize, double[,] ephemArr, out int genEphemPts);


      /// <summary>
      /// Generates ephemerides for the input TLE - in an array format - for the specified time span and step size (OS - in One Step)
      /// Notes: 
      /// - This function takes in TLE data directly and doesn't need to go through loading/geting satKey/initializing steps
      /// - if arrSize isn't big enough to store all the ephemeris points, the function will exit when the ephemArr reaches
      ///   that many points (arrSize) and the errCode is set to IDX_ERR_WARN
      /// </summary>
      /// <param name="xa_tle">Input array containing TLE's numerical fields (see XA_TLE_?) (in-Double[64])</param>
      /// <param name="startTime">Start time expressed in days since 1950, UTC. (in-Double)</param>
      /// <param name="endTime">End time expressed in days since 1950, UTC. (in-Double)</param>
      /// <param name="stepSize">Step size in minutes (static); enter predefine negative values (DYN_SS_?) to request dynamic step size (in-Double)</param>
      /// <param name="sgp4_ephem">Output ephemeris type 1=ECI, 2=J2K. (in-Integer)</param>
      /// <param name="arrSize">Size of input ephemArr (in-Integer)</param>
      /// <param name="ephemArr">Output ephemerides - 0: time in days since 1950 UTC, 1-3: pos (km), 4-6: vel (km/sec) (out-Double[*, 7])</param>
      /// <param name="genEphemPts">Actual number of ephemeris points generated (always &le; arrSize) (out-Integer)</param>
      /// <returns>0 if the propagation is successful, non-0 if there is an error (see GP_ERR_?).</returns>
      [DllImport(Sgp4PropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int Sgp4GenEphems_OS(double[] xa_tle, double startTime, double endTime, double stepSize, int sgp4_ephem, int arrSize, double[,] ephemArr, out int genEphemPts);


      /// <summary>
      /// Propagates all input satellites, represented by their satKeys, to the time expressed in days since 1950, UTC. 
      /// </summary>
      /// <param name="satKeys">The satellite keys of all input satellites (in-Long[*])</param>
      /// <param name="numOfSats">The total number of satellites (in-Integer)</param>
      /// <param name="ds50UTC">The time to propagate all satelllites to, expressed in days since 1950, UTC. (in-Double)</param>
      /// <param name="ephemArr">0-2: pos (km), 3-5: vel (km/sec) (out-Double[*, 6])</param>
      /// <returns>0 if the propagation is successful, non-0 if there is an error.</returns>
      [DllImport(Sgp4PropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int Sgp4PropAllSats(long[] satKeys, int numOfSats, double ds50UTC, double[,] ephemArr);


      /// <summary>
      /// Provides the native XP equinoctial elements and rates at given time
      /// </summary>
      /// <param name="satKey">The unique key of the satellite to reepoch. (in-Long)</param>
      /// <param name="ds50UTC">The new epoch, express in days since 1950, UTC. (in-Double)</param>
      /// <param name="xa_eqnx">Equinoctial Elements (subtract thetaG from Longitude) at reEpoch time (out-Double[6])</param>
      /// <param name="xa_eqnx_dot">Equinoctial Element rates (subtract earth rotation rate from Longitude Rate) at reEpoch time (out-Double[6])</param>
      /// <returns>0 if the reepoch is successful, non-0 if there is an error.</returns>
      [DllImport(Sgp4PropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int XpGetNativeElts(long satKey, double ds50UTC, double[] xa_eqnx, double[] xa_eqnx_dot);


      /// <summary>
      /// Reepochs to a csv and provides the native XP equinoctial elements and rates
      /// </summary>
      /// <param name="satKey">Input satKey (in-Long)</param>
      /// <param name="reEpochDs50UTC">The new epoch, express in days since 1950, UTC. (in-Double)</param>
      /// <param name="csvLine">A string to hold the reepoched CSV. (out-Character[512])</param>
      /// <param name="xa_eqnx">Equinoctial Elements (subtract thetaG from Longitude) at reEpoch time (out-Double[6])</param>
      /// <param name="xa_eqnx_dot">Equinoctial Element rates (subtract earth rotation rate from Longitude Rate) at reEpoch time (out-Double[6])</param>
      /// <returns>0 if the propagation is successful, non-0 if there is an error.</returns>
      [DllImport(Sgp4PropDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int XpReepochGetNativeElts(long satKey, double reEpochDs50UTC, byte[] csvLine, double[] xa_eqnx, double[] xa_eqnx_dot);
      // GP types (TLE ephemeris types) - They are different than ELTTYPE
      public static int  
         GPTYPE_SGP4 =  0,    // TLE SGP4 gpType (Brouwer mean motion)
         GPTYPE_PPT3 =  3,    // TLE PPT3 gpType (Brouwer mean motion)
         GPTYPE_XP   =  4,    // TLE SGP4-XP gpType (Brouwer mean motion)
         GPTYPE_NONE = 99;    // Not a GP type 
      
      // Different return values of errCode from Sgp4 propagation
      public static int  
         GP_ERR_NONE        = 0,    // SGP4 propagates successfully
         GP_ERR_BADFK       = 1,    // Bad FK model (FK5 must be selected)
         GP_ERR_ANEGATIVE   = 2,    // A is negative
         GP_ERR_ATOOLARGE   = 3,    // A is to large
         GP_ERR_EHYPERPOLIC = 4,    // Eccentricity is hyperbolic
         GP_ERR_ENEGATIVE   = 5,    // Eccentricity is negative
         GP_ERR_MATOOLARGE  = 6,    // Mean anomaly is too large
         GP_ERR_E2TOOLARGE  = 7,    // e**2 is too large
         GP_ERR_INCOUTOFBND = 8,    // Inclination out of bounds
         GP_ERR_MNMOTNEG    = 9,    // Mean motion is negative 
         GP_ERR_DECAY       =10,    // Satellite decayed
         GP_ERR_UNKNOWNTYPE =99;    // Unknown GP element type
      
      // Different time types for passing to Sgp4PropAll
      public static int  
         SGP4_TIMETYPE_MSE      = 0,   // propagation time is in minutes since epoch
         SGP4_TIMETYPE_DS50UTC  = 1;   // propagation time is in days since 1950, UTC
         
      // Sgp4 propagated output fields
      public static int  
         XF_SGP4OUT_REVNUM       = 1,    // Revolution number
         XF_SGP4OUT_NODAL_AP_PER = 2,    // Nodal period, apogee, perigee
         XF_SGP4OUT_MEAN_KEP     = 3,    // Mean Keplerian
         XF_SGP4OUT_OSC_KEP      = 4;    // Osculating Keplerian
         
      // Sgp4 propagated data
      public static int  
         XA_SGP4OUT_DS50UTC      =  0,   // Propagation time in days since 1950, UTC
         XA_SGP4OUT_MSE          =  1,   // Propagation time in minutes since the satellite's epoch time
         XA_SGP4OUT_POSX         =  2,   // ECI X position (km) in True Equator and Mean Equinox of Epoch
         XA_SGP4OUT_POSY         =  3,   // ECI Y position (km) in True Equator and Mean Equinox of Epoch
         XA_SGP4OUT_POSZ         =  4,   // ECI Z position (km) in True Equator and Mean Equinox of Epoch
         XA_SGP4OUT_VELX         =  5,   // ECI X velocity (km/s) in True Equator and Mean Equinox of Epoch
         XA_SGP4OUT_VELY         =  6,   // ECI Y velocity (km/s) in True Equator and Mean Equinox of Epoch
         XA_SGP4OUT_VELZ         =  7,   // ECI Z velocity (km/s) in True Equator and Mean Equinox of Epoch
         XA_SGP4OUT_LAT          =  8,   // Geodetic latitude (deg)
         XA_SGP4OUT_LON          =  9,   // Geodetic longitude (deg)
         XA_SGP4OUT_HEIGHT       = 10,   // Height above geoid (km)
         XA_SGP4OUT_REVNUM       = 11,   // Revolution number
         XA_SGP4OUT_NODALPER     = 12,   // Nodal period (min)
         XA_SGP4OUT_APOGEE       = 13,   // Apogee (km)
         XA_SGP4OUT_PERIGEE      = 14,   // Perigee (km)
         XA_SGP4OUT_MN_A         = 15,   // Mean semi-major axis (km)
         XA_SGP4OUT_MN_E         = 16,   // Mean eccentricity (unitless)
         XA_SGP4OUT_MN_INCLI     = 17,   // Mean inclination (deg)
         XA_SGP4OUT_MN_MA        = 18,   // Mean mean anomaly (deg)
         XA_SGP4OUT_MN_NODE      = 19,   // Mean right ascension of the asending node (deg)
         XA_SGP4OUT_MN_OMEGA     = 20,   // Mean argument of perigee (deg)
         XA_SGP4OUT_OSC_A        = 21,   // Osculating semi-major axis (km)  
         XA_SGP4OUT_OSC_E        = 22,   // Osculating eccentricity (unitless)
         XA_SGP4OUT_OSC_INCLI    = 23,   // Osculating inclination (deg)
         XA_SGP4OUT_OSC_MA       = 24,   // Osculating mean anomaly (deg)
         XA_SGP4OUT_OSC_NODE     = 25,   // Osculating right ascension of the asending node (deg)
         XA_SGP4OUT_OSC_OMEGA    = 26,   // Osculating argument of perigee (deg)
      
         XA_SGP4OUT_SIZE         = 64;
         
      // Different options for generating ephemerides from SGP4
      public static int  
         SGP4_EPHEM_ECI   = 1,       // ECI TEME of DATE     - 0: time in days since 1950 UTC, 1-3: pos (km), 4-6: vel (km/sec)
         SGP4_EPHEM_J2K   = 2;       // MEME of J2K (4 terms)- 0: time in days since 1950 UTC, 1-3: pos (km), 4-6: vel (km/sec) 
         
      
      // Different dynamic step size options
      public static int     
         DYN_SS_BASIC  = -1;    // Use a simple algorithm to determine step size based on satellite's current position
         
      //*******************************************************************************
         
   }
// ========================= End of auto generated code ==========================
}
