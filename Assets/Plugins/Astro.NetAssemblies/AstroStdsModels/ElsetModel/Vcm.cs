using AFSPC.AstroStds.Models.SatelliteModel;
using AFSPC.AstroStds.Utilities;
using AFSPC.AstroStds.Wrappers;
using System;

namespace AFSPC.AstroStds.Models.ElsetModel
{
   [Serializable]
   /// <summary>
   /// Class <c>VCM</c> models a Vector Covariance Message (VCM).
   /// </summary>
   public class Vcm
   {
      /// <summary>
      /// <value>Property <c>ErrCode</c> represents the current error status.</value>
      /// </summary>
      public int ErrCode { get; set; }
      public int SatNum { get; set; }
      public DaysSince50UTC EpochTimeDs50UTC { get; set; }
      public int RevNum { get; set; }

      public SatellitePosition J2KSatPos { get; set; }
      public SatelliteVelocity J2KSatVel { get; set; }
      public SatellitePosition ECISatPos { get; set; }
      public SatelliteVelocity ECISatVel { get; set; }
      public SatellitePosition EFGSatPos { get; set; }
      public SatelliteVelocity EFGSatVel { get; set; }
      public int GeoZonals { get; set; }
      public int GeoTesserals { get; set; }

      public double BTerm { get; set; } // (m^2/kg)
      public double BDot { get; set; } // (m^2/kg-s)
      public double Agom { get; set; } // (m^2/km)
      public double EDR { get; set; } // (w/kg)
      public double OgParm { get; set; }
      public double CMOffset { get; set; }
      public int F10 { get; set; }
      public int F10Avg { get; set; }
      public double ApAvg { get; set; }
      public double TaiMUtc { get; set; }
      public double Ut1MUtc { get; set; }
      public double Ut1Rate { get; set; }
      public double PolarX { get; set; }
      public double PolarY { get; set; }
      public int NutTerms { get; set; }
      public double LeapDs50UTC { get; set; }
      public double InitStepSize { get; set; }
      public double ErrCtrl { get; set; }
      public SatellitePosition UVWPosSigmas { get; set; }
      public SatelliteVelocity UVWVelSigmas { get; set; }
      public int CovMtxSize { get; set; }

      public double RMS { get; set; }

      public string SatName { get; set; }
      public string CommName { get; set; }
      public string GeoName { get; set; }
      public string DragMode { get; set; }
      public string LunarSolarFlag { get; set; }
      public string RadPressFlag { get; set; }
      public string EarthTidesFlag { get; set; }
      public string IntrackThrustFlag { get; set; }
      public string IntegMode { get; set; }
      public string CoordSys { get; set; }
      public string Partials { get; set; }
      public string StepMode { get; set; }
      public string FixedStep { get; set; }
      public string StepSelection { get; set; }
      public string Vcm1Line { get; set; }
      public string VcmLines { get; set; }
      public double[] CovMtx1D { get; set; }

      public Vcm()
      {
      }

      public Vcm(string vcmString)
      {
         byte[] xs_vcm = new byte[512];
         double[] xa_vcm = new double[512];

         this.ErrCode = VcmWrapper.VcmStringToArray(vcmString, xa_vcm, xs_vcm);
         if (this.ErrCode == 0)
            PopulateDataFrArray(xa_vcm, Utility.BytesToString(xs_vcm));
      }

      public Vcm(byte[] bVcmString)
      {
         byte[] xs_vcm = new byte[512];
         double[] xa_vcm = new double[512];

         this.ErrCode = VcmWrapper.VcmStringToArray(Utility.BytesToString(bVcmString), xa_vcm, xs_vcm);
         if (this.ErrCode == 0)
            PopulateDataFrArray(xa_vcm, Utility.BytesToString(xs_vcm));
      }

      public Vcm(long satKey)
      {
         double[] xa_vcm = new double[512];
         byte[] xs_vcm = new byte[512];

         this.ErrCode = VcmWrapper.VcmRetrieveAllData(satKey, xs_vcm, xa_vcm);
         if (this.ErrCode != 0)
            throw new KeyNotFoundException("VcmWrapper.VcmRetrieveAllData", satKey);

         PopulateDataFrArray(xa_vcm, Utility.BytesToString(xs_vcm));

      }

