// This wrapper file was generated automatically by the GenDllWrappers program.
using System.Runtime.InteropServices;



namespace AFSPC.AstroStds.Wrappers
{
   public static partial class AstroFuncWrapper
   {
      // Provide the path to the dll
      #if WINDOWS
      const string AstroFuncDll = "astrofunc";
      #else
      const string AstroFuncDll = "libastrofunc.so";
      #endif
      // static constructor
      static AstroFuncWrapper()
      {
         // Set AS library default path/LD_LIBRARY_PATH environment
         Utilities.Utility.Init();
      }



      /// <summary>
      /// Notes: This function has been deprecated since v9.0. 
      /// Initializes AstroFunc DLL for use in the program.
      /// If this function returns an error, it is recommended that you stop the program immediately.
      /// 
      /// An error will occur if you forget to load and initialize all the prerequisite DLLs, as listed in the DLL Prerequisites section of the accompanying documentation, before using this DLL.
      /// </summary>
      /// <param name="apAddr">The handle that was returned from DllMainInit(). See the documentation for DllMain.dll for details. (in-Long)</param>
      /// <returns>0 if AstroFunc.dll is initialized successfully, non-0 if there is an error.</returns>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int AstroFuncInit(long apAddr);


      /// <summary>
      /// Retrieves information about the current version of AstroFunc.dll. The information is placed in the string parameter you pass in.
      /// The returned string provides information about the version number, build date, and platform.
      /// </summary>
      /// <param name="infoStr">A string to hold the information about AstroFunc.dll. (out-Character[128])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void AstroFuncGetInfo(byte[] infoStr);


      /// <summary>
      /// Converts a set of Keplerian elements to a set of equinoctial elements. 
      /// </summary>
      /// <param name="xa_kep">The set of Keplerian elements to be converted (see XA_KEP_?) (in-Double[6])</param>
      /// <param name="xa_eqnx">The resulting set of equinoctial elements (see XA_EQNX_?) (out-Double[6])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void KepToEqnx(double[] xa_kep, double[] xa_eqnx);


      /// <summary>
      /// Converts a set of osculating Keplerian elements to osculating position and velocity vectors.
      /// </summary>
      /// <param name="xa_kep">The set of Keplerian elements to be converted (see XA_KEP_?) (in-Double[6])</param>
      /// <param name="pos">The resulting position vector. (out-Double[3])</param>
      /// <param name="vel">The resulting velocity vector. (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void KepToPosVel(double[] xa_kep, double[] pos, double[] vel);


      /// <summary>
      /// Converts a set of Keplerian elements to Ubar, Vbar, and Wbar vectors.
      /// </summary>
      /// <param name="xa_kep">The set of Keplerian elements to be converted (see XA_KEP_?) (in-Double[6])</param>
      /// <param name="uBar">The resulting ubar vector. (out-Double[3])</param>
      /// <param name="vBar">The resulting vbar vector. (out-Double[3])</param>
      /// <param name="wBar">The resulting wbar vector. (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void KepToUVW(double[] xa_kep, double[] uBar, double[] vBar, double[] wBar);


      /// <summary>
      /// Converts a set of classical elements to a set of equinoctial elements. 
      /// </summary>
      /// <param name="xa_cls">The set of classical elements to be converted (see XA_CLS_?) (in-Double[6])</param>
      /// <param name="xa_eqnx">The resulting set of equinoctial elements (see XA_EQNX_?) (out-Double[6])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void ClassToEqnx(double[] xa_cls, double[] xa_eqnx);


      /// <summary>
      /// Converts a set of equinoctial elements to a set of classical elements.
      /// </summary>
      /// <param name="xa_eqnx">The set of equinoctial elements to be converted (see XA_EQNX_?) (in-Double[6])</param>
      /// <param name="xa_cls">The resulting set of classical elements (see XA_CLS_?) (out-Double[6])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void EqnxToClass(double[] xa_eqnx, double[] xa_cls);


      /// <summary>
      /// Converts a set of equinoctial elements to a set of Keplerian elements. 
      /// </summary>
      /// <param name="xa_eqnx">The set of equinoctial elements to be converted (see XA_EQNX_?) (in-Double[6])</param>
      /// <param name="xa_kep">The resulting set of Keplerian elements (see XA_KEP_?) (out-Double[6])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void EqnxToKep(double[] xa_eqnx, double[] xa_kep);


      /// <summary>
      /// Converts a set of equinoctial elements to position and velocity vectors.
      /// </summary>
      /// <param name="xa_eqnx">The set of equinoctial elements to be converted (see XA_EQNX_?) (in-Double[6])</param>
      /// <param name="pos">The resulting position vector. (out-Double[3])</param>
      /// <param name="vel">The resulting velocity vector. (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void EqnxToPosVel(double[] xa_eqnx, double[] pos, double[] vel);


      /// <summary>
      /// Converts position and velocity vectors to a set of equinoctial elements.
      /// </summary>
      /// <param name="pos">The position vector to be converted. (in-Double[3])</param>
      /// <param name="vel">The velocity vector to be converted. (in-Double[3])</param>
      /// <param name="xa_eqnx">The resulting set of equinoctial elements (see XA_EQNX_?) (out-Double[6])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void PosVelToEqnx(double[] pos, double[] vel, double[] xa_eqnx);


      /// <summary>
      /// Converts position and velocity vectors to a set of equinoctial elements with the given mu value. 
      /// This function is used when working with the SP propagator to get a more accurate set of equinoctial elements.
      /// </summary>
      /// <param name="pos">The position vector to be converted. (in-Double[3])</param>
      /// <param name="vel">The velocity vector to be converted. (in-Double[3])</param>
      /// <param name="mu">The value of mu. (in-Double)</param>
      /// <param name="xa_eqnx">The resulting set of equinoctial elements (see XA_EQNX_?) (out-Double[6])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void PosVelMuToEqnx(double[] pos, double[] vel, double mu, double[] xa_eqnx);


      /// <summary>
      /// Converts osculating position and velocity vectors to a set of osculating Keplerian elements.
      /// </summary>
      /// <param name="pos">The position vector to be converted. (in-Double[3])</param>
      /// <param name="vel">The velocity vector to be converted. (in-Double[3])</param>
      /// <param name="xa_kep">The resulting set of Keplerian elements (see XA_KEP_?) (out-Double[6])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void PosVelToKep(double[] pos, double[] vel, double[] xa_kep);


      /// <summary>
      /// Converts osculating position and velocity vectors to a set of osculating Keplerian elements with the given value of mu.
      /// This function is used when working with the SP propagator to get a more accurate set of Keplerian elements.
      /// </summary>
      /// <param name="pos">The position vector to be converted. (in-Double[3])</param>
      /// <param name="vel">The velocity vector to be converted. (in-Double[3])</param>
      /// <param name="mu">The value of mu. (in-Double)</param>
      /// <param name="xa_kep">The resulting set of Keplerian elements (see XA_KEP_?) (out-Double[6])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void PosVelMuToKep(double[] pos, double[] vel, double mu, double[] xa_kep);


      /// <summary>
      /// Converts position and velocity vectors to U, V, W vectors. See the remarks section for details.
      /// The resulting vectors have the following meanings.
      /// U vector: along radial direction
      /// V vector: W x U
      /// W vector: pos x vel
      /// </summary>
      /// <param name="pos">The position vector to be converted. (in-Double[3])</param>
      /// <param name="vel">The velocity vector to be converted. (in-Double[3])</param>
      /// <param name="uvec">The resulting U vector. (out-Double[3])</param>
      /// <param name="vVec">The resulting V vector. (out-Double[3])</param>
      /// <param name="wVec">The resulting W vector. (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void PosVelToUUVW(double[] pos, double[] vel, double[] uvec, double[] vVec, double[] wVec);


      /// <summary>
      /// Converts position and velocity vectors to U, V, W vectors. See the remarks section for details.
      /// The resulting vectors have the following meanings.
      /// U vector: V x W
      /// V vector: along velocity direction
      /// W vector: pos x vel
      /// </summary>
      /// <param name="pos">The position vector. (in-Double[3])</param>
      /// <param name="vel">The velocity vector. (in-Double[3])</param>
      /// <param name="uvec">The resulting U vector. (out-Double[3])</param>
      /// <param name="vVec">The resulting V vector. (out-Double[3])</param>
      /// <param name="wVec">The resulting W vector. (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void PosVelToPTW(double[] pos, double[] vel, double[] uvec, double[] vVec, double[] wVec);


