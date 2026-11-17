// This wrapper file was generated automatically by the GenDllWrappers program.
using System.Runtime.InteropServices;



namespace AFSPC.AstroStds.Wrappers
{
   public static partial class ObsOpsWrapper
   {
      // Provide the path to the dll
      #if WINDOWS
      const string ObsOpsDll = "obsops";
      #else
      const string ObsOpsDll = "libobsops.so";
      #endif
      // static constructor
      static ObsOpsWrapper()
      {
         // Set AS library default path/LD_LIBRARY_PATH environment
         Utilities.Utility.Init();
      }



      /// <summary>
      /// Notes: This function has been deprecated since v9.0.    
      /// Initializes ObsOps DLL for use in the program
      /// </summary>
      /// <param name="apAddr">The handle that was returned from DllMainInit() (in-Long)</param>
      /// <returns>0 if ObsOps.dll is initialized successfully, non-0 if there is an error</returns>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ObsOpsInit(long apAddr);


      /// <summary>
      /// Returns information about the current version of ObsOps DLL. 
      /// The information is placed in the string parameter passed in.
      /// </summary>
      /// <param name="infoStr">A string to hold the information about ObsOps.dll (out-Character[128])</param>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void ObsOpsGetInfo(byte[] infoStr);


      /// <summary>
      /// Loads Iomod parameters from an input text file
      /// </summary>
      /// <param name="iomodInputFile">The name of the file containing IOMOD-related parameters (in-Character[512])</param>
      /// <returns>0 if the input file is read successfully, non-0 if there is an error</returns>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int IomodLoadFile(string iomodInputFile);


      /// <summary>
      /// Loads Iomod control parameters and all Iomod related data (environment, time, sensors, obs) from an input text file
      /// </summary>
      /// <param name="iomodInputFile">The name of the file containing Iomod control parameters and all Iomod related data (in-Character[512])</param>
      /// <returns>0 if the input file is read successfully, non-0 if there is an error</returns>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int IomodLoadFileAll(string iomodInputFile);


      /// <summary>
      /// Saves any currently loaded IOMOD-related settings to a file
      /// </summary>
      /// <param name="iomodFile">The name of the file in which to save the settings (in-Character[512])</param>
      /// <param name="saveMode">Specifies whether to create a new file or append to an existing one (0 = create, 1= append) (in-Integer)</param>
      /// <param name="saveForm">Specifies the mode in which to save the file (0 = text format, 1 = not yet implemented, reserved for future) (in-Integer)</param>
      /// <returns>0 if the data is successfully saved to the file, non-0 if there is an error</returns>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int IomodSaveFile(string iomodFile, int saveMode, int saveForm);


      /// <summary>
      /// Builds and returns the IOMOD-parameter card (IP-Card) from the current IOMOD settings
      /// </summary>
      /// <param name="iomodIPCard">the resulting IOMOD IP card (out-Character[512])</param>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void IomodGetIpCard(byte[] iomodIPCard);


      /// <summary>
      /// Retrieves all IOMOD parameters with a single function call
      /// </summary>
      /// <param name="printIOHG">control flag for IOHG advisory prints: 0=do not print IOHG information, 1=print IOHG (out-Integer)</param>
      /// <param name="useFirst3Obs">use first 3 obs flag (out-Integer)</param>
      /// <param name="satNum">satelite number (out-Integer)</param>
      /// <param name="sortOrder">obs sorting order (out-Integer)</param>
      /// <param name="satName">international designator (out-Character[8])</param>
      /// <param name="runDC">run DC flag: 1=yes, 0=no (out-Integer)</param>
      /// <param name="runRotas">run ROTAS flag: 1=yes, 0=no (out-Integer)</param>
      /// <param name="extArr">the extra array (future use) (out-Double[128])</param>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void IomodGetIpAll(out int printIOHG, out int useFirst3Obs, out int satNum, out int sortOrder, byte[] satName, out int runDC, out int runRotas, double[] extArr);


