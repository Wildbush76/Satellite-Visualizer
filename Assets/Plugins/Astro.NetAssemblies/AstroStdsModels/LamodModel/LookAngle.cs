using System;
using System.ComponentModel;

using AFSPC.AstroStds.Wrappers;
using AFSPC.AstroStds.Models.SatelliteModel;
using AFSPC.AstroStds.Models.ObsModel;



namespace AFSPC.AstroStds.Models.LamodModel
{
   [Serializable]
   public class LookAngle
   {
      [Browsable(false)]
      public int ErrCode { get; set; }
      [Browsable(false)]
      public int LookCode { get; set; }

      public int SatNum { get; set; }

      [Browsable(false)]
      public string SatRecName
      {
         get
         {
            return SatStateService.GetRecName(SatKey);
         }
      }

      [Browsable(false)]
      public long SatKey { get; set; }

      public int SenNum { get; set; }
      public DaysSince50UTC LookTimeDs50UTC { get; set; } //DateTime LookTime { get; set; }

      public double LookTimeMSE { get; set; }
      public string DDD_ddd
      {
         get
         {
            return LookTimeDs50UTC.Dtg17Str.Substring(5); // TimeFuncWrapper.UTCToDtg17Str(LookTimeDs50UTC).Substring(5);
         }
      }


      public string YYDDDHHMMSS
      {
         get
         {
            return LookTimeDs50UTC.Dtg23Str; // TimeFuncWrapper.UTCToDtg23Str(LookTimeDs50UTC);
         }
      }

      public string DTGForm2
      {
         get
         {
            return LookTimeDs50UTC.ToDTGForm2();
         }
      }

      public double Azimuth_deg { get; set; }
      public double Elevation_deg { get; set; }
      public double Range_km { get; set; }
      public double RangeRate_kmps { get; set; }
      public double RA_deg { get; set; }
      public double Declination_degDec { get; set; }
      public double AzRate_degps { get; set; }
      public double ElRate_degps { get; set; }
      public double SAA_deg { get; set; }
      public char Illumination { get; set; }
      public double RelVel { get; set; }
      public double OffBoresightAngle { get; set;}
      public double AngleToFan { get; set; }
      public double EaSenSat_deg { get; set; }
      public double EaSatSen_deg { get; set; }
      public double EaSenLimb_deg { get; set; }


      [Browsable(false)]
      public double[] SenPosEFG { get; set; }

      /// <summary>
      /// vector from center of the earth to the satellite
      /// </summary>
      [Browsable(false)]
      public double[] UVec { get; set; }


      /// <summary>
      /// station unit vector - vector directed from the center of the earth to the sensor
      /// </summary>
      [Browsable(false)]
      public double[] LVec { get; set; }

      /// <summary>
      /// station zenith vector 
      /// </summary>
      [Browsable(false)]
      public double[] ZVec { get; set; }


      /// <summary>
      /// slant range unit vector - vector from the sensor to the satellite
      /// </summary>
      [Browsable(false)]
      public double[] UnitRangeVec { get; set; }  // slant range unit vector (rho unit vector)

      /// <summary>
      /// range vector (km) - vector from the sensor to the satellite
      /// </summary>
      [Browsable(false)]
      public double[] RangeVec { get; set; }

      /// <summary>
      /// computed range-rate vector/relative velocity vector (km/sec)
      /// </summary>
      [Browsable(false)]
      public double[] RngRateVec { get; set; }


      [Browsable(false)]
      public double[] SunVec { get; set; }

      [Browsable(false)]
      public double[] MoonVec { get; set; }
      [Browsable(false)]
      public double[] LimChkFlgs { get; set; }

      public bool PassVisual { get; set; }
      public bool PassAzLim { get; set; }
      public bool PassElLim { get; set; }
      public bool PassEarthBkgr { get; set; }
      public bool PassEarthOb { get; set; }
      public bool PassLunExcl { get; set; }
      public bool PassMaxRange { get; set; }
      public bool PassMinRange { get; set; }
      public bool PassPenEclip { get; set; }
      public bool PassRVelLim { get; set; }
      public bool PassSAALim { get; set; }
      public bool PassSolExcl { get; set; }
      public bool PassUmbEclip { get; set; }

      