      /// <summary>
      /// Solves Kepler's equation (M = E - e sin(E)) for the eccentric anomaly, E, by iteration.
      /// </summary>
      /// <param name="xa_kep">The set of Keplerian elements for which to solve the equation (see XA_KEP_?) (in-Double[6])</param>
      /// <returns>The eccentric anomaly.</returns>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern double SolveKepEqtn(double[] xa_kep);


      /// <summary>
      /// Computes true anomaly from a set of Keplerian elements.
      /// </summary>
      /// <param name="xa_kep">The set of Keplerian elements for which to compute true anomaly (see XA_KEP_?) (in-Double[6])</param>
      /// <returns>The true anomaly in degrees.</returns>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern double CompTrueAnomaly(double[] xa_kep);


      /// <summary>
      /// Converts mean motion N to semi-major axis A.
      /// </summary>
      /// <param name="n">Mean motion N (revs/day). (in-Double)</param>
      /// <returns>The semi-major axis A (km).</returns>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern double NToA(double n);


      /// <summary>
      /// Converts semi-major axis A to mean motion N.
      /// </summary>
      /// <param name="a">Semi-major axis A (km). (in-Double)</param>
      /// <returns>The mean motion N (revs/day).</returns>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern double AToN(double a);


      /// <summary>
      /// Converts Kozai mean motion to Brouwer mean motion.
      /// SGP TLE's use Kozai mean motion while SGP4/SGP4-XP TLE's use Brouwer mean motion.
      /// </summary>
      /// <param name="eccen">eccentricity (in-Double)</param>
      /// <param name="incli">inclination (degrees) (in-Double)</param>
      /// <param name="nKozai">Kozai mean motion (revs/day). (in-Double)</param>
      /// <returns>Brouwer mean motion (revs/day).</returns>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern double KozaiToBrouwer(double eccen, double incli, double nKozai);


      /// <summary>
      /// Converts Brouwer mean motion to Kozai mean motion.
      /// SGP TLE's use Kozai mean motion while SGP4/SGP4-XP TLE's use Brouwer mean motion.
      /// </summary>
      /// <param name="eccen">eccentricity (in-Double)</param>
      /// <param name="incli">inclination (degrees) (in-Double)</param>
      /// <param name="nBrouwer">Brouwer mean motion (revs/day). (in-Double)</param>
      /// <returns>Kozai mean motion (revs/day).</returns>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern double BrouwerToKozai(double eccen, double incli, double nBrouwer);


      /// <summary>
      /// Converts a set of osculating Keplerian elements to a set of mean Keplerian elements using method 9 algorithm.
      /// </summary>
      /// <param name="xa_OscKep">The set of osculating Keplerian elements to be converted (see XA_KEP_?) (in-Double[6])</param>
      /// <param name="xa_MeanKep">The resulting set of mean Keplerian elements (see XA_KEP_?) (out-Double[6])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void KepOscToMean(double[] xa_OscKep, double[] xa_MeanKep);


      /// <summary>
      /// Converts an ECI position vector XYZ to geodetic latitude, longitude, and height.
      /// Note: Since this conversion doesn't use timing constants (polar wander), it produces less accurate results
      /// </summary>
      /// <param name="thetaG">ThetaG - Greenwich mean sidereal time (rad). (in-Double)</param>
      /// <param name="metricPos">The ECI (TEME of Date) position vector (km) to be converted. (in-Double[3])</param>
      /// <param name="metricLLH">The resulting geodetic north latitude (degree), east longitude(degree), and height (km). (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void XYZToLLH(double thetaG, double[] metricPos, double[] metricLLH);


      /// <summary>
      /// Converts an ECI position vector XYZ to geodetic latitude, longitude, and height at the specified time.
      /// </summary>
      /// <param name="ds50UTC">Input time in days since 1950 UTC (in-Double)</param>
      /// <param name="metricPos">The ECI (TEME of Date) position vector (km) to be converted. (in-Double[3])</param>
      /// <param name="metricLLH">The resulting geodetic north latitude (degree), east longitude(degree), and height (km). (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void XYZToLLHTime(double ds50UTC, double[] metricPos, double[] metricLLH);


      /// <summary>
      /// Converts geodetic latitude, longitude, and height to an ECI position vector XYZ.
      /// Note: Since this conversion doesn't use timing constants (polar wander), it produces less accurate results
      /// </summary>
      /// <param name="thetaG">Theta - Greenwich mean sidereal time (rad). (in-Double)</param>
      /// <param name="metricLLH">An array containing geodetic north latitude (degree), east longitude (degree), and height (km) to be converted. (in-Double[3])</param>
      /// <param name="metricXYZ">The resulting ECI (TEME of Date) position vector (km). (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void LLHToXYZ(double thetaG, double[] metricLLH, double[] metricXYZ);


      /// <summary>
      /// Converts geodetic latitude, longitude, and height to an ECI position vector XYZ at the specified time.
      /// </summary>
      /// <param name="ds50UTC">Input time in days since 1950 UTC (in-Double)</param>
      /// <param name="metricLLH">An array containing geodetic north latitude (degree), east longitude (degree), and height (km) to be converted. (in-Double[3])</param>
      /// <param name="metricXYZ">The resulting ECI (TEME of Date) position vector (km). (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void LLHToXYZTime(double ds50UTC, double[] metricLLH, double[] metricXYZ);


      /// <summary>
      /// Converts EFG position and velocity vectors to ECI position and velocity vectors.
      /// </summary>
      /// <param name="thetaG">Theta - Greenwich mean sidereal time (rad). (in-Double)</param>
      /// <param name="posEFG">The EFG position vector (km) to be converted. (in-Double[3])</param>
      /// <param name="velEFG">The EFG velocity vector (km/s) to be converted. (in-Double[3])</param>
      /// <param name="posECI">The resulting ECI (TEME of Date) position vector (km). (out-Double[3])</param>
      /// <param name="velECI">The resulting ECI (TEME of Date) velocity vector (km/s). (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void EFGToECI(double thetaG, double[] posEFG, double[] velEFG, double[] posECI, double[] velECI);


      /// <summary>
      /// Converts EFG position and velocity vectors to ECI position and velocity vectors at the specified time.
      /// </summary>
      /// <param name="ds50UTC">Input time in days since 1950 UTC (in-Double)</param>
      /// <param name="posEFG">The EFG position vector (km) to be converted. (in-Double[3])</param>
      /// <param name="velEFG">The EFG velocity vector (km/s) to be converted. (in-Double[3])</param>
      /// <param name="posECI">The resulting ECI (TEME of Date) position vector (km). (out-Double[3])</param>
      /// <param name="velECI">The resulting ECI (TEME of Date) velocity vector (km/s). (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void EFGToECITime(double ds50UTC, double[] posEFG, double[] velEFG, double[] posECI, double[] velECI);


      /// <summary>
      /// Converts ECI position and velocity vectors to EFG position and velocity vectors.
      /// </summary>
      /// <param name="thetaG">Theta - Greenwich mean sidereal time (rad). (in-Double)</param>
      /// <param name="posECI">The ECI (TEME of Date) position vector (km) to be converted. (in-Double[3])</param>
      /// <param name="velECI">The ECI (TEME of Date) velocity vector (km/s) to be converted. (in-Double[3])</param>
      /// <param name="posEFG">The resulting EFG position vector (km). (out-Double[3])</param>
      /// <param name="velEFG">The resulting EFG velocity vector (km/s). (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void ECIToEFG(double thetaG, double[] posECI, double[] velECI, double[] posEFG, double[] velEFG);


      /// <summary>
      /// Converts ECI position and velocity vectors to EFG position and velocity vectors at the specified time.
      /// </summary>
      /// <param name="ds50UTC">Input time in days since 1950 UTC (in-Double)</param>
      /// <param name="posECI">The ECI (TEME of Date) position vector (km) to be converted. (in-Double[3])</param>
      /// <param name="velECI">The ECI (TEME of Date) velocity vector (km/s) to be converted. (in-Double[3])</param>
      /// <param name="posEFG">The resulting EFG position vector (km). (out-Double[3])</param>
      /// <param name="velEFG">The resulting EFG velocity vector (km/s). (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void ECIToEFGTime(double ds50UTC, double[] posECI, double[] velECI, double[] posEFG, double[] velEFG);