      /// <summary>
      /// Sets all IOMOD parameters with a single function call
      /// </summary>
      /// <param name="printIOHG">control flag for IOHG advisory prints: 0=do not print IOHG information, 1=print IOHG (in-Integer)</param>
      /// <param name="useFirst3Obs">use first 3 obs flag (in-Integer)</param>
      /// <param name="satNum">satelite number (in-Integer)</param>
      /// <param name="sortOrder">obs sorting order (in-Integer)</param>
      /// <param name="satName">international designator (in-Character[8])</param>
      /// <param name="runDC">run DC flag: 1=yes, 0=no (in-Integer)</param>
      /// <param name="runRotas">run ROTAS flag: 1=yes, 0=no (in-Integer)</param>
      /// <param name="extArr">the extra array (future use) (in-Double[128])</param>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void IomodSetIpAll(int printIOHG, int useFirst3Obs, int satNum, int sortOrder, string satName, int runDC, int runRotas, double[] extArr);


      /// <summary>
      /// Retrieves the value of a specified IOMOD parameter
      /// 
      /// The table below indicates which index values for the xf_IP parameter:
      /// 
      /// table
      /// 
      /// xf_IP named constants/values
      /// Interpretation
      /// 
      /// XF_IP_PRINTIOHG  = 1control flag for IOHG advisory prints: 0=do not print IOHG information, 1=print
      /// IOHG
      /// XF_IP_USE1ST3OBS = 2use first 3 obs flag
      /// XF_IP_SATNUM     = 3satelite number
      /// XF_IP_SORTORDER  = 4obs sorting order
      /// XF_IP_SATNAME    = 5international designator 
      /// XF_IP_RUNDC      = 6run DC flag: 1=yes, 0=no [deprecated]
      /// XF_IP_RUNROTAS   = 7run ROTAS flag: 1=yes, 0=no [deprecated]
      /// 
      /// </summary>
      /// <param name="xf_IP">Predefined number specifying which IOMOD parameter to retrieve (in-Integer)</param>
      /// <param name="retVal">A string to hold the value of the requested IOMOD parameter (out-Character[512])</param>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void IomodGetIpField(int xf_IP, byte[] retVal);


      /// <summary>
      /// Sets the value of a specified IOMOD parameter
      /// See IomodGetIpField for a description of the xf_IP parameter.
      /// </summary>
      /// <param name="xf_IP">Predefined number specifying which IOMOD parameter to set (see XF_IP_?) (in-Integer)</param>
      /// <param name="valueStr">The new value of the specified field, expressed as a string (in-Character[512])</param>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void IomodSetIpField(int xf_IP, string valueStr);


      /// <summary>
      /// Computes initial orbit vector from many observations using Herrick-Gibbs method
      /// </summary>
      /// <param name="obsKeyArr">the obsKey array contains obsKeys that are available for IOMOD (in-Long[*])</param>
      /// <param name="arrSize">the size of the obsKeyArr (in-Integer)</param>
      /// <param name="timeDs50UTC">the resulting time of the new vector in ds50UTC (out-Double)</param>
      /// <param name="pos">the resulting position vector (km) (out-Double[3])</param>
      /// <param name="vel">the resulting velocity vector (km/s) (out-Double[3])</param>
      /// <param name="arr3ObsKeys">the resulting 3 obsKeys that were auto-selected by IOMOD (out-Long[3])</param>
      /// <returns>0 if the computation is successful, non-0 if there is an error</returns>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ManyObsToPosVel(long[] obsKeyArr, int arrSize, out double timeDs50UTC, double[] pos, double[] vel, long[] arr3ObsKeys);


      /// <summary>
      /// Computes initial orbit vector from three selected observations using Herrick-Gibbs method
      /// </summary>
      /// <param name="arr3ObsKeys">the three input obsKeys (in-Long[3])</param>
      /// <param name="timeDs50UTC">the resulting time of the new vector in ds50UTC (out-Double)</param>
      /// <param name="pos">the resulting position vector (km) (out-Double[3])</param>
      /// <param name="vel">the resulting velocity vector (km/s) (out-Double[3])</param>
      /// <returns>0 if the computation is successful, non-0 if there is an error</returns>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ThreeObsToPosVel(long[] arr3ObsKeys, out double timeDs50UTC, double[] pos, double[] vel);