      public int RAHr { get; set; }
      public int RAMin { get; set; }
      public double RASec { get; set; }
      public int DecliDeg { get; set; }
      public int DecliMin { get; set; }
      public double DecliSec { get; set; }

      

      public double SunSenSat_deg { get; set; }
      public double SunEarthSat_deg { get; set; }
      public double SunEarthSen_deg { get; set; }
      public double MoonSenSat_deg { get; set; }
      public double MoonEarthSat_deg { get; set; }
      public double MoonEarthSen_deg { get; set; }
      public int NBore1 { get; set; }
      public int NBore2 { get; set; }
      public double OffBoreElev1_deg { get; set; }
      public double OffBoreAzim1_deg { get; set; }
      public double OffBoreElev2_deg { get; set; }
      public double OffBoreAzim2_deg { get; set; }


      public SatellitePosition SatPosECI { get; set; }
      public SatelliteVelocity SatVelECI { get; set; }

      [Browsable(false)]
      public SatelliteLLH SatLLH { get; set; }


      //[Browsable(false)]
      //public SatelliteEFG SatPosEFG { get; set; }

      public SatellitePosition SenPosECI { get; set; }
      public SatelliteVelocity SenVelECI { get; set; }

      [Browsable(false)]
      public SatelliteLLH SenLLH { get; set; }


      public int PassNum { get; set; }

      public LookAngle()
      {
      }

      public LookAngle(SenSatData senSatData, double currDs50TAI)
      {
         double[] xa_lv = new double[128];

         ErrCode = LamodWrapper.LamodComputeLookView(senSatData.SenSatKey, currDs50TAI, xa_lv);

         if (ErrCode != 0)
         {
            return;
         }

         SatNum = senSatData.SatNum;
         SatKey = senSatData.SatKey;
         SenNum = senSatData.SenNum;

         PopulateLookAngle(xa_lv, SatNum, SatKey, SenNum);
      }

      public LookAngle(int satNum, long satKey, int senNum, double[] xa_lv)
      {
         PopulateLookAngle(xa_lv, satNum, satKey, senNum);
      }