      /// <summary>
      /// Converts ECR position and velocity vectors to EFG position and velocity vectors.
      /// </summary>
      /// <param name="polarX">Polar motion X (arc-sec). (in-Double)</param>
      /// <param name="polarY">Polar motion Y (arc-sec). (in-Double)</param>
      /// <param name="posECR">The ECR position vector (km) to be converted. (in-Double[3])</param>
      /// <param name="velECR">The ECR velocity vector (km/s) to be converted. (in-Double[3])</param>
      /// <param name="posEFG">The resulting EFG position vector (km). (out-Double[3])</param>
      /// <param name="velEFG">The resulting EFG velocity vector (km/s). (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void ECRToEFG(double polarX, double polarY, double[] posECR, double[] velECR, double[] posEFG, double[] velEFG);


      /// <summary>
      /// Converts ECR position and velocity vectors to EFG position and velocity vectors at the specified time.
      /// Note: Without timing constant data, the accuracy of the conversion results will be reduced
      /// </summary>
      /// <param name="ds50UTC">Input time in days since 1950 UTC. (in-Double)</param>
      /// <param name="posECR">The ECR position vector (km) to be converted. (in-Double[3])</param>
      /// <param name="velECR">The ECR velocity vector (km/s) to be converted. (in-Double[3])</param>
      /// <param name="posEFG">The resulting EFG position vector (km) (set to zeros if timing constants not loaded). (out-Double[3])</param>
      /// <param name="velEFG">The resulting EFG velocity vector (km/s) (set to zeros if timing constants not loaded). (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void ECRToEFGTime(double ds50UTC, double[] posECR, double[] velECR, double[] posEFG, double[] velEFG);


      /// <summary>
      /// Converts EFG position and velocity vectors to ECR position and velocity vectors.
      /// </summary>
      /// <param name="polarX">Polar motion X (arc-sec). (in-Double)</param>
      /// <param name="polarY">Polar motion Y (arc-sec). (in-Double)</param>
      /// <param name="posEFG">The EFG position vector (km) to be converted. (in-Double[3])</param>
      /// <param name="velEFG">The EFG velocity vector (km/s) to be converted. (in-Double[3])</param>
      /// <param name="posECR">The resulting ECR position vector (km). (out-Double[3])</param>
      /// <param name="velECR">The resulting ECR velocity vector (km/s). (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void EFGToECR(double polarX, double polarY, double[] posEFG, double[] velEFG, double[] posECR, double[] velECR);


      /// <summary>
      /// Converts EFG position and velocity vectors to ECR position and velocity vectors at the specified time.
      /// Note: Without timing constant data, the accuracy of the conversion results will be reduced
      /// </summary>
      /// <param name="ds50UTC">Input time in days since 1950 UTC (in-Double)</param>
      /// <param name="posEFG">The EFG position vector (km) to be converted. (in-Double[3])</param>
      /// <param name="velEFG">The EFG velocity vector (km/s) to be converted. (in-Double[3])</param>
      /// <param name="posECR">The resulting ECR position vector (km) (set to zeros if timing constants not loaded). (out-Double[3])</param>
      /// <param name="velECR">The resulting ECR velocity vector (km/s) (set to zeros if timing constants not loaded). (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void EFGToECRTime(double ds50UTC, double[] posEFG, double[] velEFG, double[] posECR, double[] velECR);


      /// <summary>
      /// Converts an EFG position vector to geodetic latitude, longitude, and height.
      /// Note: Since this conversion doesn't include time and timing constants (polar wander), it produces less accurate results
      /// </summary>
      /// <param name="posEFG">The EFG position vector (km) to be converted. (in-Double[3])</param>
      /// <param name="metricLLH">The resulting geodetic north latitude (degree), east longitude (degree), and height (km). (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void EFGPosToLLH(double[] posEFG, double[] metricLLH);


      /// <summary>
      /// Converts geodetic latitude, longitude, and height to an EFG position vector.
      /// Note: Since this conversion doesn't include time and timing constants (polar wander), it produces less accurate results
      /// </summary>
      /// <param name="metricLLH">An Array containing the geodetic north latitude (degree), east longitude (degree), and height (km) to be converted. (in-Double[3])</param>
      /// <param name="posEFG">The resulting EFG position vector (km). (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void LLHToEFGPos(double[] metricLLH, double[] posEFG);


      /// <summary>
      /// Converts an EFG position vector to geodetic latitude, longitude, and height.
      /// </summary>
      /// <param name="ds50UTC">Input time in days since 1950 UTC (in-Double)</param>
      /// <param name="posEFG">The EFG position vector (km) to be converted. (in-Double[3])</param>
      /// <param name="metricLLH">The resulting geodetic north latitude (degree), east longitude (degree), and height (km). (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void EFGPosToLLHTime(double ds50UTC, double[] posEFG, double[] metricLLH);


      /// <summary>
      /// Converts geodetic latitude, longitude, and height to an EFG position vector.
      /// </summary>
      /// <param name="ds50UTC">Input time in days since 1950 UTC (in-Double)</param>
      /// <param name="metricLLH">An Array containing the geodetic north latitude (degree), east longitude (degree), and height (km) to be converted. (in-Double[3])</param>
      /// <param name="posEFG">The resulting EFG position vector (km). (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void LLHToEFGPosTime(double ds50UTC, double[] metricLLH, double[] posEFG);


      /// <summary>
      /// Rotates position and velocity vectors from J2000 to coordinates of the specified date, expressed in ds50TAI.
      /// </summary>
      /// <param name="spectr">Specifies whether to run in SPECTR compatibility mode. A value of 1 means Yes. (in-Integer)</param>
      /// <param name="nutationTerms">Nutation terms (4-106, 4:least accurate, 106:most acurate). (in-Integer)</param>
      /// <param name="ds50TAI">The date to rotate to coordinates of, expressed in days since 1950, TAI. (in-Double)</param>
      /// <param name="posJ2K">The position vector from J2000. (in-Double[3])</param>
      /// <param name="velJ2K">The velocity vector from J2000. (in-Double[3])</param>
      /// <param name="posDate">The resulting position vector in coordinates of date, ds50TAI. (out-Double[3])</param>
      /// <param name="velDate">The resulting velocity vector in coordinates of date, ds50TAI. (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void RotJ2KToDate(int spectr, int nutationTerms, double ds50TAI, double[] posJ2K, double[] velJ2K, double[] posDate, double[] velDate);


      /// <summary>
      /// Rotates position and velocity vectors from coordinates of date to J2000.
      /// </summary>
      /// <param name="spectr">Specifies whether to run in SPECTR compatibility mode. A value of 1 means Yes. (in-Integer)</param>
      /// <param name="nutationTerms">Nutation terms (4-106, 4:least accurate, 106:most acurate). (in-Integer)</param>
      /// <param name="ds50TAI">Time in days since 1950, TAI for which the coordinates of position and velocity vectors are currently expressed. (in-Double)</param>
      /// <param name="posDate">The position vector from coordinates of Date. (in-Double[3])</param>
      /// <param name="velDate">The velocity vector from coordinates of Date. (in-Double[3])</param>
      /// <param name="posJ2K">The resulting position vector in coordinates of J2000. (out-Double[3])</param>
      /// <param name="velJ2K">The resulting velocity vector in coordinates of J2000. (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void RotDateToJ2K(int spectr, int nutationTerms, double ds50TAI, double[] posDate, double[] velDate, double[] posJ2K, double[] velJ2K);


      /// <summary>
      /// Computes the Sun and Moon position at the specified time.
      /// </summary>
      /// <param name="ds50ET">The number of days since 1950, ET (Ephemeris Time/Terrestrial Time) for which to compute the sun and moon position. (in-Double)</param>
      /// <param name="uvecSun">The resulting sun position unit vector. (out-Double[3])</param>
      /// <param name="sunVecMag">The resulting magnitude of the sun position vector (km). (out-Double)</param>
      /// <param name="uvecMoon">The resulting moon position unit vector. (out-Double[3])</param>
      /// <param name="moonVecMag">The resulting magnitude of the moon position vector (km). (out-Double)</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void CompSunMoonPos(double ds50ET, double[] uvecSun, out double sunVecMag, double[] uvecMoon, out double moonVecMag);


