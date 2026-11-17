using AFSPC.AstroStds.Wrappers;
using System.ComponentModel;
using AFSPC.AstroStds.Utilities;
using System;

namespace AFSPC.AstroStds.Models.SatelliteModel
{
   [Serializable]
   public class EphemDeltas
   {
      [Browsable(false)]
      public int ErrCode { get; set; }

      public int PriSatNum { get; set; }
      public int SecSatNum { get; set; }

      // X-Axis data fields
      [Browsable(false)]
      public double XDate { get; set; }
      public double Mse { get; set; }
      public double TrueAnomaly_deg { get; set; }
      public double ArgOfLatitude_deg { get; set; }


      [Browsable(false)]
      public DaysSince50UTC EphemTimeDs50UTC { get; set; }     // Ephemeris time in days since 1950 UTC

      public string DDD_ddd { get; set; }
      public string YYDDDHHMMSS { get; set; }
      public string DTGForm2 { get; set; }

      public double Pos_km { get; set; }
      public double Time_sec { get; set; }
      public double PosRadial_km { get; set; }
      public double PosIntrack_km { get; set; }
      public double PosCrossTrack_km { get; set; }
      public double Vel_kmps { get; set; }
      public double VelRadial_kmps { get; set; }
      public double VelIntrack_kmps { get; set; }
      public double VelCrossTrack_kmps { get; set; }
      public double Beta_deg { get; set; }
      public double Height_km { get; set; }
      public double AngMom_deg { get; set; } // delta angular momentum (deg)
      public double MhlnbsUVW { get; set; }
      public double MhlnbsHTB { get; set; }




      public EphemDeltas()
      {
      }


      // Note: deltas = secondary - primary
      public EphemDeltas(long priSatKey, long secSatKey, double currDs50UTC, bool isInUVW)
      {
         double[] deltas = new double[100];

         ErrCode = SatStateWrapper.SatStateEphCom(priSatKey, secSatKey, currDs50UTC, isInUVW ? 1 : 0, deltas);
         if (ErrCode != 0) return;

         PriSatNum = ElOpsWrapper.SatNumOf(priSatKey);
         SecSatNum = ElOpsWrapper.SatNumOf(secSatKey);

         EphemTimeDs50UTC = currDs50UTC;
         DDD_ddd = EphemTimeDs50UTC.Dtg17Str.Substring(5); //TimeFuncWrapper.UTCToDtg17Str(EphemTimeDs50UTC).Substring(5);
         YYDDDHHMMSS = EphemTimeDs50UTC.Dtg23Str; // TimeFuncWrapper.UTCToDtg23Str(EphemTimeDs50UTC);
         DTGForm2 = EphemTimeDs50UTC.ToDTGForm2(); //

         PopulateDeltas(deltas);
         
      }

      //// Note: deltas = secondary - primary
      //public EphemDeltas(long priSatKey, long secSatKey, double currDs50UTC, bool isInUVW, double[] refPos, double[] refVel, double xDate, double mse)
      //{
      //   double[] deltas = new double[100];

      //   ErrCode = SatStateWrapper.SatStateEphCom(priSatKey, secSatKey, currDs50UTC, isInUVW ? 1 : 0, deltas);
      //   if (ErrCode != 0) return;

      //   PriSatNum = ElOpsWrapper.SatNumOf(priSatKey);
      //   SecSatNum = ElOpsWrapper.SatNumOf(secSatKey);

      //   EphemTimeDs50UTC = currDs50UTC;
      //   DDD_ddd = EphemTimeDs50UTC.Dtg17Str.Substring(5); //TimeFuncWrapper.UTCToDtg17Str(EphemTimeDs50UTC).Substring(5);
      //   YYDDDHHMMSS = EphemTimeDs50UTC.Dtg23Str; // TimeFuncWrapper.UTCToDtg23Str(EphemTimeDs50UTC);
      //   DTGForm2 = EphemTimeDs50UTC.ToDTGForm2(); //


      //   PopulateDeltas(deltas);

      //   // X-Axis data fields
      //   // Convert to Keplerian elements
      //   double[] refKep = new double[6];
      //   AstroFuncWrapper.PosVelToKep(refPos, refVel, refKep);

      //   TrueAnomaly_deg = AstroFuncWrapper.CompTrueAnomaly(refKep) % 360;
      //   ArgOfLatitude_deg = (TrueAnomaly_deg + refKep[AstroFuncWrapper.XA_KEP_OMEGA]) % 360;
      //   XDate = xDate;
      //   Mse = mse;
      //}

      //public EphemDeltas(double currDs50UTC, double[] refPos, double[] refVel, double xDate, double mse)
      //{
      //   EphemTimeDs50UTC = currDs50UTC;

      //   // X-Axis data fields
      //   // Convert to Keplerian elements
      //   double[] refKep = new double[6];
      //   AstroFuncWrapper.PosVelToKep(refPos, refVel, refKep);

      //   TrueAnomaly_deg = AstroFuncWrapper.CompTrueAnomaly(refKep) % 360;
      //   ArgOfLatitude_deg = (TrueAnomaly_deg + refKep[AstroFuncWrapper.XA_KEP_OMEGA]) % 360;
      //   XDate = xDate;
      //   Mse = mse;
      //}

      private void PopulateDeltas(double[] deltas)
      {
         Pos_km = deltas[SatStateWrapper.XA_DELTA_POS];
         Time_sec = deltas[SatStateWrapper.XA_DELTA_TIME];
         PosRadial_km = deltas[SatStateWrapper.XA_DELTA_PRADIAL];
         PosIntrack_km = deltas[SatStateWrapper.XA_DELTA_PINTRCK];
         PosCrossTrack_km = deltas[SatStateWrapper.XA_DELTA_PCRSSTRCK];
         Vel_kmps = deltas[SatStateWrapper.XA_DELTA_VEL];
         VelRadial_kmps = deltas[SatStateWrapper.XA_DELTA_VRADIAL];
         VelIntrack_kmps = deltas[SatStateWrapper.XA_DELTA_VINTRCK];
         VelCrossTrack_kmps = deltas[SatStateWrapper.XA_DELTA_VCRSSTRCK];
         Beta_deg = deltas[SatStateWrapper.XA_DELTA_BETA];
         Height_km = deltas[SatStateWrapper.XA_DELTA_HEIGHT];
         AngMom_deg = deltas[SatStateWrapper.XA_DELTA_ANGMOM];
         MhlnbsUVW = deltas[SatStateWrapper.XA_DELTA_MHLNBS_UVW];
         MhlnbsHTB = deltas[SatStateWrapper.XA_DELTA_MHLNBS_HTB];
      }
   }
}