      /// <summary>
      /// Computes initial orbit vector directly from the input sensor/observation data - thread safe
      /// </summary>
      /// <param name="xa_senLoc">array contains sensor location data (see XA_SENLOC_?) (in-Double[16])</param>
      /// <param name="numObs">number of input obs in xa_obsArr (in-Integer)</param>
      /// <param name="xa_obsArr">array of input obs (see XA_OBS_?) (in-Double[*, 64])</param>
      /// <param name="timeDs50UTC">the resulting time of the new vector in ds50UTC (out-Double)</param>
      /// <param name="pos">the resulting position vector (km) (out-Double[3])</param>
      /// <param name="vel">the resulting velocity vector (km/s) (out-Double[3])</param>
      /// <returns>0 if the computation is successful, non-0 if there is an error</returns>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int Iomod_OS(double[] xa_senLoc, int numObs, double[,] xa_obsArr, out double timeDs50UTC, double[] pos, double[] vel);


      /// <summary>
      /// Computes initial orbit vector from two selected observations using Two-Ob method
      /// Note: Two-Ob method only works with ob types: 2, 3, 4, P, and V
      /// </summary>
      /// <param name="arr2ObsKeys">the two input obsKeys (in-Long[2])</param>
      /// <param name="timeDs50UTC">the resulting time of the new vector in ds50UTC (out-Double)</param>
      /// <param name="pos">the resulting position vector (km) (out-Double[3])</param>
      /// <param name="vel">the resulting velocity vector (km/s) (out-Double[3])</param>
      /// <returns>0 if the computation is successful, non-0 if there is an error</returns>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TwoObsToPosVel(long[] arr2ObsKeys, out double timeDs50UTC, double[] pos, double[] vel);


      /// <summary>
      /// Computes the initial orbit vector from three selected angle-only observations using the Gooding Algorithm.
      /// </summary>
      /// <param name="arr3ObsKeys">the three input obsKeys (in-Long[3])</param>
      /// <param name="range1">the range guess to first observation (km) (in-Double)</param>
      /// <param name="range3">the range guess to third observation (km) (in-Double)</param>
      /// <param name="nhrev">number of half revolutions (in-Integer)</param>
      /// <param name="ind">solution number (in-Integer)</param>
      /// <param name="maxIt">maximum number of iterations (in-Integer)</param>
      /// <param name="pdInc">partial derivative increment (in-Double)</param>
      /// <param name="criVal">convergence criterion (in-Double)</param>
      /// <param name="timeDs50UTC">the resulting time of the new vector in ds50UTC (out-Double)</param>
      /// <param name="pos">the resulting position vector (km) (out-Double[3])</param>
      /// <param name="vel">the resulting velocity vector (km/s) (out-Double[3])</param>
      /// <returns>0 if the computation is successful, non-0 if there is an error</returns>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int AnglesOnlyToPosVel(long[] arr3ObsKeys, double range1, double range3, int nhrev, int ind, int maxIt, double pdInc, double criVal, out double timeDs50UTC, double[] pos, double[] vel);


      /// <summary>
      /// Computes the track length of a track whose first observation started at the input startIdx.
      /// This routine can be used in a loop to find all the tracks in the observation list.
      /// Remember to sort obs in the obsKeyArr in the ascending order of sensor, satno, obsType, time, elev 
      /// by calling ObsGetLoaded(-6, pObsKeys) before feeding the array to this routine
      /// </summary>
      /// <param name="obsKeyArr">the input obsKeyArr (in-Long[*])</param>
      /// <param name="arrSize">the size of the obsKeyArr (number of obs) (in-Integer)</param>
      /// <param name="startIdx">the index of the first observation to start computing the track length (1-based array indexing) (in-Integer)</param>
      /// <param name="trackLength">the track length (trackLength = 1 for single obs) (out-Integer)</param>
      /// <returns>0 if the computation is successful, non-0 if there is an error</returns>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int FindTrack(long[] obsKeyArr, int arrSize, int startIdx, out int trackLength);


