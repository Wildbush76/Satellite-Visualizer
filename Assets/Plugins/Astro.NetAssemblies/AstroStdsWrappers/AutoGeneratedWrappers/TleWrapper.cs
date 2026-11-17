// This wrapper file was generated automatically by the GenDllWrappers program.
using System.Runtime.InteropServices;



namespace AFSPC.AstroStds.Wrappers
{
   public static partial class TleWrapper
   {
      // Provide the path to the dll
#if WINDOWS
      const string TleDll = "tle";
#else
      const string TleDll = "libtle.so";
#endif
      // static constructor
      static TleWrapper()
      {
         // Set AS library default path/LD_LIBRARY_PATH environment
         //Utilities.Utility.Init();
      }



      /// <summary>
      /// Notes: This function has been deprecated since v9.0.    
      /// Initializes Tle DLL for use in the program.
      /// If this function returns an error, it is recommended that you stop the program immediately.
      /// 
      /// An error will occur if you forget to load and initialize all the prerequisite DLLs, as listed in the DLL Prerequisites section of the accompanying documentation, before using this DLL.
      /// </summary>
      /// <param name="apAddr">The handle that was returned from DllMainInit. See the documentation for DllMain.dll for details. (in-Long)</param>
      /// <returns>0 if Tle.dll is initialized successfully, non-0 if there is an error.</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TleInit(long apAddr);


      /// <summary>
      /// Returns the information about the Tle DLL.
      /// The returned string provides information about the version number, build date, and the platform of the Tle DLL. 
      /// </summary>
      /// <param name="infoStr">A string to hold the information about the Tle DLL. (out-Character[128])</param>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void TleGetInfo(byte[] infoStr);


      /// <summary>
      /// Loads TLEs (satellites) contained in a text file into the TLE DLL's binary tree.
      /// You may use this function repeatedly to load TLEs from different input files. However, only unique satKeys are loaded. Duplicated TLEs won't be stored. 
      /// 
      /// TLEs can be included directly in the specified file, or they can be read from a separate file identified with "ELTFIL=[path\filename]" or "VECFIL=[path\filename]".
      /// 
      /// The input file is read in two passes. The function first looks for "ELTFIL=" and "VECFIL=" lines, then it looks for TLEs which were included directly. The result of this is that data entered using both methods will be processed, but the "ELTFIL=" and "VECFIL=" data will be processed first.
      /// </summary>
      /// <param name="tleFile">The name of the file containing two line element sets to be loaded. (in-Character[512])</param>
      /// <returns>0 if the two line element sets in the file are successfully loaded, non-0 if there is an error.</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TleLoadFile(string tleFile);


      /// <summary>
      /// Saves currently loaded TLEs to a file. 
      /// In append mode, if the specified file does not exist it will be created.
      /// If you call this routine immediately after TleLoadFile(), the TLE contents in the two files should be the same (minus duplicated TLE's or bad TLE's).
      /// 
      /// The purpose of this function is to save the current state of the loaded TLE's, usually used in GUI applications, for future use.
      /// </summary>
      /// <param name="tleFile">The name of the file in which to save the TLE's. (in-Character[512])</param>
      /// <param name="saveMode">Specifies whether to create a new file or append to an existing file. (0 = create new file, 1= append to existing file) (in-Integer)</param>
      /// <param name="xf_tleForm">Specifies the format in which to save the file. (0 = two-line element set format, 1 =  CSV, others = future implementation) (in-Integer)</param>
      /// <returns>0 if the data is successfully saved to the file, non-0 if there is an error.</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TleSaveFile(string tleFile, int saveMode, int xf_tleForm);


      /// <summary>
      /// Removes a TLE represented by the satKey from memory. 
      /// If the users enter an invalid satKey (a non-existing satKey), the function will return a non-zero value indicating an error.
      /// </summary>
      /// <param name="satKey">The unique key of the satellite to be removed. (in-Long)</param>
      /// <returns>0 if the TLE is removed successfully, non-0 if there is an error.</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TleRemoveSat(long satKey);


      /// <summary>
      /// Removes all the TLEs from memory.
      /// </summary>
      /// <returns>0 if all TLE's are removed successfully from memory, non-0 if there is an error.</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TleRemoveAllSats();


      /// <summary>
      /// Returns the number of TLEs currently loaded. 
      /// See TleGetLoaded for an example.
      /// This function is useful for dynamically allocating memory for the array that is passed to the function TleGetLoaded().
      /// </summary>
      /// <returns>The number of TLEs currently loaded.</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TleGetCount();


      /// <summary>
      /// Retrieves all of the currently loaded satKeys. These satKeys can be used to access the internal data for the TLE's.
      /// It is recommended that TleGetCount() be  used to determine how many satellites are currently loaded. This value can then be used to dynamically allocate an array to hold the satKeys.
      /// 
      /// If you are going to pass a statically allocated array to this function, ensure it is large enough to hold all of the returned satKeys.
      /// </summary>
      /// <param name="order">Specifies the order in which the satKeys should be returned. 0 = sort in ascending order of satKeys, 1 = sort in descending order of satKeys, 2 = sort in the order in which the satKeys were loaded in memory, 9 = Quickest: sort in the order in which the satKeys were stored in the tree (in-Integer)</param>
      /// <param name="satKeys">The array in which to store the satKeys. (out-Long[*])</param>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void TleGetLoaded(int order, long[] satKeys);


      /// <summary>
      /// Adds a TLE (satellite), using its directly specified first and second lines. 
      /// The function will indicate an error if the specified two line element set corresponds to a satellite that is already in memory.
      /// 
      /// This function can be called repeatedly to add many TLEs, one at a time.
      /// </summary>
      /// <param name="line1">The first line of a two line element set (or CSV Tle). (in-Character[512])</param>
      /// <param name="line2">The second line of a two line element set (in-Character[512])</param>
      /// <returns>The satKey of the newly added TLE on success, a negative value on error.</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern long TleAddSatFrLines(string line1, string line2);


      /// <summary>
      /// This function is similar to TleAddSatFrLines but designed to be used in Matlab.
      /// Matlab doesn't correctly return the 19-digit satellite key using TleAddSatFrLines. This method is an alternative way to return the satKey output.
      /// </summary>
      /// <param name="line1">The first line of a two line element set (or CSV Tle). (in-Character[512])</param>
      /// <param name="line2">The second line of a two line element set (in-Character[512])</param>
      /// <param name="satKey">The satKey of the newly added TLE on success, a negative value on error. (out-Long)</param>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void TleAddSatFrLinesML(string line1, string line2, out long satKey);


      /// <summary>
      /// Adds a TLE (satellite), using its CSV string format. 
      /// </summary>
      /// <param name="csvLine">Input CSV TLE string (in-Character[512])</param>
      /// <returns>The satKey of the newly added TLE on success, a negative value on error.</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern long TleAddSatFrCsv(string csvLine);


      /// <summary>
      /// This function is similar to TleAddSatFrCsv but designed to be used in Matlab.
      /// </summary>
      /// <param name="csvLine">Input CSV TLE string (in-Character[512])</param>
      /// <param name="satKey">The satKey of the newly added TLE on success, a negative value on error. (out-Long)</param>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void TleAddSatFrCsvML(string csvLine, out long satKey);


