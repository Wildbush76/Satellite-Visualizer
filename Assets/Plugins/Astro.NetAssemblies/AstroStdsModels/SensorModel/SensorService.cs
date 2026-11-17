using System;
using AFSPC.AstroStds.Wrappers;
using System.IO;
using AFSPC.AstroStds.Utilities;

namespace AFSPC.AstroStds.Models.SensorModel
{
   public static class SensorService
   {
      // Print sensor's limits data
      public static void PrintSensorData(StreamWriter sw, long senKey)
      {
         string[] unitStr = new string[] { "km, km/sec", "nm, nm/sec" };
         string[] rngUnitStr = new string[] { "km", "nm" };
         string[] noYes = new string[] { "no", "yes", "yes" };

         string dashLine = new string('-', 79);
         string ddashLine = new string('=', 79);

         Sensor sen = new Sensor(senKey);

         string lim1Str = "ELEVATION";
         string lim2Str = " AZIMUTH ";

         sw.WriteLine(" " + ddashLine);

         int senNum = SensorWrapper.SenNumOf(senKey);

         if (sen.ViewType == '9') // orbiting sensor
         {
            sw.WriteLine(" |                ORBITNG SENSOR {0,3} (SATELLITE {1,5}) DATA                    |", senNum, sen.OrbSatNum);
            sw.WriteLine(" " + dashLine);
            if (sen.Boresight1 > 0)
            {
               lim1Str = "OFF-BORST";
               lim2Str = "CLOCK ANG";
            }
         }
         else
         {
            sw.WriteLine(" |                               SENSOR {0,3} DATA                               |", senNum);
            sw.WriteLine(" " + dashLine);
            sw.WriteLine(" |  SENSOR LOCATION                                                            |");
            sw.WriteLine(" |  ASTRONOMICAL LAT  (deg): {0,9:F4}{1,-41}|", sen.AstroLatN_deg, " ");
            sw.WriteLine(" |  ASTRONOMICAL LONG (deg): {0,9:F4} (+West, -East){1,-26}|", -sen.AstroLonE_deg, " ");
            
            if (sen.SenLocType == SensorWrapper.SENLOC_TYPE_ECR)
            {
               sw.WriteLine(" |  GEOCENTRIC ROT. X (m)  : {0,9:F0}{1,-41}|", sen.SenPosECR_m[0], " ");
               sw.WriteLine(" |  GEOCENTRIC ROT. Y (m)  : {0,9:F0}{1,-41}|", sen.SenPosECR_m[1], " ");
               sw.WriteLine(" |  GEOCENTRIC ROT. Z (m)  : {0,9:F0}{1,-41}|", sen.SenPosECR_m[2], " ");
            }
            else if (sen.SenLocType == SensorWrapper.SENLOC_TYPE_EFG)
            {
               sw.WriteLine(" |  EFG X (m)              : {0,9:F0}{1,-41}|", sen.SenPosEFG_m[0], " ");
               sw.WriteLine(" |  EFG Y (m)              : {0,9:F0}{1,-41}|", sen.SenPosEFG_m[1], " ");
               sw.WriteLine(" |  EFG Z (m)              : {0,9:F0}{1,-41}|", sen.SenPosEFG_m[2], " ");
            }
            else if (sen.SenLocType == SensorWrapper.SENLOC_TYPE_LLH)
            {
               sw.WriteLine(" |  LATITUDE (deg)         : {0,9:F4}{1,-41}|", sen.SenPosLLH.Latitude_deg, " ");
               sw.WriteLine(" |  LONGITUDE (deg)        : {0,9:F4}{1,-41}|", sen.SenPosLLH.Longitude_deg, " ");
               sw.WriteLine(" |  HEIGHT (m)             : {0,9:F4}{1,-41}|", sen.SenPosLLH.Height_km * 1000, " ");
            }
         }

         sw.WriteLine(" " + dashLine);
         sw.WriteLine(" |  SENSOR LIMITS                                                              |");
         sw.WriteLine(" |  A.  SENSOR TYPE:                {0, -43}|", ((SensorWrapper.SenViewTypeEnum)sen.ViewType).ToDescription());
         sw.WriteLine(" |  B.  SENSOR OBSERVATION TYPE:    {0, -43}|", ((SensorWrapper.SenObsTypeEnum)sen.ObsType).ToDescription());
         sw.WriteLine(" |  C.  RANGE/RANGE-RATE UNITS:     {0, -43}|", unitStr[sen.RngUnits]);
         sw.WriteLine(" |  D.  APPLY RANGE CONSTRAINTS?    {0, -43}|", sen.RngLimFlg == 0 ? "yes" : "no");
         if (sen.RngLimFlg == 0)
            sw.WriteLine(" |  E.  MIN/MAX OBSERVABLE RANGE:   {0, 8} to{1, 9} {2, -22}|", sen.MinRngLim, sen.MaxRngLim, rngUnitStr[sen.RngUnits]);

         if (sen.ViewType == 'R')
         {
            string fileName = Utility.GetKey64String(SensorWrapper.SensorGetLimField, senKey, SensorWrapper.XF_SENLIM_FILE);
            sw.WriteLine(" |  F.  AZ/EL TABLE FILE NAME   :   {0, -43}|", fileName);
         }
         else if (sen.ViewType == 'V')
         {
            sw.WriteLine(" |  F.  N/S BEAMWIDTH ANGLE:        {0,8:F3}{1, -35}|", sen.ElLim1, " deg");
            sw.WriteLine(" |  G.  FENCE TILT ANGLE   :        {0,8:F3}{1, -35}|", sen.ElLim2, " deg");
         }
         else if (sen.ViewType == '2')
         {
            if (sen.AzLim1 != 0 && sen.AzLim2 != 0 && sen.ElLim1 != 0 && sen.ElLim2 != 0)
            {
               sw.WriteLine(" |  F.  BORESIGHT AZIMUTH  :        {0,8:F3}{1, -35}|", sen.AzLim1, " deg");
               sw.WriteLine(" |  G.  BORESIGHT ELEVATION:        {0,8:F3}{1, -35}|", sen.ElLim1, " deg");
               sw.WriteLine(" |  H.  OFF-BORESIGHT ANGLE:        {0,8:F3}{1, -35}|", sen.AzLim2, " deg");
               sw.WriteLine(" |  I.  MIN ELEVATION ANGLE:        {0,8:F3}{1, -35}|", sen.ElLim2, " deg");
            }
         }
         else if (sen.ViewType == '7')
         {
            sw.WriteLine(" |  F.  CONSTANT AZIMUTH   :        {0,8:F3}{1, -35}|", sen.AzLim1, " deg");
            sw.WriteLine(" |  G.  MIN ELEVATION ANGLE:        N/A");
            sw.WriteLine(" |  G.  MIN ELEVATION ANGLE:        N/A{0, -40}|", " ");
            sw.WriteLine(" |  H.  FAN TILT ANGLE     :        {0,8:F3}{1, -35}|", sen.ElLim2, " deg (-: N/W tilt, +: S/E tilt)");
            sw.WriteLine(" |  I.  OFF-BORESIGHT ANGLE:        {0,8:F3}{1, -35}|", sen.AzLim2, " deg");
         }
         else if (sen.ViewType != 'V')
         {
            sw.WriteLine(" |  F.  FIRST  {0} INTERVAL:  {1,8:F3} to {2,8:F3}{3, -23}|", lim1Str, sen.ElLim1, sen.ElLim2, " deg");
            sw.WriteLine(" |  G.  FIRST  {0} INTERVAL:  {1,8:F3} to {2,8:F3}{3, -23}|", lim2Str, sen.AzLim1, sen.AzLim2, " deg");
         }

         if (sen.ViewType == '9' && sen.Boresight2 == ' ') // no limit or n/a
         {
            sw.WriteLine(" |  H.  SECOND {0} INTERVAL:  {1, -43}|", lim1Str, "n/a");
            sw.WriteLine(" |  I.  SECOND {0} INTERVAL:  {1, -43}|", lim2Str, "n/a");
         }
         else if (sen.ViewType != '2' && sen.ViewType != '7' && sen.ViewType != 'R' && sen.ViewType != 'V')
         {
            sw.WriteLine(" |  H.  SECOND {0} INTERVAL:  {1,8:F3} to {2,8:F3}{3, -23}|", lim1Str, sen.ElLim3, sen.ElLim4, " deg");
            sw.WriteLine(" |  I.  SECOND {0} INTERVAL:  {1,8:F3} to {2,8:F3}{3, -23}|", lim2Str, sen.AzLim3, sen.AzLim4, " deg");
         }

         int numOfSegs = Utility.GetKey64Int(SensorWrapper.SensorGetLimField, sen.SenKey, SensorWrapper.XF_SENLIM_NUMSEGS); ; // GetKey64IntegerField((fnPtrGetKey64Field)SensorGetLimField, senKey, XF_SENLIM_NUMSEGS);
         if (numOfSegs > 0)
         {
            sw.WriteLine(" |  ADDITIONAL SENSOR LIMITS  (via 3L-Card or call SensorAddSegments() API     |");
            sw.WriteLine(" |  - DOME: Az fr/to (deg), El fr/to (deg), Range min/max (km)                 |");
            sw.WriteLine(" |  - CONE: BS Az/El (deg), Angle fr/to (deg), Range min/max (km), min El (deg)|");
         }

         for (int segNum = 1; segNum <= numOfSegs; segNum++)
         {
            int segType;
            double[] xa_seg = new double[16];
            int errCode = SensorWrapper.SensorGetSegment(senKey, segNum, out segType, xa_seg);
            if (segType == SensorWrapper.SEG_DOME)
            {
               sw.WriteLine(" |  {0:D3} - DOME: {1, 7:F3}, {2 ,7:F3}, {3 ,7:F3}, {4 ,7:F3}, {5 ,7:F0}, {6 ,7:F0}           |", segNum, 
                  xa_seg[0], xa_seg[1], xa_seg[2], xa_seg[3], xa_seg[4], xa_seg[5]);
            }
            else if (segType == SensorWrapper.SEG_BCONE)
            {
               sw.WriteLine(" |  {0:D3} - CONE: {1, 7:F3}, {2 ,7:F3}, {3 ,7:F3}, {4 ,7:F3}, {5 ,7:F0}, {6 ,7:F0} , {6 ,7:F3}  |", segNum,
                  xa_seg[0], xa_seg[1], xa_seg[2], xa_seg[3], xa_seg[4], xa_seg[5], xa_seg[6]);
            }
         }

         sw.WriteLine(" |  J.  NUMBER OF POINTS PER PASS:  {0, 4}{1, 39}|", sen.MaxPPP, " ");
         sw.WriteLine(" |  K.  INTERVAL BETWEEN POINTS:    {0,8:F3}{1, -35}|", sen.Interval, " min");
         sw.WriteLine(" |      (Note: The Lamod's Output Interval value takes precedence over this)   |");

         if (sen.ObsType == '5' || sen.ObsType == '9' || sen.ObsType == 'O' || sen.ObsType == '1')
         {
            sw.WriteLine(" |  L.  IR/OPTICAL VISIBILITY CK:   {0, -43}|", ((SensorWrapper.SenOptVisFlgEnum)sen.VisFlg).ToDescription());
            sw.WriteLine(" |      (Note: The Lamod's Optical Visibility flag takes precedence over this) |");
         }
         else if (sen.VisFlg == 2 || sen.VisFlg == 3)
            sw.WriteLine(" |  L.  APPLY SOLAR ASPECT ANGLE:   {0, -43}|", "yes");
         else
         {
            sw.WriteLine(" |  L.  OPTICALLY VISIBLE OPTION:   {0, -43}|", "n/a");
            sw.WriteLine(" |      (Note: The Lamod's Optical Visibility flag takes precedence over this) |");
         }

         sw.WriteLine(" |  M.  RGE-RT/REL-VELOCITY LIMIT:  {0,8:F3}{1,-35}|", sen.RrLim, " k/s");
         if (sen.ViewType == '9')
         {
            sw.WriteLine(" |  O.  PLANETARY RESTRICTIONS:     {0, -43}|", ((SensorWrapper.SenPlntryRstrtnEnum)sen.PlntryRes).ToDescription());
            if (sen.PlntryRes != 5)
            {
               sw.WriteLine(" |  P.  ALLOW EARTH IN BACKGROUND?  {0, -43}|", noYes[sen.EarthBckgrnd]);
               sw.WriteLine(" |  R.  EARTH LIMB EXCLUSION DIST.  {0,8:F3}{1,-35}|", sen.EarthLimb, " km");
               sw.WriteLine(" |  S.  SOLAR EXCLUSION ANGLE:      {0,8:F3}{1,-35}|", sen.SolarXAngle, " deg");
               sw.WriteLine(" |  T.  LUNAR EXCLUSION ANGLE:      {0,8:F3}{1,-35}|", sen.LunarXAngle, " deg");
            }
            sw.WriteLine(" |  U.  SENSOR BORESIGHT VECTOR 1:  {0, -43}|", ((SensorWrapper.SenBoreVecEnum)sen.Boresight1).ToDescription());
            sw.WriteLine(" |  V.  SENSOR BORESIGHT VECTOR 2:  {0, -43}|", ((SensorWrapper.SenBoreVecEnum)sen.Boresight2).ToDescription());
         }
         sw.WriteLine(" " + dashLine);
         sw.WriteLine("");
      }

   }
}
