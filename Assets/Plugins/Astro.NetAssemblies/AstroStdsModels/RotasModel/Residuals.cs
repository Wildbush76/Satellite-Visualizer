using AFSPC.AstroStds.Wrappers;
using AFSPC.AstroStds.Utilities;
using AFSPC.AstroStds.Models.ObsModel;
using System.ComponentModel;
using AFSPC.AstroStds.Models.SaasModel;
using System;
using System.Globalization;
using System.Collections.Generic;

namespace AFSPC.AstroStds.Models.RotasModel
{
   [Serializable]
   public class Residuals
   {
      [Browsable(false)]
      public int ErrCode;

      public int SatNum { get; set; }                 // Satellite number (pull from elset)
      public int CaveNetSatNum { get; set; }          // Cavenet Satellite number (pull from obs)
      public double AzDelta_deg { get; set; }         // Azimuth residual (deg)
      public double ElDelta_deg { get; set; }         // Elevation residual (deg)
      public double RADelta_deg { get; set; }         // right ascension residual (deg)
      public double DecDelta_deg { get; set; }        // declination residaul (deg)
      public double RangeDelta_km { get; set; }       // Range residual (km)
      public double RngRtDelta_kmps { get; set; }     // Range rate residual (km/sec)
      public double HeightDelta_km { get; set; }          // Height residual (km)
      public double BetaDelta_deg { get; set; }           // Beta residual (deg)
      public double AngMomDelta_deg { get; set; }    // Angular momentum residual (deg)
      public double DeltaT_min { get; set; }         // Delta T residual (min)
      public double VMag_km { get; set; }            // Vector magnitude (km)
      public double Age_days { get; set; }                // Time since epoch (days)
      public double RevNum { get; set; }             // Revolution number  
      public double LonDelta_deg { get; set; }
      public double PosUDelta_km { get; set; }        //Position U residual (km) radial vector
      public double PosVDelta_km { get; set; }        //Position V residual (km) completes right-hand coordinate system
      public double PosWDelta_km { get; set; }        //Position U residual (km) vecotr perpendicular to orbital plane
      public double ChiDelta { get; set; }
      public double TdoaDelta_ns { get; set; }           //Tdoa residual (nano-second)

      public double FdoaDelta_Hz { get; set; }           //Fdoa residual (Hz)
      [Browsable(false)]
      public long SatKey { get; set; }

      [Browsable(false)]
      public long ObsKey { get; set; }

      public int SenNum { get; set; }
      public int SenNum2 { get; set; }


      // X-axis values
      [Browsable(false)]
      public DaysSince50UTC ObsTimeDs50UTC { get; set; }     // Observation time in days since 1950 UTC

      public string DDD_ddd { get; set; }
      public string YYDDDHHMMSS { get; set; }
      public string DTGForm2 { get; set; }

      public double SatArgOfLatitude_deg { get; set; }   // True argument of latitude (deg)
      public double SatTrueAnomaly_deg { get; set; }     // Satellite true anomaly (deg)
      public double SatElevation_deg { get; set; }       // Satellite elevation (deg)
      public double SatLon_deg { get; set; }             // Satellite geodetic longitude (deg)

      public double ObsAz_deg { get; set; }         // obs azimuth (deg)
      public double ObsEl_deg { get; set; }       // obs elevation (deg)
      public double ObsRA_deg { get; set; }         // obs azimuth (deg)
      public double ObsDec_deg { get; set; }       // obs elevation (deg)
      public double ObsRange_km { get; set; }            // obs range (km)
      public double ObsRngRt_kmps { get; set; }      // obs range rate (km/s)

      public double CompObsRange_km { get; set; }        // Computed obs range for angle only obs (km)



      public int ObsType { get; set; }               // Observation type
      public int SatCat { get; set; }                // Satellite maintenance category
      public int CompASTAT { get; set; }                 // Observation ASTAT

      [Browsable(false)]
      public double[] SatPosECI { get; set; }
      [Browsable(false)]
      public double[] SatVelECI { get; set; }
      [Browsable(false)]
      public double[] SatLLH { get; set; }
      [Browsable(false)]
      public double[] ObsPosECI { get; set; }
      [Browsable(false)]
      public double[] ObsVelECI { get; set; }
      [Browsable(false)]
      public double[] ObsLLH { get; set; }
      public int Id { get; set; }

      public bool Rejected { get; set; }