      /// <summary>
      /// Adds a GP TLE using its individually provided field values. 
      /// The function will indicate an error if the specified two line element set corresponds to a satellite that is already in memory.
      /// 
      /// This function can be called repeatedly to add many satellites (one satellite at a time).
      /// 
      /// SGP satellites (ephType = 0) use Kozai mean motion. SGP4 satellites (ephType = 2) use Brouwer mean motion.
      /// </summary>
      /// <param name="satNum">Satellite number (in-Integer)</param>
      /// <param name="secClass">Security classification (in-Character)</param>
      /// <param name="satName">Satellite international designator (in-Character[8])</param>
      /// <param name="epochYr">Element epoch time - year, [YY]YY (in-Integer)</param>
      /// <param name="epochDays">Element epoch time - day of year, DDD.DDDDDDDD (in-Double)</param>
      /// <param name="bstar">B* drag term (1/er) (ephType = 0, 2) or BTerm - ballistic coefficient (m2/kg) (ephType = 4, XP) (in-Double)</param>
      /// <param name="ephType">Satellite ephemeris type (0: SGP, 2: SGP4) (in-Integer)</param>
      /// <param name="elsetNum">Element set number (in-Integer)</param>
      /// <param name="incli">Orbit inclination (degrees) (in-Double)</param>
      /// <param name="node">Right ascension of ascending node (degrees) (in-Double)</param>
      /// <param name="eccen">Eccentricity (in-Double)</param>
      /// <param name="omega">Argument of perigee (degrees) (in-Double)</param>
      /// <param name="mnAnomaly">Mean anomaly (degrees) (in-Double)</param>
      /// <param name="mnMotion">Mean motion (rev/day) (ephType = 0: Kozai mean motion, ephType = 2 or 4: Brouwer mean motion) (in-Double)</param>
      /// <param name="revNum">Revolution number at epoch (in-Integer)</param>
      /// <returns>The satKey of the newly added TLE on success, a negative value on error.</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern long TleAddSatFrFieldsGP(int satNum, char secClass, string satName, int epochYr, double epochDays, double bstar, int ephType, int elsetNum, double incli, double node, double eccen, double omega, double mnAnomaly, double mnMotion, int revNum);


      /// <summary>
      /// Adds a GP TLE using its individually provided field values. 
      /// The function will indicate an error if the specified two line element set corresponds to a satellite that is already in memory.
      /// 
      /// This function can be called repeatedly to add many satellites (one satellite at a time).
      /// 
      /// SGP satellites (ephType = 0) use Kozai mean motion. SGP4 satellites (ephType = 2) use Brouwer mean motion.
      /// </summary>
      /// <param name="satNum">Satellite number (in-Integer)</param>
      /// <param name="secClass">Security classification (in-Character)</param>
      /// <param name="satName">Satellite international designator (in-Character[8])</param>
      /// <param name="epochYr">Element epoch time - year, [YY]YY (in-Integer)</param>
      /// <param name="epochDays">Element epoch time - day of year, DDD.DDDDDDDD (in-Double)</param>
      /// <param name="bstar">B* drag term (1/er) (ephType = 0, 2) or BTerm - ballistic coefficient (m2/kg) (ephType = 4, XP) (in-Double)</param>
      /// <param name="ephType">Satellite ephemeris type (0: SGP, 2: SGP4) (in-Integer)</param>
      /// <param name="elsetNum">Element set number (in-Integer)</param>
      /// <param name="incli">Orbit inclination (degrees) (in-Double)</param>
      /// <param name="node">Right ascension of ascending node (degrees) (in-Double)</param>
      /// <param name="eccen">Eccentricity (in-Double)</param>
      /// <param name="omega">Argument of perigee (degrees) (in-Double)</param>
      /// <param name="mnAnomaly">Mean anomaly (degrees) (in-Double)</param>
      /// <param name="mnMotion">Mean motion (rev/day) (ephType = 0: Kozai mean motion, ephType = 2 or 4: Brouwer mean motion) (in-Double)</param>
      /// <param name="revNum">Revolution number at epoch (in-Integer)</param>
      /// <param name="satKey">The satKey of the newly added TLE on success, a negative value on error. (out-Long)</param>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void TleAddSatFrFieldsGPML(int satNum, char secClass, string satName, int epochYr, double epochDays, double bstar, int ephType, int elsetNum, double incli, double node, double eccen, double omega, double mnAnomaly, double mnMotion, int revNum, out long satKey);


      /// <summary>
      /// This function is similar to TleAddSatFrFieldsGP but includes nDotO2 and n2DotO6. 
      /// nDotO2 and n2DotO6 values are not used in the SGP4 propagator. However, some users still want to preserve the integrity of all input data.
      /// </summary>
      /// <param name="satNum">Satellite number (in-Integer)</param>
      /// <param name="secClass">Security classification (in-Character)</param>
      /// <param name="satName">Satellite international designator (in-Character[8])</param>
      /// <param name="epochYr">Element epoch time - year, [YY]YY (in-Integer)</param>
      /// <param name="epochDays">Element epoch time - day of year, DDD.DDDDDDDD (in-Double)</param>
      /// <param name="bstar">B* drag term (1/er) (ephType = 0, 2) or BTerm - ballistic coefficient (m2/kg) (ephType = 4, XP) (in-Double)</param>
      /// <param name="ephType">Satellite ephemeris type (0: SGP, 2: SGP4) (in-Integer)</param>
      /// <param name="elsetNum">Element set number (in-Integer)</param>
      /// <param name="incli">Orbit inclination (degrees) (in-Double)</param>
      /// <param name="node">Right ascension of ascending node (degrees) (in-Double)</param>
      /// <param name="eccen">Eccentricity (in-Double)</param>
      /// <param name="omega">Argument of perigee (degrees) (in-Double)</param>
      /// <param name="mnAnomaly">Mean anomaly (degrees) (in-Double)</param>
      /// <param name="mnMotion">Mean motion (rev/day) (ephType = 0: Kozai mean motion, ephType = 2 or 4: Brouwer mean motion) (in-Double)</param>
      /// <param name="revNum">Revolution number at epoch (in-Integer)</param>
      /// <param name="nDotO2">Mean motion derivative (rev/day**2 /2) (in-Double)</param>
      /// <param name="n2DotO6">Mean motion second derivative (rev/day**3 /6) or agom (ephType = 4, XP) (m2/kg) (in-Double)</param>
      /// <returns>The satKey of the newly added TLE on success, a negative value on error.</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern long TleAddSatFrFieldsGP2(int satNum, char secClass, string satName, int epochYr, double epochDays, double bstar, int ephType, int elsetNum, double incli, double node, double eccen, double omega, double mnAnomaly, double mnMotion, int revNum, double nDotO2, double n2DotO6);


      /// <summary>
      /// This function is similar to TleAddSatFrFieldsGP2 but designed to be used in Matlab. 
      /// Matlab doesn't seem to correctly return the 19-digit satellite key using TleAddSatFrFieldsGP2. This method is an alternative way to return the satKey output.
      /// </summary>
      /// <param name="satNum">Satellite number (in-Integer)</param>
      /// <param name="secClass">Security classification (in-Character)</param>
      /// <param name="satName">Satellite international designator (in-Character[8])</param>
      /// <param name="epochYr">Element epoch time - year, [YY]YY (in-Integer)</param>
      /// <param name="epochDays">Element epoch time - day of year, DDD.DDDDDDDD (in-Double)</param>
      /// <param name="bstar">B* drag term (1/er) (ephType = 0, 2) or BTerm - ballistic coefficient (m2/kg) (ephType = 4, XP) (in-Double)</param>
      /// <param name="ephType">Satellite ephemeris type (0: SGP, 2: SGP4) (in-Integer)</param>
      /// <param name="elsetNum">Element set number (in-Integer)</param>
      /// <param name="incli">Orbit inclination (degrees) (in-Double)</param>
      /// <param name="node">Right ascension of ascending node (degrees) (in-Double)</param>
      /// <param name="eccen">Eccentricity (in-Double)</param>
      /// <param name="omega">Argument of perigee (degrees) (in-Double)</param>
      /// <param name="mnAnomaly">Mean anomaly (degrees) (in-Double)</param>
      /// <param name="mnMotion">Mean motion (rev/day) (ephType = 0: Kozai mean motion, ephType = 2 or 4: Brouwer mean motion) (in-Double)</param>
      /// <param name="revNum">Revolution number at epoch (in-Integer)</param>
      /// <param name="nDotO2">Mean motion derivative (rev/day**2 /2) (in-Double)</param>
      /// <param name="n2DotO6">Mean motion second derivative (rev/day**3 /6) or agom (ephType = 4, XP) (m2/kg) (in-Double)</param>
      /// <param name="satKey">The satKey of the newly added TLE on success, a negative value on error. (out-Long)</param>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void TleAddSatFrFieldsGP2ML(int satNum, char secClass, string satName, int epochYr, double epochDays, double bstar, int ephType, int elsetNum, double incli, double node, double eccen, double omega, double mnAnomaly, double mnMotion, int revNum, double nDotO2, double n2DotO6, out long satKey);


