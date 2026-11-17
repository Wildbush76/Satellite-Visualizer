// This wrapper file was generated automatically by the GenDllWrappers program.
using System.Runtime.InteropServices;



namespace AFSPC.AstroStds.Wrappers
{
   public static partial class DllMainWrapper
   {
      // Provide the path to the dll
#if WINDOWS
      const string DllMainDll = "dllmain";
#else
      const string DllMainDll = "libdllmain.so";
#endif
      // static constructor
      static DllMainWrapper()
      {
         // Set AS library default path/LD_LIBRARY_PATH environment
         Utilities.Utility.Init();
      }



      /// <summary>
      /// Notes: This function has been deprecated since v9.0. 
      /// Initializes DllMain program (host of Astro Standards libraries) for use in any Astro Standards applications
      /// </summary>
      /// <returns>A handle to the global data set. You will pass this handle to other initialization functions within other DLLs in the API.</returns>
      [DllImport(DllMainDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern long DllMainInit();


      /// <summary>
      /// Returns information about the DllMain DLL. 
      /// The returned string provides information about the version number, build date, and the platform. 
      /// </summary>
      /// <param name="infoStr">A string to hold the information about DllMain.dll. (out-Character[128])</param>
      [DllImport(DllMainDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void DllMainGetInfo(byte[] infoStr);


      /// <summary>
      /// Loads DllMain-related parameters (AS_MOIC) from a text file
      /// </summary>
      /// <param name="dllMainFile">The name of the input file. (in-Character[512])</param>
      /// <returns>Returns zero indicating the input file has been loaded successfully. Other values indicate an error.</returns>
      [DllImport(DllMainDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int DllMainLoadFile(string dllMainFile);


      /// <summary>
      /// Opens a log file and enables the writing of diagnostic information into it. 
      /// All of the DLLs in the library will write diagnostic information into the log file once this function has been called.
      /// If the file specified by logFileName already exists, its contents are erased.
      /// 
      /// Enabling logging can potentially result in large amounts of diagnostic information being generated, which can lead to large amounts of storage being consumed as well as performance decreases. For this reason, it is recommended that this function only be used for debugging purposes.
      /// </summary>
      /// <param name="fileName">The name of the log file to use. (in-Character[512])</param>
      /// <returns>0 if the file was opened successfully. Other values indicate an error.</returns>
      [DllImport(DllMainDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int OpenLogFile(string fileName);


      /// <summary>
      /// Closes the currently open log file and reset the last logged error message to null. 
      /// Remember to close the log file before exiting the program.
      /// </summary>
      [DllImport(DllMainDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void CloseLogFile();


      /// <summary>
      /// Writes a message into the log file.
      /// Make sure the log file is open by calling OpenLogFile before using this function.
      /// 
      /// The message is limited to 128 characters. If the message is longer than this, it will be truncated.
      /// </summary>
      /// <param name="msgStr">A message to be written into the log file. (in-Character[128])</param>
      [DllImport(DllMainDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void LogMessage(string msgStr);


      /// <summary>
      /// Returns a character string describing the last error that occurred. 
      /// As a common practice, this function is called to retrieve the error message when an error occurs.
      /// 
      /// This function works with or without an opened log file.
      /// 
      /// If you call this function before you have called DllMainInit(), the function will return an invalid string. This could result in undefined behavior.
      /// </summary>
      /// <param name="lastErrMsg">A string that stores the last logged error message. The message will be placed in the string you pass to this function. (out-Character[128])</param>
      [DllImport(DllMainDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void GetLastErrMsg(byte[] lastErrMsg);


      /// <summary>
      /// Returns a character string describing the last informational message that was recorded. 
      /// This function is usually called right after space objects (TLEs, VCMs, sensors, observations, etc.) in an input text file were loaded. It gives information about how many records were successfully loaded, how many were bad, and how many were duplicated.
      /// 
      /// This function works with or without an opened log file.
      /// 
      /// If you call this function before you have called DllMainInit(), the function will return an invalid string. This could result in undefined behavior.
      /// This function provides a quick way to check whether all of the prerequisite DLLs have been loaded and initialized correctly. Improper initialization of the Standardized Astrodynamic Algorithms DLLs is one of the most common causes of program crashes.
      /// </summary>
      /// <param name="lastInfoMsg">A string that stores the last logged informational message. The message will be placed in the string you pass to this function. (out-Character[128])</param>
      [DllImport(DllMainDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void GetLastInfoMsg(byte[] lastInfoMsg);


      /// <summary>
      /// Notes: This function has been deprecated since v9.0. 
      /// Returns a list of names of the Standardized Astrodynamic Algorithms DLLs that were initialized successfully.
      /// </summary>
      /// <param name="initDllNames">A string that stores names of the DLLs that were initialized successfully. (out-Character[512])</param>
      [DllImport(DllMainDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void GetInitDllNames(byte[] initDllNames);


      /// <summary>
      /// Tests different input/output data types that are supported by the Astrodynamic Standards library.
      /// </summary>
      /// <param name="cIn">an input character (in-Character)</param>
      /// <param name="cOut">an output character - should return the same value as the input cIn (out-Character)</param>
      /// <param name="intIn">an input 32-bit integer (in-Integer)</param>
      /// <param name="intOut">an output 32-bit integer - should return the same value as the input intIn (out-Integer)</param>
      /// <param name="longIn">an input 64-bit integer (in-Long)</param>
      /// <param name="longOut">an output 64-bit integer - should return the same value as the input longIn (out-Long)</param>
      /// <param name="realIn">an input 64-bit real (in-Double)</param>
      /// <param name="realOut">an output 64-bit real - should return the same value as the input realIn (out-Double)</param>
      /// <param name="strIn">input string (in-Character[512])</param>
      /// <param name="strOut">output string - should return the same value as the input strIn (out-Character[512])</param>
      /// <param name="int1DIn">an input array of 32-bit integers (in-Integer[3])</param>
      /// <param name="int1DOut">an output array of 32-bit integers - should return the same values as the input int1DIn (out-Integer[3])</param>
      /// <param name="long1DIn">an input array of 64-bit integers (in-Long[3])</param>
      /// <param name="long1DOut">an output array of 64-bit integers - should return the same values as the input long1DIn (out-Long[3])</param>
      /// <param name="real1DIn">an input array of 64-bit reals (in-Double[3])</param>
      /// <param name="real1DOut">an output array of 64-bit reals - should return the same values as the input real1DIn (out-Double[3])</param>
      /// <param name="int2DIn">an input 2D-array of 32-bit integers (2 rows, 3 columns) - for column-major order language, reverse the order (in-Integer[2, 3])</param>
      /// <param name="int2DOut">an output 2D-array of 32-bit integers - should return the same values as the input int2DIn (out-Integer[2, 3])</param>
      /// <param name="long2DIn">an input 2D-array of 64-bit integers (2 rows, 3 columns) - for column-major order language, reverse the order (in-Long[2, 3])</param>
      /// <param name="long2DOut">an output 2D-array of 64-bit integers - should return the same values as the input long2DIn (out-Long[2, 3])</param>
      /// <param name="real2DIn">an input 2D-array of 64-bit reals (2 rows, 3 columns) - for column-major order language, reverse the order (in-Double[2, 3])</param>
      /// <param name="real2DOut">an output 2D-array of 64-bit reals - should return the same values as the input real2DIn (out-Double[2, 3])</param>
      [DllImport(DllMainDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void TestInterface(char cIn, out char cOut, int intIn, out int intOut, long longIn, out long longOut, double realIn, out double realOut, string strIn, byte[] strOut, int[] int1DIn, int[] int1DOut, long[] long1DIn, long[] long1DOut, double[] real1DIn, double[] real1DOut, int[,] int2DIn, int[,] int2DOut, long[,] long2DIn, long[,] long2DOut, double[,] real2DIn, double[,] real2DOut);


      /// <summary>
      /// Tests different input/output data types that are supported by the Astrodynamic Standards library.
      /// </summary>
      /// <param name="cInOut">Output should return 'Z' (inout-Character)</param>
      /// <param name="intInOut">Output should return Input + 1 (inout-Integer)</param>
      /// <param name="longInOut">Output should return Input + 2 (inout-Long)</param>
      /// <param name="realInOut">Output should return Input + 42.123456 (inout-Double)</param>
      /// <param name="strInOut">Output should return "It doesn't matter what your string was." (inout-Character[512])</param>
      /// <param name="int1DInOut">Output should return Input + 1 (inout-Integer[3])</param>
      /// <param name="long1DInOut">Output should return Input + 1234567890123456789 (inout-Long[3])</param>
      /// <param name="real1DInOut">Output should return Input + 42.0 (inout-Double[3])</param>
      /// <param name="int2DInOut">Output should return Input + 1 (inout-Integer[2, 3])</param>
      /// <param name="long2DInOut">Output should return Input + 6 (inout-Long[2, 3])</param>
      /// <param name="real2DInOut">Output should return Input + 7.6 (inout-Double[2, 3])</param>
      [DllImport(DllMainDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void TestInterface2(out char cInOut, out int intInOut, out long longInOut, out double realInOut, byte[] strInOut, int[] int1DInOut, long[] long1DInOut, double[] real1DInOut, int[,] int2DInOut, long[,] long2DInOut, double[,] real2DInOut);


      /// <summary>
      /// Tests input and output arrays with unknown length that are supported by the Astrodynamic Standards library.
      /// </summary>
      /// <param name="unk1DIn">Unknown dimension should be length (3) (in-Integer[*])</param>
      /// <param name="unk1DOut">Unknown dimension should be length (3), unk1DOut should return same as unk1DIn * 4 (out-Integer[*])</param>
      /// <param name="unk2DIn">Unknown dimension should be length (2) (in-Integer[*, 3])</param>
      /// <param name="unk2DOut">Unknown dimension should be length (2), unk2DOut should return same as unk2DIn * 5 (out-Integer[*, 3])</param>
      [DllImport(DllMainDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void TestInterface3(int[] unk1DIn, int[] unk1DOut, int[,] unk2DIn, int[,] unk2DOut);


      /// <summary>
      /// Returns data parsed from user's AS_MOIC-typed input card - up to 128 fields are allowed.
      /// </summary>
      /// <param name="arrSize">size of the xa_moc array - actual number of fields the user enters in an "AS_MOIC" input card (in-Integer)</param>
      /// <param name="xa_moic">The returning xa_moc array (out-Double[*])</param>
      [DllImport(DllMainDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void GetMOICData(int arrSize, double[] xa_moic);


      /// <summary>
      /// Sets ELSET key mode
      /// This mode can also be turned on if the user loads an input text file that includes this line - "AS_DMA_ON" -
      /// and is currently calling any of these methods: DllMainLoadFile(), TleLoadFile(), SpVecLoadFile(), or VcmLoadFile()
      /// </summary>
      /// <param name="elset_keyMode">Desired elset key mode (see ELSET_KEYMODE_?) (in-Integer)</param>
      /// <returns>0 if the set is successful, non-0 if there is an error.</returns>
      [DllImport(DllMainDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SetElsetKeyMode(int elset_keyMode);


      /// <summary>
      /// Gets current ELSET key mode
      /// </summary>
      /// <returns>Current elset key mode (see ELSET_KEYMODE_?)</returns>
      [DllImport(DllMainDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int GetElsetKeyMode();


      /// <summary>
      /// Sets key mode for ALL (elsets/obs/sensors). This takes precedence over individual elset/obs/sensor key mode
      /// This mode can also be turned on if the user loads an input text file that includes this line - "AS_DMA_ALL_ON"
      /// </summary>
      /// <param name="all_keyMode">Desired elset key mode (see ALL_KEYMODE_?) (in-Integer)</param>
      /// <returns>0 if the set is successful, non-0 if there is an error.</returns>
      [DllImport(DllMainDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SetAllKeyMode(int all_keyMode);


      /// <summary>
      /// Gets current ALL (elsets/obs/sensors) key mode
      /// </summary>
      /// <returns>Current elset key mode (see ALL_KEYMODE_?)</returns>
      [DllImport(DllMainDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int GetAllKeyMode();


      /// <summary>
      /// Resets ALL (elsets/obs/sensors) key mode to its default value which then allows individual elsets/obs/sensors to use their own key mode settings.
      /// Also reset DUPLICATION key mode to its default value.
      /// </summary>
      [DllImport(DllMainDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void ResetAllKeyMode();


      /// <summary>
      /// Sets DUPLICATION key mode - change the default behavior of returning a key which already exists in memory: zero versus actual value
      /// </summary>
      /// <param name="dupKeyMode">Desired duplication key mode (see DUPKEY_?) (in-Integer)</param>
      /// <returns>0 if the set is successful, non-0 if there is an error.</returns>
      [DllImport(DllMainDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SetDupKeyMode(int dupKeyMode);


      /// <summary>
      /// Gets current DUPLICATION key mode
      /// </summary>
      /// <returns>Current duplication key mode (see DUPKEY_?)</returns>
      [DllImport(DllMainDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int GetDupKeyMode();


      /// <summary>
      /// Retrieves error message associated with the input errCode. 
      /// As a common practice, this function is called to retrieve the error message when an error occurs.
      /// </summary>
      /// <param name="errCode">Input error code (in-Integer)</param>
      /// <param name="errMsg">A string that stores the last logged error message. The message will be placed in the string you pass to this function. (out-Character[128])</param>
      [DllImport(DllMainDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void GetErrMsg(int errCode, byte[] errMsg);


      /// <summary>
      /// Sets error message mode 
      /// mode = 0: Only last error message is stored and then retrieved via GetLastErrMsg(). Appropriate for single-thread applications
      /// mode = 1: Each thread stores and retrieves its own error message via GetErrMsg(). Appropriate for multi-thread applications
      /// </summary>
      /// <param name="mode">Error message mode: 0= use GetLastErrMsg(), 1= use GetErrMsg() to retrieve appropriate error message (in-Integer)</param>
      /// <returns>0 if the set is successful, non-0 if there is an error.</returns>
      [DllImport(DllMainDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int SetErrMsgMode(int mode);


      /// <summary>
      /// Gets error message mode 
      /// mode = 0: Only last error message is stored and then retrieved via GetLastErrMsg(). Appropriate for single-thread applications
      /// mode = 1: Each thread stores and retrieves its own error message via GetErrMsg(). Appropriate for multi-thread applications
      /// </summary>
      /// <returns>Error message mode: 0= use GetLastErrMsg(), 1= use GetErrMsg() to retrieve appropriate error message</returns>
      [DllImport(DllMainDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int GetErrMsgMode();

      // log message string length
      public static int LOGMSGLEN = 128;

      // DHN 06Feb12 - Increase file path length to 512 characters from 128 characters to handle longer file path
      public static int FILEPATHLEN = 512;

      // DHN 10Feb12 - Uniformally using 512 characters to passing/receiving string in all Get/Set Field functions
      public static int GETSETSTRLEN = 512;

      public static int INFOSTRLEN = 128;

      // DHN 10Feb12 - All input card types' (elsets, ob, sensors, ...) can now have maximum of 512 characters
      public static int INPUTCARDLEN = 512;

      // Different orbital element types 
      public static int
         ELTTYPE_TLE_SGP = 1,    // Element type - SGP Tle type 0
         ELTTYPE_TLE_SGP4 = 2,    // Element type - SGP4 Tle type 2
         ELTTYPE_TLE_SP = 3,    // Element type - SP Tle type 6
         ELTTYPE_SPVEC_B1P = 4,    // Element type - SP Vector
         ELTTYPE_VCM = 5,    // Element type - VCM
         ELTTYPE_EXTEPH = 6,    // Element type - External ephemeris
         ELTTYPE_TLE_XP = 7,    // Element type - SGP Tle type 4 - XP
         ELTTYPE_TLE_PPT3 = 8;    // Element type - PPT3 Tle type 3 - XP

      //*******************************************************************************

      // Propagation types
      public static int
         PROPTYPE_GP = 1,       // GP/SGP4/SGP4-XP propagator
         PROPTYPE_SP = 2,       // SP propagator
         PROPTYPE_X = 3,       // External ephemeris
         PROPTYPE_UK = 4;       // Unknown
                                //*******************************************************************************

      // Add sat error 
      public static int
         BADSATKEY = -1,      // Bad satellite key
         BADSATNUM = -1,      // Bad satellite number
         DUPSATKEY = 0;      // Duplicate satellite key

      //*******************************************************************************

      // satellite/observation/sensor key possible errors
      public static int
         BADKEY = -1,      // Bad (satellite/observation/sensor) key
         DUPKEY = 0;      // Duplicate (satellite/observation/sensor) key

      //*******************************************************************************

      // Options used in GetLoaded()   
      public static int
         IDX_ORDER_ASC = 0,    // ascending order
         IDX_ORDER_DES = 1,    // descending order
         IDX_ORDER_READ = 2,    // order as read
         IDX_ORDER_QUICK = 9;    // tree traversal order

      //*******************************************************************************

      // Different key mode options for all elset-satKey/obs-obsKey/sensor-senKey
      public static int
         ALL_KEYMODE_NODUP = 0,    // Default - duplicate elsets/observations/sensors can not be loaded in their binary trees                           
         ALL_KEYMODE_DMA = 1;    // Allow duplicate elsets/obs/sensor to be loaded and have direct memory access (DMA - no duplication check and no binary tree)

      //*******************************************************************************


      // Different key mode options for elset satKey
      public static int
         ELSET_KEYMODE_NODUP = 0,    // Default - duplicate elsets can not be loaded in binary tree                           
         ELSET_KEYMODE_DMA = 1;    // Allow duplicate elsets to be loaded and have direct memory access (DMA - no duplication check and no binary tree)

      //*******************************************************************************

      // Different duplication key mode options (apply to non DMA mode only)
      public static int
         DUPKEY_ZERO = 0,    // Returning (satellite/sensor/obs) key is zero to signify the existing data/key was already in memory
         DUPKEY_ACTUAL = 1;    // Return actual (satellite/sensor/obs) key regardless of the key/data duplication

      //*******************************************************************************


      public static int
         TIME_IS_MSE = 1,     // Input time is in minutes since epoch 
         TIME_IS_TAI = 2,     // Input time is in days since 1950 TAI
         TIME_IS_UTC = 3;     // Input time is in days since 1950 UTC

      //*******************************************************************************   

   }
   // ========================= End of auto generated code ==========================
}