      public int IterationRejected { get; set; }

      public double? RaCorrectedResidual { get; set; }

      public double? DecCorrectedResidual { get; set; }

      //public double SolarPhaseAngle
      //{
      //	get;
      //	private set;
      //}



      public bool Error { get; set; }

      public List<string> ErrorMessages { get; set; }

      [Browsable(false)]
      public Obs Obs { get; set; }

      public Residuals()
      {

      }


      public Residuals(long obsKey, long satKey, bool obsIsTruth)
      {
         double[] residualArr = new double[100];
         double[] satElts = new double[9];
         double[] obsElts = new double[9];

         RotasParameters.Get1P();
         //this.ErrCode = RotasWrapper.RotasComputeObsResiduals(obsKey, satKey, residualArr, satElts, obsElts);
         this.ErrCode = RotasWrapper.RotasComputeObsResiduals_MT(RotasParameters.XA_RT_Parms, obsKey, satKey, residualArr, satElts, obsElts);
         if (this.ErrCode != 0)
         {
            return;
         }

         Obs = new Obs(obsKey);

         ObsKey = obsKey;
         SatKey = satKey;
         SenNum = Obs.SenNum; // ObsWrapper.SenNumFrObsKey(obsKey);
         SatNum = ElOpsWrapper.SatNumOf(satKey);
         CaveNetSatNum = Obs.SatNum;
         ObsRange_km = Obs.Range_km;
         ObsRngRt_kmps = Obs.RangeRate_kmps;
         SenNum2 = Obs.SenNum2;


         PopulateResiduals(residualArr, satElts, obsElts, obsIsTruth);
      }


      public Residuals(long[] obsKeys, long satKey, bool obsIsTruth)
      {
         RotasParameters.Get1P();
         double[] obsRes = new double[100];
         double[] satElts = new double[9];
         double[] obElts = new double[9];
         long[] trackObsKeys = new long[3];
         byte[] buf = new byte[512];
         this.ErrCode = RotasWrapper.RotasComputeTrackResiduals(obsKeys, 1, obsKeys.Length, satKey, obsRes, trackObsKeys, buf, satElts, obElts);
         if (this.ErrCode != 0)
         {
            return;
         }

         Obs = new Obs(obsKeys[1]);

         ObsKey = obsKeys[1];
         SatKey = satKey;
         SenNum = Obs.SenNum; // ObsWrapper.SenNumFrObsKey(obsKeys[1]);
         SatNum = ElOpsWrapper.SatNumOf(satKey);
         CaveNetSatNum = Obs.SatNum;
         ObsRange_km = Obs.Range_km;
         ObsRngRt_kmps = Obs.RangeRate_kmps;
         SenNum2 = Obs.SenNum2;

         PopulateResiduals(obsRes, satElts, obElts, obsIsTruth);
      }

      public Residuals(Obs obs, long satKey, bool obsIsTruth)
      {
         double[] residualArr = new double[100];
         double[] satElts = new double[9];
         double[] obsElts = new double[9];

         RotasParameters.Get1P();
         this.ErrCode = RotasWrapper.RotasComputeObsResiduals_MT(RotasParameters.XA_RT_Parms, obs.ObsKey, satKey, residualArr, satElts, obsElts);

         ObsKey = obs.ObsKey;
         SatKey = satKey;
         SenNum = obs.SenNum; // ObsWrapper.SenNumFrObsKey(obs.ObsKey);
         SatNum = ElOpsWrapper.SatNumOf(satKey);
         CaveNetSatNum = obs.SatNum;
         ObsRange_km = obs.Range_km;
         ObsRngRt_kmps = obs.RangeRate_kmps;
         SenNum2 = obs.SenNum2;


         PopulateResiduals(residualArr, satElts, obsElts, obsIsTruth);
      }

      public Residuals(double[] residualArr, double[] satElts, double[] obsElts, bool obsIsTruth)
      {
         PopulateResiduals(residualArr, satElts, obsElts, obsIsTruth);
      }