      /// <summary>
      /// Updates a GP satellite's data in memory by providing its individual field values. Note: satNum, year, day, and ephtype can't be updated.
      /// The satellite's unique key will not be changed by this function. If you specify a satKey that does not correspond to a currently loaded satellite, the function will indicate an error.
      /// 
      /// Remember to use the correct mean motion depending on the satellite's ephType.
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="secClass">Security classification (in-Character)</param>
      /// <param name="satName">Satellite international designator (in-Character[8])</param>
      /// <param name="bstar">B* drag term (1/er) (ephType = 0, 2) or BTerm - ballistic coefficient (m2/kg) (ephType = 4, XP) (in-Double)</param>
      /// <param name="elsetNum">Element set number (in-Integer)</param>
      /// <param name="incli">Orbit inclination (degrees) (in-Double)</param>
      /// <param name="node">Right ascension of ascending node (degrees) (in-Double)</param>
      /// <param name="eccen">Eccentricity (in-Double)</param>
      /// <param name="omega">Argument of perigee (degrees) (in-Double)</param>
      /// <param name="mnAnomaly">Mean anomaly (degrees) (in-Double)</param>
      /// <param name="mnMotion">Mean motion  (rev/day) (ephType = 0: Kozai mean motion, ephType = 2: Brouwer mean motion) (in-Double)</param>
      /// <param name="revNum">Revolution number at epoch (in-Integer)</param>
      /// <returns>0 if the TLE is successfully updated, non-0 if there is an error.</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TleUpdateSatFrFieldsGP(long satKey, char secClass, string satName, double bstar, int elsetNum, double incli, double node, double eccen, double omega, double mnAnomaly, double mnMotion, int revNum);


      /// <summary>
      /// This function is similar to TleUpdateSatFrFieldsGP but includes nDotO2 and n2DotO6. Note: satNum, year, day, and ephtype can't be updated.
      /// nDotO2 and n2DotO6 values are not used in the SGP4 propagator. However, some users still want to preserve the integrity of all input data.
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="secClass">Security classification (in-Character)</param>
      /// <param name="satName">Satellite international designator (in-Character[8])</param>
      /// <param name="bstar">B* drag term (1/er) (ephType = 0, 2) or BTerm - ballistic coefficient (m2/kg) (ephType = 4, XP) (in-Double)</param>
      /// <param name="elsetNum">Element set number (in-Integer)</param>
      /// <param name="incli">Orbit inclination (degrees) (in-Double)</param>
      /// <param name="node">Right ascension of ascending node (degrees) (in-Double)</param>
      /// <param name="eccen">Eccentricity (in-Double)</param>
      /// <param name="omega">Argument of perigee (degrees) (in-Double)</param>
      /// <param name="mnAnomaly">Mean anomaly (degrees) (in-Double)</param>
      /// <param name="mnMotion">Mean motion  (rev/day) (ephType = 0: Kozai mean motion, ephType = 2: Brouwer mean motion) (in-Double)</param>
      /// <param name="revNum">Revolution number at epoch (in-Integer)</param>
      /// <param name="nDotO2">Mean motion derivative (rev/day**2 /2) (in-Double)</param>
      /// <param name="n2DotO6">Mean motion second derivative (rev/day**3 /6) or agom (ephType = 4, XP) (m2/kg) (in-Double)</param>
      /// <returns>0 if the TLE is successfully updated, non-0 if there is an error.</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TleUpdateSatFrFieldsGP2(long satKey, char secClass, string satName, double bstar, int elsetNum, double incli, double node, double eccen, double omega, double mnAnomaly, double mnMotion, int revNum, double nDotO2, double n2DotO6);


      /// <summary>
      /// Adds an SP satellite using the individually provided field values.
      /// Only applies to SP propagator.
      /// </summary>
      /// <param name="satNum">Satellite number (in-Integer)</param>
      /// <param name="secClass">Security classification (in-Character)</param>
      /// <param name="satName">Satellite international designator (in-Character[8])</param>
      /// <param name="epochYr">Element epoch time - year, [YY]YY (in-Integer)</param>
      /// <param name="epochDays">Element epoch time - day of year, DDD.DDDDDDDD (in-Double)</param>
      /// <param name="bTerm">Ballistic coefficient (m^2/kg) (in-Double)</param>
      /// <param name="ogParm">Outgassing parameter/Thrust Acceleration (km/s^2) (in-Double)</param>
      /// <param name="agom">agom (m^2/kg) (in-Double)</param>
      /// <param name="elsetNum">Element set number (in-Integer)</param>
      /// <param name="incli">Orbit inclination (degrees) (in-Double)</param>
      /// <param name="node">Right ascension of ascending node (degrees) (in-Double)</param>
      /// <param name="eccen">Eccentricity (in-Double)</param>
      /// <param name="omega">Argument of perigee (degrees) (in-Double)</param>
      /// <param name="mnAnomaly">Mean anomaly (degrees) (in-Double)</param>
      /// <param name="mnMotion">Mean motion (rev/day) (in-Double)</param>
      /// <param name="revNum">Revolution number at epoch (in-Integer)</param>
      /// <returns>The satKey of the newly added TLE on success, a negative value on error.</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern long TleAddSatFrFieldsSP(int satNum, char secClass, string satName, int epochYr, double epochDays, double bTerm, double ogParm, double agom, int elsetNum, double incli, double node, double eccen, double omega, double mnAnomaly, double mnMotion, int revNum);


      /// <summary>
      /// This function is similar to TleAddSatFrFieldsSP but designed to be used in Matlab.
      /// Only applies to SP propagator.
      /// Matlab doesn't correctly return the 19-digit satellite key using TleAddSatFrFieldsSP. This method is an alternative way to return the satKey output.
      /// </summary>
      /// <param name="satNum">Satellite number (in-Integer)</param>
      /// <param name="secClass">Security classification (in-Character)</param>
      /// <param name="satName">Satellite international designator (in-Character[8])</param>
      /// <param name="epochYr">Element epoch time - year, [YY]YY (in-Integer)</param>
      /// <param name="epochDays">Element epoch time - day of year, DDD.DDDDDDDD (in-Double)</param>
      /// <param name="bTerm">Ballistic coefficient (m^2/kg) (in-Double)</param>
      /// <param name="ogParm">Outgassing parameter/Thrust Acceleration (km/s^2) (in-Double)</param>
      /// <param name="agom">agom (m^2/kg) (in-Double)</param>
      /// <param name="elsetNum">Element set number (in-Integer)</param>
      /// <param name="incli">Orbit inclination (degrees) (in-Double)</param>
      /// <param name="node">Right ascension of ascending node (degrees) (in-Double)</param>
      /// <param name="eccen">Eccentricity (in-Double)</param>
      /// <param name="omega">Argument of perigee (degrees) (in-Double)</param>
      /// <param name="mnAnomaly">Mean anomaly (degrees) (in-Double)</param>
      /// <param name="mnMotion">Mean motion (rev/day) (in-Double)</param>
      /// <param name="revNum">Revolution number at epoch (in-Integer)</param>
      /// <param name="satKey">The satKey of the newly added TLE on success, a negative value on error. (out-Long)</param>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void TleAddSatFrFieldsSPML(int satNum, char secClass, string satName, int epochYr, double epochDays, double bTerm, double ogParm, double agom, int elsetNum, double incli, double node, double eccen, double omega, double mnAnomaly, double mnMotion, int revNum, out long satKey);