      public Vcm(double[] xa_vcm, byte[] xs_vcm)
      {
         string s_tle = Utility.BytesToString(xs_vcm);

         PopulateDataFrArray(xa_vcm, s_tle);
      }

      public Vcm(double[] xa_vcm, string s_vcm)
      {
         PopulateDataFrArray(xa_vcm, s_vcm);
      }


      private void PopulateDataFrArray(double[] xa_vcm, string xs_vcm)
      {
         this.SatNum = (int)xa_vcm[VcmWrapper.XA_VCM_SATNUM];
         this.EpochTimeDs50UTC = new DaysSince50UTC(xa_vcm[VcmWrapper.XA_VCM_EPOCHDS50UTC]);
         this.RevNum = (int)xa_vcm[VcmWrapper.XA_VCM_REVNUM];
         this.J2KSatPos = new SatellitePosition(new double[] { xa_vcm[VcmWrapper.XA_VCM_J2KPOSX], xa_vcm[VcmWrapper.XA_VCM_J2KPOSY], xa_vcm[VcmWrapper.XA_VCM_J2KPOSZ] });
         this.J2KSatVel = new SatelliteVelocity(new double[] { xa_vcm[VcmWrapper.XA_VCM_J2KVELX], xa_vcm[VcmWrapper.XA_VCM_J2KVELY], xa_vcm[VcmWrapper.XA_VCM_J2KVELZ] });
         this.ECISatPos = new SatellitePosition(new double[] { xa_vcm[VcmWrapper.XA_VCM_ECIPOSX], xa_vcm[VcmWrapper.XA_VCM_ECIPOSY], xa_vcm[VcmWrapper.XA_VCM_ECIPOSZ] });
         this.ECISatVel = new SatelliteVelocity(new double[] { xa_vcm[VcmWrapper.XA_VCM_ECIVELX], xa_vcm[VcmWrapper.XA_VCM_ECIVELY], xa_vcm[VcmWrapper.XA_VCM_ECIVELZ] });
         this.EFGSatPos = new SatellitePosition(new double[] { xa_vcm[VcmWrapper.XA_VCM_EFGPOSX], xa_vcm[VcmWrapper.XA_VCM_EFGPOSY], xa_vcm[VcmWrapper.XA_VCM_EFGPOSZ] });
         this.EFGSatVel = new SatelliteVelocity(new double[] { xa_vcm[VcmWrapper.XA_VCM_EFGVELX], xa_vcm[VcmWrapper.XA_VCM_EFGVELY], xa_vcm[VcmWrapper.XA_VCM_EFGVELZ] });


         this.GeoZonals = (int)xa_vcm[VcmWrapper.XA_VCM_GEOZON];
         this.GeoTesserals = (int)xa_vcm[VcmWrapper.XA_VCM_GEOTES];
         this.BTerm = xa_vcm[VcmWrapper.XA_VCM_BTERM];
         this.BDot = xa_vcm[VcmWrapper.XA_VCM_BDOT];
         this.Agom = xa_vcm[VcmWrapper.XA_VCM_AGOM];
         this.EDR = xa_vcm[VcmWrapper.XA_VCM_EDR];
         this.OgParm = xa_vcm[VcmWrapper.XA_VCM_OGPARM];
         this.CMOffset = xa_vcm[VcmWrapper.XA_VCM_CMOFFSET];
         this.F10 = (int)xa_vcm[VcmWrapper.XA_VCM_F10];
         this.F10Avg = (int)xa_vcm[VcmWrapper.XA_VCM_F10AVG];
         this.ApAvg = xa_vcm[VcmWrapper.XA_VCM_APAVG];
         this.TaiMUtc = xa_vcm[VcmWrapper.XA_VCM_TAIMUTC];
         this.Ut1MUtc = xa_vcm[VcmWrapper.XA_VCM_UT1MUTC];
         this.Ut1Rate = xa_vcm[VcmWrapper.XA_VCM_UT1RATE];
         this.PolarX = xa_vcm[VcmWrapper.XA_VCM_POLMOTX];
         this.PolarY = xa_vcm[VcmWrapper.XA_VCM_POLMOTY];
         this.NutTerms = (int)xa_vcm[VcmWrapper.XA_VCM_NUTTERMS];
         this.LeapDs50UTC = new DaysSince50UTC(xa_vcm[VcmWrapper.XA_VCM_LEAPDS50UTC]);
         this.InitStepSize = xa_vcm[VcmWrapper.XA_VCM_INITSTEP];
         this.ErrCtrl = xa_vcm[VcmWrapper.XA_VCM_ERRCTRL];
         this.UVWPosSigmas = new SatellitePosition(new double[] { xa_vcm[VcmWrapper.XA_VCM_POSUSIG], xa_vcm[VcmWrapper.XA_VCM_POSVSIG], xa_vcm[VcmWrapper.XA_VCM_POSWSIG] });
         this.UVWVelSigmas = new SatelliteVelocity(new double[] { xa_vcm[VcmWrapper.XA_VCM_VELUSIG], xa_vcm[VcmWrapper.XA_VCM_VELVSIG], xa_vcm[VcmWrapper.XA_VCM_VELWSIG] });
         this.CovMtxSize = (int)xa_vcm[VcmWrapper.XA_VCM_COVMTXSIZE];
         this.RMS = xa_vcm[VcmWrapper.XA_VCM_RMS];

         if (this.CovMtxSize > 0)
         {

            this.CovMtx1D = new double[45];
            int j = 0;
            for (int i = VcmWrapper.XA_VCM_COVELEMS; i < VcmWrapper.XA_VCM_COVELEMS + 45; i++)
               this.CovMtx1D[j++] = xa_vcm[i];
         }

         this.SatName = xs_vcm.Substring(VcmWrapper.XS_VCM_SATNAME_0_8, 8).Trim();
         this.CommName = xs_vcm.Substring(VcmWrapper.XS_VCM_COMMNAME_8_25, 25).Trim();
         this.GeoName = xs_vcm.Substring(VcmWrapper.XS_VCM_GEONAME_33_6, 6).Trim();
         this.DragMode = xs_vcm.Substring(VcmWrapper.XS_VCM_DRAGMOD_39_12, 12).Trim();
         this.LunarSolarFlag = xs_vcm.Substring(VcmWrapper.XS_VCM_LUNAR_51_3, 3).Trim();
         this.RadPressFlag = xs_vcm.Substring(VcmWrapper.XS_VCM_RADPRESS_54_3, 3).Trim();
         this.EarthTidesFlag = xs_vcm.Substring(VcmWrapper.XS_VCM_EARTHTIDES_57_3, 3).Trim();
         this.IntrackThrustFlag = xs_vcm.Substring(VcmWrapper.XS_VCM_INTRACK_60_3, 3).Trim();
         this.IntegMode = xs_vcm.Substring(VcmWrapper.XS_VCM_INTEGMODE_63_6, 6).Trim();
         this.CoordSys = xs_vcm.Substring(VcmWrapper.XS_VCM_COORDSYS_69_5, 5).Trim();
         this.Partials = xs_vcm.Substring(VcmWrapper.XS_VCM_PARTIALS_74_8, 8).Trim();
         this.StepMode = xs_vcm.Substring(VcmWrapper.XS_VCM_STEPMODE_82_4, 4).Trim();
         this.FixedStep = xs_vcm.Substring(VcmWrapper.XS_VCM_FIXEDSTEP_86_3, 3).Trim();
         this.StepSelection = xs_vcm.Substring(VcmWrapper.XS_VCM_STEPSEL_89_6, 6).Trim();

         byte[] vcmLines = new byte[4000]; // concatenated string
         VcmWrapper.VcmArrayToVcmLines(xa_vcm, xs_vcm, vcmLines);
         this.VcmLines = Utility.BytesToString(vcmLines);

         byte[] vcm1Line = new byte[1500];
         VcmWrapper.VcmArrayToVcm1Line(xa_vcm, xs_vcm, vcm1Line);
         this.Vcm1Line = Utility.BytesToString(vcm1Line);
      }