      /// <summary>
      /// Computes initial orbit vector from a track of obs. Also returns the three obs in the track that are used by IOMOD
      /// </summary>
      /// <param name="obsKeys">the input obsKeyArr (in-Long[*])</param>
      /// <param name="trackStartIdx">the index of the first observation that starts the track (1-based array indexing) (in-Integer)</param>
      /// <param name="trackLength">the track length (in-Integer)</param>
      /// <param name="timeDs50UTC">the resulting time of the new vector in ds50UTC (out-Double)</param>
      /// <param name="pos">the resulting position vector (km) (out-Double[3])</param>
      /// <param name="vel">the resulting velocity vector (km/s) (out-Double[3])</param>
      /// <param name="trackObsKeys">obsKeys of the tree obs that are used by IOMOD (out-Long[3])</param>
      /// <returns>0 if the computation is successful, non-0 if there is an error</returns>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int ATrackToPosVel(long[] obsKeys, int trackStartIdx, int trackLength, out double timeDs50UTC, double[] pos, double[] vel, long[] trackObsKeys);


      /// <summary>
      /// Returns the default values of the triangulation settings
      /// </summary>
      /// <param name="xa_tri">triangulation parameters (see XA_TRI_?) (out-Double[32])</param>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void TriGetDefParams(double[] xa_tri);


      /// <summary>
      /// Triangulates the input obs and write generated obs with range data to the specified output file
      /// </summary>
      /// <param name="obsKeyArr">the input obsKeyArr contains obsKeys that are available for triangulation (in-Long[*])</param>
      /// <param name="arrSize">the size of the obsKeyArr (number of obs) (in-Integer)</param>
      /// <param name="xa_tri">triangulation parameters (see XA_TRI_?) (in-Double[32])</param>
      /// <param name="toFile">output file that stores the generated obs with the computed range data (in-Character[512])</param>
      /// <returns>0 if successful, non-0 if there is an error</returns>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TriComputeToFile(long[] obsKeyArr, int arrSize, double[] xa_tri, string toFile);


      /// <summary>
      /// Triangulates the input obs and returns the overlap and polyfit information; creates and loads obs with range data into memory and returns their associated obsKeys
      /// </summary>
      /// <param name="obsKeyArr">the input obsKeyArr contains obsKeys that are available for triangulation (in-Long[*])</param>
      /// <param name="arrSize">the size of the obsKeyArr (number of obs) (in-Integer)</param>
      /// <param name="xa_tri">triangulation parameters (see XA_TRI_?) (in-Double[32])</param>
      /// <param name="numOvlps">number of overlap tracks (out-Integer)</param>
      /// <param name="xa_ovlp">overlap data (see XA_OVLP_?) (out-Double[*, 8])</param>
      /// <param name="numFits">number of qualified tracks for fitting (out-Integer)</param>
      /// <param name="xa_fit">polyfit data (see XA_FIT_?) (out-Double[*, 32])</param>
      /// <param name="maxGenObs">maximum number of generated obs (in-Integer)</param>
      /// <param name="nGenObs">number of actual obs generated by triangulation process (nGenObs always lt or eq maxGenObs) (out-Integer)</param>
      /// <param name="genObsKeys">generate obs with range, then load them into memory, and return the obsKeys of all generated obs (out-Long[*])</param>
      /// <returns>0 if successful, non-0 if there is an error</returns>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TriCompute1(long[] obsKeyArr, int arrSize, double[] xa_tri, out int numOvlps, double[,] xa_ovlp, out int numFits, double[,] xa_fit, int maxGenObs, out int nGenObs, long[] genObsKeys);