      /// <summary>
      /// Updates an SP satellite's data in memory using its individually provided field values. Note: satNum, year, day, and ephtype can't be updated.
      /// Only applies to SP propagator.
      /// The satellite's unique key will not be changed by this function. If you specify a satKey that does not correspond to a currently loaded TLE, the function will indicate an error. 
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="secClass">Security classification (in-Character)</param>
      /// <param name="satName">Satellite international designator (in-Character[8])</param>
      /// <param name="bterm">Ballistic coefficient (m^2/kg) (in-Double)</param>
      /// <param name="ogParm">Outgassing parameter/Thrust Acceleration (km/s^2) (in-Double)</param>
      /// <param name="agom">agom (m^2/kg) (in-Double)</param>
      /// <param name="elsetNum">Element set number (in-Integer)</param>
      /// <param name="incli">Orbit inclination (degrees) (in-Double)</param>
      /// <param name="node">Right ascension of ascending node (degrees) (in-Double)</param>
      /// <param name="eccen">Eccentricity (in-Double)</param>
      /// <param name="omega">Argument of perigee (degrees) (in-Double)</param>
      /// <param name="mnAnomaly">Mean anomaly (degrees) (in-Double)</param>
      /// <param name="mnMotion">Mean motion (rev/day) (in-Double)</param>
      /// <param name="revNum">Revolution number at epoch (in-Integer)</param>
      /// <returns>0 if the TLE is successfully updated, non-0 if there is an error.</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TleUpdateSatFrFieldsSP(long satKey, char secClass, string satName, double bterm, double ogParm, double agom, int elsetNum, double incli, double node, double eccen, double omega, double mnAnomaly, double mnMotion, int revNum);


      /// <summary>
      /// Updates the value of a field of a TLE. This function can be used for both GP and SP satellites.
      /// 
      /// The table below indicates which index values correspond to which fields. Make sure to use the appropriate field index for GP TLEs and SP TLEs.  For indexes 5, 15 and 16, the interpretation depends on the ephemeris type of the TLE.
      /// satNum (1), Epoch (4), and Ephemeris Type (5) cannot be altered.
      /// 
      /// table
      /// 
      /// 	index
      /// 	index Interpretation
      /// 
      /// 1Satellite number
      /// 2Security classification
      /// 3Satellite international designator
      /// 4Epoch
      /// 5Ephemeris type = 0,2: B* drag term (1/er) Ephemeris type = 6   : SP radiation pressure
      /// coefficient agom (m2/kg)
      /// 6Ephemeris type
      /// 7Element set number
      /// 8Orbit inclination (degrees)
      /// 9Right ascension of ascending node (degrees)
      /// 10Eccentricity
      /// 11Argument of perigee (degrees)
      /// 12Mean anomaly (degrees)
      /// 13Mean motion (rev/day)
      /// 14Revolution number at epoch
      /// 15Ephemeris type = 0: SGP mean motion derivative (rev/day /2) or Ephemeris type = 6: SP
      /// ballistic coefficient (m2/kg)
      /// 16Ephemeris type = 0: SGP mean motion second derivative (rev/day**2 /6) or Ephemeris type = 6:
      /// SP Outgassing parameter/Thrust Acceleration (km/s2)
      /// 
      /// </summary>
      /// <param name="satKey">The satellite's unique key. (in-Long)</param>
      /// <param name="xf_Tle">Predefined number specifying which field to set. See remarks. (in-Integer)</param>
      /// <param name="valueStr">The new value of the specified field, expressed as a string. (in-Character[512])</param>
      /// <returns>0 if the TLE is successfully updated, non-0 if there is an error</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TleSetField(long satKey, int xf_Tle, string valueStr);


      /// <summary>
      /// Retrieves the value of a specific field of a TLE. 
      /// 
      /// The table below indicates which index values correspond to which fields. Make sure to use the appropriate field index for GP TLEs and SP TLEs.  For indexes 5, 15 and 16, the interpretation depends on the ephemeris type of the TLE.
      /// 
      /// table
      /// 
      /// 	index
      /// 	index Interpretation
      /// 
      /// 1Satellite number
      /// 2Security classification
      /// 3Satellite international designator
      /// 4Epoch
      /// 5Ephemeris type = 0,2: B* drag term (1/er) Ephemeris type = 6   : SP radiation pressure
      /// coefficient agom (m2/kg)
      /// 6Ephemeris type
      /// 7Element set number
      /// 8Orbit inclination (degrees)
      /// 9Right ascension of ascending node (degrees)
      /// 10Eccentricity
      /// 11Argument of perigee (degrees)
      /// 12Mean anomaly (degrees)
      /// 13Mean motion (rev/day)
      /// 14Revolution number at epoch
      /// 15Ephemeris type = 0: SGP mean motion derivative (rev/day /2) or Ephemeris type = 6: SP
      /// ballistic coefficient (m2/kg)
      /// 16Ephemeris type = 0: SGP mean motion second derivative (rev/day**2 /6) or Ephemeris type = 6:
      /// SP Outgassing parameter/Thrust Acceleration (km/s2)
      /// 
      /// </summary>
      /// <param name="satKey">The satellite's unique key. (in-Long)</param>
      /// <param name="xf_Tle">Predefined number specifying which field to retrieve. See remarks. (in-Integer)</param>
      /// <param name="valueStr">A string to contain the value of the requested field. (out-Character[512])</param>
      /// <returns>0 if the TLE data is successfully retrieved, non-0 if there is an error.</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TleGetField(long satKey, int xf_Tle, byte[] valueStr);


      /// <summary>
      /// Retrieves all of the data for a GP satellite in a single function call. 
      /// This function only works for GP satellites. The field values are placed in the corresponding parameters of the function. 
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="satNum">Satellite number (out-Integer)</param>
      /// <param name="secClass">Security classification (out-Character)</param>
      /// <param name="satName">Satellite international designator (out-Character[8])</param>
      /// <param name="epochYr">Element epoch time - year, [YY]YY (out-Integer)</param>
      /// <param name="epochDays">Element epoch time - day of year, DDD.DDDDDDDD (out-Double)</param>
      /// <param name="bstar">B* drag term (1/er) (ephType = 0, 2) or BTerm - ballistic coefficient (m2/kg) (ephType = 4, XP) (out-Double)</param>
      /// <param name="ephType">Satellite ephemeris type (0: SGP, 2: SGP4, 6: SP) (out-Integer)</param>
      /// <param name="elsetNum">Element set number (out-Integer)</param>
      /// <param name="incli">Orbit inclination (degrees) (out-Double)</param>
      /// <param name="node">Right ascension of ascending node (degrees) (out-Double)</param>
      /// <param name="eccen">Eccentricity (out-Double)</param>
      /// <param name="omega">Argument of perigee (degrees) (out-Double)</param>
      /// <param name="mnAnomaly">Mean anomaly (deg) (out-Double)</param>
      /// <param name="mnMotion">Mean motion (rev/day) (ephType = 0: Kozai mean motion, ephType = 2 or 4: Brouwer mean motion) (out-Double)</param>
      /// <param name="revNum">Revolution number at epoch (out-Integer)</param>
      /// <returns>0 if all values are retrieved successfully, non-0 if there is an error.</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TleGetAllFieldsGP(long satKey, out int satNum, out char secClass, byte[] satName, out int epochYr, out double epochDays, out double bstar, out int ephType, out int elsetNum, out double incli, out double node, out double eccen, out double omega, out double mnAnomaly, out double mnMotion, out int revNum);


      /// <summary>
      /// Retrieves all of the data (including nDotO2 and n2DotO6) for a GP satellite in a single function call. 
      /// This function is similar to TleGetAllFieldsGP but also includes nDotO2 and n2DotO6. 
      /// This function only works for GP satellites. The field values are placed in the corresponding parameters of the function. 
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="satNum">Satellite number (out-Integer)</param>
      /// <param name="secClass">Security classification (out-Character)</param>
      /// <param name="satName">Satellite international designator (out-Character[8])</param>
      /// <param name="epochYr">Element epoch time - year, [YY]YY (out-Integer)</param>
      /// <param name="epochDays">Element epoch time - day of year, DDD.DDDDDDDD (out-Double)</param>
      /// <param name="bstar">B* drag term (1/er) (ephType = 0, 2) or BTerm - ballistic coefficient (m2/kg) (ephType = 4, XP) (out-Double)</param>
      /// <param name="ephType">Satellite ephemeris type (0: SGP, 2: SGP4, 6: SP) (out-Integer)</param>
      /// <param name="elsetNum">Element set number (out-Integer)</param>
      /// <param name="incli">Orbit inclination (degrees) (out-Double)</param>
      /// <param name="node">Right ascension of ascending node (degrees) (out-Double)</param>
      /// <param name="eccen">Eccentricity (out-Double)</param>
      /// <param name="omega">Argument of perigee (degrees) (out-Double)</param>
      /// <param name="mnAnomaly">Mean anomaly (degrees) (out-Double)</param>
      /// <param name="mnMotion">Mean motion (rev/day) (ephType = 0: Kozai mean motion, ephType = 2 or 4: Brouwer mean motion) (out-Double)</param>
      /// <param name="revNum">Revolution number at epoch (out-Integer)</param>
      /// <param name="nDotO2">Mean motion derivative (rev/day**2 /2) (out-Double)</param>
      /// <param name="n2DotO6">Mean motion second derivative (rev/day**3 /6) or agom (ephType = 4, XP) (m2/kg) (out-Double)</param>
      /// <returns>0 if all values are retrieved successfully, non-0 if there is an error.</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TleGetAllFieldsGP2(long satKey, out int satNum, out char secClass, byte[] satName, out int epochYr, out double epochDays, out double bstar, out int ephType, out int elsetNum, out double incli, out double node, out double eccen, out double omega, out double mnAnomaly, out double mnMotion, out int revNum, out double nDotO2, out double n2DotO6);


