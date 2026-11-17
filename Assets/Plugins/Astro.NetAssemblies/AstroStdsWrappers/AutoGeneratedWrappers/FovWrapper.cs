// This wrapper file was generated automatically by the GenDllWrappers program.
using System.Runtime.InteropServices;



namespace AFSPC.AstroStds.Wrappers
{
   public static partial class FovWrapper
   {
      // Provide the path to the dll
      #if WINDOWS
      const string FovDll = "fov";
      #else
      const string FovDll = "libfov.so";
      #endif
      // static constructor
      static FovWrapper()
      {
         // Set AS library default path/LD_LIBRARY_PATH environment
         Utilities.Utility.Init();
      }



      /// <summary>
      /// Notes: This function has been deprecated since v9.0. 
      /// Initializes Fov dll for use in the program
      /// </summary>
      /// <param name="apAddr">The handle that was returned from DllMainInit() (in-Long)</param>
      /// <returns>0 if Fov.dll is initialized successfully, non-0 if there is an error</returns>
      [DllImport(FovDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int FovInit(long apAddr);


      /// <summary>
      /// Returns information about the current version of Fov.dll. The information is placed in the string parameter you pass in
      /// </summary>
      /// <param name="infoStr">A string to hold the information about Fov.dll. (out-Character[128])</param>
      [DllImport(FovDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void FovGetInfo(byte[] infoStr);


      /// <summary>
      /// Loads Fov-related parameters (1P/2P/3P cards, and Fov parameter free format) from an input text file
      /// </summary>
      /// <param name="fovInputFile">The name of the file containing Fov-related parameters (in-Character[512])</param>
      /// <returns>0 if the input file is read successfully, non-0 if there is an error</returns>
      [DllImport(FovDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int FovLoadFile(string fovInputFile);


      /// <summary>
      /// Loads Fov-related parameters (1P/2P/3P cards, and Fov parameter free format) from an input text file
      /// </summary>
      /// <param name="fovInputFile">The name of the file containing Fov-related parameters (in-Character[512])</param>
      /// <returns>0 if the input file is read successfully, non-0 if there is an error</returns>
      [DllImport(FovDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int FovLoadFileAll(string fovInputFile);


      /// <summary>
      /// Loads a single Fov-typed card
      /// </summary>
      /// <param name="card">Fov-type input card (in-Character[512])</param>
      /// <returns>0 if the input card is read successfully, non-0 if there is an error</returns>
      [DllImport(FovDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int FovLoadCard(string card);


      /// <summary>
      /// Saves any currently loaded Fov-related settings to a file
      /// </summary>
      /// <param name="fovFile">The name of the file in which to save the settings (in-Character[512])</param>
      /// <param name="saveMode">Specifies whether to create a new file or append to an existing one (0 = create, 1= append) (in-Integer)</param>
      /// <param name="saveForm">Specifies the mode in which to save the file (0 = text format, 1 = not yet implemented, reserved for future) (in-Integer)</param>
      /// <returns>0 if the data is successfully saved to the file, non-0 if there is an error</returns>
      [DllImport(FovDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int FovSaveFile(string fovFile, int saveMode, int saveForm);


      /// <summary>
      /// This function retrieves various FOV input data being entered from input flat files (various FOV cards)
      /// </summary>
      /// <param name="xa_fovCtrl">FOV control parameters (see XA_FOVCTRL_?) (out-Double[8])</param>
      /// <param name="numOfWindows">Number of specified start/stop windows (out-Integer)</param>
      /// <param name="startStopTimes">Array of start/stop times in days since 1950 UTC (even idxs=start times, odd idxs=stop times) (out-Double[256])</param>
      /// <param name="numOfSources">Number of sources (out-Integer)</param>
      /// <param name="xa_fovSrc_Arr">Array of source records (see XA_FOVSRC_?) (out-Double[128, 8])</param>
      /// <param name="numOfTargets">Number of targets (out-Integer)</param>
      /// <param name="xa_fovTgt_Arr">Array of target records (see XA_FOVTGT_?) (out-Double[128, 8])</param>
      /// <param name="numVicSats">Number of specified potential victims (out-Integer)</param>
      /// <param name="vicSatNums">Array of satellite numbers of the specified potential victims (out-Integer[128])</param>
      [DllImport(FovDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void FovGetDataFrInputFiles(double[] xa_fovCtrl, out int numOfWindows, double[] startStopTimes, out int numOfSources, double[,] xa_fovSrc_Arr, out int numOfTargets, double[,] xa_fovTgt_Arr, out int numVicSats, int[] vicSatNums);


      /// <summary>
      /// This function screens the specified start/end time window and returns passes when the target satellite can be seen (passed all limit checks) by the source 
      /// Note: Since v9.7, this function allows source to be a space-based sensor as well
      /// </summary>
      /// <param name="startTimeDs50UTC">window start time in days since 1950 UTC (in-Double)</param>
      /// <param name="stopTimeDs50UTC">window stop time in days since 1950 UTC (in-Double)</param>
      /// <param name="srcSenKey">the source sensor key (in-Long)</param>
      /// <param name="tgtSatKey">the targeted satellite's unique key (in-Long)</param>
      /// <param name="tgtPassesArr">array of entry/exit times (in Ds50TAI) of each pass; the size of the array should match the specified value in XA_FOVRUN_MAXPASSES (out-Double[*, 2])</param>
      /// <param name="numOfPasses">number of passes that target satellite can be seen (passed all limit checks) by the source sensor (out-Integer)</param>
      /// <returns>0 if successfully, non-0 if there is an error</returns>
      [DllImport(FovDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int FovFindTargetPasses(double startTimeDs50UTC, double stopTimeDs50UTC, long srcSenKey, long tgtSatKey, double[,] tgtPassesArr, out int numOfPasses);


      /// <summary>
      /// This function screens a potential victim satellite for penetrating the illumination cone between a source and a target (target is an elset).
      /// 
      /// The xa_emeDat array size is [numObs, 3].  The penetration time indexes are:
      /// 
      /// table
      /// 
      /// Name
      /// Index
      /// Index Interpretation
      /// 
      ///  0 entry time (ds50TAI)
      ///  1 minimum time (ds50TAI)
      ///  2 exit time (ds50TAI)
      /// 
      /// 
      /// See FovGetDataFrInputFiles for a description of the XA_FOVSRC parameter values.
      /// </summary>
      /// <param name="xa_fovRun">fov run parameters (see XA_FOVRUN_?) (in-Double[8])</param>
      /// <param name="xa_fovSrc">source data (see XA_FOVSRC_?) (in-Double[8])</param>
      /// <param name="tgtSatKey">the targeted satellite's unique key (in-Long)</param>
      /// <param name="vicSatKey">the victim satellite's unique key (in-Long)</param>
      /// <param name="xa_emeDat">array of entry/minimum/exit (in Ds50TAI) times of each pass (out-Double[*, 3])</param>
      /// <param name="numOfPasses">number of passes found (out-Integer)</param>
      /// <returns>0 if successfully, non-0 if there is an error</returns>
      [DllImport(FovDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int FovTargetElset(double[] xa_fovRun, double[] xa_fovSrc, long tgtSatKey, long vicSatKey, double[,] xa_emeDat, out int numOfPasses);


      /// <summary>
      /// This function returns a look angle from the source to the potential victim satellite at the specified time (target is an elset)
      /// 
      /// </summary>
      /// <param name="currDs50TAI">Time, in ds50TAI, for which to compute the look angle (in-Double)</param>
      /// <param name="xa_fovSrc">source data (see XA_FOVSRC_?) (in-Double[8])</param>
      /// <param name="vicSatKey">the victim satellite's unique key (in-Long)</param>
      /// <param name="xa_look">look angle data (see XA_LOOK_?) (out-Double[8])</param>
      /// <returns>0 if successfully, non-0 if there is an error</returns>
      [DllImport(FovDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int FovTargetElsetLook(double currDs50TAI, double[] xa_fovSrc, long vicSatKey, double[] xa_look);


      /// <summary>
      /// This function returns look and view data from the source to the potential victim satellite at the specified time (target is an elset)
      /// 
      /// </summary>
      /// <param name="currDs50TAI">Time, in ds50TAI, for which to compute the look angle (in-Double)</param>
      /// <param name="xa_fovSrc">source data (see XA_FOVSRC_?) (in-Double[8])</param>
      /// <param name="vicSatKey">the victim satellite's unique key (in-Long)</param>
      /// <param name="xa_lv">The array that stores resulting look and view data (see XA_LV_?) (out-Double[128])</param>
      /// <returns>0 if successfully, non-0 if there is an error</returns>
      [DllImport(FovDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int FovTargetElsetLookView(double currDs50TAI, double[] xa_fovSrc, long vicSatKey, double[] xa_lv);


      /// <summary>
      /// This function screens a potential victim satellite for penetrating the illumination cone between a source and a target (target is a vector: AZ/EL or RA/DEC or ELSET #).
      /// See FovTargetElset for a description of the xa_emeDat array.
      /// If actual number of passes exceeds the set limit in XA_FOVRUN_MAXPASSES, an error is returned. However, the data in xa_emeDat is still valid 
      /// </summary>
      /// <param name="xa_fovRun">fov run parameters (see XA_FOVRUN_?) (in-Double[8])</param>
      /// <param name="xa_fovSrc">source data (see XA_FOVSRC_?) (in-Double[8])</param>
      /// <param name="xa_fovTgt">target data (see XA_FOVTGT_?) (in-Double[8])</param>
      /// <param name="vicSatKey">the victim satellite's unique key (in-Long)</param>
      /// <param name="xa_emeDat">array of entry/minimum/exit (in Ds50TAI) times of each pass (out-Double[*, 3])</param>
      /// <param name="numOfPasses">number of passes found (out-Integer)</param>
      /// <returns>0 if successfully, non-0 if there is an error</returns>
      [DllImport(FovDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int FovTargetVec(double[] xa_fovRun, double[] xa_fovSrc, double[] xa_fovTgt, long vicSatKey, double[,] xa_emeDat, out int numOfPasses);


      /// <summary>
      /// This function returns a look angle from the source to the potential victim satellite at the specified time (target is a vector: AZ/EL or RA/DEC or ELSET #).
      /// See FovGetDataFrInputFiles for a description of the XA_FOVSRC and XA_FOVTGT parameter values.
      /// See FovTargetElsetLook for a description of the XA_LOOK parameter values.
      /// </summary>
      /// <param name="currDs50TAI">Time, in ds50TAI, for which to compute the look angle (in-Double)</param>
      /// <param name="xa_fovSrc">source data (see XA_FOVSRC_?) (in-Double[8])</param>
      /// <param name="xa_fovTgt">target data (see XA_FOVTGT_?) (in-Double[8])</param>
      /// <param name="vicSatKey">the victim satellite's unique key (in-Long)</param>
      /// <param name="xa_look">look angle data (see XA_LOOK_?) (out-Double[8])</param>
      /// <returns>0 if successfully, non-0 if there is an error</returns>
      [DllImport(FovDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int FovTargetVecLook(double currDs50TAI, double[] xa_fovSrc, double[] xa_fovTgt, long vicSatKey, double[] xa_look);


      /// <summary>
      /// This function returns look and view data from the source to the potential victim satellite at the specified time (target is a vector: AZ/EL or RA/DEC or ELSET #).
      /// See FovGetDataFrInputFiles for a description of the XA_FOVSRC and XA_FOVTGT parameter values.
      /// Note: The source's limit checks won't be executed therefore all limit flags are returned as PASSes
      /// </summary>
      /// <param name="currDs50TAI">Time, in ds50TAI, for which to compute the look angle (in-Double)</param>
      /// <param name="xa_fovSrc">source data (see XA_FOVSRC_?) (in-Double[8])</param>
      /// <param name="xa_fovTgt">target data (see XA_FOVTGT_?) (in-Double[8])</param>
      /// <param name="vicSatKey">the victim satellite's unique key (in-Long)</param>
      /// <param name="xa_lv">The array that stores resulting look and view data (see XA_LV_?) (out-Double[128])</param>
      /// <returns>0 if successfully, non-0 if there is an error</returns>
      [DllImport(FovDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int FovTargetVecLookView(double currDs50TAI, double[] xa_fovSrc, double[] xa_fovTgt, long vicSatKey, double[] xa_lv);


      /// <summary>
      /// Resets all FOV control parameters back to their default values
      /// </summary>
      [DllImport(FovDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void FovReset();
         
      // Fov run parameters
      public static int     
         XA_FOVRUN_MAXPASSES= 0,     // Maximum number of passes (entry/minimum/exit times) that FOV returns in one start/stop time
         XA_FOVRUN_START    = 1,     // FOV start time in days since 1950, UTC
         XA_FOVRUN_STOP     = 2,     // FOV stop time in days since 1950, UTC
         XA_FOVRUN_HALFCONE = 3,     // Cone half angle (deg) (0=auto) (0-45)
         XA_FOVRUN_INTERVAL = 4,     // Search interval (min) 
         
         XA_FOVRUN_SIZE     = 8;      
      
      // FOV source types   
      public static int  
         FOV_SRCTYPE_SEN = 1,    // Source is sensor
         FOV_SRCTYPE_LLH = 2,    // Source is lat lon height
         FOV_SRCTYPE_EFG = 3,    // Source is EFG 
         FOV_SRCTYPE_XYZ = 4;    // Source is XYZ (ECI Teme of Date)
         
      // FOV source types   
      public static int  
         FOV_TGTTYPE_ELSET = 1,    // Target is elset
         FOV_TGTTYPE_AZEL  = 2,    // Target is constant Az/El
         FOV_TGTTYPE_RADEC = 3;    // Target is RA/Dec (STAR)
         
      // FOV source specification
      public static int  
         XA_FOVSRC_TYPE    = 0,   // 1=SEN   | 2=LLH       | 3=EFG      | 4=XYZ
         XA_FOVSRC_ID      = 1,   // Sensor# | Source ID   | Source ID  | Source ID                            
         XA_FOVSRC_ELEM1   = 2,   //         | N lat (deg) | EFG: E (m) | X (m)
         XA_FOVSRC_ELEM2   = 3,   //         | E lon (deg) | EFG: F (m) | Y (m)
         XA_FOVSRC_ELEM3   = 4,   //         | height (m)  | EFG: G (m) | Z (m)
         XA_FOVSRC_ELEM4   = 5,   //         |             |            | time of position in Ds50UTC
         XA_FOVSRC_HALFCONE= 6,   //         | Cone half angle (deg) if source is LLH/EFG/XYZ to be used in FovTargetVecLook()/FovTargetVecLookView()
         
         XA_FOVSRC_SIZE  = 8;
         
      
      // FOV target specification
      public static int  
         XA_FOVTGT_TYPE  = 0,   // 1=ELSET | 2=AZEL    | 3=RADEC
         XA_FOVTGT_ID    = 1,   // Elset#  | Target ID | Target ID
         XA_FOVTGT_ELEM1 = 2,   //         | az (deg)  | RA (deg)
         XA_FOVTGT_ELEM2 = 3,   //         | el (deg)  | Dec (deg)
         XA_FOVTGT_ELEM3 = 4,   //         |           | equinox indicator 
         
         XA_FOVTGT_SIZE  = 8;
         
      // entry/minimum/exit time data   
      public static int  
         XA_EMEDAT_ENTRY = 0,   // entry time (ds50TAI)
         XA_EMEDAT_MIN   = 1,   // minimum time (ds50TAI)
         XA_EMEDAT_EXIT  = 2,   // exit time (ds50TAI)
         
         XA_EMEDAT_SIZE  = 3;
      
      // FOV parameters   
      public static int  
         XA_FOVCTRL_HALFCONE = 0,     // Cone half angle (deg) (0=auto) (0-45)
         XA_FOVCTRL_INTERVAL = 1,     // Search interval (min) 
         XA_FOVCTRL_PRTOPT   = 2,     // Print option
         
         XA_FOVCTRL_SIZE     = 8;     
         
         
      public static int FOVMAXNUM = 128;  // maximum number of windows/potential, victims/sources/targets allowed to be entered via an input file
         
   }
// ========================= End of auto generated code ==========================
}