      /// <summary>
      /// Computes the Sun position at the specified time.
      /// </summary>
      /// <param name="ds50ET">The number of days since 1950, ET (Ephemeris Time/Terrestrial Time) for which to compute the sun position. (in-Double)</param>
      /// <param name="uvecSun">The resulting sun position unit vector. (out-Double[3])</param>
      /// <param name="sunVecMag">The resulting magnitude of the sun position vector (km). (out-Double)</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void CompSunPos(double ds50ET, double[] uvecSun, out double sunVecMag);


      /// <summary>
      /// Computes the Moon position at the specified time.
      /// </summary>
      /// <param name="ds50ET">The number of days since 1950, ET (Ephemeris Time/Terrestrial Time) for which to compute the moon position. (in-Double)</param>
      /// <param name="uvecMoon">The resulting moon position unit vector. (out-Double[3])</param>
      /// <param name="moonVecMag">The resulting magnitude of the moon position vector (km). (out-Double)</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void CompMoonPos(double ds50ET, double[] uvecMoon, out double moonVecMag);


      /// <summary>
      /// This function is intended for future use.  No information is currently available.
      /// </summary>
      /// <param name="xf_Conv">Index of the conversion function (in-Integer)</param>
      /// <param name="frArr">The input array (in-Double[128])</param>
      /// <param name="toArr">The resulting array (out-Double[128])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void AstroConvFrTo(int xf_Conv, double[] frArr, double[] toArr);


      /// <summary>
      /// Converts right ascension and declination to vector triad LAD in topocentric equatorial coordinate system.
      /// </summary>
      /// <param name="ra">Right ascension (deg). (in-Double)</param>
      /// <param name="dec">Declination (deg). (in-Double)</param>
      /// <param name="l">The resulting unit vector from the station to the satellite (referred to the equatorial coordinate system axis). (out-Double[3])</param>
      /// <param name="a_tilde">The resulting unit vector perpendicular to the hour circle passing through the satellite, in the direction of increasing RA. (out-Double[3])</param>
      /// <param name="d_tilde">The resulting unit vector perpendicular to L and is directed toward the north, in the plane of the hour circle. (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void RADecToLAD(double ra, double dec, double[] l, double[] a_tilde, double[] d_tilde);


      /// <summary>
      /// Converts azimuth and elevation to vector triad LAD in topocentric horizontal coordinate system.
      /// </summary>
      /// <param name="az">Input azimuth (deg). (in-Double)</param>
      /// <param name="el">Input elevation angle (deg). (in-Double)</param>
      /// <param name="lh">The resulting unit vector from the station to the satellite (referred to the horizon coordinate system axis). (out-Double[3])</param>
      /// <param name="ah">The resulting unit vector perpendicular to the hour circle passing through the satellite, in the direction of increasing Az. (out-Double[3])</param>
      /// <param name="dh">The resulting unit vector perpendicular to L and is directed toward the zenith, in the plane of the hour circle. (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void AzElToLAD(double az, double el, double[] lh, double[] ah, double[] dh);


      /// <summary>
      /// Converts satellite ECI position/velocity vectors and sensor location to topocentric components.
      /// The xa_topo array has the following structure:
      /// [0]: Resulting right ascension (RA) (deg)
      /// [1]: Declination (deg)
      /// [2]: Azimuth (deg)
      /// [3]: Elevation (deg)
      /// [4]: Range (km)
      /// [5]: RAdot (first derivative of right ascension) (deg/s)
      /// [6]: DecDot (first derivative of declination) (deg/s)
      /// [7]: AzDot (first derivative of azimuth) (deg/s)
      /// [8]: ElDot (first derivative of elevation) (deg/s)
      /// [9]: RangeDot (first derivative of range) (km/s)   
      /// </summary>
      /// <param name="theta">Theta - local sidereal time(rad). (in-Double)</param>
      /// <param name="lat">Station's astronomical latitude (deg). (+N) (-S) (in-Double)</param>
      /// <param name="senPos">Sensor position in ECI (km). (in-Double[3])</param>
      /// <param name="satPos">Satellite position in ECI (km). (in-Double[3])</param>
      /// <param name="satVel">Satellite velocity in ECI (km/s). (in-Double[3])</param>
      /// <param name="xa_topo">An array that stores the resulting topocentric components (see XA_TOPO_?) (out-Double[10])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void ECIToTopoComps(double theta, double lat, double[] senPos, double[] satPos, double[] satVel, double[] xa_topo);


      /// <summary>
      /// Converts right ascension and declination in the topocentric reference frame to Azimuth/Elevation in the local horizon reference frame.
      /// </summary>
      /// <param name="thetaG">Theta - Greenwich mean sidereal time (rad). (in-Double)</param>
      /// <param name="lat">Station's astronomical latitude (deg). (+N) (-S) (in-Double)</param>
      /// <param name="lon">Station's astronomical longitude (deg). (+E) (-W) (in-Double)</param>
      /// <param name="ra">Right ascension (deg) (in-Double)</param>
      /// <param name="dec">Declination (deg) (in-Double)</param>
      /// <param name="az">Azimuth (deg) (out-Double)</param>
      /// <param name="el">Elevation (deg) (out-Double)</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void RaDecToAzEl(double thetaG, double lat, double lon, double ra, double dec, out double az, out double el);


      /// <summary>
      /// Converts right ascension and declination in the topocentric reference frame to Azimuth/Elevation in the local horizon reference frame.
      /// </summary>
      /// <param name="ds50UTC">Input time in days since 1950 UTC (in-Double)</param>
      /// <param name="lat">Station's astronomical latitude (deg). (+N) (-S) (in-Double)</param>
      /// <param name="lon">Station's astronomical longitude (deg). (+E) (-W) (in-Double)</param>
      /// <param name="ra">Right ascension (deg) (in-Double)</param>
      /// <param name="dec">Declination (deg) (in-Double)</param>
      /// <param name="az">Azimuth (deg) (out-Double)</param>
      /// <param name="el">Elevation (deg) (out-Double)</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void RaDecToAzElTime(double ds50UTC, double lat, double lon, double ra, double dec, out double az, out double el);


      /// <summary>
      /// Converts Azimuth/Elevation in the local horizon reference frame to Right ascension/Declination in the topocentric reference frame
      /// </summary>
      /// <param name="thetaG">Theta - Greenwich mean sidereal time (rad). (in-Double)</param>
      /// <param name="lat">Station's astronomical latitude (deg). (+N) (-S) (in-Double)</param>
      /// <param name="lon">Station's astronomical longitude (deg). (+E) (-W) (in-Double)</param>
      /// <param name="az">Azimuth (deg) (in-Double)</param>
      /// <param name="el">Elevation (deg) (in-Double)</param>
      /// <param name="ra">Right ascension (deg) (out-Double)</param>
      /// <param name="dec">Declination (deg) (out-Double)</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void AzElToRaDec(double thetaG, double lat, double lon, double az, double el, out double ra, out double dec);


      /// <summary>
      /// Converts Azimuth/Elevation in the local horizon reference frame to Right ascension/Declination in the topocentric reference frame
      /// </summary>
      /// <param name="ds50UTC">Input time in days since 1950 UTC (in-Double)</param>
      /// <param name="lat">Station's astronomical latitude (deg). (+N) (-S) (in-Double)</param>
      /// <param name="lon">Station's astronomical longitude (deg). (+E) (-W) (in-Double)</param>
      /// <param name="az">Azimuth (deg) (in-Double)</param>
      /// <param name="el">Elevation (deg) (in-Double)</param>
      /// <param name="ra">Right ascension (deg) (out-Double)</param>
      /// <param name="dec">Declination (deg) (out-Double)</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void AzElToRaDecTime(double ds50UTC, double lat, double lon, double az, double el, out double ra, out double dec);


      /// <summary>
      /// Converts full state RAE (range, az, el, and their rates) to full state ECI (position and velocity)
      /// The xa_rae array has the following structure:
      /// [0]: Range (km)
      /// [1]: Azimuth (deg)
      /// [2]: Elevation (deg)
      /// [3]: Range Dot (km/s)
      /// [4]: Azimuth Dot (deg/s)
      /// [5]: Elevation Dot (deg/s)
      /// </summary>
      /// <param name="theta">Theta - local sidereal time(rad). (in-Double)</param>
      /// <param name="astroLat">Astronomical latitude (ded). (in-Double)</param>
      /// <param name="xa_rae">An array contains input data (see XA_RAE_?) (in-Double[6])</param>
      /// <param name="senPos">Sensor position in ECI (km). (in-Double[3])</param>
      /// <param name="satPos">Satellite position in ECI (km). (out-Double[3])</param>
      /// <param name="satVel">Satellite velocity in ECI (km/s). (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void RAEToECI(double theta, double astroLat, double[] xa_rae, double[] senPos, double[] satPos, double[] satVel);