      /// <summary>
      /// Triangulates the input obs and returns the overlap and polyfit information 
      /// </summary>
      /// <param name="obsKeyArr">the input obsKeyArr contains obsKeys that are available for triangulation (in-Long[*])</param>
      /// <param name="arrSize">the size of the obsKeyArr (number of obs) (in-Integer)</param>
      /// <param name="xa_tri">triangulation parameters (see XA_TRI_?) (in-Double[32])</param>
      /// <param name="numOvlps">number of overlap tracks (out-Integer)</param>
      /// <param name="xa_ovlp">overlap data (see XA_OVLP_?) (out-Double[*, 8])</param>
      /// <param name="numFits">number of qualified tracks for fitting (out-Integer)</param>
      /// <param name="xa_fit">polyfit data (see XA_FIT_?) (out-Double[*, 32])</param>
      /// <returns>0 if successful, non-0 if there is an error</returns>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TriCompute2(long[] obsKeyArr, int arrSize, double[] xa_tri, out int numOvlps, double[,] xa_ovlp, out int numFits, double[,] xa_fit);


      /// <summary>
      /// Triangulates the input obs; creates and loads obs with range data (by triangulation) into memory and returns their associated obsKeys
      /// </summary>
      /// <param name="obsKeyArr">the input obsKeyArr contains obsKeys that are available for triangulation (in-Long[*])</param>
      /// <param name="arrSize">the size of the obsKeyArr (number of obs) (in-Integer)</param>
      /// <param name="xa_tri">triangulation parameters (see XA_TRI_?) (in-Double[32])</param>
      /// <param name="maxGenObs">maximum number of generated obs (in-Integer)</param>
      /// <param name="nGenObs">number of actual obs generated by triangulation process (nGenObs always lt or eq maxGenObs) (out-Integer)</param>
      /// <param name="genObsKeys">generate obs with range, then load them into memory, and return the obsKeys of all generated obs (out-Long[*])</param>
      /// <returns>0 if successful, non-0 if there is an error</returns>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int TriCompute3(long[] obsKeyArr, int arrSize, double[] xa_tri, int maxGenObs, out int nGenObs, long[] genObsKeys);


      /// <summary>
      /// Returns the default values of the waterfall altitude (km)
      /// </summary>
      /// <returns>The default altitude (km) being used in the Waterfall algorithm</returns>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern double GetWaterfallAlt();


      /// <summary>
      /// Converts angle-only observation to lat/lon/height positions
      /// </summary>
      /// <param name="obsKey">The unique key of the angle-only observation that needs to be converted by the waterfall calculation (in-Long)</param>
      /// <param name="assumedAltitude">The input assumed altitude (km) to calculate LLH (set to 0 to indicate default altitude should be used) (in-Double)</param>
      /// <param name="rho">The ouput calculated range to sphere of assumed altitude (out-Double[2])</param>
      /// <param name="llhs">The calculated LLH at sphere of assumed altitude (out-Double[2, 3])</param>
      /// <param name="numSols">Number of Waterfall Solutions (out-Integer)</param>
      /// <returns>Returns 0 if the conversion is successful, non-0 if there is an error</returns>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int AnglesOnlyToLLH(long obsKey, double assumedAltitude, double[] rho, double[,] llhs, out int numSols);


      /// <summary>
      /// Calculates length of output arrays for Lomb
      /// Used to allocated px, py, prob before call to Lomb
      /// </summary>
      /// <param name="ofac">Oversample factor, typical value is "4" or "5" (in-Double)</param>
      /// <param name="hifac">Ratio of highest frequency to Nyquist frequency (in-Double)</param>
      /// <param name="nSample">Number of samples for x and y (in-Integer)</param>
      /// <returns>Used to allocated px, py, prob before call to Lomb</returns>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int LombLengthOut(double ofac, double hifac, int nSample);


