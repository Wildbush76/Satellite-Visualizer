// This wrapper file was generated automatically by the GenDllWrappers program.
using System.Runtime.InteropServices;



namespace AFSPC.AstroStds.Wrappers
{
   public static partial class SatStateWrapper
   {
      // Provide the path to the dll
      #if WINDOWS
      const string SatStateDll = "satstate";
      #else
      const string SatStateDll = "libsatstate.so";
      #endif
      // static constructor
      static SatStateWrapper()
      {
         // Set AS library default path/LD_LIBRARY_PATH environment
         Utilities.Utility.Init();
      }



      /// <summary>
      /// Notes: This function has been deprecated since v9.0. 
      /// Initializes SatState DLL for use in the program
      /// If this function returns an error, it is recommended that the users stop the program immediately. 
      /// The error occurs if the users forget to load and initialize all the prerequisite DLLs, 
      /// as listed in the DLL Prerequisite section, before using this DLL.
      /// </summary>
      /// <param name="apAddr">The handle that was returned from DllMainInit() (in-Long)</param>
      /// <returns>0 if SatState.dll is initialized successfully, non-0 if there is an error</returns>
      [DllImport(SatStateDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SatStateInit(long apAddr);


      /// <summary>
      /// Returns information about the current version of SatState DLL. 
      /// The information is placed in the string parameter passed in.
      /// The returned string provides information about the version number, build date, and the platform of the SatState DLL. 
      /// </summary>
      /// <param name="infoStr">A string to hold the information about SatState.dll (out-Character[128])</param>
      [DllImport(SatStateDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void SatStateGetInfo(byte[] infoStr);


      /// <summary>
      /// Loads any orbital element types (TLE's/SPVEC's/VCM's), EXTEPHEM's, and/or propagator controls from an input text file
      /// Internally, if taskMode = 1, this function calls SpProp.SpLoadFile(); 
      /// and if taskMode = 2, this function calls Tle.TleLoadFile(), SpVec.SpVecLoadFile(), Vcm.VcmLoadFile(), ExtEphem.ExtEphLoadFile(); 
      /// if taskMode = 3, both tasks (1 and 2) are executed.
      /// </summary>
      /// <param name="inputFile">The name of the input file to load (in-Character[512])</param>
      /// <param name="xf_Task">Specified task mode: 1=load SP control parameters, 2=load elsets only, 3=both 1 + 2 (in-Integer)</param>
      /// <returns>0 if the input file is read successfully, non-0 if there is an error</returns>
      [DllImport(SatStateDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SatStateLoadFile(string inputFile, int xf_Task);


      /// <summary>
      /// Saves currently loaded orbital element types (TLE's/SPVEC's/VCM's), EXTEPHEM's, and/or propagator controls to a file
      /// The purpose of this function is to save the current SatState-related settings, usually used in GUI applications, for future use.
      /// 
      /// Internally, if taskMode = 1, this function calls SpProp.SpSaveFile(); 
      /// and if taskMode = 2, this function calls Tle.TleSaveFile(), SpVec.SpVecSavedFile(), Vcm.VcmSaveFile(), ExtEphem.ExtEphSaveFile(); 
      /// if taskMode = 3, both tasks (1 and 2) are executed.
      /// </summary>
      /// <param name="outFile">The name of the file in which to save the settings (in-Character[512])</param>
      /// <param name="saveMode">Specifies whether to create a new file or append to an existing one (0 = create, 1= append) (in-Integer)</param>
      /// <param name="saveForm">Specifies the mode in which to save the file (0 = text format, 1 = not yet implemented, reserved for future) (in-Integer)</param>
      /// <param name="xf_Task">Specified task mode: 1=Only save propagator control parameters, 2=Only save orbital elements/external ephemeris data,	3=Save both 1 + 2 (in-Integer)</param>
      /// <returns>0 if the data is successfully saved to the file, non-0 if there is an error</returns>
      [DllImport(SatStateDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SatStateSaveFile(string outFile, int saveMode, int saveForm, int xf_Task);


      /// <summary>
      /// Removes a satellite from the appropriate elset DLL's set of loaded satellites. 
      /// The function will automatically choose the proper set of elsets from which to remove the satellite. 
      /// The choices are: Tle.dll, SpVec.dll, Vcm.dll, or ExtEphem.dll   
      /// If the users enter an invalid satKey (a satKey that does not exist in memory), the function will return a non-zero value indicating an error.
      /// </summary>
      /// <param name="satKey">The unique key of the satellite to be removed (in-Long)</param>
      /// <returns>0 if the satellite is successfully removed from memory, non-0 if there is an error</returns>
      [DllImport(SatStateDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SatStateRemoveSat(long satKey);


      /// <summary>
      /// Removes a satellite's propagator object from the appropriate elset DLL's set of loaded satellites. 
      /// This removes only the propagator object, leaving the Tle, SpVec, Vcm, or ExtEphem. 
      /// The function will automatically choose the proper propagator from which to remove the satellite. 
      /// The choices are: SpProp.dll, Sgp4Prop.dll   
      /// If the users enter an invalid satKey (a satKey that does not exist in memory), the function will return a non-zero value indicating an error.
      /// </summary>
      /// <param name="satKey">The unique key of the satellite to be removed (in-Long)</param>
      /// <returns>0 if the satellite is successfully removed from memory, non-0 if there is an error</returns>
      [DllImport(SatStateDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SatStateRemoveSatProp(long satKey);


      /// <summary>
      /// Removes a satellite from the appropriate sets of loaded satellites (elset dll and propagator dll). 
      /// The function will remove the satellite from the elset DLL's sets as in SatStateRemoveSat() and 
      /// from the appropriate propagator's set of initialized satellites if it exists there.
      /// </summary>
      /// <param name="satKey">The unique key of the satellite to be removed (in-Long)</param>
      /// <returns>0 if the satellite is successfully removed from memory, non-0 if there is an error</returns>
      [DllImport(SatStateDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SatStateRemoveSatAll(long satKey);


      /// <summary>
      /// Removes all satellites from all of the loaded data sets. 
      /// It will remove all satellites from the following sets: Tle, SpVec, Vcm, ExtEphem, Sgp4Prop, and SpProp
      /// </summary>
      /// <returns>0 if all of the data sets are cleared successfully, non-0 if there is an error</returns>
      [DllImport(SatStateDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SatStateRemoveAllSats();


      /// <summary>
      /// Resets propagator settings back to their default values
      /// </summary>
      [DllImport(SatStateDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void SatStateReset();


      /// <summary>
      /// Returns the total number of satellites (TLE's, SPVEC's, VCM's, and EXTEPHEM's) currently loaded in memory
      /// See SatStateGetLoaded for example.
      /// This function is useful for dynamically allocating memory for the array that is passed to the function SatStateGetLoaded.
      /// </summary>
      /// <returns>The total number of currently loaded satellites</returns>
      [DllImport(SatStateDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SatStateGetCount();


      /// <summary>
      /// Retrieves all of the currently loaded satKeys. 
      /// These satKeys can be used to access the internal data for the satellites.
      /// It is recommended that SatStateGetCount() is used to count how many satellites are currently loaded in memory. 
      /// The user can then use this number to dynamically allocate the satKeys array and pass it to this function.
      /// 
      /// If the user prefers to pass a static array to the function, make sure it is big enough to store all the satKeys in memory.
      /// </summary>
      /// <param name="order">Specifies the order in which the satKeys should be returned:	0=ascending order, 1=descending order, 2=order in which the satKeys were loaded in memory (in-Integer)</param>
      /// <param name="satKeys">The array in which to store the satKeys (out-Long[*])</param>
      [DllImport(SatStateDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void SatStateGetLoaded(int order, long[] satKeys);


      /// <summary>
      /// Returns the first satKey that contains the specified satellite number in all sets of loaded satellites. 
      /// These sets will be searched: Tle, SpVec, Vcm, and ExtEphem.
      /// This function is useful when a satellite is used in applications that require only one record for one 
      /// satellite and the applications refer to that satellite by its satellite number. 
      /// However, the Astrodynamic Standard Shared library is only working with satKeys, this function helps to return the associated satKey of that satellite.
      /// </summary>
      /// <param name="satNum">The satellite number to search for (in-Integer)</param>
      /// <returns>The satKey of the matching satellite if one is found, a negative value indicating an error if not</returns>
      [DllImport(SatStateDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern long SatStateNumToKey(int satNum);


      /// <summary>
      /// Returns the first satKey that contains the specified satellite number in all sets of loaded satellites. 
      /// These sets will be searched: Tle, SpVec, Vcm, and ExtEphem.
      /// This function is useful when a satellite is used in applications that require only one record for one 
      /// satellite and the applications refer to that satellite by its satellite number. 
      /// However, the Astrodynamic Standard Shared library is only working with satKeys, this function helps to return the associated satKey of that satellite.
      /// </summary>
      /// <param name="satNum">The satellite number to search for (in-Integer)</param>
      /// <param name="satKey">The satKey of the matching satellite if one is found, a negative value indicating an error if not (out-Long)</param>
      [DllImport(SatStateDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void SatStateNumToKeyML(int satNum, out long satKey);


      /// <summary>
      /// Retrieves the data which is common to all satellite types. 
      /// All common fields are retrieved with a single function call.
      /// The apogee height and perigee height are defined as the distance above an ellipsoid 
      /// created using the earth flattening factor from the selected geopotential model.
      /// Note:  When using SP elsets (TLE type 6, SPVEC, or VCM), calling SatStateGetSatDataAll 
      /// will implicitly call SpInit in order to extract the mu value from the GEO file the elset is tied to.  
      /// The elset must have a valid GEO directory available or an error will be returned.
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="satNum">Satellite number (out-Integer)</param>
      /// <param name="satName">Satellite international designator (out-Character[8])</param>
      /// <param name="eltType">Element type (see ELTTYPE_?) (out-Integer)</param>
      /// <param name="revNum">Revolution number at epoch (out-Integer)</param>
      /// <param name="epochDs50UTC">Epoch time time in days since 1950 UTC (out-Double)</param>
      /// <param name="bField">Ballistic coefficient (m^2/kg) (out-Double)</param>
      /// <param name="elsetNum">Element set number (out-Integer)</param>
      /// <param name="incli">Inclination (deg) (out-Double)</param>
      /// <param name="node">Right ascension of ascending node (deg) (out-Double)</param>
      /// <param name="eccen">Eccentricity (out-Double)</param>
      /// <param name="omega">Argument of perigee (deg) (out-Double)</param>
      /// <param name="mnAnomaly">Mean anomaly (deg) (out-Double)</param>
      /// <param name="mnMotion">Mean motion (rev/day) (out-Double)</param>
      /// <param name="period">Satellite period (min) (out-Double)</param>
      /// <param name="perigeeHt">Satellite perigee height (km) above the ellipsoid (out-Double)</param>
      /// <param name="apogeeHt">Satellite apogee height (km) above the ellipsoid (out-Double)</param>
      /// <param name="perigee">Satellite perigee height from the center of the earth (km) (out-Double)</param>
      /// <param name="apogee">Satellite apogee height from the center of the earth (km) (out-Double)</param>
      /// <param name="a">Semi-major axis (km) (out-Double)</param>
      /// <returns>0 if all satellite data fields are retrieved successfully, non-0 if there is an error</returns>
      [DllImport(SatStateDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SatStateGetSatDataAll(long satKey, out int satNum, byte[] satName, out int eltType, out int revNum, out double epochDs50UTC, out double bField, out int elsetNum, out double incli, out double node, out double eccen, out double omega, out double mnAnomaly, out double mnMotion, out double period, out double perigeeHt, out double apogeeHt, out double perigee, out double apogee, out double a);


      /// <summary>
      /// Retrieves an individual field of a satellite. 
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="xf_Sat">Predefined number specifying which field to retrieve (see XF_SAT_?) (in-Integer)</param>
      /// <param name="retVal">A string to contain the value of the requested field (out-Character[512])</param>
      /// <returns>0 if the satellite data is successfully retrieved, non-0 if there is an error</returns>
      [DllImport(SatStateDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SatStateGetSatDataField(long satKey, int xf_Sat, byte[] retVal);


      /// <summary>
      /// Initializes a TLE, SPVEC, or VCM in preparation for propagation, or an EXTEPHEM in preparation for interpolation
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <returns>0 if the satellite is successfully initialized, non-0 if there is an error</returns>
      [DllImport(SatStateDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SatStateInitSat(long satKey);


      /// <summary>
      /// Checks to see if this satellite is initiazlied
      /// </summary>
      /// <param name="satKey">The satellite's unique key. This key will have been returned by one of the routines in Tle.dll. (in-Long)</param>
      /// <returns>1 if the satellite is initialized or 0 if it isn't.</returns>
      [DllImport(SatStateDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SatStateIsSatInit(long satKey);


      /// <summary>
      /// Propagates a TLE/SPVEC/VCM, or interpolates an EXTEPHEM. 
      /// The satellite is propagated/interpolated to the specified time calculated in minutes since the satellite's epoch time
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="mse">The time to propagate to, specified in minutes since the satellite's epoch time (in-Double)</param>
      /// <param name="ds50UTC">Resulting time in days since 1950, UTC (out-Double)</param>
      /// <param name="revNum">Revolution number (out-Integer)</param>
      /// <param name="pos">Resulting ECI position vector (km) in True Equator and Mean Equinox of Epoch (out-Double[3])</param>
      /// <param name="vel">Resulting ECI velocity vector (km/s) in True Equator and Mean Equinox of Epoch (out-Double[3])</param>
      /// <param name="llh">Resulting geodetic latitude (deg), longitude(deg), and height (km) (out-Double[3])</param>
      /// <returns>0 if the satellite is propagated/interpolated successfully, non-0 if there is an error</returns>
      [DllImport(SatStateDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SatStateMse(long satKey, double mse, out double ds50UTC, out int revNum, double[] pos, double[] vel, double[] llh);


      /// <summary>
      /// Propagates a TLE/SPVEC/VCM, or interpolates an EXTEPHEM. 
      /// The satellite is propagated/interpolated to the specified time calculated in days since 1950, UTC.
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="ds50UTC">The time to propagate to, specified in days since 1950, UTC (in-Double)</param>
      /// <param name="mse">Resulting time in minutes since the satellite's epoch time (out-Double)</param>
      /// <param name="revNum">Revolution number (out-Integer)</param>
      /// <param name="pos">Resulting ECI position vector (km) in True Equator and Mean Equinox of Epoch (out-Double[3])</param>
      /// <param name="vel">Resulting ECI velocity vector (km/s) in True Equator and Mean Equinox of Epoch (out-Double[3])</param>
      /// <param name="llh">Resulting geodetic latitude (deg), longitude(deg), and height (km) (out-Double[3])</param>
      /// <returns>0 if the satellite is propagated/interpolated successfully, non-0 if there is an error</returns>
      [DllImport(SatStateDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SatStateDs50UTC(long satKey, double ds50UTC, out double mse, out int revNum, double[] pos, double[] vel, double[] llh);


      /// <summary>
      /// Returns additional propagated/interpolated results (reserved for future implementation)
      /// Reserved for future implementation
      /// Use this function immediately after the call to SatStateMse or SatStateDs50UTC. 
      /// </summary>
      /// <param name="satKey">the satellite's unique key (in-Long)</param>
      /// <param name="index">type of returned data (in-Integer)</param>
      /// <param name="destArr">the resulting array (out-Double[128])</param>
      /// <returns>0 if successful, non-0 if there is an error</returns>
      [DllImport(SatStateDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SatStateGetPropOut(long satKey, int index, double[] destArr);


      /// <summary>
      /// Returns various ephemeris comparison results between two satellite states.
      /// 
      /// The "in-track" is NOT the velocity direction, but is defined as completing the right handed coordinate system 
      /// defined by the position vector (radial) and the angular momentum vector (cross-track).
      /// </summary>
      /// <param name="primSatKey">The primary satellite's unique key (in-Long)</param>
      /// <param name="secSatKey">The secondary satellite's unique key (in-Long)</param>
      /// <param name="ds50UTC">Requested time in days since 1950 UTC (in-Double)</param>
      /// <param name="uvwFlag">UVW coordinate system flag: 0=use rotating UVW, 1=use inertial UVW (in-Integer)</param>
      /// <param name="xa_Delta">The resulting ephemeris comparison deltas (see XA_DELTA_?) (out-Double[100])</param>
      /// <returns>0 if the comparison results are computed successfully, non-0 if there is an error</returns>
      [DllImport(SatStateDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SatStateEphCom(long primSatKey, long secSatKey, double ds50UTC, int uvwFlag, double[] xa_Delta);


      /// <summary>
      /// Returns various ephemeris comparison results between two satellite states for the requested time span.
      /// 
      /// The "in-track" is NOT the velocity direction, but is defined as completing the right handed coordinate system 
      /// defined by the position vector (radial) and the angular momentum vector (cross-track).
      /// </summary>
      /// <param name="primSatKey">The primary satellite's unique key (in-Long)</param>
      /// <param name="secSatKey">The secondary satellite's unique key (in-Long)</param>
      /// <param name="uvwFlag">UVW coordinate system flag: 0=use rotating UVW, 1=use inertial UVW (in-Integer)</param>
      /// <param name="startTime">Start time expressed in days since 1950, UTC. (in-Double)</param>
      /// <param name="endTime">End time expressed in days since 1950, UTC. (in-Double)</param>
      /// <param name="stepSize">Step size in minutes (can be negative if startTime > endTime desired) (in-Double)</param>
      /// <param name="arrSize">Size of input xa_Delta array (in-Integer)</param>
      /// <param name="xa_Delta_array">The resulting ephemeris comparison deltas (see XA_DELTA_?) throughout the the requested time span - Please note only 32 instead of 100 array elements (out-Double[*, 32])</param>
      /// <param name="genEphComPts">Actual number of ephemeris comparison data points generated (always &le; arrSize) (out-Integer)</param>
      /// <returns>0 if the comparison results are computed successfully, non-0 if there is an error</returns>
      [DllImport(SatStateDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SatStateEphComSpan(long primSatKey, long secSatKey, int uvwFlag, double startTime, double endTime, double stepSize, int arrSize, double[,] xa_Delta_array, out int genEphComPts);


      /// <summary>
      /// Returns various ephemeris comparison results between two satellite states (_OS one step) .
      /// 
      /// The "in-track" is NOT the velocity direction, but is defined as completing the right handed coordinate system 
      /// defined by the position vector (radial) and the angular momentum vector (cross-track).
      /// </summary>
      /// <param name="priPosVel">The primary satellite's state (TEME of Date) in an array (position(1st-3rd, km), velocity(4th-6th, km/s)) (in-Double[6])</param>
      /// <param name="secPosVel">The secondary satellite's state (TEME of Date) in an array (position(1st-3rd, km), velocity(4th-6th, km/s)) (in-Double[6])</param>
      /// <param name="ds50UTC">Requested time in days since 1950 UTC (in-Double)</param>
      /// <param name="uvwFlag">UVW coordinate system flag: 0=use rotating UVW, 1=use inertial UVW (in-Integer)</param>
      /// <param name="xa_Delta">The resulting ephemeris comparison deltas (see XA_DELTA_?) (out-Double[100])</param>
      [DllImport(SatStateDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void SatStateEphCom_OS(double[] priPosVel, double[] secPosVel, double ds50UTC, int uvwFlag, double[] xa_Delta);


      /// <summary>
      /// Determines if a satellite contains covariance matrix. 
      /// 0=no, 1=yes
      /// </summary>
      /// <param name="satKey">the satellite's unique key (in-Long)</param>
      /// <returns>0=sat doesn't have cov mtx, 1=sat has cov mtx</returns>
      [DllImport(SatStateDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SatStateHasCovMtx(long satKey);


      /// <summary>
      /// Propagates/Interpolates UVW covariance matrix from VCM/External ephemeris to the time in days since 1950
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="ds50UTC">The input time in days since 1950 UTC (in-Double)</param>
      /// <param name="covUVW">6x6 UVW covariance matrix (out-Double[6, 6])</param>
      /// <returns>0 if the UVW covariance matrix is propagated/interpolated successfully, non-0 if there is an error</returns>
      [DllImport(SatStateDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SatStateGetCovUVW(long satKey, double ds50UTC, double[,] covUVW);


      /// <summary>
      /// Generate external ephemeris file for the specified satellite (via its unique satKey) 
      /// Note: No need to initialize this satellite if you don't intend to keep it in the propagator's memory afterward. If not initialized, it will be initialized, used, and then removed afterward.
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="startDs50UTC">Start time in days since 1950 UTC (in-Double)</param>
      /// <param name="stopDs50UTC">Stop time in days since 1950 UTC (in-Double)</param>
      /// <param name="stepSizeSecs">Step size in seconds. Set to zero if natural integration step size (auto adjust) is desired for SP propagator, and use original data points for external ephemeris file (in-Double)</param>
      /// <param name="ephFileName">The generated external ephemeris file name (in-Character[512])</param>
      /// <param name="ephFileType">External ephemeris file type (see EPHFILETYPE_?) (in-Integer)</param>
      /// <returns>0 if the external ephemeris file was generated successfully, non-0 if there is an error</returns>
      [DllImport(SatStateDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SatStateGenEphFile(long satKey, double startDs50UTC, double stopDs50UTC, double stepSizeSecs, string ephFileName, int ephFileType);


      /// <summary>
      /// Finds the time of ascending nodal crossing of the specified satellite prior to an input time in days since 1950 TAI
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="ds50TAI">Input time in ds50TAI (in-Double)</param>
      /// <returns>Time at nodal crossing ds50TAI</returns>
      [DllImport(SatStateDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern double GetNodalCrossingPriorToTime(long satKey, double ds50TAI);


      /// <summary>
      /// Get the Gobs parameters for a TLE (SGP4/XP only)
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="ds50UTC">Propagation in days since 1950 UTC (in-Double)</param>
      /// <param name="xa_gobs">(See XA_GOBS_?) (out-Double[32])</param>
      /// <param name="errCode">Error code (out-Integer)</param>
      [DllImport(SatStateDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void GetGobsParams(long satKey, double ds50UTC, double[] xa_gobs, out int errCode);


      /// <summary>
      /// Does an XP GOBS comparison
      /// </summary>
      /// <param name="primSatKey">The primary satellite's unique key (in-Long)</param>
      /// <param name="secSatKey">The secondary satellite's unique key (in-Long)</param>
      /// <param name="ds50UTC">Common epoch to compare in days since 1950 UTC (in-Double)</param>
      /// <param name="xa_gobs_lim">The gobs comparison limits (see XA_GOBS_LIM?) (in-Double[16])</param>
      /// <param name="xa_gobs_delta">The resulting gobs comparison deltas (see XA_GOBS_DELTA_?) (out-Double[16])</param>
      /// <returns>0 if the comparison results are computed successfully, non-0 if there is an error</returns>
      [DllImport(SatStateDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int GobsCom(long primSatKey, long secSatKey, double ds50UTC, double[] xa_gobs_lim, double[] xa_gobs_delta);


      /// <summary>
      /// Does an XP GOBS comparison using gobs arrays
      /// </summary>
      /// <param name="xa_gobs_prim">Primary sat GOBS parameters (see XA_GOBS_?) (in-Double[32])</param>
      /// <param name="xa_gobs_sec">Secondary sat GOBS parameters (see XA_GOBS_?) (in-Double[32])</param>
      /// <param name="xa_gobs_lim">The gobs comparison limits (see XA_GOBS_LIM_?) (in-Double[16])</param>
      /// <param name="xa_gobs_delta">The resulting gobs comparison deltas (see XA_GOBS_DELTA_?) (out-Double[16])</param>
      [DllImport(SatStateDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void GobsComArr(double[] xa_gobs_prim, double[] xa_gobs_sec, double[] xa_gobs_lim, double[] xa_gobs_delta);
      
      // Indexes of available satellite data fields
      public static int  
         XF_SATFIELD_EPOCHUTC =  1,    // Satellite epoch time in days since 1950 UTC
         XF_SATFIELD_MNANOM   =  2,    // Mean anomaly (deg)
         XF_SATFIELD_NODE     =  3,    // Right ascension of asending node (deg) 
         XF_SATFIELD_OMEGA    =  4,    // Argument of perigee (deg)
         XF_SATFIELD_PERIOD   =  5,    // Satellite's period (min)
         XF_SATFIELD_ECCEN    =  6,    // Eccentricity
         XF_SATFIELD_INCLI    =  7,    // Orbit inclination (deg)
         XF_SATFIELD_MNMOTION =  8,    // Mean motion (rev/day)
         XF_SATFIELD_BFIELD   =  9,    // GP B* drag term (1/er)  or SP Radiation Pressure Coefficient
         XF_SATFIELD_PERIGEEHT= 10,    // Perigee height above the geoid (km)
         XF_SATFIELD_APOGEEHT = 11,    // Apogee height above the geoid (km)
         XF_SATFIELD_PERIGEE  = 12,    // Perigee height above the center of the earth (km)
         XF_SATFIELD_APOGEE   = 13,    // Apogee height above the center of the earth (km)
         XF_SATFIELD_A        = 14,    // Semimajor axis (km)
         XF_SATFIELD_NDOT     = 15,    // Mean motion derivative (rev/day**2 /2)
         XF_SATFIELD_SATCAT   = 16,    // Satellite category (Synchronous, Deep space, Decaying, Routine)
         XF_SATFIELD_HTM3     = 17,    // Astat 3 Height multiplier
         XF_SATFIELD_CMOFFSET = 18,    // Center of mass offset (m)
         XF_SATFIELD_N2DOT    = 19,    // Unused
         XF_SATFIELD_NODEDOT  = 20,    // GP node dot (deg/s)
         XF_SATFIELD_ERRORTIME= 21,    // GP only - the last time when propagation has error
         XF_SATFIELD_MU       = 22;    // value of mu
      
        
      //*******************************************************************************
      
      // Indexes of available deltas
      public static int  
         XA_DELTA_POS         =  0,     // delta position (km)
         XA_DELTA_TIME        =  1,     // delta time (sec)
         XA_DELTA_PRADIAL     =  2,     // delta position in radial direction (km)
         XA_DELTA_PINTRCK     =  3,     // delta position in in-track direction (km)
         XA_DELTA_PCRSSTRCK   =  4,     // delta position in cross-track direction (km)
         XA_DELTA_VEL         =  5,     // delta velocity (km/sec)
         XA_DELTA_VRADIAL     =  6,     // delta velocity in radial direction (km/sec)
         XA_DELTA_VINTRCK     =  7,     // delta velocity in in-track direction (km/sec)
         XA_DELTA_VCRSSTRCK   =  8,     // delta velocity in cross-track direction (km/sec)
         XA_DELTA_BETA        =  9,     // delta Beta (deg)
         XA_DELTA_HEIGHT      = 10,     // delta height (km)
         XA_DELTA_ANGMOM      = 11,     // delta angular momentum (deg)
         XA_DELTA_MHLNBS_UVW  = 12,     // 3D position Mahalanobis distance in UVW Space (Bubble Covariance, only if covariance propagation is available or turned on)
         XA_DELTA_MHLNBS_HTB  = 13,     // 3D position Mahalanobis distance in Height-Time_Beta Space (Banana Covariance, only if covariance propagation is available or turned on)
         
         XA_DELTA_SIZE        =100; 
         
      // Indexes of Satellite data fields
      public static int  
         XF_SAT_NUM      =  1,      // Satellite number I5
         XF_SAT_NAME     =  2,      // Satellite international designator A8
         XF_SAT_ELTTYPE  =  3,      // Element type I1 (old name XF_SAT_EPHTYPE)
         XF_SAT_EPHTYPE  =  3,      // Obsolete - should use new name XF_SAT_ELTTYPE instead
         XF_SAT_REVNUM   =  4,      // Epoch revolution number I6
         XF_SAT_EPOCH    =  5,      // Epoch time in days since 1950
         XF_SAT_BFIELD   =  6,      // BStar drag component (GP) or Ballistic coefficient-BTerm (SP) (m^2/kg)
         XF_SAT_ELSETNUM =  7,      // Element set number
         XF_SAT_INCLI    =  8,      // Inclination (deg)
         XF_SAT_NODE     =  9,      // Right ascension of ascending node (deg)
         XF_SAT_ECCEN    = 10,      // Eccentricity
         XF_SAT_OMEGA    = 11,      // Argument of perigee (deg)
         XF_SAT_MNANOM   = 12,      // Mean anomaly (deg)
         XF_SAT_MNMOTN   = 13,      // Mean motion (revs/day)
         XF_SAT_PERIOD   = 14,      // Satellite period (min)
         XF_SAT_PERIGEEHT= 15,      // Perigee Height(km)
         XF_SAT_APOGEEHT = 16,      // Apogee Height (km)
         XF_SAT_PERIGEE  = 17,      // Perigee(km)
         XF_SAT_APOGEE   = 18,      // Apogee (km)
         XF_SAT_A        = 19;      // Semi-major axis (km)
      
      
      // Indexes of SatState's load/save file task mode
      public static int  
         XF_TASK_CTRLONLY = 1,     // Only load/save propagator control parameters
         XF_TASK_SATONLY  = 2,     // Only load/save orbital elements/external ephemeris data
         XF_TASK_BOTH     = 3;     // Load/Save both 1 and 2
         
      
      // Different external ephemeris file types
      public static int  
         EPHFILETYPE_ITC         =  1,     // ITC file format
         EPHFILETYPE_ITC_WOCOV   =  2,     // ITC compact (without covariance matrix) file format
         EPHFILETYPE_CZML        =  3;     // Cesium czml file
         
      
      // Gobs records
      public static int  
         XA_GOBS_SATNUM    =  0,   // Satellite number
         XA_GOBS_LONE      =  1,   // East Longitude
         XA_GOBS_DRIFT     =  2,   // Longitude Drift Rate
         XA_GOBS_RELENERGY =  3,   // satellite's relative energy (deg^2/sec^2) - only for GOBS
         XA_GOBS_WX        =  4,   // sin(incl)*sin(r.a. node)
         XA_GOBS_WY        =  5,   // -sin(incl)*cos(r.a. node)
         XA_GOBS_WZ        =  6,   // cos(incl)
         XA_GOBS_ABARX     =  7,   // abar x
         XA_GOBS_ABARY     =  8,   // abar y 
         XA_GOBS_ABARZ     =  9,   // abar z
         XA_GOBS_AGOM      = 10,   // AGOM
         XA_GOBS_TROUGH    = 11,   // Trough/Drift Flag, 0 - 75 deg trough, 1 - 255 deg trough, 2 - both troughs, 3 - unstable point, 4 - East drift, 5 - West drift
         XA_GOBS_TOTENERGY = 12,   // Total Energy of sat (-mu/(2*a))
         XA_GOBS_INCL      = 13,   // Inclination of sat
      
         XA_GOBS_SIZE      = 32; 
      
      
      // Indexes of GOBS limits
      public static int  
         XA_GOBS_LIM_TROUGH      =  0,     // 0 - ignore trough logic, 1 - implement trough logic
         XA_GOBS_LIM_PCP         =  1,     // Primary satellite is plane changer
         XA_GOBS_LIM_PCS         =  2,     // Secondary satellite is plane changer
         XA_GOBS_LIM_ACTIVEP     =  3,     // Primary satellite is plane changer
         XA_GOBS_LIM_ACTIVES     =  4,     // Secondary satellite is plane changer
         XA_GOBS_LIM_LONGMIN     =  5,     // Min Longitude of sat
         XA_GOBS_LIM_LONGMAX     =  6,     // Max Longitude of sat
         XA_GOBS_LIM_AGOMMIN     =  7,     // Min Agom of sat
         XA_GOBS_LIM_AGOMMAX     =  8,     // Max Agom of sat
         XA_GOBS_LIM_INCLMAX     =  9,     // Max Inclination of sat
      
         XA_GOBS_LIM_SIZE        = 16; 
      
      
      // Indexes of available deltas
      public static int  
         XA_GOBS_DELTA_PRIMESAT     =  0,     // Primary satellite number 
         XA_GOBS_DELTA_SECONDARYSAT =  1,     // Secondary satellite number 
         XA_GOBS_DELTA_ASTAT        =  2,     // GOBS correlation score
         XA_GOBS_DELTA_DOP          =  3,     // delta orbital plane
         XA_GOBS_DELTA_DABAR        =  4,     // delta shape
         XA_GOBS_DELTA_DRELENERGY   =  5,     // delta Relative Energy (deg^2/day^2)
         XA_GOBS_DELTA_LONGP        =  6,     // Longitude of Primary
         XA_GOBS_DELTA_LONGMIN      =  7,     // Minimum Longitude of Secondary
         XA_GOBS_DELTA_LONGMAX      =  8,     // Maximum Longitude of Secondary
         XA_GOBS_DELTA_TROUGH       =  9,     // 0 - opposite throughs or drift rates, 1 - trough or drift rates match
         XA_GOBS_DELTA_PLANE        = 10,     // 0|1    Plane Match Flag
         XA_GOBS_DELTA_SHAPE        = 11,     // 0|1    Shape Match Flag
         XA_GOBS_DELTA_ENERGY       = 12,     // 0|1    Relative Energy Match Flag
         XA_GOBS_DELTA_LONG         = 13,     // 0|1|2  Longitude Match Flag (2 is fuzzy match)
         XA_GOBS_DELTA_AGOM         = 14,     // 0|1    Agom Match Flag
         XA_GOBS_DELTA_INCL         = 15,     // 0|1    Incl Match Flag
      
         XA_GOBS_DELTA_SIZE         = 16; 
      
      
      //*******************************************************************************
      
   }
// ========================= End of auto generated code ==========================
}