      /// <summary>
      /// Computes initial values for the SGP drag term nDot and the SGP4 drag term BSTAR based upon eccentricity and semi-major axis.
      /// </summary>
      /// <param name="semiMajorAxis">Semi-major axis (km). (in-Double)</param>
      /// <param name="eccen">Eccentricity (unitless). (in-Double)</param>
      /// <param name="nDot">nDot (revs/day^2). (out-Double)</param>
      /// <param name="bstar">Bstar (1/earth radii). (out-Double)</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void GetInitialDrag(double semiMajorAxis, double eccen, out double nDot, out double bstar);


      /// <summary>
      /// Converts covariance matrix PTW to UVW.
      /// PTW = P: TxW, T: along velocity direction, W: pos x vel.
      /// UVW = U: radial direction, V: in plane, perpendicular to U, W: pos x vel.
      /// </summary>
      /// <param name="pos">The input position vector (km). (in-Double[3])</param>
      /// <param name="vel">The input velocity vector (km/s). (in-Double[3])</param>
      /// <param name="ptwCovMtx">The PTW covariance matrix to be converted. (in-Double[6, 6])</param>
      /// <param name="uvwCovMtx">The resulting UVW covariance matrix. (out-Double[6, 6])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void CovMtxPTWToUVW(double[] pos, double[] vel, double[,] ptwCovMtx, double[,] uvwCovMtx);


      /// <summary>
      /// Converts covariance matrix UVW to PTW.
      /// PTW = P: TxW, T: along velocity direction, W: pos x vel.
      /// UVW = U: radial direction, V: in plane, perpendicular to U, W: pos x vel.
      /// </summary>
      /// <param name="pos">The input position vector (km). (in-Double[3])</param>
      /// <param name="vel">The input velocity vector (km/s). (in-Double[3])</param>
      /// <param name="uvwCovMtx">The UVW covariance matrix to be converted. (in-Double[6, 6])</param>
      /// <param name="ptwCovMtx">The resulting PTW covariance matrix. (out-Double[6, 6])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void CovMtxUVWToPTW(double[] pos, double[] vel, double[,] uvwCovMtx, double[,] ptwCovMtx);


      /// <summary>
      /// Computes Earth/Sensor/Earth Limb and Earth/Sensor/Satellite angles.
      /// </summary>
      /// <param name="earthLimb">Earth limb distance (km). (in-Double)</param>
      /// <param name="satECI">Satellite position in ECI (km). (in-Double[3])</param>
      /// <param name="senECI">Sensor position in ECI (km). (in-Double[3])</param>
      /// <param name="earthSenLimb">The resulting earth/sensor/limb angle (deg). (out-Double)</param>
      /// <param name="earthSenSat">The resulting earth/sensor/sat angle (deg). (out-Double)</param>
      /// <param name="satEarthSen">The resulting sat/earth/sensor angle (deg). (out-Double)</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void EarthObstructionAngles(double earthLimb, double[] satECI, double[] senECI, out double earthSenLimb, out double earthSenSat, out double satEarthSen);


      /// <summary>
      /// Determines if a point in space is sunlit at the input time ds50ET
      /// </summary>
      /// <param name="ds50ET">The number of days since 1950, ET (Ephemeris Time/Terrestrial Time) for which to determine if the point is sunlit. (in-Double)</param>
      /// <param name="ptEci">a position in ECI (km). (in-Double[3])</param>
      /// <returns>0=no, the specified point isn't sunlit, 1=yes, the specified point is sunlit</returns>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int IsPointSunlit(double ds50ET, double[] ptEci);


      /// <summary>
      /// Rotates Right Ascension and Declination to specified epoch
      /// </summary>
      /// <param name="nutationTerms">Nutation terms (4-106, 4:least accurate, 106:most acurate). (in-Integer)</param>
      /// <param name="dir">1: TEME of Date To MEME year of equinox, 2: MEME year of equinox to TEME of Date (in-Integer)</param>
      /// <param name="ds50UTCIn">Origin time in days since 1950 UTC (in-Double)</param>
      /// <param name="raIn">Input right ascension (deg) (in-Double)</param>
      /// <param name="declIn">Input declination (deg) (in-Double)</param>
      /// <param name="ds50UTCOut">Destination time in days since 1950 UTC (in-Double)</param>
      /// <param name="raOut">Output right ascension (deg) (out-Double)</param>
      /// <param name="declOut">Output declination (deg) (out-Double)</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void RotRADecl(int nutationTerms, int dir, double ds50UTCIn, double raIn, double declIn, double ds50UTCOut, out double raOut, out double declOut);


      /// <summary>
      /// Rotates Right Ascension and Declination from TEME of Date to MEME of the specified year of equinox
      /// </summary>
      /// <param name="nutationTerms">Nutation terms (4-106, 4:least accurate, 106:most acurate). (in-Integer)</param>
      /// <param name="yrOfEqnx">Year of equinox (see YROFEQNX_?) (in-Integer)</param>
      /// <param name="ds50UTCIn">Input time in days since 1950 UTC (in-Double)</param>
      /// <param name="raIn">Input right ascension (deg) (in-Double)</param>
      /// <param name="declIn">Input declination (deg) (in-Double)</param>
      /// <param name="raOut">Output right ascension (deg) (out-Double)</param>
      /// <param name="declOut">Output declination (deg) (out-Double)</param>
      /// <returns>0 if the rotation was successful, non-0 if there is an error</returns>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int RotRADec_DateToEqnx(int nutationTerms, int yrOfEqnx, double ds50UTCIn, double raIn, double declIn, out double raOut, out double declOut);


      /// <summary>
      /// Rotates Right Ascension and Declination from MEME of the specified year of equinox to TEME of Date
      /// </summary>
      /// <param name="nutationTerms">Nutation terms (4-106, 4:least accurate, 106:most acurate) (in-Integer)</param>
      /// <param name="yrOfEqnx">Year of equinox (see YROFEQNX_?) (in-Integer)</param>
      /// <param name="ds50UTCIn">Input time in days since 1950 UTC (in-Double)</param>
      /// <param name="raIn">Input right ascension (deg) (in-Double)</param>
      /// <param name="declIn">Input declination (deg) (in-Double)</param>
      /// <param name="raOut">Output right ascension (deg) (out-Double)</param>
      /// <param name="declOut">Output declination (deg) (out-Double)</param>
      /// <returns>0 if the rotation was successful, non-0 if there is an error</returns>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern int RotRADec_EqnxToDate(int nutationTerms, int yrOfEqnx, double ds50UTCIn, double raIn, double declIn, out double raOut, out double declOut);


      /// <summary>
      /// Rotates the Equinoctial covariance to UVW
      /// Note: This method uses the global Earth constants so make sure that you select the right Earth model by calling the EnvConst/EnvSetGeoIdx method
      /// The n terms must be normalized by n
      /// The input position, velocity and covariance must all have the same reference equator and equinox.
      /// </summary>
      /// <param name="pos">The input position vector in ECI (in-Double[3])</param>
      /// <param name="vel">The input velocity vector in ECI (in-Double[3])</param>
      /// <param name="covMtxEqnx">The input covariance matrix in equinoctial elements (af, ag, Lbar, n/n, chi, psi) (in-Double[6, 6])</param>
      /// <param name="covMtxUVW">The output covariance in UVW (out-Double[6, 6])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void CovMtxEqnxToUVW(double[] pos, double[] vel, double[,] covMtxEqnx, double[,] covMtxUVW);


      /// <summary>
      /// Rotates the UVW covariance to Equinoctial
      /// Note: This method uses the global Earth constants so make sure that you select the right Earth model by calling the EnvConst/EnvSetGeoIdx method
      /// The n terms are normalized by n
      /// The input position, velocity reference equator and equinox determine the output covariance reference frame.
      /// </summary>
      /// <param name="pos">The input position vector in ECI TEME Of Date (in-Double[3])</param>
      /// <param name="vel">The input velocity vector in ECI TEME Of Date (in-Double[3])</param>
      /// <param name="covMtxUVW">The input covariance matrix UVW (in-Double[6, 6])</param>
      /// <param name="covMtxEqnx">The output covariance in in equinoctial elements (af, ag, Lbar, n/n, chi, psi) (out-Double[6, 6])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void CovMtxUVWToEqnx(double[] pos, double[] vel, double[,] covMtxUVW, double[,] covMtxEqnx);