      private void PopulateLookAngle(double[] xa_lv, int satNum, long satKey, int senNum)
      {
         SatNum = satNum;
         SatKey = satKey;
         SenNum = senNum;

         double[] satPos = new double[3];
         double[] satVel = new double[3];
         double[] satLLH = new double[3];
         double[] senPos = new double[3];
         double[] senVel = new double[3];
         double[] senLLH = new double[3];

         ErrCode = 0;

         byte[] byteArrValue = new byte[DllMainWrapper.GETSETSTRLEN];

         Array.Copy(xa_lv, LamodWrapper.XA_LV_SATPOSX, satPos, 0, 3);
         Array.Copy(xa_lv, LamodWrapper.XA_LV_SATVELX, satVel, 0, 3);
         Array.Copy(xa_lv, LamodWrapper.XA_LV_SATLAT, satLLH, 0, 3);

         Array.Copy(xa_lv, LamodWrapper.XA_LV_SENPOSX, senPos, 0, 3);
         Array.Copy(xa_lv, LamodWrapper.XA_LV_SENVELX, senVel, 0, 3);
         Array.Copy(xa_lv, LamodWrapper.XA_LV_SENLAT, senLLH, 0, 3);

         UVec = new double[3];
         LVec = new double[3];
         ZVec = new double[3];
         UnitRangeVec = new double[3];
         RangeVec = new double[3];
         RngRateVec = new double[3];
         SenPosEFG = new double[3];
         SunVec = new double[3];
         MoonVec = new double[3];
         LimChkFlgs = new double[12];

         Array.Copy(xa_lv, LamodWrapper.XA_LV_UVECX, UVec, 0, 3);
         Array.Copy(xa_lv, LamodWrapper.XA_LV_LVECX, LVec, 0, 3);
         Array.Copy(xa_lv, LamodWrapper.XA_LV_ZVECX, ZVec, 0, 3);
         Array.Copy(xa_lv, LamodWrapper.XA_LV_RUVECX, UnitRangeVec, 0, 3);
         Array.Copy(xa_lv, LamodWrapper.XA_LV_RNGVECX, RangeVec, 0, 3);
         Array.Copy(xa_lv, LamodWrapper.XA_LV_RRTVECX, RngRateVec, 0, 3);

         Array.Copy(xa_lv, LamodWrapper.XA_LV_SENPOSE, SenPosEFG, 0, 3);
         Array.Copy(xa_lv, LamodWrapper.XA_LV_USUNX, SunVec, 0, 3);
         Array.Copy(xa_lv, LamodWrapper.XA_LV_UMOONX, MoonVec, 0, 3);

         Array.Copy(xa_lv, LamodWrapper.XA_LV_AZLIM, LimChkFlgs, 0, 12);


         this.SatPosECI = new SatellitePosition(satPos);
         this.SatVelECI = new SatelliteVelocity(satVel);
         this.SatLLH    = new SatelliteLLH(satLLH);
         this.SenPosECI = new SatellitePosition(senPos);
         this.SenVelECI = new SatelliteVelocity(senVel);
         this.SenLLH    = new SatelliteLLH(senLLH);

         LookCode = (int)xa_lv[LamodWrapper.XA_LV_LOOKCODE];
         LookTimeDs50UTC = xa_lv[LamodWrapper.XA_LV_DS50UTC];
         LookTimeMSE = xa_lv[LamodWrapper.XA_LV_MSE];
         Azimuth_deg = xa_lv[LamodWrapper.XA_LV_AZIM];
         Elevation_deg = xa_lv[LamodWrapper.XA_LV_ELEV];
         Range_km = xa_lv[LamodWrapper.XA_LV_RNG];
         RangeRate_kmps = xa_lv[LamodWrapper.XA_LV_RNGRT];
         AzRate_degps = xa_lv[LamodWrapper.XA_LV_AZRATE];
         ElRate_degps = xa_lv[LamodWrapper.XA_LV_ELRATE];

         double ra, decl;
         int raHr, raMin, decliDeg, decliMin;
         double raSec, decliSec;

         RelVel = xa_lv[LamodWrapper.XA_LV_RELVEL];
         OffBoresightAngle = xa_lv[LamodWrapper.XA_LV_OBSANGLE];
         AngleToFan = xa_lv[LamodWrapper.XA_LV_ANG2FAN];


         PassVisual = xa_lv[LamodWrapper.XA_LV_OPTVIS] == 1 ? true : false;
         PassAzLim = xa_lv[LamodWrapper.XA_LV_AZLIM] == 1 ? true : false;
         PassElLim = xa_lv[LamodWrapper.XA_LV_ELLIM] == 1 ? true : false;
         PassEarthBkgr = xa_lv[LamodWrapper.XA_LV_EARTHBK] == 1 ? true : false;
         PassEarthOb = xa_lv[LamodWrapper.XA_LV_EARTHOB] == 1 ? true : false;
         PassLunExcl = xa_lv[LamodWrapper.XA_LV_LUNEXCL] == 1 ? true : false;
         PassMaxRange = xa_lv[LamodWrapper.XA_LV_MAXRANGE] == 1 ? true : false;
         PassMinRange = xa_lv[LamodWrapper.XA_LV_MINRANGE] == 1 ? true : false;
         PassPenEclip = xa_lv[LamodWrapper.XA_LV_PENECLIP] == 1 ? true : false;
         PassRVelLim = xa_lv[LamodWrapper.XA_LV_RVELLIM] == 1 ? true : false;
         PassSAALim = xa_lv[LamodWrapper.XA_LV_SAALIM] == 1 ? true : false;
         PassSolExcl = xa_lv[LamodWrapper.XA_LV_SOLEXCL] == 1 ? true : false;
         PassUmbEclip = xa_lv[LamodWrapper.XA_LV_UMBECLIP] == 1 ? true : false;


         SunSenSat_deg = xa_lv[LamodWrapper.XA_LV_SUSESA];
         SunEarthSat_deg = xa_lv[LamodWrapper.XA_LV_SUEASA];
         SunEarthSen_deg = xa_lv[LamodWrapper.XA_LV_SUEASE];
         MoonSenSat_deg = xa_lv[LamodWrapper.XA_LV_MOSESA];
         MoonEarthSat_deg = xa_lv[LamodWrapper.XA_LV_MOEASA];
         MoonEarthSen_deg = xa_lv[LamodWrapper.XA_LV_MOEASE];
         EaSenSat_deg = xa_lv[LamodWrapper.XA_LV_EASENSAT];
         EaSatSen_deg = xa_lv[LamodWrapper.XA_LV_EASATSEN];
         EaSenLimb_deg = xa_lv[LamodWrapper.XA_LV_EASENLIMB];



         // DHN 28Dec17 - wrong index for solar aspect angle, it should be xa_lv_
         //SAA_deg = xa_lv[LamodWrapper.XA_ANGLE_SOLARASPECT];
         SAA_deg = xa_lv[LamodWrapper.XA_LV_SOLAA];


         LamodWrapper.LamodCompRaDec(UnitRangeVec, out ra, out decl, out raHr, out raMin, out raSec, out decliDeg, out decliMin, out decliSec);
         RA_deg = ra;
         Declination_degDec = decl;
         RAHr = raHr;
         RAMin = raMin;
         RASec = raSec;
         DecliDeg = decliDeg;
         DecliMin = decliMin;
         DecliSec = decliSec;

         Illumination = Convert.ToChar((int)xa_lv[LamodWrapper.XA_LV_VIS]);

         NBore1 = (int)xa_lv[LamodWrapper.XA_LV_NBORE1];
         NBore2 = (int)xa_lv[LamodWrapper.XA_LV_NBORE2];
         OffBoreElev1_deg = xa_lv[LamodWrapper.XA_LV_OBEL1];
         OffBoreAzim1_deg = xa_lv[LamodWrapper.XA_LV_OBAZ1];
         OffBoreElev2_deg = xa_lv[LamodWrapper.XA_LV_OBEL2];
         OffBoreAzim2_deg = xa_lv[LamodWrapper.XA_LV_OBAZ1];
      }

