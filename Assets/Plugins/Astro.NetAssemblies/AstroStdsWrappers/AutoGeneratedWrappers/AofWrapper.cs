// This wrapper file was generated automatically by the GenDllWrappers program.
using System.Runtime.InteropServices;



namespace AFSPC.AstroStds.Wrappers
{
   public static partial class AofWrapper
   {
      // Provide the path to the dll
#if WINDOWS
      const string AofDll = "aof";
#else
      const string AofDll = "libaof.so";
#endif
      // static constructor
      static AofWrapper()
      {
         // Set AS library default path/LD_LIBRARY_PATH environment
         Utilities.Utility.Init();
      }



      /// <summary>
      /// Notes: This function has been deprecated since v9.0. 
      /// Initializes Aof dll for use in the program
      /// </summary>
      /// <param name="apAddr">The handle that was returned from DllMainInit() (in-Long)</param>
      /// <returns>0 if Aof.dll is initialized successfully, non-0 if there is an error</returns>
      [DllImport(AofDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int AofInit(long apAddr);


      /// <summary>
      /// Returns information about the current version of Aof.dll. The information is placed in the string parameter you pass in
      /// </summary>
      /// <param name="infoStr">A string to hold the information about Aof.dll. (out-Character[128])</param>
      [DllImport(AofDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void AofGetInfo(byte[] infoStr);


      /// <summary>
      /// Loads Aof-related parameters (1P/2P/3P cards, and Aof parameter free format) from an input text file
      /// </summary>
      /// <param name="aofInputFile">The name of the file containing Aof-related parameters (in-Character[512])</param>
      /// <returns>0 if the input file is read successfully, non-0 if there is an error</returns>
      [DllImport(AofDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int AofLoadFile(string aofInputFile);


      /// <summary>
      /// Loads Aof-related parameters from an input text file
      /// </summary>
      /// <param name="aofInputFile">The name of the file containing Aof-related parameters (in-Character[512])</param>
      /// <returns>0 if the input file is read successfully, non-0 if there is an error</returns>
      [DllImport(AofDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int AofLoadFileAll(string aofInputFile);


      /// <summary>
      /// Loads a single Aof-typed card
      /// </summary>
      /// <param name="card">Aof-type input card (in-Character[512])</param>
      /// <returns>0 if the input card is read successfully, non-0 if there is an error</returns>
      [DllImport(AofDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int AofLoadCard(string card);


      /// <summary>
      /// This function retrieves various AOF input data being entered from input flat files (and/or various AOF input cards)
      /// </summary>
      /// <param name="xa_aofCtrl">AOF control parameters (see XA_AOFCTRL_?) (out-Double[16])</param>
      /// <param name="numOfInpSats">Number of satellite numbers entered in AOF P-card and/or 8P-card (out-Integer)</param>
      /// <param name="aofInpSats">Array of satellite numbers entered via AOF P-card and/or 8P-card (out-Integer[100])</param>
      /// <param name="numOfMissions">Number of satellite/mission records entered via Satellite/Mission (Satellite/Sensor) Data card (out-Integer)</param>
      /// <param name="xa_aofSen">Array of mission records entered via Satellite/Mission (Satellite/Sensor) Data card (see XA_AOFSEN_?) (out-Double[100, 16])</param>
      /// <param name="numOfAreas">Number of defended areas entered via Defended Area Data card (out-Integer)</param>
      /// <param name="xa_aofArea">Array of defended areas entered via Defended Area Data card (see XA_AOFAREA_?) (out-Double[100, 16])</param>
      [DllImport(AofDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void AofGetDataFrInputFiles(double[] xa_aofCtrl, out int numOfInpSats, int[] aofInpSats, out int numOfMissions, double[,] xa_aofSen, out int numOfAreas, double[,] xa_aofArea);


      /// <summary>
      /// This function resets all Aof control parameters previously loaded from input text files
      /// </summary>
      [DllImport(AofDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void AofReset();


      /// <summary>
      /// Computes entry/exit times of basic overflight satellite/sensor (cone) versus basic defended areas (box, circle, point)
      /// 
      /// Note:  for xa_aofArea type=4 (polygon), use the AofComplex method.
      /// </summary>
      /// <param name="xa_aofRun">aof run parameters (see XA_AOFRUN_?) (in-Double[8])</param>
      /// <param name="satKey">the overflight satellite's unique key (in-Long)</param>
      /// <param name="xa_aofSen">satellite/mission data (see XA_AOFSEN_?) (in-Double[16])</param>
      /// <param name="xa_aofArea">defended area data (see XA_AOFAREA_?) (in-Double[16])</param>
      /// <param name="numOfPasses">number of passes found (out-Integer)</param>
      /// <param name="xa_entExitTimes">array of entry/exit times (out-Double[*])</param>
      /// <returns>0 if successfully, non-0 if there is an error</returns>
      [DllImport(AofDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int AofBasic(double[] xa_aofRun, long satKey, double[] xa_aofSen, double[] xa_aofArea, out int numOfPasses, double[] xa_entExitTimes);


      /// <summary>
      /// Computes entry/exit times of overflight satellite/sensor having complex configurations (cone, donut, butterfly, leftButterly, rightButterfly)
      /// versus defended areas defined by multiple lat lon height points (polygon)
      /// The input array llhArr is a zero-based array with latitude, longitude and altitude of each point in subsequent entries, e.g.:
      /// llhArr[0] = point 1 latitude, 
      /// llhArr[1] = point 1 longitude, 
      /// llhArr[2] = point 1 altitude, 
      /// llhArr[3] = point 2 latitude, etc.
      /// </summary>
      /// <param name="xa_aofRun">aof run parameters (see XA_AOFRUN_?) (in-Double[8])</param>
      /// <param name="satKey">the overflight satellite's unique key (in-Long)</param>
      /// <param name="xa_aofSen">satellite/mission data (see XA_AOFSEN_?) (in-Double[16])</param>
      /// <param name="numOfLlhPts">number of actual input lat-lon-height points (maximum 120 points) - lat+N (deg)/lon+E (deg)/height (m) (in-Integer)</param>
      /// <param name="llhArr">defended area defined by array of lat-lon-height points (maximum 120 points) (in-Double[360])</param>
      /// <param name="numOfPasses">number of passes found (out-Integer)</param>
      /// <param name="xa_entExitTimes">array of entry/exit times (out-Double[*])</param>
      /// <returns>0 if successfully, non-0 if there is an error</returns>
      [DllImport(AofDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int AofComplex(double[] xa_aofRun, long satKey, double[] xa_aofSen, int numOfLlhPts, double[] llhArr, out int numOfPasses, double[] xa_entExitTimes);


      /// <summary>
      /// This function returns a look angle from the llh point to the overfly satellite at the specified time
      /// </summary>
      /// <param name="ds50TAI">Time, in ds50TAI, for which to compute the look angle (in-Double)</param>
      /// <param name="llh">lat +N -S (deg) /lon (+E) (deg) / height (m) (in-Double[3])</param>
      /// <param name="satKey">the overflight satellite's unique key (in-Long)</param>
      /// <param name="xa_look">look angle data (see XA_LOOK_?) (out-Double[8])</param>
      /// <returns>0 if successfully, non-0 if there is an error</returns>
      [DllImport(AofDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int AofGetLook(double ds50TAI, double[] llh, long satKey, double[] xa_look);


      /// <summary>
      /// This function returns a view angle from the overfly satellite to a llh point at the specified time
      /// </summary>
      /// <param name="ds50TAI">Time, in ds50TAI, for which to compute the view angle (in-Double)</param>
      /// <param name="llh">lat +N -S (deg) /lon (+E) (deg) / height (m) (in-Double[3])</param>
      /// <param name="satKey">the overflight satellite's unique key (in-Long)</param>
      /// <param name="xa_aofView">view angle data (see XA_AOFVIEW_?) (out-Double[8])</param>
      /// <returns>0 if successfully, non-0 if there is an error</returns>
      [DllImport(AofDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int AofGetView(double ds50TAI, double[] llh, long satKey, double[] xa_aofView);


      /// <summary>
      /// Determines darkness level of the "defended" area at the specified time
      /// For a description of the input parameter array xa_aofArea, see AofBasic.
      /// </summary>
      /// <param name="ds50TAI">Time, in ds50TAI, for which to compute the darkness status of the defended area (in-Double)</param>
      /// <param name="xa_aofArea">defended area data (see XA_AOFAREA_?) (in-Double[16])</param>
      /// <returns>darkness level (see DARKLEVEL_?)</returns>
      [DllImport(AofDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int AofGetDarknessLevel(double ds50TAI, double[] xa_aofArea);

      // AOF parameters
      public static int
         XA_AOFCTRL_TIMEFLG = 0,   // input start/stop time type: 1=minutes since epoch (MSE), 0=date time (DTG)
         XA_AOFCTRL_START = 1,   // start time of interest (either MSE or DTG)
         XA_AOFCTRL_STOP = 2,   // stop time of interest (either MSE or DTG)
         XA_AOFCTRL_INTERVAL = 3,   // search interval (min)
         XA_AOFCTRL_PRTOPT = 4,   // print output control flag, 0=print penetrations only, 1=print penetrations and data description
         XA_AOFCTRL_SRCHMET = 5,   // search method: 0=use brute force method, 1=use analytical method
         XA_AOFCTRL_SRTTYPE = 6,   // output sort type: S=sort each area by sat#, then time, T=sort each area by time, then sat#

         XA_AOFCTRL_SIZE = 16;


      // Defended area types   
      public static int
         AREATYPE_I_BOX = 1,     // area box type
         AREATYPE_I_CIRCLE = 2,     // area circle type
         AREATYPE_I_POINT = 3,     // area point type
         AREATYPE_I_POLYGON = 4;     // area polygon type


      // AOF satellite/mission (satellite/sensor) data
      public static int
         XA_AOFSEN_SATNUM = 0,   // satellite number of sensor-bearing satellite
         XA_AOFSEN_TYPE = 1,   // reserved for future use
         XA_AOFSEN_MINEL = 2,   // off nadir minimum look angle (deg) (=0 for Cone)
         XA_AOFSEN_MAXEL = 3,   // off nadir maximum look angle (deg)
         XA_AOFSEN_MINAZ1 = 4,   // minimum azimuth of first azimuth range (deg)
         XA_AOFSEN_MAXAZ1 = 5,   // maximum azimuth of first azimuth range (deg)
         XA_AOFSEN_MINAZ2 = 6,   // minimum azimuth of second azimuth range (deg)
         XA_AOFSEN_MAXAZ2 = 7,   // maximum azimuth of second azimuth range (deg)
         XA_AOFSEN_ELEM7 = 8,   // reserved for future use
         XA_AOFSEN_ELEM8 = 9,   // reserved for future use
         XA_AOFSEN_ELEM9 = 10,   // reserved for future use
         XA_AOFSEN_ELEM10 = 11,   // reserved for future use

         XA_AOFSEN_SIZE = 16;


      // AOF satellite/sensor types   
      public static int
         AOFSENTYPE_CIRCLE = 0;  // circle (specify only off-nadir maximum look angle)


      // AOF run parameters
      public static int
         XA_AOFRUN_MAXPASSES = 0,     // Maximum number of passes that AOF returns in one start/stop time
         XA_AOFRUN_START = 1,     // AOF start time in days since 1950, UTC
         XA_AOFRUN_STOP = 2,     // AOF stop time in days since 1950, UTC
         XA_AOFRUN_INTERVAL = 3,     // Search interval (min) 

         XA_AOFRUN_SIZE = 8;


      // AOF defended area types   
      public static int
         AOF_AREATYPE_BOX = 1,    // Defended area is a box
         AOF_AREATYPE_CIRCLE = 2,    // Defended area is a circle
         AOF_AREATYPE_POINT = 3,    // Defended area is a point
         AOF_AREATYPE_POLYGON = 4;    // Defended area is a polygon



      // AOF defended area data   
      public static int
         XA_AOFAREA_NUM = 0,      // Area number
         XA_AOFAREA_TYPE = 1,      // | 1 = BOX                       | 2 = CIRCLE              | 3 = POINT
         XA_AOFAREA_ELEM1 = 2,      // | N lat (deg) upper left corner | N lat (deg) center point| N lat (deg) center point
         XA_AOFAREA_ELEM2 = 3,      // | E lon (deg) upper left corner | E lon (deg) center point| E lon (deg) center point
         XA_AOFAREA_ELEM3 = 4,      // | N lat (deg) lower right corner| circle radius (km)      | height (km, above reference geoid)
         XA_AOFAREA_ELEM4 = 5,      // | E lon (deg) lower right corner|                         |            

         XA_AOFAREA_SIZE = 16;


      // Penetration-level darkness status   
      public static int
         DARKLEVEL_ALLLIT = 0,   // lit throughout penetration
         DARKLEVEL_ALLDARK = 1,   // dark throughout penetration
         DARKLEVEL_PARTLIT = 2;   // partly-lit during penetration

      // View angle from overfly satellite to a llh point
      public static int
         XA_AOFVIEW_AZIM = 0,   // Azimuth (deg)
         XA_AOFVIEW_ELEV = 1,   // Elevation (deg)
         XA_AOFVIEW_HASLOS = 2,   // Has line of sight to the point (1=Yes, 0=No-earth obstructs the view)

         XA_AOFVIEW_SIZE = 8;

      public static int MAX_LLHPOINTS = 120; // maximum of number of lat-lon-height points that can be used to describe a defended area   




   }
   // ========================= End of auto generated code ==========================
}