      /// <summary>
      /// Rotates the ECI covariance to UVW
      /// Note: This method uses the global Earth constants so make sure that you select the proper Earth model by calling the EnvConst/EnvSetGeoIdx method
      /// </summary>
      /// <param name="pos">The input position vector in either TEME or J2K ECI (in-Double[3])</param>
      /// <param name="vel">The input velocity vector in either TEME or J2K ECI (in-Double[3])</param>
      /// <param name="covMtxECI">The input covariance matrix in ECI (in-Double[6, 6])</param>
      /// <param name="covMtxUVW">The output covariance in UVW (out-Double[6, 6])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void CovMtxECIToUVW(double[] pos, double[] vel, double[,] covMtxECI, double[,] covMtxUVW);


      /// <summary>
      /// Rotates the UVW covariance to ECI
      /// Note: This method uses the global Earth constants so make sure that you select the proper Earth model by calling the EnvConst/EnvSetGeoIdx method
      /// </summary>
      /// <param name="pos">The input position vector in either TEME or J2K ECI (in-Double[3])</param>
      /// <param name="vel">The input velocity vector in either TEME or J2K ECI (in-Double[3])</param>
      /// <param name="covMtxUVW">The input covariance matrix in UVW (in-Double[6, 6])</param>
      /// <param name="covMtxECI">The output covariance in ECI (out-Double[6, 6])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void CovMtxUVWToECI(double[] pos, double[] vel, double[,] covMtxUVW, double[,] covMtxECI);


      /// <summary>
      /// Converts covariance matrix ECI to EFG.
      /// EFG = Earth Fixed Greenwich
      /// ECI = Earth Centered Inertial - need to determine TEME or J2K
      /// </summary>
      /// <param name="thetaG">theta - local sidereal time (rad) (in-Double)</param>
      /// <param name="covMtxECI">6x6 ECI Covariance (in-Double[6, 6])</param>
      /// <param name="covMtxEFG">6x6 EFG Covariance (out-Double[6, 6])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void CovMtxECIToEFG(double thetaG, double[,] covMtxECI, double[,] covMtxEFG);


      /// <summary>
      /// Converts covariance matrix EFG to ECI.
      /// EFG = Earth Fixed Greenwich
      /// ECI = Earth Centered Inertial - need to determine TEME or J2K
      /// </summary>
      /// <param name="thetaG">theta - local sidereal time (rad) (in-Double)</param>
      /// <param name="covMtxEFG">6x6 EFG Covariance (in-Double[6, 6])</param>
      /// <param name="covMtxECI">6x6 ECI Covariance (out-Double[6, 6])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void CovMtxEFGToECI(double thetaG, double[,] covMtxEFG, double[,] covMtxECI);


      /// <summary>
      /// Converts covariance matrix ECI of Date to ECI of J2K.
      /// J2K = Earth Centered Inertial MEME of J2K
      /// ECI = Earth Centered Inertial TEME of Date
      /// </summary>
      /// <param name="ds50Utc">Time in days since 1950 (in-Double)</param>
      /// <param name="nTerms">Number on nutation terms (106 max) (in-Integer)</param>
      /// <param name="covMtxECI">6x6 ECI of Date Covariance (in-Double[6, 6])</param>
      /// <param name="covMtxJ2k">6x6 MEME J2K Covariance (out-Double[6, 6])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void CovMtxECIToJ2k(double ds50Utc, int nTerms, double[,] covMtxECI, double[,] covMtxJ2k);


      /// <summary>
      /// Converts covariance matrix ECI of J2K to ECI of Date.
      /// J2K = Earth Centered Inertial MEME of J2K
      /// ECI = Earth Centered Inertial TEME of Date
      /// </summary>
      /// <param name="ds50Utc">Time in days since 1950 (in-Double)</param>
      /// <param name="nTerms">Number on nutation terms (106 max) (in-Integer)</param>
      /// <param name="covMtxJ2K">6x6 ECI of Date Covariance (in-Double[6, 6])</param>
      /// <param name="covMtxECI">6x6 MEME J2K Covariance (out-Double[6, 6])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void CovMtxJ2kToECI(double ds50Utc, int nTerms, double[,] covMtxJ2K, double[,] covMtxECI);


      /// <summary>
      /// Converts 6x6 symmetric Matrix/2D array to 1D array of 21 elements (lower triangular of a 6x6 symmetric matrix)
      /// </summary>
      /// <param name="symMtx6x6">Input 6x6 symmetric matrix (in-Double[6, 6])</param>
      /// <param name="lta21">Output 1D array of 21 elements (lower triangular of a 6x6 matrix) (out-Double[21])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void Mtx6x6ToLTA21(double[,] symMtx6x6, double[] lta21);


      /// <summary>
      /// Converts 1D array of 21 elements (lower triangular of a 6x6 symmetric matrix) to a 6x6 symmetric matrix
      /// </summary>
      /// <param name="lta21">Input 1D array of 21 elements (lower triangular of a 6x6 matrix) (in-Double[21])</param>
      /// <param name="symMtx6x6">Output 6x6 symmetric matrix (out-Double[6, 6])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void LTA21ToMtx6x6(double[] lta21, double[,] symMtx6x6);


      /// <summary>
      /// Converts 9x9 symmetric Matrix/2D array to 1D array of 45 elements (lower triangular of a 9x9 symmetric matrix)
      /// </summary>
      /// <param name="symMtx9x9">Input 9x9 symmetric matrix (in-Double[9, 9])</param>
      /// <param name="lta45">Output 1D array of 45 elements (lower triangular of a 9x9 matrix) (out-Double[45])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void Mtx9x9ToLTA45(double[,] symMtx9x9, double[] lta45);


      /// <summary>
      /// Converts 1D array of 45 elements (lower triangular of a 9x9 symmetric matrix) to a 9x9 symmetric matrix
      /// </summary>
      /// <param name="lta45">Input 1D array of 45 elements (lower triangular of a 9x9 matrix) (in-Double[45])</param>
      /// <param name="symMtx9x9">Output 9x9 symmetric matrix (out-Double[9, 9])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void LTA45ToMtx9x9(double[] lta45, double[,] symMtx9x9);


      /// <summary>
      /// Propagate xyzDate covariance forward to the propagation time
      /// </summary>
      /// <param name="rms">Root mean square of observation residulas (in-Double)</param>
      /// <param name="consider">Density consider parameter, 12 is recommended (this is a percentile) (in-Double)</param>
      /// <param name="stateArray">State transition matrix (in-Double[54])</param>
      /// <param name="cov">Covariance matrix at start time (in-Double[9, 9])</param>
      /// <param name="propCov">Covariance at end time for state matrix (out-Double[6, 6])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void PropCovFrState(double rms, double consider, double[] stateArray, double[,] cov, double[,] propCov);


      /// <summary>
      /// Rotates the ECI covariance to Equinoctial
      /// Note: This method uses the global Earth constants so make sure that you select the proper Earth model by calling the EnvConst/EnvSetGeoIdx method
      /// </summary>
      /// <param name="pos">The input position vector in either TEME or J2K ECI (in-Double[3])</param>
      /// <param name="vel">The input velocity vector in either TEME or J2K ECI (in-Double[3])</param>
      /// <param name="covMtxECI">The input covariance matrix in ECI (in-Double[9, 9])</param>
      /// <param name="covMtxEqnx">The output covariance in UVW (out-Double[9, 9])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void CovMtxECIToEqnx(double[] pos, double[] vel, double[,] covMtxECI, double[,] covMtxEqnx);


      /// <summary>
      /// Rotates the Equinoctial covariance to ECI 
      /// Note: This method uses the global Earth constants so make sure that you select the proper Earth model by calling the EnvConst/EnvSetGeoIdx method
      /// </summary>
      /// <param name="pos">The input position vector in either TEME or J2K ECI (in-Double[3])</param>
      /// <param name="vel">The input velocity vector in either TEME or J2K ECI (in-Double[3])</param>
      /// <param name="covMtxEqnx">45 or 55 (in-Double[9, 9])</param>
      /// <param name="covMtxECI">The output covariance in ECI (out-Double[9, 9])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void CovMtxEqnxToECI9x9(double[] pos, double[] vel, double[,] covMtxEqnx, double[,] covMtxECI);


