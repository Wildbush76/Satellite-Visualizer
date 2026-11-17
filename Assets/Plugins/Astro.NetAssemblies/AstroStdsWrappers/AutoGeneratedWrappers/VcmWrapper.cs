// This wrapper file was generated automatically by the GenDllWrappers program.
using System.Runtime.InteropServices;



namespace AFSPC.AstroStds.Wrappers
{
   public static partial class VcmWrapper
   {
      // Provide the path to the dll
      #if WINDOWS
      const string VcmDll = "vcm";
      #else
      const string VcmDll = "libvcm.so";
      #endif
      // static constructor
      static VcmWrapper()
      {
         // Set AS library default path/LD_LIBRARY_PATH environment
         Utilities.Utility.Init();
      }



      /// <summary>
      /// Notes: This function has been deprecated since v9.0. 
      /// Initializes Vcm DLL for use in the program
      /// If this function returns an error, it is recommended that the users stop the program immediately. The error occurs if the users forget to load and initialize all the prerequisite DLLs, as listed in the DLL Prerequisites section, before using this DLL.
      /// </summary>
      /// <param name="apAddr">The handle that was returned from DllMainInit() (in-Long)</param>
      /// <returns>0 if Vcm.dll is initialized successfully, non-0 if there is an error</returns>
      [DllImport(VcmDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int VcmInit(long apAddr);


      /// <summary>
      /// Returns information about the current version of Vcm DLL. The information is placed in the string parameter you pass in
      /// The returned string provides information about the version number, build date, and the platform of the Vcm DLL. 
      /// </summary>
      /// <param name="infoStr">A string to hold the information about Vcm.dll (out-Character[128])</param>
      [DllImport(VcmDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void VcmGetInfo(byte[] infoStr);


      /// <summary>
      /// Loads a text file containing Vcm's
      /// The users can use this function repeatedly to load Vcm records from different input files. However, only unique satKeys are stored in the binary tree. Duplicated satKeys won't be stored. 
      /// 
      /// State vectors can be included directly in the main input file or they can be read from a separate file identified with "ELTFIL=[pathname\filename]" or "VECFIL=[path/filename]" or "SPVMSG=[path/filename]".
      /// 
      /// This function only reads Vcm records from the main input file or Vcm records from the file identified with ELTFIL or VECFIL in the input file. It won't read anything else.
      /// </summary>
      /// <param name="vcmFile">The name of the file containing VCMs to be loaded (in-Character[512])</param>
      /// <returns>0 if the input file is read successfully, non-0 if there is an error</returns>
      [DllImport(VcmDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int VcmLoadFile(string vcmFile);


      /// <summary>
      /// Saves the currently loaded VCM's to a file
      /// If the users call this routine immediately after VcmLoadFile. The VCMs contents in the two file should be the same (minus duplicated VCMs or bad VCMs).
      /// 
      /// The purpose of this function is to save the current state of the loaded VCMs, usually used in GUI applications, for future use.
      /// </summary>
      /// <param name="vcmFile">The name of the file in which to save the settings (in-Character[512])</param>
      /// <param name="saveMode">Specifies whether to create a new file or append to an existing one (0 = create, 1= append) (in-Integer)</param>
      /// <param name="saveForm">Specifies the mode in which to save the file (0 = text format, 1 = not yet implemented, reserved for future) (in-Integer)</param>
      /// <returns>0 if the data is successfully saved to the file, non-0 if there is an error</returns>
      [DllImport(VcmDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int VcmSaveFile(string vcmFile, int saveMode, int saveForm);


      /// <summary>
      /// Removes a VCM represented by the satKey from memory
      /// If the users enter an invalid satKey - a non-existing satKey in memory, the function will return a non-zero value indicating an error.
      /// </summary>
      /// <param name="satKey">The unique key of the satellite to be removed (in-Long)</param>
      /// <returns>0 if the VCM is removed successfully, non-0 if there is an error</returns>
      [DllImport(VcmDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int VcmRemoveSat(long satKey);


      /// <summary>
      /// Removes all VCM's from memory
      /// The function returns zero if all the satellites are removed successfully from the Vcm DLL's binary tree.
      /// </summary>
      /// <returns>0 if all VCM's are removed successfully from memory, non-0 if there is an error</returns>
      [DllImport(VcmDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int VcmRemoveAllSats();


      /// <summary>
      /// Retrieves all of the currently loaded satKeys. These satKeys can be used to access the internal data for the VCM's
      /// See VcmGetLoaded for an example.
      /// This function is useful for dynamically allocating memory for the array that is passed to the function VcmGetLoaded().
      /// </summary>
      /// <returns>The number of VCM's currently loaded</returns>
      [DllImport(VcmDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int VcmGetCount();


      /// <summary>
      /// Retrieves all of the currently loaded satKeys. These satKeys can be used to access the internal data for the VCM's
      /// It is recommended that VcmGetCount() is used to count how many satellites are currently loaded in the Vcm DLL's binary tree. The users then use this number to dynamically allocate the satKeys array and pass it to this function. 
      /// 
      /// If the users prefer to pass a static array to the function, make sure it is big enough to store all the satKeys in memory.
      /// </summary>
      /// <param name="order">Specifies the order in which the satKeys should be returned: 0=ascending, 1=descending, 2=order as loaded (in-Integer)</param>
      /// <param name="satKeys">The array in which to store the satKeys (out-Long[*])</param>
      [DllImport(VcmDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void VcmGetLoaded(int order, long[] satKeys);


      /// <summary>
      /// Adds a VCM using its 1-line or concatenated string formats
      /// If the satellite was previously added to the Vcm DLL's binary tree, the function also returns a negative value indicating an error. 
      /// 
      /// The users can use this function repeatedly to add many satellites (one satellite at a time) to the Vcm DLL's binary tree. 
      /// </summary>
      /// <param name="vcmString">1-line or concatenated string representation of the VCM (in-Character[4000])</param>
      /// <returns>The satKey of the newly added VCM on success, a negative value on error</returns>
      [DllImport(VcmDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern long VcmAddSatFrLines(string vcmString);


      /// <summary>
      /// Works like VcmAddSatFrLines but designed for Matlab
      /// </summary>
      /// <param name="vcmString">1-line or concatenated string representation of the VCM (in-Character[4000])</param>
      /// <param name="satKey">The satKey of the newly added VCM on success, a negative value on error (out-Long)</param>
      [DllImport(VcmDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void VcmAddSatFrLinesML(string vcmString, out long satKey);


      /// <summary>
      /// Adds a VCM using its individually provided field values
      /// 
      /// The table below indicates starting character position for each field in the xs_vcm parameter:
      /// 
      /// table
      /// 
      /// Starting Character Position
      /// Input Description
      /// 
      ///  0satellite name, A8
      ///  8common satellite name, A25
      /// 33geopotential name, A6 (WGS-72, WGS-84, EGM-96, ...)
      /// 39drag model, A12
      /// 51lunar solar pertubations, A3 (ON, OFF)
      /// 54radiation pressure pertubations, A3 (ON, OFF)
      /// 57Earth + ocean tides pertubation, A3 (ON, OFF)
      /// 60intrack thrust, A3 (ON, OFF)
      /// 63integration mode, A6 (ASW, OSW, SPADOC, ...)
      /// 69coordinate system, A5
      /// 74type of partial derivatives, A8
      /// 82step mode, A4 (AUTO, TIME, S)
      /// 86fixed step size indicator, A3 (ON, OFF)
      /// 89initial step size selection, A6 (AUTO, MANUAL)
      /// 
      ///  
      /// The table below indicates the index for each field in the xa_vcm array:
      /// 
      /// table
      /// 
      /// Index (xa_vcm)
      /// index Interpretation
      /// 
      ///   0satellite number
      ///   1satellite's epoch time
      ///   2epoch revolution number
      ///   3J2K position X (km)
      ///   4J2K position Y (km)
      ///   5J2K position Z (km)
      ///   6J2K velocity X (km/s)
      ///   7J2K velocity Y (km/s)
      ///   8J2K velocity Z (km/s)
      ///   9ECI position X (km)
      ///  10ECI position Y (km)
      ///  11ECI position Z (km)
      ///  12ECI velocity X (km/s)
      ///  13ECI velocity Y (km/s)
      ///  14ECI velocity Z (km/s)
      ///  15EFG position X (km)
      ///  16EFG position Y (km)
      ///  17EFG position Z (km)
      ///  18EFG velocity X (km/s)
      ///  19EFG velocity Y (km/s)
      ///  20EFG velocity Z (km/s)
      ///  21geopotential zonals
      ///  22geopotential tesserals
      ///  23ballistic coefficient (m^2/kg)
      ///  24BDOT (m^2/kg-s)
      ///  25solar radiation pressure coefficient (m^2/kg)
      ///  26energy dissipation rate (w/kg)
      ///  27outgassing parameter/thrust acceleration (km/s^2)
      ///  28center of mass offset (m)
      ///  29solar flux F10
      ///  30average F10
      ///  31average Ap
      ///  32TAI - UTC (s)
      ///  33UT1 - UTC (s)
      ///  34UT1 rate (ms/day)
      ///  35polar motion X (arcsec)
      ///  36polar motion Y (arcsec)
      ///  37nutation terms
      ///  38leap second time in days since 1950 UTC
      ///  39initial step size
      ///  40integrator control error
      ///  41position u sigma (km)
      ///  42position v sigma (km)
      ///  43position w sigma (km)
      ///  44velocity u sigma (km/s)
      ///  45velocity v sigma (km/s)
      ///  46velocity w sigma (km/s)
      ///  47covariance matrix size
      ///  48weighted RM of last DC on the satellite
      /// 100the lower triangle portion of the full cov matrix (100-120: 6x6 covmtx, ..., 100-144: 9x9
      /// covmtx)
      /// 
      /// 
      /// The order of the values in the lower half of the covariance matrix is described in the VCM Data Description section.  
      /// 
      /// For a 6x6 covariance matrix, the 21 values should be entered in the xa_vcm array using index values 100-120.  
      /// 
      /// For a 9x9 covariance matrix, the 45 values should be entered using the index values 100-144.
      /// </summary>
      /// <param name="xs_vcm">The input string that contains all VCM's text fields (in-Character[512])</param>
      /// <param name="xa_vcm">The input array that contains all VCM's numerical fields (see XA_VCM_?) (in-Double[512])</param>
      /// <returns>The satKey of the newly added VCM on success, a negative value on error</returns>
      [DllImport(VcmDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern long VcmAddSatFrFields(string xs_vcm, double[] xa_vcm);


      /// <summary>
      /// Works like VcmAddSatFrFields but designed for Matlab
      /// 
      /// See VcmAddSatFrFields for definitions of the xs_vcm and xa_vcm parameters.
      /// </summary>
      /// <param name="xs_vcm">The input string that contains all VCM's text fields (in-Character[512])</param>
      /// <param name="xa_vcm">The input array that contains all VCM's numerical fields (see XA_VCM_?) (in-Double[512])</param>
      /// <param name="satKey">The satKey of the newly added VCM on success, a negative value on error (out-Long)</param>
      [DllImport(VcmDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void VcmAddSatFrFieldsML(string xs_vcm, double[] xa_vcm, out long satKey);


      /// <summary>
      /// Retrieves VCM data associated with the input satKey
      /// </summary>
      /// <param name="satKey">The satKey of the loaded VCM (in-Long)</param>
      /// <param name="xs_vcm">The output string that contains all VCM's text fields (out-Character[512])</param>
      /// <param name="xa_vcm">The output array that contains all VCM's numerical fields (see XA_VCM_?) (out-Double[512])</param>
      /// <returns>0 if the VCM data is successfully retrieved, non-0 if there is an error</returns>
      [DllImport(VcmDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int VcmRetrieveAllData(long satKey, byte[] xs_vcm, double[] xa_vcm);


      /// <summary>
      /// Updates a VCM using its individual field values. Note: satellite's number and epoch won't be updated
      /// 
      /// See VcmAddSatFrFields for definitions of the xs_vcm and xa_vcm parameters.
      /// </summary>
      /// <param name="satKey">The unique key of the satellite to update (in-Long)</param>
      /// <param name="xs_vcm">The input string that contains all VCM's text fields (in-Character[512])</param>
      /// <param name="xa_vcm">The input array that contains all VCM's numerical fields (see XA_VCM_?) (in-Double[512])</param>
      /// <returns>0 if the Vcm data is successfully updated, non-0 if there is an error</returns>
      [DllImport(VcmDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int VcmUpdateSatFrFields(long satKey, string xs_vcm, double[] xa_vcm);


      /// <summary>
      /// Retrieves the value of a specific field of a VCM
      /// 
      /// The table below shows the values for the xf_Vcm parameter:
      /// 
      /// table
      /// 
      /// index
      /// index Interpretation
      /// 
      ///  1  Satellite number I5
      ///  2  Satellite international designator A8
      ///  3  Epoch YYYYDDDHHMMSS.SSS A17
      ///  4  Revolution number I5
      ///  5  position X (km) F16.8 
      ///  6  position Y (km) F16.8 
      ///  7  position Z (km) F16.8   
      ///  8  velocity X (km/s) F16.12
      ///  9  velocity Y (km/s) F16.12
      /// 10  velocity Z (km/s) F16.12
      /// 11  Geo Name A6
      /// 12  Geo zonals I2
      /// 13  Geo tesserals I2
      /// 14  Drag modelel A12 (NONE, JAC70/MSIS90) 
      /// 15  Lunar solar A3 (ON/OFF)
      /// 16  Radiation pressure pertubations A3 (ON/OFF)
      /// 17  Earth + ocean tides pertubations A3 (ON/OFF)
      /// 18  Intrack thrust A3 (ON/OFF)
      /// 19  Ballistic coefficient (m^2/kg)
      /// 20  Radiation pressure coefficient  (m^2/kg)
      /// 21  Outgassing parameter (km/s^2)
      /// 22  Center of mass offset (m)
      /// 23  Solar flux F10 I3
      /// 24  Solar flux F10 average I3
      /// 25  Ap average F5.1
      /// 26  TAI minus UTC (s)I2
      /// 27  UT1 minus UTC (s) F7.5
      /// 28  UT1 rate (ms/day)  F5.3
      /// 29  Polar motion X (arcsec) F6.4
      /// 30  Polar motion Y (arcsec) F6.4
      /// 31  Nutation terms I3
      /// 32  Leap second time YYYYDDDHHMMSS.SSS A17
      /// 33  Integration mode A6 (ASW, OSW, SPADOC)
      /// 34  Type of partial derivatives A8 (ANALYTIC, FULL NUM, FAST NUM)
      /// 35  Integration step mode A4 (AUTO/TIME, S)
      /// 36  Fixed step size indicator A3 (ON/OFF)
      /// 37  Initial step size selection A6 (AUTO/MANUAL)
      /// 38  Initial integration step size F8.3
      /// 39  Integrator error control E9.3
      /// 40  Weighted RMS of last DC E10.5
      /// 41  BDOT (M2/KG-S)
      /// 42  EDR (W/KG)
      /// 
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="xf_Vcm">Predefined number specifying which field to set (in-Integer)</param>
      /// <param name="valueStr">A string to contain the value of the requested field (out-Character[512])</param>
      /// <returns>0 if the VCM is successfully retrieved, non-0 if there is an error</returns>
      [DllImport(VcmDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int VcmGetField(long satKey, int xf_Vcm, byte[] valueStr);


      /// <summary>
      /// Updates the value of a specific field of a VCM
      /// See VcmGetField for a description of the xf_Vcm parameter.
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="xf_Vcm">Predefined number specifying which field to set (in-Integer)</param>
      /// <param name="valueStr">The new value of the specified field, expressed as a string (in-Character[512])</param>
      /// <returns>0 if the VCM is successfully updated, non-0 if there is an error</returns>
      [DllImport(VcmDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int VcmSetField(long satKey, int xf_Vcm, string valueStr);


      /// <summary>
      /// Retrieves all of the data for a VCM in a single function call
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="satNum">Satellite number (out-Integer)</param>
      /// <param name="satName">Satellite name A8 (out-Character[8])</param>
      /// <param name="epochDtg">Satellite epoch time A17 YYYYDDDHHMMSS.SSS (out-Character[17])</param>
      /// <param name="revNum">Revolution number (out-Integer)</param>
      /// <param name="posECI">ECI position (out-Double[3])</param>
      /// <param name="velECI">ECI velocity (out-Double[3])</param>
      /// <param name="geoName">Geopotential name A6 (WGS-72, WGS-84, EGM-96...) (out-Character[6])</param>
      /// <param name="geoZonals">Geopotential zonals (out-Integer)</param>
      /// <param name="geoTesserals">Geopotential tesserals (out-Integer)</param>
      /// <param name="dragModel">Drag model A12 (NONE, JAC70/MSIS90...) (out-Character[12])</param>
      /// <param name="lunarSolar">Lunar solar pertubations A3: ON, OFF (out-Character[3])</param>
      /// <param name="radPress">Radiation pressure pertubations A3: ON, OFF (out-Character[3])</param>
      /// <param name="earthTides">Earth + ocean tides pertubations A3: ON, OFF (out-Character[3])</param>
      /// <param name="intrackThrust">Intrack thrust A3: ON, OFF (out-Character[3])</param>
      /// <param name="bTerm">Ballistic coefficient (m2/kg) E13.10 (out-Double)</param>
      /// <param name="agom">Solar radiation pressure coefficient (m2/kg) E13.10 (out-Double)</param>
      /// <param name="ogParm">Outgassing parameter/Thrust acceleration (m/s2) E13.10 (out-Double)</param>
      /// <param name="cmOffset">Center of mass offset (m) (out-Double)</param>
      /// <param name="f10">Solar flux F10 I3 (out-Integer)</param>
      /// <param name="f10Avg">Soluar flux F10 average I3 (out-Integer)</param>
      /// <param name="apAvg">Ap average F5.1 (out-Double)</param>
      /// <param name="tconRec">1: TaiMinusUTC, 2: UT1MinusUTC, 3: UT1Rate, 4: PolarX, 5: PolarY (out-Double[5])</param>
      /// <param name="nTerms">Number of nutation terms I3 (out-Integer)</param>
      /// <param name="leapYrDtg">Leap second time (out-Character[17])</param>
      /// <param name="integMode">Integration mode A6: ASW, OSW, SPADOC (SPECTR=1 if ASW, OSW) (out-Character[6])</param>
      /// <param name="partials">Type of partial derivatives A8 (Analytic, FULL NUM, FAST NUM) (out-Character[8])</param>
      /// <param name="stepMode">Integrator step mode A4: AUTO, TIME, S (out-Character[4])</param>
      /// <param name="fixStep">Fixed step size indicator A3: ON, OFF (out-Character[3])</param>
      /// <param name="stepSelection">Initial step size selection A6: AUTO, MANUAL (out-Character[6])</param>
      /// <param name="initStepSize">Initial integration step size  F8.3 (out-Double)</param>
      /// <param name="errCtrl">Integrator error control  E9.3 (out-Double)</param>
      /// <param name="rms">Weighted RMS of last DC on the satellite E10.5 (out-Double)</param>
      /// <returns>! 0 if the VCM is successfully retrieved, non-0 if there is an error</returns>
      [DllImport(VcmDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int VcmGetAllFields(long satKey, out int satNum, byte[] satName, byte[] epochDtg, out int revNum, double[] posECI, double[] velECI, byte[] geoName, out int geoZonals, out int geoTesserals, byte[] dragModel, byte[] lunarSolar, byte[] radPress, byte[] earthTides, byte[] intrackThrust, out double bTerm, out double agom, out double ogParm, out double cmOffset, out int f10, out int f10Avg, out double apAvg, double[] tconRec, out int nTerms, byte[] leapYrDtg, byte[] integMode, byte[] partials, byte[] stepMode, byte[] fixStep, byte[] stepSelection, out double initStepSize, out double errCtrl, out double rms);


      /// <summary>
      /// Returns the concatenated string representation of a VCM by the satellite's satKey
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="vcmLines">The resulting concatenated string representation of the VCM (out-Character[4000])</param>
      /// <returns>0 if successful, non-0 on error</returns>
      [DllImport(VcmDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int VcmGetLines(long satKey, byte[] vcmLines);


      /// <summary>
      /// Converts VCM 1-line format to multi-line format (as a concatenated string)
      /// </summary>
      /// <param name="vcm1Line">The input VCM 1-line format (in-Character[1500])</param>
      /// <param name="vcmLines">The resulting concatenated string (out-Character[4000])</param>
      /// <returns>0 if successful, non-0 on error</returns>
      [DllImport(VcmDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int Vcm1LineToMultiLine(string vcm1Line, byte[] vcmLines);


      /// <summary>
      /// Converts VCM multi-line format (as a concatenated string) to 1-line format 
      /// </summary>
      /// <param name="vcmLines">The input concatenated string (in-Character[4000])</param>
      /// <param name="vcm1Line">The resulting VCM 1-line format (out-Character[1500])</param>
      /// <returns>0 if successful, non-0 on error</returns>
      [DllImport(VcmDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int VcmMultiLineTo1Line(string vcmLines, byte[] vcm1Line);


      /// <summary>
      /// Returns the first satKey from the currently loaded set of VCM's that contains the specified satellite number
      /// This function is useful when Vcm dll is used in applications that requires only one record (one Vcm entry) for one satellite and the applications refer to that Vcm by its satellite number. However, the Standardized Astrodynamic Algorithms library is only working with satKeys, this function helps to return the associated satKey of that satellite.
      /// </summary>
      /// <param name="satNum">The input satellite number (in-Integer)</param>
      /// <returns>The satellite's unique key</returns>
      [DllImport(VcmDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern long VcmGetSatKey(int satNum);


      /// <summary>
      /// Works like VcmGetSatKey but designed for Matlab
      /// </summary>
      /// <param name="satNum">The input satellite number (in-Integer)</param>
      /// <param name="satKey">The satellite's unique key (out-Long)</param>
      [DllImport(VcmDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void VcmGetSatKeyML(int satNum, out long satKey);


      /// <summary>
      /// Computes a satKey from the input data
      /// This is the proper way to reconstruct a satKey from its fields. If the users use their own routine to do this, the computed satKey might be different. 
      /// </summary>
      /// <param name="satNum">The input satellite number (in-Integer)</param>
      /// <param name="epochDtg">[yy]yydddhhmmss.sss or [yy]yyddd.ddddddd or DTG15, DTG17, DTG20 (in-Character[20])</param>
      /// <returns>The satellite's unique key</returns>
      [DllImport(VcmDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern long VcmFieldsToSatKey(int satNum, string epochDtg);


      /// <summary>
      /// Works like VcmFieldsToSatKey but designed for Matlab
      /// </summary>
      /// <param name="satNum">The input satellite number (in-Integer)</param>
      /// <param name="epochDtg">[yy]yydddhhmmss.sss or [yy]yyddd.ddddddd or DTG15, DTG17, DTG20 (in-Character[20])</param>
      /// <param name="satKey">The satellite's unique key (out-Long)</param>
      [DllImport(VcmDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void VcmFieldsToSatKeyML(int satNum, string epochDtg, out long satKey);


      /// <summary>
      /// Constructs a multi-line VCM (as a concatenated string) from the VCM data stored in the input arrays.
      /// </summary>
      /// <param name="xa_vcm">Array containing VCM's numerical fields (see XA_VCM_?) (in-Double[512])</param>
      /// <param name="xs_vcm">Input string that contains all VCM's text fields (see XS_VCM_?) (in-Character[512])</param>
      /// <param name="vcmLines">The resulting concatenated string representation of a VCM (out-Character[4000])</param>
      [DllImport(VcmDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void VcmArrayToVcmLines(double[] xa_vcm, string xs_vcm, byte[] vcmLines);


      /// <summary>
      /// Constructs a 1-line VCM from the VCM data stored in the input arrays.
      /// </summary>
      /// <param name="xa_vcm">Array containing VCM's numerical fields (see XA_VCM_?) (in-Double[512])</param>
      /// <param name="xs_vcm">Input string that contains all VCM's text fields (see XS_VCM_?) (in-Character[512])</param>
      /// <param name="vcm1Line">The resulting 1-line VCM (out-Character[1500])</param>
      [DllImport(VcmDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void VcmArrayToVcm1Line(double[] xa_vcm, string xs_vcm, byte[] vcm1Line);


      /// <summary>
      /// Parses data either in 1-line or multi-line (as a concatenated string) VCM and stores that data into the output arrays
      /// This function only parses data from the input VCM but DOES NOT load/add the input VCM to memory.
      /// </summary>
      /// <param name="vcmString">An input 1-line or concatenated string representation of the VCM (in-Character[4000])</param>
      /// <param name="xa_vcm">Array containing VCM's numerical fields (see XA_VCM_?) (out-Double[512])</param>
      /// <param name="xs_vcm">Output string that contains all VCM's text fields (see XS_VCM_?) (out-Character[512])</param>
      /// <returns>0 if the VCM is parsed successfully, non-0 if there is an error.</returns>
      [DllImport(VcmDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int VcmStringToArray(string vcmString, double[] xa_vcm, byte[] xs_vcm);


      /// <summary>
      /// Checks to see if this satKey exists (was already loaded) in the Vcm binary tree
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <returns>Returns: 1= key exists; 0= key doesn't exist</returns>
      [DllImport(VcmDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int VcmKeyExists(long satKey);
      
      // Maximum string length of a multi-line VCM concatenated into one big string
      public static int VCMSTRLEN = 4000;
      
      
      // Maximum string length of a 1-line VCM string
      public static int VCM1LINELEN = 1500;
      
      
      // Starting location of the VCM's text data fields
      public static int  
         XS_VCM_SATNAME       =   0,     // satellite name A8
         XS_VCM_COMMNAME      =   8,     // common satellite name A25
         XS_VCM_GEONAME       =  33,     // geopotential name A6 (WGS-72, WGS-84, EGM-96, ...)
         XS_VCM_DRAGMOD       =  39,     // drag model A12
         XS_VCM_LUNAR         =  51,     // lunar solar pertubations A3 (ON, OFF)
         XS_VCM_RADPRESS      =  54,     // radiation pressure pertubations A3 (ON, OFF)
         XS_VCM_EARTHTIDES    =  57,     // Earth + ocean tides pertubation A3 (ON, OFF)
         XS_VCM_INTRACK       =  60,     // intrack thrust A3 (ON, OFF)
         XS_VCM_INTEGMODE     =  63,     // integration mode A6 (ASW, OSW, SPADOC, ...)
         XS_VCM_COORDSYS      =  69,     // coordinate system A5
         XS_VCM_PARTIALS      =  74,     // type of partial derivatives A8
         XS_VCM_STEPMODE      =  82,     // step mode A4 (AUTO, TIME, S)
         XS_VCM_FIXEDSTEP     =  86,     // fixed step size indicator A3 (ON, OFF)
         XS_VCM_STEPSEL       =  89,     // initial step size selection A6 (AUTO, MANUAL)
         
         XS_VCM_SIZE          = 512;
      
      // VCM's text data fields - new convention (start index, string length)
      public static int  
         XS_VCM_SATNAME_0_8      =   0,     // satellite name A8
         XS_VCM_COMMNAME_8_25    =   8,     // common satellite name A25
         XS_VCM_GEONAME_33_6     =  33,     // geopotential name A6 (WGS-72, WGS-84, EGM-96, ...)
         XS_VCM_DRAGMOD_39_12    =  39,     // drag model A12
         XS_VCM_LUNAR_51_3       =  51,     // lunar solar pertubations A3 (ON, OFF)
         XS_VCM_RADPRESS_54_3    =  54,     // radiation pressure pertubations A3 (ON, OFF)
         XS_VCM_EARTHTIDES_57_3  =  57,     // Earth + ocean tides pertubation A3 (ON, OFF)
         XS_VCM_INTRACK_60_3     =  60,     // intrack thrust A3 (ON, OFF)
         XS_VCM_INTEGMODE_63_6   =  63,     // integration mode A6 (ASW, OSW, SPADOC, ...)
         XS_VCM_COORDSYS_69_5    =  69,     // coordinate system A5
         XS_VCM_PARTIALS_74_8    =  74,     // type of partial derivatives A8
         XS_VCM_STEPMODE_82_4    =  82,     // step mode A4 (AUTO, TIME, S)
         XS_VCM_FIXEDSTEP_86_3   =  86,     // fixed step size indicator A3 (ON, OFF)
         XS_VCM_STEPSEL_89_6     =  89,     // initial step size selection A6 (AUTO, MANUAL)
         
         XS_VCM_LENGTH           = 512;
      
      // Indexes to access data from an array containing VCM numerical data fields
      public static int     
         XA_VCM_SATNUM        =   0,     // satellite number
         XA_VCM_EPOCHDS50UTC  =   1,     // satellite's epoch time
         XA_VCM_REVNUM        =   2,     // epoch revolution number
         XA_VCM_J2KPOSX       =   3,     // J2K position X (km)
         XA_VCM_J2KPOSY       =   4,     // J2K position Y (km)
         XA_VCM_J2KPOSZ       =   5,     // J2K position Z (km)
         XA_VCM_J2KVELX       =   6,     // J2K velocity X (km/s)
         XA_VCM_J2KVELY       =   7,     // J2K velocity Y (km/s)
         XA_VCM_J2KVELZ       =   8,     // J2K velocity Z (km/s)
         XA_VCM_ECIPOSX       =   9,     // ECI position X (km)
         XA_VCM_ECIPOSY       =  10,     // ECI position Y (km)
         XA_VCM_ECIPOSZ       =  11,     // ECI position Z (km)
         XA_VCM_ECIVELX       =  12,     // ECI velocity X (km/s)
         XA_VCM_ECIVELY       =  13,     // ECI velocity Y (km/s)
         XA_VCM_ECIVELZ       =  14,     // ECI velocity Z (km/s)
         XA_VCM_EFGPOSX       =  15,     // EFG position X (km)
         XA_VCM_EFGPOSY       =  16,     // EFG position Y (km)
         XA_VCM_EFGPOSZ       =  17,     // EFG position Z (km)
         XA_VCM_EFGVELX       =  18,     // EFG velocity X (km/s)
         XA_VCM_EFGVELY       =  19,     // EFG velocity Y (km/s)
         XA_VCM_EFGVELZ       =  20,     // EFG velocity Z (km/s)
         XA_VCM_GEOZON        =  21,     // geopotential zonals
         XA_VCM_GEOTES        =  22,     // geopotential tesserals
         XA_VCM_BTERM         =  23,     // ballistic coefficient (m^2/kg)
         XA_VCM_BDOT          =  24,     // BDOT (m^2/kg-s)
         XA_VCM_AGOM          =  25,     // solar radiation pressure coefficient (m^2/kg)
         XA_VCM_EDR           =  26,     // energy dissipation rate (w/kg)
         XA_VCM_OGPARM        =  27,     // outgassing parameter/thrust acceleration (m/s^2)
         XA_VCM_CMOFFSET      =  28,     // center of mass offset (m)
         XA_VCM_F10           =  29,     // solar flux F10
         XA_VCM_F10AVG        =  30,     // average F10
         XA_VCM_APAVG         =  31,     // average Ap
         XA_VCM_TAIMUTC       =  32,     // TAI - UTC (s)
         XA_VCM_UT1MUTC       =  33,     // UT1 - UTC (s)
         XA_VCM_UT1RATE       =  34,     // UT1 rate (ms/day)
         XA_VCM_POLMOTX       =  35,     // polar motion X (arcsec)
         XA_VCM_POLMOTY       =  36,     // polar motion Y (arcsec)
         XA_VCM_NUTTERMS      =  37,     // nutation terms
         XA_VCM_LEAPDS50UTC   =  38,     // leap second time in days since 1950 UTC
         XA_VCM_INITSTEP      =  39,     // initial step size
         XA_VCM_ERRCTRL       =  40,     // integrator control error 
         XA_VCM_POSUSIG       =  41,     // position u sigma (km)   
         XA_VCM_POSVSIG       =  42,     // position v sigma (km)
         XA_VCM_POSWSIG       =  43,     // position w sigma (km)
         XA_VCM_VELUSIG       =  44,     // velocity u sigma (km/s)
         XA_VCM_VELVSIG       =  45,     // velocity v sigma (km/s)
         XA_VCM_VELWSIG       =  46,     // velocity w sigma (km/s)
         XA_VCM_COVMTXSIZE    =  47,     // covariance matrix size
         XA_VCM_RMS           =  48,     // weighted RM of last DC on the satellite
         XA_VCM_COVELEMS      = 100,     // the lower triangle portion of the full cov matrix (100-120: 6x6 covmtx, ..., 100-144: 9x9 covmtx)
         
         XA_VCM_SIZE          = 512;
      
      // Indexes of VCM data fields
      public static int  
         XF_VCM_SATNUM    =  1,      // Satellite number I5
         XF_VCM_SATNAME   =  2,      // Satellite international designator A8
         XF_VCM_EPOCH     =  3,      // Epoch YYYYDDDHHMMSS.SSS A17
         XF_VCM_REVNUM    =  4,      // Revolution number I5
         XF_VCM_POSX      =  5,      // position X (km) F16.8 
         XF_VCM_POSY      =  6,      // position Y (km) F16.8 
         XF_VCM_POSZ      =  7,      // position Z (km) F16.8   
         XF_VCM_VELX      =  8,      // velocity X (km/s) F16.12
         XF_VCM_VELY      =  9,      // velocity Y (km/s) F16.12
         XF_VCM_VELZ      = 10,      // velocity Z (km/s) F16.12
         XF_VCM_GEONAME   = 11,      // Geo Name A6
         XF_VCM_GEOZONALS = 12,      // Geo zonals I2
         XF_VCM_GEOTESSER = 13,      // Geo tesserals I2
         XF_VCM_DRAGMODE  = 14,      // Drag modelel A12 (NONE, JAC70/MSIS90) 
         XF_VCM_LUNSOL    = 15,      // Lunar solar A3 (ON/OFF)
         XF_VCM_RADPRESS  = 16,      // Radiation pressure pertubations A3 (ON/OFF)
         XF_VCM_ERTHTIDES = 17,      // Earth + ocean tides pertubations A3 (ON/OFF)
         XF_VCM_INTRACK   = 18,      // Intrack thrust A3 (ON/OFF)
         XF_VCM_BTERM     = 19,      // Ballistic coefficient (m^2/kg)
         XF_VCM_AGOM      = 20,      // Radiation pressure coefficient  (m^2/kg)
         XF_VCM_OGPARM    = 21,      // Outgassing parameter (m/s^2)
         XF_VCM_CMOFFSET  = 22,      // Center of mass offset (m)
         XF_VCM_F10       = 23,      // Solar flux F10 I3
         XF_VCM_F10AVG    = 24,      // Solar flux F10 average I3
         XF_VCM_APAVG     = 25,      // Ap average F5.1
         XF_VCM_TAIMUTC   = 26,      // TAI minus UTC (s)I2
         XF_VCM_UT1MUTC   = 27,      // UT1 minus UTC (s) F7.5
         XF_VCM_UT1RATE   = 28,      // UT1 rate (ms/day)  F5.3
         XF_VCM_POLARX    = 29,      // Polar motion X (arcsec) F6.4
         XF_VCM_POLARY    = 30,      // Polar motion Y (arcsec) F6.4
         XF_VCM_NTERMS    = 31,      // Nutation terms I3
         XF_VCM_LEAPYR    = 32,      // Leap second time YYYYDDDHHMMSS.SSS A17
         XF_VCM_INTEGMODE = 33,      // Integration mode A6 (ASW, OSW, SPADOC)
         XF_VCM_PARTIALS  = 34,      // Type of partial derivatives A8 (Analytic, FULL NUM, FAST NUM)
         XF_VCM_STEPMODE  = 35,      // Integration step mode A4 (AUTO/TIME, S)
         XF_VCM_FIXEDSTEP = 36,      // Fixed step size indicator A3 (ON/OFF)
         XF_VCM_STEPSLCTN = 37,      // Initial step size selection A6 (AUTO/MANUAL)
         XF_VCM_STEPSIZE  = 38,      // Initial integration step size F8.3
         XF_VCM_ERRCTRL   = 39,      // Integrator error control E9.3
         XF_VCM_RMS       = 40,      // Weighted rms of last DC E10.5
         XF_VCM_BDOT      = 41,      // BDOT (M2/KG-S)
         XF_VCM_EDR       = 42;      // EDR (W/KG)
         
      
      //*******************************************************************************
      // Different ingetration control/step mode
      public static int  
         STEPMODE_AUTO = 0,  
         STEPMODE_TIME = 1,  
         STEPMODE_S    = 2;
         
      
   }
// ========================= End of auto generated code ==========================
}
