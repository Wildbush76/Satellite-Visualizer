using AFSPC.AstroStds.Wrappers;
using AFSPC.AstroStds.Utilities;
using System;

namespace AFSPC.AstroStds.Models.ElsetModel
{
   [Serializable]
   public class Tle
   {
      public int ErrCode { get; set; }
      public string Line1 { get; set; }
      public string Line2 { get; set; }
      public int SatNum { get; set; }
      public char SecClass { get; set; }
      public string SatName { get; set; }
      public int EpochYear { get; set; }
      public double EpochDays { get; set; }
      public DaysSince50UTC EpochTimeDs50UTC { get; set; }
      public double NDotO2 { get; set; }
      public double N2DotO6 { get; set; }
      public double OgParm { get; set; }
      public double GP_Agom { get; set; }
      public double SP_Agom { get; set; }
      public double BStar { get; set; }
      public TleWrapper.TleTypeEnum TleType { get; set; }
      public int ElsetNum { get; set; }
      public double Inclination_deg { get; set; }
      public double Node_deg { get; set; }
      public double Eccentricity { get; set; }
      public double Omega_deg { get; set; }
      public double MeanAnomaly_deg { get; set; }
      public double MeanMotion_revpday { get; set; }
      public int RevNum { get; set; }

      //// CSV TLE fields
      public int OrgSatNum { get; set; }
      public string LongSatName { get; set; }
      public double BTerm { get; set; }
      public double ObsTime { get; set; }
      public double EGR { get; set; }
      public double EDR { get; set; }
      public double VisMag { get; set; }
      public double RCS { get; set; }

      public string ObjType { get; set; }

      public double A_km
      {
         get
         {
            return AstroFuncWrapper.NToA(MeanMotion_revpday);
         }

      }

      public Tle(string line1, string line2)
      {
         double[] xa_tle = new double[64];
         byte[] xs_tle = new byte[512];

         this.ErrCode = TleWrapper.TleLinesToArray(line1, line2, xa_tle, xs_tle);
         if (this.ErrCode == 0)
            PopulateDataFrArray(xa_tle, Utility.BytesToString(xs_tle));
      }

      public Tle(long satKey)
      {
         double[] xa_tle = new double[64];
         byte[] xs_tle = new byte[512];

         this.ErrCode = TleWrapper.TleDataToArray(satKey, xa_tle, xs_tle);
         if (this.ErrCode != 0)
            throw new KeyNotFoundException("TleWrapper.TleDataToArray", satKey);


         PopulateDataFrArray(xa_tle, Utility.BytesToString(xs_tle));

      }

      public Tle(double[] xa_tle, byte[] xs_tle)
      {
         string s_tle = Utility.BytesToString(xs_tle);

         PopulateDataFrArray(xa_tle, s_tle);
      }

      public Tle(double[] xa_tle, string s_tle)
      {
         PopulateDataFrArray(xa_tle, s_tle);
      }


      private void PopulateDataFrArray(double[] xa_tle, string xs_tle)
      {
         this.ErrCode = 0;
         this.SatNum = (int)(xa_tle[TleWrapper.XA_TLE_SATNUM]);
         this.SecClass = xs_tle[TleWrapper.XS_TLE_SECCLASS_1];
         this.SatName = xs_tle.Substring(TleWrapper.XS_TLE_SATNAME_1_12, 8);
         this.EpochTimeDs50UTC = xa_tle[TleWrapper.XA_TLE_EPOCH];
         this.EpochYear = EpochTimeDs50UTC.Year;
         this.EpochDays = EpochTimeDs50UTC.DayOfYearReal;

         if (this.TleType == TleWrapper.TleTypeEnum.TLETYPE_SP)
         {
            // GP stuff
            this.BStar = 0;
            this.NDotO2 = 0;
            this.N2DotO6 = 0;
            this.GP_Agom = 0;

            // SP stuff
            this.BTerm = xa_tle[TleWrapper.XA_TLE_SP_BTERM];
            this.SP_Agom = xa_tle[TleWrapper.XA_TLE_SP_AGOM];
            this.OgParm = xa_tle[TleWrapper.XA_TLE_SP_OGPARM];
         }
         else
         {
            // GP stuff
            this.BStar = xa_tle[TleWrapper.XA_TLE_BSTAR];
            this.BTerm = xa_tle[TleWrapper.XA_TLE_BTERM];
            this.GP_Agom = xa_tle[TleWrapper.XA_TLE_AGOMGP];
            this.NDotO2 = xa_tle[TleWrapper.XA_TLE_NDOT];
            this.N2DotO6 = xa_tle[TleWrapper.XA_TLE_NDOTDOT];
            this.BTerm = xa_tle[TleWrapper.XA_TLE_BTERM];

            // SP stuff
            this.SP_Agom = 0;
            this.OgParm = 0;
         }

         this.TleType = (TleWrapper.TleTypeEnum)xa_tle[TleWrapper.XA_TLE_EPHTYPE];
         this.ElsetNum = (int)xa_tle[TleWrapper.XA_TLE_ELSETNUM];
         this.Inclination_deg = xa_tle[TleWrapper.XA_TLE_INCLI];
         this.Node_deg = xa_tle[TleWrapper.XA_TLE_NODE];
         this.Eccentricity = xa_tle[TleWrapper.XA_TLE_ECCEN];
         this.Omega_deg = xa_tle[TleWrapper.XA_TLE_OMEGA];
         this.MeanAnomaly_deg = xa_tle[TleWrapper.XA_TLE_MNANOM];
         this.MeanMotion_revpday = xa_tle[TleWrapper.XA_TLE_MNMOTN];
         this.RevNum = (int)xa_tle[TleWrapper.XA_TLE_REVNUM];
         this.ObsTime = xa_tle[TleWrapper.XA_TLE_OBSTIME];
         this.EGR = xa_tle[TleWrapper.XA_TLE_EGR];
         this.EDR = xa_tle[TleWrapper.XA_TLE_EDR];
         this.VisMag = xa_tle[TleWrapper.XA_TLE_VISMAG];
         this.RCS = xa_tle[TleWrapper.XA_TLE_RCS];

         this.OrgSatNum = (int)xa_tle[TleWrapper.XA_TLE_ORGSATNUM];


         this.LongSatName = xs_tle.Substring(TleWrapper.XS_TLE_SATNAME_1_12, 12);
         this.ObjType = xs_tle.Substring(TleWrapper.XS_TLE_OBJTYPE_13_1, 11);

         //this.SatLongName = s_tle.Substring(TleWrapper.XS_TLE_SATNAME_1_12, 12);

         byte[] line1 = new byte[512];
         byte[] line2 = new byte[512];
         TleWrapper.TleGPArrayToLines(xa_tle, xs_tle, line1, line2);

         this.Line1 = Utility.BytesToStringLRTrim(line1);
         this.Line2 = Utility.BytesToStringLRTrim(line2);
      }
   }
}
