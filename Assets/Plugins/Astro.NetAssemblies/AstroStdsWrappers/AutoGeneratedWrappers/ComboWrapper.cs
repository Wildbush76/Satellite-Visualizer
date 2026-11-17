// This wrapper file was generated automatically by the GenDllWrappers program.
using System.Runtime.InteropServices;



namespace AFSPC.AstroStds.Wrappers
{
   public static partial class ComboWrapper
   {
      // Provide the path to the dll
      #if WINDOWS
      const string ComboDll = "combo";
      #else
      const string ComboDll = "libcombo.so";
      #endif
      // static constructor
      static ComboWrapper()
      {
         // Set AS library default path/LD_LIBRARY_PATH environment
         Utilities.Utility.Init();
      }



      /// <summary>
      /// Notes: This function has been deprecated since v9.0. 
      /// Initializes Combo dll for use in the program
      /// If this function returns an error, it is recommended that the users stop the program immediately. The error occurs if the users forget to load and initialize all the prerequisite DLLs, as listed in the DLL Prerequisite section, before using this DLL.
      /// </summary>
      /// <param name="apAddr">The handle that was returned from DllMainInit() (in-Long)</param>
      /// <returns>0 if the Combo dll is initialized successfully, non-0 if there is an error</returns>
      [DllImport(ComboDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ComboInit(long apAddr);


      /// <summary>
      /// Returns information about the current version of Combo.dll. The information is placed in the string parameter you pass in
      /// The returned string provides information about the version number, build date, and the platform of the Combo DLL. 
      /// </summary>
      /// <param name="infoStr">A string to hold the information about Combo.dll (out-Character[128])</param>
      [DllImport(ComboDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void ComboGetInfo(byte[] infoStr);


      /// <summary>
      /// Loads Combo-related parameters (7P/8P/9P cards, and Combo parameter free format) from a text file
      /// </summary>
      /// <param name="comboInputFile">The name of the file containing Combo-related parameters (in-Character[512])</param>
      /// <returns>0 if the input file is read successfully, non-0 if there is an error</returns>
      [DllImport(ComboDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ComboLoadFile(string comboInputFile);


      /// <summary>
      /// Loads Combo control parameters and all Combo related data (environment, time, elsets) from an input text file
      /// </summary>
      /// <param name="comboInputFile">The name of the file containing Combo control parameters and all Combo related data (in-Character[512])</param>
      /// <returns>0 if the input file is read successfully, non-0 if there is an error</returns>
      [DllImport(ComboDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ComboLoadFileAll(string comboInputFile);


      /// <summary>
      /// Loads a single Combo-typed card (7P, 8P, and 9P)
      /// </summary>
      /// <param name="card">Combo-type input card (in-Character[512])</param>
      /// <returns>0 if the input card is read successfully, non-0 if there is an error</returns>
      [DllImport(ComboDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ComboLoadCard(string card);


      /// <summary>
      /// Saves any currently loaded COMBO-related settings to a file
      /// The purpose of this function is to save the current Combo-related settings, usually used in GUI applications, for future use.
      /// </summary>
      /// <param name="comboFile">The name of the file in which to save the settings (in-Character[512])</param>
      /// <param name="saveMode">Specifies whether to create a new file or append to an existing one (0 = create, 1= append) (in-Integer)</param>
      /// <param name="saveForm">Specifies the mode in which to save the file (0 = text format, 1 = not yet implemented, reserved for future) (in-Integer)</param>
      /// <returns>0 if the data is successfully saved to the file, non-0 if there is an error</returns>
      [DllImport(ComboDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ComboSaveFile(string comboFile, int saveMode, int saveForm);


      /// <summary>
      /// Returns the number of primary and secondary satellites that were entered via 7P-card, 8P-card, 9P-card, or Combo parameter free format 
      /// </summary>
      /// <param name="numPriSats">The number of primary satellites that were entered via 7P/9P cards or Combo parameter free format (out-Integer)</param>
      /// <param name="numSecSats">The number of secondary satellites that were entered via 7P/8P cards or Combo parameter free format (out-Integer)</param>
      [DllImport(ComboDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void ComboGetNumOfPriSecSats(out int numPriSats, out int numSecSats);


      /// <summary>
      /// Returns an array of primary satellite numbers that were entered via 7P-card, 9P-card, or Combo parameter free format
      /// </summary>
      /// <param name="priSatNums">An array to hold the primary satellite numbers (out-Integer[*])</param>
      [DllImport(ComboDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void ComboGetPriSatNums(int[] priSatNums);


      /// <summary>
      /// Returns an array of secondary satellite numbers that were entered via 7P-card, 8P-card, or Combo parameter free format
      /// </summary>
      /// <param name="secSatNums">An array to hold the secondary satellite numbers (out-Integer[*])</param>
      [DllImport(ComboDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void ComboGetSecSatNums(int[] secSatNums);


      /// <summary>
      /// Constructs Combo 7P-Card from COMBO control parameters
      /// </summary>
      /// <param name="combo7pCard">A string to hold the resulting Combo Parameter Card (7P-Card) (out-Character[512])</param>
      [DllImport(ComboDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void ComboGet7pCard(byte[] combo7pCard);


      /// <summary>
      /// Retrieves all Combo control parameters with a single function call
      /// </summary>
      /// <param name="timeFlg">Input time format:	0 = Input time is in minutes since epoch (MSE),	1 = Input time is in days since 1950, UTC (ds50UTC) (out-Integer)</param>
      /// <param name="startTime">Begin time in ds50UTC or MSE, depending on timeFlg (out-Double)</param>
      /// <param name="stopTime">End time  in ds50UTC or MSE, depending on timeFlg (out-Double)</param>
      /// <param name="relMinFlg">Control for computing relative minima: 0 = Do not compute relative minima, 1 = Compute relative minima (out-Integer)</param>
      /// <param name="absMinFlg">Control for computing absolute minimum:	0 = Do not compute absolute minimum, 1 = Compute absolute minimum (out-Integer)</param>
      /// <param name="closeProxFlg">Control for computing periods of close proximity: 0 = Do not compute periods of close proximity, 1 = Compute periods of close proximity (out-Integer)</param>
      /// <param name="relEphFlg">Control for computing relative ephemeris: 0 = Do not compute relative ephemeris, 1 = Compute relative ephemeris (out-Integer)</param>
      /// <param name="pocSigma">Probability of collision sigma (out-Double)</param>
      /// <param name="relMinLim">Maximum separation for relative minima (km) (out-Double)</param>
      /// <param name="closeProxLim">Close proximity limit (out-Double)</param>
      /// <param name="relEphInterval">Relative ephemeris sampling interval (min) (out-Double)</param>
      /// <param name="prtOpt">Print options:	0 = Relative geometry only, 1 = Position/velocity (plus option 0), 2 = Lat, lon, height (plus option 1), 3 = Relative position (plus option 2), 4 = No output (out-Integer)</param>
      [DllImport(ComboDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void ComboGet7pAll(out int timeFlg, out double startTime, out double stopTime, out int relMinFlg, out int absMinFlg, out int closeProxFlg, out int relEphFlg, out double pocSigma, out double relMinLim, out double closeProxLim, out double relEphInterval, out int prtOpt);


      /// <summary>
      /// Sets all Combo control parameters with a single function call
      /// </summary>
      /// <param name="timeFlg">Input time format:	0 = Input time is in minutes since epoch (MSE),	1 = Input time is in days since 1950, UTC (ds50UTC) (in-Integer)</param>
      /// <param name="startTime">Begin time in ds50UTC or MSE, depending on timeFlg (in-Double)</param>
      /// <param name="stopTime">End time  in ds50UTC or MSE, depending on timeFlg (in-Double)</param>
      /// <param name="relMinFlg">Control for computing relative minima: 0 = Do not compute relative minima, 1 = Compute relative minima (in-Integer)</param>
      /// <param name="absMinFlg">Control for computing absolute minimum:	0 = Do not compute absolute minimum, 1 = Compute absolute minimum (in-Integer)</param>
      /// <param name="closeProxFlg">Control for computing periods of close proximity: 0 = Do not compute periods of close proximity, 1 = Compute periods of close proximity (in-Integer)</param>
      /// <param name="relEphFlg">Control for computing relative ephemeris: 0 = Do not compute relative ephemeris, 1 = Compute relative ephemeris (in-Integer)</param>
      /// <param name="pocSigma">Probability of collision sigma (in-Double)</param>
      /// <param name="relMinLim">Maximum separation for relative minima (km) (in-Double)</param>
      /// <param name="closeProxLim">Close proximity limit (in-Double)</param>
      /// <param name="relEphInterval">Relative ephemeris sampling interval (min) (in-Double)</param>
      /// <param name="prtOpt">Print options:	0 = Relative geometry only, 1 = Position/velocity (plus option 0), 2 = Lat, lon, height (plus option 1), 3 = Relative position (plus option 2), 4 = No output (in-Integer)</param>
      [DllImport(ComboDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void ComboSet7pAll(int timeFlg, double startTime, double stopTime, int relMinFlg, int absMinFlg, int closeProxFlg, int relEphFlg, double pocSigma, double relMinLim, double closeProxLim, double relEphInterval, int prtOpt);


      /// <summary>
      /// Retrieves the value of a single Combo control parameter (7P-card)
      /// </summary>
      /// <param name="xf_7P">Predefined number specifying which Combo control parameter to retrieve (see XF_7P_?) (in-Integer)</param>
      /// <param name="retVal">A string to hold the value of the requested COMBO control parameter (out-Character[512])</param>
      [DllImport(ComboDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void ComboGet7pField(int xf_7P, byte[] retVal);


      /// <summary>
      /// Sets the value of a single Combo control parameter (7P-card)
      /// See ComboGet7pField for a list of index values for the parameter xf_7P.
      /// </summary>
      /// <param name="xf_7P">Predefined number specifying which Combo control parameter to set (see XF_7P_?) (in-Integer)</param>
      /// <param name="valueStr">the new value of the specified COMBO control parameter, expressed as a string (in-Character[512])</param>
      [DllImport(ComboDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void ComboSet7pField(int xf_7P, string valueStr);


      /// <summary>
      /// Returns computation of miss between orbits (COMBO) data for the specified primary and secondary satellites
      /// 
      /// If absMinDs50UTC = 0 or absMinDist = 1.0E20, this means no absolute minimum found. Otherwise, the absMinDs50UTC and absMinDist are returned whether or not absMinDist is greater than the specified "Close proximity limit".
      /// 
      /// Returns zero indicating the COMBO data was computed successfully. Other values indicate an error. Here is the list of possible error/warning code it returns:
      /// 
      ///     table
      /// 
      /// Value
      /// Value Interpretation
      /// 
      /// 101 Primary satellite's epoch too far from the time span
      /// 102 Secondary satellite's epoch too far from the time span
      /// 103 Primary/secondary satellites are identical
      /// 104 Secondary satellite failed perigee/apogee test
      /// 105 Secondary satellite is considered a brief span
      /// 106 Secondary satellite is in sustained relative geometry
      /// 
      /// 
      /// When the function's returned value is 108 (secondary satellite is in sustained relative geometry), the entry/exit times should be ignored.
      /// </summary>
      /// <param name="priSatKey">The primary satellite's unique key (in-Long)</param>
      /// <param name="secSatKey">The secondary satellite's unique key (in-Long)</param>
      /// <param name="absMinDs50UTC">Time (in days since 1950, UTC) when the primary and secondary satellites are in closest approach (absolute minimum) (out-Double)</param>
      /// <param name="absMinDist">Absolute minimum distance (km) between primary and secondary satellites obtained at the time absMinDs50UTC (out-Double)</param>
      /// <param name="relMinDs50UTCs">Times (in days since 1950, UTC) when  the primary and secondary satellites are at relative minima (out-Double[1000])</param>
      /// <param name="numOfRelMins">Number of relative minima found during the requested time span (out-Integer)</param>
      /// <param name="entryDs50UTCs">Times (in days since 1950, UTC) when the primary and secondary satellites approach the distance as specified in the "Maximum separation for relative minima" (out-Double[1000])</param>
      /// <param name="exitDs50UTCs">Times (in days since 1950, UTC) when the primary and secondary satellites leave the distance as specified in the "Maximum separation for relative minima" (out-Double[1000])</param>
      /// <param name="numOfEntryExits">Number of times when the primary and secondary satellites enter and exit the "Maximum separation for relative minima" (out-Integer)</param>
      /// <returns>0 if the COMBO data was computed successfully, non-0 if there is an error</returns>
      [DllImport(ComboDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ComboCompPriSec(long priSatKey, long secSatKey, out double absMinDs50UTC, out double absMinDist, double[] relMinDs50UTCs, out int numOfRelMins, double[] entryDs50UTCs, double[] exitDs50UTCs, out int numOfEntryExits);


      /// <summary>
      /// Returns computation of miss between orbits (COMBO) data for the specified primary and secondary satellites
      /// This function is similar to ComboCompPriSec but allows the user to enter separate COMBO parameters for each call which is suitable for multithread application
      /// </summary>
      /// <param name="xa_cb_parms">Combo control parameters (see XA_CB_PARMS_?) (in-Double[16])</param>
      /// <param name="priSatKey">The primary satellite's unique key (in-Long)</param>
      /// <param name="secSatKey">The secondary satellite's unique key (in-Long)</param>
      /// <param name="absMinDs50UTC">Time (in days since 1950, UTC) when the primary and secondary satellites are in closest approach (absolute minimum) (out-Double)</param>
      /// <param name="absMinDist">Absolute minimum distance (km) between primary and secondary satellites obtained at the time absMinDs50UTC (out-Double)</param>
      /// <param name="relMinDs50UTCs">Times (in days since 1950, UTC) when  the primary and secondary satellites are at relative minima (out-Double[1000])</param>
      /// <param name="numOfRelMins">Number of relative minima found during the requested time span (out-Integer)</param>
      /// <param name="entryDs50UTCs">Times (in days since 1950, UTC) when the primary and secondary satellites approach the distance as specified in the "Maximum separation for relative minima" (out-Double[1000])</param>
      /// <param name="exitDs50UTCs">Times (in days since 1950, UTC) when the primary and secondary satellites leave the distance as specified in the "Maximum separation for relative minima" (out-Double[1000])</param>
      /// <param name="numOfEntryExits">Number of times when the primary and secondary satellites enter and exit the "Maximum separation for relative minima" (out-Integer)</param>
      /// <returns>0 if the COMBO data was computed successfully, non-0 if there is an error</returns>
      [DllImport(ComboDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ComboCompPriSec_MT(double[] xa_cb_parms, long priSatKey, long secSatKey, out double absMinDs50UTC, out double absMinDist, double[] relMinDs50UTCs, out int numOfRelMins, double[] entryDs50UTCs, double[] exitDs50UTCs, out int numOfEntryExits);


      /// <summary>
      /// Computes probability of collision between a primary and secondary satellite
      /// </summary>
      /// <param name="priPosEfg">The primary satellite's ECI position vector (km) (in-Double[3])</param>
      /// <param name="priVelEfg">The primary satellite's ECI velocity vector (km/s) (in-Double[3])</param>
      /// <param name="priSatCov">The primary satellite's 3x3 covariance matrix (in-Double[3, 3])</param>
      /// <param name="priSatRad">The primary satellite's effective radius (m) (in-Double)</param>
      /// <param name="secPosEfg">The secondary satellite's ECI position vector (km) (in-Double[3])</param>
      /// <param name="secVelEfg">The secondary satellite's ECI velocity vector (km/s) (in-Double[3])</param>
      /// <param name="secSatCov">The secondary satellite's 3x3 covariance matrix (in-Double[3, 3])</param>
      /// <param name="secSatRad">The secondary satellite's effective radius (m) (in-Double)</param>
      /// <param name="xf_CovType">Input covariance type (see XF_COVTYPE_?) (in-Integer)</param>
      /// <param name="xa_cb_poc">The resulting probability of collision (see XA_CB_POC_?) (out-Double[4])</param>
      /// <returns>0 if the probability of collision is computed successfully, non-0 if there is an error</returns>
      [DllImport(ComboDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ComboPOC(double[] priPosEfg, double[] priVelEfg, double[,] priSatCov, double priSatRad, double[] secPosEfg, double[] secVelEfg, double[,] secSatCov, double secSatRad, int xf_CovType, double[] xa_cb_poc);


      /// <summary>
      /// Computes probability of collision using data from a CSM/OCM file
      /// </summary>
      /// <param name="csmFile">ocm or csm file (in-Character[512])</param>
      /// <param name="sumR">hard-body radius (R1 + R2) (m), if not zero, overwrite values in csm's (in-Double)</param>
      /// <param name="xa_cb_poc">The resulting probability of collision (see XA_CB_POC_?) (out-Double[4])</param>
      /// <returns>0 if the POC was computed successfully, non-0 if there is an error</returns>
      [DllImport(ComboDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ComboCSMPOC(string csmFile, double sumR, double[] xa_cb_poc);
      
      // Combo control parameters in input array xa_cb_parms that is used in ComboCompPriSec_MT()
      public static int  
         XA_CB_PARMS_TIMEFLG     =  1,       // input format for begin and end times (0: minutes since epoch, 1: days since 1950 UTC)
         XA_CB_PARMS_STARTTIME   =  2,       // begin time
         XA_CB_PARMS_STOPTIME    =  3,       // end time
         XA_CB_PARMS_MAXSEP      =  4,       // maximum separation for relative minima (km) - for reporting times when relative minima is less than this specified value
         XA_CB_PARMS_PROXLIM     =  5,       // close proximity limit (km) - for reporting entry/exit times when pri/sec separation distance equals this specified value   
         XA_CB_PARMS_SIZE        = 16;
      
      // Constants represent combo failed cases
      public static int  
         FAILED_PRITOOFAR = 101,       // Primary satellite's epoch too far from time span (> 60 days)
         FAILED_SECTOOFAR = 102,       // Secondary satellite's epoch too far from time span (> 60 days)
         FAILED_SAMESAT   = 103,       // Primary/secondary satellites are identical   
         FAILED_ALTITUDE  = 104,       // Secondary satellite failed perigee/apogee test 
         WARN_BRIEFSPAN   = 105,       // Secondary satellite is considered a brief span
         WARN_SUSRELGEO   = 106;       // Secondary satellite is in sustained relative geometry
         
      // Indexes of COMBO 7P-card fields
      public static int  
         XF_7P_TIMEFLAG    = 1,       // Input time format : 0: Minutes since epoch, 1: UTC
         XF_7P_BEGTIME     = 2,       // Begin time
         XF_7P_ENDTIME     = 3,       // End time
         XF_7P_RELMINFLG   = 4,       // Control flag for computing relative minima
         XF_7P_ABSMINFLG   = 5,       // Control flag for computing absolute minimum
         XF_7P_EETIMES     = 6,       // Control flag for computing periods of close proximity
         XF_7P_RELEPHFLG   = 7,       // Control flag for computing relative ephemeris
         XF_7P_POCSIGMA    = 8,       // Control flag for computing probability of collision
         XF_7P_RELMINLIM   = 9,       // Maximum separation for relative minima
         XF_7P_ABSMINLIM   = 10,      // Close proximity limit
         XF_7P_RELEPHINT   = 11,      // Relative ephemeris sampling interval 
         XF_7P_PRTOPT      = 12,      // Print options
         XF_7P_PRADIUS     = 13,      // Primary satellite effective radius (m)
         XF_7P_SRADIUS     = 14;      // Secondary satellite effective radius (m)
         
      // Different input of covariance matrix to compute POC   
      public static int  
         XF_COVTYPE_ECI   = 1,        // ECI pos, vel, and ECI covariance
         XF_COVTYPE_UVW   = 2;        // EFG pos, vel, and UVW covariance
         
      // Indexes of Combo's POC data
      public static int  
         XA_CB_POC_NORMSQR  = 0,     // Normalized distance (n-sigma) to circumscribing square
         XA_CB_POC_SQR      = 1,     // POC of square
         XA_CB_POC_NORMCIR  = 2,     // Normalized distance (n-sigma) to circle
         XA_CB_POC_CIR      = 3,     // POC of circle
         
         XA_CB_POC_SIZE     = 4;
         
      
   }
// ========================= End of auto generated code ==========================
}