      /// <summary>
      /// Rotates the Equinoctial covariance to UVW
      /// Note: This method uses the global Earth constants so make sure that you select the proper Earth model by calling the EnvConst/EnvSetGeoIdx method
      /// </summary>
      /// <param name="pos">The input position vector in either TEME or J2K ECI (in-Double[3])</param>
      /// <param name="vel">The input velocity vector in either TEME or J2K ECI (in-Double[3])</param>
      /// <param name="covMtxEqnx">45 or 55 (in-Double[9, 9])</param>
      /// <param name="covMtxUVW">The output covariance in ECI (out-Double[9, 9])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void CovMtxEqnxToUVW9x9(double[] pos, double[] vel, double[,] covMtxEqnx, double[,] covMtxUVW);


      /// <summary>
      /// Rotates the ECR covariance to EFG
      /// </summary>
      /// <param name="ds50UTC">Input time in days since 1950 UTC (polarX, polarY will be retrieved from this time) (in-Double)</param>
      /// <param name="covMtxECR">The input covariance matrix in ECR (in-Double[6, 6])</param>
      /// <param name="covMtxEFG">The output covariance in EFG (out-Double[6, 6])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void CovMtxECRToEFG(double ds50UTC, double[,] covMtxECR, double[,] covMtxEFG);


      /// <summary>
      /// Rotates the EFG covariance to ECR
      /// </summary>
      /// <param name="ds50UTC">Input time in days since 1650 UTC (polarX, polarY will be retrieved from this time) (in-Double)</param>
      /// <param name="covMtxEFG">The input covariance in EFG (in-Double[6, 6])</param>
      /// <param name="covMtxECR">The output covariance matrix in ECR (out-Double[6, 6])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void CovMtxEFGToECR(double ds50UTC, double[,] covMtxEFG, double[,] covMtxECR);


      /// <summary>
      /// Update (propagate) covariance to a future time with a supplied covariance, state transition matrix
      /// consider parameter and RMS. Consider parameter is applied to the drag term only.
      /// Full covariance matrix is multiplied by RMS squared.  State transition matrix can be obtained from 
      /// SpProp.SpGetStateMtx or supplying your own. State matrix, input and output covariance must be in 
      /// matching coordinate systems.
      /// </summary>
      /// <param name="rmsIn">Root mean square of sensor errors (in-Double)</param>
      /// <param name="consider">Consider parameter (percentage) (in-Double)</param>
      /// <param name="cov">Covariance at epoch (in-Double[9, 9])</param>
      /// <param name="stateArray">Consider parameter and root mean square (in-Double[54])</param>
      /// <param name="propCov">Updated covariance (out-Double[6, 6])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void CovMtxUpdate(double rmsIn, double consider, double[,] cov, double[] stateArray, double[,] propCov);


      /// <summary>
      /// Annual Aberration calculated using equations from Astronomical Algorithms, Jean Meeus, 2nd Edition with Corrections as of June 15, 2005
      /// </summary>
      /// <param name="ra">Input right ascension (deg) (in-Double)</param>
      /// <param name="decl">Input declination (deg) (in-Double)</param>
      /// <param name="dS50UTC">Time in days since 1950 UTC (in-Double)</param>
      /// <param name="raDelta">Right Ascension delta due to Annual Aberration (deg) (out-Double)</param>
      /// <param name="decDelta">Declination delta due to Annual Aberration (deg) (out-Double)</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void AberrationAnnual(double ra, double decl, double dS50UTC, out double raDelta, out double decDelta);


      /// <summary>
      /// Diurnal Aberration is due to the rotation of the Earth about it's axis. This is only valid for ground based sensors.
      /// Diurnal Aberration calculated using equations from Explanatory Supplement to the Astronomical Almanac 3rd Edition, 2013
      /// </summary>
      /// <param name="ra">Input right ascension (deg) (in-Double)</param>
      /// <param name="decl">Input declination (deg) (in-Double)</param>
      /// <param name="dS50UTC">Time in days since 1950 UTC (in-Double)</param>
      /// <param name="senPos">Sensor EFG geocentric position (meters) (in-Double[3])</param>
      /// <param name="raDelta">Right Ascension delta due to Diurnal Aberration (deg) (out-Double)</param>
      /// <param name="decDelta">Declination delta due to Diurnal Aberration (deg) (out-Double)</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void AberrationDiurnal(double ra, double decl, double dS50UTC, double[] senPos, out double raDelta, out double decDelta);


      /// <summary>
      /// Sets JPL parameters
      /// Notes: Set JPL parameters will be used by SP, SPG4-XP, and anything that requires access to JPL data
      /// </summary>
      /// <param name="jplFile">The name of the JPL file. (in-Character[512])</param>
      /// <param name="ds50Start">Start Time of JPL ephmeris to load (in-Double)</param>
      /// <param name="ds50Stop">End Time of JPL ephemeris to load (in-Double)</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void JplSetParameters(string jplFile, double ds50Start, double ds50Stop);


      /// <summary>
      /// Gets JPL parameters
      /// </summary>
      /// <param name="jplFile">The name of the JPL file. (out-Character[512])</param>
      /// <param name="ds50Start">Start Time of Ephmeris to load (out-Double)</param>
      /// <param name="ds50Stop">End Time of Ephemeris to load (out-Double)</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void JplGetParameters(byte[] jplFile, out double ds50Start, out double ds50Stop);


      /// <summary>
      /// Resets JPL parameters & removes JPL ephemeris data
      /// </summary>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void JplReset();


      /// <summary>
      /// Computes various Sun and Moon vectors base on loaded JPL data at the specified time.
      /// Note: if JPL data isn't loaded or available, all output parameters are set to zero
      /// </summary>
      /// <param name="ds50UTC">Input time in day since 1950 UTC (in-Double)</param>
      /// <param name="uvecSun">The resulting sun position unit vector. (out-Double[3])</param>
      /// <param name="sunVecMag">The resulting magnitude of the sun position vector (km). (out-Double)</param>
      /// <param name="uvecMoon">The resulting moon position unit vector. (out-Double[3])</param>
      /// <param name="moonVecMag">The resulting magnitude of the moon position vector (km). (out-Double)</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void JplCompSunMoonVec(double ds50UTC, double[] uvecSun, out double sunVecMag, double[] uvecMoon, out double moonVecMag);


      /// <summary>
      /// Computes Sun and Moon position vectors base on loaded JPL data at the specified time.
      /// Note: if JPL data isn't loaded or available, all output parameters are set to zero
      /// </summary>
      /// <param name="ds50UTC">Input time in day since 1950 UTC (in-Double)</param>
      /// <param name="sunVec">The resulting sun position vector (km). (out-Double[3])</param>
      /// <param name="moonVec">The resulting sun position vector (km). (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void JplCompSunMoonPos(double ds50UTC, double[] sunVec, double[] moonVec);


      /// <summary>
      /// Removes the JPL ephemeris from memory
      /// </summary>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void RemoveJpl();


      /// <summary>
      /// Rotates position and velocity vectors from TEME of Epoch to TEME of Date
      /// </summary>
      /// <param name="nutationTerms">Nutation terms (4-106, 4:least accurate, 106:most acurate). (in-Integer)</param>
      /// <param name="epochDs50TAI">The date of which posEpoch/velEpoch are based on, expressed in days since 1950, TAI. (in-Double)</param>
      /// <param name="dateDs50TAI">The date of which posEpoch/velEpoch will rotate to, expressed in days since 1950, TAI. (in-Double)</param>
      /// <param name="posEpoch">The position vector in TEME of Epoch. (in-Double[3])</param>
      /// <param name="velEpoch">The velocity vector in TEME of Epoch. (in-Double[3])</param>
      /// <param name="posDate">The resulting position vector in TEME of Date. (out-Double[3])</param>
      /// <param name="velDate">The resulting velocity vector in TEME of Date. (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void TemeEpochToDate(int nutationTerms, double epochDs50TAI, double dateDs50TAI, double[] posEpoch, double[] velEpoch, double[] posDate, double[] velDate);


