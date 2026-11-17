// This wrapper file was generated automatically by the GenDllWrappers program.
using System.Runtime.InteropServices;



namespace AFSPC.AstroStds.Wrappers
{
   public static partial class SpVecWrapper
   {
      // Provide the path to the dll
      #if WINDOWS
      const string SpVecDll = "spvec";
      #else
      const string SpVecDll = "libspvec.so";
      #endif
      // static constructor
      static SpVecWrapper()
      {
         // Set AS library default path/LD_LIBRARY_PATH environment
         Utilities.Utility.Init();
      }



      /// <summary>
      /// Notes: This function has been deprecated since v9.0. 
      /// Initializes SpVec DLL for use in the program
      /// If this function returns an error, it is recommended that the users stop the program immediately. The error occurs if the users forget to load and initialize all the prerequisite DLLs, as listed in the DLL Prerequisites section, before using this DLL.
      /// </summary>
      /// <param name="apAddr">The handle that was returned from DllMainInit() (in-Long)</param>
      /// <returns>0 if SpVec.dll is initialized successfully, non-0 if there is an error</returns>
      [DllImport(SpVecDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpVecInit(long apAddr);


      /// <summary>
      /// Returns information about the current version of SpVec DLL. The information is placed in the string parameter you pass in
      /// The returned string provides information about the version number, build date, and the platform of the SpVec DLL. 
      /// </summary>
      /// <param name="infoStr">A string to hold the information about SpVec.dll (out-Character[128])</param>
      [DllImport(SpVecDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void SpVecGetInfo(byte[] infoStr);


      /// <summary>
      /// Loads a text file containing SpVec's
      /// The users can use this function repeatedly to load B1P records from different input files. However, only unique satKeys are stored in the binary tree. Duplicated satKeys won't be stored. 
      /// 
      /// B1P records can be included directly in the main input file or they can be read from a separate file identified with "ELTFIL=[pathname\filename]" or "VECFIL=[path/filename]".
      /// 
      /// This function only reads B1P records from the main input file or B1P records from the file identified with ELTFIL or VECFIL in the input file. It won't read anything else.
      /// </summary>
      /// <param name="spVecFile">The name of the file containing osculating vectors (SpVecs) to be loaded (in-Character[512])</param>
      /// <returns>0 if the input file is read successfully, non-0 if there is an error</returns>
      [DllImport(SpVecDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpVecLoadFile(string spVecFile);


      /// <summary>
      /// Saves the currently loaded SpVecs's to a file
      /// If the users call this routine immediately after SpVecLoadFile. The SPVECs contents in the two file should be the same (minus duplicated SPVECs or bad SPVECs).
      /// 
      /// The purpose of this function is to save the current state of the loaded SPVECs, usually used in GUI applications, for future use.
      /// </summary>
      /// <param name="spVecFile">The name of the file in which to save the settings (in-Character[512])</param>
      /// <param name="saveMode">Specifies whether to create a new file or append to an existing one (0 = create, 1= append) (in-Integer)</param>
      /// <param name="saveForm">Specifies the mode in which to save the file (0 = text format, 1 = not yet implemented, reserved for future) (in-Integer)</param>
      /// <returns>0 if the data is successfully saved to the file, non-0 if there is an error</returns>
      [DllImport(SpVecDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpVecSaveFile(string spVecFile, int saveMode, int saveForm);


      /// <summary>
      /// Removes an SpVec represented by the satKey from memory
      /// If the users enter an invalid satKey, a non-existing satKey in memory, the function will return a non-zero value indicating an error.
      /// </summary>
      /// <param name="satKey">The unique key of the satellite to be removed (in-Long)</param>
      /// <returns>0 if the SpVec is removed successfully, non-0 if there is an error</returns>
      [DllImport(SpVecDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpVecRemoveSat(long satKey);


      /// <summary>
      /// Removes all SpVec's from memory
      /// </summary>
      /// <returns>0 if all SpVec's are removed successfully from memory, non-0 if there is an error</returns>
      [DllImport(SpVecDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpVecRemoveAllSats();


      /// <summary>
      /// Retrieves all of the currently loaded satKeys. These satKeys can be used to access the internal data for the SpVec's
      /// See SpVecGetLoaded for example.
      /// This function is useful for dynamically allocating memory for the array that is passed to the function SpVecGetLoaded().
      /// </summary>
      /// <returns>The number of SpVec's currently loaded</returns>
      [DllImport(SpVecDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpVecGetCount();


      /// <summary>
      /// Retrieves all of the currently loaded satKeys. These satKeys can be used to access the internal data for the SpVec's
      /// It is recommended that SpVecGetCount() is used to count how many satellites are currently loaded in the SpVec DLL's binary tree. The users then use this number to dynamically allocate the satKeys array and pass it to this function. 
      /// 
      /// If the users prefer to pass a static array to the function, make sure it is big enough to store all the satKeys in memory.
      /// </summary>
      /// <param name="order">Specifies the order in which the satKeys should be returned: 0=ascending, 1=descending, 2=order as loaded (in-Integer)</param>
      /// <param name="satKeys">The array in which to store the satKeys (out-Long[*])</param>
      [DllImport(SpVecDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void SpVecGetLoaded(int order, long[] satKeys);


      /// <summary>
      /// Adds an SpVec using its directly specified first and second lines
      /// If the satellite was previously added to the SpVec DLL's binary tree, the function also returns a negative value indicating an error. 
      /// 
      /// The users can use this function repeatedly to add many satellites (one satellite at a time) to the SpVec DLL's binary tree. 
      /// </summary>
      /// <param name="line1">The first input line of the two line element set (in-Character[512])</param>
      /// <param name="line2">The second input line of the two line element set (in-Character[512])</param>
      /// <returns>The satKey of the newly added SpVec on success, a negative value on error</returns>
      [DllImport(SpVecDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern long SpVecAddSatFrLines(string line1, string line2);


      /// <summary>
      /// Works like SpVecAddSatFrLines but designed for Matlab
      /// </summary>
      /// <param name="line1">The first input line of the two line element set (in-Character[512])</param>
      /// <param name="line2">The second input line of the two line element set (in-Character[512])</param>
      /// <param name="satKey">The satKey of the newly added SpVec on success, a negative value on error (out-Long)</param>
      [DllImport(SpVecDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void SpVecAddSatFrLinesML(string line1, string line2, out long satKey);


      /// <summary>
      /// Adds an SpVec using its individually provided field values
      /// </summary>
      /// <param name="pos">position vector (km) (in-Double[3])</param>
      /// <param name="vel">velocity vector (m/s) (in-Double[3])</param>
      /// <param name="secClass">Security classification: U=Unclass, C=Confidential, S=Secret (in-Character)</param>
      /// <param name="satNum">Satellite number (in-Integer)</param>
      /// <param name="satName">Satellite name A8 (in-Character[8])</param>
      /// <param name="epochDtg">Satellite's epoch A17 (YYYYDDDHHMMSS.SSS) (in-Character[17])</param>
      /// <param name="revNum">Revolution number (in-Integer)</param>
      /// <param name="elsetNum">Element set number (in-Integer)</param>
      /// <param name="bterm">Bterm m^2/kg (in-Double)</param>
      /// <param name="agom">Agom  m^2/kg (in-Double)</param>
      /// <param name="ogParm">Outgassing parameter (km/s^2) (in-Double)</param>
      /// <param name="coordSys">Input coordinate system A5 - TMDAT/TMEPO: Epoch, MMB50/MMJ2K: J2000 (in-Character[5])</param>
      /// <returns>The satKey of the newly added SpVec on success, a negative value on error</returns>
      [DllImport(SpVecDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern long SpVecAddSatFrFields(double[] pos, double[] vel, char secClass, int satNum, string satName, string epochDtg, int revNum, int elsetNum, double bterm, double agom, double ogParm, string coordSys);


      /// <summary>
      /// Works like SpVecAddSatFrFields but designed for Matlab 
      /// </summary>
      /// <param name="pos">position vector (km) (in-Double[3])</param>
      /// <param name="vel">velocity vector (m/s) (in-Double[3])</param>
      /// <param name="secClass">Security classification: U=Unclass, C=Confidential, S=Secret (in-Character)</param>
      /// <param name="satNum">Satellite number (in-Integer)</param>
      /// <param name="satName">Satellite name A8 (in-Character[8])</param>
      /// <param name="epochDtg">Satellite's epoch A17 (YYYYDDDHHMMSS.SSS) (in-Character[17])</param>
      /// <param name="revNum">Revolution number (in-Integer)</param>
      /// <param name="elsetNum">Element set number (in-Integer)</param>
      /// <param name="bterm">Bterm m^2/kg (in-Double)</param>
      /// <param name="agom">Agom  m^2/kg (in-Double)</param>
      /// <param name="ogParm">Outgassing parameter (km/s^2) (in-Double)</param>
      /// <param name="coordSys">Input coordinate system A5 - TMDAT/TMEPO: Epoch, MMB50/MMJ2K: J2000 (in-Character[5])</param>
      /// <param name="satKey">The satKey of the newly added SpVec on success, a negative value on error (out-Long)</param>
      [DllImport(SpVecDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void SpVecAddSatFrFieldsML(double[] pos, double[] vel, char secClass, int satNum, string satName, string epochDtg, int revNum, int elsetNum, double bterm, double agom, double ogParm, string coordSys, out long satKey);


      /// <summary>
      /// Updates an SpVec satellite's data in memory using individually provided field values. Note: satNum, epoch string can't be updated.
      /// The satellite's unique key will not be changed in this function call. 
      /// </summary>
      /// <param name="satKey">The sattelite's unique key (in-Long)</param>
      /// <param name="pos">position vector (km) (in-Double[3])</param>
      /// <param name="vel">velocity vector (m/s) (in-Double[3])</param>
      /// <param name="secClass">Security classification: U=Unclass, C=Confidential, S=Secret (in-Character)</param>
      /// <param name="satName">Satellite name A8 (in-Character[8])</param>
      /// <param name="revNum">Revolution number (in-Integer)</param>
      /// <param name="elsetNum">Element set number (in-Integer)</param>
      /// <param name="bterm">Bterm m^2/kg (in-Double)</param>
      /// <param name="agom">Agom  m^2/kg (in-Double)</param>
      /// <param name="ogParm">Outgassing parameter (km/s^2) (in-Double)</param>
      /// <param name="coordSys">Input coordinate system A5 - TMDAT/TMEPO: Epoch, MMB50/MMJ2K: J2000 (in-Character[5])</param>
      /// <returns>0 if the SpVec is successfully updated, non-0 if there is an error</returns>
      [DllImport(SpVecDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpVecUpdateSatFrFields(long satKey, double[] pos, double[] vel, char secClass, string satName, int revNum, int elsetNum, double bterm, double agom, double ogParm, string coordSys);


      /// <summary>
      /// Retrieves the value of a specific field of an SpVec
      /// 
      /// The table below shows the values for the xf_SpVec parameter:
      /// 
      /// table
      /// 
      /// Index
      /// Index Interpretation
      /// 
      ///  1-3First 3 elements of 1P
      ///  4-6Second 3 elements of 1P
      ///  7Security classification
      ///  9Satellite number
      /// 10Satellite common name
      /// 11Epoch date
      /// 12Epoch revolution number
      /// 13Elset number
      /// 14Ballistic coefficient
      /// 15Radiation pressure coefficient
      /// 16Outgassing parameter
      /// 17Input coordinate system
      /// 
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="xf_SpVec">Predefined number specifying which field to set (in-Integer)</param>
      /// <param name="valueStr">A string to contain the value of the requested field (out-Character[512])</param>
      /// <returns>0 if the SpVec is successfully retrieved, non-0 if there is an error</returns>
      [DllImport(SpVecDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpVecGetField(long satKey, int xf_SpVec, byte[] valueStr);


      /// <summary>
      /// Updates the value of a field of an SpVec
      /// See SpVecGetField for a description of the xf_SpVec parameter.  satNum (9) and epoch date (11) cannot be altered.
      /// The set value type was intentionally chosen as a character string because it allows the users to set value for different data types.
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="xf_SpVec">Predefined number specifying which field to set (in-Integer)</param>
      /// <param name="valueStr">The new value of the specified field, expressed as a string (in-Character[512])</param>
      /// <returns>0 if the SpVec is successfully updated, non-0 if there is an error</returns>
      [DllImport(SpVecDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpVecSetField(long satKey, int xf_SpVec, string valueStr);


      /// <summary>
      /// Retrieves all of the data for an SpVec satellite in a single function call
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="pos">position vector (km) (out-Double[3])</param>
      /// <param name="vel">velocity vector (m/s) (out-Double[3])</param>
      /// <param name="secClass">Security classification U: unclass, C: confidential, S: Secret (out-Character)</param>
      /// <param name="satNum">Satellite number (out-Integer)</param>
      /// <param name="satName">Satellite name A8 (out-Character[8])</param>
      /// <param name="epochDtg">Satellite's epoch A17 (YYYYDDDHHMMSS.SSS) (out-Character[17])</param>
      /// <param name="revNum">Revolution number (out-Integer)</param>
      /// <param name="elsetNum">Element set number (out-Integer)</param>
      /// <param name="bterm">Bterm m^2/kg (out-Double)</param>
      /// <param name="agom">Agom  m^2/kg (out-Double)</param>
      /// <param name="ogParm">Outgassing parameter (km/s^2) (out-Double)</param>
      /// <param name="coordSys">Input coordinate system A5 - TMDAT/TMEPO: Epoch, MMB50/MMJ2K: J2000 (out-Character[5])</param>
      /// <returns>0 if the SpVec is successfully retrieved, non-0 if there is an error</returns>
      [DllImport(SpVecDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpVecGetAllFields(long satKey, double[] pos, double[] vel, out char secClass, out int satNum, byte[] satName, byte[] epochDtg, out int revNum, out int elsetNum, out double bterm, out double agom, out double ogParm, byte[] coordSys);


      /// <summary>
      /// Retrieves all of the data for an SpVec satellite in a single function call
      /// </summary>
      /// <param name="line1">the first input line of a 1P/2P card (in-Character[512])</param>
      /// <param name="line2">the second input line of a 1P/2P card (in-Character[512])</param>
      /// <param name="pos">position vector (km) (out-Double[3])</param>
      /// <param name="vel">velocity vector (m/s) (out-Double[3])</param>
      /// <param name="secClass">Security classification U: unclass, C: confidential, S: Secret (out-Character)</param>
      /// <param name="satNum">Satellite number (out-Integer)</param>
      /// <param name="satName">Satellite name A8 (out-Character[8])</param>
      /// <param name="epochDtg">Satellite's epoch A17 (YYYYDDDHHMMSS.SSS) (out-Character[17])</param>
      /// <param name="revNum">Revolution number (out-Integer)</param>
      /// <param name="elsetNum">Element set number (out-Integer)</param>
      /// <param name="bterm">Bterm m^2/kg (out-Double)</param>
      /// <param name="agom">Agom  m^2/kg (out-Double)</param>
      /// <param name="ogParm">Outgassing parameter (km/s^2) (out-Double)</param>
      /// <param name="coordSys">Input coordinate system A5 - TMDAT/TMEPO: Epoch, MMB50/MMJ2K: J2000 (out-Character[5])</param>
      /// <returns>0 if the SpVec data is successfully parsed, non-0 if there is an error</returns>
      [DllImport(SpVecDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpVecParse(string line1, string line2, double[] pos, double[] vel, out char secClass, out int satNum, byte[] satName, byte[] epochDtg, out int revNum, out int elsetNum, out double bterm, out double agom, out double ogParm, byte[] coordSys);


      /// <summary>
      /// Parses SPVEC data from the input first and second lines of an 1P/2P state vector and store that data back into the output parameters.
      /// This function only parses data from the input SPVEC but DOES NOT load/add the input SPVEC to memory.
      /// </summary>
      /// <param name="line1">The first line of the two line element set. (in-Character[512])</param>
      /// <param name="line2">The second line of the two line element set (in-Character[512])</param>
      /// <param name="xa_spVec">Array containing SPVEC's numerical fields (see XA_SPVEC_?) (out-Double[512])</param>
      /// <param name="xs_spVec">Output string that contains all SPVEC's text fields (see XS_SPVEC_?) (out-Character[512])</param>
      /// <returns>0 if the SPVEC is parsed successfully, non-0 if there is an error.</returns>
      [DllImport(SpVecDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpVecLinesToArray(string line1, string line2, double[] xa_spVec, byte[] xs_spVec);


      /// <summary>
      /// Returns the first and second lines of the 1P/2P representation of an SpVec
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="line1">The resulting first line of a 1P/2P card (out-Character[512])</param>
      /// <param name="line2">The resulting second line of a 1P/2P card (out-Character[512])</param>
      /// <returns>0 if successful, non-0 on error</returns>
      [DllImport(SpVecDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpVecGetLines(long satKey, byte[] line1, byte[] line2);


      /// <summary>
      /// Constructs 1P/2P cards from individually provided SpVec data fields
      /// Returned line1 and line2 are empty if the function fails to construct the lines as requested.
      /// </summary>
      /// <param name="pos">position vector (km) (in-Double[3])</param>
      /// <param name="vel">velocity vector (m/s) (in-Double[3])</param>
      /// <param name="secClass">Security classification U: unclass, C: confidential, S: Secret (in-Character)</param>
      /// <param name="satNum">Satellite number (in-Integer)</param>
      /// <param name="satName">Satellite name A8 (in-Character[8])</param>
      /// <param name="epochDtg">Satellite's epoch A17 (YYYYDDDHHMMSS.SSS) (in-Character[17])</param>
      /// <param name="revNum">Revolution number (in-Integer)</param>
      /// <param name="elsetNum">Element set number (in-Integer)</param>
      /// <param name="bterm">Bterm m^2/kg (in-Double)</param>
      /// <param name="agom">Agom  m^2/kg (in-Double)</param>
      /// <param name="ogParm">Outgassing parameter (km/s^2) (in-Double)</param>
      /// <param name="coordSys">Input coordinate system A5 - TMDAT/TMEPO: Epoch, MMB50/MMJ2K: J2000 (in-Character[5])</param>
      /// <param name="line1">The resulting first line of a 1P/2P card (out-Character[512])</param>
      /// <param name="line2">The resulting second line of a 1P/2P card (out-Character[512])</param>
      [DllImport(SpVecDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void SpVecFieldsToLines(double[] pos, double[] vel, char secClass, int satNum, string satName, string epochDtg, int revNum, int elsetNum, double bterm, double agom, double ogParm, string coordSys, byte[] line1, byte[] line2);


      /// <summary>
      /// Constructs 1P/2P cards from SPVEC data stored in the input arrays.
      /// This function only parses data from the input data but DOES NOT load/add the SPVEC to memory.
      /// Returned line1 and line2 will be empty if the function fails to construct the lines as requested.
      /// </summary>
      /// <param name="xa_spVec">Array containing SPVEC's numerical fields (see XA_SPVEC_?) (in-Double[512])</param>
      /// <param name="xs_spVec">Input string that contains all SPVEC's text fields (see XS_SPVEC_?) (in-Character[512])</param>
      /// <param name="line1">Returned first line of an SPVEC. (out-Character[512])</param>
      /// <param name="line2">Returned second line of an SPVEC (out-Character[512])</param>
      [DllImport(SpVecDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void SpVecArrayToLines(double[] xa_spVec, string xs_spVec, byte[] line1, byte[] line2);


      /// <summary>
      /// Returns the first satKey from the currently loaded set of SpVec's that contains the specified satellite number
      /// This function is useful when SpVec DLL is used in applications that requires only one record (one SpVec entry) for one satellite and the applications refer to that SpVec by its satellite number. However, the Standardized Astrodynamic Algorithms library is only working with satKeys, this function helps to return the associated satKey of that satellite.
      /// </summary>
      /// <param name="satNum">The input satellite number (in-Integer)</param>
      /// <returns>The satellite's unique key</returns>
      [DllImport(SpVecDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern long SpVecGetSatKey(int satNum);


      /// <summary>
      /// This function is similar to SpVecGetSatKey but designed to be used in Matlab. 
      /// This function is useful when SpVec DLL is used in applications that requires only one record (one SpVec entry) for one satellite and the applications refer to that SpVec by its satellite number. However, the Standardized Astrodynamic Algorithms library is only working with satKeys, this function helps to return the associated satKey of that satellite.
      /// </summary>
      /// <param name="satNum">The input satellite number (in-Integer)</param>
      /// <param name="satKey">The satellite's unique key (out-Long)</param>
      [DllImport(SpVecDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void SpVecGetSatKeyML(int satNum, out long satKey);


      /// <summary>
      /// Computes a satKey from the input data
      /// This is the proper way to reconstruct a satKey from its fields. If the users use their own routine to do this, the computed satKey might be different.
      /// </summary>
      /// <param name="satNum">The input satellite number (in-Integer)</param>
      /// <param name="epochDtg">[yy]yydddhhmmss.sss or [yy]yyddd.ddddddd or DTG15, DTG17, DTG20 (in-Character[20])</param>
      /// <returns>The resulting satellite key</returns>
      [DllImport(SpVecDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern long SpVecFieldsToSatKey(int satNum, string epochDtg);


      /// <summary>
      /// This function is similar to SpVecFieldsToSatKey but designed to be used in Matlab. 
      /// This is the proper way to reconstruct a satKey from its fields. If the users use their own routine to do this, the computed satKey might be different.
      /// </summary>
      /// <param name="satNum">The input satellite number (in-Integer)</param>
      /// <param name="epochDtg">[yy]yydddhhmmss.sss or [yy]yyddd.ddddddd or DTG15, DTG17, DTG20 (in-Character[20])</param>
      /// <param name="satKey">The resulting satellite key (out-Long)</param>
      [DllImport(SpVecDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void SpVecFieldsToSatKeyML(int satNum, string epochDtg, out long satKey);


      /// <summary>
      /// Adds an SpVec using its individually provided field values
      /// </summary>
      /// <param name="xa_spVec">Array containing SPVEC's numerical fields (see XA_SPVEC_?) (in-Double[512])</param>
      /// <param name="xs_spVec">Input string that contains all SPVEC's text fields (see XS_SPVEC_?) (in-Character[512])</param>
      /// <returns>The satKey of the newly added SPVEC on success, a negative value on error.</returns>
      [DllImport(SpVecDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern long SpVecAddSatFrArray(double[] xa_spVec, string xs_spVec);


      /// <summary>
      /// Adds an SpVec using its individually provided field values
      /// </summary>
      /// <param name="xa_spVec">Array containing SPVEC's numerical fields (see XA_SPVEC_?) (in-Double[512])</param>
      /// <param name="xs_spVec">Input string that contains all SPVEC's text fields (see XS_SPVEC_?) (in-Character[512])</param>
      /// <param name="satKey">The satKey of the newly added SPVEC on success, a negative value on error. (out-Long)</param>
      [DllImport(SpVecDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void SpVecAddSatFrArrayML(double[] xa_spVec, string xs_spVec, out long satKey);


      /// <summary>
      /// Updates existing SPVEC data with the provided new data stored in the input parameters. Note: satNum, epoch string can't be updated.
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="xa_spVec">Array containing SPVEC's numerical fields (see XA_SPVEC_?) (in-Double[512])</param>
      /// <param name="xs_spVec">Input string that contains all SPVEC's text fields (see XS_SPVEC_?) (in-Character[512])</param>
      /// <returns>0 if the SPVEC is successfully updated, non-0 if there is an error.</returns>
      [DllImport(SpVecDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpVecUpdateSatFrArray(long satKey, double[] xa_spVec, string xs_spVec);


      /// <summary>
      /// Retrieves SPVEC data and stored it in the passing parameters
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <param name="xa_spVec">Array containing SPVEC's numerical fields (see XA_SPVEC_?) (out-Double[512])</param>
      /// <param name="xs_spVec">Output string that contains all SPVEC's text fields (see XS_SPVEC_?) (out-Character[512])</param>
      /// <returns>0 if all values are retrieved successfully, non-0 if there is an error</returns>
      [DllImport(SpVecDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpVecDataToArray(long satKey, double[] xa_spVec, byte[] xs_spVec);


      /// <summary>
      /// Checks to see if this satKey exists (was already loaded) in the SpVec binary tree
      /// </summary>
      /// <param name="satKey">The satellite's unique key (in-Long)</param>
      /// <returns>Returns: 1= key exists; 0= key doesn't exist</returns>
      [DllImport(SpVecDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SpVecKeyExists(long satKey);
      
      // Indexes of SPVEC data fields
      public static int  
         XF_SPVEC_POS1     =  1,      // X component of satellite's position (km)
         XF_SPVEC_POS2     =  2,      // Y component of satellite's position (km)
         XF_SPVEC_POS3     =  3,      // Z component of satellite's position (km)
         XF_SPVEC_VEL1     =  4,      // X component of satellite's velocity (m/s)
         XF_SPVEC_VEL2     =  5,      // Y component of satellite's velocity (m/s)
         XF_SPVEC_VEL3     =  6,      // Z component of satellite's velocity (m/s)
         XF_SPVEC_SECCLASS =  7,      // Security classification
         XF_SPVEC_SATNUM   =  9,      // Satellite number
         XF_SPVEC_SATNAME  = 10,      // Satellite common name
         XF_SPVEC_EPOCH    = 11,      // Epoch date
         XF_SPVEC_REVNUM   = 12,      // Epoch revolution number
         XF_SPVEC_ELSETNUM = 13,      // Elset number
         XF_SPVEC_BTERM    = 14,      // Ballistic coefficient (m^2/kg)
         XF_SPVEC_AGOM     = 15,      // Radiation pressure coefficient (m^2/kg)
         XF_SPVEC_OGPARM   = 16,      // Outgassing parameter (km/s^2)
         XF_SPVEC_INPCOORD = 17;      // Inpute coordinate system
         
         
      // Indexes of SPVEC numerical data in an array
      public static int  
         XA_SPVEC_SATNUM   =   0,      // Satellite number
         XA_SPVEC_EPOCH    =   1,      // Epoch date in days since 1950 UTC
         XA_SPVEC_REVNUM   =   2,      // Epoch revolution number
         XA_SPVEC_ELSETNUM =   3,      // Elset number
         XA_SPVEC_BTERM    =   4,      // Ballistic coefficient (m^2/kg)
         XA_SPVEC_AGOM     =   5,      // Radiation pressure coefficient (m^2/kg)
         XA_SPVEC_OGPARM   =   6,      // Outgassing parameter (km/s^2)
         XA_SPVEC_INPCOORD =   7,      // Inpute coordinate systemm, see SPVEC_INPCOORD_? for available options
      
         XA_SPVEC_POS1     =  20,      // X component of satellite's position (km)
         XA_SPVEC_POS2     =  21,      // Y component of satellite's position (km)
         XA_SPVEC_POS3     =  22,      // Z component of satellite's position (km)
         XA_SPVEC_VEL1     =  23,      // X component of satellite's velocity (m/s)
         XA_SPVEC_VEL2     =  24,      // Y component of satellite's velocity (m/s)
         XA_SPVEC_VEL3     =  25,      // Z component of satellite's velocity (m/s)
         
         XA_SPVEC_HASOWNCRL=  70,      // Flag to indicate SP vec has its own numerical integration control (vs global 4P settings)
         XA_SPVEC_GEOIDX   =  71,      // Geopotential model to use
         XA_SPVEC_BULGEFLG =  72,      // Earth gravity pertubations flag
         XA_SPVEC_DRAGFLG  =  73,      // Drag pertubations flag 
         XA_SPVEC_RADFLG   =  74,      // Radiation pressure pertubations flag
         XA_SPVEC_LUNSOLFLG=  75,      // Lunar/Solar pertubations flag
         XA_SPVEC_F10      =  76,      // F10 value
         XA_SPVEC_F10AVG   =  77,      // F10 average value
         XA_SPVEC_AP       =  78,      // Ap value
         XA_SPVEC_TRUNC    =  79,      // Geopotential truncation order/degree/zonals
         XA_SPVEC_CONVERG  =  80,      // Corrector step convergence criterion, exponent of 1/10, default = 10
         XA_SPVEC_OGFLG    =  81,      // Outgassing pertubations flag
         XA_SPVEC_TIDESFLG =  82,      // Solid earth and ocean tide pertubations flag
         XA_SPVEC_NTERMS   =  84,      // Nutation terms
         XA_SPVEC_REEVAL   =  85,      // Recompute pertubations at each corrector step
         XA_SPVEC_INTEGCTRL=  86,      // Variable of intergration control
         XA_SPVEC_VARSTEP  =  87,      // Variable step size control
         XA_SPVEC_INITSTEP =  88,      // Initial step size
         
         XA_SPVEC_RMS      =  99,      // weighted RM of last DC on the satellite 
         XA_SPVEC_COVELEMS = 100,      // the lower triangle portion of the full cov matrix (100-120: 6x6 covmtx, ..., 100-154: 10x10 covmtx)
         
         XA_SPVEC_SIZE     = 512;
         
      
      // Indexes of SPVEC text data in an array of chars
      public static int     
         XS_SPVEC_SECCLASS_1 =  0,      // Security classification
         XS_SPVEC_SATNAME_8  =  1,      // Satellite common name
         
         XS_SPVEC_SIZE       = 512;
      
      // SPVEC's text data fields - new convention (start index, string length)
      public static int     
         XS_SPVEC_SECCLASS_0_1 =  0,      // Security classification
         XS_SPVEC_SATNAME_1_8  =  1,      // Satellite common name
         
         XS_SPVEC_LENGTH     = 512;
      
         
      // Different input coordinate for SpVec
      public static int     
         SPVEC_INPCOORD_4P    =  0,      // Use input coordinate specified in 4P-card
         SPVEC_INPCOORD_TMDAT =  1,      // Input coordinate systems is coordinates of epoch
         SPVEC_INPCOORD_MMJ2K =  2;      // Input coordinate systems is coordinates of J2000
      
   }
// ========================= End of auto generated code ==========================
}