      /// <summary>
      /// Retrieves all of the data for an SP satellite in a single function call.
      /// Only applies to SP propagator.
      /// This function only works for SP satellites. The field values are placed in the corresponding parameters of the function. 
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="satNum">Satellite number (out-Integer)</param>
      /// <param name="secClass">Security classification (out-Character)</param>
      /// <param name="satName">Satellite international designator (out-Character[8])</param>
      /// <param name="epochYr">Element epoch time - year, [YY]YY (out-Integer)</param>
      /// <param name="epochDays">Element epoch time - day of year, DDD.DDDDDDDD (out-Double)</param>
      /// <param name="bTerm">Ballistic coefficient (m^2/kg) (out-Double)</param>
      /// <param name="ogParm">Outgassing parameter/Thrust Acceleration (km/s^2) (out-Double)</param>
      /// <param name="agom">Agom (m^2/kg) (out-Double)</param>
      /// <param name="elsetNum">Element set number (out-Integer)</param>
      /// <param name="incli">Orbit inclination (degrees) (out-Double)</param>
      /// <param name="node">Right ascension of ascending node (degrees) (out-Double)</param>
      /// <param name="eccen">Eccentricity (out-Double)</param>
      /// <param name="omega">Argument of perigee (degrees) (out-Double)</param>
      /// <param name="mnAnomaly">Mean anomaly (degrees) (out-Double)</param>
      /// <param name="mnMotion">Mean motion (rev/day) (out-Double)</param>
      /// <param name="revNum">Revolution number at epoch (out-Integer)</param>
      /// <returns>0 if all values are retrieved successfully, non-0 if there is an error.</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TleGetAllFieldsSP(long satKey, out int satNum, out char secClass, byte[] satName, out int epochYr, out double epochDays, out double bTerm, out double ogParm, out double agom, out int elsetNum, out double incli, out double node, out double eccen, out double omega, out double mnAnomaly, out double mnMotion, out int revNum);


      /// <summary>
      /// Parses GP data from the input first and second lines of a two line element set or a CSV Tle.
      /// This function only parses data from the input TLE but DOES NOT load/add the input TLE to memory.
      /// </summary>
      /// <param name="line1">The first line of the two line element set or a CSV tle (in-Character[512])</param>
      /// <param name="line2">The second line of the two line element set or an empty string for a CVS tle (in-Character[512])</param>
      /// <param name="satNum">Satellite number (out-Integer)</param>
      /// <param name="secClass">Security classification (out-Character)</param>
      /// <param name="satName">Satellite international designator (out-Character[8])</param>
      /// <param name="epochYr">Element epoch time - year, [YY]YY (out-Integer)</param>
      /// <param name="epochDays">Element epoch time - day of year, DDD.DDDDDDDD (out-Double)</param>
      /// <param name="nDotO2">n-dot/2 (for SGP, ephType = 0) (out-Double)</param>
      /// <param name="n2DotO6">n-double-dot/6 (for SGP, ephType = 0) or agom (ephType = 4, XP) (m2/kg) (out-Double)</param>
      /// <param name="bstar">B* drag term (1/er) (ephType = 0, 2) or BTerm - ballistic coefficient (m2/kg) (ephType = 4, XP) (out-Double)</param>
      /// <param name="ephType">Satellite ephemeris type (0: SGP, 2: SGP4, 6: SP) (out-Integer)</param>
      /// <param name="elsetNum">Element set number (out-Integer)</param>
      /// <param name="incli">Orbit inclination (degrees) (out-Double)</param>
      /// <param name="node">Right ascension of ascending node (degrees) (out-Double)</param>
      /// <param name="eccen">Eccentricity (out-Double)</param>
      /// <param name="omega">Argument of perigee (degrees) (out-Double)</param>
      /// <param name="mnAnomaly">Mean anomaly (degrees) (out-Double)</param>
      /// <param name="mnMotion">Mean motion (rev/day) (ephType = 0: Kozai mean motion, ephType = 2 or 4: Brouwer mean motion) (out-Double)</param>
      /// <param name="revNum">Revolution number at epoch (out-Integer)</param>
      /// <returns>0 if the TLE is parsed successfully, non-0 if there is an error.</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TleParseGP(string line1, string line2, out int satNum, out char secClass, byte[] satName, out int epochYr, out double epochDays, out double nDotO2, out double n2DotO6, out double bstar, out int ephType, out int elsetNum, out double incli, out double node, out double eccen, out double omega, out double mnAnomaly, out double mnMotion, out int revNum);


      /// <summary>
      /// Parses GP data from the input first and second lines of a two line element set or a CSV tle and store that data back into the output parameters.
      /// This function only parses data from the input TLE but DOES NOT load/add the input TLE to memory.
      /// </summary>
      /// <param name="line1">The first line of the two line element set or a CSV tle (in-Character[512])</param>
      /// <param name="line2">The second line of the two line element set or an empty string for a CVS tle (in-Character[512])</param>
      /// <param name="xa_tle">Array containing TLE's numerical fields (see XA_TLE_?) (out-Double[64])</param>
      /// <param name="xs_tle">Output string that contains all TLE's text fields (see XS_TLE_?) (out-Character[512])</param>
      /// <returns>0 if the TLE is parsed successfully, non-0 if there is an error.</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TleLinesToArray(string line1, string line2, double[] xa_tle, byte[] xs_tle);


      /// <summary>
      /// Parses SP data from the input first and second lines of a two line element set.
      /// Only applies to SP propagator.
      /// This function only parses data from the input TLE but DOES NOT load/add the input TLE to memory.
      /// </summary>
      /// <param name="line1">The first line of the two line element set (in-Character[512])</param>
      /// <param name="line2">The second line of the two line element set (in-Character[512])</param>
      /// <param name="satNum">Satellite number (out-Integer)</param>
      /// <param name="secClass">Security classification (out-Character)</param>
      /// <param name="satName">Satellite international designator (out-Character[8])</param>
      /// <param name="epochYr">Element epoch time - year, [YY]YY (out-Integer)</param>
      /// <param name="epochDays">Element epoch time - day of year, DDD.DDDDDDDD (out-Double)</param>
      /// <param name="bTerm">Ballistic coefficient (m^2/kg) (out-Double)</param>
      /// <param name="ogParm">Outgassing parameter/Thrust Acceleration (km/s^2) (out-Double)</param>
      /// <param name="agom">Agom (m^2/kg) (out-Double)</param>
      /// <param name="elsetNum">Element set number (out-Integer)</param>
      /// <param name="incli">Orbit inclination (degrees) (out-Double)</param>
      /// <param name="node">Right ascension of ascending node (degrees) (out-Double)</param>
      /// <param name="eccen">Eccentricity (out-Double)</param>
      /// <param name="omega">Argument of perigee (degrees) (out-Double)</param>
      /// <param name="mnAnomaly">Mean anomaly (degrees) (out-Double)</param>
      /// <param name="mnMotion">Mean motion (rev/day) (out-Double)</param>
      /// <param name="revNum">Revolution number at epoch (out-Integer)</param>
      /// <returns>0 if the TLE is parsed successfully, non-0 if there is an error.</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TleParseSP(string line1, string line2, out int satNum, out char secClass, byte[] satName, out int epochYr, out double epochDays, out double bTerm, out double ogParm, out double agom, out int elsetNum, out double incli, out double node, out double eccen, out double omega, out double mnAnomaly, out double mnMotion, out int revNum);