      public void ToSpVec(out double[] xa_spVec, out string s_spVec)
      {
         xa_spVec = new double[512];

         xa_spVec[SpVecWrapper.XA_SPVEC_AGOM] = this.Agom;
         xa_spVec[SpVecWrapper.XA_SPVEC_AP] = this.ApAvg;
         xa_spVec[SpVecWrapper.XA_SPVEC_BTERM] = this.BTerm;
         xa_spVec[SpVecWrapper.XA_SPVEC_BULGEFLG] = this.GeoName == "2-BODY" ? 0 : 1;
         xa_spVec[SpVecWrapper.XA_SPVEC_CONVERG] = this.ErrCtrl;
         Array.Copy(this.CovMtx1D, 0, xa_spVec, SpVecWrapper.XA_SPVEC_COVELEMS, this.CovMtx1D.Length);
         xa_spVec[SpVecWrapper.XA_SPVEC_DRAGFLG] = this.DragMode.Trim() == "NONE" ? 0 : 1;
         xa_spVec[SpVecWrapper.XA_SPVEC_ELSETNUM] = 0; // vcm doesn't have elset number
         xa_spVec[SpVecWrapper.XA_SPVEC_EPOCH] = this.EpochTimeDs50UTC;
         xa_spVec[SpVecWrapper.XA_SPVEC_F10] = this.F10;
         xa_spVec[SpVecWrapper.XA_SPVEC_F10AVG] = this.F10Avg;
         xa_spVec[SpVecWrapper.XA_SPVEC_GEOIDX] = 71; // EGM-96 70th order (thru 70,70)
         xa_spVec[SpVecWrapper.XA_SPVEC_HASOWNCRL] = 1;
         xa_spVec[SpVecWrapper.XA_SPVEC_INITSTEP] = this.InitStepSize;
         xa_spVec[SpVecWrapper.XA_SPVEC_INPCOORD] = SpVecWrapper.SPVEC_INPCOORD_TMDAT;

         if (this.StepMode == "AUTO")
            xa_spVec[SpVecWrapper.XA_SPVEC_INTEGCTRL] = VcmWrapper.STEPMODE_AUTO;
         else if (this.StepMode == "TIME")
            xa_spVec[SpVecWrapper.XA_SPVEC_INTEGCTRL] = VcmWrapper.STEPMODE_TIME;
         else if (this.StepMode == "S")
            xa_spVec[SpVecWrapper.XA_SPVEC_INTEGCTRL] = VcmWrapper.STEPMODE_S;

         xa_spVec[SpVecWrapper.XA_SPVEC_LUNSOLFLG] = this.LunarSolarFlag == "ON" ? 1 : 0;
         xa_spVec[SpVecWrapper.XA_SPVEC_NTERMS] = this.NutTerms;
         xa_spVec[SpVecWrapper.XA_SPVEC_OGFLG] = this.OgParm == 0 ? 0 : 1;
         xa_spVec[SpVecWrapper.XA_SPVEC_OGPARM] = this.OgParm;
         xa_spVec[SpVecWrapper.XA_SPVEC_POS1] = this.ECISatPos.X_km;
         xa_spVec[SpVecWrapper.XA_SPVEC_POS2] = this.ECISatPos.Y_km;
         xa_spVec[SpVecWrapper.XA_SPVEC_POS3] = this.ECISatPos.Z_km;
         xa_spVec[SpVecWrapper.XA_SPVEC_RADFLG] = this.Agom == 0 ? 0 : 1;
         xa_spVec[SpVecWrapper.XA_SPVEC_REEVAL] = 0; // not used
         xa_spVec[SpVecWrapper.XA_SPVEC_REVNUM] = this.RevNum;
         xa_spVec[SpVecWrapper.XA_SPVEC_RMS] = this.RMS;
         xa_spVec[SpVecWrapper.XA_SPVEC_SATNUM] = this.SatNum;
         xa_spVec[SpVecWrapper.XA_SPVEC_TIDESFLG] = this.EarthTidesFlag == "ON" ? 1 : 0;
         xa_spVec[SpVecWrapper.XA_SPVEC_TRUNC] = this.GeoTesserals;
         xa_spVec[SpVecWrapper.XA_SPVEC_VARSTEP] = this.FixedStep == "ON" ? 1 : 0;
         xa_spVec[SpVecWrapper.XA_SPVEC_VEL1] = this.ECISatVel.XD_kmps * 1000;
         xa_spVec[SpVecWrapper.XA_SPVEC_VEL2] = this.ECISatVel.YD_kmps * 1000;
         xa_spVec[SpVecWrapper.XA_SPVEC_VEL3] = this.ECISatVel.ZD_kmps * 1000;

         s_spVec = string.Format("{0}{1}{2}", 'U', this.SatName.Substring(0, Math.Min(8, this.SatName.Length)), "                                     ");
      }
   }
}