      /// <summary>
      /// Creates Lomb-Scargle periodogram.  Reference: Numerical Recipes   
      /// Used to find frequencies in unevenly sampled data, such as Visual Magnitude vs. Time
      /// </summary>
      /// <param name="x">array times to input (units don't matter, can be days, seconds, etc...), length of nSample (in-Double[*])</param>
      /// <param name="y">array of data (such as Visual Magnitude) associated with times to input, length of nSample (in-Double[*])</param>
      /// <param name="nSample">Number of samples for x and y (in-Integer)</param>
      /// <param name="ofac">Oversample factor, typical value is "4" or "5" (in-Double)</param>
      /// <param name="hifac">Ratio of highest frequency to Nyquist frequency (in-Double)</param>
      /// <param name="px">array of frequencies, should be of length (0.5d0*ofac*hifac*nSample) (out-Double[*])</param>
      /// <param name="py">array of powers associated with frequencies, should be of length (0.5d0*ofac*hifac*nSample) (out-Double[*])</param>
      /// <param name="prob">array of false alarm probabilities corresponding to py, should be of length (0.5d0*ofac*hifac*nSample) (out-Double[*])</param>
      /// <param name="pxmax">most powerful frequency (out-Double)</param>
      /// <param name="pymax">power of most powerful frequency (out-Double)</param>
      /// <param name="probMax">false alarm probability of most powerful frequency (out-Double)</param>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void Lomb(double[] x, double[] y, int nSample, double ofac, double hifac, double[] px, double[] py, double[] prob, out double pxmax, out double pymax, out double probMax);


      /// <summary>
      /// Find False Alarm Probablility of given Power (lower is less likely to be a false alarm)
      /// </summary>
      /// <param name="py">Power (in-Double)</param>
      /// <param name="nout">Number of output frequencies (0.5*ofac*hifac*n) (in-Integer)</param>
      /// <param name="ofac">oversample factor (in-Double)</param>
      /// <returns>False Alarm Probability</returns>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern double LombFalseProb(double py, int nout, double ofac);


      /// <summary>
      /// Find Power of given False Alarm Probablility
      /// </summary>
      /// <param name="prob">False Alarm Probability (in-Double)</param>
      /// <param name="nout">Number of output frequencies (0.5*ofac*hifac*n) (in-Integer)</param>
      /// <param name="ofac">oversample factor (in-Double)</param>
      /// <returns>Power</returns>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern double LombFalseProbInv(double prob, int nout, double ofac);


      /// <summary>
      /// Calculate length of output for LombWaveFit
      /// </summary>
      /// <param name="nFreq">Number of frequencies of interest (in-Integer)</param>
      /// <returns>Returned number of coefficients to use in LombWaveFit and LombCalcValueFromTime, use to allocate coeffs</returns>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int LombLengthCoef(int nFreq);


      /// <summary>
      /// Fits user defined frequencies to data to find the magnitudes
      /// Solves the equation
      /// y= C + sum( A(i)*sin(2*pi*freq(i)*t) + B(i)*sin(2*pi*freq(i)*t)  )
      /// For coefficients A and B corresponding to each frequency
      /// </summary>
      /// <param name="x">Times, length of nSamples (in-Double[*])</param>
      /// <param name="y">Data, length of nSamples (in-Double[*])</param>
      /// <param name="nSamples">Number of input samples, length of x and y (in-Integer)</param>
      /// <param name="freq">set of frequencies to fit, should not be same as output of Lomb, lenth of nfreq (in-Double[*])</param>
      /// <param name="nfreq">number of frequencies, length of freq (in-Integer)</param>
      /// <param name="coeffs">resulting coefficients(A(1) B(1) ... A(nfreq) B(nfreq) C), will have length(2*nfreq+1) (out-Double[*])</param>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void LombWaveFit(double[] x, double[] y, int nSamples, double[] freq, int nfreq, double[] coeffs);


      /// <summary>
      /// Calculate value of y given time, x, 
      /// and frequencies and magnitudes that were gotten from wavefit
      /// solves the equation
      /// y= C + sum( A(i)*sin(2*pi*freq(i)*t) + B(i)*sin(2*pi*freq(i)*t)  )
      /// for y
      /// </summary>
      /// <param name="x">Time (in-Double)</param>
      /// <param name="mags">Magnitudes from wavefit, length of (2*nfreq+1) (in-Double[*])</param>
      /// <param name="freq">frequencies of interest, length of nfreq (in-Double[*])</param>
      /// <param name="nfreq">Number of frequencies of interest (in-Integer)</param>
      /// <returns>resulting y value</returns>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern double LombCalcValueFromTime(double x, double[] mags, double[] freq, int nfreq);