      /// <summary>
      /// Returns the first and second lines representation of a TLE of a satellite. 
      /// </summary>
      /// <param name="satKey">The satellite's unique key. (in-Long)</param>
      /// <param name="line1">A string to hold the first line of the TLE (out-Character[512])</param>
      /// <param name="line2">A string to hold the second line of the TLE (out-Character[512])</param>
      /// <returns>0 if successful, non-0 on error.</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TleGetLines(long satKey, byte[] line1, byte[] line2);


      /// <summary>
      /// Returns the CSV string representation of a TLE of a satellite. 
      /// </summary>
      /// <param name="satKey">The satellite's unique key. (in-Long)</param>
      /// <param name="csvLine">A string to hold the TLE in csv format. (out-Character[512])</param>
      /// <returns>0 if successful, non-0 on error.</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TleGetCsv(long satKey, byte[] csvLine);


      /// <summary>
      /// Constructs a TLE from individually provided GP data fields.
      /// This function only parses data from the input fields but DOES NOT load/add the TLE to memory.
      /// Returned line1 and line2 will be empty if the function fails to construct the lines as requested.
      /// </summary>
      /// <param name="satNum">Satellite number (in-Integer)</param>
      /// <param name="secClass">Security classification (in-Character)</param>
      /// <param name="satName">Satellite international designator (in-Character[8])</param>
      /// <param name="epochYr">Element epoch time - year, [YY]YY (in-Integer)</param>
      /// <param name="epochDays">Element epoch time - day of year, DDD.DDDDDDDD (in-Double)</param>
      /// <param name="nDotO2">N Dot/2 (rev/day**2 /2) (in-Double)</param>
      /// <param name="n2DotO6">N Double Dot/6 (rev/day**3 /6) or agom (ephType = 4, XP) (m2/kg) (in-Double)</param>
      /// <param name="bstar">B* drag term (1/er) (ephType = 0, 2) or BTerm - ballistic coefficient (m2/kg) (ephType = 4, XP) (in-Double)</param>
      /// <param name="ephType">Satellite ephemeris type (0: SGP, 2: SGP4) (in-Integer)</param>
      /// <param name="elsetNum">Element set number (in-Integer)</param>
      /// <param name="incli">Orbit inclination (degrees) (in-Double)</param>
      /// <param name="node">Right ascension of ascending node (degrees) (in-Double)</param>
      /// <param name="eccen">Eccentricity (in-Double)</param>
      /// <param name="omega">Argument of perigee (degrees) (in-Double)</param>
      /// <param name="mnAnomaly">Mean anomaly (degrees) (in-Double)</param>
      /// <param name="mnMotion">Mean motion (rev/day) (ephType = 0: Kozai mean motion, ephType = 2 or 4: Brouwer mean motion) (in-Double)</param>
      /// <param name="revNum">Revolution number at epoch (in-Integer)</param>
      /// <param name="line1">Returned first line of a TLE. (out-Character[512])</param>
      /// <param name="line2">Returned second line of a TLE. (out-Character[512])</param>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void TleGPFieldsToLines(int satNum, char secClass, string satName, int epochYr, double epochDays, double nDotO2, double n2DotO6, double bstar, int ephType, int elsetNum, double incli, double node, double eccen, double omega, double mnAnomaly, double mnMotion, int revNum, byte[] line1, byte[] line2);


      /// <summary>
      /// Constructs a TLE from individually provided GP data fields.
      /// This function only parses data from the input fields but DOES NOT load/add the TLE to memory.
      /// Returned line1 and line2 will be empty if the function fails to construct the lines as requested.
      /// </summary>
      /// <param name="satNum">Satellite number (in-Integer)</param>
      /// <param name="secClass">Security classification (in-Character)</param>
      /// <param name="satName">Satellite international designator (in-Character[8])</param>
      /// <param name="epochYr">Element epoch time - year, [YY]YY (in-Integer)</param>
      /// <param name="epochDays">Element epoch time - day of year, DDD.DDDDDDDD (in-Double)</param>
      /// <param name="nDotO2">N Dot/2 (rev/day**2 /2) (in-Double)</param>
      /// <param name="n2DotO6">N Double Dot/6 (rev/day**3 /6) or agom (ephType = 4, XP) (m2/kg) (in-Double)</param>
      /// <param name="bstar">B* drag term (1/er) (ephType = 0, 2) or BTerm - ballistic coefficient (m2/kg) (ephType = 4, XP) (in-Double)</param>
      /// <param name="ephType">Satellite ephemeris type (0: SGP, 2: SGP4) (in-Integer)</param>
      /// <param name="elsetNum">Element set number (in-Integer)</param>
      /// <param name="incli">Orbit inclination (degrees) (in-Double)</param>
      /// <param name="node">Right ascension of ascending node (degrees) (in-Double)</param>
      /// <param name="eccen">Eccentricity (in-Double)</param>
      /// <param name="omega">Argument of perigee (degrees) (in-Double)</param>
      /// <param name="mnAnomaly">Mean anomaly (degrees) (in-Double)</param>
      /// <param name="mnMotion">Mean motion (rev/day) (ephType = 0: Kozai mean motion, ephType = 2 or 4: Brouwer mean motion) (in-Double)</param>
      /// <param name="revNum">Revolution number at epoch (in-Integer)</param>
      /// <param name="csvLine">A string to hold the TLE in csv format. (out-Character[512])</param>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void TleGPFieldsToCsv(int satNum, char secClass, string satName, int epochYr, double epochDays, double nDotO2, double n2DotO6, double bstar, int ephType, int elsetNum, double incli, double node, double eccen, double omega, double mnAnomaly, double mnMotion, int revNum, byte[] csvLine);


      /// <summary>
      /// Constructs a TLE from GP data stored in the input parameters.
      /// This function only parses data from the input data but DOES NOT load/add the TLE to memory.
      /// 
      /// Returned line1 and line2 will be empty if the function fails to construct the lines as requested.
      /// </summary>
      /// <param name="xa_tle">Array containing TLE's numerical fields (see XA_TLE_?) (in-Double[64])</param>
      /// <param name="xs_tle">Input string that contains all TLE's text fields (see XS_TLE_?) (in-Character[512])</param>
      /// <param name="line1">Returned first line of a TLE (out-Character[512])</param>
      /// <param name="line2">Returned second line of a TLE (out-Character[512])</param>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void TleGPArrayToLines(double[] xa_tle, string xs_tle, byte[] line1, byte[] line2);


      /// <summary>
      /// Constructs a TLE from GP data stored in the input parameters.
      /// This function only parses data from the input data but DOES NOT load/add the TLE to memory.
      /// Returned line1 and line2 will be empty if the function fails to construct the lines as requested.
      /// </summary>
      /// <param name="xa_tle">Array containing TLE's numerical fields (see XA_TLE_?) (in-Double[64])</param>
      /// <param name="xs_tle">Input string that contains all TLE's text fields (see XS_TLE_?) (in-Character[512])</param>
      /// <param name="csvline">Returned csv format elements. (out-Character[512])</param>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void TleGPArrayToCsv(double[] xa_tle, string xs_tle, byte[] csvline);