      private void PopulateResiduals(double[] residualArr, double[] satElts, double[] obsElts, bool obsIsTruth)
      {

         double sign;

         if (obsIsTruth)
            sign = 1.0;
         else
            sign = -1.0;

         ErrCode = 0;


         AzDelta_deg = sign * residualArr[RotasWrapper.XA_OBSRES_AZ];
         ElDelta_deg = sign * residualArr[RotasWrapper.XA_OBSRES_EL];
         RangeDelta_km = sign * residualArr[RotasWrapper.XA_OBSRES_RANGE];
         RngRtDelta_kmps = sign * residualArr[RotasWrapper.XA_OBSRES_RNGRATE];
         HeightDelta_km = sign * residualArr[RotasWrapper.XA_OBSRES_HEIGHT];
         BetaDelta_deg = sign * residualArr[RotasWrapper.XA_OBSRES_BETA];
         AngMomDelta_deg = sign * residualArr[RotasWrapper.XA_OBSRES_ANGMOM];
         DeltaT_min = sign * residualArr[RotasWrapper.XA_OBSRES_DELTAT];
         VMag_km = sign * residualArr[RotasWrapper.XA_OBSRES_VMAG];

         TdoaDelta_ns = sign * residualArr[RotasWrapper.XA_OBSRES_TDOA];
         FdoaDelta_Hz = sign * residualArr[RotasWrapper.XA_OBSRES_FDOA];

         PosUDelta_km = residualArr[RotasWrapper.XA_OBSRES_POSU];
         PosVDelta_km = residualArr[RotasWrapper.XA_OBSRES_POSV];
         PosWDelta_km = residualArr[RotasWrapper.XA_OBSRES_POSW];
         ChiDelta = residualArr[RotasWrapper.XA_OBSRES_CHI];

         Age_days = residualArr[RotasWrapper.XA_OBSRES_AGE];
         SatArgOfLatitude_deg = residualArr[RotasWrapper.XA_OBSRES_SU];
         RevNum = residualArr[RotasWrapper.XA_OBSRES_REVNUM];

         SatTrueAnomaly_deg = residualArr[RotasWrapper.XA_OBSRES_SATANOM];
         SatElevation_deg = residualArr[RotasWrapper.XA_OBSRES_SATELEV];
         ObsType = (int)residualArr[RotasWrapper.XA_OBSRES_OBSTYPE];
         CompASTAT = (int)residualArr[RotasWrapper.XA_OBSRES_ASTAT];
         SatCat = (int)residualArr[RotasWrapper.XA_OBSRES_SATCAT];
         SatLon_deg = satElts[6];
         ObsTimeDs50UTC = residualArr[RotasWrapper.XA_OBSRES_OBSTIME];
         DDD_ddd = ObsTimeDs50UTC.Dtg17Str.Substring(5);
         YYDDDHHMMSS = ObsTimeDs50UTC.Dtg23Str;
         DTGForm2 = ObsTimeDs50UTC.ToDTGForm2();
         ObsAz_deg = residualArr[RotasWrapper.XA_OBSRES_OBSAZ];
         ObsEl_deg = residualArr[RotasWrapper.XA_OBSRES_OBSEL];
         ObsRA_deg = residualArr[RotasWrapper.XA_OBSRES_OBSRA];
         ObsDec_deg = residualArr[RotasWrapper.XA_OBSRES_OBSDEC];
         CompObsRange_km = residualArr[RotasWrapper.XA_OBSRES_OBSRNG];

         RADelta_deg = residualArr[RotasWrapper.XA_OBSRES_RA];
         DecDelta_deg = residualArr[RotasWrapper.XA_OBSRES_DEC];
         LonDelta_deg = residualArr[RotasWrapper.XA_OBSRES_LON];

         SatPosECI = new double[3];
         SatVelECI = new double[3];
         SatLLH = new double[3];
         ObsPosECI = new double[3];
         ObsVelECI = new double[3];
         ObsLLH = new double[3];

         //      //Compute Solar Phase Angle
         //double[] xa_lv = new double[128];
         //long senSatKey = LamodWrapper.LamodInitSenSat(SenNum, SatKey);
         //LamodWrapper.LamodComputeLookView(senSatKey, ObsTimeDs50UTC, xa_lv);			
         //SolarPhaseAngle = xa_lv[LamodWrapper.XA_LV_SOLAA]; 

         Array.Copy(satElts, 0, SatPosECI, 0, 3);
         Array.Copy(satElts, 3, SatVelECI, 0, 3);
         Array.Copy(satElts, 6, SatLLH, 0, 3);

         Array.Copy(obsElts, 0, ObsPosECI, 0, 3);
         Array.Copy(obsElts, 3, ObsVelECI, 0, 3);
         Array.Copy(obsElts, 6, ObsLLH, 0, 3);
      }

   }
}