      /// <summary>
      /// Given two observations, calculate the angle between them
      /// Supports obsType 1,2,3,4,5,8,9,18,19
      /// </summary>
      /// <param name="obsKey1">1st obsKey (in-Long)</param>
      /// <param name="obsKey2">2nd obsKey (in-Long)</param>
      /// <param name="angSep">Angular Separation(Deg) (out-Double)</param>
      /// <param name="errCode">Error Code, 1 - Obs times not equal (will still do calculation), 2 - Calculation errored out, sensor numbers are different, or unsupported obs type (out-Integer)</param>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void AngleBetweenObs(long obsKey1, long obsKey2, out double angSep, out int errCode);


      /// <summary>
      /// Combines the TwoObs/ThreeObs/ManyObs methods into one while returning intermediate data of the Iomod iterations
      /// </summary>
      /// <param name="obsKeyArr">the obsKey array contains obsKeys that are available for IOMOD (in-Long[*])</param>
      /// <param name="arrSize">the size of the obsKeyArr (2, 3, or more) (in-Integer)</param>
      /// <param name="xa_iomod">array containing settings for the IOMOD run (unused for now) (in-Double[64])</param>
      /// <param name="timeDs50UTC">the resulting time of the new vector in ds50UTC (out-Double)</param>
      /// <param name="pos">the resulting position vector (km) (out-Double[3])</param>
      /// <param name="vel">the resulting velocity vector (km/s) (out-Double[3])</param>
      /// <param name="arr3ObsKeys">the resulting 3 obsKeys that were auto-selected by IOMOD (if more than 3 obs provided) (out-Long[3])</param>
      /// <param name="numIters">the number of iterations that Iomod took (out-Integer)</param>
      /// <param name="xa_iomIter">Iomod iteration data (see XA_IOMITER_?) (out-Double[25, 8])</param>
      /// <param name="arr3Pos">the three computed position vectors from the selected obs (out-Double[3, 3])</param>
      /// <returns>0 if the computation is successful, non-0 if there is an error</returns>
      [DllImport(ObsOpsDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int IomodCombine(long[] obsKeyArr, int arrSize, double[] xa_iomod, out double timeDs50UTC, double[] pos, double[] vel, long[] arr3ObsKeys, out int numIters, double[,] xa_iomIter, double[,] arr3Pos);
      
      // Different obs selection options 
      public static int  
         OBSSELMODE_AUTO   = 0,    // auto select 3 obs among the loaded/selected obs
         OBSSELMODE_1ST3OBS= 1;    // use the first 3 obs in list of loaded/selected obs
      
      
      // Indexes for accessing Iomod control parameters
      public static int  
         XF_IP_PRINTIOHG  = 1,     // control flag for IOHG advisory prints: 0=do not print IOHG information, 1=print IOHG
         XF_IP_USE1ST3OBS = 2,     // use first 3 obs flag
         XF_IP_SATNUM     = 3,     // satelite number
         XF_IP_SORTORDER  = 4,     // obs sorting order
         XF_IP_SATNAME    = 5,     // international designator 
         XF_IP_RUNDC      = 6,     // run DC flag: 1=yes, 0=no
         XF_IP_RUNROTAS   = 7;     // run ROTAS flag: 1=yes, 0=no
         
      // Indexes of TRI configuration parameters
      public static int  
         XA_TRI_USEDEFAULT       =  0,    // I - Flag to specify whether or not to use triangulation's default parameters: =[1]: use them and ignore the rest, =0: not to use them
         XA_TRI_OBSTYPEOUT       =  1,    // I - Output obs type (valid values are 0, 2, 3, 4, 6, 8 and 9)
         XA_TRI_APPLYBIAS        =  2,    // I - Flag to specify whether or not to apply sensor bias: =[1]: apply bias, =0: not apply bias
         XA_TRI_LTC              =  3,    // I - apply light time correction (applyLTC) flag: 0=do no apply LTC, [1]=apply LTC 
         XA_TRI_CREATEOBSSIZE    =  4,    // I - number of obs created for each overlap track
         XA_TRI_DEFANGLEORDER    =  5,    // I - default order of the Polyfit for angle RA/DEC
         XA_TRI_DEFRANGEORDER    =  6,    // I - default order of the Polyfit for range
         XA_TRI_DEFEFGORDER      =  7,    // I - default order of the Polyfit for EFG components
         XA_TRI_AUTOFITANGLE     =  8,    // I - whether or not to use auto mode for angle (increase order if needed when doing Polyfit)
         XA_TRI_AUTOFITRANGE     =  9,    // I - whether or not to use auto for range (increase order if needed when doing Polyfit)
         XA_TRI_ANCHORRADEC      = 10,    // I - anchor order of RA/DEC  
         XA_TRI_SEPSATNO         = 11,    // I - check to see satellite number is different
         XA_TRI_RARMSLIM         = 12,    // D - limit RMS for RA (arcsec)
         XA_TRI_DECRMSLIM        = 13,    // D - limit RMS for Dec (arcsec)
         XA_TRI_RANGERMSLIM      = 14,    // D - limit RMS for Range (km)
         XA_TRI_TRKSEPARATION    = 15,    // D - track separation criteria (minutes)
         XA_TRI_MINSTEREOANGLE   = 16,    // D - minimum stereoscopic angle (deg)
                                          
         XA_TRI_SIZE             = 32;   
      
      // Indexes of overlap track data 
      public static int  
         XA_OVLP_SATNUM       =  0,       // I - Satellite number
         XA_OVLP_SENNUM1      =  1,       // I - Sensor number of track 1
         XA_OVLP_SENNUM2      =  2,       // I - Sensor number of track 2
         XA_OVLP_STARTTIME    =  3,       // D - Overlap track's start time (in DS50UTC)
         XA_OVLP_ENDTIME      =  4,       // D - Overlap track's end time (in DS50UTC)
         
         XA_OVLP_SIZE         =  8;       //
         
      // Indexes of polyfitting track data
      public static int  
         XA_FIT_SATNUM        =  0,       // I - Satellite number
         XA_FIT_SENNUM1       =  1,       // I - Sensor number of track 1
         XA_FIT_SENNUM2       =  2,       // I - Sensor number of track 2
         
         XA_FIT_RAORDER1      =  3,       // I - Right ascension polyfit order of track 1
         XA_FIT_RAORDER2      =  4,       // I - Right ascension polyfit order of track 2
         XA_FIT_RARMS1        =  5,       // D - Right ascension RMS of track 1
         XA_FIT_RARMS2        =  6,       // D - Right ascension RMS of track 2
         
         XA_FIT_DECORDER1     =  7,       // I - Declination polyfit order of track 1
         XA_FIT_DECORDER2     =  8,       // I - Declination polyfit order of track 2
         XA_FIT_DECMS1        =  9,       // D - Declination RMS of track 1
         XA_FIT_DECRMS2       = 10,       // D - Declination RMS of track 2
         
         XA_FIT_RANGEORDER1   = 11,       // I - Declination polyfit order of track 1
         XA_FIT_RANGEORDER2   = 12,       // I - Declination polyfit order of track 2
         XA_FIT_RANGERMS1     = 13,       // D - Declination RMS of track 1
         XA_FIT_RANGERMS2     = 14,       // D - Declination RMS of track 2
         
         XA_FIT_THETA1        = 15,       // D - First stereoscopic angle (deg)
         XA_FIT_THETA2        = 16,       // D - Second stereoscopic angle (deg)
         XA_FIT_THETA3        = 17,       // D - Third stereoscopic angle (deg)
         
         XA_FIT_SIZE          =  32;       //
      
      // Iomod iteration data
      public static int     
         XA_IOMITER_RNG1 = 0,   // Computed estimated range for ob# 1
         XA_IOMITER_RNG2 = 1,   // Computed estimated range for ob# 2
         XA_IOMITER_RNG3 = 2,   // Computed estimated range for ob# 3
         XA_IOMITER_CONV = 3,   // Convergence value
         
         XA_IOMITER_SIZE = 8;
         
      // Iomod maximum number of iterations
      public static int IOMOD_MAXITERS = 25;
         
   }
// ========================= End of auto generated code ==========================
}