      /// <summary>
      /// Constructs a TLE from individually provided SP data fields.
      /// Only applies to SP propagator.
      /// This function only parses data from the input fields but DOES NOT load/add the TLE to memory.
      /// Returned line1 and line2 will be empty if the function fails to construct the lines as requested.
      /// </summary>
      /// <param name="satNum">Satellite number (in-Integer)</param>
      /// <param name="secClass">Security classification (in-Character)</param>
      /// <param name="satName">Satellite international designator (in-Character[8])</param>
      /// <param name="epochYr">Element epoch time - year, [YY]YY (in-Integer)</param>
      /// <param name="epochDays">Element epoch time - day of year, DDD.DDDDDDDD (in-Double)</param>
      /// <param name="bTerm">Ballistic coefficient (m^2/kg) (in-Double)</param>
      /// <param name="ogParm">Outgassing parameter/Thrust Acceleration (km/s^2) (in-Double)</param>
      /// <param name="agom">Agom (m^2/kg) (in-Double)</param>
      /// <param name="elsetNum">Element set number (in-Integer)</param>
      /// <param name="incli">Orbit inclination (degrees) (in-Double)</param>
      /// <param name="node">Right ascension of ascending node (degrees) (in-Double)</param>
      /// <param name="eccen">Eccentricity (in-Double)</param>
      /// <param name="omega">Argument of perigee (degrees) (in-Double)</param>
      /// <param name="mnAnomaly">Mean anomaly (degrees) (in-Double)</param>
      /// <param name="mnMotion">Mean motion (rev/day) (in-Double)</param>
      /// <param name="revNum">Revolution number at epoch (in-Integer)</param>
      /// <param name="line1">Returned first line of a TLE. (out-Character[512])</param>
      /// <param name="line2">Returned second line of a TLE. (out-Character[512])</param>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void TleSPFieldsToLines(int satNum, char secClass, string satName, int epochYr, double epochDays, double bTerm, double ogParm, double agom, int elsetNum, double incli, double node, double eccen, double omega, double mnAnomaly, double mnMotion, int revNum, byte[] line1, byte[] line2);


      /// <summary>
      /// Returns the first satKey from the currently loaded set of TLEs that contains the specified satellite number.
      /// This function is useful when Tle.dll is used in applications that require only one record (one TLE entry) for one satellite, and which refer to that TLE by its satellite number. This function can be used to retrieve a satKey in that situation, which is useful since the Standardized Astrodynamic Algorithms library works only with satKeys.
      /// A negative value will be returned if there is an error.
      /// </summary>
      /// <param name="satNum">Satellite number (in-Integer)</param>
      /// <returns>The satellite's unique key</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern long TleGetSatKey(int satNum);


      /// <summary>
      /// This function is similar to TleGetSatKey but designed to be used in Matlab. 
      /// Matlab doesn't correctly return the 19-digit satellite key using TleGetSatKey. This method is an alternative way to return the satKey output.
      /// This function is useful when Tle.dll is used in applications that require only one record (one TLE entry) for one satellite, and which refer to that TLE by its satellite number. This function can be used to retrieve a satKey in that situation, which is useful since the Standardized Astrodynamic Algorithms library works only with satKeys.
      /// A negative value will be returned in satKey if there is an error.
      /// </summary>
      /// <param name="satNum">Satellite number (in-Integer)</param>
      /// <param name="satKey">The satKey of the satellite if a satellite with the requested number is found in the set of loaded satellites, a negative value if there is an error. (out-Long)</param>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void TleGetSatKeyML(int satNum, out long satKey);


      /// <summary>
      /// Computes a satKey from the input data.
      /// There is no need for a matching satellite to be loaded prior to using this function. The function simply computes the satKey from the provided fields.
      /// 
      /// This is the proper way to reconstruct a satKey from its fields. If you use your own routine to do this, the computed satKey might be different. 
      /// A negative value will be returned if there is an error.
      /// </summary>
      /// <param name="satNum">Satellite number (in-Integer)</param>
      /// <param name="epochYr">Element epoch time - year, [YY]YY (in-Integer)</param>
      /// <param name="epochDays">Element epoch time - day of year, DDD.DDDDDDDD (in-Double)</param>
      /// <param name="ephType">Ephemeris type (in-Integer)</param>
      /// <returns>The resulting satellite key if the computation is successful; a negative value if there is an error.</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern long TleFieldsToSatKey(int satNum, int epochYr, double epochDays, int ephType);


      /// <summary>
      /// This function is similar to TleFieldsToSatKey but designed to be used in Matlab. 
      /// Matlab doesn't correctly return the 19-digit satellite key using TleFieldsToSatKey. This method is an alternative way to return the satKey output.
      /// There is no need for a matching satellite to be loaded prior to using this function. The function simply computes the satKey from the provided fields.
      /// 
      /// This is the proper way to reconstruct a satKey from its fields. If you use your own routine to do this, the computed satKey might be different.
      /// A negative value will be returned in satKey if there is an error.
      /// </summary>
      /// <param name="satNum">Satellite number (in-Integer)</param>
      /// <param name="epochYr">Element epoch time - year, [YY]YY (in-Integer)</param>
      /// <param name="epochDays">Element epoch time - day of year, DDD.DDDDDDDD (in-Double)</param>
      /// <param name="ephType">Ephemeris type (in-Integer)</param>
      /// <param name="satKey">The satKey if the computation is successful, a negative value if there is an error. (out-Long)</param>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void TleFieldsToSatKeyML(int satNum, int epochYr, double epochDays, int ephType, out long satKey);


      /// <summary>
      /// Adds a TLE (satellite), using its data stored in the input parameters.
      /// </summary>
      /// <param name="xa_tle">Array containing TLE's numerical fields (see XA_TLE_?) (in-Double[64])</param>
      /// <param name="xs_tle">Input string that contains all TLE's text fields (see XS_TLE_?) (in-Character[512])</param>
      /// <returns>The satKey of the newly added TLE on success, a negative value on error.</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern long TleAddSatFrArray(double[] xa_tle, string xs_tle);


      /// <summary>
      /// This function is similar to TleAddSatFrArray but designed to be used in Matlab. 
      /// </summary>
      /// <param name="xa_tle">Array containing TLE's numerical fields (see XA_TLE_?) (in-Double[64])</param>
      /// <param name="xs_tle">Input string that contains all TLE's text fields (see XS_TLE_?) (in-Character[512])</param>
      /// <param name="satKey">The satKey of the newly added TLE on success, a negative value on error. (out-Long)</param>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void TleAddSatFrArrayML(double[] xa_tle, string xs_tle, out long satKey);


      /// <summary>
      /// Updates existing TLE data with the provided new data stored in the input parameters. Note: satNum, year, day, and ephtype can't be updated.
      /// nDotO2 and n2DotO6 values are not used in the SGP4 propagator. However, some users still want to preserve the integrity of all input data.
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="xa_tle">Array containing TLE's numerical fields (see XA_TLE_?) (in-Double[64])</param>
      /// <param name="xs_tle">Input string that contains all TLE's text fields (see XS_TLE_?) (in-Character[512])</param>
      /// <returns>0 if the TLE is successfully updated, non-0 if there is an error.</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TleUpdateSatFrArray(long satKey, double[] xa_tle, string xs_tle);


      /// <summary>
      /// Retrieves TLE data and stored it in the passing parameters
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="xa_tle">Array containing TLE's numerical fields (see XA_TLE_?) (out-Double[64])</param>
      /// <param name="xs_tle">Output string that contains all TLE's text fields (see XS_TLE_?) (out-Character[512])</param>
      /// <returns>0 if all values are retrieved successfully, non-0 if there is an error</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TleDataToArray(long satKey, double[] xa_tle, byte[] xs_tle);


      /// <summary>
      /// Converts TLE two line format to CSV format
      /// </summary>
      /// <param name="line1">The first line of a two line element set (in-Character[512])</param>
      /// <param name="line2">The second line of a two line element set (in-Character[512])</param>
      /// <param name="csvline">A string to hold the TLE in csv format. (out-Character[512])</param>
      /// <returns>0 if the conversion is successful, non-0 if there is an error.</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TleLinesToCsv(string line1, string line2, byte[] csvline);


      /// <summary>
      /// Converts TLE CSV format to two line format
      /// </summary>
      /// <param name="csvLine">The input TLE in CSV format (in-Character[512])</param>
      /// <param name="newSatno">New satellite number to replace what's in CSV obs if desired (won't use/renumber if it's zero) (in-Integer)</param>
      /// <param name="line1">The output first line of the two line element set (out-Character[512])</param>
      /// <param name="line2">The output second line of the two line element set (out-Character[512])</param>
      /// <returns>0 if the conversion is successful, non-0 if there is an error.</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TleCsvToLines(string csvLine, int newSatno, byte[] line1, byte[] line2);


      /// <summary>
      /// Sets TLE key mode - This function was deprecated, please use DllMain/SetElsetKeyMode() instead
      /// </summary>
      /// <param name="tle_keyMode">Desired Tle key mode (in-Integer)</param>
      /// <returns>0 if the set is successful, non-0 if there is an error.</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SetTleKeyMode(int tle_keyMode);