      public Obs ToObs(char obsType)
      {
         Obs obs = new Obs();

         obs.SiteTag = this.SatNum;
         obs.SenNum = this.SenNum;
         obs.ObsType = obsType;
         obs.ObsTimeDs50UTC = this.LookTimeDs50UTC;

         if (obsType == '1' || obsType == '2' || obsType == '3' || obsType == '4' || obsType == '8' || obsType == 'M')
         {
            obs.ElOrDec = this.Elevation_deg;
            obs.AzOrRA = this.Azimuth_deg;
         }
         else if (obsType == '5' || obsType == '9' || obsType == 'O')
         {
            obs.ElOrDec = this.Declination_degDec;
            obs.AzOrRA = this.RA_deg;
         }
         else
         {
            obs.ElOrDec = 0;
            obs.AzOrRA = 0;
         }

         obs.Elevation_deg = this.Elevation_deg;
         obs.Declination_deg = this.Declination_degDec;
         obs.Azimuth_deg = this.Azimuth_deg;
         obs.RA_deg = this.RA_deg;

         if (obsType == '2' || obsType == '3' || obsType == '4' || obsType == '6' || obsType == 'M' || obsType == 'O')
            obs.Range_km = this.Range_km;

         obs.RangeRate_kmps = this.RangeRate_kmps;
         obs.ElRate_degps = 0;
         obs.AzRate_degps = 0;
         obs.RangeAccel_kmps2 = 0;
         obs.ObsType = obsType;
         if (obsType == '9')
         {
            obs.Pos[0] = this.SenPosEFG[0];
            obs.Pos[1] = this.SenPosEFG[1];
            obs.Pos[2] = this.SenPosEFG[2];
         }


         return obs;
      }
   }
}