      /// <summary>
      /// Converts ECR position and velocity vectors to ECI (TEME of Date) position and velocity vectors.
      /// Note: Without timing constant data, the accuracy of the conversion results will be reduced
      /// </summary>
      /// <param name="ds50UTC">Input time in days since 1950 UTC (in-Double)</param>
      /// <param name="posECR">The ECR position vector (km) to be converted. (in-Double[3])</param>
      /// <param name="velECR">The ECR velocity vector (km/s) to be converted. (in-Double[3])</param>
      /// <param name="posECI">The resulting ECI (TEME of Date) position vector (km). (out-Double[3])</param>
      /// <param name="velECI">The resulting ECI (TEME of Date) velocity vector (km/s). (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void ECRToECITime(double ds50UTC, double[] posECR, double[] velECR, double[] posECI, double[] velECI);


      /// <summary>
      /// Converts ECI (TEME of Date) position and velocity vectors to ECR position and velocity vectors.
      /// Note: Without timing constant data, the accuracy of the conversion results will be reduced
      /// </summary>
      /// <param name="ds50UTC">Input time in days since 1950 UTC (in-Double)</param>
      /// <param name="posECI">The ECI (TEME of Date) position vector (km) to be converted. (in-Double[3])</param>
      /// <param name="velECI">The ECI (TEME of Date) velocity vector (km/s) to be converted. (in-Double[3])</param>
      /// <param name="posECR">The resulting ECR position vector (km). (out-Double[3])</param>
      /// <param name="velECR">The resulting ECR velocity vector (km/s). (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void ECIToECRTime(double ds50UTC, double[] posECI, double[] velECI, double[] posECR, double[] velECR);


      /// <summary>
      /// Converts ECR position and velocity vectors to J2K (MEME of J2000) position and velocity vectors.
      /// Note: Without timing constant data, the accuracy of the conversion results will be reduced
      /// </summary>
      /// <param name="spectr">Specifies whether to run in SPECTR compatibility mode. A value of 1 means Yes. (in-Integer)</param>
      /// <param name="nutationTerms">Nutation terms (4-106, 4:least accurate, 106:most acurate). (in-Integer)</param>
      /// <param name="ds50UTC">Time in days since 1950, UTC for which the coordinates of position and velocity vectors are currently expressed. (in-Double)</param>
      /// <param name="posECR">The ECR position vector (km) to be converted. (in-Double[3])</param>
      /// <param name="velECR">The ECR velocity vector (km/s) to be converted. (in-Double[3])</param>
      /// <param name="posJ2K">The resulting J2K (MEME of J2000) position vector (km). (out-Double[3])</param>
      /// <param name="velJ2K">The resulting J2K (MEME of J2000) velocity vector (km/s). (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void ECRToJ2KTime(int spectr, int nutationTerms, double ds50UTC, double[] posECR, double[] velECR, double[] posJ2K, double[] velJ2K);


      /// <summary>
      /// Converts J2K (MEME of J2000) position and velocity vectors to ECR position and velocity vectors.
      /// Note: Without timing constant data, the accuracy of the conversion results will be reduced
      /// </summary>
      /// <param name="spectr">Specifies whether to run in SPECTR compatibility mode. A value of 1 means Yes. (in-Integer)</param>
      /// <param name="nutationTerms">Nutation terms (4-106, 4:least accurate, 106:most acurate). (in-Integer)</param>
      /// <param name="ds50UTC">Time in days since 1950, UTC for which the coordinates of position and velocity vectors are currently expressed. (in-Double)</param>
      /// <param name="posJ2K">The J2K (MEME of J2000) position vector (km) to be converted. (in-Double[3])</param>
      /// <param name="velJ2K">The J2K (MEME of J2000) velocity vector (km/s) to be converted. (in-Double[3])</param>
      /// <param name="posECR">The resulting ECR position vector (km). (out-Double[3])</param>
      /// <param name="velECR">The resulting ECR velocity vector (km/s). (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void J2KToECRTime(int spectr, int nutationTerms, double ds50UTC, double[] posJ2K, double[] velJ2K, double[] posECR, double[] velECR);


      /// <summary>
      /// Converts an ECR position vector to geodetic latitude, longitude, and height.
      /// </summary>
      /// <param name="posECR">The ECR position vector (km) to be converted. (in-Double[3])</param>
      /// <param name="metricLLH">The resulting geodetic north latitude (degree), east longitude (degree), and height (km). (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void ECRPosToLLH(double[] posECR, double[] metricLLH);


      /// <summary>
      /// Converts geodetic latitude, longitude, and height to an ECR position vector.
      /// </summary>
      /// <param name="metricLLH">An Array containing the geodetic north latitude (degree), east longitude (degree), and height (km) to be converted. (in-Double[3])</param>
      /// <param name="posECR">The resulting ECR position vector (km). (out-Double[3])</param>
      [DllImport(AstroFuncDll, CallingConvention = CallingConvention.Cdecl)]
      public static extern void LLHToECRPos(double[] metricLLH, double[] posECR);
      
      // Index of Keplerian elements
      public static int  
         XA_KEP_A     =   0,       // semi-major axis (km)
         XA_KEP_E     =   1,       // eccentricity (unitless)
         XA_KEP_INCLI =   2,       // inclination (deg)
         XA_KEP_MA    =   3,       // mean anomaly (deg)
         XA_KEP_NODE  =   4,       // right ascension of the asending node (deg)
         XA_KEP_OMEGA =   5,       // argument of perigee (deg)
         XA_KEP_SIZE  =   6;   
         
      // Index of classical elements
      public static int  
         XA_CLS_N     =   0,       // N mean motion (revs/day)
         XA_CLS_E     =   1,       // eccentricity (unitless)
         XA_CLS_INCLI =   2,       // inclination (deg)
         XA_CLS_MA    =   3,       // mean anomaly (deg)
         XA_CLS_NODE  =   4,       // right ascension of the asending node (deg)
         XA_CLS_OMEGA =   5,       // argument of perigee (deg)
         XA_CLS_SIZE  =   6;
      
      // Index of equinoctial elements
      public static int  
         XA_EQNX_AF   =   0,       // Af (unitless) 
         XA_EQNX_AG   =   1,       // Ag (unitless)
         XA_EQNX_CHI  =   2,       // chi (unitless)
         XA_EQNX_PSI  =   3,       // psi (unitless)
         XA_EQNX_L    =   4,       // L mean longitude (deg)
         XA_EQNX_N    =   5,       // N mean motion (revs/day)
         XA_EQNX_SIZE =   6;
         
      // Indexes of AstroConvFrTo
      public static int  
         XF_CONV_SGP42SGP = 101;        // SGP4 (A, E, Incli, BStar) to SGP (nDot, n2Dot)
      
      
      // Indexes for topocentric components
      public static int  
         XA_TOPO_RA    = 0,         // Right ascension (deg)
         XA_TOPO_DEC   = 1,         // Declination (deg)
         XA_TOPO_AZ    = 2,         // Azimuth (deg)
         XA_TOPO_EL    = 3,         // Elevation (deg)
         XA_TOPO_RANGE = 4,         // Range (km)
         XA_TOPO_RADOT = 5,         // Right ascension dot (deg/s)
         XA_TOPO_DECDOT= 6,         // Declincation dot (deg/s)
         XA_TOPO_AZDOT = 7,         // Azimuth dot (deg/s)
         XA_TOPO_ELDOT = 8,         // Elevation dot (deg/s)
         XA_TOPO_RANGEDOT = 9,      // Range dot (km/s)   
         XA_TOPO_SIZE  = 10;   
         
         
      // Indexes for RAE components
      public static int  
         XA_RAE_RANGE   = 0,        // Range (km)
         XA_RAE_AZ      = 1,        // Azimuth (deg)
         XA_RAE_EL      = 2,        // Elevation (deg)
         XA_RAE_RANGEDOT= 3,        // Range dot (km/s)   
         XA_RAE_AZDOT   = 4,        // Azimuth dot (deg/s)
         XA_RAE_ELDOT   = 5,        // Elevation dot (deg/s)
         XA_RAE_SIZE    = 6;
         
         
      // Year of Equinox indicator
      public static int  
         YROFEQNX_OBTIME = 0,       // Date of observation
         YROFEQNX_CURR   = 1,       // 0 Jan of Date
         YROFEQNX_2000   = 2,       // J2000
         YROFEQNX_1950   = 3;       // B1950
         
   }
// ========================= End of auto generated code ==========================
}