      /// <summary>
      /// Gets current TLE key mode - This function was deprecated, please use DllMain/GetElsetKeyMode() instead
      /// </summary>
      /// <returns>Current Tle key mode</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int GetTleKeyMode();


      /// <summary>
      /// Finds the check sums of TLE lines
      /// </summary>
      /// <param name="line1">The input TLE line 1 in TLE format (in-Character[512])</param>
      /// <param name="line2">The input TLE line 2 in TLE format (in-Character[512])</param>
      /// <param name="chkSum1">Check Sum of Line 1 (out-Integer)</param>
      /// <param name="chkSum2">Check Sum of Line 2 (out-Integer)</param>
      /// <param name="errCode">Error code - 0 if successful, non-0 if there is an error (out-Integer)</param>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void GetCheckSums(string line1, string line2, out int chkSum1, out int chkSum2, out int errCode);


      /// <summary>
      /// Checks to see if this satKey exists (was already loaded) in the Tle binary tree
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <returns>Returns: 1= key exists; 0= key doesn't exist</returns>
      [DllImport(TleDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TleKeyExists(long satKey);

      // TLE types (TLE ephemeris types) - They are different than ELTTYPE
      public static int
         TLETYPE_SGP = 0,    // TLE SGP elset (Kozai mean motion)
         TLETYPE_SGP4 = 2,    // TLE SGP4 elset (Brouwer mean motion)
         TLETYPE_PPT3 = 3,    // TLE PPT3 elset (Kozai mean motion)
         TLETYPE_XP = 4,    // TLE SGP4-XP elset (Brouwer mean motion)
         TLETYPE_SP = 6;    // TLE SP elset (osculating elements)

      // Indexes of TLE data fields
      public static int
         XF_TLE_SATNUM = 1,    // Satellite number
         XF_TLE_CLASS = 2,    // Security classification U: unclass, C: confidential, S: Secret
         XF_TLE_SATNAME = 3,    // Satellite name A8
         XF_TLE_EPOCH = 4,    // Satellite's epoch time "YYYYJJJ.jjjjjjjj"
         XF_TLE_BSTAR = 5,    // GP B* drag term (1/er)  (not the same as XF_TLE_BTERM) 
         XF_TLE_EPHTYPE = 6,    // Satellite ephemeris type: 0=SGP, 2=SGP4, 4=SGP4-XP, 6=SP
         XF_TLE_ELSETNUM = 7,    // Element set number
         XF_TLE_INCLI = 8,    // Orbit inclination (deg)
         XF_TLE_NODE = 9,    // Right ascension of asending node (deg)
         XF_TLE_ECCEN = 10,    // Eccentricity
         XF_TLE_OMEGA = 11,    // Argument of perigee (deg)
         XF_TLE_MNANOM = 12,    // Mean anomaly (deg)
         XF_TLE_MNMOTN = 13,    // Mean motion (rev/day) (ephType=0: Kozai, ephType=2: Brouwer)
         XF_TLE_REVNUM = 14,    // Revolution number at epoch 

         XF_TLE_NDOT = 15,    // GP Mean motion derivative (rev/day**2 /2)
         XF_TLE_NDOTDOT = 16,    // GP Mean motion second derivative (rev/day**3 /6)
         XF_TLE_AGOMGP = 16,    // Solar radiation pressure GP (m2/kg)

         XF_TLE_SP_AGOM = 5,    // SP Radiation Pressure Coefficient
         XF_TLE_SP_BTERM = 15,    // SP ballistic coefficient (m2/kg)
         XF_TLE_SP_OGPARM = 16,    // SP outgassing parameter (km/s2)

         XF_TLE_ORGSATNUM = 17,    // Original satellite number
         XF_TLE_BTERM = 18,    // GP ballistic coefficient (m2/kg) (not the same as XF_TLE_BSTAR)
         XF_TLE_OBSTIME = 19,    // Time of last observation relative to epoch +/- fractional days
         XF_TLE_EGR = 20,    // Last calculated error growth rate (km/day)
         XF_TLE_EDR = 21,    // Last calculated energy dissipation rate (w/kg)
         XF_TLE_VISMAG = 22,    // Median Vismag 
         XF_TLE_RCS = 23,    // Median RCS - diameter in centimeters (cm)
         XF_TLE_OBJTYPE = 24,    // Object Type (Payload, Rocket Body, Platform, Debris, Unknown)
         XF_TLE_SATNAME_12 = 25;    // Satellite name A12 (upto 12 character long)


      // Indexes of TLE numerical data in an array
      public static int
         // Line 1
         XA_TLE_SATNUM = 0,    // Satellite number
         XA_TLE_EPOCH = 1,    // Satellite's epoch time in DS50UTC
         XA_TLE_NDOT = 2,    // GP Mean motion derivative (rev/day**2 /2)
         XA_TLE_NDOTDOT = 3,    // GP Mean motion second derivative (rev/day**3 /6)
         XA_TLE_BSTAR = 4,    // GP B* drag term (1/er)  
         XA_TLE_EPHTYPE = 5,    // Satellite ephemeris type: 0=SGP, 2=SGP4, 4=SGP4-XP, 6=SP

         // Line 2
         XA_TLE_INCLI = 20,    // Orbit inclination (deg)
         XA_TLE_NODE = 21,    // Right ascension of asending node (deg)
         XA_TLE_ECCEN = 22,    // Eccentricity
         XA_TLE_OMEGA = 23,    // Argument of perigee (deg)
         XA_TLE_MNANOM = 24,    // Mean anomaly (deg)
         XA_TLE_MNMOTN = 25,    // Mean motion (rev/day) (ephType=0, 4: Kozai, ephType=2: Brouwer)
         XA_TLE_REVNUM = 26,    // Revolution number at epoch 
         XA_TLE_ELSETNUM = 30,    // Element set number

         // CSV (or TLE-XP, ephemType=4) specific fields 
         XA_TLE_ORGSATNUM = 31,    // Original satellite number
         XA_TLE_BTERM = 32,    // SP/SGP4-XP ballistic coefficient (m2/kg) 
         XA_TLE_OBSTIME = 33,    // Time of last observation relative to epoch +/- fractional days
         XA_TLE_EGR = 34,    // Last calculated error growth rate (km/day)
         XA_TLE_EDR = 35,    // Last calculated energy dissipation rate (w/kg)
         XA_TLE_VISMAG = 36,    // Median Vismag 
         XA_TLE_RCS = 37,    // Median RCS - diameter in centimeters (cm)

         // CSV (or TLE-XP, ephemType=4)
         XA_TLE_AGOMGP = 38,    // Solar Radiation Pressure Coefficient GP (m2/kg)


         // SP specific fields
         XA_TLE_SP_BTERM = 2,    // SP ballistic coefficient (m2/kg)
         XA_TLE_SP_OGPARM = 3,    // SP outgassing parameter (km/s2)
         XA_TLE_SP_AGOM = 4,    // SP Radiation Pressure Coefficient

         XA_TLE_SIZE = 64;

      // Indexes of TLE text data in an array of chars
      public static int
         XS_TLE_SECCLASS_1 = 0,    // Security classification of line 1 and line 2
         XS_TLE_SATNAME_12 = 1,    // Satellite name
         XS_TLE_OBJTYPE_11 = 13,    // Object Type (Payload, Rocket Body, Platform, Debris, Unknown) - csv only

         XS_TLE_SIZE = 512;

      // TLE's text data fields - new convention (start index, string length)
      public static int
         XS_TLE_SECCLASS_0_1 = 0,    // Security classification of line 1 and line 2
         XS_TLE_SATNAME_1_12 = 1,    // Satellite name
         XS_TLE_OBJTYPE_13_1 = 13,    // Object Type (Payload, Rocket Body, Platform, Debris, Unknown) - csv only

         XS_TLE_LENGTH = 512;

      // Indexes of different TLE file's formats
      public static int
         XF_TLEFORM_ORG = 0,    // Original TLE format
         XF_TLEFORM_CSV = 1;    // CSV format


   }
   // ========================= End of auto generated code ==========================
}
